Imports System.Xml
Imports System.Data
Partial Class TravelAgency_MSSR_Order
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            txtAgencyName.Attributes.Add("onblur", "return OrderWholeGroup()")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                objeAAMS.BindDropDown(ddlCountry, "COUNTRY", True, 3)
                objeAAMS.BindDropDown(ddlCity, "CITY", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(ddlOrderStatus, "ORDERSTATUS", True, 3)
                BindOrderType("2")
                objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            End If
           
            If Not Page.IsPostBack Then
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtOrderNumber.Text = Session("Action").ToString().Split("|").GetValue(2)
                        ddlOrderType.SelectedValue = Session("Action").ToString().Split("|").GetValue(3)
                        ddlOrderStatus.SelectedValue = Session("Action").ToString().Split("|").GetValue(4)
                        ddlRegion.SelectedValue = Session("Action").ToString().Split("|").GetValue(5)
                        txtAgencyName.Text = Session("Action").ToString().Split("|").GetValue(21)
                        chbWholeGroup.Checked = IIf(Session("Action").ToString().Split("|").GetValue(7) = "1", True, False)
                        ddlCity.SelectedValue = Session("Action").ToString().Split("|").GetValue(8)
                        ddlCountry.SelectedValue = Session("Action").ToString().Split("|").GetValue(9)
                        txtMessageFrom.Text = Session("Action").ToString().Split("|").GetValue(10)
                        txtMessageTo.Text = Session("Action").ToString().Split("|").GetValue(11)
                        txtApprovalFrom.Text = Session("Action").ToString().Split("|").GetValue(12)
                        txtApprovalTo.Text = Session("Action").ToString().Split("|").GetValue(13)
                        txtReceivedFrom.Text = Session("Action").ToString().Split("|").GetValue(14)
                        txtReceivedTo.Text = Session("Action").ToString().Split("|").GetValue(15)
                        txtProcessedFrom.Text = Session("Action").ToString().Split("|").GetValue(16)
                        txtProcessedTo.Text = Session("Action").ToString().Split("|").GetValue(17)
                        txtSentFrom.Text = Session("Action").ToString().Split("|").GetValue(18)
                        txtSentTo.Text = Session("Action").ToString().Split("|").GetValue(19)
                        hdAgencyNameId.Value = Session("Action").ToString().Split("|").GetValue(20)
                        OrderSearch(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
            '***************************************************************************************

            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        OrderDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            '*********************Security Segment**************************************************
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '*********************End of Security Segment*****************************************************

            ' Code for Delete 
            If (hdOrderID.Value <> "") Then
                OrderDelete(hdOrderID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindOrderType(ByVal str As String)
        Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
        Dim objOutputXml As New XmlDocument
        Dim objInputXml As New XmlDocument
        objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = str
        Dim objNodeList As XmlNodeList
        objOutputXml = objbzOrderType.List(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
            For Each objXmlNode As XmlNode In objNodeList
                ddlOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
            Next
        End If
        ddlOrderType.Items.Insert(0, New ListItem("--All--", ""))
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Order.aspx?Action=I|")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        OrderSearch(PageOperation.Search)
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_Order.aspx")
    End Sub
    Sub OrderSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            'objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></UP_SEARCHORDER_INPUT>")

            objInputXml.LoadXml("<UP_SEARCHORDER_INPUT>	<LCODE />	<ORDER_NUMBER />	<ORDERTYPEID />	<ORDERSTATUSID />	<REGION />	<AGENCYNAME />	<GROUPDATA />	<City />	<Country />	<MSG_SEND_DATE_FROM />	<SENDBACK_DATE_FROM />	<APPROVAL_DATE_FROM />	<RECEIVED_DATE_FROM />	<PROCESSED_DATE_FROM />	<MSG_SEND_DATE_TO />	<SENDBACK_DATE_TO />	<APPROVAL_DATE_TO />	<RECEIVED_DATE_TO />	<PROCESSED_DATE_TO />	<Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><COMP_VERTICAL></COMP_VERTICAL></UP_SEARCHORDER_INPUT>")

            With objInputXml.DocumentElement
                If Request.Form("txtAgencyName") = "" Then
                    .SelectSingleNode("LCODE").InnerText = ""
                Else
                    .SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
                End If

                .SelectSingleNode("ORDER_NUMBER").InnerText = txtOrderNumber.Text.Trim()
                .SelectSingleNode("ORDERTYPEID").InnerText = ddlOrderType.SelectedValue.Trim()
                .SelectSingleNode("ORDERSTATUSID").InnerText = ddlOrderStatus.SelectedValue.Trim()
                .SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue.Trim()
                .SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
                If Request.Form("txtAgencyName") = "" Then
                    .SelectSingleNode("GROUPDATA").InnerText = "0"
                Else
                    .SelectSingleNode("GROUPDATA").InnerText = IIf(chbWholeGroup.Checked, "1", "0")
                End If
                If ddlCity.SelectedIndex = 0 Then
                    .SelectSingleNode("City").InnerText = ""
                Else
                    .SelectSingleNode("City").InnerText = ddlCity.SelectedItem.Text.Trim()

                End If

                If ddlCountry.SelectedIndex = 0 Then
                    .SelectSingleNode("Country").InnerText = ""
                Else
                    .SelectSingleNode("Country").InnerText = ddlCountry.SelectedItem.Text.Trim()
                End If

                '.SelectSingleNode("Country").InnerText = ddlCountry.SelectedItem.Text
                .SelectSingleNode("MSG_SEND_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtMessageFrom.Text.Trim())
                .SelectSingleNode("MSG_SEND_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtMessageTo.Text.Trim())
                .SelectSingleNode("APPROVAL_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtApprovalFrom.Text.Trim())
                .SelectSingleNode("APPROVAL_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtApprovalTo.Text.Trim())
                .SelectSingleNode("RECEIVED_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtReceivedFrom.Text.Trim())
                .SelectSingleNode("RECEIVED_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtReceivedTo.Text.Trim())
                .SelectSingleNode("PROCESSED_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtProcessedFrom.Text.Trim())
                .SelectSingleNode("PROCESSED_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtProcessedTo.Text.Trim())
                .SelectSingleNode("SENDBACK_DATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtSentFrom.Text.Trim())
                .SelectSingleNode("SENDBACK_DATE_TO").InnerText = objeAAMS.ConvertTextDate(txtSentTo.Text.Trim())

                'Dim objSecurityXml As New XmlDocument
                'objSecurityXml.LoadXml(Session("Security"))

                '.SelectSingleNode("Limited_To_OwnAagency").InnerText = "" ' hdAgencyNameId.Value
                '.SelectSingleNode("Limited_To_Aoffice").InnerText = "" 'objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                '.SelectSingleNode("Limited_To_Region").InnerText = "" 'objS;ecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText

                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0) ' "1" 'xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion
                Else
                    .SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If


                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If



                If Not Session("LoginSession") Is Nothing Then
                    .SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If

            End With
            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then

                If ViewState("PrevSearching") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AGENCYNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AGENCYNAME" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting

            'Here Back end Method Call
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            objOutputXml = objbzOrder.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvOrder.DataSource = ds.Tables("ORDERS").DefaultView
                gvOrder.DataBind()
                lblError.Text = ""
                'Code Added For Paging And Sorting In case Of Delete The Record

                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
                If count <> ddlPageNumber.Items.Count Then
                    ddlPageNumber.Items.Clear()
                    For i As Integer = 1 To count
                        ddlPageNumber.Items.Add(i.ToString)
                    Next
                End If
                ddlPageNumber.SelectedValue = selectedValue
                'Code for hiding prev and next button based on count
                If count = 1 Then
                    'pnlPaging.Visible = False
                    ' ddlPageNumber.Visible = False
                    lnkNext.Visible = False
                    lnkPrev.Visible = False
                Else
                    'ddlPageNumber.Visible = True
                    lnkPrev.Visible = True
                    lnkNext.Visible = True
                End If

                'Code for hiding next button when pagenumber is equal to page count
                If ddlPageNumber.SelectedValue = count.ToString Then
                    lnkNext.Visible = False
                Else
                    lnkNext.Visible = True
                End If

                'Code for hiding prev button when pagenumber is 1
                If ddlPageNumber.SelectedValue = "1" Then
                    lnkPrev.Visible = False
                Else
                    lnkPrev.Visible = True
                End If
                hdRecordOnCurrentPage.Value = ds.Tables("ORDERS").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "AGENCYNAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "ADDRESS"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "Region"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select

                    Case "COMP_VERTICAL"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select

                    Case "CITY"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "COUNTRY"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select
                    Case "ORDER_NUMBER"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select


                    Case "ORDER_TYPE_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                    Case "ORDER_STATUS_NAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "APPROVAL_DATE"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select
                    Case "RECEIVED_DATE"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select
                    Case "ODDICEID"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select
                    Case "MSG_SEND_DATE"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(12).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(12).Controls.Add(imgDown)

                        End Select


                    Case "PENDINGWITHNAME"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(13).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(13).Controls.Add(imgDown)
                        End Select
                    Case "INSTALLATION_DUE_DATE"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(14).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(14).Controls.Add(imgDown)
                        End Select
                    Case "RECEIVED_DATE_MKT"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(15).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(15).Controls.Add(imgDown)

                        End Select

                    Case "REMARKS"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvOrder.HeaderRow.Cells(16).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOrder.HeaderRow.Cells(16).Controls.Add(imgDown)

                        End Select

                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvOrder.DataSource = ds.Tables("ORDERS").DefaultView
                gvOrder.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub OrderDelete(ByVal OrderTypeID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzOrder As New AAMS.bizTravelAgency.bzOrder
            objInputXml.LoadXml("<MS_DELETEORDER_INPUT>	<ORDERID/></MS_DELETEORDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDERID").InnerXml = OrderTypeID
            hdOrderID.Value = ""
            'Here Back end Method Call
            objOutputXml = objbzOrder.Delete(objInputXml)
            OrderSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' Session("Action") = Request.QueryString("Action")

                lblError.Text = objeAAMSMessage.messDelete
                'Response.Redirect("MSSR_ORDER.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim s As String
        Dim hdORDERID As New HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdORDERID = e.Row.FindControl("hdORDERID")
        Dim cityval As New HiddenField
        cityval = e.Row.FindControl("hdCITY")
        'Dim b1 As New BoundField
        s = hdORDERID.Value & "|" & cityval.Value
        s = objED.Encrypt(hdORDERID.Value) & "|" & cityval.Value
        If hdORDERID.Value = "" Then
            e.Row.Cells.Clear()
            Exit Sub
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Disabled = True
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionOrderMain('" & hdORDERID.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Disabled = True
            'Else
            '    linkEdit.Attributes.Add("onclick", "return EditFunctionOrderMain('" & s & "');")

            'End If
            linkEdit.Attributes.Add("onclick", "return EditFunctionOrderMain('" & s & "');")
        Else
            'linkEdit.Attributes.Add("onclick", "return EditFunctionOrderMain('" & hdORDERID.Value & "');")

            linkEdit.Attributes.Add("onclick", "return EditFunctionOrderMain('" & s & "' );")
            linkDelete.Attributes.Add("onclick", "return DeleteFunctionOrderMain('" & hdORDERID.Value & "');")
        End If
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            OrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            OrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            OrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvOrder.Sorting
        Try


            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            OrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            OrderSearch(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        '        <ORDERS ORDERID="" AGENCYNAME="" ADDRESS="" ADDRESS1="" Region="NORTH" CITY="" COUNTRY="" 
        'ORDER_NUMBER="" ORDER_TYPE_NAME="" ORDER_STATUS_NAME="" APPROVAL_DATE="" 
        'RECEIVED_DATE="" ODDICEID="" OFFICEID1="" MSG_SEND_DATE="" 
        'INSTALLATION_DUE_DATE="" PENDINGWITHNAME="" RECEIVED_DATE_MKT="" APC="0" RESEND_DATE_MKT="" /> 

        'Dim strArray() As String = {"Agency Name", "Address", "Region", "City", "Country", "Order Number", "Order Type", "Order Status", "Approval Date", "Receive", "Office ID", "Message Send Date", "Pending With Employees", "Installation Due Date", "Receiving", "Remarks"}
        'Dim intArray() As Integer = {1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 15, 17, 20}

        Dim strArray() As String = {"Agency Name", "Address", "Region", "Company Vertical", "City", "Country", "Order Number", "Order Type", "Order Status", "Approval Date", "Receive", "Office ID", "Message Send Date", "Pending With Employees", "Installation Due Date", "Receiving", "Remarks"}
        Dim intArray() As Integer = {1, 2, 4, 21, 5, 6, 7, 8, 9, 10, 11, 12, 14, 16, 15, 17, 20}

        objExport.ExportDetails(objOutputXml, "ORDERS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportORDER.xls")
    End Sub
End Class
