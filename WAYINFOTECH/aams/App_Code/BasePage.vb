Imports Microsoft.VisualBasic
Imports System.IO
Imports System

Public MustInherit Class BasePage
    Inherits System.Web.UI.Page
    Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Dim viewState As String = Request.Form("__VSTATE")
        Dim bytes() As Byte = Convert.FromBase64String(viewState)
        bytes = CompressViewState.Decompress(bytes)
        Dim formatter As LosFormatter = New LosFormatter()
        Dim strDeCompress As String = Convert.ToBase64String(bytes)
        Return formatter.Deserialize(strDeCompress)
    End Function

    Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        Dim formatter As LosFormatter = New LosFormatter()
        Dim writer As StringWriter = New StringWriter()
        formatter.Serialize(writer, viewState)
        Dim viewStateString As String = writer.ToString()
        Dim bytes() As Byte = Convert.FromBase64String(viewStateString)
        bytes = CompressViewState.Compress(bytes)
        Dim strCompress As String = Convert.ToBase64String(bytes)
        ClientScript.RegisterHiddenField("__VSTATE", strCompress)
    End Sub
End Class
