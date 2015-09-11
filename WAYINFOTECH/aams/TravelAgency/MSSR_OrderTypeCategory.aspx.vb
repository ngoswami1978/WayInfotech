Imports System.Xml
Imports System.Data


Partial Class Setup_MSSR_OrderTypeCategory
    Inherits System.Web.UI.Page
#Region "Global Declaration Section "
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objSecurityXml As New XmlDocument
    Dim objED As New EncyrptDeCyrpt
#End Region

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

#Region "Button Search Event Definition"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Try
                lblError.Text = ""
                OrderTypeCatSearch(PageOperation.Search)
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Search Function Definition "
    Private Sub OrderTypeCatSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objtaCatType As New AAMS.bizTravelAgency.bzOrderCategory
            objInputXml.LoadXml("<MS_SEARCHORDERTYPECATEGORY_INPUT><OrderTypeCategoryName /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHORDERTYPECATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OrderTypeCategoryName").InnerText = txtOrderTypeCat.Text.Trim()
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
                    ViewState("SortName") = "OrderTypeCategoryName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "OrderTypeCategoryName" '"LOCATION_CODE"
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

            objOutputXml = objtaCatType.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdOrderTypeCat.DataSource = ds.Tables("CATEGORY").DefaultView
                grdOrderTypeCat.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("CATEGORY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "OrderTypeCategoryName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdOrderTypeCat.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdOrderTypeCat.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                grdOrderTypeCat.DataSource = Nothing
                grdOrderTypeCat.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Form Load Event Definition"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes
            Dim objInputXml As New XmlDocument
            Session("PageName") = "TravelAgency/MSSR_OrderTypeCategory.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            '' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                ' btnReset.Attributes.Add("onclick", "return OrderTypeCategoryReset();")
                btnNew.Attributes.Add("onclick", "return NewFunction();")

                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                        OrdertypeCatDelete(Request.QueryString("CategoryID").ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "GridView RowDataBound Definition"
    Protected Sub grdOrderTypeCat_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOrderTypeCat.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdorderStaus As HiddenField
            Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            lnkEdit = e.Row.FindControl("linkEdit")
            Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            lnkDelete = e.Row.FindControl("linkDelete")
            hdorderStaus = e.Row.FindControl("catIDHidden")

            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type Category']").Attributes("Value").Value)

                If strBuilder(3) = "0" Then
                    lnkDelete.Disabled = True
                Else
                    lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdorderStaus.Value & ");")
                End If

                'If strBuilder(2) = "0" Then
                '    lnkEdit.Disabled = True
                'Else
                '    lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdorderStaus.Value & ");")
                'End If
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdorderStaus.Value) & "');")
            Else
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdorderStaus.Value & ");")
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdorderStaus.Value) & "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Order Type Category Delete Function"
    Private Sub OrdertypeCatDelete(ByVal strID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaOrderTypeCat As New AAMS.bizTravelAgency.bzOrderCategory

            objInputXml.LoadXml(" <MS_DELETEORDERTYPECATEGORY_INPUT><OrderTypeCategoryID /></MS_DELETEORDERTYPECATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OrderTypeCategoryID").InnerXml = strID
            'Here Back end Method Call
            objOutputXml = objtaOrderTypeCat.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                OrderTypeCatSearch(PageOperation.Search)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
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
            OrderTypeCatSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            OrderTypeCatSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            OrderTypeCatSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrderTypeCat_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrderTypeCat.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrderTypeCat_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdOrderTypeCat.Sorting
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
            OrderTypeCatSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            OrderTypeCatSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Order Type Category"}
        Dim intArray() As Integer = {1}

        objExport.ExportDetails(objOutputXml, "CATEGORY", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportOrderTypeCategory.xls")
    End Sub
    'End Code For Export

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_OrderTypeCategory.aspx")
    End Sub
End Class
