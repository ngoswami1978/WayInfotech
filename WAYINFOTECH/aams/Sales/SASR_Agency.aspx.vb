
Partial Class Sales_SASR_Agency
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler


#Region "Global variables declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
#End Region


#Region "Code for Filter "

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

#End Region

#Region "Page_Load()"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strManagerLog As String
        Try
            Session("PageName") = Request.Url.ToString()
            btnSearch.Attributes.Add("onclick", "return CheckValidationTravelAgency();")

            If Not Page.IsPostBack Then
                ' Code For Agency Using Birdres Start
                If Request.QueryString("HelpDeskType") IsNot Nothing Then
                    If Request.QueryString("HelpDeskType") = "BR" Then
                        chkAgencyUsingBirdres.Checked = True
                    End If
                End If
                ' Code End
                If Request.QueryString("IsManager") IsNot Nothing Then
                    strManagerLog = Request.QueryString("IsManager")
                    If strManagerLog = "True" Then
                        LblPlan.Text = "Planned Visit"
                    Else
                        LblPlan.Text = "Unplanned Visit"
                    End If
                Else
                    LblPlan.Text = "Unplanned Visit"
                End If
            End If

            If Request.QueryString("PREDATE") Is Nothing Then
                lblError.Text = "Invalid URL."
                Exit Sub
            End If
            If Request.QueryString("PREDATE").Trim.ToString.Length = 0 Then
                lblError.Text = "Invalid URL."
                Exit Sub
            End If
            If Session("AgencyDSRLOGXML") IsNot Nothing Then
                Dim objOutputDSRXml As New XmlDocument
                objOutputDSRXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                If objOutputDSRXml.DocumentElement.SelectSingleNode("DSRLOG") IsNot Nothing Then
                    If objOutputDSRXml.DocumentElement.SelectSingleNode("DSRLOG").Attributes("PREDATE").Value.Trim() <> objeAAMS.ConvertTextDate(Request.QueryString("PREDATE").ToString) Then
                        lblError.Text = "DSR Date has been changed."
                        ' ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.opener.document.forms['form1']['lblError'].innerHTML='DSR Date has been changed.'; window.close();", True)
                        btnSearch.Enabled = False
                        Exit Sub
                    End If
                End If
            End If



            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerDataTravelAgency", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            btnUp.Attributes.Add("onclick", "return fillBackUpTravelAgency()")
            lnkAdvance.Attributes.Add("onclick", "return fillBackUpTravelAgency()")

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Session("EmployeePageName") IsNot Nothing Then
                hdEmployeePageName.Value = Session("EmployeePageName")
            End If


            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                hdAdvanceSearch.Value = "0"
                If objSecurityXml.DocumentElement.SelectSingleNode("Employee_Name") IsNot Nothing Then
                    txtAResponsibility.Text = objSecurityXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText
                End If

                BindAllControl()
            End If

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpAoffice.SelectedValue = li.Value
                            End If

                        End If
                        drpAoffice.Enabled = False
                    End If
                End If
            End If

            If hdAdvanceSearch.Value = "1" Then
                drpBackupOnlineStatus.Items.Clear()
                objeAAMS.BindDropDown(drpBackupOnlineStatus, "ONLINESTATUSCODE", True, 3)
                If Request.Form("drpBackupOnlineStatus") <> "" Then
                    drpBackupOnlineStatus.SelectedValue = Request.Form("drpBackupOnlineStatus")
                End If

                drpOnlineStatus.Items.Clear()
                objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUSCODE", True, 3)
                If Request.Form("drpOnlineStatus") <> "" Then
                    drpOnlineStatus.SelectedValue = Request.Form("drpOnlineStatus")
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            If Request.QueryString("EncodedChainCode") IsNot Nothing Then
                txtChainCode.Text = objED.Decrypt(Request.QueryString("EncodedChainCode").ToString)
                txtChainCode.Enabled = False
            End If
        End Try
    End Sub
#End Region

