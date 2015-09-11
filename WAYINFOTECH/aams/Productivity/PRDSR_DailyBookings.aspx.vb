Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System.Collections.Generic
Partial Class Productivity_PRDSR_DailyBookings
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim FooterDs As DataSet
    Dim objED As New EncyrptDeCyrpt
    Dim objDictionary As New HybridDictionary
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub AllNonDescColumnDefault()
        Try
            objDictionary.Add("LCODE", "LCODE")
            objDictionary.Add("AgencyName", "AgencyName")
            objDictionary.Add("Chain_Code", "Chain_Code")
            objDictionary.Add("Group_Classification_Name", "Group_Classification_Name")
            objDictionary.Add("Employee_Name", "Employee_Name")
            objDictionary.Add("ADDRESS", "ADDRESS")
            objDictionary.Add("ONLINE_STATUS", "ONLINE_STATUS")
            objDictionary.Add("CITY", "CITY")
            objDictionary.Add("COUNTRY", "COUNTRY")
            objDictionary.Add("OFFICEID", "OFFICEID")
            objDictionary.Add("Aoffice", "Aoffice")
            objDictionary.Add("Airline_Name", "Airline_Name")
           Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
       'Code Added on 23rd January
        SetGridWidth()
        'Code Added on 23rd January


        'drpAirLineName(drpAgencyStatus) drpAgencyType drpCitys drpCountrys drpOneAOffice ddlOnlineStatus  drpRegion drpResponsibleStaff 
        ' Code for Delete Action in Dropdowns
        drpAirLineName.Attributes.Add("onkeyup", "return gotop('drpAirLineName')")
        drpAgencyStatus.Attributes.Add("onkeyup", "return gotop('drpAgencyStatus')")
        drpAgencyType.Attributes.Add("onkeyup", "return gotop('drpAgencyType')")
        drpCitys.Attributes.Add("onkeyup", "return gotop('drpCitys')")
        drpCountrys.Attributes.Add("onkeyup", "return gotop('drpCountrys')")
        drpOneAOffice.Attributes.Add("onkeyup", "return gotop('drpOneAOffice')")
        chkOnlineStatus.Attributes.Add("onkeyup", "return gotop('ddlOnlineStatus')")
        drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")
        drpResponsibleStaff.Attributes.Add("onkeyup", "return gotop('drpResponsibleStaff')")
        drpPerformence.Attributes.Add("onkeyup", "return gotopPerDAILYB()")
        drpProductivity.Attributes.Add("onkeyup", "return gotopProdDAILYB()")

        drpGroupAgencyType.Attributes.Add("onkeyup", "return gotop('drpGroupAgencyType')")

        ' Code for Delete Action in Dropdowns
        Page.MaintainScrollPositionOnPostBack = True
        'Code for Paging $ Sorting
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        'Code for Paging $ Sorting

        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        ' This code is used for Expiration of Page From Cache
        objEams.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
            Exit Sub
        End If

        'hdAgency.Value = "-111"

        ' btnReset.Attributes.Add("onclick", "return ResetDailBookingDAILYB();")
        btnDisplay.Attributes.Add("onclick", "return ValidateSearchDAILYB();")
        btnExport.Attributes.Add("onclick", "return ValidateSearchDAILYB();")



        'txtAgencyName.Attributes.Add("onkeydown", "return LcodeResetDAILYB();")


        'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
        '    ' btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvDailyBookingsAll');")
        '    'txtRecordCount.Text = grdvDailyBookingsAll.Rows.Count.ToString()
        'End If

        'If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
        '    ' btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvNoChk');")
        'End If

        'If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
        '    ' btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvAirWithAirBr');")
        'End If

        'If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
        '    ' btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvCar');")
        'End If

        'If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
        '    '  btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvHotel');")
        'End If

        'If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
        '    '  btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvCarHotel');")
        'End If

        'If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
        '    '   btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvAirCar');")
        'End If


        'If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
        '    ' btnPrint.Attributes.Add("onclick", "return CallPrintDAILYB('grdvAirHotel');")
        'End If



        chkCar.Attributes.Add("onclick", "return ValidateCarHotelDAILYB();")
        chkHotel.Attributes.Add("onclick", "return ValidateCarHotelDAILYB();")
        chkAirBreakUp.Attributes.Add("onclick", "return validateAirBreakUpDAILYB();")

        txtProductivityFrm.Attributes.Add("onfocusout", "return validateProductivityToDAILYB();")
        txtProductivityFrm.Attributes.Add("onblur", "return validateProductivityToDAILYB();")

        txtProductivityTo.Attributes.Add("onfocusout", "return validateProductivityToDAILYB();")
        txtProductivityTo.Attributes.Add("onblur", "return validateProductivityToDAILYB();")

        txtPerformenceFrm.Attributes.Add("onfocusout", "return validateProductivityToDAILYB();")
        txtPerformenceFrm.Attributes.Add("onblur", "return validateProductivityToDAILYB();")


        txtPerformenceTo.Attributes.Add("onfocusout", "return validateProductivityToDAILYB();")
        txtPerformenceTo.Attributes.Add("onblur", "return validateProductivityToDAILYB();")


        drpPerformence.Attributes.Add("onchange", "return validatePerformencFromToDAILYB();")
        drpProductivity.Attributes.Add("onchange", "return validateProductivityFromToDAILYB();")

        ''Added by Tapan Nath for GroupProducitivity Enable--
        txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();") ''Added by Tapan Nath
        ''txtAgencyName.Attributes.Add("onblur", "return GroupProductivityChkDAILYB();")
        txtLcode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
        txtChainCode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
        txtAgencyName.Attributes.Add("onfocusout", "return EnableDisableLcodeChainCode();")
        ''Added by Tapan Nath for GroupProducitivity Enable-- Ends here

        'Added
        grdvDailyBookingsAll.Columns(3).HeaderText = "Agency Category"
        grdvAirWithAirBr.Columns(3).HeaderText = "Agency Category"
        grdvCar.Columns(3).HeaderText = "Agency Category"
        grdvHotel.Columns(3).HeaderText = "Agency Category"
        grdvNoChk.Columns(3).HeaderText = "Agency Category"
        grdvCarHotel.Columns(3).HeaderText = "Agency Category"
        grdvAirCar.Columns(3).HeaderText = "Agency Category"
        grdvAirHotel.Columns(3).HeaderText = "Agency Category"
        'Added

        If Session("Security") Is Nothing Then
            lblError.Text = "Re Login"
            Exit Sub
        End If

        Dim xDoc As New XmlDocument
        xDoc.LoadXml(Session("Security"))

        'If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim().ToUpper() = "TRUE" Then
        '    drpOneAOffice.SelectedItem.Text = xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.Trim()
        '    drpOneAOffice.Enabled = False
        'End If


        If txtAgencyName.Text.Trim().Length <= 0 Then
            ' hdAgency.Value = "-111"
            hdAgencyNameId.Value = ""
        End If

        If Not Page.IsPostBack Then
            BindAllControls()
            drpMonth.SelectedIndex = DateTime.Now.Month - 1
            drpYear.SelectedValue = DateTime.Now.Year
           
            'Page.RegisterClientScriptBlock("chkbox", "<script type='text/javascript' language='javascript'> document.getElementById('chkGroupProductivity').disabled =true;</script>")
            'chkGroupProductivity.Enabled  = False
       
        End If



        Dim str As String()
        str = Session("LoginSession").ToString().Split("|")
        'Following Statement is written for Limited to own Agency

        xDoc.LoadXml(Session("Security"))

        '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

        If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
            drpOneAOffice.SelectedValue = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim()  'Login time u find Need for discussion
            drpOneAOffice.Enabled = False
        End If


        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnDisplay.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Air']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Air']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    chkAir.Checked = False
                    chkAir.Enabled = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If


        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_AirBreakup']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_AirBreakup']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    chkAirBreakUp.Enabled = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If



        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Car']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Car']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    chkCar.Enabled = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Hotel']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Dailybookings_Hotel']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    chkHotel.Enabled = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    chkOriginalBk.Checked = False
                    chkOriginalBk.Visible = False
                End If
            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If

        'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
        '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Productivity']").Count <> 0 Then
        '        strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Productivity']").Attributes("Value").Value)
        '        If strBuilder(0) = "0" Then
        '            chkGroupProductivity.Enabled = False
        '        End If
        '    End If
        'Else
        '    strBuilder = objEams.SecurityCheck(31)
        'End If


    End Sub
    Private Sub BindAllControls()
        Try

            objEams.BindDropDown(drpAirLineName, "AIRLINE", False, 3)
            'drpAirLineName.Items.Insert(0, New ListItem("All", ""))

            objEams.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", False, 3)
            'drpAgencyStatus.Items.Insert(0, New ListItem("All", ""))

            objEams.BindDropDown(drpAgencyType, "AGENCYTYPE", False, 3)
            'drpAgencyType.Items.Insert(0, New ListItem("All", ""))


            objEams.BindDropDown(drpCitys, "CITY", False, 3)
            'drpCitys.Items.Insert(0, New ListItem("All", ""))

            objEams.BindDropDown(drpCountrys, "COUNTRY", False, 3)
            'drpCountrys.Items.Insert(0, New ListItem("All", ""))


            objEams.BindDropDown(drpOneAOffice, "AOFFICE", False, 3)
            'drpOneAOffice.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
           
            ' objEams.BindDropDown(ddlOnlineStatus, "ONLINESTATUSCODE", False, 3)
            'ddlOnlineStatus.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpGroupAgencyType, "AGENCYGROUPCLASSTYPE", False, 3)

            objEams.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

            Dim objOutputXml As XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
            objOutputXml = New XmlDocument
            objOutputXml = objTaProducts.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                chkOnlineStatus.DataSource = ds.Tables("Status")
                chkOnlineStatus.DataTextField = "StatusCode"
                chkOnlineStatus.DataValueField = "StatusCode"
                chkOnlineStatus.DataBind()
            End If




            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            ' drpRegion.Items.Insert(0, New ListItem("All", ""))


            objEams.BindDropDown(drpResponsibleStaff, "ResponsbileStaff", False, 3)
            'drpResponsibleStaff.Items.Insert(0, New ListItem("All", ""))


            'For Binding Year for DropDown
            Dim dtYear As New DateTime
            Dim counter As Integer
            For counter = DateTime.Now.Year To 1990 Step -1
                drpYear.Items.Add(counter.ToString())
            Next
            ' drpYear.Items.Insert(0, "---Select One---")
            'End of Binding Dropdown
            'drpMonth.Items.Add(Month(DateTime.Now).ToString("MMM"))

            'dtYear = Convert.ToDateTime("01/01/2008")

            ' drpMonth.Items.Insert(0, "---Select One---")
            For counter = 0 To 11
                drpMonth.Items.Add(MonthName(counter + 1))
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub BindData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
        Try
            'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
            objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><ResponsibleStaff></ResponsibleStaff><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><AgencyName></AgencyName><ShowGroup></ShowGroup><OriginalBookings></OriginalBookings><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><ONLINE_STATUS></ONLINE_STATUS><Producitivity_Type></Producitivity_Type><Producitivity_From></Producitivity_From><Producitivity_To></Producitivity_To><Performance_Type></Performance_Type><Performance_From></Performance_From><Performance_To></Performance_To><Region></Region><Air></Air><Car></Car><Hotel></Hotel><AirBreakup></AirBreakup><Month></Month><Year></Year><TYPEID></TYPEID> <Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> <CHAIN_CODE/> <COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_DAILYBOOKINGS_INPUT>")
            'End If
            With objInputXml.DocumentElement
                If DlstCompVertical.SelectedValue <> "" Then
                    .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If
                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
                End If
                If drpCountrys.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    .SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
                End If

                If txtChainCode.Text.Trim <> "" Then
                    .SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chkGroupProductivity.Checked = True Then
                    .SelectSingleNode("ShowGroup").InnerText = "True"
                Else
                    .SelectSingleNode("ShowGroup").InnerText = "False"
                End If
                If chkOriginalBk.Checked = True Then
                    .SelectSingleNode("OriginalBookings").InnerText = "True"
                Else
                    .SelectSingleNode("OriginalBookings").InnerText = "False"
                End If
                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("ResponsibleStaff").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If
                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If
                If drpOneAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
                End If

                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If

                Dim count As Integer = 0

                .SelectSingleNode("ONLINE_STATUS").InnerText = ""

                For Each ch As ListItem In chkOnlineStatus.Items
                    If ch.Selected = True Then
                        count += 1
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ch.Value.Trim()
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ","
                    End If
                Next
                If .SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length > 0 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                End If

                If count = 1 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(0, 1)
                End If




                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Producitivity_Type").InnerText = drpProductivity.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Producitivity_From").InnerText = Request("txtProductivityFrm")
                .SelectSingleNode("Producitivity_To").InnerText = Request("txtProductivityTo")
                If drpPerformence.SelectedIndex <> 0 Then
                    .SelectSingleNode("Performance_Type").InnerText = drpPerformence.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Performance_From").InnerText = Request("txtPerformenceFrm") 'txtPerformenceFrm.Text.Trim()
                .SelectSingleNode("Performance_To").InnerText = Request("txtPerformenceTo") 'txtPerformenceTo.Text.Trim()

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                If chkAir.Checked = True Then
                    .SelectSingleNode("Air").InnerText = "True"
                Else
                    .SelectSingleNode("Air").InnerText = "False"
                End If
                If chkCar.Checked = True Then
                    .SelectSingleNode("Car").InnerText = "True"
                Else
                    .SelectSingleNode("Car").InnerText = "False"
                End If
                If chkHotel.Checked = True Then
                    .SelectSingleNode("Hotel").InnerText = "True"
                Else
                    .SelectSingleNode("Hotel").InnerText = "False"
                End If

                '.SelectSingleNode("Hotel").InnerXml=

                If chkAirBreakUp.Checked = True Then
                    .SelectSingleNode("AirBreakup").InnerText = "True"

                    If drpAirLineName.SelectedIndex <> 0 Then
                        .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
                    End If

                Else
                    .SelectSingleNode("AirBreakup").InnerText = "False"
                End If

                '.SelectSingleNode("AirBreakup").InnerXml=
                '  If drpMonth.SelectedIndex <> 0 Then
                .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
                ' End If
                'If drpYear.SelectedIndex <> 0 Then
                .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()
                '  End If




                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESP_1A").InnerText = str(0)

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))



                .SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion



                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If


                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                'Start CODE for sorting and paging
                If hdButtonClick.Value = "1" Then
                    ViewState("PrevSearching") = Nothing
                    ViewState("SortName") = Nothing
                    ViewState("Desc") = Nothing
                    ddlPageNumber.Items.Clear()
                    hdButtonClick.Value = "-1"
                End If


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
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")


                    If ViewState("SortName").ToString().Trim().ToUpper() = "CHAIN_CODE" And chkShowChaniCode.Checked = False Then
                        ViewState("SortName") = "AgencyName"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
                        ViewState("Desc") = Nothing
                    End If

                    If ViewState("SortName").ToString().Trim().ToUpper() = "ADDRESS" And chkShowAddress.Checked = False Then
                        ViewState("SortName") = "AgencyName"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
                        ViewState("Desc") = Nothing
                    End If

                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else

                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If

                'End Code for paging and sorting
            End With

            'Here Back end Method Call
            'If objbzDailyBooking.Search(objInputXml) IsNot Nothing Then

            If ViewState("PrevSearching") IsNot Nothing Then
                Dim objXml1 As New XmlDocument
                objXml1.LoadXml(ViewState("PrevSearching"))
                Dim objNodes As XmlNodeList = objXml1.DocumentElement.ChildNodes
                If objXml1.OuterXml <> objInputXml.OuterXml Then
                    If objXml1.DocumentElement.SelectSingleNode("Air").InnerText <> objInputXml.DocumentElement.SelectSingleNode("Air").InnerText Or objXml1.DocumentElement.SelectSingleNode("Car").InnerText <> objInputXml.DocumentElement.SelectSingleNode("Car").InnerText Or objXml1.DocumentElement.SelectSingleNode("Hotel").InnerText <> objInputXml.DocumentElement.SelectSingleNode("Hotel").InnerText Or objXml1.DocumentElement.SelectSingleNode("AirBreakup").InnerText <> objInputXml.DocumentElement.SelectSingleNode("AirBreakup").InnerText Or objXml1.DocumentElement.SelectSingleNode("OriginalBookings").InnerText <> objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText Then

                        ViewState("SortName") = "AgencyName"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"

                        ViewState("Desc") = "FALSE"
                        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                    End If
                End If
            End If
           

            If chkAirBreakUp.Checked Then
                'Call Save method 
                'lblError.Text = "Your requested data will be sent at your E-mail ID"
                'objOutputXml = objbzDailyBooking.Search(objInputXml)
                ' objOutputXml.Save("C:\Admin\AirBreakup.xml")
                'PAGE PAGE_COUNT='' TOTAL_ROWS='' 
                If drpCitys.SelectedIndex = 0 And drpAirLineName.SelectedIndex = 0 And txtAgencyName.Text.Trim().Length = 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
                    ' objInputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value = "0"
                    objbzDailyBooking.UpdateRequest("I", Nothing, objInputXml)
                    lblError.Text = "Data will be send soon"
                    pnlPaging.Visible = False
                    Exit Sub

                    objOutputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_OUTPUT><DAILYBOOKINGS LCODE='' Chain_Code='' Employee_Name='' AgencyName='' ADDRESS='' ONLINE_STATUS='' CITY='' COUNTRY='' OFFICEID='' Aoffice='' Airline_Name='' TARGET='' TARGETPERDAY='' AverageBookings='' Netbookings='' Passive='' WithPassive='' Performance='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' /> <PAGE_TOTAL TARGET='' TARGETPERDAY='' AverageBookings='' Netbookings='' Passive='' WithPassive='' Performance='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' /> <PAGE PAGE_COUNT='' TOTAL_ROWS='' /> <Errors Status='TRUE'><Error Code='101' Description='Data will be send soon' /> </Errors></PR_SEARCH_DAILYBOOKINGS_OUTPUT>")
                Else
                    objOutputXml = objbzDailyBooking.Search(objInputXml)
                End If

            Else
                objOutputXml = objbzDailyBooking.Search(objInputXml)
            End If


            ' End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                'Code for Pagin
                ViewState("PrevSearching") = objInputXml.OuterXml
                ds.ReadXml(objXmlReader)
                'This dataSet contains Footer Value 
                FooterDs = New DataSet()
                FooterDs = ds
                lblError.Text = ""
                'Grid binding as per our conditions
                If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvDailyBookingsAll.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvDailyBookingsAll.DataBind()
                    'Code for paging
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvDailyBookingsAll)
                    If ViewState("Desc") = "FALSE" Then
                        grdvDailyBookingsAll.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvDailyBookingsAll.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                    'End of Code for paging
                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
                    '  txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvNoChk.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvNoChk.DataBind()
                    'Code for paging
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvNoChk)
                    If ViewState("Desc") = "FALSE" Then
                        grdvNoChk.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvNoChk.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                End If
                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    '  txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirWithAirBr.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirWithAirBr.DataBind()
                    'Code for paging
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvAirWithAirBr)
                    If ViewState("Desc") = "FALSE" Then
                        grdvAirWithAirBr.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvAirWithAirBr.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCar.DataBind()

                    'Code for checking
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvCar)
                    If ViewState("Desc") = "FALSE" Then
                        grdvCar.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvCar.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If


                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvHotel.DataBind()


                    'Code for checking
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvHotel)
                    If ViewState("Desc") = "FALSE" Then
                        grdvHotel.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvHotel.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If


                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    '   txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvCarHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCarHotel.DataBind()


                    'Code for checking
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvCarHotel)
                    If ViewState("Desc") = "FALSE" Then
                        grdvCarHotel.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvCarHotel.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If


                End If

                If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirCar.DataBind()

                    'Code for paging
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvAirCar)
                    If ViewState("Desc") = "FALSE" Then
                        grdvAirCar.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvAirCar.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If



                End If


                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirHotel.DataBind()

                    'Code for paging
                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvAirHotel)
                    If ViewState("Desc") = "FALSE" Then
                        grdvAirHotel.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvAirHotel.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If

                End If
            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDs = Nothing
                'txtRecordCount.Text = "0"



                'Code for Paging
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                'Code for Paging


                'Grid binding as per our conditions
                If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvDailyBookingsAll.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvDailyBookingsAll.DataBind()

                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvNoChk.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvNoChk.DataBind()
                End If

                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirWithAirBr.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirWithAirBr.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCar.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvHotel.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvCarHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCarHotel.DataBind()
                End If

                If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirCar.DataBind()
                End If


                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirHotel.DataBind()
                End If
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub grdvDailyBookingsAll_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvDailyBookingsAll.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString()
                    'e.Row.Cells(11).Text = hdTARGET.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() 'hdTARGETPERDAY.Value
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdAverageBookings.Value.Trim()

                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() ' hdNetbookings.Value

                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString() ' hdNetbookings.Value
                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString() ' hdNetbookings.Value

                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString() 'hdCar_Netbookings.Value
                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() ' hdHotel_Netbookings.Value


                    Dim counter As Integer = 21 '20 '19
                    Dim datecounter As Integer
                    'Dim strDt As String = "D"
                    'Dim strCar As String = "Car_D"
                    'Dim strHotel As String = "Hotel_D"

                    ' setting footer values

                    For datecounter = 1 To 31
                        e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D" + datecounter.ToString()).ToString()
                        counter += 1
                        e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D" + datecounter.ToString()).ToString()
                        counter += 1
                        e.Row.Cells(counter).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D" + datecounter.ToString()).ToString()
                        counter += 1
                    Next

                    e.Row.Cells(114).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
                End If
            End If

            'If e.Row.RowIndex < 0 Then
            '    Exit Sub
            'End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                    e.Row.Cells.Clear()
                    Exit Sub
                End If
                'showDetails(lcode)
                'lnkDetails
                Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
                lnkBtn = e.Row.FindControl("lnkDetails")

                Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

                'lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" + strl + "');")


                lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")

                Dim intMont As Integer = drpMonth.SelectedIndex + 1
                If intMont = 2 Then
                    If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                        ' grdvDailyBookingsAll.Columns(106).Visible = False
                        grdvDailyBookingsAll.Columns(108).Visible = False
                        grdvDailyBookingsAll.Columns(109).Visible = False
                        grdvDailyBookingsAll.Columns(110).Visible = False
                        grdvDailyBookingsAll.Columns(111).Visible = False
                        grdvDailyBookingsAll.Columns(112).Visible = False
                        grdvDailyBookingsAll.Columns(113).Visible = False
                    Else
                        'grdvDailyBookingsAll.Columns(101).Visible = False
                        'grdvDailyBookingsAll.Columns(102).Visible = False
                        'grdvDailyBookingsAll.Columns(103).Visible = False
                        grdvDailyBookingsAll.Columns(105).Visible = False
                        grdvDailyBookingsAll.Columns(106).Visible = False
                        grdvDailyBookingsAll.Columns(107).Visible = False
                        grdvDailyBookingsAll.Columns(108).Visible = False
                        grdvDailyBookingsAll.Columns(109).Visible = False
                        grdvDailyBookingsAll.Columns(110).Visible = False
                        grdvDailyBookingsAll.Columns(111).Visible = False
                        grdvDailyBookingsAll.Columns(112).Visible = False
                        grdvDailyBookingsAll.Columns(113).Visible = False
                    End If
             
                End If
                If intMont <> 2 Then
                    'grdvDailyBookingsAll.Columns(103).Visible = True
                    grdvDailyBookingsAll.Columns(105).Visible = True
                    grdvDailyBookingsAll.Columns(106).Visible = True
                    grdvDailyBookingsAll.Columns(107).Visible = True
                    grdvDailyBookingsAll.Columns(108).Visible = True
                    grdvDailyBookingsAll.Columns(109).Visible = True
                    grdvDailyBookingsAll.Columns(110).Visible = True
                    grdvDailyBookingsAll.Columns(111).Visible = True
                    grdvDailyBookingsAll.Columns(112).Visible = True
                    grdvDailyBookingsAll.Columns(113).Visible = True
                End If


                If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                    'grdvDailyBookingsAll.Columns(109).Visible = False
                    grdvDailyBookingsAll.Columns(111).Visible = False
                    grdvDailyBookingsAll.Columns(112).Visible = False
                    grdvDailyBookingsAll.Columns(113).Visible = False
                ElseIf intMont = 1 Or intMont = 3 Or intMont = 5 Or intMont = 7 Or intMont = 8 Or intMont = 10 Or intMont = 12 Then
                    'grdvDailyBookingsAll.Columns(109).Visible = True
                    grdvDailyBookingsAll.Columns(111).Visible = True
                    grdvDailyBookingsAll.Columns(112).Visible = True
                    grdvDailyBookingsAll.Columns(113).Visible = True
                End If
            End If
            grdvDailyBookingsAll.Columns(12).Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvDailyBookingsAll_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvDailyBookingsAll.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            Dim dtFormat As String
            dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
            Dim cellcounter As Integer = 21 '20 '19
            Dim datecounter As Integer
            If grvRow.RowType = DataControlRowType.Header Then

                If grdvDailyBookingsAll.AllowSorting = True Then
                    If chkOriginalBk.Checked = True Then
                        CType(grvRow.Cells(18).Controls(0), LinkButton).Text = "Total Air"
                    End If
                Else
                    grvRow.Cells(18).Text = "Total Air"
                End If
                ' creating dynamic header.
                If grdvDailyBookingsAll.AllowSorting = True Then
                    For datecounter = 1 To 31
                        CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
                        cellcounter += 1
                        CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                        cellcounter += 1
                        CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                        cellcounter += 1
                    Next
                Else
                    For datecounter = 1 To 31
                        grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
                        cellcounter += 1
                        grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                        cellcounter += 1
                        grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                        cellcounter += 1
                    Next
                End If

            End If
            grdvDailyBookingsAll.Columns(12).Visible = False

            If chkShowAddress.Checked = True Then
                grdvDailyBookingsAll.Columns(5).Visible = True
            Else
                grdvDailyBookingsAll.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvDailyBookingsAll.Columns(2).Visible = True
            Else
                grdvDailyBookingsAll.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked Then
                grdvDailyBookingsAll.Columns(3).Visible = True
            Else
                grdvDailyBookingsAll.Columns(3).Visible = False
            End If
            '15,16
            If chkOriginalBk.Checked = False Then
                grdvDailyBookingsAll.Columns(17).Visible = False
                grdvDailyBookingsAll.Columns(18).Visible = False
            Else
                grdvDailyBookingsAll.Columns(17).Visible = True
                grdvDailyBookingsAll.Columns(18).Visible = True
            End If


            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            'code for FEB month
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    'grdvDailyBookingsAll.Columns(106).Visible = False
                    grdvDailyBookingsAll.Columns(108).Visible = False
                    grdvDailyBookingsAll.Columns(109).Visible = False
                    grdvDailyBookingsAll.Columns(110).Visible = False
                    grdvDailyBookingsAll.Columns(111).Visible = False
                    grdvDailyBookingsAll.Columns(112).Visible = False
                    grdvDailyBookingsAll.Columns(113).Visible = False
                Else
                    'grdvDailyBookingsAll.Columns(101).Visible = False
                    'grdvDailyBookingsAll.Columns(102).Visible = False
                    'grdvDailyBookingsAll.Columns(103).Visible = False
                    grdvDailyBookingsAll.Columns(105).Visible = False
                    grdvDailyBookingsAll.Columns(106).Visible = False
                    grdvDailyBookingsAll.Columns(107).Visible = False
                    grdvDailyBookingsAll.Columns(108).Visible = False
                    grdvDailyBookingsAll.Columns(109).Visible = False
                    grdvDailyBookingsAll.Columns(110).Visible = False
                    grdvDailyBookingsAll.Columns(111).Visible = False
                    grdvDailyBookingsAll.Columns(112).Visible = False
                    grdvDailyBookingsAll.Columns(113).Visible = False
                End If

            End If
            'end
            If intMont <> 2 Then
                'grdvDailyBookingsAll.Columns(103).Visible = True
                grdvDailyBookingsAll.Columns(105).Visible = True
                grdvDailyBookingsAll.Columns(106).Visible = True
                grdvDailyBookingsAll.Columns(107).Visible = True
                grdvDailyBookingsAll.Columns(108).Visible = True
                grdvDailyBookingsAll.Columns(109).Visible = True
                grdvDailyBookingsAll.Columns(110).Visible = True
                grdvDailyBookingsAll.Columns(111).Visible = True
                grdvDailyBookingsAll.Columns(112).Visible = True
                grdvDailyBookingsAll.Columns(113).Visible = True
            End If

            ' code for 30 days in month
            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                'grdvDailyBookingsAll.Columns(109).Visible = False
                grdvDailyBookingsAll.Columns(111).Visible = False
                grdvDailyBookingsAll.Columns(112).Visible = False
                grdvDailyBookingsAll.Columns(113).Visible = False
                ' code for 31 days in month
            ElseIf intMont = 1 Or intMont = 3 Or intMont = 5 Or intMont = 7 Or intMont = 8 Or intMont = 10 Or intMont = 12 Then
                'grdvDailyBookingsAll.Columns(109).Visible = True
                grdvDailyBookingsAll.Columns(111).Visible = True
                grdvDailyBookingsAll.Columns(112).Visible = True
                grdvDailyBookingsAll.Columns(113).Visible = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirWithAirBr_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirWithAirBr.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            Dim cellcounter, datecounter As Int16
            cellcounter = 19 '18 ' 17

            'If chkOriginalBk.Checked = True Then
            '    CType(grvRow.Cells(16).Controls(0), LinkButton).Text = "Air net with Psv"
            'End If


            If grdvAirWithAirBr.AllowSorting = True Then
                CType(grvRow.Cells(18).Controls(0), LinkButton).Text = "Total Air"

                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                Next
            Else
                grvRow.Cells(18).Text = "Total Air"
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                Next
            End If
            

            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    grdvAirWithAirBr.Columns(48).Visible = False
                    grdvAirWithAirBr.Columns(49).Visible = False

                Else
                    ' grdvAirWithAirBr.Columns(45).Visible = False
                    grdvAirWithAirBr.Columns(47).Visible = False
                    grdvAirWithAirBr.Columns(48).Visible = False
                    grdvAirWithAirBr.Columns(49).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvAirWithAirBr.Columns(49).Visible = False
            End If


            If chkAir.Checked And chkAirBreakUp.Checked = False Then
                grdvAirWithAirBr.Columns(12).Visible = False
            End If

            If chkAirBreakUp.Checked = True Then
                grdvAirWithAirBr.Columns(13).Visible = False
                grdvAirWithAirBr.Columns(14).Visible = False
            End If




            If chkShowAddress.Checked = True Then
                grdvAirWithAirBr.Columns(5).Visible = True
            Else
                grdvAirWithAirBr.Columns(5).Visible = False
            End If

            If chkShowChaniCode.Checked = True Then
                grdvAirWithAirBr.Columns(2).Visible = True
            Else
                grdvAirWithAirBr.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvAirWithAirBr.Columns(3).Visible = True
            Else
                grdvAirWithAirBr.Columns(3).Visible = False
            End If
            '15,16
            If chkOriginalBk.Checked = False Then
                grdvAirWithAirBr.Columns(17).Visible = False
                grdvAirWithAirBr.Columns(18).Visible = False
            Else
                ' grdvAirWithAirBr.Columns(15).Visible = True
                grdvAirWithAirBr.Columns(17).Visible = True
                grdvAirWithAirBr.Columns(18).Visible = True
            End If


            If chkAir.Checked And chkAirBreakUp.Checked = False Then
                grdvAirWithAirBr.Columns(12).Visible = False
            End If
            'If chkAirBreakUp.Checked = True Then
            '    grdvAirWithAirBr.Columns(11).Visible = False
            '    grdvAirWithAirBr.Columns(12).Visible = False
            'End If

            '15,16
            'If chkOriginalBk.Checked = False Then
            '    grdvAirWithAirBr.Columns(15).Visible = False
            '    grdvAirWithAirBr.Columns(16).Visible = False
            'Else
            '    grdvAirWithAirBr.Columns(15).Visible = True
            '    grdvAirWithAirBr.Columns(16).Visible = True
            'End If
        End If
    End Sub

    Protected Sub grdvAirWithAirBr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirWithAirBr.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() 'hdTARGET.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() 'hdTARGETPERDAY.Value
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdAverageBookings.Value
                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() 'hdNetbookings.Value

                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString()
                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString()

                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D1").ToString() 'hdD1.Value
                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D2").ToString() 'hdD2.Value

                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D3").ToString() 'hdD3.Value
                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D4").ToString() 'hdD4.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D5").ToString() 'hdD5.Value

                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D6").ToString() 'hdD6.Value

                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D7").ToString() 'hdD7.Value

                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D8").ToString() ' hdD8.Value
                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D9").ToString() 'hdD9.Value
                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D10").ToString() 'hdD10.Value

                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D11").ToString() ' hdD11.Value

                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D12").ToString() ' hdD12.Value

                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D13").ToString() 'hdD13.Value

                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D14").ToString() ' hdD14.Value

                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D15").ToString() 'hdD15.Value

                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D16").ToString() ' hdD16.Value

                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D17").ToString() 'hdD17.Value

                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D18").ToString() ' hdD18.Value

                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D19").ToString() ' hdD19.Value

                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D20").ToString() ' hdD20.Value

                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D21").ToString() ' hdD21.Value

                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D22").ToString() ' hdD22.Value

                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D23").ToString() 'hdD23.Value

                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D24").ToString() ' hdD24.Value

                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D25").ToString() ' hdD25.Value

                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D26").ToString() ' hdD26.Value

                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D27").ToString() ' hdD27.Value

                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D28").ToString() ' hdD28.Value
                    e.Row.Cells(47).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D29").ToString() ' hdD29.Value
                    e.Row.Cells(48).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D30").ToString() ' hdD30.Value
                    e.Row.Cells(49).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D31").ToString() ' hdD31.Value
                    e.Row.Cells(50).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
                End If
            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If




            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            'lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" + strl + "');")

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")


            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    'grdvAirWithAirBr.Columns(46).Visible = False
                    grdvAirWithAirBr.Columns(48).Visible = False
                    grdvAirWithAirBr.Columns(49).Visible = False

                Else
                    ' grdvAirWithAirBr.Columns(45).Visible = False
                    grdvAirWithAirBr.Columns(47).Visible = False
                    grdvAirWithAirBr.Columns(48).Visible = False
                    grdvAirWithAirBr.Columns(49).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvAirWithAirBr.Columns(49).Visible = False
            End If


            If chkAir.Checked And chkAirBreakUp.Checked = False Then
                grdvAirWithAirBr.Columns(12).Visible = False
            End If

            If chkAirBreakUp.Checked = True Then
                'grdvAirWithAirBr.Columns(11).Visible = False
                grdvAirWithAirBr.Columns(13).Visible = False
                grdvAirWithAirBr.Columns(14).Visible = False
            End If




            If chkShowAddress.Checked = True Then
                grdvAirWithAirBr.Columns(5).Visible = True
            Else
                grdvAirWithAirBr.Columns(5).Visible = False
            End If

            If chkShowChaniCode.Checked = True Then
                grdvAirWithAirBr.Columns(2).Visible = True
            Else
                grdvAirWithAirBr.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvAirWithAirBr.Columns(3).Visible = True
            Else
                grdvAirWithAirBr.Columns(3).Visible = False
            End If

            '15,16
            If chkOriginalBk.Checked = False Then
                'grdvAirWithAirBr.Columns(15).Visible = False
                grdvAirWithAirBr.Columns(17).Visible = False
                grdvAirWithAirBr.Columns(18).Visible = False
            Else
                ' grdvAirWithAirBr.Columns(15).Visible = True
                grdvAirWithAirBr.Columns(17).Visible = True
                grdvAirWithAirBr.Columns(18).Visible = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

   

    Protected Sub grdvCar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCar.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then
                    e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() ' hdTARGET.Value
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() 'hdTARGETPERDAY.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString() 'hdCar_Netbookings.Value
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D1").ToString() ' hdCar_D1.Value
                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D2").ToString() 'hdCar_D2.Value

                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D3").ToString() 'hdCar_D3.Value
                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D4").ToString() ' hdCar_D4.Value
                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D5").ToString() 'hdCar_D5.Value
                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D6").ToString() ' hdCar_D6.Value

                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D7").ToString() ' hdD7.Value

                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D8").ToString() 'hdCar_D8.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D9").ToString() ' hdCar_D9.Value
                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D10").ToString() ' hdCar_D10.Value

                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D11").ToString() 'hdCar_D11.Value

                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D12").ToString() ' hdCar_D12.Value

                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D13").ToString() 'hdCar_D13.Value

                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D14").ToString() 'hdCar_D14.Value

                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D15").ToString() ' hdCar_D15.Value

                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D16").ToString() ' hdCar_D16.Value

                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D17").ToString() ' hdCar_D17.Value

                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D18").ToString() ' hdCar_D18.Value

                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D19").ToString() ' hdCar_D19.Value

                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D20").ToString() ' hdCar_D20.Value

                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D21").ToString() ' hdCar_D21.Value

                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D22").ToString() ' hdCar_D22.Value

                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D23").ToString() 'hdCar_D23.Value

                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D24").ToString() ' hdCar_D24.Value

                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D25").ToString() 'hdCar_D25.Value

                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D26").ToString() ' hdCar_D26.Value

                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D27").ToString() 'hdCar_D27.Value

                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D28").ToString() 'hdCar_D28.Value
                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D29").ToString() 'hdCar_D29.Value
                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D30").ToString() 'hdCar_D30.Value
                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D31").ToString() 'hdCar_D31.Value
                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
                End If
            End If


            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If


            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")


            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    'grdvCar.Columns(42).Visible = False
                    grdvCar.Columns(43).Visible = False
                    grdvCar.Columns(45).Visible = False

                Else
                    ' grdvCar.Columns(41).Visible = False
                    grdvCar.Columns(43).Visible = False
                    grdvCar.Columns(44).Visible = False
                    grdvCar.Columns(45).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvCar.Columns(45).Visible = False
            End If

            e.Row.Cells(12).Visible = False


            If chkShowAddress.Checked = True Then
                grdvCar.Columns(5).Visible = True
            Else
                grdvCar.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvCar.Columns(2).Visible = True
            Else
                grdvCar.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvCar.Columns(3).Visible = True
            Else
                grdvCar.Columns(3).Visible = False
            End If




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCar_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCar.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            Dim cellcounter, datecounter As Int16
            cellcounter = 15 '14 '13
            If grdvCar.AllowSorting = True Then
                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                Next
            Else
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                Next
            End If
           
        End If
        e.Row.Cells(12).Visible = False

        Dim intMont As Integer = drpMonth.SelectedIndex + 1
        If intMont = 2 Then
            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                'grdvCar.Columns(42).Visible = False
                grdvCar.Columns(44).Visible = False
                grdvCar.Columns(45).Visible = False

            Else
                'grdvCar.Columns(41).Visible = False
                grdvCar.Columns(43).Visible = False
                grdvCar.Columns(44).Visible = False
                grdvCar.Columns(45).Visible = False
            End If

        End If

        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            grdvCar.Columns(45).Visible = False
        End If

        If chkShowAddress.Checked = True Then
            grdvCar.Columns(5).Visible = True
        Else
            grdvCar.Columns(5).Visible = False
        End If
        If chkShowChaniCode.Checked = True Then
            grdvCar.Columns(2).Visible = True
        Else
            grdvCar.Columns(2).Visible = False
        End If

        If chkGroupClassification.Checked = True Then
            grdvCar.Columns(3).Visible = True
        Else
            grdvCar.Columns(3).Visible = False
        End If


    End Sub

    Protected Sub grdvHotel_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvHotel.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            Dim cellcounter, datecounter As Int16
            cellcounter = 15 ' 14 '13
            If grdvHotel.AllowSorting = True Then
                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            Else
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            End If
            
        End If
        grdvHotel.Columns(12).Visible = False

        If chkShowAddress.Checked = True Then
            grdvHotel.Columns(5).Visible = True
        Else
            grdvHotel.Columns(5).Visible = False
        End If
        If chkShowChaniCode.Checked = True Then
            grdvHotel.Columns(2).Visible = True
        Else
            grdvHotel.Columns(2).Visible = False
        End If

        If chkGroupClassification.Checked = True Then
            grdvHotel.Columns(3).Visible = True
        Else
            grdvHotel.Columns(3).Visible = False
        End If


        Dim intMont As Integer = drpMonth.SelectedIndex + 1
        If intMont = 2 Then
            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                'grdvHotel.Columns(42).Visible = False
                grdvHotel.Columns(44).Visible = False
                grdvHotel.Columns(45).Visible = False

            Else
                ' grdvHotel.Columns(41).Visible = False
                grdvHotel.Columns(43).Visible = False
                grdvHotel.Columns(44).Visible = False
                grdvHotel.Columns(45).Visible = False
            End If

        End If

        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            grdvHotel.Columns(45).Visible = False
        End If

    End Sub

    Protected Sub grdvHotel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvHotel.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() 'hdTARGET.Value
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() ' hdTARGETPERDAY.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() ' hdHotel_Netbookings.Value


                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D1").ToString() ' hdHotel_D1.Value
                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D2").ToString() 'hdHotel_D2.Value

                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D3").ToString() ' hdHotel_D3.Value
                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D4").ToString() 'hdHotel_D4.Value
                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D5").ToString() ' hdHotel_D5.Value
                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D6").ToString() ' hdHotel_D6.Value

                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D7").ToString() ' hdHotel_D7.Value

                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D8").ToString() ' hdHotel_D8.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D9").ToString() ' hdHotel_D9.Value
                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D10").ToString() ' hdHotel_D10.Value

                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D11").ToString() ' hdHotel_D11.Value

                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D12").ToString() ' hdHotel_D12.Value

                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D13").ToString() ' hdHotel_D13.Value

                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D14").ToString() ' hdHotel_D14.Value

                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D15").ToString() ' hdD15.Value

                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D16").ToString() ' hdHotel_D16.Value

                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D17").ToString() ' hdHotel_D17.Value

                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D18").ToString() ' hdHotel_D18.Value

                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D19").ToString() ' hdHotel_D19.Value

                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D20").ToString() ' hdHotel_D20.Value

                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D21").ToString() ' hdHotel_D21.Value

                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D22").ToString() ' hdHotel_D22.Value

                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D23").ToString() 'hdHotel_D23.Value

                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D24").ToString() 'hdHotel_D24.Value

                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D25").ToString() 'hdHotel_D25.Value

                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D26").ToString() 'hdHotel_D26.Value

                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D27").ToString() 'hdHotel_D27.Value

                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D28").ToString() ' hdHotel_D28.Value
                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D29").ToString() 'hdHotel_D29.Value
                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D30").ToString() ' hdHotel_D30.Value
                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D31").ToString() ' hdHotel_D31.Value
                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value

                End If
            End If


            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If

            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")

            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    ' grdvHotel.Columns(42).Visible = False
                    grdvHotel.Columns(44).Visible = False
                    grdvHotel.Columns(45).Visible = False

                Else
                    'grdvHotel.Columns(41).Visible = False
                    grdvHotel.Columns(43).Visible = False
                    grdvHotel.Columns(44).Visible = False
                    grdvHotel.Columns(45).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvHotel.Columns(45).Visible = False
            End If

            grdvHotel.Columns(12).Visible = False

            If chkShowAddress.Checked = True Then
                grdvHotel.Columns(5).Visible = True
            Else
                grdvHotel.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvHotel.Columns(2).Visible = True
            Else
                grdvHotel.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvHotel.Columns(3).Visible = True
            Else
                grdvHotel.Columns(3).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvNoChk_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvNoChk.RowCreated
        Try
            grdvNoChk.Columns(12).Visible = False

            If chkAirBreakUp.Checked = True Then
                'grdvNoChk.Columns(11).Visible = False
                grdvNoChk.Columns(13).Visible = False
                grdvNoChk.Columns(14).Visible = False
            End If

            If chkShowAddress.Checked = True Then
                grdvNoChk.Columns(5).Visible = True
            Else
                grdvNoChk.Columns(5).Visible = False
            End If

            If chkShowChaniCode.Checked = True Then
                grdvNoChk.Columns(2).Visible = True
            Else
                grdvNoChk.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvNoChk.Columns(3).Visible = True
            Else
                grdvNoChk.Columns(3).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvNoChk_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvNoChk.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() ' hdTARGET.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() '  hdTARGETPERDAY.Value
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() 'hdPerformance.Value
                End If
            End If
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If

            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")


            'If chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False Then
            grdvNoChk.Columns(12).Visible = False
            'End If
            If chkAirBreakUp.Checked = True Then
                'grdvNoChk.Columns(11).Visible = False
                grdvNoChk.Columns(13).Visible = False
                grdvNoChk.Columns(14).Visible = False
            End If

            If chkShowAddress.Checked = True Then
                grdvNoChk.Columns(5).Visible = True
            Else
                grdvNoChk.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvNoChk.Columns(2).Visible = True
            Else
                grdvNoChk.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvNoChk.Columns(3).Visible = True
            Else
                grdvNoChk.Columns(3).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCarHotel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCarHotel.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() ' hdTARGET.Value
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() ' hdTARGETPERDAY.Value

                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString() ' hdHotel_Netbookings.Value
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() ' hdCar_Netbookings.Value

                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D1").ToString() ' hdCar_D1.Value
                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D1").ToString() ' hdHotel_D1.Value

                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D2").ToString() ' hdCar_D2.Value
                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D2").ToString() ' hdHotel_D2.Value


                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D3").ToString() ' hdCar_D3.Value
                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D3").ToString() ' hdHotel_D3.Value



                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D4").ToString() ' hdCar_D4.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D4").ToString() 'hdHotel_D4.Value


                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D5").ToString() ' hdCar_D5.Value
                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D5").ToString() ' hdHotel_D5.Value


                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D6").ToString() ' hdCar_D6.Value
                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D6").ToString() ' hdHotel_D6.Value
                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D7").ToString() ' hdCar_D7.Value
                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D7").ToString() ' hdHotel_D7.Value

                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D8").ToString() ' hdCar_D8.Value
                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D8").ToString() ' hdHotel_D8.Value

                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D9").ToString() ' hdCar_D9.Value
                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D9").ToString() 'hdHotel_D9.Value


                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D10").ToString() ' hdCar_D10.Value
                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D10").ToString() ' hdHotel_D10.Value


                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D11").ToString() ' hdCar_D11.Value
                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D11").ToString() ' hdHotel_D11.Value


                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D12").ToString() 'hdCar_D12.Value
                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D12").ToString() ' hdHotel_D12.Value




                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D13").ToString() 'hdCar_D13.Value
                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D13").ToString() ' hdHotel_D13.Value



                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D14").ToString() ' hdCar_D14.Value
                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D14").ToString() ' hdHotel_D14.Value



                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D15").ToString() ' hdCar_D15.Value
                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D15").ToString() ' hdHotel_D15.Value



                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D16").ToString() ' hdCar_D16.Value
                    e.Row.Cells(47).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D16").ToString() ' hdCar_D16.Value


                    e.Row.Cells(48).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D17").ToString() ' hdCar_D17.Value
                    e.Row.Cells(49).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D17").ToString() ' hdHotel_D17.Value


                    e.Row.Cells(50).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D18").ToString() ' hdCar_D18.Value
                    e.Row.Cells(51).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D18").ToString() ' hdHotel_D18.Value



                    e.Row.Cells(52).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D19").ToString() ' hdCar_D19.Value
                    e.Row.Cells(53).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D19").ToString() ' hdHotel_D19.Value



                    e.Row.Cells(54).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D20").ToString() ' hdCar_D20.Value
                    e.Row.Cells(55).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D20").ToString() ' hdHotel_D20.Value


                    e.Row.Cells(56).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D21").ToString() ' hdCar_D21.Value
                    e.Row.Cells(57).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D21").ToString() 'hdHotel_D21.Value


                    e.Row.Cells(58).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D22").ToString() ' hdCar_D22.Value
                    e.Row.Cells(59).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D22").ToString() ' hdHotel_D22.Value



                    e.Row.Cells(60).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D23").ToString() ' hdCar_D23.Value
                    e.Row.Cells(61).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D23").ToString() ' hdHotel_D23.Value


                    e.Row.Cells(62).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D24").ToString() ' hdCar_D24.Value
                    e.Row.Cells(63).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D24").ToString() ' hdHotel_D24.Value


                    e.Row.Cells(64).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D25").ToString() ' hdCar_D25.Value
                    e.Row.Cells(65).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D25").ToString() ' hdHotel_D25.Value


                    e.Row.Cells(66).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D26").ToString() ' hdCar_D26.Value
                    e.Row.Cells(67).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D26").ToString() ' hdHotel_D26.Value

                    e.Row.Cells(68).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D27").ToString() ' hdCar_D27.Value
                    e.Row.Cells(69).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D27").ToString() 'hdHotel_D27.Value


                    e.Row.Cells(70).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D28").ToString() ' hdCar_D28.Value
                    e.Row.Cells(71).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D28").ToString() 'hdHotel_D28.Value



                    e.Row.Cells(72).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D29").ToString() ' hdCar_D29.Value
                    e.Row.Cells(73).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D29").ToString() ' hdHotel_D29.Value


                    e.Row.Cells(74).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D30").ToString() 'hdCar_D30.Value
                    e.Row.Cells(75).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D30").ToString() ' hdHotel_D30.Value

                    e.Row.Cells(76).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D31").ToString() ' hdCar_D31.Value
                    e.Row.Cells(77).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D31").ToString() ' hdHotel_D31.Value

                    e.Row.Cells(78).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value

                End If
            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If


            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")



            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    'grdvCarHotel.Columns(72).Visible = False
                    grdvCarHotel.Columns(74).Visible = False
                    grdvCarHotel.Columns(75).Visible = False
                    grdvCarHotel.Columns(76).Visible = False
                    grdvCarHotel.Columns(77).Visible = False

                Else
                    'grdvCarHotel.Columns(68).Visible = False
                    'grdvCarHotel.Columns(69).Visible = False
                    'grdvCarHotel.Columns(70).Visible = False
                    grdvCarHotel.Columns(72).Visible = False
                    grdvCarHotel.Columns(73).Visible = False
                    grdvCarHotel.Columns(74).Visible = False
                    grdvCarHotel.Columns(75).Visible = False
                    grdvCarHotel.Columns(76).Visible = False
                    grdvCarHotel.Columns(77).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                ' grdvCarHotel.Columns(74).Visible = False
                grdvCarHotel.Columns(76).Visible = False
                grdvCarHotel.Columns(77).Visible = False
            End If

            grdvCarHotel.Columns(12).Visible = False

            If chkShowAddress.Checked = True Then
                grdvCarHotel.Columns(5).Visible = True
            Else
                grdvCarHotel.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvCarHotel.Columns(2).Visible = True
            Else
                grdvCarHotel.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvCarHotel.Columns(3).Visible = True
            Else
                grdvCarHotel.Columns(3).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCarHotel_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCarHotel.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            Dim cellcounter, datecounter As Int16
            cellcounter = 16 ' 15 '14

            If grdvCarHotel.AllowSorting = True Then
                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            Else
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            End If
            
        End If

        grdvCarHotel.Columns(12).Visible = False
        If chkShowAddress.Checked = True Then
            grdvCarHotel.Columns(5).Visible = True
        Else
            grdvCarHotel.Columns(5).Visible = False
        End If
        If chkShowChaniCode.Checked = True Then
            grdvCarHotel.Columns(2).Visible = True
        Else
            grdvCarHotel.Columns(2).Visible = False
        End If

        If chkGroupClassification.Checked = True Then
            grdvCarHotel.Columns(3).Visible = True
        Else
            grdvCarHotel.Columns(3).Visible = False
        End If


        Dim intMont As Integer = drpMonth.SelectedIndex + 1
        If intMont = 2 Then
            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                ' grdvCarHotel.Columns(72).Visible = False
                grdvCarHotel.Columns(74).Visible = False
                grdvCarHotel.Columns(75).Visible = False
                grdvCarHotel.Columns(76).Visible = False
                grdvCarHotel.Columns(77).Visible = False
            Else
                'grdvCarHotel.Columns(68).Visible = False
                'grdvCarHotel.Columns(69).Visible = False
                'grdvCarHotel.Columns(70).Visible = False
                grdvCarHotel.Columns(72).Visible = False
                grdvCarHotel.Columns(73).Visible = False
                grdvCarHotel.Columns(74).Visible = False
                grdvCarHotel.Columns(75).Visible = False
                grdvCarHotel.Columns(76).Visible = False
                grdvCarHotel.Columns(77).Visible = False
            End If

        End If

        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            'grdvCarHotel.Columns(74).Visible = False
            grdvCarHotel.Columns(76).Visible = False
            grdvCarHotel.Columns(77).Visible = False
        End If

    End Sub

    Protected Sub grdvAirCar_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirCar.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            'If chkOriginalBk.Checked = True Then
            '    CType(grvRow.Cells(15).Controls(0), LinkButton).Text = "Air net with Psv"
            'End If

            Dim cellcounter, datecounter As Int16
            cellcounter = 19 '18 '17
            If grdvAirCar.AllowSorting = True Then
                CType(grvRow.Cells(17).Controls(0), LinkButton).Text = "Total Air"

                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                Next
            Else
                grvRow.Cells(17).Text = "Total Air"

                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Car"
                    cellcounter += 1
                Next
            End If
          
        End If

        grdvAirCar.Columns(12).Visible = False
        If chkShowAddress.Checked = True Then
            grdvAirCar.Columns(5).Visible = True
        Else
            grdvAirCar.Columns(5).Visible = False
        End If
        If chkShowChaniCode.Checked = True Then
            grdvAirCar.Columns(2).Visible = True
        Else
            grdvAirCar.Columns(2).Visible = False
        End If


        If chkGroupClassification.Checked = True Then
            grdvAirCar.Columns(3).Visible = True
        Else
            grdvAirCar.Columns(3).Visible = False
        End If

        '15,16
        If chkOriginalBk.Checked = False Then
            'grdvAirCar.Columns(14).Visible = False
            grdvAirCar.Columns(16).Visible = False
            grdvAirCar.Columns(17).Visible = False
        Else
            ' grdvAirCar.Columns(14).Visible = True
            grdvAirCar.Columns(16).Visible = True
            grdvAirCar.Columns(17).Visible = True
        End If


        Dim intMont As Integer = drpMonth.SelectedIndex + 1
        If intMont = 2 Then
            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                'grdvAirCar.Columns(75).Visible = False
                grdvAirCar.Columns(77).Visible = False
                grdvAirCar.Columns(78).Visible = False
                grdvAirCar.Columns(79).Visible = False
                grdvAirCar.Columns(80).Visible = False
            Else
                'grdvAirCar.Columns(73).Visible = False
                grdvAirCar.Columns(75).Visible = False
                grdvAirCar.Columns(76).Visible = False
                grdvAirCar.Columns(77).Visible = False
                grdvAirCar.Columns(78).Visible = False
                grdvAirCar.Columns(79).Visible = False
                grdvAirCar.Columns(80).Visible = False
            End If

        End If

        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            'grdvAirCar.Columns(77).Visible = False
            grdvAirCar.Columns(79).Visible = False
            grdvAirCar.Columns(80).Visible = False
        End If

    End Sub

    Protected Sub grdvAirCar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirCar.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() '  hdTARGET.Value
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() ' hdTARGETPERDAY.Value


                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString()
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString()

                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString()
                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString()



                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString()


                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D1").ToString() ' hdD1.Value
                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D1").ToString() ' hdCar_D1.Value
                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D2").ToString() 'hdD2.Value
                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D2").ToString() ' hdCar_D2.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D3").ToString() ' hdD3.Value
                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D3").ToString() ' hdCar_D3.Value
                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D4").ToString() 'hdD4.Value
                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D4").ToString() ' hdCar_D4.Value
                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D5").ToString() ' hdD5.Value
                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D5").ToString() ' hdCar_D5.Value
                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D6").ToString() ' hdD6.Value
                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D6").ToString() 'hdCar_D6.Value
                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D7").ToString() ' hdD7.Value
                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D7").ToString() ' hdCar_D7.Value
                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D8").ToString() 'hdD8.Value
                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D8").ToString() ' hdCar_D8.Value
                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D9").ToString() ' hdD9.Value
                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D9").ToString() ' hdCar_D9.Value
                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D10").ToString() ' hdD10.Value
                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D10").ToString() 'hdCar_D10.Value
                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D11").ToString() 'hdD11.Value
                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D11").ToString() 'hdCar_D11.Value
                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D12").ToString() ' hdD12.Value
                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D12").ToString() 'hdCar_D12.Value


                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D13").ToString() ' hdD13.Value
                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D13").ToString() ' hdCar_D13.Value

                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D14").ToString() 'hdD14.Value
                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D14").ToString() ' hdCar_D14.Value

                    e.Row.Cells(47).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D15").ToString() 'hdD15.Value
                    e.Row.Cells(48).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D15").ToString() ' hdCar_D15.Value

                    e.Row.Cells(49).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D16").ToString() 'hdD16.Value
                    e.Row.Cells(50).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D16").ToString() 'hdCar_D16.Value

                    e.Row.Cells(51).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D17").ToString() 'hdD17.Value
                    e.Row.Cells(52).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D17").ToString() ' hdCar_D17.Value

                    e.Row.Cells(54).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D18").ToString() ' hdD18.Value
                    e.Row.Cells(54).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D18").ToString() 'hdCar_D18.Value

                    e.Row.Cells(55).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D19").ToString() 'hdD19.Value
                    e.Row.Cells(56).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D19").ToString() ' hdCar_D19.Value

                    e.Row.Cells(57).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D20").ToString() ' hdD20.Value
                    e.Row.Cells(58).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D20").ToString() ' hdCar_D20.Value

                    e.Row.Cells(59).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D21").ToString() ' hdD21.Value
                    e.Row.Cells(60).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D21").ToString() ' hdCar_D21.Value

                    e.Row.Cells(61).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D22").ToString() ' hdD22.Value
                    e.Row.Cells(62).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D22").ToString() ' hdCar_D22.Value

                    e.Row.Cells(63).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D23").ToString() ' hdD23.Value
                    e.Row.Cells(64).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D23").ToString() 'hdCar_D23.Value

                    e.Row.Cells(65).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D24").ToString() 'hdD24.Value
                    e.Row.Cells(66).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D24").ToString() ' hdCar_D24.Value

                    e.Row.Cells(67).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D25").ToString() ' hdD25.Value
                    e.Row.Cells(68).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D25").ToString() 'hdCar_D25.Value

                    e.Row.Cells(69).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D26").ToString() ' hdD26.Value
                    e.Row.Cells(70).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D26").ToString() ' hdCar_D26.Value


                    e.Row.Cells(71).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D27").ToString() 'hdD27.Value
                    e.Row.Cells(72).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D27").ToString() ' hdCar_D27.Value

                    e.Row.Cells(73).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D28").ToString() 'hdD28.Value
                    e.Row.Cells(74).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D28").ToString() ' hdCar_D28.Value

                    e.Row.Cells(75).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D29").ToString() ' hdD29.Value
                    e.Row.Cells(76).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D29").ToString() ' hdCar_D29.Value

                    e.Row.Cells(77).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D30").ToString() 'hdD30.Value
                    e.Row.Cells(78).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D30").ToString() ' hdCar_D30.Value

                    e.Row.Cells(79).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D31").ToString() ' hdD31.Value
                    e.Row.Cells(80).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D31").ToString() ' hdCar_D31.Value

                    e.Row.Cells(81).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() 'hdPerformance.Value

                End If
            End If
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If

            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")
            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    'grdvAirCar.Columns(75).Visible = False
                    grdvAirCar.Columns(77).Visible = False
                    grdvAirCar.Columns(78).Visible = False
                    grdvAirCar.Columns(79).Visible = False
                    grdvAirCar.Columns(80).Visible = False
                Else
                    'grdvAirCar.Columns(73).Visible = False
                    grdvAirCar.Columns(75).Visible = False
                    grdvAirCar.Columns(76).Visible = False
                    grdvAirCar.Columns(77).Visible = False
                    grdvAirCar.Columns(78).Visible = False
                    grdvAirCar.Columns(79).Visible = False
                    grdvAirCar.Columns(80).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                'grdvAirCar.Columns(77).Visible = False
                grdvAirCar.Columns(79).Visible = False
                grdvAirCar.Columns(80).Visible = False
            End If

            grdvAirCar.Columns(12).Visible = False

            If chkShowAddress.Checked = True Then
                grdvAirCar.Columns(5).Visible = True
            Else
                grdvAirCar.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvAirCar.Columns(2).Visible = True
            Else
                grdvAirCar.Columns(2).Visible = False
            End If

            If chkGroupClassification.Checked = True Then
                grdvAirCar.Columns(3).Visible = True
            Else
                grdvAirCar.Columns(3).Visible = False
            End If


            '15,16
            If chkOriginalBk.Checked = False Then
                ' grdvAirCar.Columns(14).Visible = False
                grdvAirCar.Columns(16).Visible = False
                grdvAirCar.Columns(17).Visible = False
            Else
                'grdvAirCar.Columns(14).Visible = True
                grdvAirCar.Columns(16).Visible = True
                grdvAirCar.Columns(17).Visible = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirHotel_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirHotel.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(12).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() ' hdTARGET.Value
                    e.Row.Cells(13).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() ' hdTARGETPERDAY.Value
                    e.Row.Cells(14).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdAverageBookings.Value.Trim()
                    e.Row.Cells(15).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() ' hdNetbookings.Value

                    e.Row.Cells(16).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString() ' hdNetbookings.Value
                    e.Row.Cells(17).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString() ' hdNetbookings.Value


                    e.Row.Cells(18).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() 'hdHotel_Netbookings.Value
                    e.Row.Cells(19).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D1").ToString() 'hdD1.Value

                    e.Row.Cells(20).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D1").ToString() ' hdHotel_D1.Value
                    e.Row.Cells(21).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D2").ToString() 'hdD2.Value

                    e.Row.Cells(22).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D2").ToString() 'hdHotel_D2.Value
                    e.Row.Cells(23).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D3").ToString() 'hdD3.Value



                    e.Row.Cells(24).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D3").ToString() ' hdHotel_D3.Value
                    e.Row.Cells(25).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D4").ToString() ' hdD4.Value


                    e.Row.Cells(26).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D4").ToString() ' hdHotel_D4.Value
                    e.Row.Cells(27).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D5").ToString() 'hdD5.Value


                    e.Row.Cells(28).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D5").ToString() 'hdHotel_D5.Value
                    e.Row.Cells(29).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D6").ToString() ' hdD6.Value


                    e.Row.Cells(30).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D6").ToString() 'hdHotel_D6.Value
                    e.Row.Cells(31).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D7").ToString() 'hdD7.Value



                    e.Row.Cells(32).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D7").ToString() ' hdHotel_D7.Value
                    e.Row.Cells(33).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D8").ToString() ' hdD8.Value


                    e.Row.Cells(34).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D8").ToString() ' hdHotel_D8.Value
                    e.Row.Cells(35).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D9").ToString() ' hdD9.Value


                    e.Row.Cells(36).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D9").ToString() ' hdHotel_D9.Value
                    e.Row.Cells(37).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D10").ToString() ' hdD10.Value


                    e.Row.Cells(38).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D10").ToString() ' hdHotel_D10.Value
                    e.Row.Cells(39).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D11").ToString() 'hdD11.Value


                    e.Row.Cells(40).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D11").ToString() ' hdHotel_D11.Value
                    e.Row.Cells(41).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D12").ToString() ' hdD12.Value




                    e.Row.Cells(42).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D12").ToString() ' hdHotel_D12.Value
                    e.Row.Cells(43).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D13").ToString() ' hdD13.Value



                    e.Row.Cells(44).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D13").ToString() ' hdHotel_D13.Value
                    e.Row.Cells(45).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D14").ToString() 'hdD14.Value



                    e.Row.Cells(46).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D14").ToString() ' hdHotel_D14.Value
                    e.Row.Cells(47).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D15").ToString() ' hdD15.Value



                    e.Row.Cells(48).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D15").ToString() ' hdHotel_D15.Value
                    e.Row.Cells(49).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D16").ToString() ' hdD16.Value


                    e.Row.Cells(50).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D16").ToString() ' hdHotel_D16.Value
                    e.Row.Cells(51).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D17").ToString() ' hdD17.Value


                    e.Row.Cells(52).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D17").ToString() ' hdHotel_D17.Value
                    e.Row.Cells(53).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D18").ToString() 'hdD18.Value



                    e.Row.Cells(54).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D18").ToString() ' hdHotel_D18.Value
                    e.Row.Cells(55).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D19").ToString() ' hdD19.Value



                    e.Row.Cells(56).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D19").ToString() ' hdHotel_D19.Value
                    e.Row.Cells(57).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D20").ToString() ' hdD20.Value


                    e.Row.Cells(58).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D20").ToString() ' hdHotel_D20.Value
                    e.Row.Cells(59).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D21").ToString() ' hdD21.Value


                    e.Row.Cells(60).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D21").ToString() ' hdHotel_D21.Value
                    e.Row.Cells(61).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D22").ToString() ' hdD22.Value

                    e.Row.Cells(62).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D22").ToString() 'hdHotel_D22.Value
                    e.Row.Cells(63).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D23").ToString() 'hdD23.Value


                    e.Row.Cells(64).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D23").ToString() 'hdHotel_D23.Value
                    e.Row.Cells(65).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D24").ToString() 'hdD24.Value


                    e.Row.Cells(66).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D24").ToString() ' hdHotel_D24.Value
                    e.Row.Cells(67).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D25").ToString() ' hdD25.Value


                    e.Row.Cells(68).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D25").ToString() ' hdHotel_D25.Value
                    e.Row.Cells(69).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D26").ToString() ' hdD26.Value
                    e.Row.Cells(70).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D26").ToString() ' hdHotel_D26.Value
                    e.Row.Cells(71).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D27").ToString() ' hdD27.Value


                    e.Row.Cells(72).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D27").ToString() 'hdHotel_D27.Value
                    e.Row.Cells(73).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D28").ToString() 'hdD28.Value
                    e.Row.Cells(74).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D28").ToString() ' hdHotel_D28.Value
                    e.Row.Cells(75).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D29").ToString() 'hdD29.Value


                    e.Row.Cells(76).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D29").ToString() 'hdHotel_D29.Value
                    e.Row.Cells(77).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D30").ToString() ' hdD30.Value

                    e.Row.Cells(78).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D30").ToString() ' hdHotel_D30.Value

                    e.Row.Cells(79).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D31").ToString() ' hdD31.Value
                    e.Row.Cells(80).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D31").ToString() ' hdHotel_D31.Value
                    e.Row.Cells(81).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
                End If
            End If


            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If CType(e.Row.FindControl("lblLcode"), Label).Text.Trim().Length = 0 Then
                e.Row.Cells.Clear()
                Exit Sub
            End If


            Dim lnkBtn As System.Web.UI.HtmlControls.HtmlAnchor
            lnkBtn = e.Row.FindControl("lnkDetails")

            Dim strl As String = CType(e.Row.FindControl("lblLcode"), Label).Text.Trim()

            lnkBtn.Attributes.Add("onclick", "return showDetailsDAILYB('" & objED.Encrypt(strl) & "');")


            Dim intMont As Integer = drpMonth.SelectedIndex + 1
            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    ' grdvAirHotel.Columns(75).Visible = False
                    grdvAirHotel.Columns(77).Visible = False
                    grdvAirHotel.Columns(79).Visible = False
                    grdvAirHotel.Columns(78).Visible = False
                    grdvAirHotel.Columns(80).Visible = False
                Else
                    'grdvAirHotel.Columns(71).Visible = False
                    'grdvAirHotel.Columns(72).Visible = False
                    ' grdvAirHotel.Columns(73).Visible = False
                    grdvAirHotel.Columns(75).Visible = False
                    grdvAirHotel.Columns(76).Visible = False
                    grdvAirHotel.Columns(77).Visible = False
                    grdvAirHotel.Columns(79).Visible = False
                    grdvAirHotel.Columns(78).Visible = False
                    grdvAirHotel.Columns(80).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvAirHotel.Columns(79).Visible = False
                grdvAirHotel.Columns(80).Visible = False
            End If

            grdvAirHotel.Columns(12).Visible = False

            If chkShowAddress.Checked = True Then
                grdvAirHotel.Columns(5).Visible = True
            Else
                grdvAirHotel.Columns(5).Visible = False
            End If
            If chkShowChaniCode.Checked = True Then
                grdvAirHotel.Columns(2).Visible = True
            Else
                grdvAirHotel.Columns(2).Visible = False
            End If


            If chkGroupClassification.Checked = True Then
                grdvAirHotel.Columns(3).Visible = True
            Else
                grdvAirHotel.Columns(3).Visible = False
            End If


            If chkOriginalBk.Checked = False Then
                'grdvAirHotel.Columns(14).Visible = False
                grdvAirHotel.Columns(16).Visible = False
                grdvAirHotel.Columns(17).Visible = False
            Else
                ' grdvAirHotel.Columns(14).Visible = True
                grdvAirHotel.Columns(16).Visible = True
                grdvAirHotel.Columns(17).Visible = True
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirHotel_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirHotel.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            'If chkOriginalBk.Checked = True Then
            '    CType(grvRow.Cells(15).Controls(0), LinkButton).Text = "Air net with Psv"
            'End If

            Dim cellcounter, datecounter As Int16
            cellcounter = 19 '18 '17

            If grdvAirHotel.AllowSorting = True Then
                CType(grvRow.Cells(17).Controls(0), LinkButton).Text = "Total Air"
                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            Else
                grvRow.Cells(17).Text = "Total Air"
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
                    cellcounter += 1
                Next
            End If
            
        End If

        grdvAirHotel.Columns(12).Visible = False
        If chkShowAddress.Checked = True Then
            grdvAirHotel.Columns(5).Visible = True
        Else
            grdvAirHotel.Columns(5).Visible = False
        End If
        If chkShowChaniCode.Checked = True Then
            grdvAirHotel.Columns(2).Visible = True
        Else
            grdvAirHotel.Columns(2).Visible = False
        End If

        If chkGroupClassification.Checked = True Then
            grdvAirHotel.Columns(3).Visible = True
        Else
            grdvAirHotel.Columns(3).Visible = False
        End If


        If chkOriginalBk.Checked = False Then
            ' grdvAirHotel.Columns(14).Visible = False
            grdvAirHotel.Columns(16).Visible = False
            grdvAirHotel.Columns(17).Visible = False
        Else
            ' grdvAirHotel.Columns(14).Visible = True
            grdvAirHotel.Columns(16).Visible = True
            grdvAirHotel.Columns(17).Visible = True
        End If


        Dim intMont As Integer = drpMonth.SelectedIndex + 1
        If intMont = 2 Then
            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                ' grdvAirHotel.Columns(75).Visible = False
                grdvAirHotel.Columns(77).Visible = False
                grdvAirHotel.Columns(79).Visible = False
                grdvAirHotel.Columns(78).Visible = False
                grdvAirHotel.Columns(80).Visible = False
            Else
                'grdvAirHotel.Columns(71).Visible = False
                'grdvAirHotel.Columns(72).Visible = False
                ' grdvAirHotel.Columns(73).Visible = False
                grdvAirHotel.Columns(75).Visible = False
                grdvAirHotel.Columns(76).Visible = False
                grdvAirHotel.Columns(77).Visible = False
                grdvAirHotel.Columns(79).Visible = False
                grdvAirHotel.Columns(78).Visible = False
                grdvAirHotel.Columns(80).Visible = False
            End If

        End If

        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            grdvAirHotel.Columns(79).Visible = False
            'grdvAirHotel.Columns(77).Visible = False
            grdvAirHotel.Columns(80).Visible = False
        End If


    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
           

            'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
            objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><ResponsibleStaff></ResponsibleStaff><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><AgencyName></AgencyName><ShowGroup></ShowGroup><OriginalBookings></OriginalBookings><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><ONLINE_STATUS></ONLINE_STATUS><Producitivity_Type></Producitivity_Type><Producitivity_From></Producitivity_From><Producitivity_To></Producitivity_To><Performance_Type></Performance_Type><Performance_From></Performance_From><Performance_To></Performance_To><Region></Region><Air></Air><Car></Car><Hotel></Hotel><AirBreakup></AirBreakup><Month></Month><Year></Year> <Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> <CHAIN_CODE/><TYPEID/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_DAILYBOOKINGS_INPUT>")
            'End If
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                If drpAirLineName.SelectedIndex <> 0 Then
                    .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
                End If
                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
                End If
                If drpCountrys.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chkGroupProductivity.Checked = True Then
                    .SelectSingleNode("ShowGroup").InnerText = "True"
                Else
                    .SelectSingleNode("ShowGroup").InnerText = "False"
                End If

                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    .SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
                End If

                If txtChainCode.Text.Trim <> "" Then
                    .SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011

                If chkOriginalBk.Checked = True Then
                    .SelectSingleNode("OriginalBookings").InnerText = "True"
                Else
                    .SelectSingleNode("OriginalBookings").InnerText = "False"
                End If
                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("ResponsibleStaff").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If
                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If
                If drpOneAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
                End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If


                Dim count As Integer = 0

                .SelectSingleNode("ONLINE_STATUS").InnerText = ""

                For Each ch As ListItem In chkOnlineStatus.Items
                    If ch.Selected = True Then
                        count += 1
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ch.Value.Trim()
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ","
                    End If
                Next
                If .SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length > 0 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                End If

                If count = 1 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(0, 1)
                End If


                'If chkOnlineStatus.SelectedIndex <> 0 Then
                '    .SelectSingleNode("ONLINE_STATUS").InnerText = chkOnlineStatus.SelectedValue.Trim()
                'End If



                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Producitivity_Type").InnerText = drpProductivity.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Producitivity_From").InnerText = Request("txtProductivityFrm")
                .SelectSingleNode("Producitivity_To").InnerText = Request("txtProductivityTo")
                If drpPerformence.SelectedIndex <> 0 Then
                    .SelectSingleNode("Performance_Type").InnerText = drpPerformence.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Performance_From").InnerText = Request("txtPerformenceFrm") 'txtPerformenceFrm.Text.Trim()
                .SelectSingleNode("Performance_To").InnerText = Request("txtPerformenceTo") 'txtPerformenceTo.Text.Trim()

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                If chkAir.Checked = True Then
                    .SelectSingleNode("Air").InnerText = "True"
                Else
                    .SelectSingleNode("Air").InnerText = "False"
                End If
                If chkCar.Checked = True Then
                    .SelectSingleNode("Car").InnerText = "True"
                Else
                    .SelectSingleNode("Car").InnerText = "False"
                End If
                If chkHotel.Checked = True Then
                    .SelectSingleNode("Hotel").InnerText = "True"
                Else
                    .SelectSingleNode("Hotel").InnerText = "False"
                End If

                '.SelectSingleNode("Hotel").InnerXml=

                If chkAirBreakUp.Checked = True Then
                    .SelectSingleNode("AirBreakup").InnerText = "True"
                Else
                    .SelectSingleNode("AirBreakup").InnerText = "False"
                End If

                '.SelectSingleNode("AirBreakup").InnerXml=
                '  If drpMonth.SelectedIndex <> 0 Then
                .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
                ' End If
                'If drpYear.SelectedIndex <> 0 Then
                .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()
                '  End If

                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If


                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESP_1A").InnerText = str(0)

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                .SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If



                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
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
            End With


            'Call of Method for Getting Output Result
            'objOutputXml = objbzDailyBooking.Search(objInputXml)

            If chkAirBreakUp.Checked Then
                'Call Save method 
                'lblError.Text = "Your requested data will be sent at your E-mail ID"
                'objOutputXml = objbzDailyBooking.Search(objInputXml)
                ' objOutputXml.Save("C:\Admin\AirBreakup.xml")
                'PAGE PAGE_COUNT='' TOTAL_ROWS='' 
                If drpCitys.SelectedIndex = 0 And drpAirLineName.SelectedIndex = 0 And txtAgencyName.Text.Trim().Length = 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""

                    ' objInputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value = "0"

                    objbzDailyBooking.UpdateRequest("I", Nothing, objInputXml)
                    lblError.Text = "Data will be send soon"
                    pnlPaging.Visible = False
                    Exit Sub
                    objOutputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_OUTPUT><DAILYBOOKINGS LCODE='' Chain_Code='' Employee_Name='' AgencyName='' ADDRESS='' ONLINE_STATUS='' CITY='' COUNTRY='' OFFICEID='' Aoffice='' Airline_Name='' TARGET='' TARGETPERDAY='' AverageBookings='' Netbookings='' Passive='' WithPassive='' Performance='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' /> <PAGE_TOTAL TARGET='' TARGETPERDAY='' AverageBookings='' Netbookings='' Passive='' WithPassive='' Performance='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' /> <PAGE PAGE_COUNT='' TOTAL_ROWS='' /> <Errors Status='TRUE'><Error Code='101' Description='Data will be send soon' /> </Errors></PR_SEARCH_DAILYBOOKINGS_OUTPUT>")
                Else
                    objOutputXml = objbzDailyBooking.Search(objInputXml)
                End If
            Else
                objOutputXml = objbzDailyBooking.Search(objInputXml)
            End If

            ' objOutputXml = objbzDailyBooking.Search(objInputXml)

            'End of Getting Result







            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                lblError.Text = ""

                If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
                    grdvDailyBookingsAll.AllowSorting = False
                    grdvDailyBookingsAll.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If
                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvDailyBookingsAll.DataSource = dSetBind
                    grdvDailyBookingsAll.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvDailyBookingsAll.HeaderRow.Cells.Count - 2
                        If grdvDailyBookingsAll.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvDailyBookingsAll.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvDailyBookingsAll.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNode.Attributes.Count - 1

                        If grdvDailyBookingsAll.Columns(intclmn).Visible = True Then
                            strArray(intclmnVis) = grdvDailyBookingsAll.HeaderRow.Cells(intclmn).Text
                            For kk As Integer = 0 To objXmlNode.Attributes.Count - 1

                                If objXmlNode.Attributes(kk).Name.Trim = grdvDailyBookingsAll.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")

                End If
                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
                    grdvNoChk.AllowSorting = False
                    grdvNoChk.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If
                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvNoChk.DataSource = dSetBind
                    grdvNoChk.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvNoChk.HeaderRow.Cells.Count - 2
                        If grdvNoChk.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvNoChk.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvNoChk.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNode.Attributes.Count - 1

                        If grdvNoChk.Columns(intclmn).Visible = True Then
                            strArray(intclmnVis) = grdvNoChk.HeaderRow.Cells(intclmn).Text
                            For kk As Integer = 0 To objXmlNode.Attributes.Count - 1

                                If objXmlNode.Attributes(kk).Name.Trim = grdvNoChk.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")


                End If
                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    grdvAirWithAirBr.AllowSorting = False
                    grdvAirWithAirBr.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If

                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvAirWithAirBr.DataSource = dSetBind
                    grdvAirWithAirBr.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvAirWithAirBr.HeaderRow.Cells.Count - 2
                        If grdvAirWithAirBr.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvAirWithAirBr.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvAirWithAirBr.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvAirWithAirBr.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvAirWithAirBr.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvAirWithAirBr.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data

                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then

                    grdvCar.AllowSorting = False
                    grdvCar.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If
                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvCar.DataSource = dSetBind
                    grdvCar.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvCar.HeaderRow.Cells.Count - 2
                        If grdvCar.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvCar.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvCar.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvCar.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvCar.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvCar.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data




                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    grdvHotel.AllowSorting = False
                    grdvHotel.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If

                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvHotel.DataSource = dSetBind
                    grdvHotel.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvHotel.HeaderRow.Cells.Count - 2
                        If grdvHotel.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvHotel.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvHotel.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvHotel.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data




                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    grdvCarHotel.AllowSorting = False
                    grdvCarHotel.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If
                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvCarHotel.DataSource = dSetBind
                    grdvCarHotel.DataBind()


                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvCarHotel.HeaderRow.Cells.Count - 2
                        If grdvCarHotel.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvCarHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvCarHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvCarHotel.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvCarHotel.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvCarHotel.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data

                End If

                If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    grdvAirCar.AllowSorting = False
                    grdvAirCar.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If

                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If

                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvAirCar.DataSource = dSetBind
                    grdvAirCar.DataBind()

                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvAirCar.HeaderRow.Cells.Count - 2
                        If grdvAirCar.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvAirCar.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvAirCar.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvAirCar.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvAirCar.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvAirCar.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data



                End If
                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    grdvAirHotel.AllowSorting = False
                    grdvAirHotel.EnableViewState = True
                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                            objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                        End If
                        If objXmlNode.Attributes("Aoffice") IsNot Nothing Then
                            objXmlNodeClone.Attributes("Aoffice").Value = "Total"
                        End If
                    Next

                    objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                    Dim dSetBind As New DataTable
                    Dim dCol As DataColumn
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                        dSetBind.Columns.Add(dCol)
                    Next

                    Dim dRow As DataRow
                    dRow = dSetBind.NewRow()
                    For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes
                        Dim strAttribut As String = xmlAttrTotal.Name
                        dRow(strAttribut) = xmlAttrTotal.Value
                    Next

                    dSetBind.Rows.Add(dRow)

                    grdvAirHotel.DataSource = dSetBind
                    grdvAirHotel.DataBind()

                    Dim objExport As New ExportExcel
                    Dim IntInvisible As Integer = 0
                    For intclmn As Integer = 0 To grdvAirHotel.HeaderRow.Cells.Count - 2
                        If grdvAirHotel.Columns(intclmn).Visible = False Then
                            IntInvisible = IntInvisible + 1
                        End If
                    Next
                    Dim strArray(grdvAirHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As String

                    Dim intArray(grdvAirHotel.HeaderRow.Cells.Count - 2 - IntInvisible) As Integer
                    Dim intclmnVis As Integer = 0

                    For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                        If grdvAirHotel.Columns(intclmn).Visible = True Then
                            'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                            'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                            strArray(intclmnVis) = grdvAirHotel.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                            '@ Finding Position From xml Related with Header Text

                            For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                                If objXmlNodeClone.Attributes(kk).Name.Trim = grdvAirHotel.Columns(intclmn).SortExpression.Trim Then

                                    intArray(intclmnVis) = kk

                                    intclmnVis = intclmnVis + 1

                                    Exit For

                                End If

                            Next kk

                        End If

                    Next intclmn



                    objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "DailyBooking.xls")
                    'Code for Exporting Data


                End If
            Else
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If









            'Dim grvRow As GridViewRow
            'grvRow = e.Row
            
            'If grvRow.RowType = DataControlRowType.Header Then

            '    Dim cellcounter, datecounter As Int16
            '    cellcounter = 15

            '    If grdvAirHotel.AllowSorting = True Then
            '        For datecounter = 1 To 31
            '            CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
            '            cellcounter += 1
            '            CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
            '            cellcounter += 1
            '        Next
            '    Else
            '        For datecounter = 1 To 31
            '            grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
            '            cellcounter += 1
            '            grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat & " Hotel"
            '            cellcounter += 1
            '        Next
            '    End If

            '  End If










            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    'LCODE="1132" Chain_Code="928" Employee_Name="Andy" AgencyName="Bird Travels Pvt Ltd" ADDRESS="E-9,ConnaughtplaceNULL" ONLINE_STATUS="BTRELO" CITY="New Delhi" COUNTRY="India" OFFICEID="DELFF2240" Aoffice="DEL" Airline_Name="" TARGET="960" TARGETPERDAY="33" AverageBookings="27" Netbookings="745" Performance="1" D1="-16" D2="5" D3="26" D4="1" D5="170" D6="38" D7="83" D8="17" D9="39" D10="29" D11="0" D12="6" D13="36" D14="-51" D15="27" D16="2" D17="-28" D18="1" D19="32" D20="21" D21="43" D22="103" D23="-17" D24="8" D25="8" D26="-57" D27="119" D28="100" D29="0" D30="0" D31="0" 
            '    Dim dtFormat As String
            '    Dim objExport As New ExportExcel
            '    Dim arlst As New ArrayList
            '    Dim strArray As New ArrayList



            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            '    'This dataSet contains Footer Value 
            '    FooterDs = New DataSet()
            '    FooterDs = ds



            '    dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
            '    strArray.Insert(0, "LCODE")
            '    strArray.Insert(1, "Chain Code")
            '    strArray.Insert(2, "Agency Name")
            '    strArray.Insert(3, "Employee Name")
            '    strArray.Insert(4, "Address")
            '    strArray.Insert(5, "Online Status")
            '    strArray.Insert(6, "City")
            '    strArray.Insert(7, "Country")
            '    strArray.Insert(8, "Office ID")
            '    strArray.Insert(9, "Aoffice")
            '    strArray.Insert(10, "Airline Name")
            '    strArray.Insert(11, "Target")
            '    strArray.Insert(12, "Target Per Day")
            '    strArray.Insert(13, "AverageBookings")
            '    strArray.Insert(14, "Netbookings")
            '    strArray.Insert(15, "Car Netbookings")
            '    strArray.Insert(16, "Hotel Netbookings")

            '    Dim cellcounter, datecounter As Int16



            '    For datecounter = 0 To 110
            '        arlst.Insert(datecounter, datecounter)
            '    Next

            '    If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
            '        cellcounter = 17
            '        For datecounter = 1 To 31
            '            strArray.Insert(cellcounter, datecounter.ToString() & "/" & dtFormat)
            '            cellcounter += 1
            '            strArray.Insert(cellcounter, datecounter.ToString() & "/" & dtFormat & " Car")
            '            cellcounter += 1
            '            strArray.Insert(cellcounter, datecounter.ToString() & "/" & dtFormat & " Hotel")
            '            cellcounter += 1
            '        Next
            '        strArray.Insert(cellcounter, "Performence")
            '        Dim counterList As Int16 = 110

            '        If chkShowAddress.Checked = False Then
            '            arlst.RemoveAt(4)
            '            strArray.RemoveAt(4)
            '            counterList -= 1
            '        End If
            '        If chkShowChaniCode.Checked = False Then
            '            arlst.RemoveAt(counterList - 108)
            '            strArray.RemoveAt(counterList - 108)
            '            counterList -= 1
            '        End If

            '        Dim intMont As Integer = drpMonth.SelectedIndex + 1
            '        If intMont = 2 Then
            '            If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
            '                arlst.RemoveRange(counterList - 6, 6)
            '                strArray.RemoveRange(counterList - 6, 6)
            '                counterList -= 5
            '            Else
            '                arlst.RemoveRange(counterList - 9, 9) '.RemoveAt(101)
            '                strArray.RemoveRange(counterList - 9, 9)
            '                counterList -= 8
            '            End If

            '        End If

            '        If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            '            arlst.RemoveRange(counterList - 3, 3) '.RemoveAt(101)
            '            strArray.RemoveRange(counterList - 3, 3)
            '            counterList -= 3
            '        End If
            '        'Hide Airline Name 
            '        arlst.RemoveAt(counterList - 100)
            '        strArray.RemoveAt(counterList - 100)
            '        counterList -= 1
            '        'Case End with Chkbox conditions
            '    End If






            '    Dim objOutputXmlExport As New XmlDocument
            '    Dim objXmlNode, objXmlNodeClone As XmlNode
            '    objOutputXmlExport.LoadXml(ds.GetXml().ToString)
            '    objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DAILYBOOKINGS")
            '    objXmlNodeClone = objXmlNode.CloneNode(True)
            '    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
            '        XmlAttr.Value = ""
            '    Next

            '    With objXmlNodeClone

            '        If FooterDs IsNot Nothing Then

            '            .Attributes("TARGET").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString()
            '            'e.Row.Cells(11).Text = hdTARGET.Value
            '            .Attributes("TARGETPERDAY").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() 'hdTARGETPERDAY.Value
            '            .Attributes("AverageBookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdAverageBookings.Value.Trim()

            '            .Attributes("Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() ' hdNetbookings.Value

            '            .Attributes("Passive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive").ToString() ' hdNetbookings.Value
            '            .Attributes("WithPassive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive").ToString() ' hdNetbookings.Value


            '            .Attributes("Car_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings").ToString() 'hdCar_Netbookings.Value
            '            .Attributes("Hotel_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings").ToString() ' hdHotel_Netbookings.Value


            '            Dim counter As Integer = 19
            '            Dim datecounter1 As Integer
            '            'Dim strDt As String = "D"
            '            'Dim strCar As String = "Car_D"
            '            'Dim strHotel As String = "Hotel_D"

            '            For datecounter1 = 1 To 31
            '                .Attributes("D" + datecounter1.ToString()).Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D" + datecounter1.ToString()).ToString()
            '                counter += 1
            '                .Attributes("Car_D" + datecounter1.ToString()).Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_D" + datecounter1.ToString()).ToString()
            '                counter += 1
            '                .Attributes("Hotel_D" + datecounter1.ToString()).Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_D" + datecounter1.ToString()).ToString()
            '                counter += 1
            '            Next

            '            .Attributes("Performance").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() ' hdPerformance.Value
            '        End If
            '    End With

            '    objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

            '    objExport.ExportDetails(objOutputXmlExport, "DAILYBOOKINGS", arlst, strArray, ExportExcel.ExportFormat.Excel, "exportREport.xls")



            'End If


            'If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then




            '    'grdvNoChk.Columns(10).Visible = False

            '    'If chkAirBreakUp.Checked = True Then
            '    '    grdvNoChk.Columns(11).Visible = False
            '    '    grdvNoChk.Columns(12).Visible = False
            '    'End If

            '    'If chkShowAddress.Checked = True Then
            '    '    grdvNoChk.Columns(4).Visible = True
            '    'Else
            '    '    grdvNoChk.Columns(4).Visible = False
            '    'End If

            '    'If chkShowChaniCode.Checked = True Then
            '    '    grdvNoChk.Columns(2).Visible = True
            '    'Else
            '    '    grdvNoChk.Columns(2).Visible = False
            '    'End If

            '    'grdvNoChk.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvNoChk.DataBind()
            'End If



            'If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then


            '    'If grvRow.RowType = DataControlRowType.Header Then

            '    '    Dim cellcounter, datecounter As Int16
            '    '    cellcounter = 15

            '    '    If grdvAirWithAirBr.AllowSorting = True Then
            '    '        For datecounter = 1 To 31
            '    '            CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
            '    '            cellcounter += 1
            '    '        Next
            '    '    Else
            '    '        For datecounter = 1 To 31
            '    '            grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
            '    '            cellcounter += 1
            '    '        Next
            '    '    End If



            '    '    If chkAir.Checked And chkAirBreakUp.Checked = False Then
            '    '        grdvAirWithAirBr.Columns(10).Visible = False
            '    '    End If
            '    '    If chkAirBreakUp.Checked = True Then
            '    '        grdvAirWithAirBr.Columns(11).Visible = False
            '    '        grdvAirWithAirBr.Columns(12).Visible = False
            '    '    End If
            '    'End If



            '    'Dim intMont As Integer = drpMonth.SelectedIndex + 1
            '    'If intMont = 2 Then
            '    '    If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
            '    '        grdvAirWithAirBr.Columns(44).Visible = False
            '    '        grdvAirWithAirBr.Columns(45).Visible = False

            '    '    Else
            '    '        grdvAirWithAirBr.Columns(43).Visible = False
            '    '        grdvAirWithAirBr.Columns(44).Visible = False
            '    '        grdvAirWithAirBr.Columns(45).Visible = False
            '    '    End If

            '    'End If

            '    'If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
            '    '    grdvAirWithAirBr.Columns(45).Visible = False
            '    'End If


            '    'If chkAir.Checked And chkAirBreakUp.Checked = False Then
            '    '    grdvAirWithAirBr.Columns(10).Visible = False
            '    'End If

            '    'If chkAirBreakUp.Checked = True Then
            '    '    grdvAirWithAirBr.Columns(11).Visible = False
            '    '    grdvAirWithAirBr.Columns(12).Visible = False
            '    'End If




            '    'If chkShowAddress.Checked = True Then
            '    '    grdvAirWithAirBr.Columns(4).Visible = True
            '    'Else
            '    '    grdvAirWithAirBr.Columns(4).Visible = False
            '    'End If

            '    'If chkShowChaniCode.Checked = True Then
            '    '    grdvAirWithAirBr.Columns(2).Visible = True
            '    'Else
            '    '    grdvAirWithAirBr.Columns(2).Visible = False
            '    'End If



            '    'grdvAirWithAirBr.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvAirWithAirBr.DataBind()
            'End If












            'If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
            '    'grdvCar.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvCar.DataBind()
            'End If

            'If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
            '    'grdvHotel.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvHotel.DataBind()
            'End If

            'If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
            '    'grdvCarHotel.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvCarHotel.DataBind()
            'End If

            'If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
            '    'grdvAirCar.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvAirCar.DataBind()

            'End If
            'If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
            '    'grdvAirHotel.DataSource = ds.Tables("DAILYBOOKINGS")
            '    'grdvAirHotel.DataBind()
            'End If






        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ExportGridView(ByVal grdvCommon As GridView)

        Dim strFileName As String = CType(grdvCommon, GridView).ID
        strFileName = strFileName.Substring(4)
        Dim attachment As String = "attachment; filename=" & strFileName & ".xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        grdvCommon.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(grdvCommon)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        Dim l As New Literal
        Dim name As String = ""
        Dim lb As New System.Web.UI.HtmlControls.HtmlAnchor

        Dim i As Int32
        For i = 0 To gv.Controls.Count - 1
            If (gv.Controls(i).GetType Is GetType(HtmlAnchor)) Then
                l.Text = CType(gv.Controls(i), HtmlAnchor).Name
                gv.Controls.Remove(gv.Controls(i))
                gv.Controls.AddAt(i, l)

            End If

            If (gv.Controls(i).HasControls()) Then
                PrepareGridViewForExport(gv.Controls(i))
            End If

        Next
    End Sub

    Protected Sub grdvDailyBookingsAll_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvDailyBookingsAll.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvAirWithAirBr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirWithAirBr.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCar_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvCar.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvHotel_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvHotel.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvNoChk_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvNoChk.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCarHotel_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvCarHotel.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirCar_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirCar.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirHotel_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirHotel.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)
        AllNonDescColumnDefault()
        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ViewState("Desc") = "FALSE"
            If Not objDictionary.Contains(SortName) Then
                ViewState("Desc") = "TRUE"
            End If
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
                If Not objDictionary.Contains(SortName) Then
                    ViewState("Desc") = "TRUE"
                End If
            End If
        End If
        BindData()
    End Sub
