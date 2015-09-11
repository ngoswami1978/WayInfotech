
Partial Class Main
    Inherits System.Web.UI.Page
    Protected strMainFramePage As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("PageName") Is Nothing Then
                'If Not Session("LoginSession") Is Nothing Then
                '    If Not Session("LoginSession").ToString().Split("|").GetValue(2).ToString() = "" Then
                '        strMainFramePage = "Setup/" & Session("LoginSession").ToString().Split("|").GetValue(2).ToString() & ".aspx"
                '    Else
                '        strMainFramePage = "Setup/MSSR_Employee.aspx"
                '    End If
                'Else
                '    strMainFramePage = "Setup/MSSR_Employee.aspx"
                'End If
                strMainFramePage = "Welcome.aspx"
            Else
                strMainFramePage = Session("PageName")
            End If
            'Response.Redirect("Setup/MSSR_Employee.aspx")
        Catch ex As Exception

        End Try
    End Sub
End Class
