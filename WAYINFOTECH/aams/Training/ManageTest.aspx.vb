Imports System.Xml
Imports System.Data

Partial Class ManageTest
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objMessages As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Const strUpdateTest As String = "<T_MS_TEST_QUESTIONS_INPUT><TEST ACTION='' TR_COURSE_ID='' DAYS='' USER_ID='' TR_COURSE_NAME='' NO_OF_QUESTIONS='' TEST_DESCRIPTION='' TEST_TIME='' TOTAL_MARKS='' START_DATE='' END_DATE=''/><QUESTIONS UP_ACTION='' QS_ID='' QS_TEXT='' QS_OPTION1='' QS_OPTION2='' QS_OPTION3='' QS_OPTION4='' QS_RIGHT_OPTION='' MARK_FOR_RIGHT_ANSWER='' MARK_FOR_WRONG_ANSWER='' SVC_ID='' QL_ID='' QS_MANDATORY=''/><ERRORS STATUS=''><ERROR CODE='' DESC=''/></ERRORS></T_MS_TEST_QUESTIONS_INPUT>"
    Const strViewTest As String = "<MS_VIEWTESTQUESTIONS_INPUT><TR_COURSE_ID></TR_COURSE_ID><DAYS></DAYS></MS_VIEWTESTQUESTIONS_INPUT>"
    Dim objXmltestInput As New XmlDocument
    Dim objXmlservertestInput As New XmlDocument
    Dim objXmlTestOutput As XmlDocument
    Dim strErrorMsg As String
    Dim objfunction As New Ack_Functions
    Dim objTest As New AAMS.bizTraining.bzTraining
    Dim objNode, objNode1, objNode3, objNode4 As XmlNode
    Dim objnodeList As XmlNodeList
    Public i As Integer = 0