#End Region



    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function GetSortColumnIndexMukund(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return -1
    End Function
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




    Private Sub BindDataExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
        Try
            'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
            objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_INPUT><AIRLINE_CODE></AIRLINE_CODE><ResponsibleStaff></ResponsibleStaff><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><AgencyName></AgencyName><ShowGroup></ShowGroup><OriginalBookings></OriginalBookings><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><ONLINE_STATUS></ONLINE_STATUS><Producitivity_Type></Producitivity_Type><Producitivity_From></Producitivity_From><Producitivity_To></Producitivity_To><Performance_Type></Performance_Type><Performance_From></Performance_From><Performance_To></Performance_To><Region></Region><Air></Air><Car></Car><Hotel></Hotel><AirBreakup></AirBreakup><Month></Month><Year></Year> <Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCH_DAILYBOOKINGS_INPUT>")
            'End If
            With objInputXml.DocumentElement


                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
                End If
                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
                End If
                If drpCountrys.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chkGroupProductivity.Checked = True Then
                    .SelectSingleNode("ShowGroup").InnerText = "True"
                Else
                    .SelectSingleNode("ShowGroup").InnerText = "False"
                End If
                If chkOriginalBk.Checked = True Then
                    .SelectSingleNode("OriginalBookings").InnerText = "True"
                Else
                    .SelectSingleNode("OriginalBookings").InnerText = "False"
                End If
                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("ResponsibleStaff").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If
                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If
                If drpOneAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
                End If

                Dim count As Integer = 0

                .SelectSingleNode("ONLINE_STATUS").InnerText = ""

                For Each ch As ListItem In chkOnlineStatus.Items
                    If ch.Selected = True Then
                        count += 1
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ch.Value.Trim()
                        .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                        .SelectSingleNode("ONLINE_STATUS").InnerText += ","
                    End If
                Next
                If .SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length > 0 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                End If

                If count = 1 Then
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                    .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(0, 1)
                End If



                'If chkOnlineStatus.SelectedIndex <> 0 Then
                '    .SelectSingleNode("ONLINE_STATUS").InnerText = chkOnlineStatus.SelectedValue.Trim()
                'End If


                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Producitivity_Type").InnerText = drpProductivity.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Producitivity_From").InnerText = Request("txtProductivityFrm")
                .SelectSingleNode("Producitivity_To").InnerText = Request("txtProductivityTo")
                If drpPerformence.SelectedIndex <> 0 Then
                    .SelectSingleNode("Performance_Type").InnerText = drpPerformence.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("Performance_From").InnerText = Request("txtPerformenceFrm") 'txtPerformenceFrm.Text.Trim()
                .SelectSingleNode("Performance_To").InnerText = Request("txtPerformenceTo") 'txtPerformenceTo.Text.Trim()

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                If chkAir.Checked = True Then
                    .SelectSingleNode("Air").InnerText = "True"
                Else
                    .SelectSingleNode("Air").InnerText = "False"
                End If
                If chkCar.Checked = True Then
                    .SelectSingleNode("Car").InnerText = "True"
                Else
                    .SelectSingleNode("Car").InnerText = "False"
                End If
                If chkHotel.Checked = True Then
                    .SelectSingleNode("Hotel").InnerText = "True"
                Else
                    .SelectSingleNode("Hotel").InnerText = "False"
                End If

                '.SelectSingleNode("Hotel").InnerXml=

                If chkAirBreakUp.Checked = True Then
                    .SelectSingleNode("AirBreakup").InnerText = "True"
                Else
                    .SelectSingleNode("AirBreakup").InnerText = "False"
                End If

                '.SelectSingleNode("AirBreakup").InnerXml=
                '  If drpMonth.SelectedIndex <> 0 Then
                .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
                ' End If
                'If drpYear.SelectedIndex <> 0 Then
                .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()
                '  End If




                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESP_1A").InnerText = str(0)

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                .SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                    .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
                End If

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If

               

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
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
            End With


            objOutputXml = objbzDailyBooking.Search(objInputXml)
            ' End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                'Code for Pagin
                ViewState("PrevSearching") = objInputXml.OuterXml
                ds.ReadXml(objXmlReader)
                'This dataSet contains Footer Value 
                FooterDs = New DataSet()
                FooterDs = ds
                lblError.Text = ""
                'Grid binding as per our conditions
                If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvDailyBookingsAll.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvDailyBookingsAll.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvNoChk.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvNoChk.DataBind()

                    

                End If
                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirWithAirBr.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirWithAirBr.DataBind()
                    
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCar.DataBind()

                   


                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    ' txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvHotel.DataBind()


                   


                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvCarHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCarHotel.DataBind()


                    


                End If

                If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    '  txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirCar.DataBind()

                   



                End If


                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    '  txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()

                    grdvAirHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirHotel.DataBind()

                   

                End If
            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDs = Nothing
                ' txtRecordCount.Text = "0"



                'Code for Paging
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                'Code for Paging


                'Grid binding as per our conditions
                If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvDailyBookingsAll.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvDailyBookingsAll.DataBind()

                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Or (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvNoChk.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvNoChk.DataBind()
                End If

                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = True) Or (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirWithAirBr.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirWithAirBr.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCar.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvHotel.DataBind()
                End If

                If (chkAir.Checked = False And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvCarHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvCarHotel.DataBind()
                End If

                If (chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = False And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirCar.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirCar.DataBind()
                End If


                If (chkAir.Checked = True And chkCar.Checked = False And chkHotel.Checked = True And chkAirBreakUp.Checked = False) Then
                    'txtRecordCount.Text = ds.Tables("DAILYBOOKINGS").Rows.Count.ToString()
                    grdvAirHotel.DataSource = ds.Tables("DAILYBOOKINGS")
                    grdvAirHotel.DataBind()
                End If
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        '  Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings

        'If chkAir.Checked = True And chkCar.Checked = True And chkHotel.Checked = True And chkAirBreakUp.Checked = False Then
        objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGS_INPUT><AIRLINE_CODE></AIRLINE_CODE><ResponsibleStaff></ResponsibleStaff><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><AgencyName></AgencyName><ShowGroup></ShowGroup><OriginalBookings></OriginalBookings><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><ONLINE_STATUS></ONLINE_STATUS><Producitivity_Type></Producitivity_Type><Producitivity_From></Producitivity_From><Producitivity_To></Producitivity_To><Performance_Type></Performance_Type><Performance_From></Performance_From><Performance_To></Performance_To><Region></Region><Air></Air><Car></Car><Hotel></Hotel><AirBreakup></AirBreakup><Month></Month><Year></Year> <Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL> </PR_SEARCH_DAILYBOOKINGS_INPUT>")
        'End If
        With objInputXml.DocumentElement

            If DlstCompVertical.SelectedValue <> "" Then
                .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If


            If drpCitys.SelectedIndex <> 0 Then
                .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
            End If
            If drpCitys.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
            End If
            If drpCountrys.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
            End If
            .SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
            .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
            If chkGroupProductivity.Checked = True Then
                .SelectSingleNode("ShowGroup").InnerText = "True"
            Else
                .SelectSingleNode("ShowGroup").InnerText = "False"
            End If
            If chkOriginalBk.Checked = True Then
                .SelectSingleNode("OriginalBookings").InnerText = "True"
            Else
                .SelectSingleNode("OriginalBookings").InnerText = "False"
            End If
            If drpResponsibleStaff.SelectedIndex <> 0 Then
                .SelectSingleNode("ResponsibleStaff").InnerText = drpResponsibleStaff.SelectedValue.Trim()
            End If

            If drpAgencyType.SelectedIndex <> 0 Then
                .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
            End If
            If drpAgencyStatus.SelectedIndex <> 0 Then
                .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
            End If
            If drpOneAOffice.SelectedIndex <> 0 Then
                .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
            End If

            Dim count As Integer = 0

            .SelectSingleNode("ONLINE_STATUS").InnerText = ""

            For Each ch As ListItem In chkOnlineStatus.Items
                If ch.Selected = True Then
                    count += 1
                    .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                    .SelectSingleNode("ONLINE_STATUS").InnerText += ch.Value.Trim()
                    .SelectSingleNode("ONLINE_STATUS").InnerText += "'"
                    .SelectSingleNode("ONLINE_STATUS").InnerText += ","
                End If
            Next
            If .SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length > 0 Then
                .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
            End If

            If count = 1 Then
                .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(.SelectSingleNode("ONLINE_STATUS").InnerText.Trim.Length - 1, 1)
                .SelectSingleNode("ONLINE_STATUS").InnerText = .SelectSingleNode("ONLINE_STATUS").InnerText.Remove(0, 1)
            End If


            'If chkOnlineStatus.SelectedIndex <> 0 Then
            '    .SelectSingleNode("ONLINE_STATUS").InnerText = chkOnlineStatus.SelectedValue.Trim()
            'End If


            If drpProductivity.SelectedIndex <> 0 Then
                .SelectSingleNode("Producitivity_Type").InnerText = drpProductivity.SelectedItem.Text.Trim()
            End If
            .SelectSingleNode("Producitivity_From").InnerText = Request("txtProductivityFrm")
            .SelectSingleNode("Producitivity_To").InnerText = Request("txtProductivityTo")
            If drpPerformence.SelectedIndex <> 0 Then
                .SelectSingleNode("Performance_Type").InnerText = drpPerformence.SelectedItem.Text.Trim()
            End If
            .SelectSingleNode("Performance_From").InnerText = Request("txtPerformenceFrm") 'txtPerformenceFrm.Text.Trim()
            .SelectSingleNode("Performance_To").InnerText = Request("txtPerformenceTo") 'txtPerformenceTo.Text.Trim()

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
            End If
            If chkAir.Checked = True Then
                .SelectSingleNode("Air").InnerText = "True"
            Else
                .SelectSingleNode("Air").InnerText = "False"
            End If
            If chkCar.Checked = True Then
                .SelectSingleNode("Car").InnerText = "True"
            Else
                .SelectSingleNode("Car").InnerText = "False"
            End If
            If chkHotel.Checked = True Then
                .SelectSingleNode("Hotel").InnerText = "True"
            Else
                .SelectSingleNode("Hotel").InnerText = "False"
            End If

            '.SelectSingleNode("Hotel").InnerXml=

            If chkAirBreakUp.Checked = True Then
                .SelectSingleNode("AirBreakup").InnerText = "True"
            Else
                .SelectSingleNode("AirBreakup").InnerText = "False"
            End If

            '.SelectSingleNode("AirBreakup").InnerXml=
            '  If drpMonth.SelectedIndex <> 0 Then
            .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
            ' End If
            'If drpYear.SelectedIndex <> 0 Then
            .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()
            '  End If




            Dim str As String()
            str = Session("LoginSession").ToString().Split("|")
            .SelectSingleNode("RESP_1A").InnerText = str(0)

            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            .SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                .SelectSingleNode("Limited_To_Aoffice").InnerText = xDoc.DocumentElement.SelectSingleNode("Aoffice").InnerText  'Login time u find Need for discussion
            End If

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If



            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AgencyName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
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
        End With


        objOutputXml = objbzDailyBooking.Search(objInputXml)

        ' End If
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim objExport As New ExportExcel
            'LCODE="1132" Chain_Code="928" Employee_Name="Andy" AgencyName="Bird Travels Pvt Ltd" ADDRESS="E-9,ConnaughtplaceNULL" ONLINE_STATUS="BTRELO" CITY="New Delhi" COUNTRY="India" OFFICEID="DELFF2240" Aoffice="DEL" Airline_Name="" TARGET="960" TARGETPERDAY="33" AverageBookings="27" Netbookings="745" Performance="1" D1="-16" D2="5" D3="26" D4="1" D5="170" D6="38" D7="83" D8="17" D9="39" D10="29" D11="0" D12="6" D13="36" D14="-51" D15="27" D16="2" D17="-28" D18="1" D19="32" D20="21" D21="43" D22="103" D23="-17" D24="8" D25="8" D26="-57" D27="119" D28="100" D29="0" D30="0" D31="0" 
            Dim strArray() As String = {"LCODE", "Chain Code", "Employee Name", "Agency Name", "ADDRESS", "ONLINE STATUS", "CITY", "COUNTRY", "OFFICEID", "Aoffice", "Airline Name", "TARGET", "TARGETPERDAY", "AverageBookings", "Netbookings", "Performance", "2007Da", "2008Ya"}
            Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17}
            objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportREport.xls")
            'objXmlReader = New XmlNodeReader(objOutputXml)
            'System.Diagnostics.Debug.Print("")

        End If

    End Sub
    Private Sub bindCheckboxList()
        Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        objOutputXml = objTaProducts.List()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            '.DataSource = ds.Tables("Status")
            'drpDownList.DataTextField = "StatusCode"
            'drpDownList.DataValueField = "StatusCode"
            'drpDownList.DataBind()
        End If
    End Sub

    Protected Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub chkOriginalBk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOriginalBk.CheckedChanged

    End Sub

    Private Sub SetGridWidth()
        If chkShowAddress.Checked Then
            'Checked all 3 from starting
            tlbgrdvDailyBookingsAll.Width = 9300
            grdvDailyBookingsAll.Width = 9300
            PanelAll.Width = 9300
            'Car
            tlbgrdvCar.Width = 4300
            Panel1.Width = 4300
            grdvCar.Width = 4300

            'Breakup or and Air
            grdvAirWithAirBr.Width = 4300
            tlbgrdvAirWithAirBr.Width = 4300
            Panel2.Width = 4300

            'Hotel Only
            tlbgrdvHotel.Width = 3900
            Panel3.Width = 3900
            grdvHotel.Width = 3900

            'No any check 
            Panel4.Width = 1700
            tlbgrdvNoChk.Width = 1700
            grdvNoChk.Width = 1700

            'Car Hotel both
            Panel5.Width = 6300
            tlbgrdvCarHotel.Width = 6300
            grdvCarHotel.Width = 6300
            'Air +Car
            Panel6.Width = 6600
            tlbgrdvAirCar.Width = 6600
            grdvAirCar.Width = 6600
            'Air+Hotel
            Panel7.Width = 6600
            tlbgrdvAirHotel.Width = 6600
            grdvAirHotel.Width = 6600
        Else
            'Checked all 3 from starting
            tlbgrdvDailyBookingsAll.Width = 8800
            grdvDailyBookingsAll.Width = 8800
            PanelAll.Width = 8800

            'Car
            tlbgrdvCar.Width = 3900
            Panel1.Width = 3900
            grdvCar.Width = 3900

            'Breakup or and Air
            grdvAirWithAirBr.Width = 3700
            tlbgrdvAirWithAirBr.Width = 3700
            Panel2.Width = 3700

            'Hotel Only
            tlbgrdvHotel.Width = 3500
            Panel3.Width = 3500
            grdvHotel.Width = 3500

            'No any check 
            Panel4.Width = 1300
            tlbgrdvNoChk.Width = 1300
            grdvNoChk.Width = 1300

            'Car Hotel both
            Panel5.Width = 5900
            tlbgrdvCarHotel.Width = 5900
            grdvCarHotel.Width = 5900
            'Air +Car
            Panel6.Width = 6100
            tlbgrdvAirCar.Width = 6100
            grdvAirCar.Width = 6100

            'Air+Hotel
            Panel7.Width = 6300
            tlbgrdvAirHotel.Width = 6300
            grdvAirHotel.Width = 6300
        End If
    End Sub

    Protected Sub drpAgencyType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpAgencyType.SelectedIndexChanged

    End Sub

    Protected Sub drpResponsibleStaff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpResponsibleStaff.SelectedIndexChanged

    End Sub
End Class
