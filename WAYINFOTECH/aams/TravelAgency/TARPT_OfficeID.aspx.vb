Imports System.Data
Imports System.Xml
Partial Class TravelAgency_TARPT_OfficeID
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS

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

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Dim objInputXml, objOutputXml, objOutputXml1 As New XmlDocument
        '     Dim objParentNode As XmlNode
        '        Dim objXmlReader, objXmlReader1 As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Try
            objInputXml.LoadXml("<TA_RPT_SUMMARYOFFICEID_INPUT><CityCode></CityCode><ALLOCATED></ALLOCATED><SUMMARYTYPE></SUMMARYTYPE></TA_RPT_SUMMARYOFFICEID_INPUT>")
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = drpCity.SelectedItem.Value.Split("|").GetValue(1)
            Else
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = ""
            End If
            objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = True
            'Rakesh Comment Start
            'If chkallocatedid.Checked Then
            '    objInputXml.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerXml = True
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerXml = False
            'End If
            'Rakesh Comment End
            'Add Code Start Rakesh
            objInputXml.DocumentElement.SelectSingleNode("SUMMARYTYPE").InnerXml = ddlSummaryType.SelectedValue
            'Add Code End Rakesh
            Session("eOfficeID") = objInputXml.OuterXml
            Select Case ddlSummaryType.SelectedValue
                Case "0"
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=OfficeIdUnallocCItyWise", False)
                Case "1"
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=OfficeIdallocCityWise", False)
                Case "2"
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=OfficeIdSumCityWise", False)
            End Select

            'If chkallocatedid.Checked = True Then
            '    Response.Redirect("../RPSR_ReportShow.aspx?Case=OfficeIdSumCityWise", False)
            'Else
            '    Response.Redirect("../RPSR_ReportShow.aspx?Case=OfficeIdUnallocCItyWise", False)
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TARPT_OfficeID.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId Report']").Attributes("Value").Value)
                End If
                If strBuilder(4) = "0" Then
                    btnDisplay.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITYOFFICEIDGENERATION", True, 3)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