#End Region

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

        
            Session("PageName") = Request.Url.ToString()
            CheckSecurity()
            btnAddToGrid.Attributes.Add("OnClick", "return ValidateQuestion();")
            BtnSave.Attributes.Add("OnClick", "return ValidateTest();")
            If Not Page.IsPostBack Then
                objXmlservertestInput.LoadXml(strUpdateTest)
                objXmltestInput.LoadXml(strUpdateTest)
                Session("objXmltestInput") = objXmltestInput
                Session("objXmlservertestInput") = objXmlservertestInput
                '  FillComboBoxes()
                If Request.Params("Test_id") <> "" Then
                    fillpage()
                    ' ddlDays.SelectedValue = 1
                    ViewState("Action") = "U"
                Else
                    ViewState("Action") = "I"
                End If
                If Request.QueryString("Msg") IsNot Nothing Then
                    lblError.Text = objMessages.messUpdate
                End If
            End If
            ' code added by pankaj
            ' code for copy questions
            If hdCourseID.Value <> "" Then
                ViewQuestion()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try



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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Test Question']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Test Question']").Attributes("Value").Value)
                End If
                'If strBuilder(2) = "0" Then
                '    BtnSave.Enabled = False
                '    btnAddToGrid.Enabled = False
                '    btnCopy.Enabled = False
                '    btnQuestionSet.Enabled = False
                'End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    'BtnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    BtnSave.Enabled = False
                    btnAddToGrid.Enabled = False
                    btnCopy.Enabled = False
                End If
                If strBuilder(2) = "0" And (Request.Params("Test_id") IsNot Nothing OrElse Request.Params("Test_id") <> "") Then
                    BtnSave.Enabled = False
                    btnAddToGrid.Enabled = False
                    btnCopy.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    BtnSave.Enabled = True
                    btnAddToGrid.Enabled = True
                    btnCopy.Enabled = True
                End If
                If strBuilder(4) = "0" Then
                    btnQuestionSet.Enabled = False
                End If

                ''''''''''''''''''''''''
                'If strBuilder(0) = "0" Then
                '    Response.Redirect("../NoRights.aspx")

                'End If
                'If strBuilder(1) = "0" Then
                '    BtnSave.Enabled = False
                'End If
                'If strBuilder(2) = "0" Then
                '    ' Response.Redirect("../NoRights.aspx", False)
                '    BtnSave.Enabled = False
                '    btnAddToGrid.Enabled = False
                '    btnCopy.Enabled = False
                '    ' btnQuestionSet.Enabled = False
                'End If
                'If strBuilder(4) = "0" Then
                '   btnQuestionSet.Enabled = False
                'End If




            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnAddToGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddToGrid.Click"
    Protected Sub btnAddToGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddToGrid.Click

        objXmltestInput.LoadXml(strUpdateTest)
        If CType(Session("objXmltestInput"), XmlDocument) Is Nothing Then
        Else
            objXmltestInput = CType(Session("objXmltestInput"), XmlDocument)
        End If
        lblError.Text = String.Empty
        If objXmltestInput.DocumentElement.SelectSingleNode("QUESTIONS") Is Nothing Then
            objXmltestInput.LoadXml(strUpdateTest)
            objNode = objXmltestInput.DocumentElement.SelectSingleNode("QUESTIONS")
            objNode1 = objNode.CloneNode(True)
        Else
            objNode = objXmltestInput.DocumentElement.SelectSingleNode("QUESTIONS")
            objNode1 = objNode.CloneNode(True)
        End If
        If CType(Session("objXmltestInput"), XmlDocument) Is Nothing Or objXmltestInput.DocumentElement.SelectSingleNode("QUESTIONS").Attributes("QS_TEXT").InnerText = "" Then
            objXmltestInput.DocumentElement.RemoveChild(objNode)
        End If

        If objXmltestInput.DocumentElement.SelectSingleNode("QUESTIONS[@QS_TEXT='" + txtQuest.Text + "']") Is Nothing Then

            With objNode1
                If Qs_id.Value <> "" Then

                    .Attributes("UP_ACTION").InnerText = "U"
                    .Attributes("QS_ID").InnerText = Qs_id.Value
                Else

                    .Attributes("UP_ACTION").InnerText = "I"
                    .Attributes("QS_ID").InnerText = Qs_id.Value
                End If
                .Attributes("QS_TEXT").InnerText = txtQuest.Text.Replace("'", "")
                .Attributes("QS_OPTION1").InnerText = txtAns1.Text
                .Attributes("QS_OPTION2").InnerText = txtAns2.Text
                .Attributes("QS_OPTION3").InnerText = txtAns3.Text
                .Attributes("QS_OPTION4").InnerText = txtAns4.Text
                .Attributes("QS_RIGHT_OPTION").InnerText = drpRAns.SelectedValue
                .Attributes("MARK_FOR_RIGHT_ANSWER").InnerText = txtMforRAns.Text
                .Attributes("MARK_FOR_WRONG_ANSWER").InnerText = txtMforWAns.Text
                .Attributes("SVC_ID").InnerText = "" 'drpServiceType.SelectedValue
                .Attributes("QL_ID").InnerText = "" 'drpQlevel.SelectedValue
                'If (chkMandatoryQ.Checked) Then
                .Attributes("QS_MANDATORY").InnerText = 1
                'Else
                '.Attributes("QS_MANDATORY").InnerText = 0
                'End If
            End With
            Qs_id.Value = ""
            objXmltestInput.DocumentElement.AppendChild(objNode1)
            objNode1 = objNode.CloneNode(True)
            Session("objXmltestInput") = objXmltestInput
            If ViewState("SortName") IsNot Nothing Then
                showGrid(objXmltestInput, ViewState("SortName"), ViewState("Desc"))
            Else
                showGrid(objXmltestInput)
            End If
            'BindControls()
            resetQuestcontrol()

        Else
            lblError.Text = "Already Exists"
        End If

        txtQuest.Focus()
    End Sub
#End Region

#Region "BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click"
    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        '  If objfunction.CheckSecurity(5, 3, CType(Session("SecurityXML"), XmlDocument)) And ViewState("Action") = "U" Then
        Dim intNoOfQues As Integer = 0
        Try
            If txtTestTime.Text = "" Or txtTestTime.Text = "0" Then
                lblError.Text = "Test time is mandatory."
                Exit Sub
            End If
            If txtTMarks.Text = "" Or txtTMarks.Text = "0" Then
                lblError.Text = "Total marks is mandatory."
                Exit Sub
            End If

            intNoOfQues = txtNoOfQuest.Text.Trim()
            If intNoOfQues <= GrdTestDetails.Rows.Count Then
                SaveQuestion()
            Else
                lblError.Text = "Questions should be greater than or equal to total number of Marks."

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

        '   ElseIf objfunction.CheckSecurity(5, 2, CType(Session("SecurityXML"), XmlDocument)) And ViewState("Action") = "I" Then
        '  SaveQuestion()
        ' Else
        ' lblError.Text = "You donot have sufficient rights."
        ' End If
    End Sub
#End Region

