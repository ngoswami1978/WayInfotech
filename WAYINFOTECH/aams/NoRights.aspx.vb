
Partial Class NoRights
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal% ender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("RestrictedUser") = "Yes" Then
            Dim loginScript As String
            loginScript = "<script language='javascript'>" & _
           "window.location.href='SupportPages/TimeOutSession.aspx?LogOut=Logout'; " & _
           "</script>"

            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", loginScript)

        End If
    End Sub
End Class
