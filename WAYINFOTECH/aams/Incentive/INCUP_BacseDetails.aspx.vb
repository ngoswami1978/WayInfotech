Partial Class Incentive_INCSR_BacseDetails
    Inherits System.Web.UI.Page
    'Inherits BasePage

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Public totalConn As Double = 0
    Public totalHardware As Double = 0
    Public totalHw As Double = 0
    Public dsBidtAgencyTotal As DataTable
    Public Delegate Sub lnkEdit_ClickDel(ByVal sender As Object, ByVal e As System.EventArgs)
    Enum Grid
        All = 1
        Connectivity = 2
        Hardware = 3
        BreakUpLast = 4
        Slab = 5
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try

            If Request.QueryString("RefreshAction") IsNot Nothing Then
                hdRefreshAction.Value = Request.QueryString("RefreshAction")
            End If

            lblInnerError.Text = ""


            lnkShowBidtGroup.Attributes.Add("onclick", "return ShoImageRotaion('" + imgAvl.ClientID + "')")
            chklstCriteria.Attributes.Add("onclick", "return makeSelection()")
            chkSLABQUALIFICATION.Attributes.Add("onclick", "return makeSelectionSLABQUALIFICATION()")

            ChkMinimunCriteriaNew.Attributes.Add("onclick", "return CheckOrUnckeckItemFromMinimunCriteria('ChkMinimunCriteriaNew')")
            chkLstMSMore.Attributes.Add("onclick", "return CheckOrUnckeckItemFromMinimunCriteria('chkLstMSMore')")
            chkLstSQMore.Attributes.Add("onclick", "return CheckOrUnckeckItemFromQlaificationModal()")

            chkLstGvShowMIDT.Attributes.Add("onclick", "return makeSelection1()")

            'Changed code in December
            btnHistory.Attributes.Add("onclick", "return history()")
            rdbIncentiveType.Attributes.Add("onclick", "return incentiveType();")
            rdbUpfrontTypeName.Attributes.Add("onclick", "return UpfrontTypeName();")
            chkPlb.Attributes.Add("onclick", "bonusCheck()")
            rdbPlbTypeName.Attributes.Add("onclick", "return plb_paymenttypeFixed_Bonus();")
            rdbPaymentType.Attributes.Add("onclick", "return PaymentType_Rate_Fixed();")
            trSlabPlb.Attributes.Add("class", "displayNone")
            If chkPlb.Checked Then
                trPlbTypeName.Attributes.Add("class", "displayBlock")
                If rdbPlbTypeName.SelectedValue = "1" Then
                    trPlbAmount.Attributes.Add("class", "displayBlock")
                    trSlabPlb.Attributes.Add("class", "displayNone")
                Else
                    trPlbAmount.Attributes.Add("class", "displayNone")
                    trSlabPlb.Attributes.Add("class", "displayBlock")

                End If
            Else
                trPlbAmount.Attributes.Add("class", "displayNone")
                trPlbTypeName.Attributes.Add("class", "displayNone")
            End If





            rdbIncentiveTypeOld.Attributes.Add("onclick", "return incentiveTypeOld();")
            chkPlbOld.Attributes.Add("onclick", "bonusCheckOld()")
            rdbPlbTypeNameOld.Attributes.Add("onclick", "return plb_paymenttypeFixed_BonusOld();")
            rdbPaymentTypeOld.Attributes.Add("onclick", "return PaymentType_Rate_FixedOld();")
            trSlabPlbOld.Attributes.Add("class", "displayNone")
            If chkPlb.Checked Then
                trPlbTypeName.Attributes.Add("class", "displayBlock")
                If rdbPlbTypeName.SelectedValue = "1" Then
                    trPlbAmount.Attributes.Add("class", "displayBlock")
                    trSlabPlb.Attributes.Add("class", "displayNone")
                Else
                    trPlbAmount.Attributes.Add("class", "displayNone")
                    trSlabPlb.Attributes.Add("class", "displayBlock")

                End If
            Else
                trPlbAmount.Attributes.Add("class", "displayNone")
                trPlbTypeName.Attributes.Add("class", "displayNone")
            End If

            If rdbIncentiveType.SelectedValue = "2" Then
                'trUpfrontTypeName
                'trUpfrontAmount
                trUpfrontTypeName.Attributes.Add("class", "displayNone")
                trUpfrontAmount.Attributes.Add("class", "displayNone")
                TRMinimumSegmentCriteriaNew.Attributes.Add("class", "redborder displayBlock")
                TblMinimumSegmentCriteriaNew.Attributes.Add("class", "redborder displayBlock")
            Else
                trUpfrontTypeName.Attributes.Add("class", "displayBlock")
                trUpfrontAmount.Attributes.Add("class", "displayBlock")

                TRMinimumSegmentCriteriaNew.Attributes.Add("class", "displayNone")
                TblMinimumSegmentCriteriaNew.Attributes.Add("class", "displayNone")

            End If


            'End of Changed code in December



            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                lblError.Text = "Session is expired."
                lblInnerError.Text = "Session is expired."
                Exit Sub
            End If
            '   btnNewConnectivity.Attributes.Add("onclick", "return validateConnectivity();")
            '  btnAddHardware.Attributes.Add("onclick", "return validateHardware();")

            'btnAddSlabs.Attributes.Add("onclick", "return validateSlabs();")
            btnAddSlabN.Attributes.Add("onclick", "return validateSlabs();")
            btnPlbSlabN.Attributes.Add("onclick", "return validatePLBSlabs();")

            btnAddPlan.Attributes.Add("onclick", "return validatePlan();")


            '  lnkShowBidtGroup.Attributes.Add("className", "displayNone")
            '  btnSaveRemarks.Attributes.Add("onclick", "return validateRemarks();")
            ' drpPlanType.Attributes.Add("onchange", "return validateIncentiveType();")

            'drpPlanTypeOld.Attributes.Add("onchange", "return validateIncentiveTypeOld();")

            btnSave.Attributes.Add("onclick", "return validateBusinessCase();")
            txtIncRateN.Attributes.Add("onblur", "return calculateFixedIncentive();")
            txtIncExpectedSegN.Attributes.Add("onblur", "return calculateFixedIncentive();")

            txtFixIncentive.Attributes.Add("onblur", "return CalculateFixIncentiveICIT();")
            txtFixIncMonth.Attributes.Add("onblur", "return CalculateFixIncentiveICIT();")
            txtFixIncMinMonthSeg.Attributes.Add("onblur", "return CalculateFixIncCPS();")
            txtMinMonthSegNew.Attributes.Add("onblur", "return CalculateMinMonthSegNew();")

            ' txtConversionPer.Attributes.Add("onblur", "return calculateSegmentLessICITvalue();")

            '
            'btnUp.Attributes.Add("onclick", "return MoreOptions();")    
            ' updtIncentivePlan.RenderMode = UpdatePanelRenderMode.Block
            lnkCriteriaSelectionN.Attributes.Add("onclick", "return ShowModalPopupExtenderForTrainRoute('" + "78" + "')")
            ' lnkSLABQUALIFICATION.Attributes.Add("onclick", "return ShowModalPopupExtenderForSLABQUALIFICATION('" + "80" + "')")

            ' lnkPreview.Attributes.Add("onclick", "return showModalPopupExtenderPreview()")
            ' btnUp.Attributes.Add("onclick", "return ShowHideBIDT();")

            '  btnDwon.Attributes.Add("onclick", "return MoreOptions();")

            LnkSlabQualMore.Attributes.Add("onclick", "return ShowModPopExtForSQ('" + "810" + "')")
            LnkMinSeg.Attributes.Add("onclick", "return ShowModPopExtForMS('" + "820" + "')")

            hdTabID.Style.Add("display", "none")
            txtHiddenBidtFlag.Style.Add("display", "none")

            Page.MaintainScrollPositionOnPostBack = True
            If Not Page.IsPostBack Then
                Session("slabBackup") = Nothing
                Session("INCENTIVEPLAN") = Nothing
                Session("conData") = Nothing
                Session("HardwareDataList") = Nothing
                Session("TempSlabsXml") = Nothing
                Session("TempSlabsXml") = Nothing
                imgBIDT_Panel.Style.Add("display", "none")

                ' BindMinimumSegmentCriteria(ChkMinimunCriteriaNew)

                ' BindSlab_Qualification()

                If Request.QueryString("TabID") IsNot Nothing Then
                    hdTabID.Text = Request.QueryString("TabID")
                Else
                    hdTabID.Text = "0"
                End If
                txtHiddenBidtFlag.Text = "0"


                If Request.QueryString("MSG") IsNot Nothing Then
                    If Request.QueryString("MSG") = "U" Then
                        lblError.Text = "Record Updated successfully"
                    Else
                        lblError.Text = "Record Added successfully"
                    End If
                End If


                If Request.QueryString("BCaseID") IsNot Nothing Then
                    hdBcID.Value = Request.QueryString("BCaseID").Trim()
                End If

                Bindata()
                BindAllDropDowns()
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    hdEnChainCode.Value = Request.QueryString("Chain_Code")
                    hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                    ' For Default Page Setting
                    ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                    ' For Default Page Setting
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        LoadData(hdChainCode.Value, Request.QueryString("BCaseID"))
                        'GetAllRemrks(Request.QueryString("BCaseID").Trim())
                        hdBcID.Value = Request.QueryString("BCaseID").Trim()

                    Else
                        LoadData(hdChainCode.Value, "")
                    End If

                    '*****Binding Approvars************
                    ' bindApprovars(hdChainCode.Value)
                    '*****Binding Approvars************

                    ''Code for Binding BIDT data
                    'GetBidtAgencyData()
                    'GetBidtGroupData()
                    ''End of Code for Binding BIDT data

                End If
                bindChkListForShowModal()
                EnableDisableCase()
                bindSQForShowModal()
                bindMSForShowModal()
            End If


            'If hdBcID.Value.Trim() = "" Then
            '    btnSendforApproval.Enabled = False
            'Else
            '    btnSendforApproval.Enabled = True
            'End If
            scrollbarSetting()

            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") = "N" Then

                    btnSendforApproval.Enabled = False
                    BtnApproved.Enabled = False
                    BtnFinnallyApproved.Enabled = False
                    BtnReject.Enabled = False
                    btnHistory.Enabled = False

                    btnSendforApproval.ImageUrl = "~/Images/Btn_SendForApprovel_temp.jpg"
                    BtnApproved.ImageUrl = "~/Images/Btn_Approved_temp.jpg"
                    BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved_temp.jpg"
                    BtnReject.ImageUrl = "~/Images/btn_Reject_temp.jpg"
                    btnHistory.ImageUrl = "~/Images/btn_History_temp.jpg"


                Else

                End If
            End If
            If Request.QueryString("Action") IsNot Nothing Then
                hdAction.Value = Request.QueryString("Action")
            End If

            If rdbPaymentType.SelectedValue = "1" Then
                spnAdjAmount.Text = "Rate"
            Else
                spnAdjAmount.Text = "Amount"
            End If

            If Session("BC_MIDT_BRK") IsNot Nothing Then
                Dim objXmlDocument As New XmlDocument
                objXmlDocument.LoadXml(Session("BC_MIDT_BRK"))
                Session("BC_MIDT_BRK") = CalculateBreakUp(objXmlDocument).OuterXml
                hdCalculateBreakUp.Value = Session("BC_MIDT_BRK")
            End If

            FillXml()



            If Not Page.IsPostBack Then
                If Request.QueryString("BindGrid") & "" = "T" Then
                    BindLocation()
                End If
            End If
            HdConversionPer.Value = txtConversionPer.Text

            If Not IsPostBack Then
                checksecurity()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub BindSlab_Qualification()
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim dsCriteria As DataSet
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Try


            objOutputXml = objbzBusinessCase.List_NIDTFields()

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)
                chkSLABQUALIFICATION.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkSLABQUALIFICATION.DataTextField = "NIDT_FIELDS_NAME"
                chkSLABQUALIFICATION.DataValueField = "NIDT_FIELDS_ID"
                chkSLABQUALIFICATION.DataBind()
            Else
                chkSLABQUALIFICATION.Items.Clear()
                chkSLABQUALIFICATION.DataSource = Nothing
                chkSLABQUALIFICATION.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub FillXml()
        Try


            Dim objXmlDocument As New XmlDocument
            Dim objXmlDoc As New XmlDocument
            Dim strBC_ONLINE_CATG_ID As String = ""
            Dim strTEMP_SLAB_ID As String = ""
            Dim strCONN_COUNT As String = ""
            Dim dblGrandTotal As Decimal = 0
            Dim convPercentage As Decimal = 0
            Dim conversionPercentaeByuser As Double


            '@ Previous Code Coomented By Abhishek on 27th jan 2011

            If Val(txtConversionPer.Text.Trim()) <> 0 Then
                convPercentage = (Val(hdTotalMIDT.Value.ToString()) * (Val(txtConversionPer.Text.Trim()) / 100)) * 0.9

            Else
                convPercentage = 0
            End If
            '@ Previous Code Coomented By Abhishek on 27th jan 2011


            '@ New Code Added By Abhishek on 27th jan 2011
            'If txtConversionPer.Text.Trim().Length = 0 Then
            '    conversionPercentaeByuser = 100
            'ElseIf Val(txtConversionPer.Text.Trim()) = 0 Then
            '    conversionPercentaeByuser = 100
            'Else
            '    conversionPercentaeByuser = Val(txtConversionPer.Text)
            'End If
            'convPercentage = (Val(hdTotalMIDT.Value.ToString()) * (Val(conversionPercentaeByuser) / 100)) * 0.9
            '@ New Code Added By Abhishek on 27th jan 2011


            If Session("CONNECTIVITY") IsNot Nothing Then
                objXmlDocument.LoadXml(Session("CONNECTIVITY"))
                If grdConnectivityN.Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In grdConnectivityN.Rows
                        strBC_ONLINE_CATG_ID = Val(CType(gvRow.FindControl("hdOnlineCatID"), HiddenField).Value)
                        For Each objNode As XmlNode In objXmlDocument.DocumentElement.SelectNodes("BC_CONN[@BC_ONLINE_CATG_ID='" + strBC_ONLINE_CATG_ID + "']")
                            If Val(CType(gvRow.FindControl("txtConnNo"), TextBox).Text).ToString = "0" Then
                                strCONN_COUNT = Request.Form(CType(gvRow.FindControl("txtConnNo"), TextBox).UniqueID)
                            Else
                                strCONN_COUNT = Val(CType(gvRow.FindControl("txtConnNo"), TextBox).Text).ToString
                            End If
                            objNode.Attributes("CONN_COUNT").Value = strCONN_COUNT
                            objNode.Attributes("TOTAL").Value = Val(objNode.Attributes("BC_ONLINE_CATG_COST").Value) * Val(strCONN_COUNT)
                        Next
                    Next
                    Session("CONNECTIVITY") = objXmlDocument.OuterXml
                    BindGrid(Grid.Connectivity)
                End If

            End If

            If Session("HARDWARE") IsNot Nothing Then
                objXmlDocument.LoadXml(Session("HARDWARE"))
                If grdvHardwareN.Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In grdvHardwareN.Rows
                        '<BC_EQP BC_EQP_CATG_ID="43" BC_EQP_CATG_TYPE="abhitestone" BC_EQP_CATG_COST="" PRODUCT_COUNT="" TOTAL="0" />
                        strBC_ONLINE_CATG_ID = Val(CType(gvRow.FindControl("hdHwIDN"), HiddenField).Value)
                        For Each objNode As XmlNode In objXmlDocument.DocumentElement.SelectNodes("BC_EQP[@BC_EQP_CATG_ID='" + strBC_ONLINE_CATG_ID + "']")
                            If Val(CType(gvRow.FindControl("txtHwNoN"), TextBox).Text).ToString = "0" Then
                                strCONN_COUNT = Request.Form(CType(gvRow.FindControl("txtHwNoN"), TextBox).UniqueID)
                            Else
                                strCONN_COUNT = Val(CType(gvRow.FindControl("txtHwNoN"), TextBox).Text).ToString
                            End If
                            objNode.Attributes("PRODUCT_COUNT").Value = strCONN_COUNT
                            objNode.Attributes("TOTAL").Value = Val(objNode.Attributes("BC_EQP_CATG_COST").Value) * Val(strCONN_COUNT)
                        Next
                    Next
                    Session("HARDWARE") = objXmlDocument.OuterXml
                    BindGrid(Grid.Hardware)
                End If

            End If


            If Session("BC_MIDT_BRK") IsNot Nothing Then
                objXmlDocument.LoadXml(Session("BC_MIDT_BRK"))
                If grdvBreakUpLast.Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In grdvBreakUpLast.Rows
                        '  <BC_MIDT_BRK BR_ID="" TYPE="" TOTAL="" PER="" LESS="" RATE="" AMT="" />
                        strBC_ONLINE_CATG_ID = Val(CType(gvRow.FindControl("hdbreakID"), HiddenField).Value)
                        For Each objNode As XmlNode In objXmlDocument.DocumentElement.SelectNodes("BC_MIDT_BRK[@BR_ID='" + strBC_ONLINE_CATG_ID + "']")

                            Dim strPercentLess As String = ""
                            strPercentLess = objNode.Attributes("LESS").Value 'Val(CType(gvRow.FindControl("lbltenPerLess"), Label).Text)

                            Dim strBreakupRate As String = ""
                            strBreakupRate = Val(CType(gvRow.FindControl("txtBreakupRate"), TextBox).Text)

                            objNode.Attributes("RATE").Value = strBreakupRate
                            If objNode.Attributes("TYPE").Value = "DOM" Then
                                objNode.Attributes("AMT").Value = dblGrandTotal.ToString
                            Else
                                objNode.Attributes("AMT").Value = Val(strBreakupRate) * Val(strPercentLess)
                                dblGrandTotal += Val(strBreakupRate) * Val(strPercentLess)
                            End If


                        Next
                    Next
                    objXmlDocument.DocumentElement.SelectSingleNode("GRANDTOTAL").Attributes("TOTAL").Value = dblGrandTotal.ToString
                    Session("BC_MIDT_BRK") = objXmlDocument.OuterXml
                    BindGrid(Grid.BreakUpLast)
                End If

            End If

            If Session("TempSlabsXml") IsNot Nothing Then
                objXmlDoc.LoadXml(Session("TempSlabsXml"))
            ElseIf Session("slabBackup") IsNot Nothing Then
                objXmlDoc.LoadXml(Session("slabBackup"))
            End If


            If objXmlDoc.OuterXml <> "" Then
                If grdvSlabN.Rows.Count > 0 Then
                    For Each gvRow As GridViewRow In grdvSlabN.Rows
                        '  <PLAN_DETAILS TEMP_SLAB_ID="0" SLABS_START="1" SLABS_END="2" SLABS_RATE="3" /> 
                        strTEMP_SLAB_ID = Val(CType(gvRow.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value)
                        For Each objNode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("PLAN_DETAILS[@TEMP_SLAB_ID='" + strTEMP_SLAB_ID + "']")
                            objNode.Attributes("SLABS_START").Value = gvRow.Cells(0).Text
                            objNode.Attributes("SLABS_END").Value = gvRow.Cells(1).Text
                            objNode.Attributes("SLABS_RATE").Value = CType(gvRow.FindControl("txtAmount"), TextBox).Text
                        Next
                    Next
                    Session("slabBackup") = objXmlDoc.OuterXml
                    Session("TempSlabsXml") = objXmlDoc.OuterXml
                    BindGrid(Grid.Slab)
                End If

            End If

            CalculateFormula(convPercentage)


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindGrid(ByVal intGridCode As Int16)

        Try
            'Connectivity Section 
            Dim objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds, dsHardware, dsBreakUpLast, dsSlab As New DataSet()
            objOutputXml = New XmlDocument
            If Grid.Connectivity = intGridCode Or Grid.All = intGridCode Then
                If Session("CONNECTIVITY") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("CONNECTIVITY"))
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    grdConnectivityN.DataSource = ds.Tables("BC_CONN")
                    grdConnectivityN.DataBind()
                End If
            End If

            If Grid.Hardware = intGridCode Or Grid.All = intGridCode Then
                If Session("HARDWARE") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("HARDWARE"))
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    dsHardware.ReadXml(objXmlReader)
                    grdvHardwareN.DataSource = dsHardware.Tables("BC_EQP")
                    grdvHardwareN.DataBind()
                End If
            End If

            If Grid.BreakUpLast = intGridCode Or Grid.All = intGridCode Then
                If Session("BC_MIDT_BRK") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("BC_MIDT_BRK"))
                    Try
                        ' objOutputXml.Save("c:\BC_MIDT_BRK.xml")
                    Catch ex As Exception
                    End Try

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    dsBreakUpLast.ReadXml(objXmlReader)
                    If (objOutputXml.DocumentElement.SelectNodes("BC_MIDT_BRK[@TYPE='']").Count <> objOutputXml.DocumentElement.SelectNodes("BC_MIDT_BRK").Count) Then
                        grdvBreakUpLast.DataSource = dsBreakUpLast.Tables("BC_MIDT_BRK")
                        grdvBreakUpLast.DataBind()
                        grdvBreakUpLast.FooterRow.Cells(5).Text = objOutputXml.DocumentElement.SelectSingleNode("GRANDTOTAL").Attributes("TOTAL").Value()

                    Else
                        grdvBreakUpLast.DataSource = Nothing
                        grdvBreakUpLast.DataBind()
                    End If


                End If
            End If

            If Grid.Slab = intGridCode Then
                If Session("slabBackup") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("slabBackup"))
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    dsSlab.ReadXml(objXmlReader)
                    grdvSlabN.DataSource = dsSlab.Tables("PLAN_DETAILS").DefaultView
                    grdvSlabN.DataBind()
                End If
            End If

            'Hardware Section

            'Dim objHardward As New AAMS.bizIncetive.bzEquipment
            'objOutputXml = New XmlDocument
            'objOutputXml = objHardward.INC_HARDWARE_LIST()
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    Session("HardwareDataList") = objOutputXml.OuterXml
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsHardware.ReadXml(objXmlReader)
            '    grdvHardwareN.DataSource = dsHardware.Tables("HARDWARE").DefaultView
            '    'drpHardware.DataTextField = "BC_EQP_CATG_TYPE"
            '    'drpHardware.DataValueField = "BC_EQP_CATG_ID"
            '    grdvHardwareN.DataBind()
            'End If
            ''    drpHardware.Items.Insert(0, "--Select One--")

            ''End of Hardware Section


            ''Incentive Type
            'Dim objIncType As New AAMS.bizIncetive.bzBusinessCase
            'Dim dsIncType As New DataSet
            'objOutputXml = New XmlDocument
            'objOutputXml = objIncType.List_IncentiveType()
            '' objOutputXml.LoadXml("C:\Admin\List_IncentiveType.xml")

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsIncType.ReadXml(objXmlReader)
            '    hdIncentiveType.Value = objOutputXml.OuterXml
            '    drpPlanType.DataSource = dsIncType.Tables("INC_TYPE").DefaultView
            '    drpPlanType.DataTextField = "INC_TYPE_NAME"
            '    drpPlanType.DataValueField = "INC_TYPE_ID"
            '    drpPlanType.DataBind()


            '    drpPlanTypeOld.DataSource = dsIncType.Tables("INC_TYPE").DefaultView
            '    drpPlanTypeOld.DataTextField = "INC_TYPE_NAME"
            '    drpPlanTypeOld.DataValueField = "INC_TYPE_ID"
            '    drpPlanTypeOld.DataBind()

            'End If
            'drpPlanType.Items.Insert(0, "--Select One--")
            'drpPlanTypeOld.Items.Insert(0, "--Select One--")

            ''End of Incentive Type




            ''Binding Cases used in Modal Popup window
            'Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            'Dim dsCriteria As DataSet
            'objOutputXml = objbzBusinessCase.List_NIDTFields()
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    dsCriteria = New DataSet
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsCriteria.ReadXml(objXmlReader)
            '    chkLstGvShowMIDT.DataSource = dsCriteria.Tables("NIDTFIELDS")
            '    chkLstGvShowMIDT.DataTextField = "NIDT_FIELDS_NAME"
            '    chkLstGvShowMIDT.DataValueField = "NIDT_FIELDS_ID"
            '    chkLstGvShowMIDT.DataBind()
            'End If

            ''End of Binding Cases  used in Modal Popup window




            ''Binding Billing Cycle

            'objOutputXml = objbzBusinessCase.List_PaymentCycle()
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    dsCriteria = New DataSet
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsCriteria.ReadXml(objXmlReader)
            '    drpBillingCycle.DataSource = dsCriteria.Tables("PAYMENTCYCLE").DefaultView
            '    drpBillingCycle.DataTextField = "PAYMENT_CYCLE_NAME"
            '    drpBillingCycle.DataValueField = "PAYMENT_CYCLE_ID"
            '    drpBillingCycle.DataBind()
            'End If
            ''End of Binding Billing Cycle





        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Function resetTempID(ByVal strXml As String) As String
        Dim objXml As New XmlDocument
        Dim objXml1 As New XmlDocument
        Dim TEMP_SLAB_ID_MAIN As Integer = 0
        Dim TEMP_SLAB_ID As Integer = 0
        Dim objNode, objCloneNode As XmlNode
        Dim intArSlab(0) As Integer
        Try

            objXml.LoadXml(strXml)
            objXml1.LoadXml("<CASE><PLAN_DETAILS TEMP_SLAB_ID='' SLABS_START='' SLABS_END='' SLABS_RATE=''/></CASE>")
            objNode = objXml1.DocumentElement.SelectSingleNode("PLAN_DETAILS")
            objCloneNode = objNode.CloneNode(True)

            For Each xNode As XmlNode In objXml.SelectNodes("CASE")
                xNode.Attributes("TEMP_SLAB_ID_MAIN").Value = TEMP_SLAB_ID_MAIN.ToString()
                intArSlab = New Integer(xNode.SelectNodes("PLAN_DETAILS").Count - 1) {}

                For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                    intArSlab(TEMP_SLAB_ID) = Val(xChildNd.Attributes("SLABS_START").InnerText)
                    xChildNd.Attributes("TEMP_SLAB_ID").Value = TEMP_SLAB_ID.ToString()
                    TEMP_SLAB_ID += 1
                Next
                Array.Sort(intArSlab)
                TEMP_SLAB_ID = 0
                For x As Integer = 0 To (intArSlab.Length - 1)
                    For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                        If Val(xChildNd.Attributes("SLABS_START").InnerText) = Val(intArSlab(x)) Then
                            xChildNd.Attributes("TEMP_SLAB_ID").Value = TEMP_SLAB_ID.ToString()
                            objXml1.DocumentElement.AppendChild(objXml1.ImportNode(xChildNd, True))
                            TEMP_SLAB_ID += 1
                        End If
                    Next
                Next x

                objXml1.DocumentElement.RemoveChild(objNode)

                For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                    xNode.RemoveChild(xChildNd)
                Next

                For Each xChildNd As XmlNode In objXml1.DocumentElement.SelectNodes("PLAN_DETAILS")

                    objXml.DocumentElement.AppendChild(objXml.ImportNode(xChildNd, True))

                Next



                TEMP_SLAB_ID_MAIN += 1
                TEMP_SLAB_ID = 0
            Next



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Return objXml.OuterXml
    End Function

    Private Sub resetTempID()
        Try

            If Session("INCENTIVEPLAN") IsNot Nothing Then
                Dim TEMP_SLAB_ID_MAIN As Integer = 0
                Dim TEMP_SLAB_ID As Integer = 0
                Dim objX As New XmlDocument
                Dim objXml1 As New XmlDocument
                Dim objNode, objCloneNode As XmlNode
                Dim intArSlab() As Integer






                objX.LoadXml(Session("INCENTIVEPLAN"))
                For Each xNode As XmlNode In objX.DocumentElement.SelectNodes("CASE")
                    objXml1.LoadXml("<CASE><PLAN_DETAILS TEMP_SLAB_ID='' SLABS_START='' SLABS_END='' SLABS_RATE=''/></CASE>")
                    objNode = objXml1.DocumentElement.SelectSingleNode("PLAN_DETAILS")
                    objCloneNode = objNode.CloneNode(True)

                    xNode.Attributes("TEMP_SLAB_ID_MAIN").Value = TEMP_SLAB_ID_MAIN.ToString()
                    intArSlab = New Integer(xNode.SelectNodes("PLAN_DETAILS").Count - 1) {}
                    For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                        intArSlab(TEMP_SLAB_ID) = Val(xChildNd.Attributes("SLABS_START").InnerText)
                        xChildNd.Attributes("TEMP_SLAB_ID").Value = TEMP_SLAB_ID.ToString()
                        TEMP_SLAB_ID += 1
                    Next
                    Array.Sort(intArSlab)
                    TEMP_SLAB_ID = 0
                    For x As Integer = 0 To (intArSlab.Length - 1)
                        For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                            If Val(xChildNd.Attributes("SLABS_START").InnerText) = Val(intArSlab(x)) Then
                                xChildNd.Attributes("TEMP_SLAB_ID").Value = TEMP_SLAB_ID.ToString()
                                objXml1.DocumentElement.AppendChild(objXml1.ImportNode(xChildNd, True))
                                TEMP_SLAB_ID += 1
                            End If
                        Next
                    Next x

                    If objXml1.DocumentElement.SelectSingleNode("PLAN_DETAILS[@SLABS_START='']") IsNot Nothing Then
                        objXml1.DocumentElement.RemoveChild(objNode)
                    End If

                    For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                        xNode.RemoveChild(xChildNd)
                    Next

                    For Each xChildNd As XmlNode In objXml1.DocumentElement.SelectNodes("PLAN_DETAILS")

                        xNode.AppendChild(objX.ImportNode(xChildNd, True))

                    Next
                    TEMP_SLAB_ID_MAIN += 1
                    TEMP_SLAB_ID = 0
                Next
                Session("INCENTIVEPLAN") = objX.OuterXml
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub scrollbarSetting()
        Try
            'If Request.QueryString("Action") IsNot Nothing Then
            '    If Request.QueryString("Action") <> "U" Then
            '        If grdvIncentivePlan.Rows.Count > 0 Then
            '            rdbPlbTypeName.Enabled = False
            '        Else
            '            rdbPlbTypeName.Enabled = True
            '        End If
            '    Else
            '        rdbPlbTypeName.Enabled = False
            '    End If
            'End If

            'If grdvIncentivePlan.Rows.Count > 0 Then
            '    rdbPlbTypeName.Enabled = False
            'Else
            '    rdbPlbTypeName.Enabled = True
            'End If

            'drpPlanTypeOld.Enabled = False

            'End of Scrollbar Setting Dynamically
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub BindAllDropDowns()
        Try
            'Connectivity Section 
            Dim objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds, dsHardware As New DataSet()
            'Dim objOnlineConn As New AAMS.bizIncetive.bzConnectivity
            'objOutputXml = New XmlDocument
            'objOutputXml = objOnlineConn.INC_CONNECTIVITY_LIST()
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    Session("conData") = objOutputXml.OuterXml
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            '    grdConnectivityN.DataSource = ds.Tables("CONNECTIVITY")
            '    grdConnectivityN.DataBind()
            '    'drpConnectivity.DataSource = ds.Tables("CONNECTIVITY")
            '    'drpConnectivity.DataTextField = "BC_ONLINE_CATG_NAME"
            '    'drpConnectivity.DataValueField = "BC_ONLINE_CATG_ID"
            '    'drpConnectivity.DataBind()
            'End If
            '' drpConnectivity.Items.Insert(0, "--Select One--")
            ''End of Connectivity Section 

            ''Hardware Section

            'Dim objHardward As New AAMS.bizIncetive.bzEquipment
            'objOutputXml = New XmlDocument
            'objOutputXml = objHardward.INC_HARDWARE_LIST()
            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    Session("HardwareDataList") = objOutputXml.OuterXml
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsHardware.ReadXml(objXmlReader)
            '    grdvHardwareN.DataSource = dsHardware.Tables("HARDWARE").DefaultView
            '    'drpHardware.DataTextField = "BC_EQP_CATG_TYPE"
            '    'drpHardware.DataValueField = "BC_EQP_CATG_ID"
            '    grdvHardwareN.DataBind()
            'End If
            '    drpHardware.Items.Insert(0, "--Select One--")

            'End of Hardware Section


            'Incentive Type
            Dim objIncType As New AAMS.bizIncetive.bzBusinessCase
            Dim dsIncType As New DataSet
            'objOutputXml = New XmlDocument
            'objOutputXml = objIncType.List_IncentiveType()
            '' objOutputXml.LoadXml("C:\Admin\List_IncentiveType.xml")

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    dsIncType.ReadXml(objXmlReader)
            '    hdIncentiveType.Value = objOutputXml.OuterXml
            '    drpPlanType.DataSource = dsIncType.Tables("INC_TYPE").DefaultView
            '    drpPlanType.DataTextField = "INC_TYPE_NAME"
            '    drpPlanType.DataValueField = "INC_TYPE_ID"
            '    drpPlanType.DataBind()


            '    drpPlanTypeOld.DataSource = dsIncType.Tables("INC_TYPE").DefaultView
            '    drpPlanTypeOld.DataTextField = "INC_TYPE_NAME"
            '    drpPlanTypeOld.DataValueField = "INC_TYPE_ID"
            '    drpPlanTypeOld.DataBind()

            'End If
            'drpPlanType.Items.Insert(0, "--Select One--")
            'drpPlanTypeOld.Items.Insert(0, "--Select One--")

            'End of Incentive Type




            'Binding Cases used in Modal Popup window
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet
            objOutputXml = objbzBusinessCase.List_NIDTFields()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)
                chkLstGvShowMIDT.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkLstGvShowMIDT.DataTextField = "NIDT_FIELDS_NAME"
                chkLstGvShowMIDT.DataValueField = "NIDT_FIELDS_ID"
                chkLstGvShowMIDT.DataBind()
            End If

            'End of Binding Cases  used in Modal Popup window




            'Binding Billing Cycle

            objOutputXml = objbzBusinessCase.List_PaymentCycle()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)
                drpBillingCycle.DataSource = dsCriteria.Tables("PAYMENTCYCLE").DefaultView
                drpBillingCycle.DataTextField = "PAYMENT_CYCLE_NAME"
                drpBillingCycle.DataValueField = "PAYMENT_CYCLE_ID"
                drpBillingCycle.DataBind()
            End If
            'End of Binding Billing Cycle





        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


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

            'TabText.Add("Details")
            'TabText.Add("Business Case")
            TabText.Add("Business Case")
            TabText.Add("Details")
            TabText.Add("Location")
            TabText.Add("Region Remarks")
            TabText.Add("Market Remarks")
            TabText.Add("Approvers")
            theTabSubStrip.DataSource = TabText
            theTabSubStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        'Dim Button1 As Button
        'Button1 = e.Item.FindControl("Button1")
        'If e.Item.ItemIndex = 0 Then
        '    Button1.CssClass = "headingtab"
        'End If
        'Button1 = e.Item.FindControl("Button1")

        'Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',0);")


    End Sub

    Private Sub TablSelection()
        If hdTabID.Text.Trim() = "0" Then
            pnlDetails.Attributes.Add("display", "none")
            'pnlRemarks.Attributes.Add("display", "none")
            'pnlRemarks1.Attributes.Add("display", "none")
            'pnlApprovars.Attributes.Add("display", "none")
            pnlBusinessCase.Attributes.Add("display", "block")
        End If

        If hdTabID.Text.Trim() = "1" Then
            pnlDetails.Attributes.Add("display", "block")
            'pnlRemarks.Attributes.Add("display", "none")
            'pnlRemarks1.Attributes.Add("display", "none")
            'pnlApprovars.Attributes.Add("display", "none")
            pnlBusinessCase.Attributes.Add("display", "none")
        End If
        If hdTabID.Text.Trim() = "2" Then
            pnlDetails.Attributes.Add("display", "none")
            'pnlRemarks.Attributes.Add("display", "block")
            'pnlRemarks1.Attributes.Add("display", "none")
            'pnlApprovars.Attributes.Add("display", "none")
            pnlBusinessCase.Attributes.Add("display", "none")
        End If

        If hdTabID.Text.Trim() = "3" Then
            pnlDetails.Attributes.Add("display", "none")
            'pnlRemarks.Attributes.Add("display", "none")
            'pnlRemarks1.Attributes.Add("display", "block")
            'pnlApprovars.Attributes.Add("display", "none")
            pnlBusinessCase.Attributes.Add("display", "none")
        End If


        If hdTabID.Text.Trim() = "4" Then
            pnlDetails.Attributes.Add("display", "none")
            'pnlRemarks.Attributes.Add("display", "none")
            'pnlRemarks1.Attributes.Add("display", "none")
            'pnlApprovars.Attributes.Add("display", "block")
            pnlBusinessCase.Attributes.Add("display", "none")
        End If


    End Sub

    Protected Sub theTabSubStrip_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles theTabSubStrip.ItemCommand
        If e.CommandName = "Tab" And e.Item.ItemIndex = 2 Then
            lblError.Text = ""
            BindLocation()
        End If
    End Sub

    'Private Sub OldPlanTypeVisiblity()
    '    Try
    '        Dim objXml As New XmlDocument
    '        objXml.LoadXml(hdIncentiveType.Value)
    '        Dim objXNode As XmlNode = objXml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + drpPlanTypeOld.SelectedValue.Trim() + "']")


    '        'var slabR=orders[0].getAttribute("INC_SLAB_REQUIRED");
    '        '		          var soleAmount=orders[0].getAttribute("SOLE_AMOUNT");
    '        '		          var bonusAmount=orders[0].getAttribute("BONUS_AMOUNT");
    '        '		          var fixPayment=orders[0].getAttribute("FIXED_PAYMENT");
    '        '		          var upfrontAmount=orders[0].getAttribute("UPFRONT_AMOUNT");
    '        '		          var fixUpfront=orders[0].getAttribute("FIXED_UPFRONT");


    '        If (objXNode.Attributes("INC_SLAB_REQUIRED").Value.Trim() = "TRUE") Then

    '        ElseIf (objXNode.Attributes("").Value.Trim() = "TRUE") Then

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub theTabSubStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 0 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        If hdBcID.Value.Trim = "" Then
            If e.Item.ItemIndex > 2 Then
                Button2.Enabled = False
            Else
                Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',6);")
            End If
        Else
            Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',6);")
        End If



    End Sub
    Protected Sub GvBAgencyMIDT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBAgencyMIDT.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblSNo As Literal = CType(e.Row.FindControl("lblSNo"), Literal)
                lblSNo.Text = (e.Row.RowIndex + 1).ToString

                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
                e.Row.Cells(4).Text = "0"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).Text = "0"
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(6).Text = "0"
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(7).Text = "0"
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(8).Text = "0"
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(9).Text = "0"
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub GvHardware_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvHardware.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            totalHardware = 0
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            Try
                'hdUnitCost hdConCount
                Dim hdUnitCost As HiddenField = CType(e.Row.FindControl("hdUnitCost"), HiddenField)
                Dim hdConCount As HiddenField = CType(e.Row.FindControl("hdConCount"), HiddenField)
                totalHardware += Convert.ToDouble(e.Row.Cells(2).Text.Trim()) * Convert.ToDouble(e.Row.Cells(3).Text.Trim())
            Catch ex As Exception
                totalHardware += 0
            End Try
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "Total"
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).Text = totalHardware.ToString()
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If

    End Sub

    Protected Sub GvHardware_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvHardware.RowCommand


        Try
            'If e.CommandName = "EditX" Then


            '    '  BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' PRODUCT_COUNT='' TOTAL=''
            '    Dim dt As New DataTable
            '    dt.Columns.Add("BC_EQP_CATG_ID")
            '    dt.Columns.Add("BC_EQP_CATG_TYPE")
            '    dt.Columns.Add("BC_EQP_CATG_COST")
            '    dt.Columns.Add("PRODUCT_COUNT")
            '    dt.Columns.Add("TOTAL")


            '    Dim dr As DataRow
            '    For Each gv As GridViewRow In GvHardware.Rows
            '        dr = dt.NewRow
            '        Dim hdBC_EQP_CATG_ID As HiddenField = CType(gv.FindControl("hdBC_EQP_CATG_ID"), HiddenField)
            '        dr("BC_EQP_CATG_ID") = hdBC_EQP_CATG_ID.Value  'gv.Cells(0).Text
            '        dr("BC_EQP_CATG_TYPE") = gv.Cells(1).Text
            '        dr("BC_EQP_CATG_COST") = gv.Cells(2).Text
            '        dr("PRODUCT_COUNT") = gv.Cells(3).Text
            '        dr("TOTAL") = gv.Cells(4).Text
            '        dt.Rows.Add(dr)
            '    Next



            '    dv.RowFilter = "BC_EQP_CATG_ID='" + e.CommandArgument.ToString + "'"
            '    'drpHardware.SelectedValue = dv(0)(0).ToString
            '    'txtHWNo.Text = dv(0)(3).ToString
            '    'btnAddHardware.Text = "Update"
            '    'btnAddHardware.CommandArgument = e.CommandArgument.ToString
            '    'drpHardware.Enabled = False
            'End If
            If e.CommandName = "DelX" Then
                ' drpHardware.Enabled = True
                totalHardware = 0
                Dim dt As New DataTable
                dt.Columns.Add("BC_EQP_CATG_ID")
                dt.Columns.Add("BC_EQP_CATG_TYPE")
                dt.Columns.Add("BC_EQP_CATG_COST")
                dt.Columns.Add("PRODUCT_COUNT")
                dt.Columns.Add("TOTAL")
                Dim dr As DataRow
                For Each gv As GridViewRow In GvHardware.Rows
                    Dim hdBC_EQP_CATG_ID As HiddenField = CType(gv.FindControl("hdBC_EQP_CATG_ID"), HiddenField)
                    If e.CommandArgument.ToString <> hdBC_EQP_CATG_ID.Value.Trim() Then
                        dr = dt.NewRow
                        dr("BC_EQP_CATG_ID") = hdBC_EQP_CATG_ID.Value  'gv.Cells(0).Text
                        dr("BC_EQP_CATG_TYPE") = gv.Cells(1).Text
                        dr("BC_EQP_CATG_COST") = gv.Cells(2).Text
                        dr("PRODUCT_COUNT") = gv.Cells(3).Text
                        dr("TOTAL") = gv.Cells(4).Text
                        dt.Rows.Add(dr)
                    End If
                Next
                GvHardware.DataSource = dt
                GvHardware.DataBind()

            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "Code for Temprory Adding New Connectivity"
    'Protected Sub btnNewConnectivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewConnectivity.Click
    '    Try
    '        Dim objConX As New XmlDocument
    '        Dim objConDrpTemX As New XmlDocument
    '        totalConn = 0
    '        hdTabID.Text = "1"

    '        If Session("conData") IsNot Nothing Then
    '            objConDrpTemX.LoadXml(Session("conData").ToString())
    '        Else
    '            Return
    '        End If

    '        hdConIDCollection.Value = ""
    '        objConX.LoadXml("<CONNECTIVITY><BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT='' TOTAL='' /></CONNECTIVITY>")
    '        Dim objFinalNode As XmlNode
    '        objFinalNode = objConX.DocumentElement.SelectSingleNode("BC_CONN")
    '        objConX.DocumentElement.RemoveChild(objFinalNode)
    '        'If Session("TempXmlConnecitvityFinal") Is Nothing Then
    '        '    Session("TempXmlConnecitvityFinal") = "<CONNECTIVITY></CONNECTIVITY>"
    '        'End If

    '        ' objConX.LoadXml(Session("TempXmlConnecitvityFinal").ToString())

    '        Dim rowCounter As Integer = 0
    '        Dim objConnNode As XmlNode
    '        Dim objClone, objCloneX As XmlNode
    '        For rowCounter = 0 To GvConnectivity.Rows.Count - 1
    '            objClone = objFinalNode
    '            objCloneX = objClone.CloneNode(True)

    '            Dim hdOnlineCatID As HiddenField = CType(GvConnectivity.Rows(rowCounter).FindControl("hdBC_ONLINE_CATG_ID"), HiddenField)

    '            If hdConIDCollection.Value.Trim() = "" Then
    '                hdConIDCollection.Value = hdOnlineCatID.Value
    '            Else
    '                hdConIDCollection.Value = hdConIDCollection.Value + "," + hdOnlineCatID.Value
    '            End If

    '            If btnNewConnectivity.Text.ToUpper() = "UPDATE" Then
    '                If btnNewConnectivity.CommandArgument = hdOnlineCatID.Value.Trim() Then
    '                    btnNewConnectivity.CommandArgument = ""
    '                    'btnNewConnectivity.Text = "Add"
    '                    drpConnectivity.Enabled = True
    '                    Continue For
    '                End If
    '            End If
    '            objCloneX.Attributes("BC_ONLINE_CATG_ID").Value = hdOnlineCatID.Value  'GvConnectivity.Rows(rowCounter).Cells(0).Text.Trim()
    '            objCloneX.Attributes("BC_ONLINE_CATG_NAME").Value = GvConnectivity.Rows(rowCounter).Cells(1).Text.Trim()
    '            objCloneX.Attributes("BC_ONLINE_CATG_COST").Value = GvConnectivity.Rows(rowCounter).Cells(2).Text.Trim()
    '            objCloneX.Attributes("CONN_COUNT").Value = GvConnectivity.Rows(rowCounter).Cells(3).Text.Trim()
    '            objCloneX.Attributes("TOTAL").Value = Convert.ToDouble(GvConnectivity.Rows(rowCounter).Cells(2).Text.Trim()) * Convert.ToDouble(GvConnectivity.Rows(rowCounter).Cells(3).Text.Trim())
    '            objConX.DocumentElement.AppendChild(objCloneX)
    '            'objConX.DocumentElement.AppendChild(objConX.ImportNode(objCloneX, True))
    '            ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
    '            objCloneX = objClone.CloneNode(True)

    '        Next

    '        'Code For Duplicate Check
    '        If btnNewConnectivity.Text.ToUpper() = "UPDATE" Then
    '            btnNewConnectivity.Text = "Add"
    '        Else
    '            Dim objArrayList As New ArrayList(hdConIDCollection.Value.Split(","))
    '            For c As Integer = 0 To objArrayList.Count - 1
    '                ' For i As Integer = 0 To drpConnectivity.Items.Count - 1
    '                If objArrayList(c).ToString().Trim() = drpConnectivity.SelectedValue.Trim() Then
    '                    lblError.Text = "Duplicate Connectivity"
    '                    Return
    '                End If
    '                'Next
    '            Next
    '        End If
    '        'End of Code for Duplicate check



    '        objConnNode = objConDrpTemX.DocumentElement.SelectSingleNode("CONNECTIVITY[@BC_ONLINE_CATG_ID='" + drpConnectivity.SelectedValue.Trim() + "']")
    '        objClone = objFinalNode 'objConX.DocumentElement.SelectSingleNode("temp")
    '        objCloneX = objClone.CloneNode(True)
    '        'objConX.DocumentElement.RemoveChild(objClone)
    '        objCloneX.Attributes("BC_ONLINE_CATG_ID").Value = drpConnectivity.SelectedItem.Value
    '        objCloneX.Attributes("BC_ONLINE_CATG_NAME").Value = drpConnectivity.SelectedItem.Text.Trim()
    '        objCloneX.Attributes("BC_ONLINE_CATG_COST").Value = objConnNode.Attributes("BC_ONLINE_CATG_COST").Value
    '        objCloneX.Attributes("CONN_COUNT").Value = txtConnectivityNo.Text.Trim()
    '        objCloneX.Attributes("TOTAL").Value = Convert.ToDouble(txtConnectivityNo.Text.Trim()) * Convert.ToDouble(objConnNode.Attributes("BC_ONLINE_CATG_COST").Value.Trim())
    '        'objConX.DocumentElement.AppendChild(objCloneX)
    '        objConX.DocumentElement.AppendChild(objCloneX)
    '        ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
    '        objCloneX = objClone.CloneNode(True)

    '        If hdConIDCollection.Value.Trim() = "" Then
    '            hdConIDCollection.Value = drpConnectivity.SelectedItem.Value.Trim()
    '        Else
    '            hdConIDCollection.Value = hdConIDCollection.Value + "," + drpConnectivity.SelectedItem.Value.Trim()
    '        End If





    '        ' Session("TempXmlConnecitvityFinal") = objConX.OuterXml
    '        Dim dsConn As New DataSet
    '        Dim objRdr As XmlNodeReader
    '        objRdr = New XmlNodeReader(objConX)
    '        dsConn.ReadXml(objRdr)
    '        GvConnectivity.DataSource = dsConn.Tables("BC_CONN").DefaultView
    '        GvConnectivity.DataBind()


    '        'Session for Building Inpurt xml
    '        Session("CONNECTIVITY") = objConX.OuterXml
    '        drpConnectivity.SelectedIndex = 0
    '        txtConnectivityNo.Text = ""
    '    Catch ex As Exception
    '        lblError.Text = ex.Message


    '    End Try
    'End Sub