#Region "SaveQuestion()"
    Protected Sub SaveQuestion()
        Try
            Dim strstartdate As String = "" ' = ConvertTextDate(txtStartDate.Text)
            Dim strEnddate As String = "" ' = ConvertTextDate(txtEndDate.Text)
            Dim currdate As Date = Now.Date

            'If (CDate(strstartdate.Substring(4, 2) + "/" + strstartdate.Substring(6, 2) + "/" + strstartdate.Substring(0, 4)) < currdate) Then
            '    lblError.Text = "Start date can not be less than Current date"
            '    Exit Sub
            'End If
            'If (CDate(strEnddate.Substring(4, 2) + "/" + strEnddate.Substring(6, 2) + "/" + strEnddate.Substring(0, 4)) < CDate(strstartdate.Substring(4, 2) + "/" + strstartdate.Substring(6, 2) + "/" + strstartdate.Substring(0, 4))) Then
            '    lblError.Text = "End date can not be less than Start date"
            '    Exit Sub
            'End If

            objXmlservertestInput.LoadXml(strUpdateTest)
            objXmltestInput.LoadXml(strUpdateTest)
            objNode = objXmlservertestInput.DocumentElement.SelectSingleNode("QUESTIONS")
            'objNode1 = objNode.CloneNode(True)
            objXmlservertestInput.DocumentElement.RemoveChild(objNode)
            If Session("objXmltestInput") IsNot Nothing Then
                objXmltestInput = CType(Session("objXmltestInput"), XmlDocument)
            End If

            If CType(Session("objXmlservertestInput"), XmlDocument) Is Nothing Then
            Else
                objXmlservertestInput = CType(Session("objXmlservertestInput"), XmlDocument)
            End If
            objnodeList = objXmltestInput.DocumentElement.SelectNodes("//QUESTIONS[@UP_ACTION='I']")
            For Each objNode In objnodeList
                objXmlservertestInput.DocumentElement.AppendChild(objXmlservertestInput.ImportNode(objNode, True))
            Next
            objnodeList = objXmltestInput.DocumentElement.SelectNodes("//QUESTIONS[@UP_ACTION='U']")
            For Each objNode In objnodeList
                If (objXmlservertestInput.DocumentElement.SelectNodes("//QUESTIONS[@QS_ID='" + objNode.Attributes("QS_ID").InnerText + "']").Count <= 0) Then
                    objXmlservertestInput.DocumentElement.AppendChild(objXmlservertestInput.ImportNode(objNode, True))
                End If
            Next

            If objXmlservertestInput.DocumentElement.SelectSingleNode("QUESTIONS").Attributes("QS_TEXT").InnerText = "" Then
                objNode3 = objXmlservertestInput.DocumentElement.SelectSingleNode("QUESTIONS")
                objXmlservertestInput.DocumentElement.RemoveChild(objNode3)
            End If

            With objXmlservertestInput.DocumentElement.SelectSingleNode("TEST")
                If Test_id.Value <> "" Then
                    .Attributes("TR_COURSE_ID").InnerText = Test_id.Value
                    .Attributes("ACTION").InnerText = "U"
                Else
                    .Attributes("TEST_ID").InnerText = ""
                    .Attributes("ACTION").InnerText = "I"
                End If
                .Attributes("USER_ID").InnerText = Session("User_Id")
                .Attributes("TR_COURSE_NAME").InnerText = txtTestName.Text
                .Attributes("NO_OF_QUESTIONS").InnerText = txtNoOfQuest.Text
                .Attributes("TEST_DESCRIPTION").InnerText = txtTestDesc.Text
                .Attributes("TEST_TIME").InnerText = txtTestTime.Text
                .Attributes("TOTAL_MARKS").InnerText = txtTMarks.Text
                .Attributes("START_DATE").InnerText = strstartdate
                .Attributes("END_DATE").InnerText = strEnddate
                .Attributes("DAYS").InnerText = ddlDays.SelectedValue
            End With
            '---------------save data ------------
            objXmlTestOutput = objTest.Update(objXmlservertestInput)
            strErrorMsg = objfunction.CheckError(objXmlTestOutput)

            If strErrorMsg <> String.Empty Then
                lblError.Text = strErrorMsg
                'objXmltestInput.LoadXml(strUpdateTest)
                'objXmlservertestInput.LoadXml(strUpdateTest)
                'Session("objXmltestInput") = objXmltestInput
                'Session("objXmlservertestInput") = objXmlservertestInput
                Exit Sub

            Else
                lblError.Text = objMessages.messInsert
                Session.Remove("objXmltestInput")
                Session.Remove("objXmlservertestInput")
                '  resetTestcontrol()
                resetQuestcontrol()
                GrdTestDetails.Visible = True
                ' code modified  
                Response.Redirect("ManageTest.aspx?Msg=U&Test_id=" + Request.QueryString("Test_id") + "&Days=" + ddlDays.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "GrdTestDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdTestDetails.RowCommand"
    Protected Sub GrdTestDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdTestDetails.RowCommand
        Try
            Dim array() As String
            array = e.CommandArgument.ToString.Split("~")
            objXmltestInput = CType(Session("objXmltestInput"), XmlDocument)
            objXmlservertestInput = CType(Session("objXmlservertestInput"), XmlDocument)

            If e.CommandName = "EditX" Then
                If objfunction.CheckSecurity(5, 4, CType(Session("SecurityXML"), XmlDocument)) Then
                    objNode = objXmltestInput.SelectSingleNode("//QUESTIONS[@QS_TEXT='" + array(0) + "' and @QS_OPTION1='" + array(1).ToString + "' and @QS_OPTION2='" + array(2).ToString + "' and @QS_RIGHT_OPTION='" + array(3).ToString + "']")
                    If Not objNode Is Nothing Then
                        If (objNode.Attributes("QS_ID").InnerText = "") Then
                            objXmltestInput.DocumentElement.RemoveChild(objNode)
                            Session("objXmltestInput") = objXmltestInput
                            If ViewState("SortName") IsNot Nothing Then
                                showGrid(objXmltestInput, ViewState("SortName"), ViewState("Desc"))
                            Else
                                showGrid(objXmltestInput)
                            End If
                            'BindControls()

                        Else
                            '--------------------server xml file
                            objNode1 = objNode.CloneNode(True)
                            objNode1.Attributes("UP_ACTION").InnerText = "D"

                            If objXmlservertestInput.DocumentElement.SelectSingleNode("QUESTIONS").Attributes("QS_TEXT").InnerText = "" Then
                                objNode3 = objXmlservertestInput.DocumentElement.SelectSingleNode("QUESTIONS")
                                objXmlservertestInput.DocumentElement.RemoveChild(objNode3)
                            End If
                            objXmlservertestInput.DocumentElement.AppendChild(objXmlservertestInput.ImportNode(objNode1, True))
                            Session("objXmlservertestInput") = objXmlservertestInput
                            '--------------------Local xml file
                            objXmltestInput.DocumentElement.RemoveChild(objNode)
                            Session("objXmltestInput") = objXmltestInput
                            If ViewState("SortName") IsNot Nothing Then
                                showGrid(objXmltestInput, ViewState("SortName"), ViewState("Desc"))
                            Else
                                showGrid(objXmltestInput)
                            End If
                            'BindControls()

                        End If
                    End If

                Else
                    lblError.Text = "You donot have sufficient rights."
                End If
            End If

            If e.CommandName = "EditU" Then

                objNode = objXmltestInput.SelectSingleNode("//QUESTIONS[@QS_TEXT='" + array(0) + "' and @QS_OPTION1='" + array(1).ToString + "' and @QS_OPTION2='" + array(2).ToString + "' and @QS_RIGHT_OPTION='" + array(3).ToString + "']")
                If Not objNode Is Nothing Then
                    With objNode
                        Qs_id.Value = .Attributes("QS_ID").InnerText
                        txtQuest.Text = .Attributes("QS_TEXT").InnerText
                        txtAns1.Text = .Attributes("QS_OPTION1").InnerText
                        txtAns2.Text = .Attributes("QS_OPTION2").InnerText
                        txtAns3.Text = .Attributes("QS_OPTION3").InnerText
                        txtAns4.Text = .Attributes("QS_OPTION4").InnerText
                        drpRAns.SelectedValue = .Attributes("QS_RIGHT_OPTION").InnerText
                        txtMforRAns.Text = .Attributes("MARK_FOR_RIGHT_ANSWER").InnerText
                        txtMforWAns.Text = .Attributes("MARK_FOR_WRONG_ANSWER").InnerText

                        If (Val(.Attributes("SVC_ID").InnerText) <> 0) Then
                            ' drpServiceType.SelectedValue = .Attributes("SVC_ID").InnerText
                        Else
                            'drpServiceType.SelectedIndex = 0
                        End If

                        If (Val(.Attributes("QL_ID").InnerText) <> 0) Then
                            'drpQlevel.SelectedValue = .Attributes("QL_ID").InnerText
                        Else
                            'drpQlevel.SelectedIndex = 0
                        End If
                        'If (.Attributes("QS_MANDATORY").InnerText = 1) Then
                        '    chkMandatoryQ.Checked = True
                        'Else
                        '    chkMandatoryQ.Checked = False
                        'End If

                    End With
                    objXmltestInput.DocumentElement.RemoveChild(objNode)
                    Session("objXmltestInput") = objXmltestInput
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
#End Region

#Region "fillpage()::View Questions"
    Protected Sub fillpage()
        '<MS_VIEWTESTQUESTIONS_INPUT><TR_COURSE_ID></TR_COURSE_ID><DAYS>2</DAYS></MS_VIEWTESTQUESTIONS_INPUT>"
        Dim intDuration As Integer
        Try
            ' Test_id.Value = Request.Params("Test_id")
            Test_id.Value = objED.Decrypt(Request.QueryString("Test_id"))
            If hdDays.Value = "" Then
                If Request.QueryString("Days") IsNot Nothing Then
                    hdDays.Value = Request.QueryString("Days")
                Else
                    hdDays.Value = 1
                End If
            End If


            objXmltestInput.LoadXml(strViewTest)
            objXmltestInput.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = Test_id.Value 'Request.QueryString("Test_id")
            objXmltestInput.DocumentElement.SelectSingleNode("DAYS").InnerText = hdDays.Value 'IIf(ddlDays.SelectedValue = "", 1, ddlDays.SelectedValue) ' Default 1 For DAY 1 Questions



            objXmlTestOutput = objTest.View(objXmltestInput)

            If objXmlTestOutput.DocumentElement.SelectSingleNode("QUESTIONS").Attributes("QS_TEXT").InnerText = "" Then
                objNode3 = objXmlTestOutput.DocumentElement.SelectSingleNode("QUESTIONS")
                objXmlTestOutput.DocumentElement.RemoveChild(objNode3)
            End If
            strErrorMsg = objfunction.CheckError(objXmlTestOutput)
            If strErrorMsg <> String.Empty Then
                lblError.Text = strErrorMsg
                Exit Sub
            Else
                BindControls1()
                ' Binding Days Drop Down Start.
                intDuration = hdCourseDuration.Value
                If ddlDays.Items.Count <> 0 Then
                Else
                    ddlDays.Items.Clear()
                    If intDuration <> 0 Then
                        For i As Integer = 1 To intDuration
                            ddlDays.Items.Add(i.ToString)
                        Next
                    End If

                End If


                ddlDays.SelectedValue = hdDays.Value
                ' Binding Days Drop Down End.

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "GrdTestDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdTestDetails.RowDataBound"
    Protected Sub GrdTestDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdTestDetails.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Text = CType(e.Row.RowIndex, Integer) + 1
        End If

        'If Len(e.Row.Cells(1).Text) > 25 Then
        '    e.Row.Cells(1).Text = Left(e.Row.Cells(1).Text, 25) + "..."
        'End If
        'If Len(e.Row.Cells(2).Text) > 8 Then
        '    e.Row.Cells(2).Text = Left(e.Row.Cells(2).Text, 8)
        'End If
        'If Len(e.Row.Cells(3).Text) > 8 Then
        '    e.Row.Cells(3).Text = Left(e.Row.Cells(3).Text, 8)
        'End If
        'If Len(e.Row.Cells(4).Text) > 8 Then
        '    e.Row.Cells(4).Text = Left(e.Row.Cells(4).Text, 8)
        'End If
        'If Len(e.Row.Cells(5).Text) > 8 Then
        '    e.Row.Cells(5).Text = Left(e.Row.Cells(5).Text, 8)
        'End If

        ' Code for delete msg confirmation.
        Dim btnDelete As LinkButton
        btnDelete = CType(e.Row.FindControl("BtnDelete"), LinkButton)
        btnDelete.Attributes.Add("OnClick", "javascript:return Delete();")


    End Sub
#End Region

#Region "BindControls()"
    Protected Sub BindControls()
        Try
            objfunction.BindGrid(GrdTestDetails, objXmltestInput, "QUESTIONS")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "BindControls1()"
    Protected Sub BindControls1()
        Dim objXmlNodeList As XmlNodeList
        Dim objXmlNode As XmlNode
        Try
            With objXmlTestOutput.DocumentElement.SelectSingleNode("TEST")
                txtTestName.Text = .Attributes("TR_COURSE_NAME").InnerText
                txtTestDesc.Text = .Attributes("TR_COURSE_DESC").InnerText
                txtTestTime.Text = .Attributes("TEST_TIME").InnerText
                txtTMarks.Text = .Attributes("DAYS_TOTAL_MARKS").InnerText
                txtNoOfQuest.Text = .Attributes("DAYS_TOTAL_MARKS").InnerText
                'txtNoOfQuest.Text = .Attributes("NO_OF_QUESTIONS").InnerText
                hdCourseDuration.Value = .Attributes("TR_COURSE_NO_TEST").InnerText
                ' txtStartDate.Text = ConvertDate(.Attributes("START_DATE").InnerText)
                'txtEndDate.Text = ConvertDate(.Attributes("END_DATE").InnerText)
            End With
            objXmlNodeList = objXmlTestOutput.DocumentElement.SelectNodes("QUESTIONS")
            '   Changing values for update
            For Each objXmlNode In objXmlNodeList
                objXmlNode.Attributes("UP_ACTION").InnerText = "U"
            Next
            If ViewState("SortName") IsNot Nothing Then
                showGrid(objXmlTestOutput, ViewState("SortName"), ViewState("Desc"))
            Else
                showGrid(objXmlTestOutput)
            End If
            ' showGrid(objXmlTestOutput)
            ' objfunction.BindGrid(GrdTestDetails, objXmlTestOutput, "QUESTIONS")

            Session("objXmltestInput") = objXmlTestOutput
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "ConvertTextDate(ByVal strRetDate As String) As String"
    Public Function ConvertTextDate(ByVal strRetDate As String) As String
        Dim strDate As String = String.Empty
        Dim retMon As String = String.Empty
        Dim retDay As String = String.Empty
        Dim retYear As String = String.Empty
        If strRetDate <> String.Empty Then
            strRetDate = strRetDate.Trim
            retYear = strRetDate.Substring(5, 4)
            retMon = strRetDate.Substring(2, 3).ToUpper
            retDay = strRetDate.Substring(0, 2)
            strDate = retYear
            Select Case retMon
                Case "JAN"
                    strDate = strDate & "01"
                Case "FEB"
                    strDate = strDate & "02"
                Case "MAR"
                    strDate = strDate & "03"
                Case "APR"
                    strDate = strDate & "04"
                Case "MAY"
                    strDate = strDate & "05"
                Case "JUN"
                    strDate = strDate & "06"
                Case "JUL"
                    strDate = strDate & "07"
                Case "AUG"
                    strDate = strDate & "08"
                Case "SEP"
                    strDate = strDate & "09"
                Case "OCT"
                    strDate = strDate & "10"
                Case "NOV"
                    strDate = strDate & "11"
                Case "DEC"
                    strDate = strDate & "12"
            End Select
            strDate = strDate & retDay.ToString
        End If
        Return strDate
    End Function
#End Region

#Region "ConvertDate(ByVal strDate As String) As String"
    Public Function ConvertDate(ByVal strDate As String) As String
        Try
            Dim retMon As String = strDate.Substring(4, 2)
            Dim retYear As String = strDate.Substring(0, 4)
            'Dim dtDateFrom As New Date(strDate.Substring(0, 4), strDate.Substring(4, 2), strDate.Substring(6, 2))
            strDate = strDate.Substring(6, 2)

            Select Case retMon
                Case "01"
                    strDate = strDate & "Jan"
                Case "02"
                    strDate = strDate & "Feb"
                Case "03"
                    strDate = strDate & "Mar"
                Case "04"
                    strDate = strDate & "Apr"
                Case "05"
                    strDate = strDate & "May"
                Case "06"
                    strDate = strDate & "Jun"
                Case "07"
                    strDate = strDate & "Jul"
                Case "08"
                    strDate = strDate & "Aug"
                Case "09"
                    strDate = strDate & "Sep"
                Case "10"
                    strDate = strDate & "Oct"
                Case "11"
                    strDate = strDate & "Nov"
                Case "12"
                    strDate = strDate & "Dec"
            End Select
            strDate = strDate + retYear

            Return strDate
        Catch ex As Exception
            Return CDate("1/1/1900")
        End Try
    End Function
#End Region

#Region "resetTestcontrol()"
    Public Sub resetTestcontrol()
        'txtTestName.Text = String.Empty
        txtNoOfQuest.Text = String.Empty
        'txtTestDesc.Text = String.Empty
        txtTestTime.Text = String.Empty
        txtTestTime.Text = String.Empty
        'txtStartDate.Text = String.Empty
        ' txtEndDate.Text = String.Empty
        ' txtTMarks.Text = String.Empty
    End Sub
#End Region

#Region "resetQuestcontrol()"
    Public Sub resetQuestcontrol()
        txtQuest.Text = String.Empty
        txtAns1.Text = String.Empty
        txtAns2.Text = String.Empty
        txtAns3.Text = String.Empty
        txtAns4.Text = String.Empty
        drpRAns.SelectedIndex = 0
        txtMforRAns.Text = String.Empty
        txtMforWAns.Text = String.Empty
        ' drpServiceType.SelectedIndex = 0
        ' drpQlevel.SelectedIndex = 0
        'chkMandatoryQ.Checked = False
    End Sub
#End Region

#Region "btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click"
    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        'resetTestcontrol()
        Response.Redirect("ManageTest.aspx?Test_id=" + Request.QueryString("Test_id"))

    End Sub
#End Region

#Region "FillComboBoxes()"
    Protected Sub FillComboBoxes()
        'Dim objXmlLevel As New XmlDocument
        'Dim objXmlserviceType As New XmlDocument
        'Dim objStaff As New bizACK.bzStaff
        'objXmlLevel = objStaff.GetLEVELList
        'objXmlserviceType = objStaff.GetServiceList
        'If objfunction.FillComboBox(drpServiceType, objXmlserviceType, "SERVICE", "SVC_ID", "SVC_NAME", True) = False Then
        '    lblError.Text = "Internal System Error"
        'End If

        'If objfunction.FillComboBox(drpQlevel, objXmlLevel, "LEVEL", "QL_ID", "QL_NAME", True) = False Then
        '    lblError.Text = "Internal System Error"
        'End If

    End Sub
#End Region

#Region "btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click"
    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        ' ViewQuestion()
    End Sub
#End Region

#Region " ViewQuestion()"
    Protected Sub ViewQuestion()
        Dim objXmlDoc As New XmlDocument
        Dim objXmlNodeList As XmlNodeList
        Dim objTempXmlNode As XmlNode
        Dim objDupXmlNode As XmlNode
        Dim objxmldocfrag As XmlDocumentFragment
        Try
            objXmltestInput.LoadXml(strViewTest)
            objXmltestInput.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = hdCourseID.Value
            objXmltestInput.DocumentElement.SelectSingleNode("DAYS").InnerText = ""
            objXmlTestOutput = objTest.View(objXmltestInput)
            If objXmlTestOutput.DocumentElement.SelectSingleNode("QUESTIONS").Attributes("QS_TEXT").InnerText = "" Then
                objNode3 = objXmlTestOutput.DocumentElement.SelectSingleNode("QUESTIONS")
                objXmlTestOutput.DocumentElement.RemoveChild(objNode3)
            End If
            strErrorMsg = objfunction.CheckError(objXmlTestOutput)
            If strErrorMsg <> String.Empty Then
                lblError.Text = strErrorMsg
                Exit Sub
            Else
                'BindControls1()
                ' Appending questions into session.
                If Session("objXmltestInput") IsNot Nothing Then
                    objXmlDoc = CType(Session("objXmltestInput"), XmlDocument)
                    objXmlNodeList = objXmlTestOutput.DocumentElement.SelectNodes("QUESTIONS")
                    For Each objTempXmlNode In objXmlNodeList
                        ' checking duplicate question

                        objDupXmlNode = objXmlDoc.DocumentElement.SelectSingleNode("QUESTIONS[@QS_TEXT='" + objTempXmlNode.Attributes("QS_TEXT").Value + "']")
                        '   changing status for insert for copy topics
                        If objDupXmlNode Is Nothing Then
                            objTempXmlNode.Attributes("UP_ACTION").InnerText = "I"
                            objTempXmlNode.Attributes("QS_ID").InnerText = ""
                            objxmldocfrag = objXmlDoc.CreateDocumentFragment()
                            objxmldocfrag.InnerXml = objTempXmlNode.OuterXml
                            objXmlDoc.DocumentElement.AppendChild(objxmldocfrag)
                        End If
                    Next
                    Session("objXmltestInput") = objXmlDoc
                    ' Binding data grid.
                    hdCourseID.Value = ""
                    If ViewState("SortName") IsNot Nothing Then
                        showGrid(objXmlDoc, ViewState("SortName"), ViewState("Desc"))
                    Else
                        showGrid(objXmlDoc)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "showGrid(ByVal xmlDoc As XmlDocument)"
    Private Sub showGrid(ByVal xmlDoc As XmlDocument, Optional ByVal strSortingColumn As String = "QS_TEXT", Optional ByVal strSortOrder As String = "ASC")
        Dim ds As New DataSet
        Dim objXmlReader As XmlReader
        objXmlReader = New XmlNodeReader(xmlDoc)
        ds.ReadXml(objXmlReader)
        Dim dv As DataView
        If ds.Tables("QUESTIONS") Is Nothing Then
            GrdTestDetails.DataSource = Nothing
            GrdTestDetails.DataBind()
            Exit Sub
        End If
        dv = ds.Tables("QUESTIONS").DefaultView
        If strSortingColumn <> "" Then
            dv.Sort = strSortingColumn & " " & strSortOrder
        End If
        GrdTestDetails.DataSource = dv
        GrdTestDetails.DataBind()
        'Dim imgUp As New Image
        'imgUp.ImageUrl = "~/Images/Sortup.gif"
        'Dim imgDown As New Image
        'imgDown.ImageUrl = "~/Images/Sortdown.gif"

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "QS_TEXT"
            ViewState("Desc") = "ASC"
        End If

        'Select Case ViewState("SortName")
        '    Case "QS_TEXT"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(1).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(1).Controls.Add(imgDown)
        '        End Select
        '    Case "QS_OPTION1"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(2).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(2).Controls.Add(imgDown)
        '        End Select
        '    Case "QS_OPTION2"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(3).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(3).Controls.Add(imgDown)

        '        End Select


        '    Case "QS_OPTION3"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(4).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(4).Controls.Add(imgDown)
        '        End Select
        '    Case "QS_OPTION4"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(5).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(5).Controls.Add(imgDown)
        '        End Select
        '    Case "QS_RIGHT_OPTION"
        '        Select Case ViewState("Desc")
        '            Case "ASC"
        '                GrdTestDetails.HeaderRow.Cells(6).Controls.Add(imgUp)
        '            Case "DESC"
        '                GrdTestDetails.HeaderRow.Cells(6).Controls.Add(imgDown)

        '        End Select


        'End Select

        SetImageForSorting(GrdTestDetails)
    End Sub
#End Region

    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Desc") = "ASC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "DESC" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub

    Protected Sub ddlDays_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDays.SelectedIndexChanged
        Try
            txtTestTime.Text = ""
            txtNoOfQuest.Text = ""
            hdDays.Value = ddlDays.SelectedValue
            fillpage()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnQuestionSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuestionSet.Click
        Try
            Test_id.Value = objED.Decrypt(Request.QueryString("Test_id"))
            If hdDays.Value = "" Then
                If Request.QueryString("Days") IsNot Nothing Then
                    hdDays.Value = Request.QueryString("Days")
                Else
                    hdDays.Value = 1
                End If
            End If
            objXmltestInput.LoadXml(strViewTest)
            objXmltestInput.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = Test_id.Value 'Request.QueryString("Test_id")
            objXmltestInput.DocumentElement.SelectSingleNode("DAYS").InnerText = hdDays.Value 'IIf(ddlDays.SelectedValue = "", 1, ddlDays.SelectedValue) ' Default 1 For DAY 1 Questions
            objXmlTestOutput = objTest.View(objXmltestInput)
            If objXmlTestOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim intTotalMarks As Integer = objXmlTestOutput.DocumentElement.SelectSingleNode("TEST").Attributes("DAYS_TOTAL_MARKS").Value
                Dim intTotalQuestion As Integer = objXmlTestOutput.DocumentElement.SelectNodes("QUESTIONS").Count
                Dim i As Integer = 0
                If intTotalMarks <= intTotalQuestion Then
                    For Each objNode As XmlNode In objXmlTestOutput.DocumentElement.SelectNodes("QUESTIONS")
                        If i >= intTotalMarks Then
                            objXmlTestOutput.DocumentElement.RemoveChild(objNode)
                        End If
                        i = i + 1
                    Next


                    Session("QuestionPaper") = objXmlTestOutput.OuterXml
                    Response.Redirect("../RPSR_ReportShow.aspx?Case=QuestionPaper")
                Else
                    lblError.Text = "Not Enough Questions available"
                End If

            Else
                lblError.Text = objXmlTestOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GrdTestDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdTestDetails.Sorted

    End Sub

    Protected Sub GrdTestDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdTestDetails.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "ASC"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "ASC" Then
                        ViewState("Desc") = "DESC"
                    Else
                        ViewState("Desc") = "ASC"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "ASC"
                End If
            End If
            Dim objXmlDoc As New XmlDocument
            If Session("objXmltestInput") IsNot Nothing AndAlso Session("objXmltestInput").ToString <> "" Then
                objXmlDoc = CType(Session("objXmltestInput"), XmlDocument)
                showGrid(objXmlDoc, SortName, ViewState("Desc"))
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class


