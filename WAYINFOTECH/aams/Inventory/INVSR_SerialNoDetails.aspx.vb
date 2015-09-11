'###################################################################
'############   Page Name - Inventory_INVSR_SerialNoDetails  ########
'############   Date 21-March 2008    ##############################
'############   Developed By Abhishek  #############################
'###################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region

Partial Class Inventory_INVSR_SerialNoDetails
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
            lblError.Text = ""
            Session("PrintBarCode") = Nothing
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnPrintBarCodes.Attributes.Add("onclick", "return PrintBarCodes(ifrmPrnt);")
            btnSearch.Attributes.Add("onclick", "return  StockDetailsMandatory();")
            'btnReset.Attributes.Add("onclick", "return StockDetailsReset();")
            'btnAddScrap.Attributes.Add("onclick", "return NewFunction();")
            If (Not IsPostBack) Then
                objeAAMS.BindDropDown(drpGodown, "GODOWN", True)
                objeAAMS.BindDropDown(drpProduct, "EQUIPMENT", True)


                If Request.QueryString("GoDownId") IsNot Nothing Then
                    Dim EnstrGodownId As String
                    EnstrGodownId = objED.Decrypt(Request.QueryString("GoDownId"))
                    drpGodown.SelectedValue = EnstrGodownId 'Request.QueryString("GoDownId")
                    rdStatus.SelectedValue = "1"
                End If
                If Request.QueryString("PrdId") IsNot Nothing Then
                    Dim EnstrProductId As String
                    EnstrProductId = objED.Decrypt(Request.QueryString("PrdId"))
                    drpProduct.SelectedValue = EnstrProductId 'Request.QueryString("PrdId")
                End If
                If Request.QueryString("GoDownId") IsNot Nothing Then
                    StockDetailsSearch()
                End If
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
                        BtnExport.Enabled = False
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

#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            txtRecordCount.Text = "0"
            lblError.Text = ""
            StockDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search StockDetails"
    Private Sub StockDetailsSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan

            'objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><SERIALNUMBER /><STOCKSTATUS /><VENDERSERIALNO /><CHALLANNUMBER /><LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><SERIALNUMBER /><STOCKSTATUS /><VENDERSERIALNO /><CHALLANNUMBER /><LCODE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerText = drpProduct.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerText = drpGodown.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = txtEquipGroup.Text
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = txtEquipCode.Text
            objInputXml.DocumentElement.SelectSingleNode("SERIALNUMBER").InnerText = txtSerailNo.Text
            objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = rdStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtVendorSerialNo.Text


            '@ Coding For Paging Ansd sorting
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
                ViewState("SortName") = "EGROUP_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EGROUP_CODE" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting


            'Here Back end Method Call
            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
            'objOutputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='1' AOFFICE='dsfds' EGROUP_CODE='d' EQUIPMENT_CODE='d' PRODUCTNAME='dsddsfds' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' 	GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status='FALSE'>	<Error Code='' Description=''/>	</Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                txtRecordCount.Text = ds.Tables("DETAILS").Rows.Count

                If rdStatus.SelectedValue = 5 Then

                    gvStockDetailsInstalled.DataSource = ds.Tables("DETAILS")
                    gvStockDetailsInstalled.DataBind()

                    ''new grid
                    gvStockDetailsInstalled.Visible = True
                    tdStockDetailsInstalled.Visible = True

                    ''old grid
                    gvStockDetailsOut.Visible = False
                    tdgvStockDetailsOut.Visible = False

                    ''old grid
                    gvStockDetails.Visible = False
                    tdgvStockDetails.Visible = False

                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    'BindControlsForNavigation(2)
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvStockDetailsInstalled)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################

                    pnlPaging.Visible = True



                ElseIf rdStatus.SelectedValue = 2 Then

                    gvStockDetailsOut.DataSource = ds.Tables("DETAILS")
                    gvStockDetailsOut.DataBind()

                    gvStockDetailsOut.Visible = True
                    tdgvStockDetailsOut.Visible = True



                    gvStockDetails.DataSource = Nothing
                    gvStockDetails.DataBind()

                    gvStockDetails.Visible = False
                    tdgvStockDetails.Visible = False


                    gvStockDetailsInstalled.DataSource = Nothing
                    gvStockDetailsInstalled.DataBind()

                    gvStockDetailsInstalled.Visible = False
                    tdStockDetailsInstalled.Visible = False

                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    'BindControlsForNavigation(2)
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvStockDetailsOut)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################

                    pnlPaging.Visible = True



                Else
                    gvStockDetails.DataSource = ds.Tables("DETAILS")
                    gvStockDetails.DataBind()

                    gvStockDetails.Visible = True
                    tdgvStockDetails.Visible = True



                    gvStockDetailsOut.DataSource = Nothing
                    gvStockDetailsOut.DataBind()

                    gvStockDetailsOut.Visible = False
                    tdgvStockDetailsOut.Visible = False




                    gvStockDetailsInstalled.DataSource = Nothing
                    gvStockDetailsInstalled.DataBind()

                    gvStockDetailsInstalled.Visible = False
                    tdStockDetailsInstalled.Visible = False


                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    'BindControlsForNavigation(2)
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvStockDetails)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################

                    pnlPaging.Visible = True



                End If
                getBarCodes()
            Else
                txtRecordCount.Text = "0"

                gvStockDetails.DataSource = Nothing
                gvStockDetails.DataBind()

                gvStockDetailsOut.DataSource = Nothing
                gvStockDetailsOut.DataBind()


                gvStockDetailsInstalled.DataSource = Nothing
                gvStockDetailsInstalled.DataBind()

                If rdStatus.SelectedValue = 5 Then

                    gvStockDetailsInstalled.Visible = True
                    tdStockDetailsInstalled.Visible = True

                    gvStockDetails.Visible = False
                    tdgvStockDetails.Visible = False

                    gvStockDetailsInstalled.Visible = False
                    tdStockDetailsInstalled.Visible = False


                ElseIf rdStatus.SelectedValue = 2 Then

                    gvStockDetailsOut.Visible = True
                    tdgvStockDetailsOut.Visible = True

                    gvStockDetails.Visible = False
                    tdgvStockDetails.Visible = False

                    gvStockDetailsInstalled.Visible = False
                    tdStockDetailsInstalled.Visible = False

                Else
                    gvStockDetails.Visible = False
                    tdgvStockDetails.Visible = False

                    gvStockDetailsOut.Visible = False
                    tdgvStockDetailsOut.Visible = False

                    gvStockDetailsInstalled.Visible = False
                    tdStockDetailsInstalled.Visible = False


                End If
                pnlPaging.Visible = False
                ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
