Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class TravelAgency_TAUP_ModifyMiscInstall
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
            btnSave.Attributes.Add("onclick", "return ValidateEdit();")
            'btnReset.Attributes.Add("onclick", "return closeWindow();")
            'txtModChallNo.Attributes.Add("onfocusout", "return validateChallanNo();")

            'txtModEquipNo.Attributes.Add("onfocusout", "return validateEquipNo();")
            drpModEquipType.Attributes.Add("onchange", "return fillEquipNo();")
            'drpModifyEqup.Attributes.Add("onfocusout", "return openPopup();")
            'btnSave.Attributes.Add("onclick", "return validateUpdateReplaceChallan();")

            btnNoGrid.Attributes.Add("onclick", "return HdBtnGridNoClickFunction();")


            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
                Exit Sub
            End If


            Dim m As ClientScriptManager = Me.ClientScript
            strResult = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + strResult + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)

            If Not Page.IsPostBack Then
                hdAction.Value = Request.QueryString("Action").Trim().ToUpper()
                LoadAllControl()

                drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))

               If Request.QueryString("ROWID") IsNot Nothing Then
                    viewMisRecord()
                End If

                If Request.QueryString("Action").ToString().ToUpper() = "U" Then
                    hdChkSerialNo.Value = "1"
                    ' hdchecktype.Value = "1"
                End If


            End If
            If Request.QueryString("Action").ToString().ToUpper() = "U" Then
                txtModChallNo.ReadOnly = True
                btnValidate.Enabled = False

                txtModChallNo.CssClass = "textboxgrey"
                txtChallanNo.CssClass = "textboxgrey"

            End If

            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                lblHeading.Text = "Repacement Details"
                btnSave.Enabled = False
                DisableAllControlsFirst()
            End If

            'Security Check for Override Challan Number
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If objSecurityXml IsNot Nothing Then
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
            Else
                lblError.Text = "Please Re-Login"
            End If

            'End of Sec. Check for Override Challan Number
            ' ViewState("eqpType") = drpModifyEqup.SelectedValue
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub bindConditionalValues()
        If Request.Form("txtModEquipNo") = "" Then
            drpModifyEqup.CssClass = "dropdownlist"
            txtModEquipNo.CssClass = "displayNone"
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text
            objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
            objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpModEquipType.SelectedItem.Text

            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                drpModifyEqup.Items.Clear()
                drpModifyEqup.DataSource = ds.Tables("DETAILS")
                drpModifyEqup.DataTextField = "VENDORSR_NUMBER"
                drpModifyEqup.DataValueField = "VENDORSR_NUMBER"
                drpModifyEqup.DataBind()
                drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))
            Else
                drpModifyEqup.Items.Clear()
                drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))
            End If

            ' .Attributes("EQUIPMENTNUMBER").Value = hdEuipText.Value
        Else
            drpModifyEqup.CssClass = "displayNone"
            txtModEquipNo.CssClass = "textbox"
            drpModifyEqup.Items.Clear()
            ' .Attributes("EQUIPMENTNUMBER").Value = Request.Form("txtEquipNo")
        End If
    End Sub
    Sub LoadAllControl()
        '  objEaams.BindDropDown(drpEquipType, "EQUIPMENTMISC", False)
        objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", False)

        '  drpModEquipType.SelectedIndex = 0
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

            If id.Split("|").GetValue(1) = "1" Then

                '<INV_SEARCH_CHALLAN_OUTPUT><CHALLAN ChallanID='' ChallanNumber ='' CreationDate='' ChallanDate='' ChallanCategory='' ChallanType='' SupplierName='' AgencyName='' OfficeID='' GodownName='' RGodownName='' LCODE='' /><Errors Status=''><Error Code='' Description='' /></Errors></INV_SEARCH_CHALLAN_OUTPUT>

                'objInputXml.LoadXml("<INV_GETCHALLANSERIAL_INPUT><SerialNo></SerialNo><Type></Type></INV_GETCHALLANSERIAL_INPUT>")
                'objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = id.Split("|").GetValue(0)
                objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXml = objbzChallan.Search(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Dim strAgencyName As String
                    strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                    Dim strOfficeID As String
                    strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                    If (Session("Action") IsNot Nothing) Then
                        If (Session("Action").ToString().Split("|").Length >= 2) Then
                            If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
                                strResult = "1|" & id.Split("|").GetValue(0) & "|" & strAgencyName & "|" & strOfficeID
                            End If
                        Else
                            strResult = "-1|Session Expire||"
                        End If
                    End If

                    objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = id.Split("|").GetValue(0)
                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2" '5
                    objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        strResult = strResult & "|" & objOutputXml.OuterXml
                    Else
                        strResult = strResult & "|"
                    End If


                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
                        strResult = "0"
                    Else
                        strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If

            ElseIf id.Split("|").GetValue(1) = "2" Then

                If hdEquipCode.Value <> "" Then
                    Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                    If arEquipCode.Contains(id.Split("|").GetValue(2)) Then
                        strResult = "10|"
                        Exit Sub
                    End If
                End If
                objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/><VENDORSRNOFLTRTYPE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/><VENDORSRNOFLTRTYPE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/></INV_RPTPCINSTALL_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = id.Split("|").GetValue(0)
                objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1" 'id.Split("|").GetValue(2)


                objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)
                '<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    strResult = "11|" & id.Split("|").GetValue(0) & "|" & id.Split("|").GetValue(2)
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
                        strResult = "12|"
                    Else
                        strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If



            End If



        Catch ex As Exception
            lblError.Text = ex.Message
            strResult = "-1|" & ex.Message
        End Try

    End Sub

    Sub ValidateEquip()
        hdchecktype.Value = "0"
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim EquipNumber As String = ""
        If hdEquipCode.Value <> "" Then
            Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
            If arEquipCode.Contains(txtModEquipNo.Text) Then
                hdchecktype.Value = "1"
            Else
                objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/><VENDORSRNOFLTRTYPE /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/><COUNTRY/><AOFFICE/><REGION/><WHOLEGROUP/><VENDERSERIALNO/><ONLINESTATUS/><EQUIPMENTGROUP /><EQUIPMENTTYPE/><DATEFROM/><DATETO/><RESPONSIBLESTAFFID/><VENDORSRNOFLTRTYPE /></INV_RPTPCINSTALL_INPUT>")
                If hdchecktype1.Value = "2" And drpModifyEqup.SelectedValue <> "" And drpModifyEqup.Visible = True Then
                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = drpModifyEqup.SelectedValue
                    EquipNumber = drpModifyEqup.SelectedValue
                Else
                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtModEquipNo.Text.Trim()
                    EquipNumber = txtModEquipNo.Text
                End If

                'If drpModifyEqup.Visible Then
                '    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = drpModifyEqup.SelectedItem.Text
                'Else
                '    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtModEquipNo.Text.Trim()
                'End If

                'objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtEquipNo.Text
               
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                        objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                    End If
                End If


                'Code Added on 16th March

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
                    If txtModEquipNo.Text <> "" Or drpModifyEqup.SelectedValue <> "" Then
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
                    lblGrid.Text = "A Hardware having Serial No.  " & EquipNumber & " is already installed at Following"
                    hdchecktype.Value = "0"


                    If Request.QueryString("Action").Trim.ToUpper() = "R" Then
                        DisableAllControls()
                        txtModChallNo.ReadOnly = True
                        txtModChallNo.CssClass = "textboxgrey"
                        btnValidate.Enabled = False
                    End If


                Else

                    If drpModifyEqup.Visible = False Then
                        '*****************************
                        'Code Added on 14th March for MaintainBalance

                        Dim objMaintainBalance As New AAMS.bizMaster.bzEquipment
                        Dim objXmlInput, objXmlOutput As New XmlDocument
                        objXmlInput.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCHEQUIPMENT_INPUT>")
                        objXmlInput.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText = drpModEquipType.SelectedItem.Text.Trim()

                        objXmlOutput = objMaintainBalance.Search(objXmlInput)

                        If objXmlOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            Dim strMaintain As String = objXmlOutput.DocumentElement.SelectSingleNode("EQUIPMENT").Attributes("MAINTAIN_BALANCE").InnerText
                            Dim strMaintainBy As String = objXmlOutput.DocumentElement.SelectSingleNode("EQUIPMENT").Attributes("MAINTAIN_BALANCE_BY").InnerText

                            If strMaintainBy.Trim() = "0" And strMaintain.Trim() = "1" Then
                                pnlDataValidation.Visible = True
                                pnlErroMsg.Visible = False
                                pnlGrid.Visible = True
                                gvInstall.DataSource = Nothing
                                gvInstall.DataBind()
                                lblGrid.Text = "Given Equipment No. " & EquipNumber & " is Invalid.Do you Want to Continue? "
                                hdchecktype.Value = "0"
                                DisableAllControls()

                            Else
                                hdchecktype.Value = "1"
                            End If
                        End If

                        'Code Added on 14th March for MaintainBalance

                        'Previous Code

                        'pnlDataValidation.Visible = True
                        'pnlErroMsg.Visible = False
                        'pnlGrid.Visible = True
                        'gvInstall.DataSource = Nothing
                        'gvInstall.DataBind()
                        'lblGrid.Text = "Given Equipment No. " & EquipNumber & " is Invalid.Do you Want to Continue? "
                        'hdchecktype.Value = "0"
                        'DisableAllControls()

                        'Previous Code



                    Else
                        hdchecktype.Value = "1"
                    End If
                    '*****************************



                    '  If Request.QueryString("Action").Trim.ToUpper() = "R" Then

                    ' btnSave.Enabled = True
                    'End If


                    ' hdchecktype.Value = "1"

                End If



                End If


        End If
    End Sub




    Sub ValidateData()
        Dim strChallanNo As String = txtModChallNo.Text.Trim
        If strChallanNo = "" Then
            pnlErroMsg.Visible = True
            pnlDataValidation.Visible = True
            lblValidationMsg.Text = "Challan Number is blank. Want to continue ?"
            hdchecktype.Value = "0"
            hdChallanRights.Value = "0"

            DisableAllControls()

        Else
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
            ' objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtModChallNo.Text.Trim
            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
            objOutputXml = objbzChallan.Search(objInputXml)

            ' hdComboData.Value = objOutputXml.OuterXml

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim strAgencyName As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                Dim strOfficeID As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then
                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
                            ' strResult = "1|" & ID.Split("|").GetValue(0) & "|" & strAgencyName & "|" & strOfficeID

                            '''''''''''''''''''
                            hdchecktype.Value = "0"
                            'If challan number is exists and installed at some agency
                            hdChallanRights.Value = "1"
                            lblValidationMsg.Text = "Given challan No. " & txtModChallNo.Text & " is for " & strAgencyName & " OfficeID " & strOfficeID & " Want to reuse it for this Agency also ?"
                            pnlDataValidation.Visible = True
                            pnlErroMsg.Visible = True

                            DisableAllControls()

                            pnlGrid.Visible = False

                            '''''''''''''''''''''
                            '    End If
                            'Else
                            '    strResult = "-1|Session Expire||"
                            'End If
                        End If
                        'objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                        'objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text.Trim()
                        'objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        'objOutputXml = objbzChallan.SearchStockDetails(objInputXml)

                        'hdComboData.Value = objOutputXml.OuterXml

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


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            
            Dim status As Integer = 0
            
            If hdchecktype.Value = "0" Or hdchecktype.Value = "" Then

                If Request.QueryString("Action").ToString().ToUpper() <> "U" Then
                    ValidateData()
                End If

            End If
            If hdchecktype1.Value = "2" Then
                If txtModEquipNo.Text = "" And txtModEquipNo.Visible = False Then
                    If drpModifyEqup.SelectedValue = "" Then
                        lblError.Text = "Equip No. is Mandatory"
                        pnlDataValidation.Visible = True
                        pnlErroMsg.Visible = False
                        pnlGrid.Visible = False
                        Exit Sub
                    End If
                End If
           End If

            If hdchecktype.Value = "1" Then
                ValidateEquip()
            End If

            If hdChkSerialNo.Value = "1" Then
                ValidateEquip("a")
            End If

            If hdAction.Value.ToUpper = "U" Then
                viewMisRecord("a")
            End If

            ' hdchecktype.Value = "0"

            'Code Added on 4th September


            If (txtModEquipNo.Text = "" And hdModifyEquipNo.Value = "") Then
                If hdchecktype.Value <> "0" Then
                    lblError.Text = "Equip No. is Mandatory"
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    pnlGrid.Visible = False
                    hdchecktype.Value = "0"
                    gvInstall.DataSource = Nothing
                    gvInstall.DataBind()
                    Exit Sub
                End If
            End If

            'If txtModQtyEquip.Text = "" Then
            '    lblError.Text = "Equip Quantity is Mandatory"
            '    pnlDataValidation.Visible = True
            '    pnlErroMsg.Visible = False
            '    pnlGrid.Visible = False
            '    hdchecktype.Value = "0"
            '    Exit Sub
            'End If



            If hdchecktype.Value <> "0" And hdValidate.Value <> "1" Then
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objtaMiscInst As New AAMS.bizTravelAgency.bzMiscInstallation

                objInputXml.LoadXml("<TA_UPDATEMISCINSTALLATION_INPUT><MISCINSTALLATION ACTION='' ROWID='' LCODE='' DATE='' DATEDE='' EQUIPMENTTYPE='' EQUIPMENTNUMBER='' QTY='' ORDERNUMBER='' ORDERNUMBERDE='' CHALLANNO='' LOGGEDBY='' USE_BACKDATED_CHALLAN='' /></TA_UPDATEMISCINSTALLATION_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                    .Attributes("ACTION").Value() = Request.QueryString("Action").ToString()
                    .Attributes("ROWID").Value() = Request.QueryString("ROWID").ToString()
                    If (Session("Action") IsNot Nothing) Then
                        If (Session("Action").ToString().Split("|").Length >= 2) Then
                            .Attributes("LCODE").Value() = Session("Action").ToString().Split("|").GetValue(1)
                        Else
                            lblError.Text = "Kindly ReLogin"
                            Exit Sub
                        End If
                    Else
                        lblError.Text = "Kindly ReLogin"
                        Exit Sub
                    End If
                    .Attributes("DATE").Value() = objEaams.ConvertTextDate(txtDtInstall.Text.Trim())
                    If Request("txtDtModInstall") IsNot Nothing Then
                        .Attributes("DATEDE").Value() = objEaams.ConvertTextDate(Request("txtDtModInstall").Trim())
                    Else
                        .Attributes("DATEDE").Value() = objEaams.ConvertTextDate(txtDtModInstall.Text.Trim())

                    End If

                    If drpModEquipType.SelectedIndex <> 0 Then
                        .Attributes("EQUIPMENTTYPE").Value() = drpModEquipType.SelectedItem.Text
                    Else
                        .Attributes("EQUIPMENTTYPE").Value() = ""
                    End If


                    If txtModEquipNo.Text.Trim.Length > 0 Then
                        .Attributes("EQUIPMENTNUMBER").Value = Request("txtModEquipNo").Trim
                        'ElseIf Request("txtModEquipNo") IsNot Nothing Then
                        '    .Attributes("EQUIPMENTNUMBER").Value = txtModEquipNo.Text.Trim
                    Else
                        .Attributes("EQUIPMENTNUMBER").Value = hdModifyEquipNo.Value ' drpModifyEqup.SelectedItem.Text.Trim()
                    End If


                    '.Attributes("EQUIPMENTNUMBER").Value() = txtModEquipNo.Text.Trim()

                    .Attributes("QTY").Value() = "1" ' txtModQtyEquip.Text.Trim()
                    .Attributes("ORDERNUMBER").Value() = ""
                    .Attributes("ORDERNUMBERDE").Value() = ""
                    .Attributes("CHALLANNO").Value() = txtModChallNo.Text.Trim()

                    Dim objSecXml As New XmlDocument
                    objSecXml.LoadXml(Session("Security"))
                    ' txtCreatedBy.Text = objSecXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                    .Attributes("LOGGEDBY").Value() = objSecXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    .Attributes("USE_BACKDATED_CHALLAN").Value = hdOverRideBackDate.Value
                End With

                objOutputXml = objtaMiscInst.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objeAAMSMessage.messInsert

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().ToUpper() = "U" Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "keys", "<script type='text/javascript' language='javascript'>window.opener.document.getElementById('hdUpReplace').value='1'; window.opener.document.forms['form1'].submit();  window.close()</script> ")
                        End If

                        If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "keys", "<script type='text/javascript' language='javascript'>window.opener.document.getElementById('hdUpReplace').value='2'; window.opener.document.forms['form1'].submit();  window.close()</script> ")
                        End If

                    End If
                   

                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    viewMisRecord()
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    pnlGrid.Visible = False
                    'bindConditionalValues()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub viewMisRecord()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaMiscRecord As New AAMS.bizTravelAgency.bzMiscInstallation
        objInputXml.LoadXml("<TA_VIEWMISCINSTALLATION_INPUT><ROWID /></TA_VIEWMISCINSTALLATION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerXml = Request.QueryString("ROWID").ToString()
        objOutputXml = objtaMiscRecord.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")

                txtDtInstall.Text = objEaams.ConvertDateBlank(.Attributes("DATE").Value())
                txtDtModInstall.Text = Format(Now, "dd/MM/yyyy")

                txtQuipType.Text = .Attributes("EQUIPMENTTYPE").Value()
                'drpModEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()

                txtChallanNo.Text = .Attributes("CHALLANNO").Value()
                txtModChallNo.Text = .Attributes("CHALLANNO").Value()

                If .Attributes("CHALLANNO").Value().Trim().Length = 0 Or .Attributes("CHALLANNO").Value().Trim() = "" Then
                    viewMisRecord_BlankChallan()
                    Exit Sub
                End If

                'for Challan Number validation 
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                Dim objInputXml1, objOutputXml1 As New XmlDocument
                objInputXml1.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                'objInputXml1.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml1.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtModChallNo.Text.Trim()
                objInputXml1.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXml1 = objbzChallan.Search(objInputXml1)


                If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    drpModifyEqup.CssClass = "dropdownlist"
                    txtModEquipNo.CssClass = "displayNone"

                    Dim objOutputXml2, objInputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    objInputXml2.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml2.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text.Trim()

                    ' objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    'If drpModEquipType.SelectedIndex <> 0 Then
                    '    objInputXml2.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpModEquipType.SelectedValue
                    'End If

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                        Else
                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                        End If
                    End If


                    objOutputXml2 = objbzChallan.SearchStockDetails(objInputXml2)
                    hdComboData.Value = objOutputXml2.OuterXml
                   
                    If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        
                        'Filling Equpment Type
                        drpModEquipType.Items.Clear()

                        For Each objNode As XmlNode In objOutputXml2.DocumentElement.SelectNodes("DETAILS")
                            Dim li As New ListItem(objNode.Attributes("EQUIPMENT_CODE").Value, objNode.Attributes("EGROUP_CODE").Value)
                            If Not drpModEquipType.Items.Contains(li) Then
                                drpModEquipType.Items.Add(li)
                            End If
                        Next
                        drpModEquipType.Items.Insert(0, New ListItem("---Select One ---", ""))


                        drpModEquipType.Items.FindByText(.Attributes("EQUIPMENTTYPE").Value()).Selected = True

                        'Filling Equpment Type End






                        objXmlReader = New XmlNodeReader(objOutputXml2)
                        ds.ReadXml(objXmlReader)
                        drpModifyEqup.Items.Clear()

                        'drpModifyEqup.DataSource = ds.Tables("DETAILS")
                        'drpModifyEqup.DataTextField = "VENDORSR_NUMBER"
                        'drpModifyEqup.DataValueField = "VENDORSR_NUMBER"
                        'drpModifyEqup.DataBind()

                        If .Attributes("EQUIPMENTNUMBER").Value.ToUpper = "NA" Then
                            drpModifyEqup.CssClass = "displayNone"
                            txtModEquipNo.CssClass = "textbox"
                            hdShowHide.Value = "111"

                            txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                        Else

                            For Each objNode As XmlNode In objOutputXml2.DocumentElement.SelectNodes("DETAILS")
                                Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                                If Not drpModifyEqup.Items.Contains(li) And drpModEquipType.SelectedValue = objNode.Attributes("EGROUP_CODE").Value Then
                                    drpModifyEqup.Items.Add(li)
                                End If
                            Next

                            drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))
                            ' Dim lstItem As New ListItem
                            'lstItem.Value = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                            ' drpModifyEqup.Items.FindByText(lstItem.Value)
                            ' drpModifyEqup.SelectedItem.Text = lstItem.Value
                            ' drpModifyEqup.SelectedItem.Selected = True '  (objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value())
                            'drpModifyEqup.SelectedIndex = 1
                            hdModifyEquipNo.Value = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                            drpModifyEqup.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                        End If
                    Else
                        drpModifyEqup.Items.Clear()
                        drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))
                        'drpModifyEqup.Items.Insert(1, New ListItem(.Attributes("EQUIPMENTNUMBER").Value(), "1"))
                        'drpModifyEqup.SelectedIndex = 1

                        drpModEquipType.Items.Clear()
                        objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", True)

                        txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                        drpModifyEqup.CssClass = "displayNone"
                        txtModEquipNo.CssClass = "textbox"

                        If Request.QueryString("Action") IsNot Nothing Then
                            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                                txtModEquipNo.CssClass = "textboxgrey"
                                'txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                                'drpModifyEqup.CssClass = "displayNone"
                                txtModEquipNo.ReadOnly = True
                            End If
                        End If



                    End If

                Else



                    ' .Attributes("EQUIPMENTNUMBER").Value = hdEuipText.Value
                    'Else
                    drpModifyEqup.CssClass = "displayNone"
                    txtModEquipNo.CssClass = "textbox"
                    hdShowHide.Value = "111"

                    txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()

                    '    drpModifyEqup.Items.Clear()
                    ' .Attributes("EQUIPMENTNUMBER").Value = Request.Form("txtEquipNo")

                End If

                        'End of Challan Number Validation


                txtEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                ViewState("eqpNo") = .Attributes("EQUIPMENTNUMBER").Value()
                        'R drpModEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()
                        'Dim li As New ListItem(.Attributes("VENDORSR_NUMBER").Value, .Attributes("VENDORSR_NUMBER").Value)

                        'Dim liChk As New ListItem(.Attributes("EQUIPMENT_CODE").Value, .Attributes("EGROUP_CODE").Value)

                        ' If drpModEquipType.Items.Contains(liChk) Then
                drpModEquipType.Items.FindByText(.Attributes("EQUIPMENTTYPE").Value()).Selected = True

                ' drpModEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()
                ViewState("eqpType") = .Attributes("EQUIPMENTTYPE").Value()
                        ' End If

                        '  txtQtyEquip.Text = .Attributes("QTY").Value()
                        ' txtModQtyEquip.Text = .Attributes("QTY").Value()


                        '.Attributes("ORDERNUMBER").Value()

            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Private Sub viewMisRecord(ByVal id As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaMiscRecord As New AAMS.bizTravelAgency.bzMiscInstallation
        ' Dim blnChallanEmptyChk As Boolean = False

        objInputXml.LoadXml("<TA_VIEWMISCINSTALLATION_INPUT><ROWID /></TA_VIEWMISCINSTALLATION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerXml = Request.QueryString("ROWID").ToString()
        objOutputXml = objtaMiscRecord.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")
                'start Comment
                'txtDtInstall.Text = objEaams.ConvertDateBlank(.Attributes("DATE").Value())
                'txtDtModInstall.Text = Format(Now, "dd/MM/yyyy")

                'drpEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()
                ''drpModEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()

                'txtChallanNo.Text = .Attributes("CHALLANNO").Value()
                'txtModChallNo.Text = .Attributes("CHALLANNO").Value()


                'for Challan Number validation 
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                Dim objInputXml1, objOutputXml1 As New XmlDocument
                objInputXml1.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                ' objInputXml1.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml1.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtModChallNo.Text.Trim()
                objInputXml1.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"

                If txtModChallNo.Text.Trim().Length = 0 Or txtModChallNo.Text.Trim() = "" Then
                    'blnChallanEmptyChk = True
                    objOutputXml1.LoadXml("<INV_SEARCH_CHALLAN_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></INV_SEARCH_CHALLAN_OUTPUT>")
                Else
                    objOutputXml1 = objbzChallan.Search(objInputXml1)
                End If

                'objOutputXml1 = objbzChallan.Search(objInputXml1)


                If objOutputXml1.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    drpModifyEqup.CssClass = "dropdownlist"
                    txtModEquipNo.CssClass = "displayNone"
                    Dim objOutputXml2, objInputXml2 As New XmlDocument
                    '                    Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    objInputXml2.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml2.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text.Trim()

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                        Else
                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                        End If
                    End If

                    'objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    'If drpModEquipType.SelectedIndex <> 0 Then
                    '    objInputXml2.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpModEquipType.SelectedValue
                    'End If
                    ''Code Commented on 6th March
                    'objOutputXml2 = objbzChallan.SearchStockDetails(objInputXml2)

                    '' hdComboData.Value = objOutputXml2.OuterXml

                    'If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    '    'Filling Equpment Type
                    '    'drpModEquipType.Items.Clear()

                    '    'For Each objNode As XmlNode In objOutputXml2.DocumentElement.SelectNodes("DETAILS")
                    '    '    Dim li As New ListItem(objNode.Attributes("EGROUP_CODE").Value, objNode.Attributes("EGROUP_CODE").Value)
                    '    '    If Not drpModEquipType.Items.Contains(li) Then
                    '    '        drpModEquipType.Items.Add(li)
                    '    '    End If
                    '    'Next
                    '    'drpModEquipType.Items.Insert(0, New ListItem("---Select One ---", ""))


                    '    ''Filling Equpment Type End



                    '    'objXmlReader = New XmlNodeReader(objOutputXml2)
                    '    'ds.ReadXml(objXmlReader)
                    '    'drpModifyEqup.Items.Clear()

                    '    ''drpModifyEqup.DataSource = ds.Tables("DETAILS")
                    '    ''drpModifyEqup.DataTextField = "VENDORSR_NUMBER"
                    '    ''drpModifyEqup.DataValueField = "VENDORSR_NUMBER"
                    '    ''drpModifyEqup.DataBind()

                    '    'For Each objNode As XmlNode In objOutputXml2.DocumentElement.SelectNodes("DETAILS")
                    '    '    Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                    '    '    If Not drpModifyEqup.Items.Contains(li) And .Attributes("EQUIPMENTTYPE").Value() = objNode.Attributes("EGROUP_CODE").Value Then
                    '    '        drpModifyEqup.Items.Add(li)
                    '    '    End If
                    '    'Next

                    '    'drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))
                    '    '' Dim lstItem As New ListItem
                    '    ''lstItem.Value = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                    '    '' drpModifyEqup.Items.FindByText(lstItem.Value)
                    '    '' drpModifyEqup.SelectedItem.Text = lstItem.Value
                    '    '' drpModifyEqup.SelectedItem.Selected = True '  (objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value())
                    '    ''drpModifyEqup.SelectedIndex = 1
                    '    'hdModifyEquipNo.Value = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                    '    'drpModifyEqup.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION").Attributes("EQUIPMENTNUMBER").Value()
                    'Else
                    '    '  drpModifyEqup.Items.Clear()
                    '    '  drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))

                    '    '  drpModEquipType.Items.Clear()
                    '    '  objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", True)

                    'End If
                    ''Code Commented on 6th March

                Else


                    ' .Attributes("EQUIPMENTNUMBER").Value = hdEuipText.Value
                    'Else
                    drpModifyEqup.CssClass = "displayNone"
                    txtModEquipNo.CssClass = "textbox"
                    hdShowHide.Value = "111"

                    '  txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()

                    '    drpModifyEqup.Items.Clear()
                    ' .Attributes("EQUIPMENTNUMBER").Value = Request.Form("txtEquipNo")

                End If
                'End of Challan Number Validation


                '  txtEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()

                '  drpModEquipType.SelectedValue = .Attributes("EQUIPMENTTYPE").Value()

                '  txtQtyEquip.Text = .Attributes("QTY").Value()
                '  txtModQtyEquip.Text = .Attributes("QTY").Value()


                '.Attributes("ORDERNUMBER").Value()

            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click

        btnSave.Enabled = True

        hdValidate.Value = "1"
        hdchecktype1.Value = "3"
        hdModifyEquipNo.Value = ""
        hdGridYes.Value = ""
       Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument

        If txtModChallNo.Text.Trim.Length = 0 Or txtModChallNo.Text.Trim() = "" Then
            validate_BlankChallanClick()
            Exit Sub
        End If

        'Code Added

        objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
        'objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")


        objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtModChallNo.Text.Trim
        objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
        objOutputXml = objbzChallan.Search(objInputXml)


        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then




            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text.Trim

            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                Else
                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

                End If
            End If

            ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)

            hdComboData.Value = objOutputXml.OuterXml

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objList As XmlNodeList
                drpModifyEqup.Items.Clear()

                drpModEquipType.Items.Clear()

                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DETAILS")
                    Dim li As New ListItem(objNode.Attributes("EQUIPMENT_CODE").Value, objNode.Attributes("EGROUP_CODE").Value)
                    If Not drpModEquipType.Items.Contains(li) Then
                        drpModEquipType.Items.Add(li)
                    End If
                Next
                drpModEquipType.Items.Insert(0, New ListItem("---Select One ---", ""))


                objList = objOutputXml.DocumentElement.SelectNodes("DETAILS")
                '  If objList.Count > 0 Then
                drpModifyEqup.Visible = True
                txtModEquipNo.Visible = False
                txtModEquipNo.CssClass = "displayNone"
                drpModifyEqup.CssClass = "dropdownlist"
                hdchecktype1.Value = "2"
                hdchecktype.Value = "1"
                txtModEquipNo.Text = ""
                'Else
                'drpModifyEqup.Visible = False
                'txtModEquipNo.Visible = True
                '  hdchecktype1.Value = "0"
                ' hdchecktype.Value = "0"
                ' End If
                For Each objNode As XmlNode In objList
                    Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                    'And .Attributes("EQUIPMENTTYPE").Value() = objNode.Attributes("EGROUP_CODE").Value 
                    If Not drpModifyEqup.Items.Contains(li) Then
                        drpModifyEqup.Items.Add(li)
                    End If
                Next
                drpModifyEqup.Items.Insert(0, New ListItem("---Select One ---", ""))

                EnableAllContrls()


                'Code on 12th Sep

                If ViewState("eqpNo") IsNot Nothing AndAlso ViewState("eqpNo").ToString <> "" Then
                    drpModifyEqup.Enabled = True
                    Dim liNo As ListItem = drpModifyEqup.Items.FindByText(ViewState("eqpNo").ToString())

                    If liNo IsNot Nothing Then
                        drpModifyEqup.SelectedValue = liNo.Value
                    End If
                    'drpModEquipType.Items.FindByText(ViewState("eqpType").ToString()).Selected = True
                    ' drpModEquipType.Enabled = False
                End If
                'Code on 12th Sep


            ElseIf objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then

                objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", False)

                ' hdShowHide.Value = "111"

                drpModifyEqup.Visible = False
                txtModEquipNo.Visible = True
                txtModEquipNo.CssClass = "textbox"
                txtModEquipNo.ReadOnly = False
                drpModifyEqup.CssClass = "displayNone"
                drpModEquipType.Enabled = True
                hdchecktype1.Value = "2"
                hdchecktype.Value = "1"
                'pnlDataValidation.Visible = True
                'pnlErroMsg.Visible = True
                ' pnlGrid.Visible = False
                'lblValidationMsg.Text = "Given challan number does not exist .Do you want to Continue?"
                'DisableAllControls()

                txtModChallNo.ReadOnly = True
                txtModChallNo.CssClass = "textboxgrey"
                btnValidate.Enabled = False
            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Else
            objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", False)

            hdShowHide.Value = "111"

            drpModifyEqup.Visible = False
            txtModEquipNo.Visible = True
            txtModEquipNo.CssClass = "textboxgrey"
            drpModifyEqup.CssClass = "displayNone"
            hdchecktype1.Value = "0"
            hdchecktype.Value = "0"
            pnlDataValidation.Visible = True
            pnlErroMsg.Visible = True
            pnlGrid.Visible = False
            lblValidationMsg.Text = "Given challan number does not exist .Do you want to Continue?"
            DisableAllControls()

            txtModChallNo.ReadOnly = True
            txtModChallNo.CssClass = "textboxgrey"
            btnValidate.Enabled = False

        End If









        If ViewState("eqpType") IsNot Nothing AndAlso ViewState("eqpType").ToString <> "" Then
            ' drpModEquipType.Enabled = True
            Dim li As ListItem = drpModEquipType.Items.FindByText(ViewState("eqpType").ToString())

            If li IsNot Nothing Then
                drpModEquipType.SelectedValue = li.Value
            End If
            'drpModEquipType.Items.FindByText(ViewState("eqpType").ToString()).Selected = True
            ' drpModEquipType.Enabled = False
        End If

    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click

        ' If txtModEquipNo.CssClass = "textbox" Then
        hdShowHide.Value = "111"

        EnableAllContrls()
        'Else
        'hdShowHide.Value = "1"
        'End If

        'pnlGrid.Visible = False
        'pnlDataValidation.Visible = True
        'pnlErroMsg.Visible = False

        If txtModChallNo.Text = "" Then
            If hdOverRide.Value = "0" Or hdOverRide.Value = "" Then
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False

                lblError.Text = "You don't have enough rights to Install H/W without a ChallanNo"
                hdchecktype.Value = "0"

                DisableAllControls()
                btnValidate.Enabled = True
                txtModChallNo.ReadOnly = False
                txtModChallNo.CssClass = "textbox"
            Else
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                hdchecktype.Value = "1"
                txtModEquipNo.CssClass = "textbox"
                EnableAllContrls()
                'btnSave_Click(sender, e)
            End If
        End If
        If txtModChallNo.Text <> "" Then
            If hdOverRide.Value = "0" Or hdOverRide.Value = "" Then
                If hdChallanRights.Value = "1" Then
                    hdchecktype.Value = "1"
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    txtModEquipNo.CssClass = "textbox"
                    EnableAllContrls()
                    'btnSave_Click(sender, e)
                Else
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    lblError.Text = "You don't have enough rights to Install H/W for this agency"
                    hdchecktype.Value = "0"
                    DisableAllControls()
                    btnValidate.Enabled = True
                    txtModChallNo.ReadOnly = False
                    txtModChallNo.CssClass = "textbox"
                End If
            Else
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                hdchecktype.Value = "1"
                txtModEquipNo.CssClass = "textbox"
                EnableAllContrls()



                'btnSave_Click(sender, e)
            End If
        End If
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        'If txtModEquipNo.CssClass = "textbox" Then
        hdShowHide.Value = "111"
        'Else
        'hdShowHide.Value = ""
        'End If

        pnlGrid.Visible = False
        pnlDataValidation.Visible = True
        pnlErroMsg.Visible = False
        If drpModifyEqup.CssClass = "dropdownlist" And hdComboData.Value <> "" Then
            drpModifyEqup.Items.Clear()
            Dim objxmlDoc As New XmlDocument
            objxmlDoc.LoadXml(hdComboData.Value)
            For Each objNode As XmlNode In objxmlDoc.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE='" + drpModEquipType.SelectedValue + "']")
                Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                drpModifyEqup.Items.Add(li)
            Next
            drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))

        End If
        DisableAllControls()
        txtModChallNo.ReadOnly = False
        txtModChallNo.CssClass = "textbox"
        txtModEquipNo.ReadOnly = True
        txtModEquipNo.CssClass = "textboxgrey"
        btnValidate.Enabled = True
        btnSave.Enabled = False
    End Sub

    Sub ValidateEquip_Type_No(ByVal sender As Object, ByVal e As System.EventArgs)
        hdShowHide.Value = "111"

        EnableAllContrls()

        'Else
        'hdShowHide.Value = ""
        'End If

        hdGridYes.Value = "1"

        hdChkSerialNo.Value = ""

        If hdOverRideSerialNo.Value = "1" Then
            hdchecktype.Value = "2"
            pnlGrid.Visible = False
            pnlDataValidation.Visible = True
            pnlErroMsg.Visible = False
            btnSave_Click(sender, e)
        Else
            If drpModifyEqup.CssClass <> "displayNone" Then
                hdchecktype.Value = "2"
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False
                btnSave_Click(sender, e)
            Else
                If txtModEquipNo.Text.ToUpper = "NA" Then
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
            End If
        End If
    End Sub

    Protected Sub btnYesGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYesGrid.Click
        ValidateEquip_Type_No(sender, e)
        ' If txtModEquipNo.CssClass = "textbox" Then
      

    End Sub

    Sub ValidateEquip(ByVal str As String)
        ' If txtModEquipNo.CssClass = "textbox" Then
        hdShowHide.Value = "111"
        'Else
        'hdShowHide.Value = ""
        'End If

        hdGridYes.Value = "1"



        If hdOverRideSerialNo.Value = "1" Then
            hdchecktype.Value = "2"
            pnlGrid.Visible = False
            pnlDataValidation.Visible = True
            pnlErroMsg.Visible = False

        Else
            If drpModifyEqup.CssClass <> "displayNone" Then
                hdchecktype.Value = "2"
                pnlGrid.Visible = False
                pnlDataValidation.Visible = True
                pnlErroMsg.Visible = False

            Else
                If txtModEquipNo.Text.ToUpper = "NA" Then
                    hdchecktype.Value = "2"
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False

                Else
                    pnlGrid.Visible = False
                    pnlDataValidation.Visible = True
                    pnlErroMsg.Visible = False
                    hdchecktype.Value = "0"
                    lblError.Text = "You don't have enough rights to Override Equipment No."
                    'hdChkSerialNo.Value = ""
                End If
            End If
        End If
    End Sub

    Protected Sub btnNoGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoGrid.Click
        'If txtModEquipNo.CssClass = "textbox" Then
        hdShowHide.Value = "111"
        DisableAllControls()
        'Else
        'hdShowHide.Value = ""
        'End If


        pnlDataValidation.Visible = True
        pnlErroMsg.Visible = False
        pnlGrid.Visible = False
        If drpModifyEqup.CssClass = "dropdownlist" And hdComboData.Value <> "" Then
            drpModifyEqup.Items.Clear()
            Dim objxmlDoc As New XmlDocument
            objxmlDoc.LoadXml(hdComboData.Value)
            For Each objNode As XmlNode In objxmlDoc.DocumentElement.SelectNodes("DETAILS[@EGROUP_CODE='" + drpModEquipType.SelectedValue + "']")
                Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
                drpModifyEqup.Items.Add(li)
            Next
            drpModifyEqup.Items.Insert(0, New ListItem("---Select One---", ""))

            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                drpModifyEqup.Enabled = True
            End If

        Else
            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                txtModEquipNo.ReadOnly = False
                txtModEquipNo.CssClass = "textbox"
                btnSave.Enabled = True
                drpModEquipType.Enabled = True

            End If
        End If
        hdchecktype.Value = "1"
    End Sub

    Protected Sub btnSave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Load

    End Sub


    'Code Added on 2nd September
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub DisableAllControls()
        Try
            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                drpModEquipType.Enabled = False
                drpModifyEqup.Enabled = False
                txtModEquipNo.ReadOnly = True
                txtModEquipNo.CssClass = "textboxgrey"
                img1.Disabled = False
                img1.Disabled = True
                btnSave.Enabled = False

                

                If HdBtnGridNoClick.Value = "1" Then
                    txtModEquipNo.ReadOnly = False
                    txtModEquipNo.CssClass = "textbox"
                    HdBtnGridNoClick.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub EnableAllContrls()
        Try
            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                txtModChallNo.ReadOnly = True
                txtModChallNo.CssClass = "textboxgrey"
                btnValidate.Enabled = False

                drpModEquipType.Enabled = True
                drpModifyEqup.Enabled = True
                txtModEquipNo.ReadOnly = False
                txtModEquipNo.CssClass = "textbox"
                img1.Disabled = False
                btnSave.Enabled = True

            End If
            Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub DisableAllControlsFirst()
        Try
            If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                drpModEquipType.Enabled = False
                drpModifyEqup.Enabled = False
                If txtModEquipNo.CssClass = "textbox" Then
                    txtModEquipNo.ReadOnly = True
                    txtModEquipNo.CssClass = "textboxgrey"
                End If
               img1.Disabled = False
                img1.Disabled = True
                btnSave.Enabled = False



                If HdBtnGridNoClick.Value = "1" Then
                    txtModEquipNo.ReadOnly = False
                    txtModEquipNo.CssClass = "textbox"
                    HdBtnGridNoClick.Value = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub viewMisRecord_BlankChallan()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaMiscRecord As New AAMS.bizTravelAgency.bzMiscInstallation
        objInputXml.LoadXml("<TA_VIEWMISCINSTALLATION_INPUT><ROWID /></TA_VIEWMISCINSTALLATION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerXml = Request.QueryString("ROWID").ToString()
        objOutputXml = objtaMiscRecord.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("MISCINSTALLATION")

                txtDtInstall.Text = objEaams.ConvertDateBlank(.Attributes("DATE").Value())
                txtDtModInstall.Text = Format(Now, "dd/MM/yyyy")
                txtQuipType.Text = .Attributes("EQUIPMENTTYPE").Value()
                txtChallanNo.Text = .Attributes("CHALLANNO").Value()
                txtModChallNo.Text = .Attributes("CHALLANNO").Value()
                txtModEquipNo.CssClass = "textbox"
                drpModEquipType.Items.Clear()
                objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", True)


                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().ToUpper() = "R" Then
                        txtModEquipNo.CssClass = "textboxgrey"
                        txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                        drpModifyEqup.CssClass = "displayNone"
                        txtModEquipNo.ReadOnly = True
                    End If
                End If

                drpModifyEqup.CssClass = "displayNone"
                txtModEquipNo.CssClass = "textbox"
                hdShowHide.Value = "111"
                txtModEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                txtEquipNo.Text = .Attributes("EQUIPMENTNUMBER").Value()
                ViewState("eqpNo") = .Attributes("EQUIPMENTNUMBER").Value()
                drpModEquipType.Items.FindByText(.Attributes("EQUIPMENTTYPE").Value()).Selected = True
                ViewState("eqpType") = .Attributes("EQUIPMENTTYPE").Value()
            End With

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Private Sub validate_BlankChallanClick()
        btnSave.Enabled = True

        hdValidate.Value = "1"
        hdchecktype1.Value = "3"
        hdModifyEquipNo.Value = ""
        hdGridYes.Value = ""
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument

        'objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtModChallNo.Text.Trim

        'If Request.QueryString("Action") IsNot Nothing Then
        '    If Request.QueryString("Action").ToString().ToUpper() = "R" Then
        '        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

        '    Else
        '        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"

        '    End If
        'End If

        '' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
        'objOutputXml = objbzChallan.SearchStockDetails(objInputXml)

        'hdComboData.Value = objOutputXml.OuterXml

        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    Dim objList As XmlNodeList
        '    drpModifyEqup.Items.Clear()

        '    drpModEquipType.Items.Clear()

        '    For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DETAILS")
        '        Dim li As New ListItem(objNode.Attributes("EQUIPMENT_CODE").Value, objNode.Attributes("EGROUP_CODE").Value)
        '        If Not drpModEquipType.Items.Contains(li) Then
        '            drpModEquipType.Items.Add(li)
        '        End If
        '    Next
        '    drpModEquipType.Items.Insert(0, New ListItem("---Select One ---", ""))


        '    objList = objOutputXml.DocumentElement.SelectNodes("DETAILS")
        '    '  If objList.Count > 0 Then
        '    drpModifyEqup.Visible = True
        '    txtModEquipNo.Visible = False
        '    txtModEquipNo.CssClass = "displayNone"
        '    drpModifyEqup.CssClass = "dropdownlist"
        '    hdchecktype1.Value = "2"
        '    hdchecktype.Value = "1"
        '    txtModEquipNo.Text = ""
        '    'Else
        '    'drpModifyEqup.Visible = False
        '    'txtModEquipNo.Visible = True
        '    '  hdchecktype1.Value = "0"
        '    ' hdchecktype.Value = "0"
        '    ' End If
        '    For Each objNode As XmlNode In objList
        '        Dim li As New ListItem(objNode.Attributes("VENDORSR_NUMBER").Value, objNode.Attributes("VENDORSR_NUMBER").Value)
        '        'And .Attributes("EQUIPMENTTYPE").Value() = objNode.Attributes("EGROUP_CODE").Value 
        '        If Not drpModifyEqup.Items.Contains(li) Then
        '            drpModifyEqup.Items.Add(li)
        '        End If
        '    Next
        '    drpModifyEqup.Items.Insert(0, New ListItem("---Select One ---", ""))

        '    EnableAllContrls()


        '    'Code on 12th Sep

        '    If ViewState("eqpNo") IsNot Nothing AndAlso ViewState("eqpNo").ToString <> "" Then
        '        drpModifyEqup.Enabled = True
        '        Dim liNo As ListItem = drpModifyEqup.Items.FindByText(ViewState("eqpNo").ToString())

        '        If liNo IsNot Nothing Then
        '            drpModifyEqup.SelectedValue = liNo.Value
        '        End If
        '        'drpModEquipType.Items.FindByText(ViewState("eqpType").ToString()).Selected = True
        '        ' drpModEquipType.Enabled = False
        '    End If
        '    'Code on 12th Sep


        'ElseIf objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!" Then

        objEaams.BindDropDown(drpModEquipType, "EQUIPMENTMISC", False)

        hdShowHide.Value = "111"

        drpModifyEqup.Visible = False
        txtModEquipNo.Visible = True
        txtModEquipNo.CssClass = "textboxgrey"
        drpModifyEqup.CssClass = "displayNone"
        hdchecktype1.Value = "0"
        hdchecktype.Value = "0"
        pnlDataValidation.Visible = True
        pnlErroMsg.Visible = True
        pnlGrid.Visible = False
        lblValidationMsg.Text = "Challan Number is blank. Want to continue ?"
        DisableAllControls()

        txtModChallNo.ReadOnly = True
        txtModChallNo.CssClass = "textboxgrey"
        btnValidate.Enabled = False
        'Else

        'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        ' End If




        If ViewState("eqpType") IsNot Nothing AndAlso ViewState("eqpType").ToString <> "" Then
            ' drpModEquipType.Enabled = True
            Dim li As ListItem = drpModEquipType.Items.FindByText(ViewState("eqpType").ToString())

            If li IsNot Nothing Then
                drpModEquipType.SelectedValue = li.Value
            End If
            'drpModEquipType.Items.FindByText(ViewState("eqpType").ToString()).Selected = True
            ' drpModEquipType.Enabled = False
        End If
    End Sub
    

End Class
