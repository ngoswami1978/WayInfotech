
Partial Class SupportPages_TimeOutSession
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS

    Protected Sub linkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkLogin.Click
        Response.Redirect("../Login.aspx?Logout=True")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSecurityXml As New XmlDocument
        Dim rowIndex As Integer
        Dim primaryKey(1) As String

        If Request.QueryString("LogOut") = "Logout" Then

            '@ ########### Delte the Login Entry from Global Application Table
            'If Not IsNothing(Session("Security")) Then
            '    objSecurityXml.LoadXml(Session("Security"))
            '    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '        primaryKey(0) = objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText.ToString
            '        primaryKey(1) = Request.UserHostAddress

            '        rowIndex = CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows.IndexOf(CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows.Find(primaryKey))
            '        If rowIndex <> -1 Then
            '            CType(Application("LoginLog"), DataSet).Tables("UserLog").Rows(rowIndex).Delete()
            '            CType(Application("LoginLog"), DataSet).Tables("UserLog").AcceptChanges()
            '        End If
            '    End If
            'End If
            '@ End ########### Delte the Login Entry from Global Application Table


            Dim loginScript As String
            loginScript = "<script language='javascript'>" & _
           "parent.location.href='../Login.aspx?Logout=True'; " & _
           "</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", loginScript)

        End If
    End Sub
End Class