#Region "BindAllControl()"
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(drpCRS, "CRS", True, 3)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", True, 3)
            objeAAMS.BindDropDown(drpPriority, "PRIORITY", True, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
#End Region

#Region "btnSearch_Click()"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

#Region "AgencySearch()"
    Private Sub AgencySearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzAgency As New AAMS.bizSales.bzSalesEmployee

        Try
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><PREDATE/><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE_FROM></DATE_ONLINE_FROM><DATE_ONLINE_TO></DATE_ONLINE_TO><DATE_OFFLINE_FROM></DATE_OFFLINE_FROM><DATE_OFFLINE_TO></DATE_OFFLINE_TO><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE><CHAIN_CODE></CHAIN_CODE> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><AgencyUsingBirdres/><WholeGroup/><COMP_VERTICAL></COMP_VERTICAL><MANAGER></MANAGER></TA_SEARCHAGENCY_INPUT>")


            ' Attribute for Birdres agency
            objInputXml.DocumentElement.SelectSingleNode("AgencyUsingBirdres").InnerText = chkAgencyUsingBirdres.Checked.ToString
            objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = objeAAMS.ConvertTextDate(Request.QueryString("PREDATE").ToString) '"20110509"


            objInputXml.DocumentElement.SelectSingleNode("NAME").InnerText = txtAgencyName.Text
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText = txtShortName.Text
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpCity.SelectedItem.Text
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Country_Name").InnerText = drpCountry.SelectedItem.Text
            End If

            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = Request.Form("drpOnlineStatus")
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeId.Text
            objInputXml.DocumentElement.SelectSingleNode("Crs").InnerText = drpCRS.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SearchType").InnerText = drpSearchType.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = txtLcode.Text
            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text

            ''ASHISH
            If Request.QueryString("IsManager") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("MANAGER").InnerText = Request.QueryString("IsManager").ToString
            End If
            ''END

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objeAAMS.Limited_To_Aoffice(Session("Security"))
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objeAAMS.Limited_To_Region(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then

                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = 0
                End If
            End If
            If chkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WholeGroup").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WholeGroup").InnerText = "FALSE"
            End If
            objInputXml.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A").InnerText = txtAResponsibility.Text

            If (hdAdvanceSearch.Value = "1") Then
                objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = txtAddress.Text
                objInputXml.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText = drpAgencyStatus.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = drpAgencyType.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("EMAIL").InnerText = txtEmail.Text
                'objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOnline.Text)
                'objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOffline.Text)


                objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE_FROM").InnerText = objeAAMS.ConvertTextDate(txtDateOnlineF.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE_TO").InnerText = objeAAMS.ConvertTextDate(txtDateOnlineT.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE_FROM").InnerText = objeAAMS.ConvertTextDate(txtDateOfflineF.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE_TO").InnerText = objeAAMS.ConvertTextDate(txtDateOfflineT.Text)


                objInputXml.DocumentElement.SelectSingleNode("FAX").InnerText = txtFax.Text
                objInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = txtFielNumber.Text
                objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerText = txtIATAId.Text
                objInputXml.DocumentElement.SelectSingleNode("IPAddress").InnerText = txtIPAddress.Text
                objInputXml.DocumentElement.SelectSingleNode("Online_Status_BackUp").InnerText = Request.Form("drpBackupOnlineStatus") 'drpBackupOnlineStatus.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = drpPriority.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("PHONE").InnerText = txtPhone.Text
                objInputXml.DocumentElement.SelectSingleNode("WWW_ADDRESS").InnerText = txtWeb.Text
              
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
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
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

            If Request.QueryString("EncodedChainCode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text
            End If

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If


            objOutputXml = objbzAgency.Search(objInputXml)

            Try
                objOutputXml.Save("c:\UnplannedAgencyOut.xml")
                objInputXml.Save("c:\UnplannedAgencyIn.xml")
            Catch ex As Exception

            End Try

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdAgency.DataSource = ds.Tables("AGENCY")
                grdAgency.DataBind()
                '@ Code Added For Paging And Sorting In case Of Delete The Record

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
                txtRecordOnCurrentPage.Text = ds.Tables("AGENCY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName").ToString()
                '    Case "CHAIN_CODE"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                '    Case "LOCATION_CODE"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "OfficeID"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                '    Case "NAME"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(3).Controls.Add(imgDown)

                '        End Select
                '    Case "ADDRESS"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                '    Case "CITY"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(5).Controls.Add(imgDown)

                '        End Select
                '    Case "COUNTRY"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(6).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(6).Controls.Add(imgDown)
                '        End Select
                '    Case "ONLINE_STATUS"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdAgency.HeaderRow.Cells(7).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgency.HeaderRow.Cells(7).Controls.Add(imgDown)

                '        End Select
                'End Select
                ' @ Added Code To Show Image'
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                SetImageForSorting(grdAgency)
            Else
                txtTotalRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                grdAgency.DataSource = Nothing
                grdAgency.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                If ViewState("SortName").ToString().Trim() <> "COLORCODE" Then
                    Dim intcol As Integer = grd.Columns.IndexOf(field)
                    If ViewState("Desc") = "FALSE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If

            End If
        Next
    End Sub
    Protected Sub grdAgency_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAgency.RowCommand

        Dim objOutputXml As New XmlDocument
        Dim objsalesDSRNode, objsalesDSRNodeClone As XmlNode
        Try
            If e.CommandName = "SelectData" Then
                Dim strCommandArument As String = e.CommandArgument
                If Session("AgencyDSRLOGXML") IsNot Nothing Then
                    objOutputXml.LoadXml(Session("AgencyDSRLOGXML").ToString)
                    If objOutputXml.DocumentElement.SelectNodes("DSRLOG").Count = 0 Then
                        objOutputXml.LoadXml("<SL_SEARCH_LOG_DSR_OUTPUT><DSRLOG ROWID='' DSR_VISIT_ID='' PREDATE='' AGENCY_NAME='' ADDRESS=''	LCODE='' OFFICEID='' DESIGNATION='' PERSONMET='' PENDINGISSUES='' BCCOMMIT='' 	BCMINSEGMENT='' MANAGERID='' MANAGERNAME='' HODID='' HODNAME='' CITYID='' CITY_NAME='' 	CHAIN_CODE='' A1DAILYMOTIVES='' MIDT='' BIDT='' VISITED='' REASON_REMARKS=''  OBJ_VISITCOUNT='' PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' VISIT_TYPE_NAME='' DISCUSSION_ISSUE_REMARKS='' COMPETITION_MKT_INFO_REMARKS='' STATUS_NAME='' FOLLOWUP_REMARKS='' PREV_REMARKS1='' 	PREV_REMARKS1DATE='' PREV_REMARKS2DATE='' PREV_REMARKS2='' INTIME='' OUTTIME='' GROUP_CATG_NAME='' VISITCOUNT='' LOGDATE='' RESP_NAME='' RESP_1A='' UNPLANVISIT='' COLORCODE=''  /> <Errors Status='FALSE'>	<Error Code='' Description='' /></Errors> 	<PAGE PAGE_COUNT='1' TOTAL_ROWS='1' /></SL_SEARCH_LOG_DSR_OUTPUT>")
                    End If
                Else
                    objOutputXml.LoadXml("<SL_SEARCH_LOG_DSR_OUTPUT><DSRLOG ROWID='' DSR_VISIT_ID='' PREDATE='' AGENCY_NAME='' ADDRESS=''	LCODE='' OFFICEID='' DESIGNATION='' PERSONMET='' PENDINGISSUES='' BCCOMMIT='' 	BCMINSEGMENT='' MANAGERID='' MANAGERNAME='' HODID='' HODNAME='' CITYID='' CITY_NAME='' 	CHAIN_CODE='' A1DAILYMOTIVES='' MIDT='' BIDT='' VISITED='' REASON_REMARKS=''  OBJ_VISITCOUNT='' PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' VISIT_TYPE_NAME='' DISCUSSION_ISSUE_REMARKS='' COMPETITION_MKT_INFO_REMARKS='' STATUS_NAME='' FOLLOWUP_REMARKS='' PREV_REMARKS1='' 	PREV_REMARKS1DATE='' PREV_REMARKS2DATE='' PREV_REMARKS2='' INTIME='' OUTTIME='' GROUP_CATG_NAME='' VISITCOUNT='' LOGDATE='' RESP_NAME='' RESP_1A='' UNPLANVISIT='' COLORCODE=''  /> <Errors Status='FALSE'>	<Error Code='' Description='' /></Errors> 	<PAGE PAGE_COUNT='1' TOTAL_ROWS='1' /></SL_SEARCH_LOG_DSR_OUTPUT>")
                End If

                objsalesDSRNode = objOutputXml.DocumentElement.SelectSingleNode("DSRLOG")
                objsalesDSRNodeClone = objsalesDSRNode.CloneNode(True)

                For Each objAttr As XmlAttribute In objsalesDSRNodeClone.Attributes
                    objAttr.Value = ""
                Next
                ' <asp:LinkButton ID="lnkSelect"  runat="server" CssClass="LinkButtons" CommandName ="SelectData" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LOCATION_CODE") + "|" + DataBinder.Eval(Container.DataItem, "CHAIN_CODE")+ "|" + DataBinder.Eval(Container.DataItem, "OfficeID") + "|" + DataBinder.Eval(Container.DataItem, "NAME") + "|" + DataBinder.Eval(Container.DataItem, "CITY")+ "|" + DataBinder.Eval(Container.DataItem,"COUNTRY") %>'>Select</asp:LinkButton>

                With objsalesDSRNodeClone
                    .Attributes("ROWID").Value = objOutputXml.DocumentElement.SelectNodes("DSRLOG").Count + 1
                    .Attributes("DSR_VISIT_ID").Value = ""
                    .Attributes("PREDATE").Value = objeAAMS.ConvertTextDate(Request.QueryString("PREDATE").ToString)
                    .Attributes("LCODE").Value = strCommandArument.Split("|")(0).ToString
                    .Attributes("CHAIN_CODE").Value = strCommandArument.Split("|")(1).ToString
                    .Attributes("OFFICEID").Value = strCommandArument.Split("|")(2).ToString
                    .Attributes("AGENCY_NAME").Value = strCommandArument.Split("|")(3).ToString
                    .Attributes("CITY_NAME").Value = strCommandArument.Split("|")(4).ToString
                    .Attributes("ADDRESS").Value = strCommandArument.Split("|")(6).ToString

                    'DSRLOG ROWID='1' DSR_VISIT_ID='' PREDATE='12-May-2011' AGENCY_NAME='Abc' ADDRESS='A/21 hjsd fhjsd sdh sdffds sdf'
                    '		LCODE='2' OFFICEID='DELBR0006' DESIGNATION='rt' PERSONMET='' PENDINGISSUES='' BCCOMMIT='2'
                    '		BCMINSEGMENT='2' MANAGERID='33' MANAGERNAME='Admin' HODID='' HODNAME='' CITYID='' CITY_NAME='DELHI'
                    '		CHAIN_CODE='2' A1DAILYMOTIVES='323' MIDT='3333' BIDT='33' VISITED='' REASON_REMARKS=''
                    '		OBJ_VISITCOUNT='' PLAN_VISIT_DONE='' UNPLAN_VISIT_DONE='' VISIT_TYPE_NAME='' DISCUSSION_ISSUE_REMARKS=''
                    '		COMPETITION_MKT_INFO_REMARKS='' STATUS_NAME='' FOLLOWUP_REMARKS='' PREV_REMARKS1=''
                    '		PREV_REMARKS1DATE='' PREV_REMARKS2DATE='' PREV_REMARKS2='' INTIME='11:10' OUTTIME='15:10'
                    '		GROUP_CATG_NAME='A' VISITCOUNT='2' LOGDATE='' RESP_NAME='' RESP_1A='24' UNPLANVISIT='FALSE'
                    '  Dim HdBIDT, HdMIDT, HdMinSeg, HdMotive, HdBCCommit, HdRes1A, HdResName, Grp_Type_Ctg As HiddenField
                    .Attributes("BIDT").Value = strCommandArument.Split("|")(7).ToString
                    .Attributes("MIDT").Value = strCommandArument.Split("|")(8).ToString
                    .Attributes("BCMINSEGMENT").Value = strCommandArument.Split("|")(9).ToString
                    .Attributes("A1DAILYMOTIVES").Value = strCommandArument.Split("|")(10).ToString
                    .Attributes("BCCOMMIT").Value = strCommandArument.Split("|")(11).ToString
                    ''If Not Session("LoginSession") Is Nothing Then
                    '.Attributes("RESP_1A").Value = Session("LoginSession").ToString().Split("|")(12)
                    ''  End If

                    .Attributes("RESP_1A").Value = strCommandArument.Split("|")(12).ToString
                    .Attributes("RESP_NAME").Value = strCommandArument.Split("|")(13).ToString

                    .Attributes("GROUP_CATG_NAME").Value = strCommandArument.Split("|")(14).ToString
                    .Attributes("OBJ_VISITCOUNT").Value = strCommandArument.Split("|")(15).ToString
                    .Attributes("CITYID").Value = strCommandArument.Split("|")(16).ToString

                    .Attributes("INTIME").Value = ""
                    .Attributes("OUTTIME").Value = ""

                    .Attributes("UNPLANVISIT").Value = "TRUE"
                    .Attributes("PLAN_VISIT_DONE").Value = "0"

                    .Attributes("VISITED").Value = "Y"

                    If .Attributes("VISITED").Value = "Y" Then
                        .Attributes("UNPLAN_VISIT_DONE").Value = "1"
                    Else
                        .Attributes("UNPLAN_VISIT_DONE").Value = "0"
                    End If

                    .Attributes("COLORCODE").Value = strCommandArument.Split("|")(17).ToString

                End With

                If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@LCODE='" + objsalesDSRNodeClone.Attributes("LCODE").Value + "']").Count > 0 Then
                    lblError.Text = "[ " + objsalesDSRNodeClone.Attributes("AGENCY_NAME").Value + " ]" + " agency already added in DSR filing."
                    Exit Sub
                End If

                objOutputXml.DocumentElement.AppendChild(objsalesDSRNodeClone)
                objsalesDSRNodeClone = objsalesDSRNode.CloneNode(True)
                If objOutputXml.DocumentElement.SelectNodes("DSRLOG[@ROWID='']").Count > 0 Then
                    objOutputXml.DocumentElement.RemoveChild(objOutputXml.DocumentElement.SelectSingleNode("DSRLOG[@ROWID='']"))
                End If

                Dim RowNum As Integer = 1

                For Each objnode As XmlNode In objOutputXml.DocumentElement.SelectNodes("DSRLOG")
                    objnode.Attributes("ROWID").Value = RowNum
                    RowNum = RowNum + 1
                Next

                If objOutputXml.DocumentElement.SelectNodes("DSRLOG").Count > 0 Then
                    Session("AgencyDSRLOGXML") = objOutputXml.OuterXml
                End If


                ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.opener.document.forms['form1']['BtnAppendUnplannedData'].click(); window.close();", True)

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdAgency_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAgency.RowDataBound
        Try
            Dim i As Integer
            Dim strChain_Code As String
            If e.Row.RowType = DataControlRowType.Header Then
            End If
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            ' Code added For Secting an Items 

            'Dim HdBIDT, HdMIDT, HdMinSeg, HdMotive, HdBCCommit, HdRes1A, HdResName, HdGrp_Type_Ctg, HdObjCount As HiddenField
            'HdBIDT = CType(e.Row.FindControl("HdBIDT"), HiddenField)
            'HdMIDT = CType(e.Row.FindControl("HdMIDT"), HiddenField)
            'HdMinSeg = CType(e.Row.FindControl("HdMinSeg"), HiddenField)
            'HdMotive = CType(e.Row.FindControl("HdMotive"), HiddenField)
            'HdBCCommit = CType(e.Row.FindControl("HdBCCommit"), HiddenField)
            'HdRes1A = CType(e.Row.FindControl("HdRes1A"), HiddenField)
            'HdResName = CType(e.Row.FindControl("HdResName"), HiddenField)
            'HdGrp_Type_Ctg = CType(e.Row.FindControl("HdGrp_Type_Ctg"), HiddenField)
            'HdObjCount = CType(e.Row.FindControl("HdObjCount"), HiddenField)

            Dim StrBIDT, StrMIDT, StrMinSeg, StrMotive, StrBCCommit, StrRes1A, StrResName, StrGrp_Type_Ctg, StrObjCount, StrCityId As String
            Dim HdSelected As HiddenField
            StrBIDT = DataBinder.Eval(e.Row.DataItem, "BIDT")
            StrMIDT = DataBinder.Eval(e.Row.DataItem, "MIDT")
            StrMinSeg = DataBinder.Eval(e.Row.DataItem, "BCMINSEGMENT")
            StrMotive = DataBinder.Eval(e.Row.DataItem, "A1DAILYMOTIVES")
            StrBCCommit = DataBinder.Eval(e.Row.DataItem, "BCCOMMIT")
            StrRes1A = DataBinder.Eval(e.Row.DataItem, "RESP_1A")
            StrResName = DataBinder.Eval(e.Row.DataItem, "RESP_NAME")
            StrGrp_Type_Ctg = DataBinder.Eval(e.Row.DataItem, "GROUP_CATG_NAME")
            StrObjCount = DataBinder.Eval(e.Row.DataItem, "OBJ_VISITCOUNT")
            StrCityId = DataBinder.Eval(e.Row.DataItem, "CITYID")

            HdSelected = CType(e.Row.FindControl("HdSelected"), HiddenField)

            Dim hdColorCode As HiddenField
            hdColorCode = CType(e.Row.FindControl("hdColorCode"), HiddenField)

            Dim lnkSelect As LinkButton
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing And 1 = 2 Then
                lnkSelect.Visible = False
            Else
                Dim str() As String = lnkSelect.CommandArgument.Split("|")
                For i = 0 To str.Length - 1
                    str(i) = str(i).Replace("'", "")
                    If i = 6 Then
                        str(i) = str(i).Replace(vbCrLf, "\n")
                    Else
                        ' str(i) = Server.UrlEncode(str(i))
                    End If
                    If i = 0 Then
                        lnkSelect.CommandArgument = str(i)
                    Else
                        lnkSelect.CommandArgument += "|" + str(i)
                    End If
                Next
                Dim strAddress As String
                strAddress = DataBinder.Eval(e.Row.DataItem, "Address")
                strChain_Code = DataBinder.Eval(e.Row.DataItem, "CHAIN_CODE")
                strAddress = strAddress.Replace(vbCrLf, "\n")
                strAddress = strAddress.Replace("'", "")
                lnkSelect.Visible = True
                If HdSelected.Value.Trim.ToUpper = "Y" Then
                    lnkSelect.Enabled = False
                End If

                Dim StrNEWLOGDATETIME As String = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss") '"yyyy-MM-dd HH:mm:ss")
                ' lnkSelect.Attributes.Add("OnClick", "return SelectFunctionTravelAgency('" + lnkSelect.CommandArgument + "','" + strAddress + "','" + StrNEWLOGDATETIME + "');")
                'lnkSelect.CommandArgument = lnkSelect.CommandArgument + "|" + strAddress + "|" + HdBIDT.Value + "|" + HdMIDT.Value + "|" + HdMinSeg.Value + "|" + HdMotive.Value + "|" + HdBCCommit.Value + "|" + HdRes1A.Value + "|" + HdResName.Value + "|" + HdGrp_Type_Ctg.Value + "|" + HdObjCount.Value

                lnkSelect.CommandArgument = lnkSelect.CommandArgument + "|" + strAddress + "|" + StrBIDT + "|" + StrMIDT + "|" + StrMinSeg + "|" + StrMotive + "|" + StrBCCommit + "|" + StrRes1A + "|" + StrResName + "|" + StrGrp_Type_Ctg + "|" + StrObjCount + "|" + StrCityId + "|" + hdColorCode.Value.Trim



                '@ Start For Colorcode Implementation
             
                Dim ImgColorCode As Image
                ImgColorCode = CType(e.Row.FindControl("ImgColorCode"), Image)
                If hdColorCode.Value.Trim.ToUpper = "RED" Then
                    ImgColorCode.ImageUrl = "~/Images/Redflag.gif"
                ElseIf hdColorCode.Value.Trim.ToUpper = "PINK" Then
                    ImgColorCode.ImageUrl = "~/Images/Pinkflag.gif"
                Else
                    ImgColorCode.Visible = False
                End If
                '@ End For Colorcode Implementation

            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdAgency_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles grdAgency.RowDeleted

    End Sub


    Protected Sub grdAgency_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAgency.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdAgency_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAgency.Sorting
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
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

   

    Protected Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Dim str As String
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim objOutputXml As New XmlDocument
        Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
        objOutputXml = objTaProducts.List()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            str = objOutputXml.OuterXml
        End If
    End Sub
End Class

