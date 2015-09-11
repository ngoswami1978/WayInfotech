
Partial Class Market_MS_RPTFlash
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())
        End If
        If Not Page.IsPostBack Then
            FillData()
        End If
    End Sub

    Sub FillData()
        Dim yr As Integer = System.DateTime.Now.Year
        Dim month As Integer = System.DateTime.Now.Month
        ddlMonth.SelectedValue = (month - 1).ToString
        For i As Integer = 0 To 9
            Dim li As New ListItem((yr - i).ToString, (yr - i).ToString)
            ddlYear.Items.Add(li)
        Next

    End Sub

    Protected Sub btnFlash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlash.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objProductivity As New AAMS.bizProductivity.bzBIDT
        Try
            objInputXml.LoadXml("<RPT_PR_AGENTCONNECTIVITY_REPORT_INPUT><MNTH/><YR/><OPTION/></RPT_PR_AGENTCONNECTIVITY_REPORT_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("MNTH").InnerText = ddlMonth.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YR").InnerText = ddlYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("OPTION").InnerText = "FLASH"
            objOutputXml = objProductivity.AgencyConnectivityFlashReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("eFlashReport") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=FlashReport", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnFlashSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlashSummary.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objProductivity As New AAMS.bizProductivity.bzBIDT
        Try
            objInputXml.LoadXml("<RPT_PR_AGENTCONNECTIVITY_REPORT_INPUT><MNTH/><YR/><OPTION/></RPT_PR_AGENTCONNECTIVITY_REPORT_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("MNTH").InnerText = ddlMonth.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YR").InnerText = ddlYear.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("OPTION").InnerText = "SUMMERY"
            objOutputXml = objProductivity.AgencyConnectivityFlashReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("eFlashSummaryReport") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=FlashSummaryReport", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MS_RPTFlash.aspx")
    End Sub
End Class
