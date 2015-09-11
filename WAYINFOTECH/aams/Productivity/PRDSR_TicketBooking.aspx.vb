'###########################################################################
'############   Page Name -- Productivity/PRDSR_TicketBooking      #########  
'############   Date 28-May 2010           #################################
'############   Developed By Abhishek  #####################################
'############   Developed By Abhishek  #####################################
'###########################################################################
Partial Class Productivity_PRDSR_TicketBooking
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim FooterDataset As DataSet
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            FooterDataset = New DataSet
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            txtRecordOnCurrentPage.Text = ""
           
            dlstCity.Attributes.Add("onkeyup", "return gotops('dlstCity');")
            dlstCountry.Attributes.Add("onkeyup", "return gotops('dlstCountry');")
            drpAgencyStatus.Attributes.Add("onkeyup", "return gotops('drpAgencyStatus');")

            drpAgencyType.Attributes.Add("onkeyup", "return gotops('drpAgencyType');")
            drp1AOffice.Attributes.Add("onkeyup", "return gotops('drp1AOffice');")
            drpRegion.Attributes.Add("onkeyup", "return gotops('drpRegion');")
            drpResStaff.Attributes.Add("onkeyup", "return gotops('drpResStaff');")
            drpProductivity.Attributes.Add("onkeyup", "return gotops('drpProductivity');")
            drpLstGroupType.Attributes.Add("onkeyup", "return gotops('drpLstGroupType');")
            drpGroupAgencyType.Attributes.Add("onkeyup", "return gotops('drpGroupAgencyType');")
        
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            drpProductivity.Attributes.Add("onchange", "return SelectProdutivity();")
            drpProductivity.Attributes.Add("onclick", "return SelectProdutivity();")

            ChkAirBreackup.Attributes.Add("onclick", "return CheckedUnchecked1AProd();")

            ''Added by Tapan Nath for LCODE, & Chain Code 18/03/2011
            txtLcode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtChainCode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtAgencyName.Attributes.Add("onfocusout", "return ActDecLcodeChainCode();")
            ''Added by Tapan Nath for LCODE, & Chain Code

            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Ticket']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Ticket']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            btnSearch.Enabled = False
                            Response.Redirect("~/NoRights.aspx")
                            Exit Sub
                        End If
                        If strBuilder(4) = "0" Then
                            btnExport.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If


                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            Chk1AProd.Checked = False
                            Chk1AProd.Enabled = False
                            Hd1AprodRight.value = "0"
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If

                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkDailyBook.Enabled = False
                            ChkDailyBook.Checked = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If


                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkAllCRS.Checked = False
                            ChkAllCRS.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If

                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CODD & HX']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CODD & HX']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            chkNIDT.Visible = False
                            chkNIDT.Checked = False
                            chkNIDT.Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
               
            End If
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onclick", "return CheckValidation();")
                btnExport.Attributes.Add("onclick", "return CheckValidation();")
                
                BindAllControl()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

#Region "btnSearch_Click()"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            TicketSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region
    'Method for search TicketSearch
