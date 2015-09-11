Imports System.Xml
Imports System.Data
Partial Class Setup_MSSR_OrderType
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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        OrderTypeSearch(PageOperation.Search)
        Session("CorporateCode") = Nothing
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Session("CorporateCode") = Nothing
        Response.Redirect("MSUP_OrderType.aspx?Action=I|")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_OrderType.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            ' objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Not Page.IsPostBack Then
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtOrderTypeCat.Text = Session("Action").ToString().Split("|").GetValue(2)
                        txtOrderType.Text = Session("Action").ToString().Split("|").GetValue(3)
                        OrderTypeSearch(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
            '***************************************************************************************

            '***************************************************************************************
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    OrderTypeDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                    OrderTypeSearch(PageOperation.Search)
                    lblError.Text = objeAAMSMessage.messDelete
                End If
            End If


            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                End If
                If strBuilder(0) = "0" Then
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

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub OrderTypeSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            objInputXml.LoadXml("<MS_SEARCHORDERTYPE_INPUT>	<OrderTypeCategoryName></OrderTypeCategoryName>	<ORDER_TYPE_NAME></ORDER_TYPE_NAME><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OrderTypeCategoryName").InnerText = txtOrderTypeCat.Text
            objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE_NAME").InnerText = txtOrderType.Text
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
            objOutputXml = objbzOrderType.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdOrderType.DataSource = ds.Tables("ORDERTYPE")
                grdOrderType.DataBind()
                lblError.Text = ""
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
                hdRecordOnCurrentPage.Value = ds.Tables("ORDERTYPE").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "OrderTypeCategoryName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdOrderType.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdOrderType.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "ORDER_TYPE_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdOrderType.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdOrderType.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "TimeRequired"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdOrderType.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdOrderType.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                grdOrderType.DataSource = Nothing
                grdOrderType.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub OrderTypeDelete(ByVal OrderTypeID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            objInputXml.LoadXml("<MS_DELETEORDERTYPE_INPUT>	<ORDERTYPEID></ORDERTYPEID></MS_DELETEORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerXml = OrderTypeID
            'Here Back end Method Call
            objOutputXml = objbzOrderType.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("Action") = Request.QueryString("Action")
                lblError.Text = Session("Action")
                Response.Redirect("MSSR_ORDERTYPE.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrderType_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdOrderType.RowCommand
        'Try
        '    'Code for Edit Data
        '    If e.CommandName = "EditX" Then
        '        Response.Redirect("MSUP_OrderType.aspx?Action=U&OrderTypeID=" & e.CommandArgument)
        '    End If
        '    'Code for Delete Data
        '    If e.CommandName = "DeleteX" Then
        '        OrderTypeDelete(e.CommandArgument)
        '        OrderTypeSearch()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub grdOrderType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOrderType.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdORDERTYPE As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdORDERTYPE = e.Row.FindControl("hdORDERTYPE")

            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdORDERTYPE.Value & "');")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdORDERTYPE.Value & "');")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Type']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Disabled = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdORDERTYPE.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                '    linkEdit.Disabled = True
                'Else
                '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdORDERTYPE.Value & "');")
                'End If
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdORDERTYPE.Value) & "');")
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdORDERTYPE.Value) & "');")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdORDERTYPE.Value & "');")
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
            OrderTypeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            OrderTypeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            OrderTypeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub grdOrderType_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrderType.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrderType_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdOrderType.Sorting
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
            OrderTypeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            OrderTypeSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Order Type Category", "Order Type", "Time Required"}
        Dim intArray() As Integer = {0, 2, 3}
        objExport.ExportDetails(objOutputXml, "ORDERTYPE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportORDERTYPE.xls")
    End Sub
End Class
