#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region

Partial Class TravelAgency_TAUP_New1APCInstall
    Inherits System.Web.UI.Page


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

#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
    Dim strResult As String
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyPcInstallation"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            'btnSave.Attributes.Add("onclick", "return AgencyPCMandatory();")
            txtOrderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            drpOrderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            txtChallanNo.Attributes.Add("onchange", "return ResetSaveForChallan();")
            txtCpuNo.Attributes.Add("onchange", "return ResetSaveForCpuNo();")
            txtMonNo.Attributes.Add("onchange", "return ResetSaveForMonNo();")
            drpCpuNo.Attributes.Add("onchange", "return ResetSaveForCpuNo();")
            drpMonNo.Attributes.Add("onchange", "return ResetSaveForMonNo();")
            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                Exit Sub
            End If
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        'btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        'btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights


            If Not Page.IsPostBack Then
                ' ************************************************
                ' Security value for OVERRIDE_CHALLAN_NO,OVERRIDE_CHALLAN_SERIAL_NO,USE_BACKDATED_CHALLAN,OVERRIDE_ORDER_NO
                LoadAllEuipmentCodeValue()

                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Attributes("Value").Value)
                        If strBuilder(0) = "1" Then
                            hdblnOrderOverride.Value = "1"
                        Else
                            hdblnOrderOverride.Value = "0"
                        End If

                    Else
                        hdblnOrderOverride.Value = "0"
                    End If
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Attributes("Value").Value)
                        If strBuilder(0) = "1" Then
                            hdblnChallanOverride.Value = "1"
                        Else
                            hdblnChallanOverride.Value = "0"
                        End If
                    Else
                        hdblnChallanOverride.Value = "0"
                    End If
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Attributes("Value").Value)
                        If strBuilder(0) = "1" Then
                            hdblnSNoOverride.Value = "1"
                        Else
                            hdblnSNoOverride.Value = "0"
                        End If
                    Else
                       hdblnSNoOverride.Value = "0"
                    End If
                Else
                    hdblnOrderOverride.Value = "1"
                    hdblnChallanOverride.Value = "1"
                    hdblnSNoOverride.Value = "1"
                End If
                ' ************************************************
                txtInstallDate.Text = Format(Now, "dd/MM/yyyy") ' Format(CDate(DateTime.Now.Month.ToString + "/" + DateTime.Now.Day.ToString + "/" + DateTime.Now.Year.ToString), "dd/MM/yyyy")
                bindControls()
                '*******************************************************************

            End If
            'If hdChallanNoExist.Value = "1" Then
            '    drpCpuNo.CssClass = "dropdown"
            '    drpMonNo.CssClass = "dropdown"
            '    txtCpuNo.CssClass = "displayNone"
            '    txtMonNo.CssClass = "displayNone"
            'End If
            If Not IsPostBack Then
                pnlEnableorDisable.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
  
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            pnlMsg.Visible = False
            pnlNo.Visible = False
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
            '            Dim objXmlReader As XmlNodeReader
            Dim li As New ListItem
            Dim ds As New DataSet
            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation

            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then
                        ' Check Validation for Order No.
                        ' @ If Any Cause hdAllowSaveForOrder<>1 then can't save for a proper validation on Order NO.

                      

                        If hdAllowSaveForOrder.Value <> "1" Then
                            If drpOrderNo.Visible = True Then
                                If drpOrderNo.SelectedValue.Trim.Length = 0 Then
                                    hdAllowSaveForOrder.Value = "0"
                                    pnlEnableorDisable.Enabled = False
                                    lblConfirm.Text = "Order number is blank. Do you want to continue?"
                                    pnlMsg.Visible = True
                                    SaveStatus.Value = "Order"
                                    drpOrderNo.Focus()
                                    Exit Sub

                                End If
                            End If

                            If txtOrderNo.Visible = True Then
                                If txtOrderNo.Text.Trim().Length = 0 Then
                                    pnlEnableorDisable.Enabled = False
                                    hdAllowSaveForOrder.Value = "0"
                                    lblConfirm.Text = "Order number is blank. Do you want to continue?"
                                    pnlMsg.Visible = True
                                    ' PnlDetails.Visible = False
                                    SaveStatus.Value = "Order"
                                    txtOrderNo.Focus()
                                    Exit Sub
                                End If
                            
                                If txtOrderNo.Text.Trim().Length > 0 Then
                                    If txtOrderNo.Text.Trim() = "0" Then
                                        pnlEnableorDisable.Enabled = False
                                        lblConfirm.Text = "Order number is 0. Do you want to continue?"
                                        pnlMsg.Visible = True
                                        SaveStatus.Value = "Order"
                                        txtOrderNo.Focus()
                                        Exit Sub

                                    End If
                                End If
                                '*************************************************************
                                ' Gaeeing value for order no exist or not?
                                If txtOrderNo.Text.Trim().Length > 0 Then
                                    If txtOrderNo.Text.Trim() <> "0" Then

                                        Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                        'objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_SEARCHORDER_INPUT>")
                                        objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></UP_SEARCHORDER_INPUT>")


                                        With objInputXml.DocumentElement
                                            If (Session("Action") IsNot Nothing) Then
                                                If (Session("Action").ToString().Split("|").Length >= 2) Then
                                                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                                                    .SelectSingleNode("LCODE").InnerText = Lcode
                                                End If
                                            End If
                                            .SelectSingleNode("ORDER_NUMBER").InnerText = txtOrderNo.Text

                                            If Not Session("LoginSession") Is Nothing Then
                                                .SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                                            End If

                                        End With
                                        objOutputXml = objbzOrder.Search(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            hdOrderNoExist.Value = "1"

                                            hdAllowSaveForOrder.Value = "1"



                                        Else
                                            hdOrderNoExist.Value = "0"
                                            hdAllowSaveForOrder.Value = "0"
                                            pnlMsg.Visible = True
                                            pnlEnableorDisable.Enabled = False
                                            SaveStatus.Value = "Order"
                                            lblConfirm.Text = "Given order number does not exist. Do you want to continue?"
                                            txtOrderNo.Focus()
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If

                            '*************************************************************
                            ' Gaeeing value for order no exist or not?


                        End If

                        '###########################################
                        ' @ End of proper validation on Order NO.
                        ' ########################################

                        '###########################################
                        ' @ mandaory for cpu type.
                        ' ########################################
                        If drpCpuType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "CPU Type is mandaory."
                            SaveStatus.Value = "CPUTYPE"
                            drpCpuType.Focus()
                            Exit Sub

                        End If

                        '###########################################
                        ' @ end of mandaory for cpu type.
                        ' ########################################


                        '###########################################
                        ' Check Validation for CPU No.
                        ' @ If Any Cause hdAllowSaveForCpuNo<>1 then can't save for a proper validation on CPU NO.
                        If drpCpuNo.Visible = True Then
                            If drpCpuNo.SelectedValue = "" Then
                                lblError.Text = "CPU no. is mandatory."
                                SaveStatus.Value = "CPUNO"
                                If drpCpuNo.Visible = True Then
                                    drpCpuNo.Focus()
                                End If

                                Exit Sub
                            End If
                        End If
                        If txtCpuNo.Visible = True Then
                            If txtCpuNo.Text.Trim.Length = 0 Then
                                lblError.Text = "CPU no. is manadatory."
                                SaveStatus.Value = "CPUNO"
                                If txtCpuNo.Visible = True Then
                                    txtCpuNo.Focus()
                                End If

                                Exit Sub
                            End If
                        End If
                        If hdAllowSaveForCpuNo.Value <> "1" Then

                            If drpCpuNo.Visible = True Then
                                If drpCpuNo.SelectedValue = "NA" Then
                                    If drpCpuType.SelectedValue <> "CPP" Then
                                        If hdblnSNoOverride.Value = "0" Then
                                            lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                            SaveStatus.Value = "CPUNO"
                                            If drpCpuNo.Visible = True Then
                                                drpCpuNo.Focus()
                                            End If
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                If drpCpuNo.SelectedValue <> "" Then
                                    objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                                    ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
                                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = drpCpuNo.SelectedValue
                                    objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                    objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        '  strResult = "CT11|" & ID.Split("|").GetValue(0) & "|" & ID.Split("|").GetValue(2)
                                        '@ Show Panel For All Value of CPU No exit in database                                        

                                        bindGridPanelForCpuNo(drpCpuNo.SelectedValue)
                                        drpCpuNo.Focus()
                                        Exit Sub
                                    Else
                                        'If hdblnSNoOverride.Value = "0" Then
                                        '    lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                        '    SaveStatus.Value = "CPUNO"
                                        '    If drpCpuNo.Visible = True Then
                                        '        drpCpuNo.Focus()
                                        '    End If
                                        '    Exit Sub
                                        'End If




                                    End If
                                End If
                            End If

                            If drpCpuNo.Visible = False Then
                                'If txtCpuNo.Text.Trim.Length = 0 Then
                                '    lblError.Text = "CPU no. is manadatory."
                                '    SaveStatus.Value = "CPUNO"
                                '    If txtCpuNo.Visible = True Then
                                '        txtCpuNo.Focus()
                                '    End If

                                '    Exit Sub
                                'End If
                                'If txtCpuNo.Text.Trim().ToUpper = "NA" Then
                                '    If hdEquipCode.Value <> "" Then
                                '        Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                                '        If Not arEquipCode.Contains(drpCpuType.SelectedValue) Then
                                '            If hdblnSNoOverride.Value = "0" Then
                                '                lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                '                SaveStatus.Value = "CPUNO"
                                '                If drpCpuNo.Visible = True Then
                                '                    drpCpuNo.Focus()
                                '                End If
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End If

                                'End If
                                If txtCpuNo.Text.Trim().ToUpper = "NA" Then
                                    If drpCpuType.SelectedValue <> "CPP" Then
                                        If hdblnSNoOverride.Value = "0" Then
                                            lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                            SaveStatus.Value = "CPUNO"
                                            If drpCpuNo.Visible = True Then
                                                drpCpuNo.Focus()
                                            End If
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                If txtCpuNo.Text.Trim().Length > 0 Then

                                    If txtCpuNo.Text.Trim().ToUpper() <> "NA" Then
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                        '@ Check Whether Entered CPU No is out for selected agency or not
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                                   

                                        ds = New DataSet

                                        ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                                        objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                                        objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = "" 'txtChallanNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                        objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                                        'objInputXml.DocumentElement.SelectSingleNode("SERIALNUMBER").InnerText = txtCpuNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtCpuNo.Text
                                        objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            If objOutputXml.DocumentElement.SelectSingleNode("DETAILS ").Attributes("EQUIPMENT_CODE").Value <> drpCpuType.SelectedValue Then
                                                lblError.Text = "CPU type does not belongs to entered cpu no."
                                                SaveStatus.Value = "CPUTYPE"
                                                If drpCpuType.Visible = True Then
                                                    drpCpuType.Focus()
                                                End If
                                                Exit Sub
                                            End If
                                        Else
                                            'If hdblnSNoOverride.Value = "0" Then
                                            '    lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                            '    SaveStatus.Value = "CPUNO"
                                            '    If txtCpuNo.Visible = True Then
                                            '        txtCpuNo.Focus()
                                            '    End If
                                            '    Exit Sub
                                            'End If


                                            hdAllowSaveForCpuNo.Value = "0"
                                            pnlMsg.Visible = True
                                            pnlEnableorDisable.Enabled = False
                                            SaveStatus.Value = "CPUNO"
                                            lblConfirm.Text = "Given CPU number does not exist. Do you want to continue?"
                                            txtCpuNo.Focus()
                                            Exit Sub

                                        End If
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@




                                        ' @ Open A Poup For Showing All CPU No Used 
                                        objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                                        ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
                                        objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtCpuNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                        objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            '  strResult = "CT11|" & ID.Split("|").GetValue(0) & "|" & ID.Split("|").GetValue(2)
                                            '@ Show Panel For All Value of CPU No exit in database

                                            bindGridPanelForCpuNo(txtCpuNo.Text)
                                            If txtCpuNo.Visible = True Then
                                                txtCpuNo.Focus()
                                            End If

                                            Exit Sub
                                        Else

                                            'If hdblnSNoOverride.Value = "0" Then
                                            '    lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                            '    SaveStatus.Value = "CPUNO"
                                            '    If txtCpuNo.Visible = True Then
                                            '        txtCpuNo.Focus()
                                            '    End If
                                            '    Exit Sub
                                            'End If

                                        End If

                                    End If

                                End If
                            End If
                            'hdAllowSaveForCpuNo.Value = "1"
                        End If

                        '###########################################
                        ' End of Check Validation for CPU No.

                        '###########################################
                        ' @ mandaory for Monitor type.
                        ' ########################################
                        If drpMonType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "Monitor Type is mandaory."
                            SaveStatus.Value = "MONTYPE"
                            drpMonType.Focus()
                            Exit Sub

                        End If

                        '###########################################
                        ' @ end of mandaory for Monitor type.
                        ' ########################################


                        '###########################################
                        ' End of Check Validation for Mon No.

                        ' @ If Any Cause hdAllowSaveForMonNo<>1 then can't save for a proper validation on CPU NO.
                        If drpMonNo.Visible = True Then
                            If drpMonNo.SelectedValue = "" Then
                                lblError.Text = "Monitor no. is mandatory."
                                SaveStatus.Value = "MONNO"
                                If drpMonNo.Visible = True Then
                                    drpMonNo.Focus()
                                End If
                                Exit Sub
                            End If
                        End If
                        If txtMonNo.Visible = True Then
                            If txtMonNo.Text.Trim.Length = 0 Then
                                lblError.Text = "Monitor no. is mandatory."
                                SaveStatus.Value = "MONNO"
                                If txtMonNo.Visible = True Then
                                    txtMonNo.Focus()
                                End If

                                Exit Sub
                            End If
                        End If
                        If hdAllowSaveForMonNo.Value <> "1" Then

                            If drpMonNo.Visible = True Then
                                'If drpMonNo.SelectedValue = "" Then
                                '    lblError.Text = "Monitor no. is mandatory."
                                '    SaveStatus.Value = "MONNO"
                                '    If drpMonNo.Visible = True Then
                                '        drpMonNo.Focus()
                                '    End If

                                '    Exit Sub
                                'End If
                                'If drpMonNo.SelectedValue = "NA" Then
                                '    If hdEquipCode.Value <> "" Then
                                '        Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                                '        If Not arEquipCode.Contains(drpMonType.SelectedValue) Then
                                '            If hdblnSNoOverride.Value = "0" Then
                                '                lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                '                SaveStatus.Value = "MONNO"
                                '                If drpMonNo.Visible = True Then
                                '                    drpMonNo.Focus()
                                '                End If
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End If

                                'End If

                                If drpMonNo.SelectedValue = "NA" Then
                                    If drpMonType.SelectedValue <> "MMP" Then
                                        If hdblnSNoOverride.Value = "0" Then
                                            lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                            SaveStatus.Value = "MONNO"
                                            If drpMonNo.Visible = True Then
                                                drpMonNo.Focus()
                                            End If
                                            Exit Sub
                                        End If
                                    End If
                                End If

                                If drpMonNo.SelectedValue <> "" Then

                                    ' @ Open A Poup For Showing All CPU No Used 
                                    objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                                    ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
                                    objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = drpMonNo.SelectedValue
                                    objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                    objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        '  strResult = "CT11|" & ID.Split("|").GetValue(0) & "|" & ID.Split("|").GetValue(2)
                                        '@ Show Panel For All Value of CPU No exit in database

                                        bindGridPanelForMonNo(drpMonNo.SelectedValue)

                                        If drpMonNo.Visible = True Then
                                            drpMonNo.Focus()
                                        End If

                                        Exit Sub
                                    Else
                                        'If hdblnSNoOverride.Value = "0" Then
                                        '    lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                        '    SaveStatus.Value = "MONNO"
                                        '    If drpMonNo.Visible = True Then
                                        '        drpMonNo.Focus()
                                        '    End If
                                        '    Exit Sub
                                        'End If
                                    End If
                                End If
                            End If

                            If drpMonNo.Visible = False Then
                                'If txtMonNo.Text.Trim.Length = 0 Then
                                '    lblError.Text = "Monitor no. is mandatory."
                                '    SaveStatus.Value = "MONNO"
                                '    If txtMonNo.Visible = True Then
                                '        txtMonNo.Focus()
                                '    End If

                                '    Exit Sub
                                'End If                              

                                'If txtMonNo.Text.Trim().ToUpper = "NA" Then
                                '    If hdEquipCode.Value <> "" Then
                                '        Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                                '        If Not arEquipCode.Contains(drpMonType.SelectedValue) Then
                                '            If hdblnSNoOverride.Value = "0" Then
                                '                lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                '                SaveStatus.Value = "MONNO"
                                '                If drpMonNo.Visible = True Then
                                '                    drpMonNo.Focus()
                                '                End If
                                '                Exit Sub
                                '            End If

                                '        End If
                                '    End If

                                'End If

                                If txtMonNo.Text.Trim().ToUpper = "NA" Then
                                    If drpMonType.SelectedValue <> "MMP" Then
                                        If hdblnSNoOverride.Value = "0" Then
                                            lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                            SaveStatus.Value = "MONNO"
                                            If drpMonNo.Visible = True Then
                                                drpMonNo.Focus()
                                            End If
                                            Exit Sub
                                        End If
                                    End If
                                End If


                                If txtMonNo.Text.Trim().Length > 0 Then

                                    If txtMonNo.Text.Trim().ToUpper() <> "NA" Then
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                        '@ Check Whether Entered Mon No is out for selected agency or not
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                                   

                                        ds = New DataSet

                                        ' @ This is Used For Getting the All Mon No on the basis of Challan No
                                        objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                                        objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = "" 'txtChallanNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                        objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                                        'objInputXml.DocumentElement.SelectSingleNode("SERIALNUMBER").InnerText = txtMonNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtMonNo.Text
                                        objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            If objOutputXml.DocumentElement.SelectSingleNode("DETAILS ").Attributes("EQUIPMENT_CODE").Value <> drpMonType.SelectedValue Then
                                                lblError.Text = "Monitor type does not belongs to entered monitor no."
                                                SaveStatus.Value = "MONTYPE"
                                                If drpMonType.Visible = True Then
                                                    drpMonType.Focus()
                                                End If
                                                Exit Sub
                                            End If

                                        Else
                                            'If hdblnSNoOverride.Value = "0" Then
                                            '    lblError.Text = "You don't have enough rights to install h/w without a valid Monitor no."
                                            '    SaveStatus.Value = "MONNO"
                                            '    If txtMonNo.Visible = True Then
                                            '        txtMonNo.Focus()
                                            '    End If
                                            '    Exit Sub
                                            'End If
                                            hdAllowSaveForMonNo.Value = "0"
                                            pnlMsg.Visible = True
                                            pnlEnableorDisable.Enabled = False
                                            SaveStatus.Value = "MONNO"
                                            lblConfirm.Text = "Given Monitor number does not exist. Do you want to continue?"
                                            txtMonNo.Focus()
                                            Exit Sub

                                        End If
                                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


                                        ' @ Open A Poup For Showing All CPU No Used 
                                        objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
                                        ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
                                        objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtMonNo.Text
                                        objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                        objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            '  strResult = "CT11|" & ID.Split("|").GetValue(0) & "|" & ID.Split("|").GetValue(2)
                                            '@ Show Panel For All Value of CPU No exit in database

                                            bindGridPanelForMonNo(txtMonNo.Text)
                                            If txtMonNo.Visible = True Then
                                                txtMonNo.Focus()
                                            End If
                                            Exit Sub
                                        Else
                                            'If hdblnSNoOverride.Value = "0" Then
                                            '    lblError.Text = "You don't have enough rights to install h/w without a valid monitor no."
                                            '    SaveStatus.Value = "MONNO"
                                            '    If txtMonNo.Visible = True Then
                                            '        txtMonNo.Focus()
                                            '    End If
                                            '    Exit Sub
                                            'End If
                                        End If
                                    End If
                                End If
                            End If
                            'hdAllowSaveForCpuNo.Value = "1"
                        End If

                        '###########################################
                        ' @ End of proper validation on Mon NO.
                        ' ########################################


                        '###########################################
                        ' End of Check Validation for Keyboard No.

                        ' @ If Any Cause hdAllowSaveForKeyNo<>1 then can't save for a proper validation on CPU NO.

                        '###########################################
                        ' @ mandaory for KeyBoard type.
                        ' ########################################
                        If drpKeyType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "Keyboard Type is mandaory."
                            SaveStatus.Value = "KEYYPE"
                            drpKeyType.Focus()
                            Exit Sub

                        End If

                        '###########################################
                        ' @ end of mandaory for Keyboard type.
                        ' ########################################

                        If hdAllowSaveForKeyNo.Value <> "1" Then
                            If txtKeyboardNo.Text.Trim.Length = 0 Then
                                lblError.Text = "Keyboard no. is mandatory."
                                SaveStatus.Value = "KEYNO"
                                If txtKeyboardNo.Visible = True Then
                                    txtKeyboardNo.Focus()
                                End If
                                Exit Sub
                            End If
                            'If txtKeyboardNo.Text.Trim = "NA" Then
                            '    If hdEquipCode.Value <> "" Then
                            '        Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                            '        If Not arEquipCode.Contains(drpKeyType.SelectedValue) Then
                            '            If hdblnSNoOverride.Value = "0" Then
                            '                lblError.Text = "You don't have enough rights to Install H/W without a valid Keyboard No."
                            '                SaveStatus.Value = "KEYNO"
                            '                If txtKeyboardNo.Visible = True Then
                            '                    txtKeyboardNo.Focus()
                            '                End If
                            '                Exit Sub
                            '            End If

                            '        End If
                            '    End If

                            'End If
                            'If txtKeyboardNo.Text.Trim <> "NA" Then
                            '    If hdblnSNoOverride.Value = "0" Then
                            '        lblError.Text = "You don't have enough rights to Install H/W without a valid Keyboard No."
                            '        SaveStatus.Value = "KEYNO"
                            '        If txtKeyboardNo.Visible = True Then
                            '            txtKeyboardNo.Focus()
                            '        End If
                            '        Exit Sub
                            '    End If
                            'End If
                        End If

                        '###########################################
                        ' @ End of proper validation on Mouse NO.
                        ' ########################################



                        ' @ If Any Cause hdAllowSaveForMSENo<>1 then can't save for a proper validation on CPU NO.

                        '###########################################
                        ' @ mandaory for Mouse type.
                        ' ########################################
                        If drpMouseType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "Mouse Type is mandaory."
                            SaveStatus.Value = "MOUSETYPE"
                            drpMouseType.Focus()
                            Exit Sub

                        End If

                        '###########################################
                        ' @ end of mandaory for Mouse type.
                        ' ########################################

                        If hdAllowSaveForMSENo.Value <> "1" Then
                            If txtMouseNo.Text.Trim.Length = 0 Then
                                lblError.Text = "Mouse no. is mandatory."
                                SaveStatus.Value = "MOUSE"
                                If txtMouseNo.Visible = True Then
                                    txtMouseNo.Focus()
                                End If
                                Exit Sub
                            End If
                            'If txtMouseNo.Text.Trim = "NA" Then
                            '    If hdEquipCode.Value <> "" Then
                            '        Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
                            '        If Not arEquipCode.Contains(drpMouseType.SelectedValue) Then
                            '            If hdblnSNoOverride.Value = "0" Then
                            '                lblError.Text = "You don't have enough rights to Install H/W without a valid Mouse No."
                            '                SaveStatus.Value = "MOUSE"
                            '                If txtMouseNo.Visible = True Then
                            '                    txtMouseNo.Focus()
                            '                End If
                            '                Exit Sub
                            '            End If

                            '        End If
                            '    End If

                            'End If
                            'If txtMouseNo.Text.Trim <> "NA" Then
                            '    If hdblnSNoOverride.Value = "0" Then
                            '        lblError.Text = "You don't have enough rights to Install H/W without a valid Mouse No."
                            '        SaveStatus.Value = "MOUSE"
                            '        If txtMouseNo.Visible = True Then
                            '            txtMouseNo.Focus()
                            '        End If
                            '        Exit Sub
                            '    End If

                            'End If
                        End If

                        '###########################################
                        ' @ End of proper validation on Mon NO.
                        ' ########################################

                        objInputXml.LoadXml("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  ADDLRAM=''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' Qty='' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = '' CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO =''  OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").Value = "False"

                        ' ************************************************
                        ' Security value for OVERRIDE_CHALLAN_NO,OVERRIDE_CHALLAN_SERIAL_NO,USE_BACKDATED_CHALLAN,OVERRIDE_ORDER_NO

                        Dim objSecurityXml As New XmlDocument
                        objSecurityXml.LoadXml(Session("Security"))
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_NO']").Attributes("Value").Value)
                                If strBuilder(0) = "1" Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "True"

                                End If
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "False"
                            End If
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_CHALLAN_SERIAL_NO']").Attributes("Value").Value)
                                If strBuilder(0) = "1" Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").Value = "True"
                                End If
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").Value = "False"
                            End If
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_BACKDATED_CHALLAN']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='USE_BACKDATED_CHALLAN']").Attributes("Value").Value)
                                If strBuilder(0) = "1" Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").Value = "True"
                                End If
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").Value = "False"
                            End If
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Count <> 0 Then
                                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Attributes("Value").Value)
                                If strBuilder(0) = "1" Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").Value = "True"
                                End If
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").Value = "False"
                            End If
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "True"
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").Value = "True"
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").Value = "True"
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").Value = "True"
                        End If
                        ' ************************************************
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                        If (Request("txtInstallDate") IsNot Nothing) Then '
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(Request("txtInstallDate").Trim())
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(txtInstallDate.Text)
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").Value = drpCpuType.SelectedValue

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").Value = drpMonType.SelectedValue


                        If drpCpuNo.Visible = True Then

                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").Value = drpCpuNo.SelectedItem.Text

                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").Value = txtCpuNo.Text
                        End If

                        If drpMonNo.Visible = True Then

                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").Value = drpMonNo.SelectedItem.Text

                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").Value = txtMonNo.Text
                        End If


                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").Value = drpKeyType.SelectedValue
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").Value = txtKeyboardNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").Value = txtCdrNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").Value = drpMouseType.SelectedValue
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").Value = txtMouseNo.Text
                        If drpOrderNo.Visible = True Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = drpOrderNo.SelectedValue
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = txtOrderNo.Text
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").Value = txtRem.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").Value = txtChallanNo.Text
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").Value = Session("LoginSession").ToString().Split("|")(0)
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedDateTime").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANSTATUS").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").Value = "1"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").Value = txtRam.Text

                        'hdInputSavexml.Value = objInputXml.OuterXml

                        ' ***********************************************************************************************


                        If drpOrderNo.Visible = True Then
                            'If drpOrderNo.SelectedValue.Trim.Length > 0 Then
                            '    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                            '    objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region></UP_SEARCHORDER_INPUT>")
                            '    With objInputXml.DocumentElement
                            '        If (Session("Action") IsNot Nothing) Then
                            '            If (Session("Action").ToString().Split("|").Length >= 2) Then
                            '                Lcode = Session("Action").ToString().Split("|").GetValue(1)
                            '                .SelectSingleNode("LCODE").InnerText = Lcode
                            '            End If
                            '        End If
                            '        .SelectSingleNode("ORDER_NUMBER").InnerText = drpCpuType.SelectedValue
                            '    End With
                            '    objOutputXml = objbzOrder.Search(objInputXml)
                            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            '        hdOrderNoExist.Value = "1"
                            '    Else
                            '        hdOrderNoExist.Value = "0"
                            '    End If
                            hdOrderNoExist.Value = "1"
                            'End If
                        End If


                        If txtOrderNo.Visible = True Then
                            If txtOrderNo.Text.Trim.Length > 0 Then
                                If txtOrderNo.Text.Trim <> "0" Then
                                    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                    objInputXml2 = New XmlDocument
                                    objOutputXml2 = New XmlDocument
                                    'objInputXml2.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_SEARCHORDER_INPUT>")
                                    objInputXml2.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></UP_SEARCHORDER_INPUT>")
                                    With objInputXml2.DocumentElement
                                        If (Session("Action") IsNot Nothing) Then
                                            If (Session("Action").ToString().Split("|").Length >= 2) Then
                                                Lcode = Session("Action").ToString().Split("|").GetValue(1)
                                                .SelectSingleNode("LCODE").InnerText = Lcode
                                            End If
                                        End If
                                        .SelectSingleNode("ORDER_NUMBER").InnerText = txtOrderNo.Text
                                        If Not Session("LoginSession") Is Nothing Then
                                            .SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                                        End If


                                    End With
                                    objOutputXml2 = objbzOrder.Search(objInputXml2)
                                    If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                        hdOrderNoExist.Value = "1"
                                    Else
                                        hdOrderNoExist.Value = "0"
                                    End If
                                Else
                                    hdOrderNoExist.Value = "0"
                                End If
                            Else
                                hdOrderNoExist.Value = "0"
                            End If
                        End If


                        ' @ If Order No  does not exit in the list and have ovveridable for Order No Then Set Order No Value to 0
                        If hdOrderNoExist.Value <> "1" Then
                            If hdblnOrderOverride.Value = "1" Then

                                If txtOrderNo.Visible = True Then
                                    If txtOrderNo.Text.Trim.Length = 0 Then
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
                                    End If
                                End If
                                If drpOrderNo.Visible = True Then
                                    If drpOrderNo.SelectedValue.Trim.Length = 0 Then
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
                                    Else
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = drpOrderNo.SelectedValue
                                    End If
                                End If

                            End If
                        Else
                            If hdblnOrderOverride.Value = "1" Then
                                If txtOrderNo.Visible = True Then
                                    If txtOrderNo.Text.Trim.Length = 0 Then
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
                                    End If
                                End If
                               
                                If drpOrderNo.Visible = True Then
                                    If drpOrderNo.SelectedValue.Trim.Length = 0 Then
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
                                    Else
                                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = drpOrderNo.SelectedValue
                                    End If
                                End If

                            End If
                        End If

                        ' @ If Challan No  does not exit in the list and have ovveridable for Order No Then Set Challan No Value to 0
                        If hdChallanNoExist.Value <> "1" Then
                            If hdblnChallanOverride.Value = "1" Then
                                If txtChallanNo.Text.Trim.Length = 0 Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").Value = 0
                                End If

                            End If
                        End If







                        ' Here Back end Method Call
                        objOutputXml = objbzPCInstallation.Update(objInputXml)
                        ' objOutputXml.LoadXml("<INV_UPDATESERIALNO_OUTPUT> <DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' /> <Errors Status='FALSE'> <Error Code='' Description=''/>  </Errors></INV_UPDATESERIALNO_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            lblError.Text = objeAAMSMessage.messInsert ' "Record updated successfully."  
                            ' Response.Redirect("TAUP_NewAgencyPCInstall.aspx?Msg=U&Popup=T&PrdId=" + Request.QueryString("PrdId") + "&SNO=" + txtSNo.Text + "&VSNO=" + txtNewVenSNo.Text + "&PNAME=" + txtPrdName.Text, False)
                            btnSave.Enabled = False
                            'ClientScript.RegisterStartupScript(Me.GetType, "keys", "<script type='text/javascript' language='javascript'>window.opener.document.forms['form1'].submit();window.close();</script> ")
                            'pnlEnableorDisable.Enabled = False
                            'txtChallanNo.Enabled = True
                            'btnValidate.Enabled = True

                        Else
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                    Else
                        lblError.Text = "Incomplete Parameter"
                    End If
                Else
                    lblError.Text = "Incomplete Parameter"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Request("txtInstallDate") IsNot Nothing Then
                txtInstallDate.Text = Request("txtInstallDate")
            End If
        End Try
    End Sub
    Private Sub bindControls()

        'objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
        '' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
        'objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
        '' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
        'objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
        '' drpMonType.Items.Insert(0, New ListItem("MMP", "MMP"))
        'objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)
        ''drpMouseType.Items.Insert(0, New ListItem("MSP", "MSP"))


        objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
        drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
        drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
        ' drpKeyType.Items.Insert(drpKeyType.Items.Count, New ListItem("KBP", "KBP"))
        drpKeyType.Items.Insert(drpKeyType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
        drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
        drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)
        'drpMouseType.Items.Insert(drpMouseType.Items.Count, New ListItem("MSP", "MSP"))
        drpMouseType.Items.Insert(drpMouseType.Items.Count, New ListItem("NA", "NA"))

    End Sub

   
    Sub LoadAllEuipmentCodeValue()
        hdEquipCodexml.Value = ""
        'objeAAMS.BindDropDown(drpEquipType, "EQUIPMENTMISC", False)
        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        '  Dim objXmlReader As XmlNodeReader
        Dim objPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
        objOutputXml = objPCInstallation.RestrictEquipmentList()
        '        TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
        '<EGROUPCODE EQUIPMENT_CODE = '' />
        '</TA_GET_RESTRICTED_EQUIPMENT_CODE_OUTPUT>
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            hdEquipCodexml.Value = objOutputXml.OuterXml
          

            For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("EGROUPCODE")
                If hdEquipCode.Value = "" Then
                    hdEquipCode.Value = objNode.Attributes("EQUIPMENT_CODE").Value
                Else
                    hdEquipCode.Value = hdEquipCode.Value & "|" & objNode.Attributes("EQUIPMENT_CODE").Value
                End If
            Next
        End If
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        '    PnlDetails.Visible = True
        pnlMsg.Visible = False



        If SaveStatus.Value = "CPUNO" Then
            If hdblnSNoOverride.Value = "0" Then
                lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                SaveStatus.Value = "CPUNO"
                If txtCpuNo.Visible = True Then
                    txtCpuNo.Focus()
                End If
                pnlEnableorDisable.Enabled = True
                SaveStatus.Value = "CPUNO"
                hdAllowSaveForCpuNo.Value = "0"
                Exit Sub
            End If
            pnlEnableorDisable.Enabled = True
            hdAllowSaveForCpuNo.Value = 1
            btnSave_Click(sender, e)
        End If

        If SaveStatus.Value = "MONNO" Then
            If hdblnSNoOverride.Value = "0" Then
                lblError.Text = "You don't have enough rights to install h/w without a valid Monitor no."
                SaveStatus.Value = "MONNO"
                If txtMonNo.Visible = True Then
                    txtMonNo.Focus()
                End If
                pnlEnableorDisable.Enabled = True
                SaveStatus.Value = "MONNO"
                hdAllowSaveForMonNo.Value = "0"
                Exit Sub
            End If
            pnlEnableorDisable.Enabled = True
            hdAllowSaveForMonNo.Value = 1
            btnSave_Click(sender, e)
        End If




        If SaveStatus.Value = "Order" Then

            If hdblnOrderOverride.Value = "0" Then
                If drpOrderNo.Visible = True Then
                    If drpOrderNo.Text.Trim().Length = 0 Then
                        lblError.Text = "You don't have enough rights to install h/w without a Order No."
                    End If
                    drpOrderNo.Focus()
                ElseIf txtOrderNo.Visible = True Then

                    If txtOrderNo.Text.Trim().Length = 0 Then
                        lblError.Text = "You don't have enough rights to install h/w without a Order No."
                    Else
                        lblError.Text = "You don't have enough rights to install h/w without a  valid Order no."
                    End If
                    txtOrderNo.Focus()
                End If

                pnlEnableorDisable.Enabled = True
                SaveStatus.Value = "Order"
                hdAllowSaveForOrder.Value = "0"

                Exit Sub
            End If
            pnlEnableorDisable.Enabled = True
            hdAllowSaveForOrder.Value = 1
            btnSave_Click(sender, e)
        End If
        If SaveStatus.Value = "Challan" Then

            If hdblnChallanOverride.Value = "0" Then
                If txtChallanNo.Text.Trim().Length = 0 Then
                    lblError.Text = "You don't have enough rights to install h/w without a challan No."
                Else
                    lblError.Text = "You don't have enough rights to install h/w without a  valid challan no."
                End If

                pnlEnableorDisable.Enabled = False
                SaveStatus.Value = "Challan"
                hdAllowSaveForChallan.Value = "0"

                If hdAllowSaveForChallan.Value = "0" Then
                    txtChallanNo.Enabled = True
                    btnValidate.Enabled = True
                End If

                txtChallanNo.Focus()
                Exit Sub
            End If


            hdAllowSaveForChallan.Value = 1

            '@ New code If Challan No is valid And exist in database then  
            pnlEnableorDisable.Enabled = True
            btnValidate.Enabled = False
            txtChallanNo.Enabled = False
            '@ New Code  If Challan No is valid And exist in database then 

            If txtOrderNo.Visible = True Then
                txtOrderNo.Focus()
            ElseIf drpOrderNo.Visible = True Then
                drpOrderNo.Focus()
            End If
        End If


    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        '  PnlDetails.Visible = True
        pnlMsg.Visible = False
        If SaveStatus.Value = "CPUNO" Then
            hdAllowSaveForCpuNo.Value = 0
            pnlEnableorDisable.Enabled = True
            Exit Sub
        End If
        If SaveStatus.Value = "MONNO" Then
            hdAllowSaveForMonNo.Value = 0
            pnlEnableorDisable.Enabled = True
            Exit Sub
        End If
        If SaveStatus.Value = "Order" Then
            hdAllowSaveForOrder.Value = 0
            pnlEnableorDisable.Enabled = True
            ' txtOrderNo.Focus()
            If drpOrderNo.Visible = True Then
                drpOrderNo.Focus()
            ElseIf txtOrderNo.Visible = True Then
                txtOrderNo.Focus()
            End If
            Exit Sub
        End If
        If SaveStatus.Value = "Challan" Then
            hdAllowSaveForChallan.Value = 0
            If hdAllowSaveForChallan.Value = "0" Then
                txtChallanNo.Enabled = True
                btnValidate.Enabled = True
            End If
            txtChallanNo.Focus()
            Exit Sub
        End If

    End Sub

    Protected Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            hdAllowSaveForOrder.Value = "0"

            ValidateChallanNo()
        Catch ex As Exception

        Finally
            If Request("txtInstallDate") IsNot Nothing Then
                txtInstallDate.Text = Request("txtInstallDate")
            End If
            If hdAllowSaveForChallan.Value = "0" Then
                txtChallanNo.Enabled = False
                btnValidate.Enabled = False
            End If
        End Try

    End Sub
    Private Sub ValidateChallanNo()
        hdChallaDetails.Value = "0"
        pnlNo.Visible = False
        pnlMsg.Visible = False
        lblError.Text = ""
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim li As New ListItem
        Dim ds As New DataSet
        Dim dv As DataView
        Dim i As Integer
        'drpCpuNo.Items.Clear()
        'drpMonNo.Items.Clear()
        'getting value  for challan No exist or not?
        Try

            If txtChallanNo.Text.Trim().Length = 0 Then
                drpCpuNo.Visible = False
                drpMonNo.Visible = False
                drpOrderNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtOrderNo.Visible = True

                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                bindControls()
                'If hdblnChallanOverride.Value = "0" Then
                '    lblError.Text = "You don't have enough rights to install h/w without a challan No."
                '    SaveStatus.Value = "Challan"
                '    hdAllowSaveForChallan.Value = "0"
                '    txtChallanNo.Focus()
                '    Exit Sub
                'End If

                lblConfirm.Text = "Challan number is blank. Do you want to continue?"
                pnlMsg.Visible = True
                'PnlDetails.Visible = False
                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                pnlEnableorDisable.Enabled = False
                Exit Sub


                'Exit Sub
            End If

            If txtChallanNo.Text.Trim = "0" Then
                drpCpuNo.Visible = False
                drpMonNo.Visible = False
                drpOrderNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtOrderNo.Visible = True
                bindControls()
                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()

                lblConfirm.Text = "Challan number is 0. Do you want to continue?"
                pnlMsg.Visible = True
                'PnlDetails.Visible = False
                SaveStatus.Value = "Challan"
                pnlEnableorDisable.Enabled = False
                txtChallanNo.Focus()
                Exit Sub

                'If hdblnChallanOverride.Value = "0" Then
                '    lblError.Text = "You don't have enough rights to install h/w without a valid challan No."
                '    SaveStatus.Value = "Challan"
                '    hdAllowSaveForChallan.Value = "0"
                '    txtChallanNo.Focus()
                '    Exit Sub
                'End If
                Exit Sub
            End If
            If txtChallanNo.Text.Trim.Length > 0 Then
                ' objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXml = objbzChallan.Search(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then
                        hdChallanNoExist.Value = "1"
                        hdAllowSaveForChallan.Value = "1"
                        pnlEnableorDisable.Enabled = True
                        btnValidate.Enabled = False
                        txtChallanNo.Enabled = False

                    End If
                    Dim strAgencyName As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                    Dim strOfficeID As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                    If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then



                        'hdChallanNoExist.Value = "1"
                        'hdAllowSaveForChallan.Value = "1"

                        '@ If Challan No is valid And exist in database then  


                        '@ If Challan No is valid And exist in database then  

                        ds = New DataSet
                        ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                        objInputXml2.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                        objInputXml2.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                        objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        'objInputXml2.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpCpuType.SelectedValue
                        objOutputXml2 = objbzChallan.SearchStockDetails(objInputXml2)
                        If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                            hdChallaDetails.Value = "1"
                            objXmlReader = New XmlNodeReader(objOutputXml2)
                            ds.ReadXml(objXmlReader)



                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill ORDERNO

                            drpOrderNo.Items.Clear()
                            drpOrderNo.Visible = True
                            txtOrderNo.Visible = False

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "ORDERNUMBER<>'' "

                            For i = 0 To dv.Count - 1
                                Dim Item As New ListItem
                                Item = New ListItem(dv(i)("ORDERNUMBER").ToString, dv(i)("ORDERNUMBER").ToString)
                                If Not drpOrderNo.Items.Contains(Item) Then
                                    drpOrderNo.Items.Insert(drpOrderNo.Items.Count, Item)
                                End If
                            Next
                            drpOrderNo.Items.Insert(0, New ListItem("--Select One--", ""))
                            drpOrderNo.DataBind()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   


                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill CPUTYPE

                            drpCpuType.Items.Clear()

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "EGROUP_CODE='CPU' or EGROUP_CODE='LAP' " '"EGROUP_CODE='CPU'"


                            For i = 0 To dv.Count - 1
                                Dim Item As New ListItem
                                Item = New ListItem(dv(i)("EQUIPMENT_CODE").ToString, dv(i)("EQUIPMENT_CODE").ToString)
                                If Not drpCpuType.Items.Contains(Item) Then
                                    drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                End If
                            Next

                            ' @ Code Added If No any Record Found for CPU Type then
                            '@ Add CPP For Cpu Type

                            'If dv.Count = 0 Then
                            '    drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                            'End If

                            Dim li2 As New ListItem
                            li2 = drpCpuType.Items.FindByValue("CPP")
                            If li2 Is Nothing Then
                                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                            End If


                            drpCpuType.Items.Insert(0, New ListItem("--Select One--", ""))
                            drpCpuType.DataBind()

                            drpCpuNo.Visible = True
                            txtCpuNo.Visible = False

                            If dv.Count = 0 Then
                                drpCpuNo.Visible = False
                                txtCpuNo.Visible = True

                            End If


                            FillCPUNO()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  


                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill MONTYPE
                            drpMonType.Items.Clear()

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "EGROUP_CODE='MON' or EGROUP_CODE='TFT' " ' "EGROUP_CODE='MON'"
                            For i = 0 To dv.Count - 1
                                Dim Item As New ListItem
                                Item = New ListItem(dv(i)("EQUIPMENT_CODE").ToString, dv(i)("EQUIPMENT_CODE").ToString)
                                If Not drpMonType.Items.Contains(Item) Then
                                    drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                End If
                            Next

                            'If dv.Count = 0 Then
                            '    drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                            'End If

                            Dim li3 As New ListItem
                            li3 = drpMonType.Items.FindByValue("MMP")
                            If li3 Is Nothing Then
                                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                            End If


                            drpMonType.Items.Insert(0, New ListItem("--Select One--", ""))
                            drpMonType.DataBind()

                            drpMonNo.Visible = True
                            txtMonNo.Visible = False

                            If dv.Count = 0 Then
                                drpMonNo.Visible = False
                                txtMonNo.Visible = True
                            End If
                            If dv.Count = 0 Then
                                drpMonNo.Visible = False
                                txtMonNo.Visible = True
                            End If
                            FillMonNo()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        Else
                            ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                            drpOrderNo.Visible = False
                            txtOrderNo.Visible = True
                            drpCpuNo.Visible = False
                            txtCpuNo.Visible = True
                            drpMonNo.Visible = False
                            txtMonNo.Visible = True
                            hdChallaDetails.Value = "0"
                            bindControls()
                        End If
                    Else
                        bindControls()
                        drpCpuNo.Visible = False
                        txtCpuNo.Visible = True
                        drpOrderNo.Visible = False
                        txtOrderNo.Visible = True
                        drpMonNo.Visible = False
                        txtMonNo.Visible = True
                        pnlEnableorDisable.Enabled = False
                       
                        '@ Start of Previous Code 
                        'pnlMsg.Visible = True
                        'SaveStatus.Value = "Challan"
                        'lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                        '@ End of Previous code

                        '@ New code added
                        pnlMsg.Visible = False
                        SaveStatus.Value = "Challan"
                        lblError.Text = "Invalid challan for this agency."
                        '@ End of New code added

                        txtChallanNo.Focus()
                        Exit Sub
                    End If
                Else
                    bindControls()
                    drpCpuNo.Visible = False
                    txtCpuNo.Visible = True
                    drpOrderNo.Visible = False
                    txtOrderNo.Visible = True
                    drpMonNo.Visible = False
                    txtMonNo.Visible = True
                    pnlEnableorDisable.Enabled = False
                    pnlMsg.Visible = True
                    SaveStatus.Value = "Challan"
                    lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                    txtChallanNo.Focus()
                    Exit Sub
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
            '*************************************************************
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub drpMonType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpMonType.SelectedIndexChanged
        Try
            'If hdChallanNoExist.Value = "1" Then
            '    drpMonNo.Visible = True
            '    txtMonNo.Visible = False
            'Else
            '    txtMonNo.Visible = True
            '    drpMonNo.Visible = False
            'End If
            FillMonNo()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub drpKeyType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpKeyType.SelectedIndexChanged
        Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpCpuType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCpuType.SelectedIndexChanged

        Try
            'If hdChallanNoExist.Value = "1" Then
            '    drpCpuNo.Visible = True
            '    txtCpuNo.Visible = False
            'Else
            '    drpCpuNo.Visible = False
            '    txtCpuNo.Visible = True
            'End If
            FillCPUNO()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub bindGridPanelForCpuNo(ByVal strNo As String)
        Try
            '  lblError.Text = "A Hardware having number " & Request.QueryString("VENDERSERIALNO").ToString & " is already installed at Following"
            lblMonOrCpuNo.Text = "A CPU no. " & strNo & " is already installed at following location. "

            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet

            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
            'objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = strNo
            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"

            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("PCINSTALL") IsNot Nothing Then
                    SaveStatus.Value = "CPUNO"
                    gvInstall.DataSource = ds.Tables("PCINSTALL").DefaultView
                    gvInstall.DataBind()
                    pnlNo.Visible = True
                    pnlEnableorDisable.Enabled = False
                Else
                    gvInstall.DataSource = Nothing
                    gvInstall.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub bindGridPanelForMonNo(ByVal strNo As String)
        Try
            ' lblError.Text = "A Hardware having number " & Request.QueryString("VENDERSERIALNO").ToString & " is already installed at Following"
            lblMonOrCpuNo.Text = "A Monitor no. " & strNo & " is already installed at following location. "
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet

            '<INV_RPTMISCHARDWARE_OUTPUT><MISCHARDWARE LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' EQUIPMENTTYPE='' EQUIPMENTNO='' ONLINE_STATUS='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTMISCHARDWARE_OUTPUT>
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_RPTPCINSTALL_INPUT>")
            ' objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = strNo
            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
            'objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Request.QueryString("EQUIPMENTTYPE").ToString
            ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("PCINSTALL") IsNot Nothing Then
                    pnlNo.Visible = True
                    pnlEnableorDisable.Enabled = False
                    SaveStatus.Value = "MONNO"
                    gvInstall.DataSource = ds.Tables("PCINSTALL").DefaultView
                    gvInstall.DataBind()


                Else
                    gvInstall.DataSource = Nothing
                    gvInstall.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        pnlNo.Visible = False
        If SaveStatus.Value = "CPUNO" Then
            If hdblnSNoOverride.Value = "1" Then
                hdAllowSaveForCpuNo.Value = 1
                pnlEnableorDisable.Enabled = True
                btnSave_Click(sender, e)
                Exit Sub
            Else
                hdAllowSaveForCpuNo.Value = "0"
                lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                SaveStatus.Value = "CPUNO"
                If drpCpuNo.Visible = True Then
                    drpCpuNo.Focus()
                End If
                If txtCpuNo.Visible = True Then
                    txtCpuNo.Focus()
                End If
                pnlEnableorDisable.Enabled = True
                Exit Sub
            End If
          
        End If
        If SaveStatus.Value = "MONNO" Then
            If hdblnSNoOverride.Value = "1" Then
                hdAllowSaveForMonNo.Value = 1
                pnlEnableorDisable.Enabled = True
                btnSave_Click(sender, e)
                Exit Sub
            Else
                lblError.Text = "You don't have enough rights to install h/w without a valid Monitor no."
                SaveStatus.Value = "MONNO"
                If drpMonNo.Visible = True Then
                    drpMonNo.Focus()
                End If
                If txtMonNo.Visible = True Then
                    txtMonNo.Focus()
                End If
                pnlEnableorDisable.Enabled = True
                Exit Sub
            End If
          
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        pnlNo.Visible = False
      
        If SaveStatus.Value = "CPUNO" Then

            hdAllowSaveForCpuNo.Value = 0
            pnlEnableorDisable.Enabled = True
            Exit Sub
        End If
        If SaveStatus.Value = "MONNO" Then
            pnlEnableorDisable.Enabled = True

            hdAllowSaveForMonNo.Value = 0
            Exit Sub
        End If
    End Sub
    Private Sub FillCPUNO()
        Try
            If drpCpuType.SelectedIndex >= 0 Then
                If drpCpuType.SelectedValue <> "" Then
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim li As New ListItem
                    Dim ds As New DataSet
                    ds = New DataSet
                    drpCpuNo.Items.Clear()
                    ' @ This is Used For Getting the All Mon No on the basis of Challan No
                    objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                    ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "MON"
                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpCpuType.SelectedValue
                    objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        Dim dv As DataView
                        dv = New DataView
                        dv = ds.Tables("DETAILS").DefaultView

                        Dim i As Integer
                        For i = 0 To dv.Count - 1
                            Dim Item As New ListItem
                            'Item = New ListItem(dv(i)("SERIALNUMBER").ToString, dv(i)("SERIALNUMBER").ToString)
                            Item = New ListItem(dv(i)("VENDORSR_NUMBER").ToString, dv(i)("VENDORSR_NUMBER").ToString)
                            If Not drpCpuNo.Items.Contains(Item) Then
                                drpCpuNo.Items.Insert(drpCpuNo.Items.Count, Item)
                            End If
                        Next
                        drpCpuNo.DataBind()
                        drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    Else
                        drpCpuNo.Items.Clear()
                        drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                        drpCpuNo.DataBind()
                    End If
                Else
                    drpCpuNo.Items.Clear()
                    drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    drpCpuNo.DataBind()
                End If
            End If

        Catch ex As Exception
        Finally
            Dim li2 As New ListItem
            li2 = drpCpuNo.Items.FindByValue("NA")
            If li2 Is Nothing Then
                drpCpuNo.Items.Insert(drpCpuNo.Items.Count, New ListItem("NA", "NA"))

            End If
        End Try
    End Sub
    Private Sub FillMonNo()
        Try
            If drpMonType.SelectedIndex >= 0 Then
                If drpMonType.SelectedValue <> "" Then
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim li As New ListItem
                    Dim ds As New DataSet
                    drpMonNo.Items.Clear()
                    ds = New DataSet
                    ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                    objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                    ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "MON"
                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpMonType.SelectedValue
                    objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)


                        Dim dv As DataView
                        dv = New DataView
                        dv = ds.Tables("DETAILS").DefaultView

                        Dim i As Integer
                        For i = 0 To dv.Count - 1
                            Dim Item As New ListItem
                            'Item = New ListItem(dv(i)("SERIALNUMBER").ToString, dv(i)("SERIALNUMBER").ToString)
                            Item = New ListItem(dv(i)("VENDORSR_NUMBER").ToString, dv(i)("VENDORSR_NUMBER").ToString)
                            If Not drpMonNo.Items.Contains(Item) Then
                                drpMonNo.Items.Insert(drpMonNo.Items.Count, Item)
                            End If
                        Next
                        drpMonNo.Items.Insert(0, New ListItem("--Select One--", ""))
                        drpMonNo.DataBind()
                    Else
                        drpMonNo.Items.Insert(0, New ListItem("--Select One--", ""))
                        drpMonNo.DataBind()
                    End If
                Else
                    drpMonNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    drpMonNo.DataBind()

                End If
            End If
        Catch ex As Exception
        Finally
            Dim li2 As New ListItem
            li2 = drpMonNo.Items.FindByValue("NA")
            If li2 Is Nothing Then
                drpMonNo.Items.Insert(drpMonNo.Items.Count, New ListItem("NA", "NA"))
            End If

        End Try
      

    End Sub

    Private Sub EnableControlsForValidateChallan()
        drpOrderNo.Enabled = True
        drpCpuType.Enabled = True
        drpCpuNo.Enabled = True
        drpCpuNo.Enabled = True
        drpMonType.Enabled = True
        drpMonNo.Enabled = True
        drpKeyType.Enabled = True
        txtKeyboardNo.Enabled = True
        drpMouseType.Enabled = True
        txtMouseNo.Enabled = True
        txtCdrNo.Enabled = True
        txtRam.Enabled = True
        txtRem.Enabled = True
        btnSave.Enabled = True
        txtOrderNo.Enabled = True
        txtOrderNo.Enabled = True
        txtCpuNo.Enabled = True
        txtMonNo.Enabled = True
    End Sub
    Private Sub DisableControlsForValidateChallan()
        drpOrderNo.Enabled = False
        drpCpuType.Enabled = False
        drpCpuNo.Enabled = False
        drpCpuNo.Enabled = False
        drpMonType.Enabled = False
        drpMonNo.Enabled = False
        drpKeyType.Enabled = False
        txtKeyboardNo.Enabled = False
        drpMouseType.Enabled = False
        txtMouseNo.Enabled = False
        txtCdrNo.Enabled = False
        txtRam.Enabled = False
        txtRem.Enabled = False
        btnSave.Enabled = False
        txtOrderNo.Enabled = False
        txtCpuNo.Enabled = False
        txtMonNo.Enabled = False
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString(), False)
    End Sub
End Class
