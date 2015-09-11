
Partial Class Training_TRSR_CourseSession
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
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
            CheckSecurity()
            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            If Not IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateFormCourseSession();")

                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlRegion, "REGION", True, 3)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)
                ' objeAAMS.BindTrainer(ddlTrainer1, "", 3)
                'objeAAMS.BindTrainer(ddlTrainer2, "", 3)

                '   Checking Permission For Own Office and Region start.
                If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                    ddlAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    ddlAOffice.Enabled = False
                End If


                '   Checking Permission For Own Office and Region end.

                '    BindTrainer()

            End If
            txtTrainingRoom.Text = Request.Form("txtTrainingRoom")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub CheckSecurity()
        Dim objSecurityXml As New XmlDocument
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
        End If

        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSearch.Enabled = False

            End If
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
            End If
            If strBuilder(4) = "0" Then

                btnExport.Enabled = False
            End If

        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
    End Sub

    Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
            objInputXml.LoadXml("<TR_DELETECOURSES_INPUT><TR_COURSES_ID /></TR_DELETECOURSES_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSES_ID").InnerText = hdDeleteId.Value
            hdDeleteId.Value = ""
            objOutputXml = objbzCourseSession.Delete(objInputXml)
            SearchRecords(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                lblError.Text = objeAAMSMessage.messDelete
            Else

                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_CourseSession.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TRSR_CourseSession.aspx")
    End Sub

    Sub SearchRecords(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCourseSession As New AAMS.bizTraining.bzCourseSession
        objInputXml.LoadXml("<TR_SEARCHCOURSES_INPUT><OfficeId/><ParticipantName/><LCODE/><AGENCYNAME/><CourseName/><CourseID/><LOCATION_ID/><TRAINER1/><TRAINER2/><AOFFICE/><REGION/><DATE_TYPE_FROM/><DATE_TYPE_TO/><DATE_TYPE/><DateFrom/><DateTo/><EDateFrom/><EDateTo/><SearchDateType/><ResponsibleStaffID/><internal/><SecRegionID/><ShowOnWeb/> <Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCHCOURSES_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = Request.Form("txtAgency") 'txtAgency.Text.Trim()

        objInputXml.DocumentElement.SelectSingleNode("CourseID").InnerText = ddlCourse.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("LOCATION_ID").InnerText = hdRoomID.Value
        objInputXml.DocumentElement.SelectSingleNode("TRAINER1").InnerText = txtTrainer1.Text ' ddlTrainer1.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("TRAINER2").InnerText = txtTrainer2.Text ' ddlTrainer2.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ddlRegion.SelectedValue
        'objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = txtAgencyStaff.Text

        objInputXml.DocumentElement.SelectSingleNode("OfficeId").InnerText = txtOfficeID.Text
        '  objInputXml.DocumentElement.SelectSingleNode("EmployeeName").InnerText = txtEmployeeName.Text
        objInputXml.DocumentElement.SelectSingleNode("ParticipantName").InnerText = txtParticipant.Text

        ' If user select agency staff then input from txtAgencyStaff
        ' If user select participant then input from txtParticipant

        'If Request.Form("txtAgencyStaff") <> "" Then
        '    objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtAgencyStaff")
        'Else
        '    If Request.Form("txtParticipant") <> "" Then
        '        objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtParticipant")
        '    End If
        'End If

        ' Security Input Xml Start.
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
        ' Security Input Xml End.



        Dim strDateFrom As String = Request.Form("txtStartDateFrom").Trim()
        If strDateFrom <> "" Then
            strDateFrom = strDateFrom.Split("/").GetValue(1) + "/" + strDateFrom.Split("/").GetValue(0) + "/" + strDateFrom.Split("/").GetValue(2)
        End If

        Dim strDateTo As String = Request.Form("txtStartDateTo").Trim()
        If strDateTo <> "" Then
            strDateTo = strDateTo.Split("/").GetValue(1) + "/" + strDateTo.Split("/").GetValue(0) + "/" + strDateTo.Split("/").GetValue(2)
        End If


        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_FROM").InnerText = strDateFrom
        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE_TO").InnerText = strDateTo
        objInputXml.DocumentElement.SelectSingleNode("DATE_TYPE").InnerText = ddlDateType.SelectedValue


        'comment on 2feb 09

        'Dim strEDateFrom As String = Request.Form("txtEndDateFrom").Trim()
        'If strEDateFrom <> "" Then
        '    strEDateFrom = strEDateFrom.Split("/").GetValue(1) + "/" + strEDateFrom.Split("/").GetValue(0) + "/" + strEDateFrom.Split("/").GetValue(2)
        'End If

        'Dim strEDateTo As String = Request.Form("txtEndDateTo").Trim()
        'If strEDateTo <> "" Then
        '    strEDateTo = strEDateTo.Split("/").GetValue(1) + "/" + strEDateTo.Split("/").GetValue(0) + "/" + strEDateTo.Split("/").GetValue(2)
        'End If


        ' objInputXml.DocumentElement.SelectSingleNode("EDateTo").InnerText = strEDateTo
        'end comment

        'objInputXml.DocumentElement.SelectSingleNode("SearchDateType").InnerText = ddlAOffice.SelectedValue
        'objInputXml.DocumentElement.SelectSingleNode("ResponsibleStaffID").InnerText = txtAgency.Text
        If chkInternalSession.Checked = True Then
            objInputXml.DocumentElement.SelectSingleNode("internal").InnerText = "1"
        End If

        If chkShowOnWeb.Checked = True Then
            objInputXml.DocumentElement.SelectSingleNode("ShowOnWeb").InnerText = "1"
        Else
            objInputXml.DocumentElement.SelectSingleNode("ShowOnWeb").InnerText = ""
        End If
        objInputXml.DocumentElement.SelectSingleNode("SecRegionID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

        'Start CODE for sorting and paging
        If Operation = PageOperation.Search Then
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "TR_COURSE_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "TR_COURSE_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
        End If
        'End Code for paging and sorting



        ' Here Back end Method Call
        objOutputXml = objbzCourseSession.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvCourseSession.DataSource = ds.Tables("DETAILS")
            gvCourseSession.DataBind()
            'Code Added For Paging And Sorting In case Of Delete The Record
            ViewState("PrevSearching") = objInputXml.OuterXml
            pnlPaging.Visible = True
            Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
            Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
            If count <> ddlPageNumber.Items.Count Then
                ddlPageNumber.Items.Clear()
                For i As Integer = 1 To count
                    ddlPageNumber.Items.Add(i.ToString)
                Next
            End If
            ddlPageNumber.SelectedValue = selectedValue
            'Code for hiding prev and next button based on count
            If count = 1 Then
                'pnlPaging.Visible = False
                ' ddlPageNumber.Visible = False
                lnkNext.Visible = False
                lnkPrev.Visible = False
            Else
                'ddlPageNumber.Visible = True
                lnkPrev.Visible = True
                lnkNext.Visible = True
            End If

            'Code for hiding next button when pagenumber is equal to page count
            If ddlPageNumber.SelectedValue = count.ToString Then
                lnkNext.Visible = False
            Else
                lnkNext.Visible = True
            End If

            'Code for hiding prev button when pagenumber is 1
            If ddlPageNumber.SelectedValue = "1" Then
                lnkPrev.Visible = False
            Else
                lnkPrev.Visible = True
            End If
            hdRecordOnCurrentPage.Value = ds.Tables("DETAILS").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
            txtTotalActualPartRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("TOTAL_TR_COURSES_NBPART").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'
            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "TR_COURSE_NAME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(0).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(0).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_CLOCATION_NAME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(1).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(1).Controls.Add(imgDown)
            '        End Select
            '    Case "TRAINER1"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(2).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(2).Controls.Add(imgDown)
            '        End Select

            '    Case "TRAINER2"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(3).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(3).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_COURSES_EXPECT_DATE"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(4).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(4).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_COURSES_END_DATE"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(5).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(5).Controls.Add(imgDown)
            '        End Select

            '    Case "TR_COURSES_START_TIME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(6).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(6).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_COURSES_END_TIME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(7).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(7).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_COURSES_NBPART"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvCourseSession.HeaderRow.Cells(8).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvCourseSession.HeaderRow.Cells(8).Controls.Add(imgDown)
            '        End Select

            'End Select
            '  Added Code To Show Image'

            ' End of Code Added For Paging And Sorting In case Of Delete The Record
            lblError.Text = ""
            SetImageForSorting(gvCourseSession)
        Else
            gvCourseSession.DataSource = Nothing
            gvCourseSession.DataBind()
            txtTotalRecordCount.Text = "0"
            txtTotalActualPartRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub
  
    Protected Sub gvCourseSession_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCourseSession.RowDataBound
         If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim hdCourseSessionID As HiddenField
        Dim btnEdit As LinkButton
        Dim btnDelete As LinkButton
        Dim btnSelect As LinkButton
        btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        btnSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)
        hdCourseSessionID = CType(e.Row.FindControl("hdCourseSessionID"), HiddenField)


        Dim strStartTime As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSES_START_TIME")
        If strStartTime.Length = 4 Then
            e.Row.Cells(6).Text = strStartTime.Substring(0, 2) & ":" & strStartTime.Substring(2, 2)
        End If


        Dim strEndTime As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSES_END_TIME")
        If strEndTime.Length = 4 Then
            e.Row.Cells(7).Text = strEndTime.Substring(0, 2) & ":" & strEndTime.Substring(2, 2)
        End If

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Session']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                btnDelete.Enabled = False
            Else
                btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdCourseSessionID.Value + "');")
            End If
            'If strBuilder(2) = "0" Then
            '    btnEdit.Enabled = False
            'Else
            '    btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdCourseSessionID.Value + "');")
            'End If
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdCourseSessionID.Value) + "');")

        Else
            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdCourseSessionID.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdCourseSessionID.Value) + "');")

        End If

        If Not Request.QueryString("Popup") Is Nothing Then
            Dim course As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSE_NAME").ToString.Replace(vbCrLf, "")
            Dim trainingRoom As String = DataBinder.Eval(e.Row.DataItem, "TR_CLOCATION_NAME").ToString.Replace(vbCrLf, "")
            Dim startDate As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSES_EXPECT_DATE")
            Dim maxParticipant As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSES_NBPART")
            dim CourseLevel as String=databinder.Eval(e.Row.DataItem,"TR_COURSELEVEL_NAME")
            Dim TRAINER1 As String = DataBinder.Eval(e.Row.DataItem, "TRAINER1")
            Dim TRAINER2 As String = DataBinder.Eval(e.Row.DataItem, "TRAINER2")
            Dim strCourseSessionID As String = DataBinder.Eval(e.Row.DataItem, "TR_COURSES_ID")
            If TRAINER2.Trim <> "" Then
                TRAINER1 = TRAINER1 & "," & TRAINER2
            End If


            btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunctionCourseSession('" + hdCourseSessionID.Value + "','" + course + "','" + trainingRoom + "','" + startDate + "','" + maxParticipant + "','" + TRAINER1 + "','" + CourseLevel + "','" + strCourseSessionID + "') ;")
        Else
            btnSelect.Visible = False
        End If
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
    '        ddlTrainer1.Items.Insert(0, New ListItem("--All--", " "))
    '        ddlTrainer2.Items.Insert(0, New ListItem("--All--", " "))
    '        lblError.Text = ""
    '    Else
    '        ddlTrainer1.Items.Clear()
    '        ddlTrainer2.Items.Clear()
    '        ddlTrainer1.Items.Insert(0, New ListItem("--All--", " "))
    '        ddlTrainer2.Items.Insert(0, New ListItem("--All--", " "))
    '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '    End If
    'End Sub
#End Region

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCourseSession_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCourseSession.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCourseSession_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCourseSession.Sorting
        Try


            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchRecords(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Course", "Training Room", "Trainer 1", "Trainer 2", "Start Date", "End Date", "Start Time", "End Time", "Actual No of Participants", "Shortlist"}
        Dim intArray() As Integer = {3, 4, 5, 6, 7, 8, 9, 10, 11, 12}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCOURSESESSION.xls")
    End Sub
    'End Code For Export
End Class