#End Region

#Region "gvStockDetails_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvStockDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvStockDetails.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifySNo As LinkButton
            Dim hdStockId As New HiddenField
            lnkModifySNo = e.Row.FindControl("lnkModifySNo")
            hdStockId = CType(e.Row.FindControl("hdStockId"), HiddenField)
            Dim hdGoDownId As HiddenField
            Dim hdPrdId As HiddenField

            Dim hdSuppId As HiddenField
            Dim hdSerialno As HiddenField
            Dim hdVendorSerialNo As HiddenField
            Dim hdProductName As HiddenField

            hdGoDownId = CType(e.Row.FindControl("hdGODOWNID"), HiddenField)
            hdPrdId = CType(e.Row.FindControl("hdPrdId"), HiddenField)
            hdSuppId = CType(e.Row.FindControl("hdSuppId"), HiddenField)
            hdSerialno = CType(e.Row.FindControl("hdSerialno"), HiddenField)
            hdVendorSerialNo = CType(e.Row.FindControl("hdVendorSerialNo"), HiddenField)
            hdProductName = CType(e.Row.FindControl("hdProductName"), HiddenField)


            Dim EnstrGODOWNID As String = objED.Encrypt(hdGODOWNID.Value)
            Dim EnstrPRODUCTID As String = objED.Encrypt(hdPrdId.Value)
            Dim EnstrSuppId As String = objED.Encrypt(hdSuppId.Value)
            Dim EnstrSerialno As String = objED.Encrypt(hdSerialno.Value)
            Dim EnstrVendorSerialNo As String = objED.Encrypt(hdVendorSerialNo.Value)
            Dim EnstrProductName As String = objED.Encrypt(hdProductName.Value)




            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Attributes("Value").Value)
                    'If strBuilder(2) = "0" Then
                    'lnkModifySNo.Enabled = False
                    'End If
                    'Else
                    lnkModifySNo.Enabled = True
                    'lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction('" + lnkModifySNo.CommandArgument + "');")
                    lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction( '" + EnstrGODOWNID + "','" + EnstrPRODUCTID + "','" + EnstrSerialno + "', '" + EnstrVendorSerialNo + "',  '" + EnstrProductName + "' );")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifySNo.Enabled = True
                ' lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction('" + lnkModifySNo.CommandArgument + "');")
                lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction('" + EnstrGODOWNID + "','" + EnstrPRODUCTID + "',   '" + EnstrSerialno + "',   '" + EnstrVendorSerialNo + "',  '" + EnstrProductName + "' );")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Private Sub GetSerailNoDetails(ByVal stockNo As String)
        Try

        Catch ex As Exception

        End Try
    End Sub
