
Partial Class UserControl_IncAGControl
    Inherits System.Web.UI.UserControl
    Dim objeAAMS As New eAAMS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bindata()
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        TabText.Add("Agency Group")
        TabText.Add("CRS Details")
        TabText.Add("Competition")
        TabText.Add("Staff")
        TabText.Add("PC")
        TabText.Add("Contract")
        TabText.Add("Business Case")
        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound

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
            If Session("Security") IsNot Nothing Then
                Dim strButtonText As String
                strButtonText = Button1.Text
                Select Case strButtonText
                    'Case "Agency Group"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Details") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If
                    'Case "CRS Details"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Crs Details") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If

                    'Case "Competition"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Competition Details") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If

                    'Case "Staff"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Staff") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If

                    'Case "PC"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "PC Installation") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If

                    'Case "Contract"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Misc Hardware") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If

                    'Case "Business Case"
                    '    If objeAAMS.ReturnViewPermission(Session("Security"), "Product") = "0" Then
                    '        Button1.CssClass = "displayNone"
                    '    End If
                End Select
            End If

            Button1 = e.Item.FindControl("Button1")

            If (e.Item.ItemIndex > 0) Then
                Button1.Enabled = False
                If (Session("Action") IsNot Nothing) Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        Button1.Enabled = True
                        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',7);")
                    End If
                End If
            Else
                Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',7);")
            End If
          
        Catch ex As Exception
        End Try
    End Sub
End Class
