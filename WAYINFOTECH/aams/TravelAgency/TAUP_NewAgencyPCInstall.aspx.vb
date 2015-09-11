
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region

Partial Class TravelAgency_TAUP_NewAgencyPCInstall
    Inherits System.Web.UI.Page

#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim strResult As String = ""
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
            '  txtOrderNo.Attributes.Add("onfocusout", "return ValidateOrderNo();")
            'btnSave.Attributes.Add("onclick", "return ValidateOrderNo();")
            btnSave.Attributes.Add("onclick", "return AgencyPCMandatory();")
            btnYes.Attributes.Add("onclick", "return AgencyPCMandatory();")
            txtOrderNo.Attributes.Add("onchange", "return ResetSaveForOrder();")
            lblError.Text = ""
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


            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        'btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
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
                '*******************************************************************

                ' ************************************************
                ' Security value for OVERRIDE_CHALLAN_NO,OVERRIDE_CHALLAN_SERIAL_NO,USE_BACKDATED_CHALLAN,OVERRIDE_ORDER_NO


                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OVERRIDE_ORDER_NO']").Attributes("Value").Value)
                        If strBuilder(0) = "1" Then
                            hdblnOrderOverride.Value = "1"
                        End If
                    Else
                        hdblnOrderOverride.Value = "0"
                    End If
                Else
                    hdblnOrderOverride.Value = "1"
                End If
                ' ************************************************
                txtInstallDate.Text = Format(Now, "dd/MM/yyyy") 'Format(CDate(DateTime.Now.Month.ToString + "/" + DateTime.Now.Day.ToString + "/" + DateTime.Now.Year.ToString), "dd/MM/yyyy")
                '*******************************************************************
            End If
            If (Not IsPostBack) Then
                ' PnlDetails.Enabled =False 
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
   

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objInputXml As New XmlDocument, objInputXml2 As New XmlDocument, objOutputXml As New XmlDocument, objOutputXml2 As New XmlDocument
            '   Dim objXmlReader As XmlNodeReader
            pnlMsg.Visible = False

            If (IsValid) Then
                If (Session("Action") IsNot Nothing) Then
                    If (Session("Action").ToString().Split("|").Length >= 2) Then
                        If hdAllowSaveForOrder.Value <> "1" Then

                            'If txtOrderNo.Text.Trim().Length = 0 Then
                            '    If hdblnOrderOverride.Value = "0" Then
                            '        lblError.Text = "You don't have enough rights to Install H/W without a Order No."
                            '        SaveStatus.Value = "Order"
                            '        txtOrderNo.Focus()
                            '        Exit Sub
                            '    End If
                            'End If

                            If txtOrderNo.Text.Trim().Length = 0 Then
                                'If hdblnOrderOverride.Value = "1" Then
                                PnlDetails.Enabled = False
                                lblConfirm.Text = "Order Number is blank. Do you want to continue?"
                                pnlMsg.Visible = True
                                ' PnlDetails.Visible = False
                                SaveStatus.Value = "Order"
                                txtOrderNo.Focus()
                                Exit Sub
                                'End If
                            End If
                            'If txtOrderNo.Text.Trim().Length > 0 Then
                            '    If txtOrderNo.Text.Trim() = "0" Then
                            '        If hdblnOrderOverride.Value = "0" Then
                            '            lblError.Text = "You don't have enough rights to Install H/W without a valid Order No."
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

                                    Else
                                        hdOrderNoExist.Value = "0"
                                        'If hdblnOrderOverride.Value = "0" Then
                                        '    'pnlMsg.Visible = True
                                        '    ' PnlDetails.Visible = False
                                        '    SaveStatus.Value = "Order"
                                        '    lblError.Text = "You don't have enough rights to Install H/W without a  valid Order No."
                                        '    txtOrderNo.Focus()
                                        '    Exit Sub
                                        'End If
                                        'If hdblnOrderOverride.Value = "1" Then
                                        '    pnlMsg.Visible = True
                                        '    'PnlDetails.Visible = False
                                        '    SaveStatus.Value = "Order"
                                        '    lblConfirm.Text = "Given Order number does not exist .Do you want to Continue?"
                                        '    txtOrderNo.Focus()
                                        '    Exit Sub
                                        'End If
                                        pnlMsg.Visible = True
                                        PnlDetails.Enabled = False
                                        SaveStatus.Value = "Order"
                                        lblConfirm.Text = "Given Order number does not exist .Do you want to Continue?"
                                        txtOrderNo.Focus()
                                        Exit Sub
                                    End If
                                End If
                            End If
                            '*************************************************************
                            ' Gaeeing value for order no exist or not?


                        End If

                        '###########################################
                        ' @ End of proper validation on Order NO.
                        ' ########################################
                        Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
                        objInputXml.LoadXml("<UP_TA_UPDATE_PCINSTALLATION_INPUT><DETAIL LCODE ='' DATE  = '' ADDLRAM=''   CPUTYPE ='' CPUNO = ''  MONTYPE  = ''  MONNO  = '' KBDTYPE = ''  KBDNO  =''  CDRNO=''  MSETYPE  = '' MSENO  = ''  OrderNumber  = '' Qty='' REMARKS  = '' CHALLANDATE  ='' CHALLANNUMBER  = '' LoggedBy  ='' LoggedDateTime  = '' CHALLANSTATUS  = '' ROWID=''  PCTYPE =''  USE_BACKDATED_CHALLAN='' OVERRIDE_CHALLAN_NO =''  OVERRIDE_CHALLAN_SERIAL_NO='' OVERRIDE_ORDER_NO=''	/></UP_TA_UPDATE_PCINSTALLATION_INPUT>")


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
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("Qty").Value = txtQty.Text
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
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANNUMBER").Value = "0"
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedBy").Value = Session("LoginSession").ToString().Split("|")(0)
                        End If

                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("LoggedDateTime").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("CHALLANSTATUS").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("ROWID").Value = ""
                        objInputXml.DocumentElement.SelectSingleNode("DETAIL").Attributes("PCTYPE").Value = "0"


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





                        ''Here Back end Method Call
                        objOutputXml = objbzPCInstallation.Update(objInputXml)
                        ' objOutputXml.LoadXml("<INV_UPDATESERIALNO_OUTPUT> <DETAILS ACTION='' PRODUCTID='' VENDERSERIALNO='' NEWVENDERSERIALNO='' /> <Errors Status='FALSE'> <Error Code='' Description=''/>  </Errors></INV_UPDATESERIALNO_OUTPUT>")
                        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            lblError.Text = objeAAMSMessage.messInsert ' "Record updated successfully."  
                            btnSave.Enabled = False
                            'Response.Redirect("TAUP_NewAgencyPCInstall.aspx?Msg=U&Popup=T&PrdId=" + Request.QueryString("PrdId") + "&SNO=" + txtSNo.Text + "&VSNO=" + txtNewVenSNo.Text + "&PNAME=" + txtPrdName.Text, False)
                            'ClientScript.RegisterStartupScript(Me.GetType, "keys", "<script type='text/javascript' language='javascript'>window.opener.document.forms['form1'].submit();window.close();</script> ")
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

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        '    PnlDetails.Visible = True
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

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        '  PnlDetails.Visible = True
        pnlMsg.Visible = False
        If SaveStatus.Value = "Order" Then
            PnlDetails.Enabled = True
            hdAllowSaveForOrder.Value = 0
            txtOrderNo.Focus()
            Exit Sub
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString(), False)
    End Sub
End Class
