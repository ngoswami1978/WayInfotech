
Partial Class Training_TRUP_CourseSession
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Dim objED As New EncyrptDeCyrpt
    Dim str As String
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
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        Try

           
            txtStartDate.Text = Request.Form("txtStartDate")
            txtEndDate.Text = Request.Form("txtEndDate")
           ' txtCourseTitle.Text = Request.Form("txtCourseTitle")
            txtCourseLevel.Text = Request.Form("txtCourseLevel")
            txtTrainingRoom.Text = Request.Form("txtTrainingRoom")
            txtParticipantMaxNo.Text = Request.Form("txtParticipantMaxNo")
            txtActualNo.Text = Request.Form("txtActualNo")
            txtDuration.Text = Request.Form("txtDuration")


            'txtStartDateRegisterTab.Text = Request.Form("txtStartDateRegisterTab")
            'txtCourseTitleRegisterTab.Text = Request.Form("txtCourseTitleRegisterTab")
            'txtCourseLevelRegisterTab.Text = Request.Form("txtCourseLevelRegisterTab")
            'txtTrainingRoomRegisterTab.Text = Request.Form("txtTrainingRoomRegisterTab")
            'txtMaxNoParticipantRegisterTab.Text = Request.Form("txtMaxNoParticipantRegisterTab")
            'If ddlTrainer2.SelectedValue = "" Then
            '    txtNMCTrainersRegisterTab.Text = ddlTrainer1.SelectedItem.Text
            'Else
            '    txtNMCTrainersRegisterTab.Text = ddlTrainer1.SelectedItem.Text & "," & ddlTrainer2.SelectedItem.Text
            'End If

            CheckSecurity()
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            If Not Page.IsPostBack Then
                If Not Request.QueryString("Action") Is Nothing Then
                    hdPageStatus.Value = Request.QueryString("Action").ToString
                End If
                If Not Request.QueryString("CourseSessionID") Is Nothing Then
                    hdPageCourseSessionID.Value = objED.Decrypt(Request.QueryString("CourseSessionID").ToString)
                    hdEnPageCourseSessionID.Value = Request.QueryString("CourseSessionID").ToString
                End If
                Bindata()
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 2)
                objeAAMS.BindTrainer(ddlTrainer1, "", 1)
                objeAAMS.BindTrainer(ddlTrainer2, "", 2)
                ' BindTrainer()
                FillCourseType()
                If hdPageCourseSessionID.Value <> "" Then
                    ViewRecords()
                End If
            End If




            If hdEnPageCourseSessionID.Value = "" Then
                btnHistory.Enabled = False
            Else
                btnHistory.Enabled = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Private Sub BindTrainer()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = "31"
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
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = 1
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            End If
        End If

        'Here Back end Method Call
        objOutputXml = objbzEmployee.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            ddlTrainer1.Items.Clear()
            ddlTrainer2.Items.Clear()
            ddlTrainer1.DataSource = ds.Tables("Employee")
            ddlTrainer1.DataTextField = "Employee_Name"
            ddlTrainer1.DataValueField = "EmployeeID"
            ddlTrainer1.DataBind()
            ddlTrainer2.DataSource = ds.Tables("Employee")
            ddlTrainer2.DataTextField = "Employee_Name"
            ddlTrainer2.DataValueField = "EmployeeID"
            ddlTrainer2.DataBind()
            ddlTrainer1.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlTrainer2.Items.Insert(0, New ListItem("", ""))
            lblError.Text = ""
        Else
            ddlTrainer1.Items.Clear()
            ddlTrainer2.Items.Clear()
            ddlTrainer1.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlTrainer2.Items.Insert(0, New ListItem("", ""))
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
  
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Session")
            TabText.Add("Register")
            TabText.Add("Activate")
            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        End If
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Session"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Course Session") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Register"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Register Participant") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Activate"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Activate Test") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
            End Select
        End If
        Button1 = e.Item.FindControl("Button1")
        If (Button1.Text = "Register" Or Button1.Text = "Activate") And hdPageCourseSessionID.Value = "" Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
            Button1.Attributes.Add("onclick", "return ColorMethodManageCourseSession('" & Button1.ClientID.ToString() & "',3);")
        End If


    End Sub

    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
        objInputXml.LoadXml("<TR_VIEWCOURSES_INPUT> <TR_COURSES_ID /></TR_VIEWCOURSES_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdPageCourseSessionID.Value

        objOutputXml = objbzCourseSession.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("COURSES")

                '<COURSES TR_COURSES_ID="1" TR_COURSES_REQ_CONFIG="test" EMPLOYEE_ID2="8018" EMPLOYEE_ID1="227" 
                'TR_COURSES_NBPART="0" TR_COURSES_EXPECT_DATE="5/2/2008" TR_CLOCATION_ID="2" TR_COURSE_ID="91063" 
                'TR_CLOCATION_NAME="DEL Onsite" TRAINER1="Sarita Chaturvedi" TRAINER2="M.A.Jojomon" 
                'TR_CLOCATION_MAXNBPART="0" TR_CLETTER_ID="" TR_COURSES_END_DATE="5/6/2008" TR_COURSES_START_TIME="1728" 
                'TR_COURSES_END_TIME="1728" ACTUALNOOFPART="0" INTERNAL="False" /> 
                txtNotes.Text = .Attributes("TR_COURSES_REQ_CONFIG").Value
                ddlTrainer1.SelectedValue = .Attributes("EMPLOYEE_ID1").Value
                ddlTrainer2.SelectedValue = .Attributes("EMPLOYEE_ID2").Value

                If ddlTrainer1.SelectedValue = "" And .Attributes("EMPLOYEE_ID1").Value <> "" Then
                    ddlTrainer1.Items.Add(New ListItem(.Attributes("TRAINER1").Value, .Attributes("EMPLOYEE_ID1").Value))
                    ddlTrainer1.SelectedValue = .Attributes("EMPLOYEE_ID1").Value
                End If

                If ddlTrainer2.SelectedValue = "" And .Attributes("EMPLOYEE_ID2").Value <> "" Then
                    ddlTrainer2.Items.Add(New ListItem(.Attributes("TRAINER2").Value, .Attributes("EMPLOYEE_ID2").Value))
                    ddlTrainer2.SelectedValue = .Attributes("EMPLOYEE_ID2").Value
                End If


                hdCourseID.Value = .Attributes("TR_COURSE_ID").Value
                hdTrainingRoomPage.Value = .Attributes("TR_CLOCATION_ID").Value
                txtTrainingRoom.Text = .Attributes("TR_CLOCATION_NAME").Value
                txtParticipantMaxNo.Text = .Attributes("TR_CLOCATION_MAXNBPART").Value
                txtActualNo.Text = .Attributes("ACTUALNOOFPART").Value
                If Val(.Attributes("ACTUALNOOFPART").Value) > 0 Then
                    'btnSave.Enabled = False
                    ddlCourseType.Enabled = False
                    ddlCourseTitle.Enabled = False
                    chkInternalSession.Enabled = False
                    ddlAOffice.Enabled = False
                    'ddlTrainer1.Enabled = False
                    'ddlTrainer2.Enabled = False
                    ' txtNotes.Enabled = False
                    'Img2.Visible = False
                    'imgStartDate.Style.Add("display", "none")
                    'imgEndDate.Style.Add("display", "none")

                    ' txtStartDate.CssClass = "textboxgrey"
                    ' txtStartDate.ReadOnly = True
                    ' txtStartTimeHH.CssClass = "textboxgrey"
                    ' txtStartTimeHH.ReadOnly = True
                    ' txtStartTimeMM.CssClass = "textboxgrey"
                    'txtStartTimeMM.ReadOnly = True

                    'txtEndDate.CssClass = "textboxgrey"
                    ' txtEndDate.ReadOnly = True
                    ' txtEndTimeHH.CssClass = "textboxgrey"
                    ' txtEndTimeHH.ReadOnly = True
                    ' txtEndTimeMM.CssClass = "textboxgrey"
                    ' txtEndTimeMM.ReadOnly = True

                Else
                    btnSave.Enabled = True
                    ddlCourseType.Enabled = True
                    ddlCourseTitle.Enabled = True
                    chkInternalSession.Enabled = True
                    ddlAOffice.Enabled = True
                    ddlTrainer1.Enabled = True
                    ddlTrainer2.Enabled = True
                    '   txtNotes.Enabled = True
                    Img2.Visible = True
                    '  imgStartDate.Style.Add("display", "block")

                    ' imgEndDate.Style.Add("display", "block")


                    txtStartDate.CssClass = "textbox"
                    txtStartDate.ReadOnly = False
                    txtStartTimeHH.CssClass = "textbox"
                    txtStartTimeHH.ReadOnly = False
                    txtStartTimeMM.CssClass = "textbox"
                    txtStartTimeMM.ReadOnly = False

                    txtEndDate.CssClass = "textbox"
                    txtEndDate.ReadOnly = False
                    txtEndTimeHH.CssClass = "textbox"
                    txtEndTimeHH.ReadOnly = False
                    txtEndTimeMM.CssClass = "textbox"
                    txtEndTimeMM.ReadOnly = False

                End If

                If Val(.Attributes("RegiParticipantCount").Value) = 1 Then
                    ddlCourseTitle.Enabled = False
                    ddlCourseType.Enabled = False
                End If


                ' If any participants given feedback then disable Trainer combo else enable it.
                If Not .Attributes("FEEDBACKPARTICIPANTCOUNT") Is Nothing Then
                    If Val(.Attributes("FEEDBACKPARTICIPANTCOUNT").Value) > 0 Then
                        ddlTrainer1.Enabled = False
                        ddlTrainer2.Enabled = False
                    Else
                        ddlTrainer1.Enabled = True
                        ddlTrainer2.Enabled = True
                    End If
                End If



                Dim strDate As String = .Attributes("TR_COURSES_EXPECT_DATE").Value
                If strDate <> "" Then
                    Dim day As String = strDate.Split("/").GetValue(0)
                    Dim month As String = strDate.Split("/").GetValue(1)
                    If day.Length = 1 Then
                        day = "0" & day
                    End If
                    If month.Length = 1 Then
                        month = "0" & month
                    End If
                    strDate = day & "/" & month & "/" & strDate.Split("/").GetValue(2)
                    txtStartDate.Text = objeAAMS.GetDateFormat(strDate, "MM/dd/yyyy", "dd/MM/yyyy", "/")
                    'txtStartDate.Text = txtStartDate.Text & " " & .Attributes("TR_COURSES_START_TIME").Value.Substring(0, 2) & ":" & .Attributes("TR_COURSES_START_TIME").Value.Substring(2)
                    txtStartTimeHH.Text = .Attributes("TR_COURSES_START_TIME").Value.Substring(0, 2)
                    txtStartTimeMM.Text = .Attributes("TR_COURSES_START_TIME").Value.Substring(2)

                End If

                Dim strEndDate As String = .Attributes("TR_COURSES_END_DATE").Value
                If strEndDate <> "" Then
                    Dim day As String = strEndDate.Split("/").GetValue(0)
                    Dim month As String = strEndDate.Split("/").GetValue(1)
                    If day.Length = 1 Then
                        day = "0" & day
                    End If
                    If month.Length = 1 Then
                        month = "0" & month
                    End If
                    strEndDate = day & "/" & month & "/" & strDate.Split("/").GetValue(2)
                    txtEndDate.Text = objeAAMS.GetDateFormat(strEndDate, "MM/dd/yyyy", "dd/MM/yyyy", "/")
                    'txtEndDate.Text = txtEndDate.Text & " " & .Attributes("TR_COURSES_END_TIME").Value.Substring(0, 2) & ":" & .Attributes("TR_COURSES_END_TIME").Value.Substring(2)

                    txtEndTimeHH.Text = .Attributes("TR_COURSES_END_TIME").Value.Substring(0, 2)
                    txtEndTimeMM.Text = .Attributes("TR_COURSES_END_TIME").Value.Substring(2)
                End If

                chkInternalSession.Checked = .Attributes("INTERNAL").Value

                ddlCourseTitle.Items.FindByText(.Attributes("TR_COURSE_NAME").Value).Selected = True

                ddlCourseType.SelectedValue = IIf(.Attributes("SHOWONWEB").Value = "1", 1, "")
                ' txtCourseTitle.Text = .Attributes("TR_COURSE_NAME").Value
                txtCourseLevel.Text = .Attributes("TR_COURSELEVEL_NAME").Value
                txtDuration.Text = ddlCourseTitle.SelectedValue.Split("|").GetValue(2)
                hdDuration.Value = ddlCourseTitle.SelectedValue.Split("|").GetValue(2)
                hdNoOfTest.Value = ddlCourseTitle.SelectedValue.Split("|").GetValue(3)
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
        Try
            objInputXml.LoadXml("<TR_UPDATECOURSES_INPUT><COURSES TR_COURSES_ID='' TR_COURSES_REQ_CONFIG='' EMPLOYEE_ID2='' EMPLOYEE_ID1='' TR_COURSES_NBPART='' TR_COURSES_EXPECT_DATE='' TR_CLOCATION_ID='' TR_COURSE_ID='' TR_COURSES_END_DATE='' TR_COURSES_START_TIME='' TR_COURSES_END_TIME='' INTERNAL='' TR_COURSE_NAME='' /><EMP EMPLOYEEID=''/></TR_UPDATECOURSES_INPUT>")

            With objInputXml.DocumentElement.SelectSingleNode("COURSES")
                ' .Attributes("TR_COURSES_REQ_CONFIG").Value = objeAAMS.GetDateFormat(txtDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("TR_COURSES_REQ_CONFIG").Value = txtNotes.Text
                .Attributes("EMPLOYEE_ID2").Value = ddlTrainer2.SelectedValue
                .Attributes("EMPLOYEE_ID1").Value = ddlTrainer1.SelectedValue

                '.Attributes("TR_COURSES_EXPECT_DATE").Value = objeAAMS.GetDateFormat(txtStartDate.Text.Split(" ").GetValue(0), "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("TR_COURSES_EXPECT_DATE").Value = objeAAMS.GetDateFormat(txtStartDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("TR_CLOCATION_ID").Value = hdTrainingRoomPage.Value
                .Attributes("TR_COURSE_ID").Value = ddlCourseTitle.SelectedValue.Split("|").GetValue(1) 'hdCourseID.Value

                .Attributes("TR_COURSE_NAME").Value = ddlCourseTitle.SelectedItem.Text

                '.Attributes("TR_COURSES_END_DATE").Value = objeAAMS.GetDateFormat(txtEndDate.Text.Split(" ").GetValue(0), "dd/MM/yyyy", "yyyyMMdd", "/")
                .Attributes("TR_COURSES_END_DATE").Value = objeAAMS.GetDateFormat(txtEndDate.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                Dim strStartTimeHH As String = txtStartTimeHH.Text.Trim
                Dim strStartTimeMM As String = txtStartTimeMM.Text.Trim

                Dim strEndTimeHH As String = txtEndTimeHH.Text.Trim
                Dim strEndTimeMM As String = txtEndTimeMM.Text.Trim

                'If strStartTimeHH.Length = 0 And strStartTimeMM.Length <> 0 Then
                '    If strStartTimeMM.Length >= 1 Then
                '        strStartTimeHH = "00"
                '    End If
                'End If

                If strStartTimeHH.Length = 1 Then
                    strStartTimeHH = "0" & strStartTimeHH
                End If

                If strStartTimeMM.Length = 1 Then
                    strStartTimeMM = "0" & strStartTimeMM
                End If

                If strStartTimeHH.Length = 0 Then
                    strStartTimeHH = "00"
                End If

                If strStartTimeMM.Length = 0 Then
                    strStartTimeMM = "00"
                End If


                If strEndTimeHH.Length = 1 Then
                    strEndTimeHH = "0" & strEndTimeHH
                End If

                If strEndTimeMM.Length = 1 Then
                    strEndTimeMM = "0" & strEndTimeMM
                End If

                If strEndTimeHH.Length = 0 Then
                    strEndTimeHH = "00"
                End If

                If strEndTimeMM.Length = 0 Then
                    strEndTimeMM = "00"
                End If


                '.Attributes("TR_COURSES_START_TIME").Value = txtStartDate.Text.Split(" ").GetValue(1).ToString.Split(":").GetValue(0) & txtStartDate.Text.Split(" ").GetValue(1).ToString.Split(":").GetValue(1)
                .Attributes("TR_COURSES_START_TIME").Value = strStartTimeHH & strStartTimeMM
                ' If txtEndDate.Text <> "" Then
                '.Attributes("TR_COURSES_END_TIME").Value = txtEndDate.Text.Split(" ").GetValue(1).ToString.Split(":").GetValue(0) & txtEndDate.Text.Split(" ").GetValue(1).ToString.Split(":").GetValue(1)
                .Attributes("TR_COURSES_END_TIME").Value = strEndTimeHH & strEndTimeMM
                ' End If


                .Attributes("INTERNAL").Value = IIf(chkInternalSession.Checked = True, "1", "0")

                If hdPageCourseSessionID.Value <> "" Then
                    .Attributes("TR_COURSES_ID").Value = hdPageCourseSessionID.Value
                End If

            End With

            'added for history on 2 feb 09

            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EMP").Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
            End If

            'end

            'Here Back end Method Call
            objOutputXml = objbzCourseSession.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If hdPageCourseSessionID.Value = "" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    hdPageCourseSessionID.Value = objOutputXml.DocumentElement.SelectSingleNode("COURSES").Attributes("TR_COURSES_ID").Value
                    hdEnPageCourseSessionID.Value = objED.Encrypt(hdPageCourseSessionID.Value)
                    Bindata()
                Else
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                CheckSecurity()

                If hdEnPageCourseSessionID.Value = "" Then
                    btnHistory.Enabled = False
                Else
                    btnHistory.Enabled = True
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally

        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_CourseSession.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdPageCourseSessionID.Value <> "" Then
                ViewRecords()
            Else
                Dim strQueryString As String = Request.QueryString.ToString
                Response.Redirect("TRUP_CourseSession.aspx?" + strQueryString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    'Protected Sub gvRegisterTab_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRegisterTab.RowCommand
    '    Dim strID As String = ""
    '    Dim objXmlDoc As New XmlDocument
    '    Dim objXmlNode As XmlNode
    '    Try
    '        If e.CommandName = "DeleteX" Then
    '            strID = e.CommandArgument.ToString()
    '            objXmlDoc.LoadXml(hdData.Value)
    '            objXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("Register/Details[@Type='" + strID.Split("|").GetValue(0) + "' and @Id='" + strID.Split("|").GetValue(1) + "']")
    '            objXmlDoc.DocumentElement.SelectSingleNode("Register").RemoveChild(objXmlNode)
    '            hdData.Value = objXmlDoc.OuterXml
    '            Dim objNodeReader As XmlNodeReader
    '            Dim ds As New DataSet
    '            objNodeReader = New XmlNodeReader(objXmlDoc)
    '            ds.ReadXml(objNodeReader)
    '            gvRegisterTab.DataSource = ds.Tables("Details")
    '            gvRegisterTab.DataBind()

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub
 Sub FillCourseType()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCourse As New AAMS.bizTraining.bzCourse
        ddlCourseTitle.Items.Clear()
        Try

            objInputXml.LoadXml("<TR_SEARCH_COURSE_INPUT><TR_COURSE_NAME /><TR_COURSELEVEL_ID /><ShowOnWeb /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCH_COURSE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ShowOnWeb").InnerText = ddlCourseType.SelectedValue
            objOutputXml = objCourse.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("COURSE")
                    Dim li As New ListItem(objnode.Attributes("TR_COURSE_NAME").Value, objnode.Attributes("TR_COURSELEVEL_NAME").Value & "|" & objnode.Attributes("TR_COURSE_ID").Value & "|" & objnode.Attributes("TR_COURSE_DURATION").Value & "|" & objnode.Attributes("TR_COURSE_NO_TEST").Value)
                    ddlCourseTitle.Items.Add(li)
                Next

                'objXmlReader = New XmlNodeReader(objOutputXml)
                'ds.ReadXml(objXmlReader)
                'If ds.Tables("COURSE").Rows.Count <> 0 Then
                '    ddlCourseTitle.DataSource = ds.Tables("COURSE")
                '    ddlCourseTitle.DataTextField = ""
                '    ddlCourseTitle.DataValueField = ""
                '    ddlCourseTitle.DataBind()
                'Else
                '    ddlCourseTitle.DataSource = Nothing
                '    ddlCourseTitle.DataBind()
                'End If

            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            ddlCourseTitle.Items.Insert(0, New ListItem("--Select One--", ""))
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub ddlCourseType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCourseType.SelectedIndexChanged
        FillCourseType()
    End Sub

    Protected Sub ddlAOffice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAOffice.SelectedIndexChanged
        objeAAMS.BindTrainer(ddlTrainer1, ddlAOffice.SelectedValue, 1)
        objeAAMS.BindTrainer(ddlTrainer2, ddlAOffice.SelectedValue, 2)
    End Sub

    Private Sub CheckSecurity()
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
                btnHistory.Enabled = False
 
            End If

            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            If strBuilder(2) = "0" And (hdPageCourseSessionID.Value <> "" Or Request.QueryString("CourseSessionID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If

            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

    End Sub
   
End Class
