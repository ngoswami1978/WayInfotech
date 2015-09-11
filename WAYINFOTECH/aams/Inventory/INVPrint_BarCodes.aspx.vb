
Partial Class Inventory_INVPrint_BarCodes
    Inherits System.Web.UI.Page
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("PrintBarCode") IsNot Nothing Then
            'Dim str As String = Session("PrintBarCode").ToString
            'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession()).
            PrintBarCodes.Text = Session("PrintBarCode").ToString
        End If
    End Sub
End Class
