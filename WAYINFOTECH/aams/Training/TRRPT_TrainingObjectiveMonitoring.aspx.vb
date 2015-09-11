
Partial Class Training_TRRPT_TrainingObjectiveMonitoring
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
            CheckSecurity()
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlReagion, "REGION", True, 3)

                '   Checking Permission For Own Office and Region start.
                If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                    ddlAoffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    ddlAoffice.Enabled = False
                End If

               
                '   Checking Permission For Own Office and Region end.

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
            'Response.Redirect("../RPSR_ReportShow.aspx?Case=TrainingObjectiveMonitor")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " SearchRecords()"
    Sub SearchRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        '  Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCourseSession As New AAMS.bizTraining.bzTraining
        objInputXml.LoadXml("<TR_RPT_TRAINING_OBJECTIVE_MONITORING_INPUT><TR_CLOCATION_NAME/><AOFFICE/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><COURSE_STARTDATE_FROM/><COURSE_STARTDATE_TO/><COURSE_ENDDATE_FROM/><COURSE_ENDDATE_TO/><EMPLOYEE_ID/><Limited_To_Region/><Limited_To_Aoffice/><Limited_To_OwnAgency/></TR_RPT_TRAINING_OBJECTIVE_MONITORING_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_CLOCATION_NAME").InnerText = Request.Form("txtTrainingRoom")
        objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAoffice.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlReagion.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()
        'objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
        ' Course Start Date From
        Dim strDateFrom As String = Request.Form("txtStartDateFrom").Trim()
        If strDateFrom <> "" Then
            'objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATE_FROM").InnerText = objeAAMS.ConvertTextDate(strDateFrom)
            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = objeAAMS.ConvertTextDate(strDateFrom)
        End If
        ' Course Start Date To
        Dim strDateTo As String = Request.Form("txtStartDateTo").Trim()
        If strDateTo <> "" Then
            'objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATE_TO").InnerText = objeAAMS.ConvertTextDate(strDateTo)
            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = objeAAMS.ConvertTextDate(strDateTo)
        End If



        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue


        ' Course End Date From
        'Dim strEndDateFrom As String = Request.Form("txtEndDateFrom").Trim()
        'If strEndDateFrom <> "" Then
        '    objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_FROM").InnerText = objeAAMS.ConvertTextDate(strEndDateFrom)
        'End If
        '' Course End Date To
        'Dim strEndDateTo As String = Request.Form("txtEndDateTo").Trim()
        'If strEndDateTo <> "" Then
        '    objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_TO").InnerText = objeAAMS.ConvertTextDate(strEndDateTo)
        'End If
        objOutputXml = objbzCourseSession.rpt_ObjectiveMonitoring(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Session("TrainingObjectiveMonitor") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=TrainingObjectiveMonitor")
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If




        ' Here Back end Method Call
        'objOutputXml = objbzCourseSession.RptAgencyCourseParticipant(objInputXml)
        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    objXmlReader = New XmlNodeReader(objOutputXml)
        '    ds.ReadXml(objXmlReader)
        '    'gvCourseSession.DataSource = ds.Tables("DETAILS")
        '    'gvCourseSession.DataBind()
        '    lblError.Text = ""
        '    If ds.Tables("DETAILS").Rows.Count <> 0 Then
        '        Session("AgencyTrainingGroup") = objOutputXml.OuterXml
        '        Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyTrainingPeople")
        '    End If

        'Else
        '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        'End If
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TRRPT_TrainingObjectiveMonitoring.aspx")
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Objective Monitoring']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Objective Monitoring']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")

                End If
                If strBuilder(4) = "0" Then
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
