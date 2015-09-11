
Partial Class AAMSHEAD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        If Session("Security") IsNot Nothing Then
                Dim xmlDoc As New XmlDocument
                xmlDoc.LoadXml(Session("Security"))
                lblUserName.Text = xmlDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText
            End If

            If ConfigurationManager.AppSettings("Mode") IsNot Nothing Then
                If ConfigurationManager.AppSettings("Mode") = "T" Then
                    tdHeader.Attributes.Add("style", "background:url(images/header_01_T.gif) no-repeat top left;height:44px")
                Else
                    tdHeader.Attributes.Add("style", "background:url(images/header_01.gif) no-repeat top left;height:44px")
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class
