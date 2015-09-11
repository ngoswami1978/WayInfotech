
Partial Class Setup_MSUP_AG_BCase_Inc_PastPayment
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        If Not Page.IsPostBack Then
            Bindata()
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)

                LoadData(hdChainCode.Value)
                ' For Default Page Setting
                ' Response.Redirect("MSUP_AG_BCase_Inc_CPS.aspx?Action=U&Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting

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
        If e.Item.ItemIndex = 5 Then
            Button3.CssClass = "headingtab"
        End If
        Button3 = e.Item.FindControl("Button3")

        Button3.Attributes.Add("onclick", "return TabMethodAgencyIncentiveGroup('" & Button3.ClientID.ToString() & "',7);")


    End Sub

    Private Sub LoadData(ByVal strChainCode As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As New DataSet
        Dim objReadaer As XmlNodeReader
        Try

            objInputXml.LoadXml("<ISP_SEARCH_INPUT><CHAIN_CODE></CHAIN_CODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></ISP_SEARCH_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AMOUNT"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AMOUNT"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            ' @ Code For Setting Input Xml 


            ' objOutPutxml = objbzRequestBDR.Search(objInputXml)


            objOutPutxml.LoadXml("<PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT><BGROUPINCPASTPAID AMOUNT='' PERIOD='' SEGPAIDFOR='' GROSS='' CPS='' ></BGROUPINCPASTPAID><PAGE PAGE_COUNT='1' TOTAL_ROWS='5'></PAGE><Errors Status='FALSE'><Error Code='' Description='' /></Errors></PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT>")

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                GvPastPaid.DataSource = ds.Tables("BGROUPINCPASTPAID")
                GvPastPaid.DataBind()

               
            Else
                GvPastPaid.DataSource = Nothing
                GvPastPaid.DataBind()
                lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

             
            End If
        Catch ex As Exception

            lblError.Text = ex.Message
        End Try
    End Sub

End Class
