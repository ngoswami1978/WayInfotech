Partial Class Training_TARPT_ReTaining
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
            txtAgency.Attributes.Add("onkeydown", "return AgencyValidation();")
            CheckSecurity()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            

            If Not IsPostBack Then
                txtAgency.Focus()
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)
                objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
                'BindTrainer()
                'objeAAMS.BindTrainer(ddlTrainer1, "", 3)
                '  objeAAMS.BindTrainer(ddlTrainer2, "", 3)

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

    'Private Sub BindTrainer()
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objSecurityXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Dim objbzEmployee As New AAMS.bizMaster.bzEmployee

    '    ' objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned></MS_SEARCHEMPLOYEE_INPUT>")

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
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TARPT_ReTaining.aspx")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub SearchRecords()
        '--------------------------------------Input XML-----------------------------------------------
        '<RETRAINING_RPT_INPUT>
        '   <LCODE/><AGENCYNAME/><GROUPDATA/><AGENCYSTAFFID/><AGENCYSTAFFNAME/><COURSEID/><TRANINGLOCATION_ID/><TRANINGLOCATION_NAME/><TRAINER1/><TRAINER2/><AOFFICE/><REGION/><COURSE_START_DATEFROM/><COURSE_START_DATETO/><COURSE_END_DATEFROM/><COURSE_END_DATETO/><EMPLOYEE_ID/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/>
        '</RETRAINING_RPT_INPUT>
        '----------------------------------------------------------------------------------------------

        '--------------------------------------OutPut Xml-----------------------------------------------
        '<RETRAINING_RPT_OUTPUT>
        '<RETRAINING  COURSE='' ONSITE='' TRAINER1=''  TRAINER2=''  PARTICIPANT=''  AGENCY=''  AOFFICE='' PREVIOUSLY_TRAINED_ON='' CURRENT_DATE='' />
        '<Errors Status=''>
        '  <Error Code='' Description='' />
        '</Errors> 
        '</RETRAINING_RPT_OUTPUT> 
        '------------------------------------------------------------------------------------------------

        '''''
        'objInputXml.DocumentElement.SelectSingleNode("TR_CLOCATION_NAME").InnerText = Request.Form("txtTrainingRoom")
        'objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
        'objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlReagion.SelectedValue
        'objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = Session("LoginSession").ToString().Split("|")(0).ToString()
        'objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

        ''''

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzTraining As New AAMS.bizTraining.bzTrainingRoom

        Try
            objInputXml.LoadXml("<RETRAINING_RPT_INPUT><LCODE/><AgencyType/><AGENCYNAME/><GROUPDATA/><AGENCYSTAFFID/><AGENCYSTAFFNAME/><COURSEID/><TRANINGLOCATION_ID/><TRANINGLOCATION_NAME/><TRAINER1/><TRAINER2/><AOFFICE/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><COURSE_START_DATEFROM/><COURSE_START_DATETO/><COURSE_END_DATEFROM/><COURSE_END_DATETO/><EMPLOYEE_ID/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><LIMITED_TO_OWNAAGENCY/><RESP_1A/></RETRAINING_RPT_INPUT>")

            If chbWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdCourseLCode.Value.Trim()
                ' objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgency.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "Y"
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = hdCourseStaff.Value.Trim()
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = txtAgencyStaff.Text.Trim()

                objInputXml.DocumentElement.SelectSingleNode("AgencyType").InnerText = drpAgencyType.SelectedValue

                If ddlCourse.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("COURSEID").InnerText = ddlCourse.SelectedValue.Trim()
                End If

                objInputXml.DocumentElement.SelectSingleNode("TRANINGLOCATION_ID").InnerText = txtTrainingRoom.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("TRANINGLOCATION_NAME").InnerText = txtTrainingRoom.Text.Trim()

                ' If ddlTrainer1.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TRAINER1").InnerText = txtTrainer1.Text ' ddlTrainer1.SelectedValue.Trim()
                'End If

                '   If ddlTrainer2.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TRAINER2").InnerText = txtTrainer2.Text ' ddlTrainer2.SelectedValue.Trim()
                'End If

                If ddlAOffice.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedItem.Text.Trim()
                End If


                If ddlRegion.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue.Trim()
                End If

                'COURSE_START_DATEFROM

                
                '    objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
                '   objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATETO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
                '  objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtEndDateFrom.Text.Trim())
                ' objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATETO").InnerText = objeAAMS.ConvertTextDate(txtEndDateTo.Text.Trim())
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                ' objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.SecurityRegionID(Session("Security"))

            Else  ''''''''''''''''''GROUP CASE 

                'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdCourseLCode.Value.Trim()
                objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgency.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("AgencyType").InnerText = drpAgencyType.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "N"
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerText = hdCourseStaff.Value.Trim()
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = txtAgencyStaff.Text.Trim()

                objInputXml.DocumentElement.SelectSingleNode("AgencyType").InnerText = drpAgencyType.SelectedValue

                If ddlCourse.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("COURSEID").InnerText = ddlCourse.SelectedValue.Trim()
                End If

                objInputXml.DocumentElement.SelectSingleNode("TRANINGLOCATION_NAME").InnerText = txtTrainingRoom.Text.Trim()


                'If ddlTrainer1.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TRAINER1").InnerText = txtTrainer1.Text ' ddlTrainer1.SelectedValue.Trim()
                'End If

                '   If ddlTrainer2.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TRAINER2").InnerText = txtTrainer2.Text ' ddlTrainer2.SelectedValue.Trim()
                'End If

                If ddlAOffice.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedItem.Text.Trim()
                End If

                If ddlRegion.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue.Trim()
                End If

                'objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
                'objInputXml.DocumentElement.SelectSingleNode("COURSE_START_DATETO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
                'objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtEndDateFrom.Text.Trim())
                'objInputXml.DocumentElement.SelectSingleNode("COURSE_END_DATETO").InnerText = objeAAMS.ConvertTextDate(txtEndDateTo.Text.Trim())
                objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = objeAAMS.EmployeeID(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                If Session("Security") IsNot Nothing Then
                    'done on 18jan 09  as per discussion with neeraj
                    objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
                    'end
                End If
                ' objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.SecurityRegionID(Session("Security"))
            End If
            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = objeAAMS.ConvertTextDate(txtStartDateFrom.Text.Trim())
            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = objeAAMS.ConvertTextDate(txtStartDateTo.Text.Trim())
            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue
            objOutputXml = objbzTraining.rpt_ReTraining(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("TARPT_ReTaining") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=TARPT_ReTaining", False)    ''NOT WRITE CASE
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


    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ReTraining']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ReTraining']").Attributes("Value").Value)
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

    Protected Sub txtAgencyStaff_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgencyStaff.TextChanged

    End Sub
End Class
