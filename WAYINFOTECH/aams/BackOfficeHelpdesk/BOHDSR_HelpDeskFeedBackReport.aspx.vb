Imports System.IO
Partial Class BOHelpDesk_HDSR_HelpDeskFeedBackReport
    Inherits System.Web.UI.Page

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                btnExport.Attributes.Add("onClick", "return ValidateForm();")
                ' objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpFeedBackStatus, "BOFEEDBACKSTATUS", True, 3)
                objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)

            End If
            ' Checking security.
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Feedback Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Feedback Report']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    ' btnNew.Enabled = False
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

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim intAddedColumn As Integer 'Aoffice Added
            intAddedColumn = 1
            BindData(PageOperation.Search)
            If chkHelpDeskStatus.Checked = False Then
                'gvOrder.Columns(31).Visible = False 'Status
                'gvOrder.Columns(37).Visible = False 'AssignedTo
                gvOrder.Columns(32).Visible = False 'Status
                gvOrder.Columns(38).Visible = False 'AssignedTo
            End If
            If chkTechnicalStatus.Checked = False Then
                'gvOrder.Columns(32).Visible = False
                'gvOrder.Columns(38).Visible = False
                gvOrder.Columns(33).Visible = False
                gvOrder.Columns(39).Visible = False

            End If
            If chkSalesstatus.Checked = False Then
                'gvOrder.Columns(33).Visible = False
                'gvOrder.Columns(39).Visible = False

                gvOrder.Columns(34).Visible = False
                gvOrder.Columns(40).Visible = False
            End If
            If chkTrainingStatus.Checked = False Then
                'gvOrder.Columns(34).Visible = False
                'gvOrder.Columns(40).Visible = False
                gvOrder.Columns(35).Visible = False
                gvOrder.Columns(41).Visible = False
            End If
            If chkProductStatus.Checked = False Then
                'gvOrder.Columns(35).Visible = False
                'gvOrder.Columns(41).Visible = False
                gvOrder.Columns(36).Visible = False
                gvOrder.Columns(42).Visible = False
            End If
            If chkCorporateCommunication.Checked = False Then
                'gvOrder.Columns(36).Visible = False
                'gvOrder.Columns(42).Visible = False
                gvOrder.Columns(37).Visible = False
                gvOrder.Columns(43).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("BOHDSR_HelpDeskFeedBackReport.aspx")
    End Sub
#End Region


