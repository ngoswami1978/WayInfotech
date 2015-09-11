
Partial Class Incentive_INCSR_BacseDetails
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim dset As DataSet
    Protected WithEvents GvIncPlan As GridView
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try

            'TabText.Add("Agency Group")
            'TabText.Add("CRS Details")
            'TabText.Add("Competition")
            'TabText.Add("Staff")
            'TabText.Add("PC")
            'TabText.Add("Contract")
            'TabText.Add("Business Case")

            'theTabStrip.DataSource = TabText
            'theTabStrip.DataBind()

            BindSubTabdata()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Sub BindSubTabdata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Details")
            TabText.Add("Remarks (Region) ")
            TabText.Add("Remarks (Market) ")
            TabText.Add("Approvers")
            theTabSubStrip.DataSource = TabText
            theTabSubStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 6 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")

        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")


    End Sub

    Protected Sub theTabSubStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 0 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',4);")


    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            dset = New DataSet
            Bindata()
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                ' For Default Page Setting
                ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                End If

            End If

        End If
    End Sub

    Private Sub LoadData(ByVal strChainCode As String, ByVal strBcaseId As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

        '@ Code  for Details
        Try

            objInputXml.LoadXml("<INC_VIEW_BUSINESSCASE_INPUT>	<BC_ID></BC_ID><Chain_Code></Chain_Code>></INC_VIEW_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcaseId

            objOutPutxml = objbzBusinessCase.View(objInputXml)

            '          - <INC_VIEW_BUSINESSCASE_OUTPUT>
            '  <BUSINESSCASE BC_ID="18" CHAIN_CODE="12503" GROUP_NAME="TRAVEL WAYS LTD" AccountManager_Name="Admin" Region="NORTH EAST" PAYMENT_CYCLE_ID="3" PAYMENT_CYCLE_NAME="Qtrly" BC_EFFECTIVE_FROM="01-Jan-08" BC_VALID_TILL="01-Jan-12" BC_DTTI_CREATED="8/11/2009 7:13:38 PM" FINAL_APPROVED="" FINAL_APPROVED_BY="" FINAL_APPROVED_DTTI="" INC_TYPE_ID="4" INC_TYPE_NAME="Fixed Payment + End of the year settlement on slabs" SOLE_AMOUNT="50000" BONUS_AMOUNT="25000" FIXED_PAYMENT="" UPFRONT_AMOUNT="" FIXED_UPFRONT="" APPROVERS="" /> 
            '- <BC_PLAN>
            '- <CASE INC_PLAN_ID="" INC_TYPE_NAME="">
            '  <PLAN_DETAILS SLABS_START="" SLABS_END="" SLABS_RATE="" /> 
            '  <NIDT_FIELDS_ID ID="" /> 
            '  </CASE>
            '  </BC_PLAN>
            '  <BC_CONN BC_ONLINE_CATG_ID="" BC_ONLINE_CATG_NAME="" BC_ONLINE_CATG_COST="" CONN_COUNT="" TOTAL="" /> 
            '  <BC_EQP BC_EQP_CATG_ID="" BC_EQP_CATG_TYPE="" BC_EQP_CATG_COST="" PRODUCT_COUNT="" TOTAL="" /> 
            '- <Errors Status="FALSE">
            '  <Error Code="" Description="" /> 
            '  </Errors>
            '  <BC_MIDT_GROUP LASTAVAIL="AVG PAST 3 MONTHS" A="32" B="0" G="0" P="0" W="0" TOTAL="32" A_PER="100" B_PER="0" G_PER="0" P_PER="0" W_PER="0" /> 
            '  <BC_MIDT_GROUP LASTAVAIL="2" A="175" B="0" G="0" P="0" W="5" TOTAL="180" A_PER="97" B_PER="0" G_PER="0" P_PER="0" W_PER="3" /> 
            '  <BC_MIDT_GROUP LASTAVAIL="3" A="175" B="0" G="0" P="0" W="5" TOTAL="180" A_PER="97" B_PER="0" G_PER="0" P_PER="0" W_PER="3" /> 
            '  <BC_MIDT_AGENCY Name="Travel Ways Ltd - Kawran" OfficeID="DACVS3430" Address="99 Kazi Nazrul Islam Avenue, 2nd FloorKawran Bazar" A="142" B="0" G="0" P="0" W="0" TTP="142" /> 
            '  <BC_MIDT_BRK TYPE="" TOTAL="" PER="" LESS="" RATE="" AMT="" /> 
            '  </INC_VIEW_BUSINESSCASE_OUTPUT>


            'Dim str As String = "<INC_VIEW_BUSINESSCASE_OUTPUT>"
            'str &= "<BUSINESSCASE BC_ID='' CHAIN_CODE='' GROUP_NAME='' BC_EFFECTIVE_FROM='' BC_VALID_TILL='' AccountManager='' AccountManager_Name='' Region='' PAYMENT_CYCLE_ID='' APPROVERS='A,B,C' INC_TYPE_NAME =''/>"
            'str &= "<BC_MIDT_GROUP LASTAVAIL='2008 (AVG PAST 3 MNTHS)'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            'str &= "<BC_MIDT_GROUP LASTAVAIL='2007'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            'str &= "<BC_MIDT_GROUP LASTAVAIL='2006'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            'str &= "<BC_MIDT_GROUP LASTAVAIL='2006'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            'str &= "<BC_MIDT_GROUP LASTAVAIL='2006'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            'str &= "<BC_MIDT_AGENCY Name='uer ewkr erk erewuirer ewri ewrui eruiew ewrui ereuier ew' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            'str &= "<BC_PLAN>"
            'str &= " <INC_TYPE INC_TYPE_NAME='Fixed Paymant'/>"
            'str &= "<CASE INC_PLAN_ID='1' INC_TYPE_NAME='Case1'>"
            'str &= "<PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/>"
            'str &= "<NIDT_FIELDS_ID ID='7,8,6'></NIDT_FIELDS_ID>"
            'str &= "</CASE >"
            'str &= "<CASE INC_PLAN_ID='2' INC_TYPE_NAME='Case2'>"
            'str &= "<PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/>"
            'str &= "<PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/>"
            'str &= "<NIDT_FIELDS_ID ID='3,5,6'></NIDT_FIELDS_ID>"
            'str &= "</CASE >"
            'str &= "</BC_PLAN>"
            'str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT='' TOTAL='' />"
            'str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT=''  TOTAL='' />"
            'str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT=''  TOTAL='' />"
            'str &= "<BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            'str &= "<BC_EQP BC_EQP_CATG_ID=''   BC_EQP_CATG_TYPE=''  BC_EQP_CATG_TYPE=''BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            'str &= "<BC_EQP BC_EQP_CATG_ID=''   BC_EQP_CATG_TYPE='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            'str &= "<BC_EQP BC_EQP_CATG_ID=''  BC_EQP_CATG_TYPE=''  BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            'str &= "<BC_MIDT_BRK TYPE='' TOTAL='' PER='' LESS='' RATE='' AMT=''/>"
            'str &= "<BC_APPROVERS EMPLOYEE_NAME='' APPROVAL_STATUS_NAME=''/>"
            'str &= "<BC_APPROVERS EMPLOYEE_NAME='' APPROVAL_STATUS_NAME=''/>"

            'str &= "<Errors Status='False'>"
            'str &= "<Error Code='' Description='' />"
            'str &= "</Errors>"
            'str &= "</INC_VIEW_BUSINESSCASE_OUTPUT>"



            '  objOutPutxml.LoadXml(str)

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                'Dim objXmlApproversDocument As New XmlDocument '
                'objXmlApproversDocument.LoadXml("<PP><BC_APPROVERS EMPLOYEE_NAME='' APPROVAL_STATUS_NAME=''/></PP>")

                'With objXmlApproversDocument.DocumentElement.SelectSingleNode("BC_APPROVERS")
                '    .Attributes("APPROVAL_STATUS_NAME").Value = "Pending"
                '    .Attributes("EMPLOYEE_NAME").Value = "Abhish Bhattachrya"
                'End With

                ' objOutPutxml.DocumentElement.AppendChild(objOutPutxml.ImportNode((objXmlApproversDocument.DocumentElement.SelectSingleNode("BC_APPROVERS")), True))

                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)

                dset = New DataSet
                dset = ds

                If ds.Tables("BUSINESSCASE") IsNot Nothing Then
                    '<BUSINESSCASE BC_ID='' CHAIN_CODE=''
                    'GROUP_NAME='' BC_DTTI_EFFECTIVE_FROM='' BC_DTTI_VALID_TILL='' 
                    'AccountManager='' AccountManager_Name='' Region='' PAYMENT_CYCLE_ID='' APPROVERS=''/>"

                    txtGroupName.Text = ds.Tables("BUSINESSCASE").Rows(0)("GROUP_NAME").ToString
                    txtActManager.Text = ds.Tables("BUSINESSCASE").Rows(0)("AccountManager_Name").ToString

                    If ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString = "1" Then
                        txtBillCycle.Text = "Annual"
                    ElseIf ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString = "2" Then
                        txtBillCycle.Text = "Bi-Annual"
                    ElseIf ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString = "3" Then
                        txtBillCycle.Text = "Qtrly"
                    ElseIf ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString = "4" Then
                        txtBillCycle.Text = "Monthly"
                    Else
                        txtBillCycle.Text = ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString
                    End If

                    txtChainCode.Text = ds.Tables("BUSINESSCASE").Rows(0)("CHAIN_CODE").ToString
                    txtRegion.Text = ds.Tables("BUSINESSCASE").Rows(0)("Region").ToString

                    '  Dim strFrom As String = objeAAMS.GetDateFormat(ds.Tables("BUSINESSCASE").Rows(0)("BC_EFFECTIVE_FROM"), "dd/MM/yyyy", "dd-MMM-yy", "/") 'objeAAMS.GetDateFormat(").ToString,

                    '  Dim strTo As String = objeAAMS.GetDateFormat(ds.Tables("BUSINESSCASE").Rows(0)("BC_VALID_TILL"), "dd/MM/yyyy", "dd-MMM-yy", "/") 'objeAAMS.GetDateFormat(").ToString,

                    txtContractPeriod.Text = ds.Tables("BUSINESSCASE").Rows(0)("BC_EFFECTIVE_FROM").ToString + " - " + ds.Tables("BUSINESSCASE").Rows(0)("BC_VALID_TILL").ToString

                    ' Dim arrayApproversList As New ArrayList(ds.Tables("BUSINESSCASE").Rows(0)("APPROVERS").ToString().Split(","))

                    'If arrayApproversList.Count > 0 Then
                    '    LstApprovers.DataSource = arrayApproversList
                    '    LstApprovers.DataBind()
                    'End If

                    lblType.Text = ds.Tables("BUSINESSCASE").Rows(0)("INC_TYPE_NAME").ToString






                    '##################################################
                    '  Getting Incentive Type 

                  

                    LblSoleAmt.Text = ds.Tables("BUSINESSCASE").Rows(0)("SOLE_AMOUNT").ToString
                    'LblBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("BONUS_AMOUNT").ToString
                    LblFixPayment.Text = ds.Tables("BUSINESSCASE").Rows(0)("FIXED_PAYMENT").ToString
                    LblUpfront.Text = ds.Tables("BUSINESSCASE").Rows(0)("UPFRONT_AMOUNT").ToString
                    LblFixUpfront.Text = ds.Tables("BUSINESSCASE").Rows(0)("FIXED_UPFRONT").ToString
                    LblSoleAmtBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("SOLE_AMOUNT").ToString


                End If


                If ds.Tables("BC_APPROVERS") IsNot Nothing Then
                    GvApprovers.DataSource = ds.Tables("BC_APPROVERS")
                    GvApprovers.DataBind()
                Else
                    GvApprovers.DataSource = Nothing
                    GvApprovers.DataBind()
                End If

                If ds.Tables("BC_MIDT_GROUP") IsNot Nothing Then
                    GvBGroupMIDT.DataSource = ds.Tables("BC_MIDT_GROUP")
                    GvBGroupMIDT.DataBind()
                Else
                    GvBGroupMIDT.DataSource = Nothing
                    GvBGroupMIDT.DataBind()
                End If

                If ds.Tables("BC_MIDT_AGENCY") IsNot Nothing Then
                    GvBAgencyMIDT.DataSource = ds.Tables("BC_MIDT_AGENCY")
                    GvBAgencyMIDT.DataBind()

                    GvBAgencyMIDT.FooterRow.Cells(4).Text = 0
                    GvBAgencyMIDT.FooterRow.Cells(5).Text = 0
                    GvBAgencyMIDT.FooterRow.Cells(6).Text = 0
                    GvBAgencyMIDT.FooterRow.Cells(7).Text = 0
                    GvBAgencyMIDT.FooterRow.Cells(8).Text = 0
                    GvBAgencyMIDT.FooterRow.Cells(9).Text = 0

                    For Each row As GridViewRow In GvBAgencyMIDT.Rows
                        GvBAgencyMIDT.FooterRow.Cells(4).Text = Val(GvBAgencyMIDT.FooterRow.Cells(4).Text) + Val(row.Cells(4).Text)
                        GvBAgencyMIDT.FooterRow.Cells(5).Text = Val(GvBAgencyMIDT.FooterRow.Cells(5).Text) + Val(row.Cells(5).Text)
                        GvBAgencyMIDT.FooterRow.Cells(6).Text = Val(GvBAgencyMIDT.FooterRow.Cells(6).Text) + Val(row.Cells(6).Text)
                        GvBAgencyMIDT.FooterRow.Cells(7).Text = Val(GvBAgencyMIDT.FooterRow.Cells(7).Text) + Val(row.Cells(7).Text)
                        GvBAgencyMIDT.FooterRow.Cells(8).Text = Val(GvBAgencyMIDT.FooterRow.Cells(8).Text) + Val(row.Cells(8).Text)
                        GvBAgencyMIDT.FooterRow.Cells(9).Text = Val(GvBAgencyMIDT.FooterRow.Cells(9).Text) + Val(row.Cells(9).Text)
                    Next

                Else

                    GvBAgencyMIDT.DataSource = Nothing
                    GvBAgencyMIDT.DataBind()
                End If

                If ds.Tables("BC_CONN") IsNot Nothing Then
                    GvConnectivity.DataSource = ds.Tables("BC_CONN")
                    GvConnectivity.DataBind()

                    GvConnectivity.FooterRow.Cells(3).Text = 0
                    For Each row As GridViewRow In GvConnectivity.Rows
                        GvConnectivity.FooterRow.Cells(3).Text = Val(GvConnectivity.FooterRow.Cells(3).Text) + Val(row.Cells(3).Text)
                    Next
                Else
                    GvConnectivity.DataSource = Nothing
                    GvConnectivity.DataBind()
                End If

                If ds.Tables("BC_EQP") IsNot Nothing Then
                    GvHardware.DataSource = ds.Tables("BC_EQP")
                    GvHardware.DataBind()
                    GvHardware.FooterRow.Cells(3).Text = 0
                    For Each row As GridViewRow In GvHardware.Rows
                        GvHardware.FooterRow.Cells(3).Text = Val(GvHardware.FooterRow.Cells(3).Text) + Val(row.Cells(3).Text)
                    Next
                Else
                    GvHardware.DataSource = Nothing
                    GvHardware.DataBind()
                End If

             
                Try
                    '##########################################################

                    If ds.Tables("BUSINESSCASE") IsNot Nothing Then

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
                            StrINC_TYPE_ID = ds.Tables("BUSINESSCASE").Rows(0)("INC_TYPE_ID").ToString

                            Dim ObjNode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + StrINC_TYPE_ID + "']")
                            If ObjNode IsNot Nothing Then

                                '###############################################
                                ' On the basis of Incentive type  show plan details  
                                '##########################################################


                                LblSoleAmt.Text = ds.Tables("BUSINESSCASE").Rows(0)("SOLE_AMOUNT").ToString
                                'LblBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("BONUS_AMOUNT").ToString
                                LblFixPayment.Text = ds.Tables("BUSINESSCASE").Rows(0)("FIXED_PAYMENT").ToString
                                LblUpfront.Text = ds.Tables("BUSINESSCASE").Rows(0)("UPFRONT_AMOUNT").ToString
                                LblFixUpfront.Text = ds.Tables("BUSINESSCASE").Rows(0)("FIXED_UPFRONT").ToString
                                LblSoleAmtBonus.Text = ds.Tables("BUSINESSCASE").Rows(0)("SOLE_AMOUNT").ToString


                                If ObjNode.Attributes("SOLE_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblSoleAmt.Visible = True
                                    pnlLabelSoleAmt.Visible = True
                                Else
                                    LblSoleAmt.Visible = False
                                    pnlLabelSoleAmt.Visible = False
                                End If

                                If ObjNode.Attributes("FIXED_UPFRONT").Value.Trim.ToUpper = "TRUE" Then
                                    LblFixUpfront.Visible = True
                                    pnlLblFixUpfront.Visible = True

                                Else
                                    LblFixUpfront.Visible = False
                                    pnlLblFixUpfront.Visible = False

                                End If
                                If ObjNode.Attributes("UPFRONT_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    LblUpfront.Visible = True
                                    pnlLblUpfront.Visible = True

                                Else
                                    LblUpfront.Visible = False
                                    pnlLblUpfront.Visible = False

                                End If
                                If ObjNode.Attributes("FIXED_PAYMENT").Value.Trim.ToUpper = "TRUE" Then
                                    pnlLblFixPayment.Visible = True
                                    LblFixPayment.Visible = True

                                Else
                                    pnlLblFixPayment.Visible = False
                                    LblFixPayment.Visible = False

                                End If
                                If ObjNode.Attributes("BONUS_AMOUNT").Value.Trim.ToUpper = "TRUE" Then
                                    pnlLblSoleAmtBonus.Visible = True
                                    LblSoleAmtBonus.Visible = True
                                Else
                                    pnlLblSoleAmtBonus.Visible = False
                                    LblSoleAmtBonus.Visible = False
                                End If

                                If ObjNode.Attributes("INC_SLAB_REQUIRED").Value.Trim.ToUpper = "TRUE" Then
                                    If ds.Tables("CASE") IsNot Nothing Then
                                        RepIncPlan.DataSource = ds.Tables("CASE")
                                        RepIncPlan.DataBind()
                                      
                                    Else
                                        RepIncPlan.DataSource = Nothing
                                        RepIncPlan.DataBind()
                                        'PnlLblCase.Visible = True
                                        'LblCase.Visible = True
                                        'LblCase.Text = ds.Tables("CASE").Rows(0)("INC_PLAN_NAME").ToString
                                    End If
                                Else
                                    RepIncPlan.DataSource = Nothing
                                    RepIncPlan.DataBind()
                                End If

                            End If

                            If ds.Tables("PLAN_DETAILS") Is Nothing Then
                                PnlLblCase.Visible = True
                                LblCase.Visible = True
                                If ds.Tables("CASE") IsNot Nothing Then
                                    LblCase.Text = ds.Tables("CASE").Rows(0)("INC_PLAN_NAME").ToString
                                End If
                            Else
                                PnlLblCase.Visible = False
                                LblCase.Visible = False
                            End If

                            '###############################################
                            ' On the basis of Incentive type  show plan details  
                            '##########################################################

                        End If

                    End If



                Catch ex As Exception
                End Try

            Else
                GvBGroupMIDT.DataSource = Nothing
                GvBGroupMIDT.DataBind()
                GvBAgencyMIDT.DataSource = Nothing
                GvBAgencyMIDT.DataBind()
                GvConnectivity.DataSource = Nothing
                GvConnectivity.DataBind()
                GvHardware.DataSource = Nothing
                GvHardware.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            '@ End of Code  for Details
       
        '@ Code For Remarks Tab
            Try
                BindRemarks()
            Catch ex As Exception

            End Try
            '@ End of Code For Remarks Tab
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub GvBAgencyMIDT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBAgencyMIDT.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblSNo As Label = CType(e.Row.FindControl("lblSNo"), Label)
                lblSNo.Text = (e.Row.RowIndex + 1).ToString
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
                ' e.Row.Cells(4).Text = "0"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(5).Text = "0"
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                '  e.Row.Cells(6).Text = "0"
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(7).Text = "0"
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(8).Text = "0"
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(9).Text = "0"
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GvHardware_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvHardware.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(2).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(3).Text = "0"
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                'e.Row.Cells(4).Text = "0"
                'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub RepIncPlan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepIncPlan.ItemDataBound
        Dim objOutputCriteriaXmlXml As New XmlDocument
        Dim dsCriteria As DataSet
        Dim objReadaerCriteria As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim ds As DataSet = dset
                Dim hdCaseId As HiddenField = CType(e.Item.FindControl("hdCaseId"), HiddenField)

                '  Dim lblIncType As Label = CType(e.Item.FindControl("lblType"), Label)

                ' Dim lblCase As Label = CType(e.Item.FindControl("lblCase"), Label)
                'lblCase.Text = "Case " + (e.Item.ItemIndex + 1).ToString

                Dim ChkListCriteria As CheckBoxList = CType(e.Item.FindControl("ChkListCriteria"), CheckBoxList)

                ' <INC_LISTNIDTFIELDS_TYPE_OUTPUT>
                ' <NIDTFIELDS NIDT_FIELDS_ID='' NIDT_FIELDS_NAME='' />
                ' <Errors Status=''>
                '     <Error Code='' Description='' />
                ' </Errors>
                '</INC_LISTNIDTFIELDS_TYPE_OUTPUT > 

                objOutputCriteriaXmlXml = objbzBusinessCase.List_NIDTFields
                If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    dsCriteria = New DataSet
                    objReadaerCriteria = New XmlNodeReader(objOutputCriteriaXmlXml)
                    dsCriteria.ReadXml(objReadaerCriteria)
                    ChkListCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                    ChkListCriteria.DataTextField = "NIDT_FIELDS_NAME"
                    ChkListCriteria.DataValueField = "NIDT_FIELDS_ID"
                    ChkListCriteria.DataBind()

                    '@ Code for selection for criteria
                    Dim dvNIDTFIELDS As DataView
                    If ds.Tables("NIDT_FIELDS_ID") IsNot Nothing Then
                        dvNIDTFIELDS = ds.Tables("NIDT_FIELDS_ID").DefaultView
                        dvNIDTFIELDS.RowFilter = "Case_Id='" + hdCaseId.Value + "'"
                        If dvNIDTFIELDS.Count > 0 Then
                            Dim objArrayList As New ArrayList(dvNIDTFIELDS.Item(0)(0).ToString().Split(","))
                            '  dvNIDTFIELDS.Item(0)(0).ToString()
                            For j As Integer = 0 To objArrayList.Count - 1
                                For i As Integer = 0 To ChkListCriteria.Items.Count - 1
                                    If (objArrayList(j).trim().ToString = ChkListCriteria.Items(i).Value.ToString().Trim()) Then
                                        ChkListCriteria.Items(i).Selected = True
                                    End If
                                Next
                            Next
                        End If
                    End If
                    '@ Code for selection for criteria

                End If


               

                '@ Code for Filling PLAN DETAILS
                Dim dvPlanDetails As DataView
                Dim GvIncPlan As GridView = CType(e.Item.FindControl("GvIncPlan"), GridView)
                If ds.Tables("PLAN_DETAILS") IsNot Nothing Then
                    dvPlanDetails = ds.Tables("PLAN_DETAILS").DefaultView
                    dvPlanDetails.RowFilter = "Case_Id='" + hdCaseId.Value + "'"

                    If dvPlanDetails.Count > 0 Then
                        GvIncPlan.DataSource = dvPlanDetails
                        GvIncPlan.DataBind()
                    Else
                        GvIncPlan.DataSource = Nothing
                        GvIncPlan.DataBind()
                    End If

                Else
                    GvIncPlan.DataSource = Nothing
                    GvIncPlan.DataBind()
                End If
                '@ End of Code for Filling PLAN DETAILS

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GvConnectivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvConnectivity.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(2).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                ' e.Row.Cells(3).Text = "0"
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                'e.Row.Cells(4).Text = "0"
                'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub BindRemarks()

        If Request.QueryString("Chain_Code") IsNot Nothing Then
            ' For Default Page Setting
            If Request.QueryString("BCaseID") IsNot Nothing Then

                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzBusinessCaseIncRem As New AAMS.bizIncetive.bzBusinessCase
                objInputXml.LoadXml("<INC_GETBUSINESSREMARK_INPUT><BC_ID></BC_ID></INC_GETBUSINESSREMARK_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BCaseID").ToString

                '  objOutputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_OUTPUT>  <PAGE PAGE_COUNT='0' TOTAL_ROWS='1' />  <Errors Status='FALSE'>  <Error Code='' Description=''/>   </Errors>  <REQUESTDESCRIPTION HD_RE_ID='211629' EmployeeName='Admin' ACTION_TAKEN='aa' DATETIME='21-Jul-09 15:50' />   </HD_GETREQUESTDESCRIPTION_OUTPUT>")

                objOutputXml = objbzBusinessCaseIncRem.GetRemarks(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    If ds.Tables("BUSINESSREMARK").Rows.Count <> 0 Then


                        '@For  Region Remarks
                        Dim dvRegionRem As DataView = ds.Tables("BUSINESSREMARK").DefaultView
                        dvRegionRem.RowFilter = "BC_REMARKS_TYPE='Region Remark'"

                     
                        If dvRegionRem.Count > 0 Then
                            gvRegionRem.DataSource = ds.Tables("BUSINESSREMARK")
                            gvRegionRem.DataBind()
                        Else
                            gvRegionRem.DataSource = Nothing
                            gvRegionRem.DataBind()
                        End If



                        '@For  Market Remarks
                        Dim dvMarketRem As DataView = ds.Tables("BUSINESSREMARK").DefaultView
                        dvMarketRem.RowFilter = "BC_REMARKS_TYPE='Market Remark'"

                        If dvMarketRem.Count > 0 Then
                            gvMarketRem.DataSource = ds.Tables("BUSINESSREMARK")
                            gvMarketRem.DataBind()
                        Else
                            gvMarketRem.DataSource = Nothing
                            gvMarketRem.DataBind()
                        End If
                    Else
                        gvRegionRem.DataSource = Nothing
                        gvRegionRem.DataBind()
                        gvMarketRem.DataSource = Nothing
                        gvMarketRem.DataBind()
                    End If
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
        End If

    End Sub

  


    Protected Sub GvIncPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvIncPlan.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).ColumnSpan = "2"
                e.Row.Cells(1).Visible = False
                e.Row.Cells(0).Text = "Range"
                e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            End If

            'If e.Row.RowType = DataControlRowType.Footer Then

            '    e.Row.Cells(2).Text = "Total"
            '    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            '    ' e.Row.Cells(3).Text = "0"
            '    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            '    'e.Row.Cells(4).Text = "0"
            '    'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            'End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GvApprovers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvApprovers.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(1).Text.Trim.Length > 0 Then
                    Dim lblLavel As Label = CType(e.Row.FindControl("lblLavel"), Label)
                    lblLavel.Text = "Level " + (e.Row.RowIndex + 1).ToString
                End If
               
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            scrollbarSetting()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub scrollbarSetting()
        Try
            'Scrollbar Setting Dynamically
            If GvHardware.Rows.Count > 10 Then
                PnlHardware.ScrollBars = ScrollBars.Vertical
                PnlHardware.Height = 300
            Else
                PnlHardware.ScrollBars = ScrollBars.None
                PnlHardware.Height = 0
            End If
            If GvConnectivity.Rows.Count > 10 Then
                PnlConnectivity.ScrollBars = ScrollBars.Vertical
                PnlConnectivity.Height = 300
            Else
                PnlConnectivity.ScrollBars = ScrollBars.None
                PnlConnectivity.Height = 0
            End If
            If GvBAgencyMIDT.Rows.Count > 10 Then
                PnlAgencyMIDT.ScrollBars = ScrollBars.Vertical
                PnlAgencyMIDT.Height = 300
            Else
                PnlAgencyMIDT.ScrollBars = ScrollBars.None
                PnlAgencyMIDT.Height = 0
            End If

            If GvBGroupMIDT.Rows.Count > 10 Then
                pnlGroupMIDT.ScrollBars = ScrollBars.Vertical
                pnlGroupMIDT.Height = 300
            Else
                pnlGroupMIDT.ScrollBars = ScrollBars.None
                pnlGroupMIDT.Height = 0
            End If



            If RepIncPlan.Items.Count > 2 Then
                pnlIncentivePlan.ScrollBars = ScrollBars.Vertical
                pnlIncentivePlan.Height = 300
            Else
                pnlIncentivePlan.ScrollBars = ScrollBars.None
                pnlIncentivePlan.Height = 0
            End If
           
           
            'End of Scrollbar Setting Dynamically
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvBGroupMIDT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBGroupMIDT.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class