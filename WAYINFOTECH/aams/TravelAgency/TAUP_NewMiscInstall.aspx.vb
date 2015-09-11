Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class TravelAgency_TAUP_NewMiscInstall
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strResult As String = ""

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
        Try
            btnClose.Attributes.Add("onclick", "return CloseWindow();")
            btnSave.Attributes.Add("onclick", "return validateNewChallan();")
            'txtChallanNo.Attributes.Add("onfocusout", "return validateChallanNo();")
            'txtEquipNo.Attributes.Add("onfocusout", "return validateEquipNo();")
            drpEquipType.Attributes.Add("onchange", "return fillEquipNo();")
            'ddlEquipNo.Attributes.Add("onfocusout", "return openPopup();")
            btnNoGrid.Attributes.Add("onclick", "return HdBtnGridNoClickFunction();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
                Exit Sub
            End If



            If Session("MSG") IsNot Nothing Then
                If Session("MSG") = "1" Then
                    lblError.Text = objeAAMSMessage.messInsert
                Else
                    lblError.Text = ""
                End If

            End If


            Dim m As ClientScriptManager = Me.ClientScript
            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)

            ' If Session("Security") IsNot Nothing Then
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRide.Value = "1"
                    Else
                        hdOverRide.Value = "0"
                    End If


                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_BACKDATED_CHALLAN']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRideBackDate.Value = "1"
                    Else
                        hdOverRideBackDate.Value = "0"
                    End If

                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Attributes("Value").Value)
                    If strBuilder(0) = "1" Then
                        hdOverRideSerialNo.Value = "1"
                    Else
                        hdOverRideSerialNo.Value = "0"
                    End If
                End If
            Else
                hdOverRide.Value = "1"
                hdOverRideBackDate.Value = "1"
                hdOverRideSerialNo.Value = "1"
                strBuilder = objEaams.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                LoadAllControl()
                hdSaveClicked.Value = "0"
                disablFirst()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub LoadAllControl()
        objEaams.BindDropDown(drpEquipType, "EQUIPMENTMISC", False)
        drpEquipType.SelectedIndex = 0
        txtDtInstall.Text = Format(Now, "dd/MM/yyyy")
        Dim objOutputXml As XmlDocument
        Dim objPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
        objOutputXml = objPCInstallation.RestrictEquipmentList()
        '        TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
        '<EGROUPCODE EQUIPMENT_CODE = '' />
        '</TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EGROUPCODE")
                If hdEquipCode.Value = "" Then
                    hdEquipCode.Value = objNode.Attributes("EQUIPMENT_CODE").Value
                Else
                    hdEquipCode.Value = hdEquipCode.Value & "|" & objNode.Attributes("EQUIPMENT_CODE").Value
                End If
            Next
        End If

    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return strResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            Dim id As String
            id = eventArgument
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument

            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '<INV_SEARCH_CHALLAN_OUTPUT><CHALLAN ChallanID='' ChallanNumber ='' CreationDate='' ChallanDate='' ChallanCategory='' ChallanType='' SupplierName='' AgencyName='' OfficeID='' GodownName='' RGodownName='' LCODE='' /><Errors Status=''><Error Code='' Description='' /></Errors></INV_SEARCH_CHALLAN_OUTPUT>

            'objInputXml.LoadXml("<INV_GETCHALLANSERIAL_INPUT><SerialNo></SerialNo><Type></Type></INV_GETCHALLANSERIAL_INPUT>")
            ' objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
            objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
            objOutputXml = objbzChallan.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim strAgencyName As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                Dim strOfficeID As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                'If (Session("Action") IsNot Nothing) Then
                '    If (Session("Action").ToString().Split("|").Length >= 2) Then
                '        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
                '            strResult = "1|" & id.Split("|").GetValue(0) & "|" & strAgencyName & "|" & strOfficeID
                '        End If
                '    Else
                '        strResult = "-1|Session Expire||"
                '    End If
                'End If
                objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = id.Split("|").GetValue(0)
                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                objOutputXml = objbzChallan.SearchStockDetails(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    strResult = strResult & "|" & objOutputXml.OuterXml
                Else
                    strResult = strResult & "|"
                End If

            Else
               
            End If

            'ElseIf id.Split("|").GetValue(1) = "2" Then

            'If Request("txtEquipNo").Trim() IsNot Nothing Then
            '    txtEquipNo.Text = Request("txtEquipNo")
            'End If
            'If hdEquipCode.Value <> "" Then
            '    Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
            '    If arEquipCode.Contains(id.Split("|").GetValue(2)) Then
            '        strResult = "10|"
            '        Exit Sub
            '    End If
            'End If
            'objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/></INV_RPTPCINSTALL_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = id.Split("|").GetValue(0)
            '' objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = id.Split("|").GetValue(2)


            'objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)
            ''<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    strResult = "11|" & id.Split("|").GetValue(0) & "|" & id.Split("|").GetValue(2)
            'Else
            '    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
            '        strResult = "12|"
            '    Else
            '        strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'End If



            'End If



        Catch ex As Exception
            lblError.Text = ex.Message
            strResult = "-1|" & ex.Message
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            hdSaveClicked.Value = "1"
            Dim status As Integer = 0

            If hdchecktype.Value = "0" Or hdchecktype.Value = "" Then
                ValidateData()
            End If

            'commented on  11sep08
            'If hdValidate.Value <> "0" Then
            '    'here str has no meaning it is just used to call overloaded function
            '    ValidateData("str")
            'End If

            If hdchecktype1.Value = "2" Then
                If txtEquipNo.Text = "" And txtEquipNo.Visible = False Then
                    If ddlEquipNo.SelectedValue = "" Then
                        lblError.Text = "Equip No. is Mandatory"
                        pnlDataValidation.Visible = True
                        pnlErroMsg.Visible = False
                        pnlGrid.Visible = False
                        EnableAllContrls()
                        txtChallanNo.ReadOnly = True
                        btnValidate.Enabled = False
                        Exit Sub
                    End If
                End If
            End If
            If hdchecktype.Value = "1" Then
                ValidateEquip()
            End If
            If hdchecktype.Value = "3" Then
                ValidateEquip_Type_No(sender, e)
                If hdchecktype.Value = "3" Then
                    Exit Sub
                End If
                If hdchecktype.Value = "4" Then
                    ValidateEquip()
                End If
            End If
            If txtEquipNo.Text <> "" And ddlEquipNo.SelectedValue = "" Then
                If hdTextGridShow.Value <> "1" Then
                    If hdchecktype.Value <> "2" Then
                        ValidateEquip_Type_No(sender, e)
                    End If
                    If hdchecktype.Value = "3" Then
                        Exit Sub
                    End If
                    If hdchecktype.Value = "4" Then
                        ValidateEquip()
                    End If
                    If hdchecktype.Value = "0" Then
                        ValidateEquip()
                    End If
                End If
            End If
            If txtEquipNo.Text = "" And ddlEquipNo.SelectedValue = "" Then
                lblError.Text = "Equip No. is Mandatory"
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                pnlGrid.Visible = False
                EnableAllContrls()
                txtChallanNo.ReadOnly = True
                btnValidate.Enabled = False
                hdchecktype.Value = "0"
                gvInstall.DataSource = Nothing
                gvInstall.DataBind()
                Exit Sub
            End If

            If hdchecktype.Value <> "0" And hdValidate.Value <> "1" Then
                If drpEquipType.SelectedIndex = 0 Then
                    lblError.Text = "Equipment Type is mandatory"
                    Exit Sub
                End If

                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objtaMiscInst As New AAMS.bizTravelAgency.bzMiscInstallation

                objInputXml.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' USE_BACKDATED_CHALLAN='' /></TA_UPDATEMISCINSTALLATION_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                    .Attributes("ACTION").Value = "I"
                    .Attributes("ROWID").Value = ""
                    If (Session("Action") IsNot Nothing) Then
                        If (Session("Action").ToString().Split("|").Length >= 2) Then
                            .Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                        Else
                            lblError.Text = "Lcode is not exist."
                            Exit Sub
                        End If
                    Else
                        lblError.Text = "Lcode is not exist."
                        Exit Sub
                    End If
                    .Attributes("DATE").Value = objEaams.ConvertTextDate(Request("txtDtInstall"))
                    .Attributes("DATEDE").Value = ""
                    .Attributes("EQUIPMENTTYPE").Value = drpEquipType.SelectedItem.Text
                    If txtEquipNo.Text = "" And txtEquipNo.Visible = False Then
                        .Attributes("EQUIPMENTNUMBER").Value = ddlEquipNo.SelectedValue
                    Else
                        .Attributes("EQUIPMENTNUMBER").Value = txtEquipNo.Text
                    End If
                    .Attributes("QTY").Value = "1" ' txtQtyEquip.Text.Trim()
                    .Attributes("ORDERNUMBER").Value = ""
                    .Attributes("ORDERNUMBERDE").Value = ""
                    .Attributes("CHALLANNO").Value = txtChallanNo.Text.Trim()

                    Dim objSecXml As New XmlDocument
                    objSecXml.LoadXml(Session("Security"))
                    .Attributes("LOGGEDBY").Value = objSecXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    .Attributes("USE_BACKDATED_CHALLAN").Value = hdOverRideBackDate.Value
                End With

                objOutputXml = objtaMiscInst.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    Session("MSG") = "1"

                    'ResetAllControls()
                    'disablFirst()

                    lblError.Text = objeAAMSMessage.messInsert

                    Response.Redirect("TAUP_NewMiscInstall.aspx?" + Request.QueryString.ToString)


                Else
                    Session("MSG") = Nothing
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    pnlGrid.Visible = False
                    EnableAllContrls()
                    bindConditionalValues()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Sub bindConditionalValues()
        If Request.Form("txtEquipNo") = "" Then
            ddlEquipNo.CssClass = "dropdownlist"
            txtEquipNo.CssClass = "displayNone"
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
            objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpEquipType.SelectedValue

            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ddlEquipNo.Items.Clear()
                ddlEquipNo.DataSource = ds.Tables("DETAILS")
                ddlEquipNo.DataTextField = "VENDORSR_NUMBER"
                ddlEquipNo.DataValueField = "VENDORSR_NUMBER"
                ddlEquipNo.DataBind()
                ddlEquipNo.Items.Insert(0, New ListItem("---Select One---", ""))
            Else
                ddlEquipNo.Items.Clear()
                ddlEquipNo.Items.Insert(0, New ListItem("---Select One---", ""))
            End If
           
            ' .Attributes("EQUIPMENTNUMBER").Value = hdEuipText.Value
        Else
            ddlEquipNo.CssClass = "displayNone"
            txtEquipNo.CssClass = "textbox"
            ddlEquipNo.Items.Clear()
            ' .Attributes("EQUIPMENTNUMBER").Value = Request.Form("txtEquipNo")
        End If
    End Sub

    Sub ValidateEquip()
        hdchecktype.Value = "0"
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim EquipNumber As String = ""
        If hdEquipCode.Value <> "" Then
            Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
            If arEquipCode.Contains(txtEquipNo.Text) Then
                hdchecktype.Value = "1"
            Else
                objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><RESP_1A /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/><VENDORSRNOFLTRTYPE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/><VENDORSRNOFLTRTYPE /></INV_RPTPCINSTALL_INPUT>")
                If hdchecktype1.Value = "2" And ddlEquipNo.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = ddlEquipNo.SelectedValue
                    EquipNumber = ddlEquipNo.SelectedValue
               
                    'objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtEquipNo.Text
                    objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"

                    If Session("LoginSession") IsNot Nothing Then
                        Dim str As String()
                        str = Session("LoginSession").ToString().Split("|")
                        objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = str(0)
                    Else
                        Exit Sub
                    End If
                    


                    objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)

                    '<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If txtEquipNo.Text <> "" Or ddlEquipNo.SelectedValue <> "" Then
                            Dim objXmlReader As XmlNodeReader
                            Dim ds As New DataSet
                            objXmlReader = New XmlNodeReader(objOutputXml)
                            ds.ReadXml(objXmlReader)
                            If ds.Tables("MISCHARDWARE") IsNot Nothing Then
                                gvInstall.DataSource = ds.Tables("MISCHARDWARE").DefaultView
                                gvInstall.DataBind()
                            Else
                                gvInstall.DataSource = Nothing
                                gvInstall.DataBind()
                            End If
                        End If
                        pnlDataValidation.Visible = True
                        pnlErroMsg.Visible = False
                        pnlGrid.Visible = True
                        lblGrid.Text = "A Hardware having Serial No.  " & EquipNumber & " is already installed.Do you Want to reuse it for this Agency also ?"
                        hdchecktype.Value = "0"
                        txtEquipNo.CssClass = "textboxgrey"
                        DisableAllControls()
                        txtChallanNo.ReadOnly = True
                        btnValidate.Enabled = False

                    Else
                        '*****************************
                        'pnlDataValidation.Visible = True
                        'pnlErroMsg.Visible = False
                        'pnlGrid.Visible = True
                        'lblGrid.Text = "Given Equipment No. " & EquipNumber & " is doesn't exist.Do you Want to Continue? "
                        ''*****************************
                        hdchecktype.Value = "1"
                        'DisableAllControls()
                        'txtEquipNo.ReadOnly = False
                        'txtEquipNo.Focus()
                    End If

                Else
                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtEquipNo.Text
                    EquipNumber = txtEquipNo.Text

                    'objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtEquipNo.Text
                    objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"


                    If Session("LoginSession") IsNot Nothing Then
                        Dim str As String()
                        str = Session("LoginSession").ToString().Split("|")
                        objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)
                    Else
                        Exit Sub
                    End If


                    objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)

                    '<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If txtEquipNo.Text <> "" Or ddlEquipNo.SelectedValue <> "" Then
                            Dim objXmlReader As XmlNodeReader
                            Dim ds As New DataSet
                            objXmlReader = New XmlNodeReader(objOutputXml)
                            ds.ReadXml(objXmlReader)
                            If ds.Tables("MISCHARDWARE") IsNot Nothing Then
                                gvInstall.DataSource = ds.Tables("MISCHARDWARE").DefaultView
                                gvInstall.DataBind()
                            Else
                                gvInstall.DataSource = Nothing
                                gvInstall.DataBind()
                            End If
                        End If
                        pnlDataValidation.Visible = True
                        pnlErroMsg.Visible = False
                        pnlGrid.Visible = True
                        lblGrid.Text = "A Hardware having number " & EquipNumber & " is already installed.Do you Want to reuse it for this Agency also ?"
                        hdchecktype.Value = "0"
                        hdTextGridShow.Value = "1"
                        txtEquipNo.CssClass = "textboxgrey"
                        DisableAllControls()
                        txtChallanNo.ReadOnly = True
                        btnValidate.Enabled = False

                    Else
                        '*****************************
                        gvInstall.DataSource = Nothing
                        gvInstall.DataBind()
                        'Code Added on 14th March for MaintainBalance

                        Dim objMaintainBalance As New AAMS.bizMaster.bzEquipment
                        Dim objXmlInput, objXmlOutput As New XmlDocument
                        objXmlInput.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCHEQUIPMENT_INPUT>")
                        objXmlInput.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText = drpEquipType.SelectedItem.Text.Trim()

                        objXmlOutput = objMaintainBalance.Search(objXmlInput)

                        If objXmlOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            Dim strMaintain As String = objXmlOutput.DocumentElement.SelectSingleNode("EQUIPMENT").Attributes("MAINTAIN_BALANCE").InnerText
                            Dim strMaintainBy As String = objXmlOutput.DocumentElement.SelectSingleNode("EQUIPMENT").Attributes("MAINTAIN_BALANCE_BY").InnerText

                            If strMaintainBy.Trim() = "0" And strMaintain.Trim() = "1" Then
                                pnlDataValidation.Visible = True
                                pnlErroMsg.Visible = False
                                pnlGrid.Visible = True
                                lblGrid.Text = "Given Equipment No. " & EquipNumber & " is doesn't exist.Do you Want to Continue? "
                                '*****************************
                                hdchecktype.Value = "0"
                                DisableAllControls()
                                txtEquipNo.CssClass = "textboxgrey"
                            Else
                                hdchecktype.Value = "1"
                            End If
                        End If

                        'Code Added on 14th March for MaintainBalance


                        'txtEquipNo.ReadOnly = False
                        'txtEquipNo.Focus()
                    End If

                End If

            End If

        End If
        lblError.Text = ""
    End Sub

    Sub ValidateData()
        Dim strChallanNo As String = txtChallanNo.Text.Trim
        If strChallanNo = "" Then
            pnlErroMsg.Visible = True
            pnlDataValidation.Visible = True
            lblValidationMsg.Text = "Challan Number is blank. Want to continue ?"
            hdchecktype.Value = "0"
            'If challan number is blank
            hdChallanRights.Value = "0"
            DisableAllControls()
        Else
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
            ' objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
            objOutputXml = objbzChallan.Search(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim strAgencyName As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                Dim strOfficeID As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then
                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
                            hdchecktype.Value = "0"
                            'If challan number is exists and installed at some agency
                            hdChallanRights.Value = "1"
                            lblValidationMsg.Text = "Given challan No. " & txtChallanNo.Text & " is for " & strAgencyName & " OfficeID " & strOfficeID & " Want to reuse it for this Agency also ?"
                            pnlDataValidation.Visible = True
                            pnlErroMsg.Visible = True

                            DisableAllControls()

                            pnlGrid.Visible = False
                        End If
                        'objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                        'objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = ID.Split("|").GetValue(0)
                        'objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        'objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        '    strResult = strResult & "|" & objOutputXml.OuterXml
                        'Else
                        '    strResult = strResult & "|"
                        'End If

                    Else
                        'If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
                        '    strResult = "0"
                        'Else
                        '    strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        'End If
                    End If
                End If
            Else
                'If challan number doesn't exists 
                hdChallanRights.Value = "2"

                hdchecktype.Value = "0"
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = True
                pnlGrid.Visible = False
                lblValidationMsg.Text = "Given challan number does not exist .Do you want to Continue?"
                DisableAllControls()
            End If
        End If
    End Sub

    Sub ValidateData(ByVal str As String)
        hdValidate.Value = "1"
        hdchecktype1.Value = "3"
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objNodeList As XmlNodeList

        ' objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text.Trim
        'objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
        'objOutputXml = objbzChallan.SearchStockDetails(objInputXml)

        Dim strChallanNo As String = txtChallanNo.Text.Trim
        If strChallanNo = "" Then
            pnlErroMsg.Visible = True
            pnlDataValidation.Visible = True
            lblValidationMsg.Text = "Challan Number is blank. Want to continue ?"
            hdchecktype.Value = "0"
            'If challan number is blank
            hdChallanRights.Value = "0"
            DisableAllControls()
        Else

            objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
            'objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")


            objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
            objOutputXml = objbzChallan.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                EnableAllContrls()

                'IF CHALLAN NUMBER IS VALID THEN FILLING ALL DROPDOWNS
                objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text.Trim
                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2" '5

                objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                hdComboData.Value = objOutputXml.OuterXml

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Dim objList As XmlNodeList
                    ddlEquipNo.Items.Clear()
                    drpEquipType.Items.Clear()

                    'EGROUP_CODE="NAC" EQUIPMENT_CODE="16S" 
                    ' Code by Neeraj to Filter Misc Items from OutPut XMl
                    objNodeList = objOutputXml.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE !='CPU' and @EGROUP_CODE !='MON' and @EGROUP_CODE !='KBD' and @EGROUP_CODE !='MSE' and @EGROUP_CODE !='LAP' and @EGROUP_CODE !='TFT']")

                    For Each objNode As XmlNode In objNodeList
                        Dim li As New ListItem(objNode.Attributes("EQUIPMENT_CODE").Value, objNode.Attributes("EGROUP_CODE").Value)
                        If Not drpEquipType.Items.Contains(li) Then
                            drpEquipType.Items.Add(li)
                        End If
                    Next

                    drpEquipType.Items.Insert(0, New ListItem("---Select One ---", ""))
                    drpEquipType.Enabled = True

                    'objList = objOutputXml.DocumentElement.SelectNodes("DETAILS[@EQUIPMENT_CODE='" + drpEquipType.SelectedValue + "']")

                    objList = objOutputXml.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE !='CPU' and @EGROUP_CODE !='MON' and @EGROUP_CODE !='KBD' and @EGROUP_CODE !='MSE' and @EGROUP_CODE !='LAP' and @EGROUP_CODE !='TFT']")
                    ddlEquipNo.Visible = True
                    ddlEquipNo.Enabled = True
                    btnSave.Enabled = True
                    txtEquipNo.Visible = False
                    hdchecktype1.Value = "2"
                    hdchecktype.Value = "1"
                    txtEquipNo.Text = ""

                    For Each objNode As XmlNode In objList
                        Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                        If Not ddlEquipNo.Items.Contains(li) Then
                            ddlEquipNo.Items.Add(li)
                        End If
                        '  ddlEquipNo.Items.Add(li)
                    Next
                    ddlEquipNo.Items.Insert(0, New ListItem("---Select One ---", ""))


                End If
                'End of Filling Dropdowns incase of valid Challan Numbers

            ElseIf objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then

                objEaams.BindDropDown(drpEquipType, "EQUIPMENTMISC", False)

                ddlEquipNo.Visible = False
                txtEquipNo.Visible = True
                hdchecktype1.Value = "0"
                hdchecktype.Value = "0"

                '  hdchecktype.Value = "0"
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = True
                pnlGrid.Visible = False
                lblValidationMsg.Text = "Given challan number does not exist .Do you want to Continue?"
                DisableAllControls()
            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub

    Protected Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            lblError.Text = ""
            ValidateData("str")
            txtChallanNo.ReadOnly = True
            btnValidate.Enabled = False
            txtChallanNo.CssClass = "textboxgrey"
            '  btnSave.Enabled = True
            Session("MSG") = Nothing

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
      

    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click

        Session("MSG") = Nothing


        hdValidate.Value = "0"

        If txtChallanNo.Text.Trim() = "" Then
            If hdOverRide.Value = "0" Or hdOverRide.Value = "" Then

                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                lblError.Text = "You don't have enough rights to Install H/W without a ChallanNo"
                hdchecktype.Value = "0"
                DisableAllControls()
                btnValidate.Enabled = True
                txtChallanNo.ReadOnly = False
                txtChallanNo.CssClass = "textbox"
            Else
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                hdchecktype.Value = "1"
                txtEquipNo.CssClass = "textbox"
                EnableAllContrls()
                '  btnSave_Click(sender, e)
            End If
        End If
        If txtChallanNo.Text <> "" Then
            If hdOverRide.Value = "0" Or hdOverRide.Value = "" Then
                If hdChallanRights.Value = "1" Then
                    hdchecktype.Value = "1"
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    txtEquipNo.CssClass = "textbox"
                    EnableAllContrls()

                    ' btnSave_Click(sender, e)
                Else
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    lblError.Text = "You don't have enough rights to Install H/W for this agency"
                    hdchecktype.Value = "0"
                    DisableAllControls()
                    btnValidate.Enabled = True
                    txtChallanNo.ReadOnly = False
                    txtChallanNo.CssClass = "textbox"
                End If
            Else
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                hdchecktype.Value = "1"
                txtEquipNo.CssClass = "textbox"
                EnableAllContrls()

                'btnSave_Click(sender, e)
            End If
        End If


    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        pnlGrid.Visible = False
        pnlDataValidation.Visible = True
        pnlErroMsg.Visible = False
        If ddlEquipNo.CssClass = "dropdownlist" And hdComboData.Value <> "" Then
            ddlEquipNo.Items.Clear()
            Dim objxmlDoc As New XmlDocument
            objxmlDoc.LoadXml(hdComboData.Value)
            For Each objNode As XmlNode In objxmlDoc.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE='" + drpEquipType.SelectedValue + "']")
                Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                ddlEquipNo.Items.Add(li)
            Next
            ddlEquipNo.Items.Insert(0, New ListItem("---Select One---", ""))

        End If
        DisableAllControls()
        txtChallanNo.CssClass = "textbox"
        txtChallanNo.ReadOnly = False
        btnValidate.Enabled = True
        Session("MSG") = Nothing
    End Sub

    Protected Sub btnYesGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYesGrid.Click
        Session("MSG") = Nothing
        hdTextGridShow.Value = ""
        ValidateEquip_Type_No(sender, e)

    End Sub

    Sub ValidateEquip_Type_No(ByVal sender As Object, ByVal e As System.EventArgs)
        EnableAllContrls()
        If hdOverRideSerialNo.Value = "1" Then
            hdchecktype.Value = "2"
            pnlGrid.Visible = False
            pnlDataValidation.Visible = True
            pnlErroMsg.Visible = False
            'Code uncommented on 16th March
            btnSave_Click(sender, e)
        Else
            If ddlEquipNo.CssClass <> "dropdownlist" Or ddlEquipNo.Visible = False Then
                'this condition works when we want to insert the record. case(having a permission of challan number and not for serial number) and challan doesn't exist and Equip number ="NA"
                If txtEquipNo.Text.ToUpper = "NA" Then
                    hdchecktype.Value = "2"
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    btnSave_Click(sender, e)
                Else

                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    hdchecktype.Value = "0"
                    lblError.Text = "You don't have enough rights to Override Equipment No."
                End If
            Else
                If hdchecktype.Value = "3" Then
                    hdchecktype.Value = "4"
                Else
                    hdchecktype.Value = "3"
                End If

                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                'btnSave_Click(sender, e)
                lblError.Text = "You don't have enough rights to Override Equipment No."
            End If
        End If
    End Sub

    Protected Sub btnNoGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoGrid.Click
        hdTextGridShow.Value = ""
        DisableAllControls()
        pnlDataValidation.Visible = True
        pnlErroMsg.Visible = False
        pnlGrid.Visible = False
        If ddlEquipNo.CssClass = "dropdownlist" And hdComboData.Value <> "" Then
            ddlEquipNo.Items.Clear()
            Dim objxmlDoc As New XmlDocument
            objxmlDoc.LoadXml(hdComboData.Value)
            For Each objNode As XmlNode In objxmlDoc.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE='" + drpEquipType.SelectedValue + "']")
                Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                ddlEquipNo.Items.Add(li)
            Next
            ddlEquipNo.Items.Insert(0, New ListItem("---Select One---", ""))
            ddlEquipNo.Enabled = True
            drpEquipType.Enabled = True
        Else
            txtEquipNo.ReadOnly = False
            txtEquipNo.CssClass = "textbox"
            drpEquipType.Enabled = True
            btnSave.Enabled = True
        End If
        hdchecktype.Value = "1"
        Session("MSG") = Nothing

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Session("MSG") = Nothing

        Response.Redirect("TAUP_NewMiscInstall.aspx?" + Request.QueryString.ToString)
    End Sub

    Private Sub DisableAllControls()
        Try
            drpEquipType.Enabled = False
            ddlEquipNo.Enabled = False
            txtEquipNo.ReadOnly = True
            txtEquipNo.CssClass = "textboxgrey"
            btnSave.Enabled = False
            imgDateApproval.Disabled = True

            If HdBtnGridNoClick.Value = "1" Then
                txtEquipNo.ReadOnly = False
                btnSave.Enabled = True
                ' btnValidate.Enabled = True
                HdBtnGridNoClick.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub EnableAllContrls()
        Try
            txtChallanNo.ReadOnly = True
            btnValidate.Enabled = False

            drpEquipType.Enabled = True
            ddlEquipNo.Enabled = True
            txtEquipNo.ReadOnly = False
            txtEquipNo.CssClass = "textbox"
            btnSave.Enabled = True
            imgDateApproval.Disabled = False
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub disablFirst()
        Try
            txtChallanNo.ReadOnly = False
            txtChallanNo.CssClass = "textbox"
            btnValidate.Enabled = True

            drpEquipType.Enabled = False
            ddlEquipNo.Enabled = False
            txtEquipNo.ReadOnly = True
            txtEquipNo.CssClass = "textboxgrey"

            imgDateApproval.Disabled = True
            btnSave.Enabled = False
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ResetAllControls()
        Try
            txtChallanNo.Text = ""
            If drpEquipType.Items.Count > 0 Then
                drpEquipType.SelectedIndex = 0
            End If
            If ddlEquipNo.Items.Count > 0 Then
                ddlEquipNo.SelectedIndex = 0
            End If
            txtEquipNo.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

End Class