#End Region

    Protected Sub GvConnectivity_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvConnectivity.RowCreated

    End Sub

    Protected Sub GvConnectivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvConnectivity.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            totalConn = 0
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right

            Try
                'hdUnitCost hdConCount
                Dim hdUnitCost As HiddenField = CType(e.Row.FindControl("hdUnitCost"), HiddenField)
                Dim hdConCount As HiddenField = CType(e.Row.FindControl("hdConCount"), HiddenField)
                totalConn += Convert.ToDouble(hdUnitCost.Value.Trim()) * Convert.ToDouble(hdConCount.Value.Trim())
            Catch ex As Exception
                totalConn += 0
            End Try
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "Total"
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).Text = totalConn.ToString()
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Protected Sub GvConnectivity_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvConnectivity.RowCommand
        Try
            If e.CommandName = "EditX" Then


                Dim dt As New DataTable
                dt.Columns.Add("BC_ONLINE_CATG_ID")
                dt.Columns.Add("BC_ONLINE_CATG_NAME")
                dt.Columns.Add("BC_ONLINE_CATG_COST")
                dt.Columns.Add("CONN_COUNT")
                dt.Columns.Add("TOTAL")

                Dim dr As DataRow
                For Each gv As GridViewRow In GvConnectivity.Rows
                    dr = dt.NewRow
                    Dim hdBC_ONLINE_CATG_ID As HiddenField = CType(gv.FindControl("hdBC_ONLINE_CATG_ID"), HiddenField)
                    dr("BC_ONLINE_CATG_ID") = hdBC_ONLINE_CATG_ID.Value  'gv.Cells(0).Text
                    dr("BC_ONLINE_CATG_NAME") = gv.Cells(1).Text
                    dr("BC_ONLINE_CATG_COST") = gv.Cells(2).Text
                    dr("CONN_COUNT") = gv.Cells(3).Text
                    dr("TOTAL") = gv.Cells(4).Text
                    dt.Rows.Add(dr)
                Next

                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "BC_ONLINE_CATG_ID='" + e.CommandArgument.ToString + "'"
                'drpConnectivity.SelectedValue = dv(0)(0).ToString
                'txtConnectivityNo.Text = dv(0)(3).ToString
                'btnNewConnectivity.Text = "Update"
                'btnNewConnectivity.CommandArgument = e.CommandArgument.ToString
                'drpConnectivity.Enabled = False
            End If
            If e.CommandName = "DelX" Then
                totalConn = 0
                ' drpConnectivity.Enabled = True
                Dim dt As New DataTable
                dt.Columns.Add("BC_ONLINE_CATG_ID")
                dt.Columns.Add("BC_ONLINE_CATG_NAME")
                dt.Columns.Add("BC_ONLINE_CATG_COST")
                dt.Columns.Add("CONN_COUNT")
                dt.Columns.Add("TOTAL")
                Dim dr As DataRow
                For Each gv As GridViewRow In GvConnectivity.Rows
                    Dim hdBC_ONLINE_CATG_ID As HiddenField = CType(gv.FindControl("hdBC_ONLINE_CATG_ID"), HiddenField)
                    If e.CommandArgument.ToString <> hdBC_ONLINE_CATG_ID.Value Then
                        dr = dt.NewRow
                        dr("BC_ONLINE_CATG_ID") = hdBC_ONLINE_CATG_ID.Value  'gv.Cells(0).Text
                        dr("BC_ONLINE_CATG_NAME") = gv.Cells(1).Text
                        dr("BC_ONLINE_CATG_COST") = gv.Cells(2).Text
                        dr("CONN_COUNT") = gv.Cells(3).Text
                        dr("TOTAL") = gv.Cells(4).Text
                        dt.Rows.Add(dr)
                    End If
                Next
                GvConnectivity.DataSource = dt
                GvConnectivity.DataBind()

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    'Protected Sub btnAddHardware_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddHardware.Click
    '    Try
    '        Dim objConX As New XmlDocument
    '        Dim objConDrpTemX As New XmlDocument
    '        hdHwIDCollection.Value = ""
    '        totalHardware = 0
    '        hdTabID.Text = "1"
    '        TablSelection()

    '        If Session("HardwareDataList") IsNot Nothing Then
    '            objConDrpTemX.LoadXml(Session("HardwareDataList").ToString())
    '        Else
    '            Return
    '        End If


    '        objConX.LoadXml("<HARDWARE><BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' PRODUCT_COUNT='' TOTAL=''/></HARDWARE>")
    '        Dim objFinalNode As XmlNode
    '        objFinalNode = objConX.DocumentElement.SelectSingleNode("BC_EQP")
    '        objConX.DocumentElement.RemoveChild(objFinalNode)
    '        'If Session("TempXmlConnecitvityFinal") Is Nothing Then
    '        '    Session("TempXmlConnecitvityFinal") = "<CONNECTIVITY></CONNECTIVITY>"
    '        'End If

    '        ' objConX.LoadXml(Session("TempXmlConnecitvityFinal").ToString())

    '        Dim rowCounter As Integer = 0
    '        Dim objConnNode As XmlNode
    '        Dim objClone, objCloneX As XmlNode
    '        For rowCounter = 0 To GvHardware.Rows.Count - 1
    '            objClone = objFinalNode
    '            objCloneX = objClone.CloneNode(True)
    '            Dim hdBC_EQP_CATG_ID As HiddenField = CType(GvHardware.Rows(rowCounter).FindControl("hdBC_EQP_CATG_ID"), HiddenField)

    '            If hdHwIDCollection.Value.Trim() = "" Then
    '                hdHwIDCollection.Value = hdBC_EQP_CATG_ID.Value
    '            Else
    '                hdHwIDCollection.Value = hdHwIDCollection.Value + "," + hdBC_EQP_CATG_ID.Value
    '            End If

    '            If btnAddHardware.Text.ToUpper() = "UPDATE" Then
    '                If btnAddHardware.CommandArgument = hdBC_EQP_CATG_ID.Value.Trim() Then
    '                    btnAddHardware.CommandArgument = ""
    '                    ' btnAddHardware.Text = "Add"
    '                    drpHardware.Enabled = True
    '                    Continue For
    '                End If
    '            End If

    '            '<HARDWARE BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' />
    '            objCloneX.Attributes("BC_EQP_CATG_ID").Value = hdBC_EQP_CATG_ID.Value   'GvHardware.Rows(rowCounter).Cells(0).Text.Trim()
    '            objCloneX.Attributes("BC_EQP_CATG_TYPE").Value = GvHardware.Rows(rowCounter).Cells(1).Text.Trim()
    '            objCloneX.Attributes("BC_EQP_CATG_COST").Value = GvHardware.Rows(rowCounter).Cells(2).Text.Trim()
    '            objCloneX.Attributes("PRODUCT_COUNT").Value = GvHardware.Rows(rowCounter).Cells(3).Text.Trim()
    '            objCloneX.Attributes("TOTAL").Value = Convert.ToDouble(GvHardware.Rows(rowCounter).Cells(2).Text.Trim()) * Convert.ToDouble(GvHardware.Rows(rowCounter).Cells(3).Text.Trim())
    '            objConX.DocumentElement.AppendChild(objCloneX)
    '            'objConX.DocumentElement.AppendChild(objConX.ImportNode(objCloneX, True))
    '            ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
    '            objCloneX = objClone.CloneNode(True)

    '        Next


    '        'Code For Duplicate Check
    '        If btnAddHardware.Text.ToUpper() = "UPDATE" Then
    '            btnAddHardware.Text = "Add"
    '        Else
    '            Dim objArrayList As New ArrayList(hdHwIDCollection.Value.Split(","))
    '            For c As Integer = 0 To objArrayList.Count - 1
    '                ' For i As Integer = 0 To drpHardware.Items.Count - 1
    '                If objArrayList(c).ToString().Trim() = drpHardware.SelectedValue.Trim() Then
    '                    lblError.Text = "Duplicate Hardware"
    '                    Return
    '                End If
    '                ' Next
    '            Next
    '        End If

    '        'End of Code for Duplicate check


    '        objConnNode = objConDrpTemX.DocumentElement.SelectSingleNode("HARDWARE[@BC_EQP_CATG_ID='" + drpHardware.SelectedValue.Trim() + "']")

    '        objClone = objFinalNode 'objConX.DocumentElement.SelectSingleNode("temp")
    '        objCloneX = objClone.CloneNode(True)
    '        'objConX.DocumentElement.RemoveChild(objClone)
    '        objCloneX.Attributes("BC_EQP_CATG_ID").Value = drpHardware.SelectedItem.Value
    '        objCloneX.Attributes("BC_EQP_CATG_TYPE").Value = drpHardware.SelectedItem.Text.Trim()
    '        objCloneX.Attributes("BC_EQP_CATG_COST").Value = objConnNode.Attributes("BC_EQP_CATG_COST").Value
    '        objCloneX.Attributes("PRODUCT_COUNT").Value = txtHWNo.Text.Trim()
    '        objCloneX.Attributes("TOTAL").Value = Convert.ToDouble(txtHWNo.Text.Trim()) * Convert.ToDouble(objConnNode.Attributes("BC_EQP_CATG_COST").Value.Trim())
    '        'objConX.DocumentElement.AppendChild(objCloneX)
    '        objConX.DocumentElement.AppendChild(objCloneX)
    '        ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
    '        objCloneX = objClone.CloneNode(True)

    '        If hdHwIDCollection.Value.Trim() = "" Then
    '            hdHwIDCollection.Value = drpHardware.SelectedItem.Value.Trim()
    '        Else
    '            hdHwIDCollection.Value = "," + drpHardware.SelectedItem.Value.Trim()
    '        End If





    '        ' Session("TempXmlConnecitvityFinal") = objConX.OuterXml
    '        Dim dsConn As New DataSet
    '        Dim objRdr As XmlNodeReader
    '        objRdr = New XmlNodeReader(objConX)
    '        dsConn.ReadXml(objRdr)
    '        GvHardware.DataSource = dsConn.Tables("BC_EQP").DefaultView
    '        GvHardware.DataBind()
    '        Session("HARDWARE") = objConX.OuterXml

    '        txtHWNo.Text = ""
    '        drpHardware.SelectedIndex = 0
    '    Catch ex As Exception
    '        lblError.Text = ex.Message

    '    End Try
    'End Sub




    Private Function validateSlabRange() As Boolean
        Try
            Dim rowCounter As Integer
            Dim slabStart As Decimal = 0
            Dim slabEnd As Decimal = 0
            Dim newSlabFrom As Double = Convert.ToDouble(txtRangeFromN.Text.Trim())
            Dim newSlabTo As Double = Convert.ToDouble(txtRangeToN.Text.Trim())

            For rowCounter = 0 To grdvSlabN.Rows.Count - 1
                slabStart = Convert.ToDouble(grdvSlabN.Rows(rowCounter).Cells(0).Text.Trim())
                slabEnd = Convert.ToDouble(grdvSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                    lblError.Text = "Slab Overlaps"
                    lblInnerError.Text = "Slab Overlaps"
                    Return True
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
    Private Function getDuplicateCaseName() As Boolean
        Dim blnDuplicateCase As Boolean = False
        Dim xmldoc As New XmlDocument
        If Session("INCENTIVEPLAN") IsNot Nothing Then
            xmldoc.LoadXml(Session("INCENTIVEPLAN").ToString())
        Else
            Return True
        End If

        For Each xNode As XmlNode In xmldoc.DocumentElement.SelectNodes("CASE")
            If xmldoc.DocumentElement.SelectNodes("CASE[translate(@INC_PLAN_NAME,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')= '" + xNode.Attributes("INC_PLAN_NAME").Value.Trim().ToLower + "']").Count > 1 Then
                blnDuplicateCase = True
                Exit For
            End If
        Next
        Return blnDuplicateCase
    End Function

    Private Function getDuplicateCaseName(ByVal strCaseName As String) As Boolean
        Dim blnDuplicateCase As Boolean = False
        Dim xmldoc As New XmlDocument
        If Session("INCENTIVEPLAN") IsNot Nothing Then
            xmldoc.LoadXml(Session("INCENTIVEPLAN").ToString())
        Else
            Return True
        End If

        For Each xNode As XmlNode In xmldoc.DocumentElement.SelectNodes("CASE")
            If xmldoc.DocumentElement.SelectNodes("CASE[translate(@INC_PLAN_NAME,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')= '" + strCaseName.Trim().ToLower + "']").Count >= 1 Then
                blnDuplicateCase = True
                Exit For
            End If
        Next
        Return blnDuplicateCase
    End Function

    Private Function validateSlabRange(ByRef GvIncSlabs As GridView, ByVal txtRangeFrom As String, ByVal txtRangeTo As String) As Boolean
        Try
            Dim rowCounter As Integer
            Dim slabStart As Decimal = 0
            Dim slabEnd As Decimal = 0
            Dim newSlabFrom As Double = Convert.ToDouble(txtRangeFrom.Trim())
            Dim newSlabTo As Double = Convert.ToDouble(txtRangeTo.Trim())

            For rowCounter = 0 To GvIncSlabs.Rows.Count - 1
                slabStart = Convert.ToDouble(GvIncSlabs.Rows(rowCounter).Cells(0).Text.Trim())
                slabEnd = Convert.ToDouble(GvIncSlabs.Rows(rowCounter).Cells(1).Text.Trim())
                If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                    lblError.Text = "Slab Overlaps"
                    lblInnerError.Text = "Slab Overlaps"
                    Return True
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function

    Private Function validaeCase(ByVal strTempID As String) As Boolean
        Try
            Dim objAllSlab As New XmlDocument
            If Session("INCENTIVEPLAN") Is Nothing Then
                Return False
            End If
            objAllSlab.LoadXml(Session("INCENTIVEPLAN"))
            For Each xnode As XmlNode In objAllSlab.DocumentElement.SelectNodes("CASE")
                If strTempID.Trim() <> xnode.Attributes("TEMP_SLAB_ID_MAIN").Value.Trim() Then
                    Dim objArrayList As New ArrayList(xnode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.ToString().Split(","))
                    For c As Integer = 0 To objArrayList.Count - 1
                        For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                            If chkSelectedCriteria.Items(i).Selected Then
                                If objArrayList(c).ToString().Trim() = chkSelectedCriteria.Items(i).Value.Trim() Then
                                    lblError.Text = "Criteria Already Added to case"
                                    Return True
                                End If
                            End If

                        Next
                    Next
                End If

            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
    'chkSelectedCriteria
    Private Function validaeCase() As Boolean
        Try
            Dim objAllSlab As New XmlDocument
            If Session("INCENTIVEPLAN") Is Nothing Then
                Return False
            End If
            objAllSlab.LoadXml(Session("INCENTIVEPLAN"))
            For Each xnode As XmlNode In objAllSlab.DocumentElement.SelectNodes("CASE")
                Dim objArrayList As New ArrayList(xnode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.ToString().Split(","))
                For c As Integer = 0 To objArrayList.Count - 1
                    For i As Integer = 0 To chklstCriteria.Items.Count - 1
                        If chklstCriteria.Items(i).Selected Then
                            If objArrayList(c).ToString().Trim() = chklstCriteria.Items(i).Value.Trim() Then
                                lblError.Text = "Criteria Already Added to case"
                                Return True
                            End If
                        End If

                    Next
                Next
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function

    Private Sub EnableDisableCase()
        Try
            'Code for Case Name & Criteria
            hdEnableDisableCase.Value = ""
            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                If chkSelectedCriteria.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                    hdEnableDisableCase.Value = "1"
                Else
                    hdEnableDisableCase.Value = "0"
                End If
            Next


            If hdEnableDisableCase.Value = "1" Then
                For i As Integer = 0 To chklstCriteria.Items.Count - 1
                    If chklstCriteria.Items(i).Text.ToUpper() <> "PRODUCTIVITY" Then
                        chklstCriteria.Items(i).Enabled = False
                    End If
                Next
            ElseIf hdEnableDisableCase.Value = "0" Then
                For i As Integer = 0 To chklstCriteria.Items.Count - 1
                    If chklstCriteria.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                        chklstCriteria.Items(i).Enabled = False
                    End If
                Next
            End If


            If hdEnableDisableCase.Value = "1" Then
                For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                    If chkLstGvShowMIDT.Items(i).Text.ToUpper() <> "PRODUCTIVITY" Then
                        chkLstGvShowMIDT.Items(i).Enabled = False
                    End If
                Next
            ElseIf hdEnableDisableCase.Value = "0" Then
                For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                    If chkLstGvShowMIDT.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                        chkLstGvShowMIDT.Items(i).Enabled = False
                    End If
                Next
            End If


            'chkLstGvShowMIDT
            'chkLstGvShowMIDT

            'Code for finding Gridview Checklistbox and E/D criteria
            'chkboxlist
            'grdvIncentivePlan
            For Each row As GridViewRow In grdvIncentivePlan.Rows
                Dim pnlMidtDetailsCollaps As UpdatePanel = CType(row.FindControl("pnlMidtDetailsCollaps"), UpdatePanel)
                'pnlShowCaseDetail
                'Dim pnlShowCaseDetail As Panel = CType(pnlMidtDetailsCollaps.FindControl("pnlShowCaseDetail"), Panel)
                Dim chkboxlist As CheckBoxList = CType(pnlMidtDetailsCollaps.FindControl("chklstCriteriaNew"), CheckBoxList)
                If chkboxlist IsNot Nothing Then
                    'Decision for Production or Not
                    hdEnableDisableCase.Value = ""
                    For i As Integer = 0 To chkboxlist.Items.Count - 1
                        If chkboxlist.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                            hdEnableDisableCase.Value = "1"
                        Else
                            hdEnableDisableCase.Value = "0"
                        End If
                    Next
                    'E/D Popup for all

                    If hdEnableDisableCase.Value = "1" Then
                        For i As Integer = 0 To chklstCriteria.Items.Count - 1
                            If chklstCriteria.Items(i).Text.ToUpper() <> "PRODUCTIVITY" Then
                                chklstCriteria.Items(i).Enabled = False
                            End If
                        Next
                    ElseIf hdEnableDisableCase.Value = "0" Then
                        For i As Integer = 0 To chklstCriteria.Items.Count - 1
                            If chklstCriteria.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                                chklstCriteria.Items(i).Enabled = False
                            End If
                        Next
                    End If

                    If hdEnableDisableCase.Value = "1" Then
                        For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                            If chkLstGvShowMIDT.Items(i).Text.ToUpper() <> "PRODUCTIVITY" Then
                                chkLstGvShowMIDT.Items(i).Enabled = False
                            End If
                        Next
                    ElseIf hdEnableDisableCase.Value = "0" Then
                        For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                            If chkLstGvShowMIDT.Items(i).Text.ToUpper() = "PRODUCTIVITY" Then
                                chkLstGvShowMIDT.Items(i).Enabled = False
                            End If
                        Next
                    End If



                End If
            Next



            '*********Special Case ********************
            Dim blnChk As Boolean = True

            If chkSelectedCriteria.Items.Count <> 0 Then
                blnChk = False
            End If

            For Each row As GridViewRow In grdvIncentivePlan.Rows
                Dim pnlMidtDetailsCollaps1 As UpdatePanel = CType(row.FindControl("pnlMidtDetailsCollaps"), UpdatePanel)
                Dim chkboxlist1 As CheckBoxList = CType(pnlMidtDetailsCollaps1.FindControl("chklstCriteriaNew"), CheckBoxList)
                If chkboxlist1 IsNot Nothing Then
                    If chkboxlist1.Items.Count <> 0 Then
                        blnChk = False
                    End If
                End If
            Next


            If blnChk Then
                For i As Integer = 0 To chklstCriteria.Items.Count - 1
                    chklstCriteria.Items(i).Enabled = True
                Next

                For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                    chkLstGvShowMIDT.Items(i).Enabled = True
                Next
            End If

            '*********End of Special Case *************


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function validaeCaseAddPlan() As Boolean
        Try
            Dim objAllSlab As New XmlDocument
            If Session("INCENTIVEPLAN") Is Nothing Then
                Return False
            End If
            objAllSlab.LoadXml(Session("INCENTIVEPLAN"))
            For Each xnode As XmlNode In objAllSlab.DocumentElement.SelectNodes("CASE")
                Dim objArrayList As New ArrayList(xnode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.ToString().Split(","))
                For c As Integer = 0 To objArrayList.Count - 1
                    For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                        If chkSelectedCriteria.Items(i).Selected Then
                            If objArrayList(c).ToString().Trim() = chkSelectedCriteria.Items(i).Value.Trim() Then
                                lblError.Text = "Criteria Already Added to case"
                                Return True
                            End If
                        End If

                    Next
                Next
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function


    Protected Sub btnAddPlan_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'PnlCreatePlan.CssClass = "displayBlock"


        hdTabID.Text = "0"


        Try
            Dim objConX, FinalXml As New XmlDocument
            Dim objConDrpTemX As New XmlDocument
            Dim objRdrInc As XmlNodeReader

            Dim objXml As New XmlDocument
            Dim objXmlNode As XmlNode
            ' objXml.LoadXml(hdIncentiveType.Value)
            'objXmlNode = objXml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + rdbPaymentType.SelectedValue.Trim() + "']")

            'If objXmlNode IsNot Nothing Then
            '    If objXmlNode.Attributes("INC_SLAB_REQUIRED").Value <> "TRUE" Then
            '        Return
            '    End If
            'End If

            '' Plan Validation Section
            'If btnAddPlan.CommandArgument <> "" Then
            '    If validaeCase(btnAddPlan.CommandArgument) Then
            '        Return
            '    End If
            'Else
            '    If validaeCaseAddPlan() Then
            '        Return
            '    End If
            'End If

            If chkSelectedCriteria.Items.Count = 0 Then
                lblError.Text = "Please select Airline Data!"
                lblInnerError.Text = "Please select Airline Data!"
                Return
            End If



            Dim blnCr As Boolean = False
            For crCount As Int16 = 0 To chkSelectedCriteria.Items.Count - 1
                If chkSelectedCriteria.Items(crCount).Selected = True Then
                    blnCr = True
                    Exit For
                Else
                    blnCr = False
                End If
            Next


            If blnCr = False Then
                lblError.Text = "Please select Airline Data!"
                lblInnerError.Text = "Please select Airline Data!"
                Return
            End If

            ' End of Plan Validation Section

            If Session("INCENTIVEPLAN") Is Nothing Then
                objConX.LoadXml("<BC_PLAN></BC_PLAN>")
            Else
                objConX.LoadXml(Session("INCENTIVEPLAN").ToString())
            End If

            'Duplicate Check of Case Name
            Dim str As String = txtCaseName.Text.Trim()
            If getDuplicateCaseName(str) = True Then
                lblError.Text = "Plan Name cannot be duplicate"
                lblInnerError.Text = "Plan Name cannot be duplicate"
                Return
            End If
            'Duplicate Check of Case Name

            Dim objxmlCase As New XmlDocument

            If Session("TempSlabsXml") IsNot Nothing Then
                objxmlCase.LoadXml(Session("TempSlabsXml"))
            ElseIf Session("slabBackup") IsNot Nothing Then
                objxmlCase.LoadXml(Session("slabBackup"))
            Else
                lblError.Text = "Slab is Mandatory"
                lblInnerError.Text = "Slab is Mandatory"
                Return
            End If

            Dim dsSlabs As New DataSet
            Dim ndrSlabs As XmlNodeReader = New XmlNodeReader(objxmlCase)
            dsSlabs.ReadXml(ndrSlabs)

            If dsSlabs.Tables("PLAN_DETAILS") Is Nothing Then
                lblError.Text = "Slab is Mandatory"
                lblInnerError.Text = "Slab is Mandatory"
                Return
            End If
            Dim oldNode As XmlNode
            If btnAddPlan.CommandArgument.Trim() <> "" Then
                oldNode = objConX.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + btnAddPlan.CommandArgument.Trim() + "']")
            End If

            'Code for Getting All NIDT Fields
            Dim strMIDT As String = ""
            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                If chkSelectedCriteria.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMIDT) Then
                        strMIDT = chkSelectedCriteria.Items(i).Value
                    Else
                        strMIDT = strMIDT + "," + chkSelectedCriteria.Items(i).Value
                    End If
                End If
            Next
            objxmlCase.DocumentElement.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value = strMIDT
            objxmlCase.DocumentElement.Attributes("INC_PLAN_NAME").Value = txtCaseName.Text.Trim()

            'Code for Getting All NIDT Fields

            objxmlCase.DocumentElement.Attributes("INC_PLAN_ID").Value = ""
            Dim nd As XmlNode = objxmlCase.DocumentElement



            'for blank last Node 
            Dim objXmlLast As New XmlDocument
            objXmlLast.LoadXml(objxmlCase.OuterXml)
            'For Each xNodeSlabRate As XmlNode In objXmlLast.DocumentElement.SelectNodes("PLAN_DETAILS") 'objConX.DocumentElement.LastChild.SelectNodes("PLAN_DETAILS")
            '    xNodeSlabRate.Attributes("SLABS_RATE").Value = ""
            'Next


            If btnAddPlan.CommandArgument.Trim() <> "" Then
                objConX.DocumentElement.RemoveChild(oldNode)
                objConX.DocumentElement.AppendChild(objConX.ImportNode(objXmlLast.DocumentElement, True))
                ' objConX.DocumentElement.ReplaceChild((objConX.ImportNode(nd, True))

                btnAddPlan.CommandArgument = ""
                btnAddPlan.Text = "Add Plan"
            Else
                objConX.DocumentElement.AppendChild(objConX.ImportNode(objXmlLast.DocumentElement, True))
            End If


            For Each objXmlNode In objConX.DocumentElement.SelectNodes("CASE")
                If objXmlNode.Attributes("INC_PLAN_NAME").Value.Trim() = "" Then
                    objConX.DocumentElement.RemoveChild(objXmlNode)
                End If
            Next

            Dim counter As Integer = 0
            For Each i As XmlNode In objConX.DocumentElement.SelectNodes("CASE")
                i.Attributes("TEMP_SLAB_ID_MAIN").Value = counter.ToString()
                counter += 1
            Next









            Session("INCENTIVEPLAN") = objConX.OuterXml

            'Code for Updating Xml according to Gridview
            UpdateXmlIncentivePlan()
            objConX.LoadXml(Session("INCENTIVEPLAN").ToString())
            'Code for Updating Xml according to Gridview

            For Each objXmlNode In objxmlCase.DocumentElement.SelectNodes("PLAN_DETAILS")
                objXmlNode.Attributes("SLABS_RATE").Value = ""
            Next

            'Session("slabBackup") = Nothing 'objxmlCase.OuterXml
            Session("slabBackup") = objxmlCase.OuterXml

            Session("TempSlabsXml") = Nothing
            Dim dsIncPlan As New DataSet
            objRdrInc = New XmlNodeReader(objConX)
            dsIncPlan.ReadXml(objRdrInc)
            grdvIncentivePlan.DataSource = dsIncPlan.Tables("CASE").DefaultView
            grdvIncentivePlan.DataBind()
            dsSlabs = New DataSet
            ndrSlabs = New XmlNodeReader(objxmlCase)
            dsSlabs.ReadXml(ndrSlabs)
            If dsSlabs.Tables("PLAN_DETAILS") IsNot Nothing Then
                grdvSlabN.DataSource = dsSlabs.Tables("PLAN_DETAILS").DefaultView
                grdvSlabN.DataBind()

            End If


            txtCaseName.Text = ""
            'grdvIncentivePlanOld.DataSource = dsIncPlan.Tables("CASE").DefaultView
            'grdvIncentivePlanOld.DataBind()

            chklstCriteria.DataSource = Nothing
            chklstCriteria.DataMember = Nothing
            chklstCriteria.DataValueField = Nothing
            chklstCriteria.DataBind()
            chkSelectedCriteria.Items.Clear()
            chkSelectedCriteria.DataSource = Nothing
            chkSelectedCriteria.DataBind()

            bindChkListForShowModal()
            bindChkListForTemplate()
            EnableDisableCase()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblInnerError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvIncSlabsNested_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) 'Handles GvIncSlabs.RowCommand
        Dim objIncentiveTempXml As New XmlDocument
        Dim objNode, objNodeChld As XmlNode


        Dim dsInc As DataSet
        Dim objRdrInc As XmlNodeReader
        Dim strvalID As String
        Try

            objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())

            If e.CommandName = "EditXNested" Then
                strvalID = e.CommandArgument.ToString()

                Dim hdNestedUpdateFlag As HiddenField

                Dim grdPlanRow As GridViewRow = grdvIncentivePlan.Rows(Convert.ToInt32(strvalID.Split("|")(1)))
                hdNestedUpdateFlag = CType(grdPlanRow.FindControl("hdNestedUpdateFlag"), HiddenField)
                hdNestedUpdateFlag.Value = strvalID

                Dim txtSlabStart As TextBox = CType(grdPlanRow.FindControl("txtRangeFrom"), TextBox)
                Dim txtSlabEnd As TextBox = CType(grdPlanRow.FindControl("txtRangeTo"), TextBox)
                Dim txtSlabRate As TextBox = CType(grdPlanRow.FindControl("txtSlabAmount"), TextBox)
                Dim btnAddSlabsGrd As Button = CType(grdPlanRow.FindControl("btnAddSlabs"), Button)
                btnAddSlabsGrd.Text = "Update"
                'btnAddSlabs
                objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + strvalID.Split("|")(1) + "']")
                If objNode Is Nothing Then
                    lblError.Text = "Unable to Edit Slabs"
                    Return
                End If
                objNodeChld = objNode.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strvalID.Split("|")(0) + "']")

                If objNodeChld IsNot Nothing Then
                    txtSlabStart.Text = objNodeChld.Attributes("SLABS_START").Value
                    txtSlabEnd.Text = objNodeChld.Attributes("SLABS_END").Value

                    txtSlabRate.Text = objNodeChld.Attributes("SLABS_RATE").Value
                End If

            End If



            If e.CommandName = "DelXNested" Then
                strvalID = e.CommandArgument.ToString()
                objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + strvalID.Split("|")(1) + "']")
                objNodeChld = objNode.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strvalID.Split("|")(0) + "']")
                objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + strvalID.Split("|")(1) + "']").RemoveChild(objNodeChld)

                Session("INCENTIVEPLAN") = objIncentiveTempXml.OuterXml
                'Databinding Again to Incentive
                objRdrInc = New XmlNodeReader(objIncentiveTempXml)
                dsInc = New DataSet()
                dsInc.ReadXml(objRdrInc)

                'Code for Binding Slabs in Edit Mode
                grdvIncentivePlan.DataSource = dsInc.Tables("CASE").DefaultView
                grdvIncentivePlan.DataBind()

                grdvIncentivePlanOld.DataSource = dsInc.Tables("CASE").DefaultView 'Test for self
                grdvIncentivePlanOld.DataBind()
                'Databinding Again to Incentive

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    'Protected Sub GvIncSlabs_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) 'Handles GvIncSlabs.RowCommand
    '    Try
    '        If e.CommandName = "EditX" Then
    '            Dim dt As New DataTable
    '            dt.Columns.Add("TEMP_SLAB_ID")
    '            dt.Columns.Add("SLABS_START")
    '            dt.Columns.Add("SLABS_END")
    '            dt.Columns.Add("SLABS_RATE")

    '            Dim dr As DataRow
    '            For Each gv As GridViewRow In GvIncSlabs.Rows
    '                dr = dt.NewRow
    '                dr("TEMP_SLAB_ID") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
    '                dr("SLABS_START") = gv.Cells(0).Text
    '                dr("SLABS_END") = gv.Cells(1).Text
    '                dr("SLABS_RATE") = gv.Cells(2).Text
    '                dt.Rows.Add(dr)
    '            Next

    '            Dim dv As DataView = dt.DefaultView
    '            dv.RowFilter = "TEMP_SLAB_ID='" + e.CommandArgument.ToString + "'"
    '            txtRangeFrom.Text = dv(0)("SLABS_START").ToString
    '            txtRangeTo.Text = dv(0)("SLABS_END").ToString
    '            txtSlabAmount.Text = dv(0)("SLABS_RATE").ToString
    '            btnAddSlabs.Text = "Update"
    '            btnAddSlabs.CommandArgument = e.CommandArgument.ToString

    '        End If
    '        If e.CommandName = "DelX" Then
    '            Dim dt As New DataTable
    '            dt.Columns.Add("TEMP_SLAB_ID")
    '            dt.Columns.Add("SLABS_START")
    '            dt.Columns.Add("SLABS_END")
    '            dt.Columns.Add("SLABS_RATE")
    '            Dim dr As DataRow
    '            For Each gv As GridViewRow In GvIncSlabs.Rows
    '                If e.CommandArgument.ToString <> CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value Then
    '                    dr = dt.NewRow
    '                    dr("TEMP_SLAB_ID") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
    '                    dr("SLABS_START") = gv.Cells(0).Text
    '                    dr("SLABS_END") = gv.Cells(1).Text
    '                    dr("SLABS_RATE") = gv.Cells(2).Text
    '                    dt.Rows.Add(dr)
    '                End If
    '            Next
    '            GvIncSlabs.DataSource = dt
    '            GvIncSlabs.DataBind()

    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub grdvIncentivePlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvIncentivePlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objxmlBusinessCase As New XmlDocument

            Dim objIncRdr, objReadaerCriteria As XmlNodeReader
            objxmlBusinessCase.LoadXml(Session("INCENTIVEPLAN").ToString())
            Dim dsIncPlan, dsCriteria As DataSet
            objIncRdr = New XmlNodeReader(objxmlBusinessCase)
            dsIncPlan = New DataSet()
            dsIncPlan.ReadXml(objIncRdr)
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

            'Code for Opening Popup
            Dim imgCriteriaSelection As LinkButton = CType(e.Row.FindControl("lnkCriteriaSelection"), LinkButton)
            ' imgCriteriaSelection.Attributes.Add("onclick", "return ShowModalPopupExtenderForTrainRoute1('" + "78" + "')")

            'End of Code for Opening Popup

            Try

                Dim hdCaseId As HiddenField = CType(e.Row.FindControl("hdCaseId"), HiddenField)
                Dim ChkListCriteria As CheckBoxList = CType(e.Row.FindControl("chklstCriteriaNew"), CheckBoxList)

                Dim objOutputCriteriaXmlXml As New XmlDocument
                objOutputCriteriaXmlXml = objbzBusinessCase.List_NIDTFields
                If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim objTempMidtXml As New XmlDocument
                    objTempMidtXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")

                    '@ Code for selection for criteria
                    Dim dvNIDTFIELDS As DataView
                    If dsIncPlan.Tables("NIDT_FIELDS_ID") IsNot Nothing Then
                        dvNIDTFIELDS = dsIncPlan.Tables("NIDT_FIELDS_ID").DefaultView
                        dvNIDTFIELDS.RowFilter = "Case_Id='" + hdCaseId.Value + "'"
                        If dvNIDTFIELDS.Count > 0 Then
                            Dim objArrayList As New ArrayList(dvNIDTFIELDS.Item(0)(0).ToString().Split(","))


                            For ii As Integer = 0 To objArrayList.Count - 1
                                Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                                If objN IsNot Nothing Then
                                    objTempMidtXml.DocumentElement.AppendChild(objTempMidtXml.ImportNode(objN, True))
                                End If
                            Next


                        End If
                    End If
                    '@ Code for selection for criteria



                    dsCriteria = New DataSet
                    objReadaerCriteria = New XmlNodeReader(objTempMidtXml)
                    dsCriteria.ReadXml(objReadaerCriteria)
                    ChkListCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                    ChkListCriteria.DataTextField = "NIDT_FIELDS_NAME"
                    ChkListCriteria.DataValueField = "NIDT_FIELDS_ID"
                    ChkListCriteria.DataBind()



                    For i As Integer = 0 To ChkListCriteria.Items.Count - 1
                        ChkListCriteria.Items(i).Selected = True
                    Next

                End If


                '@ Code for Filling PLAN DETAILS
                Dim dvPlanDetails As DataView
                Dim GvIncSlabsNested As GridView = CType(e.Row.FindControl("GvIncSlabsNested"), GridView)
                If dsIncPlan.Tables("PLAN_DETAILS") IsNot Nothing Then
                    dvPlanDetails = dsIncPlan.Tables("PLAN_DETAILS").DefaultView
                    dvPlanDetails.RowFilter = "Case_Id='" + hdCaseId.Value + "'"





                    If dvPlanDetails.Count > 0 Then
                        AddHandler GvIncSlabsNested.RowDataBound, AddressOf GvIncSlabsNested_RowDataBound
                        AddHandler GvIncSlabsNested.RowCommand, AddressOf GvIncSlabsNested_RowCommand

                        GvIncSlabsNested.DataSource = dvPlanDetails
                        GvIncSlabsNested.DataBind()

                        'Dim cc As Integer = 0

                        'For cc = 0 To GvIncSlabs.Rows.Count - 1
                        '    Dim lnkEdit As LinkButton = CType(GvIncSlabs.Rows(cc).FindControl("lnkEdit"), LinkButton)
                        '    AddHandler lnkEdit.Click, AddressOf lnkEdit_Click
                        'Next

                    Else
                        GvIncSlabsNested.DataSource = Nothing
                        GvIncSlabsNested.DataBind()
                    End If

                Else
                    GvIncSlabsNested.DataSource = Nothing
                    GvIncSlabsNested.DataBind()
                End If
                '@ End of Code for Filling PLAN DETAILS



            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim a As Integer = 90
        Response.Write(a.ToString())
    End Sub

    Protected Sub grdvIncentivePlan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdvIncentivePlan.RowCommand
        Try
            If Session("Security") Is Nothing Then
                lblError.Text = "Session is expired."
                Exit Sub
            End If
            Dim objIncentiveTempXml As New XmlDocument
            Dim objNode, objNodeChld, objCloneNode As XmlNode
            objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())
            Dim dsInc As DataSet
            Dim objRdrInc As XmlNodeReader
            hdMidtGvClick.Value = ""





            If e.CommandName = "AddSlabNested" Then


                Dim valTEMP_SLAB_ID As String = ""
                Dim blnUpdate As Boolean = False

                Dim grdPlanRow As GridViewRow = grdvIncentivePlan.Rows(Convert.ToInt32(e.CommandArgument.ToString()))
                Dim hdNestedUpdateFlag As HiddenField = CType(grdPlanRow.FindControl("hdNestedUpdateFlag"), HiddenField)



                Dim txtSlabStart As TextBox = CType(grdPlanRow.FindControl("txtRangeFrom"), TextBox)
                Dim txtSlabEnd As TextBox = CType(grdPlanRow.FindControl("txtRangeTo"), TextBox)
                Dim txtSlabRate As TextBox = CType(grdPlanRow.FindControl("txtSlabAmount"), TextBox)
                Dim btnAddSlabsGrd As Button = CType(grdPlanRow.FindControl("btnAddSlabs"), Button)
                Dim grdSlab As GridView = CType(grdPlanRow.FindControl("GvIncSlabsNested"), GridView)

                If (String.IsNullOrEmpty(txtSlabStart.Text.Trim())) Then
                    txtSlabStart.Focus()
                    lblError.Text = "Range From is Mandatory "
                    lblInnerError.Text = "Range From is Mandatory "
                    Return
                ElseIf String.IsNullOrEmpty(txtSlabRate.Text.Trim()) Then
                    txtSlabRate.Focus()
                    lblError.Text = "Slab Rate is Mandatory "
                    lblInnerError.Text = "Slab Rate is Mandatory "
                    Return
                End If

                Try
                    Convert.ToDouble(txtSlabStart.Text.Trim())
                Catch ex As Exception
                    lblError.Text = "Range From is Invalid"
                    lblInnerError.Text = "Range From is Invalid"
                    txtSlabStart.Focus()
                    Return
                End Try

                Try
                    Convert.ToDouble(txtSlabRate.Text.Trim())
                Catch ex As Exception
                    lblError.Text = "Slab Amount is Invalid"
                    lblInnerError.Text = "Slab Amount is Invalid"
                    txtSlabRate.Focus()
                    Return
                End Try

                If Not String.IsNullOrEmpty(txtSlabEnd.Text.Trim()) Then
                    Try
                        Convert.ToDouble(txtSlabEnd.Text.Trim())
                    Catch ex As Exception
                        lblError.Text = "Range To is Invalid"
                        lblInnerError.Text = "Range To is Invalid"
                        txtSlabEnd.Focus()
                        Return
                    End Try
                    If Convert.ToDouble(txtSlabStart.Text.Trim()) > Convert.ToDouble(txtSlabEnd.Text.Trim()) Then
                        lblError.Text = "Range From is less than Range To"
                        lblInnerError.Text = "Range From is less than Range To"
                        txtSlabStart.Focus()
                        Return
                    End If

                End If





                If btnAddSlabsGrd.Text <> "Update" Then
                    Dim strEndval As String = "0"
                    Try
                        strEndval = txtSlabEnd.Text
                    Catch ex As Exception
                        strEndval = "0"
                    End Try
                    If validateSlabRange(grdSlab, txtSlabStart.Text, strEndval) Then
                        Return
                    End If
                End If




                If hdNestedUpdateFlag.Value.Trim() <> "" Then
                    If hdNestedUpdateFlag.Value.Trim().Split("|")(1) = e.CommandArgument.ToString() Then
                        blnUpdate = True
                    Else
                        blnUpdate = False
                    End If
                Else
                    blnUpdate = False
                End If



                For Each objNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']")
                    For Each chldNode As XmlNode In objNode.SelectNodes("PLAN_DETAILS")
                        'Slabs overlap validation Section for Update
                        valTEMP_SLAB_ID = chldNode.Attributes("TEMP_SLAB_ID").Value.Trim()
                        If blnUpdate Then
                            Dim slabStart As Decimal = 0
                            Dim slabEnd As Decimal = 0
                            Dim newSlabFrom As Double = Convert.ToDouble(txtSlabStart.Text.Trim())
                            Dim newSlabTo As Double = 0
                            Try
                                newSlabTo = Convert.ToDouble(txtSlabEnd.Text.Trim())
                            Catch ex As Exception
                                newSlabTo = 0
                            End Try

                            ' For rowCounter = 0 To GvIncSlabs.Rows.Count - 1
                            If valTEMP_SLAB_ID <> hdNestedUpdateFlag.Value.Trim().Split("|")(0) Then
                                slabStart = Convert.ToDouble(chldNode.Attributes("SLABS_START").Value.Trim())
                                Try
                                    slabEnd = Convert.ToDouble(chldNode.Attributes("SLABS_END").Value.Trim())
                                Catch ex As Exception
                                    slabEnd = 0
                                End Try
                                If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                                    lblError.Text = "Slab Overlaps"
                                    Return
                                End If
                            End If
                            ' Next
                        End If
                        'End of Slabs overlap validation Section for Update

                    Next
                Next



                'Fetch Max Salb Temp_slab ID
                For Each objNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']")
                    For Each chldNode As XmlNode In objNode.SelectNodes("PLAN_DETAILS")
                        valTEMP_SLAB_ID = chldNode.Attributes("TEMP_SLAB_ID").Value.Trim()
                    Next
                Next




                objNodeChld = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']")
                objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE/PLAN_DETAILS")
                objCloneNode = objNode.CloneNode(True)


                objCloneNode.Attributes("SLABS_START").Value = txtSlabStart.Text.Trim()
                objCloneNode.Attributes("SLABS_END").Value = txtSlabEnd.Text.Trim()
                objCloneNode.Attributes("SLABS_RATE").Value = txtSlabRate.Text.Trim()
                objCloneNode.Attributes("TEMP_SLAB_ID").Value = (Convert.ToInt32(valTEMP_SLAB_ID) + 1).ToString()


                If hdNestedUpdateFlag.Value.Trim() <> "" Then
                    If hdNestedUpdateFlag.Value.Trim().Split("|")(1) = e.CommandArgument.ToString() Then
                        Dim objOldNode As XmlNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']").SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + hdNestedUpdateFlag.Value.Trim().Split("|")(0) + "']")
                        objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']").ReplaceChild(objCloneNode, objOldNode)
                        hdNestedUpdateFlag.Value = ""
                    Else
                        objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']").AppendChild(objCloneNode)
                    End If
                Else
                    objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + e.CommandArgument.ToString() + "']").AppendChild(objCloneNode)
                End If












                Session("INCENTIVEPLAN") = objIncentiveTempXml.OuterXml
                'Databinding Again to Incentive
                objRdrInc = New XmlNodeReader(objIncentiveTempXml)
                dsInc = New DataSet()
                dsInc.ReadXml(objRdrInc)

                'Code for Binding Slabs in Edit Mode
                grdvIncentivePlan.DataSource = dsInc.Tables("CASE").DefaultView
                grdvIncentivePlan.DataBind()
                'Databinding Again to Incentive


            End If

            If e.CommandName = "EditX" Then

                Dim TEMP_SLAB_ID_MAIN As String = e.CommandArgument.ToString()

                btnAddPlan.CommandArgument = TEMP_SLAB_ID_MAIN
                btnAddPlan.Text = "Update"

                Dim objXmlNode As XmlNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + TEMP_SLAB_ID_MAIN + "']")
                Dim objUpdateXml As New XmlDocument
                objUpdateXml.LoadXml("<BC_PLAN></BC_PLAN>")
                objUpdateXml.DocumentElement.AppendChild(objUpdateXml.ImportNode(objXmlNode, True))
                objRdrInc = New XmlNodeReader(objUpdateXml)
                dsInc = New DataSet()
                dsInc.ReadXml(objRdrInc)

                'Code for Binding Slabs in Edit Mode
                ' GvIncSlabs.DataSource = dsInc.Tables("PLAN_DETAILS").DefaultView
                'GvIncSlabs.DataBind()
                txtCaseName.Text = objUpdateXml.DocumentElement.SelectSingleNode("CASE").Attributes("INC_PLAN_NAME").Value
                'End of Code for Binding Slabs in Edit Mode

                'Code for Case Name & Criteria
                For i As Integer = 0 To chklstCriteria.Items.Count - 1
                    chklstCriteria.Items(i).Selected = False
                Next
                ' Dim strSelectedChkbox() As String = objUpdateXml.DocumentElement.SelectSingleNode("CASE/NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(",")

                Dim objArrayList As New ArrayList(objUpdateXml.DocumentElement.SelectSingleNode("CASE/NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(","))

                For c As Integer = 0 To objArrayList.Count - 1
                    For i As Integer = 0 To chklstCriteria.Items.Count - 1
                        If objArrayList.Count > 0 Then
                            If objArrayList(c).ToString().Trim() = chklstCriteria.Items(i).Value.Trim() Then
                                chklstCriteria.Items(i).Selected = True
                            End If
                        End If
                    Next
                Next

                'End of Code for Case Name & Criteria
                Session("TempSlabsXml") = objXmlNode.OuterXml

            End If

            If e.CommandName = "DelXPlan" Then
                Dim TEMP_SLAB_ID_MAIN As String = e.CommandArgument.ToString()
                Dim objXmlNode As XmlNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + TEMP_SLAB_ID_MAIN + "']")
                If objXmlNode IsNot Nothing Then
                    objIncentiveTempXml.DocumentElement.RemoveChild(objXmlNode)
                    lblError.Text = "Plan Deleted Sucessufly"
                End If

                TEMP_SLAB_ID_MAIN = 0
                Dim TEMP_SLAB_ID As Integer = 0
                Dim objX As New XmlDocument

                For Each xNode As XmlNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE")
                    xNode.Attributes("TEMP_SLAB_ID_MAIN").Value = TEMP_SLAB_ID_MAIN.ToString()
                    For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                        xChildNd.Attributes("TEMP_SLAB_ID").Value = TEMP_SLAB_ID.ToString()
                        TEMP_SLAB_ID += 1
                    Next
                    TEMP_SLAB_ID_MAIN += 1
                    TEMP_SLAB_ID = 0
                Next


                objRdrInc = New XmlNodeReader(objIncentiveTempXml)
                dsInc = New DataSet()
                If objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE") IsNot Nothing Then
                    dsInc.ReadXml(objRdrInc)
                    grdvIncentivePlan.DataSource = dsInc.Tables("CASE").DefaultView
                    grdvIncentivePlan.DataBind()
                Else
                    grdvIncentivePlan.DataSource = Nothing
                    grdvIncentivePlan.DataBind()
                End If
                Session("INCENTIVEPLAN") = objIncentiveTempXml.OuterXml
                bindChkListForShowModal()
                resetTempID()
            End If

            If e.CommandName = "MODALPOPUP" Then

                tlbGvMidtSelection.Style.Add("display", "block")
                'Code for Binding Criteria for Grid Data Selection

                'End of Code for Binding Criteria for Grid Data Selection
                hdMidtGvClick.Value = e.CommandArgument

                bindChkListForGrid()
                EnableDisableCase()
                ModalForGvSelection.Show()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub LoadData(ByVal strChainCode As String, ByVal strBcID As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml, objOutPutxmlOld As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer, objXmlReader As XmlNodeReader
        Dim blnHw As Boolean = False
        Dim blnConn As Boolean = False
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim li As New ListItem
        Dim objOutputCriteriaXmlXml As New XmlDocument
        Dim objArrayList As ArrayList
        Dim objTempMidtXml As New XmlDocument
        Dim objTempMSXml As New XmlDocument
        Dim objTempSQXml As New XmlDocument
        objOutPutxmlOld.LoadXml("<INC_VIEW_BUSINESSCASE_OUTPUT></INC_VIEW_BUSINESSCASE_OUTPUT>")
        '@ Code  for Details
        Try

            BindData_MIDTAgency()

            objInputXml.LoadXml("<INC_VIEW_BUSINESSCASE_INPUT><BC_ID></BC_ID><Chain_Code></Chain_Code><CONVERSION_PER></CONVERSION_PER><EMPLOYEEID /><REFRESH/> </INC_VIEW_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = strChainCode
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcID
            objInputXml.DocumentElement.SelectSingleNode("CONVERSION_PER").InnerText = txtConversionPer.Text.Trim()
            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If
            objInputXml.DocumentElement.SelectSingleNode("REFRESH").InnerText = IIf(hdRefreshAction.Value = "", "FALSE", hdRefreshAction.Value)

            objOutPutxml = objbzBusinessCase.View(objInputXml)
            ' objOutPutxml.Save("D:\XML\XML\AAMS\Incentive\BC_VIEW_OUTPUT.xml")
            'objOutPutxml.Load("D:\XML\XML\AAMS\Incentive\BC_VIEW_OUTPUT.xml")
            '<BUSINESSCASE BC_ID='' CHAIN_CODE='' GROUP_NAME='' AccountManager_Name='' Region='' PAYMENT_CYCLE_ID='' PAYMENT_CYCLE_NAME='' 
            'BC_EFFECTIVE_FROM='' BC_VALID_TILL='' BC_DTTI_CREATED='' FINAL_APPROVED='' FINAL_APPROVED_BY='' FINAL_APPROVED_DTTI='' INC_TYPE_ID=''
            ' INC_TYPE_NAME='' INC_SLAB_REQUIRED='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT='' 
            'APPROVERS='' RATE='' EXP_SEGMENT='' TOTAL_COST='' CONVERSION_PER='' T_SEG_AS_CONV_PER='' TOT_AFTER_CONVERSION_PER='' 
            'MIN_MON_SEG='' MONTHS='' PAYMENTTYPEID='' UPFRONTTYPEID='' PLBTYPEID='' PLBSLAB=''/>

            ' <INC_DETAILS RATE="8" EXP_SEGMENT="9" TOTAL_COST="91782" SOLE_AMOUNT="" BONUS_AMOUNT="" FIXED_PAYMENT="10" UPFRONT_AMOUNT="13" FIXED_UPFRONT="" /> 

            'For Each objDelNde As XmlNode In objOutPutxml.DocumentElement.SelectNodes("BC_MIDT_AGENCY")
            '    objOutPutxml.DocumentElement.RemoveChild(objDelNde)
            'Next



            '     objOutPutxml.LoadXml(str)


            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                txtConversionPer.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("CONVERSION_PER").InnerText

                HdConversionPer.Value = txtConversionPer.Text

                objOutPutxml = CalculateBreakUp(objOutPutxml)


                Session("INCENTIVEPLAN_HISTORY") = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").OuterXml

                Dim objOldNode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN_OLD")

                objOutPutxmlOld.DocumentElement.AppendChild(objOutPutxmlOld.ImportNode(objOldNode, True))
                objOutPutxml.DocumentElement.RemoveChild(objOldNode)

                'Period From & To for Existing Deal

                If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/INC_TYPE").Attributes("BC_EFFECTIVE_FROM").Value.Trim().Length <> 0 Then
                    txtValidfromOld.Text = objeAAMS.ConvertDateBlank(objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/INC_TYPE").Attributes("BC_EFFECTIVE_FROM").Value)
                End If
                If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/INC_TYPE").Attributes("BC_VALID_TILL").Value.Trim().Length <> 0 Then
                    txtValidToOld.Text = objeAAMS.ConvertDateBlank(objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/INC_TYPE").Attributes("BC_VALID_TILL").Value)
                End If

                'Period From & To for Existing Deal


                '#################################################################
                '@ Start of Code Added By Abhishek on 27 jan 2010  
                '#################################################################

                '@ Getting the value of minimum segment criteria
                Dim objOutputMinSegCriteria As New XmlDocument
                objOutputMinSegCriteria = objbzBusinessCase.List_SegmentFields
                Dim dsNIDTField As New DataSet
                Dim objTempMinCriteriaxml As New XmlDocument

                If objOutputMinSegCriteria.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    '@ Start of  Bind the old minimum segment criteria
                    objTempMinCriteriaxml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                    If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/MIN_SEGMENT").Attributes("ID").Value.Trim <> "" Then

                        objArrayList = New ArrayList(objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/MIN_SEGMENT").Attributes("ID").Value.ToString().Split(","))

                        For ii As Integer = 0 To objArrayList.Count - 1
                            Dim objN As XmlNode = objOutputMinSegCriteria.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                            If objN IsNot Nothing Then
                                objTempMinCriteriaxml.DocumentElement.AppendChild(objTempMinCriteriaxml.ImportNode(objN, True))
                            End If
                        Next

                        ds = New DataSet()
                        objXmlReader = New XmlNodeReader(objTempMinCriteriaxml)
                        ds.ReadXml(objXmlReader)
                        ChkMinimunCriteriaOld.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                        ChkMinimunCriteriaOld.DataValueField = "NIDT_FIELDS_ID"
                        ChkMinimunCriteriaOld.DataTextField = "NIDT_FIELDS_NAME"
                        ChkMinimunCriteriaOld.DataBind()

                        For i As Integer = 0 To ChkMinimunCriteriaOld.Items.Count - 1
                            ChkMinimunCriteriaOld.Items(i).Selected = True
                        Next
                    End If

                    '@ End of  Bind the old minimum segment criteria

                    '@Start of code for New minimum segment criteria

                    objArrayList = New ArrayList(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/MIN_SEGMENT").Attributes("ID").Value.ToString().Split(","))

                    ' BindMinimumSegmentCriteria(ChkMinimunCriteriaNew)

                    '@Start of code for New minimum segment criteria

                    objArrayList = New ArrayList(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/MIN_SEGMENT").Attributes("ID").Value.ToString().Split(","))

                    ' BindMinimumSegmentCriteria(ChkMinimunCriteriaNew)

                    '@ Now Select item on the basis of user selection   
                    ChkMinimunCriteriaNew.Items.Clear()

                    objTempMSXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                    For ii As Integer = 0 To objArrayList.Count - 1
                        Dim objN As XmlNode = objOutputMinSegCriteria.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                        If objN IsNot Nothing Then
                            objTempMSXml.DocumentElement.AppendChild(objTempMSXml.ImportNode(objN, True))
                        End If
                    Next
                    If objTempMSXml.DocumentElement.SelectSingleNode("NIDTFIELDS") IsNot Nothing Then
                        ds = New DataSet()
                        objXmlReader = New XmlNodeReader(objTempMSXml)
                        ds.ReadXml(objXmlReader)
                        ChkMinimunCriteriaNew.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                        ChkMinimunCriteriaNew.DataValueField = "NIDT_FIELDS_ID"
                        ChkMinimunCriteriaNew.DataTextField = "NIDT_FIELDS_NAME"
                        ChkMinimunCriteriaNew.DataBind()
                        For i As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                            ChkMinimunCriteriaNew.Items(i).Selected = True
                        Next
                    Else
                        ChkMinimunCriteriaNew.DataSource = Nothing
                        ChkMinimunCriteriaNew.DataBind()
                    End If

                    '@ End of  New minimum segment criteria


                End If
                If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/MIN_SEGMENT").Attributes("MIN_SEGMENT_VAL") IsNot Nothing Then
                    TxtMinSegCriteriaValueOld.Text = objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/MIN_SEGMENT").Attributes("MIN_SEGMENT_VAL").Value
                End If
                If objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/MIN_SEGMENT").Attributes("MIN_SEGMENT_VAL") IsNot Nothing Then
                    TxtMinSegCriteriaValueNew.Text = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/MIN_SEGMENT").Attributes("MIN_SEGMENT_VAL").Value
                End If

                '#################################################################
                '@ End  of Code Added By Abhishek on 27 jan 2010
                '#################################################################


                '--for  old SLAB QUALIFICATION

                objOutputCriteriaXmlXml = objbzBusinessCase.List_NIDTFields
                If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    objTempMidtXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")

                    '@ Code for selection for criteria
                    If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/SLAB_QUALIFICATION").Attributes("ID").Value.Trim <> "" Then


                        objArrayList = New ArrayList(objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/SLAB_QUALIFICATION").Attributes("ID").Value.ToString().Split(","))

                        For ii As Integer = 0 To objArrayList.Count - 1
                            Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                            If objN IsNot Nothing Then
                                objTempMidtXml.DocumentElement.AppendChild(objTempMidtXml.ImportNode(objN, True))
                            End If
                        Next

                        ds = New DataSet()
                        objXmlReader = New XmlNodeReader(objTempMidtXml)
                        ds.ReadXml(objXmlReader)
                        chkSLABQUALIFICATIONOld.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                        chkSLABQUALIFICATIONOld.DataValueField = "NIDT_FIELDS_ID"
                        chkSLABQUALIFICATIONOld.DataTextField = "NIDT_FIELDS_NAME"
                        chkSLABQUALIFICATIONOld.DataBind()

                        For i As Integer = 0 To chkSLABQUALIFICATIONOld.Items.Count - 1
                            chkSLABQUALIFICATIONOld.Items(i).Selected = True
                        Next
                    End If
                End If
                '--end

                'Code for Modififying Xml 
                Dim TEMP_SLAB_ID_MAIN As Integer = 0
                Dim TEMP_SLAB_ID As Integer = 0
                For Each xNode As XmlNode In objOutPutxml.DocumentElement.SelectNodes("BC_PLAN/CASE")
                    Dim newAttr As XmlAttribute = objOutPutxml.CreateAttribute("TEMP_SLAB_ID_MAIN")
                    newAttr.Value = TEMP_SLAB_ID_MAIN.ToString()
                    xNode.Attributes.Append(newAttr)

                    For Each xChildNd As XmlNode In xNode.SelectNodes("PLAN_DETAILS")
                        Dim newAttrSlab As XmlAttribute = objOutPutxml.CreateAttribute("TEMP_SLAB_ID")
                        newAttrSlab.Value = TEMP_SLAB_ID.ToString()
                        xChildNd.Attributes.Append(newAttrSlab)
                        TEMP_SLAB_ID += 1
                    Next
                    TEMP_SLAB_ID_MAIN += 1
                    TEMP_SLAB_ID = 0
                Next


                'For Adding Temprory ID in PLB
                TEMP_SLAB_ID = 0
                For Each xChildNd As XmlNode In objOutPutxml.DocumentElement.SelectNodes("PLB/PLB_DETAILS")
                    Dim newAttrSlab As XmlAttribute = objOutPutxml.CreateAttribute("PLBTYPEID_TEMP")
                    newAttrSlab.Value = TEMP_SLAB_ID.ToString()
                    xChildNd.Attributes.Append(newAttrSlab)
                    TEMP_SLAB_ID += 1
                Next
                'For Adding Temprory ID in PLB


                'Code for Footer Info
                If objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value.Trim().ToUpper() = "TRUE" Then
                    Dim objFooterNode As XmlNode
                    objFooterNode = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").LastChild
                    objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objFooterNode)

                    Session("TempSlabsXml") = objFooterNode.OuterXml
                    'Session("slabBackup") = objFooterNode.OuterXml
                    If objFooterNode.Attributes("INC_PLAN_NAME") IsNot Nothing Then
                        txtCaseName.Text = objFooterNode.Attributes("INC_PLAN_NAME").Value.Trim()
                    End If
                    If objFooterNode IsNot Nothing Then
                        Dim dsSlabsFooter As New DataSet
                        Dim objRdr As XmlNodeReader
                        objRdr = New XmlNodeReader(objFooterNode)
                        dsSlabsFooter.ReadXml(objRdr)
                        If dsSlabsFooter.Tables("PLAN_DETAILS") IsNot Nothing Then
                            grdvSlabN.DataSource = dsSlabsFooter.Tables("PLAN_DETAILS").DefaultView
                            grdvSlabN.DataBind()
                        End If
                    End If
                    'for Criteria Selection


                    If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                        objTempMidtXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")

                        '@ Code for selection for criteria

                        objArrayList = New ArrayList(objFooterNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.ToString().Split(","))

                        For ii As Integer = 0 To objArrayList.Count - 1
                            Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                            If objN IsNot Nothing Then
                                objTempMidtXml.DocumentElement.AppendChild(objTempMidtXml.ImportNode(objN, True))
                            End If
                        Next
                        If objTempMidtXml.DocumentElement.SelectSingleNode("NIDTFIELDS") IsNot Nothing Then
                            ds = New DataSet()
                            objXmlReader = New XmlNodeReader(objTempMidtXml)
                            ds.ReadXml(objXmlReader)
                            chkSelectedCriteria.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                            chkSelectedCriteria.DataValueField = "NIDT_FIELDS_ID"
                            chkSelectedCriteria.DataTextField = "NIDT_FIELDS_NAME"
                            chkSelectedCriteria.DataBind()

                            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                                chkSelectedCriteria.Items(i).Selected = True
                            Next
                        End If

                        '------------------------------------for SLAB_QUALIFICATION
                        chkSLABQUALIFICATION.Items.Clear()
                        objArrayList = New ArrayList(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/SLAB_QUALIFICATION").Attributes("ID").Value.ToString().Split(","))
                        objTempSQXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                        For ii As Integer = 0 To objArrayList.Count - 1
                            Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                            If objN IsNot Nothing Then
                                objTempSQXml.DocumentElement.AppendChild(objTempSQXml.ImportNode(objN, True))
                            End If
                        Next
                        If objTempSQXml.DocumentElement.SelectSingleNode("NIDTFIELDS") IsNot Nothing Then
                            ds = New DataSet()
                            objXmlReader = New XmlNodeReader(objTempSQXml)
                            ds.ReadXml(objXmlReader)
                            chkSLABQUALIFICATION.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                            chkSLABQUALIFICATION.DataValueField = "NIDT_FIELDS_ID"
                            chkSLABQUALIFICATION.DataTextField = "NIDT_FIELDS_NAME"
                            chkSLABQUALIFICATION.DataBind()
                            For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                                chkSLABQUALIFICATION.Items(i).Selected = True
                            Next
                        Else
                            chkSLABQUALIFICATION.DataSource = Nothing
                            chkSLABQUALIFICATION.DataBind()
                        End If

                        'For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                        '    If objArrayList.Contains(chkSLABQUALIFICATION.Items(i).Value) Then
                        '        chkSLABQUALIFICATION.Items(i).Selected = True
                        '    End If

                        'Next
                        '--end
                    End If


                    'for Criteria Selection

                    If grdvSlabN.Rows.Count > 0 Then
                        btnAddPlan.Visible = True
                    Else
                        btnAddPlan.Visible = False
                    End If
                Else
                    grdvSlabN.DataSource = Nothing
                    grdvSlabN.DataBind()
                End If
                'Code for Footer Info


                ''Incentive
                txtIncRateN.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("RATE").Value
                txtIncExpectedSegN.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("EXP_SEGMENT").Value
                lblIncTotalCostN.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("TOTAL_COST").Value
                lblTSegTotalCostN.Text = totalConn + totalHardware + Val(lblIncTotalCostN.Text)
                lblTotalSeg.Text = txtIncExpectedSegN.Text

                'End


                'Fix Incentive for New
                'For Fix Incentive 
                txtFixIncentive.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("FIXED_PAYMENT").Value
                txtFixIncMinMonthSeg.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("MIN_MON_SEG").Value
                txtFixIncMonth.Text = objOutPutxml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("MONTHS").Value




                Session("INCENTIVEPLAN") = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").OuterXml
                Dim objConnectivity As New XmlDocument
                objConnectivity.LoadXml("<CONNECTIVITY></CONNECTIVITY>")
                For Each xConNode As XmlNode In objOutPutxml.DocumentElement.SelectNodes("BC_CONN")
                    If xConNode.Attributes("BC_ONLINE_CATG_ID").Value.Trim() <> "" Then
                        objConnectivity.DocumentElement.AppendChild(objConnectivity.ImportNode(xConNode, True))
                        blnConn = True
                    End If
                Next

                Session("CONNECTIVITY") = objConnectivity.OuterXml
                BindGrid(Grid.Connectivity)

                Dim objHardware As New XmlDocument
                objHardware.LoadXml("<HARDWARE></HARDWARE>")
                For Each xHwNode As XmlNode In objOutPutxml.DocumentElement.SelectNodes("BC_EQP")
                    If xHwNode.Attributes("BC_EQP_CATG_ID").Value.Trim() <> "" Then
                        objHardware.DocumentElement.AppendChild(objHardware.ImportNode(xHwNode, True))

                        blnHw = True
                    End If
                Next
                Session("HARDWARE") = objHardware.OuterXml
                BindGrid(Grid.Hardware)


                'End of code for modifying Xml


                If objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value.Trim().ToUpper() = "FALSE" Then
                    If objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE") IsNot Nothing Then
                        txtCaseName.Text = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE").Attributes("INC_PLAN_NAME").Value
                        objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/CASE"))
                    End If
                    Session("INCENTIVEPLAN") = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").OuterXml
                End If



                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)

                Dim dsOld As New DataSet
                Dim objRdrOld As XmlNodeReader
                objRdrOld = New XmlNodeReader(objOutPutxmlOld)
                dsOld.ReadXml(objRdrOld)

                Session("INCENTIVEPLAN_OLD") = objOutPutxmlOld.OuterXml





                '********Binding Header Information************
                If ds.Tables("BUSINESSCASE") IsNot Nothing Then
                    txtGroupName.Text = ds.Tables("BUSINESSCASE").Rows(0)("GROUP_NAME").ToString
                    txtActManager.Text = ds.Tables("BUSINESSCASE").Rows(0)("AccountManager_Name").ToString

                    li = drpBillingCycle.Items.FindByValue(ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString)
                    If li IsNot Nothing Then
                        drpBillingCycle.SelectedValue = li.Value
                    End If

                    ' drpBillingCycle.SelectedValue = ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString
                    txtChainCode.Text = ds.Tables("BUSINESSCASE").Rows(0)("CHAIN_CODE").ToString
                    txtRegion.Text = ds.Tables("BUSINESSCASE").Rows(0)("Region").ToString
                    txtPeriodFrom.Text = ds.Tables("BUSINESSCASE").Rows(0)("BC_EFFECTIVE_FROM").ToString
                    txtPeriodTo.Text = ds.Tables("BUSINESSCASE").Rows(0)("BC_VALID_TILL").ToString
                    txtActManager.Text = ds.Tables("BUSINESSCASE").Rows(0)("AccountManager_Name").ToString
                    txtConversionPer.Text = ds.Tables("BUSINESSCASE").Rows(0)("CONVERSION_PER").ToString
                    HdConversionPer.Value = txtConversionPer.Text

                    txtUpfrontAmount.Text = ds.Tables("BUSINESSCASE").Rows(0)("UPFRONT_AMOUNT").ToString

                    If ds.Tables("BUSINESSCASE").Rows(0)("INC_TYPE_ID").ToString <> "" Then
                        rdbIncentiveType.SelectedValue = ds.Tables("BUSINESSCASE").Rows(0)("INC_TYPE_ID").ToString
                    End If


                    If ds.Tables("BUSINESSCASE").Rows(0)("UPFRONTTYPEID").ToString <> "" Then
                        rdbUpfrontTypeName.SelectedValue = ds.Tables("BUSINESSCASE").Rows(0)("UPFRONTTYPEID").ToString

                    End If

                    If ds.Tables("BUSINESSCASE").Rows(0)("PAYMENTTYPEID").ToString <> "" Then
                        rdbPaymentType.SelectedValue = ds.Tables("BUSINESSCASE").Rows(0)("PAYMENTTYPEID").ToString

                    End If

                    ''added on 4 sep 10
                    If ds.Tables("BUSINESSCASE").Rows(0)("SIGNUPAMOUNT") IsNot Nothing Then
                        txtSignUpAmount.Text = ds.Tables("BUSINESSCASE").Rows(0)("SIGNUPAMOUNT").ToString
                        If ds.Tables("BUSINESSCASE").Rows(0)("ADJUSTABLE").ToString <> "" Then
                            chkAdjustable.Checked = ds.Tables("BUSINESSCASE").Rows(0)("ADJUSTABLE").ToString
                        End If

                        txtNoOfPayments.Text = ds.Tables("BUSINESSCASE").Rows(0)("NO_OF_PAYMENTS").ToString
                        txtForThePeriodOf.Text = ds.Tables("BUSINESSCASE").Rows(0)("FOR_PERIOD_OF").ToString
                    End If
                    ''end



                    If ds.Tables("BUSINESSCASE").Rows(0)("PLBTYPEID").ToString <> "" Then
                        rdbPlbTypeName.SelectedValue = ds.Tables("BUSINESSCASE").Rows(0)("PLBTYPEID").ToString
                    End If


                    If ds.Tables("PLB") IsNot Nothing Then
                        txtPlbBonus.Text = ds.Tables("PLB").Rows(0)("PLBAMOUNT").ToString
                    End If

                    If ds.Tables("BUSINESSCASE").Rows(0)("PLBSLAB").ToString.ToUpper() = "TRUE" Then
                        chkPlb.Checked = True
                    Else
                        chkPlb.Checked = False
                    End If
                    'Code for Visiblity of Radiao button 
                    Raido_visiblity()


                    'Code for Visiblity of Radiao button 
                    If rdbPlbTypeName.SelectedValue.Trim() = "2" Then
                        If ds.Tables("PLB_DETAILS") IsNot Nothing Then
                            grdvPlbSlabN.DataSource = ds.Tables("PLB_DETAILS").DefaultView
                            grdvPlbSlabN.DataBind()
                        Else
                            grdvPlbSlabN.DataSource = Nothing
                            grdvPlbSlabN.DataBind()
                        End If
                    Else
                        grdvPlbSlabN.DataSource = Nothing
                        grdvPlbSlabN.DataBind()
                    End If




                    'Existing Incentive Part
                    If dsOld.Tables("INC_DETAILS").Rows.Count > 0 Then
                        txtIncRateO.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("RATE").ToString()).ToString("00")
                        txtIncExpectedSegO.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("EXP_SEGMENT").ToString()).ToString("00")
                        lblIncTotalCostO.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("TOTAL_COST").ToString()).ToString("00")
                        lblTSegTotalCostO.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("TOTAL_COST").ToString()).ToString("00") 'Need
                        lblIncCpsTotalO.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("TOTAL_COST").ToString()).ToString("00") 'Need

                        txtUpfrontAmountOld.Text = Val(dsOld.Tables("INC_DETAILS").Rows(0)("UPFRONT_AMOUNT").ToString()).ToString("00") 'Need

                    End If
                    'End of Existing Incentive Part

                    If dsOld.Tables("INC_TYPE").Rows(0)("INC_TYPE_ID").ToString = "" Then
                        tlbRadioOLD.Attributes.Add("class", "displayNone")
                        trOldCase.Visible = False
                    Else
                        trOldCase.Visible = True
                    End If
                    If dsOld.Tables("INC_TYPE").Rows(0)("INC_TYPE_ID").ToString <> "" Then
                        rdbIncentiveTypeOld.SelectedValue = dsOld.Tables("INC_TYPE").Rows(0)("INC_TYPE_ID").ToString
                    End If
                    If dsOld.Tables("INC_TYPE").Rows(0)("UPFRONTTYPEID").ToString <> "" Then
                        rdbUpfrontTypeNameOld.SelectedValue = dsOld.Tables("INC_TYPE").Rows(0)("UPFRONTTYPEID").ToString
                    End If


                    If dsOld.Tables("INC_TYPE").Rows(0)("PAYMENTTYPEID").ToString <> "" Then
                        rdbPaymentTypeOld.SelectedValue = dsOld.Tables("INC_TYPE").Rows(0)("PAYMENTTYPEID").ToString
                    End If

                    If dsOld.Tables("INC_TYPE").Rows(0)("PLBTYPEID").ToString <> "" Then
                        rdbPlbTypeNameOld.SelectedValue = dsOld.Tables("INC_TYPE").Rows(0)("PLBTYPEID").ToString

                    End If

                    ''added on 4 sep 10
                    If dsOld.Tables("INC_TYPE").Rows(0)("SIGNUPAMOUNT") IsNot Nothing Then
                        txtSignUpAmountOld.Text = dsOld.Tables("INC_TYPE").Rows(0)("SIGNUPAMOUNT").ToString
                        If dsOld.Tables("INC_TYPE").Rows(0)("ADJUSTABLE").ToString <> "" Then
                            chkAdjustableOld.Checked = dsOld.Tables("INC_TYPE").Rows(0)("ADJUSTABLE").ToString
                        End If

                        txtNoOfPaymentsOld.Text = dsOld.Tables("INC_TYPE").Rows(0)("NO_OF_PAYMENTS").ToString
                        txtForThePeriodOfOld.Text = dsOld.Tables("INC_TYPE").Rows(0)("FOR_PERIOD_OF").ToString
                    End If
                    ''end



                    If dsOld.Tables("INC_TYPE").Rows(0)("PLBSLAB").ToString.ToUpper() = "TRUE" Then
                        chkPlbOld.Checked = True
                    Else
                        chkPlbOld.Checked = False
                    End If


                    Raido_visiblityOld()


                    If dsOld.Tables("PLB_DETAILS") IsNot Nothing Then
                        grdvPlbSlabNOld.DataSource = dsOld.Tables("PLB_DETAILS").DefaultView
                        grdvPlbSlabNOld.DataBind()
                    End If


                    'Existing Fix Incentive Part
                    If dsOld.Tables("FI_DETAILS").Rows.Count > 0 Then
                        txtFixIncOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("FIXED_INCENTIVE").ToString()).ToString("00") 'Need
                        txtFixIncMonthOld.Text = dsOld.Tables("FI_DETAILS").Rows(0)("MONTHS").ToString 'Need
                        lblFixIncPerMonthOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("INCETIVE_PER_MONTH").ToString()).ToString("00") 'Need
                        lblFixIncTotalCostOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("TOTAL_COST").ToString()).ToString("00") 'Need
                        lblFixIncMinMSegOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("MIN_MON_SEGMENT").ToString()) '.ToString("00") 'Need
                        lblFixIncCPSOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("CPS").ToString()).ToString("00") 'Need
                        lblFixIncGrossOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("GROSS").ToString()).ToString("00") 'Need
                        lblFixIncICITOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("EXCL_IC_IT").ToString()).ToString("00") 'Need
                        lblFixIncNetOld.Text = Val(dsOld.Tables("FI_DETAILS").Rows(0)("NET").ToString()).ToString("00") 'Need


                        'txtSoleAmtOld.Text = dsOld.Tables("INC_DETAILS").Rows(0)("SOLE_AMOUNT").ToString
                        'txtBonusOld.Text = dsOld.Tables("INC_DETAILS").Rows(0)("BONUS_AMOUNT").ToString
                        'txtFixPaymentOld.Text = dsOld.Tables("INC_DETAILS").Rows(0)("FIXED_PAYMENT").ToString
                        'txtFixUpfrontOld.Text = dsOld.Tables("INC_DETAILS").Rows(0)("UPFRONT_AMOUNT").ToString
                        'txtSoleAmtBonusOld.Text = dsOld.Tables("INC_DETAILS").Rows(0)("SOLE_AMOUNT").ToString

                    End If
                    'End of Existing Fix Incentive Part



                    If ds.Tables("BUSINESSCASE").Rows(0)("APPROVERS").ToString().Trim() = "" Then
                        btnSendforApproval.Enabled = True

                        btnSendforApproval.ImageUrl = "~/Images/Btn_SendForApprovel.jpg"
                    Else
                        btnSendforApproval.Enabled = False

                        btnSendforApproval.ImageUrl = "~/Images/Btn_SendForApprovel_temp.jpg"
                    End If

                    '@ Start of Added Code By Abhishek on 14/04/2011

                    If ds.Tables("BUSINESSCASE").Columns("IS_APPROVER") IsNot Nothing Then
                        If ds.Tables("BUSINESSCASE").Rows(0)("IS_APPROVER").ToString().Trim().ToUpper = "TRUE" Then
                            HdIsApproval.Value = "TRUE"
                        Else
                            HdIsApproval.Value = ""
                        End If
                    Else
                        HdIsApproval.Value = ""
                    End If
                    If ds.Tables("BUSINESSCASE").Columns("IS_FINAL_APPROVER") IsNot Nothing Then
                        If ds.Tables("BUSINESSCASE").Rows(0)("IS_FINAL_APPROVER").ToString().Trim().ToUpper = "TRUE" Then
                            HdIsFinalApproval.Value = "TRUE"
                        Else
                            HdIsFinalApproval.Value = ""
                        End If
                    Else
                        HdIsFinalApproval.Value = ""
                    End If
                    If ds.Tables("BUSINESSCASE").Rows(0)("APPROVERS").ToString().Trim() = "" Then
                       
                        BtnReject.ImageUrl = "~/Images/btn_Reject_temp.jpg"
                        BtnReject.Enabled = False
                        BtnApproved.Enabled = False
                        BtnApproved.ImageUrl = "~/Images/Btn_Approved_temp.jpg"
                        BtnFinnallyApproved.Enabled = False
                        BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved_temp.jpg"
                    Else
                        BtnReject.ImageUrl = "~/Images/btn_Reject.jpg"
                        BtnReject.Enabled = True
                        EnableDisableApprovers()

                    End If

                  
                    '@ End of Added Code By Abhishek on 14/04/2011

                    If ds.Tables("BUSINESSCASE").Rows(0)("FINAL_APPROVED").ToString().Trim().ToUpper() = "TRUE" Then
                        btnSave.Enabled = False
                        BtnFinnallyApproved.Enabled = False
                        BtnReject.Enabled = False
                        BtnApproved.Enabled = False
                        btnRefresh.Enabled = False

                        btnSave.ImageUrl = "~/Images/btn_Save_temp.jpg"
                        BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved_temp.jpg"
                        BtnReject.ImageUrl = "~/Images/btn_Reject_temp.jpg"
                        BtnApproved.ImageUrl = "~/Images/Btn_Approved_temp.jpg"
                        btnRefresh.ImageUrl = "~/Images/btn_Refresh_temp.jpg"

                    Else
                        btnSave.Enabled = True
                        btnSave.ImageUrl = "~/Images/btn_Save.jpg"
                        checksecurity()  ' if modification right in edit mode then they can save 
                    End If
                End If
                '********Binding Header Information************


                '*********Group MIDT***********
                If ds.Tables("BC_MIDT_GROUP") IsNot Nothing Then
                    hdTotalMIDT.Value = ds.Tables("BC_MIDT_GROUP").Rows(0)("TOTAL").ToString()
                    hdBreakupFromDt.Value = ds.Tables("BC_MIDT_GROUP").Rows(0)("FROMDATE").ToString()
                    hdBreakupToDt.Value = ds.Tables("BC_MIDT_GROUP").Rows(0)("TODATE").ToString()
                    GvBGroupMIDT.DataSource = ds.Tables("BC_MIDT_GROUP")
                    GvBGroupMIDT.DataBind()
                Else
                    GvBGroupMIDT.DataSource = Nothing
                    GvBGroupMIDT.DataBind()
                End If
                '*********Group MIDT***********

                '******* Code for Binding Connectivity*******
                If blnConn = True Then


                    If dsOld.Tables("BC_CONN") IsNot Nothing Then
                        GvConnectivity.DataSource = Nothing
                        GvConnectivity.DataBind()

                        GvConnectivity.DataSource = dsOld.Tables("BC_CONN").DefaultView
                        GvConnectivity.DataBind()
                    Else
                        GvConnectivity.DataSource = Nothing
                        GvConnectivity.DataBind()
                    End If
                Else
                    GvConnectivity.DataSource = Nothing
                    GvConnectivity.DataBind()
                End If
                '******* End of Code for Binding Connectivity*******



                '******* Code for Binding Equipment Details*******
                If blnHw = True Then
                    If dsOld.Tables("BC_EQP") IsNot Nothing Then
                        GvHardware.DataSource = dsOld.Tables("BC_EQP").DefaultView
                        GvHardware.DataBind()
                    Else
                        GvHardware.DataSource = Nothing
                        GvHardware.DataBind()
                    End If
                Else
                    GvHardware.DataSource = Nothing
                    GvHardware.DataBind()
                End If
                '******* End of Code for Binding Equipment Details*******




                '******* Code for Binding Incentive Plan Details*************
                If objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value.Trim().ToUpper() = "TRUE" Then
                    If ds.Tables("CASE") IsNot Nothing Then
                        grdvIncentivePlan.DataSource = ds.Tables("CASE").DefaultView
                        grdvIncentivePlan.DataBind()
                    Else
                        grdvIncentivePlan.DataSource = Nothing
                        grdvIncentivePlan.DataBind()
                    End If
                Else
                    grdvIncentivePlan.DataSource = Nothing
                    grdvIncentivePlan.DataBind()
                End If


                If ds.Tables("BC_MIDT_BRK") IsNot Nothing Then
                    If (objOutPutxml.DocumentElement.SelectNodes("BC_MIDT_BRK[@TYPE='']").Count <> objOutPutxml.DocumentElement.SelectNodes("BC_MIDT_BRK").Count) Then
                        grdvBreakUpLast.DataSource = ds.Tables("BC_MIDT_BRK").DefaultView
                        grdvBreakUpLast.DataBind()
                    Else
                        grdvBreakUpLast.DataSource = Nothing
                        grdvBreakUpLast.DataBind()
                    End If

                    Dim objNode As XmlNode = objOutPutxml.DocumentElement.SelectSingleNode("BC_MIDT_BRK[@TYPE='IC']")
                    If objNode IsNot Nothing Then
                        hdICvalue.Value = objNode.Attributes("LESS").Value.Trim()
                    Else
                        hdICvalue.Value = "0"
                    End If
                    objNode = Nothing

                    objNode = objOutPutxml.DocumentElement.SelectSingleNode("BC_MIDT_BRK[@TYPE='IT']")
                    If objNode IsNot Nothing Then
                        hdITvalue.Value = objNode.Attributes("LESS").Value.Trim()
                    Else
                        hdITvalue.Value = "0"
                    End If

                End If

                ' Session("BC_MIDT_BRK") = objOutPutxml.DocumentElement.SelectSingleNode("BC_PLAN").OuterXml
                Dim objBC_MIDT_BRK As New XmlDocument
                objBC_MIDT_BRK.LoadXml("<BC_MIDT_BRK><GRANDTOTAL TOTAL=''/></BC_MIDT_BRK>")
                For Each objNode As XmlNode In objOutPutxml.DocumentElement.SelectNodes("BC_MIDT_BRK")
                    objBC_MIDT_BRK.DocumentElement.AppendChild(objBC_MIDT_BRK.ImportNode(objNode, True))
                Next
                Session("BC_MIDT_BRK") = objBC_MIDT_BRK.OuterXml

                If objOutPutxmlOld.DocumentElement.SelectSingleNode("BC_PLAN_OLD/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value.Trim().ToUpper() = "TRUE" Then
                    If dsOld.Tables("CASE") IsNot Nothing Then
                        grdvIncentivePlanOld.DataSource = dsOld.Tables("CASE").DefaultView
                        grdvIncentivePlanOld.DataBind()
                    Else
                        grdvIncentivePlanOld.DataSource = Nothing
                        grdvIncentivePlanOld.DataBind()
                    End If
                Else
                    grdvIncentivePlanOld.DataSource = Nothing
                    grdvIncentivePlanOld.DataBind()
                End If


                '***************Fix Incentive calculation **********************
                'For Fix Incentive
                Dim convPercentage As Decimal = 0
                If Val(txtConversionPer.Text.Trim()) <> 0 Then
                    convPercentage = (Val(hdTotalMIDT.Value.ToString()) * (Val(txtConversionPer.Text.Trim()) / 100)) * 0.9
                Else
                    convPercentage = 0
                End If

                '########################################################
                '@ Start of Commented By Abhishek

                'If txtFixIncentive.Text.Trim().Length <> 0 And txtFixIncMonth.Text.Trim().Length <> 0 Then
                '    lblFixIncPerMonth.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim()))).ToString("00")
                'Else
                '    lblFixIncPerMonth.Text = "0"
                'End If


                'If (totalHardware + totalConn + Val(lblFixIncPerMonth.Text)) > 0 Then
                '    lblFixIncCPS.Text = ((totalHardware + totalConn + Val(lblFixIncPerMonth.Text))).ToString("00")
                'Else
                '    lblFixIncCPS.Text = "0"
                'End If




                'If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICITValNew.Text) > 0 Then
                '    lblFixIncICIT.Text = (Val(lblFixIncTotalCost.Text) / Val(lblSegLessICITValNew.Text)).ToString("00")
                'Else
                '    lblFixIncICIT.Text = "0"
                'End If


                'If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICValNew.Text) > 0 Then
                '    lblFixIncIC.Text = (Val(lblFixIncTotalCost.Text) / Val(lblSegLessICValNew.Text)).ToString("00")
                'Else
                '    lblFixIncIC.Text = "0"
                'End If



                'If Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                '    lblFixIncNet.Text = ((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))).ToString("00")
                'Else
                '    lblFixIncNet.Text = "0"
                'End If

                'If Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                '    lblFixIncCPS.Text = ((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))).ToString("00")
                'Else
                '    lblFixIncCPS.Text = "0"
                'End If



                'If lblFixIncTotalCost.Text.Trim().Length <> 0 And hdTotalMIDT.Value.Trim().Length <> 0 And convPercentage <> 0 Then
                '    lblFixIncGross.Text = (Val(lblFixIncTotalCost.Text) / convPercentage).ToString("00")
                'Else
                '    lblFixIncGross.Text = "0"
                'End If



                'lblTotalCostNew.Text = Val(lblIncTotalCostN.Text).ToString("00")  'Total Cost Footer New
                'txtMinMonthSegNew.Text = Val(txtFixIncMinMonthSeg.Text).ToString("00")

                'If lblFixIncGross.Text.Trim.Length <> 0 Then
                '    lblGrossNew.Text = Val(lblFixIncGross.Text).ToString("00")
                'Else
                '    lblGrossNew.Text = "0"
                'End If


                ''lblGrossNew
                'If Val(lblFixIncNet.Text) > 0 Then
                '    lblNetNew.Text = Val(lblFixIncNet.Text).ToString("00")
                'Else
                '    lblNetNew.Text = "0"
                'End If

                ''Fix Incentive for New


                'Dim ICvalue As Double = 0
                'Dim ITvalue As Double = 0
                'Dim ITotalMIDT As Double = 0


                'ICvalue = Val(hdICvalue.Value.Trim())


                'ITotalMIDT = Val(hdTotalMIDT.Value.Trim())

                'lblSegLessICValNew.Text = (convPercentage - ICvalue).ToString("00")
                'If Val(lblSegLessICValNew.Text) > 0 Then
                '    lblSegLessICPerNew.Text = ((Val(lblSegLessICValNew.Text) * 100) / convPercentage).ToString("00")

                'Else
                '    lblSegLessICPerNew.Text = "0"
                'End If


                'ITvalue = Val(hdITvalue.Value.Trim())





                'If ITotalMIDT > 0 Then
                '    lblIncRateGrossPer.Text = (ITvalue / ITotalMIDT).ToString("00")
                'Else
                '    lblIncRateGrossPer.Text = "0"
                'End If


                '' If lblFixIncPerMonth.Text.Trim() <> "" And lblSegLessICValNew.Text.Trim() <> "" Then
                'If (Val(txtFixIncentive.Text.Trim()) > 0) And (Val(txtFixIncMonth.Text.Trim()) > 0) And (Val(lblSegLessICValNew.Text.Trim()) <> 0) Then

                '    ' If Val(lblFixIncPerMonth.Text.Trim()) > 0 And Val(lblSegLessICValNew.Text) > 0 Then
                '    lblIncRateWOIC.Text = (Val(lblFixIncPerMonth.Text.Trim() / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICValNew.Text.Trim())).ToString("00")
                'Else
                '    lblIncRateWOIC.Text = "0"
                '    'End If

                'End If

                '' If (Val(txtFixIncentive.Text.Trim()) > 0) And (convPercentage > 0) Then
                'If (Val(txtFixIncentive.Text.Trim()) > 0) And (Val(txtFixIncMonth.Text.Trim()) > 0) And (Val(lblSegLessICITValNew.Text.Trim()) <> 0) Then

                '    'If Val(lblFixIncPerMonth.Text.Trim()) > 0 And Val(lblSegLessICITValNew.Text.Trim()) > 0 Then
                '    lblIncRateWOICIT.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICITValNew.Text.Trim())).ToString("00")
                'Else
                '    lblIncRateWOICIT.Text = "0"
                '    'End If

                'End If




                'If Val(txtFixIncMonth.Text.Trim()) > 0 And Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                '    lblIncRateNet.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(txtFixIncMinMonthSeg.Text.Trim())).ToString("00")
                'Else
                '    lblIncRateNet.Text = "0"
                'End If

                'lblTotalCostNew.Text = (totalHardware + totalConn).ToString("00")





                'If Val(txtMinMonthSegNew.Text.Trim()) > 0 Then
                '    lblNetNew.Text = ((totalHardware + totalConn) / Convert.ToDouble(txtMinMonthSegNew.Text.Trim())).ToString("00")
                'Else
                '    lblNetNew.Text = "0"
                'End If


                'If Val(lblSegLessICITValNew.Text) > 0 Then
                '    lblCPSMultiRateICITNew.Text = (Val(lblTotalCostNew.Text) / Val(lblSegLessICITValNew.Text)).ToString("00")
                'Else
                '    lblCPSMultiRateICITNew.Text = "0"
                'End If


                'If Val(txtMinMonthSegNew.Text.Trim()) > 0 Then
                '    lblNetNew.Text = (Val(lblTotalCostNew.Text.Trim()) / Val(txtMinMonthSegNew.Text.Trim())).ToString("00")
                'Else
                '    lblNetNew.Text = "00"
                'End If


                '@ End ofCoomentd By Abhishek
                '#############################################################
                txtMinMonthSegNew.Text = txtFixIncMinMonthSeg.Text

                CalculateFormula(convPercentage)


                '******* End ofCode for Binding Incentive Plan Details*******



            Else
                GvBGroupMIDT.DataSource = Nothing
                GvBGroupMIDT.DataBind()
                GvConnectivity.DataSource = Nothing
                GvConnectivity.DataBind()
                GvHardware.DataSource = Nothing
                GvHardware.DataBind()
                Session("INCENTIVEPLAN") = "<BC_PLAN></BC_PLAN>"
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

            '@ End of Code  for Details

            BindGrid(Grid.All)

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub




    Private Sub Raido_visiblity()
        Try
            'Display Radio
            If rdbIncentiveType.SelectedValue = "1" Then
                trUpfrontTypeName.Attributes.Add("class", "displayBlock")
                trUpfrontAmount.Attributes.Add("class", "displayBlock")

                TRMinimumSegmentCriteriaNew.Attributes.Add("class", "displayNone")
                TblMinimumSegmentCriteriaNew.Attributes.Add("class", "displayNone")
               

            Else
                trUpfrontTypeName.Attributes.Add("class", "displayNone")
                trUpfrontAmount.Attributes.Add("class", "displayNone")
                TRMinimumSegmentCriteriaNew.Attributes.Add("class", "redborder displayBlock")
                TblMinimumSegmentCriteriaNew.Attributes.Add("class", "redborder displayBlock")
               

            End If

            If chkPlb.Checked Then
                trPlbTypeName.Attributes.Add("class", "displayBlock")
                trSlabPlb.Attributes.Add("class", "displayBlock")
            Else
                trPlbTypeName.Attributes.Add("class", "displayNone")
                trSlabPlb.Attributes.Add("class", "displayNone")
            End If

            If chkPlb.Checked Then
                If rdbPlbTypeName.SelectedValue.Trim() = "2" Then
                    trSlabPlb.Attributes.Add("class", "displayBlock")
                    trPlbAmount.Attributes.Add("class", "displayNone")
                Else
                    trSlabPlb.Attributes.Add("class", "displayNone")
                    trPlbAmount.Attributes.Add("class", "displayBlock")
                End If
            Else
                trSlabPlb.Attributes.Add("class", "displayNone")
                trPlbAmount.Attributes.Add("class", "displayNone")
            End If
            'End of Display Radia
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub Raido_visiblityOld()
        Try
            'Display Radio
            If rdbIncentiveTypeOld.SelectedValue = "1" Then
                trUpfrontTypeNameOld.Attributes.Add("class", "displayBlock")
                trUpfrontAmountOld.Attributes.Add("class", "displayBlock")
                TRMinimumSegmentCriteriaOld.Attributes.Add("class", "displayNone")
                TblMinimumSegmentCriteriaOld.Attributes.Add("class", "displayNone")
            Else
                trUpfrontTypeNameOld.Attributes.Add("class", "displayNone")
                trUpfrontAmountOld.Attributes.Add("class", "displayNone")
                TRMinimumSegmentCriteriaOld.Attributes.Add("class", "redborder displayBlock")
                TblMinimumSegmentCriteriaOld.Attributes.Add("class", "redborder displayBlock")
            End If

            If chkPlbOld.Checked Then
                trPlbTypeNameOld.Attributes.Add("class", "displayBlock")
                trSlabPlbOld.Attributes.Add("class", "displayBlock")
            Else
                trPlbTypeNameOld.Attributes.Add("class", "displayNone")
                trSlabPlbOld.Attributes.Add("class", "displayNone")
            End If

            If chkPlbOld.Checked Then
                If rdbPlbTypeNameOld.SelectedValue.Trim() = "2" Then
                    trSlabPlbOld.Attributes.Add("class", "displayBlock")
                    trPlbAmountOld.Attributes.Add("class", "displayNone")
                Else
                    trSlabPlbOld.Attributes.Add("class", "displayNone")
                    trPlbAmountOld.Attributes.Add("class", "displayBlock")
                End If
            Else
                trSlabPlbOld.Attributes.Add("class", "displayNone")
                trPlbAmountOld.Attributes.Add("class", "displayNone")
            End If
            'End of Display Radia
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub btnSaveRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRemarks.Click
    '    Try
    '        hdTabID.Text = "2"

    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
    '        objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE_REMARKS BC_ID='' BC_REMARKS_TYPE='' BC_REMARKS='' EmployeeID=''/></INC_UPDATE_BUSINESSCASE_INPUT>")

    '        If txtRemarks.Text.Trim().Length = 0 Then
    '            lblError.Text = "Please fill Remarks"
    '            txtRemarks.Focus()
    '            Return
    '        End If
    '        If Session("Security") IsNot Nothing Then
    '            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
    '        End If
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS_TYPE").Value = "1" ' drpRemarksType.SelectedValue.Trim()
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS").Value = txtRemarks.Text
    '        If hdBcID.Value.Trim() = "" Then
    '            lblError.Text = "Please Save a Business Case"
    '            Return
    '        End If
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value = hdBcID.Value


    '        objOutputXml = objRmrks.UpdateRemarks(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            lblError.Text = "Remarks Added successfuly"
    '            txtRemarks.Text = ""
    '            hdBcID.Value = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value
    '            GetAllRemrks(hdBcID.Value.Trim)
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Private Sub GetAllRemrks(ByVal strBcID As String)
    '    Try
    '        Dim objInputXml, objOutputXml, objOutputXmlMarket, objOutputXmlRegion As New XmlDocument
    '        Dim objRdrNode As XmlNodeReader
    '        Dim ds As New DataSet
    '        Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
    '        objInputXml.LoadXml("<INC_GETBUSINESSREMARK_INPUT><BC_ID></BC_ID></INC_GETBUSINESSREMARK_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcID
    '        objOutputXml = objRmrks.GetRemarks(objInputXml)
    '        'objOutputXml.DocumentElement.SelectNodes("BUSINESSREMARK[@BC_REMARKS_TYPE='Market Remark']")
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            objRdrNode = New XmlNodeReader(objOutputXml)
    '            ds.ReadXml(objRdrNode)
    '            Dim dvR As DataView = ds.Tables("BUSINESSREMARK").DefaultView
    '            dvR.RowFilter = "BC_REMARKS_TYPE='Region Remark'"
    '            grdvRemarks.DataSource = dvR 'ds.Tables("BUSINESSREMARK").DefaultView
    '            grdvRemarks.DataBind()

    '            Dim dvM As DataView = ds.Tables("BUSINESSREMARK").DefaultView
    '            dvM.RowFilter = "BC_REMARKS_TYPE='Market Remark'"
    '            grdvMarketRemarks.DataSource = dvM 'ds.Tables("BUSINESSREMARK").DefaultView
    '            grdvMarketRemarks.DataBind()

    '        Else
    '            '  lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            scrollbarSetting()
            TablSelection()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub grdApprovars_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdApprovars.RowDataBound
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim lblLevel As Label = CType(e.Row.FindControl("lblLevel"), Label)

    '            Dim lblEmpName As Label = CType(e.Row.FindControl("lblApprovars"), Label)
    '            If Not String.IsNullOrEmpty(lblEmpName.Text) Then
    '                lblLevel.Text = "Level " + (e.Row.RowIndex + 1).ToString()
    '            End If

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub





    'Protected Sub btnMktRemarksSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMktRemarksSave.Click
    '    Try
    '        hdTabID.Text = "3"
    '        Dim objInputXml, objOutputXml As New XmlDocument
    '        Dim objRmrks As New AAMS.bizIncetive.bzBusinessCase
    '        objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE_REMARKS BC_ID='' BC_REMARKS_TYPE='' BC_REMARKS='' EmployeeID=''/></INC_UPDATE_BUSINESSCASE_INPUT>")

    '        If txtMktRemarks.Text.Trim().Length = 0 Then
    '            lblError.Text = "Please fill Remarks"
    '            txtMktRemarks.Focus()
    '            Return
    '        End If
    '        If Session("Security") IsNot Nothing Then
    '            objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
    '        End If
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS_TYPE").Value = "2" ' drpRemarksType.SelectedValue.Trim()
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_REMARKS").Value = txtMktRemarks.Text
    '        If hdBcID.Value.Trim() = "" Then
    '            lblError.Text = "Please Save a Business Case"
    '            Return
    '        End If
    '        objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value = hdBcID.Value


    '        objOutputXml = objRmrks.UpdateRemarks(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            lblError.Text = "Remarks Added successfuly"
    '            txtMktRemarks.Text = ""
    '            hdBcID.Value = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE_REMARKS").Attributes("BC_ID").Value
    '            GetAllRemrks(hdBcID.Value.Trim)
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub btnResetRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetRemarks.Click

    'End Sub

    'Protected Sub btnResetMarketRemarks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetMarketRemarks.Click

    'End Sub

    Private Sub bindChkListForShowModal()
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objNode As XmlNode
            Dim strMIDT As String = ""

            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())
            Else
                objIncentiveTempXml.LoadXml("<INC_VIEW_BUSINESSCASE_OUTPUT><CASE INC_PLAN_ID='' INC_PLAN_NAME=''><NIDT_FIELDS_ID ID='' /></CASE></INC_VIEW_BUSINESSCASE_OUTPUT >")
            End If

            For Each objNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE")
                ' If objNode.Attributes("TEMP_SLAB_ID_MAIN").Value.Trim() <> hdMidtGvClick.Value.Trim() Then
                If String.IsNullOrEmpty(strMIDT) Then
                    strMIDT = objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                Else
                    strMIDT = strMIDT + "," + objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                End If
                '  End If
            Next


            'Code for Template Criteria Removal
            'For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
            '    If chkSelectedCriteria.Items(i).Selected Then
            '        If String.IsNullOrEmpty(strMIDT) Then
            '            strMIDT = chkSelectedCriteria.Items(i).Value
            '        Else
            '            strMIDT = strMIDT + "," + chkSelectedCriteria.Items(i).Value
            '        End If
            '    End If
            'Next
            'End of Code for Template Criteria Removal


            Dim objArrayList As New ArrayList(strMIDT.Split(","))
            Dim objHsTab As New Hashtable

            For i As Integer = 0 To objArrayList.Count - 1
                If Not objHsTab.Contains(objArrayList(i).ToString().Trim()) Then
                    objHsTab.Add(objArrayList(i).ToString().Trim(), objArrayList(i).ToString().Trim())
                End If
            Next



            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet

            objOutputXml = objbzBusinessCase.List_NIDTFields()



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("AIRLINE_RAW_DATA") = objOutputXml.OuterXml

                For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                    If objHsTab.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                        objOutputXml.DocumentElement.RemoveChild(objNode)
                    End If
                Next

                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)

                chklstCriteria.Items.Clear()
                chklstCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chklstCriteria.DataTextField = "NIDT_FIELDS_NAME"
                chklstCriteria.DataValueField = "NIDT_FIELDS_ID"
                chklstCriteria.DataBind()
                If chklstCriteria.Items.Count = 0 Then
                    lblChkMsg.Visible = True
                    lblChkMsg.Text = "No Criteria Available for Selection"
                    btnCriteriaSelection.Visible = False
                Else
                    lblChkMsg.Visible = False
                    lblChkMsg.Text = ""
                    btnCriteriaSelection.Visible = True
                End If

            Else

            End If

            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                For ii As Integer = 0 To chklstCriteria.Items.Count - 1
                    If chklstCriteria.Items(ii).Value.Trim() = chkSelectedCriteria.Items(i).Value.Trim() Then
                        chklstCriteria.Items(ii).Selected = True
                    End If
                Next
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub bindChkListForTemplate()
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objNode As XmlNode
            Dim strMIDT As String = ""

            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())
            Else
                Return
            End If

            Try
                For Each objNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE")
                    If String.IsNullOrEmpty(strMIDT) Then
                        strMIDT = objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                    Else
                        strMIDT = strMIDT + "," + objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                    End If
                Next
            Catch ex As Exception
                strMIDT = ""
            End Try


            'For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
            '    If chkSelectedCriteria.Items(i).Selected Then
            '        If String.IsNullOrEmpty(strMIDT) Then
            '            strMIDT = chkSelectedCriteria.Items(i).Value
            '        Else
            '            strMIDT = strMIDT + "," + chkSelectedCriteria.Items(i).Value
            '        End If
            '    End If
            'Next

            If String.IsNullOrEmpty(strMIDT) Then
                Return
            End If
            Dim objArrayList As New ArrayList(strMIDT.Split(","))
            Dim objHsTab As New Hashtable

            For i As Integer = 0 To objArrayList.Count - 1
                If Not objHsTab.Contains(objArrayList(i).ToString().Trim()) Then
                    objHsTab.Add(objArrayList(i).ToString().Trim(), objArrayList(i).ToString().Trim())
                End If
            Next



            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet

            objOutputXml = objbzBusinessCase.List_NIDTFields()


            Dim objOutputBackup As New XmlDocument
            objOutputBackup.LoadXml(objOutputXml.OuterXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Dim objArrayList As New ArrayList(strMIDT.Split(","))

                For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                    If objHsTab.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                        objOutputXml.DocumentElement.RemoveChild(objNode)
                    End If
                Next

                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)

                chklstCriteria.DataSource = Nothing
                chklstCriteria.DataBind()

                chklstCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chklstCriteria.DataTextField = "NIDT_FIELDS_NAME"
                chklstCriteria.DataValueField = "NIDT_FIELDS_ID"
                chklstCriteria.DataBind()

                If chklstCriteria.Items.Count = 0 Then
                    lblChkMsg.Visible = True
                    lblChkMsg.Text = "No Criteria Available for Selection"
                    btnCriteriaSelection.Visible = False
                Else
                    lblChkMsg.Visible = False
                    lblChkMsg.Text = ""
                    btnCriteriaSelection.Visible = True
                End If




                ''Removing All except current row selection criteria
                'objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']")
                'If objNode IsNot Nothing Then
                '    Dim objArrayListCurrent As New ArrayList(objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(","))
                '    For iSelected As Integer = 0 To objArrayListCurrent.Count - 1
                '        For i As Integer = 0 To objArrayList.Count - 1
                '            If objArrayList(i).ToString().Trim() = objArrayListCurrent(iSelected).ToString().Trim() Then
                '                Try
                '                    objHsTab.Remove(objArrayListCurrent(iSelected).ToString().Trim())
                '                Catch ex As Exception
                '                End Try
                '            End If
                '        Next
                '    Next
                'End If
                ''Removing All except current row selection criteria



                'For Each objNode In objOutputBackup.DocumentElement.SelectNodes("NIDTFIELDS")
                '    If objHsTab.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                '        objOutputBackup.DocumentElement.RemoveChild(objNode)
                '    End If
                'Next

                'dsCriteria = New DataSet
                'Dim objRdr As XmlNodeReader
                'objRdr = New XmlNodeReader(objOutputBackup)
                'dsCriteria.ReadXml(objRdr)

                'If objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS").Count = 0 Then
                '    lblError.Text = "Please Select At least One Criteria"
                '    ModalPopupExtender1.Show()
                '    Return
                'End If


                'chkLstGvShowMIDT.DataSource = dsCriteria.Tables("NIDTFIELDS")
                'chkLstGvShowMIDT.DataTextField = "NIDT_FIELDS_NAME"
                'chkLstGvShowMIDT.DataValueField = "NIDT_FIELDS_ID"
                'chkLstGvShowMIDT.DataBind()

                'objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']")
                'Dim objArrayToSelect As New ArrayList(objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(","))

                'For a As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                '    For b As Integer = 0 To objArrayToSelect.Count - 1
                '        If chkLstGvShowMIDT.Items(a).Value.Trim() = objArrayToSelect(b).ToString().Trim() Then
                '            chkLstGvShowMIDT.Items(a).Selected = True
                '        End If
                '    Next
                'Next

                'For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                '    For ii As Integer = 0 To chklstCriteria.Items.Count - 1
                '        If chklstCriteria.Items(ii).Value.Trim() = chkSelectedCriteria.Items(i).Value.Trim() Then
                '            chklstCriteria.Items(ii).Selected = True
                '        End If
                '    Next
                'Next

            Else

            End If
            'End of Binding Cases


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub bindChkListForGrid()
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objNode As XmlNode
            Dim strMIDT As String = ""

            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())
            Else
                lblError.Text = "Invalid Action"
                Return
            End If
            For Each objNode In objIncentiveTempXml.DocumentElement.SelectNodes("CASE")
                ' If objNode.Attributes("TEMP_SLAB_ID_MAIN").Value.Trim() <> hdMidtGvClick.Value.Trim() Then
                If String.IsNullOrEmpty(strMIDT) Then
                    strMIDT = objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                Else
                    strMIDT = strMIDT + "," + objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim()
                End If
                '  End If
            Next





            Dim objArrayList As New ArrayList(strMIDT.Split(","))
            Dim objHsTab As New Hashtable

            For i As Integer = 0 To objArrayList.Count - 1
                If Not objHsTab.Contains(objArrayList(i).ToString().Trim()) Then
                    objHsTab.Add(objArrayList(i).ToString().Trim(), objArrayList(i).ToString().Trim())
                End If
            Next


            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                If chkSelectedCriteria.Items(i).Selected Then
                    ' If Not objHsTab.Contains(objArrayList(i).ToString().Trim()) Then
                    Try
                        objHsTab.Add(chkSelectedCriteria.Items(i).Value, chkSelectedCriteria.Items(i).Value)
                    Catch ex As Exception
                        lblError.Text = ex.Message
                    End Try
                    'End If
                End If
            Next



            ''Binding Cases
            If strMIDT.Length = 0 Then
                lblError.Text = "Please Select At least One Criteria"
                ModalPopupExtender1.Show()
                Return
            End If

            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet

            objOutputXml = objbzBusinessCase.List_NIDTFields()


            Dim objOutputBackup As New XmlDocument
            objOutputBackup.LoadXml(objOutputXml.OuterXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Dim objArrayList As New ArrayList(strMIDT.Split(","))


                'if objArrayList.Count >0 then
                For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                    If objHsTab.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                        objOutputXml.DocumentElement.RemoveChild(objNode)
                    End If
                Next

                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)

                'chklstCriteria.DataSource = Nothing
                'chklstCriteria.DataBind()


                'chklstCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                'chklstCriteria.DataTextField = "NIDT_FIELDS_NAME"
                'chklstCriteria.DataValueField = "NIDT_FIELDS_ID"
                'chklstCriteria.DataBind()



                If chklstCriteria.Items.Count = 0 Then
                    lblChkMsg.Visible = True
                    lblChkMsg.Text = "No Criteria Available for Selection"
                    btnCriteriaSelection.Visible = False
                Else
                    lblChkMsg.Visible = False
                    lblChkMsg.Text = ""
                    btnCriteriaSelection.Visible = True
                End If




                'Removing All except current row selection criteria
                objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']")
                If objNode IsNot Nothing Then
                    Dim objArrayListCurrent As New ArrayList(objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(","))
                    For iSelected As Integer = 0 To objArrayListCurrent.Count - 1
                        For i As Integer = 0 To objArrayList.Count - 1
                            If objArrayList(i).ToString().Trim() = objArrayListCurrent(iSelected).ToString().Trim() Then
                                Try
                                    objHsTab.Remove(objArrayListCurrent(iSelected).ToString().Trim())
                                Catch ex As Exception
                                    lblError.Text = ex.Message
                                End Try
                            End If
                        Next
                    Next
                End If
                'Removing All except current row selection criteria



                For Each objNode In objOutputBackup.DocumentElement.SelectNodes("NIDTFIELDS")
                    If objHsTab.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                        objOutputBackup.DocumentElement.RemoveChild(objNode)
                    End If
                Next

                dsCriteria = New DataSet
                Dim objRdr As XmlNodeReader
                objRdr = New XmlNodeReader(objOutputBackup)
                dsCriteria.ReadXml(objRdr)

                'If objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS").Count = 0 Then
                '    lblError.Text = "Please Select At least One Criteria"
                '    ModalPopupExtender1.Show()
                '    Return
                'End If


                chkLstGvShowMIDT.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkLstGvShowMIDT.DataTextField = "NIDT_FIELDS_NAME"
                chkLstGvShowMIDT.DataValueField = "NIDT_FIELDS_ID"
                chkLstGvShowMIDT.DataBind()

                objNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']")
                Dim objArrayToSelect As New ArrayList(objNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Trim().Split(","))

                For a As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                    For b As Integer = 0 To objArrayToSelect.Count - 1
                        If chkLstGvShowMIDT.Items(a).Value.Trim() = objArrayToSelect(b).ToString().Trim() Then
                            chkLstGvShowMIDT.Items(a).Selected = True
                        End If
                    Next
                Next

                'For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                '    chkSelectedCriteria.Items(i).Selected = True
                'Next
            Else

            End If
            'End of Binding Cases


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub btnCriteriaSelection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCriteriaSelection.Click
        Try

            Dim objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objNode As XmlNode
            Dim strMIDT As String = ""
            For i As Integer = 0 To chklstCriteria.Items.Count - 1
                If chklstCriteria.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMIDT) Then
                        strMIDT = chklstCriteria.Items(i).Value
                    Else
                        strMIDT = strMIDT + "," + chklstCriteria.Items(i).Value
                    End If
                End If
            Next
            'Binding Cases
            'If strMIDT.Length = 0 Then
            '    lblChkMsg.Visible = True
            '    lblPopupError.Text = "Please Select At least One Criteria"
            '    ModalPopupExtender2.Show()
            '    Return
            'Else
            '    lblChkMsg.Visible = False
            '    lblChkMsg.Text = ""
            'End If

            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet

            objOutputXml = objbzBusinessCase.List_NIDTFields()





            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" And strMIDT.Length <> 0 Then
                Dim objArrayList As New ArrayList(strMIDT.Split(","))

                For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                    If Not objArrayList.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                        objOutputXml.DocumentElement.RemoveChild(objNode)
                    End If
                Next

                If objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS").Count = 0 Then
                    lblError.Text = "Please Select At least One Criteria"
                    ModalPopupExtender1.Show()
                    Return
                End If
                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)
                chkSelectedCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkSelectedCriteria.DataTextField = "NIDT_FIELDS_NAME"
                chkSelectedCriteria.DataValueField = "NIDT_FIELDS_ID"
                chkSelectedCriteria.DataBind()

                For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                    chkSelectedCriteria.Items(i).Selected = True
                Next
            Else
                chkSelectedCriteria.Items.Clear()
                chkSelectedCriteria.DataSource = Nothing
                chkSelectedCriteria.DataBind()
            End If
            EnableDisableCase()
            'End of Binding Cases


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub FooterBind(ByVal strMIDT As String)
        Dim objOutputXml As New XmlDocument
        Dim objNode As XmlNode
        Dim objXmlReader As XmlNodeReader
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim dsCriteria As DataSet

        objOutputXml = objbzBusinessCase.List_NIDTFields()





        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" And strMIDT.Length <> 0 Then
            Dim objArrayList As New ArrayList(strMIDT.Split(","))

            For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                If Not objArrayList.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                    objOutputXml.DocumentElement.RemoveChild(objNode)
                End If
            Next

            If objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS").Count = 0 Then
                lblError.Text = "Please Select At least One Criteria"
                ModalPopupExtender1.Show()
                Return
            End If
            dsCriteria = New DataSet
            objXmlReader = New XmlNodeReader(objOutputXml)
            dsCriteria.ReadXml(objXmlReader)
            chkSelectedCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
            chkSelectedCriteria.DataTextField = "NIDT_FIELDS_NAME"
            chkSelectedCriteria.DataValueField = "NIDT_FIELDS_ID"
            chkSelectedCriteria.DataBind()

            For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                chkSelectedCriteria.Items(i).Selected = True
            Next
        Else

        End If
    End Sub

    Protected Sub GvIncSlabsNested_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            If rdbPaymentType.SelectedValue = "1" Then
                CType(e.Row.FindControl("spnAdjAmount"), Label).Text = "Rate"
            Else
                CType(e.Row.FindControl("spnAdjAmount"), Label).Text = "Amount"
            End If
        End If
    End Sub






    Protected Sub btnGvSelection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGvSelection.Click
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim dSet As DataSet
            Dim strMIDT As String = ""
            For i As Integer = 0 To chkLstGvShowMIDT.Items.Count - 1
                If chkLstGvShowMIDT.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMIDT) Then
                        strMIDT = chkLstGvShowMIDT.Items(i).Value
                    Else
                        strMIDT = strMIDT + "," + chkLstGvShowMIDT.Items(i).Value
                    End If
                End If
            Next

            If String.IsNullOrEmpty(strMIDT) Then

            End If

            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objIncentiveTempXml.LoadXml(Session("INCENTIVEPLAN").ToString())
            Else
                lblError.Text = "Invalid Action"
                Return
            End If

            If hdMidtGvClick.Value.Trim() = "" Then
                lblError.Text = "Invalid Action"
                Return
            End If

            Dim objXmlNode As XmlNode = objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']").SelectSingleNode("NIDT_FIELDS_ID")

            Dim objMidtClone As XmlNode
            objMidtClone = objXmlNode.CloneNode(True)
            objMidtClone.Attributes("ID").Value = strMIDT
            objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']").RemoveChild(objXmlNode)
            objIncentiveTempXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdMidtGvClick.Value.Trim() + "']").AppendChild(objMidtClone)
            Session("INCENTIVEPLAN") = objIncentiveTempXml.OuterXml
            objXmlReader = New XmlNodeReader(objIncentiveTempXml)
            dSet = New DataSet()
            dSet.ReadXml(objXmlReader)

            'Code for Binding Slabs in Edit Mode
            grdvIncentivePlan.DataSource = dSet.Tables("CASE").DefaultView
            grdvIncentivePlan.DataBind()
            grdvIncentivePlanOld.DataSource = dSet.Tables("CASE").DefaultView
            grdvIncentivePlanOld.DataBind()
            'Code for Rebinding MIDT Cases 
            bindChkListForShowModal()
            'Code for Rebinding MIDT Cases 
            EnableDisableCase()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdConnectivityN_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            'ConnectivityCalculation

            If e.Row.RowType = DataControlRowType.Header Then
                totalConn = 0
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right

                Dim txtConnNo As TextBox = CType(e.Row.FindControl("txtConnNo"), TextBox)
                'lblConnTotal lblConnCatCost
                Dim lblConnTotal As Label = CType(e.Row.FindControl("lblConnTotal"), Label)
                Dim lblConnCatCost As Label = CType(e.Row.FindControl("lblConnCatCost"), Label)
                lblConnTotal.Text = DataBinder.Eval(e.Row.DataItem, "TOTAL").ToString
                totalConn += Val(DataBinder.Eval(e.Row.DataItem, "TOTAL").ToString)
                txtConnNo.Attributes.Add("onblur", "return ConnectivityCalculation('" + lblConnTotal.ClientID + "','" + txtConnNo.ClientID + "','" + lblConnCatCost.ClientID + "');")
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center

                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(4).Text = totalConn.ToString
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvHardwareN_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                totalHardware = 0
            End If

            'ConnectivityCalculation
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                Dim txtHwNoN As TextBox = CType(e.Row.FindControl("txtHwNoN"), TextBox)
                'lblConnTotal lblConnCatCost
                Dim lblHwTotCostN As Label = CType(e.Row.FindControl("lblHwTotCostN"), Label)
                Dim lblHwCostN As Label = CType(e.Row.FindControl("lblHwCostN"), Label)
                lblHwTotCostN.Text = DataBinder.Eval(e.Row.DataItem, "TOTAL").ToString
                totalHardware += Val(DataBinder.Eval(e.Row.DataItem, "TOTAL").ToString)
                txtHwNoN.Attributes.Add("onblur", "return HardwareNCalculation('" + lblHwTotCostN.ClientID + "','" + txtHwNoN.ClientID + "','" + lblHwCostN.ClientID + "');")
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

                e.Row.Cells(4).Text = totalHardware.ToString
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAddSlabN_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim objConX, FinalXml As New XmlDocument
            Dim objConDrpTemX As New XmlDocument
            Dim blnUpdate As Boolean = False
            Dim UpdateRow As Int16 = 0
            Dim rowCounter As Integer = 0
            Dim objClone, objCloneX As XmlNode

            objConX.LoadXml("<CASE TEMP_SLAB_ID_MAIN='' INC_PLAN_ID='' INC_PLAN_NAME=''><PLAN_DETAILS TEMP_SLAB_ID='' SLABS_START='' SLABS_END='' SLABS_RATE=''/><NIDT_FIELDS_ID ID=''/></CASE >")
            Dim objFinalNode As XmlNode
            objFinalNode = objConX.DocumentElement.SelectSingleNode("PLAN_DETAILS")
            objConX.DocumentElement.RemoveChild(objFinalNode)

            'If Session("TempSlabsXml") IsNot Nothing Then
            '    FinalXml.LoadXml(Session("TempSlabsXml").ToString())
            'Else
            '    FinalXml.LoadXml("<CASE TEMP_SLAB_ID_MAIN='' INC_PLAN_ID='' INC_TYPE_NAME=''></CASE >")
            'End If

            'Validation Section

            If Convert.ToDouble(txtRangeFromN.Text.Trim()) > Val(txtRangeToN.Text.Trim()) Then
                lblError.Text = "Range from is greater than Range To"
                lblInnerError.Text = "Range from is greater than Range To"

                Return
            End If

            If btnAddSlabN.Text <> "Update" Then
                If validateSlabRange() Then
                    Return
                End If
            End If

            'End of Validation Section



            For rowCounter = 0 To grdvSlabN.Rows.Count - 1
                objClone = objFinalNode
                objCloneX = objClone.CloneNode(True)

                If btnAddSlabN.Text = "Update" Then
                    Dim hdTEMP_SLAB_ID As HiddenField = CType(grdvSlabN.Rows(rowCounter).FindControl("hdTEMP_SLAB_ID"), HiddenField)
                    If btnAddSlabN.CommandArgument = hdTEMP_SLAB_ID.Value.Trim() Then
                        ViewState("btnAddSlabN_CommandArgument") = btnAddSlabN.CommandArgument.ToString
                        btnAddSlabN.CommandArgument = ""
                        btnAddSlabN.Text = "Add"
                        blnUpdate = True
                        UpdateRow = rowCounter
                        Continue For
                    End If
                End If

                '<HARDWARE BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' />
                'Dim hdOnlineCatID As HiddenField = CType(GvConnectivity.Rows(rowCounter).FindControl("hdOnlineCatID"), HiddenField)
                objCloneX.Attributes("TEMP_SLAB_ID").Value = rowCounter
                objCloneX.Attributes("SLABS_START").Value = grdvSlabN.Rows(rowCounter).Cells(0).Text.Trim()
                objCloneX.Attributes("SLABS_END").Value = Val(grdvSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                ' objCloneX.Attributes("SLABS_RATE").Value = grdvSlabN.Rows(rowCounter).Cells(2).Text.Trim()
                objCloneX.Attributes("SLABS_RATE").Value = CType(grdvSlabN.Rows(rowCounter).FindControl("txtAmount"), TextBox).Text
                objConX.DocumentElement.AppendChild(objCloneX)
                'objConX.DocumentElement.AppendChild(objConX.ImportNode(objCloneX, True))
                ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
                objCloneX = objClone.CloneNode(True)
            Next


            'Slabs overlap validation Section for Update
            If blnUpdate Then
                Dim slabStart As Decimal = 0
                Dim slabEnd As Decimal = 0
                Dim newSlabFrom As Double = Convert.ToDouble(txtRangeFromN.Text.Trim())
                Dim newSlabTo As Double = Val(txtRangeToN.Text.Trim())

                For rowCounter = 0 To grdvSlabN.Rows.Count - 1
                    If rowCounter <> UpdateRow Then
                        slabStart = Convert.ToDouble(grdvSlabN.Rows(rowCounter).Cells(0).Text.Trim())
                        slabEnd = Convert.ToDouble(grdvSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                        If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                            lblError.Text = "Slab Overlaps"
                            lblInnerError.Text = "Slab Overlaps"
                            btnAddSlabN.Text = "Update"
                            btnAddSlabN.CommandArgument = ViewState("btnAddSlabN_CommandArgument").ToString
                            Return
                        End If
                    End If
                Next
            End If
            'End of Slabs overlap validation Section for Update

            If btnAddSlabN.Text <> "Update" Then


                objClone = objFinalNode 'objConX.DocumentElement.SelectSingleNode("temp")
                objCloneX = objClone.CloneNode(True)
                'objConX.DocumentElement.RemoveChild(objClone)
                objCloneX.Attributes("TEMP_SLAB_ID").Value = rowCounter
                objCloneX.Attributes("SLABS_START").Value = txtRangeFromN.Text.Trim()
                objCloneX.Attributes("SLABS_END").Value = Val(txtRangeToN.Text.Trim())
                objCloneX.Attributes("SLABS_RATE").Value = txtSlabAmountN.Text.Trim()
                'objConX.DocumentElement.AppendChild(objCloneX)
                objConX.DocumentElement.AppendChild(objCloneX)
                ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
                'FinalXml.DocumentElement.AppendChild(objConX.ImportNode((objCloneX), True))
                ' objCloneX = objClone.CloneNode(True)

            End If


            Session("TempSlabsXml") = resetTempID(objConX.OuterXml)
            Session("slabBackup") = Session("TempSlabsXml")
            objConX.LoadXml(Session("TempSlabsXml"))
            'Session("slabBackup") = objConX.OuterXml
            Dim dsConn As New DataSet
            Dim objRdr As XmlNodeReader
            objRdr = New XmlNodeReader(objConX)
            dsConn.ReadXml(objRdr)
            grdvSlabN.DataSource = dsConn.Tables("PLAN_DETAILS").DefaultView
            grdvSlabN.DataBind()

            If grdvIncentivePlan.Rows.Count > 0 Then
                If Session("INCENTIVEPLAN") IsNot Nothing Then
                    resetTempID()
                    Dim objPlanXml As New XmlDocument
                    If blnUpdate = True Then
                        UpdateXmlIncentivePlan()
                        UpdateSlabValue(UpdateRow.ToString(), txtRangeFromN.Text.Trim(), txtRangeToN.Text.Trim())
                        'UpdateXmlIncentivePlan(UpdateRow.ToString())
                    Else
                        UpdateXmlIncentivePlan()
                    End If


                    objPlanXml.LoadXml(Session("INCENTIVEPLAN").ToString())

                    'Code for Managing Slabs 
                    'For Each xNodePlan As XmlNode In objPlanXml.DocumentElement.SelectNodes("CASE")
                    '    For Each xSlabNode As XmlNode In xNodePlan.SelectNodes("PLAN_DETAILS")
                    '        xNodePlan.RemoveChild(xSlabNode)
                    '    Next
                    'Next

                    If blnUpdate <> True Then
                        For Each xNodePlan As XmlNode In objPlanXml.DocumentElement.SelectNodes("CASE")

                            Dim objXNode As XmlNode
                            For Each xSlabNode As XmlNode In objConX.DocumentElement.SelectNodes("PLAN_DETAILS")
                                objXNode = xNodePlan.SelectSingleNode("PLAN_DETAILS[@SLABS_START='" + xSlabNode.Attributes("SLABS_START").Value.Trim() + "']")
                                If objXNode Is Nothing Then
                                    xSlabNode.Attributes("SLABS_RATE").Value = ""
                                    xNodePlan.AppendChild(objPlanXml.ImportNode(xSlabNode, True))
                                End If
                            Next

                        Next
                    End If



                    'For Each xNodePlan As XmlNode In objPlanXml.DocumentElement.SelectNodes("CASE")
                    '    For Each xSlabNode As XmlNode In xNodePlan.SelectNodes("PLAN_DETAILS")
                    '        xSlabNode.Attributes("SLABS_RATE").InnerText = ""
                    '    Next
                    'Next


                    'Code for Managing Slabs 

                    Session("INCENTIVEPLAN") = objPlanXml.OuterXml
                    resetTempID()
                    Dim dsConnPlan As New DataSet
                    Dim objRdrPlan As XmlNodeReader
                    objRdrPlan = New XmlNodeReader(objPlanXml)
                    dsConnPlan.ReadXml(objRdrPlan)
                    grdvIncentivePlan.DataSource = dsConnPlan.Tables("CASE").DefaultView
                    grdvIncentivePlan.DataBind()
                End If
            Else
                '  btnAddPlan.Visible = False
            End If



            txtRangeFromN.Text = ""
            txtRangeToN.Text = ""
            txtSlabAmountN.Text = ""

        Catch ex As Exception
            lblError.Text = ex.Message
            lblInnerError.Text = ex.Message

        End Try
    End Sub
    Private Sub UpdateXmlIncentivePlan()
        Dim objXml As New XmlDocument
        Dim objXnodeCase, objXnodePlan As XmlNode
        Try
            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objXml.LoadXml(Session("INCENTIVEPLAN"))
                'Code for Maintaing Slab Rate in Gridview

                For Each grdRow As GridViewRow In grdvIncentivePlan.Rows
                    Dim grdCase As GridView = CType(grdRow.FindControl("GvIncSlabsNested"), GridView)
                    Dim hdCaseId As HiddenField = CType(grdRow.FindControl("hdCaseId"), HiddenField)

                    objXnodeCase = objXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdCaseId.Value.Trim() + "']")

                    For Each grdRowNest As GridViewRow In grdCase.Rows
                        Dim strRate As String
                        Dim slbFrom As String = ""
                        Dim slbTo As String = ""

                        strRate = CType(grdRowNest.FindControl("txtAmount"), TextBox).Text.Trim()
                        slbFrom = grdRowNest.Cells(0).Text.Trim()
                        slbTo = grdRowNest.Cells(1).Text.Trim()
                        objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@SLABS_START='" + slbFrom + "' and @SLABS_END='" + slbTo + "']")
                        If objXnodePlan IsNot Nothing Then
                            objXnodePlan.Attributes("SLABS_RATE").Value = strRate
                        End If
                    Next
                Next

                Session("INCENTIVEPLAN") = objXml.OuterXml
                'Code for Maintaing Slab Rate in Gridview


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub UpdateXmlIncentivePlan(ByVal strRowID As String)
        Dim objXml As New XmlDocument
        Dim objXnodeCase, objXnodePlan As XmlNode
        Try
            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objXml.LoadXml(Session("INCENTIVEPLAN"))
                'Code for Maintaing Slab Rate in Gridview

                For Each grdRow As GridViewRow In grdvIncentivePlan.Rows
                    Dim grdCase As GridView = CType(grdRow.FindControl("GvIncSlabsNested"), GridView)
                    Dim hdCaseId As HiddenField = CType(grdRow.FindControl("hdCaseId"), HiddenField)

                    objXnodeCase = objXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + hdCaseId.Value.Trim() + "']")


                    For Each grdRowNest As GridViewRow In grdCase.Rows
                        Dim strRate As String
                        Dim slbFrom As String = ""
                        Dim slbTo As String = ""

                        strRate = CType(grdRowNest.FindControl("txtAmount"), TextBox).Text.Trim()
                        slbFrom = grdRowNest.Cells(0).Text.Trim()
                        slbTo = grdRowNest.Cells(1).Text.Trim()
                        objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strRowID + "']") 'objXnodeCase.SelectSingleNode("PLAN_DETAILS[@SLABS_START='" + slbFrom + "' and @SLABS_END='" + slbTo + "']")
                        If objXnodePlan IsNot Nothing Then
                            objXnodePlan.Attributes("SLABS_RATE").Value = strRate
                        End If
                    Next
                Next

                Session("INCENTIVEPLAN") = objXml.OuterXml
                'Code for Maintaing Slab Rate in Gridview


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub UpdateSlabValue(ByVal strRowID As String, ByVal strValueFrm As String, ByVal strValueTo As String)
        Try
            Dim objXml As New XmlDocument
            Dim objXnodeCase, objXnodePlan As XmlNode
            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objXml.LoadXml(Session("INCENTIVEPLAN"))
                For Each objXnodeCase In objXml.DocumentElement.SelectNodes("CASE")

                    ' Session("SlabUpdateValue")
                    If Session("SlabUpdateValue") IsNot Nothing Then
                        objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@SLABS_START='" + Session("SlabUpdateValue").ToString().Split("|").GetValue(0) + "' and @SLABS_END='" + Session("SlabUpdateValue").ToString().Split("|").GetValue(1) + "']") 'objXnodeCase.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strRowID + "']")
                    End If
                    If objXnodePlan IsNot Nothing Then
                        objXnodePlan.Attributes("SLABS_START").Value = strValueFrm
                        objXnodePlan.Attributes("SLABS_END").Value = strValueTo
                    End If
                Next
                Session("INCENTIVEPLAN") = objXml.OuterXml
                Session("SlabUpdateValue") = Nothing
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub DeleteSlabs(ByVal strRowID As String)
        Try
            Dim objXml As New XmlDocument
            Dim objXnodeCase, objXnodePlan As XmlNode
            If Session("INCENTIVEPLAN") IsNot Nothing Then
                objXml.LoadXml(Session("INCENTIVEPLAN"))
                For Each objXnodeCase In objXml.DocumentElement.SelectNodes("CASE")
                    objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strRowID + "']")
                    If objXnodePlan IsNot Nothing Then
                        objXnodeCase.RemoveChild(objXnodePlan)
                    End If

                Next
                Session("INCENTIVEPLAN") = objXml.OuterXml


                Dim dsConnPlanP As New DataSet
                Dim objRdrPlanP As XmlNodeReader
                objRdrPlanP = New XmlNodeReader(objXml)
                dsConnPlanP.ReadXml(objRdrPlanP)
                If dsConnPlanP.Tables("CASE") IsNot Nothing Then
                    grdvIncentivePlan.DataSource = dsConnPlanP.Tables("CASE").DefaultView
                    grdvIncentivePlan.DataBind()
                End If



            End If

            If Session("slabBackup") IsNot Nothing Then
                objXml.LoadXml(Session("slabBackup"))
                For Each objXnodeCase In objXml.SelectNodes("CASE")
                    objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strRowID + "']")
                    If objXnodePlan IsNot Nothing Then
                        objXnodeCase.RemoveChild(objXnodePlan)
                    End If
                Next
                Session("slabBackup") = objXml.OuterXml
            End If

            If Session("TempSlabsXml") IsNot Nothing Then
                objXml.LoadXml(Session("TempSlabsXml"))
                For Each objXnodeCase In objXml.SelectNodes("CASE")
                    objXnodePlan = objXnodeCase.SelectSingleNode("PLAN_DETAILS[@TEMP_SLAB_ID='" + strRowID + "']")
                    If objXnodePlan IsNot Nothing Then
                        objXnodeCase.RemoveChild(objXnodePlan)
                    End If
                Next
                Session("TempSlabsXml") = objXml.OuterXml
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblInnerError.Text = ex.Message
        End Try
    End Sub


    Protected Sub grdvSlabN_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            If rdbPaymentType.SelectedValue = "1" Then
                CType(e.Row.FindControl("spnAdjAmount"), Label).Text = "Rate"
            Else
                CType(e.Row.FindControl("spnAdjAmount"), Label).Text = "Amount"
            End If
        End If
    End Sub

    Protected Sub grdvSlabN_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "EditX" Then
                Dim dt As New DataTable
                dt.Columns.Add("TEMP_SLAB_ID")
                dt.Columns.Add("SLABS_START")
                dt.Columns.Add("SLABS_END")
                dt.Columns.Add("SLABS_RATE")

                Dim dr As DataRow
                For Each gv As GridViewRow In grdvSlabN.Rows
                    dr = dt.NewRow
                    dr("TEMP_SLAB_ID") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
                    dr("SLABS_START") = gv.Cells(0).Text
                    dr("SLABS_END") = gv.Cells(1).Text
                    ' dr("SLABS_RATE") = gv.Cells(2).Text
                    dr("SLABS_RATE") = CType(gv.FindControl("txtAmount"), TextBox).Text
                    dt.Rows.Add(dr)
                Next

                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "TEMP_SLAB_ID='" + e.CommandArgument.ToString + "'"
                txtRangeFromN.Text = dv(0)("SLABS_START").ToString
                txtRangeToN.Text = dv(0)("SLABS_END").ToString
                txtSlabAmountN.Text = dv(0)("SLABS_RATE").ToString
                btnAddSlabN.Text = "Update"
                btnAddSlabN.CommandArgument = e.CommandArgument.ToString
                Session("SlabUpdateValue") = txtRangeFromN.Text.Trim() + "|" + txtRangeToN.Text.ToString()
            End If
            If e.CommandName = "DelX" Then
                Dim dt As New DataTable
                dt.Columns.Add("TEMP_SLAB_ID")
                dt.Columns.Add("SLABS_START")
                dt.Columns.Add("SLABS_END")
                dt.Columns.Add("SLABS_RATE")
                Dim dr As DataRow
                For Each gv As GridViewRow In grdvSlabN.Rows
                    If e.CommandArgument.ToString <> CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value Then
                        dr = dt.NewRow
                        dr("TEMP_SLAB_ID") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
                        dr("SLABS_START") = gv.Cells(0).Text
                        dr("SLABS_END") = gv.Cells(1).Text
                        'dr("SLABS_RATE") = gv.Cells(2).Text
                        dr("SLABS_RATE") = CType(gv.FindControl("txtAmount"), TextBox).Text

                        dt.Rows.Add(dr)
                    End If
                Next
                grdvSlabN.DataSource = dt
                grdvSlabN.DataBind()
                DeleteSlabs(e.CommandArgument.ToString)
                txtRangeFromN.Text = ""
                txtRangeToN.Text = ""
                txtSlabAmountN.Text = ""
                btnAddSlabN.Text = "Add"
                btnAddSlabN.CommandArgument = ""
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblInnerError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCancelCriteria_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Not for writing code
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Not for writing code
    End Sub


    Protected Sub btnGvSelection1_New_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub


    Protected Sub grdvIncentivePlanOld_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objxmlBusinessCase As New XmlDocument
            Dim objIncRdr, objReadaerCriteria As XmlNodeReader
            objxmlBusinessCase.LoadXml(Session("INCENTIVEPLAN_OLD").ToString())
            Dim dsIncPlan, dsCriteria As DataSet
            objIncRdr = New XmlNodeReader(objxmlBusinessCase)
            dsIncPlan = New DataSet()
            dsIncPlan.ReadXml(objIncRdr)
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase


            Try

                Dim hdCaseId As HiddenField = CType(e.Row.FindControl("hdCaseId"), HiddenField)
                Dim ChkListCriteria As CheckBoxList = CType(e.Row.FindControl("chklstCriteriaOld"), CheckBoxList)

                Dim objOutputCriteriaXmlXml As New XmlDocument
                objOutputCriteriaXmlXml = objbzBusinessCase.List_NIDTFields
                If objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    Dim objTempMidtXml As New XmlDocument
                    objTempMidtXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")

                    '@ Code for selection for criteria
                    Dim dvNIDTFIELDS As DataView
                    If dsIncPlan.Tables("NIDT_FIELDS_ID") IsNot Nothing Then
                        dvNIDTFIELDS = dsIncPlan.Tables("NIDT_FIELDS_ID").DefaultView
                        dvNIDTFIELDS.RowFilter = "Case_Id='" + hdCaseId.Value + "'"
                        If dvNIDTFIELDS.Count > 0 Then
                            Dim objArrayList As New ArrayList(dvNIDTFIELDS.Item(0)(0).ToString().Split(","))


                            For ii As Integer = 0 To objArrayList.Count - 1
                                Dim objN As XmlNode = objOutputCriteriaXmlXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                                If objN IsNot Nothing Then
                                    objTempMidtXml.DocumentElement.AppendChild(objTempMidtXml.ImportNode(objN, True))
                                End If
                            Next


                        End If
                    End If
                    '@ Code for selection for criteria



                    dsCriteria = New DataSet
                    objReadaerCriteria = New XmlNodeReader(objTempMidtXml)
                    dsCriteria.ReadXml(objReadaerCriteria)
                    ChkListCriteria.DataSource = dsCriteria.Tables("NIDTFIELDS")
                    ChkListCriteria.DataTextField = "NIDT_FIELDS_NAME"
                    ChkListCriteria.DataValueField = "NIDT_FIELDS_ID"
                    ChkListCriteria.DataBind()



                    For i As Integer = 0 To ChkListCriteria.Items.Count - 1
                        ChkListCriteria.Items(i).Selected = True
                    Next

                End If


                '@ Code for Filling PLAN DETAILS
                Dim dvPlanDetails As DataView
                Dim GvIncSlabsNested As GridView = CType(e.Row.FindControl("GvIncSlabsNested"), GridView)
                If dsIncPlan.Tables("PLAN_DETAILS") IsNot Nothing Then
                    dvPlanDetails = dsIncPlan.Tables("PLAN_DETAILS").DefaultView
                    dvPlanDetails.RowFilter = "Case_Id='" + hdCaseId.Value + "'"





                    If dvPlanDetails.Count > 0 Then

                        GvIncSlabsNested.DataSource = dvPlanDetails
                        GvIncSlabsNested.DataBind()

                        'Dim cc As Integer = 0

                        'For cc = 0 To GvIncSlabs.Rows.Count - 1
                        '    Dim lnkEdit As LinkButton = CType(GvIncSlabs.Rows(cc).FindControl("lnkEdit"), LinkButton)
                        '    AddHandler lnkEdit.Click, AddressOf lnkEdit_Click
                        'Next

                    Else
                        GvIncSlabsNested.DataSource = Nothing
                        GvIncSlabsNested.DataBind()
                    End If

                Else
                    GvIncSlabsNested.DataSource = Nothing
                    GvIncSlabsNested.DataBind()
                End If
                '@ End of Code for Filling PLAN DETAILS



            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub grdvBreakUpLast_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'lbltenPerLess txtBreakupRate  lblbrAmount
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right




                Dim lbltenPerLess As Label = CType(e.Row.FindControl("lbltenPerLess"), Label)
                Dim lblbrAmount As Label = CType(e.Row.FindControl("lblbrAmount"), Label)
                Dim txtBreakupRate As TextBox = CType(e.Row.FindControl("txtBreakupRate"), TextBox)
                txtBreakupRate.Attributes.Add("onblur", "return calculateBreak('" + lbltenPerLess.ClientID + "','" + lblbrAmount.ClientID + "','" + txtBreakupRate.ClientID + "');")

                Dim hdType As HiddenField = CType(e.Row.FindControl("hdType"), HiddenField)
                If hdType.Value.ToString().ToUpper() = "DOM" Then
                    txtBreakupRate.ReadOnly = True
                    txtBreakupRate.CssClass = "displayNone"
                End If

            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(4).Text = "Total Incentive"
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvBAgencyMIDT_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ViewState("Desc") = "FALSE"
        Else
            If ViewState("SortName") = SortName Then
                If ViewState("Desc") = "TRUE" Then
                    ViewState("Desc") = "FALSE"
                Else
                    ViewState("Desc") = "TRUE"
                End If
            Else
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            End If
        End If
        BindData_MIDTAgency()
    End Sub
