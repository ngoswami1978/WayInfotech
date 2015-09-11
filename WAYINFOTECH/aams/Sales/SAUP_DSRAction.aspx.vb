
Partial Class Sales_SAUP_DSRAction
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strDSRDate As String = ""
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try
            Session("PageName") = Request.Url.ToString()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not IsPostBack Then
                Session("ActionDetails") = Nothing
                If Request.QueryString("VisitDate") IsNot Nothing Then
                    strDSRDate = Request.QueryString("VisitDate").ToString
                    hdVisitDATE.Value = strDSRDate
                    hdSCVisitDATE.Value = strDSRDate
                    hdTarVisitDATE.Value = strDSRDate
                    hdAirVisitDATE.Value = strDSRDate
                    hdRetVisitDATE.Value = strDSRDate

                End If

                If Request.QueryString("Lcode") IsNot Nothing Then
                    hdLCode.Value = Request.QueryString("Lcode").ToString
                End If
                ' lblHeading.Text = "DSR Details" + Space(1) + strDSRDate.Substring(6, 2) + "-" + MonthName(strDSRDate.Substring(4, 2)) + "-" + strDSRDate.Substring(0, 4)
                If Request.QueryString("DSRCODE") IsNot Nothing And Request.QueryString("DSRCODE").ToString.Trim.Length > 0 Then
                    hdDSRCODE.Value = Request.QueryString("DSRCODE").ToString
                    hdSCDSRCODE.Value = hdDSRCODE.Value
                    hdTarDSRCODE.Value = hdDSRCODE.Value
                    hdAirDSRCODE.Value = hdDSRCODE.Value
                    hdRetDSRCODE.Value = hdDSRCODE.Value

                End If
                If Request.QueryString("DSR_DETAILID") IsNot Nothing AndAlso Request.QueryString("DSR_DETAILID").ToString.Trim.Length > 0 Then
                    hdDSR_DETAIL_ID.Value = Request.QueryString("DSR_DETAILID").ToString
                    hdDSR_SC_DETAIL_ID.Value = hdDSR_DETAIL_ID.Value
                    hdDSR_STA_DETAIL_ID.Value = hdDSR_DETAIL_ID.Value
                    hdDSR_STR_DETAIL_ID.Value = hdDSR_DETAIL_ID.Value
                    hdDSR_STT_DETAIL_ID.Value = hdDSR_DETAIL_ID.Value

                End If
                If Request.QueryString("VISITSUBTYPE") IsNot Nothing AndAlso Request.QueryString("VISITSUBTYPE").ToString.Trim.Length > 0 Then

                    If Request.QueryString("VISITSUBTYPE").ToString.Trim.ToUpper.Contains("SERVICE") Then
                        HdVISITSUBTYPE.Value = 1
                    ElseIf Request.QueryString("VISITSUBTYPE").ToString.Trim.ToUpper.Contains("STRATEGIC") Then
                        HdVISITSUBTYPE.Value = 2
                    End If

                End If
                If Request.QueryString("STRATEGICTYPE") IsNot Nothing AndAlso Request.QueryString("STRATEGICTYPE").ToString.Trim.Length > 0 Then
                    '  HdSTRATEGICTYPE.Value = Request.QueryString("STRATEGICTYPE").ToString
                    If Request.QueryString("STRATEGICTYPE").ToString.Trim.ToUpper.Contains("TARGET") Then
                        HdSTRATEGICTYPE.Value = 1
                    ElseIf Request.QueryString("STRATEGICTYPE").ToString.Trim.ToUpper.Contains("RETENTION") Then
                        HdSTRATEGICTYPE.Value = 2
                    ElseIf Request.QueryString("STRATEGICTYPE").ToString.Trim.ToUpper.Contains("AIR") Then
                        HdSTRATEGICTYPE.Value = 3
                    End If
                End If
                btnUpdateAirNonAir.Enabled = False
                btnUpdateRetention.Enabled = False
                btnUpdateServiceCall.Enabled = False
                btnUpdateTarget.Enabled = False

                ViewDetails()
            End If

            ' ddlServiceStatus.Attributes.Add("onchange", "return CallAssignedOnOrOff();")
            ddlServiceStatus.Attributes.Add("OnChange", "return CallAssignedOnOrOff('ddlServiceStatus');")

            ddlAirNonAirStatus.Attributes.Add("onchange", "return CallAirNonAirForsignDate('ddlAirNonAirStatus');")
            ddlRetentionStatus.Attributes.Add("onchange", "return SignedOnDateByRetStatus('ddlRetentionStatus');")
            ddlTargetStatus.Attributes.Add("onchange", "return SignedOnDateByTargetStatus('ddlTargetStatus');")

            EnableDisableControls()
            ' Checking security.
            CheckSecurity()

            '@Start Service Calls
            txtDepttSpecific.Text = Request.Form("txtDepttSpecific")
            txtDetailedDiscussion.Text = Request.Form("txtDetailedDiscussion")
            txtTargetCloserDate.Text = Request.Form("txtTargetCloserDate")
            txtCloserDate.Text = Request.Form("txtCloserDate")
            '@ End Service Calls

            '@ Start AirNonAir
            txtAirNonAirSignedOn.Text = Request.Form("txtAirNonAirSignedOn")
            txtRevenue.Text = Request.Form("txtRevenue")
            txtAirCloserDate.Text = Request.Form("txtAirCloserDate")
            '@ End AirNonAir



            '@ Start Retention
            txtRetentionSignedOn.Text = Request.Form("txtRetentionSignedOn")
            txtCPS.Text = Request.Form("txtCPS")
            txtRetCloserDate.Text = Request.Form("txtRetCloserDate")

            '@ End Retention

            '@ Start Target
            txtTargetSignedOn.Text = Request.Form("txtTargetSignedOn")
            txtTarCloserDate.Text = Request.Form("txtTarCloserDate")
            '@ End Target


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DSR ACTION']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        ' btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        '
                        ' btnSave.Enabled = False
                    End If
                    If strBuilder(2) = "0" And (hdDSRCODE.Value <> "" Or Request.QueryString("DSRVistedId") IsNot Nothing) Then
                        ' btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        '  btnSave.Enabled = True
                    End If
                Else
                    Response.Redirect("../NoRights.aspx")
                End If
               
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "ViewDetails(ByVal blnUpdate As Boolean)"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Try
            objInputXml.LoadXml("<SL_VIEW_DSR_ACTIONDETAILS_INPUT>	<DSRCODE></DSRCODE> <DSR_DETAILS_ID> </DSR_DETAILS_ID><VISITSUBTYPE></VISITSUBTYPE> <STRATEGICTYPE></STRATEGICTYPE><EMPLOYEEID></EMPLOYEEID></SL_VIEW_DSR_ACTIONDETAILS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("DSRCODE").InnerText = hdDSRCODE.Value
            objInputXml.DocumentElement.SelectSingleNode("DSR_DETAILS_ID").InnerText = hdDSR_DETAIL_ID.Value
            objInputXml.DocumentElement.SelectSingleNode("VISITSUBTYPE").InnerText = HdVISITSUBTYPE.Value
            objInputXml.DocumentElement.SelectSingleNode("STRATEGICTYPE").InnerText = HdSTRATEGICTYPE.Value

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId

            '   objOutputXml.Load("c:\SL_VIEW_DSR_ACTIONDETAILS_OUTPUT.xml")

            objOutputXml = objbzDSRAction.View(objInputXml)
            Try
                objInputXml.Save("c:\admin\DSRActionViewInput.xml")
                objOutputXml.Save("c:\admin\DSRActionViewOutput.xml")
            Catch ex As Exception
            End Try

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ActionDetails") = objOutputXml.OuterXml
                hdDSRCODE.Value = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSRCODE").Value
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

                '  lblHeading.Text = "DSR Details" + Space(1) + System.DateTime.Today.ToString("dd-MMM-yyyy")
                If objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSRCODE").Value.Trim.Length > 0 Then
                    hdDSRCODE.Value = objOutputXml.DocumentElement.SelectSingleNode("DSR").Attributes("DSRCODE").Value.Trim
                    txtDSRCode.Text = hdDSRCODE.Value
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
                End With

                If HdVISITSUBTYPE.Value.Trim = "1" Then

                    objeAAMS.BindDropDown(ddlDepartment, "DepartmentName", True, 1)
                    'objeAAMS.BindDropDown(ddlDepttSpecific, "PRODUCTS", True, 1)
                    objeAAMS.BindDropDown(ddlDepttSpecific, "DEPARTMENTSPECIFIC", True, 1)
                    LoadAssignedToByDepartmentId(dlstAssignedTo)
                    objeAAMS.BindDropDown(ddlServiceStatus, "ServiceCallStatus", True, 1)
                    BindGrid(objOutputXml, "SERVICECALL")
                    btnUpdateServiceCall.Enabled = False

                ElseIf HdVISITSUBTYPE.Value.Trim = "2" Then
                    If HdSTRATEGICTYPE.Value.Trim = "1" Then
                        objeAAMS.BindDropDown(DrpTargetDept, "DepartmentName", True, 1)
                        LoadTagetAssignedToByDepartmentId(DrpTargetAssignedTo)
                        objeAAMS.BindDropDown(ddlTargetStatus, "TargetStatus", True, 1)
                        BindGrid(objOutputXml, "DETAILTARGETALL")
                        btnUpdateRetention.Enabled = False
                    ElseIf HdSTRATEGICTYPE.Value.Trim = "2" Then
                        objeAAMS.BindDropDown(DrpRetDept, "DepartmentName", True, 1)
                        LoadRetentionAssignedToByDepartmentId(DrpRetAssignedTo)
                        objeAAMS.BindDropDown(ddlRetentionReason, "RetentionReason", True, 1)
                        objeAAMS.BindDropDown(ddlRetentionStatus, "RetentionStatus", True, 1)
                        BindGrid(objOutputXml, "DETAILRETENTIONALL")
                        btnUpdateTarget.Enabled = False
                    ElseIf HdSTRATEGICTYPE.Value.Trim = "3" Then
                        objeAAMS.BindDropDown(ddlProductName, "PRODUCTS", True, 1)
                        objeAAMS.BindDropDown(DrpAirNonAirDept, "DepartmentName", True, 1)
                        LoadAirNonAirAssignedToByDepartmentId(DrpAirNonAirAssignedTo)
                        objeAAMS.BindDropDown(ddlAirNonAirStatus, "AirNonAirOTHStatus", True, 1)
                        BindGrid(objOutputXml, "DETAILAIR_OTHERALL")
                        btnUpdateAirNonAir.Enabled = False
                    End If
                Else

                    lblError.Text = "Invalid Parameter"
                End If
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
#Region "EnableDisableControls()::Enable/Disable Controls On The Basis Of Service Call Or Strtegic Call"
    Private Sub EnableDisableControls()
        Try
          
            If HdVISITSUBTYPE.Value.Trim = "1" Then
                pnlServiceCall.Visible = True
                pnlStrategicVisits.Visible = False
                pnlTarget.Visible = False
                pnlAirNonAir.Visible = False
                pnlRetention.Visible = False
            ElseIf HdVISITSUBTYPE.Value.Trim = "2" Then
                If HdSTRATEGICTYPE.Value.Trim = "1" Then
                    pnlServiceCall.Visible = False
                    pnlStrategicVisits.Visible = True
                    pnlTarget.Visible = True
                    pnlRetention.Visible = False
                    pnlAirNonAir.Visible = False
                ElseIf HdSTRATEGICTYPE.Value.Trim = "2" Then
                    pnlServiceCall.Visible = False
                    pnlStrategicVisits.Visible = True
                    pnlTarget.Visible = False
                    pnlRetention.Visible = True
                    pnlAirNonAir.Visible = False
                ElseIf HdSTRATEGICTYPE.Value.Trim = "3" Then
                    pnlServiceCall.Visible = False
                    pnlStrategicVisits.Visible = True
                    pnlTarget.Visible = False
                    pnlRetention.Visible = False
                    pnlAirNonAir.Visible = True
                End If
            Else
                pnlServiceCall.Visible = False
                pnlStrategicVisits.Visible = False
                pnlTarget.Visible = False
                pnlAirNonAir.Visible = False
                pnlRetention.Visible = False
                lblError.Text = "Invalid Parameter"
            End If


            If ddlDepartment.Items.Count > 0 Then
                If ddlDepartment.SelectedItem.Text.Trim.ToUpper = "PRODUCT" Then
                    ddlDepttSpecific.Visible = True
                    txtDepttSpecific.Visible = False
                Else
                    ddlDepttSpecific.Visible = False
                    txtDepttSpecific.Visible = True
                End If
            Else
                ddlDepttSpecific.Visible = True
                txtDepttSpecific.Visible = False
            End If

          
        Catch ex As Exception
            lblError.Text = ex.Message
            'Throw ex
        End Try
    End Sub


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

#Region "BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)"
    Private Sub BindGrid(ByVal objViewXml As XmlDocument, ByVal strGridType As String)
        Dim objXmlReader As XmlNodeReader
        Dim objDS As New DataSet
        Dim objDV As DataView
        Try
            objXmlReader = New XmlNodeReader(objViewXml)
            objDS.ReadXml(objXmlReader)
            Select Case strGridType

                Case "SERVICECALL"
                    objDV = objDS.Tables("SERVICECALL").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvServiceCall.DataSource = objDV
                    Else
                        gvServiceCall.DataSource = Nothing
                    End If
                    gvServiceCall.DataBind()
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
                Case "DETAILTARGETALL"
                    objDV = objDS.Tables("DETAILTARGET").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvTarget.DataSource = objDV
                    Else
                        gvTarget.DataSource = Nothing
                    End If
                    gvTarget.DataBind()

                Case "DETAILAIR_OTHERALL"
                    objDV = objDS.Tables("DETAILAIR_OTHER").DefaultView
                    objDV.RowFilter = "SEQUENCENO <> ''"
                    objDV.Sort = "SEQUENCENO"
                    If objDV.Count > 0 Then
                        gvAirNonAir.DataSource = objDV
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


    Private Sub BindDefaultCallDetails()

        Dim objOutputXml As New XmlDocument
        Dim objDetailsNode As XmlNode
        Try
            If Session("ActionDetails") IsNot Nothing Then
                objOutputXml.LoadXml(Session("ActionDetails"))
            End If

            With objOutputXml
                If HdVISITSUBTYPE.Value.Trim = "1" Then

                    objDetailsNode = objOutputXml.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@DSRCODE='" + hdSCDSRCODE.Value + "' and @DSR_SC_DETAIL_ID='" + hdDSR_SC_DETAIL_ID.Value + "']")
                    If objDetailsNode IsNot Nothing Then
                        With objDetailsNode

                            ddlDepartment.SelectedValue = .Attributes("DEPARTMENTID").Value
                            ddlServiceStatus.SelectedValue = .Attributes("SC_STATUSID").Value
                            txtDetailedDiscussion.Text = .Attributes("SC_DISCUSSIONISSUE_REMARKS").Value

                            LoadAssignedToByDepartmentId(dlstAssignedTo)
                            Dim li As New ListItem
                            li = dlstAssignedTo.Items.FindByValue(.Attributes("ASSIGNTO_EMPLOYEEID").Value)
                            If li IsNot Nothing Then
                                dlstAssignedTo.SelectedValue = li.Value
                            End If

                            hdAssingedTo.Value = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                            txtCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)

                            txtTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)

                            If .Attributes("DEPARTMENT_NAME").Value.Trim.ToUpper = "PRODUCT" Then
                                ddlDepttSpecific.Visible = True
                                If txtDepttSpecific.Visible = True Then
                                    txtDepttSpecific.Visible = False
                                End If
                                ddlDepttSpecific.SelectedValue = ddlDepttSpecific.Items.FindByText(.Attributes("DEPARTMENT_SPECIFIC").Value.Trim).Value
                            Else
                                txtDepttSpecific.Text = .Attributes("DEPARTMENT_SPECIFIC").Value
                                txtDepttSpecific.Visible = True
                                If ddlDepttSpecific.Visible = True Then
                                    ddlDepttSpecific.Visible = False
                                End If
                            End If

                            If (ddlServiceStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                                imgCloserDate.Style.Add("display", "none")
                                txtCloserDate.CssClass = "textboxgrey"
                                txtCloserDate.ReadOnly = True
                            Else
                                imgCloserDate.Style.Add("display", "block")
                                txtCloserDate.CssClass = "textbox"
                                txtCloserDate.ReadOnly = False
                            End If
                            '@ Disable Controls for Service Call
                        End With
                    End If


                ElseIf HdVISITSUBTYPE.Value.Trim = "2" Then

                    If HdSTRATEGICTYPE.Value.Trim = "1" Then
                        objDetailsNode = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@DSRCODE='" + hdTarDSRCODE.Value + "' and @DSR_STT_DETAIL_ID='" + hdDSR_STT_DETAIL_ID.Value + "']")
                        If objDetailsNode IsNot Nothing Then
                            With objDetailsNode

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
                                    End If
                                End If

                                If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                                    txtTarCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                                End If
                                If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                                    txtTarTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
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


                            End With
                        End If

                    ElseIf HdSTRATEGICTYPE.Value.Trim = "2" Then
                        objDetailsNode = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@DSRCODE='" + hdRetDSRCODE.Value + "' and @DSR_STR_DETAIL_ID='" + hdDSR_STR_DETAIL_ID.Value + "']")
                        If objDetailsNode IsNot Nothing Then
                            With objDetailsNode

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
                                    End If
                                End If

                                If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                                    txtRetCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                                End If
                                If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                                    txtRetTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
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

                    ElseIf HdSTRATEGICTYPE.Value.Trim = "3" Then
                        objDetailsNode = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@DSRCODE='" + hdAirDSRCODE.Value + "' and @DSR_STA_DETAIL_ID='" + hdDSR_STA_DETAIL_ID.Value + "']")
                        If objDetailsNode IsNot Nothing Then
                            With objDetailsNode


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
                                    End If
                                End If

                                If .Attributes("CLOSER_DATETIME") IsNot Nothing Then
                                    txtAirCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)
                                End If
                                If .Attributes("TARGET_CLOSER_DATETIME") IsNot Nothing Then
                                    txtAirTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)
                                End If


                                '@ Disable Controls for AirNonAir Call                       

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
                End If
            End With
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
      
    End Sub


