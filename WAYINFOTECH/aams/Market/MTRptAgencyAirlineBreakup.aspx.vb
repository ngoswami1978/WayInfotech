Partial Class Market_MTRptAgencyAirlineBreakup
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            btnPrint.Attributes.Add("onclick", "return CheckValidation();")
            '  btnReset.Attributes.Add("onclick", "return IspOrderReset();")
            lblError.Text = String.Empty
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")
            dlstCity.Attributes.Add("onkeyup", "return gotop('dlstCity');")
            dlstCountry.Attributes.Add("onkeyup", "return gotop('dlstCountry');")
            dlstAoffice.Attributes.Add("onkeyup", "return gotop('dlstAoffice');")
            dlstRegion.Attributes.Add("onkeyup", "return gotop('dlstRegion');")
            dlstAirline.Attributes.Add("onkeyup", "return gotop('dlstAirline');")

            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyAirlineBreakup']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyAirlineBreakup']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnPrint.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                BindAllControl()
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = dlstAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                dlstAoffice.SelectedValue = li.Value
                            End If

                        End If
                        dlstAoffice.Enabled = False
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub BindAllControl()
        Try
            Dim i, j As Integer
            objeAAMS.BindDropDown(dlstCity, "CITY", True, 3)
            objeAAMS.BindDropDown(dlstCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(dlstAirline, "AIRLINE", True, 4)
            objeAAMS.BindDropDown(dlstRegion, "REGION", True, 3)
            objeAAMS.BindDropDown(dlstAoffice, "AOFFICE", True, 3)
            i = 0

            For j = DateTime.Now.Year To 1990 Step -1
                dlstYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                dlstYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            dlstYearFrom.SelectedValue = DateTime.Now.Year
            dlstYearTo.SelectedValue = DateTime.Now.Year
            dlstMonthFrom.SelectedValue = 1
            dlstMonthTo.SelectedValue = 12
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("MTRptAgencyAirlineBreakup.aspx", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim objAgencyAirBreakupReportInputXml, objAgencyAirBreakupReportOutputXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objAgencyAirBreakupReportInputXml.LoadXml("<PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT><LCode></LCode><AgencyName></AgencyName> <AirlineCode></AirlineCode><City></City> <Country></Country> <Region></Region> <Aoffice></Aoffice><SMonth></SMonth> <SYear></SYear> <TMonth></TMonth> <Tyear></Tyear></PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT>")
            objAgencyAirBreakupReportInputXml.LoadXml("<PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT><LCode></LCode><AgencyName></AgencyName> <AirlineCode></AirlineCode><City></City> <Country></Country> <Region></Region> <Aoffice></Aoffice><SMonth></SMonth> <SYear></SYear> <TMonth></TMonth> <Tyear></Tyear><RESP_1A/><CHAIN_CODE></CHAIN_CODE></PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT>")

            If Not Session("LoginSession") Is Nothing Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            ' objInputIspOrderReportXml.LoadXml("<RP_ORDER_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /></RP_ORDER_INPUT>")
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = ""
            Else
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value.Trim()
            End If

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCode").InnerXml = txtLcode.Text.Trim
            End If

            If txtChaincode.Text.Trim <> "" Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChaincode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011

            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            ' If (dlstAirline.SelectedIndex <> 0) Then
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("AirlineCode").InnerText = dlstAirline.SelectedValue ' dlstAirline.SelectedItem.Text
            '  End If
            If (dlstCity.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("City").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Country").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (dlstRegion.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Region").InnerText = dlstRegion.SelectedItem.Text
            End If

            If (dlstAoffice.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = dlstAoffice.SelectedValue
            End If
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = dlstMonthFrom.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("TMonth").InnerText = dlstMonthTo.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = dlstYearFrom.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Tyear").InnerText = dlstYearTo.SelectedValue

            'Here Back end Method Call
            objAgencyAirBreakupReportOutputXml = objbzMIDT.Rpt_AgencyAirlineBreakup(objAgencyAirBreakupReportInputXml)

            If objAgencyAirBreakupReportOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                ' Making Date on the basis of input data for MonthFrom , MonthTo, YearFrom And YearTo
                Dim strFromDate As String = ""
                Dim strToDate As String = ""
                strFromDate = "1/" + dlstMonthFrom.SelectedItem.Text.Substring(0, 3) + "/" + dlstYearFrom.SelectedValue
                strToDate = DateTime.DaysInMonth(dlstYearTo.SelectedValue, dlstMonthTo.SelectedValue).ToString + "/" + dlstMonthTo.SelectedItem.Text.Substring(0, 3) + "/" + dlstYearTo.SelectedValue

                Session("AgencyAirlineBreakupReport") = objAgencyAirBreakupReportOutputXml.OuterXml
                Response.Redirect("~/RPSR_ReportShow.aspx?Case=AgencyAirlineBreakupReport&FDate=" + strFromDate + "&TDate=" + strToDate, False)
            Else
                lblError.Text = objAgencyAirBreakupReportOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objAgencyAirBreakupReportInputXml = Nothing
            objAgencyAirBreakupReportOutputXml = Nothing
            objbzMIDT = Nothing
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim objAgencyAirBreakupReportInputXml, objAgencyAirBreakupReportOutputXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            'objAgencyAirBreakupReportInputXml.LoadXml("<PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT><LCode></LCode><AgencyName></AgencyName> <AirlineCode></AirlineCode><City></City> <Country></Country> <Region></Region> <Aoffice></Aoffice><SMonth></SMonth> <SYear></SYear> <TMonth></TMonth> <Tyear></Tyear></PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT>")

            objAgencyAirBreakupReportInputXml.LoadXml("<PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT><LCode></LCode><AgencyName></AgencyName> <AirlineCode></AirlineCode><City></City> <Country></Country> <Region></Region> <Aoffice></Aoffice><SMonth></SMonth> <SYear></SYear> <TMonth></TMonth> <Tyear></Tyear><RESP_1A/><CHAIN_CODE></CHAIN_CODE></PR_RPT_AGENCYAIRLINE_BREAKUP_INPUT>")
            ' objInputIspOrderReportXml.LoadXml("<RP_ORDER_INPUT><AgencyName /><City /><Country /><ISPName /><PeriodFromMonth /><PeriodFromYear /></RP_ORDER_INPUT>")

            If Not Session("LoginSession") Is Nothing Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = ""
            Else
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = hdAgencyName.Value.Trim()
            End If

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChaincode.Text.Trim <> "" Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChaincode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011



            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text
            ' If (dlstAirline.SelectedIndex <> 0) Then
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("AirlineCode").InnerText = dlstAirline.SelectedValue ' dlstAirline.SelectedItem.Text
            '  End If
            If (dlstCity.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("City").InnerText = Trim(dlstCity.SelectedItem.Text)
            End If
            If (dlstCountry.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Country").InnerText = Trim(dlstCountry.SelectedItem.Text)
            End If
            If (dlstRegion.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Region").InnerText = dlstRegion.SelectedItem.Text
            End If

            If (dlstAoffice.SelectedIndex <> 0) Then
                objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = dlstAoffice.SelectedValue
            End If
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = dlstMonthFrom.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("TMonth").InnerText = dlstMonthTo.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = dlstYearFrom.SelectedValue
            objAgencyAirBreakupReportInputXml.DocumentElement.SelectSingleNode("Tyear").InnerText = dlstYearTo.SelectedValue

            'Here Back end Method Call
            objAgencyAirBreakupReportOutputXml = objbzMIDT.Rpt_AgencyAirlineBreakup(objAgencyAirBreakupReportInputXml)

            If objAgencyAirBreakupReportOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then             '

                ' Making Date on the basis of input data for MonthFrom , MonthTo, YearFrom And YearTo
                Dim strFromDate As String = ""
                Dim strToDate As String = ""
                strFromDate = "1/" + dlstMonthFrom.SelectedItem.Text.Substring(0, 3) + "/" + dlstYearFrom.SelectedValue
                strToDate = DateTime.DaysInMonth(dlstYearTo.SelectedValue, dlstMonthTo.SelectedValue).ToString + "/" + dlstMonthTo.SelectedItem.Text.Substring(0, 3) + "/" + dlstYearTo.SelectedValue
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objAgencyAirBreakupReportOutputXml.DocumentElement.SelectSingleNode("AGENCYAIRLINE_BREAKUP")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes(2).Value = "Total:"
                    For k As Integer = 3 To objXmlNodeClone.Attributes.Count - 3
                        For Each nodes As XmlNode In objAgencyAirBreakupReportOutputXml.DocumentElement.SelectNodes("AGENCYAIRLINE_BREAKUP")
                            .Attributes(k).Value = Val(.Attributes(k).Value) + Val(nodes.Attributes(k).Value)
                        Next
                    Next
                End With
                objAgencyAirBreakupReportOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objXmlReader = New XmlNodeReader(objAgencyAirBreakupReportOutputXml)
                ds.ReadXml(objXmlReader)

                'ds.Tables("AGENCYAIRLINE_BREAKUP").Columns.Add("SNO")

                'For i As Integer = 0 To ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.Count - 3
                '    ds.Tables("AGENCYAIRLINE_BREAKUP").Rows(i)(ds.Tables("AGENCYAIRLINE_BREAKUP").Columns.Count - 1) = i + 1
                'Next

                Dim dr As DataRow
                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = ""
                dr(6) = ""
                dr(7) = ""
                dr(8) = "Agency Airline Breakup"
                dr(9) = ""
                dr(10) = ""
                dr(11) = ""
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 0)

                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = ""
                dr(6) = ""
                dr(7) = ""
                dr(8) = " " + strFromDate + " "
                dr(9) = "  " + strToDate
                dr(10) = ""
                dr(11) = ""
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 1)
                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = ""
                dr(6) = ""
                dr(7) = ""
                dr(8) = ""
                dr(9) = ""
                dr(10) = ""
                dr(11) = ""
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 2)

                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = "AIRLINE"
                dr(6) = ""
                dr(7) = ""
                dr(8) = ""
                dr(9) = ""
                dr(10) = ""
                dr(11) = "TOTAL MIDT"
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 3)

                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = ""
                dr(6) = ""
                dr(7) = ""
                dr(8) = ""
                dr(9) = ""
                dr(10) = ""
                dr(11) = ""
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 4)


                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = "Agency Name"
                dr(1) = "OfficeId"
                dr(2) = "Address"
                dr(3) = "Printer"
                dr(4) = "1A"
                dr(5) = "1B"
                dr(6) = "1G"
                dr(7) = "1P"
                dr(8) = "1W"
                dr(9) = "Total"
                dr(10) = "1A"
                dr(11) = "1B"
                dr(12) = "1G"
                dr(13) = "1P"
                dr(14) = "1W"
                dr(15) = "Total"
                dr(16) = "Chain Code"
                dr(17) = "LCode"

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 5)

                dr = ds.Tables("AGENCYAIRLINE_BREAKUP").NewRow
                dr(0) = ""
                dr(1) = ""
                dr(2) = ""
                dr(3) = ""
                dr(4) = ""
                dr(5) = ""
                dr(6) = ""
                dr(7) = ""
                dr(8) = ""
                dr(9) = ""
                dr(10) = ""
                dr(11) = ""
                dr(12) = ""
                dr(13) = ""
                dr(14) = ""
                dr(15) = ""
                dr(16) = ""
                dr(17) = ""

                ds.Tables("AGENCYAIRLINE_BREAKUP").Rows.InsertAt(dr, 6)

                objOutputXmlExport.LoadXml(ds.GetXml().ToString)
                Dim objExport As New ExportExcel
                'Dim strArray() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
                'Dim intArray() As Integer = {0, 16, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
                Dim strArray() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
                Dim intArray() As Integer = {16, 17, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
                objExport.ExportDetails(objOutputXmlExport, "AGENCYAIRLINE_BREAKUP", intArray, strArray, ExportExcel.ExportFormat.Excel, "AgencyAirLineBreakup.xls")
            Else
                lblError.Text = objAgencyAirBreakupReportOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objAgencyAirBreakupReportInputXml = Nothing
            objAgencyAirBreakupReportOutputXml = Nothing
            objbzMIDT = Nothing
        End Try
    End Sub
End Class
