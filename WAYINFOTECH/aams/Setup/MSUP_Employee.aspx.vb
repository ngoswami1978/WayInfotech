Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Partial Class Setup_MSUP_Employee
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Public strBuilder As New StringBuilder
    Public strBuilderRequest As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

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
        Session("PageName") = "Setup/MSUP_Employee.aspx"
        lblError.Text = String.Empty
        btnSave.Attributes.Add("onclick", "return ValidateEmployeeDataOnSave();")
        drpRestrict.Attributes.Add("onchange", "return CheckRestrictEmployeeData();")
        txtEmployeeName.Attributes.Add("onkeypress", "allTextWithSpace()")

        btnHistory.Attributes.Add("onclick", "return ShowPTRHistory();")

        Try
            If hdTabType.Value = "" Then
                strIndex = 0
            Else
                strIndex = hdTabType.Value
            End If
            btnSave.Enabled = True

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            'Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument

            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value)
            '        If strBuilder(1) = "0" Then
            '            btnNew.Enabled = False
            '            ' btnSave.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            CheckSecurity()


            If Not Page.IsPostBack Then
                ' ChangeButtonText()
                Session("ViewEmployeeData") = "0|0|0|0|0"
                LoadAllControl()
                Bindata()
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    hdSessionValue.Value = Session("Action")
                    ViewEmployee()
                Else

                End If
            End If
           
           If Request.QueryString("PageLink") IsNot Nothing Then
                btnSave.Enabled = False
            End If

            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()
           

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpAoffice.SelectedValue = li.Value
                            End If
                        End If
                        drpAoffice.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Attributes("Value").Value)
                End If
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']").Count <> 0 Then
                    strBuilderRequest = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']").Attributes("Value").Value)
                End If

                hdRequest.Value = Val(objSecurityXml.DocumentElement.SelectSingleNode("Request").InnerText)
               

               

               

                'When View rights disabled
                If strBuilder(0) = "0" And strBuilderRequest(0) = "0" Then
                    btnSave.Enabled = False
                    Response.Redirect("../NoRights.aspx")
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                    chkPassExpire.CssClass = "displayNone"
                    chkChangePass.CssClass = "displayNone"
                    chkActive.CssClass = "displayNone"
                    tdPasswordExpire.InnerText = ""
                    tdChangePassword.InnerText = ""
                    trActive.Visible = False
                    trActive1.Visible = False
                Else
                    If tdPasswordExpire.InnerText = "" Then
                        tdPasswordExpire.InnerText = "Password Never Expire"
                    End If

                    If tdChangePassword.InnerText = "" Then
                        tdChangePassword.InnerText = "Change Password on First Login"
                    End If

                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (Session("Action").ToString().Split("|").GetValue(0) = "U") Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If
                'When modify rights Enabled and Add rights disabled
                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If

                If strBuilderRequest(1) <> "0" And strBuilder(1) = "0" Then
                    btnSave.Text = "Request"
                    btnSave.CommandName = "Request"
                    chkActive.CssClass = ""
                    trActive.Visible = True
                    trActive1.Visible = True
                    btnSave.Enabled = True
                    btnNew.Enabled = True
                End If

                If strBuilderRequest(1) <> "0" And (strBuilder(1) <> "0" Or strBuilder(2) <> "0") And (Session("Action").ToString().Split("|").GetValue(0) = "U") Then
                    btnSave.Text = "Save"
                    btnSave.CommandName = "Save"
                    trActive.Visible = True
                    trActive1.Visible = True
                    'chkActive.Checked = True
                End If

                'If hdRequest.Value = "0" Then
                '    btnSave.Text = "Request"
                '    btnSave.CommandName = "Request"
                '    trActive.Visible = False
                '    btnSave.Enabled = True
                '    btnNew.Enabled = True
                'ElseIf hdRequest.Value = "1" Then
                '    btnSave.Text = "Save"
                '    btnSave.CommandName = "Save"
                '    trActive.Visible = True
                '    chkActive.Checked = True
                'ElseIf hdRequest.Value = "-1" Then
                '    btnSave.Text = "Save"
                '    btnSave.CommandName = "Save"
                '    trActive.Visible = False
                '    btnSave.Enabled = False
                'End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And strBuilderRequest(2) = "0" And (Session("Action").ToString().Split("|").GetValue(0) = "U") Then
                    btnSave.Enabled = False
                End If
                If btnSave.Text = "Request" Then
                    trActive.Visible = False
                    trActive1.Visible = False
                End If
            Else
                hdRequest.Value = "1"
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'This function Change the text of a button on the basis of the rights
    'If Request Element has -1 value when no rights given
    'If Request Element has 0 value then text of a button should be request and trActive row is visible false
    'If Request Element has 1 value then text of a button should be Save and trActive row is visible true
    Sub ChangeButtonText()

        If Session("Security") IsNot Nothing Then
            Dim objOutputXml As New XmlDocument
            objOutputXml.LoadXml(Session("Security"))
            If (objOutputXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                hdRequest.Value = Val(objOutputXml.DocumentElement.SelectSingleNode("Request").InnerText)
                If hdRequest.Value = "0" Then
                    btnSave.Text = "Request"
                    btnSave.CommandName = "Request"
                    trActive.Visible = False
                    trActive1.Visible = False
                ElseIf hdRequest.Value = "1" Then
                    btnSave.Text = "Save"
                    btnSave.CommandName = "Save"
                    trActive.Visible = True
                    trActive1.Visible = True
                    chkActive.Checked = True
                ElseIf hdRequest.Value = "-1" Then
                    btnSave.Text = "Save"
                    btnSave.CommandName = "Save"
                    trActive.Visible = False
                    trActive1.Visible = False
                    btnSave.Enabled = False
                End If
            Else
                btnSave.Text = "Save"
                btnSave.CommandName = "Save"
                trActive.Visible = True
                trActive1.Visible = True
                chkActive.Checked = True
                hdRequest.Value = "1"
            End If

        End If
    End Sub
    Private Sub ViewEmployee()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Try

            objInputXml.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            objOutputXml = objbzEmployee.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ViewEmployeeData") = Session("ViewEmployeeData").ToString().Remove(0, 1).Insert(0, 1)
                With objOutputXml.DocumentElement.SelectSingleNode("Employee")
                    txtEmployeeName.Text = .Attributes("Employee_Name").Value()
                    If .Attributes("Designation").Value() <> "" Then
                        Dim li As ListItem
                        li = drpDesignation.Items.FindByText(.Attributes("Designation").Value())
                        If li IsNot Nothing Then
                            '  li.Selected = True
                            drpDesignation.SelectedValue = li.Value
                        Else
                            drpDesignation.SelectedIndex = 0
                        End If
                        ' drpDesignation.SelectedItem.Text = .Attributes("Designation").Value()
                    Else
                        drpDesignation.SelectedIndex = 0
                    End If
                    txtMobileNumber.Text = .Attributes("Cell_Phone").Value()
                    txtEmail.Text = .Attributes("Email").Value()

                    If .Attributes("LoginRequired").Value() = "True" Then
                        chkLoginRequired.Checked = True
                        txtLogin.Text = .Attributes("Login").Value()
                        txtPassword.Attributes.Add("Value", .Attributes("Password").Value())
                        txtRetypePassword.Attributes.Add("Value", .Attributes("Password").Value())
                        If .Attributes("IPAddress").Value() <> "" Then
                            txtIPAddress.Attributes.Clear()
                            txtIPAddress.Attributes.Add("Value", .Attributes("IPAddress").Value())
                            txtIPAddress.CssClass = "textboxgrey"
                            txtIPAddress.Attributes.Add("readOnly", "readOnly")

                        End If
                        'txtLogin.Attributes.Add("readOnly", "false")
                        'txtRetypePassword.Attributes.Add("readOnly", "false")
                        'txtPassword.Attributes.Add("readOnly", "false")

                    Else
                        chkLoginRequired.Checked = False
                        txtLogin.CssClass = "textboxgrey"
                        txtPassword.CssClass = "textboxgrey"
                        txtRetypePassword.CssClass = "textboxgrey"
                        'txtIPAddress.CssClass = "textboxgrey"


                        txtLogin.Attributes.Clear()
                        txtRetypePassword.Attributes.Clear()
                        txtPassword.Attributes.Clear()
                        'txtIPAddress.Attributes.Clear()

                        txtLogin.Attributes.Add("Value", "")
                        txtPassword.Attributes.Add("Value", "")
                        txtRetypePassword.Attributes.Add("Value", "")
                        'txtIPAddress.Attributes.Add("Value", "")


                        txtLogin.Attributes.Add("readOnly", "readOnly")
                        txtRetypePassword.Attributes.Add("readOnly", "readOnly")
                        txtPassword.Attributes.Add("readOnly", "readOnly")
                        'txtIPAddress.Attributes.Add("readOnly", "readOnly")

                    End If

                    If .Attributes("IPAddress").Value() <> "" Then
                        txtIPAddress.Text = .Attributes("IPAddress").Value()
                        txtIPAddress.CssClass = "textboxgrey"
                        txtIPAddress.ReadOnly = True
                        'txtIPAddress.Attributes.Add("readOnly", "readOnly")
                    End If

                    If Convert.ToString(.Attributes("DateStart").Value()) <> "" Then
                        txtDOJ.Text = objeAAMS.ConvertDate(.Attributes("DateStart").Value()).ToString("dd/MM/yyyy")
                    End If

                    If .Attributes("DateEnd").Value() <> "" Then
                        txtDOL.Text = objeAAMS.ConvertDate(.Attributes("DateEnd").Value()).ToString("dd/MM/yyyy")
                    End If
                    If .Attributes("DepartmentID").Value() <> "" Then
                        drpDepartment.SelectedValue = .Attributes("DepartmentID").Value()
                    End If
                    If .Attributes("Aoffice").Value() <> "" Then
                        drpAoffice.SelectedValue = .Attributes("Aoffice").Value()
                    End If
                    If .Attributes("ManagerID").Value() <> "" Then
                        drpHOD.SelectedValue = .Attributes("ManagerID").Value()
                    End If
                    If .Attributes("ImmediateSupervisorID").Value() <> "" Then
                        drpImmediateSuperVisor.SelectedValue = .Attributes("ImmediateSupervisorID").Value()
                    End If
                    Dim liFirstForm As New ListItem
                    liFirstForm = drpFirstForm.Items.FindByValue(.Attributes("FirstForm").Value)
                    If liFirstForm IsNot Nothing Then
                        drpFirstForm.SelectedValue = liFirstForm.Value
                    End If
                    'drpFirstForm.SelectedValue = .Attributes("FirstForm").Value()

                    If .Attributes("Limited_To_OwnAgency").Value.ToUpper = "TRUE" Then
                        drpRestrict.SelectedIndex = 1
                    ElseIf .Attributes("Limited_To_Aoffice").Value.ToUpper = "TRUE" Then
                        drpRestrict.SelectedIndex = 2
                        'ElseIf .Attributes("IPRestriction").Value.ToUpper = "TRUE" Then
                        '    drpRestrict.SelectedIndex = 3
                    ElseIf .Attributes("Limited_To_Region").Value.ToUpper = "TRUE" Then
                        drpRestrict.SelectedIndex = 3
                    End If

                    If .Attributes("SecurityRegionID").Value() <> "0" Then
                        drpRegionList.SelectedValue = .Attributes("SecurityRegionID").Value()
                    End If


                    If .Attributes("AgreementSigned").Value() = "True" Then
                        chkAgmntSigned.Checked = True
                    Else
                        chkAgmntSigned.Checked = False
                    End If

                    If .Attributes("ChangePassword").Value() = "True" Then
                        chkChangePass.Checked = True
                    Else
                        chkChangePass.Checked = False
                    End If

                    If .Attributes("PwdExpire").Value() = "True" Then
                        chkPassExpire.Checked = True
                    Else
                        chkPassExpire.Checked = False
                    End If



                    If .Attributes("CityId").Value <> "" Then
                        drpCity.SelectedValue = .Attributes("CityId").Value()
                    End If
                    If .Attributes("Request").Value <> "" AndAlso hdRequest.Value = "1" Then
                        chkActive.Checked = .Attributes("Request").Value
                    End If

                    ddlAgencyType.SelectedValue = .Attributes("GroupTypeID").Value

                    If .Attributes("Show_Prod_ISUPERVISOR").Value <> "" Then
                        chkShowToSupervisor.Checked = .Attributes("Show_Prod_ISUPERVISOR").Value
                    End If

                    'If intRequest = 0 Then
                    '    btnSave.Text = "Request"
                    '    btnSave.CommandName = "Request"
                    '    trActive.Visible = False
                    'ElseIf intRequest = 1 Then
                    '    btnSave.Text = "Save"
                    '    btnSave.CommandName = "Save"
                    '    trActive.Visible = False
                    'Else
                    '    btnSave.Text = "Save"
                    '    btnSave.CommandName = "Save"
                    '    trActive.Visible = True
                    '    chkActive.Checked = iif(intRequest=
                    'End If
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Sub LoadAllControl()
        Try
            'Bind Control for Employee 
            objeAAMS.BindDropDown(drpDepartment, "DepartmentName", True)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True)
            objeAAMS.BindDropDown(drpCity, "CITY", True)
            objeAAMS.BindDropDown(drpRegionList, "SECURITYREGION", True)
            objeAAMS.BindDropDown(drpHOD, "EMPLOYEE", True)
            objeAAMS.BindDropDown(drpImmediateSuperVisor, "EMPLOYEE", True)
            objeAAMS.BindDropDown(drpDesignation, "DESIGNATION", True)
            objeAAMS.BindDropDown(drpFirstForm, "FIRSTFORM", True)
            objeAAMS.BindDropDown(ddlAgencyType, "AGROUP", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()

        Try
            TabText.Add("Employee")
            TabText.Add("Permission")
            TabText.Add("Group")
            TabText.Add("IP")
            TabText.Add("Supervisory")
            TabText.Add("HelpDesk")

            If lblError.Text.Trim() = objeAAMSMessage.messInsert Then
                Dim objSecurityXml As New XmlDocument
                Dim strBuilder As New StringBuilder
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value)
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                            Session("Action") = "I"
                            ' hideTabStrip("0")
                            strIndex = "0"
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
            End If


            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
            hideTabStrip(strIndex)

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        If Session("Action").ToString().Split("|").GetValue(0) = "I" Then
            Button1.Enabled = False
        End If
        If Button1.Text = "Supervisory" Then
            Button1.Width = 80
        End If
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Employee"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Permission"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Permission") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-ChainCode") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "IP"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Ip") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Supervisory"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Supervisory") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "HelpDesk"

                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-HelpDesk") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If

        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethodEmployeeNavigation('" & Button1.ClientID.ToString() & "',6);")
    End Sub

    Private Sub hideTabStrip(ByVal strIndex As String)
        Dim strScript As String = ""
        Select Case strIndex
            Case "0"
                strScript = "<script language='javascript'>" & _
                "document.getElementById('pnlEmployee').style.display ='block'; " & _
                "document.getElementById('pnlPermission').style.display  ='none';" & _
                "document.getElementById('pnlGroup').style.display ='none';  " & _
                "document.getElementById('pnlIP').style.display ='none'; " & _
                "document.getElementById('pnlSupervisor').style.display ='none';" & _
                "</script>"

            Case "1"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlEmployee').style.display ='none'; " & _
               "document.getElementById('pnlPermission').style.display  ='block';" & _
               "document.getElementById('pnlGroup').style.display ='none';  " & _
               "document.getElementById('pnlIP').style.display ='none'; " & _
               "document.getElementById('pnlSupervisor').style.display ='none';" & _
               "</script>"
            Case "2"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlEmployee').style.display ='none'; " & _
               "document.getElementById('pnlPermission').style.display  ='none';" & _
               "document.getElementById('pnlGroup').style.display ='block';  " & _
               "document.getElementById('pnlIP').style.display ='none'; " & _
               "document.getElementById('pnlSupervisor').style.display ='none';" & _
               "</script>"
            Case "3"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlEmployee').style.display ='none'; " & _
               "document.getElementById('pnlPermission').style.display  ='none';" & _
               "document.getElementById('pnlGroup').style.display ='none';  " & _
               "document.getElementById('pnlIP').style.display ='block'; " & _
               "document.getElementById('pnlSupervisor').style.display ='none';" & _
               "</script>"
            Case "4"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlEmployee').style.display ='none'; " & _
               "document.getElementById('pnlPermission').style.display  ='none';" & _
               "document.getElementById('pnlGroup').style.display ='none';  " & _
               "document.getElementById('pnlIP').style.display ='none'; " & _
               "document.getElementById('pnlSupervisor').style.display ='block';" & _
               "</script>"
        End Select
        ' ClientScript.RegisterStartupScript(Me.GetType(),"strScript", strScript)
    End Sub

    '**************************************************************************************************************
    'Save Employee Information
    '**************************************************************************************************************
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '<MS_UPDATEEMPLOYEE_INPUT>
        '  <Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Email='' Employee_Name='' ManagerID='' LoginRequired='' Login='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned='' Request='' GroupTypeID='' Show_Prod_ISUPERVISOR='' IPRestriction='' IPAddress=''/>
        '  <EmailID/>
        '</MS_UPDATEEMPLOYEE_INPUT>
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_UPDATEEMPLOYEE_INPUT><Employee EmployeeID='' Aoffice='' DepartmentID='' Limited_To_OwnAgency='' Limited_To_Region='' Limited_To_Aoffice='' Cell_Phone='' Email='' Employee_Name='' ManagerID='' LoginRequired='' Login='' Password='' FirstForm='' Designation='' ImmediateSupervisorID='' SecurityRegionID=''  DateStart='' DateEnd='' CityId='' ChangePassword='' PwdExpire='' AgreementSigned='' Request='' GroupTypeID='' Show_Prod_ISUPERVISOR='' IPRestriction='' IPAddress='' ChangedBy=''/><EmailID/></MS_UPDATEEMPLOYEE_INPUT>")

        Try
            ' Adding Email Id For New User Request For Sending Mail.
            If Session("Security") IsNot Nothing Then
                Dim objXmldocMail As New XmlDocument
                objXmldocMail.LoadXml(Session("Security").ToString)
                If Not objXmldocMail.DocumentElement.SelectSingleNode("EmailID") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EmailID").InnerText = objXmldocMail.DocumentElement.SelectSingleNode("EmailID").InnerText
                End If
            End If
            ' Security Input Xml End.

            With objInputXml.DocumentElement.SelectSingleNode("Employee")
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    .Attributes("EmployeeID").Value() = Session("Action").ToString().Split("|").GetValue(1)
                End If
                .Attributes("Aoffice").Value() = drpAoffice.SelectedValue
                .Attributes("DepartmentID").Value() = drpDepartment.SelectedValue

                .Attributes("Cell_Phone").Value() = txtMobileNumber.Text
                .Attributes("Login").Value() = txtLogin.Text
                .Attributes("Email").Value() = txtEmail.Text
                .Attributes("Employee_Name").Value() = txtEmployeeName.Text
                If drpHOD.SelectedIndex <> 0 Then
                    .Attributes("ManagerID").Value() = drpHOD.SelectedValue
                End If
                If drpRestrict.SelectedValue = "1" Then
                    .Attributes("Limited_To_OwnAgency").Value = 1
                Else
                    .Attributes("Limited_To_OwnAgency").Value = 0
                End If
                If drpRestrict.SelectedValue = "2" Then
                    .Attributes("Limited_To_Aoffice").Value() = 1
                Else
                    .Attributes("Limited_To_Aoffice").Value() = 0
                End If

                If drpRestrict.SelectedValue = "3" Then
                    .Attributes("IPRestriction").Value() = 1
                Else
                    .Attributes("IPRestriction").Value() = 0
                End If
                If drpRestrict.SelectedValue = "4" Then
                    .Attributes("Limited_To_Region").Value() = 1
                Else
                    .Attributes("Limited_To_Region").Value() = 0
                End If
                .Attributes("Password").Value() = txtPassword.Text
                .Attributes("FirstForm").Value() = drpFirstForm.SelectedValue
                .Attributes("Designation").Value() = drpDesignation.SelectedItem.Text
                If drpImmediateSuperVisor.SelectedIndex <> 0 Then
                    .Attributes("ImmediateSupervisorID").Value() = drpImmediateSuperVisor.SelectedValue
                End If
                If drpRegionList.SelectedIndex <> 0 Then
                    .Attributes("SecurityRegionID").Value() = drpRegionList.SelectedValue
                End If
                If txtDOJ.Text <> "" Then
                    .Attributes("DateStart").Value() = objeAAMS.ConvertTextDate(txtDOJ.Text)
                End If
                If txtDOL.Text <> "" Then
                    .Attributes("DateEnd").Value() = objeAAMS.ConvertTextDate(txtDOL.Text)
                End If
                If chkAgmntSigned.Checked Then
                    .Attributes("AgreementSigned").Value() = "1"
                Else
                    .Attributes("AgreementSigned").Value() = "0"
                End If

                If chkChangePass.Checked Then
                    .Attributes("ChangePassword").Value() = "1"
                Else
                    .Attributes("ChangePassword").Value() = "0"
                End If

                If chkPassExpire.Checked Then
                    .Attributes("PwdExpire").Value() = "1"
                Else
                    .Attributes("PwdExpire").Value() = "0"
                End If

                .Attributes("CityId").Value() = drpCity.SelectedValue


                If hdRequest.Value = "1" Then
                    .Attributes("Request").Value = IIf(chkActive.Checked = True, "1", "0")
                    .Attributes("GroupTypeID").Value = ddlAgencyType.SelectedValue
                    .Attributes("Show_Prod_ISUPERVISOR").Value = chkShowToSupervisor.Checked
                ElseIf hdRequest.Value = "0" Then
                    .Attributes("Request").Value = "0"
                    ' .Attributes("GroupTypeID").Value = ""
                    ' .Attributes("Show_Prod_ISUPERVISOR").Value = ""
                End If

                If chkLoginRequired.Checked Then
                    .Attributes("LoginRequired").Value() = chkLoginRequired.Checked
                    .Attributes("Login").Value() = txtLogin.Text
                    .Attributes("Password").Value() = txtPassword.Text
                Else
                    .Attributes("LoginRequired").Value() = chkLoginRequired.Checked
                    .Attributes("Login").Value() = ""
                    .Attributes("Password").Value() = ""
                End If
                If txtIPAddress.Text.Trim = "" Then
                    .Attributes("IPRestriction").Value() = "False"
                    .Attributes("IPAddress").Value() = ""
                Else
                    .Attributes("IPRestriction").Value() = "True"
                    .Attributes("IPAddress").Value() = txtIPAddress.Text.Trim
                End If

                ''added by ashish
                .Attributes("ChangedBy").Value() = Session("LoginSession").ToString().Split("|")(0)  ''login user
                ''end


            End With

            'Here Back end Method Call
            objOutputXml = objbzEmployee.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                End If

                CheckSecurity()
                Session("Action") = "U|" & objOutputXml.DocumentElement.SelectSingleNode("Employee").Attributes("EmployeeID").Value.Trim & ""
                hdSessionValue.Value = Session("Action")
                Bindata()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

            If chkLoginRequired.Checked Then
                txtLogin.Attributes.Add("Value", objInputXml.DocumentElement.SelectSingleNode("Employee").Attributes("Login").Value())
                txtPassword.Attributes.Add("Value", objInputXml.DocumentElement.SelectSingleNode("Employee").Attributes("Password").Value())
                txtRetypePassword.Attributes.Add("Value", objInputXml.DocumentElement.SelectSingleNode("Employee").Attributes("Password").Value())
            End If

            If txtIPAddress.Text <> "" Then
                txtIPAddress.CssClass = "textboxgrey"
                txtIPAddress.ReadOnly = True
            End If


        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            Session("EmployeeIP") = Nothing
            Session("ViewEmployeeData") = "0|0|0|0|0"
            Session("Action") = "I"
            hdSessionValue.Value = Session("Action")
            Bindata()
            Reset()
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                hdSessionValue.Value = Session("Action")
                ViewEmployee()
            Else
                Reset()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub Reset()

        Try
            txtEmployeeName.Text = String.Empty
            drpDesignation.Text = String.Empty
            txtMobileNumber.Text = String.Empty
            txtEmail.Text = String.Empty
            txtLogin.Text = String.Empty

            chkLoginRequired.Checked = True


            txtLogin.Attributes.Clear()
            txtRetypePassword.Attributes.Clear()
            txtPassword.Attributes.Clear()

            txtLogin.CssClass = "textbox"
            txtPassword.CssClass = "textbox"
            txtRetypePassword.CssClass = "textbox"

            txtLogin.Attributes.Add("Value", "")
            txtPassword.Attributes.Add("Value", "")
            txtRetypePassword.Attributes.Add("Value", "")

          

            txtDOJ.Text = String.Empty
            txtDOL.Text = String.Empty

            drpDepartment.SelectedIndex = 0
            drpAoffice.SelectedIndex = 0
            drpHOD.SelectedIndex = 0
            drpImmediateSuperVisor.SelectedIndex = 0
            drpFirstForm.SelectedIndex = 0
            drpRestrict.SelectedIndex = 0
            drpRegionList.SelectedIndex = 0
            drpCity.SelectedIndex = 0
            lblError.Text = ""
            ddlAgencyType.SelectedIndex = 0
            chkShowToSupervisor.Checked = False

            txtIPAddress.Attributes.Clear()
            txtIPAddress.Text = ""
            txtIPAddress.CssClass = "textbox"
            txtIPAddress.ReadOnly = False

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
End Class
