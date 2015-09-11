'############################################
'     Developed By Abhishek on 19/10/2010
'############################################
Imports System.Web.UI
Imports System.Math
Partial Class Incentive_INC_PaymentProcHistory
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim dset As DataSet
    Dim strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        lblError.Text = ""
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        If Not Page.IsPostBack Then
            TrAvgQual.Visible = False
            TrPayProc.Visible = False
            LoadHistory()
        End If
    End Sub
    Private Sub LoadHistory()
        Dim StrBcaseId As String
        Dim strMonth As String
        Dim StrYear As String
        Dim StrChainCode As String
        StrBcaseId = String.Empty
        StrYear = String.Empty
        strMonth = String.Empty
        StrChainCode = String.Empty
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Dim DblGrandFinal As Double = 0
        Dim objbzBCPaymentProcessHistory As New AAMS.bizIncetive.bzApprovalQue
        Try
            ViewState("PaymentData") = Nothing
            If Request.QueryString("BCaseID") IsNot Nothing Then
                StrBcaseId = Request.QueryString("BCaseID").ToString
            End If
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                StrChainCode = objED.Decrypt(Request.QueryString("Chain_Code").ToString)
            End If
            If Request.QueryString("Year") IsNot Nothing Then
                StrYear = Request.QueryString("Year").ToString
            End If
            If Request.QueryString("Month") IsNot Nothing Then
                strMonth = Request.QueryString("Month").ToString
            End If
            If StrBcaseId.Trim.Length > 0 And StrChainCode.Trim.Length > 0 And StrYear.Trim.Length > 0 And strMonth.Trim.Length > 0 Then

                objInputXml.LoadXml("<UP_SER_INC_PAYMENTPROCESHISTORY_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><PAYMENT_ID></PAYMENT_ID><MONTH></MONTH><YEAR></YEAR><PAGE_NO>1</PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY>LOGDATE</SORT_BY><DESC>TRUE</DESC><PLB></PLB><PLBPAYMENTPERIOD_FROM></PLBPAYMENTPERIOD_FROM><PLBPAYMENTPERIOD_TO></PLBPAYMENTPERIOD_TO> </UP_SER_INC_PAYMENTPROCESHISTORY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = StrBcaseId
                objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = strMonth
                objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = StrYear
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = StrChainCode
                objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = ""



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



                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

                If ViewState("PrevSearching") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If

                objOutPutxml = objbzBCPaymentProcessHistory.Payment_process_HistorySearch(objInputXml)
                ' objOutPutxml.Load("c:\UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT.xml")
                If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ViewState("PaymentData") = objOutPutxml.OuterXml
                    ViewState("PrevSearching") = objInputXml.OuterXml

                    ds = New DataSet
                    objReadaer = New XmlNodeReader(objOutPutxml)
                    ds.ReadXml(objReadaer)
                    dset = New DataSet
                    dset = ds

                    If ds.Tables("AVGQUALHISTORY") IsNot Nothing Then
                        GvQualAvg.DataSource = ds.Tables("AVGQUALHISTORY")
                        GvQualAvg.DataBind()
                        PnlQualAvg.Visible = True
                        TrAvgQual.Visible = True
                    Else
                        GvQualAvg.DataSource = Nothing
                        GvQualAvg.DataBind()
                        PnlQualAvg.Visible = False
                        TrAvgQual.Visible = False
                    End If

                    If ds.Tables("DATE") IsNot Nothing Then
                        RptPaymentHistory.DataSource = ds.Tables("DATE")
                        RptPaymentHistory.DataBind()
                        '@ Code Added For Paging And Sorting 
                        txtTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                        pnlPaging.Visible = True
                        BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                        TrPayProc.Visible = True

                    Else
                        RptPaymentHistory.DataSource = Nothing
                        RptPaymentHistory.DataBind()
                        pnlPaging.Visible = False
                        TrPayProc.Visible = False
                    End If

                Else
                    RptPaymentHistory.DataSource = Nothing
                    RptPaymentHistory.DataBind()
                    pnlPaging.Visible = False
                    lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                End If
            Else
                lblError.Text = "Invalid URL."
                pnlPaging.Visible = False
                RptPaymentHistory.DataSource = Nothing
                RptPaymentHistory.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
            RptPaymentHistory.DataSource = Nothing
            RptPaymentHistory.DataBind()
        End Try

    End Sub

    Protected Sub RptPaymentHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RptPaymentHistory.ItemDataBound
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblDate As Label = CType(e.Item.FindControl("lblDate"), Label)
                Dim GvProcessPayment As GridView = CType(e.Item.FindControl("GvProcessPayment"), GridView)
                Dim hdFixedOrRate As HtmlInputHidden = CType(e.Item.FindControl("hdFixedOrRate"), HtmlInputHidden)

                Dim hdFirstTime As HtmlInputHidden = CType(e.Item.FindControl("hdFirstTime"), HtmlInputHidden)
                Dim hdPaymentType As HtmlInputHidden = CType(e.Item.FindControl("hdPaymentType"), HtmlInputHidden)
                Dim hdIsPLB As HtmlInputHidden = CType(e.Item.FindControl("hdIsPLB"), HtmlInputHidden)

                Dim lblUpFrontAmount As Label = CType(e.Item.FindControl("lblUpFrontAmount"), Label)
                Dim txtUpFrontAmount As TextBox = CType(e.Item.FindControl("txtUpFrontAmount"), TextBox)
                Dim trUpFrontAmount As HtmlTableRow = CType(e.Item.FindControl("trUpFrontAmount"), HtmlTableRow)

                Dim lblPrevUpfrontAmount As Label = CType(e.Item.FindControl("lblPrevUpfrontAmount"), Label)
                Dim txtPrevUpfrontAmount As TextBox = CType(e.Item.FindControl("txtPrevUpfrontAmount"), TextBox)
                Dim trPrevUpfrontAmount As HtmlTableRow = CType(e.Item.FindControl("trPrevUpfrontAmount"), HtmlTableRow)

                Dim lblBalanceUpfrontAmount As Label = CType(e.Item.FindControl("lblBalanceUpfrontAmount"), Label)
                Dim txtBalanceUpfrontAmount As TextBox = CType(e.Item.FindControl("txtBalanceUpfrontAmount"), TextBox)
                Dim trBalanceUpfrontAmount As HtmlTableRow = CType(e.Item.FindControl("trBalanceUpfrontAmount"), HtmlTableRow)

                Dim lblLatestUpfontAmount As Label = CType(e.Item.FindControl("lblLatestUpfontAmount"), Label)
                Dim txtLatestUpfontAmount As TextBox = CType(e.Item.FindControl("txtLatestUpfontAmount"), TextBox)
                Dim trLatestUpfontAmount As HtmlTableRow = CType(e.Item.FindControl("trLatestUpfontAmount"), HtmlTableRow)

                Dim LblCBFAmount As Label = CType(e.Item.FindControl("LblCBFAmount"), Label)
                Dim txtCBForwardAmount As TextBox = CType(e.Item.FindControl("txtCBForwardAmount"), TextBox)
                Dim trCBFAmount As HtmlTableRow = CType(e.Item.FindControl("trCBFAmount"), HtmlTableRow)

                Dim LblSignUpAmt As Label = CType(e.Item.FindControl("LblSignUpAmt"), Label)
                Dim txtSignUpAmt As TextBox = CType(e.Item.FindControl("txtSignUpAmt"), TextBox)
                Dim trSignUpAmt As HtmlTableRow = CType(e.Item.FindControl("trSignUpAmt"), HtmlTableRow)

                Dim LblPlb As Label = CType(e.Item.FindControl("LblPlb"), Label)
                Dim txtPLBAmt As TextBox = CType(e.Item.FindControl("txtPLBAmt"), TextBox)
                Dim trLPlb As HtmlTableRow = CType(e.Item.FindControl("trLPlb"), HtmlTableRow)

                Dim txtRemarks As TextBox = CType(e.Item.FindControl("txtRemarks"), TextBox)
                Dim txtPayAppRemarks As TextBox = CType(e.Item.FindControl("txtPayAppRemarks"), TextBox)

               


                If ViewState("PaymentData") IsNot Nothing Then
                    Dim objOutPutxml As New XmlDocument
                    Dim objFinalOutPutxml As New XmlDocument
                    objFinalOutPutxml.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT> </UP_SER_INC_PAYMENTPROCESSDETAILS_OUTPUT>")
                    objOutPutxml.LoadXml(ViewState("PaymentData").ToString())
                    Dim objnode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("DATE[@LOGDATE='" + lblDate.Text.Trim + "']")
                    If objnode IsNot Nothing Then
                        objFinalOutPutxml.DocumentElement.AppendChild(objFinalOutPutxml.ImportNode(objnode, True))
                        Dim ds As DataSet
                        Dim objReadaer As XmlNodeReader
                        ds = New DataSet
                        objReadaer = New XmlNodeReader(objFinalOutPutxml)
                        ds.ReadXml(objReadaer)
                        dset = New DataSet
                        dset = ds

                        If ds.Tables("GROUPDETAILS") IsNot Nothing Then


                            hdFirstTime.Value = ds.Tables("GROUPDETAILS").Rows(0)("UPFRONTFIRSTTIME").ToString
                            hdPaymentType.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString
                            hdIsPLB.Value = ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString
                            hdFixedOrRate.Value = ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPEID").ToString


                            '   txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("REMARKS").ToString
                            If ds.Tables("GROUPDETAILS").Columns("QUALI_REMARKS") IsNot Nothing Then
                                txtRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALI_REMARKS").ToString
                            End If

                            If ds.Tables("GROUPDETAILS").Columns("PAREMARKS") IsNot Nothing Then
                                txtPayAppRemarks.Text = ds.Tables("GROUPDETAILS").Rows(0)("PAREMARKS").ToString
                            End If




                            If Request.QueryString("CurPayNo").ToString.ToUpper = "TRUE" Or Request.QueryString("CurPayNo").ToString = "1" Then
                                txtUpFrontAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("UPFRONT_AMOUNT").ToString).ToString("f2")
                                lblUpFrontAmount.Visible = True
                                txtUpFrontAmount.Visible = True
                                lblPrevUpfrontAmount.Visible = False
                                lblBalanceUpfrontAmount.Visible = False
                                lblLatestUpfontAmount.Visible = False
                                LblCBFAmount.Visible = False
                                LblSignUpAmt.Visible = True
                                txtPrevUpfrontAmount.Visible = False
                                txtBalanceUpfrontAmount.Visible = False
                                txtLatestUpfontAmount.Visible = False
                                txtCBForwardAmount.Visible = False
                                txtCBForwardAmount.Visible = False
                                txtSignUpAmt.Visible = True

                                trUpFrontAmount.Visible = True
                                trPrevUpfrontAmount.Visible = False
                                trBalanceUpfrontAmount.Visible = False
                                trLatestUpfontAmount.Visible = False
                                trCBFAmount.Visible = False
                                trSignUpAmt.Visible = True
                            Else
                                lblUpFrontAmount.Visible = False
                                txtUpFrontAmount.Visible = False
                                lblPrevUpfrontAmount.Visible = True
                                lblBalanceUpfrontAmount.Visible = True
                                LblCBFAmount.Visible = True
                                LblSignUpAmt.Visible = False

                                txtCBForwardAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BALANCE_CF").ToString).ToString("f2")

                                txtLatestUpfontAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("NEXTUPFRONTAMOUNT").ToString).ToString("f2")
                                ' HdPaidUpFrontAtthisLevel.Value = Val(ds.Tables("GROUPDETAILS").Rows(0)("NEXTUPFRONTAMOUNT").ToString).ToString("f2")

                                txtPrevUpfrontAmount.Visible = True
                                txtBalanceUpfrontAmount.Visible = True
                                txtCBForwardAmount.Visible = True
                                txtSignUpAmt.Visible = False
                                txtPrevUpfrontAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("PREVIOUSUPFRONTAMOUNT").ToString).ToString("f2")
                                txtBalanceUpfrontAmount.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BALANCEUPFRONTAMOUNT").ToString).ToString("f2")
                                trUpFrontAmount.Visible = False
                                trPrevUpfrontAmount.Visible = True
                                trBalanceUpfrontAmount.Visible = True
                                txtCBForwardAmount.Visible = True
                                trSignUpAmt.Visible = False
                                lblLatestUpfontAmount.Visible = True
                                txtLatestUpfontAmount.Visible = True
                                trLatestUpfontAmount.Visible = True
                            End If
                           
                            If Request.QueryString("PLB") IsNot Nothing Then
                                If Request.QueryString("PLB").Trim = "1" Then
                                    lblUpFrontAmount.Visible = False
                                    txtUpFrontAmount.Visible = False
                                    lblPrevUpfrontAmount.Visible = False
                                    lblBalanceUpfrontAmount.Visible = False
                                    lblLatestUpfontAmount.Visible = False
                                    LblCBFAmount.Visible = False
                                    LblSignUpAmt.Visible = False
                                    txtPrevUpfrontAmount.Visible = False
                                    txtBalanceUpfrontAmount.Visible = False
                                    txtLatestUpfontAmount.Visible = False
                                    txtCBForwardAmount.Visible = False
                                    txtCBForwardAmount.Visible = False
                                    txtSignUpAmt.Visible = False
                                    trUpFrontAmount.Visible = False
                                    trPrevUpfrontAmount.Visible = False
                                    trBalanceUpfrontAmount.Visible = False
                                    trLatestUpfontAmount.Visible = False
                                    trCBFAmount.Visible = False
                                    trSignUpAmt.Visible = False
                                    LblPlb.Visible = False
                                    txtPLBAmt.Visible = False
                                    trLPlb.Visible = False

                                    If ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString.Trim().ToUpper = "TRUE" Then
                                        If ds.Tables("GROUPDETAILS").Rows(0)("PLBTYPEID").ToString <> "2" Then   ' @ In Case Of PLB Fixed Based
                                            LblPlb.Visible = True
                                            txtPLBAmt.Visible = True
                                            txtPLBAmt.ReadOnly = True
                                            txtPLBAmt.CssClass = "textboxgrey right"
                                            txtPLBAmt.Text = ds.Tables("GROUPDETAILS").Rows(0)("PLBAMOUNT").ToString.Trim()
                                            trLPlb.Visible = True
                                        End If
                                    End If


                                End If
                            End If

                            If ds.Tables("GROUPDETAILS").Rows(0)("PAYMENTTYPE").ToString.Trim.ToUpper = "P" Then
                                trUpFrontAmount.Visible = False
                                txtUpFrontAmount.Visible = False
                                trLatestUpfontAmount.Visible = False
                                trPrevUpfrontAmount.Visible = False
                                LblCBFAmount.Text = "Carries Balance Amount"
                                lblBalanceUpfrontAmount.Text = "Balance Amount"
                            Else

                            End If

                            If ds.Tables("GROUPDETAILS").Rows(0)("ISPLB").ToString.Trim().ToUpper = "TRUE" Then
                                If ds.Tables("GROUPDETAILS").Rows(0)("YEARENDSETTLEMENT").ToString.Trim.ToUpper = "TRUE" Then
                                    LblPlb.Visible = True
                                    txtPLBAmt.Visible = True
                                    txtPLBAmt.Text = ds.Tables("GROUPDETAILS").Rows(0)("PLBAMOUNT").ToString.Trim()
                                    trLPlb.Visible = True
                                End If
                            End If
                          




                            Dim DblGrandFinal As Double = 0
                            If ds.Tables("PRODTYPEDETAILS") IsNot Nothing Then
                                GvProcessPayment.DataSource = ds.Tables("PRODTYPEDETAILS")
                                GvProcessPayment.DataBind()

                                Dim HdPaymentId As HtmlInputHidden = CType(e.Item.FindControl("HdPaymentId"), HtmlInputHidden)


                                HdPaymentId.Value = ds.Tables("PRODTYPEDETAILS").Rows(0)("PAYMENT_ID").ToString

                                If hdFixedOrRate.Value = "2" Then

                                    GvProcessPayment.HeaderRow.Cells(5).Text = "Standard Fixed / Revised Fixed"
                                    '  GvProcessPayment.HeaderRow.Cells(5).Text = "Revised Fixed"
                                End If
                                If Request.QueryString("PLB") IsNot Nothing Then
                                    If Request.QueryString("PLB").Trim = "1" Then
                                        If ds.Tables("GROUPDETAILS").Rows(0)("PLBTYPEID").ToString <> "2" Then
                                            GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Fixed / Revised Fixed"
                                        Else
                                            GvProcessPayment.HeaderRow.Cells(5).Text = "PLB" + "<br/>" + "Standard Rate / Revised Rate"
                                        End If
                                    End If
                                End If

                                GvProcessPayment.FooterRow.Cells(0).Text = "Total"
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
                                        'If hdFixedOrRate.Value = "2" Then
                                        '    'If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
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
                                CalculateBalance(GvProcessPayment, txtBalanceUpfrontAmount, txtCBForwardAmount, txtPLBAmt)


                            End If


                            If ds.Tables("GROUPDETAILS") IsNot Nothing Then

                                Dim txtQualAvgSelected As TextBox = CType(e.Item.FindControl("txtQualAvgSelected"), TextBox)
                                Dim txtQualAvgData As TextBox = CType(e.Item.FindControl("txtQualAvgData"), TextBox)

                                Dim strQulaField As String = ds.Tables("GROUPDETAILS").Rows(0)("QUALIFICATIONNIDS").ToString
                                strQulaField = strQulaField.Replace("_CODD_PK_HX", " ")
                                strQulaField = strQulaField.Replace("_DOM", " DOM")
                                txtQualAvgSelected.Text = strQulaField
                                txtQualAvgData.Text = ds.Tables("GROUPDETAILS").Rows(0)("QUALIAVG").ToString
                            End If


                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub CalculateBalance(ByVal GvProcessPayment As GridView, ByVal txtBalanceUpfrontAmount As TextBox, ByVal txtCBForwardAmount As TextBox, ByVal txtPLBAmt As TextBox)
        Dim DblBusinessAmout As Double = 0 'DblBusinessAmout =DblGrandFinal
        If GvProcessPayment.Rows.Count > 0 Then
            Dim txtGrandFinalAmount As TextBox = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), TextBox)
            DblBusinessAmout = Val(txtGrandFinalAmount.Text)
        Else
            DblBusinessAmout = 0
        End If
        If txtBalanceUpfrontAmount.Visible = True Then
            If Val(txtPLBAmt.Text) > 0 Then
                txtBalanceUpfrontAmount.Text = (DblBusinessAmout - Math.Abs(Val((Val(txtCBForwardAmount.Text)).ToString("f2"))) + Val(txtPLBAmt.Text)).ToString("f2")
            Else
                txtBalanceUpfrontAmount.Text = (DblBusinessAmout - Math.Abs(Val(txtCBForwardAmount.Text))).ToString("f2")
            End If
        End If
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            LoadHistory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            LoadHistory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            LoadHistory()
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
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else

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
#End Region
End Class
