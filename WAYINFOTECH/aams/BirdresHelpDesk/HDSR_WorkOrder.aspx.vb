
Partial Class BirdresHelpDesk_HDSR_WorkOrder
    Inherits System.Web.UI.Page
    Dim objED As New EncyrptDeCyrpt

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                txtAgencyName.Focus()
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                ' btnReset.Attributes.Add("onClick", "return ClearControls();")
                objeAAMS.BindDropDown(drpOrderType, "BRWorkOrderType", True, 3)
                objeAAMS.BindDropDown(drpFollowUp, "BRWorkOrderFollowUp", True, 3)
                objeAAMS.BindDropDown(drpSeverity, "BRWorkOrderSeverity", True, 3)
                objeAAMS.BindDropDown(drpAssignedTo, "BRWorkOrderAssignee", True, 3)
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpStatus, "BRQueryStatus", True, 3)

            End If
            ' Checking security.
            CheckSecurity()
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            '   Deleting records.
            If (hdWOrderId.Value <> "") Then
                DeleteRecords()
            End If

            '   Checking Permission For Own Office start.
            If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                drpAoffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                drpAoffice.Enabled = False
            End If

            '   Checking Permission For Own Office end.

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "gvOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowDataBound"
    Protected Sub gvOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '#############################################################
            ' Code added For Selecting an Items 

            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True

                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If
            '************* Code for edit link ****************************************************************
            Dim hdOrderID As HiddenField
            Dim btnEdit As LinkButton
            hdOrderID = CType(e.Row.FindControl("hdOrderId"), HiddenField)
            btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            '************* end code for edit ***************************************************************** 

            '************* Code for Delete link ****************************************************************
            Dim btnDelete As LinkButton
            btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            '************* end code for delete link ***************************************************************** 
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdOrderID.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = False
                'Else
                '    btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdOrderID.Value + "');")
                'End If
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdOrderID.Value) + "');")
            Else
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdOrderID.Value) + "');")
                btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdOrderID.Value + "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objWorkOrder As New AAMS.bizBRHelpDesk.bzWorkOrder
        Try
            objInputXml.LoadXml("<HD_SEARCHWORKORDER_INPUT><AGENCYNAME /><LTRNO /><WO_NUMBER /><WO_TITLE /><STATUS /><WO_SEVERITY_ID /><WO_FOLLOWUP_ID  /><LOGGEDBY /><WO_ASSIGNEE_ID /><ASSIGNED_DATE /><WO_TYPE_ID /><AOFFICE /><WO_OPENDATE_FROM /><WO_OPENDATE_TO /><WO_CLOSEDATE_FROM /><WO_CLOSEDATE_TO /><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><EmployeeID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHWORKORDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("LTRNO").InnerText = txtLTRNo.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("WO_NUMBER").InnerText = txtOrderNo.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("WO_TITLE").InnerText = txtOrderTitle.Text.Trim()
            If (drpStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = drpStatus.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = ""
            End If
            If (drpSeverity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("WO_SEVERITY_ID").InnerText = drpSeverity.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_SEVERITY_ID").InnerText = ""
            End If

            If (drpFollowUp.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("WO_FOLLOWUP_ID").InnerText = drpFollowUp.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_FOLLOWUP_ID").InnerText = ""
            End If

            If (drpAssignedTo.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("WO_ASSIGNEE_ID").InnerText = drpAssignedTo.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_ASSIGNEE_ID").InnerText = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNED_DATE").InnerText = objeAAMS.ConvertTextDate(txtAssignedDate.Text)
            If (drpOrderType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("WO_TYPE_ID").InnerText = drpOrderType.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_TYPE_ID").InnerText = ""
            End If
            If (drpAoffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = txtLoggedBy.Text
            objInputXml.DocumentElement.SelectSingleNode("WO_OPENDATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtOpenDateFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("WO_OPENDATE_TO").InnerText = objeAAMS.ConvertTextDate(txtOpenDateTo.Text)
            objInputXml.DocumentElement.SelectSingleNode("WO_CLOSEDATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtCloseDateFrom.Text)
            objInputXml.DocumentElement.SelectSingleNode("WO_CLOSEDATE_TO").InnerText = objeAAMS.ConvertTextDate(txtCloseDateTo.Text)

            ' Security Input Xml Start.
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            ' Security Input Xml End.

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
                    ViewState("SortName") = "AGENCYNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCYNAME"
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

            ' Calling Search method.
            objOutputXml = objWorkOrder.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("WORKORDER").Rows.Count <> 0 Then
                    gvOrder.DataSource = ds.Tables("WORKORDER")
                    gvOrder.DataBind()
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
                    hdRecordOnCurrentPage.Value = ds.Tables("WORKORDER").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' 
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "AGENCYNAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "ADDRESS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_NUMBER"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(2).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_TITLE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "LTRNO"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(4).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_FOLLOWUP_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(5).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(5).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_SEVERITY_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(6).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(6).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_TYPE_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(7).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(7).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "LOGGEDBY"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(8).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(8).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_OPENDATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(9).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(9).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_CLOSEDATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(10).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(10).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "WO_ASSIGNEE_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(11).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(11).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "ASSIGNED_DATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(12).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(12).Controls.Add(imgDown)
                            End Select
                    End Select
                    Select Case ViewState("SortName")
                        Case "STATUS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvOrder.HeaderRow.Cells(13).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvOrder.HeaderRow.Cells(13).Controls.Add(imgDown)
                            End Select
                    End Select
                    '  Added Code To Show Image '

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record

                Else
                    gvOrder.DataSource = Nothing
                    gvOrder.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "DeleteRecords()"
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objWorkOrder As New AAMS.bizBRHelpDesk.bzWorkOrder
            If hdWOrderId.Value <> "" Then
                objInputXml.LoadXml("<HD_DELETEWORKORDER_INPUT><WO_ID /></HD_DELETEWORKORDER_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("WO_ID").InnerText = hdWOrderId.Value
                hdWOrderId.Value = ""
                objOutputXml = objWorkOrder.Delete(objInputXml)
                BindData(PageOperation.Search)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objeAAMSMessage.messDelete
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
                'If strBuilder(1) = "0" Then
                '    btnNew.Enabled = False
                'End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtAgencyName.Text = ""
            'hdAgencyID.Value = ""
            'txtAssignedDate.Text = ""
            'txtCloseDateFrom.Text = ""
            'txtCloseDateTo.Text = ""
            'txtLoggedBy.Text = ""
            'hdLoggedBy.Value = ""
            'txtLTRNo.Text = ""
            'txtOpenDateFrom.Text = ""
            'txtOpenDateTo.Text = ""
            'txtOrderNo.Text = ""

            'drpAoffice.SelectedIndex = 0
            'drpAssignedTo.SelectedIndex = 0
            'drpFollowUp.SelectedIndex = 0
            'drpOrderType.SelectedIndex = 0
            'drpSeverity.SelectedIndex = 0
            'drpStatus.SelectedIndex = 0
            'gvOrder.DataSource = Nothing
            'gvOrder.DataBind()
            Response.Redirect("HDSR_WorkOrder.aspx")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvOrder.Sorting
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
            BindData(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
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
        Dim strArray() As String = {"Agency Name", "Address", "Order No.", "Order Title", "LTR No.", "FollowUp", "Severity", "Order Type", "Logged By", "Open Date", "Close Date", "Assignee", "Assigned Date", "Status"}
        Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}
        '<WORKORDER 
        '        WO_ID = "1"
        '        AGENCYNAME = "Gitanjali Travels"
        '        ADDRESS = "19b, Basant Lok Community Cent Vasant Vihar,"
        '        WO_NUMBER = "1212"
        '        WO_TITLE = ""
        '        LTRNO = "1"
        '        WO_FOLLOWUP_NAME = "Half"
        '        WO_SEVERITY_NAME = "High"
        '        WO_TYPE_NAME = "Data Transfer"
        '        LOGGEDBY = "Admin"
        '        WO_OPENDATE = "16-Mar-08"
        '        WO_CLOSEDATE = "16-Mar-08"
        '        WO_ASSIGNEE_NAME = "WO Offline"
        '        ASSIGNED_DATE = "16-Mar-08"
        ' STATUS="Solved - Online" /> 
        objExport.ExportDetails(objOutputXml, "WORKORDER", intArray, strArray, ExportExcel.ExportFormat.Excel, "WORKORDER.xls")
    End Sub
    'End Code For Export

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