#Region "TicketSearch"
    Private Sub TicketSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            objInputXml.LoadXml("<PR_SEARCH_1ATICKET_INPUT> <CITY></CITY> <COUNTRY></COUNTRY>  <LCODE></LCODE> <AGENCYNAME></AGENCYNAME>  <SHOWGROUP></SHOWGROUP> <RESP_1A></RESP_1A> <RESPONSIBLESTAFF></RESPONSIBLESTAFF><AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID>  <GROUPTYPEID></GROUPTYPEID> <AOFFICE></AOFFICE> <PRODUCITIVITY_TYPE></PRODUCITIVITY_TYPE> <PRODUCITIVITY_FROM></PRODUCITIVITY_FROM> <PRODUCITIVITY_TO></PRODUCITIVITY_TO>  <REGION></REGION> <AIRLINE_CODE></AIRLINE_CODE> <MONTHLYBREAKUP></MONTHLYBREAKUP> <A_TICKET></A_TICKET> <DAILYBOOKING></DAILYBOOKING> <A_PRODUCTIVITY></A_PRODUCTIVITY> <ALL_CRS></ALL_CRS> <NIDT></NIDT> <AIRBREAKUP></AIRBREAKUP> <DATEFROM></DATEFROM> <DATETO></DATETO> <TYPEID></TYPEID><PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_1ATICKET_INPUT>")

            '<PR_SEARCH_1ATICKET_INPUT>
            ' <CITY></CITY>
            ' <COUNTRY></COUNTRY> 
            ' <LCODE></LCODE>
            ' <AGENCYNAME></AGENCYNAME> 
            ' <SHOWGROUP></SHOWGROUP>
            ' <RESP_1A></RESP_1A>
            ' <RESPONSIBLESTAFF></RESPONSIBLESTAFF>
            ' <AGENCYSTATUSID></AGENCYSTATUSID>
            ' <AGENCYTYPEID></AGENCYTYPEID> 
            ' <GROUPTYPEID></GROUPTYPEID>
            ' <AOFFICE></AOFFICE>
            ' <PRODUCITIVITY_TYPE></PRODUCITIVITY_TYPE>
            ' <PRODUCITIVITY_FROM></PRODUCITIVITY_FROM>
            ' <PRODUCITIVITY_TO></PRODUCITIVITY_TO> 
            ' <REGION></REGION>
            ' <AIRLINE_CODE></AIRLINE_CODE> 
            ' <MONTHLYBREAKUP></MONTHLYBREAKUP>
            ' <A_TICKET></A_TICKET>
            ' <DAILYBOOKING></DAILYBOOKING>
            ' <A_PRODUCTIVITY></A_PRODUCTIVITY>
            ' <ALL_CRS></ALL_CRS>
            ' <AIRBREAKUP></AIRBREAKUP>
            ' <DATEFROM></DATEFROM>
            ' <DATETO></DATETO>
            ' <PAGE_NO></PAGE_NO>
            ' <PAGE_SIZE></PAGE_SIZE>
            ' <SORT_BY></SORT_BY>
            ' <DESC></DESC>
            '<COMP_VERTICAL></COMP_VERTICAL>
            '</PR_SEARCH_1ATICKET_INPUT>

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If
            If (dlstCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            If ChkGrpProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "FALSE"
            End If

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If (drpResStaff.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFF").InnerText = drpResStaff.SelectedValue
            End If
            If (drpAgencyStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue
            End If

            If (drpAgencyType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue
            End If
            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPTYPEID").InnerText = drpLstGroupType.SelectedItem.Value
            End If
            If (drp1AOffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1AOffice.SelectedValue
            End If

            If (drpProductivity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_TYPE").InnerText = Trim(drpProductivity.SelectedItem.Text)
            End If


            objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_FROM").InnerText = txtFrom.Text

            objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_TO").InnerText = txtTo.Text

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue
            End If

            If DlstAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = DlstAirline.SelectedValue.Trim()
            End If

            If RdShowMon.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
            End If


            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = drpYearFrom.SelectedValue.PadLeft(4, "0") + drpMonthFrom.SelectedValue.PadLeft(2, "0") + "01"
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = drpYearTo.SelectedValue.PadLeft(4, "0") + drpMonthTo.SelectedValue.PadLeft(2, "0") + "01"
         

            If ChkTicket.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("A_TICKET").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("A_TICKET").InnerText = "FALSE"
            End If
            If ChkDailyBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("DAILYBOOKING").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DAILYBOOKING").InnerText = "FALSE"
            End If
            If Chk1AProd.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("A_PRODUCTIVITY").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("A_PRODUCTIVITY").InnerText = "FALSE"
            End If
         
            If ChkAllCRS.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("ALL_CRS").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ALL_CRS").InnerText = "FALSE"
            End If

            If chkNIDT.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("NIDT").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("NIDT").InnerText = "FALSE"
            End If


            If ChkAirBreackup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""

            End If

            If drpGroupAgencyType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
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

            '  Start This Code is Added For ReSetting Viwstate for Sorting
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "MONTHLYBREAKUP" Or objNode.Name = "A_TICKET" Or objNode.Name = "DAILYBOOKING" Or objNode.Name = "A_PRODUCTIVITY" Or objNode.Name = "ALL_CRS" Or objNode.Name = "AIRBREAKUP" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            ViewState("SortName") = "LCODE" '"CHAIN_CODE"
                            ViewState("Desc") = "FALSE"
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If
            '   End of Code is Added For ReSetting Viwstate for Sorting

            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LCODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE"
            Else
                If ViewState("SortName") = "ONLINE_STATUS" Then
                    If ChkListStatus.Items(0).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "ADDRESS" Then
                    If ChkListStatus.Items(1).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "COUNTRY" Then
                    If ChkListStatus.Items(2).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "CHAIN_CODE" Then
                    If ChkListStatus.Items(3).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If

                If ViewState("SortName") = "GROUPTYPENAME" Then
                    If ChkListStatus.Items(4).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If

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

            objOutputXml = objbzbzBIDT.Search_1ATicket(objInputXml)
            Try
                objInputXml.Save("c:\Admin\InputTicketXml.xml")
                objOutputXml.Save("c:\Admin\OutputTicketXml.xml")
            Catch ex As Exception
            End Try

            'objOutputXml.Load("C:\PR_SEARCH_1ATICKET_OUTPUT.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvMonthlyBReak.DataSource = ds.Tables("A_TICKET")
                grdvMonthlyBReak.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvMonthlyBReak)
                '@ End of Code Added For Paging And Sorting 

                Dim TotalWidth As Int64
                TotalWidth = 0
                For intclmn As Integer = 0 To grdvMonthlyBReak.Columns.Count - 1
                    If grdvMonthlyBReak.HeaderRow.Cells(intclmn).Visible = True Then
                        TotalWidth = TotalWidth + grdvMonthlyBReak.Columns(intclmn).ItemStyle.Width.Value
                    End If
                Next
                grdvMonthlyBReak.Width = TotalWidth

            Else
                grdvMonthlyBReak.DataSource = Nothing
                grdvMonthlyBReak.DataBind()
                txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally

            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("drpProductivity") IsNot Nothing Then
                drpProductivity.SelectedValue = Request("drpProductivity")
            End If
            If Request("txtFrom") IsNot Nothing Then
                txtFrom.Text = Request("txtFrom")
            End If
            If Request("txtTo") IsNot Nothing Then
                txtTo.Text = Request("txtTo")
            End If
            If drpProductivity.SelectedValue = "" Then
                txtFrom.CssClass = "textboxgrey"
                txtTo.CssClass = "textboxgrey"
                txtFrom.Enabled = False
                txtTo.Enabled = False
            ElseIf drpProductivity.SelectedValue = "7" Then
                txtFrom.CssClass = "textbox"
                txtTo.CssClass = "textbox"
                txtFrom.Enabled = True
                txtTo.Enabled = True
            Else
                txtFrom.CssClass = "textbox"
                txtTo.CssClass = "textboxgrey"
                txtFrom.Enabled = True
                txtTo.Enabled = False
                txtTo.Text = ""
            End If
        End Try
    End Sub
#End Region
    Private Sub BindAllControl()
        Try
            Dim i, j As Integer
           
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)
            objeAAMS.BindDropDown(DlstAirline, "AIRLINE", False, 3)
            objeAAMS.BindDropDown(dlstCity, "CITY", False, 3)
            objeAAMS.BindDropDown(dlstCountry, "COUNTRY", False, 3)
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", False, 3)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", False, 3)
            objeAAMS.BindDropDown(drp1AOffice, "AOFFICE", False, 3)
            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)
            objeAAMS.BindDropDown(drpResStaff, "ResponsbileStaff", False, 3)
            objeAAMS.BindDropDown(drpGroupAgencyType, "AGENCYGROUPCLASSTYPE", False, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drp1AOffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drp1AOffice.SelectedValue = li.Value
                            End If
                        End If
                        drp1AOffice.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            grdvMonthlyBReak.AllowSorting = False
            grdvMonthlyBReak.HeaderStyle.ForeColor = Drawing.Color.Black
            TicketExport()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            TicketSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            TicketSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            TicketSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMonthlyBReak_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvMonthlyBReak.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvMonthlyBReak_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMonthlyBReak.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                'ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting on first time  of following Fields      
                ' @AIR_NETBOOKINGS, CAR_NETBOOKINGS, HOTEL_NETBOOKINGS, INSURANCE_NETBOOKINGS, TOTAL, PASSIVE, WITHPASSIVE,
                If SortName.Trim().ToUpper = "AIR_NETBOOKINGS" Or SortName.Trim().ToUpper = "CAR_NETBOOKINGS" Or SortName.Trim().ToUpper = "HOTEL_NETBOOKINGS" Or SortName.Trim().ToUpper = "INSURANCE_NETBOOKINGS" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                    ViewState("Desc") = "TRUE"
                Else
                    ViewState("Desc") = "FALSE"
                End If
                '@ Added Code For Default descending sorting on first time  of foolowing Fields
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ' ViewState("Desc") = "FALSE"

                    '@ Added Code For Default descending sorting on first time  of following Fields      
                    ' @AIR_NETBOOKINGS, CAR_NETBOOKINGS, HOTEL_NETBOOKINGS, INSURANCE_NETBOOKINGS, TOTAL, PASSIVE, WITHPASSIVE,
                    If SortName.Trim().ToUpper = "AIR_NETBOOKINGS" Or SortName.Trim().ToUpper = "CAR_NETBOOKINGS" Or SortName.Trim().ToUpper = "HOTEL_NETBOOKINGS" Or SortName.Trim().ToUpper = "INSURANCE_NETBOOKINGS" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ Added Code For Default descending sorting on first time  of foolowing Fields

                End If
            End If
            TicketSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvMonthlyBReak_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMonthlyBReak.RowCreated
        Try
        Catch ex As Exception
        End Try
    End Sub
    ' ###################################################################
#End Region
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
  
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting In case Of Delete The Record
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
        '@ End of Code Added For Paging And Sorting In case Of Delete The Record
        ' ###################################################################
    End Sub
#Region "TicketExport"
    Private Sub TicketExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            '    objInputXml.LoadXml("<PR_SEARCH_1ATICKET_INPUT> <CITY></CITY> <COUNTRY></COUNTRY>  <LCODE></LCODE> <AGENCYNAME></AGENCYNAME>  <SHOWGROUP></SHOWGROUP> <RESP_1A></RESP_1A> <RESPONSIBLESTAFF></RESPONSIBLESTAFF><AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID>  <GROUPTYPEID></GROUPTYPEID> <AOFFICE></AOFFICE> <PRODUCITIVITY_TYPE></PRODUCITIVITY_TYPE> <PRODUCITIVITY_FROM></PRODUCITIVITY_FROM> <PRODUCITIVITY_TO></PRODUCITIVITY_TO>  <REGION></REGION> <AIRLINE_CODE></AIRLINE_CODE> <MONTHLYBREAKUP></MONTHLYBREAKUP> <A_TICKET></A_TICKET> <DAILYBOOKING></DAILYBOOKING> <A_PRODUCTIVITY></A_PRODUCTIVITY> <ALL_CRS></ALL_CRS> <NIDT></NIDT><AIRBREAKUP></AIRBREAKUP> <DATEFROM></DATEFROM> <DATETO></DATETO> <PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC><CHAIN_CODE></CHAIN_CODE><TYPEID></TYPEID></PR_SEARCH_1ATICKET_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_1ATICKET_INPUT> <CITY></CITY> <COUNTRY></COUNTRY>  <LCODE></LCODE> <AGENCYNAME></AGENCYNAME>  <SHOWGROUP></SHOWGROUP> <RESP_1A></RESP_1A> <RESPONSIBLESTAFF></RESPONSIBLESTAFF><AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID>  <GROUPTYPEID></GROUPTYPEID> <AOFFICE></AOFFICE> <PRODUCITIVITY_TYPE></PRODUCITIVITY_TYPE> <PRODUCITIVITY_FROM></PRODUCITIVITY_FROM> <PRODUCITIVITY_TO></PRODUCITIVITY_TO>  <REGION></REGION> <AIRLINE_CODE></AIRLINE_CODE> <MONTHLYBREAKUP></MONTHLYBREAKUP> <A_TICKET></A_TICKET> <DAILYBOOKING></DAILYBOOKING> <A_PRODUCTIVITY></A_PRODUCTIVITY> <ALL_CRS></ALL_CRS> <NIDT></NIDT> <AIRBREAKUP></AIRBREAKUP> <DATEFROM></DATEFROM> <DATETO></DATETO> <TYPEID></TYPEID><PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_1ATICKET_INPUT>")


            '<PR_SEARCH_1ATICKET_INPUT>
            ' <CITY></CITY>
            ' <COUNTRY></COUNTRY> 
            ' <LCODE></LCODE>
            ' <AGENCYNAME></AGENCYNAME> 
            ' <SHOWGROUP></SHOWGROUP>
            ' <RESP_1A></RESP_1A>
            ' <RESPONSIBLESTAFF></RESPONSIBLESTAFF>
            ' <AGENCYSTATUSID></AGENCYSTATUSID>
            ' <AGENCYTYPEID></AGENCYTYPEID> 
            ' <GROUPTYPEID></GROUPTYPEID>
            ' <AOFFICE></AOFFICE>
            ' <PRODUCITIVITY_TYPE></PRODUCITIVITY_TYPE>
            ' <PRODUCITIVITY_FROM></PRODUCITIVITY_FROM>
            ' <PRODUCITIVITY_TO></PRODUCITIVITY_TO> 
            ' <REGION></REGION>
            ' <AIRLINE_CODE></AIRLINE_CODE> 
            ' <MONTHLYBREAKUP></MONTHLYBREAKUP>
            ' <A_TICKET></A_TICKET>
            ' <DAILYBOOKING></DAILYBOOKING>
            ' <A_PRODUCTIVITY></A_PRODUCTIVITY>
            ' <ALL_CRS></ALL_CRS>
            ' <AIRBREAKUP></AIRBREAKUP>
            ' <DATEFROM></DATEFROM>
            ' <DATETO></DATETO>
            ' <PAGE_NO></PAGE_NO>
            ' <PAGE_SIZE></PAGE_SIZE>
            ' <SORT_BY></SORT_BY>
            ' <DESC></DESC>
            '<COMP_VERTICAL></COMP_VERTICAL>
            '</PR_SEARCH_1ATICKET_INPUT>
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If
            If (dlstCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            If ChkGrpProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "FALSE"
            End If

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            If (drpResStaff.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFF").InnerText = drpResStaff.SelectedValue
            End If
            If (drpAgencyStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue
            End If

            If (drpAgencyType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue
            End If
            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPTYPEID").InnerText = drpLstGroupType.SelectedItem.Value
            End If
            If (drp1AOffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1AOffice.SelectedValue
            End If

            If (drpProductivity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_TYPE").InnerText = Trim(drpProductivity.SelectedItem.Text)
            End If


            objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_FROM").InnerText = txtFrom.Text

            objInputXml.DocumentElement.SelectSingleNode("PRODUCITIVITY_TO").InnerText = txtTo.Text

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue
            End If
            If DlstAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = DlstAirline.SelectedValue.Trim()
            End If
            If RdShowMon.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
            End If


            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = drpYearFrom.SelectedValue.PadLeft(4, "0") + drpMonthFrom.SelectedValue.PadLeft(2, "0") + "01"
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = drpYearTo.SelectedValue.PadLeft(4, "0") + drpMonthTo.SelectedValue.PadLeft(2, "0") + "01"


            If ChkTicket.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("A_TICKET").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("A_TICKET").InnerText = "FALSE"
            End If
            If ChkDailyBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("DAILYBOOKING").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DAILYBOOKING").InnerText = "FALSE"
            End If
            If Chk1AProd.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("A_PRODUCTIVITY").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("A_PRODUCTIVITY").InnerText = "FALSE"
            End If

            If ChkAllCRS.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("ALL_CRS").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ALL_CRS").InnerText = "FALSE"
            End If

            If chkNIDT.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("NIDT").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("NIDT").InnerText = "FALSE"
            End If

            If ChkAirBreackup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""

            End If
            If drpGroupAgencyType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
            End If

            'Start CODE for sorting and paging
            '  Start This Code is Added For ReSetting Viwstate for Sorting
            If ViewState("PrevSearching") Is Nothing Then
                'objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "MONTHLYBREAKUP" Or objNode.Name = "A_TICKET" Or objNode.Name = "DAILYBOOKING" Or objNode.Name = "A_PRODUCTIVITY" Or objNode.Name = "ALL_CRS" Or objNode.Name = "CODD" Or objNode.Name = "HX_BOOKINGS" Or objNode.Name = "AIRBREAKUP" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            ViewState("SortName") = "LCODE" '"CHAIN_CODE"
                            ViewState("Desc") = "FALSE"
                        End If
                    End If
                Next
            End If
            '   End of Code is Added For ReSetting Viwstate for Sorting

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LCODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE"
            Else
                If ViewState("SortName") = "ONLINE_STATUS" Then
                    If ChkListStatus.Items(0).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "ADDRESS" Then
                    If ChkListStatus.Items(1).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "COUNTRY" Then
                    If ChkListStatus.Items(2).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "CHAIN_CODE" Then
                    If ChkListStatus.Items(3).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If
                If ViewState("SortName") = "GROUPTYPENAME" Then
                    If ChkListStatus.Items(4).Selected = False Then
                        ViewState("SortName") = "LCODE"
                    End If
                End If

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
            objOutputXml = objbzbzBIDT.Search_1ATicket(objInputXml)
            ' objOutputXml.Load("C:\PR_SEARCH_1ATICKET_OUTPUT.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds

                '@ New Added Code  
                Dim dtable As New DataTable
                Dim dCol As DataColumn
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("A_TICKET").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                    dtable.Columns.Add(dCol)
                Next

                Dim dRow As DataRow
                dRow = dtable.NewRow()
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("A_TICKET").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dRow(strAttribut) = xmlAttrTotal.Value
                Next
                dtable.Rows.Add(dRow)
                grdvMonthlyBReak.DataSource = dtable
                grdvMonthlyBReak.DataBind()


                '@ Code For Exporting the Data

                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("A_TICKET")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                ' <A_TICKET LCODE='' CHAIN_CODE='' EMPLOYEE_NAME='' ADDRESS='' ONLINE_STATUS='' CITY='' COUNTRY='' OFFICEID='' AOFFICE='' AIRLINE_NAME='' TKT_NETBOOKINGS='' DB_NETBOOKINGS='' A_NETBOOKINGS='' MT_NETBOOKINGS='' />

                With objXmlNodeClone
                    '   <PAGE_TOTAL TKT_NETBOOKINGS='' DB_NETBOOKINGS='' A_NETBOOKINGS='' MT_NETBOOKINGS=''/>
                    .Attributes("OFFICEID").Value = "Total"
                    .Attributes("TKT_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TKT_NETBOOKINGS").ToString
                    .Attributes("DB_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("DB_NETBOOKINGS").ToString
                    .Attributes("A_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("A_NETBOOKINGS").ToString
                    .Attributes("MT_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("MT_NETBOOKINGS").ToString
                    .Attributes("CODD").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CODD").ToString
                    .Attributes("HX_BOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HX_BOOKINGS").ToString
                End With
                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                Dim objExport As New ExportExcel
                Dim IntInvisible As Integer = 0
                For intclmn As Integer = 0 To grdvMonthlyBReak.HeaderRow.Cells.Count - 2
                    If grdvMonthlyBReak.HeaderRow.Cells(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdvMonthlyBReak.HeaderRow.Cells.Count - 2 - IntInvisible) As String
                Dim intArray(grdvMonthlyBReak.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

                Dim intclmnVis As Integer = 0
                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                    If grdvMonthlyBReak.HeaderRow.Cells(intclmn).Visible = True Then
                        strArray(intclmnVis) = grdvMonthlyBReak.Columns(intclmn).HeaderText 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                        '@ Finding Position From xml Related with Header Text
                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvMonthlyBReak.Columns(intclmn).SortExpression.Trim Then
                                intArray(intclmnVis) = kk
                                intclmnVis = intclmnVis + 1
                                Exit For
                            End If
                        Next kk
                    End If
                Next intclmn

                objExport.ExportDetails(objOutputXmlExport, "A_TICKET", intArray, strArray, ExportExcel.ExportFormat.Excel, "Aticket.xls")

                '@ End of Code For Exporting the Data
            Else
                txtRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString

            pnlPaging.Visible = False
        Finally

            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("drpProductivity") IsNot Nothing Then
                drpProductivity.SelectedValue = Request("drpProductivity")
            End If
            If Request("txtFrom") IsNot Nothing Then
                txtFrom.Text = Request("txtFrom")
            End If
            If Request("txtTo") IsNot Nothing Then
                txtTo.Text = Request("txtTo")
            End If
            If drpProductivity.SelectedValue = "" Then
                txtFrom.CssClass = "textboxgrey"
                txtTo.CssClass = "textboxgrey"
                txtFrom.Enabled = False
                txtTo.Enabled = False
            ElseIf drpProductivity.SelectedValue = "7" Then
                txtFrom.CssClass = "textbox"
                txtTo.CssClass = "textbox"
                txtFrom.Enabled = True
                txtTo.Enabled = True
            Else
                txtFrom.CssClass = "textbox"
                txtTo.CssClass = "textboxgrey"
                txtFrom.Enabled = True
                txtTo.Enabled = False
                txtTo.Text = ""
            End If
        End Try
    End Sub
#End Region

    Protected Sub grdvMonthlyBReak_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMonthlyBReak.RowDataBound

        Dim GroupData, TicketData, DailyBookData, AProdData, CRSData, AirBreakData, Lcode, MonthF, MonthT, YearF, YearT, MBreakup, AirCode, NIDT As String
       
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    '  TKT_NETBOOKINGS='' DB_NETBOOKINGS='' A_NETBOOKINGS='' MT_NETBOOKINGS
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        e.Row.Cells(15).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TKT_NETBOOKINGS").ToString
                        e.Row.Cells(16).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("DB_NETBOOKINGS").ToString
                        e.Row.Cells(17).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("A_NETBOOKINGS").ToString
                        e.Row.Cells(18).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("MT_NETBOOKINGS").ToString
                        e.Row.Cells(19).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CODD").ToString
                        e.Row.Cells(20).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HX_BOOKINGS").ToString
                    End If
                End If
            End If
            Dim objSecurityXml As New XmlDocument
          

            If ChkListStatus.Items(0).Selected = True Then
                e.Row.Cells(8).Visible = True
            Else
                e.Row.Cells(8).Visible = False
            End If
            If ChkListStatus.Items(1).Selected = True Then
                e.Row.Cells(3).Visible = True
            Else
                e.Row.Cells(3).Visible = False
            End If
            If ChkListStatus.Items(2).Selected = True Then
                e.Row.Cells(7).Visible = True
            Else
                e.Row.Cells(7).Visible = False
            End If
            If ChkListStatus.Items(3).Selected = True Then
                e.Row.Cells(1).Visible = True
            Else
                e.Row.Cells(1).Visible = False
            End If
            If ChkListStatus.Items(4).Selected = True Then
                e.Row.Cells(9).Visible = True
            Else
                e.Row.Cells(9).Visible = False
            End If

            If ChkTicket.Checked = True Then
                e.Row.Cells(15).Visible = True
            Else
                e.Row.Cells(15).Visible = False
            End If
            If ChkDailyBook.Checked = True Then
                e.Row.Cells(16).Visible = True
            Else
                e.Row.Cells(16).Visible = False
            End If

            If Chk1AProd.Checked = True Then
                e.Row.Cells(17).Visible = True
            Else
                e.Row.Cells(17).Visible = False
            End If

            If ChkAllCRS.Checked = True Then
                e.Row.Cells(18).Visible = True
            Else
                e.Row.Cells(18).Visible = False
            End If

            If chkNIDT.Checked = True Then
                e.Row.Cells(19).Visible = True
                e.Row.Cells(20).Visible = True
            Else
                e.Row.Cells(19).Visible = False
                e.Row.Cells(20).Visible = False
            End If

            If ChkAirBreackup.Checked = True Then
                e.Row.Cells(17).Visible = False
                e.Row.Cells(14).Visible = True
            Else
                e.Row.Cells(14).Visible = False
            End If
            If RdShowAvg.Checked = True Then
                e.Row.Cells(12).Visible = False
                e.Row.Cells(13).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                If e.Row.Cells(15).Visible = True Or e.Row.Cells(16).Visible = True Or e.Row.Cells(17).Visible = True Or e.Row.Cells(18).Visible = True Or e.Row.Cells(19).Visible = True Or e.Row.Cells(20).Visible = True Then
                    If RdShowAvg.Checked = True Then
                        e.Row.Cells(11).Text = "Total"
                    Else
                        e.Row.Cells(13).Text = "Total"
                    End If

                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                GroupData = ""
                TicketData = ""
                DailyBookData = ""
                AProdData = ""
                CRSData = ""
                AirBreakData = ""
                Lcode = ""
                MonthF = ""
                MonthT = ""
                YearF = ""
                YearT = ""
                MBreakup = ""
                AirCode = ""
                NIDT = ""
                If ChkGrpProductivity.Checked = True Then
                    GroupData = "1"
                Else
                    GroupData = "0"
                End If
                If ChkTicket.Checked = True Then
                    TicketData = "1"
                Else
                    TicketData = "0"
                End If
                If ChkDailyBook.Checked = True Then
                    DailyBookData = "1"
                Else
                    DailyBookData = "0"
                End If
                If Chk1AProd.Checked = True Then
                    AProdData = "1"
                Else
                    AProdData = "0"
                End If
                If ChkAllCRS.Checked = True Then
                    CRSData = "1"
                Else
                    CRSData = "0"
                End If
                If ChkAirBreackup.Checked = True Then
                    AirBreakData = "1"
                Else
                    AirBreakData = "0"
                    AirCode = ""
                End If
                If chkNIDT.Checked = True Then
                    NIDT = "1"
                Else
                    NIDT = "0"
                End If
                If RdShowMon.Checked = True Then
                    MBreakup = "1"
                Else
                    MBreakup = "0"
                End If
                AirCode = DlstAirline.SelectedValue
                Dim linkDetails As System.Web.UI.HtmlControls.HtmlAnchor
                linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
                Dim hdLcode As HiddenField = CType(e.Row.FindControl("hdLcode"), HiddenField)
                Lcode = objED.Encrypt(hdLcode.Value)
                MonthF = drpMonthFrom.SelectedValue
                MonthT = drpMonthTo.SelectedValue
                YearF = drpYearFrom.SelectedValue
                YearT = drpYearTo.SelectedValue

                linkDetails.Attributes.Add("OnClick", "javascript:return DetailsFunction('" + MonthF + "','" + MonthT + "','" + YearF + "','" + YearT + "','" + Lcode + "','" + GroupData + "','" + TicketData + "','" + DailyBookData + "','" + AProdData + "','" + CRSData + "','" + AirBreakData + "','" + MBreakup + "','" + AirCode + "','" + NIDT + "');")

                'Dim strBuilder As New StringBuilder
                'objSecurityXml = New XmlDocument
                'objSecurityXml.LoadXml(Session("Security"))
                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                '        If strBuilder(0) = "0" Then
                '            linkDetails.Disabled = True
                '        Else
                '            linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                '        End If
                '    Else
                '        linkDetails.Disabled = True
                '    End If
                'Else
                '    strBuilder = objeAAMS.SecurityCheck(31)
                '    linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                'End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


End Class