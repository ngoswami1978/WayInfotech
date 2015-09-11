
Partial Class Training_TRRPT_MonthlyTraining
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Monthly Training']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Monthly Training']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            txtAgency.Attributes.Add("onblur", "return GroupWholeChk();")
            ' txtAgency.Attributes.Add("onkeydown", "return LcodeReset(event);")
            If Not IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)
                objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
                drpAgencyType.Items.Add(New ListItem("Blank", "10"))
                'BindTrainer()
                '     objeAAMS.BindTrainer(ddlTrainer1, "", 3)
                '    objeAAMS.BindTrainer(ddlTrainer2, "", 3)

                '   Checking Permission For Own Office and Region start.
                If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                    ddlAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    ddlAOffice.Enabled = False
                End If


                '   Checking Permission For Own Office and Region end.
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "BindTrainer()"
    'Private Sub BindTrainer()
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objSecurityXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Dim objbzEmployee As New AAMS.bizMaster.bzEmployee

    '    ' objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned></MS_SEARCHEMPLOYEE_INPUT>")

    '    '@ Added By Abhishek on 06-01-08 ' New Xml Input
    '    objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")

    '    objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = "31"

    '    If Session("Security") IsNot Nothing Then
    '        objSecurityXml.LoadXml(Session("Security"))

    '        'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
    '        'objSecurityXml.DocumentElement.SelectNodes("
    '        If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
    '            If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
    '                If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
    '                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
    '                Else
    '                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
    '                End If
    '            Else
    '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
    '            End If
    '        Else
    '            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
    '        End If
    '        If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
    '            If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
    '                If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
    '                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = 1
    '                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
    '                Else
    '                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
    '                End If
    '            Else
    '                objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
    '                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
    '            End If
    '        Else
    '            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
    '        End If
    '    End If

    '    'Here Back end Method Call
    '    objOutputXml = objbzEmployee.Search(objInputXml)

    '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '        objXmlReader = New XmlNodeReader(objOutputXml)
    '        ds.ReadXml(objXmlReader)
    '        ddlTrainer1.Items.Clear()
    '        ddlTrainer2.Items.Clear()
    '        ddlTrainer1.DataSource = ds.Tables("Employee")
    '        ddlTrainer1.DataTextField = "Employee_Name"
    '        ddlTrainer1.DataValueField = "EmployeeID"
    '        ddlTrainer1.DataBind()
    '        ddlTrainer2.DataSource = ds.Tables("Employee")
    '        ddlTrainer2.DataTextField = "Employee_Name"
    '        ddlTrainer2.DataValueField = "EmployeeID"
    '        ddlTrainer2.DataBind()
    '        ddlTrainer1.Items.Insert(0, New ListItem("All", " "))
    '        ddlTrainer2.Items.Insert(0, New ListItem("All", " "))
    '        lblError.Text = ""
    '    Else
    '        ddlTrainer1.Items.Clear()
    '        ddlTrainer2.Items.Clear()
    '        ddlTrainer1.Items.Insert(0, New ListItem("All", " "))
    '        ddlTrainer2.Items.Insert(0, New ListItem("All", " "))
    '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '    End If
    'End Sub
#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            MonthlyTrainingReport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub MonthlyTrainingReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzTraining As New AAMS.bizTraining.bzTraining

        Try

            objInputXml.LoadXml("<TR_MONTHLYTRAINING_INPUT><AgencyType/><LCODE/><AGENCYNAME/><GROUPDATA/><CourseID/><Location_Name/><Trainer1/><Trainer2/><AOFFICE/><AGENCYSTAFFNAME/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><COURSE_STARTDATE_FROM/><COURSE_STARTDATE_TO/><COURSE_ENDDATE_FROM/><COURSE_ENDDATE_TO/><EMPLOYEE_ID/><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAgency/><RESP_1A/></TR_MONTHLYTRAINING_INPUT>")
            With objInputXml.DocumentElement
                .SelectSingleNode("LCODE").InnerText = hdCourseLCode.Value.Trim()
                .SelectSingleNode("AGENCYNAME").InnerText = txtAgency.Text.Trim()
                .SelectSingleNode("AgencyType").InnerText = drpAgencyType.SelectedValue
                If chkWholeG.Checked = True Then
                    .SelectSingleNode("GROUPDATA").InnerText = True
                Else
                    .SelectSingleNode("GROUPDATA").InnerText = False
                End If

                If ddlCourse.SelectedIndex <> 0 Then
                    .SelectSingleNode("CourseID").InnerText = ddlCourse.SelectedValue.Trim()
                End If

                .SelectSingleNode("Location_Name").InnerText = txtTrainingRoom.Text.Trim()

                '   If ddlTrainer1.SelectedIndex <> 0 Then
                .SelectSingleNode("Trainer1").InnerText = txtTrainer1.Text ' ddlTrainer1.SelectedValue.Trim()
                'End If

                'If ddlTrainer2.SelectedIndex <> 0 Then
                .SelectSingleNode("Trainer2").InnerText = txtTrainer2.Text ' ddlTrainer2.SelectedValue.Trim()
                'End If

                If ddlAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedItem.Text.Trim()
                End If

                ' .SelectSingleNode("AGENCYSTAFFID").InnerText = hdCourseStaff.Value.Trim()
                .SelectSingleNode("AGENCYSTAFFNAME").InnerText = txtAgencyStaff.Text.Trim()

                If ddlRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue.Trim()
                End If
                objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
                objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
                objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue
                ' .SelectSingleNode("COURSE_STARTDATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
                ' .SelectSingleNode("COURSE_STARTDATE_TO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
                '   .SelectSingleNode("COURSE_ENDDATE_FROM").InnerText = objeAAMS.ConvertTextDate(txtEndDateFrom.Text.Trim())
                '   .SelectSingleNode("COURSE_ENDDATE_TO").InnerText = objeAAMS.ConvertTextDate(txtEndDateTo.Text.Trim())

                If Session("Security") IsNot Nothing Then
                    'done on 18jan 09  as per discussion with neeraj
                    .SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
                    'end
                    .SelectSingleNode("EMPLOYEE_ID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                    '<Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAgency/>
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    .SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                    .SelectSingleNode("Limited_To_OwnAgency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))

                End If



                ' .SelectSingleNode("SECREGIONID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

            End With
            Session("MonthlyStaffTraining") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=MonthlyStaffTrainging")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
