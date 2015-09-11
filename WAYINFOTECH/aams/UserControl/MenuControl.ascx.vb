
Partial Class UserControl_MenuControl
    Inherits System.Web.UI.UserControl
    Dim objeAAMS As New eAAMS

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Bindata()
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        TabText.Add("Detail")
        TabText.Add("CRS")
        TabText.Add("Competition")
        TabText.Add("Staff")
        TabText.Add("PC")
        TabText.Add("Misc. Hdw.")
        TabText.Add("Products")
        TabText.Add("Orders")
        TabText.Add("Notes")
        TabText.Add("Training")
        TabText.Add("FeedBack")
        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub
   
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        '###################################################################
        ' ############# Previous code Before Added By Abhishek
        '##########################################################
        'Dim Button1 As Button
        'Button1 = e.Item.FindControl("Button1")
        'If Request.QueryString("Id") = "" And Not Request.QueryString("Id") Is Nothing Then
        '    If e.Item.ItemIndex = 0 Then
        '        Button1.CssClass = "headingtab"
        '    End If
        'Else
        '    If e.Item.ItemIndex = Request.QueryString("Id") Then
        '        Button1.CssClass = "headingtab"
        '    End If
        'End If

        'Button1 = e.Item.FindControl("Button1")
        'Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',9);")
        '###################################################################
        ' ############# Previous code Before Added By Abhishek
        '##########################################################

        '###################################################################
        ' ############# New code After Added By Abhishek  '14/12/07
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


            If Session("Security") IsNot Nothing Then
                Dim strButtonText As String
                strButtonText = Button1.Text
                Select Case strButtonText
                    Case "Detail"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Details") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Case "CRS"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Crs Details") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Competition"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Competition Details") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Staff"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Staff") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "PC"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "PC Installation") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Misc. Hdw."
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Misc Hardware") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Products"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Agency product") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Orders"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Order") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "Notes"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Notes") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If


                    Case "Training"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "AgencyTraining") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If

                    Case "FeedBack"
                        If objeAAMS.ReturnViewPermission(Session("Security"), "AgencyFeedBack") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                End Select
            End If

            Button1 = e.Item.FindControl("Button1")
            If (Button1.Text.Trim() = "Competition") Then
                Button1.Width = "80"
            End If

            If (e.Item.ItemIndex > 0) Then
                Button1.Enabled = False
                If (Session("Action") IsNot Nothing) Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        Button1.Enabled = True
                        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',10);")
                    End If
                End If
            Else
                Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',10);")
            End If
            '###################################################################
            ' ############# End of New code After Added By Abhishek  '14/12/07
            '#################################################################
        Catch ex As Exception

        End Try
    End Sub
End Class
