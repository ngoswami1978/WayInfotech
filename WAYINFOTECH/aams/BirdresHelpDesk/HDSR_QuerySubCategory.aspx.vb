
Partial Class BirdresHelpDesk_HDSR_QuerySubCategory
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
    Dim objeAAMS As New eAAMS

    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtSubCategoryName.Focus()
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
            ' txtSubCategoryName.Attributes.Add("onkeypress", "return allTextWithSpace();")
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ddlQuerySubGroup.Attributes.Add("OnChange", "return fillCategoryName('ddlQuerySubGroup');")
            hdCategoryName.Value = Request.Form(ddlCategoryName.UniqueID)
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlQuerySubGroup, "BRQuerySubGroup", True, 3)
                'objeAAMS.BindDropDown(ddlCategoryName, "TCATEGORYNAME", True)

                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                '        txtSubCategoryName.Text = Request.QueryString("Action").ToString().Split("|").GetValue(2)
                '        ddlQuerySubGroup.SelectedValue = Request.QueryString("Action").ToString().Split("|").GetValue(3)
                '        fillCategoryName()
                '        ddlCategoryName.SelectedValue = Request.QueryString("Action").ToString().Split("|").GetValue(4)
                '        hdCategoryName.Value = Request.QueryString("Action").ToString().Split("|").GetValue(4)
                '        QuerySubCategoryDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                '    End If
                'End If

                ' This code is used for checking session handler according to user login.
                If Session("Security") Is Nothing Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                End If

                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Attributes("Value").Value)
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
            End If
            ' Code For Deleting Records

            If hdID.Value <> "" Then
                txtSubCategoryName.Text = hdID.Value.Split("|").GetValue(2)
                ddlQuerySubGroup.SelectedValue = hdID.Value.Split("|").GetValue(3)
                fillCategoryName()
                ddlCategoryName.SelectedValue = hdID.Value.Split("|").GetValue(4)
                hdCategoryName.Value = hdID.Value.Split("|").GetValue(4)
                QuerySubCategoryDelete(hdID.Value.Split("|").GetValue(1))
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("HDUP_QuerySubCategory.aspx?Action=I|")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            QuerySubCategorySearch(PageOperation.Search)
            fillCategoryName()
            ddlCategoryName.SelectedValue = hdCategoryName.Value
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("HDSR_QuerySubCategory.aspx")
    End Sub

    Sub QuerySubCategorySearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
        '   <HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /></HD_SEARCHCALLSUBCATEGORY_INPUT>
        ' objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME/><CALL_CATEGORY_ID/><CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
        objInputXml.LoadXml("<HD_SEARCHCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_NAME /><CALL_CATEGORY_ID /><CALL_SUB_GROUP_ID /><HD_QUERY_GROUP_ID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLSUBCATEGORY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_NAME").InnerText = Trim(txtSubCategoryName.Text)
        If hdCategoryName.Value = "" Then
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = ""
        Else
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = hdCategoryName.Value
        End If
        objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = hdCategoryName.Value
        If ddlQuerySubGroup.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
        Else
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ""
        End If

        objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
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
                ViewState("SortName") = "CALL_SUB_GROUP_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CALL_SUB_GROUP_NAME" '"LOCATION_CODE"
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
        objOutputXml = objbzCallSubCategory.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If

            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvQuerySubCategory.DataSource = ds.Tables("CALL_SUB_CATEGORY")
            gvQuerySubCategory.DataBind()
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
            hdRecordOnCurrentPage.Value = ds.Tables("CALL_SUB_CATEGORY").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'
            Dim imgUp As New Image
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            Dim imgDown As New Image
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            Select Case ViewState("SortName")
                Case "CALL_SUB_GROUP_NAME"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuerySubCategory.HeaderRow.Cells(0).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuerySubCategory.HeaderRow.Cells(0).Controls.Add(imgDown)
                    End Select
                Case "CALL_CATEGORY_NAME"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuerySubCategory.HeaderRow.Cells(1).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuerySubCategory.HeaderRow.Cells(1).Controls.Add(imgDown)
                    End Select
                Case "CALL_SUB_CATEGORY_NAME"
                    Select Case ViewState("Desc")
                        Case "FALSE"
                            gvQuerySubCategory.HeaderRow.Cells(2).Controls.Add(imgUp)
                        Case "TRUE"
                            gvQuerySubCategory.HeaderRow.Cells(2).Controls.Add(imgDown)
                    End Select
            End Select
            '  Added Code To Show Image'

            ' End of Code Added For Paging And Sorting In case Of Delete The Record

        Else
            gvQuerySubCategory.DataSource = Nothing
            gvQuerySubCategory.DataBind()
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False

            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Sub QuerySubCategoryDelete(ByVal QueryCategory As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
        ' objInputXml.LoadXml("<MS_DELETE_CALL_SUBCATEGORY_INPUT><CALL_SUB_CATEGORY_ID/></MS_DELETE_CALL_SUBCATEGORY_INPUT> ")
        objInputXml.LoadXml("<HD_DELETECALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_ID/></HD_DELETECALLSUBCATEGORY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerXml = QueryCategory
        'Here Back end Method Call
        hdID.Value = ""
        objOutputXml = objbzCallSubCategory.Delete(objInputXml)
        QuerySubCategorySearch(PageOperation.Search)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Protected Sub gvQuerySubCategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvQuerySubCategory.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdQuerySubCategory As HiddenField
        Dim linkEdit As New LinkButton
        linkEdit = e.Row.FindControl("lnkEdit")
        Dim linkDelete As New LinkButton
        linkDelete = e.Row.FindControl("lnkDelete")
        hdQuerySubCategory = e.Row.FindControl("hdQuerySubCategory")

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Enabled = False
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdQuerySubCategory.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Enabled = False
            'Else
            '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdQuerySubCategory.Value & "');")
            'End If
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdQuerySubCategory.Value) & "');")
        Else
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdQuerySubCategory.Value) & "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdQuerySubCategory.Value & "');")
        End If
    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        If eventArgument <> "" Then
            Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim id As String = eventArgument

            '  objInputXml.LoadXml("<MS_SEARCH_CALLCATEGORY_INPUT>	<CALL_CATEGORY_NAME/>	<CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></MS_SEARCH_CALLCATEGORY_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = id
            ' objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
            objOutputXml = objbzCallCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                str = objOutputXml.OuterXml
            Else
                str = ""
            End If
        End If
    End Sub

    Public Sub fillCategoryName()
        If ddlQuerySubGroup.SelectedValue <> "" Then
            Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            ' objInputXml.LoadXml("<MS_SEARCH_CALLCATEGORY_INPUT>	<CALL_CATEGORY_NAME/>	<CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></MS_SEARCH_CALLCATEGORY_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
            objOutputXml = objbzCallCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ddlCategoryName.DataSource = ds.Tables("CALL_CATEGORY")
                ddlCategoryName.DataTextField = "CALL_CATEGORY_NAME"
                ddlCategoryName.DataValueField = "CALL_CATEGORY_ID"
                ddlCategoryName.DataBind()
            End If
            ddlCategoryName.Items.Insert(0, New ListItem("--All--", ""))
        Else
            ddlCategoryName.Items.Clear()
            ddlCategoryName.Items.Insert(0, New ListItem("--All--", ""))
        End If


    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            QuerySubCategorySearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            QuerySubCategorySearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            QuerySubCategorySearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvQuerySubCategory_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvQuerySubCategory.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvQuerySubCategory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvQuerySubCategory.Sorting
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
            QuerySubCategorySearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            QuerySubCategorySearch(PageOperation.Export)
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
        Dim strArray() As String = {"Query Sub Group", "Category Name", "Sub Category"}
        Dim intArray() As Integer = {3, 2, 1}
        objExport.ExportDetails(objOutputXml, "CALL_SUB_CATEGORY", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportQUERYSUBCATEGORY.xls")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
