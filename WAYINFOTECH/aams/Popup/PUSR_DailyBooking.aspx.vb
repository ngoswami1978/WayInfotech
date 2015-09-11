Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class Popup_PUSR_DailyBooking
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim FooterDs As DataSet
    Dim FooterBreakup As DataSet
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim imgUp1 As New Image
    Dim imgDown1 As New Image
    Dim objED As New EncyrptDeCyrpt
    Dim objExport As New ExportExcel
    Dim objDictionary As New HybridDictionary

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


    Private Sub AllNonDescColumnDefault()
        Try
            objDictionary.Add("Date", "Date")
            objDictionary.Add("DATE", "DATE")
            objDictionary.Add("AirlineName", "AirlineName")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objEams.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
                Exit Sub
            End If

           

            Page.MaintainScrollPositionOnPostBack = True

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            imgUp1.ImageUrl = "~/Images/Sortup.gif"
            imgDown1.ImageUrl = "~/Images/Sortdown.gif"
            'Code for Paging $ Sorting


            btnRun.Attributes.Add("onclick", "return DataValidatin();")
            chkCar.Attributes.Add("onclick", "return ValidateCarHotel();")
            chkHotel.Attributes.Add("onclick", "return ValidateCarHotel();")
            chkAirBreakUp.Attributes.Add("onclick", "return validateAirBreakUp();")

            If Not Page.IsPostBack Then
                'upper comment line
                
                ' type = "../Popup/PUSR_DailyBooking.aspx?Lcode="+lcode +"&Air="+stAir+"&AirBreak="+stAirBreak+"&Car="+stCar+"&Hotel="+stHotel+"&Months="+dtMonth+"&Years="+dtYear;
                Dim strAir, strAirBr, strHotel, strCar As String
                strAir = ""
                strAirBr = ""
                strHotel = ""
                strCar = ""
                If Request.QueryString("Air") IsNot Nothing Then
                    strAir = Request.QueryString("Air").Trim()
                End If

                If Request.QueryString("AirBreak") IsNot Nothing Then
                    strAirBr = Request.QueryString("AirBreak").Trim()
                End If

                If Request.QueryString("Hotel") IsNot Nothing Then
                    strHotel = Request.QueryString("Hotel").Trim()
                End If

                If Request.QueryString("Car") IsNot Nothing Then
                    strCar = Request.QueryString("Car").Trim()
                End If

                'objED.Decrypt("text")

                If Request.QueryString("Lcode") IsNot Nothing Then
                    Dim strLcode As String = objED.Decrypt(Request.QueryString("Lcode").Trim())
                End If

                If Request.QueryString("OriginalBookings") IsNot Nothing Then
                    If Request.QueryString("OriginalBookings").Trim() = "true" Then
                        chkUseOriginalBooking.Checked = True
                    Else
                        chkUseOriginalBooking.Checked = False
                    End If
                End If

                Dim strMonth, strYear As String

                If Request.QueryString("Months") IsNot Nothing Then
                    strMonth = Request.QueryString("Months").Trim()
                    strMonth = (Convert.ToInt32(strMonth) + 1).ToString()
                Else
                    strMonth = DateTime.Now.Month.ToString()
                End If

                If Request.QueryString("Years") IsNot Nothing Then
                    strYear = Request.QueryString("Years").Trim()
                Else
                    strYear = DateTime.Now.Year.ToString()
                End If

                Dim intDays As Int16 = 1

                'If Convert.ToInt16(strMonth) = 12 Then
                '    intDays = DateTime.DaysInMonth(Convert.ToInt16(strYear), Convert.ToInt16(strMonth))
                'Else
                intDays = DateTime.DaysInMonth(Convert.ToInt16(strYear), Convert.ToInt16(strMonth))

                ' End If


                Dim intMonth As Int16 = 1
                'If Convert.ToInt16(strMonth) > 12 Then
                '    intMonth = 12
                'Else
                intMonth = Convert.ToInt16(strMonth)
                ' End If
                'intMonth += 1

                If intMonth < 10 Then
                    strMonth = "0" & intMonth.ToString()
                End If

                txtdtFrom.Text = "01/" & strMonth & "/" & strYear
                txtdtTo.Text = intDays.ToString() & "/" & strMonth & "/" & strYear

                'Following Segment of Conditional Code is executed on the basis of given criteria


                If strAir.Trim() = "true" Then
                    chkAir.Checked = True
                Else
                    chkAir.Checked = False
                End If

                If strAirBr.Trim() = "true" Then
                    chkAirBreakUp.Checked = True
                Else
                    chkAirBreakUp.Checked = False
                End If

                If strHotel.Trim() = "true" Then
                    chkHotel.Checked = True
                Else
                    chkHotel.Checked = False
                End If

                If strCar.Trim() = "true" Then
                    chkCar.Checked = True
                Else
                    chkCar.Checked = False
                End If

                If chkCar.Checked = True Or chkHotel.Checked = True Then
                    chkAirBreakUp.Enabled = False
                End If
                AgencyView()
                If Request("Popup") Is Nothing Then
                    btnRun_Click(sender, e)
                End If
            End If
            'upper comment line
            'Security Check
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='DailyBookings']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnRun.Enabled = False

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
                        chkUseOriginalBooking.Checked = False
                        chkUseOriginalBooking.Visible = False
                    End If
                End If
            Else
                strBuilder = objEams.SecurityCheck(31)
            End If
            'Security Check

            Panel1.Visible = False

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Try

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
            objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT><LCode></LCode><OriginalBookings></OriginalBookings><ShowGroup></ShowGroup><SDATE></SDATE><EDATE></EDATE><RESP_1A></RESP_1A><AIRBREAKUP></AIRBREAKUP><Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT>")

            lblError.Text = ""

            With objInputXml.DocumentElement
                '
                If Request.QueryString("Lcode") IsNot Nothing Then
                    .SelectSingleNode("LCode").InnerText = objED.Decrypt(Request.QueryString("Lcode").Trim())
                End If
                If chkUseOriginalBooking.Checked = True Then
                    .SelectSingleNode("OriginalBookings").InnerText = "True"
                Else
                    .SelectSingleNode("OriginalBookings").InnerText = "False"
                End If
                If chkWholGrp.Checked = True Then
                    .SelectSingleNode("ShowGroup").InnerText = "True"
                Else
                    .SelectSingleNode("ShowGroup").InnerText = "False"
                End If

                If txtdtFrom.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("SDATE").InnerText = objEams.ConvertTextDate(txtdtFrom.Text.Trim())
                End If

                If txtdtTo.Text.Trim().Length <> 0 Then
                    .SelectSingleNode("EDATE").InnerText = objEams.ConvertTextDate(txtdtTo.Text.Trim())
                End If

                If Session("LoginSession") Is Nothing Then
                    lblError.Text = "Re Login"
                    Exit Sub
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

                'If chkBreakup.Checked = True Then
                '    .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                'Else
                '    .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                'End If

                If chkAirBreakUp.Checked = True Then
                    .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                Else
                    .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                End If

                If ViewState("PrevSearching") Is Nothing Then
                    'If ViewState("PrevSearching") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText.Trim.ToUpper <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText.Trim.ToUpper Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "DATE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
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

            If chkBreakup.Checked = True Then
                objOutputXml = objbzDailyBooking.MotiveDetails_Breakup(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                    Dim objXmlReader As XmlNodeReader
                    Dim ds1 As New DataSet




                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds1.ReadXml(objXmlReader)


                    ViewState("PrevSearching") = objInputXml.OuterXml
                    ds.ReadXml(objXmlReader)
                    'This dataSet contains Footer Value 
                    FooterDs = New DataSet()
                    FooterDs = ds1

                    grdvBreakup.DataSource = ds1.Tables("MotiveDetails")
                    grdvBreakup.DataBind()

                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvBreakup)
                    If ViewState("Desc") = "FALSE" Then
                        grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If








                    If ViewState("PrevSearching1") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber1.Items.Count = 0, "1", ddlPageNumber1.SelectedValue)
                    Else
                        Dim objTempInputXml As New XmlDocument
                        Dim objNodeList As XmlNodeList

                        objTempInputXml.LoadXml(ViewState("PrevSearching1"))
                        objNodeList = objTempInputXml.DocumentElement.ChildNodes
                        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber1.Items.Count = 0, "1", ddlPageNumber1.SelectedValue)
                        For Each objNode As XmlNode In objNodeList
                            If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                                If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                    ddlPageNumber1.SelectedValue = "1"
                                End If
                            End If
                        Next
                    End If


                    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                    If ViewState("SortName1") Is Nothing Then
                        ViewState("SortName1") = "Date"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date" '"LOCATION_CODE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName1")
                    End If

                    If ViewState("Desc1") Is Nothing Then
                        ViewState("Desc1") = "FALSE"
                        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc1")
                    End If









































                    ''Code for Second Grid Paging and Sorting
                    'objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
                    'objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = ""
                    'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ""
                    'objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""
                    ''End If
                    Dim objCarXml As New XmlDocument
                    objCarXml = objbzDailyBooking.MotiveDetails_CarHotelBreakup(objInputXml)
                    If objCarXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        Dim objXmlR As XmlNodeReader
                        Dim dSet As New DataSet
                        objXmlR = New XmlNodeReader(objCarXml)
                        dSet.ReadXml(objXmlR)


                        ViewState("PrevSearching1") = objInputXml.OuterXml
                        'Code for Showing Total
                        FooterBreakup = New DataSet()
                        FooterBreakup = dSet

                        grdvCarBreakUp.DataSource = dSet.Tables("MotiveDetails")
                        grdvCarBreakUp.DataBind()

                        PagingCommonNew(objCarXml)
                        Dim intcol1 As Integer = GetSortColumnIndexMukundNew(grdvCarBreakUp)
                        If ViewState("Desc1") = "FALSE" Then
                            grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgUp1)
                        End If
                        If ViewState("Desc1") = "TRUE" Then
                            grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgDown1)
                        End If
                    Else
                        Panel1.Visible = False
                        ddlPageNumber1.Items.Clear()
                    End If
                Else
                    pnlPaging.Visible = False
                    ddlPageNumber.Items.Clear()
                End If

                Exit Sub

            End If



            'If chkAir.Checked = True And chkAirBreakUp.Checked = False And chkHotel.Checked = True And chkCar.Checked = True Then
            '    objOutputXml = objbzDailyBooking.MotiveDetails(objInputXml)
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value

            '        Dim objXmlReader As XmlNodeReader
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        grdvMotivDetails.DataSource = ds.Tables("MotiveDetails")
            '        grdvMotivDetails.DataBind()
            '    Else
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
            '        grdvMotivDetails.DataSource = String.Empty
            '        grdvMotivDetails.DataBind()

            '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'End If

            'If (chkAir.Checked = False Or chkAir.Checked = True) And (chkAirBreakUp.Checked = False Or chkAirBreakUp.Checked = True) And (chkHotel.Checked = True Or chkCar.Checked = True) Then
            '    objOutputXml = objbzDailyBooking.MotiveDetails(objInputXml)
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value

            '        Dim objXmlReader As XmlNodeReader
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        grdvMotivDetails.DataSource = ds.Tables("MotiveDetails")
            '        grdvMotivDetails.DataBind()
            '    Else
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
            '        grdvMotivDetails.DataSource = String.Empty
            '        grdvMotivDetails.DataBind()

            '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'End If

            'If chkAir.Checked = True Or chkCar.Checked = True Then
            '    objOutputXml = objbzDailyBooking.MotiveDetails(objInputXml)
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value

            '        Dim objXmlReader As XmlNodeReader
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        grdvMotivDetails.DataSource = ds.Tables("MotiveDetails")
            '        grdvMotivDetails.DataBind()
            '    Else
            '        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
            '        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
            '        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
            '        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
            '        grdvMotivDetails.DataSource = String.Empty
            '        grdvMotivDetails.DataBind()

            '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'End If
            If chkAirBreakUp.Checked = True Then
                ' If (chkAir.Checked = True And chkAirBreakUp.Checked = True And chkCar.Checked = False And chkHotel.Checked = False) Or (chkAir.Checked = True And chkAirBreakUp.Checked = False And chkCar.Checked = False And chkHotel.Checked = False) Then
                objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
                objOutputXml = objbzDailyBooking.MotiveDetails_AirBreakup(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                    Dim objXmlReader As XmlNodeReader
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)




                    ViewState("PrevSearching") = objInputXml.OuterXml
                    '  ViewState("PrevSearching1") = Nothing
                    ds.ReadXml(objXmlReader)
                    'This dataSet contains Footer Value 
                    FooterDs = New DataSet()
                    FooterDs = ds



                    grdvAirwithArBreak.DataSource = ds.Tables("MotiveDetails")
                    grdvAirwithArBreak.DataBind()

                    PagingCommon(objOutputXml)

                    Dim intcol1 As Integer = GetSortColumnIndexMukund(grdvAirwithArBreak)

                    If ViewState("Desc") = "FALSE" Then
                        grdvAirwithArBreak.HeaderRow.Cells(intcol1).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvAirwithArBreak.HeaderRow.Cells(intcol1).Controls.Add(imgDown)
                    End If

                    'End of showing List of Data

                Else
                    'txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    'txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    'txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    'txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                    grdvAirwithArBreak.DataSource = String.Empty
                    grdvAirwithArBreak.DataBind()
                    pnlPaging.Visible = False
                    ddlPageNumber.Items.Clear()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                objOutputXml = objbzDailyBooking.MotiveDetails(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value

                    Dim objXmlReader As XmlNodeReader
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)


                    ViewState("PrevSearching") = objInputXml.OuterXml
                    ds.ReadXml(objXmlReader)
                    'This dataSet contains Footer Value 
                    FooterDs = New DataSet()
                    FooterDs = ds


                    grdvMotivDetails.DataSource = ds.Tables("MotiveDetails")
                    grdvMotivDetails.DataBind()


                    Dim intcol1 As Integer = GetSortColumnIndexMukund(grdvMotivDetails)
                    PagingCommon(objOutputXml)

                    If ViewState("Desc") = "FALSE" Then
                        grdvMotivDetails.HeaderRow.Cells(intcol1).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvMotivDetails.HeaderRow.Cells(intcol1).Controls.Add(imgDown)
                    End If

                Else
                    'txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    'txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    'txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    'txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                    grdvMotivDetails.DataSource = String.Empty
                    grdvMotivDetails.DataBind()
                    pnlPaging.Visible = False
                    ddlPageNumber.Items.Clear()
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If


            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       End Sub

    Protected Sub chkBreakup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBreakup.CheckedChanged
        Try
            Panel1.Visible = False

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
            objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT><LCode></LCode><OriginalBookings></OriginalBookings><ShowGroup></ShowGroup><SDATE></SDATE><EDATE></EDATE><RESP_1A></RESP_1A><AIRBREAKUP></AIRBREAKUP><Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT>")

            lblError.Text = ""

            With objInputXml.DocumentElement
                '
                If Request.QueryString("Lcode") IsNot Nothing Then
                    .SelectSingleNode("LCode").InnerText = objED.Decrypt(Request.QueryString("Lcode").Trim())
                End If

                If chkUseOriginalBooking.Checked = True Then
                    .SelectSingleNode("OriginalBookings").InnerText = "True"
                Else
                    .SelectSingleNode("OriginalBookings").InnerText = "False"
                End If
                If chkWholGrp.Checked = True Then
                    .SelectSingleNode("ShowGroup").InnerText = "True"
                Else
                    .SelectSingleNode("ShowGroup").InnerText = "False"
                End If
                .SelectSingleNode("SDATE").InnerText = objEams.ConvertTextDate(txtdtFrom.Text.Trim())
                .SelectSingleNode("EDATE").InnerText = objEams.ConvertTextDate(txtdtTo.Text.Trim())

                If Session("LoginSession") Is Nothing Then
                    lblError.Text = "Re Login"
                    Exit Sub
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

                If chkAirBreakUp.Checked = True Then
                    .SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
                Else
                    .SelectSingleNode("AIRBREAKUP").InnerText = "FALSE"
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
                    ViewState("SortName") = "DATE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
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

            If chkBreakup.Checked = True Then
                ' objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
                objOutputXml = objbzDailyBooking.MotiveDetails_Breakup(objInputXml)
                'objOutputXml = objbzDailyBooking.MotiveDetails_AirBreakup(objInputXml)
               If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                    txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                    txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                    txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                    Dim objXmlReader As XmlNodeReader
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)




                    ViewState("PrevSearching") = objInputXml.OuterXml
                    ds.ReadXml(objXmlReader)
                    'This dataSet contains Footer Value 
                    FooterDs = New DataSet()
                    FooterDs = ds

                    grdvBreakup.DataSource = ds.Tables("MotiveDetails")
                    grdvBreakup.DataBind()


                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndexMukund(grdvBreakup)
                    If ViewState("Desc") = "FALSE" Then
                        grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If








                    If ViewState("PrevSearching1") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber1.Items.Count = 0, "1", ddlPageNumber1.SelectedValue)
                    Else
                        Dim objTempInputXml As New XmlDocument
                        Dim objNodeList As XmlNodeList

                        objTempInputXml.LoadXml(ViewState("PrevSearching1"))
                        objNodeList = objTempInputXml.DocumentElement.ChildNodes
                        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber1.Items.Count = 0, "1", ddlPageNumber1.SelectedValue)
                        For Each objNode As XmlNode In objNodeList
                            If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                                If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                    ddlPageNumber1.SelectedValue = "1"
                                End If
                            End If
                        Next
                    End If


                    objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                    If ViewState("SortName1") Is Nothing Then
                        ViewState("SortName1") = "Date"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date" '"LOCATION_CODE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName1")
                        'If (ViewState("SortName").ToString().Trim() = "HotelBookings" Or ViewState("SortName").ToString().Trim() = "Hotel_Netbookings") And chkHotel.Checked = False Then
                        '    ViewState("SortName") = "Date"
                        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date"
                        'End If

                        'If (ViewState("SortName").ToString().Trim() = "CarBookings" Or ViewState("SortName").ToString().Trim() = "Car_Netbookings") And chkCar.Checked = False Then
                        '    ViewState("SortName") = "Date"
                        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date"
                        'End If

                    End If

                    If ViewState("Desc1") Is Nothing Then
                        ViewState("Desc1") = "FALSE"
                        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc1")
                    End If



                    Dim objCarXml As New XmlDocument
                    objCarXml = objbzDailyBooking.MotiveDetails_CarHotelBreakup(objInputXml)
                    If objCarXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ViewState("PrevSearching1") = objInputXml.OuterXml

                        Dim objXmlR As XmlNodeReader
                        Dim dSet As New DataSet
                        objXmlR = New XmlNodeReader(objCarXml)
                        dSet.ReadXml(objXmlR)
                        FooterBreakup = dSet
                        grdvCarBreakUp.DataSource = dSet.Tables("MotiveDetails")
                        grdvCarBreakUp.DataBind()

                        'Code for Paging
                        PagingCommonNew(objCarXml)
                        Dim intcol1 As Integer = GetSortColumnIndexMukundNew(grdvCarBreakUp)
                        If ViewState("Desc1") = "FALSE" Then
                            grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgUp1)
                        End If
                        If ViewState("Desc1") = "TRUE" Then
                            grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgDown1)
                        End If
                    Else
                        Panel1.Visible = False
                        ddlPageNumber1.Items.Clear()
                    End If
                Else
                    'Code segment for showing List of data
                    pnlPaging.Visible = False
                    ddlPageNumber.Items.Clear()
                End If
            Else

                btnRun_Click(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub grdvMotivDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMotivDetails.RowDataBound
        Try
            'totalCost += Convert.ToDouble(Val(qty & "")) * Convert.ToDouble(Val(rate & ""))
            'txtTotalCost.Text = Format(totalCost, "0.00")
            If e.Row.RowType = DataControlRowType.Footer Then
                'lblHNetbookings  lblHAir_Netbookings lblHCar_Netbookings lblHHotel_Netbookings
                CType(e.Row.FindControl("lblHNetbookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings") ' valNetbookings.ToString()
                CType(e.Row.FindControl("lblHAir_Netbookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Air_Netbookings") ' valHAir_Netbookings.ToString()
                CType(e.Row.FindControl("lblHCar_Netbookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings") ' valHCar_Netbookings.ToString()
                CType(e.Row.FindControl("lblHHotel_Netbookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings") 'valHHotel_Netbookings.ToString()
                CType(e.Row.FindControl("lblHPASSIVE"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE") 'valHHotel_Netbookings.ToString()
                CType(e.Row.FindControl("lblHWITHPASSIVE"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE") 'valHHotel_Netbookings.ToString()

            End If

           

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            

            If chkHotel.Checked = False And chkCar.Checked = False Then
                grdvMotivDetails.Columns(5).Visible = False
                grdvMotivDetails.Columns(6).Visible = False
            Else
                If chkHotel.Checked = False Then
                    grdvMotivDetails.Columns(6).Visible = False
                End If

                If chkCar.Checked = False Then
                    grdvMotivDetails.Columns(5).Visible = False
                End If
            End If

            If chkUseOriginalBooking.Checked = True Then
                grdvMotivDetails.Columns(3).Visible = True
                grdvMotivDetails.Columns(4).Visible = True

            Else
                grdvMotivDetails.Columns(3).Visible = False
                grdvMotivDetails.Columns(4).Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub grdvBreakup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBreakup.RowDataBound
        Try

           If e.Row.RowType = DataControlRowType.Footer Then
                CType(e.Row.FindControl("lblHAIR_ADD_BOOKINGS"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_ADD_BOOKINGS") ' AIR_ADD_BOOKINGSval.ToString()
                CType(e.Row.FindControl("lblHAIR_CANCEL_BOOKINGS"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_CANCEL_BOOKINGS") 'AIR_CANCEL_BOOKINGSval.ToString()
                CType(e.Row.FindControl("lblHAIR_MODIFY_BOOKINGS"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_MODIFY_BOOKINGS") ' AIR_MODIFY_BOOKINGSval.ToString()
                CType(e.Row.FindControl("lblHAIR_NET_BOOKINGS"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_NET_BOOKINGS") ' AIR_NET_BOOKINGSval.ToString()
                CType(e.Row.FindControl("lblFPASSIVE_ADD"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_ADD") ' PASSIVE_ADDval.ToString()
                CType(e.Row.FindControl("lblFPASSIVE_CANCEL"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_CANCEL") 'PASSIVE_CANCELval.ToString()
                CType(e.Row.FindControl("lblFPASSIVE_MODIFY"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_MODIFY") ' PASSIVE_MODIFYval.ToString()
                CType(e.Row.FindControl("lblFPASSIVE_NET"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_NET") 'PASSIVE_NETval.ToString()
                CType(e.Row.FindControl("lblFNETBOOKINGS"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS") ' NETBOOKINGSval.ToString()
                CType(e.Row.FindControl("lblFAirBookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AirBookings") 'AirBookingsval.ToString()
                CType(e.Row.FindControl("lblFCarBookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("CarBookings") 'CarBookingsval.ToString()
                CType(e.Row.FindControl("lblFHotelBookings"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("HotelBookings") 'HotelBookingsval.ToString()

                CType(e.Row.FindControl("lblHPASSIVE"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE") 'valHHotel_Netbookings.ToString()
                CType(e.Row.FindControl("lblHWITHPASSIVE"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WITHPASSIVE") 'valHHotel_Netbookings.ToString()

            End If

            If e.Row.RowType = DataControlRowType.Header Then
                If chkUseOriginalBooking.Checked = True Then
                    CType(e.Row.Cells(4).Controls(0), LinkButton).Text = "Total Air"
                End If
            End If


            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            If chkHotel.Checked = False And chkCar.Checked = False Then
                grdvBreakup.Columns(5).Visible = False
                grdvBreakup.Columns(6).Visible = False
            Else
                If chkHotel.Checked = False Then
                    grdvBreakup.Columns(6).Visible = False
                End If

                If chkCar.Checked = False Then
                    grdvBreakup.Columns(5).Visible = False
                End If
            End If

            If chkAirBreakUp.Checked = False Then
                grdvBreakup.Columns(15).Visible = False
            End If
            '10,11
            If chkUseOriginalBooking.Checked = True Then
                grdvBreakup.Columns(3).Visible = True
                grdvBreakup.Columns(4).Visible = True
                ''Added on 05/12/2009
                grdvBreakup.Columns(11).Visible = True
                grdvBreakup.Columns(12).Visible = True
                grdvBreakup.Columns(13).Visible = True
                grdvBreakup.Columns(14).Visible = True

            Else
                grdvBreakup.Columns(3).Visible = False
                grdvBreakup.Columns(4).Visible = False
                ''Added on 05/12/2009
                ''Added on 05/12/2009
                grdvBreakup.Columns(11).Visible = False
                grdvBreakup.Columns(12).Visible = False
                grdvBreakup.Columns(13).Visible = False
                grdvBreakup.Columns(14).Visible = False
            End If
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

    Protected Sub grdvAirwithArBreak_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirwithArBreak.RowDataBound

        If chkAirBreakUp.Checked = False Then
            e.Row.Cells(2).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            If FooterDs IsNot Nothing Then
                CType(e.Row.FindControl("lblAir_Netbookings1"), Label).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Air_Netbookings") ' AIR_NET_BOOKINGSval.ToString()
            End If
        End If

        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
    End Sub

    Protected Sub grdvCarBreakUp_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCarBreakUp.RowDataBound

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Bookings") ' AIR_ADD_BOOKINGSval.ToString()
            e.Row.Cells(2).Text = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Hotel_Bookings") '  AIR_CANCEL_BOOKINGSval.ToString()
            e.Row.Cells(3).Text = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Voucher_Printed") ' AIR_MODIFY_BOOKINGSval.ToString()
            e.Row.Cells(4).Text = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Voucher_Printed") '  AIR_NET_BOOKINGSval.ToString()
            e.Row.Cells(5).Text = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Hotel_Nights") '  PASSIVE_ADDval.ToString()
        End If

        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If


    End Sub

    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
       
        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
        If Request.QueryString("Lcode") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = objED.Decrypt(Request.QueryString("Lcode").Trim())
        End If
        objOutputXml = objbzAgency.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                txtAgencyName.Text = .Attributes("NAME").Value()
                txtAddress.Text = .Attributes("ADDRESS").Value()
                txtCity.Text = .Attributes("CITY").Value()
                txtCountry.Text = .Attributes("COUNTRY").Value()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMotivDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMotivDetails.Sorting
        Try
            Dim SortName As String = e.SortExpression
            SortCall(SortName)
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Protected Sub grdvAirwithArBreak_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirwithArBreak.Sorting
        Try
            Dim SortName As String = e.SortExpression
            SortCall(SortName)
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub grdvBreakup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvBreakup.Sorting
        Try
            Dim SortName As String = e.SortExpression
            SortCall(SortName)
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub grdvCarBreakUp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvCarBreakUp.Sorting
        Try
            Dim SortName As String = e.SortExpression
            SortCall1(SortName)
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub PagingCommonNew(ByVal objOutputXml As XmlDocument)
        Panel1.Visible = True
        Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
        Dim selectedValue As String = IIf(ddlPageNumber1.SelectedValue = "", "1", ddlPageNumber1.SelectedValue)
        If count <> ddlPageNumber1.Items.Count Then
            ddlPageNumber1.Items.Clear()
            For i As Integer = 1 To count
                ddlPageNumber1.Items.Add(i.ToString)
            Next
        End If
        ddlPageNumber1.SelectedValue = selectedValue
        'Code for hiding prev and next button based on count
        If count = 1 Then
            'pnlPaging.Visible = False
            ' ddlPageNumber.Visible = False
            lnkNext1.Visible = False
            lnkPrev1.Visible = False
        Else
            'ddlPageNumber.Visible = True
            lnkPrev1.Visible = True
            lnkNext1.Visible = True
        End If

        'Code for hiding next button when pagenumber is equal to page count
        If ddlPageNumber1.SelectedValue = count.ToString Then
            lnkNext1.Visible = False
        Else
            lnkNext1.Visible = True
        End If

        'Code for hiding prev button when pagenumber is 1
        If ddlPageNumber1.SelectedValue = "1" Then
            lnkPrev1.Visible = False
        Else
            lnkPrev1.Visible = True
        End If
        txtTotalRecordCount1.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
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
    Private Function GetSortColumnIndexMukund(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Private Function GetSortColumnIndexMukundNew(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName1").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

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
       End Sub
#End Region

#Region "Sort Function"
    Sub SortCall1(ByVal SortName As String)
        AllNonDescColumnDefault()
        If ViewState("SortName1") Is Nothing Then
            ViewState("SortName1") = SortName
            ViewState("Desc1") = "FALSE"
            If Not objDictionary.Contains(SortName) Then
                ViewState("Desc1") = "TRUE"
            End If
        Else
            If ViewState("SortName1") = SortName Then
                If ViewState("Desc1") = "TRUE" Then
                    ViewState("Desc1") = "FALSE"
                Else
                    ViewState("Desc1") = "TRUE"
                End If
            Else
                ViewState("SortName1") = SortName
                ViewState("Desc1") = "FALSE"
                If Not objDictionary.Contains(SortName) Then
                    ViewState("Desc1") = "TRUE"
                End If
            End If
        End If
    End Sub
#End Region



#Region "Binding Car Breakup Result"
    Private Sub BindCarBreakup()
        Dim objOutputXml, objInputXml As New XmlDocument
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
        Dim ds As New DataSet

        objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT><LCode></LCode><OriginalBookings></OriginalBookings><ShowGroup></ShowGroup><SDATE></SDATE><EDATE></EDATE><RESP_1A></RESP_1A><AIRBREAKUP></AIRBREAKUP><Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT>")

      With objInputXml.DocumentElement
            '
            If Request.QueryString("Lcode") IsNot Nothing Then
                .SelectSingleNode("LCode").InnerText = Request.QueryString("Lcode").Trim()
            End If

            If chkUseOriginalBooking.Checked = True Then
                .SelectSingleNode("OriginalBookings").InnerText = "True"
            Else
                .SelectSingleNode("OriginalBookings").InnerText = "False"
            End If
            If chkWholGrp.Checked = True Then
                .SelectSingleNode("ShowGroup").InnerText = "True"
            Else
                .SelectSingleNode("ShowGroup").InnerText = "False"
            End If
            .SelectSingleNode("SDATE").InnerText = objEams.ConvertTextDate(txtdtFrom.Text.Trim())
            .SelectSingleNode("EDATE").InnerText = objEams.ConvertTextDate(txtdtTo.Text.Trim())

            If Session("LoginSession") Is Nothing Then
                lblError.Text = "Re Login"
                Exit Sub
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

            If chkAirBreakUp.Checked = True Then
                .SelectSingleNode("AIRBREAKUP").InnerText = "True"
            Else
                .SelectSingleNode("AIRBREAKUP").InnerText = "False"
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
                ViewState("SortName") = "Date"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date" '"LOCATION_CODE"
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


        Dim objCarXml As New XmlDocument
        objCarXml = objbzDailyBooking.MotiveDetails_CarHotelBreakup(objInputXml)
        If objCarXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Dim objXmlR As XmlNodeReader
            Dim dSet As New DataSet
            objXmlR = New XmlNodeReader(objCarXml)
            ViewState("PrevSearching") = objInputXml.OuterXml
            dSet.ReadXml(objXmlR)
            grdvCarBreakUp.DataSource = dSet.Tables("MotiveDetails")
            grdvCarBreakUp.DataBind()
            FooterBreakup = dSet
        End If
    End Sub
#End Region

    Protected Sub lnkNext1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext1.Click
        Try
            If ddlPageNumber1.SelectedValue <> (ddlPageNumber1.Items.Count).ToString Then
                ddlPageNumber1.SelectedValue = (CInt(ddlPageNumber1.SelectedValue) + 1).ToString
            End If
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev1.Click
        Try
            If ddlPageNumber1.SelectedValue <> "1" Then
                ddlPageNumber1.SelectedValue = (CInt(ddlPageNumber1.SelectedValue) - 1).ToString
            End If
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber1.SelectedIndexChanged
        Try
            btnRun_Click(sender, e)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMotivDetails_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMotivDetails.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            If chkUseOriginalBooking.Checked = True Then
                grdvMotivDetails.Columns(3).Visible = True
                grdvMotivDetails.Columns(4).Visible = True
                CType(e.Row.Cells(4).Controls(0), LinkButton).Text = "Total Air"
            Else
                grdvMotivDetails.Columns(3).Visible = False
                grdvMotivDetails.Columns(4).Visible = False
            End If


        End If
    End Sub

    Protected Sub grdvBreakup_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBreakup.RowCreated
        '11
        If e.Row.RowType = DataControlRowType.Header Then
            If chkUseOriginalBooking.Checked = True Then
                CType(e.Row.Cells(4).Controls(0), LinkButton).Text = "Total Air"
          End If
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Try

                Dim objInputXml, objOutputXml As New XmlDocument
                Dim ds As New DataSet
                Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
                objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT><LCode></LCode><OriginalBookings></OriginalBookings><ShowGroup></ShowGroup><SDATE></SDATE><EDATE></EDATE><RESP_1A></RESP_1A><AIRBREAKUP></AIRBREAKUP><Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT>")

                lblError.Text = ""

                With objInputXml.DocumentElement
                    '
                    If Request.QueryString("Lcode") IsNot Nothing Then
                        .SelectSingleNode("LCode").InnerText = objED.Decrypt(Request.QueryString("Lcode").Trim())
                    End If

                    If chkUseOriginalBooking.Checked = True Then
                        .SelectSingleNode("OriginalBookings").InnerText = "True"
                    Else
                        .SelectSingleNode("OriginalBookings").InnerText = "False"
                    End If

                    If chkWholGrp.Checked = True Then
                        .SelectSingleNode("ShowGroup").InnerText = "True"
                    Else
                        .SelectSingleNode("ShowGroup").InnerText = "False"
                    End If

                    .SelectSingleNode("SDATE").InnerText = objEams.ConvertTextDate(txtdtFrom.Text.Trim())
                    .SelectSingleNode("EDATE").InnerText = objEams.ConvertTextDate(txtdtTo.Text.Trim())

                    If Session("LoginSession") Is Nothing Then
                        lblError.Text = "Re Login"
                        Exit Sub
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

                    'If chkBreakup.Checked = True Then
                    '    .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                    'Else
                    '    .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                    'End If

                    If chkAirBreakUp.Checked = True Then
                        .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                    Else
                        .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                    End If


                    If ViewState("SortName") Is Nothing Then
                        ViewState("SortName") = "DATE"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
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

                If chkBreakup.Checked = True Then
                    objOutputXml = objbzDailyBooking.MotiveDetails_Breakup(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                        Dim objXmlReader As XmlNodeReader
                        Dim ds1 As New DataSet




                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds1.ReadXml(objXmlReader)


                        ViewState("PrevSearching") = objInputXml.OuterXml
                        ds.ReadXml(objXmlReader)
                        'This dataSet contains Footer Value 
                        FooterDs = New DataSet()
                        FooterDs = ds1

                        grdvBreakup.DataSource = ds1.Tables("MotiveDetails")
                        grdvBreakup.DataBind()








                        ''Code for Exporting Data
                        Dim objOutputXmlExport As New XmlDocument
                        Dim objXmlNode, objXmlNodeClone As XmlNode
                        objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                        objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DAILYBOOKINGS/MotiveDetails")
                        objXmlNodeClone = objXmlNode.CloneNode(True)

                        For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                            XmlAttr.Value = ""
                        Next

                        With objXmlNodeClone
                            If FooterDs IsNot Nothing Then
                                .Attributes("DATE").Value = "Total"
                                .Attributes("AIR_ADD_BOOKINGS").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_ADD_BOOKINGS") ' AIR_ADD_BOOKINGSval.ToString()
                                .Attributes("AIR_CANCEL_BOOKINGS").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_CANCEL_BOOKINGS") 'AIR_CANCEL_BOOKINGSval.ToString()
                                .Attributes("AIR_MODIFY_BOOKINGS").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_MODIFY_BOOKINGS") ' AIR_MODIFY_BOOKINGSval.ToString()
                                .Attributes("AIR_NET_BOOKINGS").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AIR_NET_BOOKINGS") ' AIR_NET_BOOKINGSval.ToString()
                                .Attributes("PASSIVE_ADD").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_ADD") ' PASSIVE_ADDval.ToString()
                                .Attributes("PASSIVE_CANCEL").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_CANCEL") 'PASSIVE_CANCELval.ToString()
                                .Attributes("PASSIVE_MODIFY").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_MODIFY") ' PASSIVE_MODIFYval.ToString()
                                .Attributes("PASSIVE_NET").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_NET") 'PASSIVE_NETval.ToString()
                                .Attributes("NETBOOKINGS").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS") ' NETBOOKINGSval.ToString()
                                .Attributes("AirBookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AirBookings") 'AirBookingsval.ToString()
                                .Attributes("CarBookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("CarBookings") 'CarBookingsval.ToString()
                                .Attributes("HotelBookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("HotelBookings") 'HotelBookingsval.ToString()

                                .Attributes("Passive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive") 'valHHotel_Netbookings.ToString()
                                .Attributes("WithPassive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive") 'valHHotel_Netbookings.ToString()


                            End If
                        End With
                        objOutputXmlExport.DocumentElement("DAILYBOOKINGS").AppendChild(objXmlNodeClone)

                        Dim intArry As New ArrayList()
                        Dim strArry As New ArrayList()

                        strArry.Add("Date")
                        intArry.Add(0)

                        strArry.Add("Total")
                        intArry.Add(9)

                        strArry.Add("Air Bookings")
                        intArry.Add(12)



                        If chkUseOriginalBooking.Checked = True Then
                            strArry.Add("Passive")
                            intArry.Add(10)
                            intArry.Add(11)

                            If chkUseOriginalBooking.Checked = True Then
                                strArry.Add("Total Air")
                            Else
                                strArry.Add("Total")
                            End If
                        End If

                        If chkHotel.Checked = True And chkCar.Checked = True Then
                            strArry.Add("Car Bookings")
                            strArry.Add("Hotel Bookings")
                            intArry.Add(13)
                            intArry.Add(14)

                        Else
                            If chkHotel.Checked = True Then
                                strArry.Add("Hotel Bookings")
                                intArry.Add(14)

                            End If

                            If chkCar.Checked = True Then
                                strArry.Add("Car Bookings")
                                intArry.Add(13)

                            End If
                        End If

                        'strArry.Add("Car Bookings")
                        'strArry.Add("Hotel Bookings")
                        strArry.Add("Airline Add Bookings")
                        intArry.Add(1)

                        strArry.Add("AirLine Cancel Bookings")
                        intArry.Add(2)

                        strArry.Add("AirLine Modify Bookings")
                        intArry.Add(3)

                        strArry.Add("AirLine Net Bookings")
                        intArry.Add(4)

                        strArry.Add("Passive Add")
                        intArry.Add(5)

                        strArry.Add("Passive Cancel")
                        intArry.Add(6)

                        strArry.Add("Passive Modify")
                        intArry.Add(7)

                        strArry.Add("Passive Net")
                        intArry.Add(8)

                        If chkAirBreakUp.Checked = True Then
                            strArry.Add("AirlineName")
                            intArry.Add(15)
                        End If


                        objExport.ExportDetails(objOutputXmlExport, "DAILYBOOKINGS/MotiveDetails", intArry, strArry, ExportExcel.ExportFormat.Excel, "DailyBooing.xls")

                        PagingCommon(objOutputXml)
                        Dim intcol As Integer = GetSortColumnIndexMukund(grdvBreakup)
                        If ViewState("Desc") = "FALSE" Then
                            grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                        End If
                        If ViewState("Desc") = "TRUE" Then
                            grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                        End If
                        Exit Sub
                    End If
                End If


                If chkAirBreakUp.Checked = True Then
                    ' If (chkAir.Checked = True And chkAirBreakUp.Checked = True And chkCar.Checked = False And chkHotel.Checked = False) Or (chkAir.Checked = True And chkAirBreakUp.Checked = False And chkCar.Checked = False And chkHotel.Checked = False) Then
                    objInputXml.DocumentElement.SelectSingleNode("AIRBREAKUP").InnerText = "TRUE"
                    objOutputXml = objbzDailyBooking.MotiveDetails_AirBreakup(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                        Dim objXmlReader As XmlNodeReader
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)




                        ViewState("PrevSearching") = objInputXml.OuterXml
                        '  ViewState("PrevSearching1") = Nothing
                        ds.ReadXml(objXmlReader)
                        'This dataSet contains Footer Value 
                        FooterDs = New DataSet()
                        FooterDs = ds



                        grdvAirwithArBreak.DataSource = ds.Tables("MotiveDetails")
                        grdvAirwithArBreak.DataBind()


                        'Code for Exporting Data
                        Dim objOutputXmlExport As New XmlDocument
                        Dim objXmlNode, objXmlNodeClone As XmlNode
                        objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                        objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DAILYBOOKINGS/MotiveDetails")
                        objXmlNodeClone = objXmlNode.CloneNode(True)

                        For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                            XmlAttr.Value = ""
                        Next

                        With objXmlNodeClone
                            If FooterDs IsNot Nothing Then
                                .Attributes("Date").Value = "Total"
                                .Attributes("Air_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Air_Netbookings") ' AIR_NET_BOOKINGSval.ToString()
                            End If
                        End With
                        objOutputXmlExport.DocumentElement("DAILYBOOKINGS").AppendChild(objXmlNodeClone)


                        'Creation of Array 






                        Dim intArry As New ArrayList()
                        Dim strArry As New ArrayList()

                        strArry.Add("Date")
                        intArry.Add(0)


                        strArry.Add("Air NetBookings")
                        intArry.Add(1)

                        If chkAirBreakUp.Checked = True Then
                            strArry.Add("AirLine Name")
                            intArry.Add(5)
                        End If
                        


                        

                        objExport.ExportDetails(objOutputXmlExport, "DAILYBOOKINGS/MotiveDetails", intArry, strArry, ExportExcel.ExportFormat.Excel, "DailyBooingDetails.xls")
                        'Code for Exporting Data



                        'objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS/MotiveDetails", ExportExcel.ExportFormat.Excel, "DailyBooing.xls")


                        PagingCommon(objOutputXml)

                        Dim intcol1 As Integer = GetSortColumnIndexMukund(grdvAirwithArBreak)

                        If ViewState("Desc") = "FALSE" Then
                            grdvAirwithArBreak.HeaderRow.Cells(intcol1).Controls.Add(imgUp)
                        End If
                        If ViewState("Desc") = "TRUE" Then
                            grdvAirwithArBreak.HeaderRow.Cells(intcol1).Controls.Add(imgDown)
                        End If

                        'End of showing List of Data

                    Else
                        'txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        'txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        'txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        'txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                        grdvAirwithArBreak.DataSource = String.Empty
                        grdvAirwithArBreak.DataBind()
                        pnlPaging.Visible = False
                        ddlPageNumber.Items.Clear()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                Else
                    objOutputXml = objbzDailyBooking.MotiveDetails(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value

                        Dim objXmlReader As XmlNodeReader
                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds.ReadXml(objXmlReader)


                        ViewState("PrevSearching") = objInputXml.OuterXml
                        ds.ReadXml(objXmlReader)
                        'This dataSet contains Footer Value 
                        FooterDs = New DataSet()
                        FooterDs = ds


                        grdvMotivDetails.DataSource = ds.Tables("MotiveDetails")
                        grdvMotivDetails.DataBind()





                       


                        ''Code for Exporting Data
                        Dim objOutputXmlExport As New XmlDocument
                        Dim objXmlNode, objXmlNodeClone As XmlNode
                        objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                        objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DAILYBOOKINGS/MotiveDetails")
                        objXmlNodeClone = objXmlNode.CloneNode(True)

                        For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                            XmlAttr.Value = ""
                        Next

                        With objXmlNodeClone
                            If FooterDs IsNot Nothing Then
                                .Attributes("Date").Value = "Total"
                                .Attributes("Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings") ' valNetbookings.ToString()
                                .Attributes("Air_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Air_Netbookings") ' valHAir_Netbookings.ToString()
                                .Attributes("Car_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Car_Netbookings") ' valHCar_Netbookings.ToString()
                                .Attributes("Hotel_Netbookings").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Hotel_Netbookings") 'valHHotel_Netbookings.ToString()
                                .Attributes("Passive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Passive") 'valHHotel_Netbookings.ToString()
                                .Attributes("WithPassive").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("WithPassive") 'valHHotel_Netbookings.ToString()


                            End If
                        End With
                        objOutputXmlExport.DocumentElement("DAILYBOOKINGS").AppendChild(objXmlNodeClone)






                        Dim intArry As New ArrayList()
                        Dim strArry As New ArrayList()

                        strArry.Add("Date")
                        intArry.Add(0)


                        strArry.Add("Total")
                        intArry.Add(1)

                        strArry.Add("Air Bookings")
                        intArry.Add(4)
                        '2

                        'strArry.Add("Passive")
                        'intArry.Add(3)

                        'strArry.Add("Total")
                        'intArry.Add(4)

                        'strArry.Add("Car NetBookings")
                        'intArry.Add(5)

                        'strArry.Add("Hotel NetBookings")
                        'intArry.Add(6)

                        If chkUseOriginalBooking.Checked = True Then
                            strArry.Add("Passive")
                            intArry.Add(2)
                            intArry.Add(3)

                            If chkUseOriginalBooking.Checked = True Then
                                strArry.Add("Total Air")
                            Else
                                strArry.Add("Total")
                            End If
                        End If


                        If chkHotel.Checked = True And chkCar.Checked = True Then

                            strArry.Add("Car NetBookings")
                            intArry.Add(5)

                            strArry.Add("Hotel NetBookings")
                            intArry.Add(6)

                        Else
                            If chkHotel.Checked = True Then
                                strArry.Add("Hotel NetBookings")
                                intArry.Add(6)
                            End If

                            If chkCar.Checked = True Then
                                strArry.Add("Car NetBookings")
                                intArry.Add(5)
                            End If
                        End If



                        



                        



                        objExport.ExportDetails(objOutputXmlExport, "DAILYBOOKINGS/MotiveDetails", intArry, strArry, ExportExcel.ExportFormat.Excel, "DailyBooing.xls")
                        'Code for Exporting Data



                        'objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS/MotiveDetails", ExportExcel.ExportFormat.CSV, "DailyBooing.csv")


                        Dim intcol1 As Integer = GetSortColumnIndexMukund(grdvMotivDetails)
                        PagingCommon(objOutputXml)

                        If ViewState("Desc") = "FALSE" Then
                            grdvMotivDetails.HeaderRow.Cells(intcol1).Controls.Add(imgUp)
                        End If
                        If ViewState("Desc") = "TRUE" Then
                            grdvMotivDetails.HeaderRow.Cells(intcol1).Controls.Add(imgDown)
                        End If

                    Else
                        'txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        'txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        'txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        'txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                        grdvMotivDetails.DataSource = String.Empty
                        grdvMotivDetails.DataBind()
                        pnlPaging.Visible = False
                        ddlPageNumber.Items.Clear()
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If



            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExportBrk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportBrk.Click
        Try
            Try

                Dim objInputXml, objOutputXml As New XmlDocument
                Dim ds As New DataSet
                Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
                objInputXml.LoadXml("<PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT><LCode></LCode><OriginalBookings></OriginalBookings><ShowGroup></ShowGroup><SDATE></SDATE><EDATE></EDATE><RESP_1A></RESP_1A><AIRBREAKUP></AIRBREAKUP><Limited_To_OwnAagency></Limited_To_OwnAagency><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_DAILYBOOKINGSDETAILS_INPUT>")

                lblError.Text = ""

                With objInputXml.DocumentElement
                    '
                    If Request.QueryString("Lcode") IsNot Nothing Then
                        .SelectSingleNode("LCode").InnerText = objED.Decrypt(Request.QueryString("Lcode").Trim())
                    End If
                    If chkUseOriginalBooking.Checked = True Then
                        .SelectSingleNode("OriginalBookings").InnerText = "True"
                    Else
                        .SelectSingleNode("OriginalBookings").InnerText = "False"
                    End If
                    If chkWholGrp.Checked = True Then
                        .SelectSingleNode("ShowGroup").InnerText = "True"
                    Else
                        .SelectSingleNode("ShowGroup").InnerText = "False"
                    End If
                    .SelectSingleNode("SDATE").InnerText = objEams.ConvertTextDate(txtdtFrom.Text.Trim())
                    .SelectSingleNode("EDATE").InnerText = objEams.ConvertTextDate(txtdtTo.Text.Trim())

                    If Session("LoginSession") Is Nothing Then
                        lblError.Text = "Re Login"
                        Exit Sub
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

                    'If chkBreakup.Checked = True Then
                    '    .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                    'Else
                    '    .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                    'End If

                    If chkAirBreakUp.Checked = True Then
                        .SelectSingleNode("AIRBREAKUP").InnerText = "True"
                    Else
                        .SelectSingleNode("AIRBREAKUP").InnerText = "False"
                    End If

                    

                    If ViewState("SortName") Is Nothing Then
                        ViewState("SortName") = "DATE"
                        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
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

                If chkBreakup.Checked = True Then
                    objOutputXml = objbzDailyBooking.MotiveDetails_Breakup(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Name").Value
                        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Address").Value
                        txtCity.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("City").Value
                        txtCountry.Text = objOutputXml.DocumentElement.SelectSingleNode("DAILYBOOKINGS").Attributes("Country").Value
                        Dim objXmlReader As XmlNodeReader
                        Dim ds1 As New DataSet




                        objXmlReader = New XmlNodeReader(objOutputXml)
                        ds1.ReadXml(objXmlReader)


                        ViewState("PrevSearching") = objInputXml.OuterXml
                        ds.ReadXml(objXmlReader)
                        'This dataSet contains Footer Value 
                        FooterDs = New DataSet()
                        FooterDs = ds1

                        grdvBreakup.DataSource = ds1.Tables("MotiveDetails")
                        grdvBreakup.DataBind()

                        '  objExport.ExportDetails(objOutputXml, "DAILYBOOKINGS/MotiveDetails", ExportExcel.ExportFormat.Excel, "DailyBooing.xls")

                        PagingCommon(objOutputXml)
                        Dim intcol As Integer = GetSortColumnIndexMukund(grdvBreakup)
                        If ViewState("Desc") = "FALSE" Then
                            grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                        End If
                        If ViewState("Desc") = "TRUE" Then
                            grdvBreakup.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                        End If







                        If ViewState("SortName1") Is Nothing Then
                            ViewState("SortName1") = "Date"
                            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Date" '"LOCATION_CODE"
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName1")
                        End If

                        If ViewState("Desc1") Is Nothing Then
                            ViewState("Desc1") = "FALSE"
                            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc1")
                        End If



                        Dim objCarXml As New XmlDocument
                        objCarXml = objbzDailyBooking.MotiveDetails_CarHotelBreakup(objInputXml)
                        If objCarXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            Dim objXmlR As XmlNodeReader
                            Dim dSet As New DataSet
                            objXmlR = New XmlNodeReader(objCarXml)
                            dSet.ReadXml(objXmlR)


                            ViewState("PrevSearching1") = objInputXml.OuterXml
                            'Code for Showing Total
                            FooterBreakup = New DataSet()
                            FooterBreakup = dSet

                            grdvCarBreakUp.DataSource = dSet.Tables("MotiveDetails")
                            grdvCarBreakUp.DataBind()










                            ''Code for Exporting Data
                            Dim objOutputXmlExport As New XmlDocument
                            Dim objXmlNode, objXmlNodeClone As XmlNode
                            objOutputXmlExport.LoadXml(objCarXml.OuterXml)
                            objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DAILYBOOKINGS/MotiveDetails")
                            objXmlNodeClone = objXmlNode.CloneNode(True)

                            For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                                XmlAttr.Value = ""
                            Next

                            With objXmlNodeClone
                                If FooterBreakup IsNot Nothing Then
                                    .Attributes("Date").Value = "Total"
                                    '.Attributes("TARGET").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGET").ToString() ' hdTARGET.Value
                                    '.Attributes("TARGETPERDAY").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("TARGETPERDAY").ToString() '  hdTARGETPERDAY.Value
                                    '.Attributes("Performance").Value = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Performance").ToString() 'hdPerformance.Value


                                    .Attributes("Car_Bookings").Value = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Bookings") ' AIR_ADD_BOOKINGSval.ToString()
                                    .Attributes("Hotel_Bookings").Value = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Hotel_Bookings") '  AIR_CANCEL_BOOKINGSval.ToString()
                                    .Attributes("Car_Voucher_Printed").Value = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Voucher_Printed") ' AIR_MODIFY_BOOKINGSval.ToString()
                                    .Attributes("Car_Number_of_Days").Value = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Car_Number_of_Days") '  AIR_NET_BOOKINGSval.ToString()
                                    .Attributes("Hotel_Nights").Value = FooterBreakup.Tables("PAGE_TOTAL").Rows(0)("Hotel_Nights") '  PASSIVE_ADDval.ToString()

                                End If
                            End With
                            objOutputXmlExport.DocumentElement("DAILYBOOKINGS").AppendChild(objXmlNodeClone)





                            Dim intArry As New ArrayList()
                            Dim strArry As New ArrayList()

                            strArry.Add("Date")
                            intArry.Add(0)


                            strArry.Add("Car Bookings")
                            intArry.Add(1)

                            strArry.Add("Hotel Bookings")
                            intArry.Add(2)


                            strArry.Add("Car Voucher Printed")
                            intArry.Add(3)

                            strArry.Add("Car Number of Days")
                            intArry.Add(4)

                            strArry.Add("Hotel Nights")
                            intArry.Add(5)



                            objExport.ExportDetails(objOutputXmlExport, "DAILYBOOKINGS/MotiveDetails", intArry, strArry, ExportExcel.ExportFormat.Excel, "DailyBooing.xls")


                            PagingCommonNew(objCarXml)
                            Dim intcol1 As Integer = GetSortColumnIndexMukundNew(grdvCarBreakUp)
                            If ViewState("Desc1") = "FALSE" Then
                                grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgUp1)
                            End If
                            If ViewState("Desc1") = "TRUE" Then
                                grdvCarBreakUp.HeaderRow.Cells(intcol1).Controls.Add(imgDown1)
                            End If
                        Else
                            Panel1.Visible = False
                            ddlPageNumber1.Items.Clear()
                        End If
                    Else
                        pnlPaging.Visible = False
                        ddlPageNumber.Items.Clear()
                    End If

                End If

            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