#Region "btnUpdateServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateServiceCall.Click"
    Protected Sub btnUpdateServiceCall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateServiceCall.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objServiceNode As XmlNode
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction

        Try
            '<SERVICECALLS>
            '		<SERVICECALL SEQUENCENO='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC='' SC_DISCUSSIONISSUE_REMARKS=''
            '			SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME='' SC_COMPETITION_MKT_INFO_REMARKS=''
            '			CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS='' DEPARTMENT_SPECIFIC_ID='' />
            '	</SERVICECALLS>
            objAddXmlDoc.LoadXml(Session("ActionDetails"))
            If btnUpdateServiceCall.Text.Trim = "Update" Then
                If hdSCDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_SC_DETAIL_ID.Value.Trim.Length > 0 Then
                    ' objServiceNode = objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@SEQUENCENO=" + hdServiceCall.Value.Trim + "]")
                    objServiceNode = objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@DSRCODE='" + hdSCDSRCODE.Value + "' and @DSR_SC_DETAIL_ID='" + hdDSR_SC_DETAIL_ID.Value + "']")
                    If Not objServiceNode Is Nothing Then
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

                        objServiceNode.Attributes("SC_STATUSID").Value = ddlServiceStatus.SelectedValue
                        objServiceNode.Attributes("SC_STATUSID_NAME").Value = ddlServiceStatus.SelectedItem.Text.Trim

                        If dlstAssignedTo.SelectedValue <> "" Then
                            objServiceNode.Attributes("ASSIGNTO_EMPLOYEE_NAME").Value = dlstAssignedTo.SelectedItem.Text
                            objServiceNode.Attributes("ASSIGNTO_EMPLOYEEID").Value = dlstAssignedTo.SelectedValue '
                        End If
                        objServiceNode.Attributes("CLOSER_DATETIME").Value = objeAAMS.ConvertTextDate(txtCloserDate.Text.Trim)
                        '@ End of Duplicate Checking
                    End If
                End If

            End If

         

            '#######################################
            '@ Start of Saving Data

            If hdSCDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_SC_DETAIL_ID.Value.Trim.Length > 0 Then
                objInputXml.LoadXml("<SL_UPDATE_DSR_ACTIONDETAILS_INTPUT></SL_UPDATE_DSR_ACTIONDETAILS_INTPUT>")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("CONFIG"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("DSR"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("AGENCY"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("Errors"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS"), True))

                With objInputXml.DocumentElement.SelectSingleNode("DSR")
                    '.Attributes("VISITSUBTYPE").Value = "1"
                    '.Attributes("STRATEGICTYPE").Value = ""
                    'Dim objEmpXml As New XmlDocument
                    'Dim UserId As String
                    'objEmpXml.LoadXml(Session("Security"))
                    'UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    '.Attributes("EMPLOYEEID").Value = UserId
                    .Attributes("DSR_DETAIL_ID").Value = hdDSR_SC_DETAIL_ID.Value
                End With

                ' objInputXml.LoadXml(objAddXmlDoc.OuterXml)
                '@ Now Remove Other Node For Saving input

                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("SERVICECALLS/SERVICECALL")
                    If objnode.Attributes("DSRCODE").Value = hdSCDSRCODE.Value AndAlso objnode.Attributes("DSR_SC_DETAIL_ID").Value = hdDSR_SC_DETAIL_ID.Value Then
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SERVICECALLS").RemoveChild(objnode)
                    End If
                Next
                '@ End Now Remove Other Node For Saving input

                'objOutputXml.Load("c:\SL_VIEW_DSR_ACTIONDETAILS_OUTPUT.xml")

                objOutputXml = objbzDSRAction.Update(objInputXml)
                ' objOutputXml.Save("c:\admin\DSRActionInput.xml")
                Try
                    objInputXml.Save("c:\admin\DSRActionInput.xml")
                    objOutputXml.Save("c:\admin\DSRActionOutput.xml")
                Catch ex As Exception

                End Try


                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    hdDSR_SC_DETAIL_ID.Value = ""
                    Session("ActionDetails") = objAddXmlDoc.OuterXml
                    'Binding Visit Details Data Grid
                    BindGrid(objAddXmlDoc, "SERVICECALL")
                    'Clear Controls 
                    btnCancelServiceCall_Click(sender, e)
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

                '@ End  of Saving Data
                '#######################################  
            End If
                  

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
        btnUpdateServiceCall.Enabled = False
        ddlDepartment.SelectedIndex = 0
        ddlDepttSpecific.SelectedIndex = 0
        ddlServiceStatus.SelectedIndex = 0
        txtDepttSpecific.Text = ""
        txtDetailedDiscussion.Text = ""
        LoadAssignedToByDepartmentId(dlstAssignedTo)

        Dim li As New ListItem
        li = dlstAssignedTo.Items.FindByValue("")
        If li IsNot Nothing Then
            dlstAssignedTo.SelectedValue = li.Value
        End If
        txtCloserDate.Text = ""
        txtTargetCloserDate.Text = ""
      
        imgCloserDate.Style.Add("display", "none")
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
            '		<SERVICECALL DSRCODE='' SEQUENCENO='' DEPARTMENTID='' DEPARTMENT_NAME='' DEPARTMENT_SPECIFIC='' SC_DISCUSSIONISSUE_REMARKS=''
            '			SC_STATUSID='' SC_STATUSID_NAME='' ASSIGNTO_EMPLOYEEID='' ASSIGNTO_EMPLOYEE_NAME='' SC_COMPETITION_MKT_INFO_REMARKS=''
            '			CLOSER_DATETIME='' TARGET_CLOSER_DATETIME='' SC_FOLLOWUP_REMARKS='' />
            '	</SERVICECALLS>          

            If e.CommandName = "EditX" Then
                strCommandArgs = e.CommandArgument
                hdDSR_SC_DETAIL_ID.Value = ""
                hdSCDSRCODE.Value = ""

                hdDSR_SC_DETAIL_ID.Value = strCommandArgs.Trim.Split("|")(0)
                hdSCDSRCODE.Value = strCommandArgs.Trim.Split("|")(1)
                hdSCVisitDATE.Value = strCommandArgs.Trim.Split("|")(2)

                objXmlDoc.LoadXml(Session("ActionDetails"))
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("SERVICECALLS/SERVICECALL[@DSRCODE='" + hdSCDSRCODE.Value + "' and @DSR_SC_DETAIL_ID='" + hdDSR_SC_DETAIL_ID.Value + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode

                        ddlDepartment.SelectedValue = .Attributes("DEPARTMENTID").Value
                        ddlServiceStatus.SelectedValue = .Attributes("SC_STATUSID").Value
                        txtDetailedDiscussion.Text = .Attributes("SC_DISCUSSIONISSUE_REMARKS").Value

                        LoadAssignedToByDepartmentId(dlstAssignedTo)
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

                        hdAssingedTo.Value = .Attributes("ASSIGNTO_EMPLOYEEID").Value
                        txtCloserDate.Text = ConvertToCalenderDate(.Attributes("CLOSER_DATETIME").Value)

                        txtTargetCloserDate.Text = ConvertToCalenderDate(.Attributes("TARGET_CLOSER_DATETIME").Value)

                        If .Attributes("DEPARTMENT_NAME").Value.Trim.ToUpper = "PRODUCT" Then
                            ddlDepttSpecific.Visible = True
                            If txtDepttSpecific.Visible = True Then
                                txtDepttSpecific.Visible = False
                            End If
                            Dim li2 As New ListItem
                            li2 = ddlDepttSpecific.Items.FindByText(.Attributes("DEPARTMENT_SPECIFIC").Value)
                            If li2 IsNot Nothing Then
                                ddlDepttSpecific.SelectedValue = li2.Value
                            Else
                                'ddlDepttSpecific.SelectedValue = ""
                            End If
                            'ddlDepttSpecific.SelectedValue = ddlDepttSpecific.Items.FindByText(.Attributes("DEPARTMENT_SPECIFIC").Value.Trim).Value
                        Else
                            txtDepttSpecific.Text = .Attributes("DEPARTMENT_SPECIFIC").Value
                            txtDepttSpecific.Visible = True
                            If ddlDepttSpecific.Visible = True Then
                                ddlDepttSpecific.Visible = False
                            End If
                        End If

                        If (ddlServiceStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgCloserDate.Style.Add("display", "none")
                            txtCloserDate.CssClass = "textboxgrey"
                            txtCloserDate.ReadOnly = True
                        Else
                            imgCloserDate.Style.Add("display", "block")
                            txtCloserDate.CssClass = "textbox"
                            txtCloserDate.ReadOnly = False
                        End If
                        '@ Disable Controls for Service Call
                        btnUpdateServiceCall.Enabled = True
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


                'Dim LnkSCDel As New LinkButton
                'LnkSCDel = CType(e.Row.FindControl("LnkSCDel"), LinkButton)


                Dim DSR_SC_DETAIL_ID As String = ""
                Dim DSRCODE As String = ""
                Dim DSRDATE As String = ""
                DSR_SC_DETAIL_ID = LnkSCFRem.CommandArgument.ToString.Split("|")(0).Trim.ToString
                DSRCODE = LnkSCFRem.CommandArgument.ToString.Split("|")(1).Trim.ToString
                DSRDATE = LnkSCFRem.CommandArgument.ToString.Split("|")(2).Trim.ToString

                Dim HdSC_STATUSID As HiddenField
                HdSC_STATUSID = e.Row.FindControl("HdSC_STATUSID")

                If DSR_SC_DETAIL_ID.Trim.Length > 0 Then
                    LnkSCFRem.Enabled = True
                    LnkSCFRem.Attributes.Add("onclick", "return EditServiceCallFolowupRem('" + DSR_SC_DETAIL_ID + "','" + DSRCODE + "','" + DSRDATE + "','" + HdSC_STATUSID.Value.Trim + "')")
                    'LnkSCDel.Enabled = False
                Else
                    LnkSCFRem.Attributes.Clear()
                    LnkSCFRem.Enabled = False
                    '  LnkSCDel.Enabled = True
                End If



                '

                If e.Row.Cells(6).Text.Trim.Length = 8 Then
                    e.Row.Cells(6).Text = ConvertToCalenderDate(e.Row.Cells(6).Text)
                End If
                If e.Row.Cells(7).Text.Trim.Length = 8 Then
                    e.Row.Cells(7).Text = ConvertToCalenderDate(e.Row.Cells(7).Text)
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

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrorServiceCall.Text = ex.Message
        End Try
    End Sub
#End Region



#Region "btnUpdateTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateTarget.Click"
    Protected Sub btnUpdateTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateTarget.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objTargetNode As XmlNode
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument

        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Try
            '<STRATEGIC_CALL_TARGET>
            '	<PREVIOUSREMARKS_TARGET REMARKS1="" REMARKS2="" REMARKS3="" REMARKS4="" />
            '	<DETAILTARGET DSR_STT_DETAIL_ID="" SEQUENCENO="" STT_TYPENAME="" A1APPROVED_NEW_DEAL="" 
            '   CPS="" SVT_STATUSID="" SVT_STATUS_NAME="" STT_COMPETITION_MKT_INFO_REMARKS="" 
            '   STT_DISCUSSIONISSUE_REMARKS="" STT_SIGNON_DATE="" STT_TARGET_SEG="" STT_PREV_REMARKS="" />
            '</STRATEGIC_CALL_TARGET>
            objAddXmlDoc.LoadXml(Session("ActionDetails"))
            If btnUpdateTarget.Text.Trim = "Update" Then
                If hdTarDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STT_DETAIL_ID.Value.Trim.Length > 0 Then
                    ' objTargetNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO=" + hdTarget.Value.Trim + "]")
                    objTargetNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@DSRCODE='" + hdTarDSRCODE.Value + "' and @DSR_STT_DETAIL_ID='" + hdDSR_STT_DETAIL_ID.Value + "']")
                    'objTargetNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO=" + hdTarget.Value.Trim + "]")
                    If Not objTargetNode Is Nothing Then




                        objTargetNode.Attributes("SVT_STATUSID").Value = ddlTargetStatus.SelectedValue
                        objTargetNode.Attributes("SVT_STATUS_NAME").Value = ddlTargetStatus.SelectedItem.Text.Trim

                        

                      

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

                        ' hdTarDSRCODE.Value = ""

                    End If
                End If
            Else
               
            End If
            If hdTarDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STT_DETAIL_ID.Value.Trim.Length > 0 Then
                '#######################################
                '@ Start of Saving Data


                '  objInputxml.LoadXml(objAddXmlDoc.OuterXml)

                objInputXml.LoadXml("<SL_UPDATE_DSR_ACTIONDETAILS_INTPUT></SL_UPDATE_DSR_ACTIONDETAILS_INTPUT>")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("CONFIG"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("DSR"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("AGENCY"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("Errors"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET"), True))

                With objInputXml.DocumentElement.SelectSingleNode("DSR")
                    '.Attributes("VISITSUBTYPE").Value = "2"
                    '.Attributes("STRATEGICTYPE").Value = "1"
                    'Dim objEmpXml As New XmlDocument
                    'Dim UserId As String
                    'objEmpXml.LoadXml(Session("Security"))
                    'UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    '.Attributes("EMPLOYEEID").Value = UserId
                    .Attributes("DSR_DETAIL_ID").Value = hdDSR_STT_DETAIL_ID.Value
                End With

                '@ Now Remove Other Node For Saving input

                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_TARGET/DETAILTARGET")
                    If objnode.Attributes("DSRCODE").Value = hdTarDSRCODE.Value AndAlso objnode.Attributes("DSR_STT_DETAIL_ID").Value = hdDSR_STT_DETAIL_ID.Value Then
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET").RemoveChild(objnode)
                    End If
                Next
                '@ End Now Remove Other Node For Saving input

                'objOutputXml.Load("c:\SL_VIEW_DSR_ACTIONDETAILS_OUTPUT.xml")



                objOutputXml = objbzDSRAction.Update(objInputXml)
                ' objOutputXml.Save("c:\admin\DSRActionInput.xml")
                Try
                    objInputXml.Save("c:\admin\DSRActionInput.xml")
                    objOutputXml.Save("c:\admin\DSRActionOutput.xml")
                Catch ex As Exception

                End Try

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    hdDSR_STT_DETAIL_ID.Value = ""
                    Session("ActionDetails") = objAddXmlDoc.OuterXml
                    'Binding Visit Details Data Grid
                    BindGrid(objAddXmlDoc, "DETAILTARGETALL")
                    'Clear Controls 
                    btnCancelTarget_Click(sender, e)
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

                '@ End  of Saving Data
                '#######################################  
            End If
           
                  

        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnCancelTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelTarget.Click"
    Protected Sub btnCancelTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelTarget.Click
        Try
            ClearTargetCancel()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrTarget.Text = ex.Message
        End Try
    End Sub
    Private Sub ClearTargetCancel()

        btnUpdateTarget.Enabled = False
        txtTarget1AApprovedNewDeal.Text = ""
        txtTargetCPS.Text = ""
        ddlTargetStatus.SelectedIndex = 0
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
        txtTargetSignedOn.Text = ""
 
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
            '<STRATEGIC_CALL_TARGET>
            '	<PREVIOUSREMARKS_TARGET REMARKS1="" REMARKS2="" REMARKS3="" REMARKS4="" />
            '	<DETAILTARGET DSR_STT_DETAIL_ID="" SEQUENCENO="" STT_TYPENAME="" A1APPROVED_NEW_DEAL="" 
            '   CPS="" SVT_STATUSID="" SVT_STATUS_NAME="" STT_COMPETITION_MKT_INFO_REMARKS="" 
            '   STT_DISCUSSIONISSUE_REMARKS="" STT_SIGNON_DATE="" STT_TARGET_SEG="" STT_PREV_REMARKS="" />
            '</STRATEGIC_CALL_TARGET>
            If e.CommandName = "EditX" Then


                strCommandArgs = e.CommandArgument
                hdDSR_STT_DETAIL_ID.Value = ""
                hdTarDSRCODE.Value = ""

                hdDSR_STT_DETAIL_ID.Value = strCommandArgs.Trim.Split("|")(0)
                hdTarDSRCODE.Value = strCommandArgs.Trim.Split("|")(1)
                hdTarVisitDATE.Value = strCommandArgs.Trim.Split("|")(2)

                ' strCommandArgs = e.CommandArgument
                objXmlDoc.LoadXml(Session("ActionDetails"))
                'objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@SEQUENCENO='" + strCommandArgs + "']")
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_TARGET/DETAILTARGET[@DSRCODE='" + hdTarDSRCODE.Value + "' and @DSR_STT_DETAIL_ID='" + hdDSR_STT_DETAIL_ID.Value + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode

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


                        If (ddlTargetStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgTarCloserDate.Style.Add("display", "none")
                            txtTarCloserDate.CssClass = "textboxgrey"
                            txtTarCloserDate.ReadOnly = True
                        Else
                            imgTarCloserDate.Style.Add("display", "block")
                            txtTarCloserDate.CssClass = "textbox"
                            txtTarCloserDate.ReadOnly = False
                        End If

                        btnUpdateTarget.Enabled = True
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

                If HdSVT_STATUSID.Value.Split("|")(1) = "1" Then
                    Dim lnkEdit As New LinkButton
                    lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
                    If DSR_STT_DETAIL_ID.Value.Trim.Length > 0 Then
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
            lblErrTarget.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "btnUpdateAirNonAir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateAirNonAir.Click"
    Protected Sub btnUpdateAirNonAir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateAirNonAir.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Dim objAirNonAirNode As XmlNode
        Try
            '<STRATEGIC_CALL_AIR_OTHER>
            '	<PREVIOUSREMARKS_AIR_OTHER REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILAIR_OTHER DSR_STA_DETAIL_ID='' SEQUENCENO='' STA_TYPENAME='' PRODUCTID='' PRODUCT_NAME=''
            '		REVENUE='' SV_STATUSID='' SV_STATUS_NAME='' STA_COMPETITION_MKT_INFO_REMARKS='' STA_DISCUSSIONISSUE_REMARKS=''
            '		STA_SIGNON_DATE='' STA_PREV_REMARKS='' />
            '</STRATEGIC_CALL_AIR_OTHER>
            objAddXmlDoc.LoadXml(Session("ActionDetails"))
            If btnUpdateAirNonAir.Text.Trim = "Update" Then
                If hdAirDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STA_DETAIL_ID.Value.Trim.Length > 0 Then
                    '  objAirNonAirNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO=" + hdAirNonAir.Value.Trim + "]")
                    objAirNonAirNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@DSRCODE='" + hdAirDSRCODE.Value + "' and @DSR_STA_DETAIL_ID='" + hdDSR_STA_DETAIL_ID.Value + "']")
                    If Not objAirNonAirNode Is Nothing Then

                        objAirNonAirNode.Attributes("SV_STATUSID").Value = ddlAirNonAirStatus.SelectedValue
                        objAirNonAirNode.Attributes("SV_STATUS_NAME").Value = ddlAirNonAirStatus.SelectedItem.Text.Trim
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
                    End If
                End If
            Else
            End If

            '@ Start of Saving Data
            '####################################### 
            If hdAirDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STA_DETAIL_ID.Value.Trim.Length > 0 Then
                'objInputXml.LoadXml(objAddXmlDoc.OuterXml)
                objInputXml.LoadXml("<SL_UPDATE_DSR_ACTIONDETAILS_INTPUT></SL_UPDATE_DSR_ACTIONDETAILS_INTPUT>")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("CONFIG"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("DSR"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("AGENCY"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("Errors"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER"), True))

                With objInputXml.DocumentElement.SelectSingleNode("DSR")
                    '.Attributes("VISITSUBTYPE").Value = "2"
                    '.Attributes("STRATEGICTYPE").Value = "3"
                    'Dim objEmpXml As New XmlDocument
                    'Dim UserId As String
                    'objEmpXml.LoadXml(Session("Security"))
                    'UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    '.Attributes("EMPLOYEEID").Value = UserId
                    .Attributes("DSR_DETAIL_ID").Value = hdDSR_STA_DETAIL_ID.Value
                End With
                '@ Now Remove Other Node For Saving input
                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER")
                    If objnode.Attributes("DSRCODE").Value = hdAirDSRCODE.Value AndAlso objnode.Attributes("DSR_STA_DETAIL_ID").Value = hdDSR_STA_DETAIL_ID.Value Then
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER").RemoveChild(objnode)
                    End If
                Next
                '@ End Now Remove Other Node For Saving input

                'objOutputXml.Load("c:\SL_VIEW_DSR_ACTIONDETAILS_OUTPUT.xml")
                objOutputXml = objbzDSRAction.Update(objInputXml)
                ' objOutputXml.Save("c:\admin\DSRActionInput.xml")
                Try
                    objInputXml.Save("c:\admin\DSRActionInput.xml")
                    objOutputXml.Save("c:\admin\DSRActionOutput.xml")
                Catch ex As Exception
                End Try

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    hdDSR_STA_DETAIL_ID.Value = ""
                    Session("ActionDetails") = objAddXmlDoc.OuterXml
                    'Binding Visit Details Data Grid
                    BindGrid(objAddXmlDoc, "DETAILAIR_OTHERALL")
                    'Clear Controls 
                    btnCancelAirNonAir_Click(sender, e)
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
                '@ End  of Saving Data
                '####################################### 
            End If
         
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub

#End Region

#Region "btnCancelAirNonAir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelAirNonAir.Click"
    Protected Sub btnCancelAirNonAir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelAirNonAir.Click
        Try
            ClearAirNonAir()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub
    Private Sub ClearAirNonAir()

        btnUpdateAirNonAir.Enabled = False
        ddlProductName.SelectedIndex = 0
        txtRevenue.Text = ""
        ddlAirNonAirStatus.SelectedIndex = 0
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
        txtAirCloserDate.Text = ""
        txtAirTargetCloserDate.Text = ""

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
            '<STRATEGIC_CALL_AIR_OTHER>
            '	<PREVIOUSREMARKS_AIR_OTHER REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILAIR_OTHER DSR_STA_DETAIL_ID='' SEQUENCENO='' STA_TYPENAME='' PRODUCTID='' PRODUCT_NAME=''
            '		REVENUE='' SV_STATUSID='' SV_STATUS_NAME='' STA_COMPETITION_MKT_INFO_REMARKS='' STA_DISCUSSIONISSUE_REMARKS=''
            '		STA_SIGNON_DATE='' STA_PREV_REMARKS='' />
            '</STRATEGIC_CALL_AIR_OTHER>
            If e.CommandName = "EditX" Then
                ' strCommandArgs = e.CommandArgument

                strCommandArgs = e.CommandArgument
                hdDSR_STA_DETAIL_ID.Value = ""
                hdAirDSRCODE.Value = ""
                hdDSR_STA_DETAIL_ID.Value = strCommandArgs.Trim.Split("|")(0)
                hdAirDSRCODE.Value = strCommandArgs.Trim.Split("|")(1)
                hdAirVisitDATE.Value = strCommandArgs.Trim.Split("|")(2)

                objXmlDoc.LoadXml(Session("ActionDetails"))
                ' objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@SEQUENCENO='" + strCommandArgs + "']")
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_AIR_OTHER/DETAILAIR_OTHER[@DSRCODE='" + hdAirDSRCODE.Value + "' and @DSR_STA_DETAIL_ID='" + hdDSR_STA_DETAIL_ID.Value + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode

                     
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

                        If (ddlAirNonAirStatus.SelectedValue.Split("|").GetValue(1).ToString = "0") Then
                            imgAirCloserDate.Style.Add("display", "none")
                            txtAirCloserDate.CssClass = "textboxgrey"
                            txtAirCloserDate.ReadOnly = True
                        Else
                            imgAirCloserDate.Style.Add("display", "block")
                            txtAirCloserDate.CssClass = "textbox"
                            txtAirCloserDate.ReadOnly = False
                        End If

                        btnUpdateAirNonAir.Enabled = True
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

                If HdSV_STATUSID.Value.Split("|")(1) = "1" Then
                    Dim lnkEdit As New LinkButton
                    lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
                    If HdDSR_STA_DETAIL_ID.Value.Trim.Length > 0 Then
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
            lblErrAirNonAirProduct.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "btnUpdateRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRetention.Click"
    Protected Sub btnUpdateRetention_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRetention.Click
        Dim objAddXmlDoc As New XmlDataDocument
        Dim objRetentionNode As XmlNode
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        Dim objbzDSRAction As New AAMS.bizSales.bzDSRAction
        Try
            '<STRATEGIC_CALL_RETENTION>
            '	<BCDETAILS BCID='' CHAIN_CODE='' CPS='' />
            '	<PREVIOUSREMARKS_RETENTION REMARKS1='' REMARKS2='' REMARKS3='' REMARKS4='' />
            '	<DETAILRETENTION DSR_STR_DETAIL_ID='' SEQUENCENO='' STR_TYPENAME='' EXISTINGDEAL='' CPS='' SVR_REASONID=''
            '		SVR_REASON_NAME='' SVR_STATUSID='' SVR_STATUS_NAME='' A1APPROVED_NEW_DEAL='' NEWCPS='' 
            '       STR_COMPETITION_MKT_INFO_REMARKS=''
            '		STR_DISCUSSIONISSUE_REMARKS='' STR_SIGNON_DATE='' STR_TARGET_SEG='' STR_PREV_REMARKS='' />
            '</STRATEGIC_CALL_RETENTION>
            objAddXmlDoc.LoadXml(Session("ActionDetails"))
            If btnUpdateRetention.Text.Trim = "Update" Then
                If hdRetDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STR_DETAIL_ID.Value.Trim.Length > 0 Then
                    'objRetentionNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO=" + hdRetention.Value.Trim + "]")

                    objRetentionNode = objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@DSRCODE='" + hdRetDSRCODE.Value + "' and @DSR_STR_DETAIL_ID='" + hdDSR_STR_DETAIL_ID.Value + "']")
                    If Not objRetentionNode Is Nothing Then

                        objRetentionNode.Attributes("SVR_STATUSID").Value = ddlRetentionStatus.SelectedValue
                        objRetentionNode.Attributes("SVR_STATUS_NAME").Value = ddlRetentionStatus.SelectedItem.Text.Trim
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


                    End If
                End If
            Else
            End If


            If hdRetDSRCODE.Value.Trim.Length > 0 AndAlso hdDSR_STR_DETAIL_ID.Value.Trim.Length > 0 Then

                ' objInputXml.LoadXml(objAddXmlDoc.OuterXml)
                objInputXml.LoadXml("<SL_UPDATE_DSR_ACTIONDETAILS_INTPUT></SL_UPDATE_DSR_ACTIONDETAILS_INTPUT>")
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("CONFIG"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("DSR"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("AGENCY"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("Errors"), True))
                objInputXml.DocumentElement.AppendChild(objInputXml.ImportNode(objAddXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION"), True))

                With objInputXml.DocumentElement.SelectSingleNode("DSR")
                    '.Attributes("VISITSUBTYPE").Value = "2"
                    '.Attributes("STRATEGICTYPE").Value = "2"
                    'Dim objEmpXml As New XmlDocument
                    'Dim UserId As String
                    'objEmpXml.LoadXml(Session("Security"))
                    'UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    '.Attributes("EMPLOYEEID").Value = UserId
                    .Attributes("DSR_DETAIL_ID").Value = hdDSR_STR_DETAIL_ID.Value
                End With

                '@ Now Remove Other Node For Saving input
                For Each objnode As XmlNode In objInputXml.DocumentElement.SelectNodes("STRATEGIC_CALL_RETENTION/DETAILRETENTION")
                    If objnode.Attributes("DSRCODE").Value = hdRetDSRCODE.Value AndAlso objnode.Attributes("DSR_STR_DETAIL_ID").Value = hdDSR_STR_DETAIL_ID.Value Then
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION").RemoveChild(objnode)
                    End If
                Next
                '@ End Now Remove Other Node For Saving input

                'objOutputXml.Load("c:\SL_VIEW_DSR_ACTIONDETAILS_OUTPUT.xml")

                objOutputXml = objbzDSRAction.Update(objInputXml)
                ' objOutputXml.Save("c:\admin\DSRActionInput.xml")
                Try
                    objInputXml.Save("c:\admin\DSRActionInput.xml")
                    objOutputXml.Save("c:\admin\DSRActionOutput.xml")
                Catch ex As Exception

                End Try
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    hdDSR_STR_DETAIL_ID.Value = ""
                    Session("ActionDetails") = objAddXmlDoc.OuterXml
                    'Binding Visit Details Data Grid
                    BindGrid(objAddXmlDoc, "DETAILRETENTIONALL")
                    'Clear Controls 
                    btnCancelRetention_Click(sender, e)
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
                '@ End  of Saving Data
                '####################################### 

            End If
        
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
       
        btnUpdateRetention.Enabled = False

        txtCPS.Text = ""
        ddlRetentionReason.SelectedIndex = 0
        ddlRetentionStatus.SelectedIndex = 0
        txt1AApprovedNewDeal.Text = ""
        txtNewCPS.Text = ""
      
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
        txtRetentionSignedOn.Text = ""
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
                ' strCommandArgs = e.CommandArgument
                strCommandArgs = e.CommandArgument

                hdDSR_STR_DETAIL_ID.Value = ""
                hdRetDSRCODE.Value = ""
                hdDSR_STR_DETAIL_ID.Value = strCommandArgs.Trim.Split("|")(0)
                hdRetDSRCODE.Value = strCommandArgs.Trim.Split("|")(1)
                hdRetVisitDATE.Value = strCommandArgs.Trim.Split("|")(2)

                objXmlDoc.LoadXml(Session("ActionDetails"))
                'objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@SEQUENCENO='" + strCommandArgs + "']")
                objDetailsNode = objXmlDoc.DocumentElement.SelectSingleNode("STRATEGIC_CALL_RETENTION/DETAILRETENTION[@DSRCODE='" + hdRetDSRCODE.Value + "' and @DSR_STR_DETAIL_ID='" + hdDSR_STR_DETAIL_ID.Value + "']")
                If objDetailsNode IsNot Nothing Then
                    With objDetailsNode

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

                        btnUpdateRetention.Enabled = True

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


                If HdSVR_STATUSID.Value.Split("|")(1) = "1" Then
                    Dim lnkEdit As New LinkButton
                    lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
                    If HdDSR_STR_DETAIL_ID.Value.Trim.Length > 0 Then
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
            lblErrRetention.Text = ex.Message
        End Try
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



  
    Protected Sub BtnAirReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAirReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnRetReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRetReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnSCReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSCReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnTarReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTarReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
