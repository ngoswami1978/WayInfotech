
Partial Class Sales_SASR_VisitDetails
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    ' Dim intToday As Integer
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strDSRDate As String = ""
        Try
            Session("PageName") = Request.Url.ToString()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not IsPostBack Then
                Session("VisitDetails") = Nothing

                objeAAMS.BindDropDown(DlstIataStatus, "IATASTATUS", True, 1)

                If Request.QueryString("VisitDate") IsNot Nothing Then
                    'strDSRDate = objeAAMS.GetDateFormat(Request.QueryString("VisitDate").ToString, "dd/MM/yyyy", "yyyyMMdd", "/")
                    strDSRDate = Request.QueryString("VisitDate").ToString
                    hdVisitDATE.Value = strDSRDate
                End If

                If Request.QueryString("Lcode") IsNot Nothing Then
                    hdLCode.Value = Request.QueryString("Lcode").ToString
                End If
                If Request.QueryString("IsManager") IsNot Nothing Then
                    hdIsManager.Value = Request.QueryString("IsManager").ToString
                End If

                lblHeading.Text = "DSR Details" + Space(1) + strDSRDate.Substring(6, 2) + "-" + MonthName(strDSRDate.Substring(4, 2)) + "-" + strDSRDate.Substring(0, 4)
                If Request.QueryString("DSRVistedId") IsNot Nothing And Request.QueryString("DSRVistedId").ToString.Trim.Length > 0 Then
                    hdID.Value = Request.QueryString("DSRVistedId").ToString
                End If

                'objeAAMS.BindDropDown(DrpAirNonAirAssignedTo, "EMPLOYEE", True, 1)
                'objeAAMS.BindDropDown(DrpTargetAssignedTo, "EMPLOYEE", True, 1)
                'objeAAMS.BindDropDown(DrpRetAssignedTo, "EMPLOYEE", True, 1)

                If Request.QueryString("ResId") IsNot Nothing Then
                    If objeAAMS.EmployeeID(Session("Security")).ToString.Trim = Request.QueryString("ResId").ToString.Trim Then
                        BtnFollowupCallPlan.Attributes.Add("onclick", "return FollowupCallPlan();")
                    Else
                        BtnFollowupCallPlan.Enabled = False
                    End If
                End If


                ViewDetails(False)
                If Val(HdCompetionCount.Value) = 0 Then
                    Dim param As String = "../TravelAgency/TAUP_AgencyCompetition.aspx?Id=2&SalesVisit='V'&Action=U|" + hdLCode.Value + "&Lcode=" + hdLCode.Value
                    iframeID.Attributes("src") = param

                    PnlCalenderImagePnl.CssClass = "displayBlock"
                    CalenderImage.Attributes("class") = "displayBlock"
                    mdlPopUpCalender.Show()

                End If

            End If

            ddlServiceStatus.Attributes.Add("onchange", "return CallAssignedOnOrOff();")
            ddlAirNonAirStatus.Attributes.Add("onchange", "return CallAirNonAirForsignDate();")
            ddlRetentionStatus.Attributes.Add("onchange", "return SignedOnDateByRetStatus();")

            ddlTargetStatus.Attributes.Add("onchange", "return SignedOnDateByTargetStatus();")

            ' .Attributes.Add("onchange", "ValidateIataStatus();")
            DlstIataStatus.Attributes.Add("onchange", "ValidateIataStatus();")

            EnableDisableControls()
            ' Checking security.
            CheckSecurity()



            '@ Start Visit Details
            If IsPostBack Then
                txtPersonMet.Text = Request.Form("txtPersonMet")
                txtDesignation.Text = Request.Form("txtDesignation")
                txtInTime.Text = Request.Form("txtInTime")
                txtOutTime.Text = Request.Form("txtOutTime")
            End If

            '@End Visit Details

            '@Start Service Calls
            txtDepttSpecific.Text = Request.Form("txtDepttSpecific")
            txtDetailedDiscussion.Text = Request.Form("txtDetailedDiscussion")
            txtTargetCloserDate.Text = Request.Form("txtTargetCloserDate")
            txtCloserDate.Text = Request.Form("txtCloserDate")
            '@ End Service Calls

            '@ Start AirNonAir
            txtAirNonAirSignedOn.Text = Request.Form("txtAirNonAirSignedOn")
            txtRevenue.Text = Request.Form("txtRevenue")
            '@ End AirNonAir



            '@ Start Retention
            txtRetentionSignedOn.Text = Request.Form("txtRetentionSignedOn")
            txtCPS.Text = Request.Form("txtCPS")

            '@ End Retention

            '@ Start Target
            txtTargetSignedOn.Text = Request.Form("txtTargetSignedOn")
            '@ End Target

            LblUpdateIataError.Text = ""
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails(ByVal blnUpdate As Boolean)"
    Private Sub ViewDetails(ByVal blnUpdate As Boolean)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSales As New AAMS.bizSales.bzDSR
        Try
            'RowId=1&DSRVistedId=1&UnplannedVisit=FALSE&ResId=&Lcode=2&ChainCode=2&VisitDate=25/05/2011
            If blnUpdate = True Then
                If Session("VisitDetails") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("VisitDetails"))
                End If
            Else
                objInputXml.LoadXml("<SL_VIEW_DSR_VISITDETAILS_INPUT><DSR_VISIT_ID/><PREDATE/><LCODE/><LOGINID/><MANAGER/></SL_VIEW_DSR_VISITDETAILS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DSR_VISIT_ID").InnerText = hdID.Value
                objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = hdVisitDATE.Value
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdLCode.Value
                objInputXml.DocumentElement.SelectSingleNode("LOGINID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("MANAGER").InnerText = hdIsManager.Value
                objInputXml.Save("c:\admin\aams\DSRViewInput.xml")
                objOutputXml = objSales.View(objInputXml)
                objOutputXml.Save("c:\admin\aams\DSRViewOutput.xml")
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (hdIsManager.Value = "1" Or hdIsManager.Value = "True") Then
                    objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("MANAGER").InnerText = "True"
                    lbl_logbyManager.Text = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("EMPLOYEENAME").Value.ToUpper
                Else
                    '  objInputXml.DocumentElement.SelectSingleNode("MANAGER").InnerText = "False"
                    tblTRLoggedByManager.Visible = False
                End If

                Session("VisitDetails") = objOutputXml.OuterXml
                'Code For Existing Deals.
                '  <BCDETAILS BCID="968" CHAIN_CODE="12259" CPS="" /> 
                If Not IsPostBack Then
                    If objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/BCDETAILS").Attributes("BCID").Value.Trim.Length > 0 Then
                        'lnkExistingDeals.Text = "968"
                        lnkExistingDeals.Text = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/BCDETAILS").Attributes("BCID").Value.Trim
                        hdBCID.Value = lnkExistingDeals.Text
                        hdChainCode.Value = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/BCDETAILS").Attributes("CHAIN_CODE").Value)
                        'lnkExistingDeals.Attributes.Add("onClick", "return DetailsFunction(" + objED.Encrypt(lnkExistingDeals.Text) + "," + objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/BCDETAILS").Attributes("CHAIN_CODE").Value.Trim) + ")")
                        lnkExistingDeals.CssClass = "LinkButtonsWithoutUnderline"
                    Else
                        lnkExistingDeals.Text = "N/A"
                        lnkExistingDeals.CssClass = "LinkButtonsWithoutUnderlineAndPointer"
                    End If
                End If

                'If Not IsPostBack Then
                '    lnkExistingDeals.Text = "968"
                '    lnkExistingDeals.Attributes.Add("onClick", "return DetailsFunction(12259,968)")
                'End If

                'end

                If objOutputXml.DocumentElement.SelectSingleNode("Permission") IsNot Nothing Then
                    hdBackDateAllow.Value = objOutputXml.DocumentElement.SelectSingleNode("Permission").Attributes("Allowed").Value.Trim.ToUpper
                End If


                hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSR_VISIT_ID").Value
                ' Default Value For , Checking Sign on Date for Strategic Call.
                'Used In Java Script Validation.
                '<CONFIG RETENTION='' TARGET='' AIR_NON_AIR='' />
                If objOutputXml.DocumentElement.SelectSingleNode("CONFIG") IsNot Nothing Then
                    With objOutputXml.DocumentElement.SelectSingleNode("CONFIG")
                        hdRetentionChk.Value = .Attributes("RETENTION").Value.Trim
                        hdTargetChk.Value = .Attributes("TARGET").Value.Trim
                        hdAirNonAirChk.Value = .Attributes("AIR_NON_AIR").Value.Trim
                    End With
                End If
                'End


                '  lblHeading.Text = "DSR Details" + Space(1) + System.DateTime.Today.ToString("dd-MMM-yyyy")
                If objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSR_VISIT_ID").Value.Trim.Length > 0 Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSR_VISIT_ID").Value.Trim
                    txtDSRCode.Text = hdID.Value
                End If
                'Binding Agency Details
                With objOutputXml.DocumentElement.SelectSingleNode("AGENCY")
                    'LCODE="43" CHAIN_CODE="300" NAME="Varun Travels Pvt Ltd" ADDRESS="First Floor, Landmark Complex"
                    'CITY="Ahmedabad" COUNTRY="India" DATEFROM="" DATETO="" OFFICEID="AMDWI2105"
                    txtAgencyName.Text = .Attributes("NAME").InnerText
                    txtAddress.Text = .Attributes("ADDRESS").InnerText
                    txtCity.Text = .Attributes("CITY").InnerText
                    txtCountry.Text = .Attributes("COUNTRY").InnerText
                    txtLcode.Text = .Attributes("LCODE").InnerText
                    txtChainCode.Text = .Attributes("CHAIN_CODE").InnerText
                    txtOfficeID.Text = .Attributes("OFFICEID").InnerText
                    ' txtDateFrom.Text = .Attributes("DATEFROM").InnerText
                    '  txtDateTo.Text = .Attributes("DATETO").InnerText
                    TxtCtg.Text = .Attributes("CATEGORY").InnerText
                    If .Attributes("VISITCOUNT") IsNot Nothing Then
                        TxtTotSalesObj.Text = .Attributes("VISITCOUNT").InnerText
                    End If
                    If .Attributes("SEGSTARGET") IsNot Nothing Then
                        TxtSegTarget.Text = .Attributes("SEGSTARGET").InnerText
                    End If

                    If .Attributes("VISITTARGET") IsNot Nothing Then
                        TxtVisitTarget.Text = .Attributes("VISITTARGET").InnerText
                    End If
                    'VISITCOUNT="40" SEGSTARGET="10" VISITTARGET
                    If .Attributes("IATA_ID") IsNot Nothing Then
                        TxtIataID.Text = .Attributes("IATA_ID").Value
                    End If
                    If .Attributes("IATASTATUSID") IsNot Nothing Then
                        Dim li As New ListItem
                        li = DlstIataStatus.Items.FindByValue(.Attributes("IATASTATUSID").Value)
                        If li IsNot Nothing Then
                            DlstIataStatus.SelectedValue = li.Value
                        Else
                            DlstIataStatus.SelectedValue = ""
                        End If
                        'TxtIataID.Text = .Attributes("").Value
                    End If
                    If .Attributes("COMPETITION_COUNT") IsNot Nothing Then
                        HdCompetionCount.value = .Attributes("COMPETITION_COUNT").Value
                    End If
                    If .Attributes("IATAOFFICEID") IsNot Nothing Then
                        TxtIataOfficeID.Text = .Attributes("IATAOFFICEID").Value
                    End If

                    'If DlstIataStatus.SelectedValue <> "" Then
                    '    DlstIataStatus.Enabled = False
                    'End If
                    'If TxtIataID.Text.Trim.Length <> 0 Then
                    '    TxtIataID.ReadOnly = True
                    '    TxtIataID.CssClass = "textboxgrey"
                    'End If

                    'If DlstIataStatus.SelectedValue <> "" AndAlso TxtIataID.Text.Trim.Length <> 0 Then
                    '    BtnUpdateIata.Enabled = False
                    'End If


                End With

                With objOutputXml.DocumentElement.SelectSingleNode("PRODUCTIVITY")
                    '<PRODUCTIVITY>
                    '		<MIDT A1="" B1="" G1="" P1="" W1="" TOTAL="" />
                    '		<BC ONCONTRACT="Y" BCOMMIT="100" MINSEGMENT="" LASTMONTHA1MIDT="" />
                    '		<BIDT MONTH1="157" MONTH2="188" MONTH3="118" />
                    '		<DAILYMOTIVE PAST_MONTH="563" CURRENT_MONTH="354" />
                    '	</PRODUCTIVITY>
                    If .SelectSingleNode("MIDT") IsNot Nothing Then
                        With .SelectSingleNode("MIDT")
                            lit1A.Text = .Attributes("A1").InnerText
                            lit1B.Text = .Attributes("B1").InnerText
                            lit1G.Text = .Attributes("G1").InnerText
                            lit1P.Text = .Attributes("P1").InnerText
                            lit1W.Text = .Attributes("W1").InnerText
                            litTotal.Text = .Attributes("TOTAL").InnerText
                        End With
                    End If
                    If .SelectSingleNode("BC") IsNot Nothing Then
                        With .SelectSingleNode("BC")
                            txtOnContract.Text = .Attributes("ONCONTRACT").InnerText
                            txtBusinessCommit.Text = .Attributes("BCOMMIT").InnerText
                            'txtmin.Text = .Attributes("MINSEGMENT").InnerText
                            txtLatestMonth1A.Text = .Attributes("LASTMONTHA1MIDT").InnerText
                        End With
                    End If
                    If .SelectSingleNode("BIDT") IsNot Nothing Then
                        With .SelectSingleNode("BIDT")
                            litMonthName1.Text = .Attributes("MONTH1_NAME").InnerText
                            litMonthName2.Text = .Attributes("MONTH2_NAME").InnerText
                            litMonthName3.Text = .Attributes("MONTH3_NAME").InnerText
                            litMonth1.Text = .Attributes("MONTH1").InnerText
                            litMonth2.Text = .Attributes("MONTH2").InnerText
                            litMonth3.Text = .Attributes("MONTH3").InnerText
                        End With
                    End If
                    With .SelectSingleNode("DAILYMOTIVE")
                        txtPastMonthDailyMotive.Text = .Attributes("PAST_MONTH").InnerText
                        txtCurrentMonthDailyMotive.Text = .Attributes("CURRENT_MONTH").InnerText
                    End With
                End With

                'Code Start For Visit Details
                If objOutputXml.DocumentElement.SelectSingleNode("AGENCY").Attributes("RESP_1A") IsNot Nothing Then
                    hdRes1A.Value = objOutputXml.DocumentElement.SelectSingleNode("AGENCY").Attributes("RESP_1A").Value
                End If


                BindAccByColleage(hdLCode.Value, hdIsManager.Value.Trim.ToUpper)

                BindManagers(objOutputXml)
                BindGrid(objOutputXml, "VISITDETAIL")

                'Checking Service And Strategic Call.
                If objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("SUB_TYPE_ID").Value.ToString.Trim = "1" Then
                    chkServiceCall.Checked = True
                ElseIf objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("SUB_TYPE_ID").Value.ToString.Trim = "2" Then
                    chkStrategicCall.Checked = True
                ElseIf objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("SUB_TYPE_ID").Value.ToString.Trim = "3" Then
                    chkServiceCall.Checked = True
                    chkStrategicCall.Checked = True
                    'ElseIf Val(objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("SUB_TYPE_ID").Value) = 0 Then
                    '    chkServiceCall.Checked = True
                    '    chkStrategicCall.Checked = True
                End If
                'Checking Retention and Target.
                If objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("STRATEGIC_SUB_TYPE").Value.ToString.Trim = "1" Then
                    rbRetention.Checked = True
                ElseIf objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("STRATEGIC_SUB_TYPE").Value.ToString.Trim = "2" Then
                    rbTarget.Checked = True
                Else
                    rbNone.Checked = True
                End If
                'Checking Air , NonAir Products
                If objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("AIR_NON_AIR_PRODUCT").Value.ToString.Trim = "1" Then
                    chkAirNonAir.Checked = True
                Else
                    chkAirNonAir.Checked = False
                End If
                'End
                ' Disabling Options after DSR Save                
                If hdID.Value <> "" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("VISITDETAIL")
                        If Val(.Attributes("SUB_TYPE_ID").Value) = 0 Then
                            chkStrategicCall.Enabled = True
                            chkServiceCall.Enabled = True
                            rbRetention.Enabled = True
                            rbTarget.Enabled = True
                            rbRetention.Enabled = True
                            chkAirNonAir.Enabled = True
                            rbNone.Enabled = True
                        Else
                            chkStrategicCall.Enabled = False
                            chkServiceCall.Enabled = False
                            rbRetention.Enabled = False
                            rbTarget.Enabled = False
                            rbRetention.Enabled = False
                            chkAirNonAir.Enabled = False
                            rbNone.Enabled = False
                        End If
                    End With

                End If

                '<VISITDETAIL SUB_TYPE_ID="0" STRATEGIC_SUB_TYPE="0" AIR_NON_AIR_PRODUCT="0">


                'Code Start For Service Call
                ' If chkServiceCall.Checked = True Then
                objeAAMS.BindDropDown(ddlDepartment, "DepartmentName", True, 1)
                objeAAMS.BindDropDown(DrpAirNonAirDept, "DepartmentName", True, 1)
                objeAAMS.BindDropDown(DrpRetDept, "DepartmentName", True, 1)
                objeAAMS.BindDropDown(DrpTargetDept, "DepartmentName", True, 1)

                LoadAssignedToByDepartmentId(dlstAssignedTo)

                LoadAirNonAirAssignedToByDepartmentId(DrpAirNonAirAssignedTo)
                LoadTagetAssignedToByDepartmentId(DrpTargetAssignedTo)
                LoadRetentionAssignedToByDepartmentId(DrpRetAssignedTo)

                ' objeAAMS.BindDropDown(ddlDepttSpecific, "PRODUCTS", True, 1)
                objeAAMS.BindDropDown(ddlDepttSpecific, "DEPARTMENTSPECIFIC", True, 1)


                objeAAMS.BindDropDown(ddlServiceStatus, "ServiceCallStatus", True, 1)
                BindGrid(objOutputXml, "SERVICECALL")
                'End If
                'End

                'Code Start For Strategic Visits
                'If chkStrategicCall.Checked = True Then
                objeAAMS.BindDropDown(ddlRetentionReason, "RetentionReason", True, 1)
                objeAAMS.BindDropDown(ddlRetentionStatus, "RetentionStatus", True, 1)
                objeAAMS.BindDropDown(ddlTargetStatus, "TargetStatus", True, 1)
                objeAAMS.BindDropDown(ddlAirNonAirStatus, "AirNonAirOTHStatus", True, 1)
                objeAAMS.BindDropDown(ddlProductName, "PRODUCTS", True, 1)
                'Binding Retention Details
                BindGrid(objOutputXml, "DETAILRETENTION")
                BindGrid(objOutputXml, "REMARKSRETENTION")
                'Binding Target Details
                BindGrid(objOutputXml, "DETAILTARGET")
                BindGrid(objOutputXml, "REMARKSTARGET")
                'Binding Air,NonAir Details
                BindGrid(objOutputXml, "DETAILAIR_OTHER")
                BindGrid(objOutputXml, "REMARKSAIR_OTHER")

                'End If

                'End




            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "BindManagers(ByVal objViewXml As XmlDocument)"
    Private Sub BindManagers(ByVal objViewXml As XmlDocument)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)

            ddlManager.DataSource = objDS.Tables("EMPLOYEE")
            ddlManager.DataTextField = "MANAGER_NAME"
            ddlManager.DataValueField = "MANAGERID"
            ddlManager.DataBind()

            ddlReportingManager.DataSource = objDS.Tables("EMPLOYEE")
            ddlReportingManager.DataTextField = "IMMEDIATE_MANAGERNAME"
            ddlReportingManager.DataValueField = "IMMEDIATE_MANAGERID"
            ddlReportingManager.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)"
    Private Sub BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Dim objDV As DataView
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)
            Select Case strGridType
                Case "VISITDETAIL"
                    objDV = objDS.Tables("DETAIL").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''  "
                    objDV.Sort = "SEQUENCENO"

                    If objDV.Count > 0 Then
                        gvVisitDetails.DataSource = objDV
                    Else
                        gvVisitDetails.DataSource = Nothing
                    End If
                    gvVisitDetails.DataBind()

                    '@ Start of  Added By Abhishek 
                    If gvVisitDetails.Rows.Count > 0 Then
                        'objDV.RowFilter = "DSR_DETAIL_ID<> ''  "
                        'If objDV.Count > 0 Then
                        txtInTime.ReadOnly = True
                        txtOutTime.Enabled = True
                        txtInTime.CssClass = "textboxgrey"
                        txtOutTime.CssClass = "textboxgrey"
                        txtInTime.Text = gvVisitDetails.Rows(0).Cells(5).Text.Substring(0, 2) + gvVisitDetails.Rows(0).Cells(5).Text.Substring(3, 2)
                        txtOutTime.Text = gvVisitDetails.Rows(0).Cells(6).Text.Substring(0, 2) + gvVisitDetails.Rows(0).Cells(6).Text.Substring(3, 2)
                    Else
                        txtInTime.ReadOnly = False
                        txtOutTime.ReadOnly = False
                        txtInTime.CssClass = "textbox"
                        txtOutTime.CssClass = "textbox"
                        ImgPersonMet.Visible = True
                        'End If
                    End If
                    '@ end of  Added By Abhishek 




                Case "SERVICECALL"
                    objDV = objDS.Tables("SERVICECALL").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvServiceCall.DataSource = objDV
                        chkServiceCall.Checked = True
                    Else
                        gvServiceCall.DataSource = Nothing
                    End If
                    gvServiceCall.DataBind()
                Case "DETAILRETENTION"
                    objDV = objDS.Tables("DETAILRETENTION").DefaultView
                    'objDV.RowFilter = "SEQUENCENO <> ''"

                    objDV.RowFilter = "SEQUENCENO <> '' and (ROWNO='' or ROWNO<=3)  "
                    objDV.Sort = "SEQUENCENO"

                    If objDV.Count > 0 Then
                        gvRetention.DataSource = objDV
                        'chkStrategicCall.Checked = True
                        'rbRetention.Checked = True
                        'rbTarget.Enabled = False
                        'rbRetention.Enabled = False
                    Else
                        gvRetention.DataSource = Nothing
                    End If
                    gvRetention.DataBind()

                    objDV.RowFilter = "SEQUENCENO <> '' and ROWNO<>'' and ROWNO > 3 and ROWNO<>'0'  "

                    If objDV.Count > 0 Then
                        ChkRetShowAll.Visible = True
                    Else
                        ChkRetShowAll.Visible = False
                    End If

                Case "DETAILRETENTIONALL"
                    objDV = objDS.Tables("DETAILRETENTION").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> '' "
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvRetention.DataSource = objDV
                    Else
                        gvRetention.DataSource = Nothing
                    End If
                    gvRetention.DataBind()

                Case "DETAILTARGET"
                    objDV = objDS.Tables("DETAILTARGET").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> '' and (ROWNO='' or ROWNO<=3)  "
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvTarget.DataSource = objDV
                        'chkStrategicCall.Checked = True
                        'rbTarget.Checked = True
                        'rbTarget.Enabled = False
                        'rbRetention.Enabled = False
                    Else
                        gvTarget.DataSource = Nothing
                    End If
                    gvTarget.DataBind()

                    objDV.RowFilter = "SEQUENCENO <> '' and ROWNO<>'' and ROWNO > 3 and ROWNO<>'0'   "

                    If objDV.Count > 0 Then
                        ChkTargetShowAll.Visible = True
                    Else
                        ChkTargetShowAll.Visible = False
                    End If

                Case "DETAILTARGETALL"
                    objDV = objDS.Tables("DETAILTARGET").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvTarget.DataSource = objDV
                        'chkStrategicCall.Checked = True
                        'rbTarget.Checked = True
                        'rbTarget.Enabled = False
                        'rbRetention.Enabled = False
                    Else
                        gvTarget.DataSource = Nothing
                    End If
                    gvTarget.DataBind()

                Case "DETAILAIR_OTHER"
                    objDV = objDS.Tables("DETAILAIR_OTHER").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> '' and (ROWNO='' or ROWNO<=3) "

                    objDV.Sort = "SEQUENCENO"

                    If objDV.Count > 0 Then
                        gvAirNonAir.DataSource = objDV
                        chkAirNonAir.Checked = True
                    Else
                        gvAirNonAir.DataSource = Nothing
                    End If
                    gvAirNonAir.DataBind()

                    objDV.RowFilter = "SEQUENCENO <> '' and ROWNO<>'' and ROWNO > 3 and ROWNO<>'0'  "

                    If objDV.Count > 0 Then
                        ChkAirNonAirShowAll.Visible = True
                       
                    Else
                        ChkAirNonAirShowAll.Visible = False
                       
                    End If

                Case "DETAILAIR_OTHERALL"
                    objDV = objDS.Tables("DETAILAIR_OTHER").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvAirNonAir.DataSource = objDV
                        chkAirNonAir.Checked = True
                      
                    Else
                        gvAirNonAir.DataSource = Nothing
                       
                    End If
                    gvAirNonAir.DataBind()
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnAddVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddVisitDetails.Click"
    Protected Sub btnAddVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddVisitDetails.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objVisitNode, objVisitCloneNode As XmlNode
        Try

            '<DETAIL SEQUENCENO='1' MANAGERID='1' MANAGER_NAME='VAISHALI' IMMEDIATE_MANAGERID='2' 
            'IMMEDIATE_MANAGERNAME='ANKUSH' CONTACT_NAME='PANKAJ' DESIGNATION='ML' INTIME='1000' 
            'OUTTIME='1200' />
            objAddXmlDoc.LoadXml(Session("VisitDetails"))

            If btnAddVisitDetails.Text.Trim = "Update" Then
                If hdVisitDetails.Value.Trim.Length > 0 Then
                    objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO=" + hdVisitDetails.Value.Trim + "]")
                    If Not objVisitNode Is Nothing Then


                        '@ Start of Duplicate Checking
                        Dim objCloneNode As XmlNode
                        objCloneNode = objVisitNode.CloneNode(True)
                        For Each objattributes As XmlAttribute In objCloneNode.Attributes
                            objattributes.Value = ""
                        Next

                        objCloneNode.Attributes("SEQUENCENO").Value = hdVisitDetails.Value.Trim

                        If (ddlManager.SelectedValue.Trim.Length > 0) Then
                            objCloneNode.Attributes("MANAGERID").Value = ddlManager.SelectedValue
                            objCloneNode.Attributes("MANAGER_NAME").Value = ddlManager.SelectedItem.Text
                        End If
                        If (ddlReportingManager.SelectedValue.Trim.Length > 0) Then
                            objCloneNode.Attributes("IMMEDIATE_MANAGERID").Value = ddlReportingManager.SelectedValue
                            objCloneNode.Attributes("IMMEDIATE_MANAGERNAME").Value = ddlReportingManager.SelectedItem.Text
                        End If
                        objCloneNode.Attributes("CONTACT_NAME").Value = txtPersonMet.Text.Trim
                        objCloneNode.Attributes("DESIGNATION").Value = txtDesignation.Text.Trim

                        If DuplicateVisitDetails(objCloneNode, "UPDATE") = True Then
                            lblErrorVisitDetails.Text = "Duplicate Record."
                            Exit Sub
                        End If
                        '@ End of Duplicate Checking










                        If (ddlManager.SelectedValue.Trim.Length > 0) Then
                            objVisitNode.Attributes("MANAGERID").Value = ddlManager.SelectedValue
                            objVisitNode.Attributes("MANAGER_NAME").Value = ddlManager.SelectedItem.Text
                        End If
                        If (ddlReportingManager.SelectedValue.Trim.Length > 0) Then
                            objVisitNode.Attributes("IMMEDIATE_MANAGERID").Value = ddlReportingManager.SelectedValue
                            objVisitNode.Attributes("IMMEDIATE_MANAGERNAME").Value = ddlReportingManager.SelectedItem.Text
                        End If
                        objVisitNode.Attributes("CONTACT_NAME").Value = txtPersonMet.Text.Trim
                        objVisitNode.Attributes("DESIGNATION").Value = txtDesignation.Text.Trim
                        objVisitNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                        objVisitNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim

                        hdVisitDetails.Value = ""

                        If objVisitNode.Attributes("ACCBYCOLLEAGUE") IsNot Nothing Then
                            If DrpAccByCollegeus.SelectedValue <> "" Then
                                objVisitNode.Attributes("ACCBYCOLLEAGUE").Value = DrpAccByCollegeus.SelectedValue
                            End If
                        End If
                        If objVisitNode.Attributes("ACCBYCOLLEAGUEBYNAME") IsNot Nothing Then
                            If DrpAccByCollegeus.SelectedValue <> "" Then
                                objVisitNode.Attributes("ACCBYCOLLEAGUEBYNAME").Value = DrpAccByCollegeus.SelectedItem.Text
                            End If
                        End If

                        If objVisitNode.Attributes("JOINTCALLREMARKS") IsNot Nothing Then
                            objVisitNode.Attributes("JOINTCALLREMARKS").Value = TxtJointByCall.Text.Trim
                        End If




                    End If
                End If

                '@ Start of Added By Abhishek
                For Each ObjXmlVisitDetailsNode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("VISITDETAIL/DETAIL")
                    ObjXmlVisitDetailsNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                    ObjXmlVisitDetailsNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim
                Next
                '@ End of Added By Abhishek


            Else
                objVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL")
                objVisitCloneNode = objVisitNode.CloneNode(True)

                For Each objattributes As XmlAttribute In objVisitCloneNode.Attributes
                    objattributes.Value = ""
                Next

                'Deleteing Blank Nodes
                'objBlnkVisitNode = objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO='']")
                'If Not objBlnkVisitNode Is Nothing Then
                '    objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").RemoveChild(objBlnkVisitNode)
                'End If
                objVisitCloneNode.Attributes("SEQUENCENO").Value = gvVisitDetails.Rows.Count + 1
                If (ddlManager.SelectedValue.Trim.Length > 0) Then
                    objVisitCloneNode.Attributes("MANAGERID").Value = ddlManager.SelectedValue
                    objVisitCloneNode.Attributes("MANAGER_NAME").Value = ddlManager.SelectedItem.Text
                End If
                If (ddlReportingManager.SelectedValue.Trim.Length > 0) Then
                    objVisitCloneNode.Attributes("IMMEDIATE_MANAGERID").Value = ddlReportingManager.SelectedValue
                    objVisitCloneNode.Attributes("IMMEDIATE_MANAGERNAME").Value = ddlReportingManager.SelectedItem.Text
                End If
                objVisitCloneNode.Attributes("CONTACT_NAME").Value = txtPersonMet.Text.Trim
                objVisitCloneNode.Attributes("DESIGNATION").Value = txtDesignation.Text.Trim
                objVisitCloneNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                objVisitCloneNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim

                If objVisitCloneNode.Attributes("ACCBYCOLLEAGUE") IsNot Nothing Then
                    If DrpAccByCollegeus.SelectedValue <> "" Then
                        objVisitCloneNode.Attributes("ACCBYCOLLEAGUE").Value = DrpAccByCollegeus.SelectedValue
                    End If
                End If

                If objVisitCloneNode.Attributes("ACCBYCOLLEAGUEBYNAME") IsNot Nothing Then
                    If DrpAccByCollegeus.SelectedValue <> "" Then
                        objVisitCloneNode.Attributes("ACCBYCOLLEAGUEBYNAME").Value = DrpAccByCollegeus.SelectedItem.Text
                    End If
                End If

                If objVisitCloneNode.Attributes("JOINTCALLREMARKS") IsNot Nothing Then
                    objVisitCloneNode.Attributes("JOINTCALLREMARKS").Value = TxtJointByCall.Text.Trim
                End If



                '@ Start of Duplicate Checking
                If DuplicateVisitDetails(objVisitCloneNode, "ADD") = True Then
                    lblErrorVisitDetails.Text = "Duplicate Record."
                    Exit Sub
                End If
                '@ End of Duplicate Checking



                objAddXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL").AppendChild(objVisitCloneNode)
                objVisitCloneNode = objVisitNode.CloneNode(True)

                '@ Start of Added By Abhishek
                For Each ObjXmlVisitDetailsNode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("VISITDETAIL/DETAIL")
                    ObjXmlVisitDetailsNode.Attributes("INTIME").Value = txtInTime.Text.Trim
                    ObjXmlVisitDetailsNode.Attributes("OUTTIME").Value = txtOutTime.Text.Trim
                Next
                '@ End of Added By Abhishek


            End If

            '@Start of new addded code by abhishek UpdateSequenceNo
            Dim RowNum As Integer = 1
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("VISITDETAIL/DETAIL")
                If objnode.Attributes("SEQUENCENO").Value <> "" Then
                    objnode.Attributes("SEQUENCENO").Value = RowNum
                    RowNum = RowNum + 1
                End If
            Next
            '@End of new addded code by abhishek UpdateSequenceNo


            Session("VisitDetails") = objAddXmlDoc.OuterXml
            'Binding Visit Details Data Grid
            BindGrid(objAddXmlDoc, "VISITDETAIL")
            'Clear Controls 
            btnCancelVisitDetails_Click(sender, e)

            ''@ Start of  Added By Abhishek 
            'If gvVisitDetails.Rows.Count > 0 Then
            '    txtInTime.ReadOnly = True
            '    txtOutTime.Enabled = True
            '    txtInTime.CssClass = "textboxgrey"
            '    txtOutTime.CssClass = "textboxgrey"

            '    txtInTime.Text = gvVisitDetails.Rows(0).Cells(5).Text.Substring(0, 2) + gvVisitDetails.Rows(0).Cells(5).Text.Substring(3, 2)
            '    txtOutTime.Text = gvVisitDetails.Rows(0).Cells(6).Text.Substring(0, 2) + gvVisitDetails.Rows(0).Cells(6).Text.Substring(3, 2)
            'End If
            'ImgPersonMet.Visible = True
            ''@ end of  Added By Abhishek 

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "gvVisitDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVisitDetails.RowCommand"
    Protected Sub gvVisitDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVisitDetails.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            '  <DETAIL SEQUENCENO="1" MANAGERID="152" MANAGER_NAME="Sriram" IMMEDIATE_MANAGERID="" 
            'IMMEDIATE_MANAGERNAME="" CONTACT_NAME="Pankaj Kumar Singh" DESIGNATION="General Manager" INTIME="1000" OUTTIME="1200" /> 
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("VisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("VISITDETAIL/DETAIL[@SEQUENCENO='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                        'If Val(.Attributes("DSR_DETAIL_ID").Value.ToString) > 0 Then
                        '    btnAddVisitDetails.Enabled = False
                        'Else
                        '    btnAddVisitDetails.Text = "Update"
                        '    btnAddVisitDetails.Enabled = True
                        'End If

                        btnAddVisitDetails.Text = "Update"
                        btnAddVisitDetails.Enabled = True

                        hdVisitDetails.Value = .Attributes("SEQUENCENO").Value
                        ' ddlManager.SelectedValue = .Attributes("MANAGERID").Value
                        'ddlReportingManager.SelectedValue = .Attributes("IMMEDIATE_MANAGERID").Value

                        If .Attributes("MANAGERID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = ddlManager.Items.FindByValue(.Attributes("MANAGERID").Value)
                            If li IsNot Nothing Then
                                ddlManager.SelectedValue = li.Value
                            Else
                                Dim Id As String = ""
                                Dim Name As String = ""
                                Id = .Attributes("MANAGERID").Value
                                Name = .Attributes("MANAGER_NAME").Value
                                Dim list As New ListItem(Name, Id)
                                ddlManager.Items.Add(list)
                                ddlManager.SelectedValue = Id
                            End If
                        End If

                        If .Attributes("IMMEDIATE_MANAGERID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = ddlReportingManager.Items.FindByValue(.Attributes("IMMEDIATE_MANAGERID").Value)
                            If li IsNot Nothing Then
                                ddlReportingManager.SelectedValue = li.Value
                            Else
                                Dim Id As String = ""
                                Dim Name As String = ""
                                Id = .Attributes("IMMEDIATE_MANAGERID").Value
                                Name = .Attributes("IMMEDIATE_MANAGERNAME").Value
                                Dim list As New ListItem(Name, Id)
                                ddlReportingManager.Items.Add(list)
                                ddlReportingManager.SelectedValue = Id
                            End If
                        End If

                        txtPersonMet.Text = .Attributes("CONTACT_NAME").Value
                        txtDesignation.Text = .Attributes("DESIGNATION").Value
                        txtInTime.Text = .Attributes("INTIME").Value
                        txtOutTime.Text = .Attributes("OUTTIME").Value

                        '@ Start of Added By Abhishek
                        If Val(.Attributes("DSR_DETAIL_ID").Value.ToString) > 0 Then
                            ImgPersonMet.Visible = False
                        Else
                            ImgPersonMet.Visible = True
                        End If

                        txtInTime.ReadOnly = True
                        txtOutTime.ReadOnly = True
                        txtInTime.CssClass = "textboxgrey"
                        txtOutTime.CssClass = "textboxgrey"

                        '@ End of Added By Abhishek
                        TxtJointByCall.Text = ""
                        If objDetailsNode.Attributes("ACCBYCOLLEAGUE") IsNot Nothing Then
                            Dim li As New ListItem
                            li = DrpAccByCollegeus.Items.FindByValue(objDetailsNode.Attributes("ACCBYCOLLEAGUE").Value)
                            If li IsNot Nothing Then
                                DrpAccByCollegeus.SelectedValue = li.Value
                            Else
                                Dim Id As String = ""
                                Dim Name As String = ""
                                Id = .Attributes("ACCBYCOLLEAGUE").Value
                                Name = .Attributes("ACCBYCOLLEAGUEBYNAME").Value
                                Dim list As New ListItem(Name, Id)
                                DrpAccByCollegeus.Items.Add(list)
                                DrpAccByCollegeus.SelectedValue = Id
                            End If
                        End If

                        If objDetailsNode.Attributes("JOINTCALLREMARKS") IsNot Nothing Then
                            TxtJointByCall.Text = objDetailsNode.Attributes("JOINTCALLREMARKS").Value
                        End If





                    End With
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvVisitDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVisitDetails.RowDataBound"
    Protected Sub gvVisitDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVisitDetails.RowDataBound
        Dim strInTime, strOutTime As String
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                strInTime = e.Row.Cells(5).Text
                If strInTime.ToString.Trim.Length = 4 Then
                    e.Row.Cells(5).Text = strInTime.Substring(0, 2) + ":" + strInTime.Substring(2, 2)
                End If
                strOutTime = e.Row.Cells(6).Text
                If strOutTime.ToString.Trim.Length = 4 Then
                    e.Row.Cells(6).Text = strOutTime.Substring(0, 2) + ":" + strOutTime.Substring(2, 2)
                End If
            End If

            'Dim lnkEdit As LinkButton
            'lnkEdit = e.Row.FindControl("lnkEdit")
            'lnkEdit.Attributes.Add("OnClick", "return EditVisitDetails('" & lnkEdit.CommandArgument & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnCancelVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelVisitDetails.Click"
    Protected Sub btnCancelVisitDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelVisitDetails.Click
        Try
            CancelVisitDetails()

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorVisitDetails.Text = ex.Message
        End Try
    End Sub
    Private Sub CancelVisitDetails()
        If (btnAddVisitDetails.Text.Trim = "Update") Then
            btnAddVisitDetails.Text = "Add"
        End If
        ImgPersonMet.Visible = True
        btnAddVisitDetails.Enabled = True
        ddlManager.SelectedIndex = 0
        ddlReportingManager.SelectedIndex = 0
        txtPersonMet.Text = ""
        txtDesignation.Text = ""
        txtInTime.Text = ""
        txtOutTime.Text = ""
        hdVisitDetails.Value = ""



        Dim li As New ListItem
        li = DrpAccByCollegeus.Items.FindByValue("")
        If li IsNot Nothing Then
            DrpAccByCollegeus.SelectedValue = li.Value
        End If

        TxtJointByCall.Text = ""






        '@ Start of  Added By Abhishek      

        If gvVisitDetails.Rows.Count > 0 Then
            Dim objAddXmlDoc As New XmlDocument
            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)
            'Binding Visit Details Data Grid
            BindGrid(objAddXmlDoc, "VISITDETAIL")
        End If
        '@ end of  Added By Abhishek 

    End Sub
#End Region

#Region "btnAddServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddServiceCall.Click"
    Protected Sub btnAddServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddServiceCall.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objBlankServiceNode, objServiceNode, objServiceCloneNode As XmlNode
        Try
            '<SERVICECALLS>
            '		<SERVICECALL SEQUENCENO='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC='' SC_DISCUSSIONISSUE_REMARKS=''
            '			SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME='' SC_COMPETITION_MKT_INFO_REMARKS=''
            '			CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS='' DEPARTMENT_SPECIFIC_ID='' />
            '	</SERVICECALLS>
            objAddXmlDoc.LoadXml(Session("VisitDetails"))
            If btnAddServiceCall.Text.Trim = "Update" Then
                If hdServiceCall.Value.Trim.Length > 0 Then

                    objServiceNode = objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@SEQUENCENO=" + hdServiceCall.Value.Trim + "]")
                    If Not objServiceNode Is Nothing Then

                        '@ Start of Duplicate Checking
                        Dim objnodeclone As XmlNode = objServiceNode.CloneNode(True)
                        For Each objattributes As XmlAttribute In objnodeclone.Attributes
                            objattributes.Value = ""
                        Next
                        objnodeclone.Attributes("SEQUENCENO").Value = hdServiceCall.Value.Trim
                        objnodeclone.Attributes("DEPARTMENTID").Value = ddlDepartment.SelectedValue
                        objnodeclone.Attributes("DEPARTMENT_NAME").Value = ddlDepartment.SelectedItem.Text
                        If ddlDepartment.SelectedItem.ToString.Trim.ToUpper = "PRODUCT" Then
                            If ddlDepttSpecific.SelectedValue <> "" Then
                                objnodeclone.Attributes("DEPARTMENT_SPECIFIC").Value = ddlDepttSpecific.SelectedItem.Text
                            End If

                        Else
                            objnodeclone.Attributes("DEPARTMENT_SPECIFIC").Value = txtDepttSpecific.Text.Trim
                        End If

                        objnodeclone.Attributes("SC_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text.Trim
                        objnodeclone.Attributes("SC_STATUSID").Value = ddlServiceStatus.SelectedValue
                        objnodeclone.Attributes("SC_STATUSID_NAME").Value = ddlServiceStatus.SelectedItem.Text.Trim

                        If dlstAssignedTo.SelectedValue <> "" Then
                            objnodeclone.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = dlstAssignedTo.SelectedItem.Text
                            objnodeclone.Attributes("ASSIGNTO_EMPLOYEEID").Value = dlstAssignedTo.SelectedValue '
                        End If

                        objnodeclone.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtCloserDate.Text.Trim)
                        objnodeclone.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTargetCloserDate.Text.Trim)
                       
                        If DuplicateServiceCall(objnodeclone, "UPDATE") = True Then
                            lblErrorServiceCall.Text = "Duplicate Record."
                            Exit Sub
                        End If


                        '@ End of Duplicate Checking

                        objServiceNode.Attributes("DEPARTMENTID").Value = ddlDepartment.SelectedValue
                        objServiceNode.Attributes("DEPARTMENT_NAME").Value = ddlDepartment.SelectedItem.Text
                        If ddlDepartment.SelectedItem.ToString.Trim.ToUpper = "PRODUCT" Then
                            If ddlDepttSpecific.SelectedValue <> "" Then
                                objServiceNode.Attributes("DEPARTMENT_SPECIFIC").Value = ddlDepttSpecific.SelectedItem.Text
                            Else
                                objServiceNode.Attributes("DEPARTMENT_SPECIFIC").Value = ""
                            End If

                        Else
                            objServiceNode.Attributes("DEPARTMENT_SPECIFIC").Value = txtDepttSpecific.Text.Trim
                        End If

                        objServiceNode.Attributes("SC_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text.Trim
                        objServiceNode.Attributes("SC_STATUSID").Value = ddlServiceStatus.SelectedValue
                        objServiceNode.Attributes("SC_STATUSID_NAME").Value = ddlServiceStatus.SelectedItem.Text.Trim

                        If dlstAssignedTo.SelectedValue <> "" Then
                            objServiceNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = dlstAssignedTo.SelectedItem.Text  'txtAssignedTo.Text.Trim
                            objServiceNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = dlstAssignedTo.SelectedValue 'hdAssingedTo.Value.Trim
                        End If

                        ' objServiceNode.Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value = txtCompetitionInfo.Text.Trim
                        objServiceNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtCloserDate.Text.Trim)
                        objServiceNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTargetCloserDate.Text.Trim)

                        objServiceNode.Attributes("LOGDATE").Value = objeAAMS.ConvertTextDate(System.DateTime.Now.ToString("dd/MM/yyyy"))
                        ' objServiceNode.Attributes("SC_FOLLOWUP_REMARKS").Value = txtFollowUpRemarks.Text.Trim
                        hdServiceCall.Value = ""
                        hdAssingedTo.Value = ""
                    End If
                End If
            Else
                objServiceNode = objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL")
                objServiceCloneNode = objServiceNode.CloneNode(True)

                For Each objattributes As XmlAttribute In objServiceCloneNode.Attributes
                    objattributes.Value = ""
                Next

                'Deleteing Blank Nodes
                'objBlankServiceNode = objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@SEQUENCENO='']")
                'If Not objBlankServiceNode Is Nothing Then
                '    objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS").RemoveChild(objBlankServiceNode)
                'End If
                objServiceCloneNode.Attributes("SEQUENCENO").Value = gvServiceCall.Rows.Count + 1
                objServiceCloneNode.Attributes("DEPARTMENTID").Value = ddlDepartment.SelectedValue
                objServiceCloneNode.Attributes("DEPARTMENT_NAME").Value = ddlDepartment.SelectedItem.Text
                If ddlDepartment.SelectedItem.ToString.Trim.ToUpper = "PRODUCT" Then
                    objServiceCloneNode.Attributes("DEPARTMENT_SPECIFIC").Value = ddlDepttSpecific.SelectedItem.Text
                Else
                    objServiceCloneNode.Attributes("DEPARTMENT_SPECIFIC").Value = txtDepttSpecific.Text.Trim
                End If
                objServiceCloneNode.Attributes("SC_DISCUSSIONISSUE_REMARKS").Value = txtDetailedDiscussion.Text.Trim
                objServiceCloneNode.Attributes("SC_STATUSID").Value = ddlServiceStatus.SelectedValue
                objServiceCloneNode.Attributes("SC_STATUSID_NAME").Value = ddlServiceStatus.SelectedItem.Text.Trim
                'objServiceCloneNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = hdAssingedTo.Value.Trim
                If dlstAssignedTo.SelectedValue <> "" Then
                    objServiceCloneNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = dlstAssignedTo.SelectedValue ' hdAssingedTo.Value.Trim
                    objServiceCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = dlstAssignedTo.SelectedItem.Text  'txtAssignedTo.Text.Trim
                End If

                'objServiceCloneNode.Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value = txtCompetitionInfo.Text.Trim
                objServiceCloneNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtCloserDate.Text.Trim)
                objServiceCloneNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTargetCloserDate.Text.Trim)
                'objServiceCloneNode.Attributes("SC_FOLLOWUP_REMARKS").Value = txtFollowUpRemarks.Text.Trim

                objServiceCloneNode.Attributes("LOGDATE").Value = objeAAMS.ConvertTextDate(System.DateTime.Now.ToString("dd/MM/yyyy"))

                objServiceCloneNode.Attributes("DSR_SC_DETAIL_ID").Value = ""
                hdServiceCall.Value = ""
                hdAssingedTo.Value = ""

                '@ Start of Duplicate Checking
                If DuplicateServiceCall(objServiceCloneNode, "Add") = True Then
                    lblErrorServiceCall.Text = "Duplicate Record."
                    Exit Sub
                End If
                '@ End of Duplicate Checking
                objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS").AppendChild(objServiceCloneNode)
                objServiceCloneNode = objServiceNode.CloneNode(True)
            End If

            '@ Start of Added By UpdateSequenceNo
            Dim RowNum As Integer = 1
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("SERVICECALLS/SERVICECALL")
                If objnode.Attributes("SEQUENCENO").Value <> "" Then
                    objnode.Attributes("SEQUENCENO").Value = RowNum
                    RowNum = RowNum + 1
                End If

            Next
            '@ End of Added By UpdateSequenceNo

            Session("VisitDetails") = objAddXmlDoc.OuterXml
            'Binding Visit Details Data Grid
            BindGrid(objAddXmlDoc, "SERVICECALL")
            'Clear Controls 
            btnCancelServiceCall_Click(sender, e)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorServiceCall.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnCancelServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelServiceCall.Click"
    Protected Sub btnCancelServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelServiceCall.Click
        Try
           
            CanelServiceCall()


        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorServiceCall.Text = ex.Message
        End Try
    End Sub
    Private Sub CanelServiceCall()
        If (btnAddServiceCall.Text.Trim = "Update") Then
            btnAddServiceCall.Text = "Add"
        End If
        btnAddServiceCall.Enabled = True
        ddlDepartment.SelectedIndex = 0
        ddlDepttSpecific.SelectedIndex = 0
        ddlServiceStatus.SelectedIndex = 0
        txtDepttSpecific.Text = ""
        txtDetailedDiscussion.Text = ""
        ' txtAssignedTo.Text = ""

        LoadAssignedToByDepartmentId(dlstAssignedTo)

        Dim li As New ListItem
        li = dlstAssignedTo.Items.FindByValue("")
        If li IsNot Nothing Then
            dlstAssignedTo.SelectedValue = li.Value
        End If

        txtCloserDate.Text = ""
        '  txtCompetitionInfo.Text = ""
        txtTargetCloserDate.Text = ""
        'txtFollowUpRemarks.Text = ""
        hdServiceCall.Value = ""
        hdAssingedTo.Value = ""




        '@ Enable Controls for Service Call
        ddlDepartment.Enabled = True
        ddlDepttSpecific.Enabled = True
        dlstAssignedTo.Enabled = True


        txtDepttSpecific.ReadOnly = False
        txtDetailedDiscussion.ReadOnly = False
        txtTargetCloserDate.ReadOnly = False
        txtCloserDate.ReadOnly = True

        ' imgCloserDate.visible = True

        imgCloserDate.Style.Add("display", "none")

        imgTargetCloserDate.Visible = True

        txtDepttSpecific.CssClass = "textbox"
        txtDetailedDiscussion.CssClass = "textbox"
        txtTargetCloserDate.CssClass = "textbox"
        txtCloserDate.CssClass = "textboxgrey"
        txtCloserDate.Text = ""
        txtDetailedDiscussion.Text = ""




        '@ Enable Controls for Service Call
    End Sub

