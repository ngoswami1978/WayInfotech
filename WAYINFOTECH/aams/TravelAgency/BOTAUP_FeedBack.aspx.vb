
Partial Class TravelAgency_BOTAUP_FeedBack
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As StringBuilder
    Dim objED As New EncyrptDeCyrpt

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            If Not Page.IsPostBack Then
                If Not Session("Action") = Nothing Then
                    If Session("Action").ToString().Split("|").Length > 1 Then
                        hdLCode.Value = Session("Action").ToString().Split("|").GetValue(1)
                        SearchRecords()
                        lnkClose.Visible = False
                    End If
                End If
                If Not Request.QueryString("LCode") = Nothing And Not Request.QueryString("Popup") = Nothing Then
                    hdLCode.Value = objED.Decrypt(Request.QueryString("LCode"))
                    SearchRecords()
                    MenuControl1.Visible = False
                End If
            End If
            CheckSecurity()

            ' Code For Delete.
            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyFeedBack']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyFeedBack']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    gvFeedBack.Visible = False
                End If
               
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Private Sub SearchRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objFeedback As New AAMS.bizBOHelpDesk.bzFeedback
        Try
            objInputXml.LoadXml("<HD_SEARCHFEEDBACK_INPUT><LCode/><AGENCYNAME/><OfficeID/><FEEDBACK_ID/><ExecutiveName/><HD_RE_ID/><DATEFROM/><DATETO/><LOGGEDBY/><DEPT/><ASSIGNEDTO/><FEEDBACK_STATUS_ID/><ISCRITICAL/><USINGBIRDRES/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHFEEDBACK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdLCode.Value
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
            'Start CODE for sorting and paging

            lblError.Text = ""
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "FEEDBACK_ID"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "FEEDBACK_ID"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
            ' Here Back end Method Call
            objOutputXml = objFeedback.Search(objInputXml)
            ' objOutputXml.Load("c:\staff.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvFeedBack.DataSource = ds.Tables("FEEDBACK")
                gvFeedBack.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record
                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
                If count <> ddlPageNumber.Items.Count Then
                    ddlPageNumber.Items.Clear()
                    For i As Integer = 1 To count
                        ddlPageNumber.Items.Add(i.ToString)
                    Next
                End If
                ddlPageNumber.SelectedValue = selectedValue
                'Code for hiding prev and next button based on count
                If count = 1 Then
                    'pnlPaging.Visible = False
                    ' ddlPageNumber.Visible = False
                    lnkNext.Visible = False
                    lnkPrev.Visible = False
                Else
                    'ddlPageNumber.Visible = True
                    lnkPrev.Visible = True
                    lnkNext.Visible = True
                End If

                'Code for hiding next button when pagenumber is equal to page count
                If ddlPageNumber.SelectedValue = count.ToString Then
                    lnkNext.Visible = False
                Else
                    lnkNext.Visible = True
                End If

                'Code for hiding prev button when pagenumber is 1
                If ddlPageNumber.SelectedValue = "1" Then
                    lnkPrev.Visible = False
                Else
                    lnkPrev.Visible = True
                End If
                hdRecordOnCurrentPage.Value = ds.Tables("FEEDBACK").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "FEEDBACK_ID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "HD_RE_ID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "OFFICEID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select

                    Case "AOFFICE"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                    Case "DATETIME"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "LoggedBy"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select
                End Select
                ' lblError.Text = ""
            Else
                gvFeedBack.DataSource = Nothing
                gvFeedBack.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub

    Sub DeleteRecords()
        'Dim objInputXml, objOutputXml As New XmlDocument
        'Try
        '    Dim objFbDele As New AAMS.bizBOHelpDesk.bzFeedback

        '    objInputXml.LoadXml("<HD_DELETEFEEDBACK_INPUT><FEEDBACK_ID /></HD_DELETEFEEDBACK_INPUT>")
        '    objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = hdDeleteId.Value
        '    hdDeleteId.Value = ""
        '    ' objOutputXml = objFbDele.Delete(objInputXml)
        '    SearchRecords()
        '    'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    '    lblError.Text = objeAAMSMessage.messDelete
        '    'Else
        '    '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    'End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub gvFeedBack_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFeedBack.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdFeedbackID As New HiddenField
            Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("lnkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("lnkDelete")
            hdFeedbackID = e.Row.FindControl("hdFeedbackID")
            Dim objeaams As New eAAMS
            Dim strStatus As String = ""
            Dim strHD_QUERY_GROUP_ID As String = DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_ID")
            If strHD_QUERY_GROUP_ID = 1 Then
                strStatus = "Functional"
            Else
                strStatus = "Technical"
            End If

            'For Select Section 
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyFeedBack']").Count <> 0 Then
                    strBuilder = objeaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyFeedBack']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False ' = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                '    linkEdit.Enabled = False
                'Else

                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdFeedbackID.Value) & "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "HD_RE_ID")) + "','" + strHD_QUERY_GROUP_ID + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "LCODE")) + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "AOFFICE")) + "','" + objED.Encrypt(strStatus) + "');")

                'End If
            Else
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "','" + DataBinder.Eval(e.Row.DataItem, "HD_RE_ID") + "','" + DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_ID") + "','" + DataBinder.Eval(e.Row.DataItem, "LCODE") + "' ,'" + DataBinder.Eval(e.Row.DataItem, "AOFFICE") + "');")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdFeedbackID.Value) & "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "HD_RE_ID")) + "','" + strHD_QUERY_GROUP_ID + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "LCODE")) + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "AOFFICE")) + "','" + objED.Encrypt(strStatus) + "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
            End If

            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "' );")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchRecords()
            
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchRecords()
            
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchRecords()
          
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvFeedBack_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvFeedBack.Sorted
        
    End Sub

    Protected Sub gvFeedBack_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFeedBack.Sorting
        Try


            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            SearchRecords()
           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
