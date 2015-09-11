Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Partial Class HelpDesk_HDSR_QuestionSet
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim i, j As Integer
            '    Dim s, s1, s2 As String
            Session("PageName") = "HelpDesk/HDSR_QuestionSet.aspx"
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        QuestionDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                For j = 1990 To DateTime.Now.Year
                    drpYear.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                    i += 1
                    drpYear.SelectedValue = DateTime.Now.Year
                Next
            End If

            ' Code For Delete Start.
            If hdDeleteID.Value <> "" Then
                QuestionDelete(hdDeleteID.Value)
            End If
            ' Code For Delete End.

        Catch ex As Exception
            ex.Message.ToString()
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            QuestionSearch()
        Catch ex As Exception
            lblError.Text = ex.Message()
        End Try
    End Sub
    Private Sub QuestionSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzQ As New AAMS.bizHelpDesk.bzFeedbackSet
        objInputXml.LoadXml("<HD_SEARCHQUESTION_INPUT><MONTH/><YEAR/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHQUESTION_INPUT>")
        If drpMonth.SelectedValue <> "" Then
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonth.SelectedValue
        End If
        objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue
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
            ViewState("SortName") = "ID"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ID" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If
        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'Here Back end Method Call
        objOutputXml = objbzQ.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            pnlPaging.Visible = True
            gvQuestionSet.DataSource = ds.Tables("Set")
            gvQuestionSet.DataBind()
            lblError.Text = ""
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
            txtRecordOnCurrentPage.Text = ds.Tables("Set").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'
            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Select Case ViewState("SortName")
                Case "ID"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuestionSet.HeaderRow.Cells(0).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuestionSet.HeaderRow.Cells(0).Controls.Add(imgDown)
                    End Select
                Case "Month"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuestionSet.HeaderRow.Cells(1).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuestionSet.HeaderRow.Cells(1).Controls.Add(imgDown)
                    End Select
                Case "Year"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuestionSet.HeaderRow.Cells(2).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuestionSet.HeaderRow.Cells(2).Controls.Add(imgDown)
                    End Select
            End Select
            '' @ Added Code To Show Image'
            ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Else
            gvQuestionSet.DataSource = Nothing
            gvQuestionSet.DataBind()
            pnlPaging.Visible = False
            txtTotalRecordCount.Text = "0"
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Private Sub QuestionDelete(ByVal QID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDq As New AAMS.bizHelpDesk.bzFeedbackSet
        Try
            objInputXml.LoadXml("<HD_DELETEQUESTION_INPUT><ID></ID></HD_DELETEQUESTION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ID").InnerText = QID
            hdDeleteID.Value = ""
            objOutputXml = objbzDq.Delete(objInputXml)
            QuestionSearch()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'Session("Action") = Request.QueryString("Action")
                'Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                'If txtRecordOnCurrentPage.Text = "1" Then
                '    If CurrentPage - 1 > 0 Then
                '        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                '    Else
                '        ddlPageNumber.SelectedValue = "1"
                '    End If
                'End If

                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("HDSR_QuestionSet.aspx")
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            QuestionSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            QuestionSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            QuestionSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub gvQuestionSet_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvQuestionSet.RowCommand
    '    If e.CommandName = "DeleteX" Then
    '        Session("Action") = "D|" & e.CommandArgument & ""
    '        QuestionDelete(e.CommandArgument)
    '    End If
    'End Sub

    Protected Sub gvQuestionSet_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuestionSet.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim hdQuesId As HiddenField
        Dim linkEdit As LinkButton
        Dim objSecurityXml As New XmlDocument
        Dim linkDelete As LinkButton
        'linkEdit = e.Row.FindControl("lnkEdit")
        linkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        linkDelete = e.Row.FindControl("lnkDelete")
        hdQuesId = e.Row.FindControl("hdQID")
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Questionset']").Attributes("Value").Value)
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdQuesId.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                '    linkEdit.Enabled = False
                'Else
                '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdQuesId.Value & ");")
                'End If
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdQuesId.Value) & "');")
            End If
        Else
            linkDelete.Enabled = True
            linkEdit.Enabled = True
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdQuesId.Value & "');")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdQuesId.Value) & "');")
        End If
    End Sub

    Protected Sub gvQuestionSet_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvQuestionSet.Sorting
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
            QuestionSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        gvQuestionSet.AllowSorting = False
        gvQuestionSet.HeaderStyle.ForeColor = Drawing.Color.Black
        QuestionSearchExport()
    End Sub
    Private Sub QuestionSearchExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzQ As New AAMS.bizHelpDesk.bzFeedbackSet
        objInputXml.LoadXml("<HD_SEARCHQUESTION_INPUT><MONTH/><YEAR/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHQUESTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = drpMonth.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = drpYear.SelectedValue
     

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "ID"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ID" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If
        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'Here Back end Method Call
        objOutputXml = objbzQ.Search(objInputXml)
        Dim nl As XmlNodeList
        Dim xnode As XmlNode
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            pnlPaging.Visible = True
            gvQuestionSet.DataSource = ds.Tables("Set")
            gvQuestionSet.DataBind()
            Dim objExport As New ExportExcel
            nl = objOutputXml.DocumentElement.SelectNodes("Set")
            For Each xnode In nl
                Dim strdate As String
                strdate = xnode.Attributes("Month").Value
                If strdate <> "" Then
                    xnode.Attributes("Month").Value = MonthName(strdate)
                End If
            Next
            Dim strArray() As String = {"SetId", "Month", "Year"}
            Dim intArray() As Integer = {0, 1, 2}
            objExport.ExportDetails(objOutputXml, "Set", intArray, strArray, ExportExcel.ExportFormat.Excel, "QuestionSetExport.xls")
            lblError.Text = ""
        Else
            gvQuestionSet.DataSource = Nothing
            gvQuestionSet.DataBind()
            pnlPaging.Visible = False
            txtTotalRecordCount.Text = "0"
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
