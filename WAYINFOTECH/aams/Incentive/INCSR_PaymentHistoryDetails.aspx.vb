
Partial Class Incentive_INCSR_PaymentHistoryDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Dim FooterDataset As DataSet
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

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
            HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
            ' This code is usedc for checking session handler according to user login.
            objeAAMS.ExpirePageCache()

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If (Not Page.IsPostBack) Then
                If Request.QueryString("Chain_Code") IsNot Nothing Then
                    hdEnChainCode.Value = Request.QueryString("Chain_Code")
                    hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                    If Request.QueryString("BCaseID") IsNot Nothing Then
                        If Request.QueryString("Month") IsNot Nothing Then
                            If Request.QueryString("Year") IsNot Nothing Then
                                ViewHistoryDetails()
                            End If
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewHistoryDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '        Dim rorno As Integer
        Try

            'Dim objbzBusinessCase As New AAMS.bizIncetive.bzBusinessCase
            Dim objbzBCPaymentProcess As New AAMS.bizIncetive.bzBCPaymentProcess



            objInputXml.LoadXml("<INC_GET_PAYMENT_DETIALS_HISTORY_INPUT><BC_ID></BC_ID><Month></Month><Year></Year></INC_GET_PAYMENT_DETIALS_HISTORY_INPUT>")

            '  objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = hdChainCode.Value
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = Request.QueryString("BCaseID").ToString
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = Request.QueryString("Month").Trim
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = Request.QueryString("Year").Trim

            'objOutputXml = objbzBusinessCase.PaymentAdviceHistorySearch(objInputXml)
            objOutputXml = objbzBCPaymentProcess.Payment_Details_History(objInputXml)

            '<MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT>
            '  <PAGE PAGE_COUNT="1" TOTAL_ROWS="2" /> 
            '- <Errors Status="FALSE">
            '  <Error Code="" Description="" /> 
            '  </Errors>
            '  <AGENCYCONNECTIVITYHISTORY LCode="20265" Online_Status="V INTL" ConnectivityType="P" InstallDate="20060524" EndDate="20060524" ChangeBy="Sonali Baral" ChangeDate="20090217" Reason="" IOrderNumber="" DOrderNumber="" /> 
            '  <AGENCYCONNECTIVITYHISTORY LCode="20265" Online_Status="VISTA" ConnectivityType="P" InstallDate="" EndDate="" ChangeBy="Sonali Baral" ChangeDate="20060526" Reason="" IOrderNumber="" DOrderNumber="" /> 
            '  </MS_GETAGENCYCONNECTIVITYHISTORY_OUTPUT>

            '    objOutputXml.LoadXml("<MS_GETAGETHISTORY_PAYMENT_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors> <HISTORYDETAIL DateTime='20060524' PaymentAmount='500' />  <HISTORYDETAIL DateTime='20060524' PaymentAmount='400' />   </MS_GETAGETHISTORY_PAYMENT_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                gvPaymentHistory.DataSource = ds.Tables("HISTORYDETAIL").DefaultView
                gvPaymentHistory.DataBind()

                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                'BindControlsForNavigation(2)
                'BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                'txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                'SetImageForSorting(gvPaymentHistory)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                ' pnlPaging.Visible = True

            Else
                pnlPaging.Visible = False
                gvPaymentHistory.DataSource = Nothing 'ds.Tables("HISTORYDETAIL").DefaultView
                gvPaymentHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    If Request.QueryString("Month") IsNot Nothing Then
                        If Request.QueryString("Year") IsNot Nothing Then
                            ViewHistoryDetails()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    If Request.QueryString("Month") IsNot Nothing Then
                        If Request.QueryString("Year") IsNot Nothing Then
                            ViewHistoryDetails()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    If Request.QueryString("Month") IsNot Nothing Then
                        If Request.QueryString("Year") IsNot Nothing Then
                            ViewHistoryDetails()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPaymentHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPaymentHistory.RowDataBound
        Try
            e.Row.Cells(6).Visible = False

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("TOTAL") IsNot Nothing Then
                        e.Row.HorizontalAlign = HorizontalAlign.Right
                        '  <HISTORYDETAIL BC_ID=''
                        'CHQ_DATE='' CHQ_NO='' CHQ_BNAME='' CHQ_AMOUNT=''
                        'TOTAL_AMOUNT='' PAYMENT_MONTH_FROM='' PAYMENT_MONTH_TO='' 
                        'PAYMENT_YEAR_FROM='' PAYMENT_YEAR_TO='' CREATED_DTTI=''
                        'UPFRONT_AMOUNT='' NEXTUPFRONTAMOUNT='' INC_TYPE_ID='' 
                        'INC_TYPE_NAME='' PAYMENT_CYCLE_NAME='' PAYMENTTYPEID=''
                        'PAYMENTTYPENAME='' EMPLOYEE_NAME='' />
                        e.Row.Cells(8).Text = "Total"
                        'e.Row.Cells(5).Text = FooterDataset.Tables("TOTAL").Rows(0)("UPFRONT_AMOUNT").ToString
                        'e.Row.Cells(6).Text = FooterDataset.Tables("TOTAL").Rows(0)("NEXTUPFRONTAMOUNT").ToString
                        'e.Row.Cells(7).Text = FooterDataset.Tables("TOTAL").Rows(0)("TOTAL_AMOUNT").ToString
                        e.Row.Cells(9).Text = FooterDataset.Tables("TOTAL").Rows(0)("CHQ_AMOUNT").ToString
                        e.Row.Cells(14).Text = FooterDataset.Tables("TOTAL").Rows(0)("CHQ_ADJ_AMOUNT").ToString
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

   
    Protected Sub gvPaymentHistory_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPaymentHistory.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPaymentHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPaymentHistory.Sorting
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
            If Request.QueryString("Chain_Code") IsNot Nothing Then
                hdEnChainCode.Value = Request.QueryString("Chain_Code")
                hdChainCode.Value = objED.Decrypt(hdEnChainCode.Value)
                If Request.QueryString("BCaseID") IsNot Nothing Then
                    If Request.QueryString("Month") IsNot Nothing Then
                        If Request.QueryString("Year") IsNot Nothing Then
                            ViewHistoryDetails()
                        End If
                    End If
                End If
            End If

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub
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

#End Region

End Class