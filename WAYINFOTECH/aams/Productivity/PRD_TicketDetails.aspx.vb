Imports System.IO
Imports System.Text
Partial Class Productivity_PRD_TicketDetails
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim FooterDataset As DataSet
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

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")

            ChkAirBreackup.Attributes.Add("onclick", "return CheckedUnchecked1AProd();")

            If Not IsPostBack Then

                Dim strBuilder As New StringBuilder
                Dim objSecurityXml As New XmlDocument
                'objSecurityXml.LoadXml(Session("Security"))
                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                '        If strBuilder(0) = "0" Then
                '            btnSearch.Enabled = False
                '            Response.Redirect("~/NoRights.aspx")
                '            Exit Sub
                '        End If
                '        If strBuilder(4) = "0" Then
                '            btnExport.Enabled = False
                '        End If
                '    End If
                'Else
                '    strBuilder = objeAAMS.SecurityCheck(31)
                'End If
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

                'objSecurityXml = New XmlDocument
                'objSecurityXml.LoadXml(Session("Security"))
                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Count <> 0 Then
                '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A_Productivity_Insurance']").Attributes("Value").Value)
                '        If strBuilder(0) = "0" Then
                '            ChkABooking.Items(3).Enabled = False
                '        End If
                '    End If
                'Else
                '    strBuilder = objeAAMS.SecurityCheck(31)
                'End If

            End If

            If Not IsPostBack Then
                BindAllControl()
             
                If Request.QueryString("Fmonth") IsNot Nothing Then
                    drpMonthFrom.SelectedValue = Request.QueryString("Fmonth").ToString
                End If
                If Request.QueryString("TMonth") IsNot Nothing Then
                    drpMonthTo.SelectedValue = Request.QueryString("TMonth").ToString
                End If
                If Request.QueryString("FYear") IsNot Nothing Then
                    drpYearFrom.SelectedValue = Request.QueryString("FYear").ToString
                End If
                If Request.QueryString("TYear") IsNot Nothing Then
                    drpYearTo.SelectedValue = Request.QueryString("TYear").ToString
                End If
                '   var parameter="Fmonth=" + FMonth  + "&TMonth=" + TMonth + 
                '"&FYear=" + FYear +  "&TYear=" + TYear +  "&Lcode=" + Lcode +
                '  "&GroupData=" + GroupData +  "&TicketData=" + TicketData +  
                '"&DailyBookData=" + DailyBookData  +  "&AProdData=" + AProdData  +  
                '"&CRSData=" + CRSData  +  "&AirBreakData=" + AirBreakData  ;
                If Request.QueryString("TicketData") IsNot Nothing Then
                    If Request.QueryString("TicketData").ToString = "1" Then
                        ChkTicket.Checked = True
                    Else
                        ChkTicket.Checked = False
                    End If
                End If
                If Request.QueryString("DailyBookData") IsNot Nothing Then
                    If Request.QueryString("DailyBookData").ToString = "1" Then
                        ChkDailyBook.Checked = True
                    Else
                        ChkDailyBook.Checked = False
                    End If

                End If
                If Request.QueryString("AProdData") IsNot Nothing Then
                    If Request.QueryString("AProdData").ToString = "1" Then
                        Chk1AProd.Checked = True
                        Chk1AProd.Enabled = True
                    Else
                        Chk1AProd.Checked = False
                    End If

                End If
                If Request.QueryString("CRSData") IsNot Nothing Then
                    If Request.QueryString("CRSData").ToString = "1" Then
                        ChkAllCRS.Checked = True
                    Else
                        ChkAllCRS.Checked = False
                    End If
                End If


                If Request.QueryString("GroupData") IsNot Nothing Then
                    If Request.QueryString("GroupData").ToString = "1" Then
                        ChkWholeGroup.Checked = True
                    Else
                        ChkWholeGroup.Checked = False
                    End If
                End If

                If Request.QueryString("NIDT") IsNot Nothing Then
                    If Request.QueryString("NIDT").ToString = "1" Then
                        chkNIDT.Checked = True
                    Else
                        chkNIDT.Checked = False
                    End If
                End If


                If Request.QueryString("AirBreakData") IsNot Nothing Then
                    If Request.QueryString("AirBreakData").ToString = "1" Then
                        ChkAirBreackup.Checked = True
                        Chk1AProd.Checked = False
                        Chk1AProd.Enabled = False

                        chkNIDT.Checked = False
                        chkNIDT.Enabled = False
                    Else
                        ChkAirBreackup.Checked = False
                    End If
                End If


                If Request.QueryString("AirCode") IsNot Nothing Then
                    Dim li As New ListItem
                    li = DlstAirline.Items.FindByValue(Request.QueryString("AirCode").ToString)
                    If li IsNot Nothing Then
                        DlstAirline.SelectedValue = li.Value
                    End If
                End If


                AgencyView()
                TicketDetailsSearch()
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub TicketDetailsSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            '  txtTotlaProductivity.Text = "0"

            objInputXml.LoadXml("<PR_SEARCH_1ATICKET_DETAILS_INPUT> <LCODE></LCODE> <SHOWGROUP></SHOWGROUP> <RESP_1A></RESP_1A> <MONTHLYBREAKUP></MONTHLYBREAKUP> <AIRLINE_CODE></AIRLINE_CODE><A_TICKET></A_TICKET> <DAILYBOOKING></DAILYBOOKING> <A_PRODUCTIVITY></A_PRODUCTIVITY> <ALL_CRS></ALL_CRS><NIDT></NIDT><AIRBREAKUP></AIRBREAKUP> <DATEFROM></DATEFROM> <DATETO></DATETO> <PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC></PR_SEARCH_1ATICKET_DETAILS_INPUT>")

            '<PR_SEARCH_1ATICKET_DETAILS_INPUT>
            ' <LCODE></LCODE>
            ' <SHOWGROUP></SHOWGROUP>
            ' <RESP_1A></RESP_1A>
            ' <MONTHLYBREAKUP></MONTHLYBREAKUP>
            ' <AIRLINE_CODE></AIRLINE_CODE>
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
            '</PR_SEARCH_1ATICKET_DETAILS_INPUT>            

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString())
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
         
            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = drpYearFrom.SelectedValue.PadLeft(4, "0") + drpMonthFrom.SelectedValue.PadLeft(2, "0") + "01"
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = drpYearTo.SelectedValue.PadLeft(4, "0") + drpMonthTo.SelectedValue.PadLeft(2, "0") + "01"

            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "FALSE"
            End If

            If Request.QueryString("MBreakup") IsNot Nothing Then
                If Request.QueryString("MBreakup").ToString = "1" Then
                    objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "TRUE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
            End If


            'If Request.QueryString("AirCode") IsNot Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = Request.QueryString("AirCode").ToString
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""
            'End If

            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = DlstAirline.SelectedValue


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
                ' objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""

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
                ViewState("SortName") = "LCODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "MONTHLYBREAKUP" Or objNode.Name = "A_TICKET" Or objNode.Name = "DAILYBOOKING" Or objNode.Name = "A_PRODUCTIVITY" Or objNode.Name = "ALL_CRS" Or objNode.Name = "AIRBREAKUP" Or objNode.Name = "CODD" Or objNode.Name = "HX_BOOKINGS" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                ViewState("SortName") = "LCODE" '"CHAIN_CODE"
                                ViewState("Desc") = "FALSE"
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
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

            objOutputXml = objbzbzBIDT.Search_1ATicket_Details(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds

                grdvTicketDetails.DataSource = ds.Tables("A_TICKET_DETAILS")
                grdvTicketDetails.DataBind()
                'txtRecordCount.Text = ds.Tables("BIDTDETAILS").Rows.Count.ToString
                ' ##################################################################
                '@ Code Added For Paging And Sorting 
                ' ###################################################################
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)

                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'                   
                ' grdNewFormat.SortExpression = ViewState("SortName")
                SetImageForSorting(grdvTicketDetails)
                ' @ Added Code To Show Image'
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting 
                ' ###################################################################

                pnlPaging.Visible = True

            Else
                grdvTicketDetails.DataSource = Nothing
                grdvTicketDetails.DataBind()
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

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        TicketDetailsSearch()
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(DlstAirline, "AIRLINE", False, 3)
            Dim i, j As Integer
            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Protected Sub grdvBidtDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTicketDetails.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDataset IsNot Nothing Then
                    '  TKT_NETBOOKINGS='' DB_NETBOOKINGS='' A_NETBOOKINGS='' MT_NETBOOKINGS
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        e.Row.Cells(8).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TKT_NETBOOKINGS").ToString
                        e.Row.Cells(9).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("DB_NETBOOKINGS").ToString
                        e.Row.Cells(10).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("A_NETBOOKINGS").ToString
                        e.Row.Cells(11).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("MT_NETBOOKINGS").ToString
                        e.Row.Cells(12).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("CODD").ToString
                        e.Row.Cells(13).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("HX_BOOKINGS").ToString
                    End If
                End If
            End If
            Dim objSecurityXml As New XmlDocument
         
            If ChkTicket.Checked = True Then
                e.Row.Cells(8).Visible = True
            Else
                e.Row.Cells(8).Visible = False
            End If
            If ChkDailyBook.Checked = True Then
                e.Row.Cells(9).Visible = True
            Else
                e.Row.Cells(9).Visible = False
            End If

            If Chk1AProd.Checked = True Then
                e.Row.Cells(10).Visible = True
            Else
                e.Row.Cells(10).Visible = False
            End If

            If ChkAllCRS.Checked = True Then
                e.Row.Cells(11).Visible = True
            Else
                e.Row.Cells(11).Visible = False
            End If

            If ChkAirBreackup.Checked = True Then
                e.Row.Cells(10).Visible = False
                e.Row.Cells(5).Visible = True

            Else
                e.Row.Cells(5).Visible = False

            End If
            If chkNIDT.Checked = True Then
                e.Row.Cells(12).Visible = True
                e.Row.Cells(13).Visible = True
            Else
                e.Row.Cells(12).Visible = False
                e.Row.Cells(13).Visible = False
            End If

            If Request.QueryString("MBreakup") IsNot Nothing Then
                If Request.QueryString("MBreakup").ToString = "1" Then
                    e.Row.Cells(6).Visible = True
                    e.Row.Cells(7).Visible = True
                Else
                    e.Row.Cells(6).Visible = False
                    e.Row.Cells(7).Visible = False
                End If
            End If


            If e.Row.RowType = DataControlRowType.Footer Then
                If e.Row.Cells(5).Visible = True Or e.Row.Cells(6).Visible = True Or e.Row.Cells(7).Visible = True Or e.Row.Cells(8).Visible = True Or e.Row.Cells(12).Visible = True Or e.Row.Cells(13).Visible = True Then

                    If Request.QueryString("MBreakup") IsNot Nothing Then
                        If Request.QueryString("MBreakup").ToString = "1" Then
                            e.Row.Cells(7).Text = "Total"
                        Else
                            e.Row.Cells(4).Text = "Total"
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdvTicketDetails.AllowSorting = False
        grdvTicketDetails.HeaderStyle.ForeColor = Drawing.Color.Black
        TicketDetailsExport()
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

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            TicketDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            TicketDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            TicketDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvBidtDetails_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvTicketDetails.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdvBidtDetails_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvTicketDetails.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                '  ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @AIR, CAR, HOTEL, INSURANCE, TOTAL, PASSIVE, WITHPASSIVE,
                If SortName.Trim().ToUpper = "AIR" Or SortName.Trim().ToUpper = "CAR" Or SortName.Trim().ToUpper = "HOTEL" Or SortName.Trim().ToUpper = "INSURANCE" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
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
                    ' ViewState("Desc") = "FALSE"
                    '@ Added Code For Default descending sorting order on first time  of following Fields      
                    ' @AIR, CAR, HOTEL, INSURANCE, TOTAL, PASSIVE, WITHPASSIVE,
                    If SortName.Trim().ToUpper = "AIR" Or SortName.Trim().ToUpper = "CAR" Or SortName.Trim().ToUpper = "HOTEL" Or SortName.Trim().ToUpper = "INSURANCE" Or SortName.Trim().ToUpper = "TOTAL" Or SortName.Trim().ToUpper = "PASSIVE" Or SortName.Trim().ToUpper = "WITHPASSIVE" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields

                End If
            End If
            TicketDetailsSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    '#End Region

    Protected Sub ChkWholeGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkWholeGroup.CheckedChanged
        Try
            TicketDetailsSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub




    Private Sub TicketDetailsExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try

            objInputXml.LoadXml("<PR_SEARCH_1ATICKET_DETAILS_INPUT> <LCODE></LCODE> <SHOWGROUP></SHOWGROUP> <RESP_1A></RESP_1A> <MONTHLYBREAKUP></MONTHLYBREAKUP> <AIRLINE_CODE></AIRLINE_CODE><A_TICKET></A_TICKET> <DAILYBOOKING></DAILYBOOKING> <A_PRODUCTIVITY></A_PRODUCTIVITY> <ALL_CRS></ALL_CRS> <NIDT></NIDT><AIRBREAKUP></AIRBREAKUP> <DATEFROM></DATEFROM> <DATETO></DATETO> <PAGE_NO></PAGE_NO> <PAGE_SIZE></PAGE_SIZE> <SORT_BY></SORT_BY> <DESC></DESC></PR_SEARCH_1ATICKET_DETAILS_INPUT>")

            '<PR_SEARCH_1ATICKET_DETAILS_INPUT>
            ' <LCODE></LCODE>
            ' <SHOWGROUP></SHOWGROUP>
            ' <RESP_1A></RESP_1A>
            ' <MONTHLYBREAKUP></MONTHLYBREAKUP>
            ' <AIRLINE_CODE></AIRLINE_CODE>
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
            '</PR_SEARCH_1ATICKET_DETAILS_INPUT>            

            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString())
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = drpYearFrom.SelectedValue.PadLeft(4, "0") + drpMonthFrom.SelectedValue.PadLeft(2, "0") + "01"
            objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = drpYearTo.SelectedValue.PadLeft(4, "0") + drpMonthTo.SelectedValue.PadLeft(2, "0") + "01"

            If ChkWholeGroup.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "TRUE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SHOWGROUP").InnerText = "FALSE"
            End If

            If Request.QueryString("MBreakup") IsNot Nothing Then
                If Request.QueryString("MBreakup").ToString = "1" Then
                    objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "TRUE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("MONTHLYBREAKUP").InnerText = "FALSE"
            End If

            'If Request.QueryString("AirCode") IsNot Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = Request.QueryString("AirCode").ToString
            'Else
            '    objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""
            'End If

            objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = DlstAirline.SelectedValue


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
                ' objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = ""

            End If


         
            'Start CODE for sorting and paging
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "LCODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "LCODE"
            Else
                If ViewState("PrevSearching") Is Nothing Then
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name = "MONTHLYBREAKUP" Or objNode.Name = "A_TICKET" Or objNode.Name = "DAILYBOOKING" Or objNode.Name = "A_PRODUCTIVITY" Or objNode.Name = "ALL_CRS" Or objNode.Name = "AIRBREAKUP" Or objNode.Name = "CODD" Or objNode.Name = "HX_BOOKINGS" Then
                            If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                ViewState("SortName") = "LCODE" '"CHAIN_CODE"
                                ViewState("Desc") = "FALSE"
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
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
            grdvTicketDetails.AllowSorting = False
            grdvTicketDetails.HeaderStyle.ForeColor = Drawing.Color.Black

            objOutputXml = objbzbzBIDT.Search_1ATicket_Details(objInputXml)



            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvTicketDetails.DataSource = ds.Tables("A_TICKET_DETAILS")
                grdvTicketDetails.DataBind()

                '@ Code For Exporting the Data

                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("A_TICKET_DETAILS")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                With objXmlNodeClone
                    .Attributes("YEAR").Value = "Total"
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
                For intclmn As Integer = 0 To grdvTicketDetails.HeaderRow.Cells.Count - 1
                    If grdvTicketDetails.HeaderRow.Cells(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdvTicketDetails.HeaderRow.Cells.Count - 1 - IntInvisible) As String
                Dim intArray(grdvTicketDetails.HeaderRow.Cells.Count - 1 - IntInvisible) As Integer

                Dim intclmnVis As Integer = 0
                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                    If grdvTicketDetails.HeaderRow.Cells(intclmn).Visible = True Then
                        strArray(intclmnVis) = grdvTicketDetails.Columns(intclmn).HeaderText 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                        '@ Finding Position From xml Related with Header Text
                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1
                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvTicketDetails.Columns(intclmn).SortExpression.Trim Then
                                intArray(intclmnVis) = kk
                                intclmnVis = intclmnVis + 1
                                Exit For
                            End If
                        Next kk
                    End If
                Next intclmn

                objExport.ExportDetails(objOutputXmlExport, "A_TICKET_DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "TICKETDetails.xls")




            Else
                grdvTicketDetails.DataSource = Nothing
                grdvTicketDetails.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                txtRecordCount.Text = "0"

            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
            txtRecordCount.Text = "0"

        Finally
            objInputXml = Nothing
            objOutputXml = Nothing

        End Try
    End Sub

    Private Sub AgencyView()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
        If Request.QueryString("Lcode") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = objED.Decrypt(Request.QueryString("Lcode").ToString())
        End If
        objOutputXml = objbzAgency.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                txtAgencyName.Text = .Attributes("NAME").Value()
                Dim strAddress As String = .Attributes("ADDRESS").Value()
                strAddress &= " "
                strAddress &= .Attributes("ADDRESS1").Value()
                txtAdd.Text = strAddress '.Attributes("ADDRESS").Value()
                txtCity.Text = .Attributes("CITY").Value()
                txtCountry.Text = .Attributes("COUNTRY").Value()
            End With
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class


