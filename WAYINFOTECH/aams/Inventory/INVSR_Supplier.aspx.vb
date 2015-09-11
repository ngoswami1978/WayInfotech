'##########################################################
'############   Page Name - Inventory_INSR_Supplier #######
'############   Date 19-March 2008    #####################
'############   Developed By Abhishek  ####################
'##########################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region

Partial Class Inventory_INVSR_Supplier
    Inherits System.Web.UI.Page

#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            '  btnSearch.Attributes.Add("onclick", "return SupplierMandatory();")
            btnReset.Attributes.Add("onclick", "return SupplierReset();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")

            If Not Page.IsPostBack Then
                '*******************************************************************
                ''Code For Binding data in Coboox
                objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
                '*******************************************************************
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        txtName.Text = Session("Act").ToString().Split("|").GetValue(2)
                        drpCity.SelectedValue = Session("Act").ToString().Split("|").GetValue(3)
                        drpCountry.SelectedValue = Session("Act").ToString().Split("|").GetValue(4)
                        SupplierSearch(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If
            End If
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        SupplierDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            If (hdID.Value <> "") Then
                DeleteRecords()
            End If

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Attributes("Value").Value)
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
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
   
#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SupplierSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search Supplier"
    Private Sub SupplierSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzSupplier As New AAMS.bizInventory.bzSupplier

            objInputXml.LoadXml("<INV_SEARCHSUPPLIER_INPUT><SUPPLIERNAME />	<CITYID /><COUNTRYID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHSUPPLIER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("SUPPLIERNAME").InnerText = txtName.Text
            objInputXml.DocumentElement.SelectSingleNode("CITYID").InnerText = drpCity.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("COUNTRYID").InnerText = drpCountry.SelectedValue
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
                    ViewState("SortName") = "SUPPLIERNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SUPPLIERNAME" '"LOCATION_CODE"
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
            objOutputXml = objbzSupplier.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvSupplier.DataSource = ds.Tables("SUPPLIER")
                gvSupplier.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record
                If ds.Tables("SUPPLIER").Rows.Count <> 0 Then
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
                    hdRecordOnCurrentPage.Value = ds.Tables("SUPPLIER").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName").ToString()
                        Case "SUPPLIERNAME"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvSupplier.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvSupplier.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "ADDRESS"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvSupplier.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvSupplier.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                        Case "CITY"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvSupplier.HeaderRow.Cells(2).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvSupplier.HeaderRow.Cells(2).Controls.Add(imgDown)

                            End Select
                        Case "COUNTRY"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvSupplier.HeaderRow.Cells(3).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvSupplier.HeaderRow.Cells(3).Controls.Add(imgDown)

                            End Select
                        Case "PHONENUMBER"
                            Select Case ViewState("Desc").ToString()
                                Case "FALSE"
                                    gvSupplier.HeaderRow.Cells(4).Controls.Add(imgUp)
                                Case "TRUE"
                                    gvSupplier.HeaderRow.Cells(4).Controls.Add(imgDown)

                            End Select
                    End Select
                    '  Added Code To Show Image'
                Else
                    gvSupplier.DataSource = Nothing
                    gvSupplier.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvSupplier.DataSource = Nothing
                gvSupplier.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " Sub Procedure Called for deletion of Supplier"
    'Sub SupplierDelete(ByVal strSupID As String)
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzSupplier As New AAMS.bizInventory.bzSupplier
            objInputXml.LoadXml("<INV_DELETESUPPLIER_INPUT><SUPPLIERID /></INV_DELETESUPPLIER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("SUPPLIERID").InnerText = hdID.Value 'strSupID
            'Here Back end Method Call
            hdID.Value = ""

            objOutputXml = objbzSupplier.Delete(objInputXml)
            SupplierSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                'If (Request.QueryString("PopUp")) Is Nothing Then
                '    Response.Redirect("INVSR_Supplier.aspx")
                'Else
                '    Response.Redirect("INVSR_Supplier.aspx?Popup=T", False)
                'End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "gvSupplier_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvSupplier_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSupplier.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkSelect As LinkButton
            Dim hdSuppAdd As New HiddenField
            lnkSelect = e.Row.FindControl("lnkSelect")
            hdSuppAdd = CType(e.Row.FindControl("hdSuppAdd"), HiddenField)
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                hdSuppAdd.Value = hdSuppAdd.Value.Replace(vbCrLf, "\n")
                hdSuppAdd.Value = hdSuppAdd.Value.Replace("'", " ")

                Dim str As String = lnkSelect.CommandArgument
                str = str.Replace(vbCrLf, "\n")
                str = str.Replace("'", " ")

                lnkSelect.Visible = True
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" + str + "','" + hdSuppAdd.Value + "');")
                ' lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If

            '#############################################################
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            'Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("linkEdit")
            ' Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")
            Dim hdSuppId As New HiddenField
            hdSuppId = CType(e.Row.FindControl("hdSuppId"), HiddenField)
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    Else
                        If (Request.QueryString("PopUp")) Is Nothing Then
                            linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdSuppId.Value & ");")
                        Else
                            linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdSuppId.Value & "');")
                        End If
                    End If
                    'If strBuilder(2) = "0" Then
                    'linkEdit.Disabled = False
                    'Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdSuppId.Value) & "');")
                    'End If
                Else

                End If
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdSuppId.Value) & "');")
                If (Request.QueryString("PopUp")) Is Nothing Then
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdSuppId.Value & ");")
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdSuppId.Value & "');")
                End If
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
            SupplierSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SupplierSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SupplierSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvSupplier_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSupplier.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvSupplier_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvSupplier.Sorting
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
            SupplierSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SupplierSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Name", "Address", "City", "Country", "Phone"}
        Dim intArray() As Integer = {1, 2, 4, 5, 3}
        objExport.ExportDetails(objOutputXml, "SUPPLIER", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportSUPPLIER.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

    End Sub
End Class
