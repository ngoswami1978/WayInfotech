
Partial Class ISP_ISPSR_PaymentReceived
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strError As String = ""
        Session("PageName") = Request.Url.ToString()
        objeAAMS.ExpirePageCache()

        btnSearch.Attributes.Add("onclick", "return CheckValidation();")
        btnPrint.Attributes.Add("onclick", "return CheckValidation();")
        btnExport.Attributes.Add("onclick", "return CheckValidation();")
        '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
        lblError.Text = String.Empty
        '***************************************************************************************
        'Code of Security Check
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If


        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                End If ' 
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                    btnPrint.Enabled = False
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

        If (Not IsPostBack) Then
            BindAllControl()
        End If
    End Sub
    Private Sub BindAllControl()
        Try
            Dim i, j As Integer

            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpMonthFrom.SelectedValue = DateTime.Now.Month

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputIspOrderPaySearchXml, objOutputIspOrderPaySearchXml As New XmlDocument
        Dim objbzISPPayment As New AAMS.bizISP.bzISPPayment
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        Try
            objInputIspOrderPaySearchXml.LoadXml("<IS_SEARCHPAYMENTRECEIVED_INPUT><PANumber /><ChequeNumber /><Month /><Year /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHPAYMENTRECEIVED_INPUT>")
            objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PANumber").InnerText = txtPANO.Text
            objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("ChequeNumber").InnerText = txtChqNo.Text
            objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonthFrom.SelectedValue
            objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYearFrom.SelectedValue

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then
                If ViewState("PrevSearching") Is Nothing Then
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "PAMonth"
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "PAMonth"
                Else
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting



            'Here Back end Method Call
            objOutputIspOrderPaySearchXml = objbzISPPayment.Search(objInputIspOrderPaySearchXml)
            ' objOutputIspOrderPaySearchXml.LoadXml("<IS_SEARCHPAYMENTRECEIVED_OUTPUT><PAYMENTRECEIVED PANumber='1' PAMonth='2' PAYear='2008' DTPASentToAccount='' DTPAReceivedInAccount='' ChequeNumber='' ChequeDate='' ChequeAmount='' 	ChequeSentTo='' Remarks='' /><PAGE PAGE_COUNT='1' TOTAL_ROWS ='2'/>		<Errors Status='FALSE'>		<Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTRECEIVED_OUTPUT>")

            If objOutputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputIspOrderPaySearchXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputIspOrderPaySearchXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputIspOrderPaySearchXml)
                ds.ReadXml(objXmlReader)
                gvIspPaymentRec.DataSource = ds.Tables("PAYMENTRECEIVED")
                gvIspPaymentRec.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record

                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                hdRecordOnCurrentPage.Value = ds.Tables("PAYMENTRECEIVED").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'

                SetImageForSorting(gvIspPaymentRec)
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "PAMonth"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "PAYear"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "DTPASentToAccount"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "DTPAReceivedInAccount"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ChequeNumber"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ChequeDate"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(5).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ChequeAmount"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(6).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(6).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "ChequeSentTo"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(7).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(7).Controls.Add(imgDown)
                '        End Select
                'End Select

                'Select Case ViewState("SortName")
                '    Case "Remarks"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvIspPaymentRec.HeaderRow.Cells(8).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvIspPaymentRec.HeaderRow.Cells(8).Controls.Add(imgDown)
                '        End Select
                'End Select

                
                '  Added Code To Show Image  '

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                gvIspPaymentRec.DataSource = Nothing
                gvIspPaymentRec.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputIspOrderPaySearchXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objInputIspOrderPaySearchXml = Nothing
            objOutputIspOrderPaySearchXml = Nothing
            objbzISPPayment = Nothing
        End Try
    End Sub

    Protected Sub gvIspPaymentRec_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIspPaymentRec.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim lblPAMonth As Label
            Dim Month As String = ""
            lblPAMonth = e.Row.FindControl("PAMonth")
            Month = lblPAMonth.Text
            lblPAMonth.Text = MonthName(lblPAMonth.Text)
            Dim hdPANumber As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDetails As System.Web.UI.HtmlControls.HtmlAnchor
            linkDetails = e.Row.FindControl("linkDetails")
            hdPANumber = e.Row.FindControl("hdPANumber")
            If (hdPANumber.Value = "") Then
                e.Row.Cells.Clear()
                Exit Sub
            End If
            '  linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdPANumber.Value & "'" & ");")
            ' linkDetails.Attributes.Add("onclick", "return DetailsFunction(" & "'" & hdPANumber.Value & "'" & ");")

            ' linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Payment Received']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkDetails.Disabled = True
                    Else
                        linkDetails.Attributes.Add("onclick", "return DetailsFunction(" & "'" & hdPANumber.Value & "'" + ",'" & Month & "'" + ",'" & e.Row.Cells(1).Text & "'" & ");")
                    End If
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdPANumber.Value & "'" & ");")
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdPANumber.Value & "'" & ");")
                    'End If
                Else
                    linkDetails.Disabled = True
                    linkEdit.Disabled = True
                    'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                    'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                End If
            Else
                'linkDetails.Attributes.Add("onclick", "return DetailsFunction(" & "'" & hdPANumber.Value & "'" & ");")
                linkDetails.Attributes.Add("onclick", "return DetailsFunction(" & "'" & hdPANumber.Value & "'" + ",'" & Month & "'" + ",'" & e.Row.Cells(1).Text & "'" & ");")
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdPANumber.Value & "'" & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim objInputIspOrderPayReportXml, objOutputIspOrderPayReportXml As New XmlDocument
        Dim objbzISPPayment As New AAMS.bizISP.bzISPPayment


        Try
            objInputIspOrderPayReportXml.LoadXml("<IS_SEARCHPAYMENTRECEIVED_INPUT><PANumber /><ChequeNumber /><Month /><Year /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHPAYMENTRECEIVED_INPUT>")
            objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("PANumber").InnerText = txtPANO.Text



            objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("ChequeNumber").InnerText = txtChqNo.Text

            objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonthFrom.SelectedValue

            objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYearFrom.SelectedValue

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "PAMonth"
                objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "PAMonth"
            Else
                objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputIspOrderPayReportXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'Here Back end Method Call
            objOutputIspOrderPayReportXml = objbzISPPayment.Search(objInputIspOrderPayReportXml)
            ' objOutputIspOrderPayReportXml = objbzISPPayment.PaymentReceivedReport(objInputIspOrderPayReportXml)
            ' objOutputIspOrderPaySearchXml.LoadXml("<IS_SEARCHPAYMENTRECEIVED_OUTPUT><PAYMENTRECEIVED PANumber='1' PAMonth='' PAYear='' DTPASentToAccount='' DTPAReceivedInAccount='' ChequeNumber='' ChequeDate='' ChequeAmount='' 	ChequeSentTo='' Remarks='' />		<Errors Status='FALSE'>		<Error Code='' Description='' /></Errors></IS_SEARCHPAYMENTRECEIVED_OUTPUT>")

            If objOutputIspOrderPayReportXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("ISPPaymentReceived") = objOutputIspOrderPayReportXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPPaymentReceived", False)
            Else
                lblError.Text = objOutputIspOrderPayReportXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objInputIspOrderPayReportXml = Nothing
            objInputIspOrderPayReportXml = Nothing
            objbzISPPayment = Nothing
        End Try
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvIspPaymentRec_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvIspPaymentRec.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvIspPaymentRec_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvIspPaymentRec.Sorting
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
            BindData(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputIspOrderPaySearchXml As XmlDocument)
        For Each objNode As XmlNode In objOutputIspOrderPaySearchXml.DocumentElement.SelectNodes("PAYMENTRECEIVED")
            objNode.Attributes("PAMonth").Value = MonthName(objNode.Attributes("PAMonth").Value)
        Next
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"PA Month", "PA Year", "PA Sent To Account", "PA Received In Account", "Cheque Number", "Cheque Date", "Cheque Amount", "Cheque Sent To", "Remarks"}
        Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9}

        objExport.ExportDetails(objOutputIspOrderPaySearchXml, "PAYMENTRECEIVED", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPPAYMENTRECEIVED.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString())
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
