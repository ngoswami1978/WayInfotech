
Partial Class BirdresHelpDesk_HDSR_WorkOrderFollowUp
    Inherits System.Web.UI.Page
#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                txtFollowUp.Focus()

            End If


            ' Checking security.
            CheckSecurity()
            '  Deleting Records
            If (hdID.Value <> "") Then
                DeleteRecords()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("HDUP_WorkOrderFollowUp.aspx?Action=I")
    End Sub
#End Region

#Region "gvFollowUp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFollowUp.RowDataBound"
    Protected Sub gvFollowUp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFollowUp.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '************* Code for edit link ****************************************************************
            Dim hdFollowUpID As HiddenField
            Dim btnEdit As LinkButton
            hdFollowUpID = CType(e.Row.FindControl("hdID"), HiddenField)
            btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Followup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Followup']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdFollowUpID.Value + "');")
                End If
                'If strBuilder(2) = "0" Then
                '    btnEdit.Enabled = False
                'Else
                '    btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + hdFollowUpID.Value + "');")
                'End If
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdFollowUpID.Value) + "');")
            Else
                btnEdit.Attributes.Add("OnClick", "javascript:return Edit('" + objED.Encrypt(hdFollowUpID.Value) + "');")
                btnDelete.Attributes.Add("OnClick", "javascript:return Delete('" + hdFollowUpID.Value + "');")
            End If
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
        Dim objWOFollowUp As New AAMS.bizBRHelpDesk.bzWOFollowUp
        Try
            objInputXml.LoadXml("<HD_SEARCHWOFOLLOWUP_INPUT><WO_FOLLOWUP_NAME /><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHWOFOLLOWUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("WO_FOLLOWUP_NAME").InnerText = txtFollowUp.Text.Trim()


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
                    ViewState("SortName") = "WO_FOLLOWUP_NAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "WO_FOLLOWUP_NAME"
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

            objOutputXml = objWOFollowUp.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("WO_FOLLOWUP").Rows.Count <> 0 Then
                    gvFollowUp.DataSource = ds.Tables("WO_FOLLOWUP")
                    gvFollowUp.DataBind()
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
                    hdRecordOnCurrentPage.Value = ds.Tables("WO_FOLLOWUP").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' 
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "WO_FOLLOWUP_NAME"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    gvFollowUp.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvFollowUp.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                    End Select
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    gvFollowUp.DataSource = Nothing
                    gvFollowUp.DataBind()
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

#Region "DeleteRecords()"
    Private Sub DeleteRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objWOFollowUp As New AAMS.bizBRHelpDesk.bzWOFollowUp
        Try
            objInputXml.LoadXml("<HD_DELETEWOFOLLOWUP_INPUT><WO_FOLLOWUP_ID /></HD_DELETEWOFOLLOWUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("WO_FOLLOWUP_ID").InnerText = hdID.Value
            hdID.Value = ""
            objOutputXml = objWOFollowUp.Delete(objInputXml)
            BindData(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Followup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Followup']").Attributes("Value").Value)
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("HDSR_WorkOrderFollowUp.aspx")
    End Sub


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

    Protected Sub gvFollowUp_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvFollowUp.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvFollowUp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvFollowUp.Sorting
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
        Dim strArray() As String = {"FollowUp"}
        Dim intArray() As Integer = {1}

        objExport.ExportDetails(objOutputXml, "WO_FOLLOWUP", intArray, strArray, ExportExcel.ExportFormat.Excel, "WOFollowUp.xls")
    End Sub
    'End Code For Export

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
