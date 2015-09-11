
Partial Class Incentive_INCSR_ApprovalQueue
    Inherits System.Web.UI.Page

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' Checking security.
            CheckSecurity()
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                objeAAMS.BindDropDown(ddlAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlApprovalStatus, "INCENTIVE_STATUS", True, 3)
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
            End If
           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "BindData()"
    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
        Try
            objInputXml.LoadXml("<UP_SER_INC_APPROVAL_QUE_INPUT><CHAIN_CODE></CHAIN_CODE><GROUPNAME></GROUPNAME><AOFFICE></AOFFICE><STARTDATE></STARTDATE><EMPLOYEEID></EMPLOYEEID><ENDDATE></ENDDATE><STATUS></STATUS><REC_DATE></REC_DATE><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></UP_SER_INC_APPROVAL_QUE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text.Trim
            objInputXml.DocumentElement.SelectSingleNode("GROUPNAME").InnerText = txtGroupName.Text.Trim
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAoffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("STARTDATE").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateFrom"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("ENDDATE").InnerText = objeAAMS.GetDateFormat(Request.Form("txtStartDateTo"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("REC_DATE").InnerText = objeAAMS.GetDateFormat(Request.Form("txtReceivedDate"), "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("STATUS").InnerText = ddlApprovalStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))

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
                    ViewState("SortName") = "CHAIN_CODE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CHAIN_CODE"
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

            ' Calling Back End Method For Search.
            objOutputXml = objApprovalQue.SearchApprovalQue(objInputXml)



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DETAILS").Rows.Count <> 0 Then
                    gvApprovalQueue.DataSource = ds.Tables("DETAILS")
                    gvApprovalQueue.DataBind()
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
                    hdRecordOnCurrentPage.Value = ds.Tables("DETAILS").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "CHAIN_CODE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select

                        Case "GROUPNAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "AOFFICE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(2).Controls.Add(imgDown)
                            End Select

                        Case "STARTDATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select

                        Case "ENDDATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(4).Controls.Add(imgDown)
                            End Select

                        Case "STATUS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(5).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(5).Controls.Add(imgDown)
                            End Select

                        Case "REC_DATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(6).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(6).Controls.Add(imgDown)
                            End Select

                        Case "INC_TYPE_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvApprovalQueue.HeaderRow.Cells(7).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvApprovalQueue.HeaderRow.Cells(7).Controls.Add(imgDown)
                            End Select

                    End Select
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    gvApprovalQueue.DataSource = Nothing
                    gvApprovalQueue.DataBind()
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("../NoRights.aspx")
                End If
                If strBuilder(1) = "0" Then
                    '  btnNew.Enabled = False
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
#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INCSR_ApprovalQueue.aspx")
    End Sub

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



    Protected Sub gvApprovalQueue_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvApprovalQueue.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvApprovalQueue_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvApprovalQueue.Sorting
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#End Region

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
        '<DETAILS BC_ID='' PREVIOUS_BC_ID ='' CHAIN_CODE='' GROUPNAME='' AOFFICE='' INC_TYPE_NAME='' PAYMENT_CYCLE_NAME='' STARTDATE='' ENDDATE='' REC_DATE='' STATUS =''/>
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Chain Code", "Group Name", "Aoffice", "Expected Date", "Valid Date", "Status", "Received Date", "Incentive Type"}
        Dim intArray() As Integer = {2, 3, 4, 7, 8, 10, 9, 5}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportApprovalQueue.xls")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

 
    Protected Sub gvApprovalQueue_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvApprovalQueue.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '************* Code for edit link ****************************************************************
            Dim hdBC_ID As HiddenField
            Dim hdPREVIOUS_BC_ID As HiddenField
            Dim hdCHAIN_CODE As HiddenField
            Dim btnEdit As LinkButton
            Dim btnHistory As LinkButton
            Dim btnBusinessCase As LinkButton
            hdCHAIN_CODE = CType(e.Row.FindControl("hdChainCode"), HiddenField)
            hdBC_ID = CType(e.Row.FindControl("hdBCID"), HiddenField)
            hdPREVIOUS_BC_ID = CType(e.Row.FindControl("hdPrevBCID"), HiddenField)
            btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            btnHistory = CType(e.Row.FindControl("lnkHistory"), LinkButton)
            btnBusinessCase = CType(e.Row.FindControl("lnkBusinessCase"), LinkButton)
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnHistory.Enabled = False
                Else
                    btnHistory.Attributes.Add("OnClick", "javascript:return History('" + objED.Encrypt(hdBC_ID.Value) + "','" + objED.Encrypt(hdPREVIOUS_BC_ID.Value) + "');")
                End If


                If strBuilder(2) = "0" Then
                    btnEdit.Enabled = False
                Else
                    btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdBC_ID.Value) + "','" + objED.Encrypt(hdPREVIOUS_BC_ID.Value) + "');")
                End If
                ' btnHistory.Attributes.Add("OnClick", "javascript:return History('" + objED.Encrypt(hdBC_ID.Value) + "','" + objED.Encrypt(hdPREVIOUS_BC_ID.Value) + "');")
                'btnBusinessCase.Attributes.Add("OnClick", "javascript:return DetailsFunction('" + hdBC_ID.Value + "','" + hdCHAIN_CODE.Value + "');")

                ' Checking Business Case Security For View Link
                strBuilder = New StringBuilder
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    btnBusinessCase.Enabled = False
                Else
                    btnBusinessCase.Attributes.Add("OnClick", "javascript:return DetailsFunction('" + hdBC_ID.Value + "','" + objED.Encrypt(hdCHAIN_CODE.Value) + "');")
                End If

            Else
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdBC_ID.Value) + "','" + objED.Encrypt(hdPREVIOUS_BC_ID.Value) + "');")
                btnHistory.Attributes.Add("OnClick", "javascript:return History('" + objED.Encrypt(hdBC_ID.Value) + "','" + objED.Encrypt(hdPREVIOUS_BC_ID.Value) + "');")
                btnBusinessCase.Attributes.Add("OnClick", "javascript:return DetailsFunction('" + hdBC_ID.Value + "','" + objED.Encrypt(hdCHAIN_CODE.Value) + "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
