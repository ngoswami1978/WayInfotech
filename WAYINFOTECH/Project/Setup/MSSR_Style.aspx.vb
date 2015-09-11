Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Setup_MS_Style
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt

#Region "Code for Filter "
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region

#Region "Code for Search Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "Code for Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            If Not Page.IsPostBack Then
                '// Write No Code Here
            End If

            Dim objSecurityXml As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Item Master']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Item Master']").Attributes("Value").Value)
                End If

                If strBuilder.Length = 0 Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False

                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '   Deleting records.
            If (hdDelete.Value <> "") Then
                StyleDelete(hdDelete.Value)
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrReco.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                StyleSearch()
                hdDelete.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Style Search"
    Private Sub StyleSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objStyle As New WAY.bizMaster.bzStyle

        objInputXml.LoadXml("<MS_SEARCHSTYLE_INPUT><BarcodeNo /><StyleName /><DesignNo /><ShadeNo /><MRP /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCHSTYLE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("BarcodeNo").InnerText = Trim(txtBarCode.Text)
        objInputXml.DocumentElement.SelectSingleNode("StyleName").InnerText = Trim(txtStyleName.Text)
        objInputXml.DocumentElement.SelectSingleNode("DesignNo").InnerText = Trim(txtDesignName.Text)
        objInputXml.DocumentElement.SelectSingleNode("ShadeNo").InnerText = Trim(txtShadeNo.Text)
        objInputXml.DocumentElement.SelectSingleNode("MRP").InnerText = Trim(txtMRP.Text)

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
            ViewState("SortName") = "StyleName"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "StyleName"
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

        'Here Back end Method Call
        objOutputXml = objStyle.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If hdDelete.Value.Trim = "" Then
                lblError.Text = ""
            End If

            ViewState("PrevSearching") = objInputXml.OuterXml

            dbgrdManageStyle.DataSource = ds.Tables("STYLE")
            dbgrdManageStyle.DataBind()

            txtRecordOnCurrReco.Text = dbgrdManageStyle.Rows.Count.ToString

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(dbgrdManageStyle)
            If ViewState("Desc") = "FALSE" Then
                dbgrdManageStyle.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                dbgrdManageStyle.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            dbgrdManageStyle.DataSource = Nothing
            dbgrdManageStyle.DataBind()
            txtTotalRecordCount.Text = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "Code for Delete Style"
    Sub StyleDelete(ByVal strW_StyleId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzStyle As New WAY.bizMaster.bzStyle
        objInputXml.LoadXml("<MS_DELETESTYLE_INPUT><W_StyleId></W_StyleId></MS_DELETESTYLE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("W_StyleId").InnerText = strW_StyleId
        objOutputXml = objbzStyle.Delete(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "Code for Grid Row Bound"
    Protected Sub dbgrdManageAirLineOffice_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdManageStyle.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim i As Integer
        ' Code added For Selecting an Items 
        Dim lnkSelect As LinkButton
        lnkSelect = e.Row.FindControl("lnkSelect")
        Dim str() As String = lnkSelect.CommandArgument.Split("|")
        For i = 0 To str.Length - 1
            If i = 3 Then
                str(i) = str(i).Replace(vbCrLf, "\n")
            End If
            If i = 0 Then
                lnkSelect.CommandArgument = str(i)
            Else
                lnkSelect.CommandArgument += "|" + str(i)
            End If
        Next
        Dim strcommand As String = lnkSelect.CommandArgument
        Dim BDRLetter, TempLateVersion As String
        BDRLetter = ""
        TempLateVersion = ""
        If (Request.QueryString("PopUp")) Is Nothing Then
            lnkSelect.Visible = False
        Else

            lnkSelect.Visible = True
            lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" + strcommand + "');")
        End If

        Dim hdW_StyleId As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        hdW_StyleId = e.Row.FindControl("hdW_StyleId")

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Item Master']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Item Master']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Disabled = True
            Else
                If (Request.QueryString("PopUp")) Is Nothing Then
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdW_StyleId.Value & "');")
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdW_StyleId.Value & "');")
                End If
            End If
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdW_StyleId.Value.Trim) & "');")
        Else
            linkDelete.Disabled = False
            linkEdit.Disabled = False
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdW_StyleId.Value.Trim) & "');")
            If (Request.QueryString("PopUp")) Is Nothing Then
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdW_StyleId.Value & "');")
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdW_StyleId.Value & "');")
            End If
        End If
    End Sub
#End Region

#Region "Code for Rreset"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.OriginalString)
    End Sub
#End Region
  

#Region "Code for paging"
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
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
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
#End Region

#Region "Code for Sort Column"
    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function
#End Region

#Region "Code for Sorting"
    Protected Sub dbgrdManageAirLineOffice_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdManageStyle.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

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
        StyleSearch()
    End Sub
#End Region

#Region "Code for Next and previous"
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Export into Excel"
    Protected Sub btnExport_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExport As New ExportExcel
        'intArray, strArray,
        Dim intArray(4) As Integer
        Dim strArray(4) As String
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzStyle As New WAY.bizMaster.bzStyle

        objInputXml.LoadXml("<MS_SEARCHSTYLE_INPUT><BarcodeNo /><StyleName /><DesignNo /><ShadeNo /><MRP /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCHSTYLE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("BarcodeNo").InnerText = Trim(txtBarCode.Text)
        objInputXml.DocumentElement.SelectSingleNode("StyleName").InnerText = Trim(txtStyleName.Text)
        objInputXml.DocumentElement.SelectSingleNode("DesignNo").InnerText = Trim(txtShadeNo.Text)
        objInputXml.DocumentElement.SelectSingleNode("ShadeNo").InnerText = Trim(txtShadeNo.Text)
        objInputXml.DocumentElement.SelectSingleNode("MRP").InnerText = Trim(txtMRP.Text)


        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "BarcodeNo"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BarcodeNo"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If


        objOutputXml = objbzStyle.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            lblError.Text = ""
            dbgrdManageStyle.DataSource = ds.Tables("STYLE")
            dbgrdManageStyle.DataBind()
        Else
            dbgrdManageStyle.DataSource = Nothing
            dbgrdManageStyle.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

        intArray(0) = 1
        strArray(0) = "BarcodeNo"

        intArray(1) = 2
        strArray(1) = "StyleName"

        intArray(2) = 3
        strArray(2) = "DesignNo"

        intArray(3) = 4
        strArray(3) = "ShadeNo"

        intArray(4) = 5
        strArray(4) = "MRP"

        objExport.ExportDetails(objOutputXml, "STYLE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAirLineOffice.xls")

    End Sub
#End Region
End Class
