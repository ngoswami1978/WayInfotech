
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class HelpDesk_HDSR_PTRType
    Inherits System.Web.UI.Page
#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim imgDown As New Image
    Dim imgUp As New Image
    Dim objED As New EncyrptDeCyrpt
#End Region
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString() '"HelpDesk/HDSR_PTRTYPE.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            btnSearch.Attributes.Add("onclick", "return PtrTypeMandatory();")
            ' btnReset.Attributes.Add("onclick", "return PtrTypeReset();")
            btnNew.Attributes.Add("onclick", "return NewHDUPPtrType();")
            drpCatName.Attributes.Add("onkeyup", "return gotop('drpCatName')")

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCatName, "HDPTRTYPECAT", True, 3)
                'If Not Session("Act") Is Nothing Then
                '    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then

                '        drpCatName.SelectedValue = Session("Act").ToString().Split("|").GetValue(2)
                '        txtType.Text = Session("Act").ToString().Split("|").GetValue(3)

                '        PtrTypeSearch()
                '        lblError.Text = objeAAMSMessage.messDelete
                '        Session("Act") = Nothing
                '    End If
                'End If
            End If
            ' If Not Request.QueryString("Action") Is Nothing Then
            If hdDelete.Value <> "" Then
                PtrTypeDelete(hdDelete.Value)
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                PtrTypeSearch()
                hdDelete.Value = ""
            End If
            'If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '    PtrTypeDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            'End If
            ' End If

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Attributes("Value").Value)
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
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region    'End Sub
#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            PtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search Request Type"
    Private Sub PtrTypeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzPTRType As New AAMS.bizHelpDesk.bzPTRType

            objInputXml.LoadXml("<HD_SEARCHPTR_TYPE_INPUT><HD_PTR_TYPE_NAME></HD_PTR_TYPE_NAME><HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHPTR_TYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_NAME").InnerText = txtType.Text
            If drpCatName.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYCAT_ID").InnerText = drpCatName.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_NAME").InnerText = txtType.Text
            'Start CODE for sorting and paging



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
                ViewState("SortName") = "HD_PTR_TYCAT_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_PTR_TYCAT_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If



            'End Code for paging and sorting

            'Here Back end Method Call

            objOutputXml = objbzPTRType.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
               

                ViewState("PrevSearching") = objInputXml.OuterXml
                ' lblError.Text = ""
                gvPtrType.DataSource = ds.Tables("PTR_TYPE")
                gvPtrType.DataBind()
                txtRecordOnCurrReco.Text = gvPtrType.Rows.Count.ToString
                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(gvPtrType)
                If ViewState("Desc") = "FALSE" Then
                    gvPtrType.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    gvPtrType.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            Else
                gvPtrType.DataSource = Nothing
                gvPtrType.DataBind()
                txtRecordOnCurrReco.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " Sub Procedure Called for deletion of PTRType"
    Sub PtrTypeDelete(ByVal strPtrTypeId As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzPTRType As New AAMS.bizHelpDesk.bzPTRType
            objInputXml.LoadXml("<HD_DELETEPTR_TYPE_INPUT><HD_PTR_TYPE_ID></HD_PTR_TYPE_ID></HD_DELETEPTR_TYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_ID").InnerText = strPtrTypeId
            'Here Back end Method Call

            objOutputXml = objbzPTRType.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                'Session("Act") = Request.QueryString("Action")
                'Response.Redirect("HDSR_PTRType.aspx", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gvPtrType_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvPtrType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPtrType.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrHDHDPTRTYPEID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrHDHDPTRTYPEID = e.Row.FindControl("HDHDPTRTYPEID")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdstrHDHDPTRTYPEID.Value & "');")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrHDHDPTRTYPEID.Value & ");")
                    'End If
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdstrHDHDPTRTYPEID.Value.Trim) & "');")
                Else
                End If
            Else
                linkDelete.Enabled = True
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdstrHDHDPTRTYPEID.Value.Trim) & "');")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdstrHDHDPTRTYPEID.Value & "');")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub gvPtrType_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPtrType.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            PtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            PtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            PtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

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
        PtrTypeSearch()
    End Sub
#End Region

    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objExport As New ExportExcel
        Dim intArray(1) As Integer
        Dim strArray(1) As String
        Try
            Dim objbzPTRType As New AAMS.bizHelpDesk.bzPTRType

            objInputXml.LoadXml("<HD_SEARCHPTR_TYPE_INPUT><HD_PTR_TYPE_NAME></HD_PTR_TYPE_NAME><HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHPTR_TYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_NAME").InnerText = txtType.Text
            If drpCatName.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYCAT_ID").InnerText = drpCatName.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_NAME").InnerText = txtType.Text
            'Start CODE for sorting and paging


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "HD_PTR_TYCAT_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "HD_PTR_TYCAT_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If



            'End Code for paging and sorting

            'Here Back end Method Call

            objOutputXml = objbzPTRType.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)


                ViewState("PrevSearching") = objInputXml.OuterXml
                ' lblError.Text = ""
                gvPtrType.DataSource = ds.Tables("PTR_TYPE")
                gvPtrType.DataBind()
                txtRecordOnCurrReco.Text = gvPtrType.Rows.Count.ToString
                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(gvPtrType)
                If ViewState("Desc") = "FALSE" Then
                    gvPtrType.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    gvPtrType.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
                intArray(0) = 3
                strArray(0) = "Category"

                intArray(1) = 1
                strArray(1) = "PTR Type"

                objExport.ExportDetails(objOutputXml, "PTR_TYPE", intArray, strArray, ExportExcel.ExportFormat.Excel, "Ptr.xls")

            Else
                gvPtrType.DataSource = Nothing
                gvPtrType.DataBind()
                txtRecordOnCurrReco.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("HDSR_PTRType.aspx")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
