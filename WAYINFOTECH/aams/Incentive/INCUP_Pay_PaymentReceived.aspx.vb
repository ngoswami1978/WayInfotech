
Partial Class Incentive_INCUP_Pay_PaymentReceived
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration "
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim StrError As String = ""
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            btnSave.Attributes.Add("onclick", "return ValidateForm();")
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            If (Not IsPostBack) Then
                LblPLB.Text = ""
                If Request.QueryString("Year") IsNot Nothing Then
                    hdYear.Value = Request.QueryString("Year").ToString
                End If
                If Request.QueryString("Month") IsNot Nothing Then
                    hdMonth.Value = Request.QueryString("Month").ToString
                End If

                Dim strPLBPAy As String = "False"
                If Request.QueryString("PLB") IsNot Nothing Then
                    If Request.QueryString("PLB").Trim = "1" Then
                        strPLBPAy = "True"
                        LblPLB.Text = "PLB"
                    End If
                End If
                hdPLBCYcle.Value = strPLBPAy

                Dim strPeriod As String = ""
                Dim strPeriodFrom As String = ""
                Dim strPeriodTo As String = ""
                Dim strPeriodFinalFrom As String = ""
                If Request.QueryString("Period") IsNot Nothing Then
                    strPeriod = Request.QueryString("Period").ToString
                   
                End If
                HdPLBPeriod.Value = strPeriod


                If (Request.QueryString("Action").ToString() = "U") Then
                    If (Request.QueryString("PayID") IsNot Nothing) Then
                        BtnReject.Enabled = True
                        ViewPaymentReceived(objED.Decrypt(Request.QueryString("PayID").ToString()))
                    Else
                        BtnReject.Enabled = False
                    End If
                End If
            End If
            ' Checking Security
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            EnableDiablePaymentSheet()
        End Try
    End Sub
#End Region

