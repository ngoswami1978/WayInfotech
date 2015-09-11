
Partial Class Popup_PUSRISPPaymentDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
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
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        objeAAMS.ExpirePageCache()
        If Not IsPostBack Then
            If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                ' Bind The Data in PaymentReceived Details Grid
                PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString())
            End If
        End If


        'objeAAMS.ExpirePageCache()
    End Sub
    Sub PaymentReceivedDetails(ByVal PaymentDetailsofPANumber As String)

        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        'Dim RowNo As Long
        Dim objInputPaymentDetailsXml, objOutputIPaymentDetailsXml As New XmlDocument
        Dim objbzISPOrder As New AAMS.bizISP.bzISPOrder

        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            objInputPaymentDetailsXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/></IS_SEARCHPAYMENTPROCEED_INPUT>")



            If Not Session("LoginSession") Is Nothing Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If



            ' objInputPaymentDetailsXml.LoadXml("<IS_SEARCHPAYMENTPROCEED_INPUT><LCode/><AgencyName /><City /><Country /><ISPName /><Month /><Year /><PANumber/></IS_SEARCHPAYMENTPROCEED_INPUT>")
            objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PANumber").InnerText = PaymentDetailsofPANumber

            If (Request.QueryString("PMonth") IsNot Nothing) Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("Month").InnerText = Request.QueryString("PMonth").ToString
            End If
            If (Request.QueryString("PYear") IsNot Nothing) Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("Year").InnerText = Request.QueryString("PYear").ToString
            End If

            '  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '@ Coding For Paging Ansd sorting
            If ViewState("PrevSearching") Is Nothing Then
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputPaymentDetailsXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NPID"
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EMPLOYEENAME" '"LOCATION_CODE"
            Else
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputPaymentDetailsXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting


            'Here Back end Method Call
            objOutputIPaymentDetailsXml = objbzISPOrder.PaymentProceedReport(objInputPaymentDetailsXml)

            If objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputIPaymentDetailsXml)
                ds.ReadXml(objXmlReader)
                If (Request.QueryString("PMonth") IsNot Nothing) Then
                    'For Each objNode As XmlNode In objOutputIPaymentDetailsXml.DocumentElement.SelectNodes("PAYMENTPROCEED")
                    '    objNode.Attributes("CostActivityMonth").Value = MonthName(Val(Request.QueryString("PMonth")))
                    'Next
                End If
                GvDopaymentDetails.DataSource = ds.Tables("PAYMENTPROCEED")
                GvDopaymentDetails.DataBind()

                GvDopaymentDetails.HeaderRow.Cells(14).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"
                GvDopaymentDetails.HeaderRow.Cells(16).Text = "(VAT " + ds.Tables("PAYMENTPROCEED").Rows(0)("VATPercentage").ToString + " )"

                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                'BindControlsForNavigation(2)
                BindControlsForNavigation(objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtTotalRecordCount.Text = objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(GvDopaymentDetails)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True


            Else
                GvDopaymentDetails.DataSource = Nothing
                GvDopaymentDetails.DataBind()
                lblError.Text = objOutputIPaymentDetailsXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False

        End Try
    End Sub
    'Protected Function Geturl() As String
    '    If (Request.QueryString("BDRId") IsNot Nothing) Then
    '        Return "BDRHistoryPrint.aspx?BDRId=" + Request.QueryString("BDRId")
    '    Else
    '        Return "BDRHistoryPrint.aspx"
    '    End If

    'End Function
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                ' Bind The Data in PaymentReceived Details Grid
                PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString())
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
            If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                ' Bind The Data in PaymentReceived Details Grid
                PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString())
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                ' Bind The Data in PaymentReceived Details Grid
                PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString())
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvDopaymentDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvDopaymentDetails.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GvDopaymentDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GvDopaymentDetails.Sorting
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
            If Request.QueryString("PaymentDetailsofPANumber") IsNot Nothing Then
                ' Bind The Data in PaymentReceived Details Grid
                PaymentReceivedDetails(Request.QueryString("PaymentDetailsofPANumber").ToString())
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

    Protected Sub GvDopaymentDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvDopaymentDetails.RowDataBound
        Try


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
