Imports System.Data
Imports System.Data.SqlClient
Partial Class ISP_ISRPT_ISPPlan
    Inherits System.Web.UI.Page
    Dim eaamsObj As New eAAMS

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", eaamsObj.CheckSession())
            Exit Sub
        End If

        drpCityName.Attributes.Add("onkeyup", "return gotop('drpCityName');")
       
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Plan Report']").Count <> 0 Then
                strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Plan Report']").Attributes("Value").Value)

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If
                If strBuilder(4) = "0" Then
                    btnReportPrint.Enabled = False
                End If
                If strBuilder(0) = "0" Then
                    btnReportPrint.Enabled = False
                End If
            End If
        Else
            strBuilder = eaamsObj.SecurityCheck(31)
        End If

        If Not Page.IsPostBack Then
            eaamsObj.BindDropDown(drpCityName, "CITY", True, 3)
            eaamsObj.BindDropDown(drpISPProvider, "ISPPROVIDER", True, 3)
        End If
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            ' txtISPName.Text = ""
            drpISPProvider.SelectedValue = ""
            txtNpid.Text = ""
            drpCityName.SelectedIndex = 0
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ISPList()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzISP As New AAMS.bizISP.bzISP
        Try
            'objInputXml.LoadXml("<RP_ISPPLAN_INPUT><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_ISPPLAN_INPUT>")
            objInputXml.LoadXml("<RP_ISPPLAN_INPUT><ProviderID></ProviderID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></RP_ISPPLAN_INPUT>")

            ' objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = Trim(txtISPName.Text)
            objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerText = drpISPProvider.SelectedValue

            If (drpCityName.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCityName.SelectedValue 'Trim(drpCityName.SelectedItem.Text)
            End If
            objInputXml.DocumentElement.SelectSingleNode("NPID").InnerText = Trim(txtNpid.Text)
            'Here Back end Method Call
            objOutputXml = objbzISP.ISPList(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("eISPPlanList") = objOutputXml.OuterXml 'objOxml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPPlanList", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            'Session("eISPPlanList") = objOutputXml.OuterXml 'objOxml.OuterXml
            'Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPPlanList", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReportPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReportPrint.Click
        Try
            ISPList()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
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
