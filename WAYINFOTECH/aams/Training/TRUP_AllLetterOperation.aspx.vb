
Partial Class Training_TRUP_AllLetterOperation
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
            End If

            If Not Page.IsPostBack Then
                '&
                If Request.QueryString("Aoffice") IsNot Nothing Then
                    hdPageAoffice.Value = objED.Decrypt(Request.QueryString("Aoffice"))
                End If
                BindDropDown(ddlAuth, "AurizedSignatory", False)
                If Not Request.QueryString("Operation") Is Nothing Then
                    If Request.QueryString("Operation").ToString = "Print" Then
                        btnSave.Text = "Print All"
                        btnSave.CommandName = "P"
                    ElseIf Request.QueryString("Operation").ToString = "Email" Then
                        btnSave.Text = "Email All"
                        btnSave.CommandName = "E"
                    End If
                End If
                If Request.QueryString("CourseSessionId") IsNot Nothing Then
                    hdPageCourseSessionID.Value = objED.Decrypt(Request.QueryString("CourseSessionId"))
                End If

                Bindata()
                If hdPageCourseSessionID.Value <> "" Then
                    ViewRecords()
                End If

                If rdDistinction.Checked = True Then
                    txtTemplate.ReadOnly = True
                    txtTemplate.CssClass = "textboxgrey"
                    '   Get Default Distinction Letter.
                    GetTemplateDistinction()

                Else

                    txtTemplate.ReadOnly = False
                    txtTemplate.CssClass = "textbox"
                    '   Get Default Distinction Letter.
                    GetTemplateInvitation()
                End If

            End If

            ' Code Modified On 01 Aug 08
            ' Code By Pankaj
            txtTemplate.CssClass = "displayNone"
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Sub ViewRecords()

        '<TR_PARTICIPANTLIST_INPUT>
        '    <TR_COURSES_ID />
        '    <MODE />
        '    <LETTERTYPE />
        '    </TR_PARTICIPANTLIST_INPUT>

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzParticipant As New AAMS.bizTraining.bzParticipantsBasket
        objInputXml.LoadXml("<TR_PARTICIPANTLIST_INPUT><TR_COURSES_ID /><MODE /><LETTERTYPE /></TR_PARTICIPANTLIST_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value
        objInputXml.DocumentElement.SelectSingleNode("MODE").InnerText = btnSave.CommandName 'Command holds Email(E) or Print(P)
        If rdInvitation.Checked = True Then
            objInputXml.DocumentElement.SelectSingleNode("LETTERTYPE").InnerText = "Invitation Letter"
        Else
            objInputXml.DocumentElement.SelectSingleNode("LETTERTYPE").InnerText = "Distinction Letter"
        End If

        objOutputXml = objbzParticipant.CourseSessionParticipantList(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            pnlList.CssClass = ""
            pnlTemplate.CssClass = ""
            FillGrid(objOutputXml)
            hdTabType.Value = "0"
            hdTemplateShowIndicator.Value = "1"
        Else
            If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found" Then
                gvParticipant.DataSource = Nothing
                gvParticipant.DataBind()
                hdTemplateShowIndicator.Value = "0"
                ' hdTabType.Value = "2"
                ' pnlList.CssClass = "displayNone"
                ' pnlTemplate.CssClass = "displayNone"
                If rdInvitation.Checked = True Then
                    If btnSave.CommandName = "P" Then
                        lblError.Text = "No participant to print Invitation Letter"
                    Else
                        lblError.Text = "No participant to send Invitation Letter"
                    End If


                Else
                    If btnSave.CommandName = "P" Then
                        lblError.Text = "No participant to print Distinction Letter"
                    Else
                        lblError.Text = "No participant to send Distinction Letter"
                    End If

                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                gvParticipant.DataSource = Nothing
                gvParticipant.DataBind()
            End If
        End If

    End Sub
    Sub FillGrid(ByVal objOutputXml As XmlDocument)
        Dim objNodeReader As XmlNodeReader
        Dim ds As New DataSet
        objNodeReader = New XmlNodeReader(objOutputXml)
        ds.ReadXml(objNodeReader)
        gvParticipant.DataSource = ds.Tables("DETRAILS")
        gvParticipant.DataBind()
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Template")
            TabText.Add("Participants")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2);")
    End Sub

    Protected Sub rdInvitation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdInvitation.CheckedChanged
        Try
            GetTemplateInvitation()
            ViewRecords()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub rdDistinction_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdDistinction.CheckedChanged
        '   Get Default Distinction Letter.
        Try
            GetTemplateDistinction()
            ViewRecords()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

#Region "GetTemplate()"
    Private Sub GetTemplateDistinction()

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If
            strAoffice = hdPageAoffice.Value
            ' Getting Template 
            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "Distinction Letter"
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = ""
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                txtTemplate.Text = strbody
                hdnmsg.Value = strbody
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub GetTemplateInvitation()

        Try
            Dim strbody As String = ""
            Dim objInputTempXml, objOutputTempXml, objSecurityXml As New XmlDocument
            Dim objbzbzTemplate As New AAMS.bizMaster.bzTemplate
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If
            strAoffice = hdPageAoffice.Value
            ' Getting Template 

            objInputTempXml.LoadXml("<HD_DOCUMENTTEMPLATE_INPUT><TEMPLATES TemplateName='' AOFFICE ='' AIRLINECODE ='' /></HD_DOCUMENTTEMPLATE_INPUT>")
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("TemplateName").Value() = "Invitation Letter"
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AOFFICE").Value() = strAoffice
            objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("AIRLINECODE").Value() = ""
            objOutputTempXml = objbzbzTemplate.GetDocumentTemplate(objInputTempXml)
            Session("Letter") = objOutputTempXml.OuterXml
            If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  hdTemplateVersion.Value = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("T_G_DOC_VERSION").Value
                strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("BDRTemplate").Value
                txtTemplate.Text = strbody
                hdnmsg.Value = strbody
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

#End Region

    Protected Sub gvParticipant_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParticipant.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        If DataBinder.Eval(e.Row.DataItem, "STATUS").ToString.ToUpper = "TRUE" Then
            CType(e.Row.Cells(0).Controls(1), CheckBox).Checked = False
        Else
            CType(e.Row.Cells(0).Controls(1), CheckBox).Checked = True
        End If

    End Sub
    Function CreateXml(ByVal xmlDoc As XmlDocument, ByVal strLetterType As String, ByVal Mode As String, ByRef cn As Integer) As XmlDocument
        xmlDoc.LoadXml("<TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT><LETTER MODE='' TR_PCLETTER='' LETTERTYPE='' TR_COURSES_ID='' SIGNATURE ='' DESIGNATION=''/></TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT>")
        Dim objInsertXml As New XmlDocument
        Dim objDocfrag As XmlDocumentFragment

        objInsertXml.LoadXml("<Root><DETAILS TR_PCLETTER_ID=''  TR_COURSEP_ID='' EMAILID='' /></Root>")
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("MODE").Value = Mode
        'xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("TR_PCLETTER").Value = txtTemplate.Text
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("TR_PCLETTER").Value = hdnmsg.Value
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("LETTERTYPE").Value = strLetterType
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("TR_COURSES_ID").Value = hdPageCourseSessionID.Value
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("SIGNATURE").Value = ddlAuth.SelectedItem.Text
        xmlDoc.DocumentElement.SelectSingleNode("LETTER").Attributes("DESIGNATION").Value = ddlAuth.SelectedValue
        For Each gridRow As GridViewRow In gvParticipant.Rows
            With objInsertXml.DocumentElement.SelectSingleNode("DETAILS")
                If CType(gridRow.FindControl("chkSelect"), CheckBox).Checked = True Then
                    .Attributes("TR_PCLETTER_ID").Value = CType(gridRow.FindControl("hdTR_CLETTER_ID"), HiddenField).Value
                    .Attributes("TR_COURSEP_ID").Value = CType(gridRow.FindControl("hdTR_COURSEP_ID"), HiddenField).Value
                    .Attributes("EMAILID").Value = CType(gridRow.FindControl("txtEmail"), TextBox).Text
                    If CType(gridRow.FindControl("txtEmail"), TextBox).Text <> "" Or Mode = "P" Then
                        objDocfrag = xmlDoc.CreateDocumentFragment
                        objDocfrag.InnerXml = .OuterXml
                        xmlDoc.DocumentElement.AppendChild(objDocfrag)
                        cn = 1
                    End If
                End If
            End With
        Next
        Return xmlDoc
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If btnSave.CommandName = "P" Then
                '<TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT>
                '<LETTER TR_PCLETTER='' LETTERTYPE='' TR_COURSES_ID=''/> 
                '<DETAILS  TR_COURSEP_ID='' EMAILID='' />
                '</TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT>
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim strLetterType As String
                Dim intPrintParticipantCount As Integer = 0
                If rdInvitation.Checked = True Then
                    strLetterType = "Invitation"
                Else
                    strLetterType = "Distinction"
                End If

                objInputXml = CreateXml(objInputXml, strLetterType, btnSave.CommandName, intPrintParticipantCount)
                If intPrintParticipantCount = 1 Then
                    Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
                    objOutputXml = objbzParticipant.GetParticilantList_Print_Email(objInputXml)


                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ViewRecords()
                        Session("LetterIDs") = objOutputXml.DocumentElement.SelectSingleNode("PARTICIPANT").Attributes("LETTERIDS").Value
                        'Session("LetterType") = strLetterType + " Letter"
                        If strLetterType = "Invitation" Then
                            Response.Redirect("../RPSR_ReportShow.aspx?Case=InvitationLetter")
                            ' ltrPrint.Text = "<iframe id='iframeBarCode' src='TRPR_PrintLetter.aspx?LetterType=Invitation Letter'  scrolling='no' width='0' height='0' frameborder='0' style=''></iframe>"
                        Else
                            Response.Redirect("../RPSR_ReportShow.aspx?Case=DistinctionLetter")
                            ' ltrPrint.Text = "<iframe id='iframeBarCode' src='TRPR_PrintLetter.aspx?LetterType=Distinction Letter'  scrolling='no' width='0' height='0' frameborder='0' style=''></iframe>"
                        End If

                        ClientScript.RegisterStartupScript(Me.GetType, "strLetter", "<script>iframeBarCode.focus();iframeBarCode.print();</script>")
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    lblError.Text = "You haven't selected any participant"
                End If
            End If
            If btnSave.CommandName = "E" Then
                '<TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT>
                '<LETTER TR_PCLETTER='' LETTERTYPE='' TR_COURSES_ID=''/> 
                '<DETAILS  TR_COURSEP_ID='' EMAILID='' />
                '</TR_PARTICIPANTLIST_PRINT_EMAIL_INPUT>
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim strLetterType As String
                Dim intEmailParticipantCount As Integer = 0
                If rdInvitation.Checked = True Then
                    strLetterType = "Invitation"
                Else
                    strLetterType = "Distinction"
                End If
                objInputXml = CreateXml(objInputXml, strLetterType, btnSave.CommandName, intEmailParticipantCount)
                If intEmailParticipantCount = 1 Then
                    Dim objbzParticipant As New AAMS.bizTraining.bzParticipant
                    objOutputXml = objbzParticipant.GetParticilantList_Print_Email(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = "Emailed Successfully"
                        ViewRecords()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    lblError.Text = "You haven't selected any participant"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try

    End Sub
#Region "BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)"
    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean)
        Try
            Dim objInputAurizedSignatoryXml, objSecurityXml, objOutputAurizedSignatoryXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objbzDocEmployee As New AAMS.bizMaster.bzDocEmployee
            Dim ds As New DataSet
            Dim strAoffice As String = ""

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '        strAoffice = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
            '    Else
            '        strAoffice = ""
            '    End If
            'End If
            strAoffice = hdPageAoffice.Value
            drpDownList.Items.Clear()
            Select Case strType
                Case "AurizedSignatory"
                    objInputAurizedSignatoryXml.LoadXml("<DOCUMENTEMPLOYEE_INPUT><DOCUMENT Aoffice='' /></DOCUMENTEMPLOYEE_INPUT>")
                    objInputAurizedSignatoryXml.DocumentElement.SelectSingleNode("DOCUMENT").Attributes("Aoffice").InnerText = strAoffice

                    'Here Back end Method Call
                    objOutputAurizedSignatoryXml = objbzDocEmployee.List(objInputAurizedSignatoryXml)

                    If objOutputAurizedSignatoryXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputAurizedSignatoryXml)
                        ds.ReadXml(objXmlReader)
                        drpDownList.DataSource = ds.Tables("DOCUMENT")
                        drpDownList.DataTextField = "EmployeeName"
                        drpDownList.DataValueField = "DESIGNATION"
                        drpDownList.DataBind()
                    End If
            End Select
            If bolSelect = True Then
                drpDownList.Items.Insert(0, New ListItem("Select One", ""))
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString())
    End Sub
End Class
