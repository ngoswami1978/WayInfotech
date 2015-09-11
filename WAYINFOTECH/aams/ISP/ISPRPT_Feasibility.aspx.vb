Imports System.Xml
Imports System.Data
Partial Class ISP_ISPRPT_Feasibility
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            btnReset.Attributes.Add("onclick", "return FeasibilityReset();")
            lblError.Text = String.Empty
            chkDummyFeasiblity.Attributes.Add("onclick", "return DummyFeasiblity();")
            If chkDummyFeasiblity.Checked Then
                txtAgencyName.ReadOnly = True
                txtAgencyName.CssClass = "textboxgrey"
                'hdSpan.Visible = False
            End If
            '***************************************************************************************
            'Code of Security Check
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Feasibility Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ISP Feasibility Report']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnReportPrint.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnReportPrint.Enabled = False
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnReset.Attributes.Add("onclick", "return FeasibilityReset();")

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlApprovedBy, "EMPLOYEE", True)
                objeAAMS.BindDropDown(ddlFeasibleStatus, "FeasibilityStatus", True)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    'Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    Try
    '        txtRequestId.Text = ""
    '        ddlApprovedBy.SelectedIndex = -1
    '        txtDateFrom.Text = ""
    '        lblError.Text = ""
    '        txtDateTo.Text = ""
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub btnReportPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReportPrint.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objParentNode As XmlNode
        Dim objbzISP As New AAMS.bizISP.bzISPFeasibleRequest
        Try
            objInputXml.LoadXml("<RP_ISPFEASIBILEREQUEST_INPUT><FEASIBILE_REQUEST Name='' LCODE='' FeasibleStatusID=''   RequestID='' LoggedBy='' LoggedDateFrom='' LoggedDateTo='' ISPID='' ISPName='' DummyLocation=''><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></FEASIBILE_REQUEST></RP_ISPFEASIBILEREQUEST_INPUT>")
            objParentNode = objInputXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST")
            With objParentNode
                .Attributes("Name").Value() = Trim(txtAgencyName.Text) & ""
                .Attributes("RequestID").Value() = Trim(txtRequestId.Text)
                .Attributes("LCODE").Value() = Trim(hidLcode.Value) & ""
                .Attributes("LoggedBy").Value() = ddlApprovedBy.SelectedValue
                .Attributes("LoggedDateFrom").Value() = objeAAMS.ConvertTextDate(txtDateFrom.Text)
                .Attributes("FeasibleStatusID").InnerText = ddlFeasibleStatus.SelectedValue
                .Attributes("LoggedDateTo").Value() = objeAAMS.ConvertTextDate(txtDateTo.Text)
                .Attributes("ISPID").Value() = Trim(hidIspId.Value) & ""
                .Attributes("ISPName").Value() = Trim(txtIspName.Text) & ""
                If (chkDummyFeasiblity.Checked) Then
                    .Attributes("DummyLocation").InnerText = "True"
                Else
                    .Attributes("DummyLocation").InnerText = "False"
                End If
            End With
            
            'Here Back end Method Call
            objOutputXml = objbzISP.ISPFeasibleRequestReport(objInputXml)
            Session("eISPFeasibilityRequest") = objOutputXml.OuterXml 'objOxml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=ISPFeasibilityRequest", False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

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
