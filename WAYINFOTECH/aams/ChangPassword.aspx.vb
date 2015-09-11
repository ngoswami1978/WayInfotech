
Partial Class ChangPassword
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Dim objSessionXml As New XmlDocument
        objSessionXml.LoadXml(Session("Security").ToString())
        objInputXml.LoadXml("<MS_CHANGEPASSWORD_INPUT><EmployeeID></EmployeeID><OldPassword></OldPassword><NewPassword></NewPassword></MS_CHANGEPASSWORD_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objSessionXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText
        objInputXml.DocumentElement.SelectSingleNode("OldPassword").InnerText = txtOldPassword.Text
        objInputXml.DocumentElement.SelectSingleNode("NewPassword").InnerText = txtNewPassword.Text
        'Here Back end Method Call
        objOutputXml = objbzEmployee.ChangePassword(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messPassword
            Session("PageName") = "TravelAgency/TASR_Agency.aspx"
            '@  ############## End of For Just To set Temporary Statrup page Agency Search   ##############
            Dim count As Integer = 0
            Response.Write("Thank! You are soon Redirected to Main Page")
            For count = 0 To 10000000

            Next
            Response.Redirect("Main.aspx")
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("PageName") = "ChangPassword.aspx"
        btnChange.Attributes.Add("onclick", "return CheckNewConfirmPassword();")
        btnReset.Attributes.Add("onclick", "return ConfirmPasswordReset();")
        '  Session("PageName") = "Setup/MSSR_Aoffice.aspx"
        objeAAMS.ExpirePageCache()
        If Session("Security") Is Nothing Then
            Dim loginScript As String = ""
            loginScript = " <noscript><meta http-equiv='REFRESH' content='1; URL=../Information.aspx' /> </noscript><script language='javascript'>" & _
           "parent.location.href='SupportPages/TimeOutSession.aspx?Logout=True'; " & _
           "</script>"

            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", loginScript)
            Exit Sub
        End If
    End Sub
End Class
