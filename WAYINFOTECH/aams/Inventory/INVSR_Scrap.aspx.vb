
Partial Class Inventory_INVSR_Scrap
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            'txtDateFrom.Text = Request.Form("txtDateFrom")
            'txtDateTo.Text = Request.Form("txtDateTo")
            ' Deleting Trash Records.
            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            ' Checking security.
            CheckSecurity()
            If Not Page.IsPostBack Then
                FillGodown()
                'objeAAMS.BindDropDown(ddlGodown, "GODOWN", True)
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                ' This code is used for checking session handler according to user login.


            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Sub FillGodown()

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        ' This code binds godown on the basis of logged user
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            Try
                ddlGodown.Items.Clear()
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                'If  ChallanRegionWiseGodown value is "1 or 2" then fill Region id else cityname
                If Session("Security") IsNot Nothing Then
                    Dim strRegionId As String = objeAAMS.SecurityRegionID(Session("Security"))
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId

                    '    'Commented on 27 may 08
                    '    'If  ChallanRegionWiseGodown value isnot "1 or 2" then return -100
                    '    If strRegionId = "-100" Then
                    '        'if challan category is customer and agency is selected from popup than city is passed (but in this case we can pass aoffice too)
                    '        'else aoffice will be passed
                    '        If ddlChallanCategory.SelectedValue = "1" And txtCity.Text <> "" Then
                    '            objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                    '        Else
                    '            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objeAAMS.AOffice(Session("Security"))
                    '        End If

                    '    Else
                    '        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId
                    '    End If
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                End If

                'End Comment
                objOutputXml = objbzGodown.ListGodownAoffice(objInputXml)
                'objOutputXml.LoadXml("<INV_AGENCYPENGINDORDER_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors><AGENCYORDER ORDER_NUMBER='2008/2/366' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /><AGENCYORDER ORDER_NUMBER='2008/2/367' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /></INV_AGENCYPENGINDORDER_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    ddlGodown.DataSource = ds.Tables("GODOWN").DefaultView
                    ddlGodown.DataTextField = "GODOWNNAME"
                    ddlGodown.DataValueField = "GODOWNID"
                    ddlGodown.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                ddlGodown.Items.Insert(0, New ListItem("All", ""))
            End Try
        Else
            objeAAMS.BindDropDown(ddlGodown, "GODOWN", True, 3)
        End If


    End Sub

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("INVUP_Scrap.aspx?Action=I")
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_Scrap.aspx")
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInvScrap As New AAMS.bizInventory.bzScrap
        Try
            objInputXml.LoadXml("<INV_SEARCH_SCRAP_INPUT><TrashID></TrashID><GodownID></GodownID><LoggedbyID></LoggedbyID><TrashDateFrom></TrashDateFrom><TrashDateTo></TrashDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_SCRAP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TrashID").InnerText = txtTrashID.Text.Trim()
            If ddlGodown.SelectedIndex <> "0" Then
                objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ddlGodown.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("LoggedbyID").InnerText = hdEmployeeID.Value.Trim()
            objInputXml.DocumentElement.SelectSingleNode("TrashDateFrom").InnerText = objeAAMS.ConvertTextDate(Request.Form("txtDateFrom"))
            objInputXml.DocumentElement.SelectSingleNode("TrashDateTo").InnerText = objeAAMS.ConvertTextDate(Request.Form("txtDateTo"))

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
                ViewState("SortName") = "TrashID"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "TrashID" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            objOutputXml = objInvScrap.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("SCRAP").Rows.Count <> 0 Then
                    gvScrap.DataSource = ds.Tables("SCRAP")
                    gvScrap.DataBind()
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
                    txtRecordOnCurrentPage.Text = ds.Tables("SCRAP").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString()
                        Case "TrashID"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvScrap.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvScrap.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "GodownName"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvScrap.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvScrap.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select
                        Case "Employee"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvScrap.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvScrap.HeaderRow.Cells(2).Controls.Add(imgDown)

                            End Select
                        Case "LoggedDate"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvScrap.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvScrap.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select
                    End Select
                    '' @ Added Code To Show Image'
                    ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@





                    '*************************************
                Else
                    gvScrap.DataSource = Nothing
                    gvScrap.DataBind()
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = "0"
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub
#End Region

#Region "gvScrap_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvScrap.RowDataBound"
    Protected Sub gvScrap_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvScrap.RowDataBound
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdOrderID.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = True
                'Else
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdOrderID.Value) + "');")
                'End If
            Else
            lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdOrderID.Value) + "');")
            btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdOrderID.Value + "');")
            End If
            '**********************************************
            'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            'btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdOrderID.Value + "');")
            'btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdOrderID.Value + "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "DeleteRecords()"
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objInvScrap As New AAMS.bizInventory.bzScrap
            If hdDeleteId.Value <> "" Then
                objInputXml.LoadXml("<INV_DELETE_SCRAP_INPUT><TrashID></TrashID></INV_DELETE_SCRAP_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("TrashID").InnerText = hdDeleteId.Value
                hdDeleteId.Value = ""
                objOutputXml = objInvScrap.Delete(objInputXml)
                BindData()
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SCRAP']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
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

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvScrap_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvScrap.Sorting
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
            BindData()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        gvScrap.AllowSorting = False
        gvScrap.HeaderStyle.ForeColor = Drawing.Color.Black
        BindDataExport()

        ' grdMIDT.Columns(grdMIDT.Columns.Count - 1).Visible = False
        If gvScrap.Rows.Count > 0 Then
            ' PrepareGridViewForExport(grdMIDT)
            '   ExportGridView(grdMIDT, "MIDT.xls")
        End If
    End Sub
    Private Sub BindDataExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objInvScrap As New AAMS.bizInventory.bzScrap
        Try
            objInputXml.LoadXml("<INV_SEARCH_SCRAP_INPUT><TrashID></TrashID><GodownID></GodownID><LoggedbyID></LoggedbyID><TrashDateFrom></TrashDateFrom><TrashDateTo></TrashDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_SCRAP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TrashID").InnerText = txtTrashID.Text.Trim()
            If ddlGodown.SelectedIndex <> "0" Then
                objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ddlGodown.SelectedValue
            Else
                objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("LoggedbyID").InnerText = hdEmployeeID.Value.Trim()
            objInputXml.DocumentElement.SelectSingleNode("TrashDateFrom").InnerText = objeAAMS.ConvertTextDate(Request.Form("txtDateFrom"))
            objInputXml.DocumentElement.SelectSingleNode("TrashDateTo").InnerText = objeAAMS.ConvertTextDate(Request.Form("txtDateTo"))
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "TrashID"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "TrashID" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
            objOutputXml = objInvScrap.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("SCRAP").Rows.Count <> 0 Then
                    gvScrap.DataSource = ds.Tables("SCRAP")
                    gvScrap.DataBind()
                End If

                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Employee", "GodownName", "LoggedDate", "TrashID"}
                Dim intArray() As Integer = {0, 1, 2, 3}
                objExport.ExportDetails(objOutputXml, "SCRAP", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportREport.xls")
                '*************************************
            Else
                gvScrap.DataSource = String.Empty
                gvScrap.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                'pnlPaging.Visible = False
                ' txtTotalRecordCount.Text = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
