Imports System.Text.RegularExpressions
Imports AjaxControlToolkit
Imports System.Math
Partial Class Incentive_INCUP_PaymentProcess
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim dset As DataSet
    Dim strBuilder As New StringBuilder
    Protected WithEvents GvIncPlan As GridView
    Dim StrSaveData As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        lblError.Text = ""
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())

            lblError.Text = "Session is expired."
            Exit Sub
        End If
        lblPaspaymentError.Text = ""
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Incentive Payment Process']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    ' btnSearch.Enabled = False
                End If ' 
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    BtnBCase.Enabled = False
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        btnSave.Attributes.Add("onclick", "return ValidateRegisterPage();")
        BtnSkipPayment.Attributes.Add("onclick", "return ValidateRegisterPage();")

        BtnPastpaySave.Attributes.Add("onclick", "return ValidatePastPayment();")

        If Not Page.IsPostBack Then

            If Request.QueryString("PLB") IsNot Nothing Then
                If Request.QueryString("PLB").Trim = "1" Then
                    HdPLBCycle.Value = "True"
                    LblQS.text = "PLB QUALIFICATION SLABS"
                Else
                    HdPLBCycle.Value = "False"
                End If
            Else
                HdPLBCycle.Value = "False"
            End If

            hdPLBFROM.Value = ""
            hdPLBTO.Value = ""
            If Request.QueryString("Period") IsNot Nothing Then
                Dim strPeriod As String = Request.QueryString("Period").ToString
                If strPeriod.Split("-").Length = 2 Then
                    Dim strPeriodFrom As String = strPeriod.Split("-")(0).Trim
                    Dim strPeriodTo As String = strPeriod.Split("-")(1).Trim
                    Dim strPeriodFinalFrom As String = ""
                    If strPeriodFrom.Trim.Split("/").Length = 3 Then
                        strPeriodFinalFrom = strPeriodFrom.Split("/")(0).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(1).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(2).ToString().PadLeft("4", "0")
                    End If
                    hdPLBFROM.Value = strPeriodFinalFrom
                    hdPLBTO.Value = strPeriodTo
                End If
            End If


            ViewState("PaymentData") = Nothing
            '   BtnShowPlan.Attributes.Add("onclick", "return ShowPlan();")
            HdPaymentId.Value = ""
            dset = New DataSet
            If Request.QueryString("CurPayNo") IsNot Nothing Then
                HdCurPayNo.Value = Val(Request.QueryString("CurPayNo").ToString)
            End If
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                ' For Default Page Setting
                ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting
                If Request.QueryString("PayId") IsNot Nothing Then
                    '  Session("PaymentRecDataSource") = Nothing
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        Dim Caseid As String = Request.QueryString("BCaseID").ToString
                        BtnBCase.Attributes.Add("onclick", "return DetailsFunction('" & Caseid & "','" & hdEnChainCode.Value & "');")
                        BtnPaymentHistory.Attributes.Add("onclick", "return PaymentHistory('" & Caseid & "','" & hdEnChainCode.Value & "','" & Request.QueryString("Month").ToString & "','" & Request.QueryString("Year").ToString & "');")
                    End If
                    LoadDataByPayId(Request.QueryString("PayId").ToString)
                Else
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        Dim Caseid As String = Request.QueryString("BCaseID").ToString
                        BtnBCase.Attributes.Add("onclick", "return DetailsFunction('" & Caseid & "','" & hdEnChainCode.Value & "');")
                        BtnPaymentHistory.Attributes.Add("onclick", "return PaymentHistory('" & Caseid & "','" & hdEnChainCode.Value & "','" & Request.QueryString("Month").ToString & "','" & Request.QueryString("Year").ToString & "');")
                        LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                        If Session("Msg") IsNot Nothing Then
                            lblError.Text = Session("Msg").ToString
                            Session("Msg") = Nothing
                        End If
                    End If
                End If
            End If
            '@ Start of In Case of refer from Payment Received Page
            If Request.QueryString("OnlyShow") IsNot Nothing Then
                For Each row As GridViewRow In GvProcessPayment.Rows
                    Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                    Dim txtRemByChangeInRate As TextBox = CType(row.FindControl("txtRemByChangeInRate"), TextBox)
                    Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                    txtRate.ReadOnly = True
                    txtRate.CssClass = "textboxgrey right"
                    txtExumption.ReadOnly = True
                    txtExumption.CssClass = "textboxgrey right"
                    txtRemByChangeInRate.ReadOnly = True
                    txtRemByChangeInRate.CssClass = "textboxgrey"

                Next

                txtRemarks.ReadOnly = True
                txtRemarks.CssClass = "textboxgrey"
                txtPayAppRemarks.ReadOnly = True
                txtPayAppRemarks.CssClass = "textboxgrey"
                btnSave.Enabled = False
                BtnPaymentHistory.Enabled = False
                BtnSendForApproval.Enabled = False
            End If

            '@ End of In Case of refer from Payment Received Page
        End If
    End Sub
   
    Private Sub LoadData(ByVal strChainCode As String, ByVal strBcaseId As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Dim DblGrandFinal As Double = 0
        Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzIncentive
        Dim objbzBCPaymentProcessForPLB As New AAMS.bizIncetive.bzPLB
        '@ Code  for Details
        Try
            objInputXml.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE> <MONTH></MONTH> <YEAR></YEAR><NO_OF_PAYMENT></NO_OF_PAYMENT> <PLB></PLB><PLBPAYMENTPERIOD_FROM></PLBPAYMENTPERIOD_FROM><PLBPAYMENTPERIOD_TO></PLBPAYMENTPERIOD_TO>  </UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcaseId
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Request.QueryString("Month")
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Request.QueryString("Year")
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = strChainCode
            objInputXml.DocumentElement.SelectSingleNode("NO_OF_PAYMENT").InnerText = HdCurPayNo.Value

            'Start of  New Code Added For PLB , PLBPAYMENTPERIOD_FROM  and PLBPAYMENTPERIOD_TO
            If Request.QueryString("PLB") IsNot Nothing Then
                If Request.QueryString("PLB").Trim = "1" Then
                    objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "True"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
            End If

            If Request.QueryString("Period") IsNot Nothing Then
                Dim strPeriod As String = Request.QueryString("Period").ToString
                If strPeriod.Split("-").Length = 2 Then
                    Dim strPeriodFrom As String = strPeriod.Split("-")(0).Trim
                    Dim strPeriodTo As String = strPeriod.Split("-")(1).Trim
                    Dim strPeriodFinalFrom As String = ""
                    If strPeriodFrom.Trim.Split("/").Length = 3 Then
                        strPeriodFinalFrom = strPeriodFrom.Split("/")(0).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(1).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(2).ToString().PadLeft("4", "0")
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_FROM").InnerText = strPeriodFinalFrom
                    objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_TO").InnerText = strPeriodTo
                End If
            End If
            'End  of  New Code Added For PLB , PLBPAYMENTPERIOD_FROM  and PLBPAYMENTPERIOD_TO

            Try
                objInputXml.Save("C:\Admin\BCPaymentProcessViewInput.xml")
            Catch ex As Exception
            End Try
            ' objOutPutxml = objbzBCPaymentProcess.View(objInputXml)
            If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                objOutPutxml = objbzBCPaymentProcessForPLB.View(objInputXml)
            Else
                objOutPutxml = objbzBCPaymentProcess.View(objInputXml)
            End If

            '   objOutPutxml.Load("C:\Admin\BCPaymentProcessViewOut.xml")
            Try
                objOutPutxml.Save("C:\Admin\BCPaymentProcessViewOut.xml")
            Catch ex As Exception
            End Try
            '  objOutPutxml.Save("C:\objOutPutxmlPayView.xml")
            'objOutPutxml.Load("C:\AAMSXml\objOutPutxmlPayView.xml")
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PaymentData") = objOutPutxml.OuterXml
                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                dset = New DataSet
                dset = ds
                If ds.Tables("GROUPDETAILS") IsNot Nothing Then
                    '<GROUPDETAILS ACCOUNTMANAGER='' CHAIN_CODE='' GROUP='' NAME='' AOFFICE='' CITY=''/>
                    txtGroupName.Text = ds.Tables("GROUPDETAILS").Rows(0)("GROUPNAME").ToString
                    txtActManager.Text = ds.Tables("GROUPDETAILS").Rows(0)("ACCOUNTMANAGER").ToString
                    txtChainCode.Text = ds.Tables("GROUPDETAILS").Rows(0)("CHAIN_CODE").ToString
                    txtAoffice.Text = ds.Tables("GROUPDETAILS").Rows(0)("AOFFICE").ToString
                    txtCity.Text = ds.Tables("GROUPDETAILS").Rows(0)("CITY").ToString
                    'txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("REMARKS").ToString
                    txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALI_REMARKS").ToString

                    If ds.Tables("GROUPDETAILS").Columns("PAREMARKS") IsNot Nothing Then
                        txtPayAppRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("PAREMARKS").ToString
                    End If

                    If ds.Tables("GROUPDETAILS").Columns("PAYMENTPERIODFORMAT") IsNot Nothing Then
                        HdPmtFormat.value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTPERIODFORMAT").ToString
                    End If

                    If ds.Tables("GROUPDETAILS").Columns("SKIPPAYMENT") IsNot Nothing Then
                        HdSkipPayment.Value = ds.Tables("GROUPDETAILS").Rows(0)("SKIPPAYMENT").ToString
                    End If

                    ''@ This Code is used for setting the value For AvgQualification/TotalQualification / TotalQualificationAfterExemption
                    txtQualAvg.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString
                    HdQualAgv.Value = ds.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                    txtQualAvgData.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                    txtTotalQualification.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALITOTAL").ToString
                    'txtTotalQualificationAfterExem.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALITOTALAFTEREXEM").ToString

                    TxtConPercentage.Text = ds.Tables("GROUPDETAILS").Rows(0)("CONV_PER").ToString
                    TxtMinSegment.Text = ds.Tables("GROUPDETAILS").Rows(0)("MINSEGMENT").ToString

                    ''@ This Code is used for setting the value For AvgQualification/TotalQualification / TotalQualificationAfterExemption


                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString.Trim.ToUpper = "P" Then
                        txtPaymentType.Text = "Post Payment"
                    Else
                        txtPaymentType.Text = "Upfront Payment"
                    End If
                    'If ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString.Trim = "1" Then
                    '    txtPaymentTerm.Text = "One time"
                    'ElseIf ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString.Trim = "2" Then
                    '    txtPaymentTerm.Text = "Replinishable"
                    'Else
                    '    txtPaymentTerm.Text = "Fixed"
                    'End If
                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString.Trim = "1" Then
                        txtAdjustment.Text = "Rate"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString.Trim = "2" Then
                        txtAdjustment.Text = "Fixed Payment"
                    End If
                    If ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "1" Then
                        txtBillCycle.Text = "Annual"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "2" Then
                        txtBillCycle.Text = "Bi-Annual"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "3" Then
                        txtBillCycle.Text = "Qtrly"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "4" Then
                        txtBillCycle.Text = "Monthly"
                    Else
                        txtBillCycle.Text = ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString
                    End If
                    TxtPayPeriod.Text = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTPERIOD").ToString
                    HdPACreated.Value = ds.Tables("GROUPDETAILS").Rows(0)("PA_CREATED").ToString.Trim.ToUpper
                    HdPaymentId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENT_ID").ToString
                    HdpREVPaymentId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PREVPAYMENT_ID").ToString
                    hdFinallyApproved.Value = ds.Tables("GROUPDETAILS").Rows(0)("FINALLYAPPROVED").ToString

                    hdChainCode.Value = ds.Tables("GROUPDETAILS").Rows(0)("CHAIN_CODE").ToString
                    ' hdEnChainCode.Value = objED.Encrypt(hdChainCode.Value)

                    '@ Start of Added On 04/09/10


                    HdUpNoPay.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPF_NO_OF_PAYMENT").ToString
                    HdAdjustable.Value = ds.Tables("GROUPDETAILS").Rows(0)("ADJUSTABLE").ToString
                    txtSignUpAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SIGNUPAMOUNT").ToString).ToString("f2")
                    txtCBForwardAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BALANCE_CF").ToString).ToString("f2")

                    HdPlbTypeId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PLBTYPEID").ToString

                    If HdAdjustable.Value = "True" Then
                        txtSignupAdjustable.Text = "Yes"
                    Else
                        txtSignupAdjustable.Text = "No"
                    End If

                    If HdAdjustable.Value.Trim.ToUpper = "TRUE" And Val(txtSignUpAmt.Text) > 0 Then
                        SpnSignAmt.Visible = True
                        TxtSpnSignAmt.Visible = True
                        TxtSpnSignAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SIGNUPAMOUNT").ToString).ToString("f2")
                    End If

                    hdShowPopup.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSINGPERIOD").ToString

                    HdMisNoOfPay.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_NO_OF_PAYMENT").ToString
                    HdMisYear.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_YEAR").ToString
                    HdMisMonth.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_MONTH").ToString

                    '@ End of Addded on 06/09/10
                    If ds.Tables("GROUPDETAILS").Rows(0)("YEARENDSETTLEMENT") IsNot Nothing Then
                        HdYearAndSettleMent.Value = ds.Tables("GROUPDETAILS").Rows(0)("YEARENDSETTLEMENT").ToString
                    End If
                    If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
                        ImgEndyearSettelement.Visible = True
                    Else
                        ImgEndyearSettelement.Visible = False
                    End If
                    hdFixedOrRate.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString
                    hdFirstTime.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTFIRSTTIME").ToString
                    hdPaymentType.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString
                    hdUpFronttType.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString
                    hdIsPLB.Value = ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString



                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        If ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString.Trim().ToUpper = "TRUE" Then
                            If HdPlbTypeId.Value <> "2" Then   ' @ In Case Of PLB Fixed Based
                                LblPlb.Visible = True
                                txtPLBAmt.Visible = True
                                txtPLBAmt.Text = ds.Tables("GROUPDETAILS").Rows(0)("PLBAMOUNT").ToString.Trim()
                                trLPlb.Visible = True
                            End If
                        End If
                    End If




                End If
                Try
                    '##########################################################
                    'New Added Code for Showing ----- Upfront Amount/Previous Upfront Amount/ Balance Upfront Amount/ Latest Upfront Amount
                    If HdCurPayNo.Value = "1" Then
                        'If hdFirstTime.Value.Trim.ToString.ToUpper = "TRUE" Then
                        lblBalanceUpfrontAmount.Visible = False
                        LblSignUpAmt.Visible = True
                        LblCBFAmount.Visible = False
                        txtBalanceUpfrontAmount.Visible = False
                        txtSignUpAmt.Visible = True
                        txtCBForwardAmount.Visible = False
                        trBalanceUpfrontAmount.Visible = False
                        trSignUpAmt.Visible = True
                        trCBFAmount.Visible = False
                    Else
                        lblBalanceUpfrontAmount.Visible = True
                        LblSignUpAmt.Visible = False
                        LblCBFAmount.Visible = True
                        txtBalanceUpfrontAmount.Visible = True
                        txtSignUpAmt.Visible = False
                        txtCBForwardAmount.Visible = True
                        trBalanceUpfrontAmount.Visible = True
                        trSignUpAmt.Visible = False
                        trCBFAmount.Visible = True
                    End If

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        lblBalanceUpfrontAmount.Visible = False
                        LblSignUpAmt.Visible = False
                        LblCBFAmount.Visible = False
                        txtBalanceUpfrontAmount.Visible = False
                        txtSignUpAmt.Visible = False
                        txtCBForwardAmount.Visible = False
                        trBalanceUpfrontAmount.Visible = False
                        trSignUpAmt.Visible = False
                        trCBFAmount.Visible = False
                    End If




                Catch ex As Exception
                End Try
                Try
                    If ds.Tables("PRODTYPEDETAILS") IsNot Nothing Then
                        If ds.Tables("PRODTYPEDETAILS").Rows(0)("PRODUCTIVITYTYPEID").ToString.Trim.Length > 0 Then
                            GvProcessPayment.DataSource = ds.Tables("PRODTYPEDETAILS")
                            GvProcessPayment.DataBind()
                            HdPaymentId.Value = ds.Tables("PRODTYPEDETAILS").Rows(0)("PAYMENT_ID").ToString
                            '  If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                            If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then
                                GvProcessPayment.HeaderRow.Cells(5).Text = "Standard Fixed / Revised Fixed"
                                '  GvProcessPayment.HeaderRow.Cells(5).Text = "Revised Fixed"
                            End If
                            'End If                          
                            If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                                If HdPlbTypeId.Value = "2" Then   ' @ In Case Of PLB Rate Based
                                    GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Rate / Revised Rate"
                                Else
                                    GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Fixed / Revised Fixed"
                                End If
                            End If





                            GvProcessPayment.FooterRow.Cells(2).Text = "Total"
                            GvProcessPayment.FooterRow.Cells(3).HorizontalAlign = HorizontalAlign.Center
                            GvProcessPayment.FooterRow.Cells(4).HorizontalAlign = HorizontalAlign.Right
                            GvProcessPayment.FooterRow.Cells(5).HorizontalAlign = HorizontalAlign.Right
                            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
                            Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
                            txtGrandFinalAmount.Text = 0
                            txtTotalCalCulatedSegment.Text = 0
                            Dim DblTotalCalCulatedSegment As Double = 0
                            For Each row As GridViewRow In GvProcessPayment.Rows
                                Dim txtActulaSegment As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                                Dim txtCalCulatedSegment As TextBox = CType(row.FindControl("txtCalCulatedSegment"), TextBox)
                                Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                                Dim txtFinalAmount As TextBox = CType(row.FindControl("txtFinalAmount"), TextBox)
                                Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                                Dim hdProductivityId As Label = CType(row.FindControl("lblProductivity"), Label)
                                Dim txtStdRate As TextBox = CType(row.FindControl("txtStdRate"), TextBox)


                                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)


                                Dim dblAmountCalculate As Double = 0
                                If txtActulaSegment.Text.Trim.Length > 0 Then
                                    If txtExumption.Text.Trim.Length > 0 Then
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                                    Else
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                                    End If
                                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                                Else
                                    txtCalCulatedSegment.Text = "0.00"
                                End If
                                'End of Code for calculating Amount on the basis of (segment * Exemption)

                                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                                    DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                                End If
                                ' ####################################################################


                                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same     

                                '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                                Dim strRate As Double = 0
                                If txtRate.Text.Trim.Length = 0 Then
                                    strRate = Val(txtStdRate.Text)
                                Else
                                    strRate = Val(txtRate.Text)
                                End If
                                '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10

                                Dim dblFinalCalCulatedSegment As Double = 0
                                If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                                    'If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then

                                    '    If txtRate.Text.Trim.Length > 0 Then
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                                    '    Else
                                    '        dblFinalCalCulatedSegment = Double.Parse(0) 'Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                                    '    End If
                                    'Else
                                    '    If txtRate.Text.Trim.Length > 0 Then
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                                    '    Else
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                                    '    End If
                                    'End If
                                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then
                                        dblFinalCalCulatedSegment = Double.Parse(strRate)
                                    Else
                                        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                                    End If
                                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                                End If
                                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                                '####################################################################
                                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                                    DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                                End If
                                txtFinalAmount.CssClass = "textboxgrey right"
                                txtCalCulatedSegment.CssClass = "textboxgrey right"
                            Next
                            txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
                            txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")
                        End If
                    Else
                        GvProcessPayment.DataSource = Nothing
                        GvProcessPayment.DataBind()
                    End If
                Catch ex As Exception
                    ' lblError.Text = ex.Message
                End Try
            Else
                GvProcessPayment.DataSource = Nothing
                GvProcessPayment.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            BreaupData(objOutPutxml)
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If HdPaymentId.Value.Trim.Length > 0 Then
                BtnSendForApproval.Enabled = True
            Else
                BtnSendForApproval.Enabled = False
            End If
        End Try

        If HdPACreated.Value.ToUpper = "TRUE" Then
            BtnSendForApproval.Enabled = False
        End If
        calculatebalance()
        HideUnhideSendForApproval()
        If hdFinallyApproved.Value.Trim.ToUpper = "TRUE" Then
            btnSave.Enabled = False
            BtnSendForApproval.Enabled = False
        End If

        '@Start of  For Payment Sheet
        BtnPaymentSheetReport.Enabled = False
        If HdPaymentId.Value.Trim.Length > 0 Then
            If Request.QueryString("BCaseID") IsNot Nothing Then
                If Request.QueryString("Month") IsNot Nothing Then
                    If Request.QueryString("Year") IsNot Nothing Then
                        EnableDiablePaymentSheet(Request.QueryString("BCaseID"), hdEnChainCode.Value, Request.QueryString("Month"), Request.QueryString("Year"), "U", HdCurPayNo.Value, HdPaymentId.Value.Trim)
                    End If
                End If
            End If
        Else
            BtnPaymentSheetReport.Enabled = False
        End If

        '@End of  For Payment Sheet
        LoadMIDDataAndQualificatiinSlab()
    End Sub
    Protected Sub GvProcessPayment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvProcessPayment.RowCommand
    End Sub
    Protected Sub GvProcessPayment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvProcessPayment.RowDataBound
        Dim objOutputCriteriaXmlXml As New XmlDocument
        '   Dim dsCriteria As DataSet
        '   Dim objReadaerCriteria As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtActulaSegment As TextBox
                Dim txtExumption As TextBox
                Dim txtCalCulatedSegment As TextBox
                Dim txtFinalAmount As TextBox
                Dim txtRate As TextBox
                txtActulaSegment = CType(e.Row.FindControl("txtActulaSegment"), TextBox)
                txtExumption = CType(e.Row.FindControl("txtExumption"), TextBox)
                txtCalCulatedSegment = CType(e.Row.FindControl("txtCalCulatedSegment"), TextBox)
                txtRate = CType(e.Row.FindControl("txtRate"), TextBox)
                txtFinalAmount = CType(e.Row.FindControl("txtFinalAmount"), TextBox)
                '@ In Case of Productivitytype is not defined for bacse then exemption is 100% and readonly
                Dim hdNotInBCase As HiddenField = CType(e.Row.FindControl("hdNotInBCase"), HiddenField)
                If hdNotInBCase.Value = "0" Then
                    If txtExumption IsNot Nothing Then
                        ' txtExumption.Text = "100"
                    End If
                    e.Row.BackColor = Drawing.Color.Silver
                    txtExumption.ReadOnly = True
                    txtExumption.CssClass = "textboxgrey right"
                End If
                '@ In Case of Productivitytype is not defined for bacse then exemption is 100% and readonly
                If txtExumption IsNot Nothing Then
                    If txtExumption.ReadOnly = False Then
                        txtExumption.Attributes.Add("onkeyup", "validateDecimalValue('" + txtExumption.ClientID + "')")
                        'If dset.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then

                        '    ' If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                        '    txtExumption.Attributes.Add("onblur", "validateDecimalValue('" + txtExumption.ClientID + "')")
                        'Else
                        '    txtExumption.Attributes.Add("onblur", "validateDecimalValue('" + txtExumption.ClientID + "')")
                        'End If
                        txtExumption.Attributes.Add("CalculatePayment", e.Row.RowIndex.ToString)
                    End If
                End If
                If txtRate IsNot Nothing Then
                    If txtRate.ReadOnly = False Then
                        txtRate.Attributes.Add("onkeyup", "validateDecimalValue('" + txtRate.ClientID + "')")
                        'If dset.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then

                        '    ' If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                        '    txtRate.Attributes.Add("onblur", "validateDecimalValue('" + txtRate.ClientID + "')")
                        'Else
                        '    txtRate.Attributes.Add("onblur", "validateDecimalValue('" + txtRate.ClientID + "')")
                        'End If
                        txtRate.Attributes.Add("CalculatePaymentRM", e.Row.RowIndex.ToString)
                    End If
                End If

                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ModifySlabRate']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ModifySlabRate']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            txtRate.ReadOnly = False
                            txtRate.Attributes.Add("class", "textboxbold right")
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right



                If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                    If HdPlbTypeId.Value <> "2" Then   ' @ In Case Of PLB Fixed Based
                        txtRate.ReadOnly = True
                        txtRate.CssClass = "textboxgrey right"
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        StrSaveData = ""
        Dim objPastpeymentxmldata As New XmlDocument
        Dim objpastpaynode As XmlNode
        Dim objpastpayclonenode As XmlNode
        Dim dv As DataView
        Dim Pastds As New DataSet
        Dim objxmlnodereader As XmlNodeReader
        If (IsValid) Then
            Try
                If Session("Security") Is Nothing Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                    lblError.Text = "Session is expired."
                    Exit Sub
                End If

                '##############################################################################################
                '@Start of  New Added Code if Revised rate is different than standard rate then RateRem is mandatory 20/10/10
                Dim RowId As Integer
                For RowId = 0 To GvProcessPayment.Rows.Count - 1
                    Dim txtStdRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtStdRate"), TextBox)
                    Dim txtRemByChangeInRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtRemByChangeInRate"), TextBox)
                    Dim txtRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtRate"), TextBox)
                    If txtRate.Text.Trim.Length > 0 Then
                        If Val(txtRate.Text.Trim) <> Val(txtStdRate.Text) Then
                            If txtRemByChangeInRate.Text.Trim.Length = 0 Then
                                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                                lblError.Text = "Remark is mandatory."
                                scriptManager.SetFocus(txtRemByChangeInRate.ClientID)
                                Exit Sub
                            End If
                        End If
                    End If


                    If txtRemByChangeInRate.Text.Trim.Length > 8000 Then
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        lblError.Text = "Remark can't  be greater than 8000"
                        scriptManager.SetFocus(txtRemByChangeInRate.ClientID)
                        Exit Sub
                    End If

                Next

                If txtPLBAmt.Visible = True Then
                    '@ Start Checking Decimal Value
                    Dim reg As Regex = New Regex("^[0-9.]+$")
                    If reg.IsMatch(txtPLBAmt.Text) Then
                        Dim countDecimalNo As Integer = 0
                        For i As Integer = 0 To txtPLBAmt.Text.Length - 1
                            If txtPLBAmt.Text.Chars(i) = "." Then
                                countDecimalNo = countDecimalNo + 1
                            End If
                        Next
                        If countDecimalNo > 1 Then
                            lblError.Text = "Only numeric with decimal is allowed."
                            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                            scriptManager.SetFocus(txtPLBAmt.ClientID)
                            Exit Sub
                        End If
                    Else
                        lblError.Text = "Only numeric with decimal is allowed."
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        scriptManager.SetFocus(txtPLBAmt.ClientID)
                        Exit Sub
                    End If
                    '@ End of  Checking Decimal Value 
                End If


                If hdShowPopup.Value <> "" Then
                    objPastpeymentxmldata.LoadXml("<Root><PastPayment PaymentPeriod='' PaymentAmt='' ProdAvg='' Rem='' Month='' Year='' NofPay='' ></PastPayment></Root>")
                    ' lblMsgDetails.Text = "The Process of Payments for the following period has not been made.To continue please process the following periods.: " & "<br/>"
                    objpastpaynode = objPastpeymentxmldata.DocumentElement.SelectSingleNode("PastPayment")
                    objpastpayclonenode = objpastpaynode.CloneNode(True)

                    For i As Integer = 0 To hdShowPopup.Value.Split(",").Length - 1
                        'lblMsgDetails.Text = lblMsgDetails.Text & "<br/>" & hdShowPopup.Value.Split(",")(i).Trim
                        objpastpayclonenode.Attributes("PaymentPeriod").Value = hdShowPopup.Value.Split(",")(i).Trim

                        objpastpayclonenode.Attributes("NofPay").Value = HdMisNoOfPay.Value.Split(",")(i).Trim

                        objpastpayclonenode.Attributes("Month").Value = HdMisMonth.Value.Split(",")(i).Trim

                        objpastpayclonenode.Attributes("Year").Value = HdMisYear.Value.Split(",")(i).Trim

                        objPastpeymentxmldata.DocumentElement.AppendChild(objpastpayclonenode)
                        objpastpayclonenode = objpastpaynode.CloneNode(True)
                    Next

     



                    objxmlnodereader = New XmlNodeReader(objPastpeymentxmldata)
                    Pastds.ReadXml(objxmlnodereader)
                    If Pastds.Tables("PastPayment").Rows.Count > 0 Then
                        dv = Pastds.Tables("PastPayment").DefaultView
                        dv.RowFilter = "PaymentPeriod <> '' "
                        If dv.Count > 0 Then
                            GvPasPaymentData.DataSource = dv
                            GvPasPaymentData.DataBind()
                        Else
                            GvPasPaymentData.DataSource = Nothing
                            GvPasPaymentData.DataBind()
                        End If

                    End If
                    ModalPopupExtenderPreviousDataSave.Show()
                    Exit Sub
                End If
                '@ End of New Added Code if Revised rate is different than standard rate then RateRem is mandatory
                '##############################################################################################


                If (Not Request.QueryString("BCaseID") = Nothing) Then
                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim ds As New DataSet
                    Dim Rowno As Integer
                    Dim objChildNode, objChildNodeClone As XmlNode
                    Dim strAoffice As String = ""
                    Dim objbzProcessPayment As New AAMS.bizIncetive.bzIncentive
                    Dim objbzProcessPaymentForPLB As New AAMS.bizIncetive.bzPLB
                    'objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT><BCDETAIL BC_ID='' PAYMENT_ID ='' MONTH='' YEAR='' EMPLOYEE_ID='' TOTAL_AMT='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT=''/><PRODTYPE PAYMENT_ID='' PRODUCTIVITYTYPEID ='' PRODUCTIVITYTYPE='' SEGMENT='' RATE='' EXCEMPTION='' AMOUNT='' FINALAMOUNT='' /></UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT>")
                    objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT><BCDETAIL CHAIN_CODE =''  BC_ID='' PAYMENT_ID ='' MONTH='' YEAR='' EMPLOYEE_ID='' TOTAL_AMT='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT='' PREVIOUSUPFRONTAMOUNT='' BALANCEUPFRONTAMOUNT='' NEXTUPFRONTAMOUNT='' PAYMENTTYPE = '' UPFRONTTYPE='' PAYMENTTYPEID=''  UPFRONTFIRSTTIME =''  PLBTYPEID='' PLBSLAB='' ISPLB='' PLBAMOUNT='' PREVPAYMENT_ID='' YEARENDSETTLEMENT=''  REMARKS ='' NO_OF_PAYMENT =''  SIGNUPAMOUNT='' ADJUSTABLE=''  BALANCE_CF=''   UPF_NO_OF_PAYMENT='' UPF_NO_OF_PAYMENT_DONE=''  UPF_ONETIME_NO_OF_PAYMENT='' UPF_ONETIME_NO_OF_PAYMENT_DONE='' QUALI_REMARKS='' QUALIAVG='' QUALITOTAL='' QUALITOTALAFTEREXEM='' PLBCYCLE='' PLBPAYMENTPERIOD_FROM='' PLBPAYMENTPERIOD_TO=''  PAREMARKS='' PAYMENTPERIOD='' PAYMENTPERIODFORMAT='' SKIPPAYMENT='FALSE'  /><CALCULATION><PRODTYPEDETAILS PAYMENT_ID= '' PRODUCTIVITYTYPEID ='' PRODUCTIVITYTYPE='' NIDTFIELDS='' SEGMENT='' RATE='' EXCEMPTION='' AMOUNT='' FINALAMOUNT='' ISCHECKED='' ROWID='' HL='' ROI='' STANDARDRATE='' STANDARSEGMENT ='' RATEREM='' /></CALCULATION></UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT>")
                    'Reading and Appending records into the Input XMLDocument                  
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BC_ID").Value = Request.QueryString("BCaseID") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("MONTH").Value = Request.QueryString("Month") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("YEAR").Value = Request.QueryString("Year") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENT_ID").Value = HdPaymentId.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PREVPAYMENT_ID").Value = HdpREVPaymentId.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("YEARENDSETTLEMENT").Value = HdYearAndSettleMent.Value
                    '@ New Added Attributes
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONT_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PREVIOUSUPFRONTAMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BALANCEUPFRONTAMOUNT").Value = txtBalanceUpfrontAmount.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("NEXTUPFRONTAMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTTYPE").Value = hdPaymentType.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONTTYPE").Value = hdUpFronttType.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTTYPEID").Value = hdFixedOrRate.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONTFIRSTTIME").Value = hdFirstTime.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBTYPEID").Value = ""
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBSLAB").Value = ""
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("ISPLB").Value = hdIsPLB.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBAMOUNT").Value = txtPLBAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("REMARKS").Value = txtRemarks.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALI_REMARKS").Value = txtRemarks.Text

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAREMARKS").Value = txtPayAppRemarks.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTPERIOD").Value = TxtPayPeriod.Text


                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTPERIODFORMAT").Value = HdPmtFormat.value




                    '@ New Added Attributes
                    '@ Start of  Added on 06/09/10
                    'NO_OF_PAYMENT ='' SIGNUPAMOUNT='' ADJUSTABLE=''  BALANCE_CF=''   UPF_NO_OF_PAYMENT='' UPF_NO_OF_PAYMENT_DONE=''  UPF_ONETIME_NO_OF_PAYMENT='' UPF_ONETIME_NO_OF_PAYMENT_DONE=''
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("NO_OF_PAYMENT").Value = HdCurPayNo.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("ADJUSTABLE").Value = HdAdjustable.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("SIGNUPAMOUNT").Value = txtSignUpAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BALANCE_CF").Value = Val(txtCBForwardAmount.Text)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_NO_OF_PAYMENT").Value = HdUpNoPay.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_NO_OF_PAYMENT_DONE").Value = HdUpNoPayDone.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_ONETIME_NO_OF_PAYMENT").Value = HdOneTimeUpNoOfPay.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_ONETIME_NO_OF_PAYMENT_DONE").Value = HdOneTimeUpNoPayDone.Value

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("CHAIN_CODE").Value = hdChainCode.Value

                    '   QUALIAVG='' QUALITOTAL='' QUALITOTALAFTEREXEM=''
                    '     objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALIAVG").Value = HdQualAgv.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALIAVG").Value = txtQualAvgData.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALITOTAL").Value = txtTotalQualification.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALITOTALAFTEREXEM").Value = txtTotalQualificationAfterExem.Text

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBCYCLE").Value = "TRUE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBCYCLE").Value = "FALSE"
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBPAYMENTPERIOD_FROM").Value = hdPLBFROM.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBPAYMENTPERIOD_TO").Value = hdPLBTO.Value



                    '@ End of  Added on 06/09/10
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS") 'objInputXml.DocumentElement.SelectSingleNode("PRODTYPE")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objInputXml.DocumentElement.SelectSingleNode("CALCULATION").RemoveChild(objChildNode)
                    ' objInputXml.DocumentElement.RemoveChild(objChildNode)
                    Dim sumSegmentFinal As Double = 0
                    Dim sumFinal As Double = 0
                    Dim DblTotalCalCulatedSegment As Double = 0
                    Dim DblGrandFinal As Double = 0
                    For Rowno = 0 To GvProcessPayment.Rows.Count - 1
                        Dim lblProductivity As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("lblProductivity"), Label)
                        Dim hdPayMentId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdPayMentId"), HiddenField)
                        Dim txtActulaSegment As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtActulaSegment"), TextBox)
                        Dim txtCalCulatedSegment As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtCalCulatedSegment"), TextBox)
                        Dim txtExumption As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtExumption"), TextBox)
                        Dim txtFinalAmount As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtFinalAmount"), TextBox)
                        Dim txtRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtRate"), TextBox)
                        Dim hdProductivityId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdProductivityId"), HiddenField)
                        Dim hdNotInBCase As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdNotInBCase"), HiddenField)
                        Dim hdROI As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdRowID"), HiddenField)
                        Dim hdNidtFields As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdNidtFields"), HiddenField)

                        Dim txtStdRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtStdRate"), TextBox)
                        '  objChildNodeClone.InnerText = hdProductivityId.Value 'dbgrdCategoryProductDesc.Rows(Rowno).Cells(0).Text 'strAoffice
                        objChildNodeClone.Attributes("PRODUCTIVITYTYPEID").Value = hdProductivityId.Value
                        objChildNodeClone.Attributes("PRODUCTIVITYTYPE").Value = lblProductivity.Text ' ChkProductivity.Text
                        objChildNodeClone.Attributes("SEGMENT").Value = txtActulaSegment.Text
                        objChildNodeClone.Attributes("RATE").Value = txtRate.Text
                        objChildNodeClone.Attributes("EXCEMPTION").Value = txtExumption.Text
                        objChildNodeClone.Attributes("AMOUNT").Value = "0"
                        objChildNodeClone.Attributes("ISCHECKED").Value = hdNotInBCase.Value
                        objChildNodeClone.Attributes("NIDTFIELDS").Value = hdNidtFields.Value.Trim()
                        objChildNodeClone.Attributes("ROWID").Value = hdROI.Value.Trim()
                        'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                        Dim dblAmountCalculate As Double = 0
                        If txtActulaSegment.Text.Trim.Length > 0 Then
                            If txtExumption.Text.Trim.Length > 0 Then
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                            Else
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                            End If
                            txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                        Else
                            txtCalCulatedSegment.Text = "0.00"
                        End If
                        'End of Code for calculating Amount on the basis of (segment * Exemption)
                        If lblProductivity.Text.Trim().ToUpper() <> "HL" And lblProductivity.Text.Trim().ToUpper() <> "ROI" Then
                            DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                        End If

                        ' ####################################################################
                        ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    

                        '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                        Dim strRate As Double = 0
                        If txtRate.Text.Trim.Length = 0 Then
                            strRate = Val(txtStdRate.Text)
                        Else
                            strRate = Val(txtRate.Text)
                        End If
                        '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10

                        Dim dblFinalCalCulatedSegment As Double = 0
                        If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                            'If hdFixedOrRate.Value = "2" Then
                            '    ' If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                            '    If txtRate.Text.Trim.Length > 0 Then
                            '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                            '    Else
                            '        dblFinalCalCulatedSegment = Double.Parse(0) 'Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                            '    End If
                            'Else
                            '    If txtRate.Text.Trim.Length > 0 Then
                            '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                            '    Else
                            '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                            '    End If
                            'End If
                            If hdFixedOrRate.Value = "2" Then
                                dblFinalCalCulatedSegment = Double.Parse(strRate)
                            Else
                                dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                            End If

                            txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                        End If
                        ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                        '####################################################################
                        If lblProductivity.Text.Trim().ToUpper() <> "HL" And lblProductivity.Text.Trim().ToUpper() <> "ROI" Then
                            DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                        End If
                        txtFinalAmount.CssClass = "textboxgrey right"
                        txtCalCulatedSegment.CssClass = "textboxgrey right"
                        objChildNodeClone.Attributes("FINALAMOUNT").Value = dblFinalCalCulatedSegment.ToString("f0") 'Val(txtAmount.Text) - (Val(txtAmount.Text) * Val(txtExumption.Text)) / 100
                        objChildNodeClone.Attributes("PAYMENT_ID").Value = hdPayMentId.Value

                        '@Start of New Code Added On 18/10/10

                        Dim txtRemByChangeInRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtRemByChangeInRate"), TextBox)
                        objChildNodeClone.Attributes("STANDARDRATE").Value = txtStdRate.Text
                        Dim hdStandardSeg As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdStandardSeg"), HiddenField)
                        objChildNodeClone.Attributes("STANDARSEGMENT").Value = hdStandardSeg.Value
                        objChildNodeClone.Attributes("RATEREM").Value = txtRemByChangeInRate.Text
                        '@ End of New  Code Added On 18/10/10

                        ' sumFinal = sumFinal + Val(txtFinalAmount.Text)
                        'objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objInputXml.DocumentElement.SelectSingleNode("CALCULATION").AppendChild(objChildNodeClone)

                        objChildNodeClone = objChildNode.CloneNode(True)
                    Next Rowno
                    If GvProcessPayment.Rows.Count > 0 Then
                        Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
                        Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
                        txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
                        txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")
                    End If
                    Dim sum As Double = 0
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("TOTAL_AMT").Value = (DblGrandFinal + sum).ToString("f0")
                    ' DblTotalCalCulatedSegment.ToString("f2")
                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("EMPLOYEE_ID").Value = Session("LoginSession").ToString().Split("|")(0)
                    End If
                    ' SOLE_AMOUNT='FALSE' BONUS_AMOUNT='FALSE' FIXED_PAYMENT='TRUE' UPFRONT_AMOUNT='FALSE' FIXED_UPFRONT='FALSE'
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("SOLE_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_PAYMENT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONT_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_UPFRONT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BONUS_AMOUNT").Value = "0.00"


                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        If HdPlbTypeId.Value <> "2" Then '@ In case of Fixed PLB
                            objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("TOTAL_AMT").Value = objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBAMOUNT").Value
                        End If
                    End If


                    ' Here Back end Method Call
                    'Code Added by Mukund for Importing Rows of View
                    Dim xmlView As New XmlDocument
                    If ViewState("PaymentData") IsNot Nothing Then
                        xmlView.LoadXml(ViewState("PaymentData"))
                    Else
                        lblError.Text = "Invalid Action"
                        Exit Sub
                    End If
                    For Each xN As XmlNode In xmlView.DocumentElement.SelectNodes("PRODTYPE")
                        objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xN, True))
                    Next
                    For Each xN As XmlNode In xmlView.DocumentElement.SelectNodes("PLBTYPE")
                        objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xN, True))
                    Next
                    Try
                        objInputXml.Save("C:\Admin\UpdatePaymentProcessDetailsInput.xml")
                    Catch ex As Exception
                    End Try
                    ' objOutputXml = objbzProcessPayment.UpdatePaymentProcessDetails(objInputXml)


                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        objOutputXml = objbzProcessPaymentForPLB.UpdatePLBPaymentProcessDetails(objInputXml)
                    Else
                        objOutputXml = objbzProcessPayment.UpdatePaymentProcessDetails(objInputXml)
                    End If

                    Try
                        objOutputXml.Save("C:\Admin\UpdatePaymentProcessDetailsOutput.xml")
                    Catch ex As Exception
                    End Try
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = "Payment Updated Successfully."
                        If Request.QueryString("Chain_Code") IsNot Nothing Then
                            hdEnChainCode.Value = Request.QueryString("Chain_Code")
                            hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                            If Request.QueryString("BCaseID") IsNot Nothing Then
                                LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                            End If
                        End If
                        StrSaveData = "Saved"
                    Else
                        StrSaveData = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                StrSaveData = ex.Message
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub

    Protected Sub BtnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendForApproval.Click
        StrSaveData = ""

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            lblError.Text = "Session is expired."
            Exit Sub
        End If

        Dim objXmlOuputdoc As New XmlDocument
        If ViewState("PaymentData") IsNot Nothing Then
            objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
            If txtBalanceUpfrontAmount.Visible = True Then
                If Val(objXmlOuputdoc.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BALANCEUPFRONTAMOUNT").Value) <> Val(txtBalanceUpfrontAmount.Text) Then
                    mdlPopUpExt.Show()
                    Exit Sub
                End If
            End If
          
            If txtPLBAmt.Visible = True Then
                If Val(objXmlOuputdoc.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("PLBAMOUNT").Value) <> Val(txtPLBAmt.Text) Then
                    mdlPopUpExt.Show()
                    Exit Sub
                End If
            End If
            For Rownum As Integer = 0 To GvProcessPayment.Rows.Count - 1
                Dim txtExumption As TextBox = CType(GvProcessPayment.Rows(Rownum).FindControl("txtExumption"), TextBox)
                Dim txtRate As TextBox = CType(GvProcessPayment.Rows(Rownum).FindControl("txtRate"), TextBox)
                Dim txtStdRate As TextBox = CType(GvProcessPayment.Rows(Rownum).FindControl("txtStdRate"), TextBox)
                Dim txtRemByChangeInRate As TextBox = CType(GvProcessPayment.Rows(Rownum).FindControl("txtRemByChangeInRate"), TextBox)
                Dim objPRODTYPEDETAILSNode As XmlNode
                Dim hdRowID As HiddenField = CType(GvProcessPayment.Rows(Rownum).FindControl("hdRowID"), HiddenField)
                Dim strCaseName As String = CType(GvProcessPayment.Rows(Rownum).FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
              
                For Each objPRODTYPEDETAILSNode In objXmlOuputdoc.DocumentElement.SelectNodes("CALCULATION/PRODTYPEDETAILS")
                    If strCaseName.ToUpper() = objPRODTYPEDETAILSNode.Attributes("PRODUCTIVITYTYPE").Value.ToUpper() Then
                        If Val(txtExumption.Text) <> Val(objPRODTYPEDETAILSNode.Attributes("EXCEMPTION").Value) Then
                            mdlPopUpExt.Show()
                            Exit Sub
                        End If
                        If Val(txtRate.Text) <> Val(objPRODTYPEDETAILSNode.Attributes("RATE").Value) Then
                            mdlPopUpExt.Show()
                            Exit Sub
                        End If
                        If txtRemByChangeInRate.Text <> objPRODTYPEDETAILSNode.Attributes("RATEREM").Value Then
                            mdlPopUpExt.Show()
                            Exit Sub
                        End If

                    End If
                Next
            Next
        Else
            lblError.Text = "Session is Expired."
            Exit Sub
        End If

        '  Exit Sub

        If HdPaymentId.Value.Trim.Length > 0 Then
            Dim objInputXml As New XmlDocument
            Dim objOutPutxml As New XmlDocument
            ' Dim ds As DataSet
            ' Dim objReadaer As XmlNodeReader
            Dim objbzPaymentApprovalLavel As New AAMS.bizIncetive.bzPaymentApprovalQue
            '@ Code  for Details
            Try
                objInputXml.LoadXml("<UP_INC_PAYMENT_GENERATE_PADVICE_INPUT>  <PAYMENT_ID/><SORT_BY></SORT_BY><DESC>0</DESC><PAGE_NO>0</PAGE_NO><PAGE_SIZE>0</PAGE_SIZE></UP_INC_PAYMENT_GENERATE_PADVICE_INPUT >")
                objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = HdPaymentId.Value ' strBcaseId
                objOutPutxml = objbzPaymentApprovalLavel.Generate_Payment_Advice(objInputXml)
                If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    Session("Msg") = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    Response.Redirect(Request.Url.ToString, False)
                Else
                    lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
    Private Sub LoadDataByPayId(ByVal strPayId As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Dim DblGrandFinal As Double = 0
        Dim objbzbzPaymentApprovalQue As New AAMS.bizIncetive.bzPaymentApprovalQue
        '    Dim objbzbzPaymentApprovalQueForPLB As New AAMS.bizIncetive.bzPaymentApprovalQuePLB
        '@ Code  for Details
        Try
            objInputXml.LoadXml("<UP_VIEW_INC_PAYMENT_APPROVAL_QUE_INPUT>     <PAYMENT_ID></PAYMENT_ID>  <NO_OF_PAYMENT></NO_OF_PAYMENT>  <PLB></PLB><PLBPAYMENTPERIOD_FROM></PLBPAYMENTPERIOD_FROM><PLBPAYMENTPERIOD_TO></PLBPAYMENTPERIOD_TO>   </UP_VIEW_INC_PAYMENT_APPROVAL_QUE_INPUT >")
            objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = strPayId
            objInputXml.DocumentElement.SelectSingleNode("NO_OF_PAYMENT").InnerText = HdCurPayNo.Value

            'Start of  New Code Added For PLB , PLBPAYMENTPERIOD_FROM  and PLBPAYMENTPERIOD_TO
            If Request.QueryString("PLB") IsNot Nothing Then
                If Request.QueryString("PLB").Trim = "1" Then
                    objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "True"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
            End If

            If Request.QueryString("Period") IsNot Nothing Then
                Dim strPeriod As String = Request.QueryString("Period").ToString
                If strPeriod.Split("-").Length = 2 Then
                    Dim strPeriodFrom As String = strPeriod.Split("-")(0).Trim
                    Dim strPeriodTo As String = strPeriod.Split("-")(1).Trim
                    Dim strPeriodFinalFrom As String = ""
                    If strPeriodFrom.Trim.Split("/").Length = 3 Then
                        strPeriodFinalFrom = strPeriodFrom.Split("/")(0).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(1).ToString().PadLeft("2", "0")
                        strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(2).ToString().PadLeft("4", "0")
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_FROM").InnerText = strPeriodFinalFrom
                    objInputXml.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_TO").InnerText = strPeriodTo
                End If
            End If
            'End  of  New Code Added For PLB , PLBPAYMENTPERIOD_FROM  and PLBPAYMENTPERIOD_TO


            Try
                objInputXml.Save("C:\Admin\BCPaymentProcessViewInput.xml")
            Catch ex As Exception
            End Try
            objOutPutxml = objbzbzPaymentApprovalQue.View(objInputXml)


            'If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
            '    objOutPutxml = objbzbzPaymentApprovalQueForPLB.View(objInputXml)
            'Else
            '    objOutPutxml = objbzbzPaymentApprovalQue.View(objInputXml)
            'End If


            'objOutPutxml.Load("C:\AAMSXml\objOutPutxmlPayView.xml")
            Try
                objOutPutxml.Save("C:\Admin\BCPaymentProcessViewOut.xml")
            Catch ex As Exception
            End Try
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PaymentData") = objOutPutxml.OuterXml
                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                dset = New DataSet
                dset = ds
                If ds.Tables("GROUPDETAILS") IsNot Nothing Then
                    '<GROUPDETAILS ACCOUNTMANAGER='' CHAIN_CODE='' GROUP='' NAME='' AOFFICE='' CITY=''/>
                    txtGroupName.Text = ds.Tables("GROUPDETAILS").Rows(0)("GROUPNAME").ToString
                    txtActManager.Text = ds.Tables("GROUPDETAILS").Rows(0)("ACCOUNTMANAGER").ToString
                    txtChainCode.Text = ds.Tables("GROUPDETAILS").Rows(0)("CHAIN_CODE").ToString
                    txtAoffice.Text = ds.Tables("GROUPDETAILS").Rows(0)("AOFFICE").ToString
                    txtCity.Text = ds.Tables("GROUPDETAILS").Rows(0)("CITY").ToString
                    'txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("REMARKS").ToString
                    txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALI_REMARKS").ToString


                    If ds.Tables("GROUPDETAILS").Columns("PAREMARKS") IsNot Nothing Then
                        txtPayAppRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("PAREMARKS").ToString
                    End If

                    If ds.Tables("GROUPDETAILS").Columns("PAYMENTPERIODFORMAT") IsNot Nothing Then
                        HdPmtFormat.value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTPERIODFORMAT").ToString
                    End If




                    ''@ This Code is used for setting the value For AvgQualification/TotalQualification / TotalQualificationAfterExemption
                    txtQualAvg.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString
                    HdQualAgv.Value = ds.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                    txtQualAvgData.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                    txtTotalQualification.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALITOTAL").ToString
                    'txtTotalQualificationAfterExem.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALITOTALAFTEREXEM").ToString

                    TxtConPercentage.Text = ds.Tables("GROUPDETAILS").Rows(0)("CONV_PER").ToString
                    TxtMinSegment.Text = ds.Tables("GROUPDETAILS").Rows(0)("MINSEGMENT").ToString
                    ''@ This Code is used for setting the value For AvgQualification/TotalQualification / TotalQualificationAfterExemption


                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString.Trim.ToUpper = "P" Then
                        txtPaymentType.Text = "Post Payment"
                    Else
                        txtPaymentType.Text = "Upfront Payment"
                    End If
                    'If ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString.Trim = "1" Then
                    '    txtPaymentTerm.Text = "One time"
                    'ElseIf ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString.Trim = "2" Then
                    '    txtPaymentTerm.Text = "Replinishable"
                    'Else
                    '    txtPaymentTerm.Text = "Fixed"
                    'End If
                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString.Trim = "1" Then
                        txtAdjustment.Text = "Rate"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString.Trim = "2" Then
                        txtAdjustment.Text = "Fixed Payment"
                    End If
                    If ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "1" Then
                        txtBillCycle.Text = "Annual"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "2" Then
                        txtBillCycle.Text = "Bi-Annual"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "3" Then
                        txtBillCycle.Text = "Qtrly"
                    ElseIf ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString = "4" Then
                        txtBillCycle.Text = "Monthly"
                    Else
                        txtBillCycle.Text = ds.Tables("GROUPDETAILS").Rows(0)("BILLINGCYCLE").ToString
                    End If
                    TxtPayPeriod.Text = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTPERIOD").ToString
                    HdPACreated.Value = ds.Tables("GROUPDETAILS").Rows(0)("PA_CREATED").ToString.Trim.ToUpper
                    HdPaymentId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENT_ID").ToString
                    HdpREVPaymentId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PREVPAYMENT_ID").ToString
                    hdFinallyApproved.Value = ds.Tables("GROUPDETAILS").Rows(0)("FINALLYAPPROVED").ToString
                    hdEndSettlement.Value = ds.Tables("GROUPDETAILS").Rows(0)("FINALSETTLEMENT").ToString
                    If ds.Tables("GROUPDETAILS").Rows(0)("YEARENDSETTLEMENT") IsNot Nothing Then
                        HdYearAndSettleMent.Value = ds.Tables("GROUPDETAILS").Rows(0)("YEARENDSETTLEMENT").ToString
                    End If
                    '@ Start of Addded on 06/09/10

                    '@ Start of Added On 04/09/10
                    If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
                        ImgEndyearSettelement.Visible = True
                    Else
                        ImgEndyearSettelement.Visible = False

                    End If
                    HdUpNoPay.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPF_NO_OF_PAYMENT").ToString
                    HdAdjustable.Value = ds.Tables("GROUPDETAILS").Rows(0)("ADJUSTABLE").ToString
                    txtSignUpAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SIGNUPAMOUNT").ToString).ToString("f2")
                    txtCBForwardAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BALANCE_CF").ToString).ToString("f2")

                    HdPlbTypeId.Value = ds.Tables("GROUPDETAILS").Rows(0)("PLBTYPEID").ToString

                    If HdAdjustable.Value = "True" Then
                        txtSignupAdjustable.Text = "Yes"
                    Else
                        txtSignupAdjustable.Text = "No"
                    End If

                    If HdAdjustable.Value.Trim.ToUpper = "TRUE" And Val(txtSignUpAmt.Text) > 0 Then
                        SpnSignAmt.Visible = True
                        TxtSpnSignAmt.Visible = True
                        TxtSpnSignAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SIGNUPAMOUNT").ToString).ToString("f2")
                    End If

                    hdShowPopup.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSINGPERIOD").ToString
                    HdMisNoOfPay.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_NO_OF_PAYMENT").ToString
                    HdMisYear.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_YEAR").ToString
                    HdMisMonth.Value = ds.Tables("GROUPDETAILS").Rows(0)("MISSING_MONTH").ToString


                    '@ End of Addded on 06/09/10
                    hdFixedOrRate.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString
                    hdFirstTime.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTFIRSTTIME").ToString
                    hdPaymentType.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString
                    hdUpFronttType.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTTYPE").ToString
                    hdIsPLB.Value = ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString

                    hdChainCode.Value = ds.Tables("GROUPDETAILS").Rows(0)("CHAIN_CODE").ToString
                    ' hdEnChainCode.Value = objED.Encrypt(hdChainCode.Value)

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        If ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString.Trim().ToUpper = "TRUE" Then
                            If HdPlbTypeId.Value <> "2" Then   ' @ In Case Of PLB Fixed Based
                                LblPlb.Visible = True
                                txtPLBAmt.Visible = True
                                txtPLBAmt.Text = ds.Tables("GROUPDETAILS").Rows(0)("PLBAMOUNT").ToString.Trim()
                                trLPlb.Visible = True
                            End If
                        End If
                    End If

                End If
                Try
                    ' If hdFirstTime.Value.Trim.ToString.ToUpper = "TRUE" Then
                    If HdCurPayNo.Value = "1" Then
                        lblBalanceUpfrontAmount.Visible = False
                        LblSignUpAmt.Visible = True
                        LblCBFAmount.Visible = False
                        txtBalanceUpfrontAmount.Visible = False
                        txtSignUpAmt.Visible = True
                        txtCBForwardAmount.Visible = False
                        trBalanceUpfrontAmount.Visible = False
                        trSignUpAmt.Visible = True
                        trCBFAmount.Visible = False
                    Else
                        lblBalanceUpfrontAmount.Visible = True
                        LblSignUpAmt.Visible = False
                        LblCBFAmount.Visible = True
                        txtBalanceUpfrontAmount.Visible = True
                        txtSignUpAmt.Visible = False
                        txtCBForwardAmount.Visible = True
                        trBalanceUpfrontAmount.Visible = True
                        trSignUpAmt.Visible = False
                        trCBFAmount.Visible = True
                    End If


                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        lblBalanceUpfrontAmount.Visible = False
                        LblSignUpAmt.Visible = False
                        LblCBFAmount.Visible = False
                        txtBalanceUpfrontAmount.Visible = False
                        txtSignUpAmt.Visible = False
                        txtCBForwardAmount.Visible = False
                        trBalanceUpfrontAmount.Visible = False
                        trSignUpAmt.Visible = False
                        trCBFAmount.Visible = False

                    End If

                Catch ex As Exception
                End Try
                Try
                    If ds.Tables("PRODTYPEDETAILS") IsNot Nothing Then
                        If ds.Tables("PRODTYPEDETAILS").Rows(0)("PRODUCTIVITYTYPEID").ToString.Trim.Length > 0 Then
                            GvProcessPayment.DataSource = ds.Tables("PRODTYPEDETAILS")
                            GvProcessPayment.DataBind()
                            HdPaymentId.Value = ds.Tables("PRODTYPEDETAILS").Rows(0)("PAYMENT_ID").ToString
                            If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then
                                ' If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                                GvProcessPayment.HeaderRow.Cells(5).Text = "Standard Fixed / Revised Fixed"
                                'GvProcessPayment.HeaderRow.Cells(5).Text = "Revised Fixed"
                            End If
                            If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                                If HdPlbTypeId.Value = "2" Then   ' @ In Case Of PLB Rate Based
                                    GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Rate / Revised Rate"
                                Else
                                    GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Fixed / Revised Fixed"
                                End If
                            End If

                            GvProcessPayment.FooterRow.Cells(2).Text = "Total"
                            GvProcessPayment.FooterRow.Cells(3).HorizontalAlign = HorizontalAlign.Center
                            GvProcessPayment.FooterRow.Cells(4).HorizontalAlign = HorizontalAlign.Right
                            GvProcessPayment.FooterRow.Cells(5).HorizontalAlign = HorizontalAlign.Right
                            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
                            Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
                            txtGrandFinalAmount.Text = 0
                            txtTotalCalCulatedSegment.Text = 0
                            'Dim strTotalCalCulatedSegment As String = ""
                            'Dim strGrandFinal As String = ""
                            Dim DblTotalCalCulatedSegment As Double = 0
                            For Each row As GridViewRow In GvProcessPayment.Rows
                                Dim txtActulaSegment As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                                Dim txtCalCulatedSegment As TextBox = CType(row.FindControl("txtCalCulatedSegment"), TextBox)
                                Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                                Dim txtFinalAmount As TextBox = CType(row.FindControl("txtFinalAmount"), TextBox)
                                Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                                Dim hdProductivityId As Label = CType(row.FindControl("lblProductivity"), Label)

                                Dim txtStdRate As TextBox = CType(row.FindControl("txtStdRate"), TextBox)

                                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                                Dim dblAmountCalculate As Double = 0
                                If txtActulaSegment.Text.Trim.Length > 0 Then
                                    If txtExumption.Text.Trim.Length > 0 Then
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                                    Else
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                                    End If
                                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                                Else
                                    txtCalCulatedSegment.Text = "0.00"
                                End If
                                'End of Code for calculating Amount on the basis of (segment * Exemption)
                                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                                    DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                                End If
                                ' ####################################################################
                                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    


                                '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                                Dim strRate As Double = 0
                                If txtRate.Text.Trim.Length = 0 Then
                                    strRate = Val(txtStdRate.Text)
                                Else
                                    strRate = Val(txtRate.Text)
                                End If
                                '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10

                                Dim dblFinalCalCulatedSegment As Double = 0
                                If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                                    'If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then

                                    '    If txtRate.Text.Trim.Length > 0 Then
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                                    '    Else
                                    '        dblFinalCalCulatedSegment = Double.Parse(0) 'Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                                    '    End If
                                    'Else
                                    '    If txtRate.Text.Trim.Length > 0 Then
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                                    '    Else
                                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                                    '    End If
                                    'End If
                                    If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString() = "2" Then
                                        dblFinalCalCulatedSegment = Double.Parse(strRate)
                                    Else
                                        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                                    End If

                                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                                End If
                                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                                '####################################################################
                                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                                    DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                                End If
                                txtFinalAmount.CssClass = "textboxgrey right"
                                txtCalCulatedSegment.CssClass = "textboxgrey right"
                            Next
                            txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
                            'strGrandFinal = DblGrandFinal.ToString("f2")
                            txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")
                        End If
                    Else
                        GvProcessPayment.DataSource = Nothing
                        GvProcessPayment.DataBind()
                    End If
                Catch ex As Exception
                    ' lblError.Text = ex.Message
                End Try
            Else
                GvProcessPayment.DataSource = Nothing
                GvProcessPayment.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            BreaupData(objOutPutxml)

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            If HdPaymentId.Value.Trim.Length > 0 Then
                BtnSendForApproval.Enabled = True
            Else
                BtnSendForApproval.Enabled = False
            End If
        End Try
        If HdPACreated.Value.ToUpper = "TRUE" Then
            BtnSendForApproval.Enabled = False
        End If
        calculatebalance()
        ' btnSave.Enabled = False
        If hdFinallyApproved.Value.Trim.ToUpper = "TRUE" Then
            btnSave.Enabled = False
            BtnSendForApproval.Enabled = False
        End If
        HideUnhideSendForApproval()
        '@Start of  For Payment Sheet
        BtnPaymentSheetReport.Enabled = False
        If HdPaymentId.Value.Trim.Length > 0 Then
            If Request.QueryString("BCaseID") IsNot Nothing Then
                If Request.QueryString("Month") IsNot Nothing Then
                    If Request.QueryString("Year") IsNot Nothing Then
                        EnableDiablePaymentSheet(Request.QueryString("BCaseID"), hdEnChainCode.Value, Request.QueryString("Month"), Request.QueryString("Year"), "U", HdCurPayNo.Value, HdPaymentId.Value.Trim)
                    End If
                End If
            End If
        Else
            BtnPaymentSheetReport.Enabled = False
        End If
        '@End of  For Payment Sheet
        LoadMIDDataAndQualificatiinSlab()
    End Sub

    Protected Sub txtRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            Dim txtExp As TextBox = CType(sender, TextBox)
            Dim index As Integer = Integer.Parse(txtExp.Attributes("CalculatePaymentRM"))
            Dim gvrow As GridViewRow = GvProcessPayment.Rows(index)
            'Dim txtFinalAmount As TextBox = CType(gvrow.FindControl("txtFinalAmount"), TextBox)
            ' txtFinalAmount.Text = (100 * index).ToString
            Dim kk As Integer = 100
            '  txtExp.Focus()
            If txtExp.Text.Trim.Length <= 0 Then
                'Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                'scriptManager.SetFocus(txtExp.ClientID)
                ''txtExp.Text = 0
                'Exit Sub
            End If
            '@ Start Checking Decimal Value
            If txtExp.Text.Trim.Length > 0 Then
                Dim reg As Regex = New Regex("^[0-9.]+$")
                If reg.IsMatch(txtExp.Text) Then
                    Dim countDecimalNo As Integer = 0
                    For i As Integer = 0 To txtExp.Text.Length - 1
                        If txtExp.Text.Chars(i) = "." Then
                            countDecimalNo = countDecimalNo + 1
                        End If
                    Next
                    If countDecimalNo > 1 Then
                        lblError.Text = "Only numeric with decimal is allowed."
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        scriptManager.SetFocus(txtExp.ClientID)
                        Exit Sub
                    End If
                Else
                    lblError.Text = "Only numeric with decimal is allowed."
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtExp.ClientID)
                    Exit Sub

                End If
            End If


            '##############################################################################################
            '@Start of  New Added Code if Revised rate is different than standard rate then RateRem is mandatory 20/10/10
            'Dim txtStdRate As TextBox = CType(gvrow.FindControl("txtStdRate"), TextBox)
            'Dim txtRemByChangeInRate As TextBox = CType(gvrow.FindControl("txtRemByChangeInRate"), TextBox)
            'If Val(txtExp.Text.Trim) <> Val(txtStdRate.Text) Then
            '    If txtRemByChangeInRate.Text.Trim.Length = 0 Then
            '        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
            '        lblError.Text = "Remark is mandatory."
            '        scriptManager.SetFocus(txtRemByChangeInRate.ClientID)
            '        Exit Sub
            '    End If
            'End If
            '@ End of New Added Code if Revised rate is different than standard rate then RateRem is mandatory
            '##############################################################################################




            '@ Commented By Abhishek on  06/10/10
            '@ End of  Checking Decimal Value    
            'Code for Calculation of HL
            'Code Added by Mukund
            'ByVal strROWid As String, ByVal intRate As Double, ByVal strType As String
            'If CType(gvrow.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper() = "HL" Then
            '    For Each row As GridViewRow In GvProcessPayment.Rows
            '        Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
            '        If strCaseName.ToUpper() <> "HL" And strCaseName.ToUpper() <> "ROI" Then
            '            Dim txtActulaSegment1 As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
            '            Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
            '            Dim txtExp1 As Double = Val(txtExp.Text)
            '            'Code Seg for Finding actual value of Calculation part
            '            Dim objXmlOuputdoc As New XmlDocument
            '            If ViewState("PaymentData") IsNot Nothing Then
            '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
            '            Else
            '                lblError.Text = "Invalid Action"
            '                Return
            '            End If
            '            Dim objFilteredNode As XmlNode = objXmlOuputdoc.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS[@ROWID='" + hdRowID.Value.Trim() + "']")
            '            Dim dbTotal As Double = 0
            '            If objFilteredNode IsNot Nothing Then
            '                dbTotal = Val(objFilteredNode.Attributes("SEGMENT").Value)
            '            End If
            '            'Code Seg for Finding actual value of Calculation part
            '            txtActulaSegment1.Text = (dbTotal + Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "HL"))).ToString()
            '        End If
            '    Next
            'End If
            'Dim strHLRate As String = ""
            'For Each row As GridViewRow In GvProcessPayment.Rows
            '    Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
            '    If strCaseName.ToUpper() = "HL" Then
            '        strHLRate = CType(row.FindControl("txtExumption"), TextBox).Text.Trim()
            '    End If
            'Next
            'If CType(gvrow.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper() = "ROI" Then
            '    For Each row As GridViewRow In GvProcessPayment.Rows
            '        Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
            '        If strCaseName.ToUpper() <> "HL" And strCaseName.ToUpper() <> "ROI" Then
            '            Dim txtActulaSegment1 As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
            '            Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
            '            Dim txtExp1 As Double = Val(txtExp.Text)
            '            'Code Seg for Finding actual value of Calculation part
            '            Dim objXmlOuputdoc1 As New XmlDocument
            '            If ViewState("PaymentData") IsNot Nothing Then
            '                objXmlOuputdoc1.LoadXml(ViewState("PaymentData"))
            '            Else
            '                lblError.Text = "Invalid Action"
            '                Return
            '            End If
            '            Dim objFilteredNode1 As XmlNode = objXmlOuputdoc1.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS[@ROWID='" + hdRowID.Value.Trim() + "']")
            '            Dim dbTotal As Double = 0
            '            If objFilteredNode1 IsNot Nothing Then
            '                dbTotal = Val(objFilteredNode1.Attributes("SEGMENT").Value)
            '            End If
            '            'Code Seg for Finding actual value of Calculation part
            '            'Dim strHLCalculation As String = CalculateGridData(hdRowID.Value.Trim(), strHLRate, "HL")
            '            'txtActulaSegment1.Text = (dbTotal + Val(strHLCalculation) + Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "ROI"))).ToString()
            '            Dim dbROI As Double = Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "ROI"))
            '            If dbROI > 0 Then
            '                Dim strHLCalculation As String = CalculateGridData(hdRowID.Value.Trim(), strHLRate, "HL")
            '                txtActulaSegment1.Text = (dbTotal + Val(strHLCalculation) + dbROI).ToString()
            '            End If
            '        End If
            '    Next
            'End If
            'Code Added by Mukund
            '@ Commented By Abhishek on 06/10/10

            '@ Start code for Calculation
            Dim DblGrandFinal As Double = 0
            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
            Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
            txtGrandFinalAmount.Text = 0
            txtTotalCalCulatedSegment.Text = 0
            Dim DblTotalCalCulatedSegment As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim txtActulaSegment As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                Dim txtCalCulatedSegment As TextBox = CType(row.FindControl("txtCalCulatedSegment"), TextBox)
                Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                Dim txtFinalAmount As TextBox = CType(row.FindControl("txtFinalAmount"), TextBox)
                Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                Dim hdProductivityId As Label = CType(row.FindControl("lblProductivity"), Label)
                Dim txtStdRate As TextBox = CType(row.FindControl("txtStdRate"), TextBox)
                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                Dim dblAmountCalculate As Double = 0
                If txtActulaSegment.Text.Trim.Length > 0 Then
                    If txtExumption.Text.Trim.Length > 0 Then
                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                    Else
                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                    End If
                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                Else
                    txtCalCulatedSegment.Text = "0.00"
                End If
                'End of Code for calculating Amount on the basis of (segment * Exemption)
                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                    DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                End If
                ' ####################################################################
                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    

                '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                Dim strRate As Double = 0
                If txtRate.Text.Trim.Length = 0 Then
                    strRate = Val(txtStdRate.Text)
                Else
                    strRate = Val(txtRate.Text)
                End If
                '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10



                Dim dblFinalCalCulatedSegment As Double = 0
                If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                    'If hdFixedOrRate.Value = "2" Then
                    '    If txtRate.Text.Trim.Length > 0 Then
                    '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                    '    Else
                    '        dblFinalCalCulatedSegment = Double.Parse(0) 'Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                    '    End If
                    'Else
                    '    If txtRate.Text.Trim.Length > 0 Then
                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                    '    Else
                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                    '    End If
                    'End If

                    If hdFixedOrRate.Value = "2" Then
                        dblFinalCalCulatedSegment = Double.Parse(strRate)
                    Else
                        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                    End If

                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                End If
                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                '####################################################################
                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                    DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                End If
                txtFinalAmount.CssClass = "textboxgrey right"
                txtCalCulatedSegment.CssClass = "textboxgrey right"
            Next
            txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
            'strGrandFinal = DblGrandFinal.ToString("f2")
            txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")
            calculatebalance()
            '@ Starts code for Settting Focus on Next Row on txtExumption Controls.
            If GvProcessPayment.Rows.Count > index + 1 Then

                Dim rownext As GridViewRow = GvProcessPayment.Rows(index + 1)
                Dim kkinh As Double = Val(txtExp.Text) * 100
                If rownext IsNot Nothing Then
                    Dim txtNextExp As TextBox = CType(rownext.FindControl("txtExumption"), TextBox)
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtNextExp.ClientID)
                End If
            Else
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(btnSave.ClientID)
            End If
            '@ End code for Settting Focus on Next Row on txtExumption Controls.
        Catch ex As Exception
        Finally
            If hdFinallyApproved.Value.Trim.ToUpper = "TRUE" Then
                btnSave.Enabled = False
                BtnSendForApproval.Enabled = False
            End If
            HideUnhideSendForApproval()
        End Try
    End Sub
    Protected Sub txtExumption_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub

            End If
            Dim txtExp As TextBox = CType(sender, TextBox)
            Dim index As Integer = Integer.Parse(txtExp.Attributes("CalculatePayment"))
            Dim gvrow As GridViewRow = GvProcessPayment.Rows(index)
            'Dim txtFinalAmount As TextBox = CType(gvrow.FindControl("txtFinalAmount"), TextBox)
            ' txtFinalAmount.Text = (100 * index).ToString
            Dim kk As Integer = 100
            '  txtExp.Focus()
            If txtExp.Text.Trim.Length <= 0 Then
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(txtExp.ClientID)
                txtExp.Text = 0
                Exit Sub
            End If
            '@ Start Checking Decimal Value
            Dim reg As Regex = New Regex("^[0-9.]+$")
            If reg.IsMatch(txtExp.Text) Then
                Dim countDecimalNo As Integer = 0
                For i As Integer = 0 To txtExp.Text.Length - 1
                    If txtExp.Text.Chars(i) = "." Then
                        countDecimalNo = countDecimalNo + 1
                    End If
                Next
                If countDecimalNo > 1 Then
                    lblError.Text = "Only numeric with decimal is allowed."
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtExp.ClientID)
                    Exit Sub
                End If
            Else
                lblError.Text = "Only numeric with decimal is allowed."
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(txtExp.ClientID)
                Exit Sub

            End If
            If Val(txtExp.Text.Trim) > 100 Then
                lblError.Text = "Exemption can't be greater than 100."
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(txtExp.ClientID)
                Exit Sub
            End If


            'Code for Calculation of HL
            'Code Added by Mukund
            'ByVal strROWid As String, ByVal intRate As Double, ByVal strType As String

            '@  Start of Added by Abhishek on 06/10/10
            Dim strROIRate As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                If strCaseName.ToUpper() = "ROI" Then
                    strROIRate = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
                End If
            Next
            '@ End of Added by Abhishek on 06/10/10

            If CType(gvrow.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper() = "HL" Then
                For Each row As GridViewRow In GvProcessPayment.Rows
                    Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                    If strCaseName.ToUpper() <> "HL" And strCaseName.ToUpper() <> "ROI" Then
                        Dim txtActulaSegment1 As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                        Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
                        Dim txtExp1 As Double = Val(txtExp.Text)
                        'Code Seg for Finding actual value of Calculation part
                        Dim objXmlOuputdoc As New XmlDocument
                        If ViewState("PaymentData") IsNot Nothing Then
                            objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
                        Else
                            lblError.Text = "Invalid Action"
                            Return
                        End If
                        Dim objFilteredNode As XmlNode = objXmlOuputdoc.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS[@ROWID='" + hdRowID.Value.Trim() + "']")
                        Dim dbTotal As Double = 0
                        If objFilteredNode IsNot Nothing Then
                            '  dbTotal = Val(objFilteredNode.Attributes("SEGMENT").Value)
                            dbTotal = Val(objFilteredNode.Attributes("STANDARSEGMENT").Value)
                        End If

                        'Code Seg for Finding actual value of Calculation part

                        '@ Start of Commented By Abhishek on 06/10/10
                        ' txtActulaSegment1.Text = (dbTotal + Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "HL"))).ToString()
                        '@ End of  of Commented By Abhishek on 06/10/10

                        '@ Start of Added By Abhishek on 06/10/10
                        txtActulaSegment1.Text = dbTotal
                        Dim strHLCalculation As Double = 0
                        strHLCalculation = CalculateGridData(hdRowID.Value.Trim(), txtExp1, "HL")
                        If Val(strHLCalculation) <> 0 Then
                            txtActulaSegment1.Text = (dbTotal + Val(strHLCalculation)).ToString()
                        End If
                        Dim dbROI As Double = 0
                        dbROI = Val(CalculateGridData(hdRowID.Value.Trim(), Val(strROIRate), "ROI"))
                        If dbROI <> 0 Then
                            txtActulaSegment1.Text = (Val(txtActulaSegment1.Text) + dbROI).ToString()
                        End If
                        '@ End of Added By Abhishek on 06/10/10
                    End If
                Next
            End If
            Dim strHLRate As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                If strCaseName.ToUpper() = "HL" Then
                    strHLRate = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
                End If
            Next
            If CType(gvrow.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper() = "ROI" Then
                For Each row As GridViewRow In GvProcessPayment.Rows
                    Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                    If strCaseName.ToUpper() <> "HL" And strCaseName.ToUpper() <> "ROI" Then
                        Dim txtActulaSegment1 As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                        Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
                        Dim txtExp1 As Double = Val(txtExp.Text)
                        'Code Seg for Finding actual value of Calculation part
                        Dim objXmlOuputdoc1 As New XmlDocument
                        If ViewState("PaymentData") IsNot Nothing Then
                            objXmlOuputdoc1.LoadXml(ViewState("PaymentData"))
                        Else
                            lblError.Text = "Invalid Action"
                            Return
                        End If
                        Dim objFilteredNode1 As XmlNode = objXmlOuputdoc1.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS[@ROWID='" + hdRowID.Value.Trim() + "']")
                        Dim dbTotal As Double = 0
                        If objFilteredNode1 IsNot Nothing Then
                            '  dbTotal = Val(objFilteredNode1.Attributes("SEGMENT").Value)
                            dbTotal = Val(objFilteredNode1.Attributes("STANDARSEGMENT").Value)
                        End If
                        'Code Seg for Finding actual value of Calculation part

                        '@ Start of Commented By Abhishek on 06/10/10
                        'Dim dbROI As Double = Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "ROI"))
                        'If dbROI > 0 Then
                        '    Dim strHLCalculation As String = CalculateGridData(hdRowID.Value.Trim(), strHLRate, "HL")
                        '    txtActulaSegment1.Text = (dbTotal + Val(strHLCalculation) + dbROI).ToString()
                        'End If
                        '@ End  of Commented By Abhishek on 06/10/10

                        '@ Start of Added By Abhishek on 06/10/10
                        txtActulaSegment1.Text = dbTotal
                        Dim strHLCalculation As Double = 0
                        strHLCalculation = CalculateGridData(hdRowID.Value.Trim(), Val(strHLRate), "HL")
                        If strHLCalculation <> 0 Then
                            txtActulaSegment1.Text = (dbTotal + Val(strHLCalculation)).ToString()
                        End If
                        Dim dbROI As Double = 0
                        dbROI = Val(CalculateGridData(hdRowID.Value.Trim(), txtExp1, "ROI"))
                        If dbROI <> 0 Then
                            txtActulaSegment1.Text = (Val(txtActulaSegment1.Text) + dbROI).ToString()
                        End If
                        '@ End of Added By Abhishek on 06/10/10



                    End If
                Next
            End If
            'Code Added by Mukund

            '###########################################################################################
            '@ This is used to Get the Total Qualification AfterExememption addd on 13/01/2011
            '   txtTotalQualificationAfterExem.Text = CalculateTotalQualification() + PrevFinalyExemptedValue()



            txtTotalQualificationAfterExem.Text = CalculateTotalQualification() + FinalyCalculateQualification()
            Dim intDevideByBillingCycle As Integer = 1
            If txtBillCycle.Text.Trim.ToUpper = "Annual" Then
                intDevideByBillingCycle = 12
            ElseIf txtBillCycle.Text.Trim.ToUpper = "BI-ANNUAL" Then
                intDevideByBillingCycle = 6
            ElseIf txtBillCycle.Text.Trim.ToUpper = "QTRLY" Then
                intDevideByBillingCycle = 3
            ElseIf txtBillCycle.Text.Trim.ToUpper = "Qtrly(Sign Up)".Trim.ToUpper Then ' Qtrly(Sign Up)
                intDevideByBillingCycle = 3
            ElseIf txtBillCycle.Text.Trim.ToUpper = "MONTHLY" Then
                intDevideByBillingCycle = 1
            End If

            If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
                'intDevideByBillingCycle = 12
            End If

            If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                intDevideByBillingCycle = 12
            End If



            Dim DblQualificationAvg As Double = Round(Val(txtTotalQualificationAfterExem.Text) / intDevideByBillingCycle, 0)
            txtQualAvg.Text = Math.Round(DblQualificationAvg, 0, MidpointRounding.AwayFromZero)
            txtQualAvgData.Text = Math.Round(DblQualificationAvg, 0, MidpointRounding.AwayFromZero)

            If GvSlabQualification.Rows.Count > 0 Then
                If GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(0).Text.Trim.ToUpper = "Qualification Average -".Trim.ToUpper Then
                    GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(1).Text = txtQualAvgData.Text
                End If
            End If



            '@ This is used to Get the Total Qualification AfterExememption addd on 13/01/2011
            '###########################################################################################



            '@ Start code for Calculation
            Dim DblGrandFinal As Double = 0
            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
            Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
            txtGrandFinalAmount.Text = 0
            txtTotalCalCulatedSegment.Text = 0
            Dim DblTotalCalCulatedSegment As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim txtActulaSegment As TextBox = CType(row.FindControl("txtActulaSegment"), TextBox)
                Dim txtCalCulatedSegment As TextBox = CType(row.FindControl("txtCalCulatedSegment"), TextBox)
                Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                Dim txtFinalAmount As TextBox = CType(row.FindControl("txtFinalAmount"), TextBox)
                Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                Dim hdProductivityId As Label = CType(row.FindControl("lblProductivity"), Label)

                Dim txtStdRate As TextBox = CType(row.FindControl("txtStdRate"), TextBox)
                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                Dim dblAmountCalculate As Double = 0
                If txtActulaSegment.Text.Trim.Length > 0 Then
                    If txtExumption.Text.Trim.Length > 0 Then
                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                    Else
                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                    End If
                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                Else
                    txtCalCulatedSegment.Text = "0.00"
                End If
                'End of Code for calculating Amount on the basis of (segment * Exemption)
                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                    DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                End If
                ' ####################################################################

                '@ Code For Pickup the data of rate on the basis of calculatedsegment




                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                If strCaseName.ToUpper() <> "HL" And strCaseName.ToUpper() <> "ROI" Then
                    'PickupRateontheBasisofSegment(row, index)

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        PickupRateontheBasisoFinalExemptedQualAvgFORPLB(DblQualificationAvg, row, index)
                    Else
                        PickupRateontheBasisoFinalExemptedQualAvg(DblQualificationAvg, row, index)
                    End If

                End If



                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    

                '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                Dim strRate As Double = 0
                If txtRate.Text.Trim.Length = 0 Then
                    strRate = Val(txtStdRate.Text)
                Else
                    strRate = Val(txtRate.Text)
                End If
                '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10


                Dim dblFinalCalCulatedSegment As Double = 0
                If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                    'If hdFixedOrRate.Value = "2" Then
                    '    If txtRate.Text.Trim.Length > 0 Then
                    '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                    '    Else
                    '        dblFinalCalCulatedSegment = Double.Parse(0) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                    '    End If
                    'Else
                    '    If txtRate.Text.Trim.Length > 0 Then
                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                    '    Else
                    '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                    '    End If
                    'End If
                    If hdFixedOrRate.Value = "2" Then
                        dblFinalCalCulatedSegment = Double.Parse(strRate)
                    Else
                        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                    End If

                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                End If
                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                '####################################################################
                If hdProductivityId.Text.Trim().ToUpper() <> "HL" And hdProductivityId.Text.Trim().ToUpper() <> "ROI" Then
                    DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                End If
                txtFinalAmount.CssClass = "textboxgrey right"
                txtCalCulatedSegment.CssClass = "textboxgrey right"
            Next
            txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
            'strGrandFinal = DblGrandFinal.ToString("f2")
            txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")


            If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
                If HdPlbTypeId.Value = "2" Then
                    '   PickupPLBAmountBasisofSegment()
                End If

            End If

            calculatebalance()
            '@ Starts code for Settting Focus on Next Row on txtExumption Controls.
            If GvProcessPayment.Rows.Count > index + 1 Then
                Dim rownext As GridViewRow = GvProcessPayment.Rows(index + 1)
                Dim kkinh As Double = Val(txtExp.Text) * 100
                If rownext IsNot Nothing Then
                    Dim txtNextExp As TextBox = CType(rownext.FindControl("txtExumption"), TextBox)
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtNextExp.ClientID)
                End If
            Else
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(btnSave.ClientID)
            End If
            '@ End code for Settting Focus on Next Row on txtExumption Controls.

        Catch ex As Exception
        Finally
            If hdFinallyApproved.Value.Trim.ToUpper = "TRUE" Then
                btnSave.Enabled = False
                BtnSendForApproval.Enabled = False
            End If
            HideUnhideSendForApproval()
        End Try
    End Sub

    Protected Sub txtPLBAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            '@ Start Checking Decimal Value
            Dim reg As Regex = New Regex("^[0-9.]+$")
            If reg.IsMatch(txtPLBAmt.Text) Then
                Dim countDecimalNo As Integer = 0
                For i As Integer = 0 To txtPLBAmt.Text.Length - 1
                    If txtPLBAmt.Text.Chars(i) = "." Then
                        countDecimalNo = countDecimalNo + 1
                    End If
                Next
                If countDecimalNo > 1 Then
                    lblError.Text = "Only numeric with decimal is allowed."
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtPLBAmt.ClientID)
                    Exit Sub
                End If
            Else
                lblError.Text = "Only numeric with decimal is allowed."
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(txtPLBAmt.ClientID)
                Exit Sub
            End If
            '@ End of  Checking Decimal Value   
            calculatebalance()
        Catch ex As Exception
        Finally
            HideUnhideSendForApproval()
        End Try
    End Sub
    Private Function CalculateGridData(ByVal strROWid As String, ByVal intRate As Double, ByVal strType As String) As Double
        Dim strSegment As Double = 0
        Dim dbTotal As Double = 0
        Try
            Dim objXmlOuputdoc As New XmlDocument
            If ViewState("PaymentData") IsNot Nothing Then
                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
            Else
                Return strSegment
            End If
            ' Dim objFilteredNode As XmlNode = objXmlOuputdoc.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS[@ROWID='" + strROWid + "']")

            'If objFilteredNode IsNot Nothing Then
            '    dbTotal = Val(objFilteredNode.Attributes("STANDARSEGMENT").Value)
            'End If
            If strType.ToUpper() = "HL" Then
                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@ROWID='" + strROWid + "' and @HL='TRUE']")
                    If Val(xNode.Attributes("STANDARSEGMENT").Value) <> 0 Then
                        'dbTotal = dbTotal + Val(xNode.Attributes("STANDARSEGMENT").Value) + System.Math.Ceiling(((Val(xNode.Attributes("SEGMENT").Value) * intRate)) / 100)
                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("STANDARSEGMENT").Value) * intRate)) / 100)
                    End If
                Next
            ElseIf strType.ToUpper() = "ROI" Then
                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@ROWID='" + strROWid + "' and @ROI='TRUE']")
                    If Val(xNode.Attributes("STANDARSEGMENT").Value) <> 0 Then
                        'dbTotal = dbTotal + Val(xNode.Attributes("STANDARSEGMENT").Value) + System.Math.Ceiling(((Val(xNode.Attributes("SEGMENT").Value) * intRate)) / 100)
                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("STANDARSEGMENT").Value) * intRate)) / 100)
                    End If
                Next
            End If
            Return System.Math.Round(dbTotal, 0, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            System.Math.Round(dbTotal, 0, MidpointRounding.AwayFromZero)
            lblError.Text = ex.Message
        End Try
    End Function
    Private Sub HideUnhideSendForApproval()

        If HdCurPayNo.Value = "1" Then
            If HdPaymentId.Value.Trim.Length > 0 And HdPACreated.Value.ToUpper <> "TRUE" Then
                If Request.QueryString("OnlyShow") Is Nothing Then
                    BtnSendForApproval.Enabled = True
                End If
            End If
        Else
            If Val(txtBalanceUpfrontAmount.Text) < 0 Then
                BtnSendForApproval.Enabled = False
            Else
                If HdPaymentId.Value.Trim.Length > 0 And HdPACreated.Value.ToUpper <> "TRUE" Then
                    If Request.QueryString("OnlyShow") Is Nothing Then
                        BtnSendForApproval.Enabled = True
                    End If
                End If
            End If
        End If

        '@ End of Code Addedd  on 03/09/10      

        '@ Start of New Added Code on 31 Mar 2011
        If HdSkipPayment.Value.Trim.ToUpper = "TRUE" Then
            btnSave.Enabled = False
            BtnSendForApproval.Enabled = False
        End If
        If HdPaymentId.Value.Trim.Length > 0 And HdPACreated.Value.ToUpper = "TRUE" Then
            BtnSkipPayment.Enabled = False
        End If

        '@End of  New Added Code on 31 Mar 2011
    End Sub

    Private Sub BreaupData(ByVal objoutputxmlBreakupdata As XmlDocument)
        Dim dset As DataSet
        Dim objReadaer As XmlNodeReader
        Dim TotalQualification As Double = 0
        Try
            If objoutputxmlBreakupdata.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dset = New DataSet
                objReadaer = New XmlNodeReader(objoutputxmlBreakupdata)
                dset.ReadXml(objReadaer)
                If dset.Tables("PRODTYPE") IsNot Nothing Then
                    Dim Dview As DataView = dset.Tables("PRODTYPE").DefaultView
                    Dview.Sort = "ISCHECKED DESC"
                    GvBreakup.DataSource = Dview 'dset.Tables("PRODTYPE").DefaultView.Sort("ISCHECKED")
                    GvBreakup.DataBind()
                Else
                    GvBreakup.DataSource = Nothing
                    GvBreakup.DataBind()
                End If
                If dset.Tables("GROUPDETAILS") IsNot Nothing Then
                    '<GROUPDETAILS ACCOUNTMANAGER='' CHAIN_CODE='' GROUP='' NAME='' AOFFICE='' CITY=''/>

                    Dim strQulaField As String = dset.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString
                    strQulaField = strQulaField.Replace("_CODD_PK_HX", " ")
                    strQulaField = strQulaField.Replace("_DOM", " DOM")
                    txtQualAvgSelected.Text = strQulaField
                    txtQualAvgData.Text = dset.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                End If
            Else
                GvBreakup.DataSource = Nothing
                GvBreakup.DataBind()
            End If

            '@ Srart of code for Getting Total Qualification aDeed on 13th January 2011
            Try
                txtTotalQualification.Text = CalculateTotalQualification()
                TotaQualifictionAfterHLORROIExemFunc(Val(txtTotalQualification.Text))
            Catch ex As Exception
            End Try
            '@ End of code for Getting Total Qualification aDeed on 13th January 2011
          
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GvBreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBreakup.RowDataBound
        Try
            If hdFixedOrRate.Value = "2" Then
                LblRateorFixed.Text = "Revised Fixed"
                LblStdRateorFixed.Text = "Standard Fixed"
            Else
                LblRateorFixed.Text = "Revised Rate"
                LblStdRateorFixed.Text = "Standard Rate"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hdNotInBCase As HiddenField = CType(e.Row.FindControl("hdNotInBCase"), HiddenField)
                Dim LblChecked As Label = CType(e.Row.FindControl("LblChecked"), Label)
                If hdNotInBCase.Value = "1" Then
                    LblChecked.Text = "Yes"
                Else
                    LblChecked.Text = "No"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnOpenBreakup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOpenBreakup.Click
        ModalPopupBreakup.Show()
    End Sub
    Private Sub calculatebalance()
        Dim DblBusinessAmout As Double = 0 'DblBusinessAmout =DblGrandFinal
        If GvProcessPayment.Rows.Count > 0 Then
            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
            DblBusinessAmout = Val(txtGrandFinalAmount.Text)
        Else
            DblBusinessAmout = 0
        End If
        '@Start of code added  on 06/09/10
        If txtBalanceUpfrontAmount.Visible = True Then
            If Val(txtPLBAmt.Text) > 0 Then
                txtBalanceUpfrontAmount.Text = ((DblBusinessAmout - Math.Abs(Val((Val(txtCBForwardAmount.Text)).ToString("f2"))) + Val(0))).ToString("f2")
            Else
                txtBalanceUpfrontAmount.Text = (DblBusinessAmout - Math.Abs(Val(txtCBForwardAmount.Text))).ToString("f2")
            End If
        End If


        If HdCurPayNo.Value = "1" Then
            If HdPaymentId.Value.Trim.Length > 0 And HdPACreated.Value.ToUpper <> "TRUE" Then
                BtnSendForApproval.Enabled = True
            Else
                BtnSendForApproval.Enabled = False
            End If
        Else
            If Val(txtBalanceUpfrontAmount.Text) >= 0 Then
                If HdPaymentId.Value.Trim.Length > 0 And HdPACreated.Value.ToUpper <> "TRUE" Then
                    BtnSendForApproval.Enabled = True
                Else
                    BtnSendForApproval.Enabled = False
                End If
            Else
                BtnSendForApproval.Enabled = False
            End If

        End If


        If hdFinallyApproved.Value.Trim.ToUpper = "TRUE" Then
            btnSave.Enabled = False
            BtnSendForApproval.Enabled = False
        End If
        If Request.QueryString("OnlyShow") IsNot Nothing Then
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim txtExumption As TextBox = CType(row.FindControl("txtExumption"), TextBox)
                Dim txtRemByChangeInRate As TextBox = CType(row.FindControl("txtRemByChangeInRate"), TextBox)
                Dim txtRate As TextBox = CType(row.FindControl("txtRate"), TextBox)
                txtRate.ReadOnly = True
                txtRate.CssClass = "textboxgrey right"
                txtExumption.ReadOnly = True
                txtExumption.CssClass = "textboxgrey right"
                txtRemByChangeInRate.ReadOnly = True
                txtRemByChangeInRate.CssClass = "textboxgrey"
            Next

            txtRemarks.ReadOnly = True
            txtRemarks.CssClass = "textboxgrey"
            txtPayAppRemarks.ReadOnly = True
            txtPayAppRemarks.CssClass = "textboxgrey"
            btnSave.Enabled = False
            BtnPaymentHistory.Enabled = False
            BtnSendForApproval.Enabled = False
        End If
        '@End of of code added  on 06/09/10
    End Sub



#Region "PickupRateontheBasisofSegment"
    Private Sub PickupRateontheBasisofSegment(ByVal Gvrow As GridViewRow, ByVal Rowno As Integer)
        Dim objOutPutxml As New XmlDocument
        Dim strRevisedStandardRate As String
        strRevisedStandardRate = "0"
        Dim intDevideByBillingCycle As Integer = 1
        Try
            Dim index As Integer = Gvrow.RowIndex
            If index <= Rowno Then

                If ViewState("PaymentData") IsNot Nothing Then
                    objOutPutxml.LoadXml(ViewState("PaymentData").ToString)

                    '  Dim strBillingCycle As String = objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("BILLINGCYCLE").Value

                    If txtBillCycle.Text.Trim.ToUpper = "Annual" Then
                        intDevideByBillingCycle = 12
                    ElseIf txtBillCycle.Text.Trim.ToUpper = "BI-ANNUAL" Then
                        intDevideByBillingCycle = 6
                    ElseIf txtBillCycle.Text.Trim.ToUpper = "QTRLY" Then
                        intDevideByBillingCycle = 3
                    ElseIf txtBillCycle.Text.Trim.ToUpper = "Qtrly(Sign Up)".Trim.ToUpper Then ' Qtrly(Sign Up)
                        intDevideByBillingCycle = 3
                    ElseIf txtBillCycle.Text.Trim.ToUpper = "MONTHLY" Then
                        intDevideByBillingCycle = 1
                    End If

                    If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
                        ' intDevideByBillingCycle = 12
                    End If

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        intDevideByBillingCycle = 12
                    End If


                    ' objOutPutxml.Load("c:\admin\BCPaymentProcessViewOut.xml")
                    Dim hdProductivityId As HiddenField = CType(Gvrow.FindControl("hdProductivityId"), HiddenField)
                    Dim txtRate As TextBox = CType(Gvrow.FindControl("txtRate"), TextBox)
                    Dim txtStdRate As TextBox = CType(Gvrow.FindControl("txtStdRate"), TextBox)

                    Dim txtCalCulatedSegment As TextBox = CType(Gvrow.FindControl("txtCalCulatedSegment"), TextBox)
                    Dim objFinalXmldocument As New XmlDocument
                    objFinalXmldocument.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT></UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
                    If objOutPutxml.DocumentElement.SelectNodes("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").Count > 0 Then
                        objFinalXmldocument.DocumentElement.AppendChild(objFinalXmldocument.ImportNode(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").ParentNode, True))
                        For Each objxmlnode As XmlNode In objFinalXmldocument.DocumentElement.SelectNodes("CASE/PLAN_DETAILS")
                            Dim strCalCulatedSegmentByBiilingPeriod As Double = Math.Round(Val(txtCalCulatedSegment.Text) / intDevideByBillingCycle, 2)
                            If Val(strCalCulatedSegmentByBiilingPeriod) >= Val(objxmlnode.Attributes("SLABS_START").Value) And Val(strCalCulatedSegmentByBiilingPeriod) <= Val(objxmlnode.Attributes("SLABS_END").Value) Then
                                strRevisedStandardRate = objxmlnode.Attributes("SLABS_RATE").Value
                                Exit For
                            End If
                        Next
                    End If

                    '  txtRate.Text = strRevisedRate
                    'If Val(strRevisedStandardRate) > 0 Then
                    txtStdRate.Text = strRevisedStandardRate
                    'End If

                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "PickupPLBAmountBasisofSegment"
    Private Sub PickupPLBAmountBasisofSegment()
        Dim objOutPutxml As New XmlDocument
        Dim strRevisedPLBRate As String
        strRevisedPLBRate = "0"
        Dim intDevideByBillingCycle As Integer = 1
        Try
            If GvProcessPayment.Rows.Count > 0 Then

                Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)

                If ViewState("PaymentData") IsNot Nothing Then
                    objOutPutxml.LoadXml(ViewState("PaymentData").ToString)
                    'If txtBillCycle.Text.Trim.ToUpper = "Annual" Then
                    '    intDevideByBillingCycle = 12
                    'ElseIf txtBillCycle.Text.Trim.ToUpper = "BI-ANNUAL" Then
                    '    intDevideByBillingCycle = 6
                    'ElseIf txtBillCycle.Text.Trim.ToUpper = "QTRLY" Then
                    '    intDevideByBillingCycle = 3
                    'ElseIf txtBillCycle.Text.Trim.ToUpper = "MONTHLY" Then
                    '    intDevideByBillingCycle = 1
                    'End If

                    intDevideByBillingCycle = 12

                    Dim objFinalXmldocument As New XmlDocument
                    objFinalXmldocument.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT></UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
                    If objOutPutxml.DocumentElement.SelectNodes("PLB").Count > 0 Then
                        objFinalXmldocument.DocumentElement.AppendChild(objFinalXmldocument.ImportNode(objOutPutxml.DocumentElement.SelectSingleNode("PLB"), True))
                        For Each objxmlnode As XmlNode In objFinalXmldocument.DocumentElement.SelectNodes("PLB/PLB_DETAILS")
                            Dim strTotalCalCulatedSegmentByBillingPeriod As String = Val(txtTotalCalCulatedSegment.Text) / intDevideByBillingCycle
                            If Val(strTotalCalCulatedSegmentByBillingPeriod) >= Val(objxmlnode.Attributes("SLABS_START").Value) And Val(strTotalCalCulatedSegmentByBillingPeriod) <= Val(objxmlnode.Attributes("SLABS_END").Value) Then
                                strRevisedPLBRate = objxmlnode.Attributes("SLABS_RATE").Value
                                Exit For
                            End If
                        Next
                    End If
                    '  txtPLBAmt.Text = Val(Val(strRevisedPLBRate) * (Val(txtTotalCalCulatedSegment.Text) / intDevideByBillingCycle)).ToString("f2")
                    txtPLBAmt.Text = Val(Val(strRevisedPLBRate) * Val(txtTotalCalCulatedSegment.Text)).ToString("f2")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Private Sub EnableDiablePaymentSheet(ByVal objBCID, ByVal objChainCode, ByVal objMonth, ByVal objYear, ByVal objPayTime, ByVal objCurPayNo, ByVal PayId)
        'objBCID,objChainCode,objMonth,objYear,objPayTime,objCurPayNo
        Dim strPLBPAy As String = "False"
        If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
            strPLBPAy = "True"
        End If
        Dim strPeriod As String = ""
        Dim strPeriodFrom As String = ""
        Dim strPeriodTo As String = ""
        Dim strPeriodFinalFrom As String = ""
        If Request.QueryString("Period") IsNot Nothing Then
            strPeriod = Request.QueryString("Period").ToString
            If strPeriod.Split("-").Length = 2 Then
                strPeriodFrom = strPeriod.Split("-")(0).Trim
                strPeriodTo = strPeriod.Split("-")(1).Trim
                If strPeriodFrom.Trim.Split("/").Length = 3 Then
                    strPeriodFinalFrom = strPeriodFrom.Split("/")(0).ToString().PadLeft("2", "0")
                    strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(1).ToString().PadLeft("2", "0")
                    strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(2).ToString().PadLeft("4", "0")
                End If
            End If
        End If



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
                    '"&PLB=" + objPLB + "&Period=" + objPeriod
                    '   BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport(" & "'" & objBCID & "'" + ",'" & objChainCode & "'" + ",'" & objMonth & "'" + ",'" & objYear & "','" & objPayTime & "','" & objCurPayNo & "','" & PayId.ToString & "'" & ");")
                    BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport(" & "'" & objBCID & "'" + ",'" & objChainCode & "'" + ",'" & objMonth & "'" + ",'" & objYear & "','" & objPayTime & "','" & objCurPayNo & "','" & PayId.ToString & "','" & strPeriodFinalFrom.ToString.Trim & "','" & strPeriodTo.ToString.Trim & "','" & HdPLBCycle.Value.Trim & "'" & ");")
                End If
            Else
                BtnPaymentSheetReport.Enabled = False
            End If
        Else
            BtnPaymentSheetReport.Enabled = True
            '   BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport(" & "'" & objBCID & "'" + ",'" & objChainCode & "'" + ",'" & objMonth & "'" + ",'" & objYear & "','" & objPayTime & "','" & objCurPayNo & "','" & PayId.ToString & "'" & ");")
            BtnPaymentSheetReport.Attributes.Add("onclick", "return PaymentSheetReport(" & "'" & objBCID & "'" + ",'" & objChainCode & "'" + ",'" & objMonth & "'" + ",'" & objYear & "','" & objPayTime & "','" & objCurPayNo & "','" & PayId.ToString & "','" & strPeriodFinalFrom.ToString.Trim & "','" & strPeriodTo.ToString.Trim & "','" & HdPLBCycle.Value.Trim & "'" & ");")

        End If

        If HdSkipPayment.Value.Trim.ToUpper = "TRUE" Then
            BtnPaymentSheetReport.Enabled = False
        End If


    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If HdPaymentId.Value.Trim.Length > 0 Then
                Dim objInputXml As New XmlDocument
                Dim objOutPutxml As New XmlDocument
                ' Dim ds As DataSet
                ' Dim objReadaer As XmlNodeReader
                Dim objbzPaymentApprovalLavel As New AAMS.bizIncetive.bzPaymentApprovalQue
                '@ Code  for Details
                Exit Sub
                Try
                    objInputXml.LoadXml("<UP_INC_PAYMENT_GENERATE_PADVICE_INPUT>  <PAYMENT_ID/><SORT_BY></SORT_BY><DESC>0</DESC><PAGE_NO>0</PAGE_NO><PAGE_SIZE>0</PAGE_SIZE></UP_INC_PAYMENT_GENERATE_PADVICE_INPUT >")
                    objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = HdPaymentId.Value ' strBcaseId
                    objOutPutxml = objbzPaymentApprovalLavel.Generate_Payment_Advice(objInputXml)
                    If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        Session("Msg") = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Response.Redirect(Request.Url.ToString, False)
                    Else
                        lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Catch ex As Exception
                    lblError.Text = ex.Message
                End Try
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    '@ Srart of code for Getting Total Qualification based on 13th January 2011
    Private Function CalculateTotalQualification() As Double
        Dim TotalQualification As Double = 0
        Dim dset As DataSet
        Dim objReadaer As XmlNodeReader
        Try
            Dim objXmlOuputdoc As New XmlDocument
            If ViewState("PaymentData") IsNot Nothing Then
                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
                dset = New DataSet
                objReadaer = New XmlNodeReader(objXmlOuputdoc)
                dset.ReadXml(objReadaer)

                If dset.Tables("PRODTYPE") IsNot Nothing Then
                    For RowNum As Integer = 0 To dset.Tables("PRODTYPE").Rows.Count - 1
                        If dset.Tables("GROUPDETAILS") IsNot Nothing Then
                            Dim strQulaFieldArrayList As New ArrayList(dset.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString.Split(","))
                            If strQulaFieldArrayList.Contains(dset.Tables("PRODTYPE").Rows(RowNum)("NIDTFIELDS").ToString.Trim) Then
                                TotalQualification = TotalQualification + Val(dset.Tables("PRODTYPE").Rows(RowNum)("STANDARSEGMENT").ToString)
                            End If
                        End If
                    Next
                End If
            End If
            Return TotalQualification
        Catch ex As Exception
            Return 0
        End Try

    End Function
    Private Function CalculateBenifitForQualification(ByVal intExumption As Double, ByVal strType As String) As Double
        Dim strSegment As Double = 0
        Dim dbTotal As Double = 0
        Try
            Dim objXmlOuputdoc As New XmlDocument
            If ViewState("PaymentData") IsNot Nothing Then
                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
            Else
                Return strSegment
            End If

            If strType.ToUpper() = "HL" Then
                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiHL='TRUE' ]")
                    If Val(xNode.Attributes("STANDARSEGMENT").Value) <> 0 Then
                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("STANDARSEGMENT").Value) * intExumption)) / 100)
                    End If
                Next
            ElseIf strType.ToUpper() = "ROI" Then
                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiROI='TRUE']")

                    If Val(xNode.Attributes("STANDARSEGMENT").Value) <> 0 Then
                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("STANDARSEGMENT").Value) * intExumption)) / 100)
                    End If

                Next
            End If
            Return System.Math.Round(dbTotal, 0, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            System.Math.Round(dbTotal, 0, MidpointRounding.AwayFromZero)
            lblError.Text = ex.Message
        End Try
    End Function

    Private Function FinalyCalculateQualification() As Double
        Dim dblRowWiseQualification As Double = 0
        Dim HLANDROIBenifitForQualificationForAdding As Double = 0
        Dim dblGrandQualification As Double = 0
        '@  Start of Added by Abhishek on 06/10/10
        Try

            Dim strHLExemption As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                If strCaseName.ToUpper() = "HL" Then
                    strHLExemption = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
                End If
            Next
            Dim strROIExemption As Double = 0
            For Each row As GridViewRow In GvProcessPayment.Rows
                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
                If strCaseName.ToUpper() = "ROI" Then
                    strROIExemption = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
                End If
            Next

            HLANDROIBenifitForQualificationForAdding = HLANDROIBenifitForQualificationForAdding + CalculateBenifitForQualification(strHLExemption, "HL")
            HLANDROIBenifitForQualificationForAdding = HLANDROIBenifitForQualificationForAdding + CalculateBenifitForQualification(strROIExemption, "ROI")

            dblGrandQualification = dblGrandQualification + HLANDROIBenifitForQualificationForAdding


            Return Round(dblGrandQualification, 0)
        Catch ex As Exception
            Return 0
        End Try
        '@ End of Added by Abhishek on 06/10/10
    End Function
  
#Region "PickupRateontheBasisoFinalExemptedQualAvg"


    Private Sub PickupRateontheBasisoFinalExemptedQualAvg(ByVal DblQualificationAvg As Double, ByVal Gvrow As GridViewRow, ByVal Rowno As Integer)
        Dim objOutPutxml As New XmlDocument
        Dim strRevisedStandardRate As String
        strRevisedStandardRate = "0"
        Dim intDevideByBillingCycle As Integer = 1
        Try
            If ViewState("PaymentData") IsNot Nothing Then
                objOutPutxml.LoadXml(ViewState("PaymentData").ToString)
                Dim txtStdRate As TextBox = CType(Gvrow.FindControl("txtStdRate"), TextBox)
                Dim hdProductivityId As HiddenField = CType(Gvrow.FindControl("hdProductivityId"), HiddenField)
                ' Start of code to Pick the rate on the basis of Plan
                Dim objFinalXmldocument As New XmlDocument
                objFinalXmldocument.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT></UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
                If objOutPutxml.DocumentElement.SelectNodes("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").Count > 0 Then
                    objFinalXmldocument.DocumentElement.AppendChild(objFinalXmldocument.ImportNode(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").ParentNode, True))
                    For Each objxmlnode As XmlNode In objFinalXmldocument.DocumentElement.SelectNodes("CASE/PLAN_DETAILS")
                        If Val(DblQualificationAvg) >= Val(objxmlnode.Attributes("SLABS_START").Value) And Val(DblQualificationAvg) <= Val(objxmlnode.Attributes("SLABS_END").Value) Then
                            strRevisedStandardRate = objxmlnode.Attributes("SLABS_RATE").Value
                            Exit For
                        End If
                    Next
                End If
                'End of code to Pick the rate on the basis of Plan

                '@ Now condition for selecting the rate on the basis of MIDT_PER='' CONV_PER=''  MINSEGMENT='' MIN_SEGMENT_TOTAL=''

                If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIDT_PER").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("CONV_PER").Value) Then
                    If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIN_SEGMENT_TOTAL").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MINSEGMENT").Value) Then
                        txtStdRate.Text = strRevisedStandardRate
                    Else
                        txtStdRate.Text = 0
                    End If
                Else
                    txtStdRate.Text = 0
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#End Region
#Region "PickupRateontheBasisoFinalExemptedQualAvgFORPLB"
    Private Sub PickupRateontheBasisoFinalExemptedQualAvgFORPLB(ByVal DblQualificationAvg As Double, ByVal Gvrow As GridViewRow, ByVal Rowno As Integer)
        Dim objOutPutxml As New XmlDocument
        Dim strRevisedStandardRate As String
        strRevisedStandardRate = "0"
        Dim intDevideByBillingCycle As Integer = 1
        Try

            If ViewState("PaymentData") IsNot Nothing Then
                Dim txtStdRate As TextBox = CType(Gvrow.FindControl("txtStdRate"), TextBox)


                Dim hdProductivityId As HiddenField = CType(Gvrow.FindControl("hdProductivityId"), HiddenField)
                If HdPlbTypeId.Value = "2" Then   ' @ In Case Of PLB Rate Based

                    objOutPutxml.LoadXml(ViewState("PaymentData").ToString)

                    ' Start of code to Pick the rate on the basis of Plan
                    Dim objFinalXmldocument As New XmlDocument
                    objFinalXmldocument.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT></UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
                    If objOutPutxml.DocumentElement.SelectNodes("PLB").Count > 0 Then
                        objFinalXmldocument.DocumentElement.AppendChild(objFinalXmldocument.ImportNode(objOutPutxml.DocumentElement.SelectSingleNode("PLB"), True))
                        For Each objxmlnode As XmlNode In objFinalXmldocument.DocumentElement.SelectNodes("PLB/PLB_DETAILS")

                            If Val(DblQualificationAvg) >= Val(objxmlnode.Attributes("SLABS_START").Value) And Val(DblQualificationAvg) <= Val(objxmlnode.Attributes("SLABS_END").Value) Then
                                strRevisedStandardRate = objxmlnode.Attributes("SLABS_RATE").Value
                                Exit For
                            End If
                        Next
                    End If
                    'End of code to Pick the rate on the basis of Plan

                    '@ Now condition for selecting the rate on the basis of MIDT_PER='' CONV_PER=''  MINSEGMENT='' MIN_SEGMENT_TOTAL=''

                    If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIDT_PER").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("CONV_PER").Value) Then
                        If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIN_SEGMENT_TOTAL").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MINSEGMENT").Value) Then
                            txtStdRate.Text = strRevisedStandardRate
                        Else
                            txtStdRate.Text = 0
                        End If
                    Else
                        txtStdRate.Text = 0
                    End If
                Else  ' @ In Case Of Fixed PLB
                    'strRevisedStandardRate = 0
                    'strRevisedStandardRate = Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("PLBAMOUNT").Value)

                    'If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIDT_PER").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("CONV_PER").Value) Then
                    '    If Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MIN_SEGMENT_TOTAL").Value) >= Val(objOutPutxml.DocumentElement.SelectSingleNode("GROUPDETAILS").Attributes("MINSEGMENT").Value) Then
                    '        txtStdRate.Text = strRevisedStandardRate
                    '    Else
                    '        txtStdRate.Text = 0
                    '    End If
                    'Else
                    '    txtStdRate.Text = 0
                    'End If

                    txtStdRate.Text = ""
                   

                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
  

    'Private Sub PrevPickupRateontheBasisoFinalExemptedQualAvg()
    '    Dim objOutPutxml As New XmlDocument
    '    Dim strRevisedStandardRate As String
    '    strRevisedStandardRate = "0"
    '    Dim intDevideByBillingCycle As Integer = 1
    '    Try
    '        For Each Gvrow As GridViewRow In GvProcessPayment.Rows
    '            Dim index As Integer = Gvrow.RowIndex
    '            Dim strCaseName As String = CType(Gvrow.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
    '            If strCaseName.ToUpper.Trim <> "HL" And strCaseName.ToUpper.Trim <> "ROI" Then

    '                If ViewState("PaymentData") IsNot Nothing Then
    '                    objOutPutxml.LoadXml(ViewState("PaymentData").ToString)
    '                    If txtBillCycle.Text.Trim.ToUpper = "Annual" Then
    '                        intDevideByBillingCycle = 12
    '                    ElseIf txtBillCycle.Text.Trim.ToUpper = "BI-ANNUAL" Then
    '                        intDevideByBillingCycle = 6
    '                    ElseIf txtBillCycle.Text.Trim.ToUpper = "QTRLY" Then
    '                        intDevideByBillingCycle = 3
    '                    ElseIf txtBillCycle.Text.Trim.ToUpper = "MONTHLY" Then
    '                        intDevideByBillingCycle = 1
    '                    End If

    '                    If HdYearAndSettleMent.Value.Trim().ToUpper = "TRUE" Then
    '                        intDevideByBillingCycle = 12
    '                    End If

    '                    Dim hdProductivityId As HiddenField = CType(Gvrow.FindControl("hdProductivityId"), HiddenField)
    '                    Dim txtRate As TextBox = CType(Gvrow.FindControl("txtRate"), TextBox)
    '                    Dim txtStdRate As TextBox = CType(Gvrow.FindControl("txtStdRate"), TextBox)

    '                    Dim TotalQualificationAfterExem As String = txtTotalQualificationAfterExem.Text

    '                    Dim DblQualificationAvg As Double = Round(Val(TotalQualificationAfterExem) / intDevideByBillingCycle, 0)


    '                    ' Start of code to Pick the rate on the basis of Plan
    '                    Dim objFinalXmldocument As New XmlDocument
    '                    objFinalXmldocument.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT></UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
    '                    If objOutPutxml.DocumentElement.SelectNodes("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").Count > 0 Then
    '                        objFinalXmldocument.DocumentElement.AppendChild(objFinalXmldocument.ImportNode(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE/NIDT_FIELDS_ID[@NIDT_FIELDS_IDNAME='" + hdProductivityId.Value + "']").ParentNode, True))
    '                        For Each objxmlnode As XmlNode In objFinalXmldocument.DocumentElement.SelectNodes("CASE/PLAN_DETAILS")
    '                            If Val(DblQualificationAvg) >= Val(objxmlnode.Attributes("SLABS_START").Value) And Val(DblQualificationAvg) <= Val(objxmlnode.Attributes("SLABS_END").Value) Then
    '                                strRevisedStandardRate = objxmlnode.Attributes("SLABS_RATE").Value
    '                                Exit For
    '                            End If
    '                        Next
    '                    End If
    '                    'End of code to Pick the rate on the basis of Plan



    '                    txtStdRate.Text = strRevisedStandardRate
    '                End If
    '            End If
    '        Next
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    '    '@
    '    Private Function RowWiseCalculateQualification(ByVal strROWid As String) As Double
    '        Dim strSegment As Double = 0
    '        Dim dbTotal As Double = 0
    '        Try
    '            Dim objXmlOuputdoc As New XmlDocument
    '            If ViewState("PaymentData") IsNot Nothing Then
    '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
    '            Else
    '                Return strSegment
    '            End If

    '            For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiRowId='" + strROWid + "']")
    '                dbTotal = dbTotal + Val(xNode.Attributes("SEGMENT").Value)
    '            Next
    '            Return System.Math.Round(dbTotal, 0)
    '        Catch ex As Exception
    '            System.Math.Round(dbTotal, 0)
    '            lblError.Text = ex.Message
    '        End Try

    '    End Function

    '    Private Function PrevCalculateBenifitForQualification(ByVal strROWid As String, ByVal intExumption As Double, ByVal strType As String) As Double
    '        Dim strSegment As Double = 0
    '        Dim dbTotal As Double = 0
    '        Try
    '            Dim objXmlOuputdoc As New XmlDocument
    '            If ViewState("PaymentData") IsNot Nothing Then
    '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
    '            Else
    '                Return strSegment
    '            End If

    '            If strType.ToUpper() = "HL" Then
    '                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiHL='TRUE']")


    '                    If Val(xNode.Attributes("SEGMENT").Value) <> 0 Then
    '                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("SEGMENT").Value) * intExumption)) / 100)
    '                    End If

    '                Next
    '            ElseIf strType.ToUpper() = "ROI" Then
    '                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiROI='TRUE']")
    '                    If Val(xNode.Attributes("SEGMENT").Value) <> 0 Then
    '                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("SEGMENT").Value) * intExumption)) / 100)
    '                    End If
    '                Next
    '            Else
    '                For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@QualiRowId='" + strROWid + "']")
    '                    If Val(xNode.Attributes("SEGMENT").Value) <> 0 Then
    '                        dbTotal = dbTotal + Val(((Val(xNode.Attributes("SEGMENT").Value) * intExumption)) / 100)
    '                    End If
    '                Next
    '            End If
    '            Return System.Math.Round(dbTotal, 0)
    '        Catch ex As Exception
    '            System.Math.Round(dbTotal, 0)
    '            lblError.Text = ex.Message
    '        End Try
    '    End Function    '@
    '    Private Function PrevFinalyExemptedValue() As Double
    '        Dim HLANDROIBenifitForQualificationForAdding As Double = 0
    '        Dim QualificationForSubstract As Double = 0
    '        Dim FinalExemptedValue As Double = 0
    '        '@  Start of Added by Abhishek on 06/10/10
    '        Try
    '            Dim strHLExemption As Double = 0

    '            For Each row As GridViewRow In GvProcessPayment.Rows
    '                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
    '                If strCaseName.ToUpper() = "HL" Then
    '                    strHLExemption = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
    '                End If
    '            Next
    '            Dim strROIExemption As Double = 0
    '            For Each row As GridViewRow In GvProcessPayment.Rows
    '                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
    '                If strCaseName.ToUpper() = "ROI" Then
    '                    strROIExemption = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
    '                End If
    '            Next

    '            For Each row As GridViewRow In GvProcessPayment.Rows
    '                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
    '                Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
    '                If strCaseName.ToUpper.Trim = "HL" Then
    '                    HLANDROIBenifitForQualificationForAdding = HLANDROIBenifitForQualificationForAdding + PrevCalculateBenifitForQualification(hdRowID.Value, strHLExemption, "HL")
    '                ElseIf strCaseName.ToUpper.Trim = "ROI" Then
    '                    HLANDROIBenifitForQualificationForAdding = HLANDROIBenifitForQualificationForAdding + PrevCalculateBenifitForQualification(hdRowID.Value, strROIExemption, "ROI")
    '                End If
    '            Next

    '            For Each row As GridViewRow In GvProcessPayment.Rows
    '                Dim strCaseName As String = CType(row.FindControl("lblProductivity"), System.Web.UI.WebControls.Label).Text.Trim().ToUpper()
    '                If strCaseName.ToUpper.Trim <> "HL" And strCaseName.ToUpper.Trim <> "ROI" Then
    '                    Dim hdRowID As HiddenField = CType(row.FindControl("hdRowID"), HiddenField)
    '                    Dim strExemption As Double = Val(CType(row.FindControl("txtExumption"), TextBox).Text.Trim())
    '                    QualificationForSubstract = QualificationForSubstract + PrevCalculateBenifitForQualification(hdRowID.Value, strExemption, strCaseName)
    '                End If
    '            Next
    '            FinalExemptedValue = FinalExemptedValue + HLANDROIBenifitForQualificationForAdding - Math.Abs(QualificationForSubstract)
    '            Return FinalExemptedValue
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '        '@ End of Added by Abhishek on 06/10/10
    '    End Function





    '    Private Function QualificationAirlineSegmentWhoDoesNotexistInAnyPlan() As Double
    '        Dim objXmlOuputdoc As New XmlDocument
    '        Dim objReadaer As XmlNodeReader
    '        Dim strQulaFieldArrayList As New ArrayList
    '        Dim strPlanNIDTFeildList As New ArrayList
    '        Dim strQualNIDTFldLstWhoDoesNotexistAnyPlan As New ArrayList
    '        Dim dbTotal As Double = 0
    '        Try
    '            If ViewState("PaymentData") IsNot Nothing Then
    '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
    '                dset = New DataSet
    '                objReadaer = New XmlNodeReader(objXmlOuputdoc)
    '                dset.ReadXml(objReadaer)

    '                If dset.Tables("PRODTYPE") IsNot Nothing Then
    '                    If dset.Tables("GROUPDETAILS") IsNot Nothing Then
    '                        strQulaFieldArrayList = New ArrayList(dset.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString.Split(","))
    '                    End If


    '                    For Each objxmlnode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("BC_PLAN/CASE/NIDT_FIELDS_ID")
    '                        Dim RowWisePlanNIDTFeildList As New ArrayList(objxmlnode.Attributes("NIDT_FIELDS_IDNAME").Value.ToString.Split(","))
    '                        For I As Integer = 0 To RowWisePlanNIDTFeildList.Count - 1
    '                            If RowWisePlanNIDTFeildList(I).ToString.Length > 0 Then
    '                                strPlanNIDTFeildList.Insert(strPlanNIDTFeildList.Count, RowWisePlanNIDTFeildList(I).ToString)
    '                            End If
    '                        Next
    '                    Next

    '                    For j As Integer = 0 To strQulaFieldArrayList.Count - 1
    '                        If strQulaFieldArrayList(j).ToString.Length > 0 Then
    '                            If Not strPlanNIDTFeildList.Contains(strQulaFieldArrayList(j).ToString.Trim) Then
    '                                strQualNIDTFldLstWhoDoesNotexistAnyPlan.Insert(strQualNIDTFldLstWhoDoesNotexistAnyPlan.Count, strQulaFieldArrayList(j).ToString)
    '                            End If
    '                        End If
    '                    Next

    '                    For k As Integer = 0 To strQualNIDTFldLstWhoDoesNotexistAnyPlan.Count - 1
    '                        For Each xNode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("PRODTYPE[@NIDTFIELDS='" + strQulaFieldArrayList(k).ToString.Trim + "']")
    '                            If Val(xNode.Attributes("SEGMENT").Value) <> 0 Then
    '                                dbTotal = dbTotal + Val(xNode.Attributes("SEGMENT").Value)
    '                            End If
    '                        Next
    '                    Next


    '                End If
    '            End If
    '            Return Round(dbTotal, 0)
    '        Catch ex As Exception
    '            Return 0
    '        End Try

    '    End Function

    '    Private Function FindThelistofNIDTFieldInPlan() As ArrayList
    '        Dim NIDTFieldInPlanArrayList As New ArrayList
    '        Dim objXmlOuputdoc As New XmlDocument
    '        Try
    '            If ViewState("PaymentData") IsNot Nothing Then
    '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
    '                For Each objxmlnode As XmlNode In objXmlOuputdoc.DocumentElement.SelectNodes("BC_PLAN/CASE/NIDT_FIELDS_ID")
    '                    Dim RowWisePlanNIDTFeildList As New ArrayList(objxmlnode.Attributes("NIDT_FIELDS_IDNAME").Value.ToString.Split(","))
    '                    For I As Integer = 0 To RowWisePlanNIDTFeildList.Count - 1
    '                        If RowWisePlanNIDTFeildList(I).ToString.Length > 0 Then
    '                            NIDTFieldInPlanArrayList.Insert(NIDTFieldInPlanArrayList.Count, RowWisePlanNIDTFeildList(I).ToString)
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Catch ex As Exception
    '            Return NIDTFieldInPlanArrayList
    '        End Try
    '        Return NIDTFieldInPlanArrayList
    '    End Function


    '    Private Function FindThelistofNIDTFieldInQualification() As ArrayList
    '        Dim strQulaFieldArrayList As New ArrayList
    '        Dim objXmlOuputdoc As New XmlDocument
    '        Dim objReadaer As XmlNodeReader
    '        Try
    '            If ViewState("PaymentData") IsNot Nothing Then
    '                objXmlOuputdoc.LoadXml(ViewState("PaymentData"))
    '                dset = New DataSet
    '                objReadaer = New XmlNodeReader(objXmlOuputdoc)
    '                dset.ReadXml(objReadaer)
    '                If dset.Tables("PRODTYPE") IsNot Nothing Then
    '                    If dset.Tables("GROUPDETAILS") IsNot Nothing Then
    '                        strQulaFieldArrayList = New ArrayList(dset.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString.Split(","))
    '                    End If
    '                    For i As Integer = 0 To strQulaFieldArrayList.Count - 1
    '                        If strQulaFieldArrayList(i).ToString.Length <= 0 Then
    '                            strQulaFieldArrayList.Remove(strQulaFieldArrayList(i).ToString)
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Catch ex As Exception
    '            Return strQulaFieldArrayList
    '        End Try
    '        Return strQulaFieldArrayList
    '    End Function
    '    Private Function FindNotexistInAnyPlanButExistInQualification() As ArrayList
    '        Dim strQulaFieldArrayList As New ArrayList
    '        Dim strPlanNIDTFeildList As New ArrayList
    '        Dim strQualNIDTFldLstWhoDoesNotexistAnyPlan As New ArrayList
    '        Try
    '            strQulaFieldArrayList = FindThelistofNIDTFieldInQualification()
    '            strPlanNIDTFeildList = FindThelistofNIDTFieldInPlan()
    '            For j As Integer = 0 To strQulaFieldArrayList.Count - 1
    '                If strQulaFieldArrayList(j).ToString.Length > 0 Then
    '                    If Not strPlanNIDTFeildList.Contains(strQulaFieldArrayList(j).ToString.Trim) Then
    '                        strQualNIDTFldLstWhoDoesNotexistAnyPlan.Insert(strQualNIDTFldLstWhoDoesNotexistAnyPlan.Count, strQulaFieldArrayList(j).ToString)
    '                    End If
    '                End If
    '            Next
    '        Catch ex As Exception
    '            Return strQualNIDTFldLstWhoDoesNotexistAnyPlan
    '        End Try
    '        Return strQualNIDTFldLstWhoDoesNotexistAnyPlan
    '    End Function

    Private Sub LoadMIDDataAndQualificatiinSlab()
        Dim objInput As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Dim obj As New AAMS.bizIncetive.bzIncentive
        Try

            If Request.QueryString("BCaseID") IsNot Nothing Then
                If Request.QueryString("Month") IsNot Nothing Then
                    If Request.QueryString("Year") IsNot Nothing Then
                        Dim strInput As String = "<UP_MIDT_SLABQUALIFICATION_INPUT><BC_ID>86</BC_ID><PAYMENT_ID>256</PAYMENT_ID><MONTH>12</MONTH><YEAR>2009</YEAR><CHAIN_CODE>334</CHAIN_CODE><PLB></PLB><PLBPAYMENTPERIOD_FROM></PLBPAYMENTPERIOD_FROM><PLBPAYMENTPERIOD_TO></PLBPAYMENTPERIOD_TO></UP_MIDT_SLABQUALIFICATION_INPUT>"
                        objInput.LoadXml(strInput)
                        objInput.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BCaseID").ToString
                        objInput.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = HdPaymentId.Value
                        objInput.DocumentElement.SelectSingleNode("MONTH").InnerText = Request.QueryString("Month").ToString
                        objInput.DocumentElement.SelectSingleNode("YEAR").InnerText = Request.QueryString("Year").ToString
                        objInput.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = objED.Decrypt(hdEnChainCode.Value)

                        If Request.QueryString("PLB") IsNot Nothing Then
                            If Request.QueryString("PLB").Trim = "1" Then
                                objInput.DocumentElement.SelectSingleNode("PLB").InnerText = "True"
                            Else
                                objInput.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
                            End If
                        Else
                            objInput.DocumentElement.SelectSingleNode("PLB").InnerText = "False"
                        End If


                        If Request.QueryString("Period") IsNot Nothing Then
                            Dim strPeriod As String = Request.QueryString("Period").ToString
                            If strPeriod.Split("-").Length = 2 Then
                                Dim strPeriodFrom As String = strPeriod.Split("-")(0).Trim
                                Dim strPeriodTo As String = strPeriod.Split("-")(1).Trim
                                Dim strPeriodFinalFrom As String = ""
                                If strPeriodFrom.Trim.Split("/").Length = 3 Then
                                    strPeriodFinalFrom = strPeriodFrom.Split("/")(0).ToString().PadLeft("2", "0")
                                    strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(1).ToString().PadLeft("2", "0")
                                    strPeriodFinalFrom = strPeriodFinalFrom + "/" + strPeriodFrom.Split("/")(2).ToString().PadLeft("4", "0")
                                End If

                                objInput.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_FROM").InnerText = strPeriodFinalFrom
                                objInput.DocumentElement.SelectSingleNode("PLBPAYMENTPERIOD_TO").InnerText = strPeriodTo
                            End If
                        End If
                        objOutPutxml = obj.getMidt_SlabsQualification(objInput)
                        If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            ds = New DataSet
                            objReadaer = New XmlNodeReader(objOutPutxml)
                            ds.ReadXml(objReadaer)
                            dset = New DataSet
                            dset = ds
                            If ds.Tables("MIDTDETAILS") IsNot Nothing Then
                                grdvMIDT.DataSource = ds.Tables("MIDTDETAILS")
                                grdvMIDT.DataBind()
                            Else
                                grdvMIDT.DataSource = Nothing
                                grdvMIDT.DataBind()
                            End If
                            If ds.Tables("SLABSDETAILS") IsNot Nothing Then
                                GvSlabQualification.DataSource = ds.Tables("SLABSDETAILS")
                                GvSlabQualification.DataBind()
                            Else
                                GvSlabQualification.DataSource = Nothing
                                GvSlabQualification.DataBind()
                            End If

                        Else
                            grdvMIDT.DataSource = Nothing
                            grdvMIDT.DataBind()
                            GvSlabQualification.DataSource = Nothing
                            GvSlabQualification.DataBind()
                            lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        End If
                    End If
                End If
                If GvSlabQualification.Rows.Count > 0 Then
                    If GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(0).Text.Trim.ToUpper = "Qualification Average -".Trim.ToUpper Then
                        If GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(1).Text.Trim.Length <= 0 Or GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(1).Text.Trim = "&nbsp;" Then
                            GvSlabQualification.Rows(GvSlabQualification.Rows.Count - 1).Cells(1).Text = txtQualAvgData.Text
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub TotaQualifictionAfterHLORROIExemFunc(ByVal DblCalculateTotalQualification As Double)
        Try
            txtTotalQualificationAfterExem.Text = DblCalculateTotalQualification + FinalyCalculateQualification()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    '@ End if  code for Getting Total Qualification Added on 13th January 2011

    Protected Sub GvSlabQualification_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSlabQualification.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                    If HdPlbTypeId.Value <> "2" Then
                        e.Row.Cells(0).Text = "Fixed"
                        e.Row.Cells(1).Text = "Amount"
                    End If
                Else
                    If hdFixedOrRate.Value = "2" Then
                        e.Row.Cells(0).Text = "Fixed"
                        e.Row.Cells(1).Text = "Amount"
                    End If
                End If
            End If
         
        Catch ex As Exception
            lblError.Text = ex.Message
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

    Protected Sub BtnSkipPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSkipPayment.Click
        StrSaveData = ""
        If (IsValid) Then
            Try
                If Session("Security") Is Nothing Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                    lblError.Text = "Session is expired."
                    Exit Sub
                End If

                '##############################################################################################
                '@Start of  New Added Code if Revised rate is different than standard rate then RateRem is mandatory 20/10/10
                Dim RowId As Integer
                For RowId = 0 To GvProcessPayment.Rows.Count - 1
                    Dim txtStdRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtStdRate"), TextBox)
                    Dim txtRemByChangeInRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtRemByChangeInRate"), TextBox)
                    Dim txtRate As TextBox = CType(GvProcessPayment.Rows(RowId).FindControl("txtRate"), TextBox)
                    If txtRate.Text.Trim.Length > 0 Then
                        If Val(txtRate.Text.Trim) <> Val(txtStdRate.Text) Then
                            If txtRemByChangeInRate.Text.Trim.Length = 0 Then
                                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                                lblError.Text = "Remark is mandatory."
                                scriptManager.SetFocus(txtRemByChangeInRate.ClientID)
                                Exit Sub
                            End If
                        End If
                    End If


                    If txtRemByChangeInRate.Text.Trim.Length > 8000 Then
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        lblError.Text = "Remark can't  be greater than 8000"
                        scriptManager.SetFocus(txtRemByChangeInRate.ClientID)
                        Exit Sub
                    End If

                Next

                If txtPLBAmt.Visible = True Then
                    '@ Start Checking Decimal Value
                    Dim reg As Regex = New Regex("^[0-9.]+$")
                    If reg.IsMatch(txtPLBAmt.Text) Then
                        Dim countDecimalNo As Integer = 0
                        For i As Integer = 0 To txtPLBAmt.Text.Length - 1
                            If txtPLBAmt.Text.Chars(i) = "." Then
                                countDecimalNo = countDecimalNo + 1
                            End If
                        Next
                        If countDecimalNo > 1 Then
                            lblError.Text = "Only numeric with decimal is allowed."
                            Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                            scriptManager.SetFocus(txtPLBAmt.ClientID)
                            Exit Sub
                        End If
                    Else
                        lblError.Text = "Only numeric with decimal is allowed."
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        scriptManager.SetFocus(txtPLBAmt.ClientID)
                        Exit Sub
                    End If
                    '@ End of  Checking Decimal Value 
                End If


               
                '@ End of New Added Code if Revised rate is different than standard rate then RateRem is mandatory
                '##############################################################################################


                If (Not Request.QueryString("BCaseID") = Nothing) Then
                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim ds As New DataSet
                    Dim Rowno As Integer
                    Dim objChildNode, objChildNodeClone As XmlNode
                    Dim strAoffice As String = ""
                    Dim objbzProcessPayment As New AAMS.bizIncetive.bzIncentive
                    Dim objbzProcessPaymentForPLB As New AAMS.bizIncetive.bzPLB
                    'objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT><BCDETAIL BC_ID='' PAYMENT_ID ='' MONTH='' YEAR='' EMPLOYEE_ID='' TOTAL_AMT='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT=''/><PRODTYPE PAYMENT_ID='' PRODUCTIVITYTYPEID ='' PRODUCTIVITYTYPE='' SEGMENT='' RATE='' EXCEMPTION='' AMOUNT='' FINALAMOUNT='' /></UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT>")
                    objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT><BCDETAIL CHAIN_CODE =''  BC_ID='' PAYMENT_ID ='' MONTH='' YEAR='' EMPLOYEE_ID='' TOTAL_AMT='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT='' PREVIOUSUPFRONTAMOUNT='' BALANCEUPFRONTAMOUNT='' NEXTUPFRONTAMOUNT='' PAYMENTTYPE = '' UPFRONTTYPE='' PAYMENTTYPEID=''  UPFRONTFIRSTTIME =''  PLBTYPEID='' PLBSLAB='' ISPLB='' PLBAMOUNT='' PREVPAYMENT_ID='' YEARENDSETTLEMENT=''  REMARKS ='' NO_OF_PAYMENT =''  SIGNUPAMOUNT='' ADJUSTABLE=''  BALANCE_CF=''   UPF_NO_OF_PAYMENT='' UPF_NO_OF_PAYMENT_DONE=''  UPF_ONETIME_NO_OF_PAYMENT='' UPF_ONETIME_NO_OF_PAYMENT_DONE='' QUALI_REMARKS='' QUALIAVG='' QUALITOTAL='' QUALITOTALAFTEREXEM='' PLBCYCLE='' PLBPAYMENTPERIOD_FROM='' PLBPAYMENTPERIOD_TO=''  PAREMARKS='' PAYMENTPERIOD='' PAYMENTPERIODFORMAT=''   SKIPPAYMENT='TRUE'   /><CALCULATION><PRODTYPEDETAILS PAYMENT_ID= '' PRODUCTIVITYTYPEID ='' PRODUCTIVITYTYPE='' NIDTFIELDS='' SEGMENT='' RATE='' EXCEMPTION='' AMOUNT='' FINALAMOUNT='' ISCHECKED='' ROWID='' HL='' ROI='' STANDARDRATE='' STANDARSEGMENT ='' RATEREM='' /></CALCULATION></UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT>")
                    'Reading and Appending records into the Input XMLDocument                  
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BC_ID").Value = Request.QueryString("BCaseID") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("MONTH").Value = Request.QueryString("Month") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("YEAR").Value = Request.QueryString("Year") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENT_ID").Value = HdPaymentId.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PREVPAYMENT_ID").Value = HdpREVPaymentId.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("YEARENDSETTLEMENT").Value = HdYearAndSettleMent.Value
                    '@ New Added Attributes
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONT_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PREVIOUSUPFRONTAMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BALANCEUPFRONTAMOUNT").Value = txtBalanceUpfrontAmount.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("NEXTUPFRONTAMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTTYPE").Value = hdPaymentType.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONTTYPE").Value = hdUpFronttType.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTTYPEID").Value = hdFixedOrRate.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONTFIRSTTIME").Value = hdFirstTime.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBTYPEID").Value = ""
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBSLAB").Value = ""
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("ISPLB").Value = hdIsPLB.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBAMOUNT").Value = txtPLBAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("REMARKS").Value = txtRemarks.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALI_REMARKS").Value = txtRemarks.Text

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAREMARKS").Value = txtPayAppRemarks.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTPERIOD").Value = TxtPayPeriod.Text


                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENTPERIODFORMAT").Value = HdPmtFormat.Value




                    '@ New Added Attributes
                    '@ Start of  Added on 06/09/10
                    'NO_OF_PAYMENT ='' SIGNUPAMOUNT='' ADJUSTABLE=''  BALANCE_CF=''   UPF_NO_OF_PAYMENT='' UPF_NO_OF_PAYMENT_DONE=''  UPF_ONETIME_NO_OF_PAYMENT='' UPF_ONETIME_NO_OF_PAYMENT_DONE=''
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("NO_OF_PAYMENT").Value = HdCurPayNo.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("ADJUSTABLE").Value = HdAdjustable.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("SIGNUPAMOUNT").Value = txtSignUpAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BALANCE_CF").Value = Val(txtCBForwardAmount.Text)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_NO_OF_PAYMENT").Value = HdUpNoPay.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_NO_OF_PAYMENT_DONE").Value = HdUpNoPayDone.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_ONETIME_NO_OF_PAYMENT").Value = HdOneTimeUpNoOfPay.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPF_ONETIME_NO_OF_PAYMENT_DONE").Value = HdOneTimeUpNoPayDone.Value

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("CHAIN_CODE").Value = hdChainCode.Value

                    '   QUALIAVG='' QUALITOTAL='' QUALITOTALAFTEREXEM=''
                    '     objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALIAVG").Value = HdQualAgv.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALIAVG").Value = txtQualAvgData.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALITOTAL").Value = txtTotalQualification.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("QUALITOTALAFTEREXEM").Value = txtTotalQualificationAfterExem.Text

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBCYCLE").Value = "TRUE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBCYCLE").Value = "FALSE"
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBPAYMENTPERIOD_FROM").Value = hdPLBFROM.Value
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBPAYMENTPERIOD_TO").Value = hdPLBTO.Value



                    '@ End of  Added on 06/09/10
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("CALCULATION/PRODTYPEDETAILS") 'objInputXml.DocumentElement.SelectSingleNode("PRODTYPE")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objInputXml.DocumentElement.SelectSingleNode("CALCULATION").RemoveChild(objChildNode)
                    ' objInputXml.DocumentElement.RemoveChild(objChildNode)
                    Dim sumSegmentFinal As Double = 0
                    Dim sumFinal As Double = 0
                    Dim DblTotalCalCulatedSegment As Double = 0
                    Dim DblGrandFinal As Double = 0
                    For Rowno = 0 To GvProcessPayment.Rows.Count - 1
                        Dim lblProductivity As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("lblProductivity"), Label)
                        Dim hdPayMentId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdPayMentId"), HiddenField)
                        Dim txtActulaSegment As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtActulaSegment"), TextBox)
                        Dim txtCalCulatedSegment As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtCalCulatedSegment"), TextBox)
                        Dim txtExumption As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtExumption"), TextBox)
                        Dim txtFinalAmount As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtFinalAmount"), TextBox)
                        Dim txtRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtRate"), TextBox)
                        Dim hdProductivityId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdProductivityId"), HiddenField)
                        Dim hdNotInBCase As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdNotInBCase"), HiddenField)
                        Dim hdROI As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdRowID"), HiddenField)
                        Dim hdNidtFields As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdNidtFields"), HiddenField)

                        Dim txtStdRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtStdRate"), TextBox)
                        '  objChildNodeClone.InnerText = hdProductivityId.Value 'dbgrdCategoryProductDesc.Rows(Rowno).Cells(0).Text 'strAoffice
                        objChildNodeClone.Attributes("PRODUCTIVITYTYPEID").Value = hdProductivityId.Value
                        objChildNodeClone.Attributes("PRODUCTIVITYTYPE").Value = lblProductivity.Text ' ChkProductivity.Text
                        objChildNodeClone.Attributes("SEGMENT").Value = txtActulaSegment.Text
                        objChildNodeClone.Attributes("RATE").Value = txtRate.Text
                        objChildNodeClone.Attributes("EXCEMPTION").Value = txtExumption.Text
                        objChildNodeClone.Attributes("AMOUNT").Value = "0"
                        objChildNodeClone.Attributes("ISCHECKED").Value = hdNotInBCase.Value
                        objChildNodeClone.Attributes("NIDTFIELDS").Value = hdNidtFields.Value.Trim()
                        objChildNodeClone.Attributes("ROWID").Value = hdROI.Value.Trim()
                        'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                        Dim dblAmountCalculate As Double = 0
                        If txtActulaSegment.Text.Trim.Length > 0 Then
                            If txtExumption.Text.Trim.Length > 0 Then
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100)
                            Else
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - Math.Abs((Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100)
                            End If
                            txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                        Else
                            txtCalCulatedSegment.Text = "0.00"
                        End If
                        'End of Code for calculating Amount on the basis of (segment * Exemption)
                        If lblProductivity.Text.Trim().ToUpper() <> "HL" And lblProductivity.Text.Trim().ToUpper() <> "ROI" Then
                            DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate
                        End If

                        ' ####################################################################
                        ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    

                        '@ start of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10
                        Dim strRate As Double = 0
                        If txtRate.Text.Trim.Length = 0 Then
                            strRate = Val(txtStdRate.Text)
                        Else
                            strRate = Val(txtRate.Text)
                        End If
                        '@ End of of code Now Get the rate if Revised rate is empty then pick the value from standard else rate should be Revised rate 01/11/10

                        Dim dblFinalCalCulatedSegment As Double = 0
                        If txtCalCulatedSegment.Text.Trim.Length > 0 Then
                            'If hdFixedOrRate.Value = "2" Then
                            '    ' If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                            '    If txtRate.Text.Trim.Length > 0 Then
                            '        dblFinalCalCulatedSegment = Double.Parse(txtRate.Text.Trim) ' Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                            '    Else
                            '        dblFinalCalCulatedSegment = Double.Parse(0) 'Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                            '    End If
                            'Else
                            '    If txtRate.Text.Trim.Length > 0 Then
                            '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                            '    Else
                            '        dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                            '    End If
                            'End If
                            If hdFixedOrRate.Value = "2" Then
                                dblFinalCalCulatedSegment = Double.Parse(strRate)
                            Else
                                dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(strRate)
                            End If

                            txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                        End If
                        ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                        '####################################################################
                        If lblProductivity.Text.Trim().ToUpper() <> "HL" And lblProductivity.Text.Trim().ToUpper() <> "ROI" Then
                            DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment
                        End If
                        txtFinalAmount.CssClass = "textboxgrey right"
                        txtCalCulatedSegment.CssClass = "textboxgrey right"
                        objChildNodeClone.Attributes("FINALAMOUNT").Value = dblFinalCalCulatedSegment.ToString("f0") 'Val(txtAmount.Text) - (Val(txtAmount.Text) * Val(txtExumption.Text)) / 100
                        objChildNodeClone.Attributes("PAYMENT_ID").Value = hdPayMentId.Value

                        '@Start of New Code Added On 18/10/10

                        Dim txtRemByChangeInRate As TextBox = CType(GvProcessPayment.Rows(Rowno).FindControl("txtRemByChangeInRate"), TextBox)
                        objChildNodeClone.Attributes("STANDARDRATE").Value = txtStdRate.Text
                        Dim hdStandardSeg As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdStandardSeg"), HiddenField)
                        objChildNodeClone.Attributes("STANDARSEGMENT").Value = hdStandardSeg.Value
                        objChildNodeClone.Attributes("RATEREM").Value = txtRemByChangeInRate.Text
                        '@ End of New  Code Added On 18/10/10

                        ' sumFinal = sumFinal + Val(txtFinalAmount.Text)
                        'objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objInputXml.DocumentElement.SelectSingleNode("CALCULATION").AppendChild(objChildNodeClone)

                        objChildNodeClone = objChildNode.CloneNode(True)
                    Next Rowno
                    If GvProcessPayment.Rows.Count > 0 Then
                        Dim txtTotalCalCulatedSegment As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), TextBox)
                        Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
                        txtTotalCalCulatedSegment.Text = Round(Val(DblTotalCalCulatedSegment), 0).ToString("f2")
                        txtGrandFinalAmount.Text = Round(Val(DblGrandFinal), 0).ToString("f2")
                    End If
                    Dim sum As Double = 0
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("TOTAL_AMT").Value = (DblGrandFinal + sum).ToString("f0")
                    ' DblTotalCalCulatedSegment.ToString("f2")
                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("EMPLOYEE_ID").Value = Session("LoginSession").ToString().Split("|")(0)
                    End If
                    ' SOLE_AMOUNT='FALSE' BONUS_AMOUNT='FALSE' FIXED_PAYMENT='TRUE' UPFRONT_AMOUNT='FALSE' FIXED_UPFRONT='FALSE'
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("SOLE_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_PAYMENT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONT_AMOUNT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_UPFRONT").Value = "0.00"
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BONUS_AMOUNT").Value = "0.00"


                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        If HdPlbTypeId.Value <> "2" Then '@ In case of Fixed PLB
                            objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("TOTAL_AMT").Value = objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PLBAMOUNT").Value
                        End If
                    End If

                 
                    ' Here Back end Method Call
                    'Code Added by Mukund for Importing Rows of View
                    Dim xmlView As New XmlDocument
                    If ViewState("PaymentData") IsNot Nothing Then
                        xmlView.LoadXml(ViewState("PaymentData"))
                    Else
                        lblError.Text = "Invalid Action"
                        Exit Sub
                    End If
                    For Each xN As XmlNode In xmlView.DocumentElement.SelectNodes("PRODTYPE")
                        objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xN, True))
                    Next
                    For Each xN As XmlNode In xmlView.DocumentElement.SelectNodes("PLBTYPE")
                        objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xN, True))
                    Next
                    Try
                        objInputXml.Save("C:\Admin\UpdatePaymentProcessDetailsInput.xml")
                    Catch ex As Exception
                    End Try
                  

                    If HdPLBCycle.Value.Trim.ToUpper = "TRUE" Then
                        objOutputXml = objbzProcessPaymentForPLB.UpdatePLBPaymentProcessDetails(objInputXml)
                    Else
                        objOutputXml = objbzProcessPayment.UpdatePaymentProcessDetails(objInputXml)
                    End If

                    Try
                        objOutputXml.Save("C:\Admin\UpdatePaymentProcessDetailsOutput.xml")
                    Catch ex As Exception
                    End Try
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = "Payment Updated Successfully."
                        If Request.QueryString("Chain_Code") IsNot Nothing Then
                            hdEnChainCode.Value = Request.QueryString("Chain_Code")
                            hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                            If Request.QueryString("BCaseID") IsNot Nothing Then
                                LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                            End If
                        End If
                        StrSaveData = "Saved"
                    Else
                        StrSaveData = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                StrSaveData = ex.Message
                lblError.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub BtnPastpaySave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPastpaySave.Click
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzIncentive
        Dim objpastpaynode As XmlNode
        Dim objpastpayclonenode As XmlNode
        '@ Code  for Details
        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                Exit Sub
            End If

            '##############################################################################################
            '@Start of  New Added Code if Revised rate is different than standard rate then RateRem is mandatory 20/10/10
            Dim RowId As Integer
            For RowId = 0 To GvPasPaymentData.Rows.Count - 1
                Dim TxtPaymentAmt As TextBox = CType(GvPasPaymentData.Rows(RowId).FindControl("TxtPaymentAmt"), TextBox)
                Dim TxtProdAvg As TextBox = CType(GvPasPaymentData.Rows(RowId).FindControl("TxtProdAvg"), TextBox)
                Dim TxtRem As TextBox = CType(GvPasPaymentData.Rows(RowId).FindControl("TxtRem"), TextBox)
                If TxtPaymentAmt.Text.Trim.Length = 0 Then
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    lblPaspaymentError.Text = "Payment amount is mandaory."
                    scriptManager.SetFocus(TxtPaymentAmt.ClientID)
                    ModalPopupExtenderPreviousDataSave.Show()

                    Exit Sub
                End If

                '@ Start of  Checking Decimal Value 
                Dim reg As Regex = New Regex("^[0-9.]+$")
                If reg.IsMatch(TxtPaymentAmt.Text) Then
                    Dim countDecimalNo As Integer = 0
                    For i As Integer = 0 To TxtPaymentAmt.Text.Length - 1
                        If TxtPaymentAmt.Text.Chars(i) = "." Then
                            countDecimalNo = countDecimalNo + 1
                        End If
                    Next
                    If countDecimalNo > 1 Then
                        lblPaspaymentError.Text = "Only numeric with decimal is allowed."
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        scriptManager.SetFocus(TxtPaymentAmt.ClientID)
                        ModalPopupExtenderPreviousDataSave.Show()

                        Exit Sub
                    End If
                Else
                    lblPaspaymentError.Text = "Only numeric with decimal is allowed."
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(TxtPaymentAmt.ClientID)
                    ModalPopupExtenderPreviousDataSave.Show()

                    Exit Sub
                End If
                '@ End of  Checking Decimal Value 

                If TxtProdAvg.Text.Trim.Length = 0 Then
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    lblPaspaymentError.Text = "Productivity Avg is mandaory."
                    scriptManager.SetFocus(TxtProdAvg.ClientID)
                    ModalPopupExtenderPreviousDataSave.Show()

                    Exit Sub
                End If

                '@ Start of  Checking Decimal Value 

                If reg.IsMatch(TxtProdAvg.Text) Then
                    Dim countDecimalNo As Integer = 0
                    For i As Integer = 0 To TxtProdAvg.Text.Length - 1
                        If TxtProdAvg.Text.Chars(i) = "." Then
                            countDecimalNo = countDecimalNo + 1
                        End If
                    Next
                    If countDecimalNo > 1 Then
                        lblPaspaymentError.Text = "Only numeric with decimal is allowed."
                        Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                        scriptManager.SetFocus(TxtProdAvg.ClientID)
                        ModalPopupExtenderPreviousDataSave.Show()

                        Exit Sub
                    End If
                Else
                    lblPaspaymentError.Text = "Only numeric with decimal is allowed."
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(TxtProdAvg.ClientID)
                    ModalPopupExtenderPreviousDataSave.Show()

                    Exit Sub
                End If
                '@ End of  Checking Decimal Value 

                If TxtRem.Text.Trim.Length = 0 Then
                    'Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    'lblPaspaymentError.Text = "Remark is mandaory."
                    'scriptManager.SetFocus(TxtRem.ClientID)
                    'ModalPopupExtenderPreviousDataSave.Show()

                    'Exit Sub
                End If


                If TxtRem.Text.Trim.Length > 8000 Then
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    lblPaspaymentError.Text = "Remark can't  be greater than 8000"
                    scriptManager.SetFocus(TxtRem.ClientID)
                    ModalPopupExtenderPreviousDataSave.Show()

                    Exit Sub
                End If

            Next



            objInputXml.LoadXml("<UP_UPDATE_INC_PASTPAYMENT_INPUT><PastPayment PaymentPeriod='' PaymentAmt='' ProdAvg='' Rem='' BC_ID='' MONTH='' YEAR='' CHAIN_CODE=''  NO_OF_PAYMENT='' PLB=''  PLBPAYMENTPERIOD_FROM='' PLBPAYMENTPERIOD_TO='' EMPLOYEE_ID=''  ></PastPayment></UP_UPDATE_INC_PASTPAYMENT_INPUT>")

            objpastpaynode = objInputXml.DocumentElement.SelectSingleNode("PastPayment")
            objpastpayclonenode = objpastpaynode.CloneNode(True)

            objInputXml.DocumentElement.RemoveChild(objpastpaynode)

            For Rowno As Integer = 0 To GvPasPaymentData.Rows.Count - 1
                Dim LblPaymentPeriod As Label = CType(GvPasPaymentData.Rows(Rowno).FindControl("LblPaymentPeriod"), Label)
                Dim TxtPaymentAmt As TextBox = CType(GvPasPaymentData.Rows(Rowno).FindControl("TxtPaymentAmt"), TextBox)
                Dim TxtProdAvg As TextBox = CType(GvPasPaymentData.Rows(Rowno).FindControl("TxtProdAvg"), TextBox)
                Dim TxtRem As TextBox = CType(GvPasPaymentData.Rows(Rowno).FindControl("TxtRem"), TextBox)
                Dim HdMisMonth, HdMisYear, HdNoOfPay As HiddenField
                HdMisMonth = CType(GvPasPaymentData.Rows(Rowno).FindControl("HdMisMonth"), HiddenField)
                HdMisYear = CType(GvPasPaymentData.Rows(Rowno).FindControl("HdMisYear"), HiddenField)
                HdNoOfPay = CType(GvPasPaymentData.Rows(Rowno).FindControl("HdNoOfPay"), HiddenField)

                'PaymentPeriod='' PaymentAmt='' ProdAvg='' Rem='' 
                'BC_ID='' MONTH='' YEAR='' CHAIN_CODE=''  NO_OF_PAYMENT='' 
                'PLB=''  PLBPAYMENTPERIOD_FROM='' PLBPAYMENTPERIOD_TO=''    
                objpastpayclonenode.Attributes("PaymentPeriod").Value = LblPaymentPeriod.Text
                objpastpayclonenode.Attributes("PaymentAmt").Value = TxtPaymentAmt.Text
                objpastpayclonenode.Attributes("ProdAvg").Value = TxtProdAvg.Text
                objpastpayclonenode.Attributes("Rem").Value = TxtRem.Text
                objpastpayclonenode.Attributes("BC_ID").Value = Request.QueryString("BCaseID").ToString
                objpastpayclonenode.Attributes("CHAIN_CODE").Value = hdChainCode.Value
                objpastpayclonenode.Attributes("MONTH").Value = HdMisMonth.Value
                objpastpayclonenode.Attributes("YEAR").Value = HdMisYear.Value

                objpastpayclonenode.Attributes("NO_OF_PAYMENT").Value = HdNoOfPay.Value
                objpastpayclonenode.Attributes("PLB").Value = ""
                objpastpayclonenode.Attributes("PLBPAYMENTPERIOD_FROM").Value = ""
                objpastpayclonenode.Attributes("PLBPAYMENTPERIOD_TO").Value = ""

                If Not Session("LoginSession") Is Nothing Then
                    objpastpayclonenode.Attributes("EMPLOYEE_ID").Value = Session("LoginSession").ToString().Split("|")(0)
                End If

                objInputXml.DocumentElement.AppendChild(objpastpayclonenode)
                objpastpayclonenode = objpastpaynode.CloneNode(True)
            Next Rowno

            objOutPutxml = objbzBCPaymentProcess.UpdatePastPayment(objInputXml)
            Try
                objInputXml.Save("c:\pastpaymentSaveInput.xml")
            Catch ex As Exception

            End Try

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                lblError.Text = "Payment Updated Successfully."
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    hdEnChainCode.Value = Request.QueryString("Chain_Code")
                    hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                    End If
                End If
                ModalPopupExtenderPreviousDataSave.Hide()
            Else
                ModalPopupExtenderPreviousDataSave.Show()
                lblPaspaymentError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblPaspaymentError.Text = ex.Message
            ModalPopupExtenderPreviousDataSave.Show()

        End Try
    End Sub
End Class
