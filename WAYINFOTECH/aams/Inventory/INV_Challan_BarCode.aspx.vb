
Partial Class Inventory_INV_Challan_BarCode
    Inherits System.Web.UI.PageG 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim str As String = "asasa"
        'Session("BCSCRIPT") = str
        If Session("BCSCRIPT") IsNot Nothing Then
            lbldata.Text = Session("BCSCRIPT")
        Else
            lbldata.Text = ""
        End If
    End Sub
End Class
