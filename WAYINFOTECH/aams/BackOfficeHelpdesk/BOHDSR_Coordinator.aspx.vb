
Partial Class BOHelpDesk_MSSR_Coordinator
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtName.Focus()
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        'If Not Page.IsPostBack Then
        '    If Not Session("Action") Is Nothing Then
        '        If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
        '            txtSubgroupName = Session("Action").ToString().Split("|").GetValue(1)
        '            SubGroupSearch()
        '            lblError.Text = objeAAMSMessage.messDelete
        '            Session("Action") = Nothing
        '        End If
        '    End If
        'End If
        '***************************************************************************************
        Try
            txtName.Attributes.Add("onkeypress", "return allTextWithSpace();")
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
            End If
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            ' Checking security
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Coordinator']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Coordinator']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("../NoRights.aspx")

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


            'Delete Coordinator
            If hdID.Value <> "" Then
                CoordinatorDelete(hdID.Value.Split("|").GetValue(1), hdID.Value.Split("|").GetValue(2), hdID.Value.Split("|").GetValue(3))
            End If

            '   Checking Permission For Own Office start.
            If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                ddlAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                ddlAOffice.Enabled = False
            End If

            '   Checking Permission For Own Office end.
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("BOHDUP_Coordinator.aspx?Action=I")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            CoordinatorSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("BOHDSR_Coordinator.aspx")
    End Sub

    Sub CoordinatorSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCoordinator As New AAMS.bizBOHelpDesk.bzCoordinator
        objInputXml.LoadXml("<HD_SEARCHCOORDINATOR_INPUT><COORDINATOR_TYPE /><EMPLOYEE_NAME /><Aoffice /> <Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCOORDINATOR_INPUT>")
        If rbCoordinator1.Checked = True Then
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "1"
        End If

        If rbCoordinator2.Checked = True Then
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = "2"
        End If
        If rbCoordinator1.Checked = False And rbCoordinator2.Checked = False Then
            objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerText = ""
        End If

        objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_NAME").InnerText = txtName.Text
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ddlAOffice.SelectedValue

        ' Security Input Xml Start.
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = objeAAMS.Limited_To_OwnAgency(Session("Security"))
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
                ViewState("SortName") = "AOffice"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AOffice" '"LOCATION_CODE"
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
        'Here Back end Method Call
        objOutputXml = objbzCoordinator.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvCoordinator.DataSource = ds.Tables("COORDINATOR")
            gvCoordinator.DataBind()
            lblError.Text = ""
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
            hdRecordOnCurrentPage.Value = ds.Tables("COORDINATOR").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'
            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Select Case ViewState("SortName")
                Case "Aoffice"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvCoordinator.HeaderRow.Cells(0).Controls.Add(imgUp)
                        Case "TRUE"
                            gvCoordinator.HeaderRow.Cells(0).Controls.Add(imgDown)
                    End Select
                Case "EMPLOYEE_NAME"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvCoordinator.HeaderRow.Cells(1).Controls.Add(imgUp)
                        Case "TRUE"
                            gvCoordinator.HeaderRow.Cells(1).Controls.Add(imgDown)
                    End Select
                Case "COORDINATOR_TYPE"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvCoordinator.HeaderRow.Cells(2).Controls.Add(imgUp)
                        Case "TRUE"
                            gvCoordinator.HeaderRow.Cells(2).Controls.Add(imgDown)

                    End Select
            End Select
            '  Added Code To Show Image'

            ' End of Code Added For Paging And Sorting In case Of Delete The Record
        Else
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            gvCoordinator.DataSource = Nothing
            gvCoordinator.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Sub CoordinatorDelete(ByVal AOffice As String, ByVal CoordinatorType As String, ByVal EmployeeID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCoordinator As New AAMS.bizBOHelpDesk.bzCoordinator
        objInputXml.LoadXml("<HD_DELETECOORDINATOR_INPUT><COORDINATOR_TYPE /><EmployeeID /><Aoffice /></HD_DELETECOORDINATOR_INPUT> ")
        objInputXml.DocumentElement.SelectSingleNode("COORDINATOR_TYPE").InnerXml = CoordinatorType
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = EmployeeID
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = AOffice
        'Here Back end Method Call
        objOutputXml = objbzCoordinator.Delete(objInputXml)
        hdID.Value = ""
        CoordinatorSearch(PageOperation.Search)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Protected Sub gvCoordinator_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCoordinator.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdID, hdID1, hdID2 As HiddenField
        Dim linkEdit As New LinkButton
        linkEdit = e.Row.FindControl("lnkEdit")
        Dim linkDelete As New LinkButton
        linkDelete = e.Row.FindControl("lnkDelete")
        hdID = e.Row.FindControl("hdID")
        hdID1 = e.Row.FindControl("hdID1")
        hdID2 = e.Row.FindControl("hdID2")
        e.Row.Cells(2).Text = "Coordinator" + e.Row.Cells(2).Text
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Coordinator']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Coordinator']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Enabled = False
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdID.Value & "','" & hdID1.Value & "','" & hdID2.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Enabled = False
            'Else
            '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdID.Value & "','" & hdID1.Value & "');")
            'End If
            '  linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdID.Value & "','" & hdID1.Value & "');")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdID.Value) & "','" & objED.Encrypt(hdID1.Value) & "');")
        Else
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdID.Value) & "','" & objED.Encrypt(hdID1.Value) & "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdID.Value & "','" & hdID1.Value & "','" & hdID2.Value & "');")
        End If
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            CoordinatorSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            CoordinatorSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            CoordinatorSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvCoordinator_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCoordinator.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCoordinator_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCoordinator.Sorting
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
            CoordinatorSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            CoordinatorSearch(PageOperation.Export)
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
        Dim strArray() As String = {"AOffice", "Name", "Coordinator Type"}
        Dim intArray() As Integer = {2, 1, 3}
        objExport.ExportDetails(objOutputXml, "COORDINATOR", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCOORDINATOR.xls")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
