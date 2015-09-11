Imports System.io
Partial Class Training_TRSR_ParticipantBasket
    Inherits System.Web.UI.Page

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
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
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' Checking security.
            CheckSecurity()
            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If
            txtEmployee.Text = Request.Form("txtEmployee")
            If Not IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                txtAgency.Attributes.Add("onkeydown", "return ActDeAct();")
                txtAgency.Focus()
                objeAAMS.BindDropDown(ddlStatus, "PARTCIPANTSTATUS", True, 3)
                ddlStatus.Items.Add(New ListItem("blank", "10012"))
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(ddlCourse, "COURSE", True, 3)

                '   Checking Permission For Own Office and Region start.
                If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                    ddlAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    ddlAOffice.Enabled = False
                End If

                '   Checking Permission For Own Office and Region end.
               
            End If
            '   Deleting records.
            If (hdParBasketID.Value <> "") Then
                DeleteRecords()
            End If

            If Request.QueryString("Popup") Is Nothing Then

            End If

            If Request.QueryString("CourseId") IsNot Nothing Then
                ddlCourse.SelectedValue = objED.Decrypt(Request.QueryString("CourseId"))
                ddlCourse.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_ParticipantBasket.aspx")
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TRSR_ParticipantBasket.aspx?" + Request.QueryString.ToString)
    End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim chkAll As New HtmlInputCheckBox
            If Request.QueryString("Popup") IsNot Nothing Then
                If Request.QueryString("Popup") = "T" Then
                    btnSelect.Visible = True
                    gvParticipantBasket.Columns(0).Visible = True
                Else
                    btnSelect.Visible = False
                End If
            Else
                gvParticipantBasket.Columns(0).Visible = False
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
        Try
            objInputXml.LoadXml("<TR_SEARCHBASKET_INPUT><OFFICE_ID/><LCODE/><AGENCYNAME/><AGENCYSTAFFID/><AGENCYSTAFFNAME/><TR_COURSE_ID/><EMPLOYEEID/><AOFFICE/><TR_BASKET_REQUEST_ID/><DATEFROM/><DATETO/><FROMWEB/><TR_PARTSTATUS_ID/> <Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><RESP_1A/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCHBASKET_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdCourseLCode.Value.Trim
            objInputXml.DocumentElement.SelectSingleNode("OFFICE_ID").InnerText = txtOfficeID.Text.Trim()
            If hdCourseLCode.Value.Trim = "" Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = Request.Form("txtAgency").Trim()
            End If
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtAgencyStaff").Trim()
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = ddlCourse.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = hdTrainingEmployeeID.Value
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TR_BASKET_REQUEST_ID").InnerText = txtRequestID.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("TR_PARTSTATUS_ID").InnerText = ddlStatus.SelectedValue


            ' Security Input Xml Start.
            If Session("Security") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = objeAAMS.EmployeeID(Session("Security"))
            End If
            ' Security Input Xml End.


            txtReqDateFrom.Text = Request.Form("txtReqDateFrom").Trim()
            Dim strDateFrom As String = Request.Form("txtReqDateFrom").Trim()
            If strDateFrom <> "" Then
                strDateFrom = strDateFrom.Split("/").GetValue(1) + "/" + strDateFrom.Split("/").GetValue(0) + "/" + strDateFrom.Split("/").GetValue(2)
            End If

            txtreqDateTo.Text = Request.Form("txtreqDateTo").Trim()

            Dim strDateTo As String = Request.Form("txtreqDateTo").Trim()
            If strDateTo <> "" Then
                strDateTo = strDateTo.Split("/").GetValue(1) + "/" + strDateTo.Split("/").GetValue(0) + "/" + strDateTo.Split("/").GetValue(2)
            End If

            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = strDateFrom
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = strDateTo


            If rdlrequest.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = "1"
            ElseIf rdlrequest.SelectedValue = "2" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = "0"
            ElseIf rdlrequest.SelectedValue = "3" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = ""

            End If

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
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "0"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = IIf(ViewState("Desc") = "FALSE", "0", "1")
                End If
            End If
            'End Code for paging and sorting


            objOutputXml = objParticipantsBasket.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DETAILS").Rows.Count <> 0 Then
                    gvParticipantBasket.DataSource = ds.Tables("DETAILS")
                    gvParticipantBasket.DataBind()
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

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "TR_BASKET_REQUEST_ID"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "TR_BASKET_DATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select
                        Case "NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(2).Controls.Add(imgDown)
                            End Select

                        Case "ADDRESS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(3).Controls.Add(imgDown)
                            End Select
                        Case "OFFICEID"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(4).Controls.Add(imgDown)
                            End Select
                        Case "TR_COURSE_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(5).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(5).Controls.Add(imgDown)
                            End Select

                        Case "STAFFNAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(6).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(6).Controls.Add(imgDown)
                            End Select
                        Case "EMPLOYEE_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(7).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(7).Controls.Add(imgDown)
                            End Select
                        Case "STATUS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(8).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(8).Controls.Add(imgDown)
                            End Select
                        Case "SESSION_DATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(9).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(9).Controls.Add(imgDown)
                            End Select
                        Case "PREFERREDDATE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(10).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(10).Controls.Add(imgDown)
                            End Select
                        Case "AOFFICE"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(11).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(11).Controls.Add(imgDown)
                            End Select

                        Case "EMAIL"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(12).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(12).Controls.Add(imgDown)
                            End Select
                        Case "TR_BASKET_REMARKS"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvParticipantBasket.HeaderRow.Cells(13).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvParticipantBasket.HeaderRow.Cells(13).Controls.Add(imgDown)
                            End Select

                    End Select
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    gvParticipantBasket.DataSource = Nothing
                    gvParticipantBasket.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If

            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvParticipantBasket.DataSource = Nothing
                gvParticipantBasket.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Private Sub BindDataExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
        Try
            'pnlPaging.Visible = False
            objInputXml.LoadXml("<TR_SEARCHBASKET_INPUT><LCODE/><AGENCYNAME/><AGENCYSTAFFID/><AGENCYSTAFFNAME/><TR_COURSE_ID/><EMPLOYEEID/><AOFFICE/><TR_BASKET_REQUEST_ID/><DATEFROM/><DATETO/><FROMWEB/><TR_PARTSTATUS_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCHBASKET_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = Request.Form("txtAgency").Trim()
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFNAME").InnerText = Request.Form("txtAgencyStaff").Trim()
            objInputXml.DocumentElement.SelectSingleNode("TR_COURSE_ID").InnerText = ddlCourse.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = hdTrainingEmployeeID.Value
            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TR_BASKET_REQUEST_ID").InnerText = txtRequestID.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("TR_PARTSTATUS_ID").InnerText = ddlStatus.SelectedValue
            ' objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = Request.Form("txtReqDateFrom").Trim()
            ' objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = Request.Form("txtreqDateTo").Trim()

            txtReqDateFrom.Text = Request.Form("txtReqDateFrom").Trim()
            Dim strDateFrom As String = Request.Form("txtReqDateFrom").Trim()
            If strDateFrom <> "" Then
                strDateFrom = strDateFrom.Split("/").GetValue(1) + "/" + strDateFrom.Split("/").GetValue(0) + "/" + strDateFrom.Split("/").GetValue(2)
            End If

            txtreqDateTo.Text = Request.Form("txtreqDateTo").Trim()

            Dim strDateTo As String = Request.Form("txtreqDateTo").Trim()
            If strDateTo <> "" Then
                strDateTo = strDateTo.Split("/").GetValue(1) + "/" + strDateTo.Split("/").GetValue(0) + "/" + strDateTo.Split("/").GetValue(2)
            End If

            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = strDateFrom
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = strDateTo


            If rdlrequest.SelectedValue = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = "1"
            ElseIf rdlrequest.SelectedValue = "2" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = "0"
            ElseIf rdlrequest.SelectedValue = "3" Then
                objInputXml.DocumentElement.SelectSingleNode("FROMWEB").InnerText = ""

            End If

            'Start CODE for sorting and paging




            '  objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "0"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = IIf(ViewState("Desc") = "FALSE", "0", "1")
            End If

            'End Code for paging and sorting


            objOutputXml = objParticipantsBasket.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DETAILS").Rows.Count <> 0 Then
                    gvParticipantBasket.DataSource = ds.Tables("DETAILS")
                    gvParticipantBasket.DataBind()
                Else
                    gvParticipantBasket.DataSource = Nothing
                    gvParticipantBasket.DataBind()
                    'txtTotalRecordCount.Text = "0"
                    'hdRecordOnCurrentPage.Value = "0"
                    'pnlPaging.Visible = False
                End If

            Else
                ' txtTotalRecordCount.Text = "0"
                ' hdRecordOnCurrentPage.Value = "0"
                ' pnlPaging.Visible = False
                gvParticipantBasket.DataSource = Nothing
                gvParticipantBasket.DataBind()
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

#Region "DeleteRecords()"
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objParticipantsBasket As New AAMS.bizTraining.bzParticipantsBasket
            If hdParBasketID.Value <> "" Then
                objInputXml.LoadXml("<TR_DELETEBASKET_INPUT><TR_BASKET_REQUEST_ID /></TR_DELETEBASKET_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("TR_BASKET_REQUEST_ID").InnerText = hdParBasketID.Value
                hdParBasketID.Value = ""
                objOutputXml = objParticipantsBasket.Delete(objInputXml)
                BindData(PageOperation.Search)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    lblError.Text = objeAAMSMessage.messDelete
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "gvParticipantBasket_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParticipantBasket.RowDataBound"
    Protected Sub gvParticipantBasket_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvParticipantBasket.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then

                'Dim chkSelect As HtmlInputCheckBox
                'If Request.QueryString("PageFrom") IsNot Nothing Then
                '    chkSelect = CType(e.Row.FindControl("chkAllSelect"), HtmlInputCheckBox)
                '    If chkSelect IsNot Nothing Then
                '        chkSelect.Visible = True
                '    End If
                'Else
                '    chkSelect = CType(e.Row.FindControl("chkAllSelect"), HtmlInputCheckBox)
                '    If chkSelect IsNot Nothing Then
                '        chkSelect.Visible = False
                '    End If
                'End If


                Exit Sub
            End If
            '************* Code for edit link ****************************************************************
            Dim btnSelect As LinkButton
            Dim hdContactID As HiddenField
            Dim btnEdit As LinkButton
            Dim chk As New HtmlInputCheckBox


            hdContactID = CType(e.Row.FindControl("hdBasketID"), HiddenField)
            btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            btnSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)
            chk = CType(e.Row.FindControl("chkSelect"), HtmlInputCheckBox)

            ' btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdContactID.Value + "');")
            '************* end code for edit ***************************************************************** 

            '************* Code for Delete link ****************************************************************
            Dim btnDelete As LinkButton
            btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            ' btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdContactID.Value + "');")
            '************* end code for delete link ***************************************************************** 
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdContactID.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = False
                'Else
                '    btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdContactID.Value + "');")
                'End If
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdContactID.Value) + "');")
            Else
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdContactID.Value) + "','" + DataBinder.Eval(e.Row.DataItem, "STATUS") + "');")
                btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdContactID.Value + "');")

            End If

            If Not Request.QueryString("Popup") Is Nothing Then
                Dim strStatus As String = DataBinder.Eval(e.Row.DataItem, "STATUS")
                'dim strPageFrom as String=Request.q
                'Earlier it was asked to disable the select button when status is closed and done
                'on date 10 july 08 this was changed now ,select button is disable when status is not blank
                'If strStatus.ToUpper = "CLOSED" Or strStatus.ToUpper = "DONE" Then
                Dim strAgency As String = ""

                strAgency = DataBinder.Eval(e.Row.DataItem, "NAME").ToString.Replace(vbCrLf, "")
                strAgency = strAgency.Replace("'", "")

                If strStatus <> "" Then
                    If Request.QueryString("PageFrom") IsNot Nothing Then
                        btnSelect.Enabled = False
                        chk.Disabled = True
                        chk.Visible = False

                        'Code Added by Neeraj
                        btnEdit.Enabled = False
                        btnEdit.Attributes.Remove("OnClick")
                        'Code Added by Neeraj
                    Else
                        '  btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + DataBinder.Eval(e.Row.DataItem, "AGENCYSTAFFID") + "','" + DataBinder.Eval(e.Row.DataItem, "NAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "STAFFNAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "LCODE") + "','" + DataBinder.Eval(e.Row.DataItem, "TR_BASKET_REQUEST_ID") + "');")
                        btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + DataBinder.Eval(e.Row.DataItem, "AGENCYSTAFFID") + "','" + strAgency + "','" + DataBinder.Eval(e.Row.DataItem, "STAFFNAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "LCODE") + "','" + DataBinder.Eval(e.Row.DataItem, "TR_BASKET_REQUEST_ID") + "');")
                    End If
                Else
                    ' btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + DataBinder.Eval(e.Row.DataItem, "AGENCYSTAFFID") + "','" + DataBinder.Eval(e.Row.DataItem, "NAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "STAFFNAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "LCODE") + "','" + DataBinder.Eval(e.Row.DataItem, "TR_BASKET_REQUEST_ID") + "');")
                    btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + DataBinder.Eval(e.Row.DataItem, "AGENCYSTAFFID") + "','" + strAgency + "','" + DataBinder.Eval(e.Row.DataItem, "STAFFNAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "LCODE") + "','" + DataBinder.Eval(e.Row.DataItem, "TR_BASKET_REQUEST_ID") + "');")
                End If
            Else
                btnSelect.Visible = False
                chk.Visible = False

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click"
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        Try
            'gvParticipantBasket.AllowSorting = False
            'gvParticipantBasket.HeaderStyle.ForeColor = Drawing.Color.Black
            'BindDataExport()
            'If gvParticipantBasket.Rows.Count > 0 Then
            '    ' PrepareGridViewForExport(gvParticipantBasket)
            '    ExportGridView(gvParticipantBasket, "ParticipantBasket.xls")
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ExportGridView(ByVal gv2, ByVal FileName)
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        Dim attachment As String = ""
        Try
            gvParticipantBasket.Columns(14).Visible = False
            pnlExport.Visible = False
            attachment = "attachment; filename=" + FileName
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"
            gv2.Caption = "ParticipantBasket"
            gv2.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(gv2)
            frm.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.End()
            pnlExport.Visible = True
            ' gvParticipantBasket.Columns(13).Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


    End Sub

    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        Dim l As New Literal
        Dim name As String = ""
        Dim lb As New LinkButton

        Dim i As Int32
        For i = 0 To gv.Controls.Count - 1
            If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
                l.Text = CType(gv.Controls(i), LinkButton).Text
                gv.Controls.Remove(gv.Controls(i))
                gv.Controls.AddAt(i, l)

            End If

            If (gv.Controls(i).HasControls()) Then
                PrepareGridViewForExport(gv.Controls(i))
            End If

        Next
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Participant Basket']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                    btnExport.Enabled = False
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

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

    Protected Sub gvParticipantBasket_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvParticipantBasket.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvParticipantBasket_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvParticipantBasket.Sorting
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    'Code for Export
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Req ID", "Date", "Agency", "Address", "OfficeID", "Course", "Staff Name", "Employee Name", "Status", "Session Date", "Preferred Date", "AOffice", "Email", "Remarks"}
        Dim intArray() As Integer = {0, 1, 2, 3, 4, 6, 10, 7, 14, 15, 12, 9, 13, 5}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportPARTICIPANTBASKET.xls")
    End Sub
    'End Code For Export

    Protected Sub ddlCourse_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCourse.SelectedIndexChanged

    End Sub
End Class
