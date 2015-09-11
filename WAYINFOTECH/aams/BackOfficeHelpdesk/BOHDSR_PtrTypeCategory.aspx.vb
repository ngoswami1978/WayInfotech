Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class BOHelpDesk_HDSR_PtrTypeCategory
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim imgDown As New Image
    Dim imgUp As New Image
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Session("PageName") = "HelpDesk/HDSR_PtrTypeCategory.aspx"
            objeAAMS.ExpirePageCache()

            btnNew.Attributes.Add("onclick", "return InsertPtrTypeCat();")

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Type Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Type Category']").Attributes("Value").Value)
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


            'If Not Page.IsPostBack Then

            'If Request.Form("txtPtrTypeCat") <> "" Then
            '    Dim strvalu As String = Request.Form("txtPtrTypeCat").Trim()
            'End If
            'Dim context As HttpContext = HttpContext.Current
            'If context.Items("strFirstName") IsNot Nothing Then
            '    txtPtrTypeCat.Text = context.Items("strFirstName")
            'End If

            '*****************Delete Functionality
            'If Request.QueryString("Action") IsNot Nothing Then
            '    If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
            '        DeletePtrType_Cat(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
            '        txtPtrTypeCat.Text = Session("SearchValu").ToString()
            '        HDPtrTypeSearch()
            '        ' lblError.Text = objeAAMSMessage.messDelete
            '    Else
            '    End If
            'End If
            '*****************End of Delete 
            '  End If

            If hdDelete.Value <> "" Then
                DeletePtrType_Cat(hdDelete.Value)
                hdDelete.Value = ""
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                HDPtrTypeSearch()
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            'Dim context As HttpContext = HttpContext.Current
            'context.Items.Add("strFirstName", txtPtrTypeCat.Text)
            Session("SearchValu") = txtPtrTypeCat.Text.Trim()
            HDPtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DeletePtrType_Cat(ByVal ptrTypeCatID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objhdPtrTypeCat As New AAMS.bizBOHelpDesk.bzPTRTypeCategory
            objInputXml.LoadXml("<HD_DELETEPTR_CATEGORIES_INPUT><HD_PTR_TYCAT_ID></HD_PTR_TYCAT_ID></HD_DELETEPTR_CATEGORIES_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYCAT_ID").InnerText = ptrTypeCatID
            'Call a function
            objOutputXml = objhdPtrTypeCat.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub HDPtrTypeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objhdPtrCat As New AAMS.bizBOHelpDesk.bzPTRTypeCategory

            objInputXml.LoadXml("<HD_SEARCHPTR_SEVERITY_INPUT> <HD_PTR_TYCAT_NAME></HD_PTR_TYCAT_NAME> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHPTR_SEVERITY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYCAT_NAME").InnerText = txtPtrTypeCat.Text.Trim()

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
            objOutputXml = objhdPtrCat.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)


                ViewState("PrevSearching") = objInputXml.OuterXml
                ' lblError.Text = ""
                grdvPtrTypeCat.DataSource = ds.Tables("PTR_CATEGORIES").DefaultView
                grdvPtrTypeCat.DataBind()
                txtRecordOnCurrReco.Text = grdvPtrTypeCat.Rows.Count.ToString
                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvPtrTypeCat)
                If ViewState("Desc") = "FALSE" Then
                    grdvPtrTypeCat.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvPtrTypeCat.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                'lblError.Text = ""
            Else
                txtRecordOnCurrReco.Text = "0"
                pnlPaging.Visible = False
                grdvPtrTypeCat.DataSource = Nothing
                grdvPtrTypeCat.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvPtrTypeCat_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvPtrTypeCat.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdTypeCatID As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As LinkButton
        linkDelete = e.Row.FindControl("linkDelete")
        hdTypeCatID = e.Row.FindControl("hdField")

        ' Dim chkbox As New CheckBox


        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Type Category']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Type Category']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Enabled = False
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdTypeCatID.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Disabled = True
            'Else
            '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdTypeCatID.Value & "');")
            'End If
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdTypeCatID.Value.Trim) & "');")
        Else
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdTypeCatID.Value.Trim) & "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdTypeCatID.Value & "');")
        End If
        'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdTypeCatID.Value & "');")
        'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdTypeCatID.Value & "');")
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            HDPtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            HDPtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            HDPtrTypeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvPtrTypeCat_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvPtrTypeCat.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
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
        HDPtrTypeSearch()
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
        Dim intArray(0) As Integer
        Dim strArray(0) As String
        Try
            Dim objhdPtrCat As New AAMS.bizBOHelpDesk.bzPTRTypeCategory

            objInputXml.LoadXml("<HD_SEARCHPTR_SEVERITY_INPUT> <HD_PTR_TYCAT_NAME></HD_PTR_TYCAT_NAME> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHPTR_SEVERITY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYCAT_NAME").InnerText = txtPtrTypeCat.Text.Trim()

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
            objOutputXml = objhdPtrCat.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)


                ViewState("PrevSearching") = objInputXml.OuterXml
                ' lblError.Text = ""
                grdvPtrTypeCat.DataSource = ds.Tables("PTR_CATEGORIES").DefaultView
                grdvPtrTypeCat.DataBind()
                txtRecordOnCurrReco.Text = grdvPtrTypeCat.Rows.Count.ToString

                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvPtrTypeCat)
                If ViewState("Desc") = "FALSE" Then
                    grdvPtrTypeCat.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvPtrTypeCat.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                intArray(0) = 1
                strArray(0) = "PTR Type Category"

                objExport.ExportDetails(objOutputXml, "PTR_CATEGORIES", intArray, strArray, ExportExcel.ExportFormat.Excel, "Ptr.xls")


                'lblError.Text = ""
            Else

                grdvPtrTypeCat.DataSource = Nothing
                grdvPtrTypeCat.DataBind()
                txtRecordOnCurrReco.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
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
