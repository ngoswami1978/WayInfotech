Partial Class Training_TARPT_WeeklyTraining
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
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
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Weekly Training']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Weekly Training']").Attributes("Value").Value)
                End If
                If strBuilder(4) = "0" Then
                    btnSearch.Enabled = False
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
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub SearchRecords()
        'Input XML 
        '<WEEKLYTRAINING_RPT_INPUT><AOFFICE/><REGION/><COURSE_START_DATEFROM/><COURSE_START_DATETO/><COURSE_END_DATEFROM/><COURSE_END_DATETO/><Limited_To_Region/></WEEKLYTRAINING_RPT_INPUT>
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Output XML 
        '<WEEKLYTRAINING_RPT_OUTPUT>
        ' <WEEKLYTRAINING_DETAILS AOFFICE ='' TR_COURSE_ID  =''   TR_COURSES_ID =''      TR_CLOCATION_ID='' TR_CLOCATION_NAME ='' TR_COURSE_NAME ='' NOOFPEOPLETRAINED='' />
        ' <WEEKLYTRAINING_SUMMERY AOFFICE ='' TR_CLOCATION_ID=''  TR_CLOCATION_NAME =''  NO_OF_DAYS=''  NOOFPEOPLETRAINED='' NOOFTRAININGSCONDUCTED ='' />
        '<Errors Status=''>
        '  <Error Code='' Description='' />
        '</Errors>
        '</WEEKLYTRAINING_RPT_OUTPUT>

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzTraining As New AAMS.bizTraining.bzTraining

        objInputXml.LoadXml("<WEEKLYTRAINING_RPT_INPUT><AOFFICE/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><COURSE_START_DATEFROM/><COURSE_START_DATETO/><COURSE_END_DATEFROM/><COURSE_END_DATETO/><Limited_To_Region/></WEEKLYTRAINING_RPT_INPUT>")

        If ddlAoffice.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAoffice.SelectedValue.Trim()
        End If

        If ddlReagion.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlReagion.SelectedValue.Trim()
        End If

        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue
        'objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
        'objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATETO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
        'objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtEndDateFrom.Text.Trim())
        'objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATETO").InnerText = objeAAMS.ConvertTextDate(txtEndDateTo.Text.Trim())



        '' Here Back end Method Call
        objOutputXml = objbzTraining.rpt_WeeklyTrainingSummery(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            Session("TARPT_TainingWeekly") = objOutputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=TARPT_TainingWeekly", False)
            lblError.Text = ""

            'If ds.Tables("DETAILS").Rows.Count <> 0 Then
            'Session("AgencyTrainingGroup") = objOutputXml.OuterXml
            'Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyTrainingPeople")
            'End If

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TARPT_WeeklyTraining.aspx")
    End Sub
End Class
