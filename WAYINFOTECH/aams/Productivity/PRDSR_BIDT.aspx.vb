Imports System.io
Imports System.Text
Partial Class Productivity_PRDSR_BIDT
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim Airsum As Long = 0
    Dim Carsum As Long = 0
    Dim Hotelsum As Long = 0
    Dim Inssum As Long = 0
    Dim Totalsum As Long = 0
    Dim TotalAvg As Long = 0
    Dim TotalTransSeg As Long = 0
    Dim TotalTrans As Long = 0
    Dim FooterDataset As DataSet

    'Modified 
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
            'btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            'drpProductivity.Attributes.Add("onchange", "return SelectProdutivity();")
            'ChkExTrans.Attributes.Add("onclick", "return Show1ABooking();")
            'ChkNewFormat.Attributes.Add("onclick", "return ShowAvg();")
            dlstCity.Attributes.Add("onkeyup", "return gotops('dlstCity');")
            dlstCountry.Attributes.Add("onkeyup", "return gotops('dlstCountry');")
            drpAgencyStatus.Attributes.Add("onkeyup", "return gotops('drpAgencyStatus');")

            drpAgencyType.Attributes.Add("onkeyup", "return gotops('drpAgencyType');")
            drp1AOffice.Attributes.Add("onkeyup", "return gotops('drp1AOffice');")
            drpRegion.Attributes.Add("onkeyup", "return gotops('drpRegion');")
            drpResStaff.Attributes.Add("onkeyup", "return gotops('drpResStaff');")
            drpProductivity.Attributes.Add("onkeyup", "return gotops('drpProductivity');")
            drpLstGroupType.Attributes.Add("onkeyup", "return gotops('drpLstGroupType');")
            drpLstGroupClassType.Attributes.Add("onkeyup", "return gotops('drpLstGroupClassType');")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")


            ''Added by Tapan Nath for LCODE, & Chain Code 18/03/2011
            txtLcode.Attributes.Add("onfocusout", "return EnableDisableBIDTGroupProductivity();")
            txtChainCode.Attributes.Add("onfocusout", "return EnableDisableBIDTGroupProductivity();")
            txtAgencyName.Attributes.Add("onfocusout", "return ActDecLcodeChainCode();")
            ''Added by Tapan Nath for LCODE, & Chain Code
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            grdvMonthlyBReak.Columns(9).HeaderText = "Agency Category"
            grdvShowAvg.Columns(9).HeaderText = "Agency Category"
            If Not Page.IsPostBack Then
                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity']").Attributes("Value").Value)
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
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Air']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Air']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(0).Enabled = False
                            ChkABooking.Items(0).Selected = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Car']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Car']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(1).Enabled = False

                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Hotel']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Hotel']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(2).Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkABooking.Items(3).Enabled = False
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                objSecurityXml = New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Attributes("Value").Value)
                        If strBuilder(0) = "0" Then
                            ChkOrigBook.Checked = False
                            ChkOrigBook.Visible = False
                            ' ChkListStatus.Items(0).Enabled = False
                            'ChkListStatus.Items(0).Selected = False
                        End If
                    End If
                Else
                    ChkOrigBook.Visible = True
                    ChkOrigBook.Checked = False
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
            End If


            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onclick", "return CheckValidation();")
                btnExport.Attributes.Add("onclick", "return CheckValidation();")
                drpProductivity.Attributes.Add("onchange", "return SelectProdutivity();")
                drpProductivity.Attributes.Add("onclick", "return SelectProdutivity();")
                objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)
                'ChkExTrans.Attributes.Add("onclick", "return Show1ABooking();")
                ChkNewFormat.Attributes.Add("onclick", "return ShowAvg();")

                BindAllControl()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

#Region "btnSearch_Click()"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ' ViewState("SortName") = "CHAIN_CODE"
            ' ViewState("Desc") = "FALSE"
            BIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#End Region
    'Method for search BIDTSearch
