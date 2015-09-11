'##############################################################
'############   Page Name - Inventory_INVSR_StockStatus #######
'############   Date 21-March 2008    #########################
'############   Developed By Abhishek  ########################
'##############################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class Inventory_INVSR_StockStatus
    Inherits System.Web.UI.Page

#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            btnSearch.Attributes.Add("onclick", "return StockStatusMandatory();")
            ' btnReset.Attributes.Add("onclick", "return StockStatusReset();")
            'btnAddScrap.Attributes.Add("onclick", "return NewFunction();")
            If (Not IsPostBack) Then
                FillGodown()
                ' objeAAMS.BindDropDown(drpGodown, "GODOWN", True)
                objeAAMS.BindDropDown(drpProduct, "EQUIPMENT", True, 3)
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Stock Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Stock Status']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
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
                drpGodown.Items.Clear()
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
                    drpGodown.DataSource = ds.Tables("GODOWN").DefaultView
                    drpGodown.DataTextField = "GODOWNNAME"
                    drpGodown.DataValueField = "GODOWNID"
                    drpGodown.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                drpGodown.Items.Insert(0, New ListItem("All", ""))
            End Try
        Else
            objeAAMS.BindDropDown(drpGodown, "GODOWN", True, 3)
        End If


    End Sub

#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            StockStatusSearch(PageOperation.Search)
            txtEquipGroup.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search StockStatus"
    Private Sub StockStatusSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbChallan As New AAMS.bizInventory.bzChallan

            objInputXml.LoadXml("<INV_SEARCHSTOCK_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHSTOCK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerText = drpProduct.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerText = drpGodown.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = txtEquipGroup.Text
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = txtEquipCode.Text

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
                    ViewState("SortName") = "GODOWNNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "GODOWNNAME" '"LOCATION_CODE"
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
            'Here Back end Method Call
            objOutputXml = objbChallan.SearchStock(objInputXml)
            '  objOutputXml.LoadXml("<INV_SEARCHSTOCK_OUTPUT><STOCK GODOWNID='2' PRODUCTID='33' GODOWNNAME='' GROUP_CODE='45' GROUPNAME='' EQUIPMENT_CODE=''	PRODUCTNAME='dsfds' QTYRECEIVED='' QTYINHAND='' QTYSCRAP='' STOCKOUT='' />	<Errors Status='FALSE'>	<Error Code='FALSE' Description='' />	</Errors></INV_SEARCHSTOCK_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvStockStatus.DataSource = ds.Tables("STOCK")
                gvStockStatus.DataBind()
                txtRecordCount.Text = ds.Tables("STOCK").Rows.Count
                'Code Added For Paging And Sorting In case Of Delete The Record
                If ds.Tables("STOCK").Rows.Count <> 0 Then
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
                    hdRecordOnCurrentPage.Value = ds.Tables("STOCK").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"
                    'PODescription="" OrderDate="29-Jul-03" CreationDate="07-Aug-03 14:34" SupplierName="amadeus old vendor" PRODUCTNAME="6P4C TELE JACK"
                    Select Case ViewState("SortName").ToString()
                        Case "GODOWNNAME"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "GROUP_CODE"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "EQUIPMENT_CODE"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(2).Controls.Add(imgDown)

                            End Select
                        Case "PRODUCTNAME"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select

                        Case "QTYRECEIVED"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(4).Controls.Add(imgDown)
                            End Select

                        Case "QTYINHAND"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(5).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(5).Controls.Add(imgDown)

                            End Select
                        Case "QTYSCRAP"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(6).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(6).Controls.Add(imgDown)
                            End Select
                        Case "STOCKOUT"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvStockStatus.HeaderRow.Cells(7).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvStockStatus.HeaderRow.Cells(7).Controls.Add(imgDown)
                            End Select

                    End Select
                    '  Added Code To Show Image'
                Else
                    gvStockStatus.DataSource = Nothing
                    gvStockStatus.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvStockStatus.DataSource = Nothing
                gvStockStatus.DataBind()
                txtRecordCount.Text = 0
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvStockStatus_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvStockStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvStockStatus.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkDetails As LinkButton
            Dim hdGODOWNID As HiddenField
            Dim hdPRODUCTID As HiddenField
            Dim hdGROUP_CODE As HiddenField
            hdGODOWNID = CType(e.Row.FindControl("hdGODOWNID"), HiddenField)
            hdPRODUCTID = CType(e.Row.FindControl("hdPRODUCTID"), HiddenField)
            hdGROUP_CODE = CType(e.Row.FindControl("hdGROUP_CODE"), HiddenField)

            Dim EnstrGODOWNID As String = objED.Encrypt(hdGODOWNID.Value)
            Dim EnstrPRODUCTID As String = objED.Encrypt(hdPRODUCTID.Value)
            Dim EnstrGROUP_CODE As String = objED.Encrypt(hdGROUP_CODE.Value)
            Dim hdStockId As New HiddenField

            lnkDetails = e.Row.FindControl("lnkDetails")
            hdStockId = CType(e.Row.FindControl("hdStockId"), HiddenField)
            'lnkDetails.Attributes.Add("OnClick", "return DetailsFunction('" + lnkDetails.CommandArgument + "');")
            lnkDetails.Attributes.Add("OnClick", "return DetailsFunction('" + EnstrGODOWNID + "','" + EnstrPRODUCTID + "','" + EnstrGROUP_CODE + "');")
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
            StockStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StockStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StockStatusSearch(PageOperation.Search)
            txtEquipGroup.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvStockStatus_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvStockStatus.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvStockStatus_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvStockStatus.Sorting
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
            StockStatusSearch(PageOperation.Search)
            txtEquipGroup.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            StockStatusSearch(PageOperation.Export)
            txtEquipGroup.Focus()
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
        'GODOWNID="3" PRODUCTID="7" GODOWNNAME="DHAKA GODOWN" GROUP_CODE="LRD" GROUPNAME="LAN CARD" EQUIPMENT_CODE="4CN" 
        'PRODUCTNAME="RJ 45 CONNECTOR" QTYRECEIVED="0" QTYINHAND="0" QTYSCRAP="0" STOCKOUT="0"
        ' <STOCK GODOWNID="1" PRODUCTID="2" GODOWNNAME="DELHI GODOWN" GROUP_CODE="NAC" GROUPNAME="NETWORK ACCESSORIES" EQUIPMENT_CODE="16H" PRODUCTNAME="16PORT HUB" QTYRECEIVED="7" QTYINHAND="4" QTYSCRAP="0" STOCKOUT="3" PC_INSTALLED="0" /> 


        Dim strArray() As String = {"Godown", "Group Code", "Equipment Code", "Product", "Qty. Received", "Qty. In Hand", "Scrap Qty.", "Stock Out", "PC INSTALLED"}
        Dim intArray() As Integer = {2, 3, 5, 6, 7, 8, 9, 10, 11}
        objExport.ExportDetails(objOutputXml, "STOCK", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportSTOCKSTATUS.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_StockStatus.aspx", False)
    End Sub
End Class
