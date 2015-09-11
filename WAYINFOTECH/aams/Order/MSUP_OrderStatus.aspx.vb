'##############################################################
'############   Page Name -- Order_MSUP_OrderStatus  ##########
'############   Date 29-November 2007  ########################
'############   Developed By Abhishek  ########################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSUP_OrderStatus
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_OrderStatus.aspx?Action=I")
    End Sub

   

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

    End Sub
End Class