#End Region

#Region "gvServiceCall_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvServiceCall.RowCommand"
    Protected Sub gvServiceCall_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvServiceCall.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            '<SERVICECALLS>
            '		<SERVICECALL SEQUENCENO='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC='' SC_DISCUSSIONISSUE_REMARKS=''
            '			SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME='' SC_COMPETITION_MKT_INFO_REMARKS=''
            '			CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS='' />
            '	</SERVICECALLS>
            If e.CommandName = "SCDelX" Then
                If Session("VisitDetails") IsNot Nothing Then
                    strCommandArgs = e.CommandArgument
                    objXmlDoc.LoadXml(Session("VisitDetails"))
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@SEQUENCENO='" + strCommandArgs + "']")
                    If objDetailsNode IsNot Nothing Then
                        objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS").RemoveChild(objDetailsNode)

                        '@Start of new addded code by abhishek UpdateSequenceNo
                        Dim RowNum As Integer = 1
                        For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("SERVICECALLS/SERVICECALL")
                            If objnode.Attributes("SEQUENCENO").Value <> "" Then
                                objnode.Attributes("SEQUENCENO").Value = RowNum
                                RowNum = RowNum + 1
                            End If
                        Next
                        '@End of new addded code by abhishek UpdateSequenceNo

                        Session("VisitDetails") = objXmlDoc.OuterXml
                        lblErrorServiceCall.Text = objeAAMSMessage.messDelete
                        BindGrid(objXmlDoc, "SERVICECALL")
                        CanelServiceCall()

                    End If
                Else
                End If
            End If

            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("VisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@SEQUENCENO='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                        hdServiceCall.Value = .Attributes("SEQUENCENO").Value
                        ddlDepartment.SelectedValue = .Attributes("DEPARTMENTID").Value
                        ddlServiceStatus.SelectedValue = .Attributes("SC_STATUSID").Value
                        txtDetailedDiscussion.Text = .Attributes("SC_DISCUSSIONISSUE_REMARKS").Value

                        ' txtAssignedTo.Text = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value.
                        LoadAssignedToByDepartmentId(dlstAssignedTo)


                        If .Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = dlstAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                            If li IsNot Nothing Then
                                dlstAssignedTo.SelectedValue = li.Value
                            Else
                                Dim AssId As String = ""
                                Dim AssName As String = ""
                                AssId = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                                AssName = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                                Dim list As New ListItem(AssName, AssId)
                                dlstAssignedTo.Items.Add(list)
                                dlstAssignedTo.SelectedValue = AssId
                            End If
                        End If


                        'Dim li As New ListItem
                        'li = dlstAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                        'If li IsNot Nothing Then
                        '    dlstAssignedTo.SelectedValue = li.Value
                        'End If

                        hdAssingedTo.Value = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                        txtCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                        ' txtCompetitionInfo.Text = .Attributes("SC_COMPETITION_MKT_INFO_REMARKS").Value
                        txtTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
                        'txtFollowUpRemarks.Text = .Attributes("SC_FOLLOWUP_REMARKS").Value
                        If .Attributes("DEPARTMENT_NAME").Value.Trim.ToUpper = "PRODUCT" Then
                            ddlDepttSpecific.Visible = True
                            If txtDepttSpecific.Visible = True Then
                                txtDepttSpecific.Visible = False
                            End If

                            Dim li As New ListItem
                            li = ddlDepttSpecific.Items.FindByText(.Attributes("DEPARTMENT_SPECIFIC").Value)
                            If li IsNot Nothing Then
                                ddlDepttSpecific.SelectedValue = li.Value
                            End If

                            ' ddlDepttSpecific.SelectedValue = ddlDepttSpecific.Items.FindByText(.Attributes("DEPARTMENT_SPECIFIC").Value.Trim).Value
                        Else
                            txtDepttSpecific.Text = .Attributes("DEPARTMENT_SPECIFIC").Value

                            txtDepttSpecific.Visible = True
                            If ddlDepttSpecific.Visible = True Then
                                ddlDepttSpecific.Visible = False
                            End If

                        End If

                        'If Val(.Attributes("DSR_SC_DETAIL_ID").Value.ToString) > 0 Then
                        '    btnAddServiceCall.Enabled = False
                        '    'If (ddlServiceStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then

                        '    '    ddlDepartment.Enabled = False
                        '    '    ddlDepttSpecific.Enabled = False

                        '    '    txtDepttSpecific.ReadOnly = True
                        '    '    txtDetailedDiscussion.ReadOnly = True
                        '    '    txtAssignedTo.ReadOnly = True
                        '    '    txtTargetCloserDate.ReadOnly = True
                        '    '    txtTargetCloserDate.CssClass = ""

                        '    '    btnAddServiceCall.Text = "Update"
                        '    '    btnAddServiceCall.Enabled = True
                        '    'End If
                        'Else
                        '    btnAddServiceCall.Text = "Update"
                        '    btnAddServiceCall.Enabled = True
                        'End If

                        btnAddServiceCall.Text = "Update"
                        btnAddServiceCall.Enabled = True

                        hdDSR_SC_DETAIL_ID.Value = "" ' Val(.Attributes("DSR_SC_DETAIL_ID").Value.ToString)

                        '@ Disable Controls for Service Call
                        If Val(.Attributes("DSR_SC_DETAIL_ID").Value.ToString) > 0 Then

                            hdDSR_SC_DETAIL_ID.Value = Val(.Attributes("DSR_SC_DETAIL_ID").Value.ToString)

                            ddlDepartment.Enabled = False
                            ddlDepttSpecific.Enabled = False
                            dlstAssignedTo.Enabled = False

                            txtDepttSpecific.ReadOnly = True
                            txtDetailedDiscussion.ReadOnly = True
                            txtTargetCloserDate.ReadOnly = True
                            txtCloserDate.ReadOnly = True

                            ' imgCloserDate.visible = False
                            'If (ddlServiceStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            '    ' imgCloserDate.Attributes.Add("display", "none")
                            '    imgCloserDate.Style.Add("display", "none")
                            'Else
                            '    imgCloserDate.Style.Add("display", "block")
                            'End If
                            imgCloserDate.Style.Add("display", "none")

                            imgTargetCloserDate.Visible = False

                            txtDepttSpecific.CssClass = "textboxgrey"
                            txtDetailedDiscussion.CssClass = "textboxgrey"
                            txtTargetCloserDate.CssClass = "textboxgrey"
                            txtCloserDate.CssClass = "textboxgrey"
                        Else
                            ddlDepartment.Enabled = True
                            ddlDepttSpecific.Enabled = True
                            dlstAssignedTo.Enabled = True

                            txtDepttSpecific.ReadOnly = False
                            txtDetailedDiscussion.ReadOnly = False
                            txtTargetCloserDate.ReadOnly = False


                            '  imgCloserDate.visible = True
                            If (ddlServiceStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                                imgCloserDate.Style.Add("display", "none")
                                txtCloserDate.CssClass = "textboxgrey"
                                txtCloserDate.ReadOnly = True
                            Else
                                imgCloserDate.Style.Add("display", "block")
                                txtCloserDate.CssClass = "textbox"
                                txtCloserDate.ReadOnly = False
                            End If

                            imgTargetCloserDate.Visible = True

                            txtDepttSpecific.CssClass = "textbox"
                            txtDetailedDiscussion.CssClass = "textbox"
                            txtTargetCloserDate.CssClass = "textbox"


                        End If



                        '@ Disable Controls for Service Call



                    End With
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorServiceCall.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvServiceCall_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvServiceCall.RowDataBound"
    Protected Sub gvServiceCall_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvServiceCall.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim LnkSCFRem As New LinkButton
                LnkSCFRem = CType(e.Row.FindControl("LnkSCFRem"), LinkButton)


                Dim txtSCIssueReported As New TextBox
                txtSCIssueReported = CType(e.Row.FindControl("txtSCIssueReported"), TextBox)

                If txtSCIssueReported.Text.Trim.Length > 50 Then
                    Dim strIssueReport As String = Left(txtSCIssueReported.Text, 50)
                    txtSCIssueReported.Text = strIssueReport + "..."
                End If


                Dim LnkSCDel As New LinkButton
                LnkSCDel = CType(e.Row.FindControl("LnkSCDel"), LinkButton)

                Dim HdSC_STATUSID As HiddenField
                HdSC_STATUSID = e.Row.FindControl("HdSC_STATUSID")

                Dim DSR_SC_DETAIL_ID As String = ""
                DSR_SC_DETAIL_ID = LnkSCFRem.CommandArgument.ToString
                If DSR_SC_DETAIL_ID.Trim.Length > 0 Then
                    LnkSCFRem.Enabled = True
                    LnkSCFRem.Attributes.Add("onclick", "return EditServiceCallFolowupRem('" + DSR_SC_DETAIL_ID + "','" + HdSC_STATUSID.Value.Trim + "')")
                    LnkSCDel.Enabled = False
                Else
                    LnkSCFRem.Attributes.Clear()
                    LnkSCFRem.Enabled = False
                    LnkSCDel.Enabled = True
                End If



                '

                If e.Row.Cells(6).Text.Trim.Length = 8 Then
                    e.Row.Cells(6).Text = ConvertToCalenderDate(e.Row.Cells(6).Text)
                End If
                If e.Row.Cells(7).Text.Trim.Length = 8 Then
                    e.Row.Cells(7).Text = ConvertToCalenderDate(e.Row.Cells(7).Text)
                End If
                If e.Row.Cells(8).Text.Trim.Length = 8 Then
                    e.Row.Cells(8).Text = ConvertToCalenderDate(e.Row.Cells(8).Text)
                End If
                'If Val(DataBinder.Eval(e.Row.DataItem, "DSR_STR_DETAIL_ID").ToString) > 0 Then

                'End If


                Dim lnkEdit As New LinkButton
                lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)

               


                If HdSC_STATUSID.Value.Split("|")(1) = "1" Then
                    'LnkSCFRem.Enabled = False
                    'LnkSCFRem.Attributes.Clear()
                    If DSR_SC_DETAIL_ID.Trim.Length > 0 Then
                        lnkEdit.Enabled = False
                        lnkEdit.Attributes.Clear()
                    End If
                End If

            End If

            'Dim lnkEdit As LinkButton
            'lnkEdit = e.Row.FindControl("lnkEdit")
            'lnkEdit.Attributes.Add("OnClick", "return EditVisitDetails('" & lnkEdit.CommandArgument & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorServiceCall.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnAddRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRetention.Click"
    Protected Sub btnAddRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRetention.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objRetentionNode, objRetentionCloneNode As XmlNode
        Try
            '<STRATEGIC_CALL_RETENTION>
            '	<BCDETAILS BCID='' CHAIN_CODE='' CPS='' />
            '	<PREVIOUSREMARKS_RETENTION REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILRETENTION DSR_STR_DETAIL_ID='' SEQUENCENO='' STR_TYPENAME='' EXISTINGDEAL='' CPS='' SVR_REASONID=''
            '		SVR_REASON_NAME='' SVR_STATUSID='' SVR_STATUS_NAME='' A1APPROVED_NEW_DEAL='' NEWCPS='' 
            '       STR_COMPETITION_MKT_INFO_REMARKS=''
            '		STR_DISCUSSIONISSUE_REMARKS='' STR_SIGNON_DATE='' STR_TARGET_SEG='' STR_PREV_REMARKS='' />
            '</STRATEGIC_CALL_RETENTION>
            objAddXmlDoc.LoadXml(Session("VisitDetails"))
            If btnAddRetention.Text.Trim = "Update" Then
                If hdRetention.Value.Trim.Length > 0 Then
                    objRetentionNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO=" + hdRetention.Value.Trim + "]")
                    If Not objRetentionNode Is Nothing Then
                      
                        objRetentionNode.Attributes("STR_TYPENAME").Value = "Strategic Visit - Retention"


                        objRetentionNode.Attributes("CPS").Value = txtCPS.Text.Trim
                        objRetentionNode.Attributes("SVR_REASONID").Value = ddlRetentionReason.SelectedValue
                        objRetentionNode.Attributes("SVR_REASON_NAME").Value = ddlRetentionReason.SelectedItem.Text.Trim
                        objRetentionNode.Attributes("SVR_STATUSID").Value = ddlRetentionStatus.SelectedValue
                        objRetentionNode.Attributes("SVR_STATUS_NAME").Value = ddlRetentionStatus.SelectedItem.Text.Trim
                        objRetentionNode.Attributes("A1APPROVED_NEW_DEAL").Value = txt1AApprovedNewDeal.Text.Trim
                        objRetentionNode.Attributes("NEWCPS").Value = txtNewCPS.Text.Trim
                      
                        objRetentionNode.Attributes("STR_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtRetentionSignedOn.Text.Trim)
                        objRetentionNode.Attributes("STR_TARGET_SEG").Value = txtRetentionTargetSegs.Text.Trim
                       

                        If objRetentionNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            objRetentionNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtAirNonAirFollowup.Text
                        End If

                        If DrpRetDept.SelectedValue <> "" Then
                            If objRetentionNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                                objRetentionNode.Attributes("DEPARTMENTID").Value = DrpRetDept.SelectedValue
                            End If
                            If objRetentionNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                                objRetentionNode.Attributes("DEPARTMENT_NAME").Value = DrpRetDept.SelectedItem.Text
                            End If
                        End If

                        If DrpRetAssignedTo.SelectedValue <> "" Then
                            If objRetentionNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                                objRetentionNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpRetAssignedTo.SelectedItem.Text
                            End If
                            If objRetentionNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                                objRetentionNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpRetAssignedTo.SelectedValue
                            End If
                        End If

                        If objRetentionNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            objRetentionNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtRetCloserDate.Text)
                        End If
                        If objRetentionNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            objRetentionNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtRetTargetCloserDate.Text)
                        End If


                        hdRetention.Value = ""
                    End If
                End If
            Else
                objRetentionNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION")
                objRetentionCloneNode = objRetentionNode.CloneNode(True)

                For Each objattributes As XmlAttribute In objRetentionCloneNode.Attributes
                    objattributes.Value = ""
                Next

               

                objRetentionCloneNode.Attributes("STR_TYPENAME").Value = "Strategic Visit - Retention"
                objRetentionCloneNode.Attributes("SEQUENCENO").Value = gvRetention.Rows.Count + 1

                objRetentionCloneNode.Attributes("CPS").Value = txtCPS.Text.Trim
                objRetentionCloneNode.Attributes("SVR_REASONID").Value = ddlRetentionReason.SelectedValue
                objRetentionCloneNode.Attributes("SVR_REASON_NAME").Value = ddlRetentionReason.SelectedItem.Text.Trim
                objRetentionCloneNode.Attributes("SVR_STATUSID").Value = ddlRetentionStatus.SelectedValue
                objRetentionCloneNode.Attributes("SVR_STATUS_NAME").Value = ddlRetentionStatus.SelectedItem.Text.Trim
                objRetentionCloneNode.Attributes("A1APPROVED_NEW_DEAL").Value = txt1AApprovedNewDeal.Text.Trim
                objRetentionCloneNode.Attributes("NEWCPS").Value = txtNewCPS.Text.Trim
               
                objRetentionCloneNode.Attributes("STR_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtRetentionSignedOn.Text.Trim)
                objRetentionCloneNode.Attributes("STR_TARGET_SEG").Value = txtRetentionTargetSegs.Text.Trim



                If objRetentionCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                    objRetentionCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtRetFollowup.Text
                End If

                If DrpRetDept.SelectedValue <> "" Then
                    If objRetentionCloneNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                        objRetentionCloneNode.Attributes("DEPARTMENTID").Value = DrpRetDept.SelectedValue
                    End If
                    If objRetentionCloneNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                        objRetentionCloneNode.Attributes("DEPARTMENT_NAME").Value = DrpRetDept.SelectedItem.Text
                    End If
                End If



                If DrpRetAssignedTo.SelectedValue <> "" Then
                    If objRetentionCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                        objRetentionCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpRetAssignedTo.SelectedItem.Text
                    End If
                    If objRetentionCloneNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                        objRetentionCloneNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpRetAssignedTo.SelectedValue
                    End If
                End If

                If objRetentionCloneNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    objRetentionCloneNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtRetCloserDate.Text)
                End If
                If objRetentionCloneNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    objRetentionCloneNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtRetTargetCloserDate.Text)
                End If


                objRetentionCloneNode.Attributes("DSR_STR_DETAIL_ID").Value = ""

                objRetentionCloneNode.Attributes("LOGDATE").Value = objeAAMS.ConvertTextDate(System.DateTime.Now.ToString("dd/MM/yyyy"))



                hdRetention.Value = ""

                '@ Start of Duplicate Checking
                If DuplicateRetention(objRetentionCloneNode) = True Then
                    lblErrRetention.Text = "Duplicate Record."
                    Exit Sub
                End If
                '@ End of Duplicate Checking

                objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").AppendChild(objRetentionCloneNode)
                objRetentionCloneNode = objRetentionNode.CloneNode(True)
            End If


            '@Start of new addded code by abhishek UpdateSequenceNo
            Dim RowNum As Integer = 1
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION")

                If objnode.Attributes("SEQUENCENO").Value <> "" Then
                    objnode.Attributes("SEQUENCENO").Value = RowNum
                    RowNum = RowNum + 1
                End If

            Next
            '@End of new addded code by abhishek UpdateSequenceNo


            Session("VisitDetails") = objAddXmlDoc.OuterXml


            'Binding Visit Details Data Grid

            '@ Start of Added By Abhishek
            If ChkRetShowAll.Checked = True Then
                BindGrid(objAddXmlDoc, "DETAILRETENTIONALL")
            Else
                BindGrid(objAddXmlDoc, "DETAILRETENTION")
            End If
            '@ End of Added By Abhishek

            'Clear Controls 
            btnCancelRetention_Click(sender, e)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrRetention.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnCancelRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRetention.Click"
    Protected Sub btnCancelRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelRetention.Click
        Try
            CancelRetention()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrRetention.Text = ex.Message
        End Try
    End Sub
    Private Sub CancelRetention()
        If (btnAddRetention.Text.Trim = "Update") Then
            btnAddRetention.Text = "Add"
        End If
        btnAddRetention.Enabled = True
        ' txtExistingDeal.Text = ""
        txtCPS.Text = ""
        ddlRetentionReason.SelectedIndex = 0
        ddlRetentionStatus.SelectedIndex = 0
        txt1AApprovedNewDeal.Text = ""
        txtNewCPS.Text = ""
        ' txtRetentionCompetitionInfo.Text = ""
        ' txtRetentionDetailedDiscussion.Text = ""
        txtRetentionSignedOn.Text = ""
        txtRetentionTargetSegs.Text = ""
        hdRetention.Value = ""

        Dim li As New ListItem
        li = DrpRetDept.Items.FindByValue("")
        If li IsNot Nothing Then
            DrpRetDept.SelectedValue = li.Value
        End If

        LoadRetentionAssignedToByDepartmentId(DrpRetAssignedTo)

        Dim li2 As New ListItem
        li2 = DrpRetAssignedTo.Items.FindByValue("")
        If li2 IsNot Nothing Then
            DrpRetAssignedTo.SelectedValue = li2.Value
        End If
        TxtRetFollowup.Text = ""

        'Show / Hide Controls
        EnableDisableControls()

        '@ Start Enable Controls
        txtCPS.ReadOnly = False
        ddlRetentionReason.Enabled = True
        txtCPS.CssClass = "textbox"

        hdRetentionSavedData.Value = ""
        txtRetentionSignedOn.Text = ""
        txtRetentionSignedOn.ReadOnly = True
        txtRetentionSignedOn.CssClass = "textboxgrey"
        imgRetentionSignedOn.Style.Add("display", "none")


        txtRetTargetCloserDate.Text = ""
        txtRetCloserDate.Text = ""
        txtRetCloserDate.ReadOnly = True
        txtRetCloserDate.CssClass = "textboxgrey"
        imgRetCloserDate.Style.Add("display", "none")

        '@ end Enable Controls
    End Sub
