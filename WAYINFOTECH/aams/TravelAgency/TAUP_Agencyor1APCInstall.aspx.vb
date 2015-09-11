#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region

Partial Class TravelAgency_TAUP_Agencyor1APCInstall
    Inherits System.Web.UI.Page


#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
    Dim strResult As String
#End Region


#Region "Code for Filter "

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad

    End Sub
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

        lblError.Text = ""
        Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyPcInstallation"
        ' This code is used for Expiration of Page From Cache
        objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        Try

            btnSave.Attributes.Add("onclick", "return AgencyPCMandatory();")
            'drpOrderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            txtorderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            txtChallanNo.Attributes.Add("onchange", "return ResetSaveForChallan();")
            txtCpuNo.Attributes.Add("onchange", "return ResetSaveForCpuNo();")
            txtMonNo.Attributes.Add("onchange", "return ResetSaveForMonNo();")
            drpCpuNo.Attributes.Add("onchange", "return ResetSaveForCpuNo();")
            drpMonNo.Attributes.Add("onchange", "return ResetSaveForMonNo();")

            If (Request("txtInstallDate") IsNot Nothing) Then
                txtInstallDate.Text = Request("txtInstallDate").Trim()
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
            ' btnSave.Attributes.Add("onclick", "return PCMandatory();")


            If (Not IsPostBack) Then
                ' ************************************************

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

                If (Request.QueryString("Msg") = "U") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                bindControls()
                '*************************************************
            End If

            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") = "R" Then
                    Literal1.Text = "PC Installation"
                    Literal2.Text = "Replace PC Installation"
                    dtIR.Text = "Date of Replacement"
                    btnSave.Text = "Replace"
                    hdRep.Value = "1"
                End If
                If Request.QueryString("Action") = "U" Then
                    Literal1.Text = "PC Installation"
                    Literal2.Text = "Modify PC Installation"
                    btnSave.Text = "Save"
                    dtIR.Text = "Date  of   Installation"
                End If
                If (Not IsPostBack) Then
                    If Request.QueryString("ROWID") IsNot Nothing Then
                        ViewPcInstllation()
                    End If
                End If

            Else
                lblError.Text = "Invalid Parameter"
            End If

            If Not IsPostBack Then
                If btnValidate.Visible = True Then
                    btnSave.Enabled = False
                    pnlEnableorDisable.Enabled = False
                End If
                If (Request.QueryString("Action") = "R") Then
                    txtChallanNo.Text = ""
                    txtInstallDate.Text = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
    '    Return strResult
    'End Function
    'Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
    '    Try
    '        lblError.Text = ""
    '        Dim id As String
    '        id = eventArgument
    '        Dim objbzChallan As New AAMS.bizInventory.bzChallan
    '        Dim objOutputXml As New XmlDocument
    '        Dim objInputXml As New XmlDocument
    '        If id.Split("|").GetValue(1) = "O" Then
    '            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
    '            objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region></UP_SEARCHORDER_INPUT>")
    '            With objInputXml.DocumentElement
    '                If (Session("Action") IsNot Nothing) Then
    '                    If (Session("Action").ToString().Split("|").Length >= 2) Then
    '                        Lcode = Session("Action").ToString().Split("|").GetValue(1)
    '                        .SelectSingleNode("LCODE").InnerText = Lcode
    '                    End If
    '                End If
    '                .SelectSingleNode("ORDER_NUMBER").InnerText = id.Split("|").GetValue(0)
    '            End With
    '            objOutputXml = objbzOrder.Search(objInputXml)
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                strResult = "O1|" & id & "|"
    '                hdOrderNoExist.Value = "1"
    '            Else
    '                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
    '                    strResult = "0"
    '                Else
    '                    strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '                End If
    '            End If
    '        ElseIf id.Split("|").GetValue(1) = "C" Then


    '            '<INV_SEARCH_CHALLAN_OUTPUT><CHALLAN ChallanID='' ChallanNumber ='' CreationDate='' ChallanDate='' ChallanCategory='' ChallanType='' SupplierName='' AgencyName='' OfficeID='' GodownName='' RGodownName='' LCODE='' /><Errors Status=''><Error Code='' Description='' /></Errors></INV_SEARCH_CHALLAN_OUTPUT>

    '            'objInputXml.LoadXml("<INV_GETCHALLANSERIAL_INPUT><SerialNo></SerialNo><Type></Type></INV_GETCHALLANSERIAL_INPUT>")
    '            objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
    '            objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = id.Split("|").GetValue(0)
    '            objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
    '            objOutputXml = objbzChallan.Search(objInputXml)
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                hdChallanNoExist.Value = "1"
    '                strResult = "C1|||"
    '                Dim strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
    '                Dim strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
    '                If (Session("Action") IsNot Nothing) Then
    '                    If (Session("Action").ToString().Split("|").Length >= 2) Then
    '                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value <> Session("Action").ToString().Split("|").GetValue(1) Then
    '                            strResult = "C1|" & id.Split("|").GetValue(0) & "|" & strAgencyName & "|" & strOfficeID
    '                            ' hdChallanNoExist.Value = "1"

    '                            '*****************************
    '                            ' @ If Challan No exist then Show Dropdown for CPNO and Mon No other wise show Textbox for CPNO and Mon No
    '                            ' LoadAllEuipmentCodeValue()

    '                            '*****************************


    '                        End If
    '                    Else
    '                        strResult = "-1|Session Expire||"
    '                    End If
    '                End If

    '                ' @ This is Used For Getting the All CPu No on the basis of Challan No
    '                objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
    '                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = id.Split("|").GetValue(0)
    '                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
    '                ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "CPU"
    '                'objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpCpuType.SelectedValue
    '                objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
    '                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                    strResult = strResult & "|" & objOutputXml.OuterXml
    '                Else
    '                    strResult = strResult & "|"
    '                End If

    '                ' @ This is Used For Getting the All Mon No on the basis of Challan No
    '                objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
    '                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = id.Split("|").GetValue(0)
    '                ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "MON"
    '                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
    '                '  objInputX'ml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpMonType.SelectedValue
    '                objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
    '                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                    strResult = strResult & "|" & objOutputXml.OuterXml
    '                Else
    '                    strResult = strResult & "|"
    '                End If

    '            Else
    '                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
    '                    strResult = "C0"
    '                Else
    '                    strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '                End If
    '            End If
    '            ' @ Code Check for Keyboard No
    '        ElseIf id.Split("|").GetValue(1) = "K" Then


    '        ElseIf id.Split("|").GetValue(1) = "CT" Then

    '            'If hdEquipCode.Value <> "" Then
    '            '    Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
    '            '    If arEquipCode.Contains(id.Split("|").GetValue(2)) Then
    '            '        strResult = "CT10|"
    '            '        Exit Sub
    '            '    End If
    '            'End If
    '            '<INV_RPTMISCHARDWARE_OUTPUT><MISCHARDWARE LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' EQUIPMENTTYPE='' EQUIPMENTNO='' ONLINE_STATUS='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTMISCHARDWARE_OUTPUT>
    '            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
    '            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = id.Split("|").GetValue(0)
    '            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
    '            'objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Request.QueryString("EQUIPMENTTYPE").ToString
    '            ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
    '            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)

    '            ' objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)
    '            '<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                strResult = "CT11|" & id.Split("|").GetValue(0) & "|" & id.Split("|").GetValue(2)
    '            Else
    '                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
    '                    strResult = "CT12|"
    '                Else
    '                    strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '                End If
    '            End If


    '        ElseIf id.Split("|").GetValue(1) = "MT" Then

    '            'If hdEquipCode.Value <> "" Then
    '            '    Dim arEquipCode As New ArrayList(hdEquipCode.Value.Split("|"))
    '            '    If arEquipCode.Contains(id.Split("|").GetValue(2)) Then
    '            '        strResult = "MT10|"
    '            '        Exit Sub
    '            '    End If
    '            'End If
    '            '<INV_RPTMISCHARDWARE_OUTPUT><MISCHARDWARE LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' EQUIPMENTTYPE='' EQUIPMENTNO='' ONLINE_STATUS='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTMISCHARDWARE_OUTPUT>
    '            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
    '            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = id.Split("|").GetValue(0)
    '            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
    '            'objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Request.QueryString("EQUIPMENTTYPE").ToString
    '            ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
    '            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)

    '            ' objOutputXml = objbzChallan.MISCHardwareReport(objInputXml)
    '            '<INV_SEARCHSTOCKDETAILS_OUTPUT><DETAILS PRODUCTID='' AOFFICE='' EGROUP_CODE='' EQUIPMENT_CODE='' PRODUCTNAME='' SERIALNUMBER='' VENDORSR_NUMBER='' STATUS='' CHALLANNUMBER='' CREATIONDATE='' GODOWNID='' CHALLANCATEGORY='' SUPPLIERID='' LCODE='' OUTTO='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_SEARCHSTOCKDETAILS_OUTPUT>
    '            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '                strResult = "MT11|" & id.Split("|").GetValue(0) & "|" & id.Split("|").GetValue(2)
    '            Else
    '                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
    '                    strResult = "MT12|"
    '                Else
    '                    strResult = "-1|" & objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '        strResult = "-1|" & ex.Message
    '    End Try

    'End Sub
    Private Sub ViewPcInstllation()
        If (Not Request.QueryString("Action") = Nothing) Then
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
            objInputXml.LoadXml("<UP_TA_VIEW_PCINSTALLATION_INPUT> <ROWID /> </UP_TA_VIEW_PCINSTALLATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = Request.QueryString("ROWID")
            'Here Back end Method Call
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") = "R" Then
                    objOutputXml = objbzPCInstallation.ViewReplacement(objInputXml)
                Else
                    objOutputXml = objbzPCInstallation.View(objInputXml)
                End If
              
            End If
            '  objOutputXml = objbzPCInstallation.View(objInputXml)
            ' objOutputXml.LoadXml("<UP_TA_VIEW_PCINSTALLATION_OUTPUT> <DETAIL ROWID='44' LCODE='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='4454' REMARKS='rt' CHALLANNUMBER='' CDRNO='' PCTYPE='0' /> <Errors Status='FALSE'> <Error Code='' Description='' /> </Errors> </UP_TA_VIEW_PCINSTALLATION_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindPcInstllation(objOutputXml)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub
#Region "Call BindPcInstllation For Binding Data in Controls"
    Sub BindPcInstllation(ByVal objOutputXmlData As XmlDocument)

        Try

            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim li As New ListItem
            Dim ds As New DataSet
            Dim dv As DataView
            Dim i As Integer
            '<UP_TA_VIEW_PCINSTALLATION_OUTPUT>
            ' <DETAIL ROWID='44' LCODE='' DATE='' CPUTYPE='' CPUNO='' 
            'MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO=''.
            ' OrderNumber='4454' REMARKS='rt' CHALLANNUMBER='' CDRNO='' PCTYPE='0' /> 
            '<Errors Status='FALSE'> <Error Code='' Description='' /> </Errors> </UP_TA_VIEW_PCINSTALLATION_OUTPUT>

            LoadAllEuipmentCodeValue()
            hdChallnType.Value = "0"

            txtRInstallDate.Text = objeAAMS.ConvertDateBlank(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText)
            li = drpRCpuType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText)
            If li IsNot Nothing Then
                drpRCpuType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
            End If
            txtRCpuNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
            li = drpRMonType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText)
            If li IsNot Nothing Then
                drpRMonType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
            End If
            txtRMonNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerXml
            li = drpRKeyType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText)
            If li IsNot Nothing Then
                drpRKeyType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText
            End If
            txtRKeyboardNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText

            li = drpRMouseType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText)
            If li IsNot Nothing Then
                drpRMouseType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText
            End If

            txtRMouseNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText
            txtRorderNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText
            txtRRem.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText
            txtRRam.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText
            txtRChallanNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText
            txtRCdrNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText



            txtInstallDate.Text = "" 'objeAAMS.ConvertDateBlank(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").InnerText)

            li = drpCpuType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText)
            If li IsNot Nothing Then
                drpCpuType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
            End If


            txtCpuNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText

            li = drpMonType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText)
            If li IsNot Nothing Then
                drpMonType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
            End If

            txtMonNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerXml

            li = drpKeyType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText)
            If li IsNot Nothing Then
                drpKeyType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").InnerText
            End If
            txtKeyboardNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").InnerText

            li = drpMouseType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText)
            If li IsNot Nothing Then
                drpMouseType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").InnerText
            End If
            txtMouseNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").InnerText
            txtorderNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText
            txtRem.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").InnerText
            txtChallanNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText
            txtCdrNo.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").InnerText
            txtRam.Text = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").InnerText

            li = drpCpuNo.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText)
            If li IsNot Nothing Then
                drpCpuNo.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
            End If
            li = drpMonNo.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText)
            If li IsNot Nothing Then
                drpMonNo.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText
            End If

            'hdCpuNo.Value = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
            'hdMonNo.Value = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerXml
            If Request.QueryString("PCTYPE") = "0" And Request.QueryString("Action") = "U" Then
                btnValidate.Visible = False
            End If

            If Request.QueryString("Action") = "R" Then
                txtInstallDate.Text = txtRInstallDate.Text
            End If

            If Request.QueryString("PCTYPE") IsNot Nothing Then
                If Request.QueryString("PCTYPE") = "0" And Request.QueryString("Action") = "U" Then
                    drpCpuType.Enabled = False
                    drpMonType.Enabled = False
                    drpKeyType.Enabled = False
                    drpMouseType.Enabled = False
                    txtChallanNo.Enabled = False
                    hdAllowSaveForChallan.Value = "1"
                End If
            Else
                'If txtChallanNo.Text = "" Or txtChallanNo.Text = "NA" Then
                '    txtChallanNo.Enabled = False
                '    hdAllowSaveForChallan.Value = "1"
                'End If
            End If

            '*************************************************************
            'getting value  for challan No exist or not?
            If txtChallanNo.Text.Trim.Length > 0 Then
                If txtChallanNo.Text.Trim <> "0" Then
                    ' objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo></INV_SEARCH_CHALLAN_INPUT>")
                    objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                    objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                    objOutputXml = objbzChallan.Search(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then
                            hdChallanNoExist.Value = "1"
                            hdAllowSaveForChallan.Value = "1"
                            ' txtChallanNo.Enabled = False

                        End If
                        Dim ChallanType As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"
                        hdChallnType.Value = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"

                        Dim strAgencyName As String
                        strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                        Dim strOfficeID As String
                        strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then

                            'hdChallanNoExist.Value = "1"

                            ds = New DataSet
                            ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text

                            If (ChallanType = "Receive") Then
                                If Request.QueryString("Action") = "R" Then
                                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                                Else
                                    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                End If

                            Else

                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                            End If


                            'objInputXml.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpCpuType.SelectedValue
                            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                                hdChallaDetails.Value = "1"
                                objXmlReader = New XmlNodeReader(objOutputXml)
                                ds.ReadXml(objXmlReader)



                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                ' @  Fill ORDERNO

                                ' drpOrderNo.Items.Clear()
                                'drpOrderNo.Visible = True
                                txtorderNo.Visible = True

                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "ORDERNUMBER<>'' "

                                For i = 0 To dv.Count - 1
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(i)("ORDERNUMBER").ToString, dv(i)("ORDERNUMBER").ToString)
                                    'If Not drpOrderNo.Items.Contains(Item) Then
                                    '    drpOrderNo.Items.Insert(drpOrderNo.Items.Count, Item)
                                    'End If
                                Next
                                ''drpOrderNo.Items.Insert(0, New ListItem("--Select One--", ""))
                                ' drpOrderNo.DataBind()
                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  
                                'li = drpOrderNo.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText)
                                ' If li IsNot Nothing Then
                                ' drpOrderNo.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").InnerText
                                'End If

                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                ' @  Fill CPUTYPE

                                drpCpuType.Items.Clear()

                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='CPU' or EGROUP_CODE='LAP'" 'EGROUP_CODE='CPU'"


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
                                '    drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                                'End If
                                Dim li2 As New ListItem
                                li2 = drpCpuType.Items.FindByValue("CPP")
                                If li2 Is Nothing Then
                                    drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                                End If


                                drpCpuType.Items.Insert(0, New ListItem("--Select One--", ""))
                                'drpCpuType.DataBind()

                                li = drpCpuType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText)
                                If li IsNot Nothing Then
                                    drpCpuType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
                                End If

                                drpCpuNo.Visible = True
                                txtCpuNo.Visible = False

                                If dv.Count = 0 Or (drpCpuType.SelectedValue = "CPP" And dv.Count = 1) Then
                                    drpCpuNo.Visible = False
                                    txtCpuNo.Visible = True

                                End If

                                If (ChallanType = "Receive") Then
                                    If Request.QueryString("Action") = "R" Then
                                        FillCPUNO("R")
                                    Else
                                        FillCPUNO()
                                    End If
                                Else
                                    FillCPUNO()
                                End If



                                li = drpCpuNo.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText)
                                If li IsNot Nothing Then
                                    drpCpuNo.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").InnerText
                                End If

                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  


                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                ' @  Fill MONTYPE
                                drpMonType.Items.Clear()

                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='MON' or EGROUP_CODE='TFT'" ' "EGROUP_CODE='MON'"
                                For i = 0 To dv.Count - 1
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(i)("EQUIPMENT_CODE").ToString, dv(i)("EQUIPMENT_CODE").ToString)
                                    If Not drpMonType.Items.Contains(Item) Then
                                        drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                    End If
                                Next

                                'If dv.Count = 0 Then
                                '    drpMonType.Items.Insert(0, New ListItem("MMP", "MMP"))
                                'End If
                                Dim li3 As New ListItem
                                li3 = drpMonType.Items.FindByValue("MMP")
                                If li3 Is Nothing Then
                                    drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                                End If



                                drpMonType.Items.Insert(0, New ListItem("--Select One--", ""))
                                ' drpMonType.DataBind()

                                li = drpMonType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText)
                                If li IsNot Nothing Then
                                    drpMonType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
                                End If

                                drpMonNo.Visible = True
                                txtMonNo.Visible = False

                                If dv.Count = 0 Or (drpMonType.SelectedValue = "MMP" And dv.Count = 1) Then
                                    drpMonNo.Visible = False
                                    txtMonNo.Visible = True
                                End If


                                If (ChallanType = "Receive") Then
                                    If Request.QueryString("Action") = "R" Then
                                        FillMonNo("R")
                                    Else
                                        FillMonNo()
                                    End If
                                Else
                                    FillMonNo()
                                End If

                                li = drpMonNo.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText)
                                If li IsNot Nothing Then
                                    drpMonNo.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").InnerText
                                End If

                                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            Else
                                'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value


                                hdChallaDetails.Value = "0"
                                drpCpuNo.Visible = False
                                txtCpuNo.Visible = True
                                drpMonNo.Visible = False
                                txtMonNo.Visible = True


                            End If
                        Else
                            'drpOrderNo.Visible = False
                            txtorderNo.Visible = True
                            drpCpuNo.Visible = False
                            txtCpuNo.Visible = True
                            drpMonNo.Visible = False
                            txtMonNo.Visible = True

                            drpCpuType.DataSource = Nothing
                            drpCpuType.DataBind()

                            objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                            ' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                            drpKeyType.DataSource = Nothing
                            drpKeyType.DataBind()
                            objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
                            ' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                            drpMonType.DataSource = Nothing
                            drpMonType.DataBind()

                            objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                            drpMouseType.DataSource = Nothing
                            drpMouseType.DataBind()
                            ' drpMonType.Items.Insert(0, New ListItem("MMP", "MMP"))
                            objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                            li = drpCpuType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText)
                            If li IsNot Nothing Then
                                drpCpuType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
                            End If
                            li = drpMonType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText)
                            If li IsNot Nothing Then
                                drpMonType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
                            End If
                        End If
                    Else
                        drpCpuType.DataSource = Nothing
                        drpCpuType.DataBind()
                        objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                        ' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                        drpKeyType.DataSource = Nothing
                        drpKeyType.DataBind()

                        objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
                        ' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                        drpMonType.DataSource = Nothing
                        drpMonType.DataBind()

                        objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                        ' drpMonType.Items.Insert(0, New ListItem("MMP", "MMP"))
                        drpMouseType.DataSource = Nothing
                        drpMouseType.DataBind()
                        objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                        drpCpuNo.Visible = False
                        txtCpuNo.Visible = True
                        'drpOrderNo.Visible = False
                        txtorderNo.Visible = True
                        drpMonNo.Visible = False
                        txtMonNo.Visible = True
                        li = drpCpuType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText)
                        If li IsNot Nothing Then
                            drpCpuType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").InnerText
                        End If
                        li = drpMonType.Items.FindByValue(objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText)
                        If li IsNot Nothing Then
                            drpMonType.SelectedValue = objOutputXmlData.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").InnerText
                        End If

                        'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If

                End If
            End If


            '*************************************************************
            '*********************************************
            '@ Now Allow available Writes

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If Request.QueryString("PCTYPE") IsNot Nothing Then
                If Request.QueryString("PCTYPE") = "0" And Request.QueryString("Action") = "U" Then
                    drpCpuType.Enabled = False
                    drpMonType.Enabled = False
                    drpKeyType.Enabled = False
                    drpMouseType.Enabled = False
                    txtChallanNo.Enabled = False
                    drpMonNo.Visible = False
                    drpCpuNo.Visible = False
                    txtMonNo.Visible = True
                    txtCpuNo.Visible = True
                End If
            Else
                'If txtChallanNo.Text = "" Or txtChallanNo.Text = "NA" Then
                '    txtChallanNo.Enabled = False
                '    hdAllowSaveForChallan.Value = "1"
                'End If
            End If

            If txtChallanNo.Text.Trim.Length = 0 Or txtChallanNo.Text.Trim = "0" Then
                drpMonNo.Visible = False
                drpCpuNo.Visible = False
                txtMonNo.Visible = True
                txtCpuNo.Visible = True
            End If
            If hdChallanNoExist.Value <> "1" Then
                drpMonNo.Visible = False
                drpCpuNo.Visible = False
                txtMonNo.Visible = True
                txtCpuNo.Visible = True
            Else
                drpMonNo.Visible = True
                drpCpuNo.Visible = True
                txtMonNo.Visible = False
                txtCpuNo.Visible = False
                If (drpMonType.SelectedValue = "MMP") Then
                    drpMonNo.Visible = False
                    txtMonNo.Visible = True

                End If
                If (drpCpuType.SelectedValue = "CPP") Then
                    drpCpuNo.Visible = False
                    txtCpuNo.Visible = True

                End If

            End If
            If hdChallaDetails.Value = "0" Then
                drpCpuNo.Visible = False
                txtCpuNo.Visible = True
                drpMonNo.Visible = False
                txtMonNo.Visible = True
            End If


            If Request.QueryString("PCTYPE") IsNot Nothing Then
                If Request.QueryString("PCTYPE") = "0" Then
                    Dim li2 As New ListItem
                    li2 = drpCpuType.Items.FindByValue("CPP")
                    If li2 IsNot Nothing Then
                        drpCpuType.SelectedValue = "CPP"
                    Else
                        drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                        drpCpuType.SelectedValue = "CPP"
                    End If
                    li2 = drpMonType.Items.FindByValue("MMP")
                    If li2 IsNot Nothing Then
                        drpMonType.SelectedValue = "MMP"
                    Else
                        drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                        drpMonType.SelectedValue = "MMP"
                    End If
                    li2 = drpKeyType.Items.FindByValue("KBP")
                    If li2 IsNot Nothing Then
                        drpKeyType.SelectedValue = "KBP"
                    Else
                        drpKeyType.Items.Insert(drpKeyType.Items.Count, New ListItem("KBP", "KBP"))
                        drpKeyType.SelectedValue = "KBP"
                    End If
                    li2 = drpMouseType.Items.FindByValue("MSP")
                    If li2 IsNot Nothing Then
                        drpMouseType.SelectedValue = "MSP"
                    Else
                        drpMouseType.Items.Insert(drpMouseType.Items.Count, New ListItem("MSP", "MSP"))
                        drpMouseType.SelectedValue = "MSP"
                    End If
                End If
            End If


            Dim li9 As ListItem
            If drpCpuNo.Visible = True Then
                li9 = drpCpuNo.Items.FindByValue(txtRCpuNo.Text)
                If li9 Is Nothing Then
                    drpCpuNo.Items.Insert(drpCpuNo.Items.Count, New ListItem(txtRCpuNo.Text, txtRCpuNo.Text))
                End If
                drpCpuNo.SelectedValue = txtRCpuNo.Text
            Else
                txtCpuNo.Text = txtRCpuNo.Text
            End If
            If drpMonNo.Visible = True Then
                li9 = drpMonNo.Items.FindByValue(txtRMonNo.Text)
                If li9 Is Nothing Then
                    drpMonNo.Items.Insert(drpMonNo.Items.Count, New ListItem(txtRMonNo.Text, txtRMonNo.Text))
                End If
                drpMonNo.SelectedValue = txtRMonNo.Text
            Else
                txtMonNo.Text = txtRMonNo.Text
            End If
            li9 = drpCpuType.Items.FindByValue(drpRCpuType.SelectedValue)
            If li9 Is Nothing Then
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem(drpRCpuType.Text, drpRCpuType.SelectedValue))
            End If
            drpCpuType.SelectedValue = drpRCpuType.SelectedValue


            li9 = drpMonType.Items.FindByValue(drpRMonType.SelectedValue)
            If li9 Is Nothing Then
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem(drpRMonType.Text, drpRMonType.SelectedValue))
            End If
            drpMonType.SelectedValue = drpRMonType.SelectedValue

            li9 = drpKeyType.Items.FindByValue(drpRKeyType.SelectedValue)
            If li9 Is Nothing Then
                drpKeyType.Items.Insert(drpKeyType.Items.Count, New ListItem(drpRKeyType.Text, drpRKeyType.SelectedValue))
            End If
            drpKeyType.SelectedValue = drpRKeyType.SelectedValue


            li9 = drpMouseType.Items.FindByValue(drpRMouseType.SelectedValue)
            If li9 Is Nothing Then
                drpMouseType.Items.Insert(drpMouseType.Items.Count, New ListItem(drpRMouseType.Text, drpRMouseType.SelectedValue))
            End If
            drpMouseType.SelectedValue = drpRMouseType.SelectedValue


        End Try
    End Sub
