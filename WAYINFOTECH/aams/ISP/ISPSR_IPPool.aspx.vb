
Partial Class ISP_ISPSR_IPPool
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.OriginalString
            objEaams.ExpirePageCache()


            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"


            ' btnNew.Attributes.Add("onclick", "return InsertPtrAssignee();")
            drpProviders.Attributes.Add("onkeyup", "return gotop('drpProviders')")
            drpPop.Attributes.Add("onkeyup", "return gotop('drpPop')")
            'drpFollowup.Attributes.Add("onkeyup", "return gotop('drpFollowup')")
            'drpPtrStatus.Attributes.Add("onkeyup", "return gotop('drpPtrStatus')")
            'drpPtrTypCat.Attributes.Add("onkeyup", "return gotop('drpPtrTypCat')")
            'drpPtrType.Attributes.Add("onkeyup", "return gotop('drpPtrType')")
            'drpSeverity.Attributes.Add("onkeyup", "return gotop('drpSeverity')")


            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If



            If Not Page.IsPostBack Then
                'btnSearch.Attributes.Add("onClick", "return ValidateForm();")
                btnNew.Attributes.Add("onClick", "return NewIPPool();")
                BindDropDowns()
                If Request.QueryString("Popup") IsNot Nothing Then
                    If Request.QueryString("Popup") = "T" Then
                        chkUnallocated.Checked = True
                        chkUnallocated.Enabled = False
                    End If
                End If
            End If

            If hdDeleteISPID.Value.Trim <> "" Then
                DeleteIPPool(hdDeleteISPID.Value.Trim)
                GetSearchResult()
                hdDeleteISPID.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDropDowns()
        Try
            objEaams.BindDropDown(drpPop, "IPPOP", True, 3)
            objEaams.BindDropDown(drpProviders, "IPPROVIDER", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'lblError.Text = ""
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub GetSearchResult()

        '<IS_SER_PROVIDER_INPUT> 

        ' <ProviderID/>

        ' <PopID/> 

        ' <IPAddress/>

        ' <AgencyName/>

        ' <ChkUnallocated/> 

        ' <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/>

        '</IS_SER_PROVIDER_INPUT>


        '<IS_SER_IPDEFINITION_OUTPUT>

        ' <DETAILS 

        'ProviderName=""

        'ProviderID=""


        '    PopName=""

        ' IPAddressID = ""

        ' IPAddress = ""

        ' SubnetMask = ""

        ' RouterIP = ""

        ' NumberOfTerminal = ""

        ' OfficeID=''

        ' AgencyName=''

        ' AceNumber = ""

        ' Remarks="" />

        ' <PAGE PAGE_COUNT='' TOTAL_ROWS=''/>

        ' <Errors Status=''><Error Code='' Description='' /></Errors>

        '</IS_SER_IPDEFINITION_OUTPUT>


        Try
            Dim objInputX, objOutputX As New XmlDocument
            Dim objReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objSearchIP As New AAMS.bizISP.bzIPPoolDefinition

            objInputX.LoadXml("<IS_SER_PROVIDER_INPUT><ProviderID/><PopID/><IPAddress/><AgencyName/><ChkUnallocated/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></IS_SER_PROVIDER_INPUT>")
            With objInputX.DocumentElement
                If drpProviders.SelectedIndex > 0 Then
                    .SelectSingleNode("ProviderID").InnerText = drpProviders.SelectedValue.Trim()
                End If
                If drpPop.SelectedIndex > 0 Then
                    .SelectSingleNode("PopID").InnerText = drpPop.SelectedValue.Trim()
                End If
                .SelectSingleNode("IPAddress").InnerText = txtIPAddress.Text.Trim()
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                .SelectSingleNode("ChkUnallocated").InnerText = IIf(chkUnallocated.Checked, "TRUE", "FALSE")
            End With



            If ViewState("PrevSearching") Is Nothing Then
                objInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputX.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputX.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputX.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "ProviderName"
                objInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ProviderName" '"LOCATION_CODE"
            Else
                objInputX.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputX.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputX.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            'Here Back end Method Call
          
            objOutputX = objSearchIP.Search(objInputX)

            ' objHdOutputX.LoadXml("<HD_SEARCH_PTR_OUTPUT><PTR HD_PTR_ID='1' HD_RE_ID='10002' HD_PTR_REF='43534' NAME='Test Ptre' ADDRESS='Test Address' HD_PTR_TITLE='Test Title' HD_STATUS_NAME='Ok' HD_PTR_FOLLOWUP_NAME='Test Follow' HD_PTR_SEV_NAME='sdfsd' HD_PTR_TYPE_NAME='sdfsdf' Employee_Name='' HD_PTR_OPENDATE='' HD_PTR_CLOSEDATE='20000202' ASSIGNEE_NAME='Mukesh' ASSIGNED_DATE='20000202'/><Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_SEARCH_PTR_OUTPUT>")
            If objOutputX.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objReader = New XmlNodeReader(objOutputX)
                dSet.ReadXml(objReader)
                ViewState("PrevSearching") = objInputX.OuterXml

                If hdDelMsg.Value.Trim <> "1" Then
                    lblError.Text = ""
                    'hdDelMsg.Value = ""
                End If

                hdDelMsg.Value = ""

                grdvIPPool.DataSource = dSet.Tables("DETAILS")
                grdvIPPool.DataBind()
                txtRecordOnCurrentPage.Text = grdvIPPool.Rows.Count.ToString()
                PagingCommon(objOutputX)
                Dim intcol As Integer = GetSortColumnIndex(grdvIPPool)
                If ViewState("Desc") = "FALSE" Then
                    grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvIPPool.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                ' lblError.Text = ""
            Else
                grdvIPPool.DataSource = Nothing
                grdvIPPool.DataBind()
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False
                If hdDelMsg.Value.Trim <> "1" Then
                    hdDelMsg.Value = ""
                    lblError.Text = objOutputX.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    

    Protected Sub grdvIPPool_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvIPPool.RowDataBound
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
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If
            '#############################################################

            Dim hdIPProviderID As HiddenField
            Dim hdIPID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            'hdIPAddressID
            linkEdit = e.Row.FindControl("linkEdit")
            'Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            ' linkDelete = e.Row.FindControl("linkDelete")
            hdIPProviderID = e.Row.FindControl("hdProviderID")
            hdIPID = e.Row.FindControl("hdIPAddressID")
            'History Section
            'hdIPAddressID
            
            Dim history As LinkButton
            history = e.Row.FindControl("lnkHistory")
            Dim hdLcode As HiddenField
            hdLcode = e.Row.FindControl("hdLcode")
            If hdLcode.Value.Trim = "" Then
                history.Enabled = False
            End If
            'End of History Section 

            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdProductName.Value & "');")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdProductName.Value & "');")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            Dim strTest As String = Session("Security")
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPPool']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                    btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdIPID.Value & "');")
                End If

                If strBuilder(0) = "0" Then
                    history.Enabled = False
                Else
                    history.Enabled = True
                    If hdLcode.Value.Trim <> "" Then
                        history.Attributes.Add("onclick", "return ShowHistory('" & objEn.Encrypt(hdLcode.Value.Trim) & "')")
                    End If
                End If


                If strBuilder(2) = "0" Then
                    linkEdit.Disabled = True
                Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdIPProviderID.Value) & "','" & objEn.Encrypt(hdIPID.Value.Trim) & "');")
                End If
            Else
                'history.Attributes.Add("onclick", "return ShowHistory('" & hdLcode.Value.Trim & "')")
                If hdLcode.Value.Trim <> "" Then
                    history.Attributes.Add("onclick", "return ShowHistory('" & objEn.Encrypt(hdLcode.Value.Trim) & "')")
                End If
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdIPProviderID.Value) & "','" & objEn.Encrypt(hdIPID.Value.Trim) & "');")
                btnDelete.Enabled = True
                btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdIPID.Value & "');")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub DeleteIPPool(ByVal ispID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzIsp As New AAMS.bizISP.bzIPPoolDefinition
            objInputXml.LoadXml("<IS_DELETE_IPDEFINITION_INPUT><IPAddressID/></IS_DELETE_IPDEFINITION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("IPAddressID").InnerText = ispID
            hdDeleteISPID.Value = ""
            'Call a function
            objOutputXml = objbzIsp.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
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
                hdDelMsg.Value = "1"
                GetSearchResult()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                hdDelMsg.Value = "1"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetSearchResult()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetSearchResult()
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
    Protected Sub grdvIPPool_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvIPPool.Sorting
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
        GetSearchResult()
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExport As New ExportExcel
        'intArray, strArray,
        Dim intArray() As Integer = {3, 4, 5, 6, 7, 8, 11, 12}
        Dim strArray() As String = {"Provider Name", "POP", "IP Address", "Subnet Mask", "Router IP", "No. Of Terminal", "Acc No", "Remarks"}
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAirlineOffice As New AAMS.bizISP.bzIPPoolDefinition

        objInputXml.LoadXml("<IS_SER_PROVIDER_INPUT><ProviderID/><PopID/><IPAddress/><AgencyName/><ChkUnallocated/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></IS_SER_PROVIDER_INPUT>")

        With objInputXml.DocumentElement
            If drpProviders.SelectedIndex > 0 Then
                .SelectSingleNode("ProviderID").InnerText = drpProviders.SelectedValue.Trim()
            End If
            If drpPop.SelectedIndex > 0 Then
                .SelectSingleNode("PopID").InnerText = drpPop.SelectedValue.Trim()
            End If
            .SelectSingleNode("IPAddress").InnerText = txtIPAddress.Text.Trim()
            .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
            .SelectSingleNode("ChkUnallocated").InnerText = IIf(chkUnallocated.Checked, "TRUE", "FALSE")
        End With



        





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

        'End Code for paging and sorting

        'Here Back end Method Call

        objOutputXml = objbzAirlineOffice.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            lblError.Text = ""

            grdvIPPool.DataSource = ds.Tables("DETAILS")
            grdvIPPool.DataBind()

            'strArray  = {"Provider Name", "POP", "IP Address", "Subnet Mask", "Router IP", "No. Of Terminal", "Acc No", "Remarks"}
            'Dim intArray() As Integer = {3, 4, 5, 6, 7, 8, 11, 12}

            objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "Ippool.xls")


        Else
            grdvIPPool.DataSource = Nothing
            grdvIPPool.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        

        ' objExport.ExportDetails(objOutputXml, "AIRLINE_OFFICE", ExportExcel.ExportFormat.Excel, "AirlineOffice.xls")
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