#End Region

#Region "gvRetention_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRetention.RowCommand"
    Protected Sub gvRetention_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRetention.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            If e.CommandName = "RetDelX" Then
                If Session("VisitDetails") IsNot Nothing Then
                    strCommandArgs = e.CommandArgument
                    objXmlDoc.LoadXml(Session("VisitDetails"))
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO='" + strCommandArgs + "']")
                    If objDetailsNode IsNot Nothing Then
                        objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objDetailsNode)

                        '@Start of new addded code by abhishek UpdateSequenceNo
                        Dim RowNum As Integer = 1
                        For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION")
                            If objnode.Attributes("SEQUENCENO").Value <> "" Then
                                objnode.Attributes("SEQUENCENO").Value = RowNum
                                RowNum = RowNum + 1
                            End If
                        Next
                        '@End of new addded code by abhishek UpdateSequenceNo

                        Session("VisitDetails") = objXmlDoc.OuterXml
                        lblErrRetention.Text = objeAAMSMessage.messDelete

                        ChkRetShowAll.Checked = False

                        BindGrid(objXmlDoc, "DETAILRETENTION")

                        CancelRetention()

                    End If
                Else
                End If
            End If
           
            '<STRATEGIC_CALL_RETENTION>
            '	<BCDETAILS BCID='' CHAIN_CODE='' CPS='' />
            '	<PREVIOUSREMARKS_RETENTION REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILRETENTION DSR_STR_DETAIL_ID='' SEQUENCENO='' STR_TYPENAME='' EXISTINGDEAL='' CPS='' 
            '       SVR_REASONID=''
            '		SVR_REASON_NAME='' SVR_STATUSID='' SVR_STATUS_NAME='' A1APPROVED_NEW_DEAL='' NEWCPS='' 
            '       STR_COMPETITION_MKT_INFO_REMARKS=''
            '		STR_DISCUSSIONISSUE_REMARKS='' STR_SIGNON_DATE='' STR_TARGET_SEG='' STR_PREV_REMARKS='' />
            '</STRATEGIC_CALL_RETENTION>
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("VisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                       

                        btnAddRetention.Text = "Add"
                        btnAddRetention.Enabled = True

                        hdRetention.Value = .Attributes("SEQUENCENO").Value

                        txtCPS.Text = .Attributes("CPS").Value
                        ddlRetentionReason.SelectedValue = .Attributes("SVR_REASONID").Value
                        ddlRetentionStatus.SelectedValue = .Attributes("SVR_STATUSID").Value
                        txt1AApprovedNewDeal.Text = .Attributes("A1APPROVED_NEW_DEAL").Value
                        txtNewCPS.Text = .Attributes("NEWCPS").Value
                       
                        txtRetentionSignedOn.Text = ConvertToCalenderDate(.Attributes("STR_SIGNON_DATE").Value)
                        txtRetentionTargetSegs.Text = .Attributes("STR_TARGET_SEG").Value


                        If .Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            TxtRetFollowup.Text = .Attributes("INTERNAL_FOLLOWUP_REMARKS").Value
                        End If


                        If .Attributes("DEPARTMENTID") IsNot Nothing Then
                            DrpRetDept.SelectedValue = .Attributes("DEPARTMENTID").Value
                        End If
                        LoadRetentionAssignedToByDepartmentId(DrpRetAssignedTo)


                        If .Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = DrpRetAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                            If li IsNot Nothing Then
                                DrpRetAssignedTo.SelectedValue = li.Value
                            Else
                                Dim AssId As String = ""
                                Dim AssName As String = ""
                                AssId = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                                AssName = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                                Dim list As New ListItem(AssName, AssId)
                                DrpRetAssignedTo.Items.Add(list)
                                DrpRetAssignedTo.SelectedValue = AssId
                            End If
                        End If


                        If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            txtRetCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                        End If
                        If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            txtRetTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
                        End If



                        hdRetentionSavedData.Value = ""
                        '###########@ Start of New  Code'###############
                        If ddlRetentionStatus.SelectedValue.Split("|").GetValue(0).ToString = hdRetentionChk.Value.Trim Then
                            txtRetentionSignedOn.ReadOnly = False
                            txtRetentionSignedOn.CssClass = "textbox"
                            imgRetentionSignedOn.Style.Add("display", "block")
                        Else
                            txtRetentionSignedOn.ReadOnly = True
                            txtRetentionSignedOn.CssClass = "textboxgrey"
                            imgRetentionSignedOn.Style.Add("display", "none")
                        End If


                        If (ddlRetentionStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgRetCloserDate.Style.Add("display", "none")
                            txtRetCloserDate.CssClass = "textboxgrey"
                            txtRetCloserDate.ReadOnly = True
                        Else
                            imgRetCloserDate.Style.Add("display", "block")
                            txtRetCloserDate.CssClass = "textbox"
                            txtRetCloserDate.ReadOnly = False
                        End If


                        '###########@ End of New Code'###############



                    End With
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrRetention.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvRetention_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRetention.RowDataBound"
    Protected Sub gvRetention_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRetention.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(6).Text.Trim.Length = 8 Then
                    e.Row.Cells(6).Text = ConvertToCalenderDate(e.Row.Cells(6).Text)
                End If

                Dim HdSVR_STATUSID As HiddenField = CType(e.Row.FindControl("HdSVR_STATUSID"), HiddenField)
                Dim HdDSR_STR_DETAIL_ID As HiddenField = CType(e.Row.FindControl("HdDSR_STR_DETAIL_ID"), HiddenField)

                Dim LnkRetDel As New LinkButton
                LnkRetDel = CType(e.Row.FindControl("LnkRetDel"), LinkButton)

                If HdDSR_STR_DETAIL_ID.Value.Trim.Length > 0 Then
                    LnkRetDel.Enabled = False
                Else
                    LnkRetDel.Enabled = True
                End If

                If e.Row.Cells(8).Text.Trim.Length = 8 Then
                    e.Row.Cells(8).Text = ConvertToCalenderDate(e.Row.Cells(8).Text)
                End If
                If e.Row.Cells(9).Text.Trim.Length = 8 Then
                    e.Row.Cells(9).Text = ConvertToCalenderDate(e.Row.Cells(9).Text)
                End If
                If e.Row.Cells(10).Text.Trim.Length = 8 Then
                    e.Row.Cells(10).Text = ConvertToCalenderDate(e.Row.Cells(10).Text)
                End If

            End If

            'Dim lnkEdit As LinkButton
            'lnkEdit = e.Row.FindControl("lnkEdit")
            'lnkEdit.Attributes.Add("OnClick", "return EditVisitDetails('" & lnkEdit.CommandArgument & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrRetention.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnTargetAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTargetAdd.Click"
    Protected Sub btnTargetAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTargetAdd.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objBlankTargetNode, objTargetNode, objTargetCloneNode As XmlNode
        Try
            '<STRATEGIC_CALL_TARGET>
            '	<PREVIOUSREMARKS_TARGET REMARKS1="" REMARKS2="" REMARKS3="" REMARKS4="" />
            '	<DETAILTARGET DSR_STT_DETAIL_ID="" SEQUENCENO="" STT_TYPENAME="" A1APPROVED_NEW_DEAL="" 
            '   CPS="" SVT_STATUSID="" SVT_STATUS_NAME="" STT_COMPETITION_MKT_INFO_REMARKS="" 
            '   STT_DISCUSSIONISSUE_REMARKS="" STT_SIGNON_DATE="" STT_TARGET_SEG="" STT_PREV_REMARKS="" />
            '</STRATEGIC_CALL_TARGET>
            objAddXmlDoc.LoadXml(Session("VisitDetails"))
            If btnTargetAdd.Text.Trim = "Update" Then
                If hdTarget.Value.Trim.Length > 0 Then
                    objTargetNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO=" + hdTarget.Value.Trim + "]")
                    If Not objTargetNode Is Nothing Then
                      
                        objTargetNode.Attributes("STT_TYPENAME").Value = "Strategic Visit - Target"
                        objTargetNode.Attributes("A1APPROVED_NEW_DEAL").Value = txtTarget1AApprovedNewDeal.Text.Trim
                        objTargetNode.Attributes("CPS").Value = txtTargetCPS.Text.Trim
                        objTargetNode.Attributes("SVT_STATUSID").Value = ddlTargetStatus.SelectedValue
                        objTargetNode.Attributes("SVT_STATUS_NAME").Value = ddlTargetStatus.SelectedItem.Text.Trim
                      
                        objTargetNode.Attributes("STT_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtTargetSignedOn.Text.Trim)
                        objTargetNode.Attributes("STT_TARGET_SEG").Value = txtTargetTargetSegs.Text.Trim


                        If objTargetNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            objTargetNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtTarFollowup.Text
                        End If

                        If DrpTargetDept.SelectedValue <> "" Then
                            If objTargetNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                                objTargetNode.Attributes("DEPARTMENTID").Value = DrpTargetDept.SelectedValue
                            End If
                            If objTargetNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                                objTargetNode.Attributes("DEPARTMENT_NAME").Value = DrpTargetDept.SelectedItem.Text
                            End If
                        End If

                        If DrpTargetAssignedTo.SelectedValue <> "" Then
                            If objTargetNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                                objTargetNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpTargetAssignedTo.SelectedItem.Text
                            End If
                            If objTargetNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                                objTargetNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpTargetAssignedTo.SelectedValue
                            End If
                        End If

                        If objTargetNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            objTargetNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTarCloserDate.Text)
                        End If
                        If objTargetNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            objTargetNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTarTargetCloserDate.Text)
                        End If



                        hdTarget.Value = ""
                    End If
                End If
            Else
                objTargetNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET")
                objTargetCloneNode = objTargetNode.CloneNode(True)

                For Each objattributes As XmlAttribute In objTargetCloneNode.Attributes
                    objattributes.Value = ""

                Next
               
                objTargetCloneNode.Attributes("STT_TYPENAME").Value = "Strategic Visit - Target"
                objTargetCloneNode.Attributes("SEQUENCENO").Value = gvTarget.Rows.Count + 1
                objTargetCloneNode.Attributes("A1APPROVED_NEW_DEAL").Value = txtTarget1AApprovedNewDeal.Text.Trim
                objTargetCloneNode.Attributes("CPS").Value = txtTargetCPS.Text.Trim
                objTargetCloneNode.Attributes("SVT_STATUSID").Value = ddlTargetStatus.SelectedValue
                objTargetCloneNode.Attributes("SVT_STATUS_NAME").Value = ddlTargetStatus.SelectedItem.Text.Trim
              
                objTargetCloneNode.Attributes("STT_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtTargetSignedOn.Text.Trim)
                objTargetCloneNode.Attributes("STT_TARGET_SEG").Value = txtTargetTargetSegs.Text.Trim
                objTargetCloneNode.Attributes("STT_PREV_REMARKS").Value = ""


                If objTargetCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                    objTargetCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtTarFollowup.Text
                End If

                If DrpTargetDept.SelectedValue <> "" Then
                    If objTargetCloneNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                        objTargetCloneNode.Attributes("DEPARTMENTID").Value = DrpTargetDept.SelectedValue
                    End If
                    If objTargetCloneNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                        objTargetCloneNode.Attributes("DEPARTMENT_NAME").Value = DrpTargetDept.SelectedItem.Text
                    End If
                End If


                If DrpTargetAssignedTo.SelectedValue <> "" Then
                    If objTargetCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                        objTargetCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpTargetAssignedTo.SelectedItem.Text
                    End If
                    If objTargetCloneNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                        objTargetCloneNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpTargetAssignedTo.SelectedValue
                    End If
                End If

                If objTargetCloneNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    objTargetCloneNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTarCloserDate.Text)
                End If
                If objTargetCloneNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    objTargetCloneNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtTarTargetCloserDate.Text)
                End If

                objTargetCloneNode.Attributes("LOGDATE").Value = objeAAMS.ConvertTextDate(System.DateTime.Now.ToString("dd/MM/yyyy"))


                hdTarget.Value = ""

                '@ Start of Duplicate Checking
                If DuplicateTarget(objTargetCloneNode) = True Then
                    lblErrTarget.Text = "Duplicate Record."
                    Exit Sub
                End If
                '@ End of Duplicate Checking

                objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").AppendChild(objTargetCloneNode)
                objTargetCloneNode = objTargetNode.CloneNode(True)
            End If

            '@ UpdateSequenceNo
            Dim RowNum As Integer = 1
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET")
                If objnode.Attributes("SEQUENCENO").Value <> "" Then
                    objnode.Attributes("SEQUENCENO").Value = RowNum
                    RowNum = RowNum + 1
                End If

            Next
            '@ UpdateSequenceNo

            Session("VisitDetails") = objAddXmlDoc.OuterXml
            'Binding Visit Details Data Grid

            '@ Start of Added By Abhishek
            If ChkTargetShowAll.Checked = True Then
                BindGrid(objAddXmlDoc, "DETAILTARGETALL")
            Else
                BindGrid(objAddXmlDoc, "DETAILTARGET")
            End If
            '@ End of Added By Abhishek

            'Clear Controls 
            btnbtnTargetCancel_Click(sender, e)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnbtnTargetCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbtnTargetCancel.Click"
    Protected Sub btnbtnTargetCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbtnTargetCancel.Click
        Try
            ClearTargetCancel()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
    Private Sub ClearTargetCancel()
        If (btnTargetAdd.Text.Trim = "Update") Then
            btnTargetAdd.Text = "Add"
        End If
        btnTargetAdd.Enabled = True
        txtTarget1AApprovedNewDeal.Text = ""
        txtTargetCPS.Text = ""
        ddlTargetStatus.SelectedIndex = 0
        'txtTargetCompetitionInfo.Text = ""
        'txtTargetDetailedDiscussion.Text = ""


        Dim li As New ListItem
        li = DrpTargetDept.Items.FindByValue("")
        If li IsNot Nothing Then
            DrpTargetDept.SelectedValue = li.Value
        End If

        LoadTagetAssignedToByDepartmentId(DrpTargetAssignedTo)

        Dim li2 As New ListItem
        li2 = DrpTargetAssignedTo.Items.FindByValue("")
        If li2 IsNot Nothing Then
            DrpTargetAssignedTo.SelectedValue = li2.Value
        End If

        TxtTarFollowup.Text = ""

        txtTargetSignedOn.Text = ""
        txtTargetTargetSegs.Text = ""
        hdTarget.Value = ""
        hdTargetSaveData.Value = ""

        txtTargetSignedOn.Text = ""
        txtTargetSignedOn.ReadOnly = True
        txtTargetSignedOn.CssClass = "textboxgrey"
        imgTargetSignedOn.Style.Add("display", "none")

        txtTarTargetCloserDate.Text = ""
        txtTarCloserDate.Text = ""
        txtTarCloserDate.ReadOnly = True
        txtTarCloserDate.CssClass = "textboxgrey"
        imgTarCloserDate.Style.Add("display", "none")


    End Sub