#Region "gvStockDetailsOut_RowDataBound Event fired on every row creation in  gridview"

    Protected Sub gvStockDetailsOut_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvStockDetailsOut.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifySNo As LinkButton
            Dim hdStockId As New HiddenField
            lnkModifySNo = e.Row.FindControl("lnkModifySNo")
            hdStockId = CType(e.Row.FindControl("hdStockId"), HiddenField)

            Dim hdGoDownId As HiddenField
            Dim hdPrdId As HiddenField
            Dim hdSuppId As HiddenField
            Dim hdSerialno As HiddenField
            Dim hdVendorSerialNo As HiddenField
            Dim hdProductName As HiddenField

            hdGoDownId = CType(e.Row.FindControl("hdGODOWNID"), HiddenField)
            hdPrdId = CType(e.Row.FindControl("hdPrdId"), HiddenField)
            hdSuppId = CType(e.Row.FindControl("hdSuppId"), HiddenField)
            hdSerialno = CType(e.Row.FindControl("hdSerialno"), HiddenField)
            hdVendorSerialNo = CType(e.Row.FindControl("hdVendorSerialNo"), HiddenField)
            hdProductName = CType(e.Row.FindControl("hdProductName"), HiddenField)


            Dim EnstrGODOWNID As String = objED.Encrypt(hdGoDownId.Value)
            Dim EnstrPRODUCTID As String = objED.Encrypt(hdPrdId.Value)
            Dim EnstrSuppId As String = objED.Encrypt(hdSuppId.Value)
            Dim EnstrSerialno As String = objED.Encrypt(hdSerialno.Value)
            Dim EnstrVendorSerialNo As String = objED.Encrypt(hdVendorSerialNo.Value)
            Dim EnstrProductName As String = objED.Encrypt(hdProductName.Value)




            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MODIFY SERIAL NO']").Attributes("Value").Value)
                    'If strBuilder(2) = "0" Then
                    'lnkModifySNo.Enabled = False
                    'End If
                    'Else
                    lnkModifySNo.Enabled = True
                    'lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction('" + lnkModifySNo.CommandArgument + "');")
                    lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction( '" + EnstrGODOWNID + "','" + EnstrPRODUCTID + "',   '" + EnstrSerialno + "',   '" + EnstrVendorSerialNo + "',  '" + EnstrProductName + "'  );")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifySNo.Enabled = True
                'lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction('" + lnkModifySNo.CommandArgument + "');")
                lnkModifySNo.Attributes.Add("OnClick", "return ModifySerailNoFunction( '" + EnstrGODOWNID + "','" + EnstrPRODUCTID + "',   '" + EnstrSerialno + "',   '" + EnstrVendorSerialNo + "',  '" + EnstrProductName + "' );")
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            txtRecordCount.Text = "0"
            txtEquipCode.Text = ""
            txtEquipGroup.Text = ""
            txtSerailNo.Text = ""
            If Request.QueryString("GoDownId") IsNot Nothing Then
                Dim EnstrGodownId As String
                EnstrGodownId = objED.Decrypt(Request.QueryString("GoDownId"))
                drpGodown.SelectedValue = EnstrGodownId 'Request.QueryString("GoDownId")
            End If
            If Request.QueryString("PrdId") IsNot Nothing Then
                Dim EnstrProductId As String
                EnstrProductId = objED.Decrypt(Request.QueryString("PrdId"))
                drpProduct.SelectedValue = EnstrProductId ' Request.QueryString("PrdId")
            End If
            If Request.QueryString("GoDownId") IsNot Nothing Then
                rdStatus.SelectedValue = "1"
                StockDetailsSearch()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub rdStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdStatus.SelectedIndexChanged
        Try
            ViewState("SortName") = "EGROUP_CODE"
            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
            End If
            StockDetailsSearch()
        Catch ex As Exception

        End Try
    End Sub

    
    Protected Sub btnPrintBarCodes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintBarCodes.Click
        Try
            'Session("PrintBarCode") = Nothing
            'Dim RowNo As Integer
            'Dim objInputPrintBarCodesTemplate, objOutputPrintBarCodesTemplate As New XmlDocument
            'Dim strBarCodeString As String
            'Dim strPrintCode, strFinalPrintCode As String
            'Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            ''Code of Security Check
            'Dim objPrintBarCodesTemplate As New AAMS.bizInventory.bzChallan


            ''objInputPrintBarCodesTemplate.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
            ''objInputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ISP MAILING" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
            ''Here Back end Method Call
            'objOutputPrintBarCodesTemplate = objPrintBarCodesTemplate.GetStockBarcodeString()
            ''objOutputPrintBarCodesTemplate.LoadXml("<INV_PRINTBARCODES_OUTPUT><PRINTBARCODES  PrintBarCodesTemplate='' />	<Errors Status='FALSE'>		<Error Code='' Description='' />	</Errors></INV_PRINTBARCODES_OUTPUT>	")
            'If objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    ' strBarCodeString = "               Amadues Agent Management System      " & vbCrLf & _
            '    '"EGroup_Code :GXX     Equipment_Code: PXX    Aoffice: AXX   SERIALNUMBER: SERIALNUMBER   Vendor SERIAL NUMBER   :VSNO1  Vendor SERIAL NUMBER 2 : VSNO2" 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("PrintBarCodesTemplate").Value
            '    '###############################################################################
            '    '@ Now find the value of Body parts on the basis of Lcode               
            '    strBarCodeString = objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("BARCODESTRING").InnerText
            '    strPrintCode = strBarCodeString
            '    strFinalPrintCode = ""
            '    If rdStatus.SelectedValue = 2 Then
            '        'lblError.Text = gvStockDetailsOut.Rows.Count
            '        For RowNo = 0 To gvStockDetailsOut.Rows.Count - 1
            '            strPrintCode = strBarCodeString
            '            strPrintCode = strPrintCode.Replace("GXX", " " & gvStockDetailsOut.Rows(RowNo).Cells(1).Text)  '("PRINTBARCODES").Attributes("EGroup_Code").Value)
            '            strPrintCode = strPrintCode.Replace("PXX", " " & gvStockDetailsOut.Rows(RowNo).Cells(2).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("Equipment_Code").Value)
            '            strPrintCode = strPrintCode.Replace("AXX", " " & gvStockDetailsOut.Rows(RowNo).Cells(0).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("AOffice").Value)
            '            strPrintCode = strPrintCode.Replace("SERIALNUMBER", " " & gvStockDetailsOut.Rows(RowNo).Cells(4).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("SERIALNUMBER").Value)
            '            strPrintCode = strPrintCode.Replace("VSNO1", " " & Mid(gvStockDetailsOut.Rows(RowNo).Cells(5).Text, 1, 25)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
            '            strPrintCode = strPrintCode.Replace("VSNO2", " " & Mid(gvStockDetailsOut.Rows(RowNo).Cells(5).Text, 26, 50)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)

            '            strFinalPrintCode = strFinalPrintCode & vbCrLf & strPrintCode

            '        Next
            '    Else
            '        strPrintCode = strBarCodeString
            '        'lblError.Text = gvStockDetails.Rows.Count
            '        For RowNo = 0 To gvStockDetails.Rows.Count - 1
            '            strPrintCode = strPrintCode.Replace("GXX", " " & gvStockDetails.Rows(RowNo).Cells(1).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("EGroup_Code").Value)
            '            strPrintCode = strPrintCode.Replace("PXX", " " & gvStockDetails.Rows(RowNo).Cells(2).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("Equipment_Code").Value)
            '            strPrintCode = strPrintCode.Replace("AXX", " " & gvStockDetails.Rows(RowNo).Cells(0).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("AOffice").Value)
            '            strPrintCode = strPrintCode.Replace("SERIALNUMBER", " " & gvStockDetails.Rows(RowNo).Cells(4).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("SERIALNUMBER").Value)
            '            strPrintCode = strPrintCode.Replace("VSNO1", " " & Mid(gvStockDetails.Rows(RowNo).Cells(5).Text, 1, 25)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
            '            strPrintCode = strPrintCode.Replace("VSNO2", " " & Mid(gvStockDetails.Rows(RowNo).Cells(5).Text, 26, 50)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
            '            strFinalPrintCode = strFinalPrintCode & vbCrLf & strPrintCode
            '        Next
            '    End If
            '    lblError.Text = strFinalPrintCode
            '    Session("PrintBarCode") = strFinalPrintCode
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "BarCodes", "<script>Javascript:PrintBarCodes(ifrmPrnt);</script>", True)
            '    'RegisterClientScriptBlock("BarCodes", "")
            'Else
            '    lblError.Text = ""
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub getBarCodes()
        Try
            Session("PrintBarCode") = Nothing
            Dim RowNo As Integer
            Dim objInputPrintBarCodesTemplate, objOutputPrintBarCodesTemplate As New XmlDocument
            Dim strBarCodeString As String
            Dim strPrintCode, strFinalPrintCode As String
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            'Code of Security Check
            Dim objPrintBarCodesTemplate As New AAMS.bizInventory.bzChallan


            'objInputPrintBarCodesTemplate.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
            'objInputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ISP MAILING" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
            'Here Back end Method Call
            objOutputPrintBarCodesTemplate = objPrintBarCodesTemplate.GetStockBarcodeString()
            'objOutputPrintBarCodesTemplate.LoadXml("<INV_PRINTBARCODES_OUTPUT><PRINTBARCODES  PrintBarCodesTemplate='' />	<Errors Status='FALSE'>		<Error Code='' Description='' />	</Errors></INV_PRINTBARCODES_OUTPUT>	")
            If objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' strBarCodeString = "               Amadues Agent Management System      " & vbCrLf & _
                '"EGroup_Code :GXX     Equipment_Code: PXX    Aoffice: AXX   SERIALNUMBER: SERIALNUMBER   Vendor SERIAL NUMBER   :VSNO1  Vendor SERIAL NUMBER 2 : VSNO2" 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("PrintBarCodesTemplate").Value
                '###############################################################################
                '@ Now find the value of Body parts on the basis of Lcode               
                strBarCodeString = objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("BARCODESTRING").InnerText
                strPrintCode = strBarCodeString
                strFinalPrintCode = ""
                If rdStatus.SelectedValue = 2 Then
                    'lblError.Text = gvStockDetailsOut.Rows.Count
                    For RowNo = 0 To gvStockDetailsOut.Rows.Count - 1
                        strPrintCode = strBarCodeString
                        strPrintCode = strPrintCode.Replace("GXX", gvStockDetailsOut.Rows(RowNo).Cells(1).Text)  '("PRINTBARCODES").Attributes("EGroup_Code").Value)
                        strPrintCode = strPrintCode.Replace("PXX", gvStockDetailsOut.Rows(RowNo).Cells(2).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("Equipment_Code").Value)
                        strPrintCode = strPrintCode.Replace("AXX", gvStockDetailsOut.Rows(RowNo).Cells(0).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("AOffice").Value)
                        strPrintCode = strPrintCode.Replace("SERIALNUMBER", gvStockDetailsOut.Rows(RowNo).Cells(4).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("SERIALNUMBER").Value)
                        strPrintCode = strPrintCode.Replace("VSNO1", Mid(gvStockDetailsOut.Rows(RowNo).Cells(5).Text, 1, 25)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
                        strPrintCode = strPrintCode.Replace("VSNO2", Mid(gvStockDetailsOut.Rows(RowNo).Cells(5).Text, 26, 50)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)

                        strFinalPrintCode = strFinalPrintCode & vbCrLf & strPrintCode

                    Next
                Else
                    strPrintCode = strBarCodeString
                    'lblError.Text = gvStockDetails.Rows.Count
                    For RowNo = 0 To gvStockDetails.Rows.Count - 1
                        strPrintCode = strBarCodeString
                        strPrintCode = strPrintCode.Replace("GXX", gvStockDetails.Rows(RowNo).Cells(1).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("EGroup_Code").Value)
                        strPrintCode = strPrintCode.Replace("PXX", gvStockDetails.Rows(RowNo).Cells(2).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("Equipment_Code").Value)
                        strPrintCode = strPrintCode.Replace("AXX", gvStockDetails.Rows(RowNo).Cells(0).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("AOffice").Value)
                        strPrintCode = strPrintCode.Replace("SERIALNUMBER", gvStockDetails.Rows(RowNo).Cells(4).Text) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("SERIALNUMBER").Value)
                        strPrintCode = strPrintCode.Replace("VSNO1", Mid(gvStockDetails.Rows(RowNo).Cells(5).Text, 1, 25)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
                        strPrintCode = strPrintCode.Replace("VSNO2", Mid(gvStockDetails.Rows(RowNo).Cells(5).Text, 26, 50)) 'objOutputPrintBarCodesTemplate.DocumentElement.SelectSingleNode("PRINTBARCODES").Attributes("VendorSR_Number").Value)
                        strFinalPrintCode = strFinalPrintCode & vbCrLf & strPrintCode
                    Next
                End If
                ' lblError.Text = strFinalPrintCode
                If strFinalPrintCode.Trim.Length > 0 Then
                    Session("PrintBarCode") = strFinalPrintCode
                End If

                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "BarCodes", "<script>Javascript:PrintBarCodes(ifrmPrnt);</script>", True)
                'RegisterClientScriptBlock("BarCodes", "")
            Else
                lblError.Text = ""
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
          StockDetailsSearch
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StockDetailsSearch()
          
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StockDetailsSearch()
         
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvStockDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvStockDetails.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvStockDetailsInstalled_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvStockDetailsInstalled.Sorting

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
            StockDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvStockDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvStockDetails.Sorting
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
            StockDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub gvStockDetailsOut_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvStockDetailsOut.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvStockDetailsOut_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvStockDetailsOut.Sorting
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
            StockDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
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

