Imports System.IO
Partial Class Productivity_PDSR_AirLineWiseMktShare
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objEams.ExpirePageCache()
            'If chkShowBr.Checked = False Then
            '    btnPrint.Attributes.Add("onclick", "return CallPrint('grdvMktShareDetails')")
            'Else
            '    btnPrint.Attributes.Add("onclick", "return CallPrint('grdvBreakUpOutPut')")
            'End If

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            'Code for Paging $ Sorting

            chkShowBr.Attributes.Add("onclick", "return chkShowBreakup();")

            '  btnSearch.Attributes.Add("onclick", "return SearchValidate();")

            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity')")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry')")
            drpAirLine.Attributes.Add("onkeyup", "return gotop('drpAirLine')")
            drpCarrierType.Attributes.Add("onkeyup", "return gotop('drpCarrierType')")
            drpOneAoffice.Attributes.Add("onkeyup", "return gotop('drpOneAoffice')")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")

            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                LoadAllControls()
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Wise Market Share']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Wise Market Share']").Attributes("Value").Value)
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
                strBuilder = objEams.SecurityCheck(31)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindGrid()

        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        'Dim objbzDailyBooking As New AAMS.bizProductivity.bzMIDT
        'objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><ShowBreakup></ShowBreakup><BreakupType></BreakupType></PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT>")

        'With objInputXml.DocumentElement

        '    If drpOneAoffice.SelectedIndex <> 0 Then
        '        .SelectSingleNode("Aoffice").InnerText = drpOneAoffice.SelectedValue.Trim()
        '    End If

        '    If drpCity.SelectedIndex <> 0 Then
        '        .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
        '    End If

        '    '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

        '    If drpCountry.SelectedIndex <> 0 Then
        '        .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
        '    End If

        '    If drpRegion.SelectedIndex <> 0 Then
        '        .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
        '    End If

        '    'If chkShowBr.Checked = True Then
        '    '    .SelectSingleNode("ShowGroup").InnerText = "True"
        '    'Else
        '    '    .SelectSingleNode("ShowGroup").InnerText = "False"
        '    'End If
        '    'If drpAirLine.SelectedIndex <> 0 Then
        '    .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue.Trim()
        '    ' End If

        '    .SelectSingleNode("ONLINE_CARRIER").InnerText = drpCarrierType.SelectedValue.Trim()

        '    .SelectSingleNode("SMonth").InnerText = drpMonthF.SelectedIndex + 1

        '    .SelectSingleNode("SYear").InnerText = drpYearF.SelectedItem.Text.Trim()

        '    .SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedIndex + 1

        '    .SelectSingleNode("EYear").InnerText = drpYearTo.SelectedItem.Text.Trim()

        '    If chkShowBr.Checked = True Then
        '        .SelectSingleNode("ShowBreakup").InnerText = "True"
        '        If rdShobrBookings.SelectedValue = "1" Then
        '            .SelectSingleNode("BreakupType").InnerText = "Total"
        '        Else
        '            .SelectSingleNode("BreakupType").InnerText = "Average"
        '        End If
        '    Else
        '        .SelectSingleNode("ShowBreakup").InnerText = "False"
        '    End If

        '    'Following Statement is written for Limited to own Agency
        '    Dim xDoc As New XmlDocument
        '    xDoc.LoadXml(Session("Security"))

        '    '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

        '    If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
        '        .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
        '    End If
        'End With

        'objOutputXml = objbzDailyBooking.AirLineWiseMarketShare(objInputXml)

        ''<PR_SEARCH_AIRLINEWISEMARKETSHARE_OUTPUT>

        '' <AIRLINEWISEMARKETSHARE AIRLINE_CODE='' AirlineName='' AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS=''/>

        '' <AIRLINEWISEMARKETSHARE_TOTAL AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS='' />

        '' <Errors Status=''>

        '' <Error Code='' Description='' />

        '' </Errors>


        ''</PR_SEARCH_AIRLINEPASSIVE_OUTPUT>
        'If chkShowBr.Checked = False Then
        '    With objOutputXml.DocumentElement.SelectSingleNode("AIRLINEWISEMARKETSHARE_TOTAL")
        '        hdAMADEUS.Value = .Attributes("AMADEUS").Value.Trim()
        '        hdABACUS.Value = .Attributes("ABACUS").Value.Trim()
        '        hdGALILEO.Value = .Attributes("GALILEO").Value.Trim()
        '        hdWORLDSPAN.Value = .Attributes("WORLDSPAN").Value.Trim()
        '        hdSABREDOMESTIC.Value = .Attributes("SABREDOMESTIC").Value.Trim()
        '        hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
        '    End With
        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        objXmlReader = New XmlNodeReader(objOutputXml)
        '        ds.ReadXml(objXmlReader)
        '        lblError.Text = ""
        '        grdvMktShareDetails.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
        '        grdvMktShareDetails.DataBind()
        '    Else
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    End If
        'Else
        '    With objOutputXml.DocumentElement.SelectSingleNode("AIRLINEWISEMARKETSHARE_TOTAL")
        '        'BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' 
        '        hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
        '        hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
        '        hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
        '        hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
        '        hdLATE.Value = .Attributes("LATE").Value.Trim()
        '        hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
        '        hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
        '        hdNETBOOKINGSBr.Value = .Attributes("NETBOOKINGS").Value.Trim()
        '    End With

        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        objXmlReader = New XmlNodeReader(objOutputXml)
        '        ds.ReadXml(objXmlReader)
        '        lblError.Text = ""
        '        grdvBreakUpOutPut.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
        '        grdvBreakUpOutPut.DataBind()
        '    Else
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    End If

        'End If
       

    End Sub
    Private Sub BindGrid()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzMIDT
        objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><ShowBreakup></ShowBreakup><BreakupType></BreakupType> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><GroupTypeID/></PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT>")

        With objInputXml.DocumentElement

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("Aoffice").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If

            '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
            End If

            'If chkShowBr.Checked = True Then
            '    .SelectSingleNode("ShowGroup").InnerText = "True"
            'Else
            '    .SelectSingleNode("ShowGroup").InnerText = "False"
            'End If
            'If drpAirLine.SelectedIndex <> 0 Then
            .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue.Trim()
            ' End If

            .SelectSingleNode("ONLINE_CARRIER").InnerText = drpCarrierType.SelectedValue.Trim()

            .SelectSingleNode("SMonth").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("SYear").InnerText = drpYearF.SelectedItem.Text.Trim()

            .SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedIndex + 1

            .SelectSingleNode("EYear").InnerText = drpYearTo.SelectedItem.Text.Trim()

            If chkShowBr.Checked = True Then
                .SelectSingleNode("ShowBreakup").InnerText = "True"
                If rdShobrBookings.SelectedValue = "1" Then
                    .SelectSingleNode("BreakupType").InnerText = "Total"
                Else
                    .SelectSingleNode("BreakupType").InnerText = "Average"
                End If
            Else
                .SelectSingleNode("ShowBreakup").InnerText = "False"
            End If

            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If

            If drpLstGroupType.SelectedIndex <> 0 Then
                .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If
        End With





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
            ViewState("SortName") = "AirlineName"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AirlineName" '"LOCATION_CODE"
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


        If ViewState("PrevSearching") IsNot Nothing Then
            Dim objXml1 As New XmlDocument
            objXml1.LoadXml(ViewState("PrevSearching"))
            Dim objNodes As XmlNodeList = objXml1.DocumentElement.ChildNodes
            If objXml1.OuterXml <> objInputXml.OuterXml Then
                If objXml1.DocumentElement.SelectSingleNode("ShowBreakup").InnerText <> objInputXml.DocumentElement.SelectSingleNode("ShowBreakup").InnerText Then
                    ViewState("SortName") = "AirlineName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AirlineName"
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                End If
            End If
        End If




        objOutputXml = objbzDailyBooking.AirLineWiseMarketShare(objInputXml)


        ' objOutputXml.Save("C:\WriteX\AirLineWiseMarketShare.xml")

        '<PR_SEARCH_AIRLINEWISEMARKETSHARE_OUTPUT>

        ' <AIRLINEWISEMARKETSHARE AIRLINE_CODE='' AirlineName='' AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS=''/>

        ' <AIRLINEWISEMARKETSHARE_TOTAL AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS='' />

        ' <Errors Status=''>

        ' <Error Code='' Description='' />

        ' </Errors>


        '</PR_SEARCH_AIRLINEPASSIVE_OUTPUT>
        If chkShowBr.Checked = False Then
            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                hdAMADEUS.Value = .Attributes("AMADEUS").Value.Trim()
                hdABACUS.Value = .Attributes("ABACUS").Value.Trim()
                hdGALILEO.Value = .Attributes("GALILEO").Value.Trim()
                hdWORLDSPAN.Value = .Attributes("WORLDSPAN").Value.Trim()
                hdSABREDOMESTIC.Value = .Attributes("SABREDOMESTIC").Value.Trim()
                hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
            End With
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvMktShareDetails.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
                grdvMktShareDetails.DataBind()

                PagingCommon(objOutputXml)

                Dim intcol As Integer = GetSortColumnIndex(grdvMktShareDetails)
                If ViewState("Desc") = "FALSE" Then
                    grdvMktShareDetails.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvMktShareDetails.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Else
            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                'BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' 
                hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
                hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
                hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
                hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
                hdLATE.Value = .Attributes("LATE").Value.Trim()
                hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
                hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
                hdNETBOOKINGSBr.Value = .Attributes("NETBOOKINGS").Value.Trim()
            End With

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvBreakUpOutPut.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
                grdvBreakUpOutPut.DataBind()


                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvBreakUpOutPut)
                If ViewState("Desc") = "FALSE" Then
                    grdvBreakUpOutPut.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvBreakUpOutPut.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        End If
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "TRUE" Then
            pnlPaging.Visible = False
        Else
            pnlPaging.Visible = True
        End If

    End Sub



    Private Sub LoadAllControls()
        Try
            objEams.BindDropDown(drpAirLine, "AIRLINE", False, 3)
            objEams.BindDropDown(drpCity, "CITY", False, 3)
            ' drpCity.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpCountry, "COUNTRY", False, 3)
            ' drpCountry.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpOneAoffice, "AOFFICE", False, 3)
            'drpOneAoffice.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            ' drpRegion.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)

            Dim dtYear As New DateTime
            Dim counter As Integer
            For counter = DateTime.Now.Year To 1990 Step -1
                drpYearF.Items.Add(counter.ToString())
                drpYearTo.Items.Add(counter.ToString())
            Next
            For counter = 0 To 11
                drpMonthF.Items.Add(MonthName(counter + 1))
                drpMonthTo.Items.Add(MonthName(counter + 1))
            Next
            drpMonthF.SelectedIndex = 0
            drpMonthTo.SelectedIndex = 11
            drpYearF.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year


            Dim objSecurityXml As New XmlDocument

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpOneAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpOneAoffice.SelectedValue = li.Value

                            End If
                        End If
                        drpOneAoffice.Enabled = False
                    End If
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMktShareDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMktShareDetails.RowDataBound


        '@ Code Added By abhishek
        Dim objSecurityXml As New XmlDocument
        dim strBuilder as New StringBuilder 
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lnkDetails As System.Web.UI.HtmlControls.HtmlAnchor
                Dim LnkGraph As System.Web.UI.HtmlControls.HtmlAnchor
                Dim LimAoff, LimReg, LimOwnOff, BreakupType As String
                Dim hdACode As New HiddenField

                lnkDetails = e.Row.FindControl("lnkDetails")
                LnkGraph = e.Row.FindControl("LnkGraph")
                hdACode = CType(e.Row.FindControl("hdACode"), HiddenField)

                LimAoff = ""
                LimReg = ""
                LimOwnOff = ""
                BreakupType = ""

                Dim strMonthFrom As String = drpMonthF.SelectedIndex + 1
                Dim strMonthTo As String = drpMonthTo.SelectedIndex + 1
                Dim strYearF As String = drpYearF.SelectedItem.Text.Trim()
                Dim strYearTo As String = drpYearTo.SelectedItem.Text.Trim()



                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
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
                    Else
                        LimOwnOff = "0"
                    End If
                End If


                ' " Previous code
                'lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "','" + BreakupType + "');")
                'LnkGraph.Attributes.Add("OnClick", "javascript:return GraphFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "');")

                '@ added on 11/02/10

                '  Added  on 11/02/10
                Dim Aoff, City, Country, Region, OnCarr, GType As String
                Aoff = ""
                City = ""
                Country = ""
                Region = ""
                OnCarr = ""
                GType = ""

                If drpOneAoffice.SelectedIndex <> 0 Then
                    Aoff = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpCity.SelectedIndex <> 0 Then
                    City = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    Country = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    Region = drpRegion.SelectedValue.Trim()
                End If

                'If drpCarrierType.SelectedIndex <> 0 Then
                OnCarr = drpCarrierType.SelectedValue.Trim()
                'End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    GType = drpLstGroupType.SelectedValue.Trim()
                End If

                '  Added  on 11/02/10

                lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "','" + BreakupType + "','" + City + "','" + Country + "','" + Region + "','" + Aoff + "','" + OnCarr + "','" + GType + "');")
                LnkGraph.Attributes.Add("OnClick", "javascript:return GraphFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "','" + BreakupType + "','" + City + "','" + Country + "','" + Region + "','" + Aoff + "','" + OnCarr + "','" + GType + "');")






                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                '        strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                '        If strBuilder(0) = "0" Then
                '            lnkDetails.Disabled = True
                '        Else
                '            lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                '        End If
                '    Else
                '        lnkDetails.Disabled = True
                '    End If
                'Else
                '    strBuilder = objEams.SecurityCheck(31)
                '    lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                'End If

            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try



        If e.Row.RowType = DataControlRowType.Footer Then
            '  e.Row.HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Text = "Total"
            e.Row.Cells(2).Text = hdAMADEUS.Value.Trim()
            e.Row.Cells(3).Text = hdABACUS.Value.Trim()
            e.Row.Cells(4).Text = hdGALILEO.Value.Trim()
            e.Row.Cells(5).Text = hdWORLDSPAN.Value.Trim()
            e.Row.Cells(6).Text = hdSABREDOMESTIC.Value.Trim()
            e.Row.Cells(7).Text = hdNETBOOKINGS.Value.Trim()
            '  e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objExcelExp As New ExportExcel
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzMIDT
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><ShowBreakup></ShowBreakup><BreakupType></BreakupType> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT>")

            With objInputXml.DocumentElement

                If drpOneAoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If

                '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If

                'If chkShowBr.Checked = True Then
                '    .SelectSingleNode("ShowGroup").InnerText = "True"
                'Else
                '    .SelectSingleNode("ShowGroup").InnerText = "False"
                'End If
                'If drpAirLine.SelectedIndex <> 0 Then
                .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue.Trim()
                ' End If

                .SelectSingleNode("ONLINE_CARRIER").InnerText = drpCarrierType.SelectedValue.Trim()

                .SelectSingleNode("SMonth").InnerText = drpMonthF.SelectedIndex + 1

                .SelectSingleNode("SYear").InnerText = drpYearF.SelectedItem.Text.Trim()

                .SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedIndex + 1

                .SelectSingleNode("EYear").InnerText = drpYearTo.SelectedItem.Text.Trim()

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                If chkShowBr.Checked = True Then
                    .SelectSingleNode("ShowBreakup").InnerText = "True"
                    If rdShobrBookings.SelectedValue = "1" Then
                        .SelectSingleNode("BreakupType").InnerText = "Total"
                    Else
                        .SelectSingleNode("BreakupType").InnerText = "Average"
                    End If
                Else
                    .SelectSingleNode("ShowBreakup").InnerText = "False"
                End If

                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If
            End With

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "AirlineName"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AirlineName" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else

                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If



            objOutputXml = objbzDailyBooking.AirLineWiseMarketShare(objInputXml)





            'Showing Page Total at the bottom of Excel Sheet

            'if chkShowBr.Checked=True then

            'AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS=''
            'else
            'BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                Dim objXmlReader As XmlNodeReader

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("AIRLINEWISEMARKETSHARE")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                With objXmlNodeClone

                    If ds IsNot Nothing Then
                        If chkShowBr.Checked = False Then
                            .Attributes("AirlineName").Value = "Total "
                            .Attributes("AMADEUS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("AMADEUS").ToString()
                            .Attributes("ABACUS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("ABACUS").ToString() 'hdTARGETPERDAY.Value
                            .Attributes("GALILEO").Value = ds.Tables("PAGE_TOTAL").Rows(0)("GALILEO").ToString() 'hdAverageBookings.Value.Trim()
                            .Attributes("WORLDSPAN").Value = ds.Tables("PAGE_TOTAL").Rows(0)("WORLDSPAN").ToString() ' hdNetbookings.Value
                            .Attributes("SABREDOMESTIC").Value = ds.Tables("PAGE_TOTAL").Rows(0)("SABREDOMESTIC").ToString() 'hdCar_Netbookings.Value
                            .Attributes("NETBOOKINGS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS").ToString() ' hdHotel_Netbookings.Value
                            '  .Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString() ' hdHotel_Netbookings.Value
                        Else
                            .Attributes("AirlineName").Value = "Total "
                            .Attributes("BOOKINGACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGACTIVE").ToString()
                            .Attributes("CANCELACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELACTIVE").ToString()
                            .Attributes("BOOKINGPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGPASSIVE").ToString()
                            .Attributes("CANCELPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELPASSIVE").ToString()
                            .Attributes("LATE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("LATE").ToString()
                            .Attributes("NULLACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLACTIVE").ToString()
                            .Attributes("NULLPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLPASSIVE").ToString()
                            .Attributes("NETBOOKINGS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS").ToString()
                        End If

                    End If
                End With

                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                objExcelExp.ExportDetails(objOutputXmlExport, "AIRLINEWISEMARKETSHARE", ExportExcel.ExportFormat.Excel, "exportREport.xls")

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If





            'Code for Old way to  export

            'If chkShowBr.Checked = False Then
            '    ' PrepareGridViewForExport(grdvMktShareDetails)
            '    grdvMktShareDetails.AllowSorting = False
            '    grdvMktShareDetails.HeaderStyle.ForeColor = Drawing.Color.Black
            '    BindGridExport()
            '    ExportGridView(grdvMktShareDetails)
            'Else
            '    grdvBreakUpOutPut.AllowSorting = False
            '    grdvBreakUpOutPut.HeaderStyle.ForeColor = Drawing.Color.Black
            '    ' PrepareGridViewForExport(grdvBreakUpOutPut)
            'BindGridExport()
            '    ExportGridView(grdvBreakUpOutPut)
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

    Protected Sub grdvBreakUpOutPut_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvBreakUpOutPut.RowDataBound

        '@ Code Added By abhishek
        Dim objSecurityXml As New XmlDocument
        Dim strBuilder As New StringBuilder
        Try

            If chkShowBr.Checked = True Then
                If rdShobrBookings.SelectedValue = "2" Then
                    e.Row.Cells(11).Visible = False
                End If
            End If


            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lnkDetails As System.Web.UI.HtmlControls.HtmlAnchor
                Dim LnkGraph As System.Web.UI.HtmlControls.HtmlAnchor
                Dim LimAoff, LimReg, LimOwnOff, BreakupType As String
                Dim hdACode As New HiddenField

                lnkDetails = e.Row.FindControl("lnkDetails")

                hdACode = CType(e.Row.FindControl("hdACode"), HiddenField)

                LimAoff = ""
                LimReg = ""
                LimOwnOff = ""

                Dim strMonthFrom As String = drpMonthF.SelectedIndex + 1
                Dim strMonthTo As String = drpMonthTo.SelectedIndex + 1
                Dim strYearF As String = drpYearF.SelectedItem.Text.Trim()
                Dim strYearTo As String = drpYearTo.SelectedItem.Text.Trim()



                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
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
                    Else
                        LimOwnOff = "0"
                    End If
                End If

                BreakupType = ""

                If chkShowBr.Checked = True Then
                    '  .SelectSingleNode("ShowBreakup").InnerText = "True"
                    If rdShobrBookings.SelectedValue = "1" Then
                        BreakupType = "Total"
                    Else
                        BreakupType = "Average"
                    End If
                Else
                    '.SelectSingleNode("ShowBreakup").InnerText = "False"
                End If



                '  Added  on 11/02/10
                Dim Aoff, City, Country, Region, OnCarr, GType As String
                Aoff = ""
                City = ""
                Country = ""
                Region = ""
                OnCarr = ""
                GType = ""

                If drpOneAoffice.SelectedIndex <> 0 Then
                    Aoff = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpCity.SelectedIndex <> 0 Then
                    City = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    Country = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    Region = drpRegion.SelectedValue.Trim()
                End If

                'If drpCarrierType.SelectedIndex <> 0 Then
                OnCarr = drpCarrierType.SelectedValue.Trim()
                'End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    GType = drpLstGroupType.SelectedValue.Trim()
                End If

                '  Added  on 11/02/10




                If lnkDetails IsNot Nothing Then
                    '   lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "','" + BreakupType + "');")

                    lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + hdACode.Value.Trim.ToString + "','" + BreakupType + "','" + City + "','" + Country + "','" + Region + "','" + Aoff + "','" + OnCarr + "','" + GType + "');")

                End If



                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                '        strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                '        If strBuilder(0) = "0" Then
                '            lnkDetails.Disabled = True
                '        Else
                '            lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                '        End If
                '    Else
                '        lnkDetails.Disabled = True
                '    End If
                'Else
                '    strBuilder = objEams.SecurityCheck(31)
                '    lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                'End If

            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try


        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(1).Text = "Total"
                e.Row.Cells(3).Text = hdBOOKINGACTIVE.Value.Trim()
                e.Row.Cells(4).Text = hdCANCELACTIVE.Value.Trim()
                e.Row.Cells(5).Text = hdBOOKINGPASSIVE.Value.Trim()
                e.Row.Cells(6).Text = hdCANCELPASSIVE.Value.Trim()
                e.Row.Cells(7).Text = hdLATE.Value.Trim()
                e.Row.Cells(8).Text = hdNULLACTIVE.Value.Trim()
                e.Row.Cells(9).Text = hdNULLPASSIVE.Value.Trim()
                e.Row.Cells(10).Text = hdNETBOOKINGSBr.Value.Trim()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("MTSR_AirLineWiseMktShare.aspx")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ' ViewState("Desc") = "FALSE"
            ' @ AIRLINE_CODE, AirlineName,CRS, ADDRESS,CITY, CRSCODETEXT
            If SortName.Trim().ToUpper = "AIRLINE_CODE" Or SortName.Trim().ToUpper = "AIRLINENAME" Or SortName.Trim().ToUpper = "CRS" Then
                ViewState("Desc") = "FALSE"
            Else
                ViewState("Desc") = "TRUE"
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
                ' @ AIRLINE_CODE, AirlineName,CRS, ADDRESS,CITY, CRSCODETEXT
                If SortName.Trim().ToUpper = "AIRLINE_CODE" Or SortName.Trim().ToUpper = "AIRLINENAME" Or SortName.Trim().ToUpper = "CRS" Then
                    ViewState("Desc") = "FALSE"
                Else
                    ViewState("Desc") = "TRUE"
                End If
                '@ End of Added Code For Default descending sorting order on first time  of following Fields

            End If
        End If
        BindGrid()
    End Sub
#End Region
    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
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

    Protected Sub grdvMktShareDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMktShareDetails.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvBreakUpOutPut_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvBreakUpOutPut.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGridExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzMIDT
        objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><ONLINE_CARRIER></ONLINE_CARRIER><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><ShowBreakup></ShowBreakup><BreakupType></BreakupType> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_AIRLINEWISEMARKETSHARE_INPUT>")

        With objInputXml.DocumentElement

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("Aoffice").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If

            '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
            End If

            'If chkShowBr.Checked = True Then
            '    .SelectSingleNode("ShowGroup").InnerText = "True"
            'Else
            '    .SelectSingleNode("ShowGroup").InnerText = "False"
            'End If
            'If drpAirLine.SelectedIndex <> 0 Then
            .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLine.SelectedValue.Trim()
            ' End If

            .SelectSingleNode("ONLINE_CARRIER").InnerText = drpCarrierType.SelectedValue.Trim()

            .SelectSingleNode("SMonth").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("SYear").InnerText = drpYearF.SelectedItem.Text.Trim()

            .SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedIndex + 1

            .SelectSingleNode("EYear").InnerText = drpYearTo.SelectedItem.Text.Trim()

            If chkShowBr.Checked = True Then
                .SelectSingleNode("ShowBreakup").InnerText = "True"
                If rdShobrBookings.SelectedValue = "1" Then
                    .SelectSingleNode("BreakupType").InnerText = "Total"
                Else
                    .SelectSingleNode("BreakupType").InnerText = "Average"
                End If
            Else
                .SelectSingleNode("ShowBreakup").InnerText = "False"
            End If

            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If
        End With

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "AirlineName"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AirlineName" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else

            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If



        objOutputXml = objbzDailyBooking.AirLineWiseMarketShare(objInputXml)

        '<PR_SEARCH_AIRLINEWISEMARKETSHARE_OUTPUT>

        ' <AIRLINEWISEMARKETSHARE AIRLINE_CODE='' AirlineName='' AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS=''/>

        ' <AIRLINEWISEMARKETSHARE_TOTAL AMADEUS='' ABACUS='' GALILEO='' WORLDSPAN='' SABREDOMESTIC='' NETBOOKINGS='' />

        ' <Errors Status=''>

        ' <Error Code='' Description='' />

        ' </Errors>


        '</PR_SEARCH_AIRLINEPASSIVE_OUTPUT>
        If chkShowBr.Checked = False Then
            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                hdAMADEUS.Value = .Attributes("AMADEUS").Value.Trim()
                hdABACUS.Value = .Attributes("ABACUS").Value.Trim()
                hdGALILEO.Value = .Attributes("GALILEO").Value.Trim()
                hdWORLDSPAN.Value = .Attributes("WORLDSPAN").Value.Trim()
                hdSABREDOMESTIC.Value = .Attributes("SABREDOMESTIC").Value.Trim()
                hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
            End With
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvMktShareDetails.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
                grdvMktShareDetails.DataBind()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Else
            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                'BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' 
                hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
                hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
                hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
                hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
                hdLATE.Value = .Attributes("LATE").Value.Trim()
                hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
                hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
                hdNETBOOKINGSBr.Value = .Attributes("NETBOOKINGS").Value.Trim()
            End With

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvBreakUpOutPut.DataSource = ds.Tables("AIRLINEWISEMARKETSHARE")
                grdvBreakUpOutPut.DataBind()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        End If

    End Sub


End Class