#End Region

#Region "gvTarget_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTarget.RowCommand"
    Protected Sub gvTarget_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTarget.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try

            If e.CommandName = "TarDelX" Then
                If Session("VisitDetails") IsNot Nothing Then
                    strCommandArgs = e.CommandArgument
                    objXmlDoc.LoadXml(Session("VisitDetails"))
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO='" + strCommandArgs + "']")
                    If objDetailsNode IsNot Nothing Then
                        objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objDetailsNode)

                        '@Start of new addded code by abhishek UpdateSequenceNo
                        Dim RowNum As Integer = 1
                        For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET")
                            If objnode.Attributes("SEQUENCENO").Value <> "" Then
                                objnode.Attributes("SEQUENCENO").Value = RowNum
                                RowNum = RowNum + 1
                            End If
                        Next
                        '@End of new addded code by abhishek UpdateSequenceNo

                        Session("VisitDetails") = objXmlDoc.OuterXml
                        lblErrTarget.Text = objeAAMSMessage.messDelete

                        ChkTargetShowAll.Checked = False

                        BindGrid(objXmlDoc, "DETAILTARGET")

                        ClearTargetCancel()

                    End If
                Else
                End If
            End If
           


            '<STRATEGIC_CALL_TARGET>
            '	<PREVIOUSREMARKS_TARGET REMARKS1="" REMARKS2="" REMARKS3="" REMARKS4="" />
            '	<DETAILTARGET DSR_STT_DETAIL_ID="" SEQUENCENO="" STT_TYPENAME="" A1APPROVED_NEW_DEAL="" 
            '   CPS="" SVT_STATUSID="" SVT_STATUS_NAME="" STT_COMPETITION_MKT_INFO_REMARKS="" 
            '   STT_DISCUSSIONISSUE_REMARKS="" STT_SIGNON_DATE="" STT_TARGET_SEG="" STT_PREV_REMARKS="" />
            '</STRATEGIC_CALL_TARGET>
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("VisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                      

                        btnTargetAdd.Text = "Add"
                        btnTargetAdd.Enabled = True

                        hdTarget.Value = .Attributes("SEQUENCENO").Value
                        txtTarget1AApprovedNewDeal.Text = .Attributes("A1APPROVED_NEW_DEAL").Value
                        txtTargetCPS.Text = .Attributes("CPS").Value
                        ddlTargetStatus.SelectedValue = .Attributes("SVT_STATUSID").Value
                       
                        txtTargetSignedOn.Text = ConvertToCalenderDate(.Attributes("STT_SIGNON_DATE").Value)
                        txtTargetTargetSegs.Text = .Attributes("STT_TARGET_SEG").Value


                        If .Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            TxtTarFollowup.Text = .Attributes("INTERNAL_FOLLOWUP_REMARKS").Value
                        End If


                        If .Attributes("DEPARTMENTID") IsNot Nothing Then
                            DrpTargetDept.SelectedValue = .Attributes("DEPARTMENTID").Value
                        End If
                        LoadTagetAssignedToByDepartmentId(DrpTargetAssignedTo)


                        If .Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = DrpTargetAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                            If li IsNot Nothing Then
                                DrpTargetAssignedTo.SelectedValue = li.Value
                            Else
                                Dim AssId As String = ""
                                Dim AssName As String = ""
                                AssId = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                                AssName = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                                Dim list As New ListItem(AssName, AssId)
                                DrpTargetAssignedTo.Items.Add(list)
                                DrpTargetAssignedTo.SelectedValue = AssId
                            End If
                        End If


                        If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            txtTarCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                        End If
                        If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            txtTarTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
                        End If

                        hdTargetSaveData.Value = ""



                        ' ################## New Code ##########################


                        If ddlTargetStatus.SelectedValue.Split("|").GetValue(0).ToString.Trim = hdTargetChk.Value.Trim Then
                            txtTargetSignedOn.ReadOnly = False
                            txtTargetSignedOn.CssClass = "textbox"
                            imgTargetSignedOn.Style.Add("display", "block")
                        Else
                            txtTargetSignedOn.ReadOnly = True
                            txtTargetSignedOn.CssClass = "textboxgrey"
                            imgTargetSignedOn.Style.Add("display", "none")
                        End If


                        If (ddlTargetStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgTarCloserDate.Style.Add("display", "none")
                            txtTarCloserDate.CssClass = "textboxgrey"
                            txtTarCloserDate.ReadOnly = True
                        Else
                            imgTarCloserDate.Style.Add("display", "block")
                            txtTarCloserDate.CssClass = "textbox"
                            txtTarCloserDate.ReadOnly = False
                        End If




                        '################## New Code ##########################



                       





                    End With
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvTarget_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTarget.RowDataBound"
    Protected Sub gvTarget_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTarget.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(4).Text.Trim.Length = 8 Then
                    e.Row.Cells(4).Text = ConvertToCalenderDate(e.Row.Cells(4).Text)
                End If

                Dim HdSVT_STATUSID As HiddenField = CType(e.Row.FindControl("HdSVT_STATUSID"), HiddenField)
                Dim DSR_STT_DETAIL_ID As HiddenField = CType(e.Row.FindControl("DSR_STT_DETAIL_ID"), HiddenField)

                Dim LnkTarDel As New LinkButton
                LnkTarDel = CType(e.Row.FindControl("LnkTarDel"), LinkButton)

                If DSR_STT_DETAIL_ID.Value.Trim.Length > 0 Then
                    LnkTarDel.Enabled = False
                Else
                    LnkTarDel.Enabled = True
                End If


                If e.Row.Cells(6).Text.Trim.Length = 8 Then
                    e.Row.Cells(6).Text = ConvertToCalenderDate(e.Row.Cells(6).Text)
                End If
                If e.Row.Cells(7).Text.Trim.Length = 8 Then
                    e.Row.Cells(7).Text = ConvertToCalenderDate(e.Row.Cells(7).Text)
                End If
                If e.Row.Cells(8).Text.Trim.Length = 8 Then
                    e.Row.Cells(8).Text = ConvertToCalenderDate(e.Row.Cells(8).Text)
                End If
            End If

            'Dim lnkEdit As LinkButton
            'lnkEdit = e.Row.FindControl("lnkEdit")
            'lnkEdit.Attributes.Add("OnClick", "return EditVisitDetails('" & lnkEdit.CommandArgument & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnAirNonAirAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirNonAirAdd.Click"
    Protected Sub btnAirNonAirAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirNonAirAdd.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objBlankAirNonAirNode, objAirNonAirNode, objAirNonAirCloneNode As XmlNode
        Try
            '<STRATEGIC_CALL_AIR_OTHER>
            '	<PREVIOUSREMARKS_AIR_OTHER REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILAIR_OTHER DSR_STA_DETAIL_ID='' SEQUENCENO='' STA_TYPENAME='' PRODUCTID='' PRODUCT_NAME=''
            '		REVENUE='' SV_STATUSID='' SV_STATUS_NAME='' STA_COMPETITION_MKT_INFO_REMARKS='' STA_DISCUSSIONISSUE_REMARKS=''
            '		STA_SIGNON_DATE='' STA_PREV_REMARKS='' />
            '</STRATEGIC_CALL_AIR_OTHER>
            objAddXmlDoc.LoadXml(Session("VisitDetails"))
            If btnAirNonAirAdd.Text.Trim = "Update" Then
                If hdAirNonAir.Value.Trim.Length > 0 Then
                    objAirNonAirNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO=" + hdAirNonAir.Value.Trim + "]")
                    If Not objAirNonAirNode Is Nothing Then
                       
                        objAirNonAirNode.Attributes("STA_TYPENAME").Value = "Strategic Visit - Air,NonAir"
                        objAirNonAirNode.Attributes("PRODUCTID").Value = ddlProductName.SelectedValue
                        objAirNonAirNode.Attributes("PRODUCT_NAME").Value = ddlProductName.SelectedItem.Text
                        objAirNonAirNode.Attributes("REVENUE").Value = txtRevenue.Text.Trim
                        objAirNonAirNode.Attributes("SV_STATUSID").Value = ddlAirNonAirStatus.SelectedValue
                        objAirNonAirNode.Attributes("SV_STATUS_NAME").Value = ddlAirNonAirStatus.SelectedItem.Text.Trim
                      
                        objAirNonAirNode.Attributes("STA_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtAirNonAirSignedOn.Text.Trim)
                       

                        If objAirNonAirNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            objAirNonAirNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtAirNonAirFollowup.Text
                        End If

                        If DrpAirNonAirDept.SelectedValue <> "" Then
                            If objAirNonAirNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                                objAirNonAirNode.Attributes("DEPARTMENTID").Value = DrpAirNonAirDept.SelectedValue
                            End If
                            If objAirNonAirNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                                objAirNonAirNode.Attributes("DEPARTMENT_NAME").Value = DrpAirNonAirDept.SelectedItem.Text
                            End If
                        End If


                        If DrpAirNonAirAssignedTo.SelectedValue <> "" Then
                            If objAirNonAirNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                                objAirNonAirNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpAirNonAirAssignedTo.SelectedItem.Text
                            End If
                            If objAirNonAirNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                                objAirNonAirNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpAirNonAirAssignedTo.SelectedValue
                            End If
                        End If

                        If objAirNonAirNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            objAirNonAirNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtAirCloserDate.Text)
                        End If
                        If objAirNonAirNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            objAirNonAirNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtAirTargetCloserDate.Text)
                        End If

                        hdAirNonAir.Value = ""
                    End If
                End If
            Else
                objAirNonAirNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER")
                objAirNonAirCloneNode = objAirNonAirNode.CloneNode(True)

                For Each objattributes As XmlAttribute In objAirNonAirCloneNode.Attributes
                    objattributes.Value = ""
                Next


                objAirNonAirCloneNode.Attributes("STA_TYPENAME").Value = "Strategic Visit - Air,NonAir"
                objAirNonAirCloneNode.Attributes("SEQUENCENO").Value = gvAirNonAir.Rows.Count + 1
                objAirNonAirCloneNode.Attributes("PRODUCTID").Value = ddlProductName.SelectedValue
                objAirNonAirCloneNode.Attributes("PRODUCT_NAME").Value = ddlProductName.SelectedItem.Text
                objAirNonAirCloneNode.Attributes("REVENUE").Value = txtRevenue.Text.Trim
                objAirNonAirCloneNode.Attributes("SV_STATUSID").Value = ddlAirNonAirStatus.SelectedValue
                objAirNonAirCloneNode.Attributes("SV_STATUS_NAME").Value = ddlAirNonAirStatus.SelectedItem.Text.Trim
              
                objAirNonAirCloneNode.Attributes("STA_SIGNON_DATE").Value = objeAAMS.ConvertTextDate(txtAirNonAirSignedOn.Text.Trim)

                hdAirNonAir.Value = ""


                If objAirNonAirCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                    objAirNonAirCloneNode.Attributes("INTERNAL_FOLLOWUP_REMARKS").Value = TxtAirNonAirFollowup.Text
                End If

                If DrpAirNonAirDept.SelectedValue <> "" Then
                    If objAirNonAirCloneNode.Attributes("DEPARTMENTID") IsNot Nothing Then
                        objAirNonAirCloneNode.Attributes("DEPARTMENTID").Value = DrpAirNonAirDept.SelectedValue
                    End If
                    If objAirNonAirCloneNode.Attributes("DEPARTMENT_NAME") IsNot Nothing Then
                        objAirNonAirCloneNode.Attributes("DEPARTMENT_NAME").Value = DrpAirNonAirDept.SelectedItem.Text
                    End If
                End If


                If DrpAirNonAirAssignedTo.SelectedValue <> "" Then
                    If objAirNonAirCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME") IsNot Nothing Then
                        objAirNonAirCloneNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = DrpAirNonAirAssignedTo.SelectedItem.Text
                    End If
                    If objAirNonAirCloneNode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                        objAirNonAirCloneNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = DrpAirNonAirAssignedTo.SelectedValue
                    End If
                End If

                If objAirNonAirCloneNode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    objAirNonAirCloneNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtAirCloserDate.Text)
                End If
                If objAirNonAirCloneNode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    objAirNonAirCloneNode.Attributes("TARGET_CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtAirTargetCloserDate.Text)
                End If

                objAirNonAirCloneNode.Attributes("LOGDATE").Value = objeAAMS.ConvertTextDate(System.DateTime.Now.ToString("dd/MM/yyyy"))

                '@ Start of Duplicate Checking
                If DuplicateAirNonAir(objAirNonAirCloneNode) = True Then
                    lblErrAirNonAirProduct.Text = "Duplicate Record."
                    Exit Sub
                End If
                '@ End of Duplicate Checking


                objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").AppendChild(objAirNonAirCloneNode)
                objAirNonAirCloneNode = objAirNonAirNode.CloneNode(True)
            End If


            '@ UpdateSequenceNo
            Dim RowNum As Integer = 1
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER")
                If objnode.Attributes("SEQUENCENO").Value <> "" Then
                    objnode.Attributes("SEQUENCENO").Value = RowNum
                    RowNum = RowNum + 1
                End If

            Next
            '@ UpdateSequenceNo


            Session("VisitDetails") = objAddXmlDoc.OuterXml
            'Binding Visit Details Data Grid
            '@ Start of Added By Abhishek
            If ChkAirNonAirShowAll.Checked = True Then
                BindGrid(objAddXmlDoc, "DETAILAIR_OTHERALL")
            Else

                BindGrid(objAddXmlDoc, "DETAILAIR_OTHER")
            End If
            '@ End  of Added By Abhishek

            'Clear Controls 
            btnAirNonAirCancel_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "btnAirNonAirCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirNonAirCancel.Click"
    Protected Sub btnAirNonAirCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAirNonAirCancel.Click
        Try
            ClearAirNonAir()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub
    Private Sub ClearAirNonAir()
        If (btnAirNonAirAdd.Text.Trim = "Update") Then
            btnAirNonAirAdd.Text = "Add"
        End If
        btnAirNonAirAdd.Enabled = True
        ddlProductName.SelectedIndex = 0
        txtRevenue.Text = ""
        ddlAirNonAirStatus.SelectedIndex = 0
        ' txtAirNonAirCompetitionInfo.Text = ""
      '  txtAirNonAirDetailedDiscussion.Text = ""
        txtAirNonAirSignedOn.Text = ""
        hdAirNonAir.Value = ""

        Dim li As New ListItem
        li = DrpAirNonAirDept.Items.FindByValue("")
        If li IsNot Nothing Then
            DrpAirNonAirDept.SelectedValue = li.Value
        End If

        LoadAirNonAirAssignedToByDepartmentId(DrpAirNonAirAssignedTo)


        Dim li2 As New ListItem
        li2 = DrpAirNonAirAssignedTo.Items.FindByValue("")
        If li2 IsNot Nothing Then
            DrpAirNonAirAssignedTo.SelectedValue = li2.Value
        End If

        TxtAirNonAirFollowup.Text = ""

        '@ Disable Controls for Service Call

        '   hdairSaveData.Value = ""
        hdAirNonAirSaveData.value = ""
        ddlProductName.Enabled = True
        txtRevenue.ReadOnly = False
        txtRevenue.CssClass = "textbox"
        txtAirNonAirSignedOn.ReadOnly = True
        imgAirNonAirSignedOn.Style.Add("display", "none")
        txtAirNonAirSignedOn.CssClass = "textboxgrey"


        txtAirTargetCloserDate.Text = ""
        txtAirCloserDate.Text = ""
        txtAirCloserDate.ReadOnly = True
        txtAirCloserDate.CssClass = "textboxgrey"
        imgAirCloserDate.Style.Add("display", "none")
        '@ Disable Controls for Service Call


    End Sub

#End Region

#Region "gvAirNonAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAirNonAir.RowCommand"
    Protected Sub gvAirNonAir_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAirNonAir.RowCommand
        Dim strCommandArgs As String = ""
        Dim objXmlDoc As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            If e.CommandName = "AirNonairDelX" Then
                If Session("VisitDetails") IsNot Nothing Then
                    strCommandArgs = e.CommandArgument
                    objXmlDoc.LoadXml(Session("VisitDetails"))
                    objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO='" + strCommandArgs + "']")
                    If objDetailsNode IsNot Nothing Then
                        objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objDetailsNode)

                        '@Start of new addded code by abhishek UpdateSequenceNo
                        Dim RowNum As Integer = 1
                        For Each objnode As XmlNode In objXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER")
                            If objnode.Attributes("SEQUENCENO").Value <> "" Then
                                objnode.Attributes("SEQUENCENO").Value = RowNum
                                RowNum = RowNum + 1
                            End If
                        Next
                        '@End of new addded code by abhishek UpdateSequenceNo

                        Session("VisitDetails") = objXmlDoc.OuterXml
                        lblErrAirNonAirProduct.Text = objeAAMSMessage.messDelete

                        ChkAirNonAirShowAll.Checked = False

                        BindGrid(objXmlDoc, "DETAILAIR_OTHER")

                        ClearAirNonAir()

                    End If
                Else
                End If
            End If

          
            '<STRATEGIC_CALL_AIR_OTHER>
            '	<PREVIOUSREMARKS_AIR_OTHER REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILAIR_OTHER DSR_STA_DETAIL_ID='' SEQUENCENO='' STA_TYPENAME='' PRODUCTID='' PRODUCT_NAME=''
            '		REVENUE='' SV_STATUSID='' SV_STATUS_NAME='' STA_COMPETITION_MKT_INFO_REMARKS='' STA_DISCUSSIONISSUE_REMARKS=''
            '		STA_SIGNON_DATE='' STA_PREV_REMARKS='' />
            '</STRATEGIC_CALL_AIR_OTHER>
            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("VisitDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO='" + strCommandArgs + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode
                       
                        btnAirNonAirAdd.Text = "Add"
                        btnAirNonAirAdd.Enabled = True

                        hdAirNonAir.Value = .Attributes("SEQUENCENO").Value
                        ddlProductName.SelectedValue = .Attributes("PRODUCTID").Value
                        txtRevenue.Text = .Attributes("REVENUE").Value
                        ddlAirNonAirStatus.SelectedValue = .Attributes("SV_STATUSID").Value
                        txtAirNonAirSignedOn.Text = ConvertToCalenderDate(.Attributes("STA_SIGNON_DATE").Value)
                        If .Attributes("INTERNAL_FOLLOWUP_REMARKS") IsNot Nothing Then
                            TxtAirNonAirFollowup.Text = .Attributes("INTERNAL_FOLLOWUP_REMARKS").Value
                        End If

                        If .Attributes("DEPARTMENTID") IsNot Nothing Then
                            DrpAirNonAirDept.SelectedValue = .Attributes("DEPARTMENTID").Value
                        End If

                      

                        LoadAirNonAirAssignedToByDepartmentId(DrpAirNonAirAssignedTo)

                        If .Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                            Dim li As New ListItem
                            li = DrpAirNonAirAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                            If li IsNot Nothing Then
                                DrpAirNonAirAssignedTo.SelectedValue = li.Value
                            Else
                                Dim AssId As String = ""
                                Dim AssName As String = ""
                                AssId = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                                AssName = .Attributes("ASSIGNTO_EMPLOYEE_NAME").Value
                                Dim list As New ListItem(AssName, AssId)
                                DrpAirNonAirAssignedTo.Items.Add(list)
                                DrpAirNonAirAssignedTo.SelectedValue = AssId
                            End If
                        End If


                        If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                            txtAirCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                        End If
                        If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                            txtAirTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
                        End If


                        '@ Disable Controls for AirNonAir Call
                        hdAirNonAirSaveData.Value = ""
                        If ddlAirNonAirStatus.SelectedValue.Split("|").GetValue(0).ToString = hdAirNonAirChk.Value.Trim Then
                            txtAirNonAirSignedOn.ReadOnly = False
                            txtAirNonAirSignedOn.CssClass = "textbox"
                            imgAirNonAirSignedOn.Style.Add("display", "block")
                        Else
                            txtAirNonAirSignedOn.ReadOnly = True
                            txtAirNonAirSignedOn.CssClass = "textboxgrey"
                            imgAirNonAirSignedOn.Style.Add("display", "none")
                        End If

                        If (ddlAirNonAirStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgAirCloserDate.Style.Add("display", "none")
                            txtAirCloserDate.CssClass = "textboxgrey"
                            txtAirCloserDate.ReadOnly = True
                        Else
                            imgAirCloserDate.Style.Add("display", "block")
                            txtAirCloserDate.CssClass = "textbox"
                            txtAirCloserDate.ReadOnly = False
                        End If


                    End With
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvAirNonAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAirNonAir.RowDataBound'"
    Protected Sub gvAirNonAir_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAirNonAir.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(4).Text.Trim.Length = 8 Then
                    e.Row.Cells(4).Text = ConvertToCalenderDate(e.Row.Cells(4).Text)
                End If



                Dim HdSV_STATUSID As HiddenField = CType(e.Row.FindControl("HdSV_STATUSID"), HiddenField)
                Dim HdDSR_STA_DETAIL_ID As HiddenField = CType(e.Row.FindControl("HdDSR_STA_DETAIL_ID"), HiddenField)

                Dim LnkAirNonairDel As New LinkButton
                LnkAirNonairDel = CType(e.Row.FindControl("LnkAirNonairDel"), LinkButton)

                If HdDSR_STA_DETAIL_ID.Value.Trim.Length > 0 Then
                    LnkAirNonairDel.Enabled = False
                Else
                    LnkAirNonairDel.Enabled = True
                End If

                If e.Row.Cells(5).Text.Trim.Length = 8 Then
                    e.Row.Cells(5).Text = ConvertToCalenderDate(e.Row.Cells(5).Text)
                End If
                If e.Row.Cells(6).Text.Trim.Length = 8 Then
                    e.Row.Cells(6).Text = ConvertToCalenderDate(e.Row.Cells(6).Text)
                End If
                If e.Row.Cells(7).Text.Trim.Length = 8 Then
                    e.Row.Cells(7).Text = ConvertToCalenderDate(e.Row.Cells(7).Text)
                End If
            End If

            'Dim lnkEdit As LinkButton
            'lnkEdit = e.Row.FindControl("lnkEdit")
            'lnkEdit.Attributes.Add("OnClick", "return EditVisitDetails('" & lnkEdit.CommandArgument & "');")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strSUB_TYPE_ID As String
        Dim objRemoveNode As XmlNode
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objDSRLogXml As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Try
            ' Load Input Xml.
            objInputXml.LoadXml(Session("VisitDetails").ToString())

            If chkServiceCall.Checked = True And chkStrategicCall.Checked = True Then
                strSUB_TYPE_ID = "3"
            ElseIf chkServiceCall.Checked = True Then
                strSUB_TYPE_ID = "1"
            ElseIf chkStrategicCall.Checked = True Then
                strSUB_TYPE_ID = "2"
            Else
                chkServiceCall.Focus()
                lblError.Text = "Visit sub type is mandatory."
                Exit Sub
            End If

            If (hdIsManager.Value = "1" Or hdIsManager.Value = "True") Then
                objInputXml.DocumentElement.SelectSingleNode("DSR").Attributes("MANAGER").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DSR").Attributes("MANAGER").InnerText = "False"
            End If

            If gvVisitDetails.Rows.Count = 0 Then
                lblError.Text = "Visit details is mandatory."
                Exit Sub
            End If

            If (strSUB_TYPE_ID = "1") Then
                If gvServiceCall.Rows.Count = 0 Then
                    lblError.Text = "Service call details is mandatory."
                    Exit Sub
                End If
            End If


            If (strSUB_TYPE_ID = "2") Then
                If (rbTarget.Checked = False And rbRetention.Checked = False) Then
                    'lblError.Text = "Please select Retention or Target."
                    'Exit Sub
                End If
                If rbRetention.Checked Then
                    If (gvRetention.Rows.Count = 0) Then
                        lblError.Text = "Retention details is mandatory."
                        Exit Sub
                    End If
                End If

                If rbTarget.Checked Then
                    If (gvTarget.Rows.Count = 0) Then
                        lblError.Text = "Target details is mandatory."
                        Exit Sub
                    End If
                End If
            End If

            If (strSUB_TYPE_ID = "3") Then
                If gvServiceCall.Rows.Count = 0 Then
                    lblError.Text = "Service call details is mandatory."
                    Exit Sub
                End If
                If (rbTarget.Checked = False And rbRetention.Checked = False) Then
                    'lblError.Text = "Please select Retention or Target."
                    'Exit Sub
                End If
                If rbRetention.Checked Then
                    If (gvRetention.Rows.Count = 0) Then
                        lblError.Text = "Retention details is mandatory."
                        Exit Sub
                    End If
                End If

                If rbTarget.Checked Then
                    If (gvTarget.Rows.Count = 0) Then
                        lblError.Text = "Target details is mandatory."
                        Exit Sub
                    End If
                End If
            End If


            If chkAirNonAir.Checked = True Then
                If (rbTarget.Checked = False And rbRetention.Checked = False) Then
                    'lblError.Text = "Please add Retention / Target details."
                    'Exit Sub
                End If
            End If


            If strSUB_TYPE_ID = "2" Or strSUB_TYPE_ID = "3" Then
                If (rbTarget.Checked = False And rbRetention.Checked = False And chkAirNonAir.Checked = False) Then
                    lblError.Text = "Please add Retention / Target / Air & Non Air details."
                    Exit Sub
                End If
            End If

            If strSUB_TYPE_ID = "2" Or strSUB_TYPE_ID = "3" Then
                If (rbTarget.Checked = False And rbRetention.Checked = False And chkAirNonAir.Checked = True) Then
                    If gvAirNonAir.Rows.Count = 0 Then
                        lblError.Text = "Air & Non Air details. is mandatory."
                        Exit Sub
                    End If
                ElseIf chkAirNonAir.Checked = True Then
                    If gvAirNonAir.Rows.Count = 0 Then
                        lblError.Text = "Air & Non Air details. is mandatory."
                        Exit Sub
                    End If

                End If
            End If

            '@ Issue Report Mandatory checking
            If strSUB_TYPE_ID = "2" Or strSUB_TYPE_ID = "3" Then
                If hdID.Value.Trim.Length = 0 Then

                    If (rbTarget.Checked = True) Then
                        If objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET[@DSR_VISIT_ID='']").Count = 0 Then
                            lblError.Text = "Issue Report is mandatory for Target."
                            Exit Sub
                        End If
                    End If
                    If (rbRetention.Checked = True) Then
                        If objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION[@DSR_VISIT_ID='']").Count = 0 Then
                            lblError.Text = "Issue Report is mandatory for Retention."
                            Exit Sub
                        End If
                    End If
                    If (chkAirNonAir.Checked = True) Then
                        If objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER[@DSR_VISIT_ID='']").Count = 0 Then
                            lblError.Text = "Issue Report is mandatory for Air Non Air."
                            Exit Sub
                        End If
                    End If

                End If

            End If





            objInputXml.DocumentElement.SelectSingleNode("DSR").Attributes("EMPLOYEEID").Value = Session("LoginSession").ToString().Split("|")(0)
            objInputXml.DocumentElement.SelectSingleNode("VISITDETAIL").Attributes("SUB_TYPE_ID").Value = strSUB_TYPE_ID
            'SERVICECALLS--SERVICECALL
            'STRATEGIC_CALL_RETENTION--DETAILRETENTION
            'STRATEGIC_CALL_TARGET--DETAILTARGET
            'STRATEGIC_CALL_AIR_OTHER--DETAILAIR_OTHER 
            If strSUB_TYPE_ID = 1 Then ' Code For Service Call
                For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO != '']")
                    If objRemoveNode IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objRemoveNode)
                    End If
                Next
                For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO != '']")
                    If objRemoveNode IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objRemoveNode)
                    End If
                Next
                For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO != '']")
                    If objRemoveNode IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objRemoveNode)
                    End If
                Next
            ElseIf strSUB_TYPE_ID = 2 Then 'Code For Startegic Call
                For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("SERVICECALLS/SERVICECALL[@SEQUENCENO != '']")
                    If objRemoveNode IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("SERVICECALLS").RemoveChild(objRemoveNode)
                    End If
                Next
                If rbRetention.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objRemoveNode)
                        End If
                    Next
                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objRemoveNode)
                        End If
                    Next
                End If

                If rbTarget.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objRemoveNode)
                        End If
                    Next
                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objRemoveNode)
                        End If
                    Next
                End If

                If chkAirNonAir.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objRemoveNode)
                        End If
                    Next

                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objRemoveNode)
                        End If
                    Next
                End If
            ElseIf strSUB_TYPE_ID = 3 Then 'Code For Service And Strategic Call.
                If rbRetention.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objRemoveNode)
                        End If
                    Next

                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/ISSURE_REPORT_RETENTION")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objRemoveNode)
                        End If
                    Next
                End If

                If rbTarget.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objRemoveNode)
                        End If
                    Next

                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/ISSURE_REPORT_TARGET")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objRemoveNode)
                        End If
                    Next
                End If

                If chkAirNonAir.Checked = False Then
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO != '']")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objRemoveNode)
                        End If
                    Next

                    '@ Removing Temporaty Added Issue Report
                    For Each objRemoveNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/ISSURE_REPORT_AIR_OTHER")
                        If objRemoveNode IsNot Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objRemoveNode)
                        End If
                    Next

                End If
            End If


            '@ New Added Code 
            With (objInputXml.DocumentElement.SelectSingleNode("AGENCY"))
                If .Attributes("IATA_ID") IsNot Nothing Then
                    .Attributes("IATA_ID").Value = TxtIataID.Text
                End If
                If .Attributes("IATASTATUSID") IsNot Nothing Then
                    .Attributes("IATASTATUSID").Value = ""
                    If DlstIataStatus.SelectedValue <> "" Then
                        .Attributes("IATASTATUSID").Value = DlstIataStatus.SelectedValue
                    End If
                End If
                If .Attributes("COMPETITION_COUNT") IsNot Nothing Then
                    .Attributes("COMPETITION_COUNT").Value = HdCompetionCount.Value
                End If
            End With
            '@ New Added Code         


            ' Calling update method for update.
            objInputXml.Save("c:\admin\aams\DSRUpdateInput.xml")
            objOutputXml = objDSR.Update(objInputXml)
            objOutputXml.Save("c:\admin\aams\DSRUpdateOutput.xml")
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("VisitDetails") = objOutputXml.OuterXml
                If (hdID.Value <> "") Then
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    '<DSR PREDATE="20100201" DSR_VISIT_ID="12" EMPLOYEEID="202779" /> 
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSR_VISIT_ID").Value.Trim()
                    txtDSRCode.Text = hdID.Value
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                End If
                ViewDetails(True)

                If Request.QueryString("FrmType") IsNot Nothing Then
                    If Session("AgencyDSRLOGXML") IsNot Nothing Then
                        objDSRLogXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                        With objDSRLogXml.DocumentElement.SelectSingleNode("DSRLOG[@LCODE='" + hdLCode.Value + "']")
                            .Attributes("DSR_VISIT_ID").Value = hdID.Value
                            .Attributes("INTIME").Value = gvVisitDetails.Rows(0).Cells(5).Text
                            .Attributes("OUTTIME").Value = gvVisitDetails.Rows(0).Cells(6).Text
                        End With
                        Session("AgencyDSRLOGXML") = objDSRLogXml.OuterXml
                        ClientScript.RegisterStartupScript(Me.GetType(), "close", "try{window.opener.document.forms['form1']['BtnAppendUnplannedData'].click();}catch(err){} ", True)
                    End If
                End If

                'If btnbtnTargetCancel.Text.Trim.ToUpper = "UPDATE" Then
                '    btnbtnTargetCancel_Click(sender, e)
                'End If
                'If btnAddServiceCall.Text.Trim.ToUpper = "UPDATE" Then
                '    btnAddServiceCall_Click(sender, e)
                'End If
                'If btnCancelRetention.Text.Trim.ToUpper = "UPDATE" Then
                '    btnCancelRetention_Click(sender, e)
                'End If
                'If btnAirNonAirCancel.Text.Trim.ToUpper = "UPDATE" Then
                '    btnAirNonAirCancel_Click(sender, e)
                'End If
                'If btnCancelVisitDetails.Text.Trim.ToUpper = "UPDATE" Then
                '    btnCancelVisitDetails_Click(sender, e)
                'End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            ' CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If hdID.Value = "" Then
            Response.Redirect(Request.Url.ToString)
        Else
            ViewDetails(False)
            EnableDisableControls()
            btnCancelVisitDetails_Click(sender, e)
            btnCancelServiceCall_Click(sender, e)
            btnCancelRetention_Click(sender, e)
            btnbtnTargetCancel_Click(sender, e)
            btnAirNonAirCancel_Click(sender, e)

            If Val(HdCompetionCount.Value) = 0 Then
                Dim param As String = "../TravelAgency/TAUP_AgencyCompetition.aspx?Id=2&SalesVisit='V'&Action=U|" + hdLCode.Value + "&Lcode=" + hdLCode.Value
                iframeID.Attributes("src") = param

                PnlCalenderImagePnl.CssClass = "displayBlock"
                CalenderImage.Attributes("class") = "displayBlock"
                mdlPopUpCalender.Show()

            End If
        End If
    End Sub
