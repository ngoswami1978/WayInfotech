Partial Class Inventory_INVUP_Challan
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler

    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
    Dim htPCData As New Hashtable
    Dim strResult As String = ""
    Dim strBuilder As New StringBuilder
    Dim objSecurityXml As New XmlDocument
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                '   Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Private Sub AgencyViewByOfficeID(ByVal strOfficeID As String)
        'Ashish code written on 06-AUG-2010 for officeID search
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Dim strhdEnAOffice As String = ""
        Dim strhdEnCallAgencyName_LCODE As String = ""

        Try
            '<AGNECY LOCATION_CODE="21555" CHAIN_CODE="951" OfficeID="BOMMY3100" NAME="(Aurion Pro Solutions)" ADDRESS="404,Winchester, High Street, Hiranandani" ADDRESS1="Garden" CITY="Mumbai" COUNTRY="India" PHONE="67707700" FAX="67707722" ONLINE_STATUS="DE" Aoffice="BOM" PINCODE="" Email="" CONTACT_PERSON="" CONTACT_PERSON_ID="" CITYID="180" COUNTRYID="1"/>
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><OFFICEID></OFFICEID><ResponsibleStaffID></ResponsibleStaffID></TA_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = strOfficeID
            objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerXml = objeAAMS.EmployeeID(Session("Security"))
            objOutputXml = objbzAgency.OfficeID_AgencySearch(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("AGNECY")
                    strhdEnAOffice = objED.Encrypt(.Attributes("Aoffice").Value())
                    strhdEnCallAgencyName_LCODE = objED.Encrypt(.Attributes("LOCATION_CODE").Value)
                End With
                strResult = objOutputXml.OuterXml + "$" + strhdEnAOffice + "$" + strhdEnCallAgencyName_LCODE
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
                strResult = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
        ''end here
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            'If Request.QueryString("TabType") IsNot Nothing Then
            '    hdTabType.Value = Request.QueryString("TabType")
            'End If
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            Dim m As ClientScriptManager = Me.ClientScript

            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataInventoryChallan", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)

            'For auto populate product grid in case of Replacement issued Challan
            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataChallanNumber", "'this is context from server'")
            strCallback = "function CallServerDataChallan(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServerDataChallan", strCallback, True)
            txtRplIssueChallanNo.Attributes.Add("onfocusout", "return FillProductDetails()")

            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataInventoryChallan", "'this is context from server'")
            strCallback = "function (args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "", strCallback, True)

            'Ashish code written on 06-AUG-2010 for officeID search
            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataChallanOfficeIDSearch", "'this is context from server'")
            strCallback = "function CallServerChallanOfficeIDSearch(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServerChallanOfficeIDSearch", strCallback, True)
            txtOfficeID1.Attributes.Add("onfocusout", "return FillAgencyDetailsChallanForOfficeIDSearch();")




            hdQueryString.Value = Request.QueryString.ToString
            txtApprovedDate.Text = Request.Form("txtApprovedDate")
            txtChallanDate.Text = Request.Form("txtChallanDate")
            txtRequestedDate.Text = Request.Form("txtRequestedDate")
            txtPurchaseOrder.Text = Request.Form("txtPurchaseOrder")
            txtOrderDate.Text = Request.Form("txtOrderDate")
            txtSupplier.Text = Request.Form("txtSupplier")
            txtDescription.Text = Request.Form("txtDescription")
            txtChallanGodownName.Text = Request.Form("txtChallanGodownName")
            txtGodownAddress.Text = Request.Form("txtGodownAddress")
            txtAgencyName.Text = Request.Form("txtAgencyName")
            txtAddress.Text = Request.Form("txtAddress")
            txtCountry.Text = Request.Form("txtCountry")
            txtCity.Text = Request.Form("txtCity")
            txtFax.Text = Request.Form("txtFax")
            txtPhone.Text = Request.Form("txtPhone")
            txtOfficeId.Text = Request.Form("txtOfficeId")
            txtSupplierName.Text = Request.Form("txtSupplierName")
            txtChallanSupplierAddress.Text = Request.Form("txtChallanSupplierAddress")
            txtChallanApprovedBy.Text = Request.Form("txtChallanApprovedBy")
            txtChallanReceivedBy.Text = Request.Form("txtChallanReceivedBy")
            txtChallanRequestedBy.Text = Request.Form("txtChallanRequestedBy")

            If txtAddress.Text.Trim.ToString() <> "" Then
                If Right(txtAddress.Text.Trim.ToString(), 2) = "**" Then ' For Amadeus India Orders 
                    tdOrderAmadeusIndia.Style.Add("display", "block")
                    chkOrderAmadeusIndia.Style.Add("display", "block")
                Else
                    tdOrderAmadeusIndia.Style.Add("display", "none")
                    chkOrderAmadeusIndia.Style.Add("display", "none")
                End If
            End If

            If Request.QueryString("Popup") Is Nothing Then
                ' lnkClose.Visible = False
            Else
                'lnkClose.Visible = True
            End If

            checkSecurity()
            If Not Page.IsPostBack Then
                If Not Request.QueryString("Action") Is Nothing Then
                    hdPageStatus.Value = Request.QueryString("Action").ToString
                End If
                If Not Request.QueryString("ChallanID") Is Nothing Then
                    hdChallanID.Value = objED.Decrypt(Request.QueryString("ChallanID").ToString)
                    hdEnChallanID.Value = Request.QueryString("ChallanID").ToString
                End If

                'Function to get maximum no of products and quantity can be added at a time
                GetConfigValue()
                LoadAllControl()
                Bindata()
                ChallanCategory()
                'Call view method based on condition 
                If hdChallanID.Value <> "" Then
                    ViewRecords()
                Else
                    btnExecute.Enabled = False
                    btnPrintLabel.Enabled = False
                    btnPrint.Enabled = False
                End If
            End If
            'Function to fill product grid when challan category is purchase order and Purchase order is  selected
            If hdPurchaseOrder.Value <> "" Then
                FillProductList()
                ManageOrder()
                BindProduct()
            End If
            'Function to fill order grid when challan category is customer and agency is selected
            If hdChallanLCodeTemp.Value <> "" Then
                FillInstalledPC()
                FillOrder()
                FillGodown()
                BindXml("1")
                ManageOrder()
                BindProduct()
            End If
            'Function to fill product grid with serial and vender serial number from pop up page
            If hdProductListPopUpPage.Value <> "" Then
                BindXml()
                FillProductPopup()
                ManageOrder()
                BindProduct()
            End If
            'Function to maintain state of radio button inside gridview
            If hdOrderList.Value <> "" Then
                BindOrderXml()
            End If

            'Function to get the status of checkbox from gvinstalledpc
            If hdInstalledPCXML.Value <> "" Then
                BindInstalledPCXml()
            End If

            If hdChallanDetails.Value <> "" Then
                hdProductList.Value = hdChallanDetails.Value
                ManageOrder()
                BindProduct()
            ElseIf (ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "2") Then
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
            End If
            If Request.QueryString("ChallanID") Is Nothing Then
                btnModifyChallan.Visible = False
            End If

            ''ASHISH SECURITY FOR "UPDATE REC.CHALLAN" Button
            '' Set Security for Modify Rec. challan
            objSecurityXml.LoadXml(Session("Security"))
            If (ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue <> "2") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Attributes("Value").Value)
                    If strBuilder(0) <> "0" Or strBuilder(1) <> "0" Or strBuilder(2) <> "0" Then
                        btnModifyChallan.Visible = True
                        gvInstalledPC.Enabled = True
                    Else
                        btnModifyChallan.Visible = False
                    End If
                End If

            Else
                btnModifyChallan.Visible = False
            End If
            ''end here

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Sub BindInstalledPCXml()
        Dim objXml As New XmlDocument
        Dim strRowId As String = ""
        Dim objNode As XmlNode
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim htPCData As New Hashtable
        If hdInstalledPCXML.Value <> "" Then
            objXml.LoadXml(hdInstalledPCXML.Value)
            ' If objXml.DocumentElement.SelectNodes("REPLACEMENT/PCINSTALLATION[@ROWID!='']").Count > 0 Then
            If objXml.DocumentElement.SelectNodes("PCINSTALLATION[@ROWID!='']").Count > 0 Then


                htPCData.Add("CPU", "0")
                htPCData.Add("MON", "0")
                htPCData.Add("KBD", "0")
                htPCData.Add("MSE", "0")
                For Each gvRow As GridViewRow In gvInstalledPC.Rows
                    strRowId = CType(gvRow.FindControl("hdRowID"), HiddenField).Value
                    objNode = objXml.DocumentElement.SelectSingleNode("PCINSTALLATION[@ROWID='" + strRowId + "']")
                    If CType(gvRow.FindControl("chkCPUTYPECHECK"), CheckBox).Checked Then
                        objNode.Attributes("CPUTYPECHECK").Value = "TRUE" ' CType(gvRow.FindControl("chkCPUTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                        htPCData("CPU") = Val(htPCData("CPU").ToString()) + 1
                    Else
                        objNode.Attributes("CPUTYPECHECK").Value = "FALSE" ' CType(gvRow.FindControl("chkCPUTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                    End If

                    If CType(gvRow.FindControl("chkMONTYPECHECK"), CheckBox).Checked Then
                        objNode.Attributes("MONTYPECHECK").Value = "TRUE" ' CType(gvRow.FindControl("chkMONTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                        htPCData("MON") = Val(htPCData("MON").ToString()) + 1
                    Else
                        objNode.Attributes("MONTYPECHECK").Value = "FALSE" ' CType(gvRow.FindControl("chkMONTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                    End If

                    If CType(gvRow.FindControl("chkKBDTYPECHECK"), CheckBox).Checked Then
                        objNode.Attributes("KBDTYPECHECK").Value = "TRUE" ' CType(gvRow.FindControl("chkKBDTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                        htPCData("KBD") = Val(htPCData("KBD").ToString()) + 1
                    Else
                        objNode.Attributes("KBDTYPECHECK").Value = "FALSE" 'CType(gvRow.FindControl("chkKBDTYPECHECK"), CheckBox).Checked.ToString.ToUpper
                    End If

                    If CType(gvRow.FindControl("chkMSETYPECHECK"), CheckBox).Checked Then
                        objNode.Attributes("MSETYPECHECK").Value = "TRUE" ' CType(gvRow.FindControl("chkMSETYPECHECK"), CheckBox).Checked.ToString.ToUpper
                        htPCData("MSE") = Val(htPCData("MSE").ToString()) + 1
                    Else
                        objNode.Attributes("MSETYPECHECK").Value = "FALSE" ' CType(gvRow.FindControl("chkMSETYPECHECK"), CheckBox).Checked.ToString.ToUpper
                    End If
                Next
                objXmlReader = New XmlNodeReader(objXml)
                ds.ReadXml(objXmlReader)
                gvInstalledPC.DataSource = ds.Tables("PCINSTALLATION").DefaultView
                gvInstalledPC.DataBind()
                hdInstalledPCXML.Value = objXml.OuterXml

                If chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                    trInstalledPC.Style.Add("display", "block")
                    txtRplIssueChallanNo.Style.Add("display", "none")
                ElseIf chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "2" Then
                    txtRplIssueChallanNo.Style.Add("display", "block")
                End If
            End If
        End If
    End Sub

    Sub checkSecurity()
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Attributes("Value").Value)
            End If


            'When View rights disabled
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
            End If
            'When Add rights disabled
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            'When modify rights disabled and Add rights enabled
            If strBuilder(2) = "0" And (hdChallanID.Value <> "" Or Request.QueryString("ChallanID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If
            'When modify rights Enabled and Add rights disabled
            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If

            'security for execute button
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EXECUTE_CHALLAN']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EXECUTE_CHALLAN']").Attributes("Value").Value)
            End If
            If strBuilder(2) = "0" Then
                btnExecute.Enabled = False
            End If
            'security for Print Label button
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PRINT_LABEL']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PRINT_LABEL']").Attributes("Value").Value)
            End If


            If strBuilder(4) = "0" Then
                btnPrintLabel.Enabled = False
            End If

            'security for checkbox chkOrderAmadeusIndia 'add on 13 Aug 08
            'if add or modify given then show the checkbox else donot show the checkbox
            'if checkbox is checked then order number is not mandatory in case of challan category "customer" and order no passed to backend is " 00/00/00" immaterial of order no selected 
            'if checkbox selected ideally user should not able to select order number from grid
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order for Amadeus India']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order for Amadeus India']").Attributes("Value").Value)
            End If
            If strBuilder(1) <> "0" Or strBuilder(2) <> "0" Then
                hdAmadeusOrderNoRights.Value = "1"
                chkOrderAmadeusIndia.CssClass = ""
                tdOrderAmadeusIndia.InnerText = "Order for Amadeus India"
            Else
                hdAmadeusOrderNoRights.Value = "0"
                chkOrderAmadeusIndia.CssClass = "displayNone"
                tdOrderAmadeusIndia.InnerText = ""
                chkOrderAmadeusIndia.Checked = False
            End If

            ' ''ASHISH SECURITY FOR "UPDATE REC.CHALLAN" Button
            'If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Count <> 0 Then
            '    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Attributes("Value").Value)
            '    If strBuilder(0) <> "0" Or strBuilder(1) <> "0" Or strBuilder(2) <> "0" Then
            '        btnModifyChallan.Visible = True
            '        gvInstalledPC.Enabled = True
            '    Else
            '        btnModifyChallan.Visible = False

            '    End If
            'End If
            ' ''end here


        Else
            strBuilder = objeAAMS.SecurityCheck(31)

            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Attributes("Value").Value)
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnModifyChallan.Visible = True
                    gvInstalledPC.Enabled = True
                Else
                    btnModifyChallan.Visible = False

                End If
            End If
        End If
    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return strResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim id As String
        id = eventArgument.ToString()
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument

            If (Right(id, 3) = "|CH") Then 'denotes callback for SerialNumber
                ViewRecordsbyChallanNumber(id.Split("|").GetValue(0))

            ElseIf (Right(id, 3) = "|OF") Then  ' ''ashish code written on 06-AUG-2010 for officeID search

                AgencyViewByOfficeID(id.Split("|").GetValue(0)) ' ''ashish code written on 06-AUG-2010 for officeID search

            Else
                'objInputXml.LoadXml("<INV_GETCHALLANSERIAL_INPUT><SerialNo></SerialNo><Type></Type></INV_GETCHALLANSERIAL_INPUT>")
                objInputXml.LoadXml("<INV_SEARCHSERIALNO_INPUT><ProductID /><ProductName /><SerialNO /><Type/><ChallanCategory /><ChallanType /><ReceiveLcode /><SecurityRight /><GodownID /><RGodownID/><GodownName /><RGodownName/></INV_SEARCHSERIALNO_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ProductID").InnerText = id.Split("|").GetValue(4)
                objInputXml.DocumentElement.SelectSingleNode("SerialNO").InnerText = id.Split("|").GetValue(0)
                objInputXml.DocumentElement.SelectSingleNode("Type").InnerText = id.Split("|").GetValue(1)
                objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = id.Split("|").GetValue(5) ' ddlChallanCategory.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("ChallanType").InnerText = id.Split("|").GetValue(6) ' ddlChallanType.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("ReceiveLcode").InnerText = id.Split("|").GetValue(7) ' hdChallanLCode.Value
                objInputXml.DocumentElement.SelectSingleNode("SecurityRight").InnerText = "False"
                If id.Split("|").GetValue(5) = "1" And id.Split("|").GetValue(6) = "2" Then
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRight").InnerText = hdMandatoryOrder.Value
                End If
                objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = id.Split("|").GetValue(8) ' ddlGodown.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("RGodownID").InnerText = id.Split("|").GetValue(9) ' hdChallanGodownId.Value
                objOutputXml = objbzChallan.ValidateSerialNumber(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    strResult = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("SERIALNUMBER").Value
                    strResult = strResult & "|" & objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("VENDORSR_NUMBER").Value & "|" & id.Split("|").GetValue(2) & "|" & id.Split("|").GetValue(3)
                    'CType(gvProduct.FindControl(id.Split("|").GetValue(2)), TextBox).Text = objOutputXml.DocumentElement.SelectSingleNode("CHALLANSERIAL").Attributes("SerialNumber").Value
                    'CType(gvProduct.FindControl(id.Split("|").GetValue(3)), TextBox).Text = objOutputXml.DocumentElement.SelectSingleNode("CHALLANSERIAL").Attributes("VenderSerialNo").Value
                Else
                    'CType(gvProduct.FindControl(id.Split("|").GetValue(2)), TextBox).Text = ""
                    ' CType(gvProduct.FindControl(id.Split("|").GetValue(3)), TextBox).Text = ""
                    strResult = "||" & id.Split("|").GetValue(2) & "|" & id.Split("|").GetValue(3) & "|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
                txtQuantity.Focus()

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            strResult = "||" & id.Split("|").GetValue(2) & "|" & id.Split("|").GetValue(3) & "|" & ex.Message
        End Try

    End Sub

    Sub LoadAllControl()
        '  objeAAMS.BindDropDown(ddlGodown, "GODOWN", True)
        FillGodown()
        txtRplIssueChallanNo.Style.Add("display", "none")
        'objeAAMS.BindDropDown(ddlStockGodown, "GODOWN", True)
        objeAAMS.BindDropDown(ddlProduct, "EQUIPMENTCODEWITH_MAINTAIN_BALANCE_BY", True, 1)
    End Sub

    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Details")
            TabText.Add("Product List")
            TabText.Add("Notes")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")
        If Button1.Text.Trim() = "Product List" Then
            Button1.Width = "90"
        End If

        'If hdPageHD_RE_ID.Value = "" Then
        '    If e.Item.ItemIndex = 4 Then
        '        Button1.Visible = False
        '    End If
        'End If

        'If Request.QueryString("Popup") Is Nothing Then
        '    If hdPageHD_RE_ID.Value = "" Then
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',4);")
        '    Else
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',5);")
        '    End If

        'Else
        Button1.Attributes.Add("onclick", "return ColorMethodInventoryChallan('" & Button1.ClientID.ToString() & "',3);")
        ' End If


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("INVUP_Challan.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdChallanID.Value <> "" Then
                lblError.Text = ""
                'txtDescription.Text = ViewState("vsDescription")
                'txtSolution.Text = ViewState("vsSolution")
                'txtFollowDesc.Text = ViewState("vsFollowDesc")
                'ddlMode.SelectedValue = ViewState("vsMode")
                'txtNxtFollowupDate.Text = ViewState("vsNxtFollowupDate")

                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("INVUP_Challan.aspx?" + strQueryString)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Sub ViewRecordsbyChallanNumber(ByVal strissueNo As String)
        Dim objInputXml, objOutputXml, objTempXml As New XmlDocument
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim strChallanID As String = String.Empty
        Dim strAgencyName As String = String.Empty
        Dim strReplacemantChallan As String = String.Empty
        Dim strChallanType As String = String.Empty
        Dim strChallanCatg As String = String.Empty


        objInputXml.LoadXml("<INV_VIEW_CHALLAN_INPUT><ChallanNumber></ChallanNumber></INV_VIEW_CHALLAN_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = strissueNo.ToString()

        'Here Back end Method Call 
        objOutputXml = objbzChallan.ViewbyChallanNumber(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            strChallanID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanID").InnerText

            'strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyNameD").InnerText
            'strReplacemantChallan = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("Replacement_Challan").InnerText
            'strChallanType = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").InnerText
            'strChallanCatg = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanCategory").InnerText

            'If txtAgencyName.Text.Trim() <> strAgencyName Then
            '    lblError.Text = "Challan umber is not valid for this Agency."
            '    Exit Sub
            'End If
            'If strChallanCatg.Trim() <> "1" And strChallanType.Trim() <> "2" Then
            '    lblError.Text = "Challan number is not valid issued replacement challan."
            '    Exit Sub
            'End If

            If strChallanID <> "" Then
                With objOutputXml.DocumentElement.SelectSingleNode("CHALLAN")
                    hdProductList.Value = objOutputXml.OuterXml
                    strResult = objOutputXml.OuterXml
                    ManageOrder()
                    BindProduct()
                End With
            Else
                hdInstalledPCXML.Value = ""
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
            End If
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Sub ViewRecords()
        Dim objInputXml, objOutputXml, objTempXml As New XmlDocument
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        objInputXml.LoadXml("<INV_VIEW_CHALLAN_INPUT><ChallanID></ChallanID></INV_VIEW_CHALLAN_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ChallanID").InnerText = hdChallanID.Value
        'Here Back end Method Call 

        objOutputXml = objbzChallan.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("CHALLAN")
                ddlChallanType.SelectedValue = .Attributes("ChallanType").Value
                ddlChallanCategory.SelectedValue = .Attributes("ChallanCategory").Value

                txtPurchaseOrder.Text = .Attributes("POID").Value
                txtOrderDate.Text = objeAAMS.GetDateFormat(.Attributes("OrderDate").Value, "yyyyMMdd", "dd-MMM-yyyy", "/")
                txtSupplier.Text = .Attributes("SupplierName").Value
                txtDescription.Text = .Attributes("SupplierNotes").Value
                hdChallanGodownId.Value = .Attributes("RGodownID").Value
                txtChallanGodownName.Text = .Attributes("RGodownName").Value
                txtGodownAddress.Text = .Attributes("RGodownAddress").Value
                hdChallanLCode.Value = .Attributes("Lcode").Value
                hdEnChallanLCode.Value = objED.Encrypt(.Attributes("Lcode").Value)
                txtAgencyName.Text = .Attributes("AgencyName").Value
                txtAddress.Text = .Attributes("AgencyAddress").Value
                txtCountry.Text = .Attributes("Country").Value
                txtCity.Text = .Attributes("City").Value
                txtPhone.Text = .Attributes("Phone").Value
                txtFax.Text = .Attributes("Fax").Value
                txtOfficeId.Text = .Attributes("OfficeID").Value
                chkMiscChallan.Checked = .Attributes("Mischardware_Challan").Value
                chkReplacementChallan.Checked = .Attributes("Replacement_Challan").Value
                txtRplIssueChallanNo.Text = .Attributes("Replacement_challanNumber").Value

                chkMiscChallan.Enabled = False
                chkReplacementChallan.Enabled = False

                If .Attributes("Replacement_Challan").Value.ToUpper = "TRUE" Then
                    tdMiscChallan.InnerText = ""
                    chkMiscChallan.Style.Add("display", "none")
                    trInstalledPC.Style.Add("display", "block")
                Else
                    tdMiscChallan.InnerText = "Misc H/W Challan"
                    chkMiscChallan.Style.Add("display", "block")
                    trInstalledPC.Style.Add("display", "none")
                End If


                If .Attributes("Mischardware_Challan").Value.ToUpper = "TRUE" Then
                    tdReplacementChallan.InnerText = ""
                    chkReplacementChallan.Style.Add("display", "none")
                Else
                    tdReplacementChallan.InnerText = "  Replacement Challan"
                    chkReplacementChallan.Style.Add("display", "block")
                End If

                If .Attributes("Replacement_Challan").Value.ToUpper = "FALSE" And .Attributes("Mischardware_Challan").Value.ToUpper = "FALSE" Then
                    tdMiscChallan.InnerText = "Misc H/W Challan"
                    chkMiscChallan.Style.Add("display", "block")
                    tdReplacementChallan.InnerText = "  Replacement Challan"
                    chkReplacementChallan.Style.Add("display", "block")
                End If

                txtChallanNo.Text = .Attributes("ChallanNumber").Value
                txtCreationDate.Text = .Attributes("CreationDate").Value
                ddlGodown.SelectedValue = .Attributes("GodownID").Value
                txtExecutionDate.Text = .Attributes("ExecutionDate").Value
                txtChallanDate.Text = objeAAMS.GetDateFormat(.Attributes("ChallanDate").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                txtIssueChallanNo.Text = .Attributes("Reference").Value
                hdChallanRequestedBy.Value = .Attributes("RequestBy").Value
                txtChallanRequestedBy.Text = .Attributes("RequestByName").Value
                txtRequestedDate.Text = objeAAMS.GetDateFormat(.Attributes("RequestDate").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                txtChallanApprovedBy.Text = .Attributes("ApprovedByName").Value
                hdChallanApprovedBy.Value = .Attributes("ApprovedBy").Value
                txtApprovedDate.Text = objeAAMS.GetDateFormat(.Attributes("ApprovalDate").Value, "yyyyMMdd", "dd/MM/yyyy", "/")
                txtChallanLoggedBy.Text = .Attributes("LoggedByName").Value
                ' hdChallanReceivedBy.Value = .Attributes("RecieveBY").Value
                'txtChallanReceivedBy.Text = .Attributes("RecieveBYName").Value
                txtNote.Text = .Attributes("Notes").Value

                If ddlChallanCategory.SelectedValue = 4 Then
                    txtChallanReceivedBy.CssClass = "textboxgrey"
                    txtChallanReceivedBy.ReadOnly = True
                    Img5.Visible = False
                    If .Attributes("RecieveBY").Value.Split("|").Length = 2 Then
                        hdChallanReceivedBy.Value = .Attributes("RecieveBY").Value.Split("|").GetValue(1)
                        txtChallanReceivedBy.Text = .Attributes("RecieveBY").Value.Split("|").GetValue(0)
                    Else
                        txtChallanReceivedBy.Text = .Attributes("RecieveBY").Value
                    End If
                Else
                    txtChallanReceivedBy.Text = .Attributes("RecieveBY").Value
                    hdChallanReceivedBy.Value = ""
                    txtChallanReceivedBy.CssClass = "textbox"
                    txtChallanReceivedBy.ReadOnly = False
                    Img5.Visible = True
                End If
                ddlChallanCategory.Enabled = False
                ddlChallanType.Enabled = False
                hdProductList.Value = objOutputXml.OuterXml
                btnExecute.Enabled = True
                btnPrint.Enabled = True
                If ddlChallanCategory.SelectedValue = "2" And ddlChallanType.SelectedValue = "2" And txtExecutionDate.Text <> "" Then
                    btnPrintLabel.Enabled = True
                Else
                    btnPrintLabel.Enabled = False
                End If
                ManageOrder()
                BindProduct()
                If ddlChallanCategory.SelectedValue = "1" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("ORDER_NUMBER").Value <> "" Then
                        If objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("ORDER_NUMBER").Value <> "00/00/00" Then 'Added on  13 aug 08 for "amadeus order" security
                            hdOrderQuantity.Value = objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("ORDER_NUMBER").Value & "|" & objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("APC").Value
                            Dim objXmlReader As XmlNodeReader
                            Dim ds As New DataSet
                            objXmlReader = New XmlNodeReader(objOutputXml)
                            ds.ReadXml(objXmlReader)
                            gvOrder.DataSource = ds.Tables("AGENCYORDER").DefaultView
                            gvOrder.DataBind()
                        ElseIf objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("ORDER_NUMBER").Value = "00/00/00" Then
                            chkOrderAmadeusIndia.Checked = True 'Added on  13 aug 08 for "amadeus order" security
                        End If
                    End If
                    If objOutputXml.DocumentElement.SelectSingleNode("REPLACEMENT") IsNot Nothing Then

                        hdInstalledPCXML.Value = "<MS_GETPCINSTALLATION_OUTPUT>" & objOutputXml.DocumentElement.SelectSingleNode("REPLACEMENT").InnerXml & "</MS_GETPCINSTALLATION_OUTPUT>"
                        'For Each objNodeXml As XmlNode In objOutputXml.DocumentElement.SelectNodes("REPLACEMENT/PCINSTALLATION")
                        '    objInputXml.DocumentElement.SelectSingleNode("REPLACEMENT").AppendChild(objInputXml.ImportNode(objNodeXml, False))
                        'Next

                        BindInstalledPCXml()



                    End If

                    'If hdInstalledPCXML.Value <> "" Then
                    '    objInstalledPcXML.LoadXml(hdInstalledPCXML.Value)
                    '    For Each objNodeXml As XmlNode In objInstalledPcXML.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' or @MONTYPECHECK='TRUE' or @KBDTYPECHECK ='TRUE' or MSETYPECHECK='TRUE']")
                    '        objInputXml.DocumentElement.SelectSingleNode("REPLACEMENT").AppendChild(objInputXml.ImportNode(objNodeXml, False))
                    '    Next
                    'End If



                End If
                If txtExecutionDate.Text <> "" Then
                    txtRplIssueChallanNo.Enabled = False
                    For Each ctrl As Control In Page.Controls
                        ChangeControlStatus(ctrl)
                    Next
                Else
                    btnReset.Enabled = True
                End If

            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    'Code to add Product in xml
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objOutputXml, objInstalledPCXML, objOrderXml As New XmlDocument
            Dim ds As New DataSet
            Dim cnQuantityCount As Integer
            Dim intcnReplacementCount As Integer = 0
            Dim objNodeListTemp As XmlNodeList
            Dim strEGROUP_CODE_CHECK As String = ""
            Dim strOrder_Number As String
            BindXml()
            If hdProductList.Value <> "" Then
                objOutputXml.LoadXml(hdProductList.Value)
            Else
                objOutputXml.LoadXml("<Root><CHALLAN ChallanType='' ChallanCategory='' ChallanID='' ChallanNumber ='' CreationDate=''  POID='' OrderDate='' SupplierName='' SupplierNotes='' Lcode='' AgencyName='' AgencyAddress='' City='' Country='' Phone='' Fax=''  OfficeID='' GodownID='' GodownAddress='' RGodownID ='' ExecutionDate=''  ChallanDate=''  Reference='' LoggedBy=''  LoggedByName='' RecieveBY='' RecieveBYName='' RequestBy='' RequestByName='' RequestDate=''  ApprovedBy='' ApprovedByName='' ApprovalDate='' Notes=''>	<CHALLANDETAILS LineNumber=''	ProductID='' ProductName='' Qty='' SerialNumber='' VenderSerialNo='' MAINTAIN_BALANCE_BY ='' MAINTAIN_BALANCE='' EGROUP_CODE='' /></CHALLAN></Root>")
            End If

            If hdInstalledPCXML.Value <> "" Then
                objInstalledPCXML.LoadXml(hdInstalledPCXML.Value)

            End If

            If ddlChallanCategory.SelectedValue = ChallanType.PurchaseOrder Then
                If GetQuantityCount(hdProductIDANDQuantity.Value) = True Then
                Else
                    lblError.Text = "Quantity entered exceeds Quantity available "
                    BindProduct()
                    Exit Sub
                End If
            End If

            'added on 7 nov 09 when selected order type id matches with config order type id value then only (MON ,TFT) products allowed
            If ddlChallanCategory.SelectedValue = ChallanType.Customer Then
                If hdOrderList.Value <> "" Then
                    objOrderXml.LoadXml(hdOrderList.Value)
                    For Each objnode As XmlNode In objOrderXml.DocumentElement.SelectNodes("AGENCYORDER")
                        strOrder_Number = objnode.Attributes("ORDER_NUMBER").Value
                        If strOrder_Number.ToUpper = hdOrderQuantity.Value.Split("|").GetValue(0).ToString.ToUpper Then
                            If objnode.Attributes("ORDER_TYPE_ID").Value.ToUpper = hdOrderTypeValue.Value.ToUpper Then
                                If Not (ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "MON" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "TFT") Then
                                    lblError.Text = "Only MON and TFT products allowed"
                                    ManageOrder()
                                    BindProduct()
                                    Exit Sub
                                End If

                            End If
                        End If
                    Next

                End If
            End If
            'end

            If GetQuantityCount() = True Then
                If GetProductCount() = True Then
                    Dim objXmlNodeClone As XmlNode
                    Dim cn As Integer = 0
                    Try
                        'if ddlProduct selected value, at position "1 and 2 " ,after splitting ,is false and false, then set the value of cn to -"1" 
                        'in this case txtquantity textbox becomes readonly
                        If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
                            cn = -1
                        Else
                            cn = Convert.ToInt32(txtQuantity.Text)
                        End If
                    Catch ex As Exception
                        cn = -1
                    End Try
                    'Start Code to check quantity when challan category is customer and Replacement challan is not checked(on 4 nov 09 conditions changed now replacement challan may be checked or not) (order number mandatory) and Hardware type is 1A HW  
                    'Added Quantity will not exceed the quantity that is mentioned against selected order number
                    'If ddlChallanCategory.SelectedValue = "1" And (chkReplacementChallan.Checked = False And chkMiscChallan.Checked = False) Then 'Commented on 13 aug 08
                    ' If ddlChallanCategory.SelectedValue = "1" And (chkReplacementChallan.Checked = False And chkMiscChallan.Checked = False And chkOrderAmadeusIndia.Checked = False) Then 'added on 13 aug 08 'commented on 4 nov 09
                    If ddlChallanCategory.SelectedValue = "1" And (chkMiscChallan.Checked = False And chkOrderAmadeusIndia.Checked = False) Then 'added on 04 nov 09
                        'If hdOrderQuantity.Value <> "" Then

                        If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then

                        End If

                        If hdOrderQuantity.Value <> "" Then
                            cnQuantityCount = Convert.ToInt16(hdOrderQuantity.Value.Split("|").GetValue(1))

                            'If ((Not (ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE")) And (ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "CPU" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "MON" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "KBD" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "MSE")) Then
                            If ((Not (ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE")) And (ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "CPU" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "MON" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "KBD" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "MSE" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "LAP" Or ddlProduct.SelectedValue.Split("|").GetValue(3).ToString = "TFT")) Then 'modify on 13 oct 09 as told by neeraj
                                If chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                                    If cn > cnQuantityCount Then
                                        lblError.Text = "Maximum quantity allowed for this product is " & cnQuantityCount
                                        BindProduct()
                                        Exit Try
                                    Else
                                        strEGROUP_CODE_CHECK = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                                        strEGROUP_CODE_CHECK = IIf(strEGROUP_CODE_CHECK = "LAP", "CPU", strEGROUP_CODE_CHECK)
                                        strEGROUP_CODE_CHECK = IIf(strEGROUP_CODE_CHECK = "TFT", "MON", strEGROUP_CODE_CHECK)
                                        strEGROUP_CODE_CHECK = strEGROUP_CODE_CHECK & "TYPECHECK"
                                        intcnReplacementCount = objInstalledPCXML.DocumentElement.SelectNodes("PCINSTALLATION[@" & strEGROUP_CODE_CHECK & "='TRUE']").Count
                                        If cn > intcnReplacementCount Then
                                            lblError.Text = "Maximum quantity allowed for this product is " & intcnReplacementCount
                                            ManageOrder()
                                            BindProduct()
                                            Exit Try
                                        End If
                                    End If
                                Else
                                    If cn > cnQuantityCount Then
                                        lblError.Text = "Maximum quantity allowed for this product is " & cnQuantityCount
                                        ManageOrder()
                                        BindProduct()
                                        Exit Try
                                    End If
                                End If

                                'commented on 4 nov 09
                                'If cn > cnQuantityCount Then
                                '    lblError.Text = "Maximum quantity allowed for this product is " & cnQuantityCount
                                '    BindProduct()
                                '    Exit Try
                                'End If
                                'End
                            End If
                        Else
                            BindProduct()

                            If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked = False Then
                                lblError.Text = "Select atleast one order number"
                                Exit Try
                            End If

                        End If
                    End If
                    'End code
                    If cn > 0 Or cn = -1 Then
                        If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
                            cn = 1
                            Dim objNode As XmlNode = Nothing
                            objNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                            If objNode Is Nothing Then
                                objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                                With objXmlNodeClone
                                    .Attributes("LineNumber").Value = Convert.ToInt32(hdSNo.Value) + 1
                                    .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                                    .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                                    .Attributes("Qty").Value = ""
                                    .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                                    .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                                    .Attributes("EGROUP_CODE").Value = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                                    objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                    hdSNo.Value = (Convert.ToInt32(hdSNo.Value) + 1).ToString
                                End With
                            End If
                        Else
                            If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "TRUE" Then
                                objNodeListTemp = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                                If objNodeListTemp.Count > cn Then
                                    Dim iTemp As Integer = 0
                                    For Each objNodeTemp As XmlNode In objNodeListTemp
                                        If iTemp < objNodeListTemp.Count - cn Then
                                            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNodeTemp)
                                            iTemp = iTemp + 1
                                        End If
                                    Next
                                Else

                                    For i As Integer = 0 To cn - 1 - objNodeListTemp.Count
                                        objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                                        With objXmlNodeClone
                                            .Attributes("LineNumber").Value = Convert.ToInt32(hdSNo.Value) + 1
                                            .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                                            .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                                            .Attributes("Qty").Value = txtQuantity.Text
                                            .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                                            .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                                            .Attributes("EGROUP_CODE").Value = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                                            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                            hdSNo.Value = (Convert.ToInt32(hdSNo.Value) + 1).ToString
                                        End With
                                    Next

                                End If
                            Else


                                ''******************code written by ashish on date 14-Jan-2011***************************
                                Dim objNode As XmlNode
                                objNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                                If objNode IsNot Nothing Then
                                    objNode.Attributes("Qty").Value = cn.ToString
                                Else

                                    For i As Integer = 0 To cn - 1
                                        objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                                        With objXmlNodeClone
                                            .Attributes("LineNumber").Value = Convert.ToInt32(hdSNo.Value) + 1
                                            .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                                            .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                                            .Attributes("Qty").Value = txtQuantity.Text
                                            .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                                            .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                                            .Attributes("EGROUP_CODE").Value = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                                            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                            hdSNo.Value = (Convert.ToInt32(hdSNo.Value) + 1).ToString
                                        End With
                                    Next
                                End If
                            End If
                            '**************************** END HERE*******************************************************


                            '****************************************previous code************************************************
                            '    Dim objNode As XmlNode

                            '    If objNode IsNot Nothing Then
                            '        objNode.Attributes("Qty").Value = cn.ToString '(cn + Convert.ToInt32(objNode.Attributes("Qty").Value)).ToString
                            '    Else
                            '        objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                            '        With objXmlNodeClone
                            '            .Attributes("LineNumber").Value = Convert.ToInt32(hdSNo.Value) + 1
                            '            .Attributes("ProductID").Value = ddlProduct.SelectedValue.Split("|").GetValue(0)
                            '            .Attributes("ProductName").Value = ddlProduct.SelectedItem.Text
                            '            .Attributes("Qty").Value = txtQuantity.Text
                            '            .Attributes("MAINTAIN_BALANCE_BY").Value = ddlProduct.SelectedValue.Split("|").GetValue(1)
                            '            .Attributes("MAINTAIN_BALANCE").Value = ddlProduct.SelectedValue.Split("|").GetValue(2)
                            '            .Attributes("EGROUP_CODE").Value = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                            '            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                            '            hdSNo.Value = (Convert.ToInt32(hdSNo.Value) + 1).ToString
                            '        End With
                            '    End If
                            'End If
                            '***************************************end here********************************************************       


                        End If
                    End If

                    If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                        objOutputXml = ManageOrder(objOutputXml)

                        If ValidateAddedReplacementProduct(objOutputXml, True) = False Then
                            lblError.Text = "Product added not exists in selected product to replace"
                            ManageOrder()
                            BindProduct()
                            Exit Sub
                        End If

                        If ValidateAddedReplacementProduct(objOutputXml) = False Then
                            lblError.Text = "Product added exceeds the product selected to replace"
                            ManageOrder()
                            BindProduct()
                            Exit Sub
                        End If

                    ElseIf ddlChallanCategory.SelectedValue = "1" And (chkMiscChallan.Checked = False And chkOrderAmadeusIndia.Checked = False And chkReplacementChallan.Checked = False) Then
                        objOutputXml = ManageOrder(objOutputXml)
                        If ValidateAddedProduct(objOutputXml) = False Then
                            lblError.Text = "Maximum Quantity Exceeds for this product"
                            ManageOrder()
                            BindProduct()

                            Exit Sub
                        End If
                    End If

                    hdProductList.Value = objOutputXml.OuterXml
                    If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                        hdChallanDetails.Value = hdProductList.Value
                    End If
                    ManageOrder()
                    BindProduct()

                Else
                    lblError.Text = "Maximum Product allowed is " + hdProductCount.Value
                    BindProduct()
                End If
            Else
                lblError.Text = "Maximum quantity allowed is " + hdQuantity.Value
                BindProduct()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
            txtQuantity.ReadOnly = True
        Else
            txtQuantity.ReadOnly = False
        End If

    End Sub

    Protected Sub gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand
        Dim strSno As String
        strSno = e.CommandArgument.ToString
        Dim objOutputXml As New XmlDocument
        Dim objSearchNode As XmlNode
        objOutputXml.LoadXml(hdProductList.Value)
        objSearchNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@LineNumber='" + strSno + "']")
        objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objSearchNode)
        hdProductList.Value = objOutputXml.OuterXml
        If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
            hdChallanDetails.Value = hdProductList.Value
        End If
        BindXml(1)
        ManageOrder()
        BindProduct()
        gvProduct.Focus()
    End Sub

    Protected Sub gvProduct_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProduct.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim txt As TextBox
        Dim txt1 As TextBox
        Dim strProductID As String
        strProductID = DataBinder.Eval(e.Row.DataItem, "ProductID")
        If (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "TRUE") Then
            'e.Row.Cells(5).Controls.Clear()
            'e.Row.Cells(5).Text = DataBinder.Eval(e.Row.DataItem, "VenderSerialNo")
            If ddlChallanCategory.SelectedValue = "4" Or ddlChallanCategory.SelectedValue = "1" Then
                If e.Row.Cells(5).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                    txt = CType(e.Row.Cells(5).Controls(1), TextBox)
                    If txt IsNot Nothing Then

                        If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                            '  txt.Attributes.Add("onmouseout", "fillSerial2('" + txt.ClientID + "','" + strProductID + "')")
                            txt.Attributes.Add("onblur", "fillSerial2InventoryChallan('" + txt.ClientID + "' , '" + strProductID + "' , '" + "Applicable" + "')")

                        End If
                    End If
                End If
            End If

        Else

            If ddlChallanCategory.SelectedValue = "4" Or ddlChallanCategory.SelectedValue = "1" Then
                If e.Row.Cells(5).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                    txt = CType(e.Row.Cells(5).Controls(1), TextBox)
                    If txt IsNot Nothing Then

                        If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                            '  txt.Attributes.Add("onmouseout", "fillSerial2('" + txt.ClientID + "','" + strProductID + "')")
                            txt.Attributes.Add("onblur", "fillSerial2InventoryChallan('" + txt.ClientID + "','" + strProductID + "', '" + "NotApplicable" + "' )")

                        End If
                    End If
                End If
            End If
        End If

        Try

        
            If (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "TRUE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "TRUE") Or (ddlChallanCategory.SelectedValue = "2") Then

                If (ddlChallanCategory.SelectedValue = "2") Then
                    e.Row.Cells(4).Controls.Clear()
                    e.Row.Cells(4).Text = DataBinder.Eval(e.Row.DataItem, "SerialNumber")
                End If
                
                If ddlChallanCategory.SelectedValue = "4" Or ddlChallanCategory.SelectedValue = "1" Then
                    If e.Row.Cells(4).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                        txt1 = CType(e.Row.Cells(4).Controls(1), TextBox)
                        If txt1 IsNot Nothing Then

                            If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                                ' txt1.Attributes.Add("onmouseout", "fillSerial1('" + txt1.ClientID + "','" + strProductID + "')")
                                txt1.Attributes.Add("onblur", "fillSerial1InventoryChallan('" + txt1.ClientID + "','" + strProductID + "', '" + "Applicable" + "' )")
                            End If
                        End If
                    End If
                End If

            Else

                If ddlChallanCategory.SelectedValue = "4" Or ddlChallanCategory.SelectedValue = "1" Then
                    If e.Row.Cells(4).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                        txt1 = CType(e.Row.Cells(4).Controls(1), TextBox)
                        If txt1 IsNot Nothing Then

                            If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                                ' txt1.Attributes.Add("onmouseout", "fillSerial1('" + txt1.ClientID + "','" + strProductID + "')")
                                txt1.Attributes.Add("onblur", "fillSerial1InventoryChallan('" + txt1.ClientID + "','" + strProductID + "', '" + "NotApplicable" + "' )")
                            End If
                        End If
                    End If
                End If
            End If


            '''''''''''''''''''''''''''
            If (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "FALSE") Or (DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE_BY").ToString.ToUpper = "FALSE" And DataBinder.Eval(e.Row.DataItem, "MAINTAIN_BALANCE").ToString.ToUpper = "TRUE") Then
                e.Row.Cells(3).Controls.Clear()
                e.Row.Cells(3).Text = DataBinder.Eval(e.Row.DataItem, "Qty")

                'If e.Row.Cells(3).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                '    txt1 = CType(e.Row.Cells(3).Controls(1), TextBox)
                '    If txt1 IsNot Nothing Then
                '        If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                '            txt1.Attributes.Add("onkeyup", "checknumericGreaterZero('" + txt1.ClientID + "')")
                '        End If
                '    End If
                'End If

                'Else
                '    If e.Row.Cells(3).Controls(1).GetType.Name.ToUpper = "TEXTBOX" Then
                '        txt1 = CType(e.Row.Cells(3).Controls(1), TextBox)
                '        If txt1 IsNot Nothing Then
                '            If txtChallanNo.Text.Trim = "" Or txtChallanNo.Text.Trim = "0" Then
                '                txt1.Attributes.Add("onkeyup", "checknumericGreaterZero('" + txt1.ClientID + "')")
                '            End If
                '        End If
                '    End If
            Else
                e.Row.Cells(3).Controls.Clear()
                e.Row.Cells(3).Text = DataBinder.Eval(e.Row.DataItem, "Qty")
            End If

            ''''''''''''''''''''''''''


            If e.Row.RowIndex = 0 Then
                ViewState("ProductID") = strProductID
            Else
                If strProductID = ViewState("ProductID") Then
                    e.Row.Cells(2).Text = ""
                    e.Row.Cells(3).Text = ""
                End If
            End If
            ViewState("ProductID") = strProductID
            hdSNo.Value = e.Row.Cells(1).Text
            If txtChallanNo.Text.Trim <> "" And txtChallanNo.Text.Trim <> "0" Then
                Dim lnkDelete As LinkButton
                lnkDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
                lnkDelete.Enabled = False
            End If
        Catch ex As Exception

        End Try


    End Sub
    'Code to fill vender serial number in xml from grid
    Sub BindXml()
        Dim objOutputXml As New XmlDocument
        If gvProduct.Rows.Count > 0 Then

            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdProductList.Value)

            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID != '']")
            'CHALLANDETAILS =''	ProductID='' ProductName='' Qty='' SerialNumber='' VenderSerialNo='' txtAmadeusSerial txtVenderSerial
            
            For i As Integer = 0 To objNodeList.Count - 1
                objNode = objNodeList(i)
                ' objNode.Attributes("SNo").Value = gvProduct.Rows(i).Cells(0).Text
                ' objNode.Attributes("ProductID").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("hdProductID"), HiddenField).Value
                '  objNode.Attributes("ProductName").Value = gvProduct.Rows(i).Cells(1).Text
                'objNode.Attributes("Qty").Value = gvProduct.Rows(i).Cells(2).Text

                ' If ddlChallanCategory.SelectedValue <> "2" Then
                If ddlChallanCategory.SelectedValue = "2" And ddlChallanType.SelectedValue = "2" Then
                Else
                    If CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox) IsNot Nothing Then
                        objNode.Attributes("Qty").Value = CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox).Text
                    End If
                End If


                If CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox) IsNot Nothing Then
                    objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox).Text
                End If
                ' End If

                If CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                    objNode.Attributes("VenderSerialNo").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox).Text
                End If

            Next
        End If

        'If chkReplacementChallan.Checked Then
        '    txtRplIssueChallanNo.Style.Add("display", "block")
        'End If

        If chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
            trInstalledPC.Style.Add("display", "block")
            txtRplIssueChallanNo.Style.Add("display", "none")
        ElseIf chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "2" Then
            txtRplIssueChallanNo.Style.Add("display", "block")
        End If

        hdProductList.Value = objOutputXml.OuterXml
        If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
            hdChallanDetails.Value = hdProductList.Value
        End If
    End Sub

    Sub BindXml(ByVal st As String)
        Dim objOutputXml As New XmlDocument
        If gvProduct.Rows.Count > 0 Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            If hdProductList.Value <> "" Then


                objOutputXml.LoadXml(hdProductList.Value)

                objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID != '']")
                For i As Integer = 0 To objNodeList.Count - 1
                    objNode = objNodeList(i)
                    If ddlChallanCategory.SelectedValue <> "2" Then
                        If CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox) IsNot Nothing Then
                            objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox).Text
                        End If
                    End If
                    If CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                        objNode.Attributes("VenderSerialNo").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox).Text
                    End If
                    If CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox) IsNot Nothing Then
                        objNode.Attributes("Qty").Value = CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox).Text
                    End If
                    'If CType(gvProduct.Rows(i).Cells(5).FindControl("hdManintainBy"), HiddenField) IsNot Nothing Then
                    '    objNode.Attributes("MAINTAIN_BALANCE_BY").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("hdManintainBy"), HiddenField).Value
                    'End If
                    'If CType(gvProduct.Rows(i).Cells(5).FindControl("hdManintain"), HiddenField) IsNot Nothing Then
                    '    objNode.Attributes("MAINTAIN_BALANCE").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("hdManintain"), HiddenField).Value
                    'End If hdOrderList.Value
                Next
                hdProductList.Value = objOutputXml.OuterXml
                If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                    hdChallanDetails.Value = hdProductList.Value
                End If
            End If
        End If

        'If chkReplacementChallan.Checked Then
        '    txtRplIssueChallanNo.Style.Add("display", "block")
        'End If

        If chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
            trInstalledPC.Style.Add("display", "block")
            txtRplIssueChallanNo.Style.Add("display", "none")
        ElseIf chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "2" Then
            txtRplIssueChallanNo.Style.Add("display", "block")
        End If

    End Sub

    Sub BindXml(ByVal st As Integer)
        Dim objOutputXml As New XmlDocument
        Dim intProductId, intProductcount As Integer
        If gvProduct.Rows.Count > 0 Then
            Dim objNodeList As XmlNodeList
            Dim objNode As XmlNode
            objOutputXml.LoadXml(hdProductList.Value)

            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID != '']")
            For i As Integer = 0 To objNodeList.Count - 1
                objNode = objNodeList(i)
                For j As Integer = 0 To gvProduct.Rows.Count - 1
                    If gvProduct.Rows(j).Cells(1).Text = objNode.Attributes("LineNumber").Value Then

                        '---start
                        If i = 0 Then
                            intProductId = objNode.Attributes("ProductID").Value
                            If gvProduct.Rows(i).Cells(3).Text <> "" Then
                                objNode.Attributes("Qty").Value = gvProduct.Rows(i).Cells(3).Text
                                intProductcount = gvProduct.Rows(i).Cells(3).Text
                            End If
                        ElseIf intProductId = objNode.Attributes("ProductID").Value Then
                            objNode.Attributes("Qty").Value = intProductcount
                        Else
                            If gvProduct.Rows(i).Cells(3).Text <> "" Then
                                objNode.Attributes("Qty").Value = gvProduct.Rows(i).Cells(3).Text
                                intProductcount = gvProduct.Rows(i).Cells(3).Text
                            End If
                        End If
                        '---end

                        'If CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox) IsNot Nothing Then
                        '    objNode.Attributes("Qty").Value = CType(gvProduct.Rows(i).Cells(3).FindControl("txtQty"), TextBox).Text
                        'End If
                        If CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox) IsNot Nothing Then
                            objNode.Attributes("VenderSerialNo").Value = CType(gvProduct.Rows(i).Cells(5).FindControl("txtVenderSerial"), TextBox).Text
                        End If
                        If ddlChallanCategory.SelectedValue <> "2" Then
                            If CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox) IsNot Nothing Then
                                objNode.Attributes("SerialNumber").Value = CType(gvProduct.Rows(i).Cells(4).FindControl("txtAmadeusSerial"), TextBox).Text
                            End If
                        End If
                    End If
                Next
            Next
            hdProductList.Value = objOutputXml.OuterXml
            If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                hdChallanDetails.Value = hdProductList.Value
            End If
        End If
    End Sub
    'Code to maintain state of radio button inside gridview
    Sub BindOrderXml()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml.LoadXml(hdOrderList.Value)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvOrder.DataSource = ds.Tables("AGENCYORDER").DefaultView
            gvOrder.DataBind()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub

    'Code to maintain sequence of xml as same as grid
    Sub ManageOrder()
        Dim objOutputXml As New XmlDocument
        Dim objtempXml As New XmlDocument
        Dim objProductIDQuantity As New XmlDocument
        Dim ar As New ArrayList
        ' Dim arGodownName As New ArrayList
        Dim objXmlNodeClone As XmlNode
        Dim intQty As Integer = 0
        If hdProductList.Value <> "" Then


            objOutputXml.LoadXml(hdProductList.Value)
            objtempXml.LoadXml("<ROOT><CHALLAN><CHALLANDETAILS LineNumber=''	ProductID='' ProductName='' Qty='' SerialNumber='' VenderSerialNo='' MAINTAIN_BALANCE_BY ='' MAINTAIN_BALANCE='' EGROUP_CODE=''/></CHALLAN></ROOT>")
            objProductIDQuantity.LoadXml("<Root><Product ID='' Qty=''/></Root>")
            Dim objNodeList As XmlNodeList
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNode)
            Next

            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS")
            Dim objTempCloneNode, objTempNode As XmlNode
            objTempNode = objProductIDQuantity.DocumentElement.SelectSingleNode("Product")
            For Each objNode As XmlNode In objNodeList
                If ar.Contains(objNode.Attributes("ProductID").Value) Then

                Else
                    ar.Add(objNode.Attributes("ProductID").Value)

                    objTempCloneNode = objTempNode.CloneNode(True)
                    objTempCloneNode.Attributes("ID").Value = objNode.Attributes("ProductID").Value
                    objTempCloneNode.Attributes("Qty").Value = objNode.Attributes("Qty").Value
                    objProductIDQuantity.DocumentElement.AppendChild(objTempCloneNode)
                    'htProducTID_Quantity.Add(objNode.Attributes("ProductID").Value, objNode.Attributes("Qty").Value)
                End If
            Next
            objProductIDQuantity.DocumentElement.RemoveChild(objTempNode)
            hdProductIDANDQuantity.Value = objProductIDQuantity.OuterXml
            Dim LineNumber As Integer = 1
            For i As Integer = 0 To ar.Count - 1
                objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ar(i) + "']")
                For Each objNode As XmlNode In objNodeList
                    objXmlNodeClone = objtempXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                    With objXmlNodeClone
                        .Attributes("LineNumber").Value = LineNumber
                        .Attributes("ProductID").Value = objNode.Attributes("ProductID").Value
                        .Attributes("ProductName").Value = objNode.Attributes("ProductName").Value
                        .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                        .Attributes("SerialNumber").Value = objNode.Attributes("SerialNumber").Value
                        .Attributes("VenderSerialNo").Value = objNode.Attributes("VenderSerialNo").Value
                        .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                        .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                        .Attributes("EGROUP_CODE").Value = objNode.Attributes("EGROUP_CODE").Value
                        objtempXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                        LineNumber = LineNumber + 1
                    End With
                Next
            Next

            'For i As Integer = 0 To ar.Count - 1
            '    objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ar(i) + "']")
            '    For Each objNode As XmlNode In objNodeList
            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveAll()
            '     Next
            '  Next
            Dim objDocFrag As XmlDocumentFragment

            objDocFrag = objOutputXml.CreateDocumentFragment()
            objDocFrag.InnerXml = objtempXml.DocumentElement.SelectSingleNode("CHALLAN").InnerXml
            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objDocFrag)

            Dim objNodeList1 As XmlNodeList
            objNodeList1 = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS")
            If ddlChallanCategory.SelectedValue = "2" And ddlChallanType.SelectedValue = "2" Then
                For Each objNode As XmlNode In objNodeList1
                    Dim objNodeList2 As XmlNodeList
                    objNodeList2 = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("ProductID").Value + "']")

                    If (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToLower = "true" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToLower = "true") Or (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToLower = "true" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToLower = "false") Then
                        objNode.Attributes("Qty").Value = objNodeList2.Item(0).Attributes("Qty").Value.ToString()
                    Else
                        objNode.Attributes("Qty").Value = objNodeList2.Count
                    End If
                    If (objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "FALSE") Or objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "" Then
                        objNode.Attributes("Qty").Value = ""
                    End If
                Next
            Else
                For Each objNode As XmlNode In objNodeList1
                    Dim objNodeList2 As XmlNodeList
                    objNodeList2 = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("ProductID").Value + "']")
                    If objNodeList2.Count = 1 Then
                        'code comment by ashish
                        'If objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "TRUE" Then
                        objNode.Attributes("Qty").Value = objNodeList2.Count
                        'End If 'code comment by ashish
                    Else
                        objNode.Attributes("Qty").Value = objNodeList2.Count
                    End If

                    If (objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "FALSE") Or objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "" Then
                        objNode.Attributes("Qty").Value = ""
                    End If
                Next
            End If

            hdProductList.Value = objOutputXml.OuterXml
            If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                hdChallanDetails.Value = hdProductList.Value
            End If
        End If
    End Sub

    'Code to maintain sequence of xml as same as grid and fill the quantity
    Function ManageOrder(ByVal objOutputXml As XmlDocument) As XmlDocument
        Dim objtempXml As New XmlDocument
        Dim objProductIDQuantity As New XmlDocument
        Dim ar As New ArrayList
        ' Dim arGodownName As New ArrayList
        Dim objXmlNodeClone As XmlNode
        Dim intQty As Integer = 0


        objtempXml.LoadXml("<ROOT><CHALLAN><CHALLANDETAILS LineNumber=''	ProductID='' ProductName='' Qty='' SerialNumber='' VenderSerialNo='' MAINTAIN_BALANCE_BY ='' MAINTAIN_BALANCE='' EGROUP_CODE=''/></CHALLAN></ROOT>")
        objProductIDQuantity.LoadXml("<Root><Product ID='' Qty=''/></Root>")
        Dim objNodeList As XmlNodeList
        objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='']")
        For Each objNode As XmlNode In objNodeList
            objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNode)
        Next

        objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS")
        Dim objTempCloneNode, objTempNode As XmlNode
        objTempNode = objProductIDQuantity.DocumentElement.SelectSingleNode("Product")
        For Each objNode As XmlNode In objNodeList
            If ar.Contains(objNode.Attributes("ProductID").Value) Then

            Else
                ar.Add(objNode.Attributes("ProductID").Value)

                objTempCloneNode = objTempNode.CloneNode(True)
                objTempCloneNode.Attributes("ID").Value = objNode.Attributes("ProductID").Value
                objTempCloneNode.Attributes("Qty").Value = objNode.Attributes("Qty").Value
                objProductIDQuantity.DocumentElement.AppendChild(objTempCloneNode)
                'htProducTID_Quantity.Add(objNode.Attributes("ProductID").Value, objNode.Attributes("Qty").Value)
            End If
        Next
        objProductIDQuantity.DocumentElement.RemoveChild(objTempNode)
        hdProductIDANDQuantity.Value = objProductIDQuantity.OuterXml
        Dim LineNumber As Integer = 1
        For i As Integer = 0 To ar.Count - 1
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ar(i) + "']")
            For Each objNode As XmlNode In objNodeList
                objXmlNodeClone = objtempXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                With objXmlNodeClone
                    .Attributes("LineNumber").Value = LineNumber
                    .Attributes("ProductID").Value = objNode.Attributes("ProductID").Value
                    .Attributes("ProductName").Value = objNode.Attributes("ProductName").Value
                    .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                    .Attributes("SerialNumber").Value = objNode.Attributes("SerialNumber").Value
                    .Attributes("VenderSerialNo").Value = objNode.Attributes("VenderSerialNo").Value
                    .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                    .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                    .Attributes("EGROUP_CODE").Value = objNode.Attributes("EGROUP_CODE").Value
                    objtempXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                    LineNumber = LineNumber + 1
                End With
            Next
        Next

        'For i As Integer = 0 To ar.Count - 1
        '    objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ar(i) + "']")
        '    For Each objNode As XmlNode In objNodeList
        objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveAll()
        '     Next
        '  Next
        Dim objDocFrag As XmlDocumentFragment

        objDocFrag = objOutputXml.CreateDocumentFragment()
        objDocFrag.InnerXml = objtempXml.DocumentElement.SelectSingleNode("CHALLAN").InnerXml
        objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objDocFrag)

        Dim objNodeList1 As XmlNodeList
        objNodeList1 = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS")
        For Each objNode As XmlNode In objNodeList1
            Dim objNodeList2 As XmlNodeList
            objNodeList2 = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("ProductID").Value + "']")
            If objNodeList2.Count = 1 Then
                If objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "TRUE" Then
                    objNode.Attributes("Qty").Value = objNodeList2.Count
                End If
            Else '
                objNode.Attributes("Qty").Value = objNodeList2.Count
            End If
            If (objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "FALSE" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "FALSE") Or objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE_BY").Value.ToString.ToUpper = "" And objNodeList2.Item(0).Attributes("MAINTAIN_BALANCE").Value.ToString.ToUpper = "" Then
                objNode.Attributes("Qty").Value = ""
            End If
        Next

        Return objOutputXml

    End Function

    Sub BindProduct()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objNodeList As XmlNodeList
        Dim sGroupByColumns As String = "ProductID"
        Dim objDT As New DataTable
        Dim foundRows As DataRow()
        Dim IntCount As Integer = 0

        If hdProductList.Value <> "" Then
            objOutputXml.LoadXml(hdProductList.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNode)
            Next
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            If ds.Tables("CHALLANDETAILS") IsNot Nothing Then

                ' ''-------------------Neeraj
                'objDT = GroupByMultiple(sGroupByColumns, "Qty", ds.Tables("CHALLANDETAILS"))

                'For Each row As DataRow In objDT.Rows
                '    For Each rowORA As DataRow In ds.Tables("CHALLANDETAILS").Rows
                '        If row("ProductID").ToString() = rowORA("ProductID").ToString() Then
                '            ds.Tables("CHALLANDETAILS").Rows(IntCount)("Qty") = row("Count")
                '            IntCount = IntCount + 1
                '        End If
                '    Next
                'Next
                'ds.Tables("CHALLANDETAILS").AcceptChanges()

                ''foundRows = ds.Tables("CHALLANDETAILS").Select("ProductID = 185")
                ' ''------------------------

                Dim dv As DataView
                dv = ds.Tables("CHALLANDETAILS").DefaultView
                ' dv.Sort = "ProductName"

                gvProduct.DataSource = dv
                gvProduct.DataBind()

            Else
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
            End If
        ElseIf hdChallanDetails.Value <> "" Then

            objOutputXml.LoadXml(hdChallanDetails.Value)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNode)
            Next
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            If ds.Tables("CHALLANDETAILS") IsNot Nothing Then

                Dim dv As DataView
                dv = ds.Tables("CHALLANDETAILS").DefaultView
                gvProduct.DataSource = dv
                gvProduct.DataBind()
            Else
                gvProduct.DataSource = Nothing
                gvProduct.DataBind()
            End If
        End If

        'If chkReplacementChallan.Checked Then
        '    txtRplIssueChallanNo.Style.Add("display", "block")
        'End If
        If chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
            trInstalledPC.Style.Add("display", "block")
            txtRplIssueChallanNo.Style.Add("display", "none")
        ElseIf chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "2" Then
            txtRplIssueChallanNo.Style.Add("display", "block")
        End If

    End Sub

    Function GroupByMultiple(ByVal i_sGroupByColumns As String, ByVal i_sAggregateColumn As String, ByVal i_dSourceTable As DataTable) As DataTable

        Dim dv As New DataView(i_dSourceTable)
        Dim dtGroup As DataTable = dv.ToTable(True, i_sGroupByColumns)

        dtGroup.Columns.Add("Count", GetType(Integer))

        Dim sCondition As String
        For Each dr As DataRow In dtGroup.Rows
            sCondition = ""
            sCondition &= i_sGroupByColumns & " = '" & dr(i_sGroupByColumns) & "' "
            dr("Count") = i_dSourceTable.Compute("Count(" & i_sAggregateColumn & ")", sCondition)
        Next
        Return dtGroup
    End Function


    Sub ChallanCategory()
        ' ddlChallanCategory.Items.Insert(0, New ListItem("---Select One---", ""))
        ddlChallanCategory.Items.Clear()
        ddlChallanCategory.Items.Insert(0, New ListItem("Customer", "1"))
        ddlChallanCategory.Items.Insert(1, New ListItem("Purchase Order", "2"))
        ddlChallanCategory.Items.Insert(2, New ListItem("Stock Transfer", "4"))
        If hdPageStatus.Value = "U" Then
            ddlChallanCategory.Items.Insert(2, New ListItem("Replacement", "3"))
        End If
    End Sub
    'this function returns qunatity count of a particular EGROUP_CODE for replacement challan
    Function getQuantityCountCheck(ByVal objNodeList As XmlNodeList, ByVal objInstalledPCXML As XmlDocument, ByVal strEGROUP_CODE As String) As Boolean
        Dim blnResult As Boolean = False
        Dim objLoopNode As XmlNode
        Dim intProductCount, intInstalledPcCountSelected As Integer
        Dim arProductID As New ArrayList
        intProductCount = 0 = intInstalledPcCountSelected = 0
        Try
            For Each objLoopNode In objNodeList
                If Not (objLoopNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objLoopNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then
                    If Val(objLoopNode.Attributes("Qty").Value) <> 0 Then
                        If Not arProductID.Contains(objLoopNode.Attributes("ProductID").Value) Then
                            intProductCount += Val(objLoopNode.Attributes("Qty").Value)
                            arProductID.Add(objLoopNode.Attributes("ProductID").Value)
                        End If
                    End If
                End If
            Next
            intInstalledPcCountSelected = Val(objInstalledPCXML.DocumentElement.SelectNodes("PCINSTALLATION[@" & strEGROUP_CODE & "='TRUE']").Count)
            If intProductCount > intInstalledPcCountSelected Then
                blnResult = False
            Else
                blnResult = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return blnResult
    End Function

    'this function returns qunatity count of a particular EGROUP_CODE
    Function getQuantityCountCheck(ByVal objNodeList As XmlNodeList) As Boolean
        Dim blnResult As Boolean = False
        Dim objLoopNode As XmlNode
        Dim intProductCount, cnQuantityCount As Integer
        Dim arProductID As New ArrayList
        intProductCount = 0 = cnQuantityCount = 0
        Try
            For Each objLoopNode In objNodeList
                If Not (objLoopNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objLoopNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then
                    If Val(objLoopNode.Attributes("Qty").Value) <> 0 Then
                        If Not arProductID.Contains(objLoopNode.Attributes("ProductID").Value) Then
                            intProductCount += Val(objLoopNode.Attributes("Qty").Value)
                            arProductID.Add(objLoopNode.Attributes("ProductID").Value)
                        End If
                    End If
                End If
            Next
            'If intProductCount > 0 Then
            '    intProductCount += Val(txtQuantity.Text)
            'End If

            cnQuantityCount = Convert.ToInt16(Val(hdOrderQuantity.Value.Split("|").GetValue(1) & ""))
            If intProductCount > cnQuantityCount Then
                blnResult = False
            Else
                blnResult = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
        Return blnResult
    End Function

    'this function check the quantity added against the product selected for replacemement
    Function ValidateAddedReplacementProduct(ByVal objProductXml As XmlDocument) As Boolean
        Dim blnResult As Boolean = False
        Dim objInstalledPC As New XmlDocument
        Dim objNodeList As XmlNodeList
        Try
            If hdInstalledPCXML.Value <> "" Then
                objInstalledPC.LoadXml(hdInstalledPCXML.Value)
                'for CPU and LAP
                objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='CPU' or @EGROUP_CODE='LAP']")
                If getQuantityCountCheck(objNodeList, objInstalledPC, "CPUTYPECHECK") Then
                    blnResult = True
                Else
                    blnResult = False
                    GoTo Result
                End If
                'end

                'for MON and TFT
                objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='MON' or @EGROUP_CODE='TFT']")
                If getQuantityCountCheck(objNodeList, objInstalledPC, "MONTYPECHECK") Then
                    blnResult = True
                Else
                    blnResult = False
                    GoTo Result
                End If
                'end

                'for KBD
                objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='KBD']")
                If getQuantityCountCheck(objNodeList, objInstalledPC, "KBDTYPECHECK") Then
                    blnResult = True
                Else
                    blnResult = False
                    GoTo Result
                End If
                'end

                'for MSE
                objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='MSE']")
                If getQuantityCountCheck(objNodeList, objInstalledPC, "MSETYPECHECK") Then
                    blnResult = True
                Else
                    blnResult = False
                    GoTo Result
                End If
                'end
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            blnResult = False
        End Try
Result:
        Return blnResult
    End Function

    ' this function check the added product exists into selected products from replacement
    Function ValidateAddedReplacementProduct(ByVal objProductXml As XmlDocument, ByVal boolReplacement As Boolean) As Boolean
        Dim blnResult As Boolean = False
        Dim objInstalledPC As New XmlDocument
        Dim strEGROUP_CODE_CHECK As String = String.Empty
        Dim intInstalledPcCountSelected As Integer

        Try
            If boolReplacement = True Then
                If hdInstalledPCXML.Value <> "" Then
                    objInstalledPC.LoadXml(hdInstalledPCXML.Value)
                    strEGROUP_CODE_CHECK = ddlProduct.SelectedValue.Split("|").GetValue(3).ToString.ToUpper
                    strEGROUP_CODE_CHECK = IIf(strEGROUP_CODE_CHECK = "LAP", "CPU", strEGROUP_CODE_CHECK)
                    strEGROUP_CODE_CHECK = IIf(strEGROUP_CODE_CHECK = "TFT", "MON", strEGROUP_CODE_CHECK)
                    strEGROUP_CODE_CHECK = strEGROUP_CODE_CHECK & "TYPECHECK"
                    intInstalledPcCountSelected = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@" & strEGROUP_CODE_CHECK & "]").Count)
                    If intInstalledPcCountSelected = 0 Then
                        blnResult = False
                    Else
                        blnResult = True
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            blnResult = False
        End Try

        Return blnResult
    End Function

    'this function check the quantity added against the product selected 
    Function ValidateAddedProduct(ByVal objProductXml As XmlDocument) As Boolean
        Dim blnResult As Boolean = False
        Dim objNodeList As XmlNodeList
        Try

            'for CPU and LAP
            objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='CPU' or @EGROUP_CODE='LAP']")
            If getQuantityCountCheck(objNodeList) Then
                blnResult = True
            Else
                blnResult = False
                GoTo Result
            End If
            'end

            'for MON and TFT
            objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='MON' or @EGROUP_CODE='TFT']")
            If getQuantityCountCheck(objNodeList) Then
                blnResult = True
            Else
                blnResult = False
                GoTo Result
            End If
            'end

            'for KBD
            objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='KBD']")
            If getQuantityCountCheck(objNodeList) Then
                blnResult = True
            Else
                blnResult = False
                GoTo Result
            End If
            'end

            'for MSE
            objNodeList = objProductXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@EGROUP_CODE='MSE']")
            If getQuantityCountCheck(objNodeList) Then
                blnResult = True
            Else
                blnResult = False
                GoTo Result
            End If
            'end


        Catch ex As Exception
            lblError.Text = ex.Message
            blnResult = False
        End Try
Result:
        Return blnResult
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objTempInputXml, objInstalledPcXML, objOutputXml As New XmlDocument
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objDocFrag As XmlDocumentFragment
        Dim objNodeList As XmlNodeList
        Dim intInstalledPcCountSelected As Integer
        Dim intPcCountAdded As Integer
        Dim objInstalledPC As New XmlDocument
        Dim objArray As New Hashtable

        Try
            '<CHALLAN Reference=''  RequestBy='' RequestDate=''  ApprovedBy='' ApprovalDate='' =''>
            '<CHALLANDETAILS	ProductID='' Qty='' SerialNumber='' VenderSerialNo='' />	

            objTempInputXml.LoadXml(hdProductList.Value)

            If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                If ValidateAddedReplacementProduct(objTempInputXml) = False Then
                    lblError.Text = "Product added exceeds the product selected to replace"
                    Exit Sub
                End If
            End If

            'Validation to check no. of products selected from replacement should be equal in added product list---  Added by Neeraj 
            If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                If hdInstalledPCXML.Value <> "" Then
                    objInstalledPC.LoadXml(hdInstalledPCXML.Value)

                    objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='' and @EGROUP_CODE='LAP']")
                    If objNodeList.Count >= 1 Then 'IN CASE WHEN LAP (LAPTOP)  TO LAP (LAPTOP) IS REPLACED 
                        objArray.Add("CPU", 0)
                        objArray.Add("LAP", 0)
                        objArray.Add("MON", 0)
                        objArray.Add("TFT", 0)
                        objArray.Add("KBD", 0)
                        objArray.Add("MSE", 0)

                        objArray("CPU") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='CPU']").Count)
                        objArray("LAP") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='LAP']").Count)
                        objArray("MON") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                        objArray("TFT") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                        objArray("KBD") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                        objArray("MSE") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                        '*'WRITE LOGIC FOR LAPTOP.....
                        objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                        For Each objNode As XmlNode In objNodeList
                            If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then

                                If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP" Then
                                    If objArray("LAP") = 0 Then 'if LAP not exists in replacemnt list than [1(one) CPU + 1(one) MON + 1(one) KBD + 1(one) MSE]  =  1(one) LAp (Laptop)
                                        If objArray("CPU") <> 0 Then
                                            objArray("CPU") = objArray("CPU") - 1
                                        Else
                                            lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select CPU"
                                            Exit Sub
                                        End If
                                        If objArray("MON") <> 0 Then
                                            objArray("MON") = objArray("MON") - 1
                                        Else
                                            lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Monitor"
                                            Exit Sub
                                        End If
                                        If objArray("KBD") <> 0 Then
                                            objArray("KBD") = objArray("KBD") - 1
                                        Else
                                            lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select keyboard"
                                            Exit Sub
                                        End If
                                        If objArray("MSE") <> 0 Then
                                            objArray("MSE") = objArray("MSE") - 1
                                        Else
                                            lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Mouse"
                                            Exit Sub
                                        End If
                                    Else
                                        objArray("LAP") = objArray("LAP") - 1
                                    End If
                                ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU" Then
                                    If objArray("CPU") <> 0 Then
                                        objArray("CPU") = objArray("CPU") - 1
                                    Else
                                        lblError.Text = "Product added is not equal to product selected to replace"
                                        Exit Sub
                                    End If
                                ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON" Then
                                    If objArray("MON") <> 0 Then
                                        objArray("MON") = objArray("MON") - 1
                                    Else
                                        lblError.Text = "Product added is not equal to product selected to replace"
                                        Exit Sub
                                    End If
                                ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT" Then
                                    If objArray("TFT") <> 0 Then
                                        objArray("TFT") = objArray("TFT") - 1
                                    Else
                                        lblError.Text = "Product added is not equal to product selected to replace"
                                        Exit Sub
                                    End If
                                ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                    If objArray("KBD") <> 0 Then
                                        objArray("KBD") = objArray("KBD") - 1
                                    Else
                                        lblError.Text = "Product added is not equal to product selected to replace"
                                        Exit Sub
                                    End If
                                ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Then
                                    If objArray("MSE") <> 0 Then
                                        objArray("MSE") = objArray("MSE") - 1
                                    Else
                                        lblError.Text = "Product added is not equal to product selected to replace"
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next
                    Else 'IN CASE WHEN LAP TO LAP NOT REPLACED
                        'TOTAL COUNT FOR CPU , LAP , MON , KBD , MSE
                        intInstalledPcCountSelected = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE']").Count)
                        intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@LAPTYPECHECK='TRUE']").Count)
                        intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                        intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                        intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                        intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                        objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                        For Each objNode As XmlNode In objNodeList
                            If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then
                                If (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON") Then
                                    intPcCountAdded = intPcCountAdded + 1
                                End If
                                If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Or objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                    intPcCountAdded = intPcCountAdded + 1 'Val(objNode.Attributes("Qty").InnerText & "")
                                End If
                            End If
                        Next
                        If intPcCountAdded <> intInstalledPcCountSelected Then
                            lblError.Text = "Product added is not equal to product selected to replace"
                            Exit Sub
                        End If
                    End If

                End If
            End If

            BindXml()
            ManageOrder()

            objTempInputXml.LoadXml(hdProductList.Value)
            objInputXml.LoadXml("<INV_UPDATE_CHALLAN_INPUT><CHALLAN ChallanType='' ChallanCategory='' ChallanID='' ChallanNumber ='' CreationDate=''  POID='' Lcode='' GodownID='' RGodownID ='' LoggedBy='' ExecutionDate='' RecieveBY='' ChallanDate=''  Reference=''  RequestBy='' RequestDate='' Replacement_Challan = '' Mischardware_Challan=''  ApprovedBy='' ApprovalDate='' Notes='' Order_Number='' Replacement_challanNumber =''><CHALLANDETAILS	ProductID='' Qty='' SerialNumber='' VenderSerialNo='' MAINTAIN_BALANCE_BY='' MAINTAIN_BALANCE=''  /></CHALLAN><REPLACEMENT /></INV_UPDATE_CHALLAN_INPUT>")
            objDocFrag = objInputXml.CreateDocumentFragment()

            objDocFrag.InnerXml = objTempInputXml.DocumentElement.SelectSingleNode("CHALLAN").InnerXml
            objInputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objDocFrag)

            'fill installedpc selected data
            If hdInstalledPCXML.Value <> "" Then
                objInstalledPcXML.LoadXml(hdInstalledPCXML.Value)
                For Each objNodeXml As XmlNode In objInstalledPcXML.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' or @MONTYPECHECK='TRUE' or @KBDTYPECHECK ='TRUE' or @MSETYPECHECK='TRUE']")
                    objInputXml.DocumentElement.SelectSingleNode("REPLACEMENT").AppendChild(objInputXml.ImportNode(objNodeXml, False))
                Next
            End If
            objNodeList = objInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='']")
            For Each objNode As XmlNode In objNodeList
                objInputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objNode)
            Next

            With objInputXml.DocumentElement.SelectSingleNode("CHALLAN")
                .Attributes("ChallanType").Value = ddlChallanType.SelectedValue
                .Attributes("ChallanCategory").Value = ddlChallanCategory.SelectedValue
                .Attributes("ChallanNumber").Value = txtChallanNo.Text
                '.Attributes("CreationDate").Value = txtCreationDate.Text
                .Attributes("POID").Value = txtPurchaseOrder.Text
                .Attributes("Lcode").Value = hdChallanLCode.Value
                .Attributes("GodownID").Value = ddlGodown.SelectedValue
                .Attributes("RGodownID").Value = hdChallanGodownId.Value

                .Attributes("LoggedBy").Value = Session("LoginSession").ToString().Split("|")(0)
                '.Attributes("ExecutionDate").Value = txtExecutionDate.Text
                .Attributes("ChallanDate").Value = objeAAMS.GetDateFormat(txtChallanDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                If ddlChallanCategory.SelectedValue = "4" Then
                    .Attributes("RecieveBY").Value = txtChallanReceivedBy.Text + "|" + hdChallanReceivedBy.Value
                Else
                    .Attributes("RecieveBY").Value = txtChallanReceivedBy.Text
                End If

                .Attributes("Reference").Value = txtIssueChallanNo.Text
                .Attributes("RequestBy").Value = hdChallanRequestedBy.Value
                .Attributes("RequestDate").Value = objeAAMS.GetDateFormat(txtRequestedDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("Replacement_Challan").Value = chkReplacementChallan.Checked.ToString
                .Attributes("Replacement_challanNumber").Value = txtRplIssueChallanNo.Text
                .Attributes("Mischardware_Challan").Value = chkMiscChallan.Checked.ToString
                .Attributes("ApprovedBy").Value = hdChallanApprovedBy.Value
                .Attributes("ApprovalDate").Value = objeAAMS.GetDateFormat(txtApprovedDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("Notes").Value = txtNote.Text
                If hdChallanID.Value <> "" Then
                    .Attributes("ChallanID").Value = hdChallanID.Value
                End If

                If ddlChallanCategory.SelectedValue = "1" Then
                    If chkOrderAmadeusIndia.Checked = True Then
                        .Attributes("Order_Number").Value = "00/00/00" 'Added on 13 Aug 08
                    Else
                        'If chkReplacementChallan.Checked = False And chkMiscChallan.Checked = False Then 'commented on 5 nov 09
                        If chkMiscChallan.Checked = False Then 'added on 5 nov 09
                            If hdOrderQuantity.Value <> "" Then
                                .Attributes("Order_Number").Value = hdOrderQuantity.Value.Split("|").GetValue(0)
                            End If
                        End If
                    End If
                End If

            End With

            'Here Back end Method Call
            objOutputXml = objbzChallan.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If hdChallanID.Value = "" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    hdChallanID.Value = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanID").Value
                    hdEnChallanID.Value = objED.Encrypt(hdChallanID.Value)
                    hdPageStatus.Value = "U"
                Else
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                hdTabType.Value = "0"
                ddlProduct.SelectedIndex = 0
                txtQuantity.Text = ""
                ChallanCategory()
                ViewRecords()
                checkSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                BindProduct()
            End If

            If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                trInstalledPC.Style.Add("display", "none")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim objXmlInput As New XmlDocument
        Dim objXmlOutPut As New XmlDocument
        Dim objInventory As New AAMS.bizInventory.bzChallan
        Dim strEmpID As String = ""
        Dim strChallanID As String = ""
        Try
            If Session("LoginSession") Is Nothing Then
                Exit Sub
            End If
            strEmpID = Session("LoginSession").Split("|").GetValue(0).ToString()
            strChallanID = hdChallanID.Value
            If ddlChallanCategory.SelectedValue = "1" Then
                ' Code for Customer.
                objXmlInput.LoadXml("<INV_RPT_CUSTOMER_CHALLAN_INPUT><ChallanID></ChallanID><EmployeeID></EmployeeID></INV_RPT_CUSTOMER_CHALLAN_INPUT>")
                objXmlInput.DocumentElement.SelectSingleNode("ChallanID").InnerText = strChallanID
                objXmlInput.DocumentElement.SelectSingleNode("EmployeeID").InnerText = strEmpID
                objXmlOutPut = objInventory.Rpt_Customer_Challan(objXmlInput)
                If objXmlOutPut.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("CustomerReport") = objXmlOutPut.OuterXml
                    If ddlChallanType.SelectedValue = "1" Then
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('../RPSR_ReportShow.aspx?Case=CustomerIssue','CustomerIssue','height=600,width=820,top=30,left=20,scrollbars=1'); </script>")
                    ElseIf ddlChallanType.SelectedValue = "2" Then
                        ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('../RPSR_ReportShow.aspx?Case=CustomerReceive','CustomerReceive','height=600,width=820,top=30,left=20,scrollbars=1'); </script>")
                    End If
                    ' ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('../RPSR_ReportShow.aspx?Case=InventoryChallanReportCustomer','challanRpt','height=600,width=820,top=30,left=20,scrollbars=1'); window.open('/AAMS/RPSR_ReportShow.aspx?Case=InventoryChallanReportCustomerAgent','challanRptAgent','height=600,width=820,top=30,left=20,scrollbars=1'); window.open('/AAMS/RPSR_ReportShow.aspx?Case=InventoryChallanReportCustomerDeInstall','challanRptDeInstall','height=600,width=820,top=30,left=20,scrollbars=1'); window.open('/AAMS/RPSR_ReportShow.aspx?Case=InventoryChallanReportCustomerDelivery','challanRptDelivery','height=600,width=820,top=30,left=20,scrollbars=1'); </script>")
                Else
                    lblError.Text = objXmlOutPut.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            ElseIf ddlChallanCategory.SelectedValue = "2" Then

                ' Code for Purchase order.

                objXmlInput.LoadXml("<INV_RPT_PURCHASEORDER_CHALLAN_INPUT><ChallanID></ChallanID><EmployeeID></EmployeeID></INV_RPT_PURCHASEORDER_CHALLAN_INPUT>")
                objXmlInput.DocumentElement.SelectSingleNode("ChallanID").InnerText = strChallanID
                objXmlInput.DocumentElement.SelectSingleNode("EmployeeID").InnerText = strEmpID
                objXmlOutPut = objInventory.Rpt_Purchase_Challan(objXmlInput)
                If objXmlOutPut.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("PurchaseOrderRpt") = objXmlOutPut.OuterXml
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('../RPSR_ReportShow.aspx?Case=InventoryChallanReport','challanRpt','height=600,width=820,top=30,left=20,scrollbars=1');  </script>")
                Else
                    lblError.Text = objXmlOutPut.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            ElseIf ddlChallanCategory.SelectedValue = "4" Then

                ' code for stock transfer
                objXmlInput.LoadXml("<INV_RPT_STOCKTRANSFER_CHALLAN_INPUT><ChallanID></ChallanID><EmployeeID></EmployeeID></INV_RPT_STOCKTRANSFER_CHALLAN_INPUT>")
                objXmlInput.DocumentElement.SelectSingleNode("ChallanID").InnerText = strChallanID
                objXmlInput.DocumentElement.SelectSingleNode("EmployeeID").InnerText = strEmpID
                objXmlOutPut = objInventory.Rpt_StockTransfer(objXmlInput)
                If objXmlOutPut.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("StockTransferRpt") = objXmlOutPut.OuterXml
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('../RPSR_ReportShow.aspx?Case=InvStockTransferReport','stockTransferRpt','height=600,width=820,top=30,left=20,scrollbars=1');  </script>")
                Else
                    lblError.Text = objXmlOutPut.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            BindProduct()
            'For Each ctrl As Control In Page.Controls'commented on 13 aug 08
            'ChangeControlStatus(ctrl)
            ' Next'commented on 13 aug 08
            'this code is changed because previously it disabled all controls without checking challan is executed or not
            'once the challna is executed no need to disable all controls just we have disable gvproduct controls only
            If txtExecutionDate.Text <> "" Then 'Added on 13 aug
                ChangeControlStatus(gvProduct) 'Added on 13 aug
            End If 'Added on 13 aug

        End Try

    End Sub

    Protected Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        Dim objInputXml, objOutputXml, objInstalledPcXML As New XmlDocument
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim blnValidateCheckCP As Boolean
        Try
            blnValidateCheckCP = ValidateCheckedPCandPCInstalled() ' Validation to check PC checkd equal to PC Installed
            If blnValidateCheckCP = False Then
                Exit Sub
            End If


            objInputXml.LoadXml("<INV_CHALLAN_EXECUTE_INPUT><ChallanID></ChallanID> <ChallanType></ChallanType> <ChallanCategory></ChallanCategory>   <GodownID></GodownID> <RGodownID></RGodownID>  <ExecutionDate></ExecutionDate><REPLACEMENT /></INV_CHALLAN_EXECUTE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ChallanID").InnerText = hdChallanID.Value
            objInputXml.DocumentElement.SelectSingleNode("ChallanType").InnerText = ddlChallanType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = ddlChallanCategory.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ddlGodown.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("RGodownID").InnerText = hdChallanGodownId.Value
            objInputXml.DocumentElement.SelectSingleNode("ExecutionDate").InnerText = txtExecutionDate.Text

            'fill installedpc selected data
            If hdInstalledPCXML.Value <> "" Then
                objInstalledPcXML.LoadXml(hdInstalledPCXML.Value)
                For Each objNodeXml As XmlNode In objInstalledPcXML.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' or @MONTYPECHECK='TRUE' or @KBDTYPECHECK ='TRUE' or @MSETYPECHECK='TRUE']")
                    objInputXml.DocumentElement.SelectSingleNode("REPLACEMENT").AppendChild(objInputXml.ImportNode(objNodeXml, False))
                Next
            End If

            'Here Back end Method Call
            objOutputXml = objbzChallan.Execute(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                txtChallanNo.Text = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanNumber").Value
                txtExecutionDate.Text = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ExecutionDate").Value
                ddlGodown.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("GodownID").Value
                hdProductList.Value = objOutputXml.OuterXml
                lblError.Text = "Challan executed successfully"
                ManageOrder()
                BindProduct()
                If ddlChallanCategory.SelectedValue = "2" And ddlChallanType.SelectedValue = "2" And txtExecutionDate.Text.Trim <> "" Then
                    btnPrintLabel.Enabled = True
                Else
                    btnPrintLabel.Enabled = False
                End If
                For Each ctrl As Control In Page.Controls
                    ChangeControlStatus(ctrl)
                Next
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                ManageOrder()
                BindProduct()
            End If
            If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                trInstalledPC.Style.Add("display", "none")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString

        Finally

        End Try

    End Sub


    Sub ChangeControlStatus(ByVal c As Control)
        If c.Controls.Count > 0 Then
            For Each ctrl As Control In c.Controls
                ChangeControlStatus(ctrl)
            Next
        End If


        If TypeOf c Is TextBox Then
            DirectCast(c, TextBox).ReadOnly = True
        ElseIf TypeOf c Is Button Then
            If DirectCast(c, Button).ID = "btnPrintLabel" Or DirectCast(c, Button).ID = "btnPrint" Or DirectCast(c, Button).Text = "Details" Or DirectCast(c, Button).Text = "Product List" Or DirectCast(c, Button).Text = "Notes" Or DirectCast(c, Button).ID = "btnNew" Or DirectCast(c, Button).ID = "btnModifyChallan" Then
            Else
                DirectCast(c, Button).Enabled = False
            End If
        ElseIf TypeOf c Is RadioButton Then
            DirectCast(c, RadioButton).Enabled = False
        ElseIf TypeOf c Is ImageButton Then
            DirectCast(c, ImageButton).Enabled = False
        ElseIf TypeOf c Is CheckBox Then
            DirectCast(c, CheckBox).Enabled = False
        ElseIf TypeOf c Is DropDownList Then
            DirectCast(c, DropDownList).Enabled = False
        ElseIf TypeOf c Is HyperLink Then
            DirectCast(c, HyperLink).Enabled = False
        End If
    End Sub

    'Code to fill product grid when challan category is purchase order
    Sub FillProductList()
        Dim objInputOrderXml, objOutputOrderXml As New XmlDocument
        Dim objbzPurchaseOrder As New AAMS.bizInventory.bzPurchaseOrder
        Dim objOutputXml As New XmlDocument
        Dim intQty As Integer = 0
        objInputOrderXml.LoadXml("<INV_VIEW_PURCHASEORDER_INPUT><PurchaseOrderID></PurchaseOrderID></INV_VIEW_PURCHASEORDER_INPUT>")
        objInputOrderXml.DocumentElement.SelectSingleNode("PurchaseOrderID").InnerText = hdPurchaseOrder.Value
        hdPurchaseOrder.Value = ""
        objOutputOrderXml = objbzPurchaseOrder.ViewChallanProducts(objInputOrderXml)
        '      <PURCHASEORDER PurchaseOrderID="997" SupplierID="2" SupplierName="V.M.ENTERPRISES PVT.LTD" Address="40/57,CHITTARANJAN PARK,NEW DELHI" OrderDate="20050408" CreationDate="04/08/2005 16:41" LateDeliveryP="" LatePaymentP="" PaymentTerms="after delivery" PODescription="" EmployeeID="24" Employee_Name="Admin" Notes="" ApprovedBy="270" ApprovedByName="Sandeep Kumar" ApprovalDate="200548" TOTALCOST="10000.00" TAX="" NETCOST="10000.00">
        '<PURCHASEORDERDETAILS PRODUCTID="69" PRODUCTNAME="CPL- LAPTOP AGENT" Qty="2" Rate="5000.00" Tax="" Warranty="1" WarrantyOnSite="True" DeliveryDate="20050804" GodownID="20" GodownName="BANGALORE GODOWN" /> 
        '</PURCHASEORDER>
        If objOutputOrderXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            Dim objXmlNodeClone As XmlNode
            Dim objNodeList As XmlNodeList
            objOutputXml.LoadXml("<Root><CHALLAN >	<CHALLANDETAILS LineNumber=''	ProductID='' ProductName='' Qty='' SerialNumber='' VenderSerialNo='' MAINTAIN_BALANCE_BY ='' MAINTAIN_BALANCE='' EGROUP_CODE='' /></CHALLAN></Root>")
            objNodeList = objOutputOrderXml.DocumentElement.SelectNodes("PURCHASEORDER/PURCHASEORDERDETAILS")
            If objNodeList.Count > 0 Then
                Dim cn As Integer = 1
                For Each objNode As XmlNode In objNodeList
                    If objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToLower = "false" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToLower = "false" Then
                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("PRODUCTID").Value + "']") IsNot Nothing Then
                            objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                            With objXmlNodeClone
                                .Attributes("LineNumber").Value = cn.ToString
                                .Attributes("ProductID").Value = objNode.Attributes("PRODUCTID").Value
                                .Attributes("ProductName").Value = objNode.Attributes("PRODUCTNAME").Value
                                .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                                .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                                .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                cn = cn + 1
                            End With
                        End If
                    ElseIf (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToLower = "true" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToLower = "true") Or (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToLower = "true" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToLower = "false") Then
                        'this condtions for quantity (checks when qty is greater than zero than  only we add into the product list
                        If Val(objNode.Attributes("Qty").Value) > 0 Then
                            If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("PRODUCTID").Value + "']") IsNot Nothing Then
                                intQty = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("PRODUCTID").Value + "']").Attributes("Qty").InnerText
                                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + objNode.Attributes("PRODUCTID").Value + "']").Attributes("Qty").InnerText = Val(objNode.Attributes("Qty").Value) + intQty
                                intQty = 0
                            Else
                                objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                                With objXmlNodeClone
                                    .Attributes("LineNumber").Value = cn.ToString
                                    .Attributes("ProductID").Value = objNode.Attributes("PRODUCTID").Value
                                    .Attributes("ProductName").Value = objNode.Attributes("PRODUCTNAME").Value
                                    .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                                    .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                                    .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                                    objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                    cn = cn + 1
                                End With
                            End If
                        End If
                    Else

                        'this condtions for quantity (checks when qty is greater than zero than  only we add into the product list
                        If Val(objNode.Attributes("Qty").Value) > 0 Then
                            objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                            With objXmlNodeClone
                                .Attributes("LineNumber").Value = cn.ToString
                                .Attributes("ProductID").Value = objNode.Attributes("PRODUCTID").Value
                                .Attributes("ProductName").Value = objNode.Attributes("PRODUCTNAME").Value
                                .Attributes("Qty").Value = objNode.Attributes("Qty").Value
                                .Attributes("MAINTAIN_BALANCE_BY").Value = objNode.Attributes("MAINTAIN_BALANCE_BY").Value
                                .Attributes("MAINTAIN_BALANCE").Value = objNode.Attributes("MAINTAIN_BALANCE").Value
                                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                                cn = cn + 1
                            End With
                        End If
                    End If

                Next
                hdProductList.Value = objOutputXml.OuterXml
                If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                    hdChallanDetails.Value = hdProductList.Value
                End If
                ddlChallanType.Focus()
                ' ManageOrder()
                ' BindProduct()
            Else
                lblError.Text = "No Product against this purchase order left"
            End If

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            ' hdProductList.Value = objOutputXml.OuterXml
        End If


    End Sub

    'Code to fill godown dropdown when challan category is customer and agency is selected from popup
    'And Bydefault(and other than customer) is filled with aoffice rather than city 
    'In case when challan category is stock transfer ddlGodown is used as  godown from which 
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
                    ddlGodown.Items.Insert(0, New ListItem("--Select One--", ""))
                    ddlGodown.SelectedIndex = 0
                    If ddlGodown.Items.Count >= 1 And txtAgencyName.Text <> "" And ddlGodown.SelectedValue = "" Then
                        ' ddlGodown.SelectedIndex = 0

                        Dim objInputXml1 As New XmlDocument
                        Dim objOutputXml1 As New XmlDocument
                        objInputXml1.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                        objInputXml1.DocumentElement.SelectSingleNode("CITYNAME").InnerText = Request.Form("txtCity")
                        objOutputXml1 = objbzGodown.ListGodownAoffice(objInputXml1)
                        If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            If objOutputXml1.DocumentElement.SelectSingleNode("GODOWN") IsNot Nothing Then
                                Dim li As New ListItem
                                li = ddlGodown.Items.FindByValue(objOutputXml1.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value)
                                If li IsNot Nothing Then
                                    ddlGodown.SelectedValue = objOutputXml1.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value
                                End If
                            End If

                        End If
                    End If
                Else
                    ddlGodown.Items.Insert(0, New ListItem("--Select One--", ""))
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                'ddlGodown.Items.Insert(0, New ListItem("--Select One--", ""))
            End Try
        Else
            objeAAMS.BindDropDown(ddlGodown, "GODOWN", True, 1)
            If ddlGodown.Items.Count >= 1 And txtAgencyName.Text <> "" And ddlGodown.SelectedValue = "" Then
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                objInputXml.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = Request.Form("txtCity")
                objOutputXml = objbzGodown.ListGodownAoffice(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("GODOWN") IsNot Nothing Then
                        ddlGodown.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value
                    End If

                End If
            Else
                '   ddlGodown.SelectedIndex = 1
            End If
        End If


    End Sub

    'Code to fill order grid when challan category is customer and agency is selected from popup
    Sub FillOrder()
        If ddlChallanCategory.SelectedValue = "1" Then
            Try
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<INV_AGENCYPENGINDORDER_INPUT><LCODE/><CHALLANCATEGORY /><CHALLANTYPE /></INV_AGENCYPENGINDORDER_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdChallanLCodeTemp.Value
                objInputXml.DocumentElement.SelectSingleNode("CHALLANCATEGORY").InnerText = ddlChallanCategory.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = ddlChallanType.SelectedValue
                objOutputXml = objbzChallan.GetAgencyPendingOrder(objInputXml)
                'objOutputXml.LoadXml("<INV_AGENCYPENGINDORDER_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors><AGENCYORDER ORDER_NUMBER='2008/2/366' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /><AGENCYORDER ORDER_NUMBER='2008/2/367' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /></INV_AGENCYPENGINDORDER_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvOrder.DataSource = ds.Tables("AGENCYORDER").DefaultView
                    gvOrder.DataBind()
                    hdOrderList.Value = objOutputXml.OuterXml
                Else
                    gvOrder.DataSource = Nothing
                    gvOrder.DataBind()
                    hdOrderList.Value = ""
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                gvOrder.DataSource = Nothing
                gvOrder.DataBind()
                hdOrderList.Value = ""
                lblError.Text = ex.Message
            Finally
                hdChallanLCodeTemp.Value = ""
            End Try
        End If
    End Sub

    'Code to fill installed item in grid when challan category is customer and agency is selected from popup
    'this gird will be shown when user selects replacement challan checkbox
    Sub FillInstalledPC()
        If ddlChallanCategory.SelectedValue = "1" Then
            Try
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                '<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE></MS_GETPCINSTALLATION_INPUT>

                'Output :
                '<MS_GETPCINSTALLATION_OUTPUT>
                ' <PCINSTALLATION ROWID='' LCODE='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='' REMARKS='' CHALLANDATE='' CHALLANNUMBER='' LoggedBy='' Employee_Name='' LoggedDateTime='' CHALLANSTATUS='' CDRNO='' LastModifiedDate ='' />
                ' <TOTAL A1PC='' AGENCYPC='' />
                ' <Errors Status=''>
                ' <Error Code='' Description='' />
                ' </Errors>
                '</MS_GETPCINSTALLATION_OUTPUT>

                objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE><CHALLANNUMBER></CHALLANNUMBER></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdChallanLCodeTemp.Value
                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                objOutputXml = objbzChallan.GetInstalledPC(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvInstalledPC.DataSource = ds.Tables("PCINSTALLATION").DefaultView
                    gvInstalledPC.DataBind()
                    hdInstalledPCXML.Value = objOutputXml.OuterXml
                Else
                    gvInstalledPC.DataSource = Nothing
                    gvInstalledPC.DataBind()
                    hdInstalledPCXML.Value = ""
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                gvInstalledPC.DataSource = Nothing
                gvInstalledPC.DataBind()
                hdInstalledPCXML.Value = ""
                lblError.Text = ex.Message
            Finally

            End Try
        End If
    End Sub


    Sub GetConfigValue()
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            objOutputXml = objbzChallan.GetChallanConfig()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '<CHALLANPRODUCTITEMNO FIELD_NAME="CHALLAN PRODUCT ITEM" FIELD_VALUE="7" /> 
                '  <CHALLANPRODUCTITEMNO FIELD_NAME="CHALLAN ITEM" FIELD_VALUE="7" /> 
                Dim objNodeList As XmlNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLANPRODUCTITEMNO")
                For Each objNode As XmlNode In objNodeList
                    If objNode.Attributes("FIELD_NAME").Value = "CHALLAN PRODUCT ITEM" Then
                        hdProductCount.Value = objNode.Attributes("FIELD_VALUE").Value
                    End If
                    If objNode.Attributes("FIELD_NAME").Value = "CHALLAN ITEM" Then
                        hdQuantity.Value = objNode.Attributes("FIELD_VALUE").Value
                    End If
                    If objNode.Attributes("FIELD_NAME").Value = "ORDER_TYPE_NAME" Then
                        hdOrderTypeValue.Value = objNode.Attributes("FIELD_VALUE").Value
                    End If

                Next
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

            hdMandatoryOrder.Value = objeAAMS.OVERRIDE_ORDER_NO(Session("Security"))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Function GetProductCount() As Boolean
        Try
            Dim status As Boolean = True
            Dim ProductCount As Integer = Convert.ToInt32(hdProductCount.Value)
            Dim QuantityCount As Integer = Convert.ToInt32(hdQuantity.Value)
            Dim ar As New ArrayList
            Dim objOutputXml As New XmlDocument
            If hdProductList.Value <> "" Then
                objOutputXml.LoadXml(hdProductList.Value)
                Dim objNodeList As XmlNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID != '']")
                For Each objNode As XmlNode In objNodeList
                    'Commented on 18 Sep 08
                    'previously product count check enabled for MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="True"
                    'Now product count  enabled for all cases
                    ' If objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "TRUE" Then
                    If ar.Contains(objNode.Attributes("ProductID").Value) Then
                    Else
                        ar.Add(objNode.Attributes("ProductID").Value)
                    End If
                    'End If
                Next
                If ar.Count >= ProductCount Then
                    status = False
                End If
            End If
            Return status
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
    'Function Overloading used
    Function GetQuantityCount() As Boolean
        Try
            Dim status As Boolean = True
            'Commented on 18 Sep 08
            'previously quantity check disabled for MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="FALSE"
            'Now quantity check enaled only when MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="TRUE"
            '  If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
            If Not (ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "TRUE") Then

                status = True
            Else

                Dim Quantity As Integer = 0 ' Convert.ToInt32(txtQuantity.Text)
                If txtQuantity.Text = "" Then
                    Quantity = Convert.ToInt32(Request.Form("txtQuantity"))
                Else
                    Quantity = Convert.ToInt32(txtQuantity.Text)
                    'Double.TryParse(txtQuantity.Text, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture, Quantity)
                End If

                Dim QuantityCount As Integer = Convert.ToInt32(hdQuantity.Value)
                If hdProductList.Value <> "" Then
                    Dim objOutputXml As New XmlDocument
                    objOutputXml.LoadXml(hdProductList.Value)
                    Dim objNodeList As XmlNodeList = objOutputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "' and @MAINTAIN_BALANCE_BY='False' and @MAINTAIN_BALANCE='True']")
                    ' Quantity = Quantity + objNodeList.Count
                    ' Code modified by pankaj
                    ' Do not check quantity validation for Purchase Order.
                    '  If ddlChallanCategory.SelectedValue <> "2" Then
                    If Quantity > QuantityCount Then
                        status = False
                    End If
                    'End If

                Else
                    If ddlChallanCategory.SelectedValue <> "2" Then
                        If Quantity > QuantityCount Then
                            status = False
                        End If
                    End If
                End If
            End If
            Return status
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
    'Function Overloading used
    'Count quantity based on value from popup( earlier  ) now i m not using it 
    'still i kept this function for future use
    Function GetQuantityCount(ByVal ProductID As String, ByVal xmlDoc As XmlDocument) As Boolean
        Try
            Dim status As Boolean = True
            Dim Quantity As Integer = 0
            Dim QuantityCount As Integer = Convert.ToInt32(hdQuantity.Value)
            Dim objNodeList As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID='" + ProductID + "' and @MAINTAIN_BALANCE_BY='False' and @MAINTAIN_BALANCE='True']")
            Quantity = Quantity + objNodeList.Count
            If Quantity >= QuantityCount Then
                status = False
            End If

            Return status
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
    'Function Overloading used 
    'This function check the quantity entered by the user will not exceed the quantity defined in the purchase order
    'This function will be called when callan type is purchase order
    Function GetQuantityCount(ByVal strxmlDoc As String) As Boolean
        Try
            Dim status As Boolean = True
            'Commented on 18 Sep 08
            'previously quantity check disabled for MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="FALSE"
            'Now quantity check enaled only when MAINTAIN_BALANCE_BY="FALSE" and MAINTAIN_BALANCE="TRUE"
            '  If ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "FALSE" Then
            If Not (ddlProduct.SelectedValue.Split("|").GetValue(1).ToString.ToUpper = "FALSE" And ddlProduct.SelectedValue.Split("|").GetValue(2).ToString.ToUpper = "TRUE") Then
                status = True
            Else
                Dim Quantity As Integer = 0
                If txtQuantity.Text = "" Then
                    Quantity = Convert.ToInt32(Request.Form("txtQuantity"))
                Else
                    Quantity = Convert.ToInt32(txtQuantity.Text)
                End If
                'Dim QuantityCount As Integer = Convert.ToInt32(hdQuantity.Value)
                If strxmlDoc <> "" Then
                    Dim objOutputXml As New XmlDocument
                    objOutputXml.LoadXml(strxmlDoc)
                    If objOutputXml.DocumentElement.SelectNodes("Product").Count > 0 Then
                        Dim objNode As XmlNode = objOutputXml.DocumentElement.SelectSingleNode("Product[@ID='" + ddlProduct.SelectedValue.Split("|").GetValue(0) + "']")
                        'this conditions is used to check whether product is exist in the purchase if it is than its quantity should be less than or equal to the quantoty entered
                        'else if this product is not exists in the purchase order than we don't have to check this condition of quantity
                        If objNode IsNot Nothing Then
                            Dim QuantityCount As Integer = Convert.ToInt32(objNode.Attributes("Qty").Value)
                            If Quantity > QuantityCount Then
                                status = False
                            End If
                        End If
                    Else
                        status = False
                    End If
                Else
                    status = False
                End If
            End If
            Return status
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function

    Protected Sub btnPrintLabel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintLabel.Click
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        objInputXml.LoadXml("<INV_PRINT_BARCODE_INPUT><ChallanID></ChallanID></INV_PRINT_BARCODE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ChallanID").InnerText = hdChallanID.Value
        objOutputXml = objbzChallan.PrintLabel(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Session("BCSCRIPT") = objOutputXml.DocumentElement.SelectSingleNode("BarCode").InnerText
            'Session("BCSCRIPT") = Replace(Session("BCSCRIPT"), vbCrLf, "<br />")
            ltrPrint.Text = "<iframe id='iframeBarCode' src='INV_Challan_BarCode.aspx'  scrolling='no' width='0' height='0' frameborder='0' style=''></iframe>"
            ClientScript.RegisterStartupScript(Me.GetType, "strBarCode1", "<script>window.setTimeout('YesNoPrint();',1200);</script>")
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        BindProduct()
        For Each ctrl As Control In Page.Controls
            ChangeControlStatus(ctrl)
        Next
    End Sub

    Protected Sub ddlChallanCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlChallanCategory.SelectedIndexChanged
        txtApprovedDate.Text = ""
        txtChallanDate.Text = ""
        txtRequestedDate.Text = ""
        txtPurchaseOrder.Text = ""
        txtOrderDate.Text = ""
        txtSupplier.Text = ""
        txtDescription.Text = ""
        txtChallanGodownName.Text = ""
        txtGodownAddress.Text = ""
        txtAgencyName.Text = ""
        txtAddress.Text = ""
        txtCountry.Text = ""
        txtCity.Text = ""
        txtFax.Text = ""
        txtPhone.Text = ""
        txtOfficeId.Text = ""
        txtSupplierName.Text = ""
        txtChallanSupplierAddress.Text = ""
        txtChallanApprovedBy.Text = ""
        txtChallanReceivedBy.Text = ""
        txtChallanRequestedBy.Text = ""

        gvProduct.DataSource = Nothing
        gvProduct.DataBind()
        hdProductList.Value = ""

        If ddlChallanCategory.SelectedValue = "2" Then
            ddlChallanType.Items.Clear()
            ddlChallanType.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlChallanType.Items.Insert(1, New ListItem("Receive", "2"))
            ddlChallanType.SelectedIndex = 0
            btnSelect.Visible = False

        Else
            ddlChallanType.Items.Clear()
            ddlChallanType.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlChallanType.Items.Insert(1, New ListItem("Issue", "1"))
            ddlChallanType.Items.Insert(2, New ListItem("Receive", "2"))
            ddlChallanType.SelectedIndex = 0
            btnSelect.Visible = True
        End If
        'code added by pankaj
        If ddlChallanCategory.SelectedValue = "2" Or ddlChallanCategory.SelectedValue = "4" Then
            gvOrder.DataSource = Nothing
            gvOrder.DataBind()
        End If

        'Code to fill godown dropdown when challan category is customer and agency is selected from popup
        If ddlChallanCategory.SelectedValue = "1" Then
            If txtAgencyName.Text <> "" Then
                FillGodown()
            End If
        Else
            ddlGodown.Items.Clear()
            ' objeAAMS.BindDropDown(ddlGodown, "GODOWN", True)
            FillGodown()
        End If
    End Sub

    Protected Sub gvOrder_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowCreated
        'If hdChallanLCodeTemp.Value <> "" Then

        'End If
    End Sub


    Protected Sub ddlHardwareType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHardwareType.SelectedIndexChanged
        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
        Dim objNodeList As XmlNodeList
        objOutputXml = New XmlDocument
        objOutputXml = objbzEquipment.List2()

        If ddlHardwareType.SelectedValue = "1" Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ddlProduct.Items.Clear()
                objNodeList = objOutputXml.DocumentElement.SelectNodes("EQUIPMENT[@EGROUP_CODE='CPU' or @EGROUP_CODE='MON' or @EGROUP_CODE='KBD' or @EGROUP_CODE='MSE' or @EGROUP_CODE='LAP' or @EGROUP_CODE='TFT']")
                For Each objNode As XmlNode In objNodeList
                    Dim li As New ListItem(objNode.Attributes("DESCRIPTION").Value, objNode.Attributes("PRODUCTID").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE").Value & "|" & objNode.Attributes("EGROUP_CODE").Value)
                    ddlProduct.Items.Add(li)
                Next
            End If
            ddlProduct.Items.Insert(0, New ListItem("--Select One--", ""))
        End If
        If ddlHardwareType.SelectedValue = "2" Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ddlProduct.Items.Clear()
                objNodeList = objOutputXml.DocumentElement.SelectNodes("EQUIPMENT[@EGROUP_CODE != 'CPU' and @EGROUP_CODE != 'MON' and @EGROUP_CODE != 'KBD' and @EGROUP_CODE != 'MSE' and @EGROUP_CODE!='LAP' and @EGROUP_CODE!='TFT']")
                For Each objNode As XmlNode In objNodeList
                    Dim li As New ListItem(objNode.Attributes("DESCRIPTION").Value, objNode.Attributes("PRODUCTID").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE").Value & "|" & objNode.Attributes("EGROUP_CODE").Value)
                    ddlProduct.Items.Add(li)
                Next
            End If
            ddlProduct.Items.Insert(0, New ListItem("--Select One--", ""))
        End If
        If ddlHardwareType.SelectedValue = "" Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ddlProduct.Items.Clear()
                objNodeList = objOutputXml.DocumentElement.SelectNodes("EQUIPMENT")
                For Each objNode As XmlNode In objNodeList
                    Dim li As New ListItem(objNode.Attributes("DESCRIPTION").Value, objNode.Attributes("PRODUCTID").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE_BY").Value & "|" & objNode.Attributes("MAINTAIN_BALANCE").Value & "|" & objNode.Attributes("EGROUP_CODE").Value)
                    ddlProduct.Items.Add(li)
                Next
            End If
            ddlProduct.Items.Insert(0, New ListItem("--Select One--", ""))
        End If

        BindXml()
        BindProduct()
    End Sub
    'Code to fill product grid with serial and vender serial number from pop up page
    Sub FillProductPopup()
        If hdProductList.Value <> "" Then
            Dim str As String = hdProductListPopUpPage.Value
            Dim objOutputXml As New XmlDocument
            Dim objNode, objNode1 As XmlNode
            Dim strArray() As String = hdProductListPopUpPage.Value.Split(",")
            Dim strProductId As New ArrayList
            objOutputXml.LoadXml(hdProductList.Value)

            'Filling xml based on value selected from pop up
            For i As Integer = 0 To strArray.Length - 1
                objNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + strArray(i).Split("|").GetValue(0).ToString + "' and @VenderSerialNo='' and @SerialNumber='' and @MAINTAIN_BALANCE_BY='False' and @MAINTAIN_BALANCE='True']")
                objNode1 = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + strArray(i).Split("|").GetValue(0).ToString + "' and @VenderSerialNo = '" + strArray(i).Split("|").GetValue(3).ToString + "' and @SerialNumber = '" + strArray(i).Split("|").GetValue(2).ToString + "' and @MAINTAIN_BALANCE_BY='False' and @MAINTAIN_BALANCE='True']")
                If ((Not objNode Is Nothing) And (objNode1 Is Nothing)) Then
                    objNode.Attributes("SerialNumber").Value = strArray(i).Split("|").GetValue(2).ToString
                    objNode.Attributes("VenderSerialNo").Value = strArray(i).Split("|").GetValue(3).ToString
                End If
                'objNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@ProductID='" + strArray(i).Split("|").GetValue(0).ToString + "' and @VenderSerialNo='" + strArray(i).Split("|").GetValue(3).ToString + "' and @SerialNumber='" + strArray(i).Split("|").GetValue(2).ToString + "']")
                'If objNode Is Nothing Then
                '    objXmlNodeClone = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS").CloneNode(True)
                '    If GetQuantityCount(strArray(i).Split("|").GetValue(0).ToString, objOutputXml) = True Then
                '        If GetProductCount() = True Then
                '            With objXmlNodeClone
                '                'PRODUCTID PRODUCTNAME SERIALNUMBER VENDORSR_NUMBER
                '                .Attributes("ProductID").Value = strArray(i).Split("|").GetValue(0).ToString
                '                .Attributes("ProductName").Value = strArray(i).Split("|").GetValue(1).ToString
                '                .Attributes("MAINTAIN_BALANCE_BY").Value = "False"
                '                .Attributes("MAINTAIN_BALANCE").Value = "True"
                '                .Attributes("SerialNumber").Value = strArray(i).Split("|").GetValue(2).ToString
                '                .Attributes("VenderSerialNo").Value = strArray(i).Split("|").GetValue(3).ToString
                '                objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").AppendChild(objXmlNodeClone)
                '            End With
                '        End If
                '    End If
                'End If
            Next
            hdProductListPopUpPage.Value = ""
            hdProductList.Value = objOutputXml.OuterXml
            If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                hdChallanDetails.Value = hdProductList.Value
            End If
            'code End
        End If
    End Sub

    Protected Sub gvOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ' Grab a reference to the Literal control 
            Dim output As New Literal
            Dim hdTemp As HtmlInputHidden
            output = CType(e.Row.FindControl("RadioButtonMarkup"), Literal)
            hdTemp = CType(e.Row.FindControl("hdRadioButtonMarkup"), HtmlInputHidden)
            Dim str As String = ""
            str = DataBinder.Eval(e.Row.DataItem, "ORDER_NUMBER")
            str = str & "|" & DataBinder.Eval(e.Row.DataItem, "APC")
            str = str & "|" & e.Row.RowIndex.ToString
            output.Text = ""
            'hdOrderQuantity.Value.Split("|").GetValue(0)
            If hdOrderQuantity.Value <> "" Then
                If hdOrderQuantity.Value.Split("|").GetValue(0).ToString = DataBinder.Eval(e.Row.DataItem, "ORDER_NUMBER") Then
                    output.Text = String.Format("<input type='radio' runat='server' name='SuppliersGroup'    id='RowSelector{0}' value='{0}' checked='true' onClick='getOrderQuantityInventoryChallan(" + hdTemp.ClientID + ")' />", e.Row.RowIndex)
                Else
                    output.Text = String.Format("<input type='radio' name='SuppliersGroup'  runat='server'  id='RowSelector{0}' value='{0}' onClick='getOrderQuantityInventoryChallan(" + hdTemp.ClientID + ")' />", e.Row.RowIndex)
                End If
            Else
                output.Text = String.Format("<input type='radio' name='SuppliersGroup' runat='server'   id='RowSelector{0}' value='{0}' onClick='getOrderQuantityInventoryChallan(" + hdTemp.ClientID + ")' />", e.Row.RowIndex)
            End If
        End If
    End Sub

    Enum ChallanType
        Customer = 1
        PurchaseOrder = 2
        Replacement = 3
        StockTransfer = 4
    End Enum

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strSno As String
        Dim objOutputXml As New XmlDocument
        Dim objSearchNode As XmlNode

        If gvProduct.Rows.Count > 0 Then
            For Each gvRow As GridViewRow In gvProduct.Rows
                If CType(gvRow.Cells(0).Controls(1), HtmlInputCheckBox).Checked = True Then
                    objOutputXml.LoadXml(hdProductList.Value)
                    strSno = gvRow.Cells(1).Text.Trim
                    objSearchNode = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN/CHALLANDETAILS[@LineNumber='" + strSno + "']")
                    If objSearchNode IsNot Nothing Then
                        objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").RemoveChild(objSearchNode)
                        hdProductList.Value = objOutputXml.OuterXml
                        If ddlChallanCategory.SelectedValue = "1" And ddlChallanType.SelectedValue = "2" And chkReplacementChallan.Checked = True Then
                            hdChallanDetails.Value = hdProductList.Value
                        End If

                    End If

                End If
            Next
            BindXml(1)
            ManageOrder()
            BindProduct()
            gvProduct.Focus()
        End If

    End Sub

    Protected Sub gvInstalledPC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInstalledPC.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Count <> 0 Then
            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Receive Challan']").Attributes("Value").Value)
            If strBuilder(2) <> "0" Then
                Dim chk As CheckBox
                btnModifyChallan.Visible = True
                gvInstalledPC.Visible = True
                gvInstalledPC.Enabled = True

                chk = CType(e.Row.Cells(1).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "CPUTYPECHECKENABLED")
                End If

                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If

                chk = CType(e.Row.Cells(4).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "MONTYPECHECKENABLED")
                End If

                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If


                chk = CType(e.Row.Cells(7).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "KBDTYPECHECKENABLED")
                End If
                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If

                chk = CType(e.Row.Cells(10).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "MSETYPECHECKENABLED")
                End If
                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If

            Else
                Dim chk As CheckBox
                chk = CType(e.Row.Cells(1).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "CPUTYPECHECKENABLED")
                End If

                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If

                chk = CType(e.Row.Cells(4).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "MONTYPECHECKENABLED")
                End If

                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If


                chk = CType(e.Row.Cells(7).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "KBDTYPECHECKENABLED")
                End If
                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If

                chk = CType(e.Row.Cells(10).Controls(1), CheckBox)
                If chk.Enabled = True Then
                    chk.Enabled = DataBinder.Eval(e.Row.DataItem, "MSETYPECHECKENABLED")
                End If
                If txtExecutionDate.Text <> "" Then
                    chk.Enabled = False
                End If
            End If
        End If
    End Sub

    Protected Sub btnModifyChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModifyChallan.Click
        Dim OutPutXml As New XmlDocument
        Dim objDocFrag As XmlDocumentFragment
        Dim objOutputXml As New XmlDocument
        Dim intInstalledPcCountSelected As Integer
        Dim intPcCountAdded As Integer
        Dim objNodeList As XmlNodeList
        Dim objtempXml As New XmlDocument
        Dim objInstalledPC As New XmlDocument
        Dim objArray As New Hashtable
        Dim objbzChallan As New AAMS.bizInventory.bzChallan

        Dim objInputXml, objTempInputXml, objInstalledPcXML As New XmlDocument

        Try
            objtempXml.LoadXml("<X><CHALLAN CHALLANID=''/></X>")
            If hdChallanID.Value.ToString <> "" Then
                objtempXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CHALLANID").InnerText = hdChallanID.Value.ToString()
            End If

            If Not Request.QueryString("ChallanID") Is Nothing Then
                hdChallanID.Value = objED.Decrypt(Request.QueryString("ChallanID").ToString)
                hdEnChallanID.Value = Request.QueryString("ChallanID").ToString
            Else
                Exit Sub
            End If
            If gvInstalledPC.Rows.Count >= 1 Then
                BindInstalledPCXml()
                OutPutXml.LoadXml(hdInstalledPCXML.Value)

                objDocFrag = OutPutXml.CreateDocumentFragment()
                objDocFrag.InnerXml = objtempXml.DocumentElement.SelectSingleNode("CHALLAN").OuterXml
                OutPutXml.DocumentElement.AppendChild(objDocFrag)

                'Validation to check no. of products selected from replacement should be equal in added product list---  Added by Neeraj 

                objTempInputXml.LoadXml(hdProductList.Value)

                If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                    If ValidateAddedReplacementProduct(objTempInputXml) = False Then
                        lblError.Text = "Product added exceeds the product selected to replace"
                        Exit Sub
                    End If
                End If


                If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                    If hdInstalledPCXML.Value <> "" Then
                        objInstalledPC.LoadXml(hdInstalledPCXML.Value)

                        objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='' and @EGROUP_CODE='LAP']")
                        If objNodeList.Count >= 1 Then 'IN CASE WHEN LAP (LAPTOP)  TO LAP (LAPTOP) IS REPLACED 
                            objArray.Add("CPU", 0)
                            objArray.Add("LAP", 0)
                            objArray.Add("MON", 0)
                            objArray.Add("TFT", 0)
                            objArray.Add("KBD", 0)
                            objArray.Add("MSE", 0)

                            objArray("CPU") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='CPU']").Count)
                            objArray("LAP") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='LAP']").Count)

                            objArray("MON") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                            objArray("TFT") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                            objArray("KBD") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                            objArray("MSE") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                            '*'WRITE LOGIC FOR LAPTOP.....
                            objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                            For Each objNode As XmlNode In objNodeList
                                If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then

                                    If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP" Then
                                        If objArray("LAP") = 0 Then 'if LAP not exists in replacemnt list than [1(one) CPU + 1(one) MON + 1(one) KBD + 1(one) MSE]  =  1(one) LAp (Laptop)
                                            If objArray("CPU") <> 0 Then
                                                objArray("CPU") = objArray("CPU") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select CPU"
                                                Exit Sub
                                            End If
                                            If objArray("MON") <> 0 Then
                                                objArray("MON") = objArray("MON") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Monitor"
                                                Exit Sub
                                            End If
                                            If objArray("KBD") <> 0 Then
                                                objArray("KBD") = objArray("KBD") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select keyboard"
                                                Exit Sub
                                            End If
                                            If objArray("MSE") <> 0 Then
                                                objArray("MSE") = objArray("MSE") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Mouse"
                                                Exit Sub
                                            End If
                                        Else
                                            objArray("LAP") = objArray("LAP") - 1
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU" Then
                                        If objArray("CPU") <> 0 Then
                                            objArray("CPU") = objArray("CPU") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Sub
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON" Then
                                        If objArray("MON") <> 0 Then
                                            objArray("MON") = objArray("MON") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Sub
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT" Then
                                        If objArray("TFT") <> 0 Then
                                            objArray("TFT") = objArray("TFT") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Sub
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                        If objArray("KBD") <> 0 Then
                                            objArray("KBD") = objArray("KBD") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Sub
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Then
                                        If objArray("MSE") <> 0 Then
                                            objArray("MSE") = objArray("MSE") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Next
                        Else 'IN CASE WHEN LAP TO LAP NOT REPLACED
                            'TOTAL COUNT FOR CPU , LAP , MON , KBD , MSE
                            intInstalledPcCountSelected = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@LAPTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                            objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                            For Each objNode As XmlNode In objNodeList
                                If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then
                                    If (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON") Then
                                        intPcCountAdded = intPcCountAdded + 1
                                    End If
                                    If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Or objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                        intPcCountAdded = intPcCountAdded + 1
                                    End If
                                End If
                            Next
                            If intPcCountAdded <> intInstalledPcCountSelected Then
                                lblError.Text = "Product added is not equal to product selected to replace"
                                Exit Sub
                            End If
                        End If

                    End If
                End If
                'end Validation to check no. of products selected from replacement should be equal in added product list---  Added by Neeraj 

                objOutputXml = objbzChallan.UpdateReplacementInstalledPCDetails(OutPutXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Function ValidateCheckedPCandPCInstalled() As Boolean

        Dim OutPutXml As New XmlDocument
        Dim objDocFrag As XmlDocumentFragment
        Dim objOutputXml As New XmlDocument
        Dim intInstalledPcCountSelected As Integer
        Dim intPcCountAdded As Integer
        Dim objNodeList As XmlNodeList
        Dim objtempXml As New XmlDocument
        Dim objInstalledPC As New XmlDocument
        Dim objArray As New Hashtable
        Dim objInputXml, objTempInputXml, objInstalledPcXML As New XmlDocument

        Dim ValidatePCChecked As Boolean = False

        Try

            objtempXml.LoadXml("<X><CHALLAN CHALLANID=''/></X>")
            If hdChallanID.Value.ToString <> "" Then
                objtempXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CHALLANID").InnerText = hdChallanID.Value.ToString()
            End If

            If gvInstalledPC.Rows.Count >= 1 Then
                BindInstalledPCXml()
                OutPutXml.LoadXml(hdInstalledPCXML.Value)

                objDocFrag = OutPutXml.CreateDocumentFragment()
                objDocFrag.InnerXml = objtempXml.DocumentElement.SelectSingleNode("CHALLAN").OuterXml
                OutPutXml.DocumentElement.AppendChild(objDocFrag)

                'Validation to check no. of products selected from replacement should be equal in added product list---  Added by Neeraj 

                objTempInputXml.LoadXml(hdProductList.Value)

                If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                    If ValidateAddedReplacementProduct(objTempInputXml) = False Then
                        lblError.Text = "Product added exceeds the product selected to replace"
                        Exit Function
                    End If
                End If


                If ddlChallanCategory.SelectedValue = "1" And chkReplacementChallan.Checked And ddlChallanType.SelectedValue = "1" Then
                    If hdInstalledPCXML.Value <> "" Then
                        objInstalledPC.LoadXml(hdInstalledPCXML.Value)

                        objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='' and @EGROUP_CODE='LAP']")
                        If objNodeList.Count >= 1 Then 'IN CASE WHEN LAP (LAPTOP)  TO LAP (LAPTOP) IS REPLACED 
                            objArray.Add("CPU", 0)
                            objArray.Add("LAP", 0)
                            objArray.Add("MON", 0)
                            objArray.Add("TFT", 0)
                            objArray.Add("KBD", 0)
                            objArray.Add("MSE", 0)

                            objArray("CPU") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='CPU']").Count)
                            objArray("LAP") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE' and @CPUEGROUP='LAP']").Count)

                            objArray("MON") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                            objArray("TFT") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                            objArray("KBD") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                            objArray("MSE") = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                            '*'WRITE LOGIC FOR LAPTOP.....
                            objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                            For Each objNode As XmlNode In objNodeList
                                If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then

                                    If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP" Then
                                        If objArray("LAP") = 0 Then 'if LAP not exists in replacemnt list than [1(one) CPU + 1(one) MON + 1(one) KBD + 1(one) MSE]  =  1(one) LAp (Laptop)
                                            If objArray("CPU") <> 0 Then
                                                objArray("CPU") = objArray("CPU") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select CPU"
                                                Exit Function
                                            End If
                                            If objArray("MON") <> 0 Then
                                                objArray("MON") = objArray("MON") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Monitor"
                                                Exit Function
                                            End If
                                            If objArray("KBD") <> 0 Then
                                                objArray("KBD") = objArray("KBD") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select keyboard"
                                                Exit Function
                                            End If
                                            If objArray("MSE") <> 0 Then
                                                objArray("MSE") = objArray("MSE") - 1
                                            Else
                                                lblError.Text = "In case of Laptop product added is not equal to product selected to replace please select Mouse"
                                                Exit Function
                                            End If
                                        Else
                                            objArray("LAP") = objArray("LAP") - 1
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU" Then
                                        If objArray("CPU") <> 0 Then
                                            objArray("CPU") = objArray("CPU") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Function
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON" Then
                                        If objArray("MON") <> 0 Then
                                            objArray("MON") = objArray("MON") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Function
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT" Then
                                        If objArray("TFT") <> 0 Then
                                            objArray("TFT") = objArray("TFT") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Function
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                        If objArray("KBD") <> 0 Then
                                            objArray("KBD") = objArray("KBD") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Function
                                        End If
                                    ElseIf objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Then
                                        If objArray("MSE") <> 0 Then
                                            objArray("MSE") = objArray("MSE") - 1
                                        Else
                                            lblError.Text = "Product added is not equal to product selected to replace"
                                            Exit Function
                                        End If
                                    End If
                                End If
                            Next
                        Else 'IN CASE WHEN LAP TO LAP NOT REPLACED
                            'TOTAL COUNT FOR CPU , LAP , MON , KBD , MSE
                            intInstalledPcCountSelected = Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@CPUTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@LAPTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@TFTTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MONTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@KBDTYPECHECK='TRUE']").Count)
                            intInstalledPcCountSelected = intInstalledPcCountSelected + Val(objInstalledPC.DocumentElement.SelectNodes("PCINSTALLATION[@MSETYPECHECK='TRUE']").Count)

                            objNodeList = objTempInputXml.DocumentElement.SelectNodes("CHALLAN/CHALLANDETAILS[@ProductID!='']")
                            For Each objNode As XmlNode In objNodeList
                                If Not (objNode.Attributes("MAINTAIN_BALANCE_BY").Value.ToUpper = "FALSE" And objNode.Attributes("MAINTAIN_BALANCE").Value.ToUpper = "FALSE") Then
                                    If (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "CPU") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "LAP") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "TFT") Or (objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MON") Then
                                        intPcCountAdded = intPcCountAdded + 1
                                    End If
                                    If objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "MSE" Or objNode.Attributes("EGROUP_CODE").InnerText.ToString.Trim().ToUpper = "KBD" Then
                                        intPcCountAdded = intPcCountAdded + 1 '+ Val(objNode.Attributes("Qty").InnerText & "")
                                    End If
                                End If
                            Next
                            If intPcCountAdded <> intInstalledPcCountSelected Then
                                lblError.Text = "Product added is not equal to product selected to replace"
                                Exit Function
                            End If
                        End If

                    End If
                End If
                'end Validation to check no. of products selected from replacement should be equal in added product list---  Added by Neeraj 
            End If
            ValidatePCChecked = True
            Return ValidatePCChecked

        Catch ex As Exception
            ValidatePCChecked = False
        End Try
    End Function

    
End Class
