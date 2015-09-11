Imports System.Xml
Imports System.Data
Partial Class Order_MSSR_Product
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
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
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        ' This code is used for Expiration of Page From Cache
        '  objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Not Page.IsPostBack Then
            objeAAMS.BindDropDown(ddlGroupName, "PRODUCTGROUP", True, 3)
            objeAAMS.BindDropDown(ddlCrs, "PROVIDERCRS", True, 3)
            objeAAMS.BindDropDown(ddlOS, "OS", True, 3)
        End If
        If Not Page.IsPostBack Then
            If Not Session("Action") Is Nothing Then
                If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                    ddlGroupName.SelectedIndex = Session("Action").ToString().Split("|").GetValue(2)
                    txtProductName.Text = Session("Action").ToString().Split("|").GetValue(3)
                    ddlCrs.SelectedIndex = Session("Action").ToString().Split("|").GetValue(4)
                    ddlOS.SelectedIndex = Session("Action").ToString().Split("|").GetValue(5)
                    ProductSearch(PageOperation.Search)
                    lblError.Text = objeAAMSMessage.messDelete
                    Session("Action") = Nothing
                End If
            End If
        End If
        '***************************************************************************************

        '***************************************************************************************
        'If Not Request.QueryString("Action") Is Nothing Then
        '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
        '        ProductDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
        '    End If
        'End If

        ' This code is used for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                btnSearch.Enabled = False
                Response.Redirect("~/NoRights.aspx", False)
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

        ' Code for Delete 
        If (hdProductID.Value <> "") Then
            ProductDelete(hdProductID.Value)
        End If
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Product.aspx?Action=I|")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ProductSearch(PageOperation.Search)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_Product.aspx")
    End Sub

    Sub ProductSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzProduct As New AAMS.bizTravelAgency.bzProduct
            objInputXml.LoadXml("<MS_SEARCHPRODUCTS_INPUT>	<PRODUCT productGroupId='' PRODUCTNAME='' CRSCode='' OSId=''/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHPRODUCTS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCT").Attributes("productGroupId").InnerText = ddlGroupName.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("PRODUCT").Attributes("PRODUCTNAME").InnerText = txtProductName.Text
            objInputXml.DocumentElement.SelectSingleNode("PRODUCT").Attributes("CRSCode").InnerText = ddlCrs.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("PRODUCT").Attributes("OSId").InnerText = ddlOS.SelectedValue
            'Here Back end Method Call
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
                    ViewState("SortName") = "productGroupName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "productGroupName" '"LOCATION_CODE"
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
            objOutputXml = objbzProduct.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvProduct.DataSource = ds.Tables("Product")
                gvProduct.DataBind()
                lblError.Text = ""
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
                hdRecordOnCurrentPage.Value = ds.Tables("Product").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "productGroupName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvProduct.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvProduct.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "PRODUCTNAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvProduct.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvProduct.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "VERSION"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvProduct.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvProduct.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub ProductDelete(ByVal strProductID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzProduct As New AAMS.bizTravelAgency.bzProduct
            objInputXml.LoadXml("<MS_DELETEPRODUCTS_INPUT><PRODUCTID></PRODUCTID></MS_DELETEPRODUCTS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerXml = strProductID
            'Here Back end Method Call
            hdProductID.Value = ""
            objOutputXml = objbzProduct.Delete(objInputXml)
            ProductSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  Session("Action") = Request.QueryString("Action")
                lblError.Text = objeAAMSMessage.messDelete
                ' Response.Redirect("MSSR_Product.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand
        '    Try
        '        'Code for Edit Data
        '        If e.CommandName = "EditX" Then
        '            Response.Redirect("MSUP_Product.aspx?Action=U&ProductID=" & e.CommandArgument)
        '        End If
        '        'Code for Delete Data
        '        If e.CommandName = "DeleteX" Then
        '            ProductDelete(e.CommandArgument)
        '            ProductSearch()
        '        End If
        '    Catch ex As Exception
        '        lblError.Text = ex.Message
        '    End Try
    End Sub

    Protected Sub gvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduct.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdProductName As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdProductName = e.Row.FindControl("hdProductName")

        'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdProductName.Value & "');")
        'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdProductName.Value & "');")
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Disabled = True
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdProductName.Value & "');")
            End If
            If strBuilder(2) = "0" Then
                ' linkEdit.Disabled = True
            End If
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdProductName.Value) & "');")
        Else
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdProductName.Value) & "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdProductName.Value & "');")
        End If
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ProductSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ProductSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ProductSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvProduct_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvProduct.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvProduct_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvProduct.Sorting
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
            ProductSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ProductSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Group Name", "Product Name", "Version"}
        Dim intArray() As Integer = {0, 1, 3}
        objExport.ExportDetails(objOutputXml, "PRODUCT", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportPRODUCT.xls")
    End Sub
End Class
