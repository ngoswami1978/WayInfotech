
Partial Class Training_TRRPT_PendingTraining
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
            Dim strurl As String = "Training/TRRPT_PendingTraining.aspx"
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            txtAgencyName.Attributes.Add("onkeydown", "return AgencyValidation();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Pending Training']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Pending Training']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
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
                btnDisplay.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
                drpAgencyType.Items.Add(New ListItem("Blank", "10"))

                'objeAAMS.BindTrainer(ddlTrainer1, "", 3)
                'objeAAMS.BindTrainer(ddlTrainer2, "", 3)
                'BindTrainer()
                'Checking Permission For Own Office and Region start.

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

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim objSecurityXml As New XmlDocument
            Dim ds As New DataSet
            Dim objbzPendingTraining As New AAMS.bizTraining.bzCourseSession
            objInputXml.LoadXml("<TR_RPT_PENDINGTRAINING_INPUT><LCODE/><AGENCYNAME/><GROUPDATA/><CourseID/><LocationName/><TRAINER1/><TRAINER2/><AOFFICE/><AGENCYSTAFFNAME/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><COURSE_STARTDATE_FROM/><COURSE_STARTDATE_TO/><COURSE_ENDDATE_FROM/><COURSE_ENDDATE_TO/><EMPLOYEE_ID/><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAgency/><AgencyType/><RESP_1A/></TR_RPT_PENDINGTRAINING_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = Request.Form("txtAgency") 'txtAgency.Text.Trim()
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = ""
            End If
            If chbWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "False"
            End If
            objInputXml.DocumentElement.SelectSingleNode("CourseID").InnerText = ddlCourse.SelectedValue


            ' objInputXml.DocumentElement.SelectSingleNode("LocationName").InnerText = hdRoomID.Value
            objInputXml.DocumentElement.SelectSingleNode("LocationName").InnerText = txtTrainingRoom.Text
            objInputXml.DocumentElement.SelectSingleNode("TRAINER1").InnerText = txtTrainer1.Text ' ddlTrainer1.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TRAINER2").InnerText = txtTrainer2.Text ' ddlTrainer2.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
            If drpAgencyType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AgencyType").InnerText = Trim(drpAgencyType.SelectedValue)
            End If
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtAgencyStaff") 'txtAgencyStaff.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_ID").InnerText = objeAAMS.EmployeeID(Session("Security"))

            'If Not Session("LoginSession") Is Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            'End If
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = ""
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))


                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = "True"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = "False"
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText = "False"
                End If

            End If
            If Session("Security") IsNot Nothing Then
                'done on 18jan 09  as per discussion with neeraj
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
                'end
            End If
            'objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

            Dim strDateFrom As String = Request.Form("txtStartDateFrom").Trim()
            If strDateFrom <> "" Then
                ' objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATE_FROM").InnerText = objeAAMS.ConvertTextDate(strDateFrom)
                objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = objeAAMS.ConvertTextDate(strDateFrom)
            End If

            Dim strDateTo As String = Request.Form("txtStartDateTo").Trim()
            If strDateTo <> "" Then
                ' objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATE_TO").InnerText = objeAAMS.ConvertTextDate(strDateTo)
                objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = objeAAMS.ConvertTextDate(strDateTo)
            End If



            objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue


            'Dim strEDateFrom As String = Request.Form("txtEndDateFrom").Trim()
            'If strEDateFrom <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_FROM").InnerText = objeAAMS.ConvertTextDate(strEDateFrom)
            'End If

            'Dim strEDateTo As String = Request.Form("txtEndDateTo").Trim()
            'If strEDateTo <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_TO").InnerText = objeAAMS.ConvertTextDate(strEDateTo)
            'End If
            Session("ePendingTraining") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=PendingTraining", False)
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

    '    '@ Added By Abhishek on 06-01-08 ' New Xml Input
    '    objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")

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
        Response.Redirect("TRRPT_PendingTraining.aspx")
    End Sub
End Class
