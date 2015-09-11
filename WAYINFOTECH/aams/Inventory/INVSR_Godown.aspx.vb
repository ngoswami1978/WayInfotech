Imports System.Data
Imports System.Xml
Partial Class Inventory_Inv_Godown
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Inventory/INVSR_Godown.aspx"
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
                btnNew.Attributes.Add("onclick", "return NewFunction();")

                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                '        GodownDelete(Request.QueryString("GODOWNID").ToString())
                '    End If
                'End If
            End If
            If (hdID.Value <> "") Then
                DeleteRecords()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    ' Sub GodownDelete(ByVal strGodownID As String)
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtFeasibilityStatus As New AAMS.bizInventory.bzGodown
            objInputXml.LoadXml("<IN_DELETEGODOWN_INPUT><GODOWNID /></IN_DELETEGODOWN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerXml = hdID.Value 'strGodownID
            hdID.Value = ""
            'Here Back end Method Call
            objOutputXml = objtFeasibilityStatus.Delete(objInputXml)
            GodownSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                'txtGodownName.Text = Request.QueryString("GodownName").Trim()
                'GodownSearch(PageOperation.Search)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GodownSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objSecurityXml As New XmlDocument
        Dim ds As New DataSet
        Dim objbzGodown As New AAMS.bizInventory.bzGodown
        Try
            objInputXml.LoadXml("<INV_SEARCHGODOWN_INPUT><GODOWNNAME /><CITYID /><Limited_To_Region /> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHGODOWN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GODOWNNAME").InnerText = Trim(txtGodownName.Text)
            objInputXml.DocumentElement.SelectSingleNode("CITYID").InnerText = Trim(drpCity.SelectedValue)
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
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
                    ViewState("SortName") = "GODOWNNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "GODOWNNAME" '"LOCATION_CODE"
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

            objOutputXml = objbzGodown.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdGodown.DataSource = ds.Tables("GODOWN")
                grdGodown.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record
                If ds.Tables("GODOWN").Rows.Count <> 0 Then
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
                    hdRecordOnCurrentPage.Value = ds.Tables("GODOWN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString()
                        Case "GODOWNNAME"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdGodown.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdGodown.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "Address"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdGodown.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdGodown.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "City"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdGodown.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdGodown.HeaderRow.Cells(2).Controls.Add(imgDown)

                            End Select
                        Case "Phone"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdGodown.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdGodown.HeaderRow.Cells(3).Controls.Add(imgDown)

                            End Select
                        Case "PostalCode"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    grdGodown.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdGodown.HeaderRow.Cells(4).Controls.Add(imgDown)

                            End Select
                    End Select
                    '  Added Code To Show Image'
                Else
                    grdGodown.DataSource = Nothing
                    grdGodown.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If

            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                grdGodown.DataSource = String.Empty
                grdGodown.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            GodownSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdGodown_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGodown.RowDataBound
        Dim objSecurityXml As New XmlDocument

        Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        'Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor

        Dim lnkDelete As LinkButton

        Dim hdGodownId As New HiddenField
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                Dim str As String = ""
                Dim strAddress As String = ""
                strAddress = DataBinder.Eval(e.Row.DataItem, "Address").ToString()
                strAddress = Replace(strAddress, vbCrLf, "")
                str = DataBinder.Eval(e.Row.DataItem, "GODOWNID") + "|" + DataBinder.Eval(e.Row.DataItem, "GodownName") + "|" + strAddress 'DataBinder.Eval(e.Row.DataItem, "Address")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & str & "');")
            End If

            lnkEdit = e.Row.FindControl("linkEdit")
            lnkDelete = e.Row.FindControl("linkDelete")
            hdGodownId = e.Row.FindControl("hdGodownId")
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Attributes("Value").Value)
                End If

                If strBuilder(3) = "0" Then
                    lnkDelete.Enabled = False
                Else
                    lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdGodownId.Value & ");")
                End If

                'If strBuilder(2) = "0" Then
                '    lnkEdit.Disabled = True
                'Else
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdGodownId.Value) & "');")
                ' End If

            Else
                lnkDelete.Enabled = True
                lnkEdit.Disabled = False
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdGodownId.Value) & "');")
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdGodownId.Value & ");")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GodownSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GodownSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GodownSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub grdGodown_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGodown.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdGodown_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGodown.Sorting
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
            GodownSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            GodownSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Godown Name", "Address", "City", "Phone", "Postal Code"}
        Dim intArray() As Integer = {1, 2, 5, 3, 4}
        objExport.ExportDetails(objOutputXml, "GODOWN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportGODOWN.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_Godown.aspx")
    End Sub
End Class
