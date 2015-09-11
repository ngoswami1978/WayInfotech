
Partial Class Setup_MSSR_BcaseDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try

            'TabText.Add("Agency Group")
            'TabText.Add("CRS Details")
            'TabText.Add("Competition")
            'TabText.Add("Staff")
            'TabText.Add("PC")
            'TabText.Add("Contract")
            'TabText.Add("Business Case")

            'theTabStrip.DataSource = TabText
            'theTabStrip.DataBind()

            BindSubTabdata()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Sub BindSubTabdata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Details")
            TabText.Add("Remarks")
          
            theTabSubStrip.DataSource = TabText
            theTabSubStrip.DataBind()

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

    Protected Sub theTabSubStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubStrip.ItemDataBound
        Dim Button2 As Button
        Button2 = e.Item.FindControl("Button2")
        If e.Item.ItemIndex = 2 Then
            Button2.CssClass = "headingtab"
        End If
        Button2 = e.Item.FindControl("Button2")

        Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',2);")


    End Sub



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
                ' For Default Page Setting
                ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    LoadData(hdChainCode.Value, Request.QueryString("BCaseID").ToString)
                End If

            End If

        End If
    End Sub

    Private Sub LoadData(ByVal strChainCode As String, ByVal strBcaseId As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objReadaer As XmlNodeReader
        Try

            objInputXml.LoadXml("<INC_VIEW_BUSINESSCASE_INPUT>	<BC_ID></BC_ID></INC_VIEW_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = strBcaseId

            ' objOutPutxml = objbzRequestBDR.Search(objInputXml)     


            Dim str As String = "<INC_VIEW_BUSINESSCASE_OUTPUT>"
            str &= "<BUSINESSCASE BC_ID='' CHAIN_CODE='' GROUP_NAME='' BC_DTTI_EFFECTIVE_FROM='' BC_DTTI_VALID_TILL='' AccountManager='' AccountManager_Name='' Region='' PAYMENT_CYCLE_ID='' APPROVERS=''/>"
            str &= "<BC_MIDT_GROUP LASTAVAIL='2008 (AVG PAST 3 MNTHS)'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            str &= "<BC_MIDT_GROUP LASTAVAIL='2007'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            str &= "<BC_MIDT_GROUP LASTAVAIL='2006'  A='' B='' G='' P='' W='' TOTAL='' A_PER='' B_PER='' G_PER='' P_PER='' W_PER=''/>"
            str &= "<BC_MIDT_AGENCY Name='uer ewkr erk erewuirer ewri ewrui eruiew ewrui ereuier ew' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            str &= "<BC_MIDT_AGENCY Name='' OfficeID='' Address=''  A='' B='' G='' P='' W='' TTP=''/>"
            str &= "<BC_PLAN>"
            str &= "<INC_TYPE_NAME></INC_TYPE_NAME>"
            str &= "<CASE INC_PLAN_ID='' INC_TYPE_NAME=''>"
            str &= "<PLAN_DETAILS SLABS_START='' SLABS_END='' SLABS_RATE=''/>"
            str &= "<NIDT_FIELDS_ID/>"
            str &= "</CASE >"
            str &= "</BC_PLAN>"
            str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT='' TOTAL='' />"
            str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT=''  TOTAL='' />"
            str &= "<BC_CONN BC_ONLINE_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_ONLINE_CATG_COST='' CONN_COUNT=''  TOTAL='' />"
            str &= "<BC_EQP BC_EQP_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            str &= "<BC_EQP BC_EQP_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            str &= "<BC_EQP BC_EQP_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            str &= "<BC_EQP BC_EQP_CATG_ID='' BC_ONLINE_CATG_NAME='' BC_EQP_CATG_COST='' PRODUCT_COUNT=''  TOTAL='' />"
            str &= "<BC_MIDT_BRK TYPE='' TOTAL='' PER='' LESS='' RATE='' AMT=''/>"
            str &= "<Errors Status='False'>"
            str &= "<Error Code='' Description='' />"
            str &= "</Errors>"
            str &= "</INC_VIEW_BUSINESSCASE_OUTPUT>"


            ' objOutPutxml.LoadXml("<PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT><BGROUPMIDT LASTAVAIL='2008 (AVG PAST 3 MNTHS)  ' A='' B='' G='' P='' W='' TOTAL=''  APER='' BPER='' GPER='' PPER='' WPER=''></BGROUPMIDT><BGROUPMIDT LASTAVAIL='2007  ' A='' B='' G='' P='' W='' TOTAL=''  APER='' BPER='' GPER='' PPER='' WPER=''></BGROUPMIDT><BGROUPMIDT LASTAVAIL='2006  ' A='' B='' G='' P='' W='' TOTAL=''  APER='' BPER='' GPER='' PPER='' WPER=''></BGROUPMIDT><PAGE PAGE_COUNT='1' TOTAL_ROWS='3'></PAGE><Errors Status='FALSE'><Error Code='' Description='' /></Errors></PR_SEARCH_BGROUPAGENCYMIDT_OUTPUT>")
            objOutPutxml.LoadXml(str)

            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)

                If ds.Tables("BUSINESSCASE") IsNot Nothing Then
                    '<BUSINESSCASE BC_ID='' CHAIN_CODE=''
                    'GROUP_NAME='' BC_DTTI_EFFECTIVE_FROM='' BC_DTTI_VALID_TILL='' 
                    'AccountManager='' AccountManager_Name='' Region='' PAYMENT_CYCLE_ID='' APPROVERS=''/>"
                    txtGroupName.Text = ds.Tables("BUSINESSCASE").Rows(0)("GROUP_NAME").ToString
                    txtActManager.Text = ds.Tables("BUSINESSCASE").Rows(0)("AccountManager_Name").ToString
                    txtBillCycle.Text = ds.Tables("BUSINESSCASE").Rows(0)("PAYMENT_CYCLE_ID").ToString
                    txtChainCode.Text = ds.Tables("BUSINESSCASE").Rows(0)("CHAIN_CODE").ToString
                    '  txtIATANO.Text = ds.Tables("BUSINESSCASE").Rows(0)("").ToString
                    txtRegion.Text = ds.Tables("BUSINESSCASE").Rows(0)("Region").ToString
                    txtContractPeriod.Text = ds.Tables("BUSINESSCASE").Rows(0)("BC_DTTI_EFFECTIVE_FROM").ToString + " - " + ds.Tables("BUSINESSCASE").Rows(0)("BC_DTTI_VALID_TILL").ToString
                    '   ListBox1.Text = ds.Tables("BUSINESSCASE").Rows(0)("").ToString
                    LstApprovers.Text = ds.Tables("BUSINESSCASE").Rows(0)("APPROVERS").ToString





                End If

                If ds.Tables("BC_MIDT_GROUP") IsNot Nothing Then
                    GvBGroupMIDT.DataSource = ds.Tables("BC_MIDT_GROUP")
                    GvBGroupMIDT.DataBind()
                Else
                    GvBGroupMIDT.DataSource = Nothing
                    GvBGroupMIDT.DataBind()
                End If

                If ds.Tables("BC_MIDT_AGENCY") IsNot Nothing Then
                    GvBAgencyMIDT.DataSource = ds.Tables("BC_MIDT_AGENCY")
                    GvBAgencyMIDT.DataBind()
                Else

                    GvBAgencyMIDT.DataSource = Nothing
                    GvBAgencyMIDT.DataBind()
                End If

                If ds.Tables("BC_CONN") IsNot Nothing Then
                    GvConnectivity.DataSource = ds.Tables("BC_CONN")
                    GvConnectivity.DataBind()
                Else
                    GvConnectivity.DataSource = Nothing
                    GvConnectivity.DataBind()
                End If

                If ds.Tables("BC_EQP") IsNot Nothing Then
                    GvHardware.DataSource = ds.Tables("BC_EQP")
                    GvHardware.DataBind()
                Else
                    GvHardware.DataSource = Nothing
                    GvHardware.DataBind()
                End If

            Else
                GvBGroupMIDT.DataSource = Nothing
                GvBGroupMIDT.DataBind()
                GvBAgencyMIDT.DataSource = Nothing
                GvBAgencyMIDT.DataBind()
                GvConnectivity.DataSource = Nothing
                GvConnectivity.DataBind()
                GvHardware.DataSource = Nothing
                GvHardware.DataBind()
                '   lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


         
        Catch ex As Exception

            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub GvBAgencyMIDT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBAgencyMIDT.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lblSNo As Label = CType(e.Row.FindControl("lblSNo"), Label)
                lblSNo.Text = (e.Row.RowIndex + 1).ToString

            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(3).Text = "Total"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
                e.Row.Cells(4).Text = "0"
                e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(5).Text = "0"
                e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(6).Text = "0"
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(7).Text = "0"
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(8).Text = "0"
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(9).Text = "0"
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GvHardware_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvHardware.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.Footer Then

                e.Row.Cells(2).Text = "Total"
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(3).Text = "0"
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
                'e.Row.Cells(4).Text = "0"
                'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

            End If
        Catch ex As Exception

        End Try

    End Sub
End Class