#End Region

#Region "ConvertToCalenderDate(ByVal strDateInInterger As String) As String"
    Private Function ConvertToCalenderDate(ByVal strDateInInterger As String) As String
        Try
            If strDateInInterger.Trim.Length = 8 Then
                strDateInInterger = strDateInInterger.Substring(6, 2) + "/" + strDateInInterger.Substring(4, 2) + "/" + strDateInInterger.Substring(0, 4)
            End If
        Catch ex As Exception
        End Try
        Return strDateInInterger
    End Function
#End Region

#Region "EnableDisableControls()::Enable/Disable Controls On The Basis Of Service Call Or Strtegic Call"
    Private Sub EnableDisableControls()
        Try
            If chkServiceCall.Checked = True Then
                pnlServiceCallCollapseManage.Visible = True
                pnlServiceCall.Visible = True
            Else
                pnlServiceCallCollapseManage.Visible = False
                pnlServiceCall.Visible = False
            End If

            If chkStrategicCall.Checked = True Then
                pnlStrategicVisits.Visible = True
            Else
                pnlStrategicVisits.Visible = False
            End If

            If rbRetention.Checked = True Then
                pnlRetention.Visible = True
                pnlRetentionCollapseManage.Visible = True
            Else
                pnlRetention.Visible = False
                pnlRetentionCollapseManage.Visible = False

            End If
            If rbTarget.Checked = True Then
                pnlTarget.Visible = True
                pnlTargetCollapseManage.Visible = True
            Else
                pnlTarget.Visible = False
                pnlTargetCollapseManage.Visible = False
            End If

            If rbnone.Checked = True Then
                pnlTarget.Visible = False
                pnlTargetCollapseManage.Visible = False
                pnlTarget.Visible = False
                pnlTargetCollapseManage.Visible = False
            End If



            If ddlDepartment.Items.Count > 0 Then
                If ddlDepartment.SelectedItem.Text.Trim.ToUpper = "PRODUCT" Then
                    ddlDepttSpecific.Visible = True
                    txtDepttSpecific.Visible = False
                Else
                    ddlDepttSpecific.Visible = False
                    txtDepttSpecific.Visible = True
                End If
            End If

            If chkAirNonAir.Checked = True Then
                'If Page.IsPostBack Then
                '    If (rbTarget.Checked = False And rbRetention.Checked = False) Then
                '        lblError.Text = "please select retention / target."
                '    End If
                'End If
                pnlAirNonAir.Visible = True
                pnlAirNonAirCollapseManage.Visible = True
            Else
                pnlAirNonAir.Visible = False
                pnlAirNonAirCollapseManage.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            'Throw ex
        End Try
    End Sub
