Imports System.IO
Imports System.Text
Partial Class Market_MTSRAirLine_ProductivityCmpareReport
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim Total1Asum As Decimal = 0
    Dim Total1Bsum As Decimal = 0
    Dim Total1Gsum As Decimal = 0
    Dim Total1Psum As Decimal = 0
    Dim Total1Wsum As Decimal = 0
    Dim TotalSum As Decimal = 0
    Dim TotalTypePerSum As Decimal = 0
    Dim Total1APersum As Decimal = 0
    Dim Total1BPersum As Decimal = 0
    Dim Total1GPersum As Decimal = 0
    Dim Total1PPersum As Decimal = 0
    Dim Total1WPersum As Decimal = 0
    Dim FooterDataSet As DataSet
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
            lblError.Text = ""

            grdvAirlineProductivityCom.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry');")
            drpAoffice.Attributes.Add("onkeyup", "return gotop('drpAoffice');")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion');")
            drpAirline.Attributes.Add("onkeyup", "return gotop('drpAirline');")


            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())

                'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Productivity Comparision']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Productivity Comparision']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        btnComparision.Enabled = False
                        btnFirstYearProductivity.Enabled = False
                        btnSecYearProductivity.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            'ChkUseDailyBookData.Attributes.Add("onclick", "return SelectDateRange();")
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            btnFirstYearProductivity.Attributes.Add("onclick", "return CheckValidation();")
            btnSecYearProductivity.Attributes.Add("onclick", "return CheckValidation();")
            btnComparision.Attributes.Add("onclick", "return CheckValidation();")
            If Not IsPostBack Then
                BindAllControl()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AirlineProductivityCompareSearch(ByVal ReportType)
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            If ReportType = "3" Then
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT> <FYFMONTH></FYFMONTH> <FYFYEAR></FYFYEAR>  <FYTMONTH></FYTMONTH>  <FYTYEAR></FYTYEAR>  <SYFMONTH></SYFMONTH> <SYFYEAR></SYFYEAR> <SYTMONTH></SYTMONTH>  <SYTYEAR></SYTYEAR>  <AIRLINE/> <CITY/>  <AOFFICE/> <GroupTypeID />   <REGION/> <COUNTRY/> <SELECTBY/> <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            Else
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT><FMONTH/> <FYEAR/> <TMONTH/> <TYEAR/>  <AIRLINE/> <CITY/>  <AOFFICE/>  <REGION/> <COUNTRY/> <SELECTBY/> <GroupTypeID />  <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            End If

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE").InnerText = drpAirline.SelectedItem.Text
            End If

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedValue
            End If

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If
            If ReportType = "3" Then
                objInputXml.DocumentElement.SelectSingleNode("FYFMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYFYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTYEAR").InnerText = drpYearTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTYEAR").InnerText = drpYearTo2.SelectedValue

            ElseIf ReportType = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue
            ElseIf ReportType = "2" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo2.SelectedValue

            End If

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SHOWBREAKUP").InnerText = "0"


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If





            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

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
            End If
            Session("AirLinePrdDataSource") = Nothing
            '@ Reset DataSource If any input Change
            'If ViewState("PrevSearching") Is Nothing Then
            '    ' objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            'Else
            '    Dim objTempInputXml As New XmlDocument
            '    Dim objNodeList As XmlNodeList
            '    objTempInputXml.LoadXml(ViewState("PrevSearching"))
            '    objNodeList = objTempInputXml.DocumentElement.ChildNodes
            '    For Each objNode As XmlNode In objNodeList
            '        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
            '            ddlPageNumber.SelectedValue = 1
            '        End If
            '    Next
            'End If

            ddlPageNumber.SelectedValue = 1
           


            If ReportType = "3" Then
                objOutputXml = objbzBIDT.AirlineMarketShareComparison(objInputXml)
            Else
                objOutputXml = objbzBIDT.AirlineMarketShareYearWiseData(objInputXml)
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            ' objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''  ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''  TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''  GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = '' /><Errors Status='FALSE'><Error Code='' Description=''/></Errors></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                Dim dsets As New DataSet
                dsets = ds
                'For i As Integer = 0 To ds.Tables("DETAILS").Rows.Count - 1
                '    For j As Integer = 2 To ds.Tables("DETAILS").Columns.Count - 1
                '        ds.Tables("DETAILS").Rows(i)(j) = ds.Tables("DETAILS").Rows(i)(j).ToString.PadLeft(12, "0")
                '    Next
                'Next  
              

                Dim Clmn As String = ""
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"

                End If

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If

                grdvAirlineProductivityCom.PageIndex = 0

                grdvAirlineProductivityCom.DataSource = Nothing
                grdvAirlineProductivityCom.DataBind()

                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS")
                grdvAirlineProductivityCom.DataBind()
                txtRecordCount.Text = ds.Tables("DETAILS").Rows.Count.ToString

                Session("AirLinePrdDataSource") = ds
                ' lblFound.Visible = True
                txtRecordCount.Visible = True
                SetImageForSorting(grdvAirlineProductivityCom)


                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer


                BindControlsForNavigation(grdvAirlineProductivityCom.PageCount)
                'txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value


            Else
                grdvAirlineProductivityCom.DataSource = Nothing
                grdvAirlineProductivityCom.DataBind()
                txtRecordCount.Text = "0"
                ' lblFound.Visible = True
                txtRecordCount.Visible = True
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
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        btnSecYearProductivity.CssClass = "headingtabactive"
        btnComparision.CssClass = "headingtabactive"
        btnFirstYearProductivity.CssClass = "headingtab"
        AirlineProductivityCompareSearch(1)
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)

            objeAAMS.BindDropDown(drpCity, "CITY", False, 3)

            objeAAMS.BindDropDown(drpCountry, "COUNTRY", False, 3)

            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", False, 3)

            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)

            objeAAMS.BindDropDown(drpAirline, "AIRLINE", True, 1)




            Dim i, j As Integer
            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
            drpMonthTo2.SelectedValue = "12"
            drpMonthFrom2.SelectedValue = "1"
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearFrom2.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo2.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year - 1
            drpYearTo.SelectedValue = DateTime.Now.Year - 1
            drpYearFrom2.SelectedValue = DateTime.Now.Year
            drpYearTo2.SelectedValue = DateTime.Now.Year

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument

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

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        grdvAirlineProductivityCom.AllowPaging = False
        grdvAirlineProductivityCom.AllowSorting = False
        grdvAirlineProductivityCom.HeaderStyle.ForeColor = Drawing.Color.Black

        If btnFirstYearProductivity.CssClass = "headingtab" Then
            ' AirlineProductivityCompareSearch2(1)
            AirlineProductivityCompareNewExport(1)
        End If
        If btnSecYearProductivity.CssClass = "headingtab" Then
            ' AirlineProductivityCompareSearch2(2)
            AirlineProductivityCompareNewExport(2)
        End If
        If btnComparision.CssClass = "headingtab" Then
            'AirlineProductivityCompareSearch2(3)
            AirlineProductivityCompareNewExport(3)
        End If

        If grdvAirlineProductivityCom.Rows.Count > 0 Then
            ' PrepareGridViewForExport(grdvAirlineProductivityCom)
            ' ExportGridView(grdvAirlineProductivityCom, "AirlineProductivityCompare.xls")
        End If


    End Sub
    Private Sub ExportGridView(ByVal gv2 As GridView, ByVal FileName As String)

        Dim attachment As String = "attachment; filename=" + FileName
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        gv2.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(gv2)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()

    End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    'End Sub
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
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

            If (gv.Controls(i).HasControls()) Then
                PrepareGridViewForExport(gv.Controls(i))
            End If

        Next
    End Sub

    Protected Sub grdvAirlineProductivityCom_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirlineProductivityCom.RowDataBound
        Try
            'If e.Row.RowType = DataControlRowType.Header Then
            '    e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
            '    e.Row.Cells(7).Text = rdSummOpt.SelectedItem.Text + "%"
            'End If

            'If e.Row.RowType = DataControlRowType.Footer Then
            '    e.Row.Cells(0).Text = "Total"
            'End If
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "AMADEUS")
            '    Total1Asum += sum
            'End If

            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1A As Label
            '    Tot1A = CType(e.Row.FindControl("Tot1A"), Label)
            '    Tot1A.Text = String.Format("{0:d}", Total1Asum.ToString)
            'End If
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "ABACUS")
            '    Total1Bsum += sum
            'End If

            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1B As Label
            '    Tot1B = CType(e.Row.FindControl("Tot1B"), Label)
            '    Tot1B.Text = String.Format("{0:d}", Total1Bsum.ToString)
            'End If
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "GALILEO")
            '    Total1Gsum += sum
            'End If

            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1G As Label
            '    Tot1G = CType(e.Row.FindControl("Tot1G"), Label)
            '    Tot1G.Text = String.Format("{0:d}", Total1Gsum.ToString)
            'End If
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "WORLDSPAN")
            '    Total1Psum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1P As Label
            '    Tot1P = CType(e.Row.FindControl("Tot1P"), Label)
            '    Tot1P.Text = String.Format("{0:d}", Total1Psum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "SABREDOMESTIC")
            '    Total1Wsum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1W As Label
            '    Tot1W = CType(e.Row.FindControl("Tot1W"), Label)
            '    Tot1W.Text = String.Format("{0:d}", Total1Wsum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "TOTAL")
            '    TotalSum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Total As Label
            '    Total = CType(e.Row.FindControl("Total"), Label)
            '    Total.Text = String.Format("{0:d}", TotalSum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "TYPE_PER")
            '    TotalTypePerSum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim TotType As Label
            '    TotType = CType(e.Row.FindControl("TotType"), Label)
            '    TotType.Text = String.Format("{0:d}", TotalTypePerSum.ToString)
            'End If



            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "AMADEUS_PER")
            '    Total1APersum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1APer As Label
            '    Tot1APer = CType(e.Row.FindControl("Tot1APer"), Label)
            '    Tot1APer.Text = String.Format("{0:d}", Total1APersum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "ABACUS_PER")
            '    Total1BPersum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1BPer As Label
            '    Tot1BPer = CType(e.Row.FindControl("Tot1BPer"), Label)
            '    Tot1BPer.Text = String.Format("{0:d}", Total1BPersum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "GALILEO_PER")
            '    Total1GPersum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1GPer As Label
            '    Tot1GPer = CType(e.Row.FindControl("Tot1GPer"), Label)
            '    Tot1GPer.Text = String.Format("{0:d}", Total1GPersum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "WORLDSPAN_PER")
            '    Total1PPersum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1WPer As Label
            '    Tot1WPer = CType(e.Row.FindControl("Tot1PPer"), Label)
            '    Tot1WPer.Text = String.Format("{0:d}", Total1PPersum.ToString)
            'End If

            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "SABREDOMESTIC_PER")
            '    Total1WPersum += sum
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim Tot1PPer As Label
            '    Tot1PPer = CType(e.Row.FindControl("Tot1WPer"), Label)
            '    Tot1PPer.Text = String.Format("{0:d}", Total1WPersum.ToString)
            'End If



        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MTSRAirLine_ProductivityCompareReport.aspx", False)
    End Sub
    Protected Sub btnFirstYearProductivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirstYearProductivity.Click

        btnSecYearProductivity.CssClass = "headingtabactive"
        btnComparision.CssClass = "headingtabactive"
        btnFirstYearProductivity.CssClass = "headingtab"
        AirlineProductivityCompareSearch(1)
    End Sub
    Protected Sub btnSecYearProductivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSecYearProductivity.Click
        btnComparision.CssClass = "headingtabactive"
        btnFirstYearProductivity.CssClass = "headingtabactive"
        btnSecYearProductivity.CssClass = "headingtab"
        AirlineProductivityCompareSearch(2)
    End Sub

    Protected Sub btnComparision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComparision.Click
        btnFirstYearProductivity.CssClass = "headingtabactive"
        btnSecYearProductivity.CssClass = "headingtabactive"
        btnComparision.CssClass = "headingtab"
        AirlineProductivityCompareSearch(3)
    End Sub
