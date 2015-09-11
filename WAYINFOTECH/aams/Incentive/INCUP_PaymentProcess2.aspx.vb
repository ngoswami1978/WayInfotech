Imports System.Text.RegularExpressions
Partial Class Incentive_INCUP_PaymentProcess2
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim dset As DataSet
    Dim strBuilder As New StringBuilder

    Protected WithEvents GvIncPlan As GridView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        lblError.Text = ""
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If


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

        If Not Page.IsPostBack Then
            ViewState("PaymentData") = Nothing


            '   BtnShowPlan.Attributes.Add("onclick", "return ShowPlan();")
            HdPaymentId.Value = ""
            dset = New DataSet
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)

                ' For Default Page Setting
                ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting

                If Request.QueryString("PayId") IsNot Nothing Then

                    Session("PaymentRecDataSource") = Nothing

                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        Dim Caseid As String = Request.QueryString("BCaseID").ToString
                        BtnBCase.Attributes.Add("onclick", "return DetailsFunction('" & Caseid & "','" & hdEnChainCode.Value & "');")
                    End If

                    LoadDataByPayId(Request.QueryString("PayId").ToString)

                Else

                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        Dim Caseid As String = Request.QueryString("BCaseID").ToString
                        BtnBCase.Attributes.Add("onclick", "return DetailsFunction('" & Caseid & "','" & hdEnChainCode.Value & "');")

                        LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)

                        Try

                        Catch ex As Exception

                        End Try
                        If Session("Msg") IsNot Nothing Then
                            lblError.Text = Session("Msg").ToString
                            Session("Msg") = Nothing
                        End If
                    End If
                End If




            End If

        End If
    End Sub

    Private Sub LoadData(ByVal strChainCode As String, ByVal strBcaseId As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Dim DblGrandFinal As Double = 0
        Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzIncentive
        '@ Code  for Details
        Try

            objInputXml.LoadXml("<UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE> <MONTH></MONTH> <YEAR></YEAR>    </UP_SER_INC_PAYMENTPROCESSDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcaseId
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Request.QueryString("Month")
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Request.QueryString("Year")
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = strChainCode

            'Dim str As String = ""
            'str = "<INC_VIEW_BUSINESSCASE_OUTPUT>"
            'str = str + "<GROUPDETAILS ACCOUNTMANAGER='' CHAIN_CODE='' GROUPNAME='' NAME='' AOFFICE='' CITY='' BILLINGCYCLE='' INC_TYPE_ID='4'    BC_ID='' BC_EFFECTIVE_FROM='' BC_VALID_TILL='' BC_DTTI_CREATED='' FINAL_APPROVED='' FINAL_APPROVED_BY='' FINAL_APPROVED_DTTI=''  INC_SLAB_REQUIRED='TRUE' SOLE_AMOUNT='FALSE' BONUS_AMOUNT='FALSE' FIXED_PAYMENT='TRUE' UPFRONT_AMOUNT='FALSE' FIXED_UPFRONT='FALSE' PA_CREATED='FALSE' />"
            'str = str + "<BC_PLAN>"
            'str = str + "<INC_TYPE INC_TYPE_ID='4' INC_TYPE_NAME='Fixed Payment + End of the year settlement on slabs' /> "
            'str = str + "<CASE INC_PLAN_ID='9' INC_PLAN_NAME='Case1'>"
            'str = str + "<NIDT_FIELDS_ID ID='1, 4, 5' /> "
            'str = str + "<PLAN_DETAILS SLABS_START='1' SLABS_END='100' SLABS_RATE='500' /> "
            'str = str + "<PLAN_DETAILS SLABS_START='101' SLABS_END='500' SLABS_RATE='500' /> "
            'str = str + "</CASE>"
            'str = str + "<CASE INC_PLAN_ID='10' INC_PLAN_NAME='Case2'>"
            'str = str + "<NIDT_FIELDS_ID ID='6, 7' /> "
            'str = str + " <PLAN_DETAILS SLABS_START='1' SLABS_END='100' SLABS_RATE='500' /> "
            'str = str + "<PLAN_DETAILS SLABS_START='101' SLABS_END='500' SLABS_RATE='500' /> "
            'str = str + "</CASE>"
            'str = str + "</BC_PLAN>"
            'str = str + "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT='' TOTAL='' /> "
            'str = str + "<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' PRODUCT_COUNT='' TOTAL='' /> "
            'str = str + " <PRODTYPE PAYMENT_ID ='' PRODUCTIVITYTYPEID ='20' PRODUCTIVITYTYPE='DOM_IT'   SEGMENT='50'  RATE='2' EXCEMPTION='1' AMOUNT='999994.89' FINALAMOUNT ='99999458999' />"
            'str = str + " <PRODTYPE PAYMENT_ID=''  PRODUCTIVITYTYPEID ='12' PRODUCTIVITYTYPE='AI_HL'  SEGMENT='100'  RATE='4' EXCEMPTION='34' AMOUNT='999992954.78' FINALAMOUNT='99999295499'/>"
            'str = str + "<Errors Status='FALSE'>"
            'str = str + "<Error Code='' Description='' /> "
            'str = str + "</Errors>"
            'str = str + "</INC_VIEW_BUSINESSCASE_OUTPUT>"
            ' objOutPutxml.LoadXml(str)

            objOutPutxml = objbzBCPaymentProcess.View(objInputXml)


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

                End If


                Try
                    '##########################################################

                    If ds.Tables("GROUPDETAILS") IsNot Nothing Then

                        'Incentive Type
                        Dim objIncType As New AAMS.bizIncetive.bzBusinessCase
                        'Dim objXmlReader As XmlReader
                        Dim dsIncType As New DataSet
                        objOutPutxml = New XmlDocument
                        objOutPutxml = objIncType.List_IncentiveType()
                        ' objOutputXml.LoadXml("C:\Admin\List_IncentiveType.xml")

                        If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            'objXmlReader = New XmlNodeReader(objOutPutxml)
                            'dsIncType.ReadXml(objXmlReader)

                            '<INC_TYPE INC_TYPE_ID="8" INC_TYPE_NAME="Domestic Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="2" INC_TYPE_NAME="Fixed Payment" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="TRUE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="3" INC_TYPE_NAME="Fixed Payment + Bonus(Fixed)" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="TRUE" BONUS_AMOUNT="TRUE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="4" INC_TYPE_NAME="Fixed Payment + End of the year settlement on slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="TRUE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="6" INC_TYPE_NAME="Fixed Upfront" INC_SLAB_REQUIRED="FALSE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="TRUE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="7" INC_TYPE_NAME="International Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="9" INC_TYPE_NAME="Intl + Dom Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="1" INC_TYPE_NAME="Slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="5" INC_TYPE_NAME="Upfront + End of the year settlement on slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 


                            'INC_TYPE
                            '
                            '###############################################

                            Dim StrINC_TYPE_ID As String = ""
                            StrINC_TYPE_ID = ds.Tables("GROUPDETAILS").Rows(0)("INC_TYPE_ID").ToString

                            Dim ObjNode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + StrINC_TYPE_ID + "']")
                            If ObjNode IsNot Nothing Then

                                txtIncetiveType.Text = ObjNode.Attributes("INC_TYPE_NAME").Value

                                '###############################################
                                ' On the basis of Incentive type  show plan details  
                                '##########################################################
                                '###################################################################################################
                                '@ Code For 

                                txtSoleAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SOLE_AMOUNT").ToString).ToString("f2")
                                'LblBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("BONUS_AMOUNT").ToString
                                txtFixedPayAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("FIXED_PAYMENT").ToString).ToString("f2")
                                txtUpfrontAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("UPFRONT_AMOUNT").ToString).ToString("f2")
                                txtFixUpFrontAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("FIXED_UPFRONT").ToString).ToString("f2")
                                txtBonusAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BONUS_AMOUNT").ToString).ToString("f2")


                                If ObjNode.Attributes("SOLE_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblSoleAmount.Visible = True
                                    txtSoleAmt.Visible = True
                                Else
                                    LblSoleAmount.Visible = False
                                    txtSoleAmt.Visible = False
                                End If

                                If ObjNode.Attributes("FIXED_PAYMENT").Value.Trim.ToUpper = "TRUE" Then
                                    LabelFixPayAmt.Visible = True
                                    txtFixedPayAmt.Visible = True

                                Else
                                    LabelFixPayAmt.Visible = False
                                    txtFixedPayAmt.Visible = False

                                End If


                                If ObjNode.Attributes("UPFRONT_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblUpFrontAmt.Visible = True
                                    txtUpfrontAmt.Visible = True

                                Else
                                    LblUpFrontAmt.Visible = False
                                    txtUpfrontAmt.Visible = False

                                End If
                                If ObjNode.Attributes("FIXED_UPFRONT").Value.Trim.ToUpper = "TRUE" Then
                                    LblFixUpFrontAmt.Visible = True
                                    txtFixUpFrontAmt.Visible = True

                                Else
                                    LblFixUpFrontAmt.Visible = False
                                    txtFixUpFrontAmt.Visible = False

                                End If

                                If ObjNode.Attributes("BONUS_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblBonus.Visible = True
                                    txtBonusAmt.Visible = True
                                Else
                                    LblBonus.Visible = False
                                    txtBonusAmt.Visible = False
                                End If
                                '@ Code Fo
                                '###################################################################################################

                            End If
                        End If
                    End If

                Catch ex As Exception
                End Try

                Try
                    If ds.Tables("PRODTYPE") IsNot Nothing Then


                        If ds.Tables("PRODTYPE").Rows(0)("PRODUCTIVITYTYPEID").ToString.Trim.Length > 0 Then


                            GvProcessPayment.DataSource = ds.Tables("PRODTYPE")
                            GvProcessPayment.DataBind()

                            HdPaymentId.Value = ds.Tables("PRODTYPE").Rows(0)("PAYMENT_ID").ToString

                            If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                                GvProcessPayment.HeaderRow.Cells(4).Text = "Amount"
                            End If

                            GvProcessPayment.FooterRow.Cells(2).Text = "Total"
                            GvProcessPayment.FooterRow.Cells(3).HorizontalAlign = HorizontalAlign.Center
                            GvProcessPayment.FooterRow.Cells(4).HorizontalAlign = HorizontalAlign.Right
                            GvProcessPayment.FooterRow.Cells(5).HorizontalAlign = HorizontalAlign.Right

                            Dim txtGrandFinalAmount As Label = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), Label)

                            Dim txtTotalCalCulatedSegment As Label = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), Label)

                            txtGrandFinalAmount.Text = 0
                            txtTotalCalCulatedSegment.Text = 0
                            'Dim strTotalCalCulatedSegment As String = ""
                            'Dim strGrandFinal As String = ""

                            Dim DblTotalCalCulatedSegment As Double = 0


                            For Each row As GridViewRow In GvProcessPayment.Rows
                                Dim txtActulaSegment As Label = CType(row.FindControl("txtActulaSegment"), Label)
                                Dim txtCalCulatedSegment As Label = CType(row.FindControl("txtCalCulatedSegment"), Label)
                                Dim txtExumption As Label = CType(row.FindControl("txtExumption"), Label)
                                Dim txtFinalAmount As Label = CType(row.FindControl("txtFinalAmount"), Label)
                                Dim txtRate As Label = CType(row.FindControl("txtRate"), Label)

                                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                                Dim dblAmountCalculate As Double = 0
                                If txtActulaSegment.Text.Trim.Length > 0 Then
                                    If txtExumption.Text.Trim.Length > 0 Then
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100
                                    Else
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100
                                    End If
                                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                                Else
                                    txtCalCulatedSegment.Text = "0.00"
                                End If
                                'End of Code for calculating Amount on the basis of (segment * Exemption)


                                DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate

                                ' ####################################################################
                                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    
                                Dim dblFinalCalCulatedSegment As Double = 0
                                If txtCalCulatedSegment.Text.Trim.Length > 0 Then

                                    If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then

                                        If txtRate.Text.Trim.Length > 0 Then
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                                        Else
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                                        End If
                                    Else
                                        If txtRate.Text.Trim.Length > 0 Then
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                                        Else
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                                        End If

                                    End If


                                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                                End If
                                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                                '####################################################################

                                DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment

                                txtFinalAmount.CssClass = "textboxgrey right"
                                txtCalCulatedSegment.CssClass = "textboxgrey right"

                            Next


                            txtTotalCalCulatedSegment.Text = DblTotalCalCulatedSegment.ToString("f2")
                            'strGrandFinal = DblGrandFinal.ToString("f2")
                            txtGrandFinalAmount.Text = DblGrandFinal.ToString("f2")



                            'Dim sum As Double
                            'If txtSoleAmt.Visible = True Then
                            '    sum = Val(txtSoleAmt.Text)
                            'End If
                            'If txtFixedPayAmt.Visible = True Then
                            '    sum += Val(txtFixedPayAmt.Text)
                            'End If

                            'If txtUpfrontAmt.Visible = True Then
                            '    sum += Val(txtUpfrontAmt.Text)
                            'End If

                            'If txtFixUpFrontAmt.Visible = True Then
                            '    sum += Val(txtFixUpFrontAmt.Text)
                            'End If
                            'If txtBonusAmt.Visible = True Then
                            '    sum += Val(txtBonusAmt.Text)
                            'End If

                            'txtGrandFinalAmt.Text = (DblGrandFinal + sum).ToString("f2")

                        End If

                    Else
                        GvProcessPayment.DataSource = Nothing
                        GvProcessPayment.DataBind()
                    End If
                Catch ex As Exception
                    ' lblError.Text = ex.Message
                End Try
            Else
                'GvConnectivity.DataSource = Nothing
                'GvConnectivity.DataBind()
                'GvHardware.DataSource = Nothing
                'GvHardware.DataBind()
                GvProcessPayment.DataSource = Nothing
                GvProcessPayment.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

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


        Dim sum As Double
        If txtSoleAmt.Visible = True Then
            sum = Val(txtSoleAmt.Text)
        End If
        If txtFixedPayAmt.Visible = True Then
            sum += Val(txtFixedPayAmt.Text)
        End If

        If txtUpfrontAmt.Visible = True Then
            sum += Val(txtUpfrontAmt.Text)
        End If

        If txtFixUpFrontAmt.Visible = True Then
            sum += Val(txtFixUpFrontAmt.Text)
        End If
        If txtBonusAmt.Visible = True Then
            sum += Val(txtBonusAmt.Text)
        End If
        txtGrandFinalAmt.Text = (DblGrandFinal + sum).ToString("f2")


    End Sub

    Protected Sub GvProcessPayment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvProcessPayment.RowCommand

    End Sub
    Protected Sub GvProcessPayment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvProcessPayment.RowDataBound
        Dim objOutputCriteriaXmlXml As New XmlDocument
        '        Dim dsCriteria As DataSet
        '        Dim objReadaerCriteria As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then



                'Dim sum As Double
                'sum = Val(txtSoleAmt.Text)
                'sum += Val(txtFixedPayAmt.Text)
                'sum += Val(txtUpfrontAmt.Text)
                'sum += Val(txtFixUpFrontAmt.Text)
                'sum += Val(txtBonusAmt.Text)

                Dim sum As Double
                If txtSoleAmt.Visible = True Then
                    sum = Val(txtSoleAmt.Text)
                End If
                If txtFixedPayAmt.Visible = True Then
                    sum += Val(txtFixedPayAmt.Text)
                End If

                If txtUpfrontAmt.Visible = True Then
                    sum += Val(txtUpfrontAmt.Text)
                End If

                If txtFixUpFrontAmt.Visible = True Then
                    sum += Val(txtFixUpFrontAmt.Text)
                End If
                If txtBonusAmt.Visible = True Then
                    sum += Val(txtBonusAmt.Text)
                End If




                Dim txtActulaSegment As Label
                Dim txtExumption As TextBox
                Dim txtCalCulatedSegment As Label
                Dim txtFinalAmount As Label
                Dim txtRate As Label

                txtActulaSegment = CType(e.Row.FindControl("txtActulaSegment"), Label)
                txtExumption = CType(e.Row.FindControl("txtExumption"), TextBox)
                txtCalCulatedSegment = CType(e.Row.FindControl("txtCalCulatedSegment"), Label)
                txtRate = CType(e.Row.FindControl("txtRate"), Label)
                txtFinalAmount = CType(e.Row.FindControl("txtFinalAmount"), Label)

                If txtExumption IsNot Nothing Then

                    ' txtExumption.Attributes.Add("onkeyup", "checknumericWithDotForInc('" + txtExumption.ClientID + "')")

                    'If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                    '    txtExumption.Attributes.Add("onblur", "ChangeAmountForFixedPayment('" + txtActulaSegment.ClientID + "','" + txtExumption.ClientID + "','" + txtCalCulatedSegment.ClientID + "','" + txtRate.ClientID + "','" + txtFinalAmount.ClientID + "','" + GvProcessPayment.ClientID + "','" + txtGrandFinalAmt.ClientID + "','" + sum.ToString("f2") + "')")
                    'Else
                    '    txtExumption.Attributes.Add("onblur", "ChangeAmount('" + txtActulaSegment.ClientID + "','" + txtExumption.ClientID + "','" + txtCalCulatedSegment.ClientID + "','" + txtRate.ClientID + "','" + txtFinalAmount.ClientID + "','" + GvProcessPayment.ClientID + "','" + txtGrandFinalAmt.ClientID + "','" + sum.ToString("f2") + "')")
                    'End If

                    txtExumption.Attributes.Add("onkeyup", "validateDecimalValue('" + txtExumption.ClientID + "')")

                    If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                        txtExumption.Attributes.Add("onblur", "validateDecimalValue('" + txtExumption.ClientID + "')")

                    Else
                        txtExumption.Attributes.Add("onblur", "validateDecimalValue('" + txtExumption.ClientID + "')")

                    End If
                    txtExumption.Attributes.Add("GetData", e.Row.RowIndex.ToString)

                End If

                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                If (Not Request.QueryString("BCaseID") = Nothing) Then
                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim ds As New DataSet
                    Dim Rowno As Integer
                    Dim objChildNode, objChildNodeClone As XmlNode
                    Dim strAoffice As String = ""
                    Dim objbzProcessPayment As New AAMS.bizIncetive.bzIncentive

                    objInputXml.LoadXml("<UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT><BCDETAIL BC_ID='' PAYMENT_ID ='' MONTH='' YEAR='' EMPLOYEE_ID='' TOTAL_AMT='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT=''/><PRODTYPE PAYMENT_ID='' PRODUCTIVITYTYPEID ='' PRODUCTIVITYTYPE='' SEGMENT='' RATE='' EXCEMPTION='' AMOUNT='' FINALAMOUNT='' /></UP_UPDATE_INC_PAYMENTPROCESSDETAILS_INPUT>")

                    'Reading and Appending records into the Input XMLDocument                  
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BC_ID").Value = Request.QueryString("BCaseID") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("MONTH").Value = Request.QueryString("Month") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("YEAR").Value = Request.QueryString("Year") 'objED.Decrypt(Request.QueryString("Chain_Code").Trim)

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("PAYMENT_ID").Value = HdPaymentId.Value

                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("PRODTYPE")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objInputXml.DocumentElement.RemoveChild(objChildNode)

                    Dim sumSegmentFinal As Double = 0
                    Dim sumFinal As Double = 0

                    Dim DblTotalCalCulatedSegment As Double = 0
                    Dim DblGrandFinal As Double = 0

                    'If GvProcessPayment.Rows.Count.ToString <= 0 Then
                    '    lblError.Text = "Not any item exist for saving."
                    '    Exit Sub
                    'End If
                    For Rowno = 0 To GvProcessPayment.Rows.Count - 1

                        ' Dim ChkProductivity As CheckBox = CType(GvProcessPayment.Rows(Rowno).FindControl("ChkProductivity"), CheckBox)


                        Dim lblProductivity As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("lblProductivity"), Label)
                        Dim hdPayMentId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdPayMentId"), HiddenField)

                        'Dim txtExumption As Label
                        'Dim txtAmount As Label
                        'Dim txtFinalAmount As Label

                        Dim txtActulaSegment As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("txtActulaSegment"), Label)
                        Dim txtCalCulatedSegment As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("txtCalCulatedSegment"), Label)
                        Dim txtExumption As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("txtExumption"), Label)
                        Dim txtFinalAmount As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("txtFinalAmount"), Label)
                        Dim txtRate As Label = CType(GvProcessPayment.Rows(Rowno).FindControl("txtRate"), Label)

                        Dim hdProductivityId As HiddenField = CType(GvProcessPayment.Rows(Rowno).FindControl("hdProductivityId"), HiddenField)
                        '  objChildNodeClone.InnerText = hdProductivityId.Value 'dbgrdCategoryProductDesc.Rows(Rowno).Cells(0).Text 'strAoffice

                        objChildNodeClone.Attributes("PRODUCTIVITYTYPEID").Value = hdProductivityId.Value
                        objChildNodeClone.Attributes("PRODUCTIVITYTYPE").Value = lblProductivity.Text ' ChkProductivity.Text
                        objChildNodeClone.Attributes("SEGMENT").Value = txtActulaSegment.Text
                        objChildNodeClone.Attributes("RATE").Value = txtRate.Text
                        objChildNodeClone.Attributes("EXCEMPTION").Value = txtExumption.Text
                        objChildNodeClone.Attributes("AMOUNT").Value = "0.00"

                        'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                        Dim dblAmountCalculate As Double = 0
                        If txtActulaSegment.Text.Trim.Length > 0 Then
                            If txtExumption.Text.Trim.Length > 0 Then
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100
                            Else
                                dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100
                            End If
                            txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                        Else
                            txtCalCulatedSegment.Text = "0.00"
                        End If
                        'End of Code for calculating Amount on the basis of (segment * Exemption)


                        DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate

                        ' ####################################################################
                        ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    
                        Dim dblFinalCalCulatedSegment As Double = 0
                        If txtCalCulatedSegment.Text.Trim.Length > 0 Then

                            If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then

                                If txtRate.Text.Trim.Length > 0 Then
                                    dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                                Else
                                    dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                                End If
                            Else
                                If txtRate.Text.Trim.Length > 0 Then
                                    dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                                Else
                                    dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                                End If
                            End If

                            txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                        End If
                        ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                        '####################################################################

                        DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment

                        txtFinalAmount.CssClass = "textboxgrey right"
                        txtCalCulatedSegment.CssClass = "textboxgrey right"

                        objChildNodeClone.Attributes("FINALAMOUNT").Value = dblFinalCalCulatedSegment.ToString("f2") 'Val(txtAmount.Text) - (Val(txtAmount.Text) * Val(txtExumption.Text)) / 100

                        objChildNodeClone.Attributes("PAYMENT_ID").Value = hdPayMentId.Value

                        ' sumFinal = sumFinal + Val(txtFinalAmount.Text)

                        objInputXml.DocumentElement.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)

                    Next Rowno



                    If GvProcessPayment.Rows.Count > 0 Then
                        Dim txtTotalCalCulatedSegment As Label = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), Label)
                        Dim txtGrandFinalAmount As Label = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), Label)
                        txtTotalCalCulatedSegment.Text = DblTotalCalCulatedSegment.ToString("f2")
                        txtGrandFinalAmount.Text = DblGrandFinal.ToString("f2")
                    End If

                    Dim sum As Double
                    If txtSoleAmt.Visible = True Then
                        sum = Val(txtSoleAmt.Text)
                    End If
                    If txtFixedPayAmt.Visible = True Then
                        sum += Val(txtFixedPayAmt.Text)
                    End If

                    If txtUpfrontAmt.Visible = True Then
                        sum += Val(txtUpfrontAmt.Text)
                    End If

                    If txtFixUpFrontAmt.Visible = True Then
                        sum += Val(txtFixUpFrontAmt.Text)
                    End If
                    If txtBonusAmt.Visible = True Then
                        sum += Val(txtBonusAmt.Text)
                    End If



                    txtGrandFinalAmt.Text = (DblGrandFinal + sum).ToString("f2")




                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("TOTAL_AMT").Value = (DblGrandFinal + sum).ToString("f2")
                    ' DblTotalCalCulatedSegment.ToString("f2")

                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("EMPLOYEE_ID").Value = Session("LoginSession").ToString().Split("|")(0)
                    End If


                    ' SOLE_AMOUNT='FALSE' BONUS_AMOUNT='FALSE' FIXED_PAYMENT='TRUE' UPFRONT_AMOUNT='FALSE' FIXED_UPFRONT='FALSE'

                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("SOLE_AMOUNT").Value = txtSoleAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_PAYMENT").Value = txtFixedPayAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("UPFRONT_AMOUNT").Value = txtUpfrontAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("FIXED_UPFRONT").Value = txtFixUpFrontAmt.Text
                    objInputXml.DocumentElement.SelectSingleNode("BCDETAIL").Attributes("BONUS_AMOUNT").Value = txtBonusAmt.Text


                    ' Here Back end Method Call
                    objOutputXml = objbzProcessPayment.UpdatePaymentProcessDetails(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = "Payment Updated Successfully."
                        If Request.QueryString("Chain_Code") IsNot Nothing Then
                            hdEnChainCode.Value = Request.QueryString("Chain_Code")
                            hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                            If Request.QueryString("BCaseID") IsNot Nothing Then
                                LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                            End If
                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub


    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub


    Protected Sub BtnSendForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSendForApproval.Click

        If HdPaymentId.Value.Trim.Length > 0 Then
            Dim objInputXml As New XmlDocument
            Dim objOutPutxml As New XmlDocument
            '            Dim ds As DataSet
            '            Dim objReadaer As XmlNodeReader
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
        '@ Code  for Details
        Try

            objInputXml.LoadXml("<UP_VIEW_INC_PAYMENT_APPROVAL_QUE_INPUT>     <PAYMENT_ID></PAYMENT_ID>     </UP_VIEW_INC_PAYMENT_APPROVAL_QUE_INPUT >")

            objInputXml.DocumentElement.SelectSingleNode("PAYMENT_ID").InnerText = strPayId
            'objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Request.QueryString("Month")
            'objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Request.QueryString("Year")
            'objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = strChainCode

            objOutPutxml = objbzbzPaymentApprovalQue.View(objInputXml)

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

                End If


                Try
                    '##########################################################

                    If ds.Tables("GROUPDETAILS") IsNot Nothing Then

                        'Incentive Type
                        Dim objIncType As New AAMS.bizIncetive.bzBusinessCase
                        'Dim objXmlReader As XmlReader
                        Dim dsIncType As New DataSet
                        objOutPutxml = New XmlDocument
                        objOutPutxml = objIncType.List_IncentiveType()
                        ' objOutputXml.LoadXml("C:\Admin\List_IncentiveType.xml")

                        If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            'objXmlReader = New XmlNodeReader(objOutPutxml)
                            'dsIncType.ReadXml(objXmlReader)

                            '<INC_TYPE INC_TYPE_ID="8" INC_TYPE_NAME="Domestic Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="2" INC_TYPE_NAME="Fixed Payment" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="TRUE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="3" INC_TYPE_NAME="Fixed Payment + Bonus(Fixed)" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="TRUE" BONUS_AMOUNT="TRUE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="4" INC_TYPE_NAME="Fixed Payment + End of the year settlement on slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="TRUE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="6" INC_TYPE_NAME="Fixed Upfront" INC_SLAB_REQUIRED="FALSE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="TRUE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="7" INC_TYPE_NAME="International Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="9" INC_TYPE_NAME="Intl + Dom Rates" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="1" INC_TYPE_NAME="Slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 
                            '  <INC_TYPE INC_TYPE_ID="5" INC_TYPE_NAME="Upfront + End of the year settlement on slabs" INC_SLAB_REQUIRED="TRUE" SOLE_AMOUNT="FALSE" BONUS_AMOUNT="FALSE" FIXED_PAYMENT="FALSE" UPFRONT_AMOUNT="FALSE" FIXED_UPFRONT="FALSE" /> 


                            'INC_TYPE
                            '
                            '###############################################

                            Dim StrINC_TYPE_ID As String = ""
                            StrINC_TYPE_ID = ds.Tables("GROUPDETAILS").Rows(0)("INC_TYPE_ID").ToString

                            Dim ObjNode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + StrINC_TYPE_ID + "']")
                            If ObjNode IsNot Nothing Then

                                txtIncetiveType.Text = ObjNode.Attributes("INC_TYPE_NAME").Value


                                '###############################################
                                ' On the basis of Incentive type  show plan details  
                                '##########################################################


                                '###################################################################################################
                                '@ Code For 

                                txtSoleAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("SOLE_AMOUNT").ToString).ToString("f2")
                                'LblBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("BONUS_AMOUNT").ToString
                                txtFixedPayAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("FIXED_PAYMENT").ToString).ToString("f2")
                                txtUpfrontAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("UPFRONT_AMOUNT").ToString).ToString("f2")
                                txtFixUpFrontAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("FIXED_UPFRONT").ToString).ToString("f2")
                                txtBonusAmt.Text = Val(ds.Tables("GROUPDETAILS").Rows(0)("BONUS_AMOUNT").ToString).ToString("f2")




                                If ObjNode.Attributes("SOLE_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblSoleAmount.Visible = True
                                    txtSoleAmt.Visible = True
                                Else
                                    LblSoleAmount.Visible = False
                                    txtSoleAmt.Visible = False
                                End If

                                If ObjNode.Attributes("FIXED_PAYMENT").Value.Trim.ToUpper = "TRUE" Then
                                    LabelFixPayAmt.Visible = True
                                    txtFixedPayAmt.Visible = True

                                Else
                                    LabelFixPayAmt.Visible = False
                                    txtFixedPayAmt.Visible = False

                                End If


                                If ObjNode.Attributes("UPFRONT_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblUpFrontAmt.Visible = True
                                    txtUpfrontAmt.Visible = True

                                Else
                                    LblUpFrontAmt.Visible = False
                                    txtUpfrontAmt.Visible = False

                                End If
                                If ObjNode.Attributes("FIXED_UPFRONT").Value.Trim.ToUpper = "TRUE" Then
                                    LblFixUpFrontAmt.Visible = True
                                    txtFixUpFrontAmt.Visible = True

                                Else
                                    LblFixUpFrontAmt.Visible = False
                                    txtFixUpFrontAmt.Visible = False

                                End If



                                If ObjNode.Attributes("BONUS_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblBonus.Visible = True
                                    txtBonusAmt.Visible = True
                                Else
                                    LblBonus.Visible = False
                                    txtBonusAmt.Visible = False
                                End If
                                '@ Code Fo
                                '###################################################################################################
                            End If
                        End If
                    End If

                Catch ex As Exception
                End Try

                Try
                    If ds.Tables("PRODTYPE") IsNot Nothing Then
                        If ds.Tables("PRODTYPE").Rows(0)("PRODUCTIVITYTYPEID").ToString.Trim.Length > 0 Then
                            GvProcessPayment.DataSource = ds.Tables("PRODTYPE")
                            GvProcessPayment.DataBind()
                            HdPaymentId.Value = ds.Tables("PRODTYPE").Rows(0)("PAYMENT_ID").ToString

                            If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then

                                GvProcessPayment.HeaderRow.Cells(4).Text = "Amount"
                            End If

                            GvProcessPayment.FooterRow.Cells(2).Text = "Total"
                            GvProcessPayment.FooterRow.Cells(3).HorizontalAlign = HorizontalAlign.Center
                            GvProcessPayment.FooterRow.Cells(4).HorizontalAlign = HorizontalAlign.Right
                            GvProcessPayment.FooterRow.Cells(5).HorizontalAlign = HorizontalAlign.Right

                            Dim txtGrandFinalAmount As Label = CType(GvProcessPayment.FooterRow.FindControl("txtGrandFinalAmount"), Label)

                            Dim txtTotalCalCulatedSegment As Label = CType(GvProcessPayment.FooterRow.FindControl("txtTotalCalCulatedSegment"), Label)

                            txtGrandFinalAmount.Text = 0
                            txtTotalCalCulatedSegment.Text = 0
                            'Dim strTotalCalCulatedSegment As String = ""
                            'Dim strGrandFinal As String = ""

                            Dim DblTotalCalCulatedSegment As Double = 0

                            For Each row As GridViewRow In GvProcessPayment.Rows
                                Dim txtActulaSegment As Label = CType(row.FindControl("txtActulaSegment"), Label)
                                Dim txtCalCulatedSegment As Label = CType(row.FindControl("txtCalCulatedSegment"), Label)
                                Dim txtExumption As Label = CType(row.FindControl("txtExumption"), Label)
                                Dim txtFinalAmount As Label = CType(row.FindControl("txtFinalAmount"), Label)
                                Dim txtRate As Label = CType(row.FindControl("txtRate"), Label)

                                'Start of Code for calculating Calcullated Segment on the basis of (segment * Exemption)
                                Dim dblAmountCalculate As Double = 0
                                If txtActulaSegment.Text.Trim.Length > 0 Then
                                    If txtExumption.Text.Trim.Length > 0 Then
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(txtExumption.Text)) / 100
                                    Else
                                        dblAmountCalculate = Double.Parse(txtActulaSegment.Text.Trim) - (Double.Parse(txtActulaSegment.Text.Trim)) * (Double.Parse(0)) / 100
                                    End If
                                    txtCalCulatedSegment.Text = dblAmountCalculate.ToString("f2")
                                Else
                                    txtCalCulatedSegment.Text = "0.00"
                                End If
                                'End of Code for calculating Amount on the basis of (segment * Exemption)


                                DblTotalCalCulatedSegment = DblTotalCalCulatedSegment + dblAmountCalculate

                                ' ####################################################################
                                ' @ Start If Exemption is zero then Calculated Segment and  final amount is same    
                                Dim dblFinalCalCulatedSegment As Double = 0
                                If txtCalCulatedSegment.Text.Trim.Length > 0 Then

                                    If txtIncetiveType.Text.Trim.ToUpper = "FIXED PAYMENT" Then
                                        If txtRate.Text.Trim.Length > 0 Then
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(txtRate.Text.Trim)
                                        Else
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) + Double.Parse(0)
                                        End If
                                    Else
                                        If txtRate.Text.Trim.Length > 0 Then
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(txtRate.Text.Trim)
                                        Else
                                            dblFinalCalCulatedSegment = Double.Parse(txtCalCulatedSegment.Text) * Double.Parse(0)
                                        End If
                                    End If

                                    txtFinalAmount.Text = dblFinalCalCulatedSegment.ToString("f2")
                                End If
                                ' @ End of If Exemption is zero then Calculated Segment and  final amount is same    
                                '####################################################################

                                DblGrandFinal = DblGrandFinal + dblFinalCalCulatedSegment

                                txtFinalAmount.CssClass = "textboxgrey right"
                                txtCalCulatedSegment.CssClass = "textboxgrey right"

                            Next

                            txtTotalCalCulatedSegment.Text = DblTotalCalCulatedSegment.ToString("f2")
                            'strGrandFinal = DblGrandFinal.ToString("f2")
                            txtGrandFinalAmount.Text = DblGrandFinal.ToString("f2")



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
        btnSave.Enabled = False

        Dim sum As Double
        If txtSoleAmt.Visible = True Then
            sum = Val(txtSoleAmt.Text)
        End If
        If txtFixedPayAmt.Visible = True Then
            sum += Val(txtFixedPayAmt.Text)
        End If

        If txtUpfrontAmt.Visible = True Then
            sum += Val(txtUpfrontAmt.Text)
        End If

        If txtFixUpFrontAmt.Visible = True Then
            sum += Val(txtFixUpFrontAmt.Text)
        End If
        If txtBonusAmt.Visible = True Then
            sum += Val(txtBonusAmt.Text)
        End If
        txtGrandFinalAmt.Text = (DblGrandFinal + sum).ToString("f2")

    End Sub

    Protected Sub txtExumption_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim txtExp As TextBox = CType(sender, TextBox)
            Dim index As Integer = Integer.Parse(txtExp.Attributes("GetData"))
            Dim row As GridViewRow = GvProcessPayment.Rows(index)

            Dim txtFinalAmount As Label = CType(row.FindControl("txtFinalAmount"), Label)

            txtFinalAmount.Text = (100 * index).ToString

            Dim kk As Integer = 100
            '  txtExp.Focus()


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
                    Exit Sub
                End If
            Else
                lblError.Text = "Only numeric with decimal is allowed."
                Exit Sub

            End If
            '@ End of  Checking Decimal Value                   {

            If GvProcessPayment.Rows.Count > index + 1 Then

                Dim rownext As GridViewRow = GvProcessPayment.Rows(index + 1)
                Dim kkinh As Double = Val(txtExp.Text) * 100
                If rownext IsNot Nothing Then
                    Dim txtNextExp As TextBox = CType(rownext.FindControl("txtExumption"), TextBox)
                    ' txtNextExp.Focus()
                    Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                    scriptManager.SetFocus(txtNextExp.ClientID)
                End If
            Else
                Dim scriptManager As ScriptManager = scriptManager.GetCurrent(Me.Page)
                scriptManager.SetFocus(btnSave.ClientID)

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
