
Partial Class Training_MSSR_StaffTraining
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            txtAgency.Text = Request.Form("txtAgency")
            CheckSecurity()
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)
                objeAAMS.BindDropDown(ddlStatus, "PARTCIPANTSTATUS", True, 3)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords()
            'If RadioButtonList1.SelectedIndex = 0 Then
            '    Response.Redirect("/AAMS/RPSR_ReportShow.aspx?Case=AgencyTrainingPeople")
            'Else
            '    Response.Redirect("/AAMS/RPSR_ReportShow.aspx?Case=AgencyTrainingSession")
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("MSSR_StaffTraining.aspx")
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " SearchRecords()"
    Sub SearchRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCourseSession As New AAMS.bizTraining.bzTraining
        objInputXml.LoadXml("<TR_SEARCH_STAFF_TRAINING_INPUT><LCODE/><AGENCYNAME/><AGENCYSTAFFNAME/><TR_PARTSTATUS_ID/><COURSEID/><DATEFROM/><DATETO/><GROUPBY/><AgencyType/> <Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><RESP_1A/></TR_SEARCH_STAFF_TRAINING_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = "14"
        objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = Request.Form("txtAgency") 'txtAgency.Text.Trim()
        objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtAgencyStaff") 'txtAgencyStaff.Text.Trim()
        objInputXml.DocumentElement.SelectSingleNode("COURSEID").InnerText = ddlCourse.SelectedValue

        objInputXml.DocumentElement.SelectSingleNode("TR_PARTSTATUS_ID").InnerText = ddlStatus.SelectedValue

        ' Security Input Xml Start.
        If Session("Security") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
        End If
        ' Security Input Xml End.


        Dim strDateFrom As String = Request.Form("txtStartDateFrom").Trim()
        If strDateFrom <> "" Then
            strDateFrom = objeAAMS.GetDateFormat(strDateFrom, "dd/MM/yyyy", "yyyyMMdd", "/") 'strDateFrom.Split("/").GetValue(1) + strDateFrom.Split("/").GetValue(0) + strDateFrom.Split("/").GetValue(2)
        End If
        Dim strDateTo As String = Request.Form("txtStartDateTo").Trim()
        If strDateTo <> "" Then
            strDateTo = objeAAMS.GetDateFormat(strDateTo, "dd/MM/yyyy", "yyyyMMdd", "/") ' .Split("/").GetValue(1) + strDateTo.Split("/").GetValue(0) + strDateTo.Split("/").GetValue(2)
        End If

        objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = strDateFrom
        objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = strDateTo

        ' Here Back end Method Call
        objOutputXml = objbzCourseSession.RptAgencyCourseParticipant(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            'gvCourseSession.DataSource = ds.Tables("DETAILS")
            'gvCourseSession.DataBind()
            lblError.Text = ""
            If ds.Tables("DETAILS").Rows.Count <> 0 Then
                Session("AgencyTrainingGroup") = objOutputXml.OuterXml
                If RadioButtonList1.SelectedIndex = 0 Then
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyTrainingPeople")
                Else
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyTrainingSession")
                End If
            End If

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff Training']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff Training']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class
