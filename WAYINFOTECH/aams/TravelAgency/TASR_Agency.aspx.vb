Partial Class TravelAgency_MSSR_Agency
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler


#Region "Global variables declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
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

#Region "Page_Load()"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            lblError.Text = ""
            objeAAMS.ExpirePageCache()
            btnSearch.Attributes.Add("onclick", "return CheckValidationTravelAgency();")
            btnExport.Attributes.Add("onclick", "return CheckValidationTravelAgency();")

            If Not Page.IsPostBack Then
                ' Code For Agency Using Birdres Start
                If Request.QueryString("HelpDeskType") IsNot Nothing Then
                    If Request.QueryString("HelpDeskType") = "BR" Then
                        chkAgencyUsingBirdres.Checked = True
                    End If
                End If
                ' Code End
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

            If hdDeleteFlag.Value <> "" Then
                AgencyDelete(hdDeleteFlag.Value)
            End If

            If Not Page.IsPostBack Then
                hdAdvanceSearch.Value = "0"
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

        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            'objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TA_SEARCHAGENCY_INPUT>")
            'objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE><CHAIN_CODE></CHAIN_CODE> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><AgencyUsingBirdres/><WholeGroup/><COMP_VERTICAL></COMP_VERTICAL></TA_SEARCHAGENCY_INPUT>")
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE_FROM></DATE_ONLINE_FROM><DATE_ONLINE_TO></DATE_ONLINE_TO><DATE_OFFLINE_FROM></DATE_OFFLINE_FROM><DATE_OFFLINE_TO></DATE_OFFLINE_TO><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE><CHAIN_CODE></CHAIN_CODE> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><AgencyUsingBirdres/><WholeGroup/><COMP_VERTICAL></COMP_VERTICAL></TA_SEARCHAGENCY_INPUT>")


            ' Attribute for Birdres agency
            objInputXml.DocumentElement.SelectSingleNode("AgencyUsingBirdres").InnerText = chkAgencyUsingBirdres.Checked.ToString
           

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
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A").InnerText = txtAResponsibility.Text
    
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



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdAgency.DataSource = ds.Tables("AGNECY")
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
                txtRecordOnCurrentPage.Text = ds.Tables("AGNECY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "CHAIN_CODE"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "LOCATION_CODE"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "NAME"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(3).Controls.Add(imgDown)

                        End Select
                    Case "ADDRESS"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "ADDRESS1"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select

                    Case "COMP_VERTICAL"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select


                    Case "CITY"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(7).Controls.Add(imgDown)

                        End Select
                    Case "COUNTRY"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "ONLINE_STATUS"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdAgency.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdAgency.HeaderRow.Cells(9).Controls.Add(imgDown)

                        End Select
                End Select
                ' @ Added Code To Show Image'
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record

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

#Region "AgencyDelete()"
    Private Sub AgencyDelete(ByVal strLCODE As String)
        Try
            Dim objInputXml As New XmlDocument
            Dim objOututXml As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objInputXml.LoadXml("<TA_DELETEAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_DELETEAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = strLCODE
            'Here Back end Method Call
            '
            hdDeleteFlag.Value = "" 'it contains deleteid
            objOututXml = objbzAgency.Delete(objInputXml)
            AgencySearch()
            If objOututXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

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
                lblError.Text = objOututXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Response.Redirect("TAUP_Agency.aspx?Action=I")
    End Sub

    Protected Sub grdAgency_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAgency.RowCommand
        Dim str As String = ""
        Dim str1 As String = ""
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

            Dim lnkSelect As LinkButton
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
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
                Dim strPincode As String
                Dim strEmail As String
                Dim strContactPerson As String
                Dim strContactPersonID As String
                Dim strCityID As String
                Dim strCountryID As String
                Dim strAddress As String
                Dim strAddress1 As String
                Dim Comp_Vertical As String

                strCityID = DataBinder.Eval(e.Row.DataItem, "CITYID")
                strCountryID = DataBinder.Eval(e.Row.DataItem, "COUNTRYID")

                strContactPerson = DataBinder.Eval(e.Row.DataItem, "CONTACT_PERSON")
                strContactPersonID = DataBinder.Eval(e.Row.DataItem, "CONTACT_PERSON_ID")
                strPincode = DataBinder.Eval(e.Row.DataItem, "PINCODE")
                strEmail = DataBinder.Eval(e.Row.DataItem, "Email")
                strAddress = DataBinder.Eval(e.Row.DataItem, "Address")
                strAddress1 = DataBinder.Eval(e.Row.DataItem, "Address1")
                strChain_Code = DataBinder.Eval(e.Row.DataItem, "CHAIN_CODE")
                Comp_Vertical = DataBinder.Eval(e.Row.DataItem, "COMP_VERTICAL")

                strContactPerson = strContactPerson.Replace("'", "\'")
                strAddress = strAddress.Replace(vbCrLf, "\n")
                strAddress = strAddress.Replace("'", "")
                strAddress1 = strAddress1.Replace(vbCrLf, "\n")
                strAddress1 = strAddress1.Replace("'", "")
                lnkSelect.Visible = True
                If strChain_Code = "2145" Then
                    strAddress1 = strAddress1 + "**"
                End If
                Dim StrNEWLOGDATETIME As String = DateAndTime.Now.ToString("dd/MM/yyyy HH:mm:ss") '"yyyy-MM-dd HH:mm:ss")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunctionTravelAgency('" + lnkSelect.CommandArgument & "|" & strPincode & "|" & strEmail & "|" & strContactPersonID & "|" & strContactPerson & "|" & strCityID & "|" & strCountryID + "','" + strAddress + "','" + strAddress1 + "','" + StrNEWLOGDATETIME + "','" + Comp_Vertical + "');")
            End If
            Dim strBuilder As New StringBuilder

            Dim btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
            Dim btnDelete As LinkButton

            Dim s As String
            Dim strCity As String
            Dim strLCode As String


            btnDelete = e.Row.FindControl("btnDelete")
            btnEdit = CType(e.Row.FindControl("btnEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
            strCity = DataBinder.Eval(e.Row.DataItem, "CITY")
            strLCode = DataBinder.Eval(e.Row.DataItem, "LOCATION_CODE")


            Dim objSecurityXml As New XmlDocument

            objSecurityXml.LoadXml(Session("Security"))
            's = hdlblLocationCode.Value & "|" & cityval.Value
            s = objED.Encrypt(strLCode) & "|" & strCity
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Attributes.Add("OnClick", "return DeleteFunctionTravelAgency('" & strLCode & "');")
                End If
                If strBuilder(2) = "0" Then
                    '    btnEdit.Disabled = False
                    'Else
                    '  btnEdit.Attributes.Add("OnClick", "return EditFunction('" & s & "');")
                End If

                btnEdit.Attributes.Add("OnClick", "return EditFunctionTravelAgency('" & s & "');")
            Else
                btnDelete.Attributes.Add("OnClick", "return DeleteFunctionTravelAgency('" & strLCode & "');")
                btnEdit.Attributes.Add("OnClick", "return EditFunctionTravelAgency('" & s & "');")
                ' strBuilder = objeAAMS.SecurityCheck(31)
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

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objExport As New ExportExcel
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            ' objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TA_SEARCHAGENCY_INPUT>")
            '   objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE><CHAIN_CODE></CHAIN_CODE><AgencyUsingBirdres/> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><WholeGroup></WholeGroup><COMP_VERTICAL></COMP_VERTICAL></TA_SEARCHAGENCY_INPUT>")

            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE_FROM></DATE_ONLINE_FROM><DATE_ONLINE_TO></DATE_ONLINE_TO><DATE_OFFLINE_FROM></DATE_OFFLINE_FROM><DATE_OFFLINE_TO></DATE_OFFLINE_TO><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID> <IPAddress></IPAddress><SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE><CHAIN_CODE></CHAIN_CODE> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><AgencyUsingBirdres/><WholeGroup/><COMP_VERTICAL></COMP_VERTICAL></TA_SEARCHAGENCY_INPUT>")

            ' Attribute for Birdres agency
            objInputXml.DocumentElement.SelectSingleNode("AgencyUsingBirdres").InnerText = chkAgencyUsingBirdres.Checked.ToString

            objInputXml.DocumentElement.SelectSingleNode("NAME").InnerText = Trim(txtAgencyName.Text)
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText = Trim(txtShortName.Text)
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = Trim(drpCity.SelectedItem.Text)
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Country_Name").InnerText = Trim(drpCountry.SelectedItem.Text)
            End If

            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = Request.Form("drpOnlineStatus")
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Trim(drpAoffice.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = Trim(txtOfficeId.Text)
            objInputXml.DocumentElement.SelectSingleNode("Crs").InnerText = Trim(drpCRS.SelectedValue)

            '@ New Code Added
            objInputXml.DocumentElement.SelectSingleNode("SearchType").InnerText = drpSearchType.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = txtLcode.Text

            objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerText = txtChainCode.Text

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then

                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = 0
                End If
                'End If


            End If

            If (hdAdvanceSearch.Value = "1") Then
                ' pankaj
                'Session("hdAdvanceSearch") = "1"
                objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = Trim(txtAddress.Text)
                objInputXml.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText = Trim(drpAgencyStatus.SelectedValue)
                objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = Trim(drpAgencyType.SelectedValue)
                objInputXml.DocumentElement.SelectSingleNode("EMAIL").InnerText = Trim(txtEmail.Text)
                'objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOnline.Text)
                'objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOffline.Text)


                objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE_FROM").InnerText = objeAAMS.ConvertTextDate(txtDateOnlineF.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE_TO").InnerText = objeAAMS.ConvertTextDate(txtDateOnlineT.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE_FROM").InnerText = objeAAMS.ConvertTextDate(txtDateOfflineF.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE_TO").InnerText = objeAAMS.ConvertTextDate(txtDateOfflineT.Text)


                objInputXml.DocumentElement.SelectSingleNode("FAX").InnerText = Trim(txtFax.Text)
                objInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = Trim(txtFielNumber.Text)
                objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerText = Trim(txtIATAId.Text)

                objInputXml.DocumentElement.SelectSingleNode("IPAddress").InnerText = txtIPAddress.Text

                '   <SearchType></SearchType><Online_Status_BackUp></Online_Status_BackUp><PRIORITYID></PRIORITYID><PHONE></PHONE><WWW_ADDRESS></WWW_ADDRESS><RESPONSIBILITY_1A></RESPONSIBILITY_1A><LOCATION_CODE></LOCATION_CODE>

                objInputXml.DocumentElement.SelectSingleNode("Online_Status_BackUp").InnerText = Request.Form("drpBackupOnlineStatus")

                objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = drpPriority.SelectedValue

                objInputXml.DocumentElement.SelectSingleNode("PHONE").InnerText = txtPhone.Text

                objInputXml.DocumentElement.SelectSingleNode("WWW_ADDRESS").InnerText = txtWeb.Text

                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBILITY_1A").InnerText = txtAResponsibility.Text
               
            Else
                'Session("hdAdvanceSearch") = "0"
            End If
            'Start CODE for sorting and paging

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

            If chkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("WholeGroup").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("WholeGroup").InnerText = "FALSE"
            End If

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If



            objOutputXml = objbzAgency.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                'objXmlReader = New XmlNodeReader(objOutputXml)
                'ds.ReadXml(objXmlReader)
                'grdAgency.DataSource = ds.Tables("AGNECY")
                'grdAgency.DataBind()

                '<AGNECY LOCATION_CODE="26986" CHAIN_CODE="11610" OfficeID=""
                ' NAME="Aakash Ganga Travels" ADDRESS="Township Nagar,Tripura(W), Nr. Mbb Club House"
                ' ADDRESS1="House Property Of Mayur Hotel," CITY="Agartala" COUNTRY="India"
                ' PHONE="9831059075" FAX="" ONLINE_STATUS="" Aoffice="CCU" PINCODE="" Email=""
                ' CONTACT_PERSON="Mr. Raja Bank" CONTACT_PERSON_ID="11360286" CITYID="114" COUNTRYID="1" /> 
                '                '  <AGNECY LOCATION_CODE="15497" CHAIN_CODE="11597" 
                'OfficeID="IXAVS3110" NAME="Agartala Travels" ADDRESS="Near Agartala Hospital" 
                'ADDRESS1="9a Mantri Bari Road, Agartala" CITY="Agartala" COUNTRY="India"
                ' PHONE="03812319257" FAX="0381-2310275" ONLINE_STATUS="VSAT" Aoffice="CCU"
                ' PINCODE="799001" Email="" CONTACT_PERSON="sujit" CONTACT_PERSON_ID="1425447" 
                'CITYID="114" COUNTRYID="1" /> 

                '                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Chain Code", "Lcode", "Office Id", "Name", "Address", "Address1", "Company Vertical", "City", "Country", "Online Status"}
                Dim intArray() As Integer = {1, 0, 2, 3, 4, 5, 18, 6, 7, 10}
                objExport.ExportDetails(objOutputXml, "AGNECY", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAgencyDetails.xls")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

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
