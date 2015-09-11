'#########################################################
'############   Page Name -- ISP_ISP_SRISPProvider  ######
'############   Date 23-July 2008  #######################  
'############   Developed By Abhishek  ###################
'#########################################################
Partial Class ISP_ISP_SRISPProvider
    Inherits System.Web.UI.Page

#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            '   btnSearch.Attributes.Add("onclick", "return ProviderMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPProvider();")

          

            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspProvider']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspProvider']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        ' btnExport.Enabled = False

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

            If hdDeleteProviderID.Value <> "" Then
                IspProviderDelete(hdDeleteProviderID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
   
#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            IspProviderSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Method for search Provider  Search"
    Private Sub IspProviderSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzProvider As New AAMS.bizISP.bzProvider
            objInputXml.LoadXml("<IS_SEARCHPROVIDER_INPUT><ProviderName/><PAGE_NO/><PAGE_SIZE/>	<SORT_BY/><DESC/></IS_SEARCHPROVIDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ProviderName").InnerText = txtISPProvider.Text

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
                ViewState("SortName") = "ProviderName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ProviderName" '"LOCATION_CODE"
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
            objOutputXml = objbzProvider.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvISPProvider.DataSource = ds.Tables("ISP")
                grdvISPProvider.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordOnCurrentPage.Text = ds.Tables("ISP").Rows.Count.ToString

                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvISPProvider)

                '@ End of Code Added For Paging And Sorting 
            Else
                grdvISPProvider.DataSource = Nothing
                grdvISPProvider.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                txtTotalRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
#End Region
    Protected Sub grdvISPProvider_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdvISPProvider.RowCommand

    End Sub
#Region " Sub Procedure Called for deletion of Isp Provider"
    Sub IspProviderDelete(ByVal strProviderID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzProvider As New AAMS.bizISP.bzProvider
            objInputXml.LoadXml("<IS_DELETEPROVIDER_INPUT><ProviderID/></IS_DELETEPROVIDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerText = strProviderID

            hdDeleteProviderID.Value = ""
            'Here Back end Method Call

            objOutputXml = objbzProvider.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
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
                IspProviderSearch()

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " grdvISPProvider_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub grdvISPProvider_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvISPProvider.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '#############################################################
            ' Code added For Selecting an Items 
            Dim lnkSelect As LinkButton
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If
            '#############################################################

            Dim hdstrProviderID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            hdstrProviderID = e.Row.FindControl("hdProviderID")


            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForstrProviderID As String
            EnstrQueryStringForstrProviderID = objED.Encrypt(hdstrProviderID.Value)
            '@ End of Code Used For Encription/Decription


          
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspProvider']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspProvider']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrProviderID.Value & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    'linkEdit.Disabled = True
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
                    'End If

                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrProviderID & "');")
                    'linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
                Else

                End If
            Else
                btnDelete.Enabled = True
                linkEdit.Disabled = False
                ' linkEdit.Attributes.Add("onclick", "return EditFunction(" & hdstrProviderID.Value & ");")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrProviderID & "');")
                btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdstrProviderID.Value & ");")
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
            IspProviderSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            IspProviderSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            IspProviderSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPProvider_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvISPProvider.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvISPProvider_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvISPProvider.Sorting
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
            IspProviderSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvISPProvider_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvISPProvider.RowCreated
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
        ' Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzProvider As New AAMS.bizISP.bzProvider
            objInputXml.LoadXml("<IS_SEARCHPROVIDER_INPUT><ProviderName/><PAGE_NO/><PAGE_SIZE/>	<SORT_BY/><DESC/></IS_SEARCHPROVIDER_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ProviderName").InnerText = txtISPProvider.Text

            'Start CODE for sorting and paging   

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "ProviderName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ProviderName" '"LOCATION_CODE"
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
            objOutputXml = objbzProvider.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"ProviderName"}
                Dim intArray() As Integer = {1}
                objExport.ExportDetails(objOutputXml, "ISP", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPProvider.xls")
            Else
                grdvISPProvider.DataSource = Nothing
                grdvISPProvider.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub


#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region

End Class
