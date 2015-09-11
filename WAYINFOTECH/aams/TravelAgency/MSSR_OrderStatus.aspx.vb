'##############################################################
'############   Page Name -- Order_MSSR_OrderStatus  ##########
'############   Date 1-December 2007  ########################
'############   Developed By Mukund  ########################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSSR_OrderStatus
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
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

#Region "Form Load Code Section"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "TravelAgency/MSSR_OrderStatus.aspx"
            '  btnReset.Attributes.Add("onclick", "return OrderStatusReset();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            '***************************************************************************************
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    OrderStatusDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If
            '***************************************************************************************
            'Code of Security Check
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx", False)
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Search Buttion Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            OrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Order Status Search"
    Private Sub OrderStatusSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus
            objInputXml.LoadXml("<MS_SEARCHORDERSTATUS_INPUT><ORDER_STATUS_NAME/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHORDERSTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDER_STATUS_NAME").InnerText = txtOrderStatus.Text

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
                    ViewState("SortName") = "ORDER_STATUS_NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ORDER_STATUS_NAME" '"LOCATION_CODE"
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

            objOutputXml = objbzOrderStatus.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvOrderStatus.DataSource = ds.Tables("ORDERSTATUS")
                gvOrderStatus.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("ORDERSTATUS").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "ORDER_STATUS_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrderStatus.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrderStatus.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                gvOrderStatus.DataSource = Nothing
                gvOrderStatus.DataBind()
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

#Region "Order Status Delete Function"

    Sub OrderStatusDelete(ByVal strOrderStatusID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaOrderStatus As New AAMS.bizTravelAgency.bzOrderStatus

            objInputXml.LoadXml("<MS_DELETEORDERSTATUS_INPUT><ORDERSTATUSID/></MS_DELETEORDERSTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerXml = strOrderStatusID

            'Here Back end Method Call
            objOutputXml = objtaOrderStatus.Delete(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                OrderStatusSearch(PageOperation.Search)

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Protected Sub gvOrderStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrderStatus.RowDataBound
        Try

        
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdorderStaus As HiddenField
            Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            lnkEdit = e.Row.FindControl("linkEdit")
            Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            lnkDelete = e.Row.FindControl("linkDelete")
            hdorderStaus = e.Row.FindControl("HiddenorderStatus")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Status']").Attributes("Value").Value)
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

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            OrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            OrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            OrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrderStatus_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOrderStatus.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrderStatus_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvOrderStatus.Sorting
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
            OrderStatusSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            OrderStatusSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Order Status"}
        Dim intArray() As Integer = {0}

        objExport.ExportDetails(objOutputXml, "ORDERSTATUS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportOrderStatus.xls")
    End Sub
    'End Code For Export

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_OrderStatus.aspx")
    End Sub
End Class

