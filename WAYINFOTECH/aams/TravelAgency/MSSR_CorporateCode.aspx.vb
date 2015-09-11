'#############################################################
'############   Page Name -- Order_MSSR_CorporateCode  #######
'############   Date 29-November 2007  ########################
'############   Developed By Mukund #######################
'#############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class Order_MSSR_CorporateCode
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

#Region "Form Load Code Section"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes

            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString() ' "TravelAgency/MSSR_CorporateCode.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            End If
            If Not Page.IsPostBack Then
                '   btnReset.Attributes.Add("onclick", "return CorporateCodeReset();")
                btnNew.Attributes.Add("onclick", "return NewFunction();")

                ' Code for Delete Start.
                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                '        CorporateCodeDelete(Request.QueryString("CorporateRowID").ToString())
                '    End If
                'End If
                ' Code for Delete End.
            End If

            ' Code for Delete Start.
            If (hdCorporateCodeID.Value <> "") Then
                CorporateCodeDelete(hdCorporateCodeID.Value)
            End If
            ' Code for Delete End.

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Search Button Click Events Code Section"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Try
                lblError.Text = ""
                CorporateCodeSearch(PageOperation.Search)
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Corporate Search Function"
    Private Sub CorporateCodeSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes
            objInputXml.LoadXml("<MS_SEARCHOFFICEIDQUALIFIERS_INPUT><Code /><Qualifier /><Description /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHOFFICEIDQUALIFIERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Code").InnerText = txtCorporateCode.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("Qualifier").InnerText = txtCorporateQualifier.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("Description").InnerText = txtDescription.Text.Trim()

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
                    ViewState("SortName") = "Code"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Code" '"LOCATION_CODE"
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
            objOutputXml = objtaCorporateCodes.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value().ToString().ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvCorporateCode.DataSource = ds.Tables("Qualifiers").DefaultView
                gvCorporateCode.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("Qualifiers").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'
                SetImageForSorting(gvCorporateCode)
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "Code"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvCorporateCode.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvCorporateCode.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                '    Case "Qualifier"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvCorporateCode.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvCorporateCode.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "Description"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvCorporateCode.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvCorporateCode.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                'End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                gvCorporateCode.DataSource = Nothing
                gvCorporateCode.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Gridview RowDataBound Events"
    Protected Sub gvCorporateCode_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCorporateCode.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim lnkSelect As LinkButton
            Dim hdSelect As HiddenField
            hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If
            Dim hdorderStaus As HiddenField
            Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            lnkEdit = e.Row.FindControl("linkEdit")
            Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            lnkDelete = e.Row.FindControl("linkDelete")
            hdorderStaus = e.Row.FindControl("rowIDHidden")
            If strBuilder(3) = "0" Then
                lnkDelete.Disabled = True
            Else
                If (Request.QueryString("PopUp")) Is Nothing Then
                    lnkDelete.Attributes.Add("OnClick", "return DeleteFunction(" & hdorderStaus.Value & ");")
                Else
                    lnkDelete.Attributes.Add("OnClick", "return DeleteFunction2(" & hdorderStaus.Value & ");")
                End If

                ' lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdorderStaus.Value & ");")
            End If

            'If strBuilder(2) = "0" Then
            '    lnkEdit.Disabled = True
            'Else
            '    lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdorderStaus.Value & ");")
            'End If
            lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdorderStaus.Value) & "');")
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Delete Function Definition"
    Sub CorporateCodeDelete(ByVal strCityID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes
            objInputXml.LoadXml(" <MS_DELETEOFFICEIDQUALIFIERS_INPUT><RowID /></MS_DELETEOFFICEIDQUALIFIERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("RowID").InnerXml = strCityID
            'Here Back end Method Call
            hdCorporateCodeID.Value = ""
            objOutputXml = objtaCorporateCodes.Delete(objInputXml)
            CorporateCodeSearch(PageOperation.Search)
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



#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            CorporateCodeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            CorporateCodeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            CorporateCodeSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCorporateCode_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCorporateCode.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvCorporateCode_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCorporateCode.Sorting
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
            CorporateCodeSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            CorporateCodeSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Corporate Code", "Qualifier", "Description"}
        Dim intArray() As Integer = {1, 2, 3}

        objExport.ExportDetails(objOutputXml, "Qualifiers", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCorporateCode.xls")
    End Sub
    'End Code For Export

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.OriginalString(), False)
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
End Class