#Region "ViewPaymentReceived(ByVal strPANumber As String)"
    Private Sub ViewPaymentReceived(ByVal strPANumber As String)
        Dim objInputXml, objOutputXml As New XmlDocument

        Try
            Dim objbzISPPayment As New AAMS.bizIncetive.bzPaymentReceived
            'Lcode = Session("LCODE")
            objInputXml.LoadXml("<INC_VIEW_PAYMENT_RECEIVED_INPUT><PAYMENT_ID></PAYMENT_ID></INC_VIEW_PAYMENT_RECEIVED_INPUT >")
            ' @ Code for Viwing The records")
            objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = strPANumber
            'Here Back end Method Call
            objOutputXml = objbzISPPayment.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtPANO.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("PAYMENT_ID").InnerText
                txtChainCode.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("PANNO").InnerText
                txtChqNo.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_NO").InnerText
                txtBankName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_BNAME").InnerText
                txtChqDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_DATE").InnerText)
                txtChqDelDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_DELIVERED_DATE").InnerText)
                txtChqRecAgenceyDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_RECEIVED_AGENCY_DATE").InnerText)
                txtAmt.Text = Val(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_AMOUNT").InnerText)
                txtEmployeeName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_DELIVERED_NAME").InnerText
                hdEmployeeID.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_DELIVERED_TO").InnerText
                txtAgency.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_RECEIVED_AGENCY").InnerText
                TxtTDSAmount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("TDSAMOUNT").InnerText

                txtChainCode.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHAIN_CODE").InnerText
                hdChainCode.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHAIN_CODE").InnerText
                hdChainCode.Value = objED.Encrypt(hdChainCode.Value)


                hdBCID.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("BC_ID").InnerText
                hdPaymentType.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("PAYMENTTYPE").InnerText
               


                txtChainName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHAIN_NAME").InnerText
                txtChainAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHAIN_ADDRESS").InnerText
                '@Start of New Code Added for Adjustment Payment

                'CHQ_ADJ_AMOUNT=''             CHQ_ADJ_NO=''   CHQ_ADJ_DATE=''           CHQ_ADJ_BNAME=''           CHQ_ADJ_DELIVERED_TO=''   CHQ_ADJ_DELIVERED_NAME          CHQ_ADJ_DELIVERED_DATE=''            CHQ_ADJ_RECEIVED_AGENCY=''           CHQ_ADJ_RECEIVED_AGENCY_DATE=''         CHQ_ADJ_TDSAMOUNT=''


                If Val(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_AMOUNT").InnerText) > 0 Then
                    PnlAdjustment.Visible = True
                    hdAadjustmentPayment.Value = "1"
                    txtAdjChqNo.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_NO").InnerText
                    txtAdjBankName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_BNAME").InnerText
                    txtAdjChqDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_DATE").InnerText)
                    txtAdjChqDelDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_DELIVERED_DATE").InnerText)
                    txtAdjChqRecAgenceyDate.Text = objeAAMS.ConvertDateBlank(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_RECEIVED_AGENCY_DATE").InnerText)
                    txtAdjAmt.Text = Val(objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_AMOUNT").InnerText)
                    txtAdjEmployeeName.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_DELIVERED_NAME").InnerText
                    hdAdjEmployeeID.Value = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_DELIVERED_TO").InnerText
                    txtAdjAgency.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_RECEIVED_AGENCY").InnerText
                    TxtAdjTDSAmount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED").Attributes("CHQ_ADJ_TDSAMOUNT").InnerText
                Else
                    hdAadjustmentPayment.Value = "0"
                    PnlAdjustment.Visible = False
                End If

                '@ End of New code added for adjustment paymemt
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim strISPOrderID As String = ""
            Dim objPaymentReceived As XmlNode
            Dim objbzISPPayment As New AAMS.bizIncetive.bzPaymentReceived
            If (IsValid) Then

                'CHQ_ADJ_AMOUNT=''             CHQ_ADJ_NO=''   CHQ_ADJ_DATE=''           CHQ_ADJ_BNAME=''           CHQ_ADJ_DELIVERED_TO=''   CHQ_ADJ_DELIVERED_NAME=''         CHQ_ADJ_DELIVERED_DATE=''             CHQ_ADJ_RECEIVED_AGENCY=''           CHQ_ADJ_RECEIVED_AGENCY_DATE=''         CHQ_ADJ_TDSAMOUNT=''
                objInputXml.LoadXml("<INC_UPDATE_PAYMENT_RECEIVED_INPUT> <PAYMENT_RECEIVED PAYMENT_ID='' CHQ_NO='' CHQ_DATE='' CHQ_BNAME='' CHQ_AMOUNT='' CHQ_DELIVERED_TO='' CHQ_DELIVERED_DATE='' CHQ_RECEIVED_AGENCY='' CHQ_RECEIVED_AGENCY_DATE='' TDSAMOUNT ='' PANNO='' CHQ_ADJ_AMOUNT=''             CHQ_ADJ_NO=''   CHQ_ADJ_DATE=''           CHQ_ADJ_BNAME=''           CHQ_ADJ_DELIVERED_TO=''   CHQ_ADJ_DELIVERED_NAME=''          CHQ_ADJ_DELIVERED_DATE=''            CHQ_ADJ_RECEIVED_AGENCY=''           CHQ_ADJ_RECEIVED_AGENCY_DATE=''         CHQ_ADJ_TDSAMOUNT='' /></INC_UPDATE_PAYMENT_RECEIVED_INPUT>")
                objPaymentReceived = objInputXml.DocumentElement.SelectSingleNode("PAYMENT_RECEIVED")
                With objPaymentReceived
                    .Attributes("PAYMENT_ID").Value() = objED.Decrypt(Request.QueryString("PayID").ToString())
                    If (txtChqDate.Text.Trim().Length > 0) Then
                        .Attributes("CHQ_DATE").Value() = objeAAMS.ConvertTextDate(txtChqDate.Text)
                    End If
                    If (txtChqDelDate.Text.Trim().Length > 0) Then
                        .Attributes("CHQ_DELIVERED_DATE").Value() = objeAAMS.ConvertTextDate(txtChqDelDate.Text)
                    End If
                    If (txtChqRecAgenceyDate.Text.Trim().Length > 0) Then
                        .Attributes("CHQ_RECEIVED_AGENCY_DATE").Value() = objeAAMS.ConvertTextDate(txtChqRecAgenceyDate.Text)
                    End If
                    .Attributes("CHQ_BNAME").Value() = txtBankName.Text
                    .Attributes("CHQ_DELIVERED_TO").Value() = hdEmployeeID.Value
                    .Attributes("CHQ_NO").Value() = txtChqNo.Text
                    .Attributes("CHQ_AMOUNT").Value() = txtAmt.Text
                    .Attributes("CHQ_RECEIVED_AGENCY").Value() = txtAgency.Text

                    .Attributes("TDSAMOUNT").Value() = TxtTDSAmount.Text

                    .Attributes("PANNO").Value() = txtChainCode.Text


                    '@ start of New Code Added for adjustment TDs

                    'CHQ_ADJ_AMOUNT=''             CHQ_ADJ_NO=''   CHQ_ADJ_DATE=''           CHQ_ADJ_BNAME=''           CHQ_ADJ_DELIVERED_TO=''            CHQ_ADJ_DELIVERED_DATE=''            CHQ_ADJ_RECEIVED_AGENCY=''           CHQ_ADJ_RECEIVED_AGENCY_DATE=''         CHQ_ADJ_TDSAMOUNT=''
                    '@ End of  New Code Added for Adjusment TDS
                    If Val(txtAdjAmt.Text) > 0 Then
                        If (txtAdjChqDate.Text.Trim().Length > 0) Then
                            .Attributes("CHQ_ADJ_DATE").Value() = objeAAMS.ConvertTextDate(txtAdjChqDate.Text)
                        End If
                        If (txtAdjChqDelDate.Text.Trim().Length > 0) Then
                            .Attributes("CHQ_ADJ_DELIVERED_DATE").Value() = objeAAMS.ConvertTextDate(txtAdjChqDelDate.Text)
                        End If
                        If (txtAdjChqRecAgenceyDate.Text.Trim().Length > 0) Then
                            .Attributes("CHQ_ADJ_RECEIVED_AGENCY_DATE").Value() = objeAAMS.ConvertTextDate(txtAdjChqRecAgenceyDate.Text)
                        End If
                        .Attributes("CHQ_ADJ_BNAME").Value() = txtAdjBankName.Text

                        .Attributes("CHQ_ADJ_DELIVERED_TO").Value() = hdAdjEmployeeID.Value

                        .Attributes("CHQ_ADJ_NO").Value() = txtAdjChqNo.Text
                        .Attributes("CHQ_ADJ_AMOUNT").Value() = txtAdjAmt.Text
                        .Attributes("CHQ_ADJ_RECEIVED_AGENCY").Value() = txtAdjAgency.Text

                        .Attributes("CHQ_ADJ_TDSAMOUNT").Value() = TxtAdjTDSAmount.Text


                    End If



                End With
                objOutputXml = objbzISPPayment.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                    txtEmployeeName.Text = Request("txtEmployeeName")
                    txtAdjEmployeeName.Text = Request("txtAdjEmployeeName")
                Else
                    txtEmployeeName.Text = Request("txtEmployeeName")
                    txtAdjEmployeeName.Text = Request("txtAdjEmployeeName")
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            txtEmployeeName.Text = Request("txtEmployeeName")
            txtAdjEmployeeName.Text = Request("txtAdjEmployeeName")
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString(), False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Received']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If
                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            '@ Right check for rejection of payment so that they can send for first level of payment queue.

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentReject']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentReject']").Attributes("Value").Value)
                    If strBuilder(1) = "0" And strBuilder(2) = "0" Then
                        BtnReject.Enabled = False
                    Else
                        BtnReject.Enabled = True
                    End If
                Else
                    BtnReject.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                BtnReject.Enabled = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub BtnReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReject.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Try
            If Session("LoginSession") Is Nothing Then
                lblError.Text = "Session is expired."
                Exit Sub
            End If

            If txtReason.Text.Trim.Length <= 0 Then
                lblError.Text = "Please provide the reason for the rejection."
                Exit Sub
            End If

            If txtReason.Text.Trim.Length > 500 Then
                lblError.Text = "Reason can't be greater than 500 characters."
                Exit Sub
            End If


            If txtPANO.Text.Trim.Length > 0 Then
                objInputXml.LoadXml("<UP_REJECT_PAYMENTADVICE_INPUT> <PAYMENT_ID></PAYMENT_ID> <BC_ID/> <EMPLOYEEID/> <REJECTED/> <REJECTED_REASON/> <REJECTED_LOGGEDBY/></UP_REJECT_PAYMENTADVICE_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = txtPANO.Text
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("REJECTED_LOGGEDBY").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If

                objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdBCID.Value
                objInputXml.DocumentElement.SelectSingleNode("REJECTED_REASON").InnerText = txtReason.Text
                objInputXml.DocumentElement.SelectSingleNode("REJECTED").InnerText = "1"

                objOutputXml = objbzPaymentApprovalQue.PaymentRejectCheckCreation(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    'BtnReject.Enabled = False
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
        End Try
    End Sub
   
    
   
    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue

        Try

            If Session("LoginSession") Is Nothing Then
                lblError.Text = "Session is expired."
                Exit Sub
            End If


            If txtReason.Text.Trim.Length <= 0 Then
                lblError.Text = "Please provide the reason for the rejection."
                Exit Sub
            End If

            If txtReason.Text.Trim.Length > 500 Then
                lblError.Text = "Reason can't be greater than 500 characters."
                Exit Sub
            End If


            If txtPANO.Text.Trim.Length > 0 Then
                objInputXml.LoadXml("<UP_REJECT_PAYMENTADVICE_INPUT> <PAYMENT_ID></PAYMENT_ID> <BC_ID/> <EMPLOYEEID/> <REJECTED/> <REJECTED_REASON/> <REJECTED_LOGGEDBY/></UP_REJECT_PAYMENTADVICE_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = txtPANO.Text
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("REJECTED_LOGGEDBY").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If

                objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdBCID.Value
                objInputXml.DocumentElement.SelectSingleNode("REJECTED_REASON").InnerText = txtReason.Text
                objInputXml.DocumentElement.SelectSingleNode("REJECTED").InnerText = "1"
                objOutputXml = objbzPaymentApprovalQue.PaymentRejectCheckCreation(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    'BtnReject.Enabled = False
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
        End Try

    End Sub
    Private Sub EnableDiablePaymentSheet()
        'objBCID,objChainCode,objMonth,objYear,objPayTime,objCurPayNo
        Try
            If Session("Security") Is Nothing Then
                Exit Sub
            End If

           



            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PaymentSheet']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        BtnPaymentSheetReport.Enabled = False
                    Else
                        BtnPaymentSheetReport.Enabled = True
                        BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport();")
                    End If
                Else
                    BtnPaymentSheetReport.Enabled = False
                End If
            Else
                BtnPaymentSheetReport.Enabled = True
                BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport();")

            End If
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                'Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