#End Region


#Region "BindData()"
    Private Sub BindData_MIDTAgency()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objMidtAgency As New AAMS.bizIncetive.bzBusinessCase
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            objInputXml.LoadXml("<INC_GET_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><EmployeeID></EmployeeID><REFRESH/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INC_GET_BUSINESSCASE_INPUT>")

            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    hdBcID.Value = Request.QueryString("BCaseID").Trim()
                    objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BCaseID").Trim()
                End If
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value
            Else
                lblError.Text = "Chain Code is blank"
                Return
            End If
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If

            objInputXml.DocumentElement.SelectSingleNode("REFRESH").InnerText = IIf(hdRefreshAction.Value = "", "FALSE", hdRefreshAction.Value)


            'Start CODE for sorting and paging

            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Name" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
            'objInputXml.Save("C:\objInputXmlMIDTAgency.xml")
            objOutputXml = objMidtAgency.SearchMIDT(objInputXml)
            ' objOutputXml.Save("C:\objOutputXmlMIDTAgency.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("BC_MIDT_AGENCY").Rows.Count <> 0 Then
                    GvBAgencyMIDT.DataSource = ds.Tables("BC_MIDT_AGENCY")
                    GvBAgencyMIDT.DataBind()
                    'Code Added For Paging And Sorting In case Of Delete The Record



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


                    pnlPaging.Visible = True
                    Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                        'pnlPaging.Visible = False
                        ' ddlPageNumber.Visible = False
                        lnkNext.Visible = False
                        lnkPrev.Visible = False
                    Else
                        'ddlPageNumber.Visible = True
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
                    hdRecordOnCurrentPage.Value = ds.Tables("BC_MIDT_AGENCY").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "Name"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "OfficeID"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(2).Controls.Add(imgDown)
                            End Select
                        Case "Address"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select

                        Case "A"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(4).Controls.Add(imgDown)
                            End Select

                        Case "B"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(5).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(5).Controls.Add(imgDown)
                            End Select

                        Case "G"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(6).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(6).Controls.Add(imgDown)
                            End Select

                        Case "P"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(7).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(7).Controls.Add(imgDown)
                            End Select

                        Case "W"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(8).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(8).Controls.Add(imgDown)
                            End Select

                        Case "TTP"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    GvBAgencyMIDT.HeaderRow.Cells(9).Controls.Add(imgUp)
                                Case "TRUE"
                                    GvBAgencyMIDT.HeaderRow.Cells(9).Controls.Add(imgDown)
                            End Select
                    End Select
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                    GvBAgencyMIDT.DataSource = Nothing
                    GvBAgencyMIDT.DataBind()
                End If

            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value.ToString.ToUpper <> "NO RECORD FOUND!" Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData_MIDTAgency()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData_MIDTAgency()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData_MIDTAgency()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Sub GetBidtGroupData()
        Try
            Dim objxBidtInput, objxBidtOutput, objxBidtInputGroup, objxBidtOutputGroup As New XmlDocument
            Dim objNoderR As XmlNodeReader
            Dim dsBidtGroup As DataSet
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

            objxBidtInput.LoadXml("<INC_GET_BIDT_INPUT><MONTH /><CURRENT_YEAR /><CHAIN_CODE /><BC_ID /><REFRESH/></INC_GET_BIDT_INPUT >")
            objxBidtInput.DocumentElement.SelectSingleNode("MONTH").InnerText = System.DateTime.Now.Month.ToString()
            objxBidtInput.DocumentElement.SelectSingleNode("CURRENT_YEAR").InnerText = System.DateTime.Now.Year.ToString()
            objxBidtInput.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value.Trim()
            objxBidtInput.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdBcID.Value.Trim()
            objxBidtInput.DocumentElement.SelectSingleNode("REFRESH").InnerText = IIf(hdRefreshAction.Value = "", "FALSE", hdRefreshAction.Value)

            objxBidtOutputGroup = objbzBusinessCase.SearchBIDTGROUP(objxBidtInput)
            'objxBidtInput.Save("C:\objxBidtInputGroup.xml")
            'objxBidtOutputGroup.Save("C:\objxBidtOutputGroup.xml")

            If objxBidtOutputGroup.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("BIDTLAST3MONTH") = objxBidtOutputGroup.OuterXml
                objNoderR = New XmlNodeReader(objxBidtOutputGroup)
                dsBidtGroup = New DataSet()
                dsBidtGroup.ReadXml(objNoderR)
                grdvBIDTGrop.DataSource = dsBidtGroup.Tables("BC_BIDT_GROUP").DefaultView
                grdvBIDTGrop.DataBind()
                lblHeaderBIDTGroup.Text = "BIDT Group"
                lblHeaderBIDTGroup.Visible = True
            Else
                lblHeaderBIDTGroup.Text = "No BIDT Group Found"
                lblHeaderBIDTGroup.Visible = True
                '  lblError.Text = objxBidtOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GetBidtAgencyData()
        Try
            Dim objxBidtInput, objxBidtOutput, objxBidtInputGroup, objxBidtOutputGroup As New XmlDocument
            Dim objNoderR As XmlNodeReader
            Dim dsBidtGroup As DataSet
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            objxBidtInput.LoadXml("<INC_GET_BIDT_INPUT><MONTH /><CURRENT_YEAR /><CHAIN_CODE /> <BC_ID /><REFRESH/> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INC_GET_BIDT_INPUT >")
            objxBidtInput.DocumentElement.SelectSingleNode("MONTH").InnerText = System.DateTime.Now.Month.ToString()
            objxBidtInput.DocumentElement.SelectSingleNode("CURRENT_YEAR").InnerText = System.DateTime.Now.Year.ToString()
            objxBidtInput.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value.Trim()
            objxBidtInput.DocumentElement.SelectSingleNode("REFRESH").InnerText = IIf(hdRefreshAction.Value = "", "FALSE", hdRefreshAction.Value)

            objxBidtInput.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdBcID.Value.Trim()

            If ViewState("PrevSearchingBidt") Is Nothing Then
                objxBidtInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumberBidt.Items.Count = 0, "1", ddlPageNumberBidt.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearchingBidt"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objxBidtInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumberBidt.Items.Count = 0, "1", ddlPageNumberBidt.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objxBidtInput.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objxBidtInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumberBidt.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objxBidtInput.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortNameBIDT") Is Nothing Then
                ViewState("SortNameBIDT") = "Name"
                objxBidtInput.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Name" '"LOCATION_CODE"
            Else
                objxBidtInput.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortNameBIDT")
            End If

            If ViewState("DescBIDT") Is Nothing Then
                ViewState("DescBIDT") = "FALSE"
                objxBidtInput.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objxBidtInput.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("DescBIDT")
            End If

            ' objxBidtInput.Save("C:\objxBidtAgencyInput.xml")
            objxBidtOutput = objbzBusinessCase.SearchBIDTAGENCY(objxBidtInput)
            'objxBidtOutput.Save("C:\objxBidtAgencyOutputAgency.xml")
            If objxBidtOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objNoderR = New XmlNodeReader(objxBidtOutput)
                dsBidtGroup = New DataSet()
                dsBidtGroup.ReadXml(objNoderR)


                'Code for Showing Total of BIDT Agency
                dsBidtAgencyTotal = New DataTable()
                dsBidtAgencyTotal = dsBidtGroup.Tables("PAGE_TOTAL")
                'End of Code for Showing Total of BIDT Agency

                grdvBidtAgency.DataSource = dsBidtGroup.Tables("BC_BIDT_AGENCY").DefaultView
                grdvBidtAgency.DataBind()





                llbHeaderBIDTAgencyDetails.Text = "BIDT Agency Details"
                llbHeaderBIDTAgencyDetails.Visible = True

                ' pnlPaging.Visible = True
                Dim count As Integer = CInt(objxBidtOutput.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                Dim selectedValue As String = IIf(ddlPageNumberBidt.SelectedValue = "", "1", ddlPageNumberBidt.SelectedValue)
                If count <> ddlPageNumberBidt.Items.Count Then
                    ddlPageNumberBidt.Items.Clear()
                    For i As Integer = 1 To count
                        ddlPageNumberBidt.Items.Add(i.ToString)
                    Next
                End If
                ddlPageNumberBidt.SelectedValue = selectedValue
                'Code for hiding prev and next button based on count
                If count = 1 Then
                    'pnlPaging.Visible = False
                    ' ddlPageNumber.Visible = False
                    lnkBidtNext.Visible = False
                    lnkPervBidt.Visible = False
                Else
                    'ddlPageNumber.Visible = True
                    lnkPervBidt.Visible = True
                    lnkBidtNext.Visible = True
                End If

                'Code for hiding next button when pagenumber is equal to page count
                If ddlPageNumberBidt.SelectedValue = count.ToString Then
                    lnkBidtNext.Visible = False
                Else
                    lnkBidtNext.Visible = True
                End If

                'Code for hiding prev button when pagenumber is 1
                If ddlPageNumberBidt.SelectedValue = "1" Then
                    lnkPervBidt.Visible = False
                Else
                    lnkPervBidt.Visible = True
                End If
                hdRecordOnCurrentPageBidt.Value = dsBidtGroup.Tables("BC_BIDT_AGENCY").Rows.Count.ToString
                txtTotalRecordCountBidt.Text = objxBidtOutput.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                pnlPagingBidtAgency.Visible = True

                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortNameBIDT")
                    Case "LCODE"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select

                    Case "Name"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select

                    Case "Address"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select

                    Case "PRODUCTIVITY"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select

                    Case "AIRNET"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select

                    Case "HOTEL"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select

                    Case "CAR"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select

                    Case "INSURANCE"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select

                    Case "NBS"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select

                    Case "WITHPASSIVE"
                        Select Case ViewState("DescBIDT")
                            Case "FALSE"
                                grdvBidtAgency.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvBidtAgency.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select

                End Select


            Else
                'lblError.Text = objxBidtOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub

    Protected Sub lnkPervBidt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlPageNumberBidt.SelectedValue <> "1" Then
                ddlPageNumberBidt.SelectedValue = (CInt(ddlPageNumberBidt.SelectedValue) - 1).ToString
            End If
            GetBidtAgencyData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumberBidt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            GetBidtAgencyData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkBidtNext_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlPageNumberBidt.SelectedValue <> (ddlPageNumberBidt.Items.Count).ToString Then
                ddlPageNumberBidt.SelectedValue = (CInt(ddlPageNumberBidt.SelectedValue) + 1).ToString
            End If
            GetBidtAgencyData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvBidtAgency_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)

        Try
            Dim sortName As String = e.SortExpression
            SortCall_BidtAgency(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Sort Function"
    Sub SortCall_BidtAgency(ByVal SortName As String)

        If ViewState("SortNameBIDT") Is Nothing Then
            ViewState("SortNameBIDT") = SortName
            ViewState("DescBIDT") = "FALSE"
        Else
            If ViewState("SortNameBIDT") = SortName Then
                If ViewState("DescBIDT") = "TRUE" Then
                    ViewState("DescBIDT") = "FALSE"
                Else
                    ViewState("DescBIDT") = "TRUE"
                End If
            Else
                ViewState("SortNameBIDT") = SortName
                ViewState("DescBIDT") = "FALSE"
            End If
        End If
        GetBidtAgencyData()
    End Sub
#End Region

    'Protected Sub lnkShowBidtGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Try
    '    '    'Code for Binding BIDT data
    '    '    GetBidtAgencyData()
    '    '    GetBidtGroupData()
    '    '    lnkShowBidtGroup.Visible = False
    '    '    'lnkShowBidtGroup.CssClass = "displayNone"
    '    '    lblShowBidtGroup.Visible = True
    '    '    'End of Code for Binding BIDT data
    '    'Catch ex As Exception

    '    'End Try
    'End Sub

    Protected Sub lnkShowBidtGroup_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            'Code for Binding BIDT data
            tdBidt.Attributes.Add("class", "redborder")
            GetBidtAgencyData()
            GetBidtGroupData()
            lnkShowBidtGroup.Visible = False
            'lnkShowBidtGroup.CssClass = "displayNone"
            imgBIDT_Panel.Style.Add("display", "block")
            'End of Code for Binding BIDT data
            'hdTabID.Text = "1"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    'Protected Sub rdbIncentiveType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If rdbIncentiveType.SelectedValue.Trim() = "1" Then
    '            rdbUpfrontTypeName.Visible = True
    '        Else


    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub rdbUpfrontTypeName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If rdbPaymentType.SelectedValue = "3" Then
    '            rdbPaymentType.SelectedValue = "1"
    '            rdbPaymentType.Enabled = False

    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub rdbPaymentType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub rdbPlbTypeName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub GvBAgencyMIDT_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

#Region "PLB Slab"
    Protected Sub btnPlbSlabN_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim objConX, FinalXml As New XmlDocument
            Dim objConDrpTemX As New XmlDocument
            Dim blnUpdate As Boolean = False
            Dim UpdateRow As Int16 = 0
            objConX.LoadXml("<PLB BC_ID='' PLBTYPEID='' PLBAMOUNT=''><PLB_DETAILS PLBTYPEID_TEMP='' SLABS_START='' SLABS_END='' SLABS_RATE='' /></PLB >")
            Dim objFinalNode As XmlNode
            objFinalNode = objConX.DocumentElement.SelectSingleNode("PLB_DETAILS")
            objConX.DocumentElement.RemoveChild(objFinalNode)

            'If Session("TempSlabsXml") IsNot Nothing Then
            '    FinalXml.LoadXml(Session("TempSlabsXml").ToString())
            'Else
            '    FinalXml.LoadXml("<CASE TEMP_SLAB_ID_MAIN='' INC_PLAN_ID='' INC_TYPE_NAME=''></CASE >")
            'End If

            'Validation Section


            If Convert.ToDouble(txtPlbSlabFromN.Text.Trim()) > Val(txtPlbSlabToN.Text.Trim()) Then
                lblError.Text = "Range from is greater than Range To"
                Return
            End If

            If btnPlbSlabN.Text <> "Update" Then
                If validateSlabRangePLB() Then
                    Return
                End If
            End If

            'End of Validation Section


            Dim rowCounter As Integer = 0
            Dim objClone, objCloneX As XmlNode
            For rowCounter = 0 To grdvPlbSlabN.Rows.Count - 1
                objClone = objFinalNode
                objCloneX = objClone.CloneNode(True)

                If btnPlbSlabN.Text = "Update" Then
                    Dim hdTEMP_SLAB_ID As HiddenField = CType(grdvPlbSlabN.Rows(rowCounter).FindControl("hdTEMP_SLAB_ID"), HiddenField)
                    If btnPlbSlabN.CommandArgument = hdTEMP_SLAB_ID.Value.Trim() Then
                        btnPlbSlabN.CommandArgument = ""
                        btnPlbSlabN.Text = "Add"
                        blnUpdate = True
                        UpdateRow = rowCounter
                        Continue For
                    End If
                End If

                '<HARDWARE BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' />
                'Dim hdOnlineCatID As HiddenField = CType(GvConnectivity.Rows(rowCounter).FindControl("hdOnlineCatID"), HiddenField)
                objCloneX.Attributes("PLBTYPEID_TEMP").Value = rowCounter
                objCloneX.Attributes("SLABS_START").Value = grdvPlbSlabN.Rows(rowCounter).Cells(0).Text.Trim()
                objCloneX.Attributes("SLABS_END").Value = Val(grdvPlbSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                objCloneX.Attributes("SLABS_RATE").Value = grdvPlbSlabN.Rows(rowCounter).Cells(2).Text.Trim()
                objConX.DocumentElement.AppendChild(objCloneX)
                'objConX.DocumentElement.AppendChild(objConX.ImportNode(objCloneX, True))
                ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
                objCloneX = objClone.CloneNode(True)
            Next



            objClone = objFinalNode 'objConX.DocumentElement.SelectSingleNode("temp")
            objCloneX = objClone.CloneNode(True)
            'objConX.DocumentElement.RemoveChild(objClone)
            objCloneX.Attributes("PLBTYPEID_TEMP").Value = rowCounter
            objCloneX.Attributes("SLABS_START").Value = Val(txtPlbSlabFromN.Text.Trim())
            objCloneX.Attributes("SLABS_END").Value = Val(txtPlbSlabToN.Text.Trim())
            objCloneX.Attributes("SLABS_RATE").Value = Val(txtPlbSlabAmountN.Text.Trim())
            'objConX.DocumentElement.AppendChild(objCloneX)
            objConX.DocumentElement.AppendChild(objCloneX)
            ' objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode((objNode), True))
            'FinalXml.DocumentElement.AppendChild(objConX.ImportNode((objCloneX), True))
            objCloneX = objClone.CloneNode(True)

            'Slabs overlap validation Section for Update
            If blnUpdate Then
                Dim slabStart As Decimal = 0
                Dim slabEnd As Decimal = 0
                Dim newSlabFrom As Double = Convert.ToDouble(txtPlbSlabFromN.Text.Trim())
                Dim newSlabTo As Double = Val(txtPlbSlabToN.Text.Trim())

                For rowCounter = 0 To grdvPlbSlabN.Rows.Count - 1
                    If rowCounter <> UpdateRow Then
                        slabStart = Convert.ToDouble(grdvPlbSlabN.Rows(rowCounter).Cells(0).Text.Trim())
                        slabEnd = Convert.ToDouble(grdvPlbSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                        If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                            lblError.Text = "Slab Overlaps"
                            Return
                        End If
                    End If
                Next
            End If
            'End of Slabs overlap validation Section for Update


            Session("TempSlabsXmlPLB") = objConX.OuterXml
            'Session("slabBackup") = objConX.OuterXml
            Dim dsConn As New DataSet
            Dim objRdr As XmlNodeReader
            objRdr = New XmlNodeReader(objConX)
            dsConn.ReadXml(objRdr)
            grdvPlbSlabN.DataSource = dsConn.Tables("PLB_DETAILS").DefaultView
            grdvPlbSlabN.DataBind()



            txtPlbSlabAmountN.Text = ""
            txtPlbSlabFromN.Text = ""
            txtPlbSlabToN.Text = ""

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvPlbSlabN_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "EditX" Then
                Dim dt As New DataTable
                dt.Columns.Add("PLBTYPEID_TEMP")
                dt.Columns.Add("SLABS_START")
                dt.Columns.Add("SLABS_END")
                dt.Columns.Add("SLABS_RATE")

                Dim dr As DataRow
                For Each gv As GridViewRow In grdvPlbSlabN.Rows
                    dr = dt.NewRow
                    dr("PLBTYPEID_TEMP") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
                    dr("SLABS_START") = gv.Cells(0).Text
                    dr("SLABS_END") = gv.Cells(1).Text
                    dr("SLABS_RATE") = gv.Cells(2).Text
                    dt.Rows.Add(dr)
                Next

                Dim dv As DataView = dt.DefaultView
                dv.RowFilter = "PLBTYPEID_TEMP='" + e.CommandArgument.ToString + "'"
                txtPlbSlabFromN.Text = dv(0)("SLABS_START").ToString
                txtPlbSlabToN.Text = dv(0)("SLABS_END").ToString
                txtPlbSlabAmountN.Text = dv(0)("SLABS_RATE").ToString
                btnPlbSlabN.Text = "Update"
                btnPlbSlabN.CommandArgument = e.CommandArgument.ToString

            End If
            If e.CommandName = "DelX" Then
                Dim dt As New DataTable
                dt.Columns.Add("PLBTYPEID_TEMP")
                dt.Columns.Add("SLABS_START")
                dt.Columns.Add("SLABS_END")
                dt.Columns.Add("SLABS_RATE")
                Dim dr As DataRow
                For Each gv As GridViewRow In grdvPlbSlabN.Rows
                    If e.CommandArgument.ToString <> CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value Then
                        dr = dt.NewRow
                        dr("PLBTYPEID_TEMP") = CType(gv.FindControl("hdTEMP_SLAB_ID"), HiddenField).Value
                        dr("SLABS_START") = gv.Cells(0).Text
                        dr("SLABS_END") = gv.Cells(1).Text
                        dr("SLABS_RATE") = gv.Cells(2).Text
                        dt.Rows.Add(dr)
                    End If
                Next
                grdvPlbSlabN.DataSource = dt
                grdvPlbSlabN.DataBind()

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Private Function validateSlabRangePLB() As Boolean
        Try
            Dim rowCounter As Integer
            Dim slabStart As Decimal = 0
            Dim slabEnd As Decimal = 0
            Dim newSlabFrom As Double = Convert.ToDouble(txtPlbSlabFromN.Text.Trim())
            Dim newSlabTo As Double = Convert.ToDouble(txtPlbSlabToN.Text.Trim())

            For rowCounter = 0 To grdvPlbSlabN.Rows.Count - 1
                slabStart = Convert.ToDouble(grdvPlbSlabN.Rows(rowCounter).Cells(0).Text.Trim())
                slabEnd = Convert.ToDouble(grdvPlbSlabN.Rows(rowCounter).Cells(1).Text.Trim())
                If (newSlabFrom >= slabStart And newSlabFrom <= slabEnd) Or (newSlabTo >= slabStart And newSlabTo <= slabEnd) Then
                    lblError.Text = "Slab Overlaps"
                    Return True
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Function
#End Region

    Protected Sub grdvBidtAgency_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub grdvBidtAgency_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If dsBidtAgencyTotal IsNot Nothing Then
                    If dsBidtAgencyTotal.Rows.Count > 0 Then
                        e.Row.Cells(5).Text = "Total"
                        e.Row.Cells(5).Text = dsBidtAgencyTotal.Rows(0)("AIR_NETBOOKINGS").ToString()
                        e.Row.Cells(9).Text = dsBidtAgencyTotal.Rows(0)("NBS").ToString()
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Try
            Dim objInputXml, objOutputXml, objConnXml, objHwXml As New XmlDocument
            Dim objXmlNode, objCloneNode As XmlNode
            Dim objBC As New AAMS.bizIncetive.bzBusinessCase
            'Code For maintaining Previous Data if Update Fails
            Session("INCENTIVEPLAN_P") = Session("INCENTIVEPLAN")
            Session("BC_MIDT_BRK_P") = Session("BC_MIDT_BRK")
            Session("BIDTLAST3MONTH_P") = Session("BIDTLAST3MONTH")
            Session("TempSlabsXmlPLB_P") = Session("TempSlabsXmlPLB")
            'Code For maintaining Previous Data if Update Fails
            Dim strMinQulalificationCriteria As String = ""

            objConnXml.LoadXml("<CONNECTIVITY><BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' E_CONN_COUNT='' CONN_COUNT='' TOTAL=''/></CONNECTIVITY>")
            objHwXml.LoadXml("<HARDWARE><BC_EQP BC_EQP_CATG_ID='' BC_EQP_CATG_TYPE='' BC_EQP_CATG_COST='' E_PRODUCT_COUNT='' PRODUCT_COUNT='' TOTAL=''/></HARDWARE>")
            Dim objXml As New XmlDocument
            ' objXml.LoadXml(hdIncentiveType.Value)
            'objXmlNode = objXml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + rdbPaymentType.SelectedValue.Trim() + "']")


            '**************Code for Connectivity Information**************

            If validateData(objInputXml) = False Then
                Exit Sub
            End If


            If grdConnectivityN.Rows.Count = 0 Then
                lblError.Text = "Please Add at least one Connectivity"
                Return
            Else
                objXmlNode = objConnXml.DocumentElement.SelectSingleNode("BC_CONN")
                objCloneNode = objXmlNode.CloneNode(True)
                objConnXml.DocumentElement.RemoveChild(objXmlNode)
                For Each grConn As GridViewRow In grdConnectivityN.Rows

                    Dim txtConnNo As TextBox = CType(grConn.FindControl("txtConnNo"), TextBox)
                    objCloneNode.Attributes("CONN_COUNT").Value = txtConnNo.Text.Trim()

                    'If txtConnNo.Text.Trim().Length <> 0 Then
                    If txtConnNo.Text.Trim().Length = 0 Then
                        txtConnNo.Text = "0"
                    End If

                    Dim lblConnName As Label = CType(grConn.FindControl("lblConnName"), Label)
                    objCloneNode.Attributes("BC_ONLINE_CATG_NAME").Value = lblConnName.Text.Trim()

                    Dim lblConnCatCost As Label = CType(grConn.FindControl("lblConnCatCost"), Label)
                    objCloneNode.Attributes("BC_ONLINE_CATG_COST").Value = lblConnCatCost.Text.Trim()


                    Dim lblEcConnCat As Label = CType(grConn.FindControl("lblEConnCount"), Label)
                    objCloneNode.Attributes("E_CONN_COUNT").Value = lblEcConnCat.Text.Trim()


                    Dim hdOnlineCatID As HiddenField = CType(grConn.FindControl("hdOnlineCatID"), HiddenField)
                    objCloneNode.Attributes("BC_ONLINE_CATG_ID").Value = hdOnlineCatID.Value.Trim()


                    objCloneNode.Attributes("TOTAL").Value = Val(txtConnNo.Text.Trim()) * Val(lblConnCatCost.Text.Trim())


                    objConnXml.DocumentElement.AppendChild(objCloneNode)

                    objCloneNode = objXmlNode.CloneNode(True)
                    'End If
                Next
                objXmlNode = Nothing
                objCloneNode = Nothing
            End If
            '**************End of Code for Connectivity Information**************



            '**************Code for Hardware Information**************
            If grdvHardwareN.Rows.Count = 0 Then
                lblError.Text = "Please Add at least one Hardware"
                Return
            Else
                objXmlNode = objHwXml.DocumentElement.SelectSingleNode("BC_EQP")
                objCloneNode = objXmlNode.CloneNode(True)
                objHwXml.DocumentElement.RemoveChild(objXmlNode)
                For Each grhw As GridViewRow In grdvHardwareN.Rows

                    Dim txtHwNoN As TextBox = CType(grhw.FindControl("txtHwNoN"), TextBox)
                    objCloneNode.Attributes("PRODUCT_COUNT").Value = txtHwNoN.Text.Trim()

                    ' If txtHwNoN.Text.Trim().Length <> 0 Then
                    If txtHwNoN.Text.Trim().Length = 0 Then
                        txtHwNoN.Text = "0"
                    End If


                    Dim lblHwNameN As Label = CType(grhw.FindControl("lblHwNameN"), Label)
                    objCloneNode.Attributes("BC_EQP_CATG_TYPE").Value = lblHwNameN.Text.Trim()

                    Dim lblHwCostN As Label = CType(grhw.FindControl("lblHwCostN"), Label)
                    objCloneNode.Attributes("BC_EQP_CATG_COST").Value = lblHwCostN.Text.Trim()


                    Dim lblEcHw As Label = CType(grhw.FindControl("lblHwNoNEcount"), Label)
                    objCloneNode.Attributes("E_PRODUCT_COUNT").Value = lblEcHw.Text.Trim()


                    Dim hdHwIDN As HiddenField = CType(grhw.FindControl("hdHwIDN"), HiddenField)
                    objCloneNode.Attributes("BC_EQP_CATG_ID").Value = hdHwIDN.Value.Trim()


                    objCloneNode.Attributes("TOTAL").Value = Val(txtHwNoN.Text.Trim()) * Val(lblHwCostN.Text.Trim())



                    objHwXml.DocumentElement.AppendChild(objCloneNode)

                    objCloneNode = objXmlNode.CloneNode(True)
                    ' End If

                Next
                objXmlNode = Nothing
                objCloneNode = Nothing
            End If
            '**************End of Code for Hardware Information**************


            '*******************************************Input Preparing**************************************************
            'objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE BC_ID='' CHAIN_CODE='' BC_EFFECTIVE_FROM='' BC_VALID_TILL='' BC_DTTI_CREATED='' PAYMENT_CYCLE_ID='' FINAL_APPROVED='' FINAL_APPROVED_BY='' FINAL_APPROVED_DTTI='' INC_TYPE_ID='' SOLE_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT='' UPFRONT_AMOUNT='' FIXED_UPFRONT='' EmployeeID='' RATE='' EXP_SEGMENT='' CONV_PER='' MIN_MON_SEG='' MONTHS=''/><BC_PLAN><INC_TYPE INC_TYPE_ID='' INC_TYPE_NAME='' INC_SLAB_REQUIRED='' /><CASE INC_PLAN_ID='' INC_PLAN_NAME=''><PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE='' /><PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE='' /><NIDT_FIELDS_ID ID='' /></CASE></BC_PLAN><BC_MIDT_BRK BC_ID='' BR_ID='' FROMDATE='' TODATE='' RATE='' TOTAL='' PER='' LESS='' /> <BC_AGENCY_GROUP_DETAILS BC_ID=''><BC_MIDT_GROUP LASTAVAIL='' A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/></BC_AGENCY_GROUP_DETAILS > </INC_UPDATE_BUSINESSCASE_INPUT>")
            objInputXml.LoadXml("<INC_UPDATE_BUSINESSCASE_INPUT><BUSINESSCASE BC_ID='' CHAIN_CODE='' BC_EFFECTIVE_FROM='' BC_VALID_TILL='' BC_DTTI_CREATED='' PAYMENT_CYCLE_ID='' FINAL_APPROVED='' FINAL_APPROVED_BY='' FINAL_APPROVED_DTTI='' INC_TYPE_ID='' PAYMENTTYPEID='' UPFRONTTYPEID='' PLBTYPEID='' PLBSLAB='' UPFRONT_AMOUNT='' BONUS_AMOUNT='' FIXED_PAYMENT=''  RATE='' EXP_SEGMENT='' CONV_PER='' MIN_MON_SEG='' MONTHS='' EmployeeID='' BC_FI_GROSS='' BC_FI_EX_IC='' BC_FI_EX_ICIT='' BC_FI_NET=''  SEG_LESS_IC='' SEG_LESS_IC_PER='' SEG_LESS_IC_IT='' SEG_LESS_IC_IT_PER='' INC_RATE_GROSS='' INC_RATE_W_IC='' INC_RATE_W_IT_IC='' INC_RATE_NET='' TOTAL_COST='' CPS_GROSS=''  CPS_EXCL_IC='' CPS_EXCL_IC_IT='' CPS_NET='' REFRESH='' SIGNUPAMOUNT='' ADJUSTABLE='' NO_OF_PAYMENTS='' FOR_PERIOD_OF=''/><BC_PLAN><SLAB_QUALIFICATION ID=''/><MIN_SEGMENT ID='' MIN_SEGMENT_VAL=''/> <INC_TYPE INC_TYPE_ID='' INC_TYPE_NAME='' INC_SLAB_REQUIRED=''/><CASE INC_PLAN_ID='' INC_PLAN_NAME=''><PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/><NIDT_FIELDS_ID ID=''/></CASE></BC_PLAN><BC_MIDT_BRK BR_ID='' FROMDATE='' TODATE='' RATE=''  TOTAL='' PER='' LESS='' MIDT_CONV_PER=''/><BC_AGENCY_GROUP_DETAILS BC_ID=''><BC_MIDT_GROUP LASTAVAIL='' A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/><BC_MIDT_AGENCY Lcode='' Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/></BC_AGENCY_GROUP_DETAILS><PLB BC_ID='' PLBTYPEID='' PLBAMOUNT=''></PLB ></INC_UPDATE_BUSINESSCASE_INPUT>")


            Dim objCaseNode As XmlNode
            objCaseNode = objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/CASE")
            With objInputXml.DocumentElement.SelectSingleNode("BUSINESSCASE")
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        .Attributes("BC_ID").Value = Request.QueryString("BCaseID").Trim()
                    End If
                End If
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    .Attributes("CHAIN_CODE").Value = objED.Decrypt(Request.QueryString("Chain_Code").Trim())
                Else
                    .Attributes("CHAIN_CODE").Value = ""
                End If

                ''

                .Attributes("REFRESH").InnerText = hdRefreshAction.Value


                ''

                .Attributes("BC_EFFECTIVE_FROM").Value = objeAAMS.ConvertTextDate(txtPeriodFrom.Text.Trim())
                .Attributes("BC_VALID_TILL").Value = objeAAMS.ConvertTextDate(txtPeriodTo.Text.Trim())

                '*********************Need to Discuss*********************
                'objInputXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK").Attributes("FROMDATE").Value = objeAAMS.ConvertTextDate(txtPeriodFrom.Text.Trim())
                'objInputXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK").Attributes("TODATE").Value = objeAAMS.ConvertTextDate(txtPeriodTo.Text.Trim())
                'objInputXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK").Attributes("RATE").Value = "10"
                '*********************Need to Discuss*********************


                .Attributes("PAYMENT_CYCLE_ID").Value = drpBillingCycle.SelectedValue.Trim()
                If Session("Security") IsNot Nothing Then
                    .Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
                End If


                .Attributes("BC_DTTI_CREATED").Value = ""
                .Attributes("FINAL_APPROVED").Value = ""
                .Attributes("FINAL_APPROVED_BY").Value = ""
                .Attributes("FINAL_APPROVED_DTTI").Value = ""



                'New Attr
                .Attributes("RATE").Value = txtIncRateN.Text.Trim()
                .Attributes("EXP_SEGMENT").Value = txtIncExpectedSegN.Text.Trim()
                .Attributes("CONV_PER").Value = txtConversionPer.Text.Trim()

                '.Attributes("INC_TYPE_ID").Value = drpPlanType.SelectedValue.Trim()

                .Attributes("INC_TYPE_ID").Value = rdbIncentiveType.SelectedValue.Trim() 'Payment Type
                .Attributes("UPFRONTTYPEID").Value = rdbUpfrontTypeName.SelectedValue.Trim()
                .Attributes("PAYMENTTYPEID").Value = rdbPaymentType.SelectedValue.Trim()
                If chkPlb.Checked Then
                    .Attributes("PLBTYPEID").Value = rdbPlbTypeName.SelectedValue.Trim()
                End If

                .Attributes("PLBSLAB").Value = chkPlb.Checked.ToString().ToUpper()

                .Attributes("UPFRONT_AMOUNT").Value = txtUpfrontAmount.Text.Trim()


                ''added on 04 sep 10
                .Attributes("SIGNUPAMOUNT").Value = txtSignUpAmount.Text
                .Attributes("ADJUSTABLE").Value = chkAdjustable.Checked.ToString().ToUpper


                If rdbIncentiveType.SelectedValue = "1" Then 'UPfront
                    If rdbUpfrontTypeName.SelectedValue = "1" Then ' (UPfront, Onetime)
                        .Attributes("FOR_PERIOD_OF").Value = txtForThePeriodOf.Text
                    ElseIf rdbUpfrontTypeName.SelectedValue = "3" Then '(UPfront, Fixed) 
                        .Attributes("NO_OF_PAYMENTS").Value = txtNoOfPayments.Text
                    End If
                End If
                ''end



                'For Fix Incentive 

                'BC_FI_GROSS='' BC_FI_EX_IC='' BC_FI_EX_ICIT='' BC_FI_NET=''

                .Attributes("BC_FI_GROSS").Value = Val(lblFixIncGross.Text.Trim())
                .Attributes("BC_FI_EX_IC").Value = Val(lblFixIncIC.Text.Trim())
                .Attributes("BC_FI_EX_ICIT").Value = Val(lblFixIncICIT.Text.Trim())
                .Attributes("BC_FI_NET").Value = Val(lblFixIncNet.Text.Trim())
                .Attributes("FIXED_PAYMENT").Value = txtFixIncentive.Text.Trim()
                'For Fix Incentive

                .Attributes("MIN_MON_SEG").Value = txtFixIncMinMonthSeg.Text.Trim()
                .Attributes("MONTHS").Value = txtFixIncMonth.Text.Trim()
            End With



            '************** Code for Incentive Plan Maintaining Data********************************
            If Session("INCENTIVEPLAN") IsNot Nothing Then
                Dim objPlanXml As New XmlDocument
                objPlanXml.LoadXml(Session("INCENTIVEPLAN").ToString())

                'Code for Saving Updated Data of Criteria Name
                Dim grdRow As GridViewRow
                For RowC As Integer = 0 To grdvIncentivePlan.Rows.Count - 1
                    grdRow = grdvIncentivePlan.Rows(RowC)
                    Dim txtPlanNameGrd As TextBox = CType(grdRow.FindControl("txtPlanName"), TextBox)

                    Dim lnkDeletePlanGrd As LinkButton = CType(grdRow.FindControl("lnkDeletePlan"), LinkButton)
                    objXmlNode = objPlanXml.DocumentElement.SelectSingleNode("CASE[@TEMP_SLAB_ID_MAIN='" + lnkDeletePlanGrd.CommandArgument.Trim() + "']")
                    If objXmlNode IsNot Nothing Then
                        objXmlNode.Attributes("INC_PLAN_NAME").Value = txtPlanNameGrd.Text.Trim()

                        If txtPlanNameGrd.Text.Trim().Length = 0 Then

                            lblInnerError.Text = "Case Name is mandatory for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                            lblError.Text = "Case Name is mandatory for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                            Return
                        End If

                        If objXmlNode.SelectSingleNode("NIDT_FIELDS_ID").Attributes("ID").Value.Length = 0 Then
                            lblError.Text = "Please Select Criteria for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                            lblInnerError.Text = "Please Select Criteria for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                            Return
                        End If


                        '######################################
                        '@  Start of Added By Abhishek
                        '######################################
                        Dim GvIncSlabsNested As GridView = grdRow.FindControl("GvIncSlabsNested")
                        If GvIncSlabsNested IsNot Nothing Then
                            If GvIncSlabsNested.Rows.Count > 0 Then
                                For Each gvRow_Nested As GridViewRow In GvIncSlabsNested.Rows
                                    '<PLAN_DETAILS SLABS_START="100" SLABS_END="200" SLABS_RATE="10" TEMP_SLAB_ID="0" />
                                    Dim str_hdTempID As String = Val(CType(gvRow_Nested.FindControl("hdTempID"), HiddenField).Value)
                                    For Each objNode As XmlNode In objXmlNode.SelectNodes("PLAN_DETAILS[@TEMP_SLAB_ID='" + str_hdTempID + "']")
                                        Dim txtAmount As TextBox = CType(gvRow_Nested.FindControl("txtAmount"), TextBox)
                                        objNode.Attributes("SLABS_RATE").Value = txtAmount.Text

                                        If objNode.Attributes("SLABS_RATE").Value.Trim.Length = 0 Then
                                            lblInnerError.Text = "Slab rate is mandatory for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                                            lblError.Text = "Slab rate is mandatory for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                                            Return
                                        End If

                                    Next
                                Next
                            Else
                                lblInnerError.Text = "Slab is mandatory for plan   [ " + txtPlanNameGrd.Text.Trim() + " ]"
                                lblError.Text = "Slab is mandatory for plan  [ " + txtPlanNameGrd.Text.Trim() + " ]"
                                Return
                            End If
                        End If



                        '######################################
                        '@end of  Added By Abhishek
                        '######################################
                    End If
                Next

                Session("INCENTIVEPLAN") = objPlanXml.OuterXml

                '######################################
                '@ Start of code Added By Abhishek 
                'Duplicate Check of Case Name
                If getDuplicateCaseName() = True Then
                    lblInnerError.Text = "Plan Name cannot be duplicate"
                    lblError.Text = "Plan Name cannot be duplicate"
                    Return
                End If
                '@ End of code Added By Abhishek 
                '######################################


                '**************************************************************

                'If Session("slabBackup") IsNot Nothing Then
                'If blnSlabChk = True Then


                Dim objFooterX As New XmlDocument
                'chkSelectedCriteria
                Dim strMIDT As String = ""
                Dim blnCrchk As Boolean = False
                For i As Integer = 0 To chkSelectedCriteria.Items.Count - 1
                    If chkSelectedCriteria.Items(i).Selected Then
                        If String.IsNullOrEmpty(strMIDT) Then
                            blnCrchk = True
                            strMIDT = chkSelectedCriteria.Items(i).Value
                        Else
                            strMIDT = strMIDT + "," + chkSelectedCriteria.Items(i).Value
                        End If
                    End If
                Next


                If (grdvIncentivePlan.Rows.Count = 0) Then
                    If (blnCrchk = False) Then
                        If txtCaseName.Text.Trim().Length > 0 Then
                            lblInnerError.Text = "Please Select Criteria of last plan [ " + txtCaseName.Text.Trim() + " ]"
                            lblError.Text = "Please Select Criteria of last plan [ " + txtCaseName.Text.Trim() + " ]"
                            Return
                        End If

                    End If
                End If

                '######################################
                '@ Start of code Added By Abhishek               
                'If (grdvIncentivePlan.Rows.Count = 0) Then
                If (grdvSlabN.Rows.Count > 0) And (blnCrchk = True) Then
                    If txtCaseName.Text.Trim().Length = 0 Then
                        lblError.Text = "Case Name is mandatory for last plan."
                        Return
                    End If

                End If
                'End If    
                If (grdvSlabN.Rows.Count > 0) Then
                    'If (blnCrchk = True) Then
                    '    If txtCaseName.Text.Trim().Length > 0 Then

                    '        lblInnerError.Text = "Please Select Criteria of last plan [ " + txtCaseName.Text.Trim() + " ]"
                    '        lblError.Text = "Please Select Criteria of last plan [ " + txtCaseName.Text.Trim() + " ]"
                    '        Return
                    '    End If
                    'End If
                Else
                    If (blnCrchk = True) Then
                        If txtCaseName.Text.Trim().Length > 0 Then
                            lblInnerError.Text = "Slab is mandatory for plan   [ " + txtCaseName.Text.Trim() + " ]"
                            lblError.Text = "Slab is mandatory for plan  [ " + txtCaseName.Text.Trim() + " ]"
                            Return
                        End If
                    End If
                End If
                '@ End of code Added By Abhishek
                '######################################

                '  objFooterX.LoadXml(Session("slabBackup").ToString())

                'End If

                '**************************************************************


                'Code for Saving First Node Xml
                objFooterX.LoadXml("<FIRST_NODE><INC_TYPE INC_TYPE_ID='' INC_TYPE_NAME='' INC_SLAB_REQUIRED='' /><CASE INC_PLAN_ID='' INC_PLAN_NAME='' TEMP_SLAB_ID_MAIN=''><PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE='' TEMP_SLAB_ID='' /><NIDT_FIELDS_ID ID='' /></CASE></FIRST_NODE>")
                objXmlNode = objFooterX.DocumentElement.SelectSingleNode("CASE/PLAN_DETAILS")
                objCloneNode = objXmlNode.CloneNode(True)
                objFooterX.DocumentElement.SelectSingleNode("CASE").RemoveChild(objXmlNode)

                For Each grdRowSlab As GridViewRow In grdvSlabN.Rows
                    objCloneNode.Attributes("SLABS_START").Value = grdRowSlab.Cells(0).Text.Trim()
                    objCloneNode.Attributes("SLABS_END").Value = grdRowSlab.Cells(1).Text.Trim()
                    ' objCloneNode.Attributes("SLABS_RATE").Value = grdRowSlab.Cells(2).Text.Trim()
                    objCloneNode.Attributes("SLABS_RATE").Value = CType(grdRowSlab.FindControl("txtAmount"), TextBox).Text

                    '######################################
                    '@  Start of Added By Abhishek
                    If (grdvSlabN.Rows.Count > 0) And (txtCaseName.Text.Trim().Length <> 0) And (blnCrchk = True) Then
                        If objCloneNode.Attributes("SLABS_RATE").Value.Trim().Length = 0 Then
                            lblInnerError.Text = "Slab Rate is mandatory for plan [ " + txtCaseName.Text.Trim() + " ]"
                            lblError.Text = "Slab Rate is mandatory for plan [ " + txtCaseName.Text.Trim() + " ]"
                            Return
                        End If
                    End If
                    '@ End  of Added By Abhishek
                    '######################################

                    objFooterX.DocumentElement.SelectSingleNode("CASE").AppendChild(objCloneNode)
                    objCloneNode = objXmlNode.CloneNode(True)
                Next

                objFooterX.DocumentElement.SelectSingleNode("CASE/NIDT_FIELDS_ID").Attributes("ID").Value = strMIDT
                objFooterX.DocumentElement.SelectSingleNode("CASE").Attributes("INC_PLAN_ID").Value = ""
                objFooterX.DocumentElement.SelectSingleNode("CASE").Attributes("INC_PLAN_NAME").Value = txtCaseName.Text.Trim()




                'Duplicate Check of Case Name
                If (grdvIncentivePlan.Rows.Count > 0) Then ' If (grdvIncentivePlan.Rows.Count=0) Then
                    If getDuplicateCaseName(txtCaseName.Text.Trim()) = True Then
                        lblError.Text = "Plan Name cannot be duplicate"
                        lblInnerError.Text = "Plan Name cannot be duplicate"
                        Return
                    End If
                End If
                'Duplicate Check of Case Name
                ' 




                If (grdvSlabN.Rows.Count > 0) And (txtCaseName.Text.Trim().Length <> 0) And (blnCrchk = True) Then
                    'Dim objNodeFooter As XmlNode = objPlanXml.DocumentElement.SelectSingleNode("CASE/NIDT_FIELDS_ID[@ID='" + objFooterX.DocumentElement.SelectSingleNode("CASE/NIDT_FIELDS_ID").Attributes("ID").Value + "']").ParentNode
                    'If objNodeFooter Is Nothing Then
                    '    objPlanXml.DocumentElement.AppendChild(objPlanXml.ImportNode(objFooterX.DocumentElement.SelectSingleNode("CASE"), True))
                    'Else
                    '    objPlanXml.DocumentElement.RemoveChild(objNodeFooter)

                    '    objPlanXml.DocumentElement.AppendChild(objPlanXml.ImportNode(objFooterX.DocumentElement.SelectSingleNode("CASE"), True))
                    'End If

                    objPlanXml.DocumentElement.AppendChild(objPlanXml.ImportNode(objFooterX.DocumentElement.SelectSingleNode("CASE"), True))
                End If
                'End If

                'for slab qualification
                strMIDT = ""
                For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                    If chkSLABQUALIFICATION.Items(i).Selected Then
                        If String.IsNullOrEmpty(strMIDT) Then
                            blnCrchk = True
                            strMIDT = chkSLABQUALIFICATION.Items(i).Value
                        Else
                            strMIDT = strMIDT + "," + chkSLABQUALIFICATION.Items(i).Value
                        End If
                    End If
                Next

                'end


                Session("INCENTIVEPLAN") = objPlanXml.OuterXml
                resetTempID()
                UpdateXmlIncentivePlan()
                objPlanXml.LoadXml(Session("INCENTIVEPLAN").ToString())
                'Code for Saving Updated Data of Criteria Name


                objXmlNode = objInputXml.DocumentElement.SelectSingleNode("BC_PLAN")
                objInputXml.DocumentElement.RemoveChild(objXmlNode)
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objPlanXml.DocumentElement, True))
                objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/SLAB_QUALIFICATION").Attributes("ID").Value = strMIDT


                'Code for Removing 

                objXmlNode = objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE")
                If objXmlNode IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objXmlNode)
                End If
                'End of Code

            Else
                lblError.Text = "Please Add Incentive Plan"
            End If




           





            For Each xHw As XmlNode In objHwXml.DocumentElement.SelectNodes("BC_EQP")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xHw, True))
            Next

            For Each xCon As XmlNode In objConnXml.DocumentElement.SelectNodes("BC_CONN")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(xCon, True))
            Next




            Dim objTempXml As New XmlDocument
            objTempXml.LoadXml("<T><INC_TYPE INC_TYPE_ID='' INC_TYPE_NAME='' INC_SLAB_REQUIRED='' /></T>")
            objTempXml.DocumentElement.SelectSingleNode("INC_TYPE").Attributes("INC_TYPE_ID").Value = rdbIncentiveType.SelectedValue.Trim() 'drpPlanType.SelectedValue.Trim()
            Dim ndbc As XmlNode = objTempXml.DocumentElement.SelectSingleNode("INC_TYPE")
            objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").AppendChild(objInputXml.ImportNode(ndbc, True))

            'For blank of INC_PLAN_ID
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action") = "N" Then
                    For Each xNode As XmlNode In objInputXml.DocumentElement.SelectNodes("BC_PLAN/CASE")
                        xNode.Attributes("INC_PLAN_ID").Value = ""
                    Next
                End If
            End If

            'End of  blank of INC_PLAN_ID

            'Removing Slab conditional base

            'objXml.LoadXml(hdIncentiveType.Value)
            ' objXmlNode = objXml.DocumentElement.SelectSingleNode("INC_TYPE[@INC_TYPE_ID='" + rdbPaymentType.SelectedValue.Trim() + "']")


            'If objXmlNode.Attributes("INC_SLAB_REQUIRED").Value = "FALSE" Then
            '    If objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/CASE") IsNot Nothing Then
            '        objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/CASE").RemoveAll()
            '        objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/CASE"))
            '    End If
            '    objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value = "FALSE"
            '    objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").AppendChild(objCaseNode)
            '    objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/CASE").Attributes("INC_PLAN_NAME").Value = txtCaseName.Text.Trim()
            '    'objCaseNode
            'Else
            objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/INC_TYPE").Attributes("INC_SLAB_REQUIRED").Value = "TRUE"
            '  End If


            'Removing Slab conditional base
            Dim objInputHistory As New XmlDocument
            If Session("INCENTIVEPLAN_HISTORY") IsNot Nothing Then
                objInputHistory.LoadXml(Session("INCENTIVEPLAN_HISTORY"))

            End If


            For Each objCloneNode In objInputXml.DocumentElement.SelectNodes("BC_PLAN/CASE")
                If objCloneNode.Attributes("INC_PLAN_NAME").Value.Trim() = "" Then
                    objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objCloneNode)
                ElseIf objInputHistory IsNot Nothing Then
                    For Each objHistoryNode As XmlNode In objInputHistory.DocumentElement.SelectNodes("CASE")
                        If objCloneNode.Attributes("INC_PLAN_NAME").Value.Trim() = objHistoryNode.Attributes("INC_PLAN_NAME").InnerText.Trim Then
                            objCloneNode.Attributes("INC_PLAN_ID").Value = objHistoryNode.Attributes("INC_PLAN_ID").InnerText
                        End If
                    Next

                End If
            Next




            'Code for Breakup Details
            Dim objBreakXml As New XmlDocument
            If Session("BC_MIDT_BRK") IsNot Nothing Then
                objBreakXml.LoadXml(Session("BC_MIDT_BRK").ToString())
                objXmlNode = objInputXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK")
                objCloneNode = objXmlNode.CloneNode(True)
                objInputXml.DocumentElement.RemoveChild(objXmlNode)
                Dim strBcID As String = ""
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "U" Then
                        strBcID = Request.QueryString("BCaseID").Trim()
                    End If
                End If

                For Each xnode As XmlNode In objBreakXml.DocumentElement.SelectNodes("BC_MIDT_BRK")
                    'objCloneNode.Attributes("BC_ID").Value = strBcID
                    objCloneNode.Attributes("BR_ID").Value = xnode.Attributes("BR_ID").Value
                    objCloneNode.Attributes("FROMDATE").Value = hdBreakupFromDt.Value.Trim()
                    objCloneNode.Attributes("TODATE").Value = hdBreakupToDt.Value.Trim()
                    objCloneNode.Attributes("RATE").Value = xnode.Attributes("RATE").Value

                    objCloneNode.Attributes("PER").Value = xnode.Attributes("PER").Value
                    objCloneNode.Attributes("LESS").Value = xnode.Attributes("LESS").Value
                    objCloneNode.Attributes("TOTAL").Value = xnode.Attributes("TOTAL").Value
                    objCloneNode.Attributes("MIDT_CONV_PER").Value = xnode.Attributes("MIDT_CONV_PER").Value


                    objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objCloneNode = objXmlNode.CloneNode(True)

                Next

            End If

            'Code  Segment for Agency Group
            Dim objMidtNode, objMidtNodeClone As XmlNode
            objMidtNode = objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS/BC_MIDT_GROUP")
            objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS").RemoveChild(objMidtNode)
            objMidtNodeClone = objMidtNode.CloneNode(True)
            For Each grdrowMidt As GridViewRow In GvBGroupMIDT.Rows
                objMidtNodeClone.Attributes("LASTAVAIL").Value = grdrowMidt.Cells(0).Text.Trim
                objMidtNodeClone.Attributes("A").Value = IIf(grdrowMidt.Cells(1).Text.Trim <> "&nbsp;", grdrowMidt.Cells(1).Text.Trim, "")
                objMidtNodeClone.Attributes("B").Value = IIf(grdrowMidt.Cells(2).Text.Trim <> "&nbsp;", grdrowMidt.Cells(2).Text.Trim, "")
                objMidtNodeClone.Attributes("G").Value = IIf(grdrowMidt.Cells(3).Text.Trim <> "&nbsp;", grdrowMidt.Cells(3).Text.Trim, "")
                objMidtNodeClone.Attributes("P").Value = IIf(grdrowMidt.Cells(4).Text.Trim <> "&nbsp;", grdrowMidt.Cells(4).Text.Trim, "")
                objMidtNodeClone.Attributes("W").Value = IIf(grdrowMidt.Cells(5).Text.Trim <> "&nbsp;", grdrowMidt.Cells(5).Text.Trim, "")
                objMidtNodeClone.Attributes("TOTAL").Value = IIf(grdrowMidt.Cells(6).Text.Trim <> "&nbsp;", grdrowMidt.Cells(6).Text.Trim, "")
                objMidtNodeClone.Attributes("A_PER").Value = IIf(grdrowMidt.Cells(7).Text.Trim <> "&nbsp;", grdrowMidt.Cells(7).Text.Trim, "")
                objMidtNodeClone.Attributes("B_PER").Value = IIf(grdrowMidt.Cells(8).Text.Trim <> "&nbsp;", grdrowMidt.Cells(8).Text.Trim, "")
                objMidtNodeClone.Attributes("G_PER").Value = IIf(grdrowMidt.Cells(9).Text.Trim <> "&nbsp;", grdrowMidt.Cells(9).Text.Trim, "")
                objMidtNodeClone.Attributes("P_PER").Value = IIf(grdrowMidt.Cells(10).Text.Trim <> "&nbsp;", grdrowMidt.Cells(10).Text.Trim, "")
                objMidtNodeClone.Attributes("W_PER").Value = IIf(grdrowMidt.Cells(11).Text.Trim <> "&nbsp;", grdrowMidt.Cells(11).Text.Trim, "")
                objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS").AppendChild(objMidtNodeClone)
                objMidtNodeClone = objMidtNode.CloneNode(True)
            Next
            If Request.QueryString("BCaseID") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS").Attributes("BC_ID").Value = Request.QueryString("BCaseID")
            End If


            'Code  Segment for Agency Group



            'Code Segment for BIDT Group 
            If Session("BIDTLAST3MONTH") Is Nothing Then
                GetBidtGroupData()
            End If

            Dim xmlDocBidt As New XmlDocument
            If Session("BIDTLAST3MONTH") IsNot Nothing Then
                xmlDocBidt.LoadXml(Session("BIDTLAST3MONTH").ToString())
                For Each xBidtNode As XmlNode In xmlDocBidt.DocumentElement.SelectNodes("BC_BIDT_GROUP")
                    objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS").AppendChild(objInputXml.ImportNode(xBidtNode, True))
                Next
            Else
                xmlDocBidt.LoadXml("<INC_GET_BIDT_OUTPUT><BC_BIDT_GROUP LASTAVAIL='' PRODUCTIVITY='' AIRNET='' HOTEL='' CAR='' INSURANCE='' NBS='' WITHPASSIVE='' /></INC_GET_BIDT_OUTPUT >")
                objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS").AppendChild(objInputXml.ImportNode(xmlDocBidt.DocumentElement.SelectSingleNode("BC_BIDT_GROUP"), True))
            End If



            'Duplicate Check of Case Name
            If getDuplicateCaseName() = True Then
                lblError.Text = "Plan Name cannot be duplicate"
                Return
            End If
            'Duplicate Check of Case Name


            'End of Code for Breakup Details


            'Code for PLB Data Input
            Dim objXmlPlb As New XmlDocument
            objXmlPlb.LoadXml("<PLB_ROOT><PLB BC_ID='' PLBTYPEID='' PLBAMOUNT='' /></PLB_ROOT>")
            Dim objPlbTempXml As New XmlDocument

            If Session("TempSlabsXmlPLB") IsNot Nothing Then
                objPlbTempXml.LoadXml(Session("TempSlabsXmlPLB").ToString())

                If chkPlb.Checked Then
                    If rdbPlbTypeName.SelectedValue.Trim() = "2" Then
                        For Each objNPlb As XmlNode In objPlbTempXml.DocumentElement.SelectNodes("PLB_DETAILS")
                            objInputXml.DocumentElement.SelectSingleNode("PLB").AppendChild(objInputXml.ImportNode(objNPlb, True))
                        Next

                    End If
                End If



            End If


            If chkPlb.Checked Then
                With objInputXml.DocumentElement.SelectSingleNode("PLB")
                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action") = "U" Then
                            .Attributes("BC_ID").Value = Request.QueryString("BCaseID").Trim()
                        End If
                    End If
                    .Attributes("PLBTYPEID").Value = rdbPlbTypeName.SelectedValue.Trim()
                    If chkPlb.Checked Then
                        If rdbPlbTypeName.SelectedValue.Trim() = "1" Then
                            .Attributes("PLBAMOUNT").Value = Val(txtPlbBonus.Text.Trim())
                        Else
                            .Attributes("PLBAMOUNT").Value = ""
                        End If
                    Else
                        .Attributes("PLBAMOUNT").Value = ""
                    End If

                End With
            End If
            ' objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS/PLB").RemoveChild(objInputXml.DocumentElement.SelectSingleNode("BC_AGENCY_GROUP_DETAILS/PLB"))


            'End of Code for PLB Data Input

            'Try
            '    ' objInputXml.Save("C:\admin\objInputXml.xml")
            'Catch ex As Exception
            'End Try


            '#################################################################
            '@ Start of Code Added By Abhishek on 27 jan 2010
            '#################################################################
            'for Minimum Qualification Criteria
            Dim ObjNodeMinSegmentDoc As New XmlDocument
            ObjNodeMinSegmentDoc.LoadXml("<BC_PLAN><MIN_SEGMENT ID='' MIN_SEGMENT_VAL='' /></BC_PLAN>")
            Dim objnodePlan As XmlNode = objInputXml.DocumentElement.SelectSingleNode("BC_PLAN/MIN_SEGMENT")
            If objnodePlan IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").RemoveChild(objnodePlan)
            End If

            If rdbIncentiveType.SelectedValue = "2" Then
                strMinQulalificationCriteria = ""
                For i As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                    If ChkMinimunCriteriaNew.Items(i).Selected Then
                        If String.IsNullOrEmpty(strMinQulalificationCriteria) Then
                            strMinQulalificationCriteria = ChkMinimunCriteriaNew.Items(i).Value
                        Else
                            strMinQulalificationCriteria = strMinQulalificationCriteria + "," + ChkMinimunCriteriaNew.Items(i).Value
                        End If
                    End If
                Next

                ObjNodeMinSegmentDoc.DocumentElement.SelectSingleNode("MIN_SEGMENT").Attributes("ID").Value = strMinQulalificationCriteria
                ObjNodeMinSegmentDoc.DocumentElement.SelectSingleNode("MIN_SEGMENT").Attributes("MIN_SEGMENT_VAL").Value = TxtMinSegCriteriaValueNew.Text
                objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").AppendChild(objInputXml.ImportNode(ObjNodeMinSegmentDoc.DocumentElement.SelectSingleNode("MIN_SEGMENT"), True))
            Else
                objInputXml.DocumentElement.SelectSingleNode("BC_PLAN").AppendChild(objInputXml.ImportNode(ObjNodeMinSegmentDoc.DocumentElement.SelectSingleNode("MIN_SEGMENT"), True))
            End If

            '#################################################################
            '@ End  of Code Added By Abhishek on 27 jan 2010
            '#################################################################



            objOutputXml = objBC.Update(objInputXml)

            Try
                objInputXml.Save("C:\admin\objBCInut.xml")
            Catch ex As Exception
            End Try

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = "Records updated successfully"
                hdBcID.Value = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("BC_ID").Value.Trim()
                'type = "../Incentive/INCUP_BacseDetails.aspx?Action=U&Chain_Code=" + ChainCode + "&BCaseID=" + BCaseID; 
                Dim strChainCode As String = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("CHAIN_CODE").Value.Trim())
                Dim strBcCode As String = objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("BC_ID").Value.Trim()

                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action") = "N" Then
                        Response.Redirect("INCUP_BacseDetails.aspx?MSG=I&Action=U&Chain_Code=" + strChainCode + "&BCaseID=" + strBcCode)
                    Else
                        Response.Redirect("INCUP_BacseDetails.aspx?MSG=U&Action=U&Chain_Code=" + strChainCode + "&BCaseID=" + strBcCode)

                    End If
                End If
                ' LoadData(objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("CHAIN_CODE").Value.Trim(), objOutputXml.DocumentElement.SelectSingleNode("BUSINESSCASE").Attributes("BC_ID").Value.Trim())
                'GetAllRemrks(hdBcID.Value.Trim())
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                'Code For maintaining Previous Data if Update Fails
                Session("INCENTIVEPLAN") = Session("INCENTIVEPLAN_P")
                Session("BC_MIDT_BRK") = Session("BC_MIDT_BRK_P")
                Session("BIDTLAST3MONTH") = Session("BIDTLAST3MONTH_P")
                Session("TempSlabsXmlPLB") = Session("TempSlabsXmlPLB_P")
                'Code For maintaining Previous Data if Update Fails

            End If

            'Session("HARDWARE")
            'Session("CONNECTIVITY")



            'End of Code for Setting Header Information



        Catch ex As Exception
            lblError.Text = ex.Message

            Session("INCENTIVEPLAN") = Session("INCENTIVEPLAN_P")
            Session("BC_MIDT_BRK") = Session("BC_MIDT_BRK_P")
            Session("BIDTLAST3MONTH") = Session("BIDTLAST3MONTH_P")
            Session("TempSlabsXmlPLB") = Session("TempSlabsXmlPLB_P")

        End Try

    End Sub


    Function validateDataold(ByVal objInputXml As XmlDocument) As Boolean
        Dim blnStatus As Boolean = True
        Dim grdRow As GridViewRow
        Dim blnCr As Boolean = False



    

        If chkSelectedCriteria.Items.Count = 0 Then
            lblError.Text = "Please select Airline Data!"
            lblInnerError.Text = "Please select Airline Data!"
            blnStatus = False
            Exit Function
        End If


        For crCount As Int16 = 0 To chkSelectedCriteria.Items.Count - 1
            If chkSelectedCriteria.Items(crCount).Selected = True Then
                blnCr = True
                Exit For
            Else
                blnCr = False
            End If
        Next


        If blnCr = False Then
            lblError.Text = "Please select Airline Data!"
            lblInnerError.Text = "Please select Airline Data!"
            blnStatus = False
            Exit Function
        End If

        If txtCaseName.Text.Trim = "" Then
            lblError.Text = "Case name is mandatory"
            lblInnerError.Text = "Case name is mandatory"
            txtCaseName.Focus()
            blnStatus = False
            Exit Function
        End If

        If rdbIncentiveType.SelectedValue = 1 Then
            If String.IsNullOrEmpty(txtUpfrontAmount.Text) Then
                lblError.Text = "Upfront amount is mandatory"
                lblInnerError.Text = "Upfront amount is mandatory"
                txtUpfrontAmount.Focus()
                blnStatus = False
                Exit Function
            End If
        End If

        If grdvSlabN.Rows.Count = 0 Then
            lblError.Text = "Slab is Mandatory"
            lblInnerError.Text = "Slab is Mandatory"
            blnStatus = False
            Exit Function
        End If
        If chkPlb.Checked Then
            If rdbPlbTypeName.SelectedValue.ToString() = "2" Then
                If grdvPlbSlabN.Rows.Count = 0 Then
                    lblError.Text = "PLB Slab is Mandatory"
                    lblInnerError.Text = "PLB Slab is Mandatory"
                    blnStatus = False
                End If
            End If
        End If


        For RowC As Integer = 0 To grdvIncentivePlan.Rows.Count - 1
            grdRow = grdvIncentivePlan.Rows(RowC)
            Dim txtPlanNameGrd As TextBox = CType(grdRow.FindControl("txtPlanName"), TextBox)
            If String.IsNullOrEmpty(txtPlanNameGrd.Text) Then
                lblError.Text = "Case name is Mandatory"
                lblInnerError.Text = "Case name is Mandatory"
                txtPlanNameGrd.Focus()
                blnStatus = False

            End If

        Next


        Return blnStatus
    End Function

    Function validateData(ByVal objInputXml As XmlDocument) As Boolean
        Dim blnStatus As Boolean = True
        Dim grdRow As GridViewRow
        Dim blnCr As Boolean = False
        Dim strMinQulalificationCriteria As String


        If rdbIncentiveType.SelectedValue = "2" Then
            strMinQulalificationCriteria = ""
            For i As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                If ChkMinimunCriteriaNew.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMinQulalificationCriteria) Then
                        strMinQulalificationCriteria = ChkMinimunCriteriaNew.Items(i).Value
                    Else
                        strMinQulalificationCriteria = strMinQulalificationCriteria + "," + ChkMinimunCriteriaNew.Items(i).Value
                    End If
                End If
            Next
            If String.IsNullOrEmpty(strMinQulalificationCriteria) Then
                lblError.Text = "Minimum Segment Criteria is mandatory."
                lblInnerError.Text = "Minimum Segment Criteria is mandatory."
                ChkMinimunCriteriaNew.Focus()
                Return False
            ElseIf (strMinQulalificationCriteria.Trim = "25") Then
                lblError.Text = "Minimum Segment Criteria is mandatory(Excluding HL/ROI)."
                lblInnerError.Text = "Minimum Segment Criteria is mandatory(Excluding HL/ROI)."
                ChkMinimunCriteriaNew.Focus()
                Return False
            End If
            If String.IsNullOrEmpty(TxtMinSegCriteriaValueNew.Text.Trim) Then
                lblError.Text = "Minimum Segment is mandatory."
                lblInnerError.Text = "Minimum Segment is mandatory."
                TxtMinSegCriteriaValueNew.Focus()
                Return False
            End If
          
        End If


        If grdvIncentivePlan.Rows.Count <> 0 Then

            If rdbIncentiveType.SelectedValue = 1 Then
                If String.IsNullOrEmpty(txtUpfrontAmount.Text) Then
                    lblError.Text = "Upfront amount is mandatory"
                    lblInnerError.Text = "Upfront amount is mandatory"
                    txtUpfrontAmount.Focus()
                    Return False

                End If
            End If
            Return True
        End If

        If chkSelectedCriteria.Items.Count = 0 Then
            lblError.Text = "Please select Airline Data!"
            lblInnerError.Text = "Please select Airline Data!"
            Return False

        End If


        For crCount As Int16 = 0 To chkSelectedCriteria.Items.Count - 1
            If chkSelectedCriteria.Items(crCount).Selected = True Then
                blnCr = True
                Exit For
            Else
                blnCr = False
            End If
        Next


        If blnCr = False Then
            lblError.Text = "Please select Airline Data!"
            lblInnerError.Text = "Please select Airline Data!"
            Return False

        End If

        If txtCaseName.Text.Trim = "" Then
            lblError.Text = "Case name is mandatory"
            lblInnerError.Text = "Case name is mandatory"
            txtCaseName.Focus()
            Return False

        End If

        If rdbIncentiveType.SelectedValue = 1 Then
            If String.IsNullOrEmpty(txtUpfrontAmount.Text) Then
                lblError.Text = "Upfront amount is mandatory"
                lblInnerError.Text = "Upfront amount is mandatory"
                txtUpfrontAmount.Focus()
                Return False

            End If
        End If

        If grdvSlabN.Rows.Count = 0 Then
            lblError.Text = "Slab is Mandatory"
            lblInnerError.Text = "Slab is Mandatory"
            Return False

        End If
        If chkPlb.Checked Then
            If rdbPlbTypeName.SelectedValue.ToString() = "2" Then
                If grdvPlbSlabN.Rows.Count = 0 Then
                    lblError.Text = "PLB Slab is Mandatory"
                    lblInnerError.Text = "PLB Slab is Mandatory"
                    Return False
                End If
            End If
        End If


        For RowC As Integer = 0 To grdvIncentivePlan.Rows.Count - 1
            grdRow = grdvIncentivePlan.Rows(RowC)
            Dim txtPlanNameGrd As TextBox = CType(grdRow.FindControl("txtPlanName"), TextBox)
            If String.IsNullOrEmpty(txtPlanNameGrd.Text) Then
                lblError.Text = "Case name is Mandatory"
                lblInnerError.Text = "Case name is Mandatory"
                txtPlanNameGrd.Focus()
                Return False

            End If

        Next


        Return blnStatus
    End Function

    Protected Sub btnSendforApproval_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSendforApproval.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objSendApproval As New AAMS.bizIncetive.bzBusinessCase
            objInputXml.LoadXml("<INC_BUSINESSCASE_APPROVALQUEUE_INPUT><BC_ID></BC_ID><EmployeeID></EmployeeID></INC_BUSINESSCASE_APPROVALQUEUE_INPUT>")
            If hdBcID.Value.Trim() <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdBcID.Value
                If Session("Security") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                Else
                    lblError.Text = "Please Login Again"
                    Return
                End If
                objOutputXml = objSendApproval.SendToApprovalQueue(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Your Business Case is sent for Approval"
                    btnSendforApproval.Enabled = False

                    btnSendforApproval.ImageUrl = "~/Images/Btn_SendForApprovel_temp.jpg"
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If



            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            EnableDisableApprovers()
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnReset.Click
        Try
            Dim strQrString As String = Request.Url.OriginalString
            strQrString = strQrString.Replace("MSG=U", "MSG=N")
            Response.Redirect(strQrString)
            ' Response.Redirect(strQrString & "&check=11111111111111111111111111111111111111111111111111111111112222222222222333333333333333344444444445555555555555556666666666666677777777777788888888888999999999999000000000007777777755555555544444444443333333333333339999999999999995555555555500000000000333333333339999999999999222222222222333333333334444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444451111111111111111111")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnApproved_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnApproved.Click
        Try

            If Request.QueryString("BCaseID") IsNot Nothing Then
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
                ' Load Input Xml.
                objInputXml.LoadXml("<UP_UPDATE_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' BC_ID='' REC_DATE='' APPROVAL_STATUS_ID='' PREVIOUS_BC_ID='' PREVIOUS_APPROVAL_STATUS_ID=''/></UP_UPDATE_INC_APPROVAL_QUE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("APPROVAL_STATUS_ID").InnerText = "2" 'ddlIncentiveStatus.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("BC_ID").InnerText = Request.QueryString("BCaseID")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_BC_ID").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_APPROVAL_STATUS_ID").InnerText = ""
                ' Calling update method for update.
                objOutputXml = objApprovalQue.UpdateApproval_Que(objInputXml)
                '   Checking error status. 
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    lblError.Text = "Business Case is approved and has been sent to next level."
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ' GetApproversByBCId()
        End Try

    End Sub

    Protected Sub BtnReject_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnReject.Click
        Try

            If Request.QueryString("BCaseID") IsNot Nothing Then
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
                ' Load Input Xml.
                objInputXml.LoadXml("<UP_UPDATE_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' BC_ID='' REC_DATE='' APPROVAL_STATUS_ID='' PREVIOUS_BC_ID='' PREVIOUS_APPROVAL_STATUS_ID=''/></UP_UPDATE_INC_APPROVAL_QUE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("APPROVAL_STATUS_ID").InnerText = "4" 'ddlIncentiveStatus.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("BC_ID").InnerText = Request.QueryString("BCaseID")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_BC_ID").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_APPROVAL_STATUS_ID").InnerText = ""
                ' Calling update method for update.
                objOutputXml = objApprovalQue.UpdateApproval_Que(objInputXml)
                '   Checking error status. 
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    lblError.Text = "Business Case is rejected and  has been sent to first level for further approval."
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnFinnallyApproved_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnFinnallyApproved.Click
        Try

            If Request.QueryString("BCaseID") IsNot Nothing Then
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
                ' Load Input Xml.
                objInputXml.LoadXml("<UP_UPDATE_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' BC_ID='' REC_DATE='' APPROVAL_STATUS_ID='' PREVIOUS_BC_ID='' PREVIOUS_APPROVAL_STATUS_ID=''/></UP_UPDATE_INC_APPROVAL_QUE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("APPROVAL_STATUS_ID").InnerText = "3" 'ddlIncentiveStatus.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("BC_ID").InnerText = Request.QueryString("BCaseID")
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_BC_ID").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_APPROVAL_STATUS_ID").InnerText = ""
                ' Calling update method for update.
                objOutputXml = objApprovalQue.UpdateApproval_Que(objInputXml)
                '   Checking error status. 
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    lblError.Text = "Business Case is finally approved."

                    btnSendforApproval.ImageUrl = "~/Images/Btn_SendForApprovel_temp.jpg"
                    BtnApproved.ImageUrl = "~/Images/Btn_Approved_temp.jpg"
                    BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved_temp.jpg"
                    BtnReject.ImageUrl = "~/Images/btn_Reject_temp.jpg"
                    btnSave.ImageUrl = "~/Images/btn_save_temp.jpg"
                    btnRefresh.ImageUrl = "~/Images/btn_Refresh_temp.jpg"

                    btnSendforApproval.Enabled = False
                    BtnApproved.Enabled = False
                    BtnFinnallyApproved.Enabled = False
                    BtnReject.Enabled = False
                    btnSave.Enabled = False
                    btnRefresh.Enabled = False


                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub lnkPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objXmlOut As New XmlDocument
        Dim objNode As XmlNode
        Dim objXmlAttName As XmlAttribute
        Dim strText, strValue As String
        Dim grdRow As GridViewRow

        '   objXmlOut.LoadXml("<Root><Details Start='' End=''/>  </Root>")
        objXmlOut.LoadXml("<Root><Details Start='' End='' Slab=''/>  </Root>")



        Dim objNodeNew, objNodeClone As XmlNode
        objNodeNew = objXmlOut.DocumentElement.SelectSingleNode("Details")
        objNodeClone = objNodeNew.CloneNode(True)
        objXmlOut.DocumentElement.RemoveChild(objNodeNew)

        For rowN As Integer = 0 To grdvSlabN.Rows.Count - 1
            objNodeClone.Attributes("Start").Value = grdvSlabN.Rows(rowN).Cells(0).Text.Trim()
            objNodeClone.Attributes("End").Value = grdvSlabN.Rows(rowN).Cells(1).Text.Trim()
            objNodeClone.Attributes("Slab").Value = grdvSlabN.Rows(rowN).Cells(0).Text.Trim() & " " & "-" & " " & grdvSlabN.Rows(rowN).Cells(1).Text.Trim()
            objXmlOut.DocumentElement.AppendChild(objNodeClone)
            objNodeClone = objNodeNew.CloneNode(True)
        Next




        For rowN As Integer = 0 To grdvSlabN.Rows.Count - 1
            Dim strStart As String = grdvSlabN.Rows(rowN).Cells(0).Text.Trim()
            Dim strEnd As String = grdvSlabN.Rows(rowN).Cells(1).Text.Trim()
            Dim strVal As String = CType(grdvSlabN.Rows(rowN).FindControl("txtAmount"), TextBox).Text.Trim()



            'Adding Node to Detail of Template
            For crCount As Int16 = 0 To chkSelectedCriteria.Items.Count - 1
                If chkSelectedCriteria.Items(crCount).Selected = True Then
                    objNode = objXmlOut.DocumentElement.SelectSingleNode("Details[@Start='" + strStart + "' and @End='" + strEnd + "']")
                    strText = chkSelectedCriteria.Items(crCount).Text
                    strValue = chkSelectedCriteria.Items(crCount).Value

                    If objNode.Attributes("a" & strValue) Is Nothing Then
                        objXmlAttName = objXmlOut.CreateAttribute("a" & strValue)
                        objXmlAttName.Value = strVal
                        objNode.Attributes.Append(objXmlAttName)
                    End If
                End If
            Next
            'Adding Node to Detail of Template




        Next







        For RowC As Integer = 0 To grdvIncentivePlan.Rows.Count - 1
            grdRow = grdvIncentivePlan.Rows(RowC)
            Dim chkAirlinesData As CheckBoxList = CType(grdRow.FindControl("chklstCriteriaNew"), CheckBoxList)
            Dim GvIncSlabsNested As GridView = CType(grdRow.FindControl("GvIncSlabsNested"), GridView)



            '--Code Inserted Here
            For rowN As Integer = 0 To GvIncSlabsNested.Rows.Count - 1
                Dim strStart As String = GvIncSlabsNested.Rows(rowN).Cells(0).Text.Trim()
                Dim strEnd As String = GvIncSlabsNested.Rows(rowN).Cells(1).Text.Trim()
                Dim strVal As String = CType(GvIncSlabsNested.Rows(rowN).FindControl("txtAmount"), TextBox).Text.Trim()



                'Adding Node to Detail of Template


                If chkAirlinesData IsNot Nothing Then
                    For crCount As Int16 = 0 To chkAirlinesData.Items.Count - 1
                        If chkAirlinesData.Items(crCount).Selected = True Then
                            objNode = objXmlOut.DocumentElement.SelectSingleNode("Details[@Start='" + strStart + "' and @End='" + strEnd + "']")
                            strText = chkAirlinesData.Items(crCount).Text
                            strValue = chkAirlinesData.Items(crCount).Value

                            If objNode.Attributes("a" & strValue) Is Nothing Then
                                objXmlAttName = objXmlOut.CreateAttribute("a" & strValue)
                                objXmlAttName.Value = strVal
                                objNode.Attributes.Append(objXmlAttName)
                            Else
                                objNode.Attributes("a" & strValue).Value = strVal
                            End If
                        End If
                    Next
                End If
                'Adding Node to Detail of Template
            Next
            '--End of code Insertion
        Next
        ''Added by Tapan for Preview Change


        Dim objnewXmlOut As New XmlDocument
        objnewXmlOut.LoadXml(objXmlOut.OuterXml)


        Dim objXmlPreview As New XmlDocument
        Dim strPreview As String = "<ROOT><PREVIEW INC_TYPE_ID='' UPFRONTTYPEID='' PAYMENTTYPEID='' UPFRONT_AMOUNT='' SIGNUPAMOUNT='' ADJUSTABLE='' NO_OF_PAYMENTS='' FOR_PERIOD_OF=''/> </ROOT>"
        objXmlPreview.LoadXml(strPreview)
        With objXmlPreview.DocumentElement.SelectSingleNode("PREVIEW")
            .Attributes("INC_TYPE_ID").InnerText = rdbIncentiveType.SelectedValue.Trim()
            .Attributes("UPFRONTTYPEID").InnerText = rdbUpfrontTypeName.SelectedValue.Trim()
            .Attributes("PAYMENTTYPEID").InnerText = rdbPaymentType.SelectedValue.Trim()

            .Attributes("UPFRONT_AMOUNT").InnerText = txtUpfrontAmount.Text
            .Attributes("SIGNUPAMOUNT").InnerText = txtSignUpAmount.Text
            .Attributes("ADJUSTABLE").InnerText = chkAdjustable.Checked
            .Attributes("NO_OF_PAYMENTS").InnerText = txtNoOfPayments.Text
            .Attributes("FOR_PERIOD_OF").InnerText = txtForThePeriodOf.Text
        End With

        objnewXmlOut.DocumentElement.AppendChild(objnewXmlOut.ImportNode(objXmlPreview.DocumentElement.SelectSingleNode("PREVIEW"), True))


        Session("AIRLINE_DATA_PREVIEW") = objnewXmlOut.OuterXml '.objXmlOut.OuterXml
        EnableDisableCase()
        ' ModalPopupExtender3.Show()

        'objXmlOut.Save("C:\WriteX\airlinePreview.xml")
        ' Response.Redirect("INC_POP_Airline_Preview.aspx")

        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "dsfjhg", "window.settimeout('showPreview();',500);return false;")
        'ClientScript.RegisterStartupScript(Me.GetType(), "Open2gfdg", "<script language='javascript'>window.open('INC_POP_Airline_Preview.aspx',blank,'toolbar=0,status=1,resizable=yes');</script>")
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "MyPopUpScript", "<script language='javascript'>window.open('INC_POP_Airline_Preview.aspx','CustomPopUp','toolbar=0,status=1,resizable=yes');</script>", False)
        '  ClientScript.RegisterClientScriptBlock(Me.GetType(), "Open2gfdg", "<script language='javascript'>window.open('INC_POP_Airline_Preview.aspx',null,'toolbar=0,status=1,resizable=yes');</script>")
        'ClientScript.RegisterClientScriptBlock(Me.GetType, "str", "<script>window.open('INC_POP_Airline_Preview.aspx?Case=InvStockTransferReport','stockTransferRpt','height=600,width=820,top=30,left=20,scrollbars=1');  </script>")

    End Sub

    Sub BindLocation()
        Dim objBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim objXmlReader As XmlReader
        Dim ds As New DataSet
        Dim objTempInputXml As New XmlDocument
        Dim objNodeList As XmlNodeList
        objInputXml.LoadXml("<INC_GET_BC_AGENCY_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><EMPLOYEEID></EMPLOYEEID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></INC_GET_BC_AGENCY_INPUT>")


        '<INC_GET_BC_AGENCY_OUTPUT>
        ' <BC_AGENCY LCODE='' NAME='' OFFICEID='' ADDRESS='' CITY='' COUNTRY='' STATUS='' COLOR=''/>' <PAGE PAGE_COUNT='' TOTAL_ROWS='' />
        ' <Errors Status=''>
        ' <Error Code='' Description='' /> 
        ' </Errors>
        '</INC_GET_BC_AGENCY_OUTPUT>

        Try

            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = hdChainCode.Value
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = (Request.QueryString("BCaseID") & "")
            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If


            'Start CODE for sorting and paging


            If ViewState("PrevSearchingLoc") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlLocPageNumber.Items.Count = 0, "1", ddlLocPageNumber.SelectedValue)
            Else


                objTempInputXml.LoadXml(ViewState("PrevSearchingLoc"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlLocPageNumber.Items.Count = 0, "1", ddlLocPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlLocPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortNameLoc") Is Nothing Then
                ViewState("SortNameLoc") = "LCODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortNameLoc")
            End If

            If ViewState("DescLoc") Is Nothing Then
                ViewState("DescLoc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("DescLoc")
            End If

            'End Code for paging and sorting
            objOutPutxml = objBusinessCase.GetBCAgency(objInputXml)
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearchingLoc") = objInputXml.OuterXml


                objXmlReader = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("BC_AGENCY").Rows.Count <> 0 Then
                    gvAgencyLoc.DataSource = ds.Tables("BC_AGENCY")
                    gvAgencyLoc.DataBind()
                    '@ Code Added For Paging And Sorting 
                    pnlPagingLoc.Visible = True

                    BindControlsForNavigation(objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                    txtLocTotalRecordCount.Text = objOutPutxml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvAgencyLoc)

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    gvAgencyLoc.DataSource = Nothing
                    gvAgencyLoc.DataBind()
                    txtLocTotalRecordCount.Text = "0"
                    pnlPagingLoc.Visible = False
                End If

            Else
                txtLocTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPagingLoc.Visible = False
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objBusinessCase = Nothing
            ds = Nothing

        End Try



    End Sub
    Private Sub BindControlsForNavigation(ByVal CurrentPageNo As String)

        pnlPagingLoc.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CurrentPageNo)
        Dim selectedValue As String = IIf(ddlLocPageNumber.SelectedValue = "", "1", ddlLocPageNumber.SelectedValue)
        If count <> ddlLocPageNumber.Items.Count Then
            ddlLocPageNumber.Items.Clear()
            For i As Integer = 1 To count
                ddlLocPageNumber.Items.Add(i.ToString)
            Next
        End If
        ddlLocPageNumber.SelectedValue = selectedValue
        'Code for hiding prev and next button based on count
        If count = 1 Then
            lnkLocNext.Visible = False
            lnkLocPrev.Visible = False
        Else

            lnkLocPrev.Visible = True
            lnkLocNext.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlLocPageNumber.SelectedValue = count.ToString Then
            lnkLocNext.Visible = False
        Else
            lnkLocNext.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlLocPageNumber.SelectedValue = "1" Then
            lnkLocPrev.Visible = False
        Else
            lnkLocPrev.Visible = True
        End If


    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortNameLoc").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("DescLoc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("DescLoc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

    Protected Sub lnkLocNext_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlLocPageNumber.SelectedValue <> (ddlLocPageNumber.Items.Count).ToString Then
                ddlLocPageNumber.SelectedValue = (CInt(ddlLocPageNumber.SelectedValue) + 1).ToString
            End If
            BindLocation()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkLocPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If ddlLocPageNumber.SelectedValue <> "1" Then
                ddlLocPageNumber.SelectedValue = (CInt(ddlLocPageNumber.SelectedValue) - 1).ToString
            End If
            BindLocation()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlLocPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            BindLocation()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyLoc_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyLoc.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If


            Dim strColor As String = String.Empty
            strColor = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "COLOR")).Trim
            If strColor <> "" Then
                e.Row.Attributes.Add("style", "background-color: " + strColor)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyLoc_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAgencyLoc.Sorted

    End Sub

    Protected Sub gvAgencyLoc_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyLoc.Sorting
        Try
            Dim SortNameLoc As String = e.SortExpression
            If ViewState("SortNameLoc") Is Nothing Then
                ViewState("SortNameLoc") = SortNameLoc
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortNameLoc") = SortNameLoc Then
                    If ViewState("DescLoc") = "TRUE" Then
                        ViewState("DescLoc") = "FALSE"
                    Else
                        ViewState("DescLoc") = "TRUE"
                    End If
                Else
                    ViewState("SortNameLoc") = SortNameLoc
                    ViewState("DescLoc") = "FALSE"
                End If
            End If
            BindLocation()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRefresh.Click
        Try

            Session("slabBackup") = Nothing
            Session("INCENTIVEPLAN") = Nothing
            Session("conData") = Nothing
            Session("HardwareDataList") = Nothing
            Session("TempSlabsXml") = Nothing
            Session("TempSlabsXml") = Nothing

            ChkMinimunCriteriaNew.Items.Clear()
            chkLstMSMore.Items.Clear()
            chkSLABQUALIFICATION.Items.Clear()
            chkLstSQMore.Items.Clear()


            hdRefreshAction.Value = "TRUE"
            LoadData(hdChainCode.Value, Request.QueryString("BCaseID") & "")
            FillXml()

            bindSQForShowModal()
            bindMSForShowModal()
           

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Function PrevCalculateBreakUp(ByVal objXml As XmlDocument) As XmlDocument
        Try

            For Each objXmlNode As XmlNode In objXml.DocumentElement.SelectNodes("BC_MIDT_BRK")
                If txtConversionPer.Text <> "" Then
                    objXmlNode.Attributes("MIDT_CONV_PER").InnerText = ((Val(objXmlNode.Attributes("TOTAL").InnerText) / 100) * Val(txtConversionPer.Text)).ToString("00")
                    objXmlNode.Attributes("LESS").InnerText = (Val(objXmlNode.Attributes("MIDT_CONV_PER").InnerText) * 0.9).ToString("00")
                End If
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Return objXml
    End Function



    '#################################################################
    '@ Start  of Code Added By Abhishek on 27 jan 2010
    '#################################################################

    Function CalculateBreakUp(ByVal objXml As XmlDocument) As XmlDocument

        Dim conversionPercentaeByuser As Decimal = 0
        Dim DblTotalMIDTAfterConversionPer As Decimal = 0
        Dim DblTotalMIDT As Double = 0

        Try
            If txtConversionPer.Text.Trim().Length = 0 Then
                conversionPercentaeByuser = 100
            ElseIf Val(txtConversionPer.Text.Trim()) = 0 Then
                conversionPercentaeByuser = 0
            Else
                conversionPercentaeByuser = Val(txtConversionPer.Text)
            End If

            DblTotalMIDT = Val(hdTotalMIDT.Value.ToString())

            DblTotalMIDTAfterConversionPer = DblTotalMIDT * (Val(conversionPercentaeByuser) / 100)


            For Each objXmlNode As XmlNode In objXml.DocumentElement.SelectNodes("BC_MIDT_BRK")
                objXmlNode.Attributes("MIDT_CONV_PER").InnerText = Math.Round((Val(objXmlNode.Attributes("TOTAL").InnerText) * (Val(conversionPercentaeByuser) / 100)), 0, MidpointRounding.AwayFromZero)
                objXmlNode.Attributes("LESS").InnerText = Math.Round(((Val(objXmlNode.Attributes("MIDT_CONV_PER").InnerText) * 0.9)), 0, MidpointRounding.AwayFromZero)

                'If DblTotalMIDTAfterConversionPer > 0 Then
                '    objXmlNode.Attributes("PER").InnerText = Math.Round((Val(objXmlNode.Attributes("MIDT_CONV_PER").InnerText) * 100) / DblTotalMIDTAfterConversionPer, 0)
                'Else
                '    objXmlNode.Attributes("PER").InnerText = 0
                'End If

            Next


            '@Reset  value of HdICValue and hdITvalue
            Dim objNode As XmlNode = objXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK[@TYPE='IC']")
            If objNode IsNot Nothing Then
                hdICvalue.Value = objNode.Attributes("LESS").Value.Trim()
            Else
                hdICvalue.Value = "0"
            End If
            objNode = Nothing

            objNode = objXml.DocumentElement.SelectSingleNode("BC_MIDT_BRK[@TYPE='IT']")
            If objNode IsNot Nothing Then
                hdITvalue.Value = objNode.Attributes("LESS").Value.Trim()
            Else
                hdITvalue.Value = "0"
            End If
            '@Reset  value of HdICValue and hdITvalue

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Return objXml
    End Function
    Sub BindMinimumSegmentCriteria(ByVal ObjCheckBoxList As CheckBoxList)
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
        Dim dsCriteria As DataSet
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Try
            objOutputXml = objbzBusinessCase.List_SegmentFields

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)
                ObjCheckBoxList.DataSource = dsCriteria.Tables("NIDTFIELDS")
                ObjCheckBoxList.DataTextField = "NIDT_FIELDS_NAME"
                ObjCheckBoxList.DataValueField = "NIDT_FIELDS_ID"
                ObjCheckBoxList.DataBind()
            Else
                ObjCheckBoxList.Items.Clear()

                ObjCheckBoxList.DataSource = Nothing
                ObjCheckBoxList.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        HdConversionPer.Value = txtConversionPer.Text
        PnlConversionMsg.CssClass = "displayNone"
        'UpdtConversionPer.Update()
        If Session("Security") Is Nothing Then
            lblInnerError.Text = "Session is expired."
            lblError.Text = "Session is expired."
            Exit Sub
        End If
     
    End Sub


    Protected Sub txtConversionPer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtConversionPer.TextChanged
        HdConversionPer.Value = txtConversionPer.Text
        PnlConversionMsg.CssClass = "displayNone"
        ' UpdtConversionPer.Update()
        If Session("Security") Is Nothing Then
            lblInnerError.Text = "Session is expired."
            lblError.Text = "Session is expired."
            Exit Sub
        End If
    End Sub

    Private Sub CalculateFormula(ByVal convPercentage As Double)

        Try

     
            '@ New Fomula For Conversion Percentage ( This is Total of !0% Less of IINTL and DOM

            '  convPercentage=
            Dim TotalTenPerLessOFINTLAndDOM As Double = 0
            If grdvBreakUpLast.Rows.Count > 0 Then
                For Each gvRow As GridViewRow In grdvBreakUpLast.Rows
                    Dim lbltenPerLess As Label = CType(gvRow.FindControl("lbltenPerLess"), Label)
                    Dim hdType As HiddenField = CType(gvRow.FindControl("hdType"), HiddenField)
                    If hdType.Value.Trim = "DOM" Then
                        TotalTenPerLessOFINTLAndDOM = TotalTenPerLessOFINTLAndDOM + Val(lbltenPerLess.Text)
                    ElseIf hdType.Value.Trim = "INTL" Then
                        TotalTenPerLessOFINTLAndDOM = TotalTenPerLessOFINTLAndDOM + Val(lbltenPerLess.Text)
                    End If
                Next
            End If
            convPercentage = TotalTenPerLessOFINTLAndDOM


            '
            ''Incentive

            If txtIncRateN.Text.Trim.Length <> 0 And txtIncExpectedSegN.Text.Length <> 0 Then
                'lblIncTotalCostN.Text = (Val(txtIncRateN.Text) * Val(txtIncExpectedSegN.Text)).ToString("00")
                lblIncTotalCostN.Text = Math.Round((Val(txtIncRateN.Text) * Val(txtIncExpectedSegN.Text)), 0, MidpointRounding.AwayFromZero)
            Else
                'lblIncTotalCostN.Text = "0"
                lblIncTotalCostN.Text = "0"
            End If

            ' lblTSegTotalCostN.Text = (totalConn + totalHardware + Val(lblIncTotalCostN.Text)).ToString("00")
            lblTSegTotalCostN.Text = Math.Round((totalConn + totalHardware + Val(lblIncTotalCostN.Text)), 0, MidpointRounding.AwayFromZero)

            lblTotalSeg.Text = txtIncExpectedSegN.Text

            If Val(lblTotalSeg.Text.Trim()) > 0 Then
                'lblIncCpsTotalN.Text = (Val(lblTSegTotalCostN.Text.Trim()) / Val(lblTotalSeg.Text.Trim())).ToString("00")
                lblIncCpsTotalN.Text = Math.Round((Val(lblTSegTotalCostN.Text.Trim()) / Val(lblTotalSeg.Text.Trim())), 0, MidpointRounding.AwayFromZero)
            Else
                lblIncCpsTotalN.Text = "0"
            End If
            'End





            'Fix Incentive
            If txtFixIncentive.Text.Trim().Length <> 0 And txtFixIncMonth.Text.Trim().Length <> 0 Then
                'lblFixIncPerMonth.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim()))).ToString("00")
                lblFixIncPerMonth.Text = Math.Round(((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim()))), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncPerMonth.Text = "0"
            End If








            'lblFixIncTotalCost.Text = (totalConn + totalHardware + Val(lblFixIncPerMonth.Text)).ToString("00")
            lblFixIncTotalCost.Text = Math.Round((totalConn + totalHardware + Val(lblFixIncPerMonth.Text)), 0, MidpointRounding.AwayFromZero)



            If Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                'lblFixIncNet.Text = ((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))).ToString("00")
                lblFixIncNet.Text = Math.Round(((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncNet.Text = "0"
            End If


            If Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                'lblFixIncCPS.Text = ((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))).ToString("00")
                lblFixIncCPS.Text = Math.Round(((Val(lblFixIncTotalCost.Text.Trim()) / Val(txtFixIncMinMonthSeg.Text.Trim()))), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncCPS.Text = "0"
            End If




            If lblFixIncTotalCost.Text.Trim().Length <> 0 And hdTotalMIDT.Value.Trim().Length <> 0 And convPercentage <> 0 Then

                'lblFixIncGross.Text = (Val(lblFixIncTotalCost.Text) / convPercentage).ToString("00")
                lblFixIncGross.Text = Math.Round((Val(lblFixIncTotalCost.Text) / convPercentage), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncGross.Text = "0"
            End If

            Dim valTotalSegAsPerConver As Double = 0
            If (Val(txtConversionPer.Text.Trim()) > 0) And (Val(hdTotalMIDT.Value.Trim()) > 0) Then
                valTotalSegAsPerConver = Val(hdTotalMIDT.Value.Trim()) / (Val(txtConversionPer.Text.Trim()) / 100)
            Else
                valTotalSegAsPerConver = 0
            End If
            ' Dim valTotalSergAsPerConverValue As Double = Val(hdTotalMIDT.Value.Trim()) * 0.9


            'lblSegLessICITValNew.Text = (convPercentage - (Val(hdICvalue.Value.Trim()) + Val(hdITvalue.Value.Trim()))).ToString("00")
            lblSegLessICITValNew.Text = Math.Round((convPercentage - (Val(hdICvalue.Value.Trim()) + Val(hdITvalue.Value.Trim()))), 0, MidpointRounding.AwayFromZero)

            ' lblSegLessICValNew.Text = (convPercentage - (Val(hdICvalue.Value.Trim()))).ToString("00")

            lblSegLessICValNew.Text = Math.Round((convPercentage - (Val(hdICvalue.Value.Trim()))), 0, MidpointRounding.AwayFromZero)


            If Val(hdTotalMIDT.Value.Trim()) > 0 And convPercentage <> 0 Then
                'lblSegLessICITPerNew.Text = ((Val(lblSegLessICITValNew.Text) * 100) / Val(convPercentage)).ToString("00")
                lblSegLessICITPerNew.Text = Math.Round(((Val(lblSegLessICITValNew.Text) * 100) / Val(convPercentage)), 0, MidpointRounding.AwayFromZero)
            Else
                lblSegLessICITPerNew.Text = "0"
            End If



            If Val(hdTotalMIDT.Value.Trim()) > 0 And convPercentage <> 0 Then
                'lblSegLessICPerNew.Text = ((Val(lblSegLessICValNew.Text) * 100) / Val(convPercentage)).ToString("00")
                lblSegLessICPerNew.Text = Math.Round(((Val(lblSegLessICValNew.Text) * 100) / Val(convPercentage)), 0, MidpointRounding.AwayFromZero)
            Else
                lblSegLessICPerNew.Text = "0"
            End If



            If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICITValNew.Text) > 0 Then
                '   lblFixIncICIT.Text = ((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICITValNew.Text))).ToString("00")
                lblFixIncICIT.Text = Math.Round(((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICITValNew.Text))), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncICIT.Text = "0"
            End If


            If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICValNew.Text) > 0 Then
                'lblFixIncIC.Text = ((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICValNew.Text))).ToString("00")
                lblFixIncIC.Text = Math.Round(((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICValNew.Text))), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncIC.Text = "0"
            End If



            'End of fix Incentive


            'For Footer Section
            'Calculation for   lblTotalCostNew
            'If grdvBreakUpLast.FooterRow IsNot Nothing Then
            '    'lblTotalCostNew.Text = (Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)).ToString("00")
            '    lblTotalCostNew.Text = Math.Round((Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)), 0, MidpointRounding.AwayFromZero)

            'End If

            Dim TotalCostNew As Double = 0
            If grdvBreakUpLast.FooterRow IsNot Nothing Then
                TotalCostNew = Val(Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim))
            End If
            If grdConnectivityN.FooterRow IsNot Nothing Then
                TotalCostNew = TotalCostNew + Val(Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim))
            End If
            If grdvHardwareN.FooterRow IsNot Nothing Then
                TotalCostNew = TotalCostNew + Val(Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim))
            End If

            If grdvBreakUpLast.FooterRow IsNot Nothing Then
                'lblTotalCostNew.Text = (Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)).ToString("00")
                ' lblTotalCostNew.Text = Math.Round((Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)), 0, MidpointRounding.AwayFromZero)
                lblTotalCostNew.Text = Math.Round(TotalCostNew, 0, MidpointRounding.AwayFromZero)
            End If



            If Val(convPercentage) > 0 Then

                'lblGrossNew.Text = (Val(lblTotalCostNew.Text.Trim()) / convPercentage).ToString("00")
                lblGrossNew.Text = Math.Round((Val(lblTotalCostNew.Text.Trim()) / convPercentage), 0, MidpointRounding.AwayFromZero)
            Else
                lblGrossNew.Text = "0"
            End If


            If Val(lblSegLessICITValNew.Text) > 0 Then
                'lblCPSMultiRateICITNew.Text = (Val(lblTotalCostNew.Text) / Val(lblSegLessICITValNew.Text)).ToString("00")
                lblCPSMultiRateICITNew.Text = Math.Round((Val(lblTotalCostNew.Text) / Val(lblSegLessICITValNew.Text)), 0, MidpointRounding.AwayFromZero)
            Else
                lblCPSMultiRateICITNew.Text = "0"
            End If


            'If Val(lblSegLessICValNew.Text) > 0 Then
            '    lblCPSMultiRateICITNew.Text = (Val(lblTotalCostNew.Text) / Val(lblSegLessICITValNew.Text)).ToString("00")
            'Else
            '    lblCPSMultiRateICITNew.Text = "0"
            'End If



            If Val(lblSegLessICValNew.Text) > 0 Then
                'lblCPSMultiRateICNew.Text = (Val(lblTotalCostNew.Text) / Val(lblSegLessICValNew.Text)).ToString("00")
                lblCPSMultiRateICNew.Text = Math.Round((Val(lblTotalCostNew.Text) / Val(lblSegLessICValNew.Text)), 0, MidpointRounding.AwayFromZero)
            Else
                lblCPSMultiRateICNew.Text = "0"
            End If



            If Val(txtMinMonthSegNew.Text.Trim()) > 0 Then
                ' lblNetNew.Text = (Val(lblTotalCostNew.Text.Trim()) / Val(txtMinMonthSegNew.Text.Trim())).ToString("00")
                lblNetNew.Text = Math.Round((Val(lblTotalCostNew.Text.Trim()) / Val(txtMinMonthSegNew.Text.Trim())), 0, MidpointRounding.AwayFromZero)
            Else
                lblNetNew.Text = "0"
            End If
            ' lblTotalCostNew.Text = (totalHardware + totalConn).ToString("00")

            'If grdvBreakUpLast.FooterRow IsNot Nothing Then
            '    ' lblTotalCostNew.Text = (Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)).ToString("00")
            '    lblTotalCostNew.Text = Math.Round((Val(grdvBreakUpLast.FooterRow.Cells(5).Text.Trim) + Val(grdvHardwareN.FooterRow.Cells(4).Text.Trim) + Val(grdConnectivityN.FooterRow.Cells(4).Text.Trim)), 0, MidpointRounding.AwayFromZero)

            'End If



            'txtFixIncMonth
            If (Val(txtFixIncentive.Text.Trim()) > 0) And (convPercentage > 0) Then
                'lblIncRateGrossVal.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(convPercentage)).ToString("00")
                lblIncRateGrossVal.Text = Math.Round(((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(convPercentage)), 0, MidpointRounding.AwayFromZero)
            Else
                lblIncRateGrossVal.Text = "0"
            End If







            If (Val(txtFixIncentive.Text.Trim()) > 0) And (Val(txtFixIncMonth.Text.Trim()) > 0) And (Val(lblSegLessICValNew.Text.Trim()) <> 0) Then

                ' If Val(lblFixIncPerMonth.Text.Trim()) > 0 And Val(lblSegLessICValNew.Text.Trim()) > 0 Then
                'lblIncRateWOIC.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICValNew.Text.Trim())).ToString("00")
                lblIncRateWOIC.Text = Math.Round(((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICValNew.Text.Trim())), 0, MidpointRounding.AwayFromZero)
            Else
                lblIncRateWOIC.Text = "0"
                'End If

            End If



            If (Val(txtFixIncentive.Text.Trim()) > 0) And (Val(txtFixIncMonth.Text.Trim()) > 0) And (Val(lblSegLessICITValNew.Text.Trim()) <> 0) Then
                ' If Val(lblFixIncPerMonth.Text.Trim()) > 0 And Val(lblSegLessICITValNew.Text.Trim()) > 0 Then
                'lblIncRateWOICIT.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICITValNew.Text.Trim())).ToString("00")
                lblIncRateWOICIT.Text = Math.Round(((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(lblSegLessICITValNew.Text.Trim())), 0, MidpointRounding.AwayFromZero)
            Else
                lblIncRateWOICIT.Text = "0"
                'End If
            End If



            If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICValNew.Text) > 0 Then
                ' lblFixIncIC.Text = (Val(lblFixIncTotalCost.Text) / Val(lblSegLessICValNew.Text)).ToString("00")
                lblFixIncIC.Text = Math.Round((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICValNew.Text)), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncIC.Text = "0"
            End If

            If Val(lblFixIncTotalCost.Text) > 0 And Val(lblSegLessICITValNew.Text) > 0 Then
                '   lblFixIncICIT.Text = (Val(lblFixIncTotalCost.Text) / Val(lblSegLessICITValNew.Text)).ToString("00")
                lblFixIncICIT.Text = Math.Round((Val(lblFixIncTotalCost.Text) / Val(lblSegLessICITValNew.Text)), 0, MidpointRounding.AwayFromZero)
            Else
                lblFixIncICIT.Text = "0"
            End If


            'Incentive Rate(Net)
            If Val(txtFixIncMonth.Text.Trim()) > 0 And Val(txtFixIncMinMonthSeg.Text.Trim()) > 0 Then
                ' lblIncRateNet.Text = ((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(txtFixIncMinMonthSeg.Text.Trim())).ToString("00")
                lblIncRateNet.Text = Math.Round(((Val(txtFixIncentive.Text.Trim()) / Val(txtFixIncMonth.Text.Trim())) / Val(txtFixIncMinMonthSeg.Text.Trim())), 0, MidpointRounding.AwayFromZero)
            Else
                lblIncRateNet.Text = "0"
            End If
            'End of Incentive Rate(Net)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bindSQForShowModal()
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objNode As XmlNode
            Dim strSQ As String = ""
            'Code for Template Criteria Removal
            For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                If chkSLABQUALIFICATION.Items(i).Selected Then
                    If String.IsNullOrEmpty(strSQ) Then
                        strSQ = chkSLABQUALIFICATION.Items(i).Value
                    Else
                        strSQ = strSQ + "," + chkSLABQUALIFICATION.Items(i).Value
                    End If
                End If
            Next
            'End of Code for Template Criteria Removal
            Dim objArrayList As New ArrayList(strSQ.Split(","))
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet
            objOutputXml = objbzBusinessCase.List_NIDTFields()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                '    If objArrayList.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                '        objOutputXml.DocumentElement.RemoveChild(objNode)
                '    End If
                'Next

                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)

                chkLstSQMore.Items.Clear()
                chkLstSQMore.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkLstSQMore.DataTextField = "NIDT_FIELDS_NAME"
                chkLstSQMore.DataValueField = "NIDT_FIELDS_ID"
                chkLstSQMore.DataBind()
                'If chkLstSQMore.Items.Count = 0 Then
                '    LBSQMsg.Visible = True
                '    LBSQMsg.Text = "No Slab Qualificaion  is available for Selection"
                '    btnSelectSQ.Visible = False
                'Else
                '    LBSQMsg.Visible = False
                '    LBSQMsg.Text = ""
                '    btnSelectSQ.Visible = True
                'End If
            End If

            For i As Integer = 0 To chkLstSQMore.Items.Count - 1
                For ii As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                    If chkSLABQUALIFICATION.Items(ii).Value.Trim() = chkLstSQMore.Items(i).Value.Trim() Then
                        chkLstSQMore.Items(i).Selected = True
                    End If
                Next
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSelectSQ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectSQ.Click
        Try
            Dim ds As New DataSet
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objTempSQXml As New XmlDocument
            Dim IsProd As Boolean = False
            Dim strSQ As String = ""
            'Code for Template Criteria Removal

            For i As Integer = 0 To chkLstSQMore.Items.Count - 1
                If chkLstSQMore.Items(i).Selected Then
                    If String.IsNullOrEmpty(strSQ) Then
                        strSQ = chkLstSQMore.Items(i).Value
                    Else
                        strSQ = strSQ + "," + chkLstSQMore.Items(i).Value
                    End If
                End If
            Next

            'For i As Integer = 0 To chkLstSQMore.Items.Count - 1
            '    If chkLstSQMore.Items(i).Selected Then
            '        If chkLstSQMore.Items(i).Text.Trim.ToUpper = "PRODUCTIVITY" Then
            '            IsProd = True
            '            Exit For
            '        End If
            '    End If
            'Next

            'If IsProd = False Then
            '    For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
            '        ' If chkSLABQUALIFICATION.Items(i).Selected Then
            '        If String.IsNullOrEmpty(strSQ) Then
            '            strSQ = chkSLABQUALIFICATION.Items(i).Value
            '        Else
            '            strSQ = strSQ + "," + chkSLABQUALIFICATION.Items(i).Value
            '        End If
            '        '  End If
            '    Next
            'End If

            If strSQ.Trim.Length > 0 Then
                Dim objArrayList As New ArrayList(strSQ.Split(","))
                Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
                objOutputXml = objbzBusinessCase.List_NIDTFields()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objTempSQXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                    For ii As Integer = 0 To objArrayList.Count - 1
                        Dim objN As XmlNode = objOutputXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                        If objN IsNot Nothing Then
                            objTempSQXml.DocumentElement.AppendChild(objTempSQXml.ImportNode(objN, True))
                        End If
                    Next
                    If objTempSQXml.DocumentElement.SelectSingleNode("NIDTFIELDS") IsNot Nothing Then
                        ds = New DataSet()
                        objXmlReader = New XmlNodeReader(objTempSQXml)
                        ds.ReadXml(objXmlReader)

                        chkSLABQUALIFICATION.Items.Clear()

                        chkSLABQUALIFICATION.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                        chkSLABQUALIFICATION.DataValueField = "NIDT_FIELDS_ID"
                        chkSLABQUALIFICATION.DataTextField = "NIDT_FIELDS_NAME"
                        chkSLABQUALIFICATION.DataBind()
                        For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
                            chkSLABQUALIFICATION.Items(i).Selected = True
                        Next
                    Else
                        chkSLABQUALIFICATION.Items.Clear()
                        chkSLABQUALIFICATION.DataSource = Nothing
                        chkSLABQUALIFICATION.DataBind()
                    End If
                End If
            Else
                chkSLABQUALIFICATION.Items.Clear()
                chkSLABQUALIFICATION.DataSource = Nothing
                chkSLABQUALIFICATION.DataBind()
            End If
            bindSQForShowModal()
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub


    Private Sub bindMSForShowModal()
        Try
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader

            Dim strMQ As String = ""
            'Code for Template Criteria Removal
            For i As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                If ChkMinimunCriteriaNew.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMQ) Then
                        strMQ = ChkMinimunCriteriaNew.Items(i).Value
                    Else
                        strMQ = strMQ + "," + ChkMinimunCriteriaNew.Items(i).Value
                    End If
                End If
            Next
            'End of Code for Template Criteria Removal
            Dim objArrayList As New ArrayList(strMQ.Split(","))
            Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim dsCriteria As DataSet
            objOutputXml = objbzBusinessCase.List_SegmentFields()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'For Each objNode In objOutputXml.DocumentElement.SelectNodes("NIDTFIELDS")
                '    If objArrayList.Contains(objNode.Attributes("NIDT_FIELDS_ID").Value.Trim()) Then
                '        objOutputXml.DocumentElement.RemoveChild(objNode)
                '    End If
                'Next

                dsCriteria = New DataSet
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsCriteria.ReadXml(objXmlReader)

                chkLstMSMore.Items.Clear()
                chkLstMSMore.DataSource = dsCriteria.Tables("NIDTFIELDS")
                chkLstMSMore.DataTextField = "NIDT_FIELDS_NAME"
                chkLstMSMore.DataValueField = "NIDT_FIELDS_ID"
                chkLstMSMore.DataBind()
                'If chkLstMSMore.Items.Count = 0 Then
                '    LBMSMsg.Visible = True
                '    LBMSMsg.Text = "No Minimum segment  is available for Selection"
                '    btnSelectMQ.Visible = False
                'Else
                '    LBMSMsg.Visible = False
                '    LBMSMsg.Text = ""
                '    btnSelectMQ.Visible = True
                'End If
            End If

            For i As Integer = 0 To chkLstMSMore.Items.Count - 1
                For ii As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                    If ChkMinimunCriteriaNew.Items(ii).Value.Trim() = chkLstMSMore.Items(i).Value.Trim() Then
                        chkLstMSMore.Items(i).Selected = True
                    End If
                Next
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSelectMS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectMS.Click
        Try
            Dim ds As New DataSet
            Dim objOutputXml, objIncentiveTempXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objTempMSXml As New XmlDocument
            Dim IsProd As Boolean = False
            Dim strMQ As String = ""
            'Code for Template Criteria Removal

            For i As Integer = 0 To chkLstMSMore.Items.Count - 1
                If chkLstMSMore.Items(i).Selected Then
                    If String.IsNullOrEmpty(strMQ) Then
                        strMQ = chkLstMSMore.Items(i).Value
                    Else
                        strMQ = strMQ + "," + chkLstMSMore.Items(i).Value
                    End If
                End If
            Next

            'For i As Integer = 0 To chkLstMSMore.Items.Count - 1
            '    If chkLstMSMore.Items(i).Selected Then
            '        If chkLstMSMore.Items(i).Text.Trim.ToUpper = "PRODUCTIVITY" Then
            '            IsProd = True
            '            Exit For
            '        End If
            '    End If
            'Next

            'If IsProd = False Then
            '    For i As Integer = 0 To chkSLABQUALIFICATION.Items.Count - 1
            '        ' If chkSLABQUALIFICATION.Items(i).Selected Then
            '        If String.IsNullOrEmpty(strSQ) Then
            '            strSQ = chkSLABQUALIFICATION.Items(i).Value
            '        Else
            '            strSQ = strSQ + "," + chkSLABQUALIFICATION.Items(i).Value
            '        End If
            '        '  End If
            '    Next
            'End If

            If strMQ.Trim.Length > 0 Then
                Dim objArrayList As New ArrayList(strMQ.Split(","))
                Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
                objOutputXml = objbzBusinessCase.List_SegmentFields
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objTempMSXml.LoadXml("<INC_LISTNIDTFIELDS_TYPE_OUTPUT></INC_LISTNIDTFIELDS_TYPE_OUTPUT >")
                    For ii As Integer = 0 To objArrayList.Count - 1
                        Dim objN As XmlNode = objOutputXml.DocumentElement.SelectSingleNode("NIDTFIELDS[@NIDT_FIELDS_ID='" + objArrayList(ii).ToString().Trim() + "']")
                        If objN IsNot Nothing Then
                            objTempMSXml.DocumentElement.AppendChild(objTempMSXml.ImportNode(objN, True))
                        End If
                    Next
                    If objTempMSXml.DocumentElement.SelectSingleNode("NIDTFIELDS") IsNot Nothing Then
                        ds = New DataSet()
                        objXmlReader = New XmlNodeReader(objTempMSXml)
                        ds.ReadXml(objXmlReader)

                        ChkMinimunCriteriaNew.Items.Clear()

                        ChkMinimunCriteriaNew.DataSource = ds.Tables("NIDTFIELDS").DefaultView
                        ChkMinimunCriteriaNew.DataValueField = "NIDT_FIELDS_ID"
                        ChkMinimunCriteriaNew.DataTextField = "NIDT_FIELDS_NAME"
                        ChkMinimunCriteriaNew.DataBind()
                        For i As Integer = 0 To ChkMinimunCriteriaNew.Items.Count - 1
                            ChkMinimunCriteriaNew.Items(i).Selected = True
                        Next
                    Else
                        ChkMinimunCriteriaNew.Items.Clear()
                        ChkMinimunCriteriaNew.DataSource = Nothing
                        ChkMinimunCriteriaNew.DataBind()
                    End If
                End If
            Else
                ChkMinimunCriteriaNew.Items.Clear()
                ChkMinimunCriteriaNew.DataSource = Nothing
                ChkMinimunCriteriaNew.DataBind()

            End If
            bindMSForShowModal()
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

    '#################################################################
    '@ End  of Code Added By Abhishek on 27 jan 2010
    '#################################################################

    '#################################################################
    '@ Start of Added Code By Abhishek on 14/04/2011
    '#################################################################

    Private Sub EnableDisableApprovers()
        Try
            If HdIsApproval.Value.ToString().Trim().ToUpper = "TRUE" Then
                BtnApproved.Enabled = True
                BtnApproved.ImageUrl = "~/Images/Btn_Approved.jpg"
            Else
                BtnApproved.Enabled = False
                BtnApproved.ImageUrl = "~/Images/Btn_Approved_temp.jpg"
            End If
            If HdIsFinalApproval.Value.ToString().Trim().ToUpper = "TRUE" Then
                BtnFinnallyApproved.Enabled = True
                BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved.jpg"
            Else
                BtnFinnallyApproved.Enabled = False
                BtnFinnallyApproved.ImageUrl = "~/Images/btn_FinallyApproved_temp.jpg"
            End If
            If HdIsApproval.Value.ToString().Trim().ToUpper = "TRUE" Or HdIsFinalApproval.Value.ToString().Trim().ToUpper = "TRUE" Then
                BtnReject.ImageUrl = "~/Images/btn_Reject.jpg"
                BtnReject.Enabled = True
            Else
                BtnReject.ImageUrl = "~/Images/btn_Reject_temp.jpg"
                BtnReject.Enabled = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub checksecurity()
        Dim objSecurityXml As New XmlDocument
        Dim strBuilder As New StringBuilder
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)

                If hdBcID.Value.Trim.Length > 0 Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                        btnSave.ImageUrl = "~/Images/btn_Save_temp.jpg"
                    End If
                Else
                    If strBuilder(1) = "0" Then
                        btnSave.Enabled = False
                        btnSave.ImageUrl = "~/Images/btn_Save_temp.jpg"
                    End If

                End If

            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
    End Sub

    '#################################################################
    '@ End of Added Code By Abhishek on 14/04/2011
    '#################################################################


End Class