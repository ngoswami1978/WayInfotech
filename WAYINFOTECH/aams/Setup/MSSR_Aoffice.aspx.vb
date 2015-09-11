Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Setup_MSSR_1Aoffice
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Session("PageName") = "Setup/MSSR_Aoffice.aspx"
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            btnReset.Attributes.Add("onclick", "return AofficeReset();")
            'objeAAMS.ExpirePageCache()
            lblError.Text = String.Empty
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Aoffice']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Aoffice']").Attributes("Value").Value)
            End If
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
            End If
            If strBuilder(4) = "0" Then
                btnExport.Enabled = False
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
                btnSearch.Enabled = False
            End If
            If Not Page.IsPostBack Then
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtAoffice.Text = Session("Action").ToString().Split("|").GetValue(2)
                        AofficeSearch()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
           

            '   Deleting records.
            If (hdAOfficeID.Value <> "") Then
                AofficeDelete(hdAOfficeID.Value)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
   
    '**************************************************************************************************
    '****************************Code for search Aoffice **********************************************
    '**************************************************************************************************
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AofficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    'Method for search Aoffice
    Private Sub AofficeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice

        objInputXml.LoadXml("<MS_SEARCHAOFFICE_INPUT><Aoffice></Aoffice> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAOFFICE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Trim(txtAoffice.Text)
        'Here Back end Method Call



        'Code for Paging and Sorting
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
            ViewState("SortName") = "Aoffice"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Aoffice" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'End of Paging and Sorting 

        objOutputXml = objbzAOffice.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            
            lblError.Text = ""
            ViewState("PrevSearching") = objInputXml.OuterXml

            grdAoffice.DataSource = ds.Tables("AOFFICE")
            grdAoffice.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(grdAoffice)
            If ViewState("Desc") = "FALSE" Then
                grdAoffice.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                grdAoffice.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            txtTotalRecordCount.Text = "0"
            'hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            grdAoffice.DataSource = Nothing
            grdAoffice.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Private Sub AofficeDelete(ByVal strAoffice As String)
        Dim objInputXml As New XmlDocument
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
        objInputXml.LoadXml("<MS_DELETEAOFFICE_INPUT><Aoffice></Aoffice></MS_DELETEAOFFICE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = strAoffice
        'Here Back end Method Call
        hdAOfficeID.Value = ""
        objInputXml = objbzAOffice.Delete(objInputXml)
        AofficeSearch()
        If objInputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ' Session("Action") = Request.QueryString("Action")
            'Response.Redirect("MSSR_Aoffice.aspx")
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objInputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    'Protected Sub grdAoffice_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdAoffice.ItemDataBound
    '    If e.Item.ItemIndex < 0 Then
    '        Exit Sub
    '    End If

    '    Dim btnDelete, btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
    '    Dim lblAoffice As Label
    '    btnDelete = CType(e.Item.FindControl("btnDelete"), System.Web.UI.HtmlControls.HtmlAnchor)
    '    btnEdit = CType(e.Item.FindControl("btnEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
    '    lblAoffice = CType(e.Item.FindControl("lblAoffice"), Label)

    '    If strBuilder(3) = "0" Then
    '        btnDelete.Disabled = True
    '    Else
    '        btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & lblAoffice.Text & "');")
    '    End If
    '    If strBuilder(2) = "0" Then
    '        btnEdit.Disabled = True
    '    Else
    '        btnEdit.Attributes.Add("OnClick", "return EditFunction('" & lblAoffice.Text & "');")
    '    End If
    'End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.OriginalString)

        'txtAoffice.Text = String.Empty
        'grdAoffice.DataSource = Nothing
        'grdAoffice.DataBind()
    End Sub

    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_Aoffice.aspx?Action=I")
    'End Sub

    'Protected Sub grdAoffice_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdAoffice.SortCommand
    '    Try
    '        Dim sortName As String = e.SortExpression
    '        SortCall(sortName)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub
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
        AofficeSearch()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            AofficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            AofficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            AofficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
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

    Protected Sub grdAoffice_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAoffice.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim btnDelete, btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lblAoffice As Label
        btnDelete = CType(e.Row.FindControl("btnDelete"), System.Web.UI.HtmlControls.HtmlAnchor)
        btnEdit = CType(e.Row.FindControl("btnEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
        lblAoffice = CType(e.Row.FindControl("lblAoffice"), Label)

        If strBuilder(3) = "0" Then
            btnDelete.Disabled = True
        Else
            btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & lblAoffice.Text & "');")
        End If
        'If strBuilder(2) = "0" Then
        '    btnEdit.Disabled = False
        'Else
        btnEdit.Attributes.Add("OnClick", "return EditFunction('" & objEn.Encrypt(lblAoffice.Text.Trim) & "');")
        'End If
    End Sub

    Protected Sub grdAoffice_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAoffice.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
        Dim objExport As New ExportExcel
        Dim intArray(2) As Integer
        Dim strArray(2) As String

        objInputXml.LoadXml("<MS_SEARCHAOFFICE_INPUT><Aoffice></Aoffice> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAOFFICE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Trim(txtAoffice.Text)
        'Here Back end Method Call



        'Code for Paging and Sorting
        

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "Aoffice"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Aoffice" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'End of Paging and Sorting 

        objOutputXml = objbzAOffice.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
            grdAoffice.DataSource = ds.Tables("AOFFICE")
            grdAoffice.DataBind()
        Else
            grdAoffice.DataSource = String.Empty
            grdAoffice.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        intArray(0) = 0
        strArray(0) = "Aoffice"

        intArray(1) = 1
        strArray(1) = "City Name"

        intArray(2) = 2
        strArray(2) = "Country Name"

       

        objExport.ExportDetails(objOutputXml, "AOFFICE", intArray, strArray, ExportExcel.ExportFormat.Excel, "Aoffice.xls")
    End Sub
End Class
