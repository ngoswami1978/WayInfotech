Imports System.Data
Imports System.Xml
Partial Class ISP_ISPSR_Feasibility
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSecurityXml As New XmlDocument

        Try
            objeAAMS.ExpirePageCache()
            Session("PageName") = Request.Url.ToString()
            ' This code is used for checking session handler according to user login.
            objSecurityXml.LoadXml(Session("Security"))
            chkDummyFeasiblity.Attributes.Add("onclick", "return DummyFeasiblity();")
            'If chkDummyFeasiblity.Checked Then
            '    txtAgencyName.ReadOnly = True
            '    txtAgencyName.CssClass = "textboxgrey"
            '    'hdSpan.Visible = False
            'End If
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityRequest']").Attributes("Value").Value)
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
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            '   btnReset.Attributes.Add("onclick", "return FeasibilityReset();")

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlApprovedBy, "EMPLOYEE", True, 3)
                objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True, 3)
                objeAAMS.BindDropDown(drpISPCountry, "COUNTRY", True, 3)
                objeAAMS.BindDropDown(drpISPCity, "CITY", True, 3)
                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                '        FeasibilityRequestDelete(Request.QueryString("FeasibilityRequestID").ToString())
                '    End If
                'End If
            End If
            '   Code for delete.
            If hdDeleteID.Value.Trim() <> "" Then
                FeasibilityRequestDelete(hdDeleteID.Value)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try

            lblError.Text = ""
            FeasibilityRequestSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Sub FeasibilityRequestDelete(ByVal strFeasibilityID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtFeasibilityRequest As New AAMS.bizISP.bzISPFeasibleRequest
            Dim s As String = ""
            objInputXml.LoadXml("<ISP_DELETEFEASIBILEREQUEST_INPUT><Name></Name><RequestID></RequestID></ISP_DELETEFEASIBILEREQUEST_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerText = strFeasibilityID.Split("|").GetValue(0).ToString()
            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = strFeasibilityID.Split("|").GetValue(1).ToString()
            'Here Back end Method Call
            objOutputXml = objtFeasibilityRequest.Delete(objInputXml)
            FeasibilityRequestSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub FeasibilityRequestSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objtFeasibilityRequest As New AAMS.bizISP.bzISPFeasibleRequest
        Try
            'objInputXml.LoadXml("<ISP_SEARCHFEASIBILEREQUEST_INPUT><Name></Name><LCODE></LCODE><RequestID></RequestID><City></City><ISPID></ISPID><LoggedBy></LoggedBy><LoggedDateFrom></LoggedDateFrom><LoggedDateTo></LoggedDateTo></ISP_SEARCHFEASIBILEREQUEST_INPUT>")
            'objInputXml.LoadXml("<ISP_SEARCHFEASIBILEREQUEST_INPUT><Name></Name><LCODE></LCODE><RequestID></RequestID><ISPID></ISPID><ISPName></ISPName><FeasibleStatusID></FeasibleStatusID><LoggedBy></LoggedBy><LoggedDateFrom></LoggedDateFrom><LoggedDateTo></LoggedDateTo><DummyLocation></DummyLocation><CountryID></CountryID><City></City><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency></ISP_SEARCHFEASIBILEREQUEST_INPUT>")
            objInputXml.LoadXml("<ISP_SEARCHFEASIBILEREQUEST_INPUT><Name></Name><LCODE></LCODE><RequestID></RequestID><ISPID></ISPID><ISPName></ISPName><FeasibleStatusID></FeasibleStatusID><LoggedBy></LoggedBy><LoggedDateFrom></LoggedDateFrom><LoggedDateTo></LoggedDateTo><DummyLocation></DummyLocation><CountryID></CountryID><City></City><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><EmployeeID/></ISP_SEARCHFEASIBILEREQUEST_INPUT>")


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = Trim(txtAgencyName.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Trim(hidLcode.Value) & ""
            objInputXml.DocumentElement.SelectSingleNode("RequestID").InnerText = Trim(txtRequestId.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = Trim(hidIspId.Value) & ""
            objInputXml.DocumentElement.SelectSingleNode("ISPName").InnerText = Trim(txtIspName.Text) & ""
            objInputXml.DocumentElement.SelectSingleNode("FeasibleStatusID").InnerText = ddlFeasibleStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LoggedBy").InnerText = ddlApprovedBy.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LoggedDateFrom").InnerText = objeAAMS.ConvertTextDate(txtDateFrom.Text.Trim())
            objInputXml.DocumentElement.SelectSingleNode("LoggedDateTo").InnerText = objeAAMS.ConvertTextDate(txtDateTo.Text.Trim())
            objInputXml.DocumentElement.SelectSingleNode("CountryID").InnerText = drpISPCountry.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("City").InnerText = drpISPCity.SelectedValue

            If (chkDummyFeasiblity.Checked = True) Then
                objInputXml.DocumentElement.SelectSingleNode("DummyLocation").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DummyLocation").InnerText = "False"
            End If


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

       


            objOutputXml = objtFeasibilityRequest.Search(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvISPFeasibilityRequest.DataSource = ds.Tables("FEASIBILE_REQUEST")
                gvISPFeasibilityRequest.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("FEASIBILE_REQUEST").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                '' 
                '' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "RequestID"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "AgencyName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ADDRESS"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "City"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select
                'End Select
                ''OFFICEID
                'Select Case ViewState("SortName")
                '    Case "OFFICEID"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "PINCODE"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(5).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "STAFFNAME"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(6).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(6).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "PHONE"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(7).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(7).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "LoggedBy"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(8).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(8).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "LoggedDatetime"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(9).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(9).Controls.Add(imgDown)

                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "Name"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(10).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(10).Controls.Add(imgDown)
                '        End Select
                'End Select
                'Select Case ViewState("SortName")
                '    Case "FeasibleDate"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(11).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(11).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "FEASIBLESTATUSNAME"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(12).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvISPFeasibilityRequest.HeaderRow.Cells(12).Controls.Add(imgDown)
                '        End Select
                'End Select

                SetImageForSorting(gvISPFeasibilityRequest)

                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record

            Else
                gvISPFeasibilityRequest.DataSource = Nothing
                gvISPFeasibilityRequest.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub gvISPFeasibilityRequest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvISPFeasibilityRequest.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdRequestId As New HiddenField
            Dim hdISPname As New HiddenField
            Dim hdAgency As New HiddenField
            Dim s As String
            Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            lnkEdit = e.Row.FindControl("linkEdit")
            Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            lnkDelete = e.Row.FindControl("linkDelete")
            hdRequestId = e.Row.FindControl("rowHidden1")
            hdISPname = e.Row.FindControl("rowHidden2")
            hdAgency = e.Row.FindControl("hdAgencyName")
            Dim cityval As New HiddenField
            cityval = e.Row.FindControl("hdCITY")
            s = hdRequestId.Value & "|" & hdISPname.Value & "|" & hdAgency.Value & "|" & cityval.Value
            's = hdRequestId.Value & "|" & hdISPname.Value


            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForstrRequestID As String
            EnstrQueryStringForstrRequestID = objED.Encrypt(s)
            '@ End of Code Used For Encription/Decription




            If strBuilder(3) = "0" Then
                lnkDelete.Disabled = True
            Else
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction('" & s & "');")
            End If

            'If strBuilder(2) = "0" Then
            '    lnkEdit.Disabled = True
            'Else
            '    lnkEdit.Attributes.Add("onclick", "return EditFunction('" & s & "');")
            'End If

            lnkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrRequestID & "');")
            'lnkEdit.Attributes.Add("onclick", "return EditFunction('" & s & "');")

            'Dim lblPDate As Label
            'lblPDate = e.Row.FindControl("lblPD")

            'If lblPDate.Text <> "0" Then
            '    lblPDate.Text = objeAAMS.ConvertDate(lblPDate.Text).ToString("dd-MMM-yy")
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
       
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("ISPSR_Feasibility.aspx")
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            FeasibilityRequestSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            FeasibilityRequestSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            FeasibilityRequestSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvISPFeasibilityRequest_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvISPFeasibilityRequest.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvISPFeasibilityRequest_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvISPFeasibilityRequest.Sorting
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
            FeasibilityRequestSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            FeasibilityRequestSearch(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)

        '<FEASIBILE_REQUEST 
        'RequestID = "118" AgencyName = "Excel Travels Pvt. Ltd." ADDRESS = ""
        'City = "New Delhi" OFFICEID = "" PINCODE = "" Employee_Name = ""
        'STAFFNAME = "" STAFFCONTACTNO = "" PHONE = "" Name = "Airtel India pvt. ltd."
        'LoggedBy = "Admin" IspName = "Airtel India pvt. ltd." LoggedDatetime = "Jul 26 2008 4:47PM"
        'FeasibleDate = "" FeasibleStatusName="Pending" /> 

        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Request ID", "Agency Name", "Address", "City", "OfficeID", "Pin Code", "Con. Person", "Con. Number", "Requested By", "Requested Date", "ISP Name", "Fasibility Date", "Status"}
        Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 7, 9, 11, 13, 12, 14, 15}

        objExport.ExportDetails(objOutputXml, "FEASIBILE_REQUEST", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPFeasibilityRequest.xls")
    End Sub
    'End Code For Export

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
End Class
