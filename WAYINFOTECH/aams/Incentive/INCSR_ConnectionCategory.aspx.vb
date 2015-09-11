
Partial Class Incentive_INCSR_EquipmentCategory
    Inherits System.Web.UI.Page


#Region " Page Level Variables/ Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

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

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Session("PageName") = Request.Url.ToString
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()

            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            ' btnReset.Attributes.Add("onclick", "return SecurityRegionReset();")
            '   btnSearch.Attributes.Add("onclick", "return CheckMandatoty();")
            btnNew.Attributes.Add("onclick", "return NewConnectionCategory();")

            If Not IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            End If

            If hdDeleteConnCateg.Value <> "" Then
                ConnectionCategoryDelete(hdDeleteConnCateg.Value)
            End If


            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                        btnExport.Enabled = False
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
#Region "btnSearch_Click Event is fired whenever btnSearch button is clicked by user. "
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchConnectionCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    ' btnNew_Click Event is fired whenever btnNew button is clicked by user. 
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_SecurityRegion.aspx?Action=I")
    'End Sub
#Region "SearchConnectionCategory method is used to find the record of security region details on the basis of Region Name and bind the data to controls."
    Sub SearchConnectionCategory()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity
            objInputXml.LoadXml("<INC_SEARCH_CONN_CAT_INPUT><COUNTRYID></COUNTRYID>	<BC_ONLINE_CATG_NAME></BC_ONLINE_CATG_NAME><BC_ONLINE_CATG_COST></BC_ONLINE_CATG_COST><SORT_BY></SORT_BY>	<DESC></DESC>	<PAGE_NO></PAGE_NO>	<PAGE_SIZE></PAGE_SIZE></INC_SEARCH_CONN_CAT_INPUT>")

            If drpCountry.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRYID").InnerText = drpCountry.SelectedValue.ToString
            End If


            objInputXml.DocumentElement.SelectSingleNode("BC_ONLINE_CATG_NAME").InnerText = txtConnectionCateg.Text


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
                ViewState("SortName") = "BC_ONLINE_CATG_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BC_ONLINE_CATG_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'Here Back end Method Call
            objOutputXml = objbzConnectivity.Search(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                dbgrdConnectionCategory.DataSource = ds.Tables("CONN_CAT")
                dbgrdConnectionCategory.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordOnCurrentPage.Text = ds.Tables("CONN_CAT").Rows.Count.ToString

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(dbgrdConnectionCategory)
                '@ End of Code Added For Paging And Sorting 


            Else
                dbgrdConnectionCategory.DataSource = Nothing
                dbgrdConnectionCategory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                txtRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
#End Region
#Region "ConnectionCategoryDelete method is used to delete one record from security region by passing value of region Id"
    Sub ConnectionCategoryDelete(ByVal strConnCategId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity
            objInputXml.LoadXml("<INC_DELETE_CONN_CAT_INPUT>	<BC_ONLINE_CATG_ID></BC_ONLINE_CATG_ID></INC_DELETE_CONN_CAT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("BC_ONLINE_CATG_ID").InnerText = strConnCategId
            'Here Back end Method Call
            hdDeleteConnCateg.Value = ""
            objOutputXml = objbzConnectivity.Delete(objInputXml)
            SearchConnectionCategory()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete ' "Deleted Successfully."
                'Session("Act") = Request.QueryString("Action")
                'Response.Redirect("MSSR_SecurityRegion.aspx")

                lblError.Text = objeAAMSMessage.messDelete

                ' ###################################################################
                '@ Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "dbgrdConnectionCategory_RowCommand event is fired by Gridview (dbgrdConnectionCategory) whenever Edit/ Delete Link button is clicked by user."
    Protected Sub dbgrdConnectionCategory_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdConnectionCategory.RowCommand
        'Try
        '    'Code for Edit Data
        '    If e.CommandName = "Editx" Then
        '        Response.Redirect("MSUP_SecurityRegion.aspx?Action=U&RegionID=" & e.CommandArgument)
        '    End If
        '    'Code for Delete Date
        '    If e.CommandName = "Deletex" Then
        '        SecurityRegionDelete(e.CommandArgument)
        '        SearchSecurityRegion()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub
#End Region
#Region "dbgrdSecurityRegion_RowDataBound Event Fired on every row creation in Gridview"
    Protected Sub dbgrdConnectionCategory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdConnectionCategory.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrConnCategId As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")

            hdstrConnCategId = e.Row.FindControl("hdConnCategId")
          
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Connectivity Category']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrConnCategId.Value & "'" & ");")
                    End If

                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objEn.Encrypt(hdstrConnCategId.Value.Trim) & "'" & ");")

                Else

                End If
            Else
                btnDelete.Enabled = True
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objEn.Encrypt(hdstrConnCategId.Value.Trim) & "'" & ");")
                btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrConnCategId.Value & "'" & ");")
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
            SearchConnectionCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchConnectionCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchConnectionCategory()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub dbgrdConnectionCategory_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgrdConnectionCategory.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dbgrdConnectionCategory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdConnectionCategory.Sorting
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
            SearchConnectionCategory()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub dbgrdConnectionCategory_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdConnectionCategory.RowCreated
        Try
            'Dim grvRow As GridViewRow
            'grvRow = e.Row
            'If e.Row.RowType = DataControlRowType.Header Then
            '    If gvManageAgencyGroup.AllowSorting = True Then
            '        CType(grvRow.Cells(0).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text
            '    Else
            '        e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
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

#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzConnectivity As New AAMS.bizIncetive.bzConnectivity

            objInputXml.LoadXml("<INC_SEARCH_CONN_CAT_INPUT><COUNTRYID></COUNTRYID>	<BC_ONLINE_CATG_NAME></BC_ONLINE_CATG_NAME><BC_ONLINE_CATG_COST></BC_ONLINE_CATG_COST><SORT_BY></SORT_BY>	<DESC></DESC>	<PAGE_NO></PAGE_NO>	<PAGE_SIZE></PAGE_SIZE></INC_SEARCH_CONN_CAT_INPUT>")

            If drpCountry.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRYID").InnerText = drpCountry.SelectedValue.ToString
            End If

            objInputXml.DocumentElement.SelectSingleNode("BC_ONLINE_CATG_NAME").InnerText = txtConnectionCateg.Text



            'Start CODE for sorting and paging         
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "BC_ONLINE_CATG_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "BC_ONLINE_CATG_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'Here Back end Method Call
            objOutputXml = objbzConnectivity.Search(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)

                Dim objExport As New ExportExcel

                Dim strArray() As String = {"Connection Category Name", "Country", "Unit Cost"}

                Dim intArray() As Integer = {1, 4, 2}

                objExport.ExportDetails(objOutputXml, "CONN_CAT", intArray, strArray, ExportExcel.ExportFormat.Excel, "ConnectionCategory.xls")

            Else
                dbgrdConnectionCategory.DataSource = Nothing
                dbgrdConnectionCategory.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
End Class
