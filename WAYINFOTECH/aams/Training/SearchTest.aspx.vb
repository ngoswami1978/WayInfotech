Imports System.Xml

Partial Class SearchTest
    Inherits System.Web.UI.Page

#Region "Global variable Declaration"
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Const strSearch As String = "<MS_SEARCHTEST_INPUT><TR_COURSE_NAME></TR_COURSE_NAME> <TR_COURSELEVEL_ID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </MS_SEARCHTEST_INPUT>"
    Dim objeAAMS As New eAAMS
    Dim strErrorMsg As String
    Dim objfunction As New Ack_Functions
    Dim objXmltestOutput As XmlDocument
    Dim objXmltestInput As XmlDocument
    Dim objTraining As New AAMS.bizTraining.bzTraining
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

        
            lblError.Text = String.Empty
            Session("PageName") = Request.Url.ToString()
            'If (Session("AdminUser") <> "ADMIN") Then
            '    Response.Redirect("../AdminLogin.aspx")
            'End If
            CheckSecurity()
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlLevel, "COURSELEVEL", True, 3)
                Session("title") = String.Empty
                Session("title") = " AAMS:: Search Test "
            End If
            txtTestName.Focus()
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
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    BtnSearch.Enabled = False

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

    Protected Sub GrdTestDetails_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdTestDetails.RowCommand
        Try

            If e.CommandName = "EditX" Then
                ' If objfunction.CheckSecurity(5, 1, CType(Session("SecurityXML"), XmlDocument)) Then
                Response.Redirect("ManageTest.aspx?Test_id=" & objED.Encrypt(e.CommandArgument.ToString), True)
                'Else
                '  lblError.Text = "You donot have sufficient rights."
                'End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        '  If objfunction.CheckSecurity(5, 1, CType(Session("SecurityXML"), XmlDocument)) Then
        SearchTest(PageOperation.Search)
        ' Else
        '  lblError.Text = "You donot have sufficient rights."
        '  End If

    End Sub

    Protected Sub GrdTestDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdTestDetails.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                e.Row.Cells(0).Text = CType(e.Row.RowIndex, Integer) + 1
            End If

            'Dim btnView As LinkButton
            'btnView = CType(e.Row.FindControl("BtnUpdate"), LinkButton)

            'Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Test Question']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Test Question']").Attributes("Value").Value)
            '    End If

            '    If strBuilder(1) = "0" Then
            '        btnView.Enabled = False
            '    Else
            '        btnView.Enabled = True
            '    End If
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub

    Protected Sub BindControls()
        Try
            objfunction.BindGrid(GrdTestDetails, objXmltestOutput, "TEST")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub SearchTest(ByVal Operation As Integer)
        Try
            objXmltestOutput = New XmlDocument
            objXmltestInput = New XmlDocument
            Dim ds As New DataSet
            Dim objXmlReader As XmlNodeReader
            objXmltestInput.LoadXml(strSearch)
            '<MS_SEARCHTEST_INPUT><TR_COURSE_NAME></TR_COURSE_NAME><TR_COURSELEVEL_ID /> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </MS_SEARCHTEST_INPUT>
            objXmltestInput.DocumentElement.SelectSingleNode("TR_COURSE_NAME").InnerText = Trim(txtTestName.Text)
            objXmltestInput.DocumentElement.SelectSingleNode("TR_COURSELEVEL_ID").InnerText = ddlLevel.SelectedValue

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then
                If ViewState("PrevSearching") Is Nothing Then
                    objXmltestInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objXmltestInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objXmltestInput.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objXmltestInput.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objXmltestInput.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "COURSE_NAME"
                    objXmltestInput.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "COURSE_NAME" '"LOCATION_CODE"
                Else
                    objXmltestInput.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objXmltestInput.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objXmltestInput.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If

            'End Code for paging and sorting



            objXmltestOutput = objTraining.Search(objXmltestInput)

            If objXmltestOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Operation = PageOperation.Export Then
                    Export(objXmltestOutput)
                    Exit Sub
                End If
                ViewState("PrevSearching") = objXmltestInput.OuterXml
                objXmlReader = New XmlNodeReader(objXmltestOutput)
                ds.ReadXml(objXmlReader)
                GrdTestDetails.DataSource = ds.Tables("TEST").DefaultView
                GrdTestDetails.DataBind()



                pnlPaging.Visible = True
                Dim count As Integer = CInt(objXmltestOutput.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
                txtTotalRecordCount.Text = objXmltestOutput.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "COURSE_NAME"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                GrdTestDetails.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                GrdTestDetails.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "TOTAL_MARKS"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                GrdTestDetails.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                GrdTestDetails.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                'End Select

                SetImageForSorting(GrdTestDetails)

            Else
                txtTotalRecordCount.Text = "0"
                'txtRecordOnCurrentPage.Text = "0"
                GrdTestDetails.DataSource = Nothing
                GrdTestDetails.DataBind()
                lblError.Text = objXmltestOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False

            End If


            'objXmltestOutput.Load("c:\searcht.xml")
            'strErrorMsg = objfunction.CheckError(objXmltestOutput)
            'If strErrorMsg <> String.Empty Then
            '    lblError.Text = strErrorMsg
            '    BindControls()
            '    Exit Sub
            'Else
            '    BindControls()
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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

#Region "Sorting and Paging by Mukund on 14th June"
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchTest(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchTest(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchTest(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub GrdTestDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdTestDetails.Sorting
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
            SearchTest(PageOperation.Search)
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchTest(PageOperation.Export)
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
        Dim strArray() As String = {"Course Name", "Total Marks"}
        Dim intArray() As Integer = {1, 3}
        objExport.ExportDetails(objOutputXml, "TEST", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportTEST.xls")

    End Sub
    'End Code For Export

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("searchtest.aspx")
    End Sub
End Class