#End Region

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan

            'objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><SERIALNUMBER /><STOCKSTATUS /><VENDERSERIALNO /><CHALLANNUMBER /><LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /><GODOWNID /><EQUIP_GROUP /><EQUIP_CODE /><SERIALNUMBER /><STOCKSTATUS /><VENDERSERIALNO /><CHALLANNUMBER /><LCODE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRODUCTID").InnerText = drpProduct.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerText = drpGodown.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = txtEquipGroup.Text
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = txtEquipCode.Text
            objInputXml.DocumentElement.SelectSingleNode("SERIALNUMBER").InnerText = txtSerailNo.Text
            objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = rdStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = ""



            '@ Coding For Paging Ansd sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "EGROUP_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EGROUP_CODE" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
            'Here Back end Method Call
            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Export(objOutputXml)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel

        '<DETAILS PRODUCTID="41" AOFFICE="DEL" EGROUP_CODE="CPU" EQUIPMENT_CODE="CM2" PRODUCTNAME="CM2 DELL" SERIALNUMBER="CM2DEL00009" VENDORSR_NUMBER="T5GSY" STATUS="3" CHALLANNUMBER="" CREATIONDATE="" GODOWNID="" CHALLANCATEGORY="" SUPPLIERID="" LCODE="" OUTTO="" PRODUCTRECEIVEDDATE="" ORDERNUMBER="" /> 

        ' <DETAILS PRODUCTID="41" AOFFICE="DEL" EGROUP_CODE="CPU" EQUIPMENT_CODE="CM2" PRODUCTNAME="CM2 DELL" SERIALNUMBER="CM2DEL00001" VENDORSR_NUMBER="T5H38" STATUS="2" CHALLANNUMBER="DEL/1/2004/00607" CREATIONDATE="01-May-04 14:08" GODOWNID="1" CHALLANCATEGORY="1" SUPPLIERID="0" LCODE="2436" OUTTO="Inter Skylinks India Pvt Ltd" PRODUCTRECEIVEDDATE="" ORDERNUMBER="" /> 
        If rdStatus.SelectedValue = 5 Then
            ''code export for installed PC
            Dim strArray() As String = {"LCODE", "AGENCYNAME", "CHAINCODE", "OFFICEID", "CITY", "COUNTRY", "ONLINE_STATUS", "INSTALLATION_DATE", "EQUIPMENT_CODE", "HARDWARE_TYPE", "SERIALNUMBER"}
            Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
            objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "StockStatusDetails.xls")


        ElseIf rdStatus.SelectedValue = 2 Then
            Dim strArray() As String = {"Aoffice", "Equipment Group", "Equipment Code ", "Product", "Serial No.", "Vendor S. No.", "Challan No.", "Challan Date", "Out To", "Receiving Date"}
            Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 8, 9, 14, 15}
            objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "StockStatusDetails.xls")
        Else
            Dim strArray() As String = {"Aoffice", "Equipment Group", "Equipment Code ", "Product", "Serial No.", "Vendor S. No.", "Receiving Date"}
            Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 15}
            objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "StockStatusDetails.xls")

        End If
    End Sub

    
End Class