#End Region


    Protected Sub chkServiceCall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkServiceCall.CheckedChanged, chkStrategicCall.CheckedChanged, chkAirNonAir.CheckedChanged

    End Sub
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR Logging']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    ' btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("DSRVistedId") IsNot Nothing) Then
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            '@ #################   Security For Modify Agency IATA
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Modify Agency Contact Info']").Attributes("Value").Value)
                    If (Session("Action") IsNot Nothing) Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            If strBuilder(2) = "0" Then
                                TxtIataID.ReadOnly = True
                                TxtIataID.CssClass = "textboxgrey"
                                DlstIataStatus.Enabled = False
                                BtnUpdateIata.Enabled = False
                            Else
                                TxtIataID.ReadOnly = False
                                TxtIataID.CssClass = "textbox"
                                DlstIataStatus.Enabled = True
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                TxtIataID.ReadOnly = False
                TxtIataID.CssClass = "textbox"
                DlstIataStatus.Enabled = True
            End If
            '@ #################  End of   Security For Modify Agency Agency IATA



            '###### New Code ############################################################################
            Try


                '@ Start  To find the Value where Visit date is available in Allow BackDate Table or not for this employee



                '@ End  To find the Value where Visit date is available in Allow BackDate Table or not for this employee

                ''  If They have back date entry then N no of days form config defined back dated entries is allowed
                '<DSR_TARGET_DAYS_CHECK>True</DSR_TARGET_DAYS_CHECK> 
                '<DSR_TARGET_DAYS>5</DSR_TARGET_DAYS> 
                If hdID.Value.Trim.Length = 0 Then

                    Dim ObjXmlNodeDSR_TARGET_DAYS_CHECK As XmlNode
                    Dim ObjXmlNodeDSR_TARGET_DAYS As XmlNode
                    ObjXmlNodeDSR_TARGET_DAYS_CHECK = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK")
                    ObjXmlNodeDSR_TARGET_DAYS = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS")
                    If ObjXmlNodeDSR_TARGET_DAYS_CHECK IsNot Nothing AndAlso ObjXmlNodeDSR_TARGET_DAYS IsNot Nothing Then
                        If ObjXmlNodeDSR_TARGET_DAYS_CHECK.InnerText.Trim.ToUpper = "TRUE" Then
                            Dim NoOfBackDaysCheckFormConfig As Integer = Val(ObjXmlNodeDSR_TARGET_DAYS.InnerText)
                            Dim VisitDate As String = Request.QueryString("VisitDate")
                            Dim StrDay As String = VisitDate.Trim.Substring(6, 2)
                            Dim StrMonth As String = VisitDate.Trim.Substring(4, 2)
                            Dim StrYear As String = VisitDate.Trim.Substring(0, 4)
                            Dim DsrDate As New Date(StrYear, StrMonth, StrDay)
                            Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
                            Dim intdaysDiff As Long = DateDiff(DateInterval.Day, DsrDate, TodayDate)

                            If (intdaysDiff >= NoOfBackDaysCheckFormConfig Or intdaysDiff < 0) Then

                                If hdBackDateAllow.Value = "TRUE" Then

                                Else
                                    btnSave.Enabled = False
                                    btnAddVisitDetails.Enabled = False
                                    btnCancelVisitDetails.Enabled = False
                                    btnAddServiceCall.Enabled = False
                                    btnCancelServiceCall.Enabled = False
                                    btnTargetAdd.Enabled = False
                                    btnbtnTargetCancel.Enabled = False
                                    btnAddRetention.Enabled = False
                                    btnCancelRetention.Enabled = False
                                    btnAirNonAirAdd.Enabled = False
                                    btnAirNonAirCancel.Enabled = False
                                End If
                            End If
                        Else
                            If hdBackDateAllow.Value = "TRUE" Then

                            Else
                                btnSave.Enabled = False
                                btnAddVisitDetails.Enabled = False
                                btnCancelVisitDetails.Enabled = False
                                btnAddServiceCall.Enabled = False
                                btnCancelServiceCall.Enabled = False
                                btnTargetAdd.Enabled = False
                                btnbtnTargetCancel.Enabled = False
                                btnAddRetention.Enabled = False
                                btnCancelRetention.Enabled = False
                                btnAirNonAirAdd.Enabled = False
                                btnAirNonAirCancel.Enabled = False
                            End If

                        End If
                    Else
                        If hdBackDateAllow.Value = "TRUE" Then

                        Else
                            btnSave.Enabled = False
                            btnAddVisitDetails.Enabled = False
                            btnCancelVisitDetails.Enabled = False
                            btnAddServiceCall.Enabled = False
                            btnCancelServiceCall.Enabled = False
                            btnTargetAdd.Enabled = False
                            btnbtnTargetCancel.Enabled = False
                            btnAddRetention.Enabled = False
                            btnCancelRetention.Enabled = False
                            btnAirNonAirAdd.Enabled = False
                            btnAirNonAirCancel.Enabled = False
                        End If

                    End If
                    '@  End of Code for Modification of DSR Back Dated

                End If
              
            Catch ex As Exception
            End Try

            '##################################################################################



            ''###### Prev New Code ############################################################################
            'Try
            '    '@  Start of Code for Modification of DSR Back Dated
            '    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Count <> 0 Then
            '            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Back Date Entry on DSR Visit']").Attributes("Value").Value)
            '            If strBuilder(1) = "1" Or strBuilder(2) = "1" Then  ' If They have back date entry then any back dated entries is allowed
            '            Else ' If They have back date entry then N no of days form config defined back dated entries is allowed

            '                '<DSR_TARGET_DAYS_CHECK>True</DSR_TARGET_DAYS_CHECK> 
            '                '<DSR_TARGET_DAYS>5</DSR_TARGET_DAYS> 

            '                Dim ObjXmlNodeDSR_TARGET_DAYS_CHECK As XmlNode
            '                Dim ObjXmlNodeDSR_TARGET_DAYS As XmlNode

            '                ObjXmlNodeDSR_TARGET_DAYS_CHECK = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS_CHECK")
            '                ObjXmlNodeDSR_TARGET_DAYS = objSecurityXml.DocumentElement.SelectSingleNode("DSR_TARGET_DAYS")
            '                If ObjXmlNodeDSR_TARGET_DAYS_CHECK IsNot Nothing AndAlso ObjXmlNodeDSR_TARGET_DAYS IsNot Nothing Then
            '                    If ObjXmlNodeDSR_TARGET_DAYS_CHECK.InnerText.Trim.ToUpper = "TRUE" Then
            '                        Dim NoOfBackDaysCheckFormConfig As Integer = Val(ObjXmlNodeDSR_TARGET_DAYS.InnerText)
            '                        Dim VisitDate As String = Request.QueryString("VisitDate")
            '                        Dim StrDay As String = VisitDate.Trim.Substring(6, 2)
            '                        Dim StrMonth As String = VisitDate.Trim.Substring(4, 2)
            '                        Dim StrYear As String = VisitDate.Trim.Substring(0, 4)
            '                        Dim DsrDate As New Date(StrYear, StrMonth, StrDay)
            '                        Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
            '                        Dim intdaysDiff As Long = DateDiff(DateInterval.Day, DsrDate, TodayDate)

            '                        If (intdaysDiff >= NoOfBackDaysCheckFormConfig Or intdaysDiff < 0) Then
            '                            btnSave.Enabled = False
            '                            btnAddVisitDetails.Enabled = False
            '                            btnCancelVisitDetails.Enabled = False
            '                            btnAddServiceCall.Enabled = False
            '                            btnCancelServiceCall.Enabled = False
            '                            btnTargetAdd.Enabled = False
            '                            btnbtnTargetCancel.Enabled = False

            '                            btnAddRetention.Enabled = False
            '                            btnCancelRetention.Enabled = False
            '                            btnAirNonAirAdd.Enabled = False
            '                            btnAirNonAirCancel.Enabled = False
            '                        End If
            '                    Else
            '                        btnSave.Enabled = False


            '                        btnAddVisitDetails.Enabled = False
            '                        btnCancelVisitDetails.Enabled = False
            '                        btnAddServiceCall.Enabled = False
            '                        btnCancelServiceCall.Enabled = False
            '                        btnTargetAdd.Enabled = False
            '                        btnbtnTargetCancel.Enabled = False

            '                        btnAddRetention.Enabled = False
            '                        btnCancelRetention.Enabled = False
            '                        btnAirNonAirAdd.Enabled = False
            '                        btnAirNonAirCancel.Enabled = False
            '                    End If
            '                Else
            '                    btnSave.Enabled = False


            '                    btnAddVisitDetails.Enabled = False
            '                    btnCancelVisitDetails.Enabled = False
            '                    btnAddServiceCall.Enabled = False
            '                    btnCancelServiceCall.Enabled = False
            '                    btnTargetAdd.Enabled = False
            '                    btnbtnTargetCancel.Enabled = False

            '                    btnAddRetention.Enabled = False
            '                    btnCancelRetention.Enabled = False
            '                    btnAirNonAirAdd.Enabled = False
            '                    btnAirNonAirCancel.Enabled = False
            '                End If
            '            End If
            '        Else
            '            btnSave.Enabled = False
            '            btnAddVisitDetails.Enabled = False
            '            btnCancelVisitDetails.Enabled = False
            '            btnAddServiceCall.Enabled = False
            '            btnCancelServiceCall.Enabled = False
            '            btnTargetAdd.Enabled = False
            '            btnbtnTargetCancel.Enabled = False

            '            btnAddRetention.Enabled = False
            '            btnCancelRetention.Enabled = False
            '            btnAirNonAirAdd.Enabled = False
            '            btnAirNonAirCancel.Enabled = False
            '        End If
            '    End If

            '    '@  End of Code for Modification of DSR Back Dated
            'Catch ex As Exception
            'End Try

            ''##################################################################################


            '######## Start of of Previous code ##########################################################################
            'Try
            '    ' After 5 days Visit Details can't be modify.
            '    Dim VisitDate As String = Request.QueryString("VisitDate")
            '    Dim StrDay As String = VisitDate.Trim.Substring(6, 2)
            '    Dim StrMonth As String = VisitDate.Trim.Substring(4, 2)
            '    Dim StrYear As String = VisitDate.Trim.Substring(0, 4)
            '    Dim DsrDate As New Date(StrYear, StrMonth, StrDay)
            '    Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
            '    Dim intdaysDiff As Long = DateDiff(DateInterval.Day, DsrDate, TodayDate)

            '    If (intdaysDiff >= 5 Or intdaysDiff < 0) Then
            '        btnSave.Enabled = False
            '        btnAddVisitDetails.Enabled = False
            '        btnCancelVisitDetails.Enabled = False
            '        btnAddServiceCall.Enabled = False
            '        btnCancelServiceCall.Enabled = False
            '        btnTargetAdd.Enabled = False
            '        btnbtnTargetCancel.Enabled = False

            '        btnAddRetention.Enabled = False
            '        btnCancelRetention.Enabled = False
            '        btnAirNonAirAdd.Enabled = False
            '        btnAirNonAirCancel.Enabled = False
            '    End If
            'Catch ex As Exception
            'End Try
            '#### End of Previous code##############################################################################
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region



