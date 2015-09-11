
Partial Class UserControl_ProductMenu
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bindata()
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        TabText.Add("Details")
        TabText.Add("Mailing List")
        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
      
        '###################################################################
        ' #############  By Abhishek  '15/01/08
        '#################################################################
        Try


            Dim Button1 As Button
            Button1 = e.Item.FindControl("Button1")
            If Request.QueryString("Id") = "" And Not Request.QueryString("Id") Is Nothing Then
                If e.Item.ItemIndex = 0 Then
                    Button1.CssClass = "headingtab"
                End If
            Else
                If e.Item.ItemIndex = Request.QueryString("Id") Then
                    Button1.CssClass = "headingtab"
                End If
            End If

            Button1 = e.Item.FindControl("Button1")
            If (e.Item.ItemIndex > 0) Then
                Button1.Enabled = False
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    Button1.Enabled = True
                    Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2,'Action=" & Request.QueryString("Action") & "');")
                End If

            Else
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2,'Action=" & Request.QueryString("Action") & "');")
                Else
                    Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',2,'Action=I|'" & ");")
                End If

            End If
            '###################################################################
            ' ############# End of New code After Added By Abhishek  '15/01/08
            '#################################################################
        Catch ex As Exception

        End Try
    End Sub
End Class