#End Region
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            pnlMsg.Visible = False
            pnlNo.Visible = False
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objInputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml As New XmlDocument, objOutputXml2 As New XmlDocument
            '            Dim objXmlReader As XmlNodeReader
            Dim li As New ListItem
            Dim ds As New DataSet
            Dim objPCReplacement As New AAMS.bizTravelAgency.bzPCReplacement
            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation

            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then
                        '###########################################
                        ' Check Validation for Order No.
                        ' @ If Any Cause hdAllowSaveForOrder<>1 then can't save for a proper validation on Order NO.
                        If txtorderNo.Text.Trim() <> txtRorderNo.Text.Trim() Then

                            If hdAllowSaveForOrder.Value <> "1" Then

                                If txtorderNo.Visible = True Then
                                    If txtorderNo.Text.Trim().Length = 0 Then
                                        pnlEnableorDisable.Enabled = False
                                        btnSave.Enabled = False

                                        lblConfirm.Text = "Order number is blank. Do you want to continue?"
                                        pnlMsg.Visible = True
                                        ' PnlDetails.Visible = False
                                        SaveStatus.Value = "Order"
                                        txtorderNo.Focus()
                                        Exit Sub
                                    End If

                                    If txtorderNo.Text.Trim().Length > 0 Then
                                        If txtorderNo.Text.Trim() = "0" Then
                                            hdAllowSaveForOrder.Value = "0"
                                            pnlEnableorDisable.Enabled = False
                                            btnSave.Enabled = False
                                            lblConfirm.Text = "Order number is 0. Do you want to continue?"
                                            pnlMsg.Visible = True
                                            SaveStatus.Value = "Order"
                                            txtorderNo.Focus()
                                            Exit Sub

                                        End If
                                    End If
                                    '*************************************************************
                                    ' Gaeeing value for order no exist or not?
                                    If txtorderNo.Text.Trim().Length > 0 Then
                                        If txtorderNo.Text.Trim() <> "0" Then

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
                                                .SelectSingleNode("ORDER_NUMBER").InnerText = txtorderNo.Text
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
                                                btnSave.Enabled = False
                                                SaveStatus.Value = "Order"
                                                lblConfirm.Text = "Given order number does not exist. Do you want to continue?"
                                                txtorderNo.Focus()
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                End If

                                '*************************************************************
                                ' Getting value for order no exist or not?
                            End If
                        End If

                        '###########################################
                        ' @ mandaory for cpu type.
                        '########################################
                        If drpCpuType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "CPU Type is mandaory."
                            SaveStatus.Value = "CPUTYPE"
                            drpCpuType.Focus()
                            Exit Sub
                        End If
                        '###########################################
                        ' @ end of mandaory for cpu type.
                        '########################################


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
                                If (drpCpuNo.SelectedValue.Trim() <> txtRCpuNo.Text.Trim()) Then 'Or (drpCpuType.SelectedValue.Trim() <> drpRCpuType.SelectedValue.Trim()) Or (txtChallanNo.Text.Trim() <> txtRChallanNo.Text.Trim()) Then
                                    If drpCpuNo.SelectedValue = "NA" Then
                                        If drpCpuType.SelectedValue <> "CPP" Then
                                            If hdblnSNoOverride.Value = "0" And hdReplacementChallanExist.Value <> "1" Then
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
                                            ' If Request.QueryString("Action") <> "R" Then
                                            If drpCpuNo.SelectedValue.Trim() <> txtRCpuNo.Text.Trim() Then

                                            End If


                                            bindGridPanelForCpuNo(drpCpuNo.SelectedValue)
                                            drpCpuNo.Focus()
                                            Exit Sub
                                            'End If
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


                            End If

                            If drpCpuNo.Visible = False Then
                                If (txtCpuNo.Text.Trim() <> txtRCpuNo.Text.Trim()) Then 'Or (drpCpuType.SelectedValue.Trim() <> drpRCpuType.SelectedValue.Trim()) Or (txtChallanNo.Text.Trim() <> txtRChallanNo.Text.Trim()) Then
                                    If txtCpuNo.Text.Trim().ToUpper = "NA" Then
                                        If drpCpuType.SelectedValue <> "CPP" Then
                                            If hdblnSNoOverride.Value = "0" And hdReplacementChallanExist.Value <> "1" Then
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
                                        '@ New Code
                                        If txtCpuNo.Text.Trim().ToUpper() <> "NA" Then
                                            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                            '@ Check Whether Entered CPU No is out for selected agency or not
                                            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                                   

                                            ds = New DataSet

                                            ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                                            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                                            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = "" 'txtChallanNo.Text

                                            If Request.QueryString("Action") = "R" Then
                                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                            Else
                                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                            End If
                                            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                                            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtCpuNo.Text
                                            'If hdReplacementChallanExist.Value = "1" Then ' SPECIAL CASE FOR STATUS = 9
                                            ' objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                                            'objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "9"
                                            'objOutputXml = objbzChallan.SearchStockReplacementDetails(objInputXml)
                                            'Else
                                            objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                                            'End If
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
                                            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtCpuNo.Text
                                            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                                bindGridPanelForCpuNo(txtCpuNo.Text)
                                                If txtCpuNo.Visible = True Then
                                                    txtCpuNo.Focus()
                                                End If
                                                Exit Sub
                                            Else
                                                If hdblnSNoOverride.Value = "0" And hdReplacementChallanExist.Value <> "1" Then
                                                    lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
                                                    SaveStatus.Value = "CPUNO"
                                                    If txtCpuNo.Visible = True Then
                                                        txtCpuNo.Focus()
                                                    End If
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
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
                        '########################################


                        '###########################################
                        'End of Check Validation for Mon No.

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
                                If (drpMonNo.SelectedValue.Trim() <> txtRMonNo.Text.Trim()) Then ' Or (drpMonType.SelectedValue.Trim() <> drpRMonType.SelectedValue.Trim()) Or (txtChallanNo.Text.Trim() <> txtRChallanNo.Text.Trim()) Then
                                    If drpMonNo.SelectedValue = "NA" Then
                                        If drpMonType.SelectedValue <> "MMP" Then
                                            If hdblnSNoOverride.Value = "0" And hdReplacementChallanExist.Value <> "1" Then
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
                                        objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = drpMonNo.SelectedValue
                                        objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                        objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                            bindGridPanelForMonNo(drpMonNo.SelectedValue)
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

                            If drpMonNo.Visible = False Then
                                If (txtMonNo.Text.Trim() <> txtRMonNo.Text.Trim()) Then 'Or (drpMonType.SelectedValue.Trim() <> drpRMonType.SelectedValue.Trim()) Or (txtChallanNo.Text.Trim() <> txtRChallanNo.Text.Trim()) Then
                                    If txtMonNo.Text.Trim().ToUpper = "NA" Then
                                        If drpMonType.SelectedValue <> "MMP" Then
                                            If hdblnSNoOverride.Value = "0" And hdReplacementChallanExist.Value <> "1" Then
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

                                        '@ New Code
                                        If txtMonNo.Text.Trim().ToUpper() <> "NA" Then
                                            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                            '@ Check Whether Entered Mon No is out for selected agency or not
                                            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                                   

                                            ds = New DataSet
                                            ' @ This is Used For Getting the All Mon No on the basis of Challan No
                                            objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                                            objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = "" 'txtChallanNo.Text
                                            If Request.QueryString("Action") = "R" Then
                                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                            Else
                                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                            End If


                                            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                                            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtMonNo.Text

                                            If hdReplacementChallanExist.Value = "1" Then ' SPECIAL CASE FOR STATUS = 9
                                                objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                                                objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                                                objOutputXml = objbzChallan.SearchStockReplacementDetails(objInputXml)
                                            Else
                                                objOutputXml = objbzChallan.SearchStockDetails(objInputXml)
                                            End If

                                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                                'If objOutputXml.DocumentElement.SelectSingleNode("DETAILS ").Attributes("EQUIPMENT_CODE").Value <> drpMonType.SelectedValue Then
                                                '    lblError.Text = "Monitor type does not belongs to entered Monitor no."
                                                '    SaveStatus.Value = "MONTYPE"
                                                '    If drpMonType.Visible = True Then
                                                '        drpMonType.Focus()
                                                '    End If
                                                '    Exit Sub
                                                'End If

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
                                            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = txtMonNo.Text
                                            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
                                            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)
                                            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                                                '  strResult = "CT11|" & ID.Split("|").GetValue(0) & "|" & ID.Split("|").GetValue(2)
                                                '@ Show Panel For All Value of Mon No exit in database
                                                bindGridPanelForMonNo(txtMonNo.Text)
                                                If txtMonNo.Visible = True Then
                                                    txtMonNo.Focus()
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
                                End If

                            End If
                        End If

                        '###########################################
                        ' @ End of proper validation on Mon NO.
                        ' ##########################################

                        ' @ If Any Cause hdAllowSaveForKeyNo<>1 then can't save for a proper validation on CPU NO.
                        '###########################################
                        ' @ mandaory for Keyboard type.
                        ' ##########################################
                        If drpKeyType.SelectedValue.Trim.Length = 0 Then
                            lblError.Text = "Keyboard Type is mandaory."
                            SaveStatus.Value = "KETTYPE"
                            drpKeyType.Focus()
                            Exit Sub
                        End If

                        '###########################################
                        ' @ end of mandaory for Keyboard type.
                        '###########################################

                        If hdAllowSaveForKeyNo.Value <> "1" Then
                            If txtKeyboardNo.Text.Trim.Length = 0 Then
                                lblError.Text = "Keyboard no. is mandatory."
                                SaveStatus.Value = "KEYNO"
                                If txtKeyboardNo.Visible = True Then
                                    txtKeyboardNo.Focus()
                                End If
                                Exit Sub
                            End If
                        End If
                        '###########################################
                        ' @ End of proper validation on Mouse NO.
                        '###########################################


                        ' @ If Any Cause hdAllowSaveForMSENo<>1 then can't save for a proper validation on CPU NO.
                        '###########################################
                        ' @ mandaory for Mouse type.
                        '###########################################
                        If drpMouseType.SelectedValue.Trim.Length = 0 Then

                            lblError.Text = "Mouse Type is mandaory."
                            SaveStatus.Value = "MSETYPE"
                            drpMouseType.Focus()
                            Exit Sub

                        End If

                        '###########################################
                        ' @ end of mandaory for Mouse type.
                        '###########################################
                        If hdAllowSaveForMSENo.Value <> "1" Then
                            If txtMouseNo.Text.Trim.Length = 0 Then
                                lblError.Text = "Mouse no. is mandatory."
                                SaveStatus.Value = "MOUSE"
                                If txtMouseNo.Visible = True Then
                                    txtMouseNo.Focus()
                                End If
                                Exit Sub
                            End If
                        End If
                        '###########################################
                        ' @ End of proper validation on Mon NO.
                        '###########################################


                        If Request.QueryString("Action") = "R" Then
                            objInputXml.LoadXml("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL ACTION='R' ADDLRAM='' LCODE ='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' Qty='' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = '' CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO =''  OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")
                        Else
                            objInputXml.LoadXml("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE =''  ADDLRAM='' DATE  = ''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' Qty='' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = '' CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO =''  OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("USE_BACKDATED_CHALLAN").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_SERIAL_NO").Value = "False"
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_ORDER_NO").Value = "False"


                        '############################################################################################################
                        ' @ Security value for OVERRIDE_CHALLAN_NO,OVERRIDE_CHALLAN_SERIAL_NO,USE_BACKDATED_CHALLAN,OVERRIDE_ORDER_NO
                        '############################################################################################################
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
                        '############################################################################################################
                        '@ end Security value for OVERRIDE_CHALLAN_NO,OVERRIDE_CHALLAN_SERIAL_NO,USE_BACKDATED_CHALLAN,OVERRIDE_ORDER_NO
                        '############################################################################################################

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1)
                        If (Request("txtInstallDate") IsNot Nothing) Then '
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(Request("txtInstallDate").Trim())
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(txtInstallDate.Text)
                        End If
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").Value = drpCpuType.SelectedValue
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
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").Value = drpMonType.SelectedValue
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").Value = drpKeyType.SelectedValue
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").Value = txtKeyboardNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").Value = txtCdrNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").Value = drpMouseType.SelectedValue
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").Value = txtMouseNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = txtorderNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").Value = txtRem.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").Value = txtChallanNo.Text
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").Value = Session("LoginSession").ToString().Split("|")(0)
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedDateTime").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANSTATUS").Value = ""
                        If Request.QueryString("PCTYPE") IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").Value = Request.QueryString("PCTYPE")
                        End If
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").Value = ""
                        If Request.QueryString("ROWID") IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").Value = Request.QueryString("ROWID")
                        End If
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ADDLRAM").Value = txtRam.Text

                        If txtorderNo.Text.Trim() <> txtRorderNo.Text.Trim() Then
                            If txtorderNo.Visible = True Then
                                If txtorderNo.Text.Trim.Length > 0 Then
                                    If txtorderNo.Text.Trim <> "0" Then
                                        Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                        objInputXml2 = New XmlDocument
                                        objOutputXml2 = New XmlDocument
                                        objInputXml2.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></UP_SEARCHORDER_INPUT>")
                                        With objInputXml2.DocumentElement
                                            If (Session("Action") IsNot Nothing) Then
                                                If (Session("Action").ToString().Split("|").Length >= 2) Then
                                                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                                                    .SelectSingleNode("LCODE").InnerText = Lcode
                                                End If
                                            End If
                                            .SelectSingleNode("ORDER_NUMBER").InnerText = txtorderNo.Text
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
                        End If


                        ' @ If Order No  does not exit in the list and have ovveridable for Order No Then Set Order No Value to 0
                        If hdOrderNoExist.Value <> "1" Then
                            If hdblnOrderOverride.Value = "1" Then
                                If txtorderNo.Text.Trim.Length = 0 Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
                                End If

                            End If
                        Else
                            If hdblnOrderOverride.Value = "1" Then
                                If txtorderNo.Text.Trim.Length = 0 Then
                                    objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = 0
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

                        'Here Back end Method Call
                        If Request.QueryString("Action") = "R" Then
                            objOutputXml = objPCReplacement.Update(objInputXml)
                        Else
                            objOutputXml = objbzPCInstallation.Update(objInputXml)
                        End If

                        ' objOutputXml.LoadXml("<INV_UPDATESERIALNO_OUTPUT> <DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' /> <Errors Status='FALSE'> <Error Code='' Description=''/>  </Errors></INV_UPDATESERIALNO_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            ' lblError.Text = objeAAMSMessage ' "Record updated successfully."  
                            If (Request.QueryString("Msg")) Is Nothing Then
                                Response.Redirect("TAUP_Agencyor1APCInstall.aspx?Msg=U&" + Request.QueryString.ToString, False)
                            Else
                                Response.Redirect("TAUP_Agencyor1APCInstall.aspx?" + Request.QueryString.ToString, False)
                            End If
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

        objeAAMS.BindDropDown(drpRCpuType, "EQUIPMENTCPU", False)
        drpRCpuType.Items.Insert(drpRCpuType.Items.Count, New ListItem("CPP", "CPP"))
        drpRCpuType.Items.Insert(drpRCpuType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpRKeyType, "EQUIPMENTKBD", False)
        drpRKeyType.Items.Insert(drpRKeyType.Items.Count, New ListItem("KBP", "KBP"))
        drpRKeyType.Items.Insert(drpRKeyType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpRMonType, "EQUIPMENTMON", False)
        drpRMonType.Items.Insert(drpRMonType.Items.Count, New ListItem("MMP", "MMP"))
        drpRMonType.Items.Insert(drpRMonType.Items.Count, New ListItem("NA", "NA"))

        objeAAMS.BindDropDown(drpRMouseType, "EQUIPMENTMSE", False)
        drpRMouseType.Items.Insert(drpRMouseType.Items.Count, New ListItem("MSP", "MSP"))
        drpRMouseType.Items.Insert(drpRMouseType.Items.Count, New ListItem("NA", "NA"))

    End Sub




    Sub LoadAllEuipmentCodeValue()
        hdEquipCodexml.Value = ""
        'objeAAMS.BindDropDown(drpEquipType, "EQUIPMENTMISC", False)
        Dim objOutputXml As XmlDocument
        Dim ds As New DataSet
        '        Dim objXmlReader As XmlNodeReader
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

        'pnlMsg.Visible = False
        'If SaveStatus.Value = "Order" Then
        '    hdAllowSaveForOrder.Value = 1
        'End If
        'If SaveStatus.Value = "Challan" Then
        '    hdAllowSaveForChallan.Value = 1
        'End If
        'btnSave_Click(sender, e)

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
                'If drpOrderNo.Visible = True Then
                '    If drpOrderNo.Text.Trim().Length = 0 Then
                '        lblError.Text = "You don't have enough rights to install h/w without a Order No."
                '    End If
                '    drpOrderNo.Focus()
                If txtorderNo.Visible = True Then

                    If txtorderNo.Text.Trim().Length = 0 Then
                        lblError.Text = "You don't have enough rights to install h/w without a Order No."
                    Else
                        lblError.Text = "You don't have enough rights to install h/w without a  valid Order no."
                    End If
                    txtorderNo.Focus()
                End If

                pnlEnableorDisable.Enabled = True
                btnSave.Enabled = True
                SaveStatus.Value = "Order"
                hdAllowSaveForOrder.Value = "0"

                Exit Sub
            End If
            pnlEnableorDisable.Enabled = True
            btnSave.Enabled = True

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
                btnSave.Enabled = False
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
            btnSave.Enabled = True
            btnValidate.Enabled = False
            txtChallanNo.Enabled = False
            '@ New Code  If Challan No is valid And exist in database then 

            If txtorderNo.Visible = True Then
                txtorderNo.Focus()
                ' ElseIf drpOrderNo.Visible = True Then
                ' drpOrderNo.Focus()
            End If
        End If


    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        '  PnlDetails.Visible = True
        'pnlMsg.Visible = False
        'If SaveStatus.Value = "Order" Then
        '    hdAllowSaveForOrder.Value = 0
        '    txtOrderNo.Focus()
        '    Exit Sub
        'End If
        'If SaveStatus.Value = "Challan" Then
        '    hdAllowSaveForChallan.Value = 0
        '    txtChallanNo.Focus()
        '    Exit Sub
        'End If

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
            btnSave.Enabled = True
            ' txtOrderNo.Focus()
            'If drpOrderNo.Visible = True Then
            'drpOrderNo.Focus()
            ' Else
            If txtorderNo.Visible = True Then
                txtorderNo.Focus()
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
        Dim strAutoReplacement As String
        Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim boolReplacementChallan As Boolean
        Dim strConversionType As String = ""
        strAutoReplacement = ""
        strConversionType = ""
        hdReplacementChallanExist.Value = "0"
        Try
            hdAllowSaveForOrder.Value = 0
            If txtChallanNo.Text.Trim() <> txtRChallanNo.Text.Trim() Then


                '@ For Auto Fill in case of replacement
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "R" Then

                        txtInstallDate.CssClass = "textbox" ''ashish

                        objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")

                        objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                        objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                        objOutputXml = objbzChallan.Search(objInputXml)
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            'set Variable true if the challan is Replacement type
                            boolReplacementChallan = CBool(objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("REPLACEMENT_CHALLAN").InnerText)
                            strConversionType = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CONVERSIONTYPE").InnerText
                            If boolReplacementChallan = True Then
                                strAutoReplacement = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CONFIGVALUE").InnerText
                                If strAutoReplacement.ToString.ToUpper = "AUTO" Then
                                    hdReplacementChallanExist.Value = "1"
                                    ValidateChallanNoForAuto(strConversionType)
                                Else
                                    ValidateChallanNo()
                                End If
                            ElseIf (strConversionType.Trim.ToString() <> "" And boolReplacementChallan = False) Then
                                strAutoReplacement = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("CONFIGVALUE").InnerText
                                hdReplacementChallanExist.Value = "1"
                                ValidateChallanNoForAuto(strConversionType)
                            Else
                                ValidateChallanNo()
                            End If
                        End If
                    Else
                        txtInstallDate.CssClass = "textboxgrey" ''ashish
                        ValidateChallanNo()
                    End If
                Else
                    txtInstallDate.CssClass = "textboxgrey" ''ashish
                    ValidateChallanNo()
                End If

            Else
                txtInstallDate.CssClass = "textboxgrey" ''ashish
                pnlEnableorDisable.Enabled = True
                txtChallanNo.Enabled = False
                btnValidate.Enabled = False
                btnSave.Enabled = True
            End If

        Catch ex As Exception
        Finally
            If Request("txtInstallDate") IsNot Nothing Then
                txtInstallDate.Text = Request("txtInstallDate")
            End If

            If strAutoReplacement.ToString.ToUpper <> "AUTO" Then
                Dim li2 As ListItem
                If drpCpuNo.Visible = True Then
                    li2 = drpCpuNo.Items.FindByValue(txtRCpuNo.Text)
                    If li2 Is Nothing Then
                        drpCpuNo.Items.Insert(drpCpuNo.Items.Count, New ListItem(txtRCpuNo.Text, txtRCpuNo.Text))
                    End If
                    drpCpuNo.SelectedValue = txtRCpuNo.Text
                Else
                    txtCpuNo.Text = txtRCpuNo.Text
                End If
                If drpMonNo.Visible = True Then
                    li2 = drpMonNo.Items.FindByValue(txtRMonNo.Text)
                    If li2 Is Nothing Then
                        drpMonNo.Items.Insert(drpMonNo.Items.Count, New ListItem(txtRMonNo.Text, txtRMonNo.Text))
                    End If
                    drpMonNo.SelectedValue = txtRMonNo.Text
                Else
                    txtMonNo.Text = txtRMonNo.Text
                End If
                li2 = drpCpuType.Items.FindByValue(drpRCpuType.SelectedValue)
                If li2 Is Nothing Then
                    drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem(drpRCpuType.Text, drpRCpuType.SelectedValue))
                End If
                drpCpuType.SelectedValue = drpRCpuType.SelectedValue


                li2 = drpMonType.Items.FindByValue(drpRMonType.SelectedValue)
                If li2 Is Nothing Then
                    drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem(drpRMonType.Text, drpRMonType.SelectedValue))
                End If
                drpMonType.SelectedValue = drpRMonType.SelectedValue

                li2 = drpKeyType.Items.FindByValue(drpRKeyType.SelectedValue)
                If li2 Is Nothing Then
                    drpKeyType.Items.Insert(drpKeyType.Items.Count, New ListItem(drpRKeyType.Text, drpRKeyType.SelectedValue))
                End If
                drpKeyType.SelectedValue = drpRKeyType.SelectedValue


                li2 = drpMouseType.Items.FindByValue(drpRMouseType.SelectedValue)
                If li2 Is Nothing Then
                    drpMouseType.Items.Insert(drpMouseType.Items.Count, New ListItem(drpRMouseType.Text, drpRMouseType.SelectedValue))
                End If
                drpMouseType.SelectedValue = drpRMouseType.SelectedValue

                If hdChallaDetails.Value = "1" Then
                    FillCPUNO()

                    li2 = drpCpuNo.Items.FindByValue(txtRCpuNo.Text)
                    If li2 Is Nothing Then
                        drpCpuNo.Items.Insert(drpCpuNo.Items.Count, New ListItem(txtRCpuNo.Text, txtRCpuNo.Text))
                    End If
                    drpCpuNo.SelectedValue = txtRCpuNo.Text

                    FillMonNo()
                    li2 = drpMonNo.Items.FindByValue(txtRMonNo.Text)
                    If li2 Is Nothing Then
                        drpMonNo.Items.Insert(drpMonNo.Items.Count, New ListItem(txtRMonNo.Text, txtRMonNo.Text))
                    End If
                    drpMonNo.SelectedValue = txtRMonNo.Text
                End If
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
        hdChallnType.Value = "0"
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
                'drpOrderNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtOrderNo.Visible = True
                ' bindControls()
                drpCpuType.Items.Clear()
                objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))

                drpMonType.Items.Clear()
                objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))

                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
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
                btnSave.Enabled = False
                Exit Sub


            End If

            If txtChallanNo.Text.Trim = "0" Then
                drpCpuNo.Visible = False
                drpMonNo.Visible = False
                ' drpOrderNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtOrderNo.Visible = True
                ' bindControls()
                drpCpuType.Items.Clear()
                objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))

                drpMonType.Items.Clear()
                objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))

                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                'If hdblnChallanOverride.Value = "0" Then
                '    lblError.Text = "You don't have enough rights to install h/w without a valid challan No."
                '    SaveStatus.Value = "Challan"
                '    hdAllowSaveForChallan.Value = "0"
                '    txtChallanNo.Focus()
                '    Exit Sub
                'End If
                lblConfirm.Text = "Challan number is 0. Do you want to continue?"
                pnlMsg.Visible = True
                'PnlDetails.Visible = False
                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                pnlEnableorDisable.Enabled = False
                btnSave.Enabled = False
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
                        btnSave.Enabled = True
                        btnValidate.Enabled = False
                        txtChallanNo.Enabled = False

                    End If

                    Dim ChallanType As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"
                    hdChallnType.Value = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"

                    Dim strAgencyName As String
                    strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                    Dim strOfficeID As String
                    strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                    If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then

                        'hdChallanNoExist.Value = "1"
                        'hdAllowSaveForChallan.Value = "1"

                        ds = New DataSet
                        ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                        objInputXml2.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                        objInputXml2.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text

                        'If Request.QueryString("Action") = "R" Then
                        '    objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "5"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                        'Else
                        '    objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        'End If

                        If (ChallanType = "Receive") Then
                            'why wrote same statement in below ??? not understand..
                            If Request.QueryString("Action") = "R" Then
                                objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                            Else
                                objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                            End If

                        Else

                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        End If



                        'objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        'objInputXml2.DocumentElement.SelectSingleNode("EQUIP_CODE").InnerText = drpCpuType.SelectedValue
                        objOutputXml2 = objbzChallan.SearchStockDetails(objInputXml2)
                        If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            hdChallaDetails.Value = "1"
                            objXmlReader = New XmlNodeReader(objOutputXml2)
                            ds.ReadXml(objXmlReader)

                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill ORDERNO

                            'drpOrderNo.Items.Clear()
                            'drpOrderNo.Visible = True
                            'txtorderNo.Visible = False

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "ORDERNUMBER<>'' "

                            For i = 0 To dv.Count - 1
                                txtorderNo.Text = dv(0)("ORDERNUMBER").ToString()
                            Next


                            'For i = 0 To dv.Count - 1
                            '    Dim Item As New ListItem
                            '    Item = New ListItem(dv(i)("ORDERNUMBER").ToString, dv(i)("ORDERNUMBER").ToString)
                            '    If Not drpOrderNo.Items.Contains(Item) Then
                            '        drpOrderNo.Items.Insert(drpOrderNo.Items.Count, Item)
                            '    End If
                            'Next
                            'drpOrderNo.Items.Insert(0, New ListItem("--Select One--", ""))
                            'drpOrderNo.DataBind()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   


                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill CPUTYPE

                            drpCpuType.Items.Clear()

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "EGROUP_CODE='CPU' or EGROUP_CODE='LAP' " ' "EGROUP_CODE='CPU'"


                            For i = 0 To dv.Count - 1
                                Dim Item As New ListItem
                                Item = New ListItem(dv(i)("EQUIPMENT_CODE").ToString, dv(i)("EQUIPMENT_CODE").ToString)
                                If Not drpCpuType.Items.Contains(Item) Then
                                    drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                End If
                            Next
                            drpCpuType.Items.Insert(0, New ListItem("--Select One--", ""))

                            '@ New code Added
                            Dim li2 As New ListItem
                            li2 = drpCpuType.Items.FindByValue("NA")
                            If li2 Is Nothing Then
                                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))
                            End If
                            li2 = drpCpuType.Items.FindByValue("CPP")
                            If li2 Is Nothing Then
                                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                            End If
                            ' @ End of new code added

                            'drpCpuType.DataBind()

                            drpCpuNo.Visible = True
                            txtCpuNo.Visible = False

                            FillCPUNO()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  


                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            ' @  Fill MONTYPE
                            drpMonType.Items.Clear()

                            dv = New DataView
                            dv = ds.Tables("DETAILS").DefaultView
                            dv.RowFilter = "EGROUP_CODE='MON' or EGROUP_CODE='TFT'" '"EGROUP_CODE='MON'"
                            For i = 0 To dv.Count - 1
                                Dim Item As New ListItem
                                Item = New ListItem(dv(i)("EQUIPMENT_CODE").ToString, dv(i)("EQUIPMENT_CODE").ToString)
                                If Not drpMonType.Items.Contains(Item) Then
                                    drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                End If
                            Next
                            drpMonType.Items.Insert(0, New ListItem("--Select One--", ""))
                            'drpMonType.DataBind()


                            '@ New code Added
                            li2 = drpMonType.Items.FindByValue("NA")
                            If li2 Is Nothing Then
                                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))
                            End If
                            li2 = drpMonType.Items.FindByValue("MMP")
                            If li2 Is Nothing Then
                                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                            End If
                            ' @ End of new code added

                            drpMonNo.Visible = True
                            txtMonNo.Visible = False

                            FillMonNo()
                            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        Else
                            ' lblError.Text = objOutputXml2.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            hdChallaDetails.Value = "0"
                            drpCpuNo.Visible = False
                            txtCpuNo.Visible = True
                            drpMonNo.Visible = False
                            txtMonNo.Visible = True

                            drpCpuType.DataSource = Nothing
                            drpCpuType.DataBind()
                            objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)

                            drpKeyType.DataSource = Nothing
                            drpKeyType.DataBind()

                            objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)

                            drpMonType.DataSource = Nothing
                            drpMonType.DataBind()
                            objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)

                            If drpCpuType.Items.FindByValue("CPP") Is Nothing Then
                                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                            End If
                            If drpMonType.Items.FindByValue("MMP") Is Nothing Then
                                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                            End If



                            objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                        End If
                    Else
                        ' drpOrderNo.Visible = False
                        'txtOrderNo.Visible = True
                        'drpCpuNo.Visible = False
                        'txtCpuNo.Visible = True
                        'drpMonNo.Visible = False
                        'txtMonNo.Visible = True
                        'objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                        '' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                        'objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
                        '' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                        'objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                        'drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                        'objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                        drpCpuType.DataSource = Nothing
                        drpCpuType.DataBind()

                        objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)

                        ' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))

                        drpKeyType.DataSource = Nothing
                        drpKeyType.DataBind()
                        objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)

                        ' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                        drpMonType.DataSource = Nothing
                        drpMonType.DataBind()
                        objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                        drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                        drpMouseType.DataSource = Nothing
                        drpMouseType.DataBind()
                        objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                        drpCpuNo.Visible = False
                        txtCpuNo.Visible = True
                        'drpOrderNo.Visible = False
                        txtorderNo.Visible = True
                        drpMonNo.Visible = False
                        txtMonNo.Visible = True



                        '@ Start of Previous Code 
                        'lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                        'pnlMsg.Visible = True
                        '@End of Previous Code 

                        '@ New code added
                        pnlMsg.Visible = False
                        SaveStatus.Value = "Challan"
                        lblError.Text = "Invalid challan for this agency."
                        '@ End of New code added

                        'PnlDetails.Visible = False
                        SaveStatus.Value = "Challan"
                        txtChallanNo.Focus()
                        pnlEnableorDisable.Enabled = False
                        btnSave.Enabled = False

                    End If
                Else
                    drpCpuType.DataSource = Nothing
                    drpCpuType.DataBind()
                    objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                    ' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                    drpKeyType.DataSource = Nothing
                    drpKeyType.DataBind()
                    objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
                    ' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                    drpMonType.DataSource = Nothing
                    drpMonType.DataBind()
                    objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                    drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                    drpMouseType.DataSource = Nothing
                    drpMouseType.DataBind()
                    objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                    drpCpuNo.Visible = False
                    txtCpuNo.Visible = True
                    'drpOrderNo.Visible = False
                    txtorderNo.Visible = True
                    drpMonNo.Visible = False
                    txtMonNo.Visible = True
                    lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                    pnlMsg.Visible = True
                    'PnlDetails.Visible = False
                    SaveStatus.Value = "Challan"
                    txtChallanNo.Focus()
                    pnlEnableorDisable.Enabled = False
                    btnSave.Enabled = False
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
            '*************************************************************
        Catch ex As Exception
            lblError.Text = ex.Message

        Finally



        End Try
    End Sub


    Private Sub ValidateChallanNoForAuto(ByVal strConversionType As String)

        'Neeraj @@@@@@@@@@@@@@@@ Three Case ti be implemented while Installing Hardware @@@@@@@@@@@@@@@@@@@@@@
        'Conv to 1A H/W   - In this case Only issue challan will create without Replacement Check.
        '                   And then Install the system accordingly Ptype to 1A like CpuType - CPP to ZN1 , 
        '                   CpuNo - NA to 'IXASD148' , MonType - MMP to MH1 , MonNo - NA to 'MH1ASA102'

        'Conv to Own H/W  - In this case only Receive challan will create without Replacement Check.
        '                   And then Install the system accordingly 1A to Ptype Like CpuType - ZN1 to CPP , 
        '                   CpuNo - 'IXASD148' to NA , MonType - MH1 to MMP  , MonNo - 'MH1ASA102' to NA 

        'Conv to Own MON  - In this case only Receive challan will create without Replacement Check.
        '                   And then Install the system accordingly 1A to Ptype for MON only CPU will not 
        '                   effect Like MonType - MH1 to MMP  , MonNo - 'MH1ASA102' to NA 
        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        hdChallaDetails.Value = "0"
        hdAllowSaveForOrder.Value = "1"
        pnlNo.Visible = False
        hdChallnType.Value = "0"
        pnlMsg.Visible = False
        lblError.Text = ""
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim li As New ListItem
        Dim ds As New DataSet
        Dim dv As DataView
        Dim i As Integer

        Dim strRCpuNo As String
        Dim strRMonNo As String
        Dim strRMouseNo As String
        Dim strRKBDNo As String
        Dim intRecFound As Boolean = False
        Dim boolFullSystemReplace As Boolean = False
        Dim boolLapReplacetoFullSystem As Boolean = False



        Try

            If txtChallanNo.Text.Trim().Length = 0 Then
                drpCpuNo.Visible = False
                drpMonNo.Visible = False
                'drpOrderNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtorderNo.Visible = True
                ' bindControls()
                drpCpuType.Items.Clear()
                objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))

                drpMonType.Items.Clear()
                objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))

                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()

                lblConfirm.Text = "Challan number is blank. Do you want to continue?"
                pnlMsg.Visible = True
                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                pnlEnableorDisable.Enabled = False
                btnSave.Enabled = False
                Exit Sub
            End If

            If txtChallanNo.Text.Trim = "0" Then
                drpCpuNo.Visible = False
                drpMonNo.Visible = False

                txtCpuNo.Visible = True
                txtMonNo.Visible = True
                txtorderNo.Visible = True
                drpCpuType.Items.Clear()
                objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("NA", "NA"))

                drpMonType.Items.Clear()
                objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("NA", "NA"))

                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()

                lblConfirm.Text = "Challan number is 0. Do you want to continue?"
                pnlMsg.Visible = True
                SaveStatus.Value = "Challan"
                txtChallanNo.Focus()
                pnlEnableorDisable.Enabled = False
                btnSave.Enabled = False
                Exit Sub
            End If
            If txtChallanNo.Text.Trim.Length > 0 Then
                objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXml = objbzChallan.Search(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then
                        hdChallanNoExist.Value = "1"
                        hdAllowSaveForChallan.Value = "1"
                        pnlEnableorDisable.Enabled = True
                        btnSave.Enabled = True
                        btnValidate.Enabled = False
                        txtChallanNo.Enabled = False
                    End If

                    Dim ChallanType As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"
                    hdChallnType.Value = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanType").Value '= "Receive"

                    Dim strAgencyName As String
                    strAgencyName = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                    Dim strOfficeID As String
                    strOfficeID = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                    If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then
                        ds = New DataSet
                        ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                        objInputXml2.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                        objInputXml2.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text

                        If (ChallanType.ToString.ToUpper.Trim() = "RECEIVE") Then
                            If Request.QueryString("Action") = "R" Then
                                objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "1"
                            Else
                                objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                            End If
                        ElseIf (ChallanType.ToString.ToUpper.Trim() = "ISSUE") Then
                            objInputXml2.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                        End If

                        '@ call dll to get Stock challan Serialnumber in OUT Entry
                        objOutputXml2 = objbzChallan.SearchStockReplacementDetails(objInputXml2)

                        If objOutputXml2.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            hdChallaDetails.Value = "1"
                            objXmlReader = New XmlNodeReader(objOutputXml2)
                            ds.ReadXml(objXmlReader)

                            drpCpuNo.Visible = False
                            drpMonNo.Visible = False
                            txtCpuNo.Visible = True
                            txtMonNo.Visible = True

                            If strConversionType.Trim = "" Then
                                txtorderNo.Text = ds.Tables("DETAILS").Rows(0)("ORDERNUMBER").ToString()


                                '@ For CpuNTYpe And Cpu No
                                strRCpuNo = txtRCpuNo.Text
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "RVENDORSR_NUMBER='" + strRCpuNo + "'"
                                drpCpuType.DataSource = Nothing
                                drpCpuType.DataBind()

                                If dv.Count > 0 Then
                                    If dv(0)("EGROUP_CODE_LAP").ToString = "LAP" Then
                                        boolFullSystemReplace = True
                                    End If

                                    If dv(0)("EGROUP_CODE_LAP").ToString = "LAC" Then
                                        boolLapReplacetoFullSystem = True
                                    End If
                                End If


                                If dv.Count > 0 Then

                                    ''ASHISH
                                    If (dv(0)("EQUIPMENT_CODE").ToString.Trim() <> "KBD") Then
                                        If (dv(0)("EQUIPMENT_CODE").ToString.Trim() <> "MSE") Then
                                            intRecFound = True
                                            Dim Item As New ListItem
                                            Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                            If Not drpCpuType.Items.Contains(Item) Then
                                                drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                            End If
                                            drpCpuType.BackColor = Drawing.Color.Cyan
                                            drpCpuType.Text = Item.ToString
                                            txtCpuNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                            txtCpuNo.BackColor = Drawing.Color.Cyan
                                        End If
                                    End If
                                End If

                                '@ For MonNTYpe And Mon No
                                drpMonType.DataSource = Nothing
                                drpMonType.DataBind()
                                strRMonNo = txtRMonNo.Text
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "RVENDORSR_NUMBER='" + strRMonNo + "'"
                                If dv.Count > 0 Then

                                    If (dv(0)("EQUIPMENT_CODE").ToString.Trim() <> "KBD") Then
                                        If (dv(0)("EQUIPMENT_CODE").ToString.Trim() <> "MSE") Then
                                            intRecFound = True
                                            Dim Item As New ListItem
                                            Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                            If Not drpMonType.Items.Contains(Item) Then
                                                drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                            End If
                                            drpMonType.BackColor = Drawing.Color.Cyan
                                            drpMonType.Text = Item.ToString
                                            txtMonNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                            txtMonNo.BackColor = Drawing.Color.Cyan
                                        End If
                                    End If
                                End If




                                '@ For MouseType And Mouse No
                                drpMouseType.DataSource = Nothing
                                drpMouseType.DataBind()
                                strRMouseNo = txtRMouseNo.Text
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "RVENDORSR_NUMBER='" + strRMouseNo + "' and REGROUP_CODE='MSE'"
                                If dv.Count > 0 Then
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                    If Not drpMouseType.Items.Contains(Item) Then
                                        drpMouseType.Items.Insert(drpMouseType.Items.Count, Item)
                                    End If
                                    drpMouseType.BackColor = Drawing.Color.Cyan
                                    drpMouseType.Text = Item.ToString
                                    txtMouseNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                    txtMouseNo.BackColor = Drawing.Color.Cyan
                                End If

                                '@ For KeyBoardType And KeyBoard No
                                drpKeyType.DataSource = Nothing
                                drpKeyType.DataBind()
                                strRKBDNo = txtRKeyboardNo.Text
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "RVENDORSR_NUMBER='" + strRKBDNo + "' and REGROUP_CODE='KBD'"
                                If dv.Count > 0 Then
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                    If Not drpKeyType.Items.Contains(Item) Then
                                        drpKeyType.Items.Insert(drpKeyType.Items.Count, Item)
                                    End If
                                    drpKeyType.BackColor = Drawing.Color.Cyan
                                    drpKeyType.Text = Item.ToString
                                    txtKeyboardNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                    txtKeyboardNo.BackColor = Drawing.Color.Cyan
                                End If

                                If boolFullSystemReplace = True Then ' Full system replace to Laptop 'LAP'
                                    '' Incase of MON
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem("MMP", "MMP")
                                    If Not drpMonType.Items.Contains(Item) Then
                                        drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                    End If
                                    drpMonType.BackColor = Drawing.Color.Cyan
                                    drpMonType.Text = Item.ToString
                                    txtMonNo.Text = "NA"
                                    txtMonNo.BackColor = Drawing.Color.Cyan

                                    ''Incase of KBD
                                    intRecFound = True
                                    Item = New ListItem("KBD", "KBD")
                                    If Not drpKeyType.Items.Contains(Item) Then
                                        drpKeyType.Items.Insert(drpKeyType.Items.Count, Item)
                                    End If
                                    drpKeyType.BackColor = Drawing.Color.Cyan
                                    drpKeyType.Text = Item.ToString
                                    txtKeyboardNo.Text = "NA"
                                    txtKeyboardNo.BackColor = Drawing.Color.Cyan

                                    ''Incase of MSE
                                    intRecFound = True
                                    Item = New ListItem("MSE", "MSE")
                                    If Not drpMouseType.Items.Contains(Item) Then
                                        drpMouseType.Items.Insert(drpMouseType.Items.Count, Item)
                                    End If
                                    drpMouseType.BackColor = Drawing.Color.Cyan
                                    drpMouseType.Text = Item.ToString
                                    txtMouseNo.Text = "NA"
                                    txtMouseNo.BackColor = Drawing.Color.Cyan

                                End If

                                If boolLapReplacetoFullSystem = True Then ' LAP to Full system replace

                                    '' Incase of CPU
                                    Dim Item As New ListItem
                                    dv = New DataView
                                    dv = ds.Tables("DETAILS").DefaultView
                                    dv.RowFilter = "REGROUP_CODE='" + "CPU" + "'"

                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        'If Not drpCpuType.Items.Contains(Item) Then
                                        drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                        'End If
                                        drpCpuType.BackColor = Drawing.Color.Cyan
                                        drpCpuType.Text = Item.ToString
                                        txtCpuNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtCpuNo.BackColor = Drawing.Color.Cyan
                                    End If


                                    '' Incase of MON
                                    dv.RowFilter = "REGROUP_CODE='" + "MON" + "'"
                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        'If Not drpMonType.Items.Contains(Item) Then
                                        drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                        'End If
                                        drpMonType.BackColor = Drawing.Color.Cyan
                                        drpMonType.Text = Item.ToString
                                        txtMonNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtMonNo.BackColor = Drawing.Color.Cyan
                                    End If


                                    ''Incase of KBD
                                    dv.RowFilter = "REGROUP_CODE='" + "KBD" + "'"
                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        'If Not drpKeyType.Items.Contains(Item) Then
                                        drpKeyType.Items.Insert(drpKeyType.Items.Count, Item)
                                        'End If
                                        drpKeyType.BackColor = Drawing.Color.Cyan
                                        drpKeyType.Text = Item.ToString
                                        txtKeyboardNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtKeyboardNo.BackColor = Drawing.Color.Cyan
                                    End If


                                    ''Incase of MSE
                                    dv.RowFilter = "REGROUP_CODE='" + "MSE" + "'"
                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        'If Not drpMouseType.Items.Contains(Item) Then
                                        drpMouseType.Items.Insert(drpMouseType.Items.Count, Item)
                                        'End If
                                        drpMouseType.BackColor = Drawing.Color.Cyan
                                        drpMouseType.Text = Item.ToString
                                        txtMouseNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtMouseNo.BackColor = Drawing.Color.Cyan
                                    End If
                                End If

                                If intRecFound = False Then
                                    lblError.Text = "Challan Number- " + txtChallanNo.Text + " having no serial number to replace!"
                                    btnSave.Enabled = False
                                    Exit Sub
                                End If
                            End If
                            If strConversionType.Trim.ToString = "1" Then 'CASE ----> 'Conv to 1A H/W'
                                intRecFound = False
                                txtorderNo.Text = ds.Tables("DETAILS").Rows(0)("ORDERNUMBER").ToString()

                                '@ For CpuNTYpe And Cpu No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='" + "CPU" + "'"
                                drpCpuType.DataSource = Nothing
                                drpCpuType.DataBind()

                                If dv.Count > 0 Then
                                    If drpCpuType.Text.Trim() = "CPP" Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        If Not drpCpuType.Items.Contains(Item) Then
                                            drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                        End If
                                        drpCpuType.BackColor = Drawing.Color.Cyan
                                        drpCpuType.Text = Item.ToString
                                        txtCpuNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtCpuNo.BackColor = Drawing.Color.Cyan
                                    End If
                                End If

                                '@ For MonTYpe And Mon No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='" + "MON" + "'"
                                drpMonType.DataSource = Nothing
                                drpMonType.DataBind()

                                If dv.Count > 0 Then
                                    If drpMonType.Text.Trim() = "MMP" Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                        If Not drpMonType.Items.Contains(Item) Then
                                            drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                        End If
                                        drpMonType.BackColor = Drawing.Color.Cyan
                                        drpMonType.Text = Item.ToString
                                        txtMonNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                        txtMonNo.BackColor = Drawing.Color.Cyan
                                    End If
                                End If

                                '@ For KbdTYpe And Kbd No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='" + "KBD" + "'"
                                drpKeyType.DataSource = Nothing
                                drpKeyType.DataBind()

                                If dv.Count > 0 Then
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                    If Not drpKeyType.Items.Contains(Item) Then
                                        drpKeyType.Items.Insert(drpKeyType.Items.Count, Item)
                                    End If
                                    drpKeyType.BackColor = Drawing.Color.Cyan
                                    drpKeyType.Text = Item.ToString
                                    txtKeyboardNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                    txtKeyboardNo.BackColor = Drawing.Color.Cyan
                                End If

                                '@ For MseTYpe And Mse No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                dv.RowFilter = "EGROUP_CODE='" + "MSE" + "'"
                                drpMouseType.DataSource = Nothing
                                drpMouseType.DataBind()

                                If dv.Count > 0 Then
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem(dv(0)("EQUIPMENT_CODE").ToString, dv(0)("EQUIPMENT_CODE").ToString)
                                    If Not drpMouseType.Items.Contains(Item) Then
                                        drpMouseType.Items.Insert(drpMouseType.Items.Count, Item)
                                    End If
                                    drpMouseType.BackColor = Drawing.Color.Cyan
                                    drpMouseType.Text = Item.ToString
                                    txtMouseNo.Text = dv(0)("VENDORSR_NUMBER").ToString
                                    txtMouseNo.BackColor = Drawing.Color.Cyan
                                End If

                                If intRecFound = False Then
                                    lblError.Text = "Challan Number- " + txtChallanNo.Text + " having no serial number to replace!"
                                    btnSave.Enabled = False
                                    Exit Sub
                                End If
                            End If
                            If strConversionType.Trim.ToString = "2" Then 'CASE ----> 'Conv to Own H/W'
                                intRecFound = False
                                txtorderNo.Text = ds.Tables("DETAILS").Rows(0)("ORDERNUMBER").ToString()
                                '@ For CpuNTYpe And Cpu No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView

                                '@ Check For CPU
                                dv.RowFilter = "VENDORSR_NUMBER='" & txtCpuNo.Text.Trim.ToString() & "'"
                                intRecFound = False
                                If dv.Count > 0 Then
                                    intRecFound = True
                                End If

                                '@ Check For MON
                                dv.RowFilter = "VENDORSR_NUMBER='" & txtMonNo.Text.Trim.ToString() & "'"
                                If dv.Count > 0 Then
                                    intRecFound = True
                                End If

                                drpCpuType.DataSource = Nothing
                                drpCpuType.DataBind()

                                If intRecFound = True Then
                                    'dv.RowFilter = "VENDORSR_NUMBER<>'Neeraj Goswami'"
                                    dv.RowFilter = ""

                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem("CPP", "CPP")
                                        If Not drpCpuType.Items.Contains(Item) Then
                                            drpCpuType.Items.Insert(drpCpuType.Items.Count, Item)
                                        End If
                                        drpCpuType.BackColor = Drawing.Color.Cyan
                                        drpCpuType.Text = Item.ToString
                                        txtCpuNo.Text = "NA"
                                        txtCpuNo.BackColor = Drawing.Color.Cyan
                                    End If

                                    '@ For MonTYpe And Mon No
                                    dv = New DataView
                                    dv = ds.Tables("DETAILS").DefaultView
                                    drpMonType.DataSource = Nothing
                                    drpMonType.DataBind()

                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem("MMP", "MMP")
                                        If Not drpMonType.Items.Contains(Item) Then
                                            drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                        End If
                                        drpMonType.BackColor = Drawing.Color.Cyan
                                        drpMonType.Text = Item.ToString
                                        txtMonNo.Text = "NA"
                                        txtMonNo.BackColor = Drawing.Color.Cyan
                                    End If

                                    '@ For KbdTYpe And Kbd No
                                    dv = New DataView
                                    dv = ds.Tables("DETAILS").DefaultView
                                    drpKeyType.DataSource = Nothing
                                    drpKeyType.DataBind()

                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem("KBD", "KBD")
                                        If Not drpKeyType.Items.Contains(Item) Then
                                            drpKeyType.Items.Insert(drpKeyType.Items.Count, Item)
                                        End If
                                        drpKeyType.BackColor = Drawing.Color.Cyan
                                        drpKeyType.Text = Item.ToString
                                        txtKeyboardNo.Text = "NA"
                                        txtKeyboardNo.BackColor = Drawing.Color.Cyan
                                    End If

                                    '@ For MseTYpe And Mse No
                                    dv = New DataView
                                    dv = ds.Tables("DETAILS").DefaultView
                                    drpMouseType.DataSource = Nothing
                                    drpMouseType.DataBind()

                                    If dv.Count > 0 Then
                                        intRecFound = True
                                        Dim Item As New ListItem
                                        Item = New ListItem("MSE", "MSE")
                                        If Not drpMouseType.Items.Contains(Item) Then
                                            drpMouseType.Items.Insert(drpMouseType.Items.Count, Item)
                                        End If
                                        drpMouseType.BackColor = Drawing.Color.Cyan
                                        drpMouseType.Text = Item.ToString
                                        txtMouseNo.Text = "NA"
                                        txtMouseNo.BackColor = Drawing.Color.Cyan
                                    End If
                                End If

                                If intRecFound = False Then
                                    lblError.Text = "Challan Number- " + txtChallanNo.Text + " having no serial number to replace!"
                                    btnSave.Enabled = False
                                    Exit Sub
                                End If
                            End If
                            If strConversionType.Trim.ToString = "3" Then 'CASE ----> 'Conv to Own MON'
                                intRecFound = False
                                txtorderNo.Text = ds.Tables("DETAILS").Rows(0)("ORDERNUMBER").ToString()
                                '@ For MonTYpe And Mon No
                                dv = New DataView
                                dv = ds.Tables("DETAILS").DefaultView
                                drpMonType.DataSource = Nothing
                                drpMonType.DataBind()

                                If dv.Count > 0 Then
                                    intRecFound = True
                                    Dim Item As New ListItem
                                    Item = New ListItem("MMP", "MMP")
                                    If Not drpMonType.Items.Contains(Item) Then
                                        drpMonType.Items.Insert(drpMonType.Items.Count, Item)
                                    End If
                                    drpMonType.BackColor = Drawing.Color.Cyan
                                    drpMonType.Text = Item.ToString
                                    txtMonNo.Text = "NA"
                                    txtMonNo.BackColor = Drawing.Color.Cyan
                                End If
                                If intRecFound = False Then
                                    lblError.Text = "Challan Number- " + txtChallanNo.Text + " having no serial number to replace!"
                                    btnSave.Enabled = False
                                    Exit Sub
                                End If
                            End If
                        Else
                            ' lblError.Text = objOutputXml2.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                            hdChallaDetails.Value = "0"
                            drpCpuNo.Visible = False
                            txtCpuNo.Visible = True
                            drpMonNo.Visible = False
                            txtMonNo.Visible = True

                            drpCpuType.DataSource = Nothing
                            drpCpuType.DataBind()
                            objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)

                            drpKeyType.DataSource = Nothing
                            drpKeyType.DataBind()

                            objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)

                            drpMonType.DataSource = Nothing
                            drpMonType.DataBind()
                            objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)

                            If drpCpuType.Items.FindByValue("CPP") Is Nothing Then
                                drpCpuType.Items.Insert(drpCpuType.Items.Count, New ListItem("CPP", "CPP"))
                            End If
                            If drpMonType.Items.FindByValue("MMP") Is Nothing Then
                                drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                            End If
                            objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)


                        End If
                    Else
                        drpCpuType.DataSource = Nothing
                        drpCpuType.DataBind()

                        objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                        drpKeyType.DataSource = Nothing
                        drpKeyType.DataBind()
                        objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)

                        drpMonType.DataSource = Nothing
                        drpMonType.DataBind()
                        objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                        drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                        drpMouseType.DataSource = Nothing
                        drpMouseType.DataBind()
                        objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)
                        drpCpuNo.Visible = False
                        txtCpuNo.Visible = True
                        txtorderNo.Visible = True
                        drpMonNo.Visible = False
                        txtMonNo.Visible = True
                        '@ New code added
                        pnlMsg.Visible = False
                        SaveStatus.Value = "Challan"
                        lblError.Text = "Invalid challan for this agency."
                        '@ End of New code added

                        'PnlDetails.Visible = False
                        SaveStatus.Value = "Challan"
                        txtChallanNo.Focus()
                        pnlEnableorDisable.Enabled = False
                        btnSave.Enabled = False

                    End If
                Else
                    drpCpuType.DataSource = Nothing
                    drpCpuType.DataBind()
                    objeAAMS.BindDropDown(drpCpuType, "EQUIPMENTCPU", False)
                    ' drpCpuType.Items.Insert(0, New ListItem("CPP", "CPP"))
                    drpKeyType.DataSource = Nothing
                    drpKeyType.DataBind()
                    objeAAMS.BindDropDown(drpKeyType, "EQUIPMENTKBD", False)
                    ' drpKeyType.Items.Insert(0, New ListItem("KBP", "KBP"))
                    drpMonType.DataSource = Nothing
                    drpMonType.DataBind()
                    objeAAMS.BindDropDown(drpMonType, "EQUIPMENTMON", False)
                    drpMonType.Items.Insert(drpMonType.Items.Count, New ListItem("MMP", "MMP"))
                    drpMouseType.DataSource = Nothing
                    drpMouseType.DataBind()
                    objeAAMS.BindDropDown(drpMouseType, "EQUIPMENTMSE", False)

                    drpCpuNo.Visible = False
                    txtCpuNo.Visible = True
                    'drpOrderNo.Visible = False
                    txtorderNo.Visible = True
                    drpMonNo.Visible = False
                    txtMonNo.Visible = True
                    lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                    pnlMsg.Visible = True
                    'PnlDetails.Visible = False
                    SaveStatus.Value = "Challan"
                    txtChallanNo.Focus()
                    pnlEnableorDisable.Enabled = False
                    btnSave.Enabled = False
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
            '*************************************************************
            'divEdit.Disabled = True
            'txtInstallDate.Enabled = True
            'f_DateInstall.Disabled = False
            'pnlEnableorDisable.Enabled = False


            drpKeyType.Enabled = True
            txtKeyboardNo.Enabled = True
            drpMouseType.Enabled = True
            txtMouseNo.Enabled = True

            txtCdrNo.Enabled = True
            txtRam.Enabled = True

            ''OLD COMMENT ON 19-SEP-2010 BY ASHISH
            'txtCdrNo.Enabled = False
            'txtRam.Enabled = False
            'txtRem.Enabled = True
            '' END HERE

            drpCpuType.Enabled = False
            drpCpuNo.Enabled = False
            txtCpuNo.Enabled = False
            drpMonType.Enabled = False
            txtMonNo.Enabled = False
            txtorderNo.Enabled = False

            If strConversionType.Trim.ToString = "2" Then
                drpKeyType.Enabled = False
                txtKeyboardNo.Enabled = False
                drpMouseType.Enabled = False
                txtMouseNo.Enabled = False
                txtCdrNo.Enabled = False
                txtRam.Enabled = False
                drpCpuType.Enabled = False
                drpCpuNo.Enabled = False
                txtCpuNo.Enabled = False
                drpMonType.Enabled = False
                txtMonNo.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub drpMonType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpMonType.SelectedIndexChanged
        Try
            If (hdChallnType.Value = "Receive") Then
                If Request.QueryString("Action") = "R" Then
                    FillMonNo("R")
                Else
                    FillMonNo()
                End If
            Else
                FillMonNo()
            End If

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
          
            ' FillCPUNO()

            If (hdChallnType.Value = "Receive") Then
                If Request.QueryString("Action") = "R" Then
                    FillCPUNO("R")
                Else
                    FillCPUNO()
                End If
            Else
                FillCPUNO()
            End If


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
                    pnlEnableorDisable.Enabled = False
                    btnSave.Enabled = False
                    gvInstall.DataSource = ds.Tables("PCINSTALL").DefaultView
                    gvInstall.DataBind()
                    pnlNo.Visible = True
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
                    btnSave.Enabled = False
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
        'pnlNo.Visible = False
        'If SaveStatus.Value = "CPUNO" Then
        '    If hdblnSNoOverride.Value = "1" Then
        '        hdAllowSaveForCpuNo.Value = 1
        '    Else
        '        hdAllowSaveForCpuNo.Value = "0"
        '        lblError.Text = "You don't have enough rights to install h/w without a valid CPU no."
        '        SaveStatus.Value = "CPUNO"
        '        If drpCpuNo.Visible = True Then
        '            drpCpuNo.Focus()
        '        End If
        '        Exit Sub
        '    End If
        '    btnSave_Click(sender, e)
        '    Exit Sub
        'End If
        'If SaveStatus.Value = "MONNO" Then
        '    If hdblnSNoOverride.Value = "1" Then
        '        hdAllowSaveForMonNo.Value = 1
        '    Else
        '        lblError.Text = "You don't have enough rights to install h/w without a valid Monitor no."
        '        SaveStatus.Value = "MONNO"
        '        If txtMonNo.Visible = True Then
        '            txtMonNo.Focus()
        '        End If
        '        Exit Sub
        '    End If
        '    btnSave_Click(sender, e)
        '    Exit Sub
        'End If


        pnlNo.Visible = False
        If SaveStatus.Value = "CPUNO" Then
            If hdblnSNoOverride.Value = "1" Then
                hdAllowSaveForCpuNo.Value = 1
                pnlEnableorDisable.Enabled = True
                btnSave.Enabled = True
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
                btnSave.Enabled = True
                Exit Sub
            End If

        End If
        If SaveStatus.Value = "MONNO" Then
            If hdblnSNoOverride.Value = "1" Then
                hdAllowSaveForMonNo.Value = 1
                pnlEnableorDisable.Enabled = True
                btnSave.Enabled = True
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
                btnSave.Enabled = True
                Exit Sub
            End If

        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        'pnlNo.Visible = False

        'If SaveStatus.Value = "CPUNO" Then

        '    hdAllowSaveForCpuNo.Value = 0
        '    Exit Sub
        'End If
        'If SaveStatus.Value = "MONNO" Then

        '    hdAllowSaveForMonNo.Value = 0
        '    Exit Sub
        'End If

        pnlNo.Visible = False
        If SaveStatus.Value = "CPUNO" Then

            hdAllowSaveForCpuNo.Value = 0
            pnlEnableorDisable.Enabled = True
            btnSave.Enabled = True
            Exit Sub
        End If
        If SaveStatus.Value = "MONNO" Then
            pnlEnableorDisable.Enabled = True
            btnSave.Enabled = True

            hdAllowSaveForMonNo.Value = 0
            Exit Sub
        End If

    End Sub
    Private Sub FillCPUNO(Optional ByVal strChallanType As String = "")
        Try

            If drpCpuType.SelectedIndex >= 0 Then
                drpCpuNo.Items.Clear()
                If drpCpuType.SelectedValue <> "" Then
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim li As New ListItem
                    Dim ds As New DataSet
                    ds = New DataSet

                    ' @ This is Used For Getting the All Mon No on the basis of Challan No
                    objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                    ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "MON"

                    'If Request.QueryString("Action") = "R" Then
                    '    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "5"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    'End If
                    If strChallanType.Trim = "R" Then
                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    End If


                    'objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
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
                        'drpCpuNo.DataBind()
                        drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    Else
                        drpCpuNo.Items.Clear()
                        drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                        'drpCpuNo.DataBind()
                    End If
                Else
                    drpCpuNo.Items.Clear()
                    drpCpuNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    ' drpCpuNo.DataBind()
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
    Private Sub FillMonNo(Optional ByVal strChallanType As String = "")
        Try
            If drpMonType.SelectedIndex >= 0 Then
                drpMonNo.Items.Clear()
                If drpMonType.SelectedValue <> "" Then
                    Dim objbzChallan As New AAMS.bizInventory.bzChallan
                    Dim objInputXml As New XmlDocument, objOutputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml2 As New XmlDocument
                    Dim objXmlReader As XmlNodeReader
                    Dim li As New ListItem
                    Dim ds As New DataSet

                    ds = New DataSet
                    ' @ This is Used For Getting the All Cpu No on the basis of Challan No
                    objInputXml.LoadXml("<INV_SEARCHSTOCKDETAILS_INPUT><PRODUCTID /> <GODOWNID /> <EQUIP_GROUP /><EQUIP_CODE /> <SERIALNUMBER /> <STOCKSTATUS /> <VENDERSERIALNO /> <CHALLANNUMBER /> <LCODE /></INV_SEARCHSTOCKDETAILS_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("CHALLANNUMBER").InnerText = txtChallanNo.Text
                    ' objInputXml.DocumentElement.SelectSingleNode("EQUIP_GROUP").InnerText = "MON"
                    'If Request.QueryString("Action") = "R" Then
                    '    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "5"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    'End If

                    If strChallanType.Trim = "R" Then
                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"   ' IN CASE OF REPLACEMENT STOCKSTATUS = 5 TO GET DETAILS ONLY
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
                    End If


                    ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
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
                        ' drpMonNo.DataBind()
                    Else
                        drpMonNo.Items.Insert(0, New ListItem("--Select One--", ""))
                        'drpMonNo.DataBind()
                    End If
                Else
                    Dim li2 As New ListItem
                    li2 = drpMonNo.Items.FindByValue("")
                    If li2 Is Nothing Then
                        drpMonNo.Items.Insert(0, New ListItem("--Select One--", ""))
                    End If

                    ' drpMonNo.DataBind()

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
  

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        lblError.Text = ""
        Response.Redirect(Request.Url.ToString, False)
    End Sub
End Class


