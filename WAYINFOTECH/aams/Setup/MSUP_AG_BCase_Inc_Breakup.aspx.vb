Imports System.Web.UI.Page


Partial Class Setup_MSUP_AG_BCase_Inc_Breakup
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'System.Globalization.


        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        txtIncentive.Attributes.Add("onkeyup", "return Data();")
        If Not Page.IsPostBack Then
            Bindata()
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
            End If
        End If
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Agency Group")
            TabText.Add("CRS Details")
            TabText.Add("Competition")
            TabText.Add("Staff")
            TabText.Add("PC")
            TabText.Add("Contract")
            TabText.Add("Business Case")

            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()

            BindSubTabdata()



        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Sub BindSubTabdata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Group MIDT")
            TabText.Add("Agency MIDT")
            TabText.Add("Connectivity")
            TabText.Add("Hardware")
            TabText.Add("Incentive")
            theTabSubGroupStrip.DataSource = TabText
            theTabSubGroupStrip.DataBind()
            BindIncentiveTabdata()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub BindIncentiveTabdata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("CPS")
            TabText.Add("Slab")
            TabText.Add("Fixed Incentive")
            TabText.Add("Commitments")
            TabText.Add("Breakup")
            TabText.Add("Past Payment")
            TabText.Add("Proposed Deal")
            theTabIncentiveGroupStrip.DataSource = TabText
            theTabIncentiveGroupStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 6 Then
            Button1.CssClass = "headingtab"
        End If
        Button1 = e.Item.FindControl("Button1")

        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")


    End Sub

    Protected Sub theTabSubGroupStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubGroupStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 4 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',5);")


    End Sub

    Protected Sub theTabIncentiveGroupStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabIncentiveGroupStrip.ItemDataBound
        Dim Button3 As Button
        Button3 = e.Item.FindControl("Button3")
        If e.Item.ItemIndex = 4 Then
            Button3.CssClass = "headingtab"
        End If
        Button3 = e.Item.FindControl("Button3")

        Button3.Attributes.Add("onclick", "return TabMethodAgencyIncentiveGroup('" & Button3.ClientID.ToString() & "',7);")


    End Sub



End Class
