
Partial Class Order_MSSR_OnlineStatus
    Inherits System.Web.UI.Page

  G Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_OnlineStatus.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

    End Sub
End Class
