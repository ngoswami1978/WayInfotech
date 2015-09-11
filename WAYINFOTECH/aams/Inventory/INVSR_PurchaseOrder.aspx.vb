
Partial Class Inventory_INVSR_PurchaseOrder
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Inventory/INVSR_PurchaseOrder.aspx"
            objEaams.ExpirePageCache()

            ' btnNew.Attributes.Add("onclick", "return InsertPtrAssignee();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Attributes("Value").Value)
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
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If

            If hdDeleteFlag.Value <> "" Then
                DeletePurchaseOrders()
                GetSearchResult(PageOperation.Search)
            End If

            If Not Page.IsPostBack Then
                BindDropDowns()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("INVUP_PurchaseOrder.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            GetSearchResult(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GetSearchResult(ByVal Operation As Integer)
        Try
            Dim objHdInputX, objHdOutputX As New XmlDocument
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            'PurchaseOrderID
            objHdInputX.LoadXml("<INV_SEARCH_PURCHASE_INPUT><PurchaseOrderID></PurchaseOrderID><SupplierID></SupplierID><SupplierName></SupplierName><PRODUCTID></PRODUCTID><OrderDateFrom></OrderDateFrom><OrderDateFromTo></OrderDateFromTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_PURCHASE_INPUT>")
            With objHdInputX.DocumentElement

                .SelectSingleNode("PurchaseOrderID").InnerText = txtpurchaseOrdrNo.Text.Trim()

                If drpSupplierName.SelectedIndex <> 0 Then
                    .SelectSingleNode("SupplierID").InnerText = drpSupplierName.SelectedValue
                    .SelectSingleNode("SupplierName").InnerText = drpSupplierName.SelectedItem.Text
                End If

                If drpProduct.SelectedIndex <> 0 Then
                    .SelectSingleNode("PRODUCTID").InnerText = drpProduct.SelectedValue
                End If
                '.SelectSingleNode("OrderDateFrom").InnerText = objEaams.ConvertTextDateBlank(txtOpenDtFrm.Text.Trim())
                '.SelectSingleNode("OrderDateFromTo").InnerText = objEaams.ConvertTextDateBlank(txtOpenDtTo.Text.Trim())
                'Start Rakesh Code
                .SelectSingleNode("OrderDateFrom").InnerText = objEaams.GetDateFormat(txtOpenDtFrm.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
                .SelectSingleNode("OrderDateFromTo").InnerText = objEaams.GetDateFormat(txtOpenDtTo.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
                'end
            End With
            Dim objHdOrderSearch As New AAMS.bizInventory.bzPurchaseOrder

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then

                If ViewState("PrevSearching") Is Nothing Then
                    objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objHdInputX.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objHdInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objHdInputX.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "OrderDate"
                    objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "OrderDate" '"LOCATION_CODE"
                Else
                    objHdInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objHdInputX.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting

            objHdOutputX = objHdOrderSearch.Search(objHdInputX)


            If objHdOutputX.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objHdInputX.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objHdOutputX)
                    Exit Sub
                End If

                objReader = New XmlNodeReader(objHdOutputX)
                dSet.ReadXml(objReader)
                grdvPurchaseOrder.DataSource = dSet.Tables("PURCHASEORDER")
                grdvPurchaseOrder.DataBind()
                txtRecordOnCurrReco.Text = grdvPurchaseOrder.Rows.Count.ToString
                'Code Added For Paging And Sorting In case Of Delete The Record
                If dSet.Tables("PURCHASEORDER").Rows.Count <> 0 Then
                    pnlPaging.Visible = True
                    Dim count As Integer = CInt(objHdOutputX.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                    hdRecordOnCurrentPage.Value = dSet.Tables("PURCHASEORDER").Rows.Count.ToString
                    txtTotalRecordCount.Text = objHdOutputX.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString()
                        Case "PurchaseOrderID"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdvPurchaseOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdvPurchaseOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "OrderDate"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdvPurchaseOrder.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdvPurchaseOrder.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "CreationDate"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdvPurchaseOrder.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdvPurchaseOrder.HeaderRow.Cells(2).Controls.Add(imgDown)

                            End Select
                        Case "SupplierName"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdvPurchaseOrder.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdvPurchaseOrder.HeaderRow.Cells(3).Controls.Add(imgDown)

                            End Select
                        Case "PRODUCTNAME"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdvPurchaseOrder.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdvPurchaseOrder.HeaderRow.Cells(4).Controls.Add(imgDown)

                            End Select
                    End Select
                    '  Added Code To Show Image'
                Else
                    grdvPurchaseOrder.DataSource = Nothing
                    grdvPurchaseOrder.DataBind()
                    txtTotalRecordCount.Text = "0"
                    txtRecordOnCurrReco.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                grdvPurchaseOrder.DataSource = Nothing
                grdvPurchaseOrder.DataBind()
                lblError.Text = objHdOutputX.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Private Sub BindDropDowns()
        Try
            objEaams.BindDropDown(drpProduct, "EQUIPMENT", True, 3)
            objEaams.BindDropDown(drpSupplierName, "SUPPLIER", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DeletePurchaseOrders()
        Dim objHdInputX, objOutputXml As New XmlDocument
        Try
            Dim objhdOrderDel As New AAMS.bizInventory.bzPurchaseOrder

            objHdInputX.LoadXml("<INV_DELETE_PURCHASEORDER_INPUT><PurchaseOrderID></PurchaseOrderID></INV_DELETE_PURCHASEORDER_INPUT>")
            objHdInputX.DocumentElement.SelectSingleNode("PurchaseOrderID").InnerText = hdDeleteFlag.Value.Trim()
            hdDeleteFlag.Value = ""
            'Call a function
            objOutputXml = objhdOrderDel.Delete(objHdInputX)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvPurchaseOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvPurchaseOrder.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdDelFlag As HiddenField
            Dim btnEdit As LinkButton
            Dim btnDelete As LinkButton
            Dim btnSelect As LinkButton
            hdDelFlag = CType(e.Row.FindControl("hdOrderID"), HiddenField)

            btnEdit = CType(e.Row.FindControl("linkEdit"), LinkButton)
            btnDelete = CType(e.Row.FindControl("linkDelete"), LinkButton)
            btnSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN PURCHASEORDER']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = False
                'Else
                btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdDelFlag.Value) + "');")

                ' End If

            Else
                btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdDelFlag.Value + "');")
                btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdDelFlag.Value) + "');")
            End If


            If (Request.QueryString("PopUp")) Is Nothing Then
                btnSelect.Visible = False
            Else
                btnSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                btnSelect.Attributes.Add("OnClick", "return SelectFunction('" & btnSelect.CommandArgument & "');")
            End If
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
            GetSearchResult(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetSearchResult(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetSearchResult(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub grdvPurchaseOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvPurchaseOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvPurchaseOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvPurchaseOrder.Sorting
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
            GetSearchResult(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            GetSearchResult(PageOperation.Export)
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
        'PurchaseOrderID="74" PODescription="" OrderDate="29-Jul-03" CreationDate="07-Aug-03 14:34" SupplierName="amadeus old vendor" PRODUCTNAME="6P4C TELE JACK"
        Dim strArray() As String = {"Order No.", "Order Date", "Creation Date", "Supplier Name", "Product Name"}
        Dim intArray() As Integer = {0, 2, 3, 4, 5}
        objExport.ExportDetails(objOutputXml, "PURCHASEORDER", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportPURCHASEORDER.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("INVSR_PurchaseOrder.aspx", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