#Region "BIDTSearch"
    Private Sub BIDTSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            '  objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID><GROUPTYPEID /> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> <GROUPTYPEID /> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><TYPEID></TYPEID><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")

            '<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>
            '<SHOWOFFICEID></SHOWOFFICEID>
            '<SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> 
            '<AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> 
            '<AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> 
            '<FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL>
            ' <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE>
            '<SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA>
            ' <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> 
            '<AGENCYTYPEID></AGENCYTYPEID> 
            '<RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> 
            '<INCLUDETRANSACTION> </INCLUDETRANSACTION>
            ' <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE>
            '</INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> 
            '</LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION>
            ' <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY>
            '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("SHOWOFFICEID").InnerText = ""

            If RdShowAvg.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 0
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            objInputXml.DocumentElement.SelectSingleNode("GROUPID").InnerText = ""

            If (dlstCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If

            If drpLstGroupClassType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TYPEID").InnerText = drpLstGroupClassType.SelectedItem.Value
            End If

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPTYPEID").InnerText = drpLstGroupType.SelectedItem.Value
            End If

            If (dlstCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (drp1AOffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1AOffice.SelectedValue
            End If

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue
            End If
            If (drpAgencyStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue
            End If
            If (drpAgencyType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue
            End If
            If (drpResStaff.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SALESPERSONID").InnerText = drpResStaff.SelectedValue
            End If


            If (drpProductivity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SYMBOL").InnerText = Trim(drpProductivity.SelectedItem.Text)
            End If

            objInputXml.DocumentElement.SelectSingleNode("FVALUE").InnerText = txtFrom.Text

            objInputXml.DocumentElement.SelectSingleNode("SVALUE").InnerText = txtTo.Text

            If ChkGrpProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If

            If ChkOrigBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If

            If ChkExTrans.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "0"
            End If


            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If ChkNewFormat.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 0
            End If
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""



            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))


                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

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

            ' Start This Code is Added For ReSetting Viwstate for Sorting
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "NEWFORMAT" Or objNode.Name = "INCLUDETRANSACTION" Or objNode.Name = "SHOWAVG" Or objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                            ViewState("Desc") = "FALSE"
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If
            '  End of Code is Added For ReSetting Viwstate for Sorting

            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE" '"CHAIN_CODE"
            Else
                If ViewState("SortName") = "ONLINE_STATUS" Then
                    If ChkListStatus.Items(0).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "ADDRESS" Then
                    If ChkListStatus.Items(1).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "COUNTRY" Then
                    If ChkListStatus.Items(2).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "CHAIN_CODE" Then
                    If ChkChainCode.Checked = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If

                If ViewState("SortName") = "GROUP_CLASSIFICATION_NAME" Then
                    If ChkGroupClass.Checked = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
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
            objOutputXml = objbzbzBIDT.BIDTProductivityMonthly_Average(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If ChkNewFormat.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    ViewState("PrevSearching") = objInputXml.OuterXml

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)



                    If ds.Tables("DETAILS").Columns("ROWNUMBER") IsNot Nothing Then
                        ds.Tables("DETAILS").Columns.Remove(ds.Tables("DETAILS").Columns("ROWNUMBER"))
                    End If

                    ds.Tables("DETAILS").Columns.Add("Action               ")

                    grdNewFormat.DataSource = ds.Tables("DETAILS")

                    FooterDataset = New DataSet
                    FooterDataset = ds

                    grdNewFormat.DataBind()
                    grdNewFormat.HeaderRow.Cells(grdNewFormat.HeaderRow.Cells.Count - 1).Text = "Action               "

                    Dim intRow, IntColno, Inti As Integer
                    grdNewFormat.FooterRow.Cells(7).Text = "Total"
                    For IntColno = 12 To grdNewFormat.Rows(0).Cells.Count - 2
                        grdNewFormat.HeaderRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                        grdNewFormat.FooterRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right

                        grdNewFormat.FooterRow.Cells(IntColno).Text = 0
                    Next

                    For intRow = 0 To grdNewFormat.Rows.Count - 1
                        For IntColno = 12 To grdNewFormat.Rows(0).Cells.Count - 2
                            grdNewFormat.Rows(intRow).Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                            Dim sum As Double = grdNewFormat.Rows(intRow).Cells(IntColno).Text

                            'If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                            '    grdNewFormat.FooterRow.Cells(IntColno).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(Inti).ToString
                            'End If

                            ' grdNewFormat.FooterRow.Cells(IntColno).Text = CType(grdNewFormat.FooterRow.Cells(IntColno).Text, Decimal) + sum
                            ' grdNewFormat.FooterRow.Cells(IntColno).Text = String.Format("{0:d}", grdNewFormat.FooterRow.Cells(IntColno).Text.ToString)
                        Next

                    Next
                    Inti = 0
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        For Inti = 0 To FooterDataset.Tables("PAGE_TOTAL").Columns.Count - 1
                            grdNewFormat.FooterRow.Cells(Inti + 12).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(Inti).ToString
                        Next
                    End If



                    ' txtRecordCount.Text = ds.Tables("DETAILS").Rows.Count.ToString

                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True

                    ' ##################################################################
                    ' Code Added For Paging And Sorting 
                    ' ###################################################################
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                    txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' ##################################################################
                    '  Added Code To Show Image'   
                    SetImageForSorting2(grdNewFormat, ds)
                    '  Added Code To Show Image'
                    ' ##################################################################
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting 
                    ' ###################################################################


                    pnlPaging.Visible = True

                Else
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()

                    txtRecordCount.Text = "0"
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If

            If ChkExTrans.Checked = True And RdShowMon.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessMonthBreak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                    GrdExcessMonthBreak.DataBind()
                    '  txtRecordCount.Text = ds.Tables("BIDTMONTHLYBREAKUP").Rows.Count.ToString

                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True


                    ' ##################################################################
                    ' Code Added For Paging And Sorting 
                    ' ###################################################################
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                    'txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' ##################################################################
                    '  Added Code To Show Image'   
                    SetImageForSorting(GrdExcessMonthBreak)
                    '  Added Code To Show Image'
                    ' ##################################################################

                    ' ###################################################################
                    ' End of Code Added For Paging And Sorting 
                    ' ###################################################################




                Else
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True
                    txtRecordCount.Text = "0"
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If

            If ChkExTrans.Checked = True And RdShowAvg.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessAvg.DataSource = ds.Tables("BIDTAVERAGE")
                    GrdExcessAvg.DataBind()
                    ' txtRecordCount.Text = ds.Tables("BIDTAVERAGE").Rows.Count.ToString
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()

                    ' ##################################################################
                    ' Code Added For Paging And Sorting 
                    ' ###################################################################
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                    ' txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' ##################################################################
                    '  Added Code To Show Image'  
                    SetImageForSorting(GrdExcessAvg)
                    '  Added Code To Show Image'
                    ' ##################################################################

                    ' ###################################################################
                    ' End of Code Added For Paging And Sorting 
                    ' ###################################################################

                Else
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    'lblFound.Visible = True
                    'txtRecordCount.Visible = True
                    txtRecordCount.Text = "0"
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    pnlPaging.Visible = False
                End If
            End If
            If ChkExTrans.Checked = False And ChkNewFormat.Checked = False Then

                If RdShowMon.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        'Session("DataSource") = ds.Tables("AGNECY")

                        FooterDataset = New DataSet
                        FooterDataset = ds

                        grdvMonthlyBReak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                        grdvMonthlyBReak.DataBind()
                        'txtRecordCount.Text = ds.Tables("BIDTMONTHLYBREAKUP").Rows.Count.ToString
                        'lblFound.Visible = True
                        'txtRecordCount.Visible = True
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()


                        ' ##################################################################
                        ' Code Added For Paging And Sorting 
                        ' ###################################################################
                        BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                        txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                        ' ##################################################################
                        ' @ Added Code To Show Image'                   
                        ' grdNewFormat.SortExpression = ViewState("SortName")
                        SetImageForSorting(grdvMonthlyBReak)
                        '  Added Code To Show Image'
                        ' ##################################################################
                        ' ###################################################################
                        ' End of Code Added For Paging And Sorting 
                        ' ###################################################################

                        'If ChkListStatus.Items(0).Selected = True And ChkListStatus.Items(1).Selected = True And ChkListStatus.Items(2).Selected = True And ChkChainCode.Checked = True And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvMonthlyBReak.Width = 1900
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = True And ChkListStatus.Items(2).Selected = True And ChkChainCode.Checked = True And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvMonthlyBReak.Width = 1800
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = False And ChkListStatus.Items(2).Selected = False And ChkChainCode.Checked = False And ChkABooking.Items(0).Selected = False And ChkABooking.Items(1).Selected = False And ChkABooking.Items(2).Selected = False And ChkABooking.Items(3).Selected = False And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvMonthlyBReak.Width = 1120
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = False And ChkListStatus.Items(2).Selected = False And ChkChainCode.Checked = False And ChkABooking.Items(0).Selected = False And ChkABooking.Items(1).Selected = False And ChkABooking.Items(2).Selected = False And ChkABooking.Items(3).Selected = False And ChkOrigBook.Checked = False Then
                        '    grdvShowAvg.Width = 900
                        'ElseIf ChkListStatus.Items(0).Selected = True And ChkListStatus.Items(1).Selected = True Then
                        '    grdvMonthlyBReak.Width = 1800
                        'ElseIf ChkListStatus.Items(0).Selected = True Then
                        '    grdvMonthlyBReak.Width = 1470
                        'ElseIf ChkListStatus.Items(1).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvMonthlyBReak.Width = 1600
                        'ElseIf ChkListStatus.Items(1).Selected = True Then
                        '    grdvMonthlyBReak.Width = 1300
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And (ChkChainCode.Checked = True) And ((ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True)) Then
                        '    grdvMonthlyBReak.Width = 1450
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True Then
                        '    grdvMonthlyBReak.Width = 1450
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And (ChkChainCode.Checked = True) Then
                        '    grdvMonthlyBReak.Width = 1250
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And ((ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True)) Then
                        '    grdvMonthlyBReak.Width = 1550
                        'Else
                        '    grdvMonthlyBReak.Width = 1200
                        'End If
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
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                        'lblFound.Visible = True
                        'txtRecordCount.Visible = True
                        txtRecordCount.Text = "0"
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        pnlPaging.Visible = False
                    End If
                ElseIf RdShowAvg.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        'Session("DataSource") = ds.Tables("AGNECY")
                        FooterDataset = New DataSet
                        FooterDataset = ds
                        grdvShowAvg.DataSource = ds.Tables("BIDTAVERAGE")
                        grdvShowAvg.DataBind()
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                        'lblFound.Visible = True
                        'txtRecordCount.Visible = True
                        'txtRecordCount.Text = ds.Tables("BIDTAVERAGE").Rows.Count.ToString


                        ' ##################################################################
                        ' Code Added For Paging And Sorting 
                        ' ###################################################################
                        BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                        txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                        ' ##################################################################
                        '  Added Code To Show Image'                   
                        ' grdNewFormat.SortExpression = ViewState("SortName")
                        SetImageForSorting(grdvShowAvg)
                        '  Added Code To Show Image'
                        ' ##################################################################
                        ' ###################################################################
                        '@ End of Code Added For Paging And Sorting 
                        ' ###################################################################
                        'If ChkListStatus.Items(0).Selected = True And ChkListStatus.Items(1).Selected = True And ChkListStatus.Items(2).Selected = True And ChkChainCode.Checked = True And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvShowAvg.Width = 1900
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = True And ChkListStatus.Items(2).Selected = True And ChkChainCode.Checked = True And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvShowAvg.Width = 1800
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = False And ChkListStatus.Items(2).Selected = False And ChkChainCode.Checked = False And ChkABooking.Items(0).Selected = False And ChkABooking.Items(1).Selected = False And ChkABooking.Items(2).Selected = False And ChkABooking.Items(3).Selected = False And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvShowAvg.Width = 1120
                        'ElseIf ChkListStatus.Items(0).Selected = False And ChkListStatus.Items(1).Selected = False And ChkListStatus.Items(2).Selected = False And ChkChainCode.Checked = False And ChkABooking.Items(0).Selected = False And ChkABooking.Items(1).Selected = False And ChkABooking.Items(2).Selected = False And ChkABooking.Items(3).Selected = False And ChkOrigBook.Checked = False Then
                        '    grdvShowAvg.Width = 900
                        'ElseIf ChkListStatus.Items(0).Selected = True And ChkListStatus.Items(1).Selected = True Then
                        '    grdvShowAvg.Width = 1800
                        'ElseIf ChkListStatus.Items(0).Selected = True Then
                        '    grdvShowAvg.Width = 1470
                        'ElseIf ChkListStatus.Items(1).Selected = True And ChkOrigBook.Checked = True And ChkOrigBook.Visible = True Then
                        '    grdvShowAvg.Width = 1600
                        'ElseIf ChkListStatus.Items(1).Selected = True Then
                        '    grdvShowAvg.Width = 1300
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And (ChkChainCode.Checked = True) And ((ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True)) Then
                        '    grdvShowAvg.Width = 1450
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True Then
                        '    grdvShowAvg.Width = 1450
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And (ChkChainCode.Checked = True) Then
                        '    grdvShowAvg.Width = 1250
                        'ElseIf (ChkOrigBook.Checked = True And ChkOrigBook.Visible = True) And ((ChkABooking.Items(0).Selected = True And ChkABooking.Items(1).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(0).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(2).Selected = True) Or (ChkABooking.Items(1).Selected = True And ChkABooking.Items(3).Selected = True) Or (ChkABooking.Items(2).Selected = True And ChkABooking.Items(3).Selected = True)) Then
                        '    grdvShowAvg.Width = 1550
                        'Else
                        '    grdvShowAvg.Width = 1200
                        'End If

                        Dim TotalWidth As Int64
                        TotalWidth = 0

                        For intclmn As Integer = 0 To grdvShowAvg.Columns.Count - 1
                            If grdvShowAvg.HeaderRow.Cells(intclmn).Visible = True Then
                                TotalWidth = TotalWidth + grdvShowAvg.Columns(intclmn).ItemStyle.Width.Value
                            End If
                        Next
                        grdvShowAvg.Width = TotalWidth

                    Else
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                        'lblFound.Visible = True
                        'txtRecordCount.Visible = True
                        txtRecordOnCurrentPage.Text = "0"
                        txtRecordCount.Text = "0"
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        pnlPaging.Visible = False
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            ' lblFound.Visible = False
            'txtRecordCount.Visible = False
            pnlPaging.Visible = False
        Finally
            ' lblFound.Visible = True
            ' txtRecordCount.Visible = True
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
            objeAAMS.BindDropDown(dlstCity, "CITY", False, 3)
            objeAAMS.BindDropDown(dlstCountry, "COUNTRY", False, 3)
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", False, 3)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", False, 3)
            objeAAMS.BindDropDown(drp1AOffice, "AOFFICE", False, 3)
            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)
            objeAAMS.BindDropDown(drpResStaff, "ResponsbileStaff", False, 3)

            objeAAMS.BindDropDown(drpLstGroupClassType, "AGENCYGROUPCLASSTYPE", False, 3)
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
            Response.Redirect("PRDSR_BIDT.aspx", False)
            'txtAgencyName.Text = ""
            'txtFrom.Text = "0"
            'txtTo.Text = "0"
            'hdAgencyName.Value = ""
            'dlstCity.SelectedIndex = 0
            'dlstCountry.SelectedIndex = 0
            'drp1AOffice.SelectedIndex = 0
            'drpAgencyStatus.SelectedIndex = 0
            'drpAgencyType.SelectedIndex = 0
            'drpResStaff.SelectedIndex = 0
            'drpProductivity.SelectedIndex = 0
            'drpRegion.SelectedIndex = 0
            'drpMonthTo.SelectedValue = "12"
            'drpMonthFrom.SelectedValue = "1"
            'drpYearFrom.SelectedValue = DateTime.Now.Year
            'drpYearTo.SelectedValue = DateTime.Now.Year
            'ChkExTrans.Checked = False
            'ChkNewFormat.Checked = False
            'ChkABooking.SelectedIndex = 0
            'ChkListStatus.SelectedIndex = -1
            'ChkGrpProductivity.Checked = False
            'txtRecordCount.Text = "0"
            'txtFrom.CssClass = "textboxgrey"
            'txtTo.CssClass = "textboxgrey"
            'txtFrom.Enabled = False
            'txtTo.Enabled = False
            'grdNewFormat.DataSource = Nothing
            'grdNewFormat.DataBind()
            'GrdExcessMonthBreak.DataSource = Nothing
            'GrdExcessMonthBreak.DataBind()
            'GrdExcessAvg.DataSource = Nothing
            'GrdExcessAvg.DataBind()
            'grdvShowAvg.DataSource = Nothing
            'grdvShowAvg.DataBind()
            'grdvMonthlyBReak.DataSource = Nothing
            'grdvMonthlyBReak.DataBind()
            'lblFound.Visible = False
            'txtRecordCount.Visible = False
            'Dim strBuilder As New StringBuilder
            'Dim objSecurityXml As New XmlDocument

            'If Session("Security") IsNot Nothing Then
            '    objSecurityXml.LoadXml(Session("Security"))

            '    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
            '        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
            '            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
            '                Dim li As ListItem
            '                li = drp1AOffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
            '                If li IsNot Nothing Then
            '                    drp1AOffice.SelectedValue = li.Value

            '                End If
            '            End If
            '            drp1AOffice.Enabled = False
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdvMonthlyBReak_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMonthlyBReak.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        ' Dim pp As String = FooterDataset.Tables("PAGE_TOTAL").Rows.Count.ToString

                        Dim TotAir As Label
                        TotAir = CType(e.Row.FindControl("TotAir"), Label)
                        TotAir.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString

                        Dim TotCar As Label
                        TotCar = CType(e.Row.FindControl("TotCar"), Label)
                        TotCar.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString

                        Dim TotHotel As Label
                        TotHotel = CType(e.Row.FindControl("TotHotel"), Label)
                        TotHotel.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString

                        Dim TotIns As Label
                        TotIns = CType(e.Row.FindControl("TotIns"), Label)
                        TotIns.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString

                        Dim TotSum As Label
                        TotSum = CType(e.Row.FindControl("TotSum"), Label)
                        TotSum.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString

                        Dim TotPassive As Label
                        TotPassive = CType(e.Row.FindControl("TotPassive"), Label)
                        TotPassive.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE").ToString


                        Dim TotWithPassive As Label
                        TotWithPassive = CType(e.Row.FindControl("TotWithPassive"), Label)
                        TotWithPassive.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE").ToString

                    End If
                End If
            End If
            Dim objSecurityXml As New XmlDocument
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(11).Text = "Total"
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TOTAL")
                ' Totalsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                ' Dim TotSum As Label
                ' TotSum = CType(e.Row.FindControl("TotSum"), Label)
                'TotSum.Text = Totalsum.ToString
            End If


            If RdShowMon.Checked = True Then
                If ChkABooking.Items(0).Selected = True Then
                    e.Row.Cells(13).Visible = True
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "AIR_NETBOOKINGS")
                        'Airsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotAir As Label
                        'TotAir = CType(e.Row.FindControl("TotAir"), Label)
                        'TotAir.Text = Airsum.ToString
                    End If
                Else
                    e.Row.Cells(13).Visible = False
                End If
                If ChkABooking.Items(1).Selected = True Then
                    e.Row.Cells(14).Visible = True

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "CAR_NETBOOKINGS")
                        ' Carsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotCar As Label
                        'TotCar = CType(e.Row.FindControl("TotCar"), Label)
                        ' TotCar.Text = Carsum.ToString
                    End If

                Else
                    e.Row.Cells(14).Visible = False
                End If
                If ChkABooking.Items(2).Selected = True Then
                    e.Row.Cells(15).Visible = True

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "HOTEL_NETBOOKINGS")
                        ' Hotelsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotHotel As Label
                        ' TotHotel = CType(e.Row.FindControl("TotHotel"), Label)
                        ' TotHotel.Text = Hotelsum.ToString
                    End If



                Else
                    e.Row.Cells(15).Visible = False
                End If
                If ChkABooking.Items(3).Selected = True Then
                    e.Row.Cells(16).Visible = True
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "INSURANCE_NETBOOKINGS")
                        ' Inssum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotIns As Label
                        ' TotIns = CType(e.Row.FindControl("TotIns"), Label)
                        ' TotIns.Text = Inssum.ToString
                    End If

                Else
                    e.Row.Cells(16).Visible = False
                End If

                If ChkListStatus.Items(0).Selected = True Then
                    e.Row.Cells(8).Visible = True
                Else
                    e.Row.Cells(8).Visible = False
                End If
                If ChkListStatus.Items(1).Selected = True Then
                    e.Row.Cells(3).Visible = True
                    ' GrdExcessMonthBreak.Columns(3).ItemStyle.Wrap = False
                    'GrdExcessMonthBreak.Columns(3).ItemStyle.Width = GrdExcessMonthBreak.Columns(3).ItemStyle.Width.Value
                Else
                    e.Row.Cells(3).Visible = False
                End If
                If ChkListStatus.Items(2).Selected = True Then
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(7).Visible = False
                End If

                If ChkChainCode.Checked = True Then
                    e.Row.Cells(0).Visible = True
                Else
                    e.Row.Cells(0).Visible = False
                End If
                If ChkOrigBook.Checked = True Then
                    e.Row.Cells(18).Visible = True
                    e.Row.Cells(19).Visible = True
                Else
                    e.Row.Cells(18).Visible = False
                    e.Row.Cells(19).Visible = False
                End If


                If ChkGroupClass.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

            End If
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim gdata, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String

            gdata = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""
            If ChkGrpProductivity.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrigBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            ResStaffId = ""
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId.Replace("'", "")
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If

            End If
            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = hdAdd.Value.Replace("'", "")

            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            Dim linkDetails, linkCRs, linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor
            linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkCRs = CType(e.Row.FindControl("linkCRs"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)

            Dim strBuilder As New StringBuilder
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkDetails.Disabled = True
                    Else
                        ' linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonDetailsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                        linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonDetailsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")

                    End If
                Else
                    linkDetails.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonDetailsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkCRs.Disabled = True
                    Else
                        linkCRs.Attributes.Add("OnClick", "javascript:return SelectMonCrsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkCRs.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkCRs.Attributes.Add("OnClick", "javascript:return SelectMonCrsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkABreakUp.Disabled = True
                    Else
                        linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectMonBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkABreakUp.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectMonBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If


            ' linkDetails.Attributes.Add("OnClick", "javascript:return SelectMonDetailsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")

            'linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectMonBreakFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            'linkCRs.Attributes.Add("OnClick", "javascript:return SelectMonCrsFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")


            'Dim strStatus As String = e.Row.Cells(5).Text.Trim


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvShowAvg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvShowAvg.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        Dim pp As String = FooterDataset.Tables("PAGE_TOTAL").Rows.Count.ToString

                        Dim TotAir As Label
                        TotAir = CType(e.Row.FindControl("TotAir"), Label)
                        TotAir.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString

                        Dim TotCar As Label
                        TotCar = CType(e.Row.FindControl("TotCar"), Label)
                        TotCar.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString

                        Dim TotHotel As Label
                        TotHotel = CType(e.Row.FindControl("TotHotel"), Label)
                        TotHotel.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString

                        Dim TotIns As Label
                        TotIns = CType(e.Row.FindControl("TotIns"), Label)
                        TotIns.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString

                        Dim TotAvg As Label
                        TotAvg = CType(e.Row.FindControl("TotAvg"), Label)
                        TotAvg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AVERAGE").ToString

                        Dim TotPassive As Label
                        TotPassive = CType(e.Row.FindControl("TotPassive"), Label)
                        TotPassive.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE").ToString


                        Dim TotWithPassive As Label
                        TotWithPassive = CType(e.Row.FindControl("TotWithPassive"), Label)
                        TotWithPassive.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE").ToString

                    End If
                End If
            End If

            Dim objSecurityXml As New XmlDocument
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(5).Text = "Total"
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "AVERAGE")
                ' TotalAvg += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                'Dim TotAvg As Label
                ' TotAvg = CType(e.Row.FindControl("TotAvg"), Label)
                ' TotAvg.Text = TotalAvg.ToString
            End If

            If RdShowAvg.Checked = True Then
                If ChkABooking.Items(0).Selected = True Then
                    e.Row.Cells(11).Visible = True
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "AIR_NETBOOKINGS")
                        ' Airsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotAir As Label
                        ' TotAir = CType(e.Row.FindControl("TotAir"), Label)
                        ' TotAir.Text = Airsum.ToString
                    End If

                Else
                    e.Row.Cells(11).Visible = False
                End If
                If ChkABooking.Items(1).Selected = True Then
                    e.Row.Cells(12).Visible = True
                    '  e.Row.Cells(12).Visible = True

                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "CAR_NETBOOKINGS")
                        'Carsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotCar As Label
                        'TotCar = CType(e.Row.FindControl("TotCar"), Label)
                        ' TotCar.Text = Carsum.ToString
                    End If

                Else
                    e.Row.Cells(12).Visible = False
                End If
                If ChkABooking.Items(2).Selected = True Then
                    e.Row.Cells(13).Visible = True
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "HOTEL_NETBOOKINGS")
                        ' Hotelsum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotHotel As Label
                        ' TotHotel = CType(e.Row.FindControl("TotHotel"), Label)
                        ' TotHotel.Text = Hotelsum.ToString
                    End If
                Else
                    e.Row.Cells(13).Visible = False
                End If
                If ChkABooking.Items(3).Selected = True Then
                    e.Row.Cells(14).Visible = True
                    If e.Row.RowType = DataControlRowType.DataRow Then
                        ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "INSURANCE_NETBOOKINGS")
                        ' Inssum += sum
                    End If
                    If e.Row.RowType = DataControlRowType.Footer Then
                        ' Dim TotIns As Label
                        ' TotIns = CType(e.Row.FindControl("TotIns"), Label)
                        ' TotIns.Text = Inssum.ToString
                    End If

                Else
                    e.Row.Cells(14).Visible = False
                End If

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
                If ChkChainCode.Checked = True Then
                    e.Row.Cells(0).Visible = True
                Else
                    e.Row.Cells(0).Visible = False
                End If
                If ChkOrigBook.Checked = True Then
                    e.Row.Cells(16).Visible = True
                    e.Row.Cells(17).Visible = True
                Else
                    e.Row.Cells(16).Visible = False
                    e.Row.Cells(17).Visible = False
                End If

                If ChkGroupClass.Checked = True Then
                    e.Row.Cells(9).Visible = True
                Else
                    e.Row.Cells(9).Visible = False
                End If

            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim gdata, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String
            gdata = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""
            If ChkGrpProductivity.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrigBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId.Replace("'", "")
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If

            End If
            Dim linkDetails, linkCRs, linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor

            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkCRs = CType(e.Row.FindControl("linkCRs"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)

            Dim strBuilder As New StringBuilder
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkDetails.Disabled = True
                    Else
                        linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkDetails.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkCRs.Disabled = True
                    Else
                        linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkCRs.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkABreakUp.Disabled = True
                    Else
                        linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkABreakUp.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            If FooterDataset IsNot Nothing Then
                If FooterDataset.Tables("TOTALS") IsNot Nothing Then
                    Dim pp As String = FooterDataset.Tables("TOTALS").Rows.Count.ToString
                    ' Dim kk As Integer
                End If
            End If
            'linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            ' linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            '  linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GrdExcessMonthBreak_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdExcessMonthBreak.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        '  Dim pp As String = FooterDataset.Tables("TOTALS").Rows.Count.ToString

                        Dim TotSum As Label
                        TotSum = CType(e.Row.FindControl("TotSum"), Label)
                        TotSum.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString

                        Dim TotTrans As Label
                        TotTrans = CType(e.Row.FindControl("TotTrans"), Label)
                        TotTrans.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TRANSACTIONS").ToString

                        Dim TotTransPerSeg As Label
                        TotTransPerSeg = CType(e.Row.FindControl("TotTransPerSeg"), Label)
                        TotTransPerSeg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TRANPERSEG").ToString


                    End If
                End If
            End If
         

            Dim objSecurityXml As New XmlDocument

            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(9).Text = "Total"
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TOTAL")
                'Totalsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                'Dim TotSum As Label
                'TotSum = CType(e.Row.FindControl("TotSum"), Label)
                'TotSum.Text = Totalsum.ToString
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TRANSACTIONS")
                ' TotalTrans += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                ' Dim TotTrans As Label
                ' TotTrans = CType(e.Row.FindControl("TotTrans"), Label)
                ' TotTrans.Text = TotalTrans.ToString
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                ' Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TRANPERSEG")
                '  TotalTransSeg += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                ' Dim TotTransPerSeg As Label
                ' TotTransPerSeg = CType(e.Row.FindControl("TotTransPerSeg"), Label)
                ' TotTransPerSeg.Text = TotalTransSeg.ToString
            End If


            If RdShowMon.Checked = True Then
                If ChkListStatus.Items(0).Selected = True Then
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(7).Visible = False
                End If
                If ChkListStatus.Items(1).Selected = True Then
                    e.Row.Cells(3).Visible = True
                Else
                    e.Row.Cells(3).Visible = False
                End If
                If ChkListStatus.Items(2).Selected = True Then
                    e.Row.Cells(6).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                End If
                If ChkChainCode.Checked = True Then
                    e.Row.Cells(0).Visible = True
                Else
                    e.Row.Cells(0).Visible = False
                End If

            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim gdata, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String
            gdata = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""
            If ChkGrpProductivity.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrigBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId.Replace("'", "")
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If

            End If
            Dim linkDetails, linkCRs, linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor

            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkCRs = CType(e.Row.FindControl("linkCRs"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)

            Dim strBuilder As New StringBuilder
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkDetails.Disabled = True
                    Else
                        linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkDetails.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkCRs.Disabled = True
                    Else
                        linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkCRs.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkABreakUp.Disabled = True
                    Else
                        linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkABreakUp.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            '  linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            '  linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            ' linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcMonFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GrdExcessAvg_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdExcessAvg.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        ' Dim pp As String = FooterDataset.Tables("PAGE_TOTAL").Rows.Count.ToString

                        Dim TotAvg As Label
                        TotAvg = CType(e.Row.FindControl("TotAvg"), Label)
                        TotAvg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows.Count.ToString

                        Dim TotTrans As Label
                        TotTrans = CType(e.Row.FindControl("TotTrans"), Label)
                        TotTrans.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TRANSACTIONS").ToString

                        Dim TotTransPerSeg As Label
                        TotTransPerSeg = CType(e.Row.FindControl("TotTransPerSeg"), Label)
                        TotTransPerSeg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TRANPERSEG").ToString



                    End If
                End If
            End If
          



            Dim objSecurityXml As New XmlDocument
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(5).Text = "Total"
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                '  Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "AVERAGE")
                ' TotalAvg += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                'Dim TotAvg As Label
                ' TotAvg = CType(e.Row.FindControl("TotAvg"), Label)
                ' TotAvg.Text = TotalAvg.ToString
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TRANSACTIONS")
                'TotalTrans += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                ' Dim TotTrans As Label
                ' TotTrans = CType(e.Row.FindControl("TotTrans"), Label)
                ' TotTrans.Text = TotalTrans.ToString
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Long = DataBinder.Eval(e.Row.DataItem, "TRANPERSEG")
                'TotalTransSeg += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                ' Dim TotTransPerSeg As Label
                ' TotTransPerSeg = CType(e.Row.FindControl("TotTransPerSeg"), Label)
                ' TotTransPerSeg.Text = TotalTransSeg.ToString
            End If

            If RdShowAvg.Checked = True Then

                If ChkListStatus.Items(0).Selected = True Then
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(7).Visible = False
                End If
                If ChkListStatus.Items(1).Selected = True Then
                    e.Row.Cells(3).Visible = True
                Else
                    e.Row.Cells(3).Visible = False
                End If
                If ChkListStatus.Items(2).Selected = True Then
                    e.Row.Cells(6).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                End If
                If ChkChainCode.Checked = True Then
                    e.Row.Cells(0).Visible = True
                Else
                    e.Row.Cells(0).Visible = False
                End If

            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim gdata, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String
            gdata = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""
            If ChkGrpProductivity.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrigBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId.Replace("'", "")
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If

            End If
            Dim linkDetails, linkCRs, linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor

            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkCRs = CType(e.Row.FindControl("linkCRs"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)


            Dim strBuilder As New StringBuilder
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkDetails.Disabled = True
                    Else
                        linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkDetails.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkCRs.Disabled = True
                    Else
                        linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkCRs.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        linkABreakUp.Disabled = True
                    Else
                        linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    linkABreakUp.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(1).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            End If
            ' linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            '   linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            '  linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            If FooterDataset IsNot Nothing Then
                If FooterDataset.Tables("TOTALS") IsNot Nothing Then
                    Dim pp As String = FooterDataset.Tables("TOTALS").Rows.Count.ToString
                    ' Dim kk As Integer
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            'If ChkExTrans.Checked = True And RdShowMon.Checked = True Then
            '    PrepareGridViewForExport(GrdExcessMonthBreak)
            '    ExportGridView(GrdExcessMonthBreak)
            'End If
            'If ChkExTrans.Checked = True And RdShowAvg.Checked = True Then
            '    PrepareGridViewForExport(GrdExcessAvg)
            '    ExportGridView(GrdExcessAvg)
            'End If
            'If ChkExTrans.Checked = False Then

            '    If RdShowMon.Checked = True Then
            '        PrepareGridViewForExport(grdvMonthlyBReak)
            '        ExportGridView(grdvMonthlyBReak)
            '    ElseIf RdShowAvg.Checked = True Then
            '        PrepareGridViewForExport(grdvShowAvg)
            '        ExportGridView(grdvShowAvg)
            '    End If
            'End If
            grdNewFormat.AllowSorting = False
            grdNewFormat.HeaderStyle.ForeColor = Drawing.Color.Black

            grdvShowAvg.AllowSorting = False
            grdvShowAvg.HeaderStyle.ForeColor = Drawing.Color.Black

            grdvMonthlyBReak.AllowSorting = False
            grdvMonthlyBReak.HeaderStyle.ForeColor = Drawing.Color.Black

            GrdExcessMonthBreak.AllowSorting = False
            GrdExcessMonthBreak.HeaderStyle.ForeColor = Drawing.Color.Black

            grdNewFormat.AllowSorting = False
            grdNewFormat.HeaderStyle.ForeColor = Drawing.Color.Black

            ' BIDTSearch2()
            BIDTExport()
            'Exit Sub
            'If GrdExcessMonthBreak.Rows.Count > 0 Then

            '    'PrepareGridViewForExport(GrdExcessMonthBreak)
            '    GrdExcessMonthBreak.Columns(GrdExcessMonthBreak.Columns.Count - 1).Visible = False

            '    ExportGridView(GrdExcessMonthBreak, "BIDTExcessMonth.xls")
            'ElseIf GrdExcessAvg.Rows.Count > 0 Then
            '    'PrepareGridViewForExport(GrdExcessAvg)
            '    GrdExcessAvg.Columns(GrdExcessAvg.Columns.Count - 1).Visible = False
            '    ExportGridView(GrdExcessAvg, "BIDTExcessAvg.xls")
            'ElseIf grdvMonthlyBReak.Rows.Count > 0 Then
            '    'grdvMonthlyBReak.Columns.Remove(grdvMonthlyBReak.Columns(grdvMonthlyBReak.Columns.Count - 1))
            '    grdvMonthlyBReak.Columns(grdvMonthlyBReak.Columns.Count - 1).Visible = False
            '    'PrepareGridViewForExport(grdvMonthlyBReak)
            '    ExportGridView(grdvMonthlyBReak, "BIDTMonthBreak.xls")

            'ElseIf grdvShowAvg.Rows.Count > 0 Then
            '    ' PrepareGridViewForExport(grdvShowAvg)
            '    grdvShowAvg.Columns(grdvShowAvg.Columns.Count - 1).Visible = False
            '    ExportGridView(grdvShowAvg, "BIDTAvgBreak.xls")
            'ElseIf grdNewFormat.Rows.Count > 0 Then

            '    'PrepareGridViewForExport(grdNewFormat)
            '    grdNewFormat.Columns(grdNewFormat.Columns.Count - 1).Visible = False
            '    ExportGridView(grdNewFormat, "BIDTNewFormat.xls")
            'End If
            ' lblError.Text = grdvMonthlyBReak.Rows.Count

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Private Sub ExportGridView(ByVal gv2 As GridView, ByVal FileName As String)
        Try
            Dim attachment As String = "attachment; filename=" + FileName
            Response.ClearContent()
            Response.AddHeader("content-disposition", attachment)
            Response.ContentType = "application/ms-excel"
            Dim sw As New StringWriter
            Dim htw As New HtmlTextWriter(sw)
            Dim frm As New HtmlForm()
            Dim pp As String



            gv2.Parent.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(gv2)
            frm.RenderControl(htw)
            pp = sw.ToString()
            pp = pp.ToString().Replace("Action", "")
            pp = pp.ToString().Replace("Details", "")
            pp = pp.ToString().Replace("CRS", "")
            pp = pp.ToString().Replace("CRS Details", "")
            pp = pp.ToString().Replace("1 A BreakUp", "")
            pp = pp.ToString().Replace("1A BreakUp", "")

            Response.Write(pp.ToString())
            Response.End()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    'End Sub
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        Try
            Dim l As New Literal
            Dim name As String = ""
            Dim lb As New LinkButton

            Dim i As Int32
            For i = 0 To gv.Controls.Count - 1
                If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
                    l.Text = CType(gv.Controls(i), LinkButton).Text
                    gv.Controls.Remove(gv.Controls(i))
                    gv.Controls.AddAt(i, l)

                End If
                'If (gv.Controls(i).GetType Is GetType(System.Web.UI.HtmlControls.HtmlAnchor)) Then
                '    l.Text = CType(gv.Controls(i), System.Web.UI.HtmlControls.HtmlAnchor).Name
                '    gv.Controls.Remove(gv.Controls(i))
                '    gv.Controls.AddAt(i, l)

                'End If


                If (gv.Controls(i).HasControls()) Then
                    PrepareGridViewForExport(gv.Controls(i))
                End If

            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdNewFormat_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNewFormat.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If e.Row.Cells(1).Text.Trim.Length <= 0 Then
                Exit Sub
            ElseIf e.Row.Cells(1).Text.Trim = "&nbsp;" Then
                e.Row.Cells.Clear()
                Exit Sub

            End If
            'If ChkListStatus.Items(1).Selected = True Then
            '    e.Row.Cells(9).Visible = True

            'Else
            '    e.Row.Cells(9).Visible = False
            'End If
            'If ChkListStatus.Items(2).Selected = True Then
            '    e.Row.Cells(4).Visible = True
            'Else
            '    e.Row.Cells(4).Visible = False
            'End If
            'If ChkListStatus.Items(3).Selected = True Then
            '    e.Row.Cells(8).Visible = True
            'Else
            '    e.Row.Cells(8).Visible = False
            '    'grdNewFormat.Columns(8).Visible = True
            'End If

            Dim objSecurityXml As New XmlDocument
            Dim gdata, useorignal, air, car, hotel, insurance, ResStaffId, LimAoff, LimReg, LimOwnOff As String

            gdata = ""
            useorignal = ""
            air = ""
            car = ""
            hotel = ""
            insurance = ""
            ResStaffId = ""
            LimAoff = ""
            LimReg = ""
            LimOwnOff = ""
            If ChkGrpProductivity.Checked = True Then
                gdata = "1"
            Else
                gdata = "0"
            End If

            If ChkOrigBook.Checked = True Then
                useorignal = "Y"
            Else
                useorignal = "N"
            End If

            If ChkABooking.Items(0).Selected = True Then
                air = "1"
            Else
                air = "0"
            End If
            If ChkABooking.Items(1).Selected = True Then
                car = "1"
            Else
                car = "0"

            End If
            If ChkABooking.Items(2).Selected = True Then
                hotel = "1"
            Else
                hotel = "0"
            End If
            If ChkABooking.Items(3).Selected = True Then
                insurance = "1"
            Else
                insurance = "0"
            End If
            If Not Session("LoginSession") Is Nothing Then
                ResStaffId = Session("LoginSession").ToString().Split("|")(0)
            End If
            ResStaffId.Replace("'", "")
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If

            End If
            Dim linkDetails, linkCRs, linkABreakUp As System.Web.UI.HtmlControls.HtmlAnchor
            'DETAILS CHAIN_CODE='' 
            'LOCATION_CODE='' 
            'AGENCYNAME='' SALESEXECUTIVE=''
            ' CITY='' OFFICEID=''
            '            TOTALJAN2006 = "13"
            '            JANUARY2006AIR = "13"
            '            JANUARY2006INSURANCE = "0"
            'TOTALFEB2006="20" FEBRUARY2006AIR="20" FEBRUARY2006INSURANCE="0" TOTALMAR2006="6" MARCH2006AIR="6" MARCH2006INSURANCE="0" TOTALAPR2006="16" APRIL2006AIR="16" APRIL2006INSURANCE="0" TOTALMAY2006="2" MAY2006AIR="2" MAY2006INSURANCE="0" TOTALJUN2006="23" JUNE2006AIR="23" JUNE2006INSURANCE="0" TOTALJUL2006="1" JULY2006AIR="1" JULY2006INSURANCE="0" TOTALAUG2006="173" AUGUST2006AIR="173" AUGUST2006INSURANCE="0" TOTALSEP2006="30" SEPTEMBER2006AIR="30" SEPTEMBER2006INSURANCE="0" TOTALOCT2006="9" OCTOBER2006AIR="9" OCTOBER2006INSURANCE="0" TOTALNOV2006="-78" NOVEMBER2006AIR="-78" NOVEMBER2006INSURANCE="0" TOTALDEC2006="-27" DECEMBER2006AIR="-27" DECEMBER2006INSURANCE="0" TOTALJAN2007="-28" JANUARY2007AIR="-28" JANUARY2007INSURANCE="0" TOTALFEB2007="14" FEBRUARY2007AIR="14" FEBRUARY2007INSURANCE="0" TOTAL="174" />


            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            linkDetails = CType(e.Row.FindControl("linkDetails"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkCRs = CType(e.Row.FindControl("linkCRs"), System.Web.UI.HtmlControls.HtmlAnchor)
            linkABreakUp = CType(e.Row.FindControl("linkABreakUp"), System.Web.UI.HtmlControls.HtmlAnchor)

            linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")



            Dim strBuilder As New StringBuilder
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        e.Row.Cells(e.Row.Cells.Count - 1).Text = "Details"
                    Else
                        e.Row.Cells(e.Row.Cells.Count - 1).Text = "<a href='#' class='LinkButtons' onclick=" + """" + "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "Details </a>"
                    End If
                Else
                    e.Row.Cells(e.Row.Cells.Count - 1).Text = "Details"
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                e.Row.Cells(e.Row.Cells.Count - 1).Text = "<a href='#' class='LinkButtons' onclick=" + """" + "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "Details </a>"
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "CRS Details "
                    Else
                        e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'   class='LinkButtons' onclick=" + """" + "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "CRS Details </a>"
                    End If
                Else

                    e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "CRS Details "

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'   class='LinkButtons' onclick=" + """" + "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "CRS Details </a>"
            End If

            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Breakup']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "1A BreakUp "
                    Else
                        e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'  class='LinkButtons'  onclick=" + """" + "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "1A BreakUp </a>"
                    End If
                Else
                    e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "1A BreakUp "
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'  class='LinkButtons'  onclick=" + """" + "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(2).Text.Trim) + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(7).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "1A BreakUp </a>"
            End If






            '  e.Row.Cells(e.Row.Cells.Count - 1).Text = "<a href='#' class='LinkButtons' onclick=" + """" + "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(2).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "Details </a>"
            ' e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'   class='LinkButtons' onclick=" + """" + "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(2).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "CRS Details </a>"
            ' e.Row.Cells(e.Row.Cells.Count - 1).Text += "&nbsp;&nbsp" + "<a href='#'  class='LinkButtons'  onclick=" + """" + "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(2).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(3).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');" + """" + " >" + "1A BreakUp </a>"

            e.Row.Cells(e.Row.Cells.Count - 1).Wrap = False


            ' e.Row.Cells(e.Row.Cells.Count - 1).Width = 200
            'linkDetails.Attributes.Add("OnClick", "javascript:return SelectDetailsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            'linkABreakUp.Attributes.Add("OnClick", "javascript:return SelectBreakExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")
            'linkCRs.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + e.Row.Cells(1).Text + "','" + drp1AOffice.SelectedValue + "','" + gdata + "','" + useorignal + "','" + ResStaffId + "','" + air + "','" + car + "','" + hotel + "','" + insurance + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(5).Text.ToString + "','" + hdCountry.Value + "');")

            'Dim Temp As TableCell = e.Row.Cells(0)
            'If Temp.Text = "Action" Then
            '    grdNewFormat.Controls.RemoveAt(0)
            '    grdNewFormat.Controls.AddAt(grdNewFormat.Controls.Count, Temp)
            'End If
            'ds.Tables("DETAILS")
            

        Catch ex As Exception

        End Try
    End Sub





    Protected Sub grdNewFormat_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdNewFormat.RowCreated
        Try
            'If e.Row.Cells(1).Text.Trim.Length <= 0 Then
            '    Exit Sub
            'ElseIf e.Row.Cells(1).Text.Trim = "&nbsp;" Then
            '    e.Row.Cells.Clear()
            '    Exit Sub

            'End If
            e.Row.Cells(0).Visible = False
            ' If e.Row.RowType = DataControlRowType.Header Then
            If ChkListStatus.Items(0).Selected = True Then
                e.Row.Cells(10).Visible = True
            Else
                e.Row.Cells(10).Visible = False
            End If
            If ChkListStatus.Items(1).Selected = True Then
                e.Row.Cells(4).Visible = True
            Else
                e.Row.Cells(4).Visible = False
            End If
            If ChkListStatus.Items(2).Selected = True Then
                e.Row.Cells(9).Visible = True
            Else
                e.Row.Cells(9).Visible = False
                'grdNewFormat.Columns(8).Visible = True
            End If

            If ChkChainCode.Checked = True Then
                e.Row.Cells(1).Visible = True
            Else
                e.Row.Cells(1).Visible = False
            End If


            If ChkGroupClass.Checked = True Then
                e.Row.Cells(11).Visible = True
            Else
                e.Row.Cells(11).Visible = False
            End If

            ' End If
            If e.Row.RowType = DataControlRowType.Header Then
                Dim grvRow As GridViewRow
                grvRow = e.Row
                If e.Row.RowType = DataControlRowType.Header Then
                    If grdNewFormat.AllowSorting = True Then
                        CType(grvRow.Cells(2).Controls(0), LinkButton).Text = "LCODE"
                    Else
                        e.Row.Cells(2).Text = "LCODE"
                    End If
                    If grdNewFormat.AllowSorting = True Then
                        CType(grvRow.Cells(1).Controls(0), LinkButton).Text = "CHAINCODE"
                    Else
                        e.Row.Cells(1).Text = "CHAINCODE"
                    End If

                    If grdNewFormat.AllowSorting = True Then
                        CType(grvRow.Cells(11).Controls(0), LinkButton).Text = "Agency Category" '"Type"
                    Else
                        e.Row.Cells(11).Text = "Agency Category" '"Type"
                    End If

                    If grdNewFormat.AllowSorting = True Then
                        CType(grvRow.Cells(6).Controls(0), LinkButton).Text = "Company Vertical" '"Type"
                    Else
                        e.Row.Cells(6).Text = "Company Vertical" '"Type"

                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.Footer Then

            End If
           
        Catch ex As Exception

        End Try
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BIDTSearch()
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
            BIDTSearch()

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

    Protected Sub grdvShowAvg_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvShowAvg.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvShowAvg_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvShowAvg.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                'ViewState("Desc") = "FALSE"

                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @AIR_NETBOOKINGS, CAR_NETBOOKINGS, HOTEL_NETBOOKINGS, INSURANCE_NETBOOKINGS, AVERAGE, PASSIVE, WITHPASSIVE,
                If SortName.Trim().ToUpper = "AIR_NETBOOKINGS" Or SortName.Trim().ToUpper = "CAR_NETBOOKINGS" Or SortName.Trim().ToUpper = "HOTEL_NETBOOKINGS" Or SortName.Trim().ToUpper = "INSURANCE_NETBOOKINGS" Or SortName.Trim().ToUpper = "AVERAGE" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                    ViewState("Desc") = "TRUE"
                Else
                    ViewState("Desc") = "FALSE"
                End If
                '@ End of Added Code For Default descending sorting order on first time  of following Fields

            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    'ViewState("Desc") = "FALSE"

                    '@ Added Code For Default descending sorting on first time  of following Fields      
                    ' @AIR_NETBOOKINGS, CAR_NETBOOKINGS, HOTEL_NETBOOKINGS, INSURANCE_NETBOOKINGS, AVERAGE, PASSIVE, WITHPASSIVE,
                    If SortName.Trim().ToUpper = "AIR_NETBOOKINGS" Or SortName.Trim().ToUpper = "CAR_NETBOOKINGS" Or SortName.Trim().ToUpper = "HOTEL_NETBOOKINGS" Or SortName.Trim().ToUpper = "INSURANCE_NETBOOKINGS" Or SortName.Trim().ToUpper = "AVERAGE" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            BIDTSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvShowAvg_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvShowAvg.RowCreated
        Try

        Catch ex As Exception
        End Try
    End Sub



    Protected Sub GrdExcessMonthBreak_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdExcessMonthBreak.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GrdExcessMonthBreak_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdExcessMonthBreak.Sorting
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
            BIDTSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub GrdExcessMonthBreak_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdExcessMonthBreak.RowCreated
        Try

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GrdExcessAvg_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdExcessAvg.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GrdExcessAvg_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdExcessAvg.Sorting
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
            BIDTSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub GrdExcessAvg_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdExcessAvg.RowCreated
        Try

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub grdNewFormat_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdNewFormat.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdNewFormat_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdNewFormat.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"


                '@ Added Code For Default Ascending  sorting order on first time  of following Fields   ant others are in Ascending    
                ' @   CHAIN_CODE, LOCATION_CODE, AGENCYNAME,  ADDRESS,  SALESEXECUTIVE,  CITY,  OFFICEID, COUNTRY, ONLINE_STATUS, GROUP_CLASSIFICATION_NAME
                If SortName.Trim().ToUpper = "CHAIN_CODE" Or SortName.Trim().ToUpper = "LOCATION_CODE" Or SortName.Trim().ToUpper = "AGENCYNAME" Or SortName.Trim().ToUpper = "ADDRESS" Or SortName.Trim().ToUpper = "SALESEXECUTIVE" Or SortName.Trim().ToUpper = "CITY" Or SortName.Trim().ToUpper = "OFFICEID" Or SortName.Trim().ToUpper = "COUNTRY" Or SortName.Trim().ToUpper = "ONLINE_STATUS" Or SortName.Trim().ToUpper = "GROUP_CLASSIFICATION_NAME" Then
                    ViewState("Desc") = "FALSE"
                Else
                    ViewState("Desc") = "TRUE"
                End If
                '@ Added Code For Default descending sorting order on first time  of following Fields

             
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    'ViewState("Desc") = "FALSE"

                    '@ Added Code For Default Ascending order sorting on first time  of following Fields   ant others are in Ascending    
                    ' @   CHAIN_CODE, LOCATION_CODE, AGENCYNAME,  ADDRESS,  SALESEXECUTIVE,  CITY,  OFFICEID, COUNTRY, ONLINE_STATUS, GROUP_CLASSIFICATION_NAME
                    If SortName.Trim().ToUpper = "CHAIN_CODE" Or SortName.Trim().ToUpper = "LOCATION_CODE" Or SortName.Trim().ToUpper = "AGENCYNAME" Or SortName.Trim().ToUpper = "ADDRESS" Or SortName.Trim().ToUpper = "SALESEXECUTIVE" Or SortName.Trim().ToUpper = "CITY" Or SortName.Trim().ToUpper = "OFFICEID" Or SortName.Trim().ToUpper = "COUNTRY" Or SortName.Trim().ToUpper = "ONLINE_STATUS" Or SortName.Trim().ToUpper = "GROUP_CLASSIFICATION_NAME" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                    '@ Added Code For Default descending sorting order on first time  of following Fields

                End If
            End If
            BIDTSearch()


            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
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
    Private Sub SetImageForSorting2(ByVal grd As GridView, ByVal ds2 As DataSet)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        '  Dim field As DataControlField
        For i As Integer = 0 To ds2.Tables("DETAILS").Columns.Count - 1
            If ds2.Tables("DETAILS").Columns(i).Caption.ToString.Trim = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = i + 1
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next

        ' grd.HeaderRow.Cells(0).n()
    End Sub
#Region "BIDTSearch2"
    Private Sub BIDTSearch2()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            '  objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")
            '<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>
            '<SHOWOFFICEID></SHOWOFFICEID>
            '<SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> 
            '<AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> 
            '<AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> 
            '<FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL>
            ' <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE>
            '<SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA>
            ' <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> 
            '<AGENCYTYPEID></AGENCYTYPEID> 
            '<RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> 
            '<INCLUDETRANSACTION> </INCLUDETRANSACTION>
            ' <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE>
            '</INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> 
            '</LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION>
            ' <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY>
            '</PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>
            objInputXml.DocumentElement.SelectSingleNode("SHOWOFFICEID").InnerText = ""

            If RdShowAvg.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 0
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            objInputXml.DocumentElement.SelectSingleNode("GROUPID").InnerText = ""

            If (dlstCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (drp1AOffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1AOffice.SelectedValue
            End If

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue
            End If
            If (drpAgencyStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue
            End If
            If (drpAgencyType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue
            End If
            If (drpResStaff.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SALESPERSONID").InnerText = drpResStaff.SelectedValue
            End If

            ' objInputXml.DocumentElement.SelectSingleNode("FDATE").InnerText = objeAAMS.ConvertTextDate(txtFrom.Text)
            ' objInputXml.DocumentElement.SelectSingleNode("TDATE").InnerText = objeAAMS.ConvertTextDate(txtTo.Text)
            If (drpProductivity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SYMBOL").InnerText = Trim(drpProductivity.SelectedItem.Text)
            End If

            objInputXml.DocumentElement.SelectSingleNode("FVALUE").InnerText = txtFrom.Text

            objInputXml.DocumentElement.SelectSingleNode("SVALUE").InnerText = txtTo.Text

            If ChkGrpProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If

            If ChkOrigBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If

            'If ChkListStatus.Items(1).Selected = True Then

            'Else

            'End If
            'If ChkListStatus.Items(2).Selected = True Then

            'Else

            'End If
            'If ChkListStatus.Items(3).Selected = True Then

            'Else

            'End If
            If ChkExTrans.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "0"
            End If


            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If ChkNewFormat.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 0
            End If
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""


            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If
              
            End If

            'Start CODE for sorting and paging

            'objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""


            ' objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString

            ' Start This Code is Added For ReSetting Viwstate for Sorting
            If ViewState("PrevSearching") Is Nothing Then
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "NEWFORMAT" Or objNode.Name = "INCLUDETRANSACTION" Or objNode.Name = "SHOWAVG" Or objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                            ViewState("Desc") = "FALSE"
                        End If
                    End If
                Next
            End If


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE" '"CHAIN_CODE"
            Else
                If ViewState("SortName") = "ONLINE_STATUS" Then
                    If ChkListStatus.Items(0).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "ADDRESS" Then
                    If ChkListStatus.Items(1).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "COUNTRY" Then
                    If ChkListStatus.Items(2).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "CHAIN_CODE" Then
                    If ChkChainCode.Checked = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
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
            'Here Back end Method Call
            objOutputXml = objbzbzBIDT.BIDTProductivityMonthly_Average(objInputXml)
            ' objOutputXml.LoadXml("<PR_SEARCH_1A_PRODUCTIVITY_NEWFORMAT_BIDT_OUTPUT><DETAILS CHAIN_CODE='' LOCATION_CODE='' AGENCYNAME='' SALESEXECUTIVE='' CITY='' OFFICEID='' TOTALJAN2006='' JANUARY2006AIR='' TOTAL='174' />><Errors Status='FALSE'><Error Code='' Description=''/></Errors></PR_SEARCH_1A_PRODUCTIVITY_NEWFORMAT_BIDT_OUTPUT>")



            If ChkNewFormat.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    grdNewFormat.DataSource = ds.Tables("DETAILS")

                    If ds.Tables("DETAILS").Columns("ROWNUMBER") IsNot Nothing Then
                        ds.Tables("DETAILS").Columns.Remove(ds.Tables("DETAILS").Columns("ROWNUMBER"))
                    End If

                    ds.Tables("DETAILS").Columns.Add("Action               ")

                    FooterDataset = New DataSet
                    FooterDataset = ds
                    grdNewFormat.DataBind()
                    Dim intRow, IntColno, Inti As Integer
                    grdNewFormat.FooterRow.Cells(7).Text = "Total"
                    For IntColno = 10 To grdNewFormat.Rows(0).Cells.Count - 2
                        grdNewFormat.HeaderRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                        grdNewFormat.FooterRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right

                        grdNewFormat.FooterRow.Cells(IntColno).Text = 0
                    Next
                    For intRow = 0 To grdNewFormat.Rows.Count - 1
                        For IntColno = 10 To grdNewFormat.Rows(0).Cells.Count - 2
                            grdNewFormat.Rows(intRow).Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                            Dim sum As Double = grdNewFormat.Rows(intRow).Cells(IntColno).Text
                            'If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                            '    grdNewFormat.FooterRow.Cells(IntColno).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(IntColno).ToString
                            'End If
                            'grdNewFormat.FooterRow.Cells(IntColno).Text = CType(grdNewFormat.FooterRow.Cells(IntColno).Text, Decimal) + sum
                            ' grdNewFormat.FooterRow.Cells(IntColno).Text = String.Format("{0:d}", grdNewFormat.FooterRow.Cells(IntColno).Text.ToString)
                        Next

                    Next

                    Inti = 0
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        For Inti = 0 To FooterDataset.Tables("PAGE_TOTAL").Columns.Count - 1
                            grdNewFormat.FooterRow.Cells(Inti + 10).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(Inti).ToString
                        Next
                    End If

                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                Else
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

            If ChkExTrans.Checked = True And RdShowMon.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessMonthBreak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                   

                Else
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                   
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

            If ChkExTrans.Checked = True And RdShowAvg.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessAvg.DataSource = ds.Tables("BIDTAVERAGE")
                    GrdExcessAvg.DataBind()

                  
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()


                Else
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                  
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
            If ChkExTrans.Checked = False And ChkNewFormat.Checked = False Then

                If RdShowMon.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        'Session("DataSource") = ds.Tables("AGNECY")
                        FooterDataset = New DataSet
                        FooterDataset = ds
                        grdvMonthlyBReak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                        grdvMonthlyBReak.DataBind()

                      
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()


                    Else
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                      
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                ElseIf RdShowAvg.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        FooterDataset = New DataSet
                        FooterDataset = ds
                        'Session("DataSource") = ds.Tables("AGNECY")
                        grdvShowAvg.DataSource = ds.Tables("BIDTAVERAGE")
                        grdvShowAvg.DataBind()
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                       
                    Else
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            lblFound.Visible = False
            txtRecordCount.Visible = False
            pnlPaging.Visible = False
        Finally
            'lblFound.Visible = True
            txtRecordCount.Visible = True
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting In case Of Delete The Record
        ' ###################################################################
    End Sub

    Protected Sub ChkOrigBook_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOrigBook.CheckedChanged
        Try
            '  BIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "BIDTExport"
    Private Sub BIDTExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            '  objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT><SHOWOFFICEID></SHOWOFFICEID><SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> <AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> <AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> <FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL> <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE><SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA> <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> <AGENCYTYPEID></AGENCYTYPEID> <RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> <INCLUDETRANSACTION> </INCLUDETRANSACTION> <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE> </INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> </LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><GROUPTYPEID></GROUPTYPEID><TYPEID></TYPEID><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>")
            '<PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>
            '<SHOWOFFICEID></SHOWOFFICEID>
            '<SHOWAVG></SHOWAVG> <LCODE></LCODE> <GROUPID></GROUPID> 
            '<AGENCYNAME></AGENCYNAME> <CITY> </CITY> <COUNTRY></COUNTRY> 
            '<AOFFICE></AOFFICE> <FMONTH></FMONTH>  <TMONTH></TMONTH> 
            '<FYEAR>  </FYEAR><TYEAR></TYEAR> <SYMBOL></SYMBOL>
            ' <REGION></REGION>   <FVALUE></FVALUE> <SVALUE> </SVALUE>
            '<SALESPERSONID></SALESPERSONID> <GROUPDATA></GROUPDATA>
            ' <USEORIGINAL>  </USEORIGINAL> <AGENCYSTATUSID></AGENCYSTATUSID> 
            '<AGENCYTYPEID></AGENCYTYPEID> 
            '<RESPONSIBLESTAFFID></RESPONSIBLESTAFFID> 
            '<INCLUDETRANSACTION> </INCLUDETRANSACTION>
            ' <AIR> </AIR> <HOTEL> </HOTEL> <CAR></CAR> <INSURANCE>
            '</INSURANCE> <NEWFORMAT></NEWFORMAT> <LIMITED_TO_AOFFICE> 
            '</LIMITED_TO_AOFFICE>  <LIMITED_TO_REGION></LIMITED_TO_REGION>
            ' <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY>
            '</PR_SEARCH_PR_1A_PRODUCTIVITY_BIDT_INPUT>

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("SHOWOFFICEID").InnerText = ""

            If RdShowAvg.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWAVG").InnerText = 0
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011


            objInputXml.DocumentElement.SelectSingleNode("GROUPID").InnerText = ""

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPTYPEID").InnerText = drpLstGroupType.SelectedItem.Value
            End If


            If drpLstGroupClassType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("TYPEID").InnerText = drpLstGroupClassType.SelectedItem.Value
            End If

            If (dlstCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (drp1AOffice.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drp1AOffice.SelectedValue
            End If

            If (drpRegion.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue
            End If
            If (drpAgencyStatus.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue
            End If
            If (drpAgencyType.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue
            End If
            If (drpResStaff.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SALESPERSONID").InnerText = drpResStaff.SelectedValue
            End If

            ' objInputXml.DocumentElement.SelectSingleNode("FDATE").InnerText = objeAAMS.ConvertTextDate(txtFrom.Text)
            ' objInputXml.DocumentElement.SelectSingleNode("TDATE").InnerText = objeAAMS.ConvertTextDate(txtTo.Text)
            If (drpProductivity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("SYMBOL").InnerText = Trim(drpProductivity.SelectedItem.Text)
            End If

            objInputXml.DocumentElement.SelectSingleNode("FVALUE").InnerText = txtFrom.Text

            objInputXml.DocumentElement.SelectSingleNode("SVALUE").InnerText = txtTo.Text

            If ChkGrpProductivity.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("GROUPDATA").InnerText = "0"
            End If

            If ChkOrigBook.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                objInputXml.DocumentElement.SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If
            If ChkExTrans.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "1"
            Else
                objInputXml.DocumentElement.SelectSingleNode("INCLUDETRANSACTION").InnerText = "0"
            End If
            If ChkABooking.Items(0).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("AIR").InnerText = 0
            End If
            If ChkABooking.Items(1).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("CAR").InnerText = 0

            End If
            If ChkABooking.Items(2).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("HOTEL").InnerText = 0
            End If
            If ChkABooking.Items(3).Selected = True Then
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("INSURANCE").InnerText = 0
            End If
            If ChkNewFormat.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 1
            Else
                objInputXml.DocumentElement.SelectSingleNode("NEWFORMAT").InnerText = 0
            End If
            objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""


            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
                End If

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

            End If
            ' Start This Code is Added For ReSetting Viwstate for Sorting
            If ViewState("PrevSearching") Is Nothing Then
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList
                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name = "NEWFORMAT" Or objNode.Name = "INCLUDETRANSACTION" Or objNode.Name = "SHOWAVG" Or objNode.Name = "AIR" Or objNode.Name = "CAR" Or objNode.Name = "HOTEL" Or objNode.Name = "INSURANCE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                            ViewState("Desc") = "FALSE"
                        End If
                    End If
                Next
            End If
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LOCATION_CODE" '"CHAIN_CODE"
            Else
                If ViewState("SortName") = "ONLINE_STATUS" Then
                    If ChkListStatus.Items(0).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "ADDRESS" Then
                    If ChkListStatus.Items(1).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "COUNTRY" Then
                    If ChkListStatus.Items(2).Selected = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "CHAIN_CODE" Then
                    If ChkChainCode.Checked = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
                    End If
                End If
                If ViewState("SortName") = "GROUP_CLASSIFICATION_NAME" Then
                    If ChkGroupClass.Checked = False Then
                        ViewState("SortName") = "LOCATION_CODE" '"CHAIN_CODE"
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
           
            objOutputXml = objbzbzBIDT.BIDTProductivityMonthly_Average(objInputXml)
            ' objOutputXml.LoadXml("<PR_SEARCH_1A_PRODUCTIVITY_NEWFORMAT_BIDT_OUTPUT><DETAILS CHAIN_CODE='' LOCATION_CODE='' AGENCYNAME='' SALESEXECUTIVE='' CITY='' OFFICEID='' TOTALJAN2006='' JANUARY2006AIR='' TOTAL='174' />><Errors Status='FALSE'><Error Code='' Description=''/></Errors></PR_SEARCH_1A_PRODUCTIVITY_NEWFORMAT_BIDT_OUTPUT>")



            If ChkNewFormat.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    '@ New Added Code  
                    Dim dtable As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        strAttribut = IIf((strAttribut.IndexOf("ROUP_CLASSIFICATION_NAME")) > 0, "AGENCYCATEGORY", xmlAttrTotal.Name)
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dtable.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dtable.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes
                        Dim strAttribut As String = IIf(xmlAttrTotal.Name.IndexOf("ROUP_CLASSIFICATION_NAME") > 0, "AGENCYCATEGORY", xmlAttrTotal.Name)
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dtable.Rows.Add(dRow)
                    grdNewFormat.DataSource = dtable


                    '@ New Added Code  
                    ' grdNewFormat.DataSource = ds.Tables("DETAILS")

                    If ds.Tables("DETAILS").Columns("ROWNUMBER") IsNot Nothing Then
                        ds.Tables("DETAILS").Columns.Remove(ds.Tables("DETAILS").Columns("ROWNUMBER"))
                    End If

                    If dtable.Columns("ROWNUMBER") IsNot Nothing Then
                        dtable.Columns.Remove(dtable.Columns("ROWNUMBER"))
                    End If



                    dtable.Columns.Add("Action               ")

                    ds.Tables("DETAILS").Columns.Add("Action               ")

                    FooterDataset = New DataSet
                    FooterDataset = ds
                    grdNewFormat.DataBind()
                    Dim intRow, IntColno, Inti As Integer
                    grdNewFormat.FooterRow.Cells(7).Text = "Total"
                    For IntColno = 12 To grdNewFormat.Rows(0).Cells.Count - 2
                        grdNewFormat.HeaderRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                        grdNewFormat.FooterRow.Cells(IntColno).HorizontalAlign = HorizontalAlign.Right

                        grdNewFormat.FooterRow.Cells(IntColno).Text = 0
                    Next
                    For intRow = 0 To grdNewFormat.Rows.Count - 1
                        For IntColno = 12 To grdNewFormat.Rows(0).Cells.Count - 2
                            grdNewFormat.Rows(intRow).Cells(IntColno).HorizontalAlign = HorizontalAlign.Right
                            Dim sum As Double = grdNewFormat.Rows(intRow).Cells(IntColno).Text
                            'If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                            '    grdNewFormat.FooterRow.Cells(IntColno).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(IntColno).ToString
                            'End If
                            'grdNewFormat.FooterRow.Cells(IntColno).Text = CType(grdNewFormat.FooterRow.Cells(IntColno).Text, Decimal) + sum
                            ' grdNewFormat.FooterRow.Cells(IntColno).Text = String.Format("{0:d}", grdNewFormat.FooterRow.Cells(IntColno).Text.ToString)
                        Next

                    Next

                    Inti = 0
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        For Inti = 0 To FooterDataset.Tables("PAGE_TOTAL").Columns.Count - 1
                            grdNewFormat.FooterRow.Cells(Inti + 12).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)(Inti).ToString
                        Next
                    End If

                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()


                    '@ Code For Exporting the Data

                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                    objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAILS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    Dim k As Integer
                    With objXmlNodeClone
                        .Attributes(6).Value = "Total"
                        For k = 10 To objXmlNodeClone.Attributes.Count - 1
                            .Attributes(k).Value = grdNewFormat.FooterRow.Cells(k + 1).Text
                        Next
                    End With
                    objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdNewFormat.HeaderRow.Cells.Count - 2
                        If grdNewFormat.HeaderRow.Cells(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdNewFormat.HeaderRow.Cells.Count - 2 - IntInvisible) As String
                    Dim intArray(grdNewFormat.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

                    Dim intclmnVis As Integer = 0
                    For intclmn As Integer = 0 To grdNewFormat.HeaderRow.Cells.Count - 2
                        If grdNewFormat.HeaderRow.Cells(intclmn).Visible = True Then
                            If objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes(intclmn - 1).Name = "LOCATION_CODE" Then
                                strArray(intclmnVis) = "LCODE"
                            ElseIf objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes(intclmn - 1).Name = "CHAIN_CODE" Then
                                strArray(intclmnVis) = "CHAINCODE"

                            ElseIf objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes(intclmn - 1).Name = "GROUP_CLASSIFICATION_NAME" Then
                                strArray(intclmnVis) = "AGENCYCATEGORY"
                            ElseIf objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes(intclmn - 1).Name = "COMP_VERTICAL_NAME" Then
                                strArray(intclmnVis) = "Company Vertical"
                            Else
                                strArray(intclmnVis) = objOutputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes(intclmn - 1).Name
                            End If
                            intArray(intclmnVis) = intclmn - 1
                            intclmnVis = intclmnVis + 1
                        End If
                    Next
                    objExport.ExportDetails(objOutputXmlExport, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "BIDTCrossTab.xls")

                    '@ End of Code For Exporting the Data
                Else
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

            If ChkExTrans.Checked = True And RdShowMon.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessMonthBreak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()


                Else
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()

                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

            If ChkExTrans.Checked = True And RdShowAvg.Checked = True Then
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    FooterDataset = New DataSet
                    FooterDataset = ds
                    GrdExcessAvg.DataSource = ds.Tables("BIDTAVERAGE")
                    GrdExcessAvg.DataBind()


                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()


                Else
                    GrdExcessAvg.DataSource = Nothing
                    GrdExcessAvg.DataBind()
                    grdvShowAvg.DataSource = Nothing
                    grdvShowAvg.DataBind()
                    grdvMonthlyBReak.DataSource = Nothing
                    grdvMonthlyBReak.DataBind()
                    GrdExcessMonthBreak.DataSource = Nothing
                    GrdExcessMonthBreak.DataBind()
                    grdNewFormat.DataSource = Nothing
                    grdNewFormat.DataBind()

                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
            If ChkExTrans.Checked = False And ChkNewFormat.Checked = False Then

                If RdShowMon.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        'Session("DataSource") = ds.Tables("AGNECY")
                        FooterDataset = New DataSet
                        FooterDataset = ds



                        '@ New Added Code  
                        Dim dtable As New DataTable
                        Dim dCol As DataColumn
                        For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes
                            Dim strAttribut As String = xmlAttrTotal.Name
                            dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                            dtable.Columns.Add(dCol)
                        Next

                        Dim dRow As DataRow
                        dRow = dtable.NewRow()
                        For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes
                            Dim strAttribut As String = xmlAttrTotal.Name
                            dRow(strAttribut) = xmlAttrTotal.Value
                        Next

                        dtable.Rows.Add(dRow)
                        grdvMonthlyBReak.DataSource = dtable


                        '@ New Added Code  

                        '  grdvMonthlyBReak.DataSource = ds.Tables("BIDTMONTHLYBREAKUP")
                        grdvMonthlyBReak.DataBind()


                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()




                        '@ Code For Exporting the Data

                        Dim objOutputXmlExport As New XmlDocument
                        Dim objXmlNode, objXmlNodeClone As XmlNode
                        objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                        objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP")
                        objXmlNodeClone = objXmlNode.CloneNode(True)
                        For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                            XmlAttr.Value = ""
                        Next
                        With objXmlNodeClone
                            .Attributes(8).Value = "Total"
                            .Attributes("AIR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString
                            .Attributes("CAR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString
                            .Attributes("HOTEL_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString
                            .Attributes("INSURANCE_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString
                            .Attributes("TOTAL").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString
                            .Attributes("PASSIVE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE").ToString
                            .Attributes("WITHPASSIVE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE").ToString
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
                      

                        For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 2
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

                        objExport.ExportDetails(objOutputXmlExport, "BIDTMONTHLYBREAKUP", intArray, strArray, ExportExcel.ExportFormat.Excel, "BIDTMonthlyBreakup.xls")

                        '@ End of Code For Exporting the Data


                    Else
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()

                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                ElseIf RdShowAvg.Checked = True Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)
                        FooterDataset = New DataSet
                        FooterDataset = ds
                        'Session("DataSource") = ds.Tables("AGNECY")

                        '@ New Added Code  
                        Dim dtable As New DataTable
                        Dim dCol As DataColumn
                        For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("BIDTAVERAGE").Attributes
                            Dim strAttribut As String = xmlAttrTotal.Name
                            dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                            dtable.Columns.Add(dCol)
                        Next

                        Dim dRow As DataRow
                        dRow = dtable.NewRow()
                        For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("BIDTAVERAGE").Attributes
                            Dim strAttribut As String = xmlAttrTotal.Name
                            dRow(strAttribut) = xmlAttrTotal.Value
                        Next

                        dtable.Rows.Add(dRow)
                        grdvShowAvg.DataSource = dtable


                        '@ New Added Code  

                        'grdvShowAvg.DataSource = ds.Tables("BIDTAVERAGE")
                        grdvShowAvg.DataBind()
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()



                        '@ Code For Exporting the Data

                        Dim objOutputXmlExport As New XmlDocument
                        Dim objXmlNode, objXmlNodeClone As XmlNode
                        objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                        objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("BIDTAVERAGE")
                        objXmlNodeClone = objXmlNode.CloneNode(True)
                        For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                            XmlAttr.Value = ""
                        Next
                        With objXmlNodeClone
                            .Attributes(8).Value = "Total"
                            .Attributes("AIR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AIR_NETBOOKINGS").ToString
                            .Attributes("CAR_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CAR_NETBOOKINGS").ToString
                            .Attributes("HOTEL_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HOTEL_NETBOOKINGS").ToString
                            .Attributes("INSURANCE_NETBOOKINGS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("INSURANCE_NETBOOKINGS").ToString
                            .Attributes("AVERAGE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("AVERAGE").ToString
                            .Attributes("PASSIVE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE").ToString
                            .Attributes("WITHPASSIVE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE").ToString
                        End With

                        objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                        Dim objExport As New ExportExcel
                        Dim IntInvisible As Integer = 0
                        For intclmn As Integer = 0 To grdvShowAvg.HeaderRow.Cells.Count - 2
                            If grdvShowAvg.HeaderRow.Cells(intclmn).Visible = False Then
                                IntInvisible = IntInvisible + 1
                            End If
                        Next
                        Dim strArray(grdvShowAvg.HeaderRow.Cells.Count - 2 - IntInvisible) As String
                        Dim intArray(grdvShowAvg.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer

                        Dim intclmnVis As Integer = 0


                        For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                            If grdvShowAvg.HeaderRow.Cells(intclmn).Visible = True Then
                                strArray(intclmnVis) = grdvShowAvg.Columns(intclmn).HeaderText 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                                '@ Finding Position From xml Related with Header Text
                                For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                                    If objXmlNodeClone.Attributes(kk).Name.Trim = grdvShowAvg.Columns(intclmn).SortExpression.Trim Then
                                        intArray(intclmnVis) = kk
                                        intclmnVis = intclmnVis + 1
                                        Exit For
                                    End If
                                Next kk
                            End If
                        Next intclmn

                        objExport.ExportDetails(objOutputXmlExport, "BIDTAVERAGE", intArray, strArray, ExportExcel.ExportFormat.Excel, "BIDTAvg.xls")




                    Else
                        grdvMonthlyBReak.DataSource = Nothing
                        grdvMonthlyBReak.DataBind()
                        grdvShowAvg.DataSource = Nothing
                        grdvShowAvg.DataBind()
                        GrdExcessMonthBreak.DataSource = Nothing
                        GrdExcessMonthBreak.DataBind()
                        GrdExcessAvg.DataSource = Nothing
                        GrdExcessAvg.DataBind()
                        grdNewFormat.DataSource = Nothing
                        grdNewFormat.DataBind()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "TRUE" Then
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            lblFound.Visible = False
            txtRecordCount.Visible = False
            pnlPaging.Visible = False
        Finally
            'lblFound.Visible = True
            txtRecordCount.Visible = True
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
   
  
    
End Class
