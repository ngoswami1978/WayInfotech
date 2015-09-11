
Partial Class Incentive_INCRPT_IncentivePaid
    Inherits System.Web.UI.Page

    Dim dsFooter As DataSet
    Dim objeAAMS As New eAAMS

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            btnSearch.Attributes.Add("onclick", "return ValidateForm();")

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            ' Checking Security
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IncentivePaidReport']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IncentivePaidReport']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()
                Dim dtMonth As String = Month(DateTime.Now)
                Dim dtYear As String = Year(DateTime.Now)
                drpMonthsFrom.SelectedIndex = Convert.ToInt16(dtMonth) - 1
                drpYearsFrom.Text = dtYear

                drpMonthsTo.SelectedIndex = Convert.ToInt16(dtMonth) - 1
                drpYearsTo.Text = dtYear

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords()
            'Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyWiseIncentivePaid")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString)
    End Sub
#End Region

#Region "BindDropDowns()"
    Private Sub BindDropDowns()
        Try
            Dim I As Integer

            For I = 1 To 12
                drpMonthsFrom.Items.Add(New ListItem(MonthName(I), I))
                drpMonthsTo.Items.Add(New ListItem(MonthName(I), I))
            Next

            Dim IntYearFrom As Integer = 3
            Dim IntYearTo As Integer = 3
            'If System.Configuration.ConfigurationManager.AppSettings("YearFrom") IsNot Nothing Then
            '    IntYearFrom = Val(System.Configuration.ConfigurationManager.AppSettings("YearFrom"))
            'End If
            'If System.Configuration.ConfigurationManager.AppSettings("YearTo") IsNot Nothing Then
            '    IntYearTo = Val(System.Configuration.ConfigurationManager.AppSettings("YearTo"))
            'End If


            For I = DateTime.Now.Year + IntYearFrom To DateTime.Now.Year - IntYearTo Step -1
                drpYearsFrom.Items.Add(I)
                drpYearsTo.Items.Add(I)
            Next
            drpYearsFrom.SelectedValue = DateTime.Now.Year
            drpYearsTo.SelectedValue = DateTime.Now.Year


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " SearchRecords()"
    Sub SearchRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objbzIncetive As New AAMS.bizIncetive.bzPaymentReceived
        objInputXml.LoadXml("<INC_RPT_PAYMENT_RECEIVED_INPUT><CHAIN_CODE></CHAIN_CODE><CHAIN_NAME></CHAIN_NAME><PAYMENT_MONTH_FROM></PAYMENT_MONTH_FROM><PAYMENT_YEAR_FROM></PAYMENT_YEAR_FROM><PAYMENT_MONTH_TO></PAYMENT_MONTH_TO><PAYMENT_YEAR_TO></PAYMENT_YEAR_TO><EmployeeID></EmployeeID></INC_RPT_PAYMENT_RECEIVED_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text.Trim
        objInputXml.DocumentElement.SelectSingleNode("CHAIN_NAME").InnerText = txtGroupName.Text.Trim
        objInputXml.DocumentElement.SelectSingleNode("PAYMENT_MONTH_FROM").InnerText = drpMonthsFrom.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("PAYMENT_YEAR_FROM").InnerText = drpYearsFrom.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("PAYMENT_MONTH_TO").InnerText = drpMonthsTo.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("PAYMENT_YEAR_TO").InnerText = drpYearsTo.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()
        'Calling Back end Method
        objOutputXml = objbzIncetive.rptIncentivePaid(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Session("AgencyWiseIncentivePaid") = objOutputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyWiseIncentivePaid")
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub
#End Region

   
End Class