#Region "BindData(ByVal Operation As Integer)"
    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objFeedback As New AAMS.bizBOHelpDesk.bzFeedback
        Try
            Dim strFromYr As Integer
            Dim strToYr As Integer
            Dim strFromMon As Integer
            Dim strToMon As Integer
            strFromYr = txtDateFrom.Text.Split("/").GetValue(2)
            strToYr = txtDateTo.Text.Split("/").GetValue(2)
            strFromMon = txtDateFrom.Text.Split("/").GetValue(1)
            strToMon = txtDateFrom.Text.Split("/").GetValue(1)
            If ((strFromYr <> strToYr) Or (strFromMon <> strToMon)) Then
                lblError.Text = "Month and Year should be same"
                Exit Sub
            End If

            objInputXml.LoadXml("<TR_RPT_FEEDBACK_INPUT><LCode/><REGION/><AGENCYNAME/><OfficeID/><FEEDBACK_ID/><ExecutiveName/><HD_RE_ID/><DATEFROM/><DATETO/><LOGGEDBY/><DEPT/><ASSIGNEDTO/><FEEDBACK_STATUS_ID/><ISCRITICAL/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><EmployeeID /><SUG_DEPT_ID/></TR_RPT_FEEDBACK_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("OfficeID").InnerText = txtOfficeID.Text
            objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()


            objInputXml.DocumentElement.SelectSingleNode("ExecutiveName").InnerText = txtExecutiveName.Text
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = txtLTRNo.Text
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.GetDateFormat(txtDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.GetDateFormat(txtDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
            objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_ID").InnerText = txtFeedBackID.Text
            objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = txtLoggedBy.Text
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEDTO").InnerText = txtAssignedTo.Text
            objInputXml.DocumentElement.SelectSingleNode("FEEDBACK_STATUS_ID").InnerText = drpFeedBackStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ISCRITICAL").InnerText = chkCritical.Checked
            objInputXml.DocumentElement.SelectSingleNode("SUG_DEPT_ID").InnerText = drpFeedbackDept.SelectedValue

            ' Security Input Xml Start.
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            ' Security Input Xml End.


            'Start CODE for sorting and pagingl
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
                    ViewState("SortName") = "QUESTION1"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "QUESTION1"
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


            ' Calling Search method.
            objOutputXml = objFeedback.CustomerFeedBackReport(objInputXml)
            'objOutputXml.Load("c:\feed.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectNodes("QUESTION").Count > 0 Then

                    hdQuestionSet1.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(0).Attributes("QUESTION_TITLE").Value
                    hdQuestionSet2.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(1).Attributes("QUESTION_TITLE").Value

                    hdQuestionSet3.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(2).Attributes("QUESTION_TITLE").Value
                    hdQuestionSet4.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(3).Attributes("QUESTION_TITLE").Value

                    hdQuestionSet5.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(4).Attributes("QUESTION_TITLE").Value
                    hdQuestionSet6.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(5).Attributes("QUESTION_TITLE").Value

                    hdQuestionSet7.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(6).Attributes("QUESTION_TITLE").Value
                    hdQuestionSet8.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(7).Attributes("QUESTION_TITLE").Value

                    hdQuestionSet9.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(8).Attributes("QUESTION_TITLE").Value
                    hdQuestionSet10.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(9).Attributes("QUESTION_TITLE").Value

                End If
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("REPORTDETAILS").Rows.Count <> 0 Then
                    gvOrder.DataSource = ds.Tables("REPORTDETAILS")
                    gvOrder.DataBind()

                    'If chkHelpDeskStatus.Checked = False Then
                    '    gvOrder.Columns(29).Visible = False
                    '    gvOrder.Columns(34).Visible = False
                    'End If
                    'If chkTechnicalStatus.Checked = False Then
                    '    gvOrder.Columns(30).Visible = False
                    '    gvOrder.Columns(35).Visible = False
                    'End If
                    'If chkSalesstatus.Checked = False Then
                    '    gvOrder.Columns(31).Visible = False
                    '    gvOrder.Columns(36).Visible = False
                    'End If
                    'If chkTrainingStatus.Checked = False Then
                    '    gvOrder.Columns(32).Visible = False
                    '    gvOrder.Columns(37).Visible = False
                    'End If
                    'If chkProductStatus.Checked = False Then
                    '    gvOrder.Columns(33).Visible = False
                    '    gvOrder.Columns(38).Visible = False
                    'End If

                    If chkHelpDeskStatus.Checked = False Then
                        gvOrder.Columns(32).Visible = False 'Status
                        gvOrder.Columns(38).Visible = False 'AssignedTo
                    End If
                    If chkTechnicalStatus.Checked = False Then
                        gvOrder.Columns(33).Visible = False
                        gvOrder.Columns(39).Visible = False

                    End If
                    If chkSalesstatus.Checked = False Then
                        gvOrder.Columns(34).Visible = False
                        gvOrder.Columns(40).Visible = False
                    End If
                    If chkTrainingStatus.Checked = False Then
                        gvOrder.Columns(35).Visible = False
                        gvOrder.Columns(41).Visible = False
                    End If
                    If chkProductStatus.Checked = False Then
                        gvOrder.Columns(36).Visible = False
                        gvOrder.Columns(42).Visible = False
                    End If
                    If chkCorporateCommunication.Checked = False Then
                        gvOrder.Columns(37).Visible = False
                        gvOrder.Columns(43).Visible = False
                    End If
                    'Code Added For Paging And Sorting In case Of Delete The Record

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
                    hdRecordOnCurrentPage.Value = ds.Tables("REPORTDETAILS").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' 
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    'Select Case ViewState("SortName")
                    '    Case "AGENCYNAME"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    'End Select

                    SetImageForSorting(gvOrder)

                    '  <TR_RPT_FEEDBACK_OUTPUT>
                    '    ' <QUESTION QUESTION_NO='' QUESTION_TITLE='' />
                    '    ' <REPORTDETAILS QUESTION1 = '' QUESTION2 = '' QUESTION3 = '' QUESTION4 = '' QUESTION5 = '' QUESTION6 = '' QUESTION7 = '' QUESTION8 = '' QUESTION9 = '' QUESTION10 = '' SUGGESTION_HELPDESK='' SUGGESTION_TECHNICAL = '' SUGGESTION_SALES = '' SUGGESTION_TRAINING = '' SUGGESTION_PRODUCT = '' STATION = '' SURVEYOR= '' LTRNo ='' DATE ='' EXECUTIVENAME = '' TRAVELAGENCY= '' OFFICEID = '' INFOTOHOD = '' ACTIONTAKEN_HELPDESK='' ACTIONTAKEN_TECHNICAL = '' ACTIONTAKEN_SALES = '' ACTIONTAKEN_TRAINING = '' ACTIONTAKEN_PRODUCT = '' STATUS_HELPDESK='' STATUS_TECHNICAL='' STATUS_SALES ='' STATUS_TRAINING='' STATUS_PRODUCT='' />
                    '    ' <Errors Status="FALSE">
                    '    ' <Error Code='' Description=''/>
                    '    ' </Errors>
                    '    '</TR_RPT_FEEDBACK_OUTPUT>
                    If objOutputXml.DocumentElement.SelectNodes("QUESTION").Count > 0 Then

                        'hdQuestionSet1.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(0).Attributes("QUESTION_TITLE").Value
                        'hdQuestionSet2.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(1).Attributes("QUESTION_TITLE").Value

                        'hdQuestionSet3.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(2).Attributes("QUESTION_TITLE").Value
                        'hdQuestionSet4.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(3).Attributes("QUESTION_TITLE").Value

                        'hdQuestionSet5.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(4).Attributes("QUESTION_TITLE").Value
                        'hdQuestionSet6.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(5).Attributes("QUESTION_TITLE").Value

                        'hdQuestionSet7.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(6).Attributes("QUESTION_TITLE").Value
                        'hdQuestionSet8.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(7).Attributes("QUESTION_TITLE").Value

                        'hdQuestionSet9.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(8).Attributes("QUESTION_TITLE").Value
                        'hdQuestionSet10.Value = objOutputXml.DocumentElement.SelectNodes("QUESTION").Item(9).Attributes("QUESTION_TITLE").Value

                        'gvOrder.HeaderRow.Cells(0).Text = hdQuestionSet1.Value
                        'gvOrder.HeaderRow.Cells(1).Text = hdQuestionSet2.Value

                        'gvOrder.HeaderRow.Cells(2).Text = hdQuestionSet3.Value
                        'gvOrder.HeaderRow.Cells(3).Text = hdQuestionSet4.Value

                        'gvOrder.HeaderRow.Cells(4).Text = hdQuestionSet5.Value
                        'gvOrder.HeaderRow.Cells(5).Text = hdQuestionSet6.Value

                        'gvOrder.HeaderRow.Cells(6).Text = hdQuestionSet7.Value
                        'gvOrder.HeaderRow.Cells(7).Text = hdQuestionSet8.Value

                        'gvOrder.HeaderRow.Cells(8).Text = hdQuestionSet9.Value
                        'gvOrder.HeaderRow.Cells(9).Text = hdQuestionSet10.Value

                    End If
                    'bindQuestins_Status()
                    '  Added Code To Show Image '

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record

                Else
                    gvOrder.DataSource = Nothing
                    gvOrder.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

    Private Function bindQuestins_Status() As XmlDocument
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objFeedback As New AAMS.bizBOHelpDesk.bzFeedback
        objOutputXml = objFeedback.ListFeedbackQuestions()
        Return objOutputXml
    End Function
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOrder.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim grvRow As GridViewRow
            grvRow = e.Row
            '  <TR_RPT_FEEDBACK_OUTPUT>
            ' <QUESTION QUESTION_NO='' QUESTION_TITLE='' />
            ' <REPORTDETAILS QUESTION1 = '' QUESTION2 = '' QUESTION3 = '' QUESTION4 = '' QUESTION5 = '' QUESTION6 = '' QUESTION7 = '' QUESTION8 = '' QUESTION9 = '' QUESTION10 = '' SUGGESTION_HELPDESK='' SUGGESTION_TECHNICAL = '' SUGGESTION_SALES = '' SUGGESTION_TRAINING = '' SUGGESTION_PRODUCT = '' STATION = '' SURVEYOR= '' LTRNo ='' DATE ='' EXECUTIVENAME = '' TRAVELAGENCY= '' OFFICEID = '' INFOTOHOD = '' ACTIONTAKEN_HELPDESK='' ACTIONTAKEN_TECHNICAL = '' ACTIONTAKEN_SALES = '' ACTIONTAKEN_TRAINING = '' ACTIONTAKEN_PRODUCT = '' STATUS_HELPDESK='' STATUS_TECHNICAL='' STATUS_SALES ='' STATUS_TRAINING='' STATUS_PRODUCT='' />
            ' <Errors Status="FALSE">
            ' <Error Code='' Description=''/>
            ' </Errors>
            '</TR_RPT_FEEDBACK_OUTPUT>

            If e.Row.RowType = DataControlRowType.Header Then
                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(0).Controls(0), LinkButton).Text = hdQuestionSet1.Value
                Else
                    e.Row.Cells(0).Text = hdQuestionSet1.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(1).Controls(0), LinkButton).Text = hdQuestionSet2.Value
                Else
                    e.Row.Cells(1).Text = hdQuestionSet2.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(2).Controls(0), LinkButton).Text = hdQuestionSet3.Value
                Else
                    e.Row.Cells(2).Text = hdQuestionSet3.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(3).Controls(0), LinkButton).Text = hdQuestionSet4.Value
                Else
                    e.Row.Cells(3).Text = hdQuestionSet4.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(4).Controls(0), LinkButton).Text = hdQuestionSet5.Value
                Else
                    e.Row.Cells(4).Text = hdQuestionSet5.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(5).Controls(0), LinkButton).Text = hdQuestionSet6.Value
                Else
                    e.Row.Cells(5).Text = hdQuestionSet6.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(6).Controls(0), LinkButton).Text = hdQuestionSet7.Value
                Else
                    e.Row.Cells(6).Text = hdQuestionSet7.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(7).Controls(0), LinkButton).Text = hdQuestionSet8.Value
                Else
                    e.Row.Cells(7).Text = hdQuestionSet8.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(8).Controls(0), LinkButton).Text = hdQuestionSet9.Value
                Else
                    e.Row.Cells(8).Text = hdQuestionSet9.Value
                End If

                If gvOrder.AllowSorting = True Then
                    CType(grvRow.Cells(9).Controls(0), LinkButton).Text = hdQuestionSet10.Value
                Else
                    e.Row.Cells(9).Text = hdQuestionSet10.Value
                End If

            End If
        End If
    End Sub

    Protected Sub gvOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvOrder.Sorting
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
            BindData(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
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
        '<REPORTDETAILS QUESTION1="Very Poor" QUESTION2="Poor" QUESTION3="Good" QUESTION4="Very Good" QUESTION5="Best" 
        'QUESTION6="Very Poor" QUESTION7="Poor" QUESTION8="Good" QUESTION9="Very Good" QUESTION10="Best" 
        'SUGGESTION_HELPDESK="" SUGGESTION_TECHNICAL="" SUGGESTION_SALES="test" SUGGESTION_TRAINING="" 
        'SUGGESTION_PRODUCT="" STATION="AGR" SURVEYOR="Admin" LTRNo="148" DATE="16/06/2008 10:59" EXECUTIVENAME=""
        'TRAVELAGENCY="Bird Travels Pvt Ltd" OFFICEID="AGRFF2120" INFOTOHOD="NA" ACTIONTAKEN_HELPDESK="" 
        'ACTIONTAKEN_TECHNICAL="" ACTIONTAKEN_SALES="done" ACTIONTAKEN_TRAINING="" ACTIONTAKEN_PRODUCT=""
        'STATUS_HELPDESK="" STATUS_TECHNICAL="" STATUS_SALES="Pending" STATUS_TRAINING="" 
        'STATUS_PRODUCT="" ASSIGNEDTO_HELPDESK="" ASSIGNEDTO_TECHNICAL="" ASSIGNEDTO_SALES="Abhishek Bhattacharya" 
        'ASSIGNEDTO_TRAINING="" ASSIGNEDTO_PRODUCT="" CALLASSIGNEDTO=''/>

        'Dim strArray() As String = {hdQuestionSet1.Value, hdQuestionSet2.Value, hdQuestionSet3.Value, hdQuestionSet4.Value, hdQuestionSet5.Value, hdQuestionSet6.Value, hdQuestionSet7.Value, hdQuestionSet8.Value, hdQuestionSet9.Value, hdQuestionSet10.Value, "Suggestion For Helpdesk", "Suggestion For Technical", "Suggestion For Sales", "Suggestion For Training", "Suggestion For Products", "Suggestion For Corp. Comm.", "Station", "Surveyor", "LTR No.", "Date", "Caller Name", "Call Assigned To", "Travel Agency", "OfficeId", "Forwarded Date(HOD)", "Action Taken For HelpDesk", "Action Taken For Technical", "Action Taken For Sales", "Action Taken For Training", "Action Taken For Product", "Action Taken For Corp. Comm.", "Helpdesk Status", "Technical Status", "Sales Status", "Training Status", "Product Status", "Corp. Comm. Status", "Assigned To(Helpdesk)", "Assigned To(Technical)", "Assigned To(Sales)", "Assigned To(Training)", "Assigned To(Product)", "Assigned To(Corp. Comm.)"}
        'Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 38, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42}
        Dim strArray() As String = {hdQuestionSet1.Value, hdQuestionSet2.Value, hdQuestionSet3.Value, hdQuestionSet4.Value, hdQuestionSet5.Value, hdQuestionSet6.Value, hdQuestionSet7.Value, hdQuestionSet8.Value, hdQuestionSet9.Value, hdQuestionSet10.Value, "Suggestion For Helpdesk", "Suggestion For Technical", "Suggestion For Sales", "Suggestion For Training", "Suggestion For Products", "Suggestion For Corp. Comm.", "Station", "Region", "Surveyor", "LTR No.", "Date", "Caller Name", "Call Assigned To", "Travel Agency", "OfficeId", "Forwarded Date(HOD)", "Action Taken For HelpDesk", "Action Taken For Technical", "Action Taken For Sales", "Action Taken For Training", "Action Taken For Product", "Action Taken For Corp. Comm.", "Helpdesk Status", "Technical Status", "Sales Status", "Training Status", "Product Status", "Corp. Comm. Status", "Assigned To(Helpdesk)", "Assigned To(Technical)", "Assigned To(Sales)", "Assigned To(Training)", "Assigned To(Product)", "Assigned To(Corp. Comm.)"}
        Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 39, 15, 16, 43, 17, 18, 19, 38, 20, 21, 22, 23, 24, 25, 26, 27, 40, 28, 29, 30, 31, 32, 41, 33, 34, 35, 36, 37, 42}

        Dim strALText As New ArrayList
        For i As Integer = 0 To strArray.Length - 1
            strALText.Add(strArray(i))
        Next

        Dim strALIndex As New ArrayList
        For i As Integer = 0 To intArray.Length - 1
            strALIndex.Add(intArray(i))
        Next


        If chkHelpDeskStatus.Checked = False Then
            strALText.Remove("Helpdesk Status")
            strALText.Remove("Assigned To(Helpdesk)")
            strALIndex.Remove(28)
            strALIndex.Remove(33)
        End If

        If chkTechnicalStatus.Checked = False Then
            strALText.Remove("Technical Status")
            strALText.Remove("Assigned To(Technical)")
            strALIndex.Remove(29)
            strALIndex.Remove(34)
        End If

        If chkSalesstatus.Checked = False Then
            strALText.Remove("Sales Status")
            strALText.Remove("Assigned To(Sales)")
            strALIndex.Remove(30)
            strALIndex.Remove(35)
        End If

        If chkTrainingStatus.Checked = False Then
            strALText.Remove("Training Status")
            strALText.Remove("Assigned To(Training)")
            strALIndex.Remove(31)
            strALIndex.Remove(36)
        End If

        If chkProductStatus.Checked = False Then
            strALText.Remove("Product Status")
            strALText.Remove("Assigned To(Product)")
            strALIndex.Remove(32)
            strALIndex.Remove(37)
        End If

        If chkCorporateCommunication.Checked = False Then
            strALText.Remove("Product Status")
            strALText.Remove("Assigned To(Product)")
            strALIndex.Remove(32)
            strALIndex.Remove(37)
        End If


        If chkCorporateCommunication.Checked = False Then
            strALText.Remove("Corp. Comm. Status")
            strALText.Remove("Assigned To(Corp. Comm.)")
            strALIndex.Remove(41)
            strALIndex.Remove(42)
        End If

        Dim myStrArr As String() = CType(strALText.ToArray(GetType(String)), String())
        Dim myIntArr() As Integer = CType(strALIndex.ToArray(GetType(Integer)), Integer())

        objExport.ExportDetails(objOutputXml, "REPORTDETAILS", myIntArr, myStrArr, ExportExcel.ExportFormat.Excel, "FEEDBACKREPORTDETAILS.xls")
    End Sub
    'End Code For Export

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





    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
