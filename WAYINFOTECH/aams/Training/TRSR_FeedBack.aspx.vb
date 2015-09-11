
Partial Class Training_TRSR_FeedBack
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
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
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            txtCourseTitleFeedBack.Text = Request.Form("txtCourseTitleFeedBack")
            txtTrainingRoomFeedBack.Text = Request.Form("txtTrainingRoomFeedBack")
            txtStartDateFeedBack.Text = Request.Form("txtStartDateFeedBack")
            txtCourseLevelFeedBack.Text = Request.Form("txtCourseLevelFeedBack")
            txtMaxNoParticipantFeedBack.Text = Request.Form("txtMaxNoParticipantFeedBack")
            txtNMCTrainersFeedBack.Text = Request.Form("txtNMCTrainersFeedBack")
            CheckSecurity()



            'Code for 
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpFeedbackDomain.Attributes.Add("OnChange", "return fillCategoryName('drpFeedbackTopic');")

            drpFeedbackTopic.Attributes.Add("onChange", "return keepSelectedID();")

            bindDropdownTopic()

            If Not IsPostBack Then
                'Code Added for Dropdown binding M.K
                bindDropdownDomain()
                bindDropdownAck()
                trRedBoarder.Visible = False
                'Code Added for Dropdown binding M.K


                hdEmployeePageName.Value = Session("EmployeePageName")
                objeAAMS.BindDropDown(ddlFeedback, "FEEDBACK", True, 3)
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(txtCourse, "COURSE", True, 3)

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

    Private Sub bindDropdownAck()
        ddlFeedbackAck.Items.Clear()
        ddlFeedbackAck.Items.Add("--All--")
        ddlFeedbackAck.Items.Add("Ack")
        ddlFeedbackAck.Items.Add("Not Ack")
    End Sub

    Private Sub bindDropdownDomain()
        Try
            Dim objOutputXml As XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            'Code Added  on 12 Aug 09
            Dim objTraining As New AAMS.bizTraining.bzDomain
            objOutputXml = New XmlDocument
            objOutputXml = objTraining.ListDomain()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                drpFeedbackDomain.DataSource = ds.Tables("DOMAIN")
                drpFeedbackDomain.DataTextField = "TR_VALTOPICDOM_NAME"
                drpFeedbackDomain.DataValueField = "TR_VALTOPICDOM_ID"
                drpFeedbackDomain.DataBind()
                Dim lst As New ListItem("--All--", "")
                drpFeedbackDomain.Items.Insert(0, lst)
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub bindDropdownTopic()
        Try
            If drpFeedbackDomain.SelectedValue.Trim() <> "" Then
                Dim objOutputXml As XmlDocument
                Dim objInputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<T_DOMAIN_INPUT><TR_VALTOPICDOM_ID></TR_VALTOPICDOM_ID><TR_COURSE_ID></TR_COURSE_ID></T_DOMAIN_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("TR_VALTOPICDOM_ID").InnerText = drpFeedbackDomain.SelectedValue.Trim()
                objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = txtCourse.SelectedValue.ToString.Trim()

                Dim objTraining As New AAMS.bizTraining.bzParticipantFeedBack
                objOutputXml = New XmlDocument
                objOutputXml = objTraining.GetDomainTopic(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpFeedbackTopic.DataSource = ds.Tables("DETAILS")
                    drpFeedbackTopic.DataTextField = "TR_TOPICS"
                    drpFeedbackTopic.DataValueField = "TR_CVALTOPIC_ID"
                    drpFeedbackTopic.DataBind()
                    Dim lst As New ListItem("--All--", "")
                    drpFeedbackTopic.Items.Insert(0, lst)
                    drpFeedbackTopic.SelectedValue = hdFeedbackTopicID.Value
                Else
                    drpFeedbackTopic.Items.Clear()
                    drpFeedbackTopic.Items.Insert(0, "--All--")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
    'Code for Getting Feedback Topic on the basis of Domain
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        If eventArgument <> "" Then
            Try
                Dim objOutputXml As XmlDocument
                Dim objInputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<T_DOMAIN_INPUT><TR_VALTOPICDOM_ID></TR_VALTOPICDOM_ID><TR_COURSE_ID></TR_COURSE_ID></T_DOMAIN_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("TR_VALTOPICDOM_ID").InnerText = eventArgument.Split("|").GetValue(0).ToString()
                objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = eventArgument.Split("|").GetValue(1).ToString()
                Dim objTraining As New AAMS.bizTraining.bzParticipantFeedBack
                objOutputXml = New XmlDocument
                objOutputXml = objTraining.GetDomainTopic(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    str = objOutputXml.OuterXml
                Else
                    str = ""
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    'Code for Getting Feedback Topic on the basis of Domain



    Private Sub SearchRecords(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objParticipantFeedBack As New AAMS.bizTraining.bzParticipantFeedBack
        Try
            objInputXml.LoadXml("<UP_RPT_FEEDBACK_REPORT_INPUT><COURSES_ID></COURSES_ID><FEEDBACKID></FEEDBACKID><SearchDateType /><COURSE_STARTDATEFROM/><COURSE_STARTDATETO/><COURSE_ENDDATE_FROM /><COURSE_ENDDATE_TO /><ACK /><FEEDBACKTOPIC /><FEEDBACKDOMAIN /><TRAINER /><AOFFICE /><COURSE /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /><Limited_To_Aoffice/><Limited_To_Region /><Limited_To_OwnAagency /><EmployeeID /></UP_RPT_FEEDBACK_REPORT_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("COURSES_ID").InnerText = hdCourseSessionFeedBack.Value
            objInputXml.DocumentElement.SelectSingleNode("FEEDBACKID").InnerText = ddlFeedback.SelectedValue


           
            'If ChkAck.Checked = True Then
            '    objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "1"
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "0"
            'End If

            If ddlFeedbackAck.SelectedItem.Text = "--All--" Then
                objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "1"
            ElseIf ddlFeedbackAck.SelectedItem.Text = "Ack" Then
                objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "2"
            ElseIf ddlFeedbackAck.SelectedItem.Text = "Not Ack" Then
                objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "3"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ACK").InnerText = "1"
            End If

            If Request("drpFeedbackTopic") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("FEEDBACKTOPIC").InnerText = Request("drpFeedbackTopic").ToString() 'drpFeedbackTopic.SelectedValue.Trim()
                '  hdFeedbackTopicID.Value = Request("drpFeedbackTopic").ToString()
            End If

            objInputXml.DocumentElement.SelectSingleNode("FEEDBACKTOPIC").InnerText = IIf(hdFeedbackTopicID.Value = "--All--", "", hdFeedbackTopicID.Value)


            If drpFeedbackDomain.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("FEEDBACKDOMAIN").InnerText = drpFeedbackDomain.SelectedItem.Text.Trim()
            End If


            Try
                drpFeedbackTopic.SelectedIndex = Convert.ToInt32(Val(hdFeedbackTopicIDIndex.Value))
            Catch ex As Exception

            End Try


            objInputXml.DocumentElement.SelectSingleNode("TRAINER").InnerText = txtTrainer2.Text
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
            If txtCourse.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COURSE").InnerText = txtCourse.SelectedItem.Text.Trim() 'txtCourse.SelectedValue.Trim()  

            End If

            If ddlDateType.SelectedIndex = 1 Then
                objInputXml.DocumentElement.SelectSingleNode("SearchDateType").InnerText = "1"
                objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATEFROM").InnerText = objeAAMS.GetDateFormat(txtStartDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATETO").InnerText = objeAAMS.GetDateFormat(txtStartDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
            ElseIf ddlDateType.SelectedIndex = 2 Then
                objInputXml.DocumentElement.SelectSingleNode("SearchDateType").InnerText = "2"
                objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_FROM").InnerText = objeAAMS.GetDateFormat(txtStartDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                objInputXml.DocumentElement.SelectSingleNode("COURSE_ENDDATE_TO").InnerText = objeAAMS.GetDateFormat(txtStartDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
            Else
                objInputXml.DocumentElement.SelectSingleNode("SearchDateType").InnerText = "0"
                objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATEFROM").InnerText = objeAAMS.GetDateFormat(txtStartDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
                objInputXml.DocumentElement.SelectSingleNode("COURSE_STARTDATETO").InnerText = objeAAMS.GetDateFormat(txtStartDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
            End If


            ' Security Input Xml Start.
            If Not Session("Security") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If
            ' Security Input Xml End.


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
                    ViewState("SortName") = "NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME"
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


            objOutputXml = objParticipantFeedBack.FeedBackReport(objInputXml)



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvFeedBack.DataSource = ds.Tables("DETAILS")
                gvFeedBack.DataBind()
                trRedBoarder.Visible = True


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
                ' txtRecordOnCurrentPage.Text = ds.Tables("AGNECY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName")
                    Case "NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "AGENCY"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "OFFICEID"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "FEEDBACK"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(3).Controls.Add(imgDown)

                        End Select
                    Case "COURSE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "DOMAIN"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "TOPIC"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select
                    Case "TRAINER"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select

                    Case "STARTDATE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(8).Controls.Add(imgDown)

                        End Select
                    Case "AOFFICE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(9).Controls.Add(imgDown)

                        End Select
                    Case "STATUS"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(10).Controls.Add(imgDown)

                        End Select
                    Case "TR_COMMENT"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                gvFeedBack.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                gvFeedBack.HeaderRow.Cells(11).Controls.Add(imgDown)

                        End Select
                End Select




            Else
                gvFeedBack.DataSource = Nothing
                gvFeedBack.DataBind()
                trRedBoarder.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TRSR_FeedBack.aspx")
    End Sub

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training FeedBack']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training FeedBack']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                'If strBuilder(1) = "0" Then
                '    btnSearch.Enabled = False
                'End If
                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
               
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub gvFeedBack_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFeedBack.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        e.Row.Cells(8).Text = DataBinder.Eval(e.Row.DataItem, "STARTDATE").ToString.Split(" ").GetValue(0)
        CType(e.Row.Cells(12).Controls(1), CheckBox).Checked = DataBinder.Eval(e.Row.DataItem, "ACK")
    End Sub

    Protected Sub gvFeedBack_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFeedBack.Sorting
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
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

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

    ' <DETAILS NAME="-DEEPA" AGENCY="Carlson Wagonlit Travels" OFFICEID="DELWL2597" FEEDBACK="Excellent" DOMAIN="Training of Amadeus application" TOPIC="DAY1(Starting on Amadeus,AIS ,Availability,Miscellaneous)" COURSE="Enter the world of Amadeus Vista" TRAINER="Rashmi Charan-left organistaion" AOFFICE="DEL" STARTDATE="29-Sep-03 00:00" STATUS="certified" ACK="False" TR_COURSEP_ID="937" TR_CVALTOPIC_ID="5" TR_COMMENT="" /> 
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Name", "Agency Name", "Office ID", "FeedBack", "Course", "Domain", "Topic", "Trainer", "Start Date", "AOffice", "Status", "Comment", "Ack"}
        Dim intArray() As Integer = {0, 1, 2, 3, 6, 4, 5, 7, 9, 8, 10, 14, 11}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportFEEDBACK.xls")
    End Sub
    'End Code For Export

    'This button is actually for save purpose .Id is given wrong as search inplace of save
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If gvFeedBack.Rows.Count > 0 Then
            Dim objInputXml, objOutputXml, objInputXmlTemp As New XmlDocument
            Dim objNodeList As XmlNodeList
            Dim objbzParticipantFeedBack As New AAMS.bizTraining.bzParticipantFeedBack
            Try
                objInputXml.LoadXml("<SAVE_PARTICIPANT_ACK_INPUT><DETAILS TR_COURSEP_ID= '' ACK='' TR_CVALTOPIC_ID='' /></SAVE_PARTICIPANT_ACK_INPUT>")
                Dim objNodeParticipant, objCloneNodeParticipant As XmlNode
                objNodeParticipant = objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                objCloneNodeParticipant = objNodeParticipant.CloneNode(True)
                objInputXml.DocumentElement.RemoveChild(objNodeParticipant)
                For Each gridRow As GridViewRow In gvFeedBack.Rows
                    '  hdTR_COURSEP_ID
                    With objCloneNodeParticipant

                        .Attributes("TR_CVALTOPIC_ID").Value = CType(gridRow.Cells(11).FindControl("hdTR_CVALTOPIC_ID"), HiddenField).Value
                        .Attributes("TR_COURSEP_ID").Value = CType(gridRow.Cells(11).FindControl("hdTR_COURSEP_ID"), HiddenField).Value
                        .Attributes("ACK").Value = IIf(CType(gridRow.Cells(12).FindControl("chkAck"), CheckBox).Checked = True, "1", "0")
                    End With
                    objInputXml.DocumentElement.AppendChild(objCloneNodeParticipant)
                    objCloneNodeParticipant = objNodeParticipant.CloneNode(True)
                Next

                'Here Back end Method Call
                objOutputXml = objbzParticipantFeedBack.UpdateparticipantAcknowledge(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    SearchRecords(PageOperation.Search)
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            Finally

            End Try
        End If
    End Sub
   
   
End Class
