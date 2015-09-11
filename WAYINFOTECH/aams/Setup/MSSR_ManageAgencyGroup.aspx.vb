'################################################################
'############   Page Name -- MSSR_ManageAgencyGroup.aspx  #######
'############   Date 10-November 2007  ##########################
'############   Developed By Abhishek  ##########################
'################################################################
#Region " Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class Setup_MSSR_ManageAgency
    Inherits System.Web.UI.Page
#Region "  Page Level Variables / Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
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


#Region " Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            lblError.Text = ""
            txtRecordCount.Text = "0"
            Session("PageName") = Request.Url.ToString() '"Setup/MSSR_ManageAgencyGroup.aspx"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()

            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' btnReset.Attributes.Add("onclick", "return AGroupReset();")
            btnSearch.Attributes.Add("onclick", "return AGroupMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPManageAgencyGroup();")

            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpLstGroupType.Attributes.Add("onkeyup", "return gotop('drpLstGroupType');")
            drpLstAoffice.Attributes.Add("onkeyup", "return gotop('drpLstAoffice');")
          
            If Not Page.IsPostBack Then
                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)
                objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
                '*******************************************************************
                ' Load Data For Editing Mode
                ' This code is used for Deletion of Agency Group
                'If Not Session("Act") Is Nothing Then
                '    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                '        txtChainCode.Text = Session("Act").ToString().Split("|").GetValue(6)
                '        txtGroupName.Text = Session("Act").ToString().Split("|").GetValue(2)
                '        drpCity.SelectedValue = Session("Act").ToString().Split("|").GetValue(3)
                '        drpLstGroupType.SelectedIndex = Session("Act").ToString().Split("|").GetValue(4)
                '        drpLstAoffice.SelectedIndex = Session("Act").ToString().Split("|").GetValue(5)
                '        SearchAgencyGroup()
                '        lblError.Text = objeAAMSMessage.messDelete
                '        Session("Act") = Nothing
                '    End If
                'End If
            End If

            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        AGroupDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If

            If hdDeleteAgroup.Value <> "" Then
                AGroupDelete(hdDeleteAgroup.Value)
            End If



            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        btnExport.Enabled = False
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

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " SearchAgencyGroup Procedure is called for searching the data of Agency Group Details According to searching criteria"
    Sub SearchAgencyGroup()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            'objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup></MS_SEARCHGROUP_INPUT>")
            objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><PANNO></PANNO><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpCity.SelectedItem.Text ' txtCity.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked

            If (drpLstGroupType.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

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
                ViewState("SortName") = "Chain_Code"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Chain_Code" '"LOCATION_CODE"
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
            objOutputXml = objbzAgencyGroup.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvManageAgencyGroup.DataSource = ds.Tables("GROUP")
                gvManageAgencyGroup.DataBind()
                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordOnCurrentPage.Text = ds.Tables("GROUP").Rows.Count.ToString

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(gvManageAgencyGroup)
                '@ End of Code Added For Paging And Sorting 



            Else
                gvManageAgencyGroup.DataSource = Nothing
                gvManageAgencyGroup.DataBind()
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
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Response.Redirect("MSUP_ManageAgencyGroup.aspx?Action=I")
    'End Sub


    Protected Sub gvManageAgencyGroup_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvManageAgencyGroup.RowCommand
        'Try
        '    'Code for Edit Data
        '    'If e.CommandName = "Editx" Then
        '    '    Response.Redirect("MSUP_ManageAgencyGroup.aspx?Action=U&Chain_Code=" & e.CommandArgument)
        '    'End If
        '    'Code for Delete Date
        '    If e.CommandName = "Deletex" Then
        '        AGroupDelete(e.CommandArgument)
        '        SearchAgencyGroup()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub
#Region " AGroupDelete Procedure is called for deletion of agency group details"
    Sub AGroupDelete(ByVal strChainCode As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            hdDeleteAgroup.Value = ""
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            objInputXml.LoadXml("<MS_DELETEGROUP_INPUT><Chain_Code></Chain_Code></MS_DELETEGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = strChainCode
            'Here Back end Method Call
            objOutputXml = objbzAgencyGroup.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ''lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                'Session("Act") = Request.QueryString("Action")
                'If (Request.QueryString("PopUp")) Is Nothing Then
                '    Response.Redirect("MSSR_ManageAgencyGroup.aspx", False)
                'Else
                '    Response.Redirect("MSSR_ManageAgencyGroup.aspx?PopUp=T", False)
                'End If


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
                SearchAgencyGroup()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnSearch_Click Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "  gvManageAgencyGroup_RowDataBound Event Fired on every row creation in gridview"
    Protected Sub gvManageAgencyGroup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvManageAgencyGroup.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '#############################################################
            ' Code added For Selecting an Items 

            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")

            Dim Comp_Vertical As String
            Dim CompVertical As HiddenField
            CompVertical = (CType(e.Row.FindControl("CompVertical"), HiddenField))
            Comp_Vertical = CompVertical.Value

            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                Dim strLinkData As String = ""
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                strLinkData = lnkSelect.CommandArgument.Split("|")(0).ToString + "|" + lnkSelect.CommandArgument.Split("|")(1).ToString.Replace("'", "\'").ToString + "|" + lnkSelect.CommandArgument.Split("|")(2).ToString
                lnkSelect.CommandArgument = strLinkData
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "','" + Comp_Vertical + "');")
                ' ####################
                ' End of New Code 
                '####################
            End If
            '#############################################################
            'End of  Code added For Secting an Items 
            Dim hdChainCode As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            hdChainCode = e.Row.FindControl("hdStatusCode")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdChainCode.Value & "'" & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    'Else
                    'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdChainCode.Value & "'" & ");")
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objED.Encrypt(hdChainCode.Value) & "'" & ");")
                    'End If
                Else

                End If
            Else
                btnDelete.Enabled = True
                linkEdit.Disabled = False
                'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdChainCode.Value & "'" & ");")
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objED.Encrypt(hdChainCode.Value) & "'" & ");")
                btnDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdChainCode.Value & "'" & ");")
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
            SearchAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvManageAgencyGroup_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvManageAgencyGroup.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub gvManageAgencyGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvManageAgencyGroup.Sorting
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
            SearchAgencyGroup()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub gvManageAgencyGroup_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvManageAgencyGroup.RowCreated
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
            ' Response.Redirect("MSSR_ManageAgencyGroup.aspx", False)
            Response.Redirect(Request.Url.OriginalString(), False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            'objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup></MS_SEARCHGROUP_INPUT>")
            objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><PANNO></PANNO><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpCity.SelectedItem.Text ' txtCity.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked

            If (drpLstGroupType.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            If (drpLstAoffice.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpLstAoffice.SelectedValue
            End If


            'Start CODE for sorting and paging         

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Chain_Code"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Chain_Code" '"LOCATION_CODE"
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
            objOutputXml = objbzAgencyGroup.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
               
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Chain Code", "Group Name", "PAN No.", "City", "Group Type", "Aoffice"}

                Dim intArray() As Integer = {0, 1, 8, 2, 3, 4}

                objExport.ExportDetails(objOutputXml, "GROUP", intArray, strArray, ExportExcel.ExportFormat.Excel, "AgencyGroup.xls")



            Else
                gvManageAgencyGroup.DataSource = Nothing
                gvManageAgencyGroup.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
End Class
