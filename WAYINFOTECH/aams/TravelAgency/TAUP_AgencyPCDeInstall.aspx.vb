
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_TAUP_AgencyPCDeInstall
    Inherits System.Web.UI.Page
    'Implements System.Web.UI.ICallbackEventHandler
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim strResult As String
    Dim Lcode As String
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
            btnSave.Attributes.Add("onclick", "return PCDeInstallMandatory();")
            txtOrderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            txtChallanNo.Attributes.Add("onchange", "return ResetSaveForChallan();")
         
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        'btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights


            If Not Page.IsPostBack Then
                '*******************************************************************
                objSecurityXml.LoadXml(Session("Security"))
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
                            hdblnOrderOverride.Value = "0"
                        End If
                    Else
                        hdblnChallanOverride.Value = "0"
                    End If
                Else
                    hdblnOrderOverride.Value = "1"
                    hdblnChallanOverride.Value = "1"
                End If

                If (Request.QueryString("Msg") = "U") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If

                txtDeInstallDate.Text = Format(Now, "dd/MM/yyyy") 'Format(CDate(DateTime.Now.Month.ToString + "/" + DateTime.Now.Day.ToString + "/" + DateTime.Now.Year.ToString), "dd/MM/yyyy")
                '*******************************************************************

                If Request.QueryString("ROWID") IsNot Nothing Then
                    ViewPcInstllation()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewPcInstllation()
        If (Not Request.QueryString("Action") = Nothing) Then
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
            objInputXml.LoadXml("<UP_TA_VIEW_PCINSTALLATION_INPUT> <ROWID /> </UP_TA_VIEW_PCINSTALLATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = Request.QueryString("ROWID")
            'Here Back end Method Call
            objOutputXml = objbzPCInstallation.View(objInputXml)
            ' objOutputXml.LoadXml("<UP_TA_VIEW_PCINSTALLATION_OUTPUT> <DETAIL ROWID='44' LCODE='' DATE='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' KBDTYPE='' KBDNO='' MSETYPE='' MSENO='' OrderNumber='4454' REMARKS='rt' CHALLANNUMBER='' CDRNO='' PCTYPE='0' /> <Errors Status='FALSE'> <Error Code='' Description='' /> </Errors> </UP_TA_VIEW_PCINSTALLATION_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'BindPcInstllation(objOutputXml)
                If Request.QueryString("PCTYPE") IsNot Nothing Then
                    If Request.QueryString("PCTYPE") = "0" Then
                        'If objOutputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText = "" Or objOutputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText = "NA" Or objOutputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText = "0" Then
                        '    txtChallanNo.Enabled = False
                        '    hdAllowSaveForChallan.Value = "1"
                        'Else
                        '    txtChallanNo.Enabled = True

                        'End If
                        txtChallanNo.Enabled = False
                        hdAllowSaveForChallan.Value = "1"
                    Else
                        txtChallanNo.Enabled = True
                    End If
                Else
                    If objOutputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText = "" Or objOutputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").InnerText = "NA" Then
                        txtChallanNo.Enabled = False
                        hdAllowSaveForChallan.Value = "1"
                    Else
                        txtChallanNo.Enabled = True

                    End If
                End If
               

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            End If
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
    '            Else
    '                If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value = "No Record Found!") Then
    '                    strResult = "C0"
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objInputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml As New XmlDocument, objOutputXml2 As New XmlDocument
            '            Dim objXmlReader As XmlNodeReader
            lblError.Text = ""
            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then

                     

                        '###########################################
                        ' Check Validation for Challan No.
                        ' @ If Any Cause hdAllowSaveForOrder<>1 then can't save for a proper validation on Order NO.


                        If hdAllowSaveForChallan.Value <> "1" Then

                            'If txtChallanNo.Text.Trim().Length = 0 Then
                            '    If hdblnChallanOverride.Value = "0" Then
                            '        lblError.Text = "You don't have enough rights to Install H/W without a Challan No."
                            '        SaveStatus.Value = "Challan"
                            '        txtChallanNo.Focus()
                            '        Exit Sub
                            '    End If
                            'End If

                            'If txtChallanNo.Text.Trim().Length = 0 Then
                            '    If hdblnChallanOverride.Value = "1" Then
                            '        lblConfirm.Text = "Challan Number is blank. Do you want to continue?"
                            '        pnlMsg.Visible = True
                            '        'PnlDetails.Visible = False
                            '        SaveStatus.Value = "Challan"
                            '        txtChallanNo.Focus()
                            '        Exit Sub
                            '    End If
                            'End If
                            If txtChallanNo.Text.Trim().Length = 0 Then
                                PnlDetails.Enabled = False
                                lblConfirm.Text = "Challan Number is blank. Do you want to continue?"
                                pnlMsg.Visible = True
                                'PnlDetails.Visible = False
                                SaveStatus.Value = "Challan"
                                txtChallanNo.Focus()
                                Exit Sub
                            End If


                            If txtChallanNo.Text.Trim().Length > 0 Then
                                If txtChallanNo.Text.Trim() = "0" Then
                                    'If hdblnChallanOverride.Value = "0" Then
                                    '    lblError.Text = "You don't have enough rights to Install H/W without a valid Challan No."
                                    '    SaveStatus.Value = "Challan"
                                    '    txtChallanNo.Focus()
                                    '    Exit Sub
                                    'End If
                                    PnlDetails.Enabled = False
                                    lblConfirm.Text = "Challan Number is 0. Do you want to continue?"
                                    pnlMsg.Visible = True
                                    'PnlDetails.Visible = False
                                    SaveStatus.Value = "Challan"
                                    txtChallanNo.Focus()
                                    Exit Sub
                                End If
                            End If
                            '*************************************************************
                            ' Getting value for Challan no exist or not?
                            If txtChallanNo.Text.Trim().Length > 0 Then
                                If txtChallanNo.Text.Trim() <> "0" Then

                                    objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                                    objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                                    objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                                    objOutputXml = objbzChallan.Search(objInputXml)
                                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then
                                            hdChallanNoExist.Value = "1"
                                            hdAllowSaveForChallan.Value = "1"
                                            txtChallanNo.Enabled = False

                                        End If
                                        Dim strAgencyName As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("AgencyName").Value
                                        Dim strOfficeID As String = objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("OfficeID").Value
                                        If objOutputXml.DocumentElement.SelectSingleNode("CHALLAN").Attributes("LCODE").Value = Session("Action").ToString().Split("|").GetValue(1) Then

                                            ' hdChallanNoExist.Value = "1"
                                            ' hdAllowSaveForChallan.Value = "1"

                                            'If hdblnChallanOverride.Value = "0" Then
                                            '    ' pnlMsg.Visible = True
                                            '    'PnlDetails.Visible = False
                                            '    SaveStatus.Value = "Challan"
                                            '    txtChallanNo.Focus()
                                            '    lblError.Text = "You don't have enough rights to install h/w without a  valid challan no."
                                            '    Exit Sub
                                            'End If
                                            'If hdblnChallanOverride.Value = "1" Then
                                            '    lblConfirm.Text = " Given challan no. " + txtChallanNo.Text + " is for " + strAgencyName + " OfficeID " + strOfficeID + " Want to reuse it for this Agency also ?"
                                            '    pnlMsg.Visible = True
                                            '    '  PnlDetails.Visible = False
                                            '    SaveStatus.Value = "Challan"
                                            '    txtChallanNo.Focus()
                                            '    Exit Sub
                                            'End If
                                        Else
                                            'If hdblnChallanOverride.Value = "0" Then

                                            '    SaveStatus.Value = "Challan"
                                            '    txtChallanNo.Focus()
                                            '    lblError.Text = "You don't have enough rights to install h/w without a  valid challan no."
                                            '    Exit Sub
                                            'End If
                                            'PnlDetails.Enabled = False
                                            'pnlMsg.Visible = True
                                            'SaveStatus.Value = "Challan"
                                            'lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                                            'txtChallanNo.Focus()
                                            'Exit Sub
                                        End If
                                    Else
                                        'If hdblnChallanOverride.Value = "0" Then
                                        '    SaveStatus.Value = "Challan"
                                        '    lblError.Text = "You don't have enough rights to install h/w without a  valid challan no."
                                        '    txtChallanNo.Focus()
                                        '    Exit Sub
                                        'End If
                                        PnlDetails.Enabled = False
                                        pnlMsg.Visible = True
                                        SaveStatus.Value = "Challan"
                                        lblConfirm.Text = "Given Challan number does not exist. Do you want to continue?"
                                        txtChallanNo.Focus()
                                        Exit Sub
                                    End If
                                End If
                            End If
                                '*************************************************************
                                ' Gaeeing value for Challan no exist or not?


                        End If

                            '###########################################
                            ' @ End of proper validation on Challan NO.
                            ' ########################################
                        ' Check Validation for Order No.
                        ' @ If Any Cause hdAllowSaveForOrder<>1 then can't save for a proper validation on Order NO.

                        If hdAllowSaveForOrder.Value <> "1" Then

                            If txtOrderNo.Visible = True Then
                                If txtOrderNo.Text.Trim().Length = 0 Then
                                    'If hdblnOrderOverride.Value = "0" Then
                                    '    lblError.Text = "You don't have enough rights to install h/w without a order no."
                                    '    SaveStatus.Value = "Order"
                                    '    txtOrderNo.Focus()
                                    '    Exit Sub
                                    'End If
                                End If

                                If txtOrderNo.Text.Trim().Length = 0 Then
                                    'If hdblnOrderOverride.Value = "1" Then
                                    lblConfirm.Text = "Order number is blank. Do you want to continue?"
                                    pnlMsg.Visible = True
                                    PnlDetails.Enabled = False
                                    SaveStatus.Value = "Order"
                                    txtOrderNo.Focus()
                                    Exit Sub
                                    'End If
                                End If
                                'If txtOrderNo.Text.Trim().Length > 0 Then
                                '    If txtOrderNo.Text.Trim() = "0" Then
                                '        If hdblnOrderOverride.Value = "0" Then
                                '            lblError.Text = "You don't have enough rights to install h/w without a valid order no."
                                '            SaveStatus.Value = "Order"
                                '            txtOrderNo.Focus()
                                '            Exit Sub
                                '        End If
                                '    End If
                                'End If

                                If txtOrderNo.Text.Trim().Length > 0 Then
                                    If txtOrderNo.Text.Trim() = "0" Then
                                        PnlDetails.Enabled = False
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

                                            '@ New Code Added For Amadaeus Order
                                            '################################################
                                        ElseIf FindAgencyOrderNoForAmadeus() = "True" Then
                                            hdOrderNoExist.Value = "1"
                                            hdAllowSaveForOrder.Value = "1"

                                            '@ New Code Added For Amadaeus Order
                                            '################################################
                                        Else
                                            hdOrderNoExist.Value = "0"
                                            'If hdblnOrderOverride.Value = "0" Then
                                            '    'pnlMsg.Visible = True
                                            '    ' PnlDetails.Visible = False
                                            '    SaveStatus.Value = "Order"
                                            '    lblError.Text = "You don't have enough rights to install h/w without a  valid order no."
                                            '    txtOrderNo.Focus()
                                            '    Exit Sub
                                            'End If
                                            'If hdblnOrderOverride.Value = "1" Then
                                            '    pnlMsg.Visible = True
                                            '    'PnlDetails.Visible = False
                                            '    SaveStatus.Value = "Order"
                                            '    lblConfirm.Text = "Given order number does not exist. Do you want to continue?"
                                            '    txtOrderNo.Focus()
                                            '    Exit Sub
                                            'End If

                                            pnlMsg.Visible = True
                                            PnlDetails.Enabled = False
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


                        Dim objbzPCDeInstallation As New AAMS.bizTravelAgency.bzPCDeInstallation
                        objInputXml.LoadXml("<UP_TA_PCDEINSTALLATION_INPUT><DETAIL ACTION ='X' LCODE ='' ADDLRAM=''  DATE  = '' CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  ='' CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' Qty ='1' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = '' CHALLANSTATUS  = '' ROWID=''  PCTYPE ='' USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO ='' OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''/></UP_TA_PCDEINSTALLATION_INPUT>")

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

                            '' ***********************************************************************************************
                            '' @ If Challan No is empty then check Whether user have right to insert a new rcord or not?
                            'If txtChallanNo.Text.Trim().Length <= 0 Then
                            '    If objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OVERRIDE_CHALLAN_NO").Value = "False" Then
                            '        lblError.Text = "You don't have enough right to Install without a challan No."
                            '        Exit Sub
                            '    End If
                            'End If
                            '' ***********************************************************************************************



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
                        If (Request("txtDeInstallDate") IsNot Nothing) Then '
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(Request("txtDeInstallDate").Trim())
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("DATE").Value = objeAAMS.ConvertTextDate(txtDeInstallDate.Text)
                        End If
                        ' objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUTYPE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CPUNO").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONTYPE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MONNO").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDTYPE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("KBDNO").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CDRNO").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSETYPE").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("MSENO").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("OrderNumber").Value = txtOrderNo.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("REMARKS").Value = txtRem.Text
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANDATE").Value = ""
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



                        If txtOrderNo.Visible = True Then
                            If txtOrderNo.Text.Trim.Length > 0 Then
                                If txtOrderNo.Text.Trim <> "0" Then
                                    Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
                                    objInputXml2 = New XmlDocument
                                    objOutputXml2 = New XmlDocument
                                    'objInputXml2.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region></UP_SEARCHORDER_INPUT>")
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
                        ' @ If Order No  does not exit in the list and have ovveridable for Order No Then Set Order No Value to 0
                        If hdOrderNoExist.Value <> "1" Then
                            If hdblnOrderOverride.Value = "1" Then
                                If txtOrderNo.Text.Trim.Length = 0 Then
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
                        objOutputXml = objbzPCDeInstallation.Update(objInputXml)
                        ' objOutputXml.LoadXml("<INV_UPDATESERIALNO_OUTPUT> <DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' /> <Errors Status='FALSE'> <Error Code='' Description=''/>  </Errors></INV_UPDATESERIALNO_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            ' lblError.Text = objeAAMSMessage ' "Record updated successfully."  
                            ' Response.Redirect("TAUP_AgencyPCDeInstall.aspx?Msg=U&" + Request.QueryString.ToString, False)
                            ClientScript.RegisterStartupScript(Me.GetType, "keys", "<script type='text/javascript' language='javascript'>window.opener.document.forms['form1'].submit();window.close();</script> ")
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
            If Request("txtDeInstallDate") IsNot Nothing Then
                txtDeInstallDate.Text = Request("txtDeInstallDate")
            End If
        End Try
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

        ''*******************************************************************************************************    
        ''code written by ashish on 31-01-2011
        Dim objbzTravelAgency As New AAMS.bizTravelAgency.bzPCDeInstallation
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument

        objInputXml.LoadXml("<MS_ORDER_CHALLAN_PC_DETAILS_INPUT><LCODE></LCODE><ORDERNUMBER></ORDERNUMBER><ROWID></ROWID></MS_ORDER_CHALLAN_PC_DETAILS_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Session("Action").ToString().Split("|").GetValue(1)
        objInputXml.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText = txtOrderNo.Text
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = Request.QueryString("ROWID")
        objOutputXml = objbzTravelAgency.CheckOrderChallanSerialAgainstDeInstallation(objInputXml)

        If hdOrderNoExist.Value = "1" Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "TRUE" Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlMsg.Visible = False
                PnlDetails.Enabled = True
                txtChallanNo.Focus()
                Exit Sub
            End If
        End If
        ''end here on 31-01-2011
        ''*******************************************************************************************************    


        pnlMsg.Visible = False
        If SaveStatus.Value = "Order" Then

            If hdblnOrderOverride.Value = "0" Then
                If txtOrderNo.Text.Trim().Length = 0 Then
                    lblError.Text = "You don't have enough rights to install h/w without a Order No."
                Else
                    lblError.Text = "You don't have enough rights to install h/w without a  valid Order no."
                End If
                txtOrderNo.Focus()

                PnlDetails.Enabled = True
                SaveStatus.Value = "Order"
                hdAllowSaveForOrder.Value = "0"

                Exit Sub
            End If
            PnlDetails.Enabled = True
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

                PnlDetails.Enabled = True
                SaveStatus.Value = "Challan"
                hdAllowSaveForChallan.Value = "0"
                txtChallanNo.Focus()
                Exit Sub
            End If


            hdAllowSaveForChallan.Value = 1

            '@ New code If Challan No is valid And exist in database then  
            PnlDetails.Enabled = True
            ' btnValidate.Enabled = False
            ' txtChallanNo.Enabled = False
            '@ New Code  If Challan No is valid And exist in database then 

            If txtOrderNo.Visible = True Then
                txtOrderNo.Focus()
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
        If SaveStatus.Value = "Order" Then
            hdAllowSaveForOrder.Value = 0
            PnlDetails.Enabled = True
            ' txtOrderNo.Focus()
           If txtOrderNo.Visible = True Then
                txtOrderNo.Focus()
            End If
            Exit Sub
        End If
        If SaveStatus.Value = "Challan" Then
            hdAllowSaveForChallan.Value = 0
            PnlDetails.Enabled = True
            txtChallanNo.Focus()
            Exit Sub
        End If

    End Sub
    Private Function FindAgencyOrderNoForAmadeus() As String
        Dim stbnl As String = "False"
        Try

            If (txtOrderNo.Text.Trim = "00/00/00") And (txtChallanNo.Text.Trim.Length > 0 And txtChallanNo.Text.Trim <> "0") Then
                Dim objInputXml, objInputXmlChallan, objOutputXmlChallan, objOutputXml As New XmlDocument
                Dim objbzChallan As New AAMS.bizInventory.bzChallan
                objInputXmlChallan.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanNumber></ChallanNumber><ChallanCategory></ChallanCategory><ChallanType></ChallanType><ChallanDateFrom></ChallanDateFrom><ChallanDateTo></ChallanDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
                objInputXmlChallan.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text.Trim
                objInputXmlChallan.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = "1"
                objOutputXmlChallan = objbzChallan.Search(objInputXmlChallan)
                If objOutputXmlChallan.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objInputXml.LoadXml("<INV_VIEW_CHALLAN_INPUT><ChallanID></ChallanID></INV_VIEW_CHALLAN_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("ChallanID").InnerText = objOutputXmlChallan.DocumentElement.SelectSingleNode("CHALLAN").Attributes("ChallanID").Value  'hdChallanID.Value
                    'Here Back end Method Call 
                    objOutputXml = objbzChallan.View(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        With objOutputXml.DocumentElement.SelectSingleNode("CHALLAN")
                            If objOutputXml.DocumentElement.SelectSingleNode("AGENCYORDER").Attributes("ORDER_NUMBER").Value = "00/00/00" Then
                                stbnl = "True"
                            End If
                        End With
                    End If
                End If
            End If

        Catch ex As Exception
        End Try
        Return (stbnl)
    End Function

End Class
