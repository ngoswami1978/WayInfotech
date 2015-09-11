Imports System.IO
Imports System.Xml
Partial Class Market_MTSR_MarketShare
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objEams.ExpirePageCache()

            'btnPrint.Attributes.Add("onclick", "return CallPrint('grdvMarketShare')")
            ' rdSummaryOption.Attributes.Add("onclick", "return showHideBreakup()")

            'drpAirLineName(drpCity)  drpCountry  drpOneAoffice  drpRegion
            btnSearch.Attributes.Add("onclick", "return SearchValidate();")
            btnExport.Attributes.Add("onclick", "return SearchValidate();")
            BtnGraph.Attributes.Add("onclick", "return SearchValidate();")

            drpAirLineName.Attributes.Add("onkeyup", "return gotop('drpAirLineName')")
            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity')")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry')")
            drpOneAoffice.Attributes.Add("onkeyup", "return gotop('drpOneAoffice')")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")

            '   chkShowBr.Attributes.Add("onclick", "return chkShowBreakup();")

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            'Code for Paging $ Sorting


            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())

                'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objEams.CheckSession())
                Exit Sub
            End If


            If Not Page.IsPostBack Then
                LoadAllControls()
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share-Airline']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share-Airline']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
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
        hdBOOKINGACTIVE.Value = ""
        hdCANCELACTIVE.Value = ""
        hdBOOKINGPASSIVE.Value = ""
        hdCANCELPASSIVE.Value = ""
        hdLATE.Value = ""
        hdNULLACTIVE.Value = ""
        hdNULLPASSIVE.Value = ""
        hdNETBOOKINGS.Value = ""
        hdA.Value = ""
        hdB.Value = ""
        hdG.Value = ""
        hdP.Value = ""
        hdTotal.Value = ""
        hdW.Value = ""

        BindGrid()
        'grdvMarketShare.DataSource = String.Empty
        'grdvMarketShare.DataBind()

        'grdvMktShareBrResult.DataSource = String.Empty
        'grdvMktShareBrResult.DataBind()

        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        'Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
        ''<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
        ''</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
        ''<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
        ''<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
        ''<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
        'objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><SHOWBREAKUP></SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>")

        'With objInputXml.DocumentElement

        '    .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

        '    .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

        '    .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

        '    .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

        '    'If drpAirLineName.SelectedIndex <> 0 Then
        '    .SelectSingleNode("AIRLINECODE").InnerText = drpAirLineName.SelectedValue.Trim()
        '    ' End If

        '    If drpCity.SelectedIndex <> 0 Then
        '        .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
        '    End If
        '    If drpCountry.SelectedIndex <> 0 Then
        '        .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
        '    End If

        '    If drpOneAoffice.SelectedIndex <> 0 Then
        '        .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
        '    End If

        '    If drpRegion.SelectedIndex <> 0 Then
        '        .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
        '    End If

        '  .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

        '    If rdSummaryOption.SelectedValue.Trim() <> "6" Then
        '        If chkShowBr.Checked = True Then
        '            .SelectSingleNode("SHOWBREAKUP").InnerText = "1"
        '        Else
        '            .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
        '        End If
        '    End If

        '    'Following Statement is written for Limited to own Agency
        '    Dim xDoc As New XmlDocument
        '    xDoc.LoadXml(Session("Security"))

        '    Dim str As String()
        '    str = Session("LoginSession").ToString().Split("|")
        '    .SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)

        '    If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
        '        .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
        '    End If
        'End With

        'objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)

        ''<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' A='' B='' G='' P='' W='' TOTAL=''/>
        ''<TOTAL A='' B='' G='' P='' W='' TOTAL=''/><Errors Status=''><Error Code='' Description=''/></Errors>
        ''</PR_SEARCHAIRMARKETSHARE_OUTPUT>

        ''<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' CRSCODETEXT='' BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' />
        ''<TOTAL BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS=''/><Errors Status=''>
        ''<Error Code='' Description=''/></Errors>
        ''</PR_SEARCHAIRMARKETSHARE_OUTPUT>


        'If chkShowBr.Checked = False Then
        '    With objOutputXml.DocumentElement.SelectSingleNode("TOTAL")
        '        hdA.Value = .Attributes("A").Value.Trim()
        '        hdB.Value = .Attributes("B").Value.Trim()
        '        hdG.Value = .Attributes("G").Value.Trim()
        '        hdP.Value = .Attributes("P").Value.Trim()
        '        hdTotal.Value = .Attributes("TOTAL").Value.Trim()
        '        hdW.Value = .Attributes("W").Value.Trim()
        '    End With
        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        objXmlReader = New XmlNodeReader(objOutputXml)
        '        ds.ReadXml(objXmlReader)
        '        lblError.Text = ""
        '        grdvMarketShare.DataSource = ds.Tables("DETAIL")
        '        grdvMarketShare.DataBind()
        '    Else
        '        grdvMarketShare.DataSource = String.Empty
        '        grdvMarketShare.DataBind()
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    End If
        'Else

        '    With objOutputXml.DocumentElement.SelectSingleNode("TOTAL")
        '        hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
        '        hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
        '        hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
        '        hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
        '        hdLATE.Value = .Attributes("LATE").Value.Trim()
        '        hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
        '        hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
        '        hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
        '    End With
        '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '        objXmlReader = New XmlNodeReader(objOutputXml)
        '        ds.ReadXml(objXmlReader)
        '        lblError.Text = ""
        '        grdvMktShareBrResult.DataSource = ds.Tables("DETAIL")
        '        grdvMktShareBrResult.DataBind()
        '    Else
        '        grdvMktShareBrResult.DataSource = String.Empty
        '        grdvMktShareBrResult.DataBind()
        '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        '    End If
        'End If

        
    End Sub
    Private Sub BindGrid()

        grdvMarketShare.DataSource = String.Empty
        grdvMarketShare.DataBind()

        grdvMktShareBrResult.DataSource = String.Empty
        grdvMktShareBrResult.DataBind()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
        '<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
        '</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
        '<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
        '<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
        '<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
        objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><GroupTypeID></GroupTypeID><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><LCODE></LCODE><SHOWBREAKUP></SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION>  <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHAIRMARKETSHARE_INPUT>")

        With objInputXml.DocumentElement

            If DlstCompVertical.SelectedValue <> "" Then
                .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If

            .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

            .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

            .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

            'If drpAirLineName.SelectedIndex <> 0 Then
            .SelectSingleNode("AIRLINECODE").InnerText = drpAirLineName.SelectedValue.Trim()
            ' End If

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If
            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
            End If

            .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

            ' If rdSummaryOption.SelectedValue.Trim() <> "6" Then
            If chkShowBr.Checked = True Then
                .SelectSingleNode("SHOWBREAKUP").InnerText = "1"
            Else
                .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
            End If
            ' End If


            If drpLstGroupType.SelectedIndex <> 0 Then
                .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            'Following Statement is written for Limited to own Agency
            If Session("Security") IsNot Nothing Then
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)
                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If
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
            ViewState("SortName") = "SELECTBY"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
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
        '  If objbzDailyBooking.Search(objInputXml) IsNot Nothing Then

        If ViewState("PrevSearching") IsNot Nothing Then
            Dim objXml1 As New XmlDocument
            objXml1.LoadXml(ViewState("PrevSearching"))
            Dim objNodes As XmlNodeList = objXml1.DocumentElement.ChildNodes
            If objXml1.OuterXml <> objInputXml.OuterXml Then
                If objXml1.DocumentElement.SelectSingleNode("SHOWBREAKUP").InnerText <> objInputXml.DocumentElement.SelectSingleNode("SHOWBREAKUP").InnerText Then
                    ViewState("SortName") = "SELECTBY"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                End If
                If objXml1.DocumentElement.SelectSingleNode("SELECTBY").InnerText <> objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText Then
                    ViewState("SortName") = "SELECTBY"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                End If

            End If
        End If




        objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)

        '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' A='' B='' G='' P='' W='' TOTAL=''/>
        '<TOTAL A='' B='' G='' P='' W='' TOTAL=''/><Errors Status=''><Error Code='' Description=''/></Errors>
        '</PR_SEARCHAIRMARKETSHARE_OUTPUT>

        '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' CRSCODETEXT='' BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' />
        '<TOTAL BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS=''/><Errors Status=''>
        '<Error Code='' Description=''/></Errors>
        '</PR_SEARCHAIRMARKETSHARE_OUTPUT>


        If chkShowBr.Checked = False Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdA.Value = .Attributes("A").Value.Trim()
                    hdB.Value = .Attributes("B").Value.Trim()
                    hdG.Value = .Attributes("G").Value.Trim()
                    hdP.Value = .Attributes("P").Value.Trim()
                    hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                    hdW.Value = .Attributes("W").Value.Trim()

                End With
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvMarketShare.DataSource = ds.Tables("DETAIL")
                grdvMarketShare.DataBind()

                pnlPaging.Visible = True

                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvMarketShare)
                If ViewState("Desc") = "FALSE" Then
                    grdvMarketShare.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvMarketShare.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            Else
                grdvMarketShare.DataSource = String.Empty
                grdvMarketShare.DataBind()
                grdvMktShareBrResult.DataSource = String.Empty
                grdvMktShareBrResult.DataBind()

                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Else
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ' If rdSummaryOption.SelectedValue <> "6" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
                    hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
                    hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
                    hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
                    hdLATE.Value = .Attributes("LATE").Value.Trim()
                    hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
                    hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
                    hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
                End With
                'End If
                'If rdSummaryOption.SelectedValue = "6" Then
                '    With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                '        hdA.Value = .Attributes("A").Value.Trim()
                '        hdB.Value = .Attributes("B").Value.Trim()
                '        hdG.Value = .Attributes("G").Value.Trim()
                '        hdP.Value = .Attributes("P").Value.Trim()
                '        hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                '        hdW.Value = .Attributes("W").Value.Trim()

                '    End With
                'End If
        End If
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
                ' If rdSummaryOption.SelectedValue <> "6" Then
                If chkShowBr.Checked = True Then

                    grdvMktShareBrResult.DataSource = ds.Tables("DETAIL")
                    grdvMktShareBrResult.DataBind()
                    ViewState("PrevSearching") = objInputXml.OuterXml

                    pnlPaging.Visible = True

                    PagingCommon(objOutputXml)
                    Dim intcol As Integer = GetSortColumnIndex(grdvMktShareBrResult)
                    If ViewState("Desc") = "FALSE" Then
                        grdvMktShareBrResult.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        grdvMktShareBrResult.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If
                End If


                'End If
                'If rdSummaryOption.SelectedValue = "6" Then
                '    grdvMarketShare.DataSource = ds.Tables("DETAIL")
                '    grdvMarketShare.DataBind()
                '    ViewState("PrevSearching") = objInputXml.OuterXml
                '    PagingCommon(objOutputXml)
                '    Dim intcol As Integer = GetSortColumnIndex(grdvMarketShare)
                '    If ViewState("Desc") = "FALSE" Then
                '        grdvMarketShare.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                '    End If
                '    If ViewState("Desc") = "TRUE" Then
                '        grdvMarketShare.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                '    End If

                'End If
            Else

                grdvMktShareBrResult.DataSource = String.Empty
                grdvMktShareBrResult.DataBind()
                grdvMarketShare.DataSource = String.Empty
                grdvMarketShare.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
        End If

    End Sub

    Private Sub LoadAllControls()
        Try

            objEams.BindDropDown(drpAirLineName, "AIRLINE", False, 1)
            objEams.BindDropDown(drpCity, "CITY", False, 3)
            'drpCity.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpCountry, "COUNTRY", False, 3)
            'drpCountry.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpOneAoffice, "AOFFICE", False, 3)
            'drpOneAoffice.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            'drpRegion.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)

            objEams.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

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

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try


            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
            Dim objExcelExp As New ExportExcel
            Dim counter As Integer = 0
            '<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
            '</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
            '<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
            '<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
            '<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
            objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><GroupTypeID></GroupTypeID><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><SHOWBREAKUP></SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION>  <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHAIRMARKETSHARE_INPUT>")

            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

                .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

                .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

                .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

                'If drpAirLineName.SelectedIndex <> 0 Then
                .SelectSingleNode("AIRLINECODE").InnerText = drpAirLineName.SelectedValue.Trim()
                ' End If

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpOneAoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
                End If

                .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

                ' If rdSummaryOption.SelectedValue.Trim() <> "6" Then
                If chkShowBr.Checked = True Then
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "1"
                Else
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
                End If
                ' End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                'Following Statement is written for Limited to own Agency
                If Session("Security") IsNot Nothing Then
                    Dim xDoc As New XmlDocument
                    xDoc.LoadXml(Session("Security"))

                    Dim str As String()
                    str = Session("LoginSession").ToString().Split("|")
                    .SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)

                    If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                        .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                    End If
                End If




            End With

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "SELECTBY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else

                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)




            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                Dim arlstNo As New ArrayList
                Dim arlstCoName As New ArrayList

                ' Dim objXmlReader As XmlNodeReader

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAIL")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                With objXmlNodeClone

                    If ds IsNot Nothing Then
                        If chkShowBr.Checked = False Then
                            If rdSummaryOption.SelectedValue <> "6" Then

                                '<DETAIL 
                                'SELECTBY = ""
                                'Chennai(" NAME="" ")
                                'ADDRESS = ""
                                'CITY = ""
                                'A = "157094"
                                'B = "31555"
                                'G = "88903"
                                'P = "7576"
                                'W = "0"
                                'TOTAL = "285128"
                                'chain_code="" /> 


                                .Attributes("SELECTBY").Value = "Total "
                                .Attributes("A").Value = ds.Tables("PAGE_TOTAL").Rows(0)("A").ToString()
                                .Attributes("B").Value = ds.Tables("PAGE_TOTAL").Rows(0)("B").ToString() 'hdTARGETPERDAY.Value
                                .Attributes("G").Value = ds.Tables("PAGE_TOTAL").Rows(0)("G").ToString() 'hdAverageBookings.Value.Trim()
                                .Attributes("P").Value = ds.Tables("PAGE_TOTAL").Rows(0)("P").ToString() ' hdNetbookings.Value
                                .Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString() 'hdCar_Netbookings.Value
                                .Attributes("W").Value = ds.Tables("PAGE_TOTAL").Rows(0)("W").ToString() ' hdHotel_Netbookings.Value
                                '  .Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString() ' hdHotel_Netbookings.Value
                                arlstCoName.Insert(0, rdSummaryOption.SelectedItem.Text)
                                'arlstCoName.Insert(1, "Agency Name")
                                'arlstCoName.Insert(2, "Agency Address")
                                arlstCoName.Insert(1, "1A")
                                arlstCoName.Insert(2, "1B")
                                arlstCoName.Insert(3, "1G")
                                arlstCoName.Insert(4, "1P")
                                arlstCoName.Insert(5, "1W")
                                arlstCoName.Insert(6, "TOTAL")

                                arlstNo.Insert(0, 0)
                                arlstNo.Insert(1, 4)
                                arlstNo.Insert(2, 5)
                                arlstNo.Insert(3, 6)
                                arlstNo.Insert(4, 7)
                                arlstNo.Insert(5, 8)
                                arlstNo.Insert(6, 9)
                                '  For counter = 0 To 6
                                'arlstNo.Insert(counter, counter)
                                '  Next

                                ' <DETAIL SELECTBY="2" NAME="Bird Travels Pvt Ltd" ADDRESS="Hotel Clarks Shiraj54,Taj_road" CITY="Agra" A="8" B="0" G="0" P="0" W="0" TOTAL="8" /> 


                            End If
                
                            If rdSummaryOption.SelectedValue = "6" Then

                                '   <DETAIL
                                '    SELECTBY = "2"
                                '    NAME = "Bird Travels Pvt Ltd"
                                '    ADDRESS = "Hotel Clarks Shiraj54,Taj_road"
                                '    CITY = "Agra"
                                '    A = "8"
                                '    B = "0"
                                '    G = "0"
                                '    P = "0"
                                '    W = "0"
                                '    TOTAL = "8"
                                '    chain_code = "928"
                                '/> 

                                .Attributes("SELECTBY").Value = "Total "
                                .Attributes("A").Value = ds.Tables("PAGE_TOTAL").Rows(0)("A").ToString()
                                .Attributes("B").Value = ds.Tables("PAGE_TOTAL").Rows(0)("B").ToString() 'hdTARGETPERDAY.Value
                                .Attributes("G").Value = ds.Tables("PAGE_TOTAL").Rows(0)("G").ToString() 'hdAverageBookings.Value.Trim()
                                .Attributes("P").Value = ds.Tables("PAGE_TOTAL").Rows(0)("P").ToString() ' hdNetbookings.Value
                                .Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString() 'hdCar_Netbookings.Value
                                .Attributes("W").Value = ds.Tables("PAGE_TOTAL").Rows(0)("W").ToString() ' hdHotel_Netbookings.Value

                                'arlstCoName.Insert(0, rdSummaryOption.SelectedItem.Text)
                                arlstCoName.Insert(0, "Lcode")

                                arlstCoName.Insert(1, "Chain Code")

                                arlstCoName.Insert(2, "Agency Name")
                                arlstCoName.Insert(3, "Agency Address")
                                arlstCoName.Insert(4, "City")
                                arlstCoName.Insert(5, "1A")
                                arlstCoName.Insert(6, "1B")
                                arlstCoName.Insert(7, "1G")
                                arlstCoName.Insert(8, "1P")
                                arlstCoName.Insert(9, "1W")
                                arlstCoName.Insert(10, "TOTAL")



                                'For counter = 0 To 10
                                '    arlstNo.Insert(counter, counter)
                                'Next

                                arlstNo.Insert(0, 0)
                                arlstNo.Insert(1, 10)
                                arlstNo.Insert(2, 1)
                                arlstNo.Insert(3, 2)
                                arlstNo.Insert(4, 3)
                                arlstNo.Insert(5, 4)
                                arlstNo.Insert(6, 5)
                                arlstNo.Insert(7, 6)
                                arlstNo.Insert(8, 7)
                                arlstNo.Insert(9, 8)
                                arlstNo.Insert(10, 9)



                            End If

                        Else
                            'BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS=''
                            If rdSummaryOption.SelectedValue <> "6" Then
                                .Attributes("SELECTBY").Value = "Total "
                                .Attributes("BOOKINGACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGACTIVE").ToString()
                                .Attributes("CANCELACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELACTIVE").ToString()
                                .Attributes("BOOKINGPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGPASSIVE").ToString()
                                .Attributes("CANCELPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELPASSIVE").ToString()
                                .Attributes("LATE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("LATE").ToString()
                                .Attributes("NULLACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLACTIVE").ToString()
                                .Attributes("NULLPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLPASSIVE").ToString()
                                .Attributes("NETBOOKINGS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS").ToString()

                                arlstCoName.Insert(0, rdSummaryOption.SelectedItem.Text)
                                arlstCoName.Insert(1, "CRS Code")
                                arlstCoName.Insert(2, "Booking Active")
                                arlstCoName.Insert(3, "Cancel Active")
                                arlstCoName.Insert(4, "Booking Passive")
                                arlstCoName.Insert(5, "Cancel Passive")
                                arlstCoName.Insert(6, "Late")
                                arlstCoName.Insert(7, "Null Active")
                                arlstCoName.Insert(8, "Null Passive")
                                arlstCoName.Insert(9, "Net Bookings")


                                For counter = 0 To 9
                                    arlstNo.Insert(counter, counter)
                                Next



                            End If
                            If rdSummaryOption.SelectedValue = "6" Then
                                '.Attributes("SELECTBY").Value = "Total "
                                '.Attributes("A").Value = ds.Tables("PAGE_TOTAL").Rows(0)("A").ToString()
                                '.Attributes("B").Value = ds.Tables("PAGE_TOTAL").Rows(0)("B").ToString() 'hdTARGETPERDAY.Value
                                '.Attributes("G").Value = ds.Tables("PAGE_TOTAL").Rows(0)("G").ToString() 'hdAverageBookings.Value.Trim()
                                '.Attributes("P").Value = ds.Tables("PAGE_TOTAL").Rows(0)("P").ToString() ' hdNetbookings.Value
                                '.Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString() 'hdCar_Netbookings.Value
                                '.Attributes("W").Value = ds.Tables("PAGE_TOTAL").Rows(0)("W").ToString() ' hdHotel_Netbookings.Value

                                ''arlstCoName.Insert(0, rdSummaryOption.SelectedItem.Text)
                                'arlstCoName.Insert(0, "Lcode")
                                'arlstCoName.Insert(1, "Agency Name")
                                'arlstCoName.Insert(2, "Agency Address")
                                'arlstCoName.Insert(3, "City")
                                'arlstCoName.Insert(4, "1A")
                                'arlstCoName.Insert(5, "1B")
                                'arlstCoName.Insert(6, "1G")
                                'arlstCoName.Insert(7, "1P")
                                'arlstCoName.Insert(8, "1W")
                                'arlstCoName.Insert(9, "TOTAL")

                                'For counter = 0 To 9
                                '    arlstNo.Insert(counter, counter)
                                'Next

                                .Attributes("SELECTBY").Value = "Total "
                                .Attributes("BOOKINGACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGACTIVE").ToString()
                                .Attributes("CANCELACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELACTIVE").ToString()
                                .Attributes("BOOKINGPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKINGPASSIVE").ToString()
                                .Attributes("CANCELPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CANCELPASSIVE").ToString()
                                .Attributes("LATE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("LATE").ToString()
                                .Attributes("NULLACTIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLACTIVE").ToString()
                                .Attributes("NULLPASSIVE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NULLPASSIVE").ToString()
                                .Attributes("NETBOOKINGS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("NETBOOKINGS").ToString()

                                ' arlstCoName.Insert(0, rdSummaryOption.SelectedItem.Text)

                                arlstCoName.Insert(0, "Lcode")
                                arlstCoName.Insert(1, "Chain Code")

                                arlstCoName.Insert(2, "Agency Name")
                                arlstCoName.Insert(3, "Agency Address")
                                arlstCoName.Insert(4, "City")

                                arlstCoName.Insert(5, "CRS Code")
                                arlstCoName.Insert(6, "Booking Active")
                                arlstCoName.Insert(7, "Cancel Active")
                                arlstCoName.Insert(8, "Booking Passive")
                                arlstCoName.Insert(9, "Cancel Passive")
                                arlstCoName.Insert(10, "Late")
                                arlstCoName.Insert(11, "Null Active")
                                arlstCoName.Insert(12, "Null Passive")
                                arlstCoName.Insert(13, "Net Bookings")
                                'For counter = 0 To 9
                                '    arlstNo.Insert(counter, counter)
                                'Next

                                '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' CRSCODETEXT='' BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' NAME='' ADDRESS='' CITY=''/><PAGE_TOTAL BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE=''  NULLPASSIVE='' NETBOOKINGS=''/><PAGE PAGE_COUNT='' TOTAL_ROWS='' /><Errors Status=''><Error Code='' Description=''/></Errors></PR_SEARCHAIRMARKETSHARE_OUTPUT>



                                arlstNo.Insert(0, 0)
                                arlstNo.Insert(1, 13)
                                arlstNo.Insert(2, 10)
                                arlstNo.Insert(3, 11)
                                arlstNo.Insert(4, 12)
                                arlstNo.Insert(5, 1)
                                arlstNo.Insert(6, 2)
                                arlstNo.Insert(7, 3)
                                arlstNo.Insert(8, 4)
                                arlstNo.Insert(9, 5)
                                arlstNo.Insert(10, 6)
                                arlstNo.Insert(11, 7)
                                arlstNo.Insert(12, 8)
                                arlstNo.Insert(13, 9)






                            End If
                        End If

                    End If
                End With

                'If rdSummaryOption.SelectedValue <> "6" Then
                '    e.Row.Cells(1).Visible = False
                '    e.Row.Cells(2).Visible = False
                'End If

                'Modification on 25th June as CR by Mukund
                'If rdSummaryOption.SelectedValue = "6" Then
                '    If drpCity.SelectedIndex <> 0 Then
                '        'e.Row.Cells(0).Visible = False
                '        arlstCoName.RemoveAt(0)
                '        arlstNo.RemoveAt(0)
                '    End If
                'End If


                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                objExcelExp.ExportDetails(objOutputXmlExport, "DETAIL", arlstNo, arlstCoName, ExportExcel.ExportFormat.Excel, "exportREport.xls")

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If







            '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' A='' B='' G='' P='' W='' TOTAL=''/>
            '<TOTAL A='' B='' G='' P='' W='' TOTAL=''/><Errors Status=''><Error Code='' Description=''/></Errors>
            '</PR_SEARCHAIRMARKETSHARE_OUTPUT>

            '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' CRSCODETEXT='' BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' />
            '<TOTAL BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS=''/><Errors Status=''>
            '<Error Code='' Description=''/></Errors>
            '</PR_SEARCHAIRMARKETSHARE_OUTPUT>


            'If chkShowBr.Checked = False Then
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
            '            hdA.Value = .Attributes("A").Value.Trim()
            '            hdB.Value = .Attributes("B").Value.Trim()
            '            hdG.Value = .Attributes("G").Value.Trim()
            '            hdP.Value = .Attributes("P").Value.Trim()
            '            hdTotal.Value = .Attributes("TOTAL").Value.Trim()
            '            hdW.Value = .Attributes("W").Value.Trim()

            '        End With
            '    End If
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        lblError.Text = ""
            '        ViewState("PrevSearching") = objInputXml.OuterXml

            '        grdvMarketShare.DataSource = ds.Tables("DETAIL")
            '        grdvMarketShare.DataBind()


            '    Else
            '        grdvMarketShare.DataSource = String.Empty
            '        grdvMarketShare.DataBind()
            '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'Else
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        If rdSummaryOption.SelectedValue <> "6" Then
            '            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
            '                hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
            '                hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
            '                hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
            '                hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
            '                hdLATE.Value = .Attributes("LATE").Value.Trim()
            '                hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
            '                hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
            '                hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
            '            End With
            '        End If
            '        If rdSummaryOption.SelectedValue = "6" Then
            '            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
            '                hdA.Value = .Attributes("A").Value.Trim()
            '                hdB.Value = .Attributes("B").Value.Trim()
            '                hdG.Value = .Attributes("G").Value.Trim()
            '                hdP.Value = .Attributes("P").Value.Trim()
            '                hdTotal.Value = .Attributes("TOTAL").Value.Trim()
            '                hdW.Value = .Attributes("W").Value.Trim()

            '            End With
            '        End If
            '    End If
            '    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '        objXmlReader = New XmlNodeReader(objOutputXml)
            '        ds.ReadXml(objXmlReader)
            '        lblError.Text = ""
            '        If rdSummaryOption.SelectedValue <> "6" Then
            '            grdvMktShareBrResult.DataSource = ds.Tables("DETAIL")
            '            grdvMktShareBrResult.DataBind()
            '            ViewState("PrevSearching") = objInputXml.OuterXml
            '        End If
            '        If rdSummaryOption.SelectedValue = "6" Then
            '            grdvMarketShare.DataSource = ds.Tables("DETAIL")
            '            grdvMarketShare.DataBind()
            '            ViewState("PrevSearching") = objInputXml.OuterXml
            '        End If
            '    Else

            '        grdvMktShareBrResult.DataSource = String.Empty
            '        grdvMktShareBrResult.DataBind()
            '        grdvMarketShare.DataSource = String.Empty
            '        grdvMarketShare.DataBind()
            '        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            '    End If
            'End If




            ''If grdvMarketShare.Rows.Count > 0 Then
            'If chkShowBr.Checked = False Then
            '    grdvMarketShare.AllowSorting = False
            '    grdvMarketShare.HeaderStyle.ForeColor = Drawing.Color.Black
            'BindGridExport()
            '    ' PrepareGridViewForExport(grdvMarketShare)
            '    ExportGridView(grdvMarketShare)
            'Else
            '    grdvMktShareBrResult.AllowSorting = False
            '    grdvMktShareBrResult.HeaderStyle.ForeColor = Drawing.Color.Black
            '    BindGridExport()
            '    ' PrepareGridViewForExport(grdvMktShareBrResult)
            '    ExportGridView(grdvMktShareBrResult)
            'End If
            '' End If


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

    Protected Sub grdvMarketShare_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMarketShare.RowDataBound
        Try
            
            '@ Code Added By abhishek
            Dim objSecurityXml As New XmlDocument
            Dim strBuilder As New StringBuilder
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then

                    Dim hdSelectedBy As New HiddenField
                  
                    hdSelectedBy = CType(e.Row.FindControl("hdSelectedBy"), HiddenField)


                    Dim LnkGraph As System.Web.UI.HtmlControls.HtmlAnchor
                    Dim LimAoff, LimReg, LimOwnOff, Acode, SelectedBy, SelectedByValue, AirLineName As String

                    LnkGraph = CType(e.Row.FindControl("LnkGraph"), System.Web.UI.HtmlControls.HtmlAnchor)
                    Acode = drpAirLineName.SelectedValue.ToString
                    AirLineName = drpAirLineName.SelectedItem.Text.ToString.Trim
                    LimAoff = ""
                    LimReg = ""
                    LimOwnOff = ""


                    SelectedByValue = hdSelectedBy.Value.Trim.ToString


                    SelectedBy = rdSummaryOption.SelectedValue.Trim()

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


                    '@ Previous Code
                    ' LnkGraph.Attributes.Add("OnClick", "javascript:return GraphFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Acode.ToString + "','" + SelectedBy.ToString + "','" + SelectedByValue.ToString + "','" + AirLineName.ToString + "');")
                    '@ New Added Code on 11/02/10 


                    '  Added  on 11/02/10
                    Dim Aoff, City, Country, Region, OnCarr, GType, Com_Ver As String
                    Aoff = ""
                    City = ""
                    Country = ""
                    Region = ""
                    OnCarr = ""
                    GType = ""
                    Com_Ver = ""
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

                    If drpLstGroupType.SelectedIndex <> 0 Then
                        GType = drpLstGroupType.SelectedValue.Trim()
                    End If

                    If DlstCompVertical.SelectedIndex <> 0 Then
                        Com_Ver = DlstCompVertical.SelectedValue.Trim()
                    End If

                    LnkGraph.Attributes.Add("OnClick", "javascript:return GraphFunction('" + strMonthFrom + "','" + strMonthTo + "','" + strYearF + "','" + strYearTo + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Acode.ToString + "','" + SelectedBy.ToString + "','" + SelectedByValue.ToString + "','" + AirLineName.ToString + "','" + City + "','" + Country + "','" + Region + "','" + Aoff + "','" + OnCarr + "','" + GType + "','" + Com_Ver + "');")

                    '  Added  on 11/02/10


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
                e.Row.Cells(0).Text = "Total"
                'e.Row.Cells(4).Text = hdA.Value.Trim()
                'e.Row.Cells(5).Text = hdB.Value.Trim()
                'e.Row.Cells(6).Text = hdG.Value.Trim()
                'e.Row.Cells(7).Text = hdP.Value.Trim()
                'e.Row.Cells(8).Text = hdW.Value.Trim()
                'e.Row.Cells(9).Text = hdTotal.Value.Trim()

                e.Row.Cells(5).Text = hdA.Value.Trim()
                e.Row.Cells(6).Text = hdB.Value.Trim()
                e.Row.Cells(7).Text = hdG.Value.Trim()
                e.Row.Cells(8).Text = hdP.Value.Trim()
                e.Row.Cells(9).Text = hdW.Value.Trim()
                e.Row.Cells(10).Text = hdTotal.Value.Trim()
            End If


            If rdSummaryOption.SelectedValue <> "6" Then
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
            End If

            'Modification on 25th June as CR by Mukund
            If rdSummaryOption.SelectedValue = "6" Then
                If drpCity.SelectedIndex <> 0 Then
                    ' e.Row.Cells(0).Visible = False
                End If
            End If


            If rdSummaryOption.SelectedValue = "6" Then

                'e.Row.Cells(4).Width = "70"
                'e.Row.Cells(5).Width = "70"
                'e.Row.Cells(6).Width = "70"
                'e.Row.Cells(7).Width = "70"
                'e.Row.Cells(8).Width = "70"
                'e.Row.Cells(9).Width = "70"
            End If


          



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMktShareBrResult_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMktShareBrResult.RowDataBound
        Try



            If rdSummaryOption.SelectedValue <> "6" Then
                e.Row.Cells(1).Visible = False
                e.Row.Cells(2).Visible = False
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
            End If


            If e.Row.RowType = DataControlRowType.Footer Then
                'e.Row.Cells(1).Text = "Total"
                'e.Row.Cells(2).Text = hdBOOKINGACTIVE.Value.Trim()
                'e.Row.Cells(3).Text = hdCANCELACTIVE.Value.Trim()
                'e.Row.Cells(4).Text = hdBOOKINGPASSIVE.Value.Trim()
                'e.Row.Cells(5).Text = hdCANCELPASSIVE.Value.Trim()
                'e.Row.Cells(6).Text = hdLATE.Value.Trim()
                'e.Row.Cells(7).Text = hdNULLACTIVE.Value.Trim()
                'e.Row.Cells(8).Text = hdNULLPASSIVE.Value.Trim()
                'e.Row.Cells(9).Text = hdNETBOOKINGS.Value.Trim()


                'e.Row.Cells(4).Text = "Total"
                'e.Row.Cells(5).Text = hdBOOKINGACTIVE.Value.Trim()
                'e.Row.Cells(6).Text = hdCANCELACTIVE.Value.Trim()
                'e.Row.Cells(7).Text = hdBOOKINGPASSIVE.Value.Trim()
                'e.Row.Cells(8).Text = hdCANCELPASSIVE.Value.Trim()
                'e.Row.Cells(9).Text = hdLATE.Value.Trim()
                'e.Row.Cells(10).Text = hdNULLACTIVE.Value.Trim()
                'e.Row.Cells(11).Text = hdNULLPASSIVE.Value.Trim()
                'e.Row.Cells(12).Text = hdNETBOOKINGS.Value.Trim()


                e.Row.Cells(5).Text = "Total"
                e.Row.Cells(6).Text = hdBOOKINGACTIVE.Value.Trim()
                e.Row.Cells(7).Text = hdCANCELACTIVE.Value.Trim()
                e.Row.Cells(8).Text = hdBOOKINGPASSIVE.Value.Trim()
                e.Row.Cells(9).Text = hdCANCELPASSIVE.Value.Trim()
                e.Row.Cells(10).Text = hdLATE.Value.Trim()
                e.Row.Cells(11).Text = hdNULLACTIVE.Value.Trim()
                e.Row.Cells(12).Text = hdNULLPASSIVE.Value.Trim()
                e.Row.Cells(13).Text = hdNETBOOKINGS.Value.Trim()


            End If

         
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("MTSR_MarketShare.aspx")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub rdSummaryOption_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdSummaryOption.SelectedIndexChanged

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

    Protected Sub grdvMarketShare_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMarketShare.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMktShareBrResult_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMktShareBrResult.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
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
#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            ' ViewState("Desc") = "FALSE"

            ' @ SELECTBY, MONTH,NAME, ADDRESS,CITY, CRSCODETEXT
            If SortName.Trim().ToUpper = "SELECTBY" Or SortName.Trim().ToUpper = "NAME" Or SortName.Trim().ToUpper = "ADDRESS" Or SortName.Trim().ToUpper = "CITY" Or SortName.Trim().ToUpper = "CRSCODETEXT" Then
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
                '  ViewState("Desc") = "FALSE"
                ' @ SELECTBY, MONTH,NAME, ADDRESS,CITY, CRSCODETEXT
                If SortName.Trim().ToUpper = "SELECTBY" Or SortName.Trim().ToUpper = "NAME" Or SortName.Trim().ToUpper = "ADDRESS" Or SortName.Trim().ToUpper = "CITY" Or SortName.Trim().ToUpper = "CRSCODETEXT" Then
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

    Protected Sub grdvMarketShare_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMarketShare.RowCreated
        'If e.Row.RowType = DataControlRowType.Header Then
        '    If rdSummaryOption.SelectedValue = "1" Then
        '        e.Row.Cells(0).Text = "City"
        '    ElseIf rdSummaryOption.SelectedValue = "2" Then
        '        e.Row.Cells(0).Text = "Country"
        '    ElseIf rdSummaryOption.SelectedValue = "4" Then
        '        e.Row.Cells(0).Text = "Region"
        '    ElseIf rdSummaryOption.SelectedValue = "3" Then
        '        e.Row.Cells(0).Text = "Office"
        '    ElseIf rdSummaryOption.SelectedValue = "6" Then
        '        e.Row.Cells(0).Text = "City"
        '    End If
        'End If
        If e.Row.RowType = DataControlRowType.Header Then
            If grdvMarketShare.AllowSorting = True Then
                If rdSummaryOption.SelectedValue = "1" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "6" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Lcode"
                End If
            Else
                If rdSummaryOption.SelectedValue = "1" Then
                    e.Row.Cells(0).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    e.Row.Cells(0).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    e.Row.Cells(0).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    e.Row.Cells(0).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "6" Then
                    e.Row.Cells(0).Text = "Lcode"
                End If
            End If
           
        End If

    End Sub

    Protected Sub grdvMktShareBrResult_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMktShareBrResult.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            If grdvMktShareBrResult.AllowSorting = True Then
                If rdSummaryOption.SelectedValue = "1" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "6" Then
                    '  CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "City"
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Lcode"
                End If
            Else
                If rdSummaryOption.SelectedValue = "1" Then
                    e.Row.Cells(0).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    e.Row.Cells(0).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    e.Row.Cells(0).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    e.Row.Cells(0).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "6" Then
                    '   e.Row.Cells(0).Text = "City"
                    e.Row.Cells(0).Text = "Lcode"
                End If
            End If
        End If
    End Sub
    Private Sub BindGridExport()

        grdvMarketShare.DataSource = String.Empty
        grdvMarketShare.DataBind()

        grdvMktShareBrResult.DataSource = String.Empty
        grdvMktShareBrResult.DataBind()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
        '<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
        '</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
        '<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
        '<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
        '<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
        objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><SHOWBREAKUP></SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION>  <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCHAIRMARKETSHARE_INPUT>")

        With objInputXml.DocumentElement

            .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

            .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

            .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

            'If drpAirLineName.SelectedIndex <> 0 Then
            .SelectSingleNode("AIRLINECODE").InnerText = drpAirLineName.SelectedValue.Trim()
            ' End If

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If
            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
            End If

            .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

            If rdSummaryOption.SelectedValue.Trim() <> "6" Then
                If chkShowBr.Checked = True Then
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "1"
                Else
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
                End If
            End If

            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            Dim str As String()
            str = Session("LoginSession").ToString().Split("|")
            .SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If
        End With

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "SELECTBY"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else

            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If

        objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)

        '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' A='' B='' G='' P='' W='' TOTAL=''/>
        '<TOTAL A='' B='' G='' P='' W='' TOTAL=''/><Errors Status=''><Error Code='' Description=''/></Errors>
        '</PR_SEARCHAIRMARKETSHARE_OUTPUT>

        '<PR_SEARCHAIRMARKETSHARE_OUTPUT><DETAIL SELECTBY='' CRSCODETEXT='' BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS='' />
        '<TOTAL BOOKINGACTIVE='' CANCELACTIVE='' BOOKINGPASSIVE='' CANCELPASSIVE='' LATE='' NULLACTIVE='' NULLPASSIVE='' NETBOOKINGS=''/><Errors Status=''>
        '<Error Code='' Description=''/></Errors>
        '</PR_SEARCHAIRMARKETSHARE_OUTPUT>


        If chkShowBr.Checked = False Then
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdA.Value = .Attributes("A").Value.Trim()
                    hdB.Value = .Attributes("B").Value.Trim()
                    hdG.Value = .Attributes("G").Value.Trim()
                    hdP.Value = .Attributes("P").Value.Trim()
                    hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                    hdW.Value = .Attributes("W").Value.Trim()

                End With
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdvMarketShare.DataSource = ds.Tables("DETAIL")
                grdvMarketShare.DataBind()


            Else
                grdvMarketShare.DataSource = String.Empty
                grdvMarketShare.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Else
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If rdSummaryOption.SelectedValue <> "6" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                        hdBOOKINGACTIVE.Value = .Attributes("BOOKINGACTIVE").Value.Trim()
                        hdCANCELACTIVE.Value = .Attributes("CANCELACTIVE").Value.Trim()
                        hdBOOKINGPASSIVE.Value = .Attributes("BOOKINGPASSIVE").Value.Trim()
                        hdCANCELPASSIVE.Value = .Attributes("CANCELPASSIVE").Value.Trim()
                        hdLATE.Value = .Attributes("LATE").Value.Trim()
                        hdNULLACTIVE.Value = .Attributes("NULLACTIVE").Value.Trim()
                        hdNULLPASSIVE.Value = .Attributes("NULLPASSIVE").Value.Trim()
                        hdNETBOOKINGS.Value = .Attributes("NETBOOKINGS").Value.Trim()
                    End With
                End If
                If rdSummaryOption.SelectedValue = "6" Then
                    With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                        hdA.Value = .Attributes("A").Value.Trim()
                        hdB.Value = .Attributes("B").Value.Trim()
                        hdG.Value = .Attributes("G").Value.Trim()
                        hdP.Value = .Attributes("P").Value.Trim()
                        hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                        hdW.Value = .Attributes("W").Value.Trim()

                    End With
                End If
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                If rdSummaryOption.SelectedValue <> "6" Then
                    grdvMktShareBrResult.DataSource = ds.Tables("DETAIL")
                    grdvMktShareBrResult.DataBind()
                    ViewState("PrevSearching") = objInputXml.OuterXml
                End If
                If rdSummaryOption.SelectedValue = "6" Then
                    grdvMarketShare.DataSource = ds.Tables("DETAIL")
                    grdvMarketShare.DataBind()
                    ViewState("PrevSearching") = objInputXml.OuterXml
                End If
            Else

                grdvMktShareBrResult.DataSource = String.Empty
                grdvMktShareBrResult.DataBind()
                grdvMarketShare.DataSource = String.Empty
                grdvMarketShare.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If

    End Sub

    Protected Sub BtnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGraph.Click
        Try

            'If chkShowBr.Checked = True Then
            '    Exit Sub
            'End If

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
            '<PR_SEARCHAIRMARKETSHARE_INPUT><MONTHFROM>01</MONTHFROM><YEARFROM>
            '</YEARFROM><MONTHTO>02</MONTHTO><YEARTO></YEARTO>
            '<AIRLINECODE>IC</AIRLINECODE><CITY></CITY><COUNTRY>INDIA</COUNTRY><AOFFICE></AOFFICE><REGION></REGION>
            '<SELECTBY>1</SELECTBY><SHOWBREAKUP>1</SHOWBREAKUP><RESPONSIBLESTAFFID />
            '<LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHAIRMARKETSHARE_INPUT>
            objInputXml.LoadXml("<PR_SEARCHAIRMARKETSHARE_INPUT><GroupTypeID></GroupTypeID><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><AIRLINECODE></AIRLINECODE><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><SELECTBY></SELECTBY><LCODE></LCODE><SHOWBREAKUP></SHOWBREAKUP><RESPONSIBLESTAFFID /><LIMITED_TO_REGION></LIMITED_TO_REGION>  <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHAIRMARKETSHARE_INPUT>")

            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    .SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

                .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

                .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

                .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

                'If drpAirLineName.SelectedIndex <> 0 Then
                .SelectSingleNode("AIRLINECODE").InnerText = drpAirLineName.SelectedValue.Trim()
                ' End If

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpOneAoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
                End If

                .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

                ' If rdSummaryOption.SelectedValue.Trim() <> "6" Then
                If chkShowBr.Checked = True Then
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
                Else
                    .SelectSingleNode("SHOWBREAKUP").InnerText = "0"
                End If
                ' End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                'Following Statement is written for Limited to own Agency
                If Session("Security") IsNot Nothing Then
                    Dim xDoc As New XmlDocument
                    xDoc.LoadXml(Session("Security"))

                    Dim str As String()
                    str = Session("LoginSession").ToString().Split("|")
                    .SelectSingleNode("RESPONSIBLESTAFFID").InnerText = str(0)

                    If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                        .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                    End If
                End If

            End With

            objOutputXml = objbzDailyBooking.SearchMarketShareAirline(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("TotalMarketShareGraph") = objOutputXml.OuterXml

                Dim strMonthFrom As String = drpMonthF.SelectedIndex + 1
                Dim strMonthTo As String = drpMonthTo.SelectedIndex + 1
                Dim strYearF As String = drpYearF.SelectedItem.Text.Trim()
                Dim strYearTo As String = drpYearTo.SelectedItem.Text.Trim()
                Dim paramDate As String = "" 'FMonth, TMonth, FYear, TYear
                paramDate = "&FMonth=" + strMonthFrom.ToString + "&TMonth=" + strMonthTo.ToString + "&FYear=" + strYearF.ToString + "TYear=" + strYearTo.ToString

                ClientScript.RegisterStartupScript(Me.GetType(), "Open1", "<script language='javascript'>" & _
                                     "PopupWindow=window.open('../RPSR_ReportShow.aspx?Case=TotalMarketShareGraph&Param=2&Acode=" & drpAirLineName.SelectedItem.ToString + "&SelectedBy=" + rdSummaryOption.SelectedValue.ToString + paramDate & "','Airline','height=500px,width=830px,top=0,left=0,scrollbars=1,status=1')" & _
                                     ";PopupWindow.focus();</script>")

                '  Response.Redirect("../RPSR_ReportShow.aspx?Case=TotalMarketShareGraph&Param=2&Acode=" & drpAirLineName.SelectedItem.ToString + "&SelectedBy=" + rdSummaryOption.SelectedValue.ToString + paramDate, False)

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
