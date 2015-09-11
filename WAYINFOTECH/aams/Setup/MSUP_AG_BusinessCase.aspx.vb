
Partial Class Setup_MSUP_AG_BusinessCase
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
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

            ' BindSubTabdata()
           
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    'Sub BindSubTabdata()
    '    Dim TabText As New ArrayList()
    '    Try

    '        TabText.Add("Group MIDT")
    '        TabText.Add("Agency MIDT")
    '        TabText.Add("Connectivity")
    '        TabText.Add("Hardware")
    '        TabText.Add("Incentive")
    '        theTabSubGroupStrip.DataSource = TabText
    '        theTabSubGroupStrip.DataBind()

    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    End Try
    'End Sub


    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 6 Then
            Button1.CssClass = "headingtab"
        End If
        '@ Start of Code Added For Removing Tab On the basis of Right (Added By Abhishek)
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Agency Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Group") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "CRS Details"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group CRSDetails") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Competition"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Competition") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Staff"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Staff") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "PC"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group PC") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Contract"
                    'If objeAAMS.ReturnViewPermission(Session("Security"),"Group Contract") = "0" Then
                    Button1.CssClass = "displayNone"
                    ' End If
                Case "Business Case"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Business Case") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If
        '@ End of Code Added For Removing Tab On the basis of Right (Added By Abhishek)

        Button1 = e.Item.FindControl("Button1")

        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")


    End Sub

    'Protected Sub theTabSubGroupStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabSubGroupStrip.ItemDataBound
    '    Dim Button2 As Button
    '    Button2 = e.Item.FindControl("Button2")
    '    If e.Item.ItemIndex = 5 Then
    '        Button2.CssClass = "headingtab"
    '    End If
    '    Button2 = e.Item.FindControl("Button2")

    '    Button2.Attributes.Add("onclick", "return TabMethodAgencySubGroup('" & Button2.ClientID.ToString() & "',5);")


    'End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        If Not Page.IsPostBack Then
            Bindata()
            If Request.QueryString("Chain_Code") IsNot Nothing Then

                btnNew.Attributes.Add("onclick", "return NewFunction('" & Request.QueryString("Chain_Code").ToString & "');")



                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                ' For Default Page Setting
                ' Response.Redirect("MSSR_AG_BCase_Connectivity.aspx?Chain_Code=" + Request.QueryString("Chain_Code").ToString)
                ' For Default Page Setting

                LoadData(hdChainCode.Value)
            End If

        End If
    End Sub

    Private Sub LoadData(ByVal strChainCode As String)
        Dim objInputXml As New XmlDocument
        Dim objOutPutxml As New XmlDocument
        Dim ds As DataSet
        Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase

        Dim objReadaer As XmlNodeReader
        Try

            ' <INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><EmployeeID></EmployeeID></INC_SEARCH_BUSINESSCASE_INPUT>
            objInputXml.LoadXml("<INC_SEARCH_BUSINESSCASE_INPUT><BC_ID></BC_ID><CHAIN_CODE></CHAIN_CODE><BC_EFFECTIVE_FROM></BC_EFFECTIVE_FROM><BC_VALID_TILL></BC_VALID_TILL><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><EmployeeID></EmployeeID></INC_SEARCH_BUSINESSCASE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = strChainCode
            objInputXml.DocumentElement.SelectSingleNode("BC_EFFECTIVE_FROM").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("BC_VALID_TILL").InnerText = ""

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            'If ViewState("SortName") Is Nothing Then
            '    ViewState("SortName") = "BC_DTTI_EFFECTIVE_FROM"
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BC_DTTI_EFFECTIVE_FROM"
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            'End If

            'If ViewState("Desc") Is Nothing Then
            '    ViewState("Desc") = "FALSE"
            '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            'End If

            objOutPutxml = objbzBusinessCase.Search(objInputXml)

            'objOutPutxml.LoadXml("<INC_SEARCH_BUSINESSCASE_OUTPUT><BUSINESSCASE BC_ID='1' CHAIN_CODE='' GROUP_NAME=''  BC_DTTI_EFFECTIVE_FROM='1 Jan 2007' BC_DTTI_VALID_TILL='31 Dec 2007' INC_TYPE_NAME ='Fixed Deals' Active=''/><BUSINESSCASE BC_ID='1' CHAIN_CODE='' GROUP_NAME=''  BC_DTTI_EFFECTIVE_FROM='1 Jan 2008' BC_DTTI_VALID_TILL='31 Dec 2008' INC_TYPE_NAME='Fixed Deals' Active ='1' />	<Errors Status='False'>	<Error Code='' Description='' /></Errors></INC_SEARCH_BUSINESSCASE_OUTPUT>")

           
            If objOutPutxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ds = New DataSet
                objReadaer = New XmlNodeReader(objOutPutxml)
                ds.ReadXml(objReadaer)
                GvBCaseDeals.DataSource = ds.Tables("BUSINESSCASE")
                GvBCaseDeals.DataBind()
                DivFAColor.Visible = True
            Else
                GvBCaseDeals.DataSource = Nothing
                GvBCaseDeals.DataBind()
                DivFAColor.Visible = False
                ' lblError.Text = objOutPutxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
         
        Catch ex As Exception
            lblError.Text = ex.Message
            DivFAColor.Visible = False
        End Try
    End Sub
    Protected Sub GvBCaseDeals_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBCaseDeals.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                ' e.Row.Cells(0).Text = (e.Row.RowIndex + 1).ToString


                If Request.QueryString("Chain_Code") IsNot Nothing Then

                    Dim hdBCaseID As HiddenField
                    hdBCaseID = e.Row.FindControl("hdBCaseID")

                    Dim hdActive As HiddenField
                    hdActive = e.Row.FindControl("hdActive")

                    If hdActive.Value.Trim.ToUpper() = "TRUE" Then
                        e.Row.BackColor = Drawing.Color.LightSeaGreen
                        e.Row.ForeColor = Drawing.Color.Black
                    End If

                    Dim linkDetails As System.Web.UI.HtmlControls.HtmlAnchor
                    linkDetails = e.Row.FindControl("linkDetails")

                    Dim linkViewDoc As System.Web.UI.HtmlControls.HtmlAnchor
                    linkViewDoc = e.Row.FindControl("linkViewDoc")

                    Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
                    linkEdit = e.Row.FindControl("linkEdit")

                    Dim strBuilder As New StringBuilder
                    Dim objSecurityXml As New XmlDocument
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Count <> 0 Then
                            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Business Case']").Attributes("Value").Value)
                            If strBuilder(0) = "0" Then
                                linkDetails.Disabled = True
                            Else
                                linkDetails.Attributes.Add("onclick", "return DetailsFunction('" & hdBCaseID.Value & "','" & Request.QueryString("Chain_Code").ToString & "');")
                            End If

                            If strBuilder(2) = "0" Then
                                linkEdit.Disabled = True
                            Else
                                linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdBCaseID.Value & "','" & Request.QueryString("Chain_Code").ToString & "');")
                            End If

                        End If
                    Else
                        linkEdit.Disabled = False
                        linkDetails.Disabled = False
                        linkViewDoc.Disabled = False

                        strBuilder = objeAAMS.SecurityCheck(31)
                        linkDetails.Attributes.Add("onclick", "return DetailsFunction('" & hdBCaseID.Value & "','" & Request.QueryString("Chain_Code").ToString & "');")
                        linkViewDoc.Attributes.Add("onclick", "return ViewDocFunction('" & hdBCaseID.Value & "','" & Request.QueryString("Chain_Code").ToString & "');")
                        linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdBCaseID.Value & "','" & Request.QueryString("Chain_Code").ToString & "');")
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
