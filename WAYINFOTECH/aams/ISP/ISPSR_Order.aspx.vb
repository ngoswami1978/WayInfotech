

Partial Class ISP_ISPSR_Order
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session("PageName") = Request.Url.ToString()
        Try
            lblError.Text = ""
            objeAAMS.ExpirePageCache()
            btnSearch.Attributes.Add("onclick", "return CheckValidationIspOrder();")
            ' btnReset.Attributes.Add("onclick", "return ISPOrderReset();")
            btnNew.Attributes.Add("onclick", "return NewFunctionIspOrder();")

            ddlCity.Attributes.Add("onkeyup", "return gotop('ddlCity');")
            ddlCountry.Attributes.Add("onkeyup", "return gotop('ddlCountry');")
            ddlApprovedBy.Attributes.Add("onkeyup", "return gotop('ddlApprovedBy');")
            ddlOrderType.Attributes.Add("onkeyup", "return gotop('ddlOrderType');")
            ddlIspOrderStatus.Attributes.Add("onkeyup", "return gotop('ddlIspOrderStatus');")
            ddlLoggedBy.Attributes.Add("onkeyup", "return gotop('ddlLoggedBy');")

            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        ' btnExport.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                hdAdvanceSearch.Value = "0" '1
                BindAllControl()
             
               
            End If
            If hdDeleteISPOrderID.Value <> "" Then
                ISPOrderDelete(hdDeleteISPOrderID.Value)
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ISPOrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    'Method for search Agency
    Private Sub ISPOrderSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objNode As XmlNode
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
        Try
            'objInputXml.LoadXml("<ISP_SEARCH_INPUT><AgencyName></AgencyName><City></City><Country></Country><ISPPlanId></ISPPlanId><OrderNumber></OrderNumber><ISPOrderStatus></ISPOrderStatus><ApprovedbyId></ApprovedbyId><LoggedbyId></LoggedbyId><CAFAcId></CAFAcId><MDNNumber></MDNNumber><OrderdateFrom></OrderdateFrom><OrderdateTo></OrderdateTo><OnlineDateFrom></OnlineDateFrom><OnlineDateTo></OnlineDateTo><ExpectedDateFrom></ExpectedDateFrom><ExpectedDateTo></ExpectedDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></ISP_SEARCH_INPUT>")

            objInputXml.LoadXml("<ISP_SEARCH_INPUT><AgencyName></AgencyName><City></City><Country></Country><ISPPlanId></ISPPlanId><OrderNumber></OrderNumber><ISPOrderStatus></ISPOrderStatus><ApprovedbyId></ApprovedbyId><LoggedbyId></LoggedbyId><CAFAcId></CAFAcId><MDNNumber></MDNNumber><OrderdateFrom></OrderdateFrom><OrderdateTo></OrderdateTo><OnlineDateFrom></OnlineDateFrom><OnlineDateTo></OnlineDateTo><ExpectedDateFrom></ExpectedDateFrom><ExpectedDateTo></ExpectedDateTo><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/><OrderTypeID></OrderTypeID></ISP_SEARCH_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = Trim(txtAgencyName.Text)
            'objInputXml.DocumentElement.SelectSingleNode("CityId").InnerText = Trim(txtShortName.Text)


            If (ddlOrderType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("OrderTypeID").InnerText = ddlOrderType.SelectedValue
            End If


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            If (ddlCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City").InnerText = Trim(ddlCity.SelectedItem.Text)
            End If
            If (ddlCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Country").InnerText = Trim(ddlCountry.SelectedItem.Text)
            End If

            'If Request("txtNpid") IsNot Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("ISPPlanId").InnerText = Request("txtNpid") 'hdISPid.Value.Trim()
            'ElseIf txtNpid.Text.Trim().Length > 0 Then
            '    objInputXml.DocumentElement.SelectSingleNode("ISPPlanId").InnerText = txtNpid.Text 'Request("txtNpid") 'hdISPid.Value.Trim()
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("ISPPlanId").InnerText = ""
            'End If
            ' txtNpid.Text = objInputXml.DocumentElement.SelectSingleNode("ISPPlanId").InnerText

            objInputXml.DocumentElement.SelectSingleNode("ISPPlanId").InnerText = hdIspPlanId.Value


            objInputXml.DocumentElement.SelectSingleNode("OrderNumber").InnerText = txtOrderNumber.Text
            If ddlIspOrderStatus.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("ISPOrderStatus").InnerText = ddlIspOrderStatus.SelectedValue  ' Trim(txtOfficeId.Text)
            End If
            If (ddlApprovedBy.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("ApprovedbyId").InnerText = ddlApprovedBy.SelectedValue
            End If
            If (ddlLoggedBy.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("LoggedbyId").InnerText = ddlLoggedBy.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("CAFAcId").InnerText = txtCafNo.Text
            objInputXml.DocumentElement.SelectSingleNode("MDNNumber").InnerText = txtMDNNo.Text



            If (hdAdvanceSearch.Value = "1") Then
                objInputXml.DocumentElement.SelectSingleNode("OrderdateFrom").InnerText = objeAAMS.ConvertTextDate(txtOrderDateFrom.Text)
                objInputXml.DocumentElement.SelectSingleNode("OrderdateTo").InnerText = objeAAMS.ConvertTextDate(txtOrderDateTo.Text)
                objInputXml.DocumentElement.SelectSingleNode("OnlineDateFrom").InnerText = objeAAMS.ConvertTextDate(txtOnlineDateFrom.Text)
                objInputXml.DocumentElement.SelectSingleNode("OnlineDateTo").InnerText = objeAAMS.ConvertTextDate(txtOnlineDateTo.Text)
                objInputXml.DocumentElement.SelectSingleNode("ExpectedDateFrom").InnerText = objeAAMS.ConvertTextDate(txtExpOnlineDateFrom.Text)
                objInputXml.DocumentElement.SelectSingleNode("ExpectedDateTo").InnerText = objeAAMS.ConvertTextDate(txtExpOnlineDateTo.Text)
            End If
            'Here Back end Method Call

            '<Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency>
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        '  objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If

                'End If

            End If


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
                    For Each objNode1 As XmlNode In objNodeList
                        If objNode1.Name <> "PAGE_NO" And objNode1.Name <> "SORT_BY" And objNode1.Name <> "DESC" And objNode1.Name <> "PAGE_SIZE" Then
                            If objNode1.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode1.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName"
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

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AgencyName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

          

            objOutputXml = objbzISPOrder.Search(objInputXml)
            ' objOutputXml.LoadXml("<ISP_SEARCH_OUTPUT><ISPDETAILS ISPOrderID='12' OrderNumber='' ISPName='' LCODE='100' AgencyName='' Address='' OfficeId='' City='' ApprovedDate='' OrderDate='' OnlineDate='' StatusName='' /><Errors Status='FALSE'><Error Code='' Description='' /></Errors></ISP_SEARCH_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'For Each objNode In objOutputXml.DocumentElement.SelectNodes("ISPDETAILS")
                '    objNode.Attributes("ApprovedDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("ApprovedDate").Value)
                '    objNode.Attributes("OrderDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("OrderDate").Value)
                '    objNode.Attributes("OnlineDate").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("OnlineDate").Value)
                'Next

                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If


                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvISPOrder.DataSource = ds.Tables("ISPDETAILS")
                gvISPOrder.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("ISPDETAILS").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                txtRecordOnCurrentPage.Text = ds.Tables("ISPDETAILS").Rows.Count.ToString
                ' 
                ' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "OrderNumber"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ISPName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "AgencyName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "Address"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "OfficeId"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "City"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(5).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ApprovedDate"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(6).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(6).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "OrderDate"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(7).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(7).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "StatusName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(8).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(8).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "OnlineDate"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPOrder.HeaderRow.Cells(9).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPOrder.HeaderRow.Cells(9).Controls.Add(imgDown)
                '        End Select
                'End Select

                SetImageForSorting(gvISPOrder)
                '  Added Code To Show Image  '

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                gvISPOrder.DataSource = Nothing
                gvISPOrder.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                txtTotalRecordCount.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("txtNpid") IsNot Nothing Then
                txtNpid.Text = Request("txtNpid")
          
            End If
        End Try
    End Sub
    Private Sub ISPOrderDelete(ByVal strIspOrderId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder
            objInputXml.LoadXml("<ISP_DELETEISPORDER_INPUT><ISPOrderID /></ISP_DELETEISPORDER_INPUT>")



            objInputXml.DocumentElement.SelectSingleNode("ISPOrderID").InnerText = strIspOrderId
            hdDeleteISPOrderID.Value = ""
            'Here Back end Method Call
            objOutputXml = objbzISPOrder.Delete(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  Session("Action") = objeAAMSMessage.messDelete
                lblError.Text = objeAAMSMessage.messDelete
                ' ###################################################################
                '@ Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                ISPOrderSearch(PageOperation.Search)

            Else
                ' Session("Action") = 
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                ' Response.Redirect("ISPSR_Order.aspx", False)
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    

    Protected Sub gvISPOrder_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvISPOrder.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
            Dim hdISPOrderID As HiddenField
            Dim hdLCODE As HiddenField
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")

            btnEdit = CType(e.Row.FindControl("linkEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
            hdISPOrderID = CType(e.Row.FindControl("hdISPOrderID"), HiddenField)
            hdLCODE = CType(e.Row.FindControl("hdLCODE"), HiddenField)

            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForISPOrderID, EnstrQueryStringForLCode As String
            EnstrQueryStringForISPOrderID = objED.Encrypt(hdISPOrderID.Value)
            EnstrQueryStringForLCode = objED.Encrypt(hdLCODE.Value)
            '@ End of Code Used For Encription/Decription


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrder']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("OnClick", "return DeleteFunctionIspOrder('" & hdISPOrderID.Value & "');")
                    End If
                    btnEdit.Attributes.Add("OnClick", "return EditFunctionIspOrder('" & EnstrQueryStringForISPOrderID & "'" & ",'" & EnstrQueryStringForLCode & "');")
                    'btnEdit.Attributes.Add("OnClick", "return EditFunctionIspOrder('" & hdISPOrderID.Value & "'" & ",'" & hdLCODE.Value & "');")
                End If
            Else
                btnDelete.Enabled = True
                btnDelete.Attributes.Add("OnClick", "return DeleteFunctionIspOrder('" & hdISPOrderID.Value & "');")
                btnEdit.Attributes.Add("OnClick", "return EditFunctionIspOrder('" & EnstrQueryStringForISPOrderID & "'" & ",'" & EnstrQueryStringForLCode & "');")
               
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(ddlCity, "CITY", True, 3)
            objeAAMS.BindDropDown(ddlCountry, "COUNTRY", True, 3)
            '  objeAAMS.BindDropDown(ddlOrderType, "OrderTypeCategory", True)
            objeAAMS.BindDropDown(ddlLoggedBy, "EMPLOYEE", True, 3)
            objeAAMS.BindDropDown(ddlApprovedBy, "EMPLOYEE", True, 3)
            objeAAMS.BindDropDown(ddlIspOrderStatus, "ISPOrderStatusList", True, 3)
            'objeAAMS.BindDropDown(ddlIspPlan, "ISPPLANLIST", True)


            BindOrderType(3)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Sub BindOrderType(ByVal str As String)
        Try
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            Dim objOutputXml As XmlDocument = New XmlDocument
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
            ddlOrderType.Items.Insert(0, New ListItem("All", ""))

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtAgencyName.Text = ""
            'txtCafNo.Text = ""
            'txtExpOnlineDateFrom.Text = ""
            'txtExpOnlineDateTo.Text = ""
            'ddlCity.SelectedIndex = 0
            'ddlApprovedBy.SelectedIndex = 0
            'ddlCountry.SelectedIndex = 0
            'ddlIspOrderStatus.SelectedIndex = 0
            'ddlLoggedBy.SelectedIndex = 0
            'ddlOrderType.SelectedIndex = 0
            'txtMDNNo.Text = ""
            'txtNpid.Text = ""
            'hdISPid.Value = ""
            'txtOrderDateFrom.Text = ""
            'txtOrderDateTo.Text = ""
            'txtOnlineDateFrom.Text = ""
            'txtOnlineDateTo.Text = ""
            'gvISPOrder.DataSource = Nothing
            'gvISPOrder.DataBind()
            Response.Redirect(Request.Url.ToString())
        Catch ex As Exception

        End Try
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ISPOrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ISPOrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ISPOrderSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvISPOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvISPOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvISPOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvISPOrder.Sorting
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
            ISPOrderSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ISPOrderSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Order Number", "ISP Name", "Agency Name", "Address", "Office ID", "City", "Approval Date", "Order Date", "Order Status", "Online Date", "Account ID", "Login Name", "Static IP", "Cancellation Date"}
        Dim intArray() As Integer = {1, 2, 4, 5, 6, 7, 8, 9, 11, 10, 12, 13, 14, 15}
        '<ISPDETAILS 
        '        ISPOrderID = "34"
        '        OrderNumber = "2008/3/1"
        '        ISPName = "DISHNET DSL"
        '        LCODE = "26064"
        '        AgencyName = "360 Degree World"
        '        Address = "E-201 Basement ,Amar Colony Lajpat Nagar 4"
        '        OfficeId = "DELVS33NW"
        '        City = "New Delhi"
        '        ApprovedDate = "20080304"
        '        OrderDate = "20080304"
        '        OnlineDate = ""
        'StatusName="Cancel" /> 
        objExport.ExportDetails(objOutputXml, "ISPDETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPORDER.xls")
    End Sub
    'End Code For Export
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
End Class