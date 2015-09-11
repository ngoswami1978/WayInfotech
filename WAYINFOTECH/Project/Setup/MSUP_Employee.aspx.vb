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
        txtEmployeeName.Attributes.Add("onkeypress", "allTextWithSpace()")
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
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='User Profile']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='User Profile']").Attributes("Value").Value)
                End If
                'When View rights disabled
                If strBuilder(0) = "0" Then
                    btnSave.Enabled = False
                    Response.Redirect("../NoRights.aspx")
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                    chkChangePass.CssClass = "displayNone"
                    trActive1.Visible = False
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

                

                If (strBuilder(1) <> "0" Or strBuilder(2) <> "0") And (Session("Action").ToString().Split("|").GetValue(0) = "U") Then
                    btnSave.Text = "Save"
                    btnSave.CommandName = "Save"

                    trActive1.Visible = True
                    'chkActive.Checked = True
                End If
               
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (Session("Action").ToString().Split("|").GetValue(0) = "U") Then
                    btnSave.Enabled = False
                End If
                If btnSave.Text = "Request" Then

                    trActive1.Visible = False
                End If
            Else
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
                
            Else
                btnSave.Text = "Save"
                btnSave.CommandName = "Save"
                trActive1.Visible = True
            End If

        End If
    End Sub
    Private Sub ViewEmployee()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New WAY.bizMaster.bzEmployee
        Try

            objInputXml.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call
            objOutputXml = objbzEmployee.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ViewEmployeeData") = Session("ViewEmployeeData").ToString().Remove(0, 1).Insert(0, 1)
                With objOutputXml.DocumentElement.SelectSingleNode("Employee")
                    txtEmployeeName.Text = .Attributes("Employee_name").Value()
                    txtMobileNumber.Text = .Attributes("Cell_phone").Value()
                    txtEmail.Text = .Attributes("Email").Value()
                    txtLogin.Text = .Attributes("Login").Value()
                    txtPassword.Text = .Attributes("Password").Value()
                    txtContactPerson.Text = .Attributes("ContactPersonName").Value()
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

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("User Profile")
            TabText.Add("Permission")

            If lblError.Text.Trim() = objeAAMSMessage.messInsert Then
                Dim objSecurityXml As New XmlDocument
                Dim strBuilder As New StringBuilder
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='User Profile']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='User Profile']").Attributes("Value").Value)
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
                Case "User Profile"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "User Profile") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Permission"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "User-Permission") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
            End Select
        End If

        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethodEmployeeNavigation('" & Button1.ClientID.ToString() & "',1);")
    End Sub

    Private Sub hideTabStrip(ByVal strIndex As String)
        Dim strScript As String = ""
        Select Case strIndex
            Case "0"
                strScript = "<script language='javascript'>" & _
                "document.getElementById('pnlEmployee').style.display ='block'; " & _
                "document.getElementById('pnlPermission').style.display  ='none';" & _
                "</script>"

            Case "1"
                strScript = "<script language='javascript'>" & _
               "document.getElementById('pnlEmployee').style.display ='none'; " & _
               "document.getElementById('pnlPermission').style.display  ='block';" & _
               "</script>"
        End Select
        ' ClientScript.RegisterStartupScript(Me.GetType(),"strScript", strScript)
    End Sub

    '**************************************************************************************************************
    'Save Employee Information
    '**************************************************************************************************************
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New WAY.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_EMPLOYEE_UPDATE_INPUT><Employee Action='' Cell_Phone='' Login='' Email='' EmployeeID='' Employee_name='' Password='' Firstform='' Changepassword='' Pwdexpire='' IPrestriction='' ContactPersonName='' /></MS_EMPLOYEE_UPDATE_INPUT>")

        Try
            ' Adding Email Id For New User Request For Sending Mail.
            If Session("Security") IsNot Nothing Then
                Dim objXmldocMail As New XmlDocument
                objXmldocMail.LoadXml(Session("Security").ToString)
            End If
            ' Security Input Xml End.

            With objInputXml.DocumentElement.SelectSingleNode("Employee")
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    .Attributes("EmployeeID").Value() = Session("Action").ToString().Split("|").GetValue(1)
                End If
                .Attributes("Cell_Phone").Value() = txtMobileNumber.Text
                .Attributes("Login").Value() = txtLogin.Text
                .Attributes("Email").Value() = txtEmail.Text
                .Attributes("Employee_name").Value() = txtEmployeeName.Text
                .Attributes("Password").Value() = txtPassword.Text
                .Attributes("ContactPersonName").Value() = txtContactPerson.Text
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
            txtMobileNumber.Text = String.Empty
            txtEmail.Text = String.Empty
            txtLogin.Text = String.Empty
            txtLogin.Attributes.Clear()
            txtRetypePassword.Attributes.Clear()
            txtPassword.Attributes.Clear()
            txtLogin.CssClass = "textbox"
            txtPassword.CssClass = "textbox"
            txtRetypePassword.CssClass = "textbox"
            txtLogin.Attributes.Add("Value", "")
            txtPassword.Attributes.Add("Value", "")
            txtRetypePassword.Attributes.Add("Value", "")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
End Class