#Region "@ Added By Abhishek"

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        LoadAssignedToByDepartmentId(dlstAssignedTo)
    End Sub
    Private Sub LoadAssignedToByDepartmentId(ByVal drpDownList As DropDownList)

        Dim objOutputXml As New XmlDocument
        Dim objInPutXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        drpDownList.Items.Clear()

        Dim ds As New DataSet

        Try
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            objOutputXml = New XmlDocument

            If ddlDepartment.SelectedValue <> "" Then
                objInPutXml.LoadXml("<MS_DEPT_EMPLOYEE_INPUT>' <DEPT></DEPT>'</MS_DEPT_EMPLOYEE_INPUT>")
                If ddlDepartment.SelectedValue.Trim.Length > 0 Then
                    objInPutXml.DocumentElement.SelectSingleNode("DEPT").InnerText = ddlDepartment.SelectedValue
                End If

                objOutputXml = objbzEmployee.GetEmployeeListDeptWise(objInPutXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            End If
            drpDownList.Items.Insert(0, New ListItem("", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub ChkAirNonAirShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkAirNonAirShowAll.CheckedChanged

        Dim objOutputXml As New XmlDocument
        If Session("VisitDetails") IsNot Nothing Then
            objOutputXml.LoadXml(Session("VisitDetails").ToString)
            If ChkAirNonAirShowAll.Checked = True Then
                BindGrid(objOutputXml, "DETAILAIR_OTHERALL")
            Else
                BindGrid(objOutputXml, "DETAILAIR_OTHER")
            End If
        End If



    End Sub

    Protected Sub ChkRetShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRetShowAll.CheckedChanged
        Dim objOutputXml As New XmlDocument
        If Session("VisitDetails") IsNot Nothing Then
            objOutputXml.LoadXml(Session("VisitDetails").ToString)
            If ChkRetShowAll.Checked = True Then
                BindGrid(objOutputXml, "DETAILRETENTIONALL")
            Else
                BindGrid(objOutputXml, "DETAILRETENTION")
            End If
        End If


    End Sub

    Protected Sub ChkTargetShowAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkTargetShowAll.CheckedChanged
        Dim objOutputXml As New XmlDocument
        If Session("VisitDetails") IsNot Nothing Then
            objOutputXml.LoadXml(Session("VisitDetails").ToString)
            If ChkTargetShowAll.Checked = True Then
                BindGrid(objOutputXml, "DETAILTARGETALL")
            Else
                BindGrid(objOutputXml, "DETAILTARGET")
            End If
        End If


    End Sub

    Private Sub BindAccByColleage(ByVal StrLocde As String, ByVal IsManagerLogged As String)
        If IsManagerLogged = "TRUE" Or IsManagerLogged = "1" Then
            LoadAccByColleageForManager(DrpAccByCollegeus)
        Else
            objeAAMS.BindDropDown(DrpAccByCollegeus, "EMPLOYEE", True, 1)
        End If


    End Sub

    Private Function DuplicateTarget(ByVal objCurrentnode As XmlNode)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument
        Dim TotAttributes As Integer = 0
        Try
            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET")
                If objnode.Attributes("A1APPROVED_NEW_DEAL").Value.Trim.ToUpper = objCurrentnode.Attributes("A1APPROVED_NEW_DEAL").Value.Trim.ToUpper Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("CPS").Value.Trim.ToUpper = objCurrentnode.Attributes("CPS").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVT_STATUSID").Value.Trim.ToUpper = objCurrentnode.Attributes("SVT_STATUSID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVT_STATUS_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("SVT_STATUS_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("STT_SIGNON_DATE").Value.Trim.ToUpper = objCurrentnode.Attributes("STT_SIGNON_DATE").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("STT_TARGET_SEG").Value.Trim.ToUpper = objCurrentnode.Attributes("STT_TARGET_SEG").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("DEPARTMENTID") IsNot Nothing AndAlso objCurrentnode.Attributes("DEPARTMENTID") IsNot Nothing Then
                    If objnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing AndAlso objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                    If objnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper = objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If

                If objnode.Attributes("CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If



                If blnFound = True Then
                    Exit For
                End If
            Next

        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function

    Private Function DuplicateRetention(ByVal objCurrentnode As XmlNode)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument
        Dim TotAttributes As Integer = 0
        Try


          
            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION")

                If objnode.Attributes("CPS").Value.Trim.ToUpper = objCurrentnode.Attributes("CPS").Value.Trim.ToUpper Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVR_REASONID").Value.Trim.ToUpper = objCurrentnode.Attributes("SVR_REASONID").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVR_REASON_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("SVR_REASON_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVR_STATUSID").Value.Trim.ToUpper = objCurrentnode.Attributes("SVR_STATUSID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SVR_STATUS_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("SVR_STATUS_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("NEWCPS").Value.Trim.ToUpper = objCurrentnode.Attributes("NEWCPS").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("STR_SIGNON_DATE").Value.Trim.ToUpper = objCurrentnode.Attributes("STR_SIGNON_DATE").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("STR_TARGET_SEG").Value.Trim.ToUpper = objCurrentnode.Attributes("STR_TARGET_SEG").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("DEPARTMENTID") IsNot Nothing AndAlso objCurrentnode.Attributes("DEPARTMENTID") IsNot Nothing Then
                    If objnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing AndAlso objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                    If objnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper = objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If

                If objnode.Attributes("CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If



                If blnFound = True Then
                    Exit For
                End If
            Next

        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function
    Private Function DuplicateAirNonAir(ByVal objCurrentnode As XmlNode)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument
        Dim TotAttributes As Integer = 0
        Try

            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER")

                If objnode.Attributes("PRODUCTID").Value.Trim.ToUpper = objCurrentnode.Attributes("PRODUCTID").Value.Trim.ToUpper Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("PRODUCT_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("PRODUCT_NAME").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("REVENUE").Value.Trim.ToUpper = objCurrentnode.Attributes("REVENUE").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SV_STATUSID").Value.Trim.ToUpper = objCurrentnode.Attributes("SV_STATUSID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SV_STATUS_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("SV_STATUS_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("STA_SIGNON_DATE").Value.Trim.ToUpper = objCurrentnode.Attributes("STA_SIGNON_DATE").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("DEPARTMENTID") IsNot Nothing AndAlso objCurrentnode.Attributes("DEPARTMENTID") IsNot Nothing Then
                    If objnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing AndAlso objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID") IsNot Nothing Then
                    If objnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper = objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If

                If objnode.Attributes("CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If


                If objnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing AndAlso objCurrentnode.Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                    If objnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                End If



                If blnFound = True Then
                    Exit For
                End If
            Next

        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function

    Private Function DuplicateServiceCall(ByVal objCurrentnode As XmlNode, ByVal Mode As String)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument
        Dim TotAttributes As Integer = 0
        Try

            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)
            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("SERVICECALLS/SERVICECALL")

                If objnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENTID").Value.Trim.ToUpper Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("DEPARTMENT_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENT_NAME").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("DEPARTMENT_SPECIFIC").Value.Trim.ToUpper = objCurrentnode.Attributes("DEPARTMENT_SPECIFIC").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SC_DISCUSSIONISSUE_REMARKS").Value.Trim.ToUpper = objCurrentnode.Attributes("SC_DISCUSSIONISSUE_REMARKS").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SC_STATUSID").Value.Trim.ToUpper = objCurrentnode.Attributes("SC_STATUSID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("SC_STATUSID_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("SC_STATUSID_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper = objCurrentnode.Attributes("ASSIGNTO_EMPLOYEEID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If objnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper = objCurrentnode.Attributes("TARGET_CLOSER_DATETIME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If Mode.Trim.ToUpper = "UPDATE" Then
                    If blnFound = True Then
                        If objnode.Attributes("SEQUENCENO").Value.Trim.ToUpper <> objCurrentnode.Attributes("SEQUENCENO").Value.Trim.ToUpper Then
                            ' blnFound = False
                        Else
                            blnFound = False
                            Exit For
                        End If
                    End If
                Else
                    If blnFound = True Then
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function
    Private Function DuplicateVisitDetails(ByVal objCurrentnode As XmlNode, ByVal Mode As String)
        Dim blnFound As Boolean = False
        Dim objAddXmlDoc As New XmlDocument

        Try

            objAddXmlDoc.LoadXml(Session("VisitDetails").ToString)

            For Each objnode As XmlNode In objAddXmlDoc.DocumentElement.SelectNodes("VISITDETAIL/DETAIL")

                If objnode.Attributes("MANAGERID").Value.Trim.ToUpper = objCurrentnode.Attributes("MANAGERID").Value.Trim.ToUpper Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("MANAGER_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("MANAGER_NAME").Value.Trim.ToUpper.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("IMMEDIATE_MANAGERID").Value.Trim.ToUpper = objCurrentnode.Attributes("IMMEDIATE_MANAGERID").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("IMMEDIATE_MANAGERNAME").Value.Trim.ToUpper = objCurrentnode.Attributes("IMMEDIATE_MANAGERNAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                If objnode.Attributes("CONTACT_NAME").Value.Trim.ToUpper = objCurrentnode.Attributes("CONTACT_NAME").Value.Trim.ToUpper And blnFound = True Then
                    blnFound = True
                Else
                    blnFound = False
                End If
            

                If Mode.Trim.ToUpper = "UPDATE" Then
                    If blnFound = True Then
                        If objnode.Attributes("SEQUENCENO").Value.Trim.ToUpper <> objCurrentnode.Attributes("SEQUENCENO").Value.Trim.ToUpper Then
                            ' blnFound = False
                        Else
                            blnFound = False
                            Exit For
                        End If
                    End If
                Else
                    If blnFound = True Then
                        Exit For
                    End If
                End If
            Next

        Catch ex As Exception
            Return blnFound
        End Try
        Return blnFound
    End Function

    Private Sub LoadAirNonAirAssignedToByDepartmentId(ByVal drpDownList As DropDownList)

        Dim objOutputXml As New XmlDocument
        Dim objInPutXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        drpDownList.Items.Clear()

        Dim ds As New DataSet

        Try
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            objOutputXml = New XmlDocument

            If DrpAirNonAirDept.SelectedValue <> "" Then
                objInPutXml.LoadXml("<MS_DEPT_EMPLOYEE_INPUT>' <DEPT></DEPT>'</MS_DEPT_EMPLOYEE_INPUT>")
                If DrpAirNonAirDept.SelectedValue.Trim.Length > 0 Then
                    objInPutXml.DocumentElement.SelectSingleNode("DEPT").InnerText = DrpAirNonAirDept.SelectedValue
                End If

                objOutputXml = objbzEmployee.GetEmployeeListDeptWise(objInPutXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            End If
            drpDownList.Items.Insert(0, New ListItem("", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadTagetAssignedToByDepartmentId(ByVal drpDownList As DropDownList)

        Dim objOutputXml As New XmlDocument
        Dim objInPutXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        drpDownList.Items.Clear()

        Dim ds As New DataSet

        Try
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            objOutputXml = New XmlDocument

            If DrpTargetDept.SelectedValue <> "" Then
                objInPutXml.LoadXml("<MS_DEPT_EMPLOYEE_INPUT>' <DEPT></DEPT>'</MS_DEPT_EMPLOYEE_INPUT>")
                If DrpTargetDept.SelectedValue.Trim.Length > 0 Then
                    objInPutXml.DocumentElement.SelectSingleNode("DEPT").InnerText = DrpTargetDept.SelectedValue
                End If

                objOutputXml = objbzEmployee.GetEmployeeListDeptWise(objInPutXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            End If
            drpDownList.Items.Insert(0, New ListItem("", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadRetentionAssignedToByDepartmentId(ByVal drpDownList As DropDownList)

        Dim objOutputXml As New XmlDocument
        Dim objInPutXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        drpDownList.Items.Clear()

        Dim ds As New DataSet

        Try
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            objOutputXml = New XmlDocument

            If DrpRetDept.SelectedValue <> "" Then
                objInPutXml.LoadXml("<MS_DEPT_EMPLOYEE_INPUT>' <DEPT></DEPT>'</MS_DEPT_EMPLOYEE_INPUT>")
                If DrpRetDept.SelectedValue.Trim.Length > 0 Then
                    objInPutXml.DocumentElement.SelectSingleNode("DEPT").InnerText = DrpRetDept.SelectedValue
                End If

                objOutputXml = objbzEmployee.GetEmployeeListDeptWise(objInPutXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "Employee_Name"
                    drpDownList.DataValueField = "EmployeeID"
                    drpDownList.DataBind()
                End If
            End If
            drpDownList.Items.Insert(0, New ListItem("", ""))
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub DrpAirNonAirDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpAirNonAirDept.SelectedIndexChanged
        Try
            LoadAirNonAirAssignedToByDepartmentId(DrpAirNonAirAssignedTo)

            If ddlAirNonAirStatus.SelectedValue.Split("|").GetValue(0).ToString = hdAirNonAirChk.Value.Trim Then
                txtAirNonAirSignedOn.ReadOnly = False
                txtAirNonAirSignedOn.CssClass = "textbox"
                imgAirNonAirSignedOn.Style.Add("display", "block")
            Else
                txtAirNonAirSignedOn.ReadOnly = True
                txtAirNonAirSignedOn.CssClass = "textboxgrey"
                imgAirNonAirSignedOn.Style.Add("display", "none")
            End If

            If (ddlAirNonAirStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                imgAirCloserDate.Style.Add("display", "none")
                txtAirCloserDate.CssClass = "textboxgrey"
                txtAirCloserDate.ReadOnly = True
            Else
                imgAirCloserDate.Style.Add("display", "block")
                txtAirCloserDate.CssClass = "textbox"
                txtAirCloserDate.ReadOnly = False
            End If


        Catch ex As Exception

        End Try

    End Sub
    Protected Sub DrpRetDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpRetDept.SelectedIndexChanged
        Try
            LoadRetentionAssignedToByDepartmentId(DrpRetAssignedTo)

            If ddlRetentionStatus.SelectedValue.Split("|").GetValue(0).ToString = hdRetentionChk.Value.Trim Then
                txtRetentionSignedOn.ReadOnly = False
                txtRetentionSignedOn.CssClass = "textbox"
                imgRetentionSignedOn.Style.Add("display", "block")
            Else
                txtRetentionSignedOn.ReadOnly = True
                txtRetentionSignedOn.CssClass = "textboxgrey"
                imgRetentionSignedOn.Style.Add("display", "none")
            End If


            If (ddlRetentionStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                imgRetCloserDate.Style.Add("display", "none")
                txtRetCloserDate.CssClass = "textboxgrey"
                txtRetCloserDate.ReadOnly = True
            Else
                imgRetCloserDate.Style.Add("display", "block")
                txtRetCloserDate.CssClass = "textbox"
                txtRetCloserDate.ReadOnly = False
            End If
        Catch ex As Exception

        End Try

       

    End Sub

    Protected Sub DrpTargetDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpTargetDept.SelectedIndexChanged
        Try
            LoadTagetAssignedToByDepartmentId(DrpTargetAssignedTo)


            If ddlTargetStatus.SelectedValue.Split("|").GetValue(0).ToString.Trim = hdTargetChk.Value.Trim Then
                txtTargetSignedOn.ReadOnly = False
                txtTargetSignedOn.CssClass = "textbox"
                imgTargetSignedOn.Style.Add("display", "block")
            Else
                txtTargetSignedOn.ReadOnly = True
                txtTargetSignedOn.CssClass = "textboxgrey"
                imgTargetSignedOn.Style.Add("display", "none")
            End If


            If (ddlTargetStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                imgTarCloserDate.Style.Add("display", "none")
                txtTarCloserDate.CssClass = "textboxgrey"
                txtTarCloserDate.ReadOnly = True
            Else
                imgTarCloserDate.Style.Add("display", "block")
                txtTarCloserDate.CssClass = "textbox"
                txtTarCloserDate.ReadOnly = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadAccByColleageForManager(ByVal drpDownList As DropDownList)
        Dim li As ListItem
        Try
            DrpAccByCollegeus.Items.Clear()
            objeAAMS.BindDropDown(DrpAccByCollegeus, "EMPLOYEE", True, 1)

            If hdRes1A.Value.Trim.Length > 0 Then
                For i As Integer = 0 To hdRes1A.Value.Trim.Split(",").Length - 1
                    If hdRes1A.Value.Trim.Split(",")(i).Trim.Length > 0 Then
                        li = New ListItem
                        li = DrpAccByCollegeus.Items.FindByValue(hdRes1A.Value.Trim.Split(",")(i).ToString)
                        If li IsNot Nothing Then
                            DrpAccByCollegeus.Items.Remove(li)
                        End If
                    End If
                Next

            End If
            'hdRes1A.Value
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

  

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Try
            If Session("Security") Is Nothing Then
                Response.Redirect("~/SupportPages/TimeOutSession.aspx?Logout=True", False)
                'ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession(), True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Rating_Script_" + Me.ClientID.ToString(), objeAAMS.CheckSession(), True)
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnUpdateIata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdateIata.Click

        Dim objInputXml, objOutputXml, objDSROutputXml As New XmlDocument
        Dim objbzSalesIATAID As New AAMS.bizSales.bzSalesIATAID
        Try
            If Session("Security") Is Nothing Then
                LblUpdateIataError.Text = "Session is expired."
                Exit Sub
            End If

            If Not DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "N" Then
                If TxtIataID.Text.Trim.Length = 0 Then
                    LblUpdateIataError.Text = "IATA ID is Mandatory."
                    Exit Sub
                End If
                If TxtIataID.Text.Trim.Length < 8 Then
                    LblUpdateIataError.Text = "IATA ID is not valid."
                    Exit Sub
                End If
            End If
          

            If DlstIataStatus.SelectedValue = "" Then
                LblUpdateIataError.Text = "IATA Status is Mandatory."
                Exit Sub
            End If

            If DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "S" Then ' In cae of Secontry
                If TxtIataOfficeID.Text.Trim.Length = 0 Then
                    LblUpdateIataError.Text = "IATA OfficeID is Mandatory."
                    Exit Sub
                End If
            End If

            objInputXml.LoadXml("<TA_UPDATE_IATA_INPUT>  <IATA LCODE='' IATA_ID='' IATASTATUSID='' IATASTATUSNAME='' IATAOFFICEID=''  EMPLOYEEID=''  />   </TA_UPDATE_IATA_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("IATA")
                .Attributes("LCODE").Value = txtLcode.Text
                .Attributes("IATA_ID").Value = TxtIataID.Text
                .Attributes("IATAOFFICEID").Value = TxtIataOfficeID.Text
                If DlstIataStatus.SelectedValue <> "" Then
                    .Attributes("IATASTATUSID").Value = DlstIataStatus.SelectedValue
                    .Attributes("IATASTATUSNAME").Value = DlstIataStatus.SelectedItem.Text
                End If

                .Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))

                If DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "N" Then
                    .Attributes("IATA_ID").Value = ""
                    .Attributes("IATAOFFICEID").Value = ""
                End If
            End With

            objOutputXml = objbzSalesIATAID.Update(objInputXml)
            Try
                objInputXml.Save("c:\admin\IataUpdateInput.xml")
                objOutputXml.Save("c:\admin\IataUpdateOutput.xml")
            Catch ex As Exception

            End Try

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                LblUpdateIataError.Text = "IATA is updated successfully."
                If Session("VisitDetails") IsNot Nothing Then
                    objDSROutputXml.LoadXml(Session("VisitDetails").ToString())
                    With objDSROutputXml.DocumentElement.SelectSingleNode("AGENCY")
                        .Attributes("IATA_ID").Value = objOutputXml.DocumentElement.SelectSingleNode("IATA").Attributes("IATA_ID").Value
                        .Attributes("IATASTATUSID").Value = objOutputXml.DocumentElement.SelectSingleNode("IATA").Attributes("IATASTATUSID").Value

                        If objOutputXml.DocumentElement.SelectSingleNode("IATA").Attributes("IATAOFFICEID") IsNot Nothing Then
                            .Attributes("IATAOFFICEID").Value = objOutputXml.DocumentElement.SelectSingleNode("IATA").Attributes("IATAOFFICEID").Value
                        End If

                        TxtIataID.Text = .Attributes("IATA_ID").Value
                        TxtIataOfficeID.Text = .Attributes("IATAOFFICEID").Value

                    End With
                End If
            Else
                LblUpdateIataError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            LblUpdateIataError.Text = ex.Message
        Finally
            ModalLoading.Hide()
        End Try

    End Sub

    Protected Sub DlstIataStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DlstIataStatus.SelectedIndexChanged
        Try
            If DlstIataStatus.SelectedValue <> "" Then
                CallByIataSatus()
            End If
        Catch ex As Exception
        Finally
            ModalLoading.Hide()
        End Try
       
    End Sub
    Private Sub CallByIataSatus()
        Dim objInputXml, objOutputXml, objTempOutputXml As New XmlDocument
        Dim objbzSalesIATAID As New AAMS.bizSales.bzSalesIATAID
        TxtIataID.CssClass = "textbox"
        TxtIataID.ReadOnly = False
        Try
           

            If DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "P" Then  ' In case of Primary
                'If Session("VisitDetails") IsNot Nothing Then
                '    objTempOutputXml.LoadXml(Session("VisitDetails"))
                '    TxtIataID.Text = objTempOutputXml.DocumentElement.SelectSingleNode("AGENCY").Attributes("IATA_ID").Value
                '    TxtIataOfficeID.Text = objTempOutputXml.DocumentElement.SelectSingleNode("AGENCY").Attributes("IATAOFFICEID").Value
                'End If
            ElseIf DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "S" Then  ' In case of Secondry

                If TxtIataID.Text.Trim.Length = 0 Then
                    LblUpdateIataError.Text = "IATA ID is Mandatory."
                    Exit Sub
                End If
                If TxtIataID.Text.Trim.Length < 8 Then
                    LblUpdateIataError.Text = "IATA ID is not valid."
                    Exit Sub
                End If

                If DlstIataStatus.SelectedValue = "" Then
                    LblUpdateIataError.Text = "IATA Status is Mandatory."
                    Exit Sub
                End If

                'If DlstIataStatus.SelectedValue.Trim.Split("|")(1).Trim.ToUpper = "S" Then ' In cae of Secontry
                '    'If TxtIataOfficeID.Text.Trim.Length = 0 Then
                '    '    LblUpdateIataError.Text = "IATA OfficeID is Mandatory."
                '    '    Exit Sub
                '    'End If
                'End If

                objInputXml.LoadXml("<TA_VIEW_IATA_INPUT>  <IATA LCODE='' IATA_ID='' IATASTATUSID='' IATASTATUSNAME='' />   </TA_VIEW_IATA_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("IATA")
                    .Attributes("LCODE").Value = txtLcode.Text
                    .Attributes("IATA_ID").Value = TxtIataID.Text
                    If DlstIataStatus.SelectedValue <> "" Then
                        .Attributes("IATASTATUSID").Value = DlstIataStatus.SelectedValue
                        .Attributes("IATASTATUSNAME").Value = DlstIataStatus.SelectedItem.Text
                    End If
                End With

                objOutputXml = objbzSalesIATAID.View(objInputXml)
                ' objOutputXml.LoadXml("<TA_VIEWIATA_OUTPUT>  <IATA LCODE='' IATA_ID='' IATASTATUSID='' IATASTATUSNAME='' IATAOFFICEID='' />  <Errors Status='TRUE'>  <Error Code='' Description='Not Getting.' />   </Errors>  </TA_VIEWIATA_OUTPUT>")
                Try
                    objInputXml.Save("c:\admin\IataViewInput.xml")
                    objOutputXml.Save("c:\admin\IataViewOutput.xml")
                Catch ex As Exception

                End Try

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("IATA")
                        TxtIataOfficeID.Text = .Attributes("IATAOFFICEID").Value
                    End With
                Else
                    LblUpdateIataError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else  ' In case of Non iata
                TxtIataOfficeID.Text = ""
                TxtIataID.Text = ""
                ' TxtIataID.ReadOnly = True
                'TxtIataID.CssClass = "textboxgrey"
            End If
        Catch ex As Exception
            LblUpdateIataError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnResetIata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnResetIata.Click
        Dim objTempOutputXml As New XmlDocument
        Try
            If Session("VisitDetails") IsNot Nothing Then
                objTempOutputXml.LoadXml(Session("VisitDetails"))
                With objTempOutputXml.DocumentElement.SelectSingleNode("AGENCY")
                    TxtIataID.Text = .Attributes("IATA_ID").Value
                    TxtIataOfficeID.Text = .Attributes("IATAOFFICEID").Value
                    If .Attributes("IATASTATUSID") IsNot Nothing Then
                        Dim li As New ListItem
                        li = DlstIataStatus.Items.FindByValue(.Attributes("IATASTATUSID").Value)
                        If li IsNot Nothing Then
                            DlstIataStatus.SelectedValue = li.Value
                        Else
                            DlstIataStatus.SelectedValue = ""
                        End If
                        'TxtIataID.Text = .Attributes("").Value
                    End If
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
