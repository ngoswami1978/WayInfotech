
Partial Class Training_TRSR_TrainingRooms
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
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
            Session("PageName") = Request.Url.ToString()
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            CheckSecurity()

            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            If Not IsPostBack Then

                objeAAMS.BindDropDown(ddlAOffice, "AOFFICE", True, 3)
                '   Checking Permission For Own Office and Region start.
                If objeAAMS.Limited_To_Aoffice(Session("Security")) <> "" Then
                    ddlAOffice.SelectedValue = objeAAMS.Limited_To_Aoffice(Session("Security"))
                    ddlAOffice.Enabled = False
                End If
                '   Checking Permission For Own Office and Region end.

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub CheckSecurity()
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Attributes("Value").Value)
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
            Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
            objInputXml.LoadXml("<TR_DELETETRAININGROOM_INPUT><TR_CLOCATION_ID /></TR_DELETETRAININGROOM_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_CLOCATION_ID").InnerText = hdDeleteId.Value
            hdDeleteId.Value = ""
            objOutputXml = objbzTrainingRoom.Delete(objInputXml)
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
        Response.Redirect("TRUP_TrainingRooms.aspx?Action=I")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TRSR_TrainingRooms.aspx")
    End Sub
    Sub SearchRecords(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzTrainingRoom As New AAMS.bizTraining.bzTrainingRoom
        objInputXml.LoadXml("<TR_SEARCHTRAININGROOM_INPUT><AOFFICE /><LOCATION_NAME /><REGION/><SECREGIONID/><Limited_To_Aoffice/><Limited_To_Region/><Limited_To_OwnAagency/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TR_SEARCHTRAININGROOM_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = ddlAOffice.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("LOCATION_NAME").InnerText = txtName.Text
        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = ""
        objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objeAAMS.Limited_To_Region(Session("Security"))

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
                ViewState("SortName") = "AOFFICE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AOFFICE" '"LOCATION_CODE"
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
        objOutputXml = objbzTrainingRoom.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvTrainingRooms.DataSource = ds.Tables("DETAILS")
            gvTrainingRooms.DataBind()
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

            ' 
            ' @ Added Code To Show Image'
            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "AOFFICE"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvTrainingRooms.HeaderRow.Cells(0).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvTrainingRooms.HeaderRow.Cells(0).Controls.Add(imgDown)
            '        End Select
            '    Case "CITY"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvTrainingRooms.HeaderRow.Cells(1).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvTrainingRooms.HeaderRow.Cells(1).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_CLOCATION_NAME"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvTrainingRooms.HeaderRow.Cells(2).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvTrainingRooms.HeaderRow.Cells(2).Controls.Add(imgDown)
            '        End Select
            '    Case "TR_CLOCATION_MAXNBPART"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                gvTrainingRooms.HeaderRow.Cells(3).Controls.Add(imgUp)
            '            Case "TRUE"
            '                gvTrainingRooms.HeaderRow.Cells(3).Controls.Add(imgDown)
            '        End Select
            'End Select

            SetImageForSorting(gvTrainingRooms)
            ''  Added Code To Show Image'

            ' End of Code Added For Paging And Sorting In case Of Delete The Record
            lblError.Text = ""
        Else
            gvTrainingRooms.DataSource = Nothing
            gvTrainingRooms.DataBind()
            txtTotalRecordCount.Text = "0"
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

    Protected Sub gvTrainingRooms_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTrainingRooms.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim hdLocation_ID As HiddenField
        Dim btnEdit As LinkButton
        Dim btnDelete As LinkButton
        Dim btnSelect As LinkButton
        btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        btnSelect = CType(e.Row.FindControl("lnkSelect"), LinkButton)
        hdLocation_ID = CType(e.Row.FindControl("hdLocation_ID"), HiddenField)


        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Training Room']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                btnDelete.Enabled = False
            Else
                btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdLocation_ID.Value + "');")
            End If
            'If strBuilder(2) = "0" Then
            '    btnEdit.Enabled = False
            'Else
            '    btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + hdLocation_ID.Value + "');")
            'End If

            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdLocation_ID.Value) + "');")

        Else
            btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdLocation_ID.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdLocation_ID.Value) + "');")

        End If

        If Not Request.QueryString("Popup") Is Nothing Then
            btnSelect.Attributes.Add("OnClick", "javascript:return SelectFunction('" + hdLocation_ID.Value + "','" + DataBinder.Eval(e.Row.DataItem, "TR_CLOCATION_NAME").ToString.Replace(vbCrLf, "") + "','" + DataBinder.Eval(e.Row.DataItem, "TR_CLOCATION_MAXNBPART") + "');")
        Else
            btnSelect.Visible = False
        End If

    End Sub

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

    Protected Sub gvTrainingRooms_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTrainingRooms.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvTrainingRooms_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvTrainingRooms.Sorting
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
        Dim strArray() As String = {"AOffice", "City", "Location", "MAX NB PART"}
        Dim intArray() As Integer = {1, 6, 2, 3}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportTRAININGROOM.xls")
    End Sub
    'End Code For Export
End Class
