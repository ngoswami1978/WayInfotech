'#####################################################
'############   Page Name -- MSSR_IPPool.aspx  #######
'############   Date 13-November 2007  ###############
'############   Developed By Abhishek  ###############
'#####################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class Setup_MSSR_IPPool
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
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
            Session("PageName") = "Setup/MSSR_IPPool.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' btnReset.Attributes.Add("onclick", "return IPPoolReset();")
            btnSearch.Attributes.Add("onclick", "return CheckMandatoty();")
            btnNew.Attributes.Add("onclick", "return NewMSUPIPPool();")

          
            drpLstAoffice.Attributes.Add("onkeyup", "return gotop('drpLstAoffice');")


            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True, 3)

                'If Not Session("Act") Is Nothing Then
                '    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                '        txtPoolName.Text = Session("Act").ToString().Split("|").GetValue(2)
                '        drpLstAoffice.SelectedIndex = Session("Act").ToString().Split("|").GetValue(3)
                '        txtDepartment.Text = Session("Act").ToString().Split("|").GetValue(4)
                '        SearchIPPool()
                '        lblError.Text = objeAAMSMessage.messDelete
                '        Session("Act") = Nothing
                '    End If
                'End If
                '*******************************************************************   
            End If

            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        IPPoolDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If

            If hdDeleteIpPool.Value <> "" Then
                IPPoolDelete(hdDeleteIpPool.Value)
            End If


            ' #######################################
            ' ########## This Code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            Dim strBuilder As New StringBuilder
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Attributes("Value").Value)
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
           




            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpLstAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpLstAoffice.SelectedValue = li.Value
                            End If

                        End If
                        drpLstAoffice.Enabled = False
                    End If
                End If
            End If


            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnSearch_Click Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchIPPool()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "SearchIPPool Procedure called for Searching IP POOL According to Searching criteria"
    Sub SearchIPPool()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            'objInputXml.LoadXml("<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name></MS_SEARCHIPPOOL_INPUT>")
            objInputXml.LoadXml("<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHIPPOOL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PoolName").InnerText = txtPoolName.Text
            objInputXml.DocumentElement.SelectSingleNode("Department_Name").InnerText = txtDepartment.Text
            If (drpLstAoffice.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpLstAoffice.SelectedValue
            End If

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
                ViewState("SortName") = "PoolName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "PoolName" '"LOCATION_CODE"
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
            objOutputXml = objbzIPPool.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                dbgrdIpPOOL.DataSource = ds.Tables("IPPOOL")
                dbgrdIpPOOL.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordOnCurrentPage.Text = ds.Tables("IPPOOL").Rows.Count.ToString

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(dbgrdIpPOOL)
                '@ End of Code Added For Paging And Sorting 


            Else
                dbgrdIpPOOL.DataSource = Nothing
                dbgrdIpPOOL.DataBind()
                txtRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try

    End Sub
#End Region
    Protected Sub dbgrdIpPOOL_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdIpPOOL.RowCommand
        'Try
        '    'Code for Edit Data
        '    If e.CommandName = "Editx" Then
        '        Response.Redirect("MSUP_IPPool.aspx?Action=U&PoolID=" & e.CommandArgument)
        '    End If
        '    'Code for Delete Date
        '    If e.CommandName = "Deletex" Then
        '        IPPoolDelete(e.CommandArgument)
        '        SearchIPPool()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub
#Region "IPPoolDelete Procedure called for the deletion of IP POOL"
    Sub IPPoolDelete(ByVal strPoolId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzbzIPPool As New AAMS.bizMaster.bzIPPool
            objInputXml.LoadXml("<MS_DELETEIPPOOL_INPUT><PoolID></PoolID><IPAddress></IPAddress></MS_DELETEIPPOOL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PoolID").InnerText = strPoolId
            'Here Back end Method Call
            hdDeleteIpPool.Value = ""
            objOutputXml = objbzbzIPPool.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = "Deleted Successfully."
                'Session("Act") = Request.QueryString("Action")
                'Response.Redirect("MSSR_IPPool.aspx")

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
                SearchIPPool()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#End Region
#Region "dbgrdIpPOOL_RowDataBound Event Fired on every row creation in Gridview"
    Protected Sub dbgrdIpPOOL_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdIpPOOL.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrPoolID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            'Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            'linkDelete = e.Row.FindControl("linkDelete")

            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")

            hdstrPoolID = e.Row.FindControl("hdPoolId")
            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPoolID.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPoolID.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IP Pool']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPoolID.Value & "'" & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    'linkEdit.Disabled = False
                    ' Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objEn.Encrypt(hdstrPoolID.Value.Trim) & "'" & ");")
                    'End If
                Else

                End If
            Else
                btnDelete.Enabled = True
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objEn.Encrypt(hdstrPoolID.Value.Trim) & "'" & ");")
                btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPoolID.Value & "'" & ");")
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
            SearchIPPool()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchIPPool()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchIPPool()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub dbgrdIpPOOL_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dbgrdIpPOOL.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dbgrdIpPOOL_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdIpPOOL.Sorting
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
            SearchIPPool()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub dbgrdIpPOOL_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdIpPOOL.RowCreated
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
            Response.Redirect("MSSR_IPPool.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        ' Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            'objInputXml.LoadXml("<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name></MS_SEARCHIPPOOL_INPUT>")
            objInputXml.LoadXml("<MS_SEARCHIPPOOL_INPUT><PoolName></PoolName><Aoffice></Aoffice><Department_Name></Department_Name><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHIPPOOL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PoolName").InnerText = txtPoolName.Text
            objInputXml.DocumentElement.SelectSingleNode("Department_Name").InnerText = txtDepartment.Text
            If (drpLstAoffice.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpLstAoffice.SelectedValue
            End If

            'Start CODE for sorting and paging

           
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "PoolName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "PoolName" '"LOCATION_CODE"
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
            objOutputXml = objbzIPPool.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
               
                Dim objExport As New ExportExcel

                Dim strArray() As String = {"IP Pool Name", "Department", "Aoffice", "IPs"}

                Dim intArray() As Integer = {1, 2, 3, 4}

                objExport.ExportDetails(objOutputXml, "IPPOOL", intArray, strArray, ExportExcel.ExportFormat.Excel, "IPPool.xls")


            Else
                dbgrdIpPOOL.DataSource = Nothing
                dbgrdIpPOOL.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try

    End Sub
End Class
