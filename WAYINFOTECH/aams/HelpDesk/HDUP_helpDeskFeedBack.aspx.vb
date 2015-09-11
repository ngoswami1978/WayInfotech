
Partial Class HelpDesk_HDUP_helpDeskFeedBack
    Inherits System.Web.UI.Page
    Protected strIndex As String
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            btnSave.Attributes.Add("onclick", "return ValidatHelpDeskFeedback();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            checkSecurity()
            If Not Page.IsPostBack Then


                If Not Request.QueryString("strStatus") Is Nothing Then
                    hdPageStatus.Value = objED.Decrypt(Request.QueryString("strStatus").ToString)
                    hdEnPageStatus.Value = Request.QueryString("strStatus").ToString

                End If
                If Not Request.QueryString("LCode") Is Nothing Then
                    hdPageLCode.Value = objED.Decrypt(Request.QueryString("LCode").ToString)
                    hdEnPageLCode.Value = Request.QueryString("LCode").ToString
                End If

                If Not Request.QueryString("HD_RE_ID") Is Nothing Then
                    hdPageHD_RE_ID.Value = objED.Decrypt(Request.QueryString("HD_RE_ID").ToString)
                    hdEnPageHD_RE_ID.Value = Request.QueryString("HD_RE_ID").ToString
                End If
                If Not Request.QueryString("FeedBackId") Is Nothing Then
                    hdFeedBackId.Value = objED.Decrypt(Request.QueryString("FeedBackId").ToString)
                    hdEnFeedBackId.Value = Request.QueryString("FeedBackId").ToString
                End If
                If Not Request.QueryString("AOFFICE") Is Nothing Then
                    hdAoffice.Value = objED.Decrypt(Request.QueryString("AOFFICE").ToString)
                    hdEnAOffice.Value = Request.QueryString("AOFFICE").ToString
                End If
                Bindata()
                FillDropDownList()
                ViewCallLogInsertDetails()
                '  bindQuestins_Status() 'Commented on 4 Aug 08
                HideShowControl()
                If hdFeedBackId.Value <> "" Then
                    ViewFeedbackDetails()
                    DisableQuestionStatus()
                    Dim xDoc As New XmlDocument
                    If Session("Security") IsNot Nothing Then
                        xDoc.LoadXml(Session("Security"))
                        hdEmpID.Value = xDoc.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                        hdLogedByName.Value = xDoc.DocumentElement.SelectSingleNode("Employee_Name").InnerText.Trim()
                        txtLogedByName.Text = hdLogedByName.Value
                    End If
                Else
                    bindQuestins_Status()
                End If
            End If
            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(2) = "0" Then
            '        btnSave.Enabled = False
            '    End If

            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

            'Code Added By Abhishek

            If Request.QueryString("Popup") Is Nothing Then
                lnkClose.Visible = False
            Else
                lnkClose.Visible = True
            End If
            'End of Code Added By Abhishek

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub checkSecurity()
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Action']").Attributes("Value").Value)
            End If
            'When View rights disabled
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSave.Enabled = False
                btnHistory.Enabled = False
            End If
            'When Add rights disabled
            If strBuilder(1) = "0" Then
                btnSave.Enabled = False
            End If
            'When modify rights disabled and Add rights enabled
            If strBuilder(2) = "0" And (hdPageHD_RE_ID.Value <> "" Or Request.QueryString("HD_RE_ID") IsNot Nothing) Then
                btnSave.Enabled = False
            End If
            'When modify rights Enabled and Add rights disabled
            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If

        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
    End Sub
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try
            TabText.Add("Call")
            TabText.Add("Description")
            TabText.Add("Solution")
            If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                TabText.Add("Follow Up")
            End If
            TabText.Add("Linked LTR")
            TabText.Add("FeedBack")

            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Sub FillDropDownList()
        Try
            'FEEDBACKSTATUS

            objeAAMS.BindDropDown(ddlHelpDeskAction, "FEEDBACKSTATUS", True, 1)
            objeAAMS.BindDropDown(ddlTechnicalHelpDeskAction, "FEEDBACKSTATUS", True, 1)
            objeAAMS.BindDropDown(ddlSalesHelpDeskAction, "FEEDBACKSTATUS", True, 1)
            objeAAMS.BindDropDown(ddlTrainingHelpDeskAction, "FEEDBACKSTATUS", True, 1)
            objeAAMS.BindDropDown(ddlProductHelpDeskAction, "FEEDBACKSTATUS", True, 1)
            objeAAMS.BindDropDown(ddlCustomerFeedbackAction, "FEEDBACKSTATUS", True, 1)

            ddlSuggestionHelpDesk.Items.Clear()
            ddlSuggestionTechnical.Items.Clear()
            ddlSuggestionSales.Items.Clear()
            ddlSuggestionTraining.Items.Clear()
            ddlSuggestionProduct.Items.Clear()
            ddlSuggestionCustomerFeedback.Items.Clear()

            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objbzFeedback As New AAMS.bizHelpDesk.bzFeedback
            objInputXml.LoadXml("<HD_GET_FEEDBACKASSIGNEDTO_INPUT><LCODE></LCODE> </HD_GET_FEEDBACKASSIGNEDTO_INPUT>")

            'objInputXml.LoadXml("<MS_SEARCHDEPARTMENTEMPLOYEE_INPUT><Employee_Type></Employee_Type><Aoffice></Aoffice></MS_SEARCHDEPARTMENTEMPLOYEE_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("Employee_Type").InnerText = "ALL"
            'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = hdAoffice.Value
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdPageLCode.Value
            objOutputXml = objbzFeedback.ListFeedbackAssignedTo(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '1 for helpdesk
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='1']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionHelpDesk.Items.Add(li)
                Next
                ' 2 for TECHNICAL
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='2']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionTechnical.Items.Add(li)
                Next
                '3 fro SALES
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='3']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionSales.Items.Add(li)
                Next
                '4 for TRAINING
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='4']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionTraining.Items.Add(li)
                Next
                '5 for PRODUCT
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='5']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionProduct.Items.Add(li)
                Next
                '6 for Customer Feedback
                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("FEEDBACKASSIGNEDTO[@DEPARTMENTID='6']")
                    Dim li As New ListItem(objNode.Attributes("ASSIGNEDTO_NAME").Value, objNode.Attributes("ASSIGNEDTO").Value)
                    ddlSuggestionCustomerFeedback.Items.Add(li)
                Next
            Else
                '   lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

            End If

            'ListFeedbackSuggestionStatus
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            ddlSuggestionTechnical.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlSuggestionHelpDesk.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlSuggestionSales.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlSuggestionTraining.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlSuggestionProduct.Items.Insert(0, New ListItem("--Select One--", ""))
            ddlSuggestionCustomerFeedback.Items.Insert(0, New ListItem("--Select One--", ""))
        End Try
    End Sub

    Sub HideShowControl()
        If hdFeedBackId.Value = "" Then
            trHelpDeskHelpDeskAction.Visible = False
            trHelpDeskHelpDeskAction1.Visible = False

            trTechnicalHelpDeskAction.Visible = False
            trTechnicalHelpDeskAction1.Visible = False

            trSalesHelpDeskAction.Visible = False
            trSalesHelpDeskAction1.Visible = False

            trProductHelpDeskAction.Visible = False
            trProductHelpDeskAction1.Visible = False

            trTrainingHelpDeskAction.Visible = False
            trTrainingHelpDeskAction1.Visible = False

            trCustomerFeedbackAction.Visible = False
            trCustomerFeedbackAction1.Visible = False
        Else
            trSuggestionHelpDesk.Visible = False
            trSuggestionHelpDesk1.Visible = False

            trSuggestionTechnical.Visible = False
            trSuggestionTechnical1.Visible = False

            trSuggestionSales.Visible = False
            trSuggestionSales1.Visible = False

            trSuggestionTraining.Visible = False
            trSuggestionTraining1.Visible = False

            trSuggestionProduct.Visible = False
            trSuggestionProduct1.Visible = False

            ''Added
            trSuggestionCustomerFeedback.Visible = False
            trSuggestionCustomerFeedback1.Visible = False

            trHelpDeskHelpDeskAction.Visible = False
            trHelpDeskHelpDeskAction1.Visible = False

            trTechnicalHelpDeskAction.Visible = False
            trTechnicalHelpDeskAction1.Visible = False

            trSalesHelpDeskAction.Visible = False
            trSalesHelpDeskAction1.Visible = False

            trProductHelpDeskAction.Visible = False
            trProductHelpDeskAction1.Visible = False

            trTrainingHelpDeskAction.Visible = False
            trTrainingHelpDeskAction1.Visible = False
            'Added
            trCustomerFeedbackAction.Visible = False
            trCustomerFeedbackAction1.Visible = False

        End If
    End Sub

    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")



        'Button1 = e.Item.FindControl("Button1")
        'If hdPageStatus.Value = "Technical" Then
        '    If Request.QueryString("Popup") Is Nothing Then
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',5,'" & e.Item.ItemIndex & "');")
        '    Else
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',4,'" & e.Item.ItemIndex & "');")
        '    End If

        'Else
        '    If Request.QueryString("Popup") Is Nothing Then
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',5,'" & e.Item.ItemIndex & "');")
        '    Else
        '        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',6,'" & e.Item.ItemIndex & "');")
        '    End If
        'End If
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Call"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Description"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Solution"
                    If hdPageStatus.Value.ToUpper <> "TECHNICAL" Then
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    Else
                        If objeAAMS.ReturnViewPermission(Session("Security"), "Log Tech Call") = "0" Then
                            Button1.CssClass = "displayNone"
                        End If
                    End If
                Case "Follow Up"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Log HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Linked LTR"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Linked HD Call") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "FeedBack"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Feedback Action") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
            End Select
        End If
        '   Button1 = e.Item.FindControl("Button1")
        If hdPageStatus.Value = "Technical" Then
            Button1.Attributes.Add("onclick", "return ColorMethodHelpDeskFeedBack('" & Button1.ClientID.ToString() & "',5,'" & e.Item.ItemIndex & "');")
            If e.Item.ItemIndex = 4 Then
                Button1.CssClass = "headingtab"
            End If
        Else
            If e.Item.ItemIndex = 5 Then
                Button1.CssClass = "headingtab"
            End If
            Button1.Attributes.Add("onclick", "return ColorMethodHelpDeskFeedBack('" & Button1.ClientID.ToString() & "',6,'" & e.Item.ItemIndex & "');")
        End If

    End Sub

    Private Sub ViewFeedbackDetails()
        '<HD_VIEWFEEDBACK_OUTPUT>
        ' <FEEDBACK FEEDBACK_ID='' DATETIME='' EMPLOYEEID='' LOGGEDBY='' ExecutiveName='' SUGGESTION='' QUESTION_ID='' QUESTION_TITLE='' STATUS_ID=''/>
        ' <SUGGESTIONDETAILS SUG_DEPT_ID='' SUG_DEPT_NAME='' DEPT_SUGGESTION='' ACTIONTAKEN=''  ASSIGENDTO='' CRITICAL=''/>

        Dim objInputXml, objOutputXml As New XmlDocument

        Dim objhdFeedbackView As New AAMS.bizHelpDesk.bzFeedback
        objInputXml.LoadXml("<HD_VIEWFEEDBACK_INPUT><FEEDBACK_ID /></HD_VIEWFEEDBACK_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = hdFeedBackId.Value

        objOutputXml = objhdFeedbackView.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


            With objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK")
                txtFeedBackNo.Text = .Attributes("FEEDBACK_ID").Value.Trim()
                txtRemarks.Text = .Attributes("SUGGESTION").Value.Trim()
                txtFeedbkDt.Text = .Attributes("DATETIME").Value.Trim()
                '   If .Attributes("EmployeeID").Value.Trim().Length <> 0 Then
                ' hdEmpID.Value = .Attributes("EmployeeID").Value.Trim()
                txtLogedByName.Text = .Attributes("LOGGEDBY").Value.Trim()
                txtExecutiveName.Text = .Attributes("ExecutiveName").Value.Trim()
                ' End If
            End With

            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            objReader = New XmlNodeReader(objOutputXml)
            dSet.ReadXml(objReader)
            grdvFeedback.DataSource = dSet.Tables("FEEDBACK")
            grdvFeedback.DataBind()



            Dim objNode As XmlNode
            Dim xNodeList As XmlNodeList
            'Code to hideShow Suggestion for deparment and Action taken against it
            xNodeList = objOutputXml.DocumentElement.SelectNodes("SUGGESTIONDETAILS")
            For Each objNode In xNodeList
                '1 Suggestion for HelpDesk
                '2 Suggestion for Technical
                '3 Suggestion for Sales
                '4 Suggestion for Training
                '5 Suggestion for Products
                '6 Suggestion for Customer Feedback
                '<SUGGESTIONDETAILS SUG_DEPT_ID='' SUG_DEPT_NAME='' DEPT_SUGGESTION='' ASSIGENDTO='' CRITICAL=''/>

                Select Case objNode.Attributes("SUG_DEPT_ID").Value
                    Case "1"
                        trHelpDeskHelpDeskAction.Visible = True
                        trHelpDeskHelpDeskAction1.Visible = True
                        trSuggestionHelpDesk.Visible = True
                        trSuggestionHelpDesk1.Visible = True

                        txtSuggestionHelpDesk.ReadOnly = True
                        'txtSuggestionHelpDesk.Enabled = False
                        ddlSuggestionHelpDesk.Enabled = False
                        chkSuggestionHelpDesk.Enabled = False
                        txtSuggestionHelpDesk.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionHelpDesk.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionHelpDesk.Checked = objNode.Attributes("CRITICAL").Value
                        txtHelpDeskAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlHelpDeskAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value


                    Case "2"
                        trTechnicalHelpDeskAction.Visible = True
                        trTechnicalHelpDeskAction1.Visible = True
                        trSuggestionTechnical.Visible = True
                        trSuggestionTechnical1.Visible = True

                        txtSuggestionTechnical.ReadOnly = True
                        'txtSuggestionTechnical.Enabled = False
                        ddlSuggestionTechnical.Enabled = False
                        chkSuggestionTechnical.Enabled = False
                        txtSuggestionTechnical.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionTechnical.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionTechnical.Checked = objNode.Attributes("CRITICAL").Value
                        txtTechnicalHelpDeskAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlTechnicalHelpDeskAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value
                    Case "3"
                        trSalesHelpDeskAction.Visible = True
                        trSalesHelpDeskAction1.Visible = True
                        trSuggestionSales.Visible = True
                        trSuggestionSales1.Visible = True

                        txtSuggestionSales.ReadOnly = True
                        'txtSuggestionSales.Enabled = False
                        ddlSuggestionSales.Enabled = False
                        chkSuggestionSales.Enabled = False
                        txtSuggestionSales.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionSales.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionSales.Checked = objNode.Attributes("CRITICAL").Value
                        txtSalesHelpDeskAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlSalesHelpDeskAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value
                    Case "4"
                        trTrainingHelpDeskAction.Visible = True
                        trTrainingHelpDeskAction1.Visible = True
                        trSuggestionTraining.Visible = True
                        trSuggestionTraining1.Visible = True

                        txtSuggestionTraining.ReadOnly = True
                        'txtSuggestionTraining.Enabled = False
                        ddlSuggestionTraining.Enabled = False
                        chkSuggestionTraining.Enabled = False
                        txtSuggestionTraining.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionTraining.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionTraining.Checked = objNode.Attributes("CRITICAL").Value
                        txtTrainingHelpDeskAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlTrainingHelpDeskAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value
                    Case "5"
                        trProductHelpDeskAction.Visible = True
                        trProductHelpDeskAction1.Visible = True
                        trSuggestionProduct.Visible = True
                        trSuggestionProduct1.Visible = True

                        txtSuggestionProduct.ReadOnly = True
                        'txtSuggestionProduct.Enabled = False
                        ddlSuggestionProduct.Enabled = False
                        chkSuggestionProduct.Enabled = False
                        txtSuggestionProduct.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionProduct.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionProduct.Checked = objNode.Attributes("CRITICAL").Value
                        txtProductHelpDeskAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlProductHelpDeskAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value
                    Case "6"
                        trSuggestionCustomerFeedback.Visible = True
                        trSuggestionCustomerFeedback1.Visible = True
                        trCustomerFeedbackAction.Visible = True
                        trCustomerFeedbackAction1.Visible = True

                        txtSuggestionCustomerFeedback.ReadOnly = True
                        ddlSuggestionCustomerFeedback.Enabled = False
                        chkSuggestionCustomerFeedback.Enabled = False

                        txtSuggestionCustomerFeedback.Text = objNode.Attributes("DEPT_SUGGESTION").Value
                        ddlSuggestionCustomerFeedback.SelectedValue = objNode.Attributes("ASSIGENDTO").Value
                        chkSuggestionCustomerFeedback.Checked = objNode.Attributes("CRITICAL").Value

                        txtCustomerFeedbackAction.Text = objNode.Attributes("ACTIONTAKEN").Value
                        ddlCustomerFeedbackAction.SelectedValue = objNode.Attributes("FEEDBACK_STATUS_ID").Value

                End Select

            Next

            'Following code is written for setting the status of all Questions

            xNodeList = objOutputXml.DocumentElement.SelectNodes("FEEDBACK")
            For Each objNode In xNodeList
                AssignQuestionStatus(objNode.Attributes("QUESTION_ID").Value.Trim(), objNode.Attributes("STATUS_ID").Value.Trim())
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Private Sub bindQuestins_Status()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objFeedback As New AAMS.bizHelpDesk.bzFeedback
        Dim objReader As XmlNodeReader
        Dim dSet As New DataSet
        objOutputXml = objFeedback.ListFeedbackQuestions()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objReader = New XmlNodeReader(objOutputXml)
            dSet.ReadXml(objReader)
            grdvFeedback.DataSource = dSet.Tables("QUESTION")
            grdvFeedback.DataBind()

        Else
            grdvFeedback.DataSource = Nothing
            grdvFeedback.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub
    Protected Sub grdvFeedback_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvFeedback.RowDataBound


        Try
            Dim lblQuestion As Label
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If


            Dim drpQuestStatus As New DropDownList
            drpQuestStatus = CType(e.Row.FindControl("drpQuestStatus"), DropDownList)
            objeAAMS.BindDropDown(drpQuestStatus, "HDQUESTIONSTATUS", True)

            lblQuestion = CType(e.Row.FindControl("lblQuestionID"), Label)
            If Not lblQuestion Is Nothing Then
                lblQuestion.Text = CType(e.Row.RowIndex, Integer) + 1
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub AssignQuestionStatus(ByVal QID As String, ByVal StatusID As String)
        Try

            Dim rowCounter As Integer
            Dim drpStatus As New DropDownList
            Dim lblQuestNo As New HiddenField
            For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                drpStatus = CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList)
                lblQuestNo = CType(grdvFeedback.Rows(rowCounter).FindControl("hdQuestionID"), HiddenField)
                If lblQuestNo.Value.Trim() = QID.Trim() Then
                    CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedValue = StatusID
                End If
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If (Not Request.QueryString("Action") = Nothing) Then

                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objFeedback As New AAMS.bizHelpDesk.bzFeedback
                Dim objChildNode, objChildNodeClone, objParentNode As XmlNode
                Dim objNode As XmlNode
                Dim objCloneNode As XmlNode
                ' <HD_UPDATECALLSTATUS_INPUT>
                ' <FEEDBACK FEEDBACK_ID='' LCODE='' HD_RE_ID='' EmployeeID='' ExecutiveName='' SEGGESTION='' >
                ' <DETAILS QUESTION_ID='' STATUS_ID=''/>
                ' <SUGGESTIONDETAILS SUG_DEPT_ID='' ASSIGNEDTO='' ACTIONTAKEN='' FEEDBACK_STATUS_ID='' />
                ' </FEEDBACK>
                '</HD_UPDATECALLSTATUS_INPUT>

                ' objInputXml.LoadXml("<HD_UPDATECALLSTATUS_INPUT><FEEDBACK FEEDBACK_ID='' LCODE='' EmployeeID='' SEGGESTION=''><DETAILS QUESTION_ID='' STATUS_ID=''/></FEEDBACK></HD_UPDATECALLSTATUS_INPUT>")
                objInputXml.LoadXml("<HD_UPDATECALLSTATUS_INPUT><FEEDBACK FEEDBACK_ID='' LCODE='' HD_RE_ID='' EmployeeID='' ExecutiveName='' SEGGESTION='' ><DETAILS QUESTION_ID='' STATUS_ID=''/><SUGGESTIONDETAILS SUG_DEPT_ID='' SUG_DEPT_NAME='' ASSIGNEDTO='' ACTIONTAKEN='' FEEDBACK_STATUS_ID='' DEPT_SUGGESTION='' CRITICAL='' /></FEEDBACK></HD_UPDATECALLSTATUS_INPUT>")
                If hdFeedBackId.Value <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value = hdFeedBackId.Value
                End If




                objParentNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK")
                objChildNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK/DETAILS")
                objChildNodeClone = objChildNode.CloneNode(True)
                objNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK/SUGGESTIONDETAILS")
                objCloneNode = objNode.CloneNode(True)
                objParentNode.RemoveChild(objChildNode)
                objParentNode.RemoveChild(objNode)
                objInputXml.DocumentElement.RemoveChild(objParentNode)

                With objParentNode
                    .Attributes("LCODE").Value = hdPageLCode.Value.Trim()
                    If hdEmpID.Value = "" And Session("Security") IsNot Nothing Then
                        .Attributes("EmployeeID").Value = objeAAMS.EmployeeID(Session("Security"))
                    Else
                        .Attributes("EmployeeID").Value = hdEmpID.Value.Trim()
                    End If

                    .Attributes("SEGGESTION").Value = txtRemarks.Text
                    .Attributes("HD_RE_ID").Value = hdPageHD_RE_ID.Value
                    .Attributes("ExecutiveName").Value = txtExecutiveName.Text
                End With


                Dim rowCounter As Integer = 0
                If grdvFeedback.Rows.Count <> 0 Then
                    For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                        '***************** Code for Questions Check *************************
                        If hdFeedBackId.Value = "" Then
                            If CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedIndex = 0 Then
                                lblError.Text = "All Question Status is Mandatory."
                                CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).Focus()
                                Exit Sub
                            End If
                        End If
                        '***************** Code for Questions Check *************************
                        objChildNodeClone.Attributes("QUESTION_ID").Value = CType(grdvFeedback.Rows(rowCounter).FindControl("hdQuestionID"), HiddenField).Value.Trim()
                        objChildNodeClone.Attributes("STATUS_ID").Value = CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).SelectedValue.Trim()
                        objParentNode.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    Next
                Else
                    lblError.Text = "No Question Defined."
                    Exit Sub
                End If



                ' objNode = objInputXml.DocumentElement.SelectSingleNode("FEEDBACK/SUGGESTIONDETAILS")
                ' objCloneNode = objNode.CloneNode(True)
                ' objInputXml.DocumentElement.RemoveChild(objNode)
                ' <SUGGESTIONDETAILS SUG_DEPT_ID='' ASSIGNEDTO='' ACTIONTAKEN='' FEEDBACK_STATUS_ID='' DEPT_SUGGESTION='' CRITICAL='' />

                If txtSuggestionHelpDesk.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "1"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Helpdesk"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionHelpDesk.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtHelpDeskAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlHelpDeskAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionHelpDesk.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionHelpDesk.Checked.ToString
                    'objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

                If txtSuggestionTechnical.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "2"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Technical"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionTechnical.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtTechnicalHelpDeskAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlTechnicalHelpDeskAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionTechnical.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionTechnical.Checked.ToString
                    '   objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

                If txtSuggestionSales.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "3"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Sales"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionSales.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtSalesHelpDeskAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlSalesHelpDeskAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionSales.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionSales.Checked.ToString
                    'objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If


                If txtSuggestionTraining.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "4"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Training"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionTraining.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtTrainingHelpDeskAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlTrainingHelpDeskAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionTraining.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionTraining.Checked.ToString
                    'objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

                If txtSuggestionProduct.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "5"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Products"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionProduct.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtProductHelpDeskAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlProductHelpDeskAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionProduct.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionProduct.Checked.ToString
                    'objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

                If txtSuggestionCustomerFeedback.Text <> "" Then
                    objCloneNode.Attributes("SUG_DEPT_ID").Value = "6"
                    objCloneNode.Attributes("SUG_DEPT_NAME").Value = "Customer Feedback"
                    objCloneNode.Attributes("ASSIGNEDTO").Value = ddlSuggestionCustomerFeedback.SelectedValue
                    objCloneNode.Attributes("ACTIONTAKEN").Value = txtCustomerFeedbackAction.Text
                    objCloneNode.Attributes("FEEDBACK_STATUS_ID").Value = ddlCustomerFeedbackAction.SelectedValue
                    objCloneNode.Attributes("DEPT_SUGGESTION").Value = txtSuggestionCustomerFeedback.Text
                    objCloneNode.Attributes("CRITICAL").Value = chkSuggestionCustomerFeedback.Checked.ToString
                    'objInputXml.DocumentElement.AppendChild(objCloneNode)
                    objParentNode.AppendChild(objCloneNode)
                    objCloneNode = objNode.CloneNode(True)
                End If

                objInputXml.DocumentElement.AppendChild(objParentNode)
                objOutputXml = objFeedback.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If hdFeedBackId.Value <> "" Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        ViewFeedbackDetails()
                        DisableQuestionStatus()
                        hdEnFeedBackId.Value = objED.Encrypt(hdFeedBackId.Value)
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        hdFeedBackId.Value = objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value
                        hdEnFeedBackId.Value = objED.Encrypt(hdFeedBackId.Value)
                        hdInsertStatus.Value = "1"
                        btnSave.Enabled = False
                        txtFeedBackNo.Text = hdFeedBackId.Value
                        txtFeedbkDt.Text = objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("DateTime").Value
                        ' HideShowControl()
                        'ViewFeedbackDetails()
                        'DisableQuestionStatus()
                        'Response.Redirect("HDUP_Feedback.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("FEEDBACK").Attributes("FEEDBACK_ID").Value, False)

                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            If hdFeedBackId.Value <> "" Then
                If hdInsertStatus.Value = "" Then
                    ' bindQuestins_Status()
                    ViewFeedbackDetails()
                    DisableQuestionStatus()
                End If
            Else
                Response.Redirect("HDUP_helpDeskFeedBack.aspx?" + Request.QueryString.ToString)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DisableQuestionStatus()
        Try
            Dim rowCounter As Integer = 0
            For rowCounter = 0 To grdvFeedback.Rows.Count - 1
                CType(grdvFeedback.Rows(rowCounter).FindControl("drpQuestStatus"), DropDownList).Enabled = False
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewCallLogInsertDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objhdPtrView As New AAMS.bizHelpDesk.bzPTR
        objOutputXml = objhdPtrView.ListPTRConfigValue()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim xNode As XmlNode
            For Each xNode In objOutputXml.DocumentElement.SelectNodes("CONFIGVALUE")
                If xNode.Attributes("FIELD_NAME").Value.Trim() = "DEFAULT_HD_FEEDBACK_STATUS" Then
                    ddlHelpDeskAction.SelectedValue = xNode.Attributes("FIELD_VALUE").Value
                    ddlTechnicalHelpDeskAction.SelectedValue = xNode.Attributes("FIELD_VALUE").Value
                    ddlSalesHelpDeskAction.SelectedValue = xNode.Attributes("FIELD_VALUE").Value
                    ddlTrainingHelpDeskAction.SelectedValue = xNode.Attributes("FIELD_VALUE").Value
                    ddlProductHelpDeskAction.SelectedValue = xNode.Attributes("FIELD_VALUE").Value

                End If

            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
