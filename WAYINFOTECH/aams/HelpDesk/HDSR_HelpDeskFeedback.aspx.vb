
Partial Class HelpDesk_HDSR_HelpDeskFeedback
    Inherits System.Web.UI.Page
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try
            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(drpFeedBackStatus, "FEEDBACKSTATUS", True, 3)
                objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(drpFeedbackDept, "FeedbackDepartment", True, 3)
            End If
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    '   btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Changed Index
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                gvFeedBack.Columns(7).Visible = False
            End If
            If chkTechnicalStatus.Checked = False Then
                gvFeedBack.Columns(8).Visible = False
            End If
            If chkSalesstatus.Checked = False Then
                gvFeedBack.Columns(9).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                gvFeedBack.Columns(10).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                gvFeedBack.Columns(11).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                gvFeedBack.Columns(12).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Changed Index
   

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("HDSR_HelpDeskFeedback.aspx")
    End Sub

    Sub SearchRecords(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objFeedback As New AAMS.bizHelpDesk.bzFeedback
        'objInputXml.LoadXml("<HD_SEARCHFEEDBACK_INPUT><LCode/><AGENCYNAME/><OfficeID/><FEEDBACK_ID/><ExecutiveName/><HD_RE_ID/><DATEFROM/><DATETO/></HD_SEARCHFEEDBACK_INPUT>")
        objInputXml.LoadXml("<HD_SEARCHFEEDBACK_INPUT><LCode/><REGION/> <AGENCYNAME/><OfficeID/><FEEDBACK_ID/><ExecutiveName/><HD_RE_ID/><DATEFROM/><DATETO/><LOGGEDBY/><DEPT/><ASSIGNEDTO/><FEEDBACK_STATUS_ID/><ISCRITICAL/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><EmployeeID/><SUG_DEPT_ID/></HD_SEARCHFEEDBACK_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value
        objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
        objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
        objInputXml.DocumentElement.SelectSingleNode("ExecutiveName").InnerText = txtExecutiveName.Text
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
        objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.GetDateFormat(txtDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.GetDateFormat(txtDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = txtFeedBackID.Text
        objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = txtLoggedBy.Text
        objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDTO").InnerText = txtAssignedTo.Text
        objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_STATUS_ID").InnerText = drpFeedBackStatus.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("ISCRITICAL").InnerText = chkCritical.Checked
        objInputXml.DocumentElement.SelectSingleNode("SUG_DEPT_ID").InnerText = drpFeedbackDept.SelectedValue

        ' Security Input Xml Start.
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
        ' Security Input Xml End.


        'Dim strDept As String = ""
        'If chkHelpDeskStatus.Checked Then
        '    strDept = "1,"
        'End If

        'If chkTechnicalStatus.Checked Then
        '    strDept = strDept & "2,"
        'End If

        'If chkSalesstatus.Checked Then
        '    strDept = strDept & "3,"
        'End If

        'If chkTrainingStatus.Checked Then
        '    strDept = strDept & "4,"
        'End If

        'If chkProductStatus.Checked Then
        '    strDept = strDept & "5,"
        'End If
        'If strDept.Length > 0 Then
        '    strDept = strDept.Substring(0, strDept.Length - 1)
        'End If
        'objInputXml.DocumentElement.SelectSingleNode("ISCRITICAL").InnerText = strDept

        'Start CODE for sorting and paging
        If Operation = PageOperation.Search Then

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
                ViewState("SortName") = "HD_RE_ID"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_RE_ID"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
        End If
        'End Code for paging and sorting
        ' Here Back end Method Call
        ' Here Back end Method Call
        objOutputXml = objFeedback.Search(objInputXml)
        ' objOutputXml.Load("c:\staff.xml")
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If
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
            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "FEEDBACK_ID"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgDown)
            '        End Select
            '    Case "HD_RE_ID"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgDown)
            '        End Select
            '    Case "AGENCYNAME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgDown)

            '        End Select


            '    Case "OFFICEID"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgDown)
            '        End Select

            '    Case "AOFFICE"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgDown)
            '        End Select
            '    Case "DATETIME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgDown)
            '        End Select
            '    Case "STATUS_HELPDESK"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(6).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(6).Controls.Add(imgDown)
            '        End Select
            '    Case "STATUS_TECHNICAL"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(7).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(7).Controls.Add(imgDown)
            '        End Select
            '    Case "STATUS_SALES"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(8).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(8).Controls.Add(imgDown)
            '        End Select
            '    Case "STATUS_TRAINING"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(9).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(9).Controls.Add(imgDown)
            '        End Select
            '    Case "STATUS_PRODUCT"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvFeedBack.HeaderRow.Cells(10).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvFeedBack.HeaderRow.Cells(10).Controls.Add(imgDown)
            '        End Select
            'End Select
            ' lblError.Text = ""

            SetImageForSorting(gvFeedBack)
        Else
            gvFeedBack.DataSource = Nothing
            gvFeedBack.DataBind()
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

    Sub DeleteRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objFbDele As New AAMS.bizHelpDesk.bzFeedback

            objInputXml.LoadXml("<HD_DELETEFEEDBACK_INPUT><FEEDBACK_ID /></HD_DELETEFEEDBACK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = hdDeleteId.Value
            hdDeleteId.Value = ""
            objOutputXml = objFbDele.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                SearchRecords(PageOperation.Search)
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Count <> 0 Then
                    strBuilder = objeaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False ' = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                '    linkEdit.Enabled = False
                'Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdFeedbackID.Value) & "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "HD_RE_ID")) + "','" + DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_ID") + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "LCODE")) + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "AOFFICE")) + "','" + objED.Encrypt(strStatus) + "');")


                'End If
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdFeedbackID.Value) & "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "HD_RE_ID")) + "','" + DataBinder.Eval(e.Row.DataItem, "HD_QUERY_GROUP_ID") + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "LCODE")) + "','" + objED.Encrypt(DataBinder.Eval(e.Row.DataItem, "AOFFICE")) + "','" + objED.Encrypt(strStatus) + "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
            End If

            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdFeedbackID.Value & "' );")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdFeedbackID.Value & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#Region "Code for Paging And sorting."

    'Changed Index
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchRecords(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                gvFeedBack.Columns(7).Visible = False
            End If
            If chkTechnicalStatus.Checked = False Then
                gvFeedBack.Columns(8).Visible = False
            End If
            If chkSalesstatus.Checked = False Then
                gvFeedBack.Columns(9).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                gvFeedBack.Columns(10).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                gvFeedBack.Columns(11).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                gvFeedBack.Columns(12).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Changed Index
    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchRecords(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                gvFeedBack.Columns(7).Visible = False
            End If
            If chkTechnicalStatus.Checked = False Then
                gvFeedBack.Columns(8).Visible = False
            End If
            If chkSalesstatus.Checked = False Then
                gvFeedBack.Columns(9).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                gvFeedBack.Columns(10).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                gvFeedBack.Columns(11).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                gvFeedBack.Columns(12).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Changed Index
    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchRecords(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                gvFeedBack.Columns(7).Visible = False
            End If
            If chkTechnicalStatus.Checked = False Then
                gvFeedBack.Columns(8).Visible = False
            End If
            If chkSalesstatus.Checked = False Then
                gvFeedBack.Columns(9).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                gvFeedBack.Columns(10).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                gvFeedBack.Columns(11).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                gvFeedBack.Columns(12).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Changed



    Protected Sub gvFeedBack_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvFeedBack.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    'Changed Index
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
            SearchRecords(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                gvFeedBack.Columns(7).Visible = False
            End If
            If chkTechnicalStatus.Checked = False Then
                gvFeedBack.Columns(8).Visible = False
            End If
            If chkSalesstatus.Checked = False Then
                gvFeedBack.Columns(9).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                gvFeedBack.Columns(10).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                gvFeedBack.Columns(11).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                gvFeedBack.Columns(12).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    'Changed Index
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchRecords(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        '    <CALLREQUEST HD_RE_ID="" LCODE="" OfficeID="" AgencyName="  " HD_RE_OPEN_DATE="
        'HD_RE_CLOSED_DATE="" LoggedBy="" HD_QUERY_GROUP_NAME="" HD_STATUS_NAME=" " COLOR_CODE="" />
        '<FEEDBACK FEEDBACK_ID="30" HD_RE_ID="113" HD_QUERY_GROUP_ID="1" LCODE="3179" AGENCYNAME="Legend Travels Pvt.Ltd" 
        'AOFFICE="DEL" DATETIME="16-May-08" OFFICEID="DELUG3195" STATUS_HELPDESK="" 
        'STATUS_TECHNICAL="" STATUS_SALES="" STATUS_TRAINING=""
        '	          STATUS_PRODUCT=""/>
        'Dim strArray() As String = {"FeedBack ID", "LTR No", "Agency Name", "Office Id", "Aoffice", "Date", "Region", "HelpDesk Status", "Techanical Status", "Sales Status", "Training Status", "Product Status", "Corporate Comm."}
        'Dim intArray() As Integer = {0, 1, 4, 7, 5, 6, 21, 8, 9, 10, 11, 12, 19}

        Dim strArray() As String = {"FeedBack ID", "LTR No", "Agency Name", "Office Id", "Aoffice", "Region", "Date", "HelpDesk Status", "Techanical Status", "Sales Status", "Training Status", "Product Status", "Corporate Comm."}
        Dim intArray() As Integer = {0, 1, 4, 7, 5, 21, 6, 8, 9, 10, 11, 12, 19}


        Dim strALText As New ArrayList
        For i As Integer = 0 To strArray.Length - 1
            strALText.Add(strArray(i))
        Next

        Dim strALIndex As New ArrayList
        For i As Integer = 0 To intArray.Length - 1
            strALIndex.Add(intArray(i))
        Next

        If chkHelpDeskStatus.Checked = False Then
            strALText.Remove("HelpDesk Status")
            strALIndex.Remove(8)
        End If
        If chkTechnicalStatus.Checked = False Then
            strALText.Remove("Techanical Status")
            strALIndex.Remove(9)
        End If
        If chkSalesstatus.Checked = False Then
            strALText.Remove("Sales Status")
            strALIndex.Remove(10)
        End If
        If chkTrainingStatus.Checked = False Then
            strALText.Remove("Training Status")
            strALIndex.Remove(11)
        End If
        If chkProductStatus.Checked = False Then
            strALText.Remove("Product Status")
            strALIndex.Remove(12)
        End If
        If chkCorporateCommunication.Checked = False Then
            strALText.Remove("Corporate Comm.")
            strALIndex.Remove(19)
        End If
        Dim myStrArr As String() = CType(strALText.ToArray(GetType(String)), String())
        Dim myIntArr() As Integer = CType(strALIndex.ToArray(GetType(Integer)), Integer())


        objExport.ExportDetails(objOutputXml, "FEEDBACK", myIntArr, myStrArr, ExportExcel.ExportFormat.Excel, "exportHelpDeskFeedback.xls")
    End Sub
   
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class

