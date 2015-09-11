
Partial Class Test_Test1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'System.u()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim i As Integer
            i = 989089999
            Dim objxmlin As New System.Xml.XmlDocument

            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation

            objxmlin.LoadXml("<pp></pp>")

        Catch ex As Exception

        End Try

        Response.Write("abhishek")
    End Sub
End Class
