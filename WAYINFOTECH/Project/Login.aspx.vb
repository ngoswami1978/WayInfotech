Imports System.Xml
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub imgLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogin.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzEmployee As New WAY.bizMaster.bzEmployee
            Dim objDataset As New DataSet
            Dim dview As New DataView
            Dim primaryKey As String = ""
            Dim objeAAMS As New eAAMS
            Dim strPageName As String = ""
            objInputXml.LoadXml("<MS_LOGIN_INPUT><Login></Login><Password></Password><IPAddress /></MS_LOGIN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Login").InnerText = txtUserId.Text
            objInputXml.DocumentElement.SelectSingleNode("Password").InnerText = txtPassword.Text
            Dim strIp As String
            strIp = Request.Params("HTTP_X_FORWARDED_FOR")
            If strIp = "" Or strIp Is Nothing Then
                strIp = Request.UserHostAddress
            End If
            objInputXml.DocumentElement.SelectSingleNode("IPAddress").InnerText = strIp

            'Back end Method Call
            objOutputXml = objbzEmployee.Login(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Session("Security") = objOutputXml.OuterXml
                Session("LoginSession") = objOutputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText & "|" & objOutputXml.DocumentElement.SelectSingleNode("Administrator").InnerText & "|" & objOutputXml.DocumentElement.SelectSingleNode("FirstForm").InnerText
                'strPageName = objeAAMS.EmployeePageName(objOutputXml.OuterXml)
                If strPageName.Trim = "" Then
                    Session("EmployeePageName") = "MSSR_Employee.aspx"
                Else
                    Session("EmployeePageName") = strPageName
                End If


                If objOutputXml.DocumentElement.SelectSingleNode("ChangePassword").InnerText = "True" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("FirstLoginDone").InnerText = "False" Or objOutputXml.DocumentElement.SelectSingleNode("ForceToChangePassword").InnerText = "True" Then
                        Response.Redirect("ChangPassword.aspx")
                    End If
                End If

                'Code to Disallow Multiple login
                If Application("dtUserApplicationDetails") Is Nothing Then
                    Dim dtUserApplicationDetails As New DataTable
                    dtUserApplicationDetails.Columns.Add("UserId", GetType(String))
                    dtUserApplicationDetails.Columns.Add("IPAddress", GetType(String))
                    dtUserApplicationDetails.Columns.Add("Password", GetType(String))
                    Dim dtclmn(1) As DataColumn
                    dtclmn(0) = dtUserApplicationDetails.Columns(0)
                    dtUserApplicationDetails.PrimaryKey = dtclmn
                    Dim drNewUser As DataRow
                    drNewUser = dtUserApplicationDetails.NewRow()
                    drNewUser("UserId") = txtUserId.Text
                    drNewUser("IPAddress") = strIp
                    drNewUser("Password") = txtPassword.Text
                    dtUserApplicationDetails.Rows.Add(drNewUser)
                    Session("UserId") = txtUserId.Text
                    Application("dtUserApplicationDetails") = dtUserApplicationDetails
                Else
                    Session("UserId") = txtUserId.Text
                    Dim dtUserApplicationDetails As New DataTable
                    dtUserApplicationDetails = CType(Application("dtUserApplicationDetails"), DataTable)
                    If dtUserApplicationDetails IsNot Nothing Then
                        Dim dr As DataRow = dtUserApplicationDetails.Rows.Find(txtUserId.Text)
                        If dr IsNot Nothing Then
                            If dr("IPAddress") <> strIp Then
                                dr("IPAddress") = strIp
                                dtUserApplicationDetails.AcceptChanges()
                                Application("dtUserApplicationDetails") = dtUserApplicationDetails
                            End If
                        Else
                            Dim drNewUser As DataRow
                            drNewUser = dtUserApplicationDetails.NewRow()
                            drNewUser("UserId") = txtUserId.Text
                            drNewUser("IPAddress") = strIp
                            drNewUser("Password") = txtPassword.Text
                            dtUserApplicationDetails.Rows.Add(drNewUser)
                            dtUserApplicationDetails.AcceptChanges()
                            Application("dtUserApplicationDetails") = dtUserApplicationDetails
                        End If
                    End If
                End If

                '@  ############## For Just To set Temporary Startup page Agency Search   ##############
                Dim strFirstPage As String
                'strFirstPage = objOutputXml.DocumentElement.SelectSingleNode("DisplayFirstForm/FirstFormDetails").Attributes("URL").Value
                strFirstPage = "setup/MSSR_Style.aspx"

                If strFirstPage.Split("/").Length >= 2 Then
                    If strFirstPage.Split("/").GetValue(1).ToString.ToUpper = "MSSR_EMPLOYEE.ASPX" Then
                        If strPageName.Trim <> "" Then
                            
                            strFirstPage = "Setup/" & strPageName
                        End If
                    End If
                End If
                If strFirstPage <> "" Then
                    Session("PageName") = strFirstPage
                Else
                    Session("PageName") = "Welcome.aspx"
                End If
                ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>window.location.href='main.aspx';</script>")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                If Request.QueryString("Logout") = "True" Then
                    Session.Abandon()
                    '    Page.RegisterClientScriptBlock("Open1", "<script language='javascript'>window.close();</script>")
                    'ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>window.close();</script>")
                End If
            End If

            If ConfigurationManager.AppSettings("Mode") IsNot Nothing Then
                If ConfigurationManager.AppSettings("Mode") = "T" Then
                    imgLoginSlice.ImageUrl = "images/login_slice_08_T.jpg"
                Else
                    imgLoginSlice.ImageUrl = "images/login_slice_08.jpg"
                End If
            End If

            Session("LeftBar") = Nothing
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
