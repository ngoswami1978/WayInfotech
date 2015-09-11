'##############################################################
'############   Page Name -- Order_MSUP_CorporateCode #########
'############   Date 29-November 2007  ########################
'############   Developed By Abhishek  ########################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSUP_CorporateCode
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Not Page.IsPostBack) Then
                If (Request.QueryString("Action") = "U") Then
                    ViewCorporateCode()
                    txtCorporateQualifier.Focus()
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_CorporateCode.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

    End Sub

  
    Private Sub ViewCorporateCode()
        Try
            txtCorporationCode.ReadOnly = True
            txtCorporationCode.CssClass = "textboxgrey"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

End Class