#Region "Code for Paging And sorting."

    Protected Sub grdvAirlineProductivityCom_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvAirlineProductivityCom.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvAirlineProductivityCom_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirlineProductivityCom.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ' ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @ VALUE
                If SortName.Trim().ToUpper = "VALUE" Then
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
                    ' ViewState("Desc") = "FALSE"
                    '@ Added Code For Default descending sorting order on first time  of following Fields      
                    ' @ VALUE
                    If SortName.Trim().ToUpper = "VALUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            If Session("AirLinePrdDataSource") IsNot Nothing Then
                Dim dv As New DataView
                Dim ds As DataSet = CType(Session("AirLinePrdDataSource"), DataSet)
                FooterDataSet = New DataSet
                FooterDataSet = ds

              

                'For i As Integer = 0 To ds.Tables("DETAILS").Rows.Count - 1
                '    For j As Integer = 2 To ds.Tables("DETAILS").Columns.Count - 1
                '        ds.Tables("DETAILS").Rows(i)(j) = ds.Tables("DETAILS").Rows(i)(j).ToString.PadLeft(12, "0")
                '    Next
                'Next

                Dim dsets As New DataSet
                dsets = ds

              
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then

                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else

                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If

                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                SetImageForSorting(grdvAirlineProductivityCom)

                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
    Protected Sub grdvAirlineProductivityCom_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvAirlineProductivityCom.PageIndexChanged
        Try

            'Dim currentPage As Integer = gvCallLog.PageIndex + 1
            'lblError.Text = ""
            'gvCallLog.PageIndex = currentPage
            'If Session("DataSource") IsNot Nothing Then
            '    gvCallLog.DataSource = CType(Session("DataSource"), DataTable)
            '    gvCallLog.DataBind()
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvAirlineProductivityCom_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdvAirlineProductivityCom.PageIndexChanging
        Try
            lblError.Text = ""
            grdvAirlineProductivityCom.PageIndex = e.NewPageIndex
            If Session("AirLinePrdDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("AirLinePrdDataSource"), DataSet)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                'Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                SetImageForSorting(grdvAirlineProductivityCom)
                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
    Private Sub AirlineProductivityCompareSearch2(ByVal ReportType As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            If ReportType = "3" Then
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT> <FYFMONTH></FYFMONTH> <FYFYEAR></FYFYEAR>  <FYTMONTH></FYTMONTH>  <FYTYEAR></FYTYEAR>  <SYFMONTH></SYFMONTH> <SYFYEAR></SYFYEAR> <SYTMONTH></SYTMONTH>  <SYTYEAR></SYTYEAR>  <AIRLINE/> <CITY/>  <AOFFICE/>  <REGION/> <COUNTRY/> <SELECTBY/> <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            Else
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT><FMONTH/> <FYEAR/> <TMONTH/> <TYEAR/>  <AIRLINE/> <CITY/>  <AOFFICE/>  <REGION/> <COUNTRY/> <SELECTBY/> <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            End If

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE").InnerText = drpAirline.SelectedItem.Text
            End If

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedValue
            End If

            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If
            If ReportType = "3" Then
                objInputXml.DocumentElement.SelectSingleNode("FYFMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYFYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTYEAR").InnerText = drpYearTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTYEAR").InnerText = drpYearTo2.SelectedValue

            ElseIf ReportType = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue
            ElseIf ReportType = "2" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo2.SelectedValue

            End If

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SHOWBREAKUP").InnerText = "0"


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

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
            End If
            If ReportType = "3" Then
                objOutputXml = objbzBIDT.AirlineMarketShareComparison(objInputXml)
            Else
                objOutputXml = objbzBIDT.AirlineMarketShareYearWiseData(objInputXml)
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            ' objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''  ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''  TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''  GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = '' /><Errors Status='FALSE'><Error Code='' Description=''/></Errors></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then

                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else

                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.AllowPaging = False
                grdvAirlineProductivityCom.AllowSorting = False
                grdvAirlineProductivityCom.HeaderStyle.ForeColor = Drawing.Color.Black

                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS")
                grdvAirlineProductivityCom.DataBind()

                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            Else
                grdvAirlineProductivityCom.DataSource = Nothing
                grdvAirlineProductivityCom.DataBind()

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

   
    Protected Sub grdvAirlineProductivityCom_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirlineProductivityCom.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            If e.Row.RowType = DataControlRowType.Header Then
                If grdvAirlineProductivityCom.AllowSorting = True Then
                    CType(grvRow.Cells(0).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text
                    CType(grvRow.Cells(7).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text + "%"
                Else
                    grvRow.Cells(0).Text = rdSummOpt.SelectedItem.Text
                    grvRow.Cells(7).Text = rdSummOpt.SelectedItem.Text + "%"
                End If
                'e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
                'e.Row.Cells(7).Text = rdSummOpt.SelectedItem.Text + "%"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bubbleSortAsc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)
        If Clmn <> "VALUE" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If CType(dset.Tables("DETAILS").Rows(j)(Clmn).ToString, Decimal) > CType(dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString, Decimal) Then

                        Dim objOutputXml As New XmlDocument

                        objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''   TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''   GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''  /></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")
                        With objOutputXml.DocumentElement("DETAILS")
                            .Attributes("TYPE").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("VALUE").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("AMADEUS").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("ABACUS").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("GALILEO").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("WORLDSPAN").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("SABREDOMESTIC").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("TOTAL").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("TYPE_PER").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMADEUS_PER").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ABACUS_PER").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("GALILEO_PER").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("WORLDSPAN_PER").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("SABREDOMESTIC_PER").Value = dset.Tables("DETAILS").Rows(j)(13)

                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)


                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("VALUE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTAL").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC_PER").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        Else
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("DETAILS").Rows(j)(Clmn).ToString.Trim, dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString.Trim) > 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''   TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''   GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''  /></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")
                        With objOutputXml.DocumentElement("DETAILS")
                            .Attributes("TYPE").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("VALUE").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("AMADEUS").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("ABACUS").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("GALILEO").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("WORLDSPAN").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("SABREDOMESTIC").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("TOTAL").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("TYPE_PER").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMADEUS_PER").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ABACUS_PER").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("GALILEO_PER").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("WORLDSPAN_PER").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("SABREDOMESTIC_PER").Value = dset.Tables("DETAILS").Rows(j)(13)
                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)


                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("VALUE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTAL").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC_PER").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        End If
    End Sub
    Private Sub bubbleSortDesc(ByRef dset As DataSet, ByVal Clmn As String, ByVal dset2 As DataSet)
        If Clmn <> "VALUE" Then
            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If CType(dset.Tables("DETAILS").Rows(j)(Clmn).ToString, Decimal) < CType(dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString, Decimal) Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''   TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''   GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''  /></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")
                        With objOutputXml.DocumentElement("DETAILS")
                            .Attributes("TYPE").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("VALUE").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("AMADEUS").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("ABACUS").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("GALILEO").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("WORLDSPAN").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("SABREDOMESTIC").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("TOTAL").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("TYPE_PER").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMADEUS_PER").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ABACUS_PER").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("GALILEO_PER").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("WORLDSPAN_PER").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("SABREDOMESTIC_PER").Value = dset.Tables("DETAILS").Rows(j)(13)
                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)


                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("VALUE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTAL").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC_PER").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i

        Else

            Dim i, j As Integer
            Dim SortedDataset As New DataSet
            For i = 0 To dset.Tables("DETAILS").Rows.Count - 2
                For j = 0 To dset.Tables("DETAILS").Rows.Count - 2 - i
                    If String.Compare(dset.Tables("DETAILS").Rows(j)(Clmn).ToString.Trim, dset.Tables("DETAILS").Rows(j + 1)(Clmn).ToString.Trim) < 0 Then
                        Dim objOutputXml As New XmlDocument
                        objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''   TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''   GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''  /></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")
                        With objOutputXml.DocumentElement("DETAILS")
                            .Attributes("TYPE").Value = dset.Tables("DETAILS").Rows(j)(0)
                            .Attributes("VALUE").Value = dset.Tables("DETAILS").Rows(j)(1)
                            .Attributes("AMADEUS").Value = dset.Tables("DETAILS").Rows(j)(2)
                            .Attributes("ABACUS").Value = dset.Tables("DETAILS").Rows(j)(3)
                            .Attributes("GALILEO").Value = dset.Tables("DETAILS").Rows(j)(4)
                            .Attributes("WORLDSPAN").Value = dset.Tables("DETAILS").Rows(j)(5)
                            .Attributes("SABREDOMESTIC").Value = dset.Tables("DETAILS").Rows(j)(6)
                            .Attributes("TOTAL").Value = dset.Tables("DETAILS").Rows(j)(7)
                            .Attributes("TYPE_PER").Value = dset.Tables("DETAILS").Rows(j)(8)
                            .Attributes("AMADEUS_PER").Value = dset.Tables("DETAILS").Rows(j)(9)
                            .Attributes("ABACUS_PER").Value = dset.Tables("DETAILS").Rows(j)(10)
                            .Attributes("GALILEO_PER").Value = dset.Tables("DETAILS").Rows(j)(11)
                            .Attributes("WORLDSPAN_PER").Value = dset.Tables("DETAILS").Rows(j)(12)
                            .Attributes("SABREDOMESTIC_PER").Value = dset.Tables("DETAILS").Rows(j)(13)
                        End With

                        dset.Tables("DETAILS").Rows(j)(0) = dset2.Tables("DETAILS").Rows(j + 1)(0)
                        dset.Tables("DETAILS").Rows(j)(1) = dset2.Tables("DETAILS").Rows(j + 1)(1)
                        dset.Tables("DETAILS").Rows(j)(2) = dset2.Tables("DETAILS").Rows(j + 1)(2)
                        dset.Tables("DETAILS").Rows(j)(3) = dset2.Tables("DETAILS").Rows(j + 1)(3)
                        dset.Tables("DETAILS").Rows(j)(4) = dset2.Tables("DETAILS").Rows(j + 1)(4)
                        dset.Tables("DETAILS").Rows(j)(5) = dset2.Tables("DETAILS").Rows(j + 1)(5)
                        dset.Tables("DETAILS").Rows(j)(6) = dset2.Tables("DETAILS").Rows(j + 1)(6)
                        dset.Tables("DETAILS").Rows(j)(7) = dset2.Tables("DETAILS").Rows(j + 1)(7)
                        dset.Tables("DETAILS").Rows(j)(8) = dset2.Tables("DETAILS").Rows(j + 1)(8)
                        dset.Tables("DETAILS").Rows(j)(9) = dset2.Tables("DETAILS").Rows(j + 1)(9)
                        dset.Tables("DETAILS").Rows(j)(10) = dset2.Tables("DETAILS").Rows(j + 1)(10)
                        dset.Tables("DETAILS").Rows(j)(11) = dset2.Tables("DETAILS").Rows(j + 1)(11)
                        dset.Tables("DETAILS").Rows(j)(12) = dset2.Tables("DETAILS").Rows(j + 1)(12)
                        dset.Tables("DETAILS").Rows(j)(13) = dset2.Tables("DETAILS").Rows(j + 1)(13)


                        dset.Tables("DETAILS").Rows(j + 1)(0) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(1) = objOutputXml.DocumentElement("DETAILS").Attributes("VALUE").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(2) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(3) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(4) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(5) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(6) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(7) = objOutputXml.DocumentElement("DETAILS").Attributes("TOTAL").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(8) = objOutputXml.DocumentElement("DETAILS").Attributes("TYPE_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(9) = objOutputXml.DocumentElement("DETAILS").Attributes("AMADEUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(10) = objOutputXml.DocumentElement("DETAILS").Attributes("ABACUS_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(11) = objOutputXml.DocumentElement("DETAILS").Attributes("GALILEO_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(12) = objOutputXml.DocumentElement("DETAILS").Attributes("WORLDSPAN_PER").Value()
                        dset.Tables("DETAILS").Rows(j + 1)(13) = objOutputXml.DocumentElement("DETAILS").Attributes("SABREDOMESTIC_PER").Value()

                        dset.AcceptChanges()
                    End If
                Next j
            Next i
        End If
    End Sub
    Private Sub AirlineProductivityCompareNewExport(ByVal ReportType)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Try
            If ReportType = "3" Then
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT> <FYFMONTH></FYFMONTH> <FYFYEAR></FYFYEAR>  <GroupTypeID /> <FYTMONTH></FYTMONTH>  <FYTYEAR></FYTYEAR>  <SYFMONTH></SYFMONTH> <SYFYEAR></SYFYEAR> <SYTMONTH></SYTMONTH>  <SYTYEAR></SYTYEAR>  <AIRLINE/> <CITY/>  <AOFFICE/>  <REGION/> <COUNTRY/> <SELECTBY/> <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            Else
                objInputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT><FMONTH/> <FYEAR/> <TMONTH/> <TYEAR/> <GroupTypeID />  <AIRLINE/> <CITY/>  <AOFFICE/>  <REGION/> <COUNTRY/> <SELECTBY/> <SHOWBREAKUP/> <RESPONSIBLESTAFFID/> <LIMITED_TO_REGION/></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_INPUT>")
            End If

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE").InnerText = drpAirline.SelectedItem.Text
            End If

            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAoffice.SelectedValue
            End If

            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = drpRegion.SelectedItem.Text
            End If

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            If ReportType = "3" Then
                objInputXml.DocumentElement.SelectSingleNode("FYFMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYFYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYTYEAR").InnerText = drpYearTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYFYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("SYTYEAR").InnerText = drpYearTo2.SelectedValue

            ElseIf ReportType = "1" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo.SelectedValue
            ElseIf ReportType = "2" Then
                objInputXml.DocumentElement.SelectSingleNode("FMONTH").InnerText = drpMonthFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TMONTH").InnerText = drpMonthTo2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("FYEAR").InnerText = drpYearFrom2.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("TYEAR").InnerText = drpYearTo2.SelectedValue

            End If

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SHOWBREAKUP").InnerText = "0"


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If





            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

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
            End If
            Session("AirLinePrdDataSource") = Nothing
            '@ Reset DataSource If any input Change
            'If ViewState("PrevSearching") Is Nothing Then
            '    ' objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            'Else
            '    Dim objTempInputXml As New XmlDocument
            '    Dim objNodeList As XmlNodeList
            '    objTempInputXml.LoadXml(ViewState("PrevSearching"))
            '    objNodeList = objTempInputXml.DocumentElement.ChildNodes
            '    For Each objNode As XmlNode In objNodeList
            '        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
            '            Session("AirLinePrdDataSource") = Nothing
            '        End If
            '    Next
            'End If


            If ReportType = "3" Then
                objOutputXml = objbzBIDT.AirlineMarketShareComparison(objInputXml)
            Else
                objOutputXml = objbzBIDT.AirlineMarketShareYearWiseData(objInputXml)
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If


            ' objOutputXml.LoadXml("<PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT><DETAILS  TYPE = ''  VALUE = '' AMADEUS =''  ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC ='' TOTAL =''  TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''  GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = '' /><Errors Status='FALSE'><Error Code='' Description=''/></Errors></PR_SEARCH_PR_AIRLINE_MARKETSHARE_COMPARISION_OUTPUT>")

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.DataSource = Nothing
                grdvAirlineProductivityCom.DataBind()

                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer

                '@ Now Export The Data
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAILS")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes("TYPE").Value = ""
                    .Attributes("VALUE").Value = "Total"
                    .Attributes("AMADEUS").Value = grdvAirlineProductivityCom.FooterRow.Cells(1).Text
                    .Attributes("ABACUS").Value = grdvAirlineProductivityCom.FooterRow.Cells(2).Text
                    .Attributes("GALILEO").Value = grdvAirlineProductivityCom.FooterRow.Cells(3).Text
                    .Attributes("WORLDSPAN").Value = grdvAirlineProductivityCom.FooterRow.Cells(4).Text
                    .Attributes("SABREDOMESTIC").Value = grdvAirlineProductivityCom.FooterRow.Cells(5).Text
                    .Attributes("TOTAL").Value = grdvAirlineProductivityCom.FooterRow.Cells(6).Text
                    .Attributes("TYPE_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(7).Text
                    .Attributes("AMADEUS_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(8).Text
                    .Attributes("ABACUS_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(9).Text
                    .Attributes("GALILEO_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(10).Text
                    .Attributes("WORLDSPAN_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(11).Text
                    .Attributes("SABREDOMESTIC_PER").Value = grdvAirlineProductivityCom.FooterRow.Cells(12).Text

                End With
                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel
                'DETAILS() TYPE = '' VALUE = '' AMADEUS =''   ABACUS =''  GALILEO =''  WORLDSPAN =''  SABREDOMESTIC =''  TOTAL =''  TYPE_PER=''  AMADEUS_PER =''  ABACUS_PER =''  GALILEO_PER =''  WORLDSPAN_PER =''  SABREDOMESTIC_PER = ''
                Dim strArray() As String = {rdSummOpt.SelectedItem.Text, "1A", "1B", "1G", "1P", "1W", "TOTAL", rdSummOpt.SelectedItem.Text + "%", "1A%", "1B%", "1G%", "1P%", "1W%"}

                Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}

                objExport.ExportDetails(objOutputXmlExport, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "AirlineProductivityCompare.xls")


            Else
                grdvAirlineProductivityCom.DataSource = Nothing
                grdvAirlineProductivityCom.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            lblError.Text = ""
            ' grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue
            '  grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue - 1
            grdvAirlineProductivityCom.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1

            If Session("AirLinePrdDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("AirLinePrdDataSource"), DataSet)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                SetImageForSorting(grdvAirlineProductivityCom)
                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            End If
            BindControlsForNavigation(grdvAirlineProductivityCom.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            lblError.Text = ""
            ' grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue
            '  grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue - 1
            grdvAirlineProductivityCom.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
            If Session("AirLinePrdDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("AirLinePrdDataSource"), DataSet)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                SetImageForSorting(grdvAirlineProductivityCom)
                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            End If
            BindControlsForNavigation(grdvAirlineProductivityCom.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            lblError.Text = ""
            ' grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue
            '    grdvAirlineProductivityCom.PageIndex = ddlPageNumber.SelectedValue - 1
            grdvAirlineProductivityCom.PageIndex = CInt(ddlPageNumber.SelectedValue) - 1
            If Session("AirLinePrdDataSource") IsNot Nothing Then
                '  Dim dv As New DataView
                Dim ds As DataSet = CType(Session("AirLinePrdDataSource"), DataSet)
                FooterDataSet = New DataSet
                FooterDataSet = ds
                ' Dim dv As DataView
                Dim dsets As New DataSet
                dsets = ds
                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                End If
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "VALUE"
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, "VALUE", dsets)
                    Else
                        bubbleSortDesc(ds, "VALUE", dsets)
                    End If
                Else
                    If ViewState("Desc") = "FALSE" Then
                        bubbleSortAsc(ds, ViewState("SortName"), dsets)
                    Else
                        bubbleSortDesc(ds, ViewState("SortName"), dsets)
                    End If
                End If
                grdvAirlineProductivityCom.DataSource = ds.Tables("DETAILS") 'dv
                grdvAirlineProductivityCom.DataBind()
                SetImageForSorting(grdvAirlineProductivityCom)
                ' @ Code For Totals in Footer
                Dim intRow, IntColno As Integer
                grdvAirlineProductivityCom.FooterRow.Cells(0).Text = "Total"
                For IntColno = 1 To FooterDataSet.Tables("DETAILS").Columns.Count - 2
                    grdvAirlineProductivityCom.FooterRow.Cells(IntColno).Text = 0
                Next
                For intRow = 0 To FooterDataSet.Tables("DETAILS").Rows.Count - 1
                    For IntColno = 2 To FooterDataSet.Tables("DETAILS").Columns.Count - 1
                        Dim sum As Double = FooterDataSet.Tables("DETAILS").Rows(intRow)(IntColno).ToString
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = CType(grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text, Decimal) + sum
                        grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text = String.Format("{0:d}", grdvAirlineProductivityCom.FooterRow.Cells(IntColno - 1).Text.ToString)
                    Next
                Next
                ' @ End ofCode For Totals in Footer
            End If
            BindControlsForNavigation(grdvAirlineProductivityCom.PageCount)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
    End Sub


#End Region
End Class
