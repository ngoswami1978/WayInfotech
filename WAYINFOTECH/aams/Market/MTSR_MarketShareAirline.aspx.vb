Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System
Imports Excel
Imports AAMS.bizShared
Imports System.Drawing

Partial Class Market_MTSR_MarketShareAirline
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim TotalPassivesum As Decimal = 0
    Dim TotalActivesum As Decimal = 0
    Dim TotalSegsum As Decimal = 0
    Dim TotalPercentsum As Decimal = 0
    Const strClass_NAME = "Market_MTSR_MarketShareAirline"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '   killAllXls()

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            lblError.Text = ""
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share Report']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        ' btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            rdSummOpt.Attributes.Add("onclick", "return HideUndideProductivity();")
            drpProductivity.Attributes.Add("onkeyup", "return gotops('drpProductivity');")
            drpProductivity.Attributes.Add("onchange", "return SelectProdutivity();")
            drpProductivity.Attributes.Add("onclick", "return SelectProdutivity();")

            btnExport.Attributes.Add("onclick", "return CheckValidation();")

            If Not IsPostBack Then
                BindAllControl()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Private Sub MarketShareSearch(ByVal ReportType)
    '    '  txtRecordCount.Text = "0"
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim ds As New DataSet
    '    Dim objSecurityXml As New XmlDocument
    '    Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT

    '    Try
    '        objInputXml.LoadXml("<RPT_PR_MARKETSHARECITYCOUNTRYREGION_INPUT><MNTH></MNTH><YR></YR> <REPORTOPTION></REPORTOPTION> </RPT_PR_MARKETSHARECITYCOUNTRYREGION_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("MNTH").InnerText = drpMonthFrom.SelectedValue
    '        objInputXml.DocumentElement.SelectSingleNode("YR").InnerText = drpYearFrom.SelectedValue




    '        If ReportType = 1 Then
    '            objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "City"

    '        ElseIf ReportType = 2 Then
    '            objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "Country"
    '        Else
    '            objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "Region"
    '        End If

    '        objOutputXml = objbzBIDT.MarketShareCityRegionCountryReport(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            If ReportType = 1 Then
    '                Session("MarketShareCity") = objOutputXml.OuterXml
    '                Response.Redirect("../RPSR_ReportShow.aspx?Case=MarketShareCity", False)
    '            ElseIf ReportType = 2 Then

    '                Session("MarketShareCountry") = objOutputXml.OuterXml
    '                Response.Redirect("../RPSR_ReportShow.aspx?Case=MarketShareCountry", False)


    '            Else
    '                Session("MarketShareRegion") = objOutputXml.OuterXml
    '                Response.Redirect("../RPSR_ReportShow.aspx?Case=MarketShareRegion", False)

    '            End If
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    Finally
    '        objInputXml = Nothing
    '        objOutputXml = Nothing
    '    End Try
    'End Sub

    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            Dim i, j As Integer
            drpMonthFrom.SelectedValue = "1"
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub MarketShareExportInExcel(ByVal ReportType As Integer)
        '  txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzBIDT As New AAMS.bizProductivity.bzBIDT
        Dim dSet As New DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objExWorkSheet As Excel.Worksheet
        Dim objExApplication As New Excel.Application
        Dim objExWorkBooks As Excel.Workbooks
        Dim objExWorkBook As Excel.Workbook
        '  Dim xlWorkBook As Excel.Workbook
        Dim strMETHOD_NAME As String = "Market Share Report"

        Dim UserId As Integer = 0
        Try
            objExApplication.DisplayAlerts = False
            objInputXml.LoadXml("<RPT_PR_MARKETSHARECITYCOUNTRYREGION_INPUT><MNTH></MNTH><YR></YR> <REPORTOPTION></REPORTOPTION> <Symbol/><FValue/><TValue/><Employee_ID/><LIMITED_TO_AOFFICE/><LIMITED_TO_REGION/><TBA></TBA><COMP_VERTICAL></COMP_VERTICAL></RPT_PR_MARKETSHARECITYCOUNTRYREGION_INPUT>")

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("MNTH").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YR").InnerText = drpYearFrom.SelectedValue
            If chkTBA.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("TBA").InnerText = "TRUE"
            End If
            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("Employee_ID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If


            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""

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
            End If



            If ReportType = 1 Then
                objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "City"

                If (drpProductivity.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("Symbol").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If

                objInputXml.DocumentElement.SelectSingleNode("FValue").InnerText = txtFrom.Text

                objInputXml.DocumentElement.SelectSingleNode("TValue").InnerText = txtTo.Text


            ElseIf ReportType = 2 Then
                objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "Country"
            Else
                objInputXml.DocumentElement.SelectSingleNode("REPORTOPTION").InnerText = "Region"
            End If

            objOutputXml = objbzBIDT.MarketShareCityRegionCountryReport(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                dSet.ReadXml(objXmlReader)

                'Server.MapPath("~\Template\"

                If ReportType = 1 Then
                    ' Session("MarketShareCity") = objOutputXml.OuterXml
                    ' Response.Redirect("../RPSR_ReportShow.aspx?Case=MarketShareCity", False)
                    Dim objTemplatePath As String
                    'Dim objFile As System.IO.File
                    Dim rowCounter As Integer = 0
                    Dim rowCounterPosForImage As Integer = 0
                    Dim oChart As Excel.Chart
                    Dim MyCharts As Excel.ChartObjects
                    Dim MyCharts1 As Excel.ChartObject

                    '************ End of  Declaration Section *******************************
                    If Not Session("LoginSession") Is Nothing Then
                        UserId = Session("LoginSession").ToString().Split("|")(0)
                    Else
                        lblError.Text = "Session Exipred."
                        Exit Sub
                    End If

                    Dim strFileName As String = "MKT_CITY_SHARE" + UserId.ToString + ".xls"

                    If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                        File.Delete(Server.MapPath("~\Template\" + strFileName))
                    End If

                    'objExApplication.Workbooks.Open(objTemplatePath)
                    'objExWorkSheet = objExApplication.ActiveSheet
                    'objExApplication.ActiveWorkbook.SaveCopyAs(Server.MapPath("~") + "\Template\" + "MKT_CITY_ SHARE" + UserId.ToString + ".xls")
                    'objExApplication.Workbooks.Close()
                    'objTemplatePath = Server.MapPath("~") + "\Template\" + "MKT_CITY_ SHARE" + UserId.ToString + ".xls"
                    'objExApplication.Workbooks.Open(objTemplatePath)
                    'objExWorkSheet = objExApplication.ActiveSheet
                    '  objExWorkSheet = objExApplication.Worksheets(1)



                    objTemplatePath = Server.MapPath("~\Template\MKT_CITY_SHARE.xls")

                    objExWorkBooks = objExApplication.Workbooks
                    objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                    objExWorkSheet = objExApplication.ActiveSheet



                    rowCounter = 1

                    'objExApplication.Visible = True
                    Dim Regionno As Integer = 0
                    For Each dtrow As DataRow In dSet.Tables("REGION").Rows
                        '@ New code Added
                        Regionno += 1
                        objExWorkSheet.Cells(rowCounter, 1) = dtrow("NAME").ToString().ToString()
                        objExWorkSheet.Range("A" + rowCounter.ToString(), "P" + rowCounter.ToString()).RowHeight = 33
                        objExWorkSheet.Range("A" + rowCounter.ToString(), "P" + rowCounter.ToString()).Font.Size = 16
                        objExWorkSheet.Range("A" + rowCounter.ToString(), "P" + rowCounter.ToString()).Font.Bold = True
                        '@ New code Added

                        rowCounter += 1

                        rowCounterPosForImage = rowCounter


                        '@ Prev Code 
                        'objExWorkSheet.Cells(rowCounter, 1) = dtrow("NAME").ToString() + "  REGION"
                        'objExWorkSheet.Cells(rowCounter, 2) = "CRS"
                        'objExWorkSheet.Cells(rowCounter, 3) = dSet.Tables("HEADER").Rows(0)("Field13").ToString()
                        'objExWorkSheet.Cells(rowCounter, 4) = dSet.Tables("HEADER").Rows(0)("Field12").ToString()
                        'objExWorkSheet.Cells(rowCounter, 5) = dSet.Tables("HEADER").Rows(0)("Field11").ToString()
                        'objExWorkSheet.Cells(rowCounter, 6) = dSet.Tables("HEADER").Rows(0)("Field10").ToString()
                        'objExWorkSheet.Cells(rowCounter, 7) = dSet.Tables("HEADER").Rows(0)("Field9").ToString()
                        'objExWorkSheet.Cells(rowCounter, 8) = dSet.Tables("HEADER").Rows(0)("Field8").ToString()
                        'objExWorkSheet.Cells(rowCounter, 9) = dSet.Tables("HEADER").Rows(0)("Field7").ToString()
                        'objExWorkSheet.Cells(rowCounter, 10) = dSet.Tables("HEADER").Rows(0)("Field6").ToString()
                        'objExWorkSheet.Cells(rowCounter, 11) = dSet.Tables("HEADER").Rows(0)("Field5").ToString()
                        'objExWorkSheet.Cells(rowCounter, 12) = dSet.Tables("HEADER").Rows(0)("Field4").ToString()
                        'objExWorkSheet.Cells(rowCounter, 13) = dSet.Tables("HEADER").Rows(0)("Field3").ToString()
                        'objExWorkSheet.Cells(rowCounter, 14) = dSet.Tables("HEADER").Rows(0)("Field2").ToString()
                        'objExWorkSheet.Cells(rowCounter, 15) = dSet.Tables("HEADER").Rows(0)("Field1").ToString()
                        'objExWorkSheet.Cells(rowCounter, 16) = dSet.Tables("HEADER").Rows(0)("Field14").ToString()
                        '@ Prev Code 

                        '@ New Code Added
                        Dim stringArrayRegHeader(0, 15) As Object
                        Dim row As Integer = 0
                        stringArrayRegHeader(row, 0) = dtrow("NAME").ToString() + "  REGION"
                        stringArrayRegHeader(row, 1) = "CRS"
                        stringArrayRegHeader(row, 2) = dSet.Tables("HEADER").Rows(0)("Field13").ToString()
                        stringArrayRegHeader(row, 3) = dSet.Tables("HEADER").Rows(0)("Field12").ToString()
                        stringArrayRegHeader(row, 4) = dSet.Tables("HEADER").Rows(0)("Field11").ToString()
                        stringArrayRegHeader(row, 5) = dSet.Tables("HEADER").Rows(0)("Field10").ToString()
                        stringArrayRegHeader(row, 6) = dSet.Tables("HEADER").Rows(0)("Field9").ToString()
                        stringArrayRegHeader(row, 7) = dSet.Tables("HEADER").Rows(0)("Field8").ToString()
                        stringArrayRegHeader(row, 8) = dSet.Tables("HEADER").Rows(0)("Field7").ToString()
                        stringArrayRegHeader(row, 9) = dSet.Tables("HEADER").Rows(0)("Field6").ToString()
                        stringArrayRegHeader(row, 10) = dSet.Tables("HEADER").Rows(0)("Field5").ToString()
                        stringArrayRegHeader(row, 11) = dSet.Tables("HEADER").Rows(0)("Field4").ToString()
                        stringArrayRegHeader(row, 12) = dSet.Tables("HEADER").Rows(0)("Field3").ToString()
                        stringArrayRegHeader(row, 13) = dSet.Tables("HEADER").Rows(0)("Field2").ToString()
                        stringArrayRegHeader(row, 14) = dSet.Tables("HEADER").Rows(0)("Field1").ToString()
                        stringArrayRegHeader(row, 15) = dSet.Tables("HEADER").Rows(0)("Field14").ToString()
                        Dim usedRange As String = "A" + rowCounter.ToString() + ":" + "P" + rowCounter.ToString()
                        objExWorkSheet.Range(usedRange).Value2 = stringArrayRegHeader
                        '@ New Code Added

                        objExWorkSheet.Range("A" + rowCounter.ToString(), "P" + rowCounter.ToString()).Font.Bold = True
                        objExWorkSheet.Range("P" + rowCounter.ToString(), "P" + rowCounter.ToString()).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                        objExWorkSheet.Range("B" + rowCounter.ToString(), "O" + rowCounter.ToString()).Columns.AutoFit()
                        objExWorkSheet.Range("A" + rowCounter.ToString(), "P" + rowCounter.ToString()).Borders.LineStyle = True

                        rowCounter += 1

                        ' For Region
                        Dim drows() As DataRow = dSet.Tables("REGIONDETAILS").Select("REGION_Id=" + dtrow("REGION_Id").ToString, "POSITION")

                        '@ New Code Added
                        Dim stringArrayRegDetails(drows.Length - 1, 15) As Object
                        row = 0
                        For Each dtrow2 As DataRow In drows

                            stringArrayRegDetails(row, 0) = ""
                            stringArrayRegDetails(row, 1) = dtrow2("CRS").ToString()
                            stringArrayRegDetails(row, 2) = dtrow2("FIELD13").ToString()
                            stringArrayRegDetails(row, 3) = dtrow2("FIELD12").ToString()
                            stringArrayRegDetails(row, 4) = dtrow2("FIELD11").ToString()
                            stringArrayRegDetails(row, 5) = dtrow2("FIELD10").ToString()
                            stringArrayRegDetails(row, 6) = dtrow2("FIELD9").ToString()
                            stringArrayRegDetails(row, 7) = dtrow2("FIELD8").ToString()
                            stringArrayRegDetails(row, 8) = dtrow2("FIELD7").ToString()
                            stringArrayRegDetails(row, 9) = dtrow2("FIELD6").ToString()
                            stringArrayRegDetails(row, 10) = dtrow2("FIELD5").ToString()
                            stringArrayRegDetails(row, 11) = dtrow2("FIELD4").ToString()
                            stringArrayRegDetails(row, 12) = dtrow2("FIELD3").ToString()
                            stringArrayRegDetails(row, 13) = dtrow2("FIELD2").ToString()
                            stringArrayRegDetails(row, 14) = dtrow2("FIELD1").ToString()

                            stringArrayRegDetails(row, 15) = dtrow2("SEGMANTTOTAL").ToString()
                            rowCounter += 1
                            row += 1
                        Next
                        usedRange = "A" + (rowCounter - 5).ToString() + ":" + "P" + (rowCounter - 1).ToString()
                        objExWorkSheet.Range(usedRange).Value2 = stringArrayRegDetails
                        objExWorkSheet.Range(usedRange.ToString()).Borders.LineStyle = True
                        '@ New Code Added

                        '@ Prev Code
                        'For Each dtrow2 As DataRow In drows
                        '   'objExWorkSheet.Cells(rowCounter, 1) = dtrow2("NAME").ToString
                        '    objExWorkSheet.Cells(rowCounter, 2) = dtrow2("CRS").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 3) = dtrow2("FIELD13").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 4) = dtrow2("FIELD12").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 5) = dtrow2("FIELD11").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 6) = dtrow2("FIELD10").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 7) = dtrow2("FIELD9").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 8) = dtrow2("FIELD8").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 9) = dtrow2("FIELD7").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 10) = dtrow2("FIELD6").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 11) = dtrow2("FIELD5").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 12) = dtrow2("FIELD4").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 13) = dtrow2("FIELD3").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 14) = dtrow2("FIELD2").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 15) = dtrow2("FIELD1").ToString()
                        '    objExWorkSheet.Cells(rowCounter, 16) = dtrow2("SEGMANTTOTAL").ToString()
                        '    rowCounter += 1
                        'Next
                        '@ Prev Code

                        objExWorkSheet.Range("B" + (rowCounter - 5).ToString(), "B" + (rowCounter - 1).ToString()).Font.Bold = True
                        'add a  chart for total comparision
                        MyCharts = objExWorkSheet.ChartObjects
                        'MyCharts1 = MyCharts.Add(15 * 60, (12.75 * rowCounterPosForImage - 7), 300, 100)
                        MyCharts1 = MyCharts.Add(15 * 46, (12.75 * rowCounterPosForImage - 12.75 + 21 * Regionno), 300, 76)
                        ' MyCharts1 = MyCharts.Add(15 * 50, (objExWorkSheet.Rows.RowHeight(rowCounterPosForImage)), 450, 120)
                        oChart = MyCharts1.Chart
                        With oChart


                            Dim chartRange As Excel.Range
                            chartRange = objExWorkSheet.Range("=CITY!$B$" + (rowCounterPosForImage).ToString + ":$O$" + (rowCounterPosForImage + 5).ToString())
                            .SetSourceData(chartRange)
                            .PlotBy = Excel.XlRowCol.xlRows
                            .ChartType = Excel.XlChartType.xlLineMarkers
                            ' .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
                            .HasLegend = True
                            .HasTitle = True
                            .ChartTitle.Text = dtrow("NAME").ToString()
                            .ChartTitle.Font.Bold = True
                            .HasDataTable = False
                            oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 2
                            oChart.PlotArea.Height = MyCharts1.Height - 10
                            .ChartArea.Font.Size = 8
                            .PlotArea.Top = 10
                            .PlotArea.Left = 5
                            .Legend.Font.Size = 8
                            .Legend.Border.LineStyle = True
                            .Legend.Border.LineStyle = Excel.XlLineStyle.xlContinuous
                            ' oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 2
                            '  oChart.PlotArea.Height = MyCharts1.Height - 10
                            oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 18 ' Change on 19-07-10
                            oChart.PlotArea.Height = MyCharts1.Height - 1 ' Change on 19-07-10
                            oChart.PlotArea.Border.LineStyle = True
                            oChart.PlotArea.Border.LineStyle = Excel.XlLineStyle.xlContinuous
                            oChart.PlotArea.Interior.Color = RGB(192, 192, 192)
                            'For Each objexcelseries As Excel.Series In .SeriesCollection
                            '    objexcelseries.Border.LineStyle = Excel.xlNone
                            '    .Shadow = False
                            'Next

                            oChart.Refresh()
                            ' PLOT AREA 

                        End With
                        rowCounter += 2

                        'rowCounter += 4

                        '  For City

                        Dim drows2() As DataRow = dSet.Tables("CITY").Select("REGION_Id=" + dtrow(0).ToString)
                        For Each dtrow3 As DataRow In drows2
                            rowCounterPosForImage = rowCounter


                            '@ New Code Added
                            Dim stringArrayCityHeader(0, 15) As Object
                            row = 0
                            stringArrayCityHeader(row, 0) = "CITY"
                            stringArrayCityHeader(row, 1) = "CRS"
                            stringArrayCityHeader(row, 2) = dSet.Tables("HEADER").Rows(0)("Field13").ToString()
                            stringArrayCityHeader(row, 3) = dSet.Tables("HEADER").Rows(0)("Field12").ToString()
                            stringArrayCityHeader(row, 4) = dSet.Tables("HEADER").Rows(0)("Field11").ToString()
                            stringArrayCityHeader(row, 5) = dSet.Tables("HEADER").Rows(0)("Field10").ToString()
                            stringArrayCityHeader(row, 6) = dSet.Tables("HEADER").Rows(0)("Field9").ToString()
                            stringArrayCityHeader(row, 7) = dSet.Tables("HEADER").Rows(0)("Field8").ToString()
                            stringArrayCityHeader(row, 8) = dSet.Tables("HEADER").Rows(0)("Field7").ToString()
                            stringArrayCityHeader(row, 9) = dSet.Tables("HEADER").Rows(0)("Field6").ToString()
                            stringArrayCityHeader(row, 10) = dSet.Tables("HEADER").Rows(0)("Field5").ToString()
                            stringArrayCityHeader(row, 11) = dSet.Tables("HEADER").Rows(0)("Field4").ToString()
                            stringArrayCityHeader(row, 12) = dSet.Tables("HEADER").Rows(0)("Field3").ToString()
                            stringArrayCityHeader(row, 13) = dSet.Tables("HEADER").Rows(0)("Field2").ToString()
                            stringArrayCityHeader(row, 14) = dSet.Tables("HEADER").Rows(0)("Field1").ToString()
                            stringArrayCityHeader(row, 15) = dSet.Tables("HEADER").Rows(0)("Field14").ToString()
                            stringArrayCityHeader(0, 15) = dSet.Tables("HEADER").Rows(0)(14).ToString()
                            usedRange = "A" + rowCounter.ToString() + ":" + "P" + rowCounter.ToString()
                            objExWorkSheet.Range(usedRange).Value2 = stringArrayCityHeader


                            '@ New Code Added

                            '@Prev Code
                            'objExWorkSheet.Cells(rowCounter, 1) = "CITY"
                            'objExWorkSheet.Cells(rowCounter, 2) = "CRS"
                            'objExWorkSheet.Cells(rowCounter, 3) = dSet.Tables("HEADER").Rows(0)("Field13").ToString()
                            'objExWorkSheet.Cells(rowCounter, 4) = dSet.Tables("HEADER").Rows(0)("Field12").ToString()
                            'objExWorkSheet.Cells(rowCounter, 5) = dSet.Tables("HEADER").Rows(0)("Field11").ToString()
                            'objExWorkSheet.Cells(rowCounter, 6) = dSet.Tables("HEADER").Rows(0)("Field10").ToString()
                            'objExWorkSheet.Cells(rowCounter, 7) = dSet.Tables("HEADER").Rows(0)("Field9").ToString()
                            'objExWorkSheet.Cells(rowCounter, 8) = dSet.Tables("HEADER").Rows(0)("Field8").ToString()
                            'objExWorkSheet.Cells(rowCounter, 9) = dSet.Tables("HEADER").Rows(0)("Field7").ToString()
                            'objExWorkSheet.Cells(rowCounter, 10) = dSet.Tables("HEADER").Rows(0)("Field6").ToString()
                            'objExWorkSheet.Cells(rowCounter, 11) = dSet.Tables("HEADER").Rows(0)("Field5").ToString()
                            'objExWorkSheet.Cells(rowCounter, 12) = dSet.Tables("HEADER").Rows(0)("Field4").ToString()
                            'objExWorkSheet.Cells(rowCounter, 13) = dSet.Tables("HEADER").Rows(0)("Field3").ToString()
                            'objExWorkSheet.Cells(rowCounter, 14) = dSet.Tables("HEADER").Rows(0)("Field2").ToString()
                            'objExWorkSheet.Cells(rowCounter, 15) = dSet.Tables("HEADER").Rows(0)("Field1").ToString()

                            ' objExWorkSheet.Cells(rowCounter, 16) = dSet.Tables("HEADER").Rows(0)("Field14").ToString()
                            '@Prev Code


                            objExWorkSheet.Range("A" + (rowCounter).ToString(), "P" + (rowCounter).ToString()).Font.Bold = True
                            objExWorkSheet.Range("P" + rowCounter.ToString(), "P" + rowCounter.ToString()).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

                            objExWorkSheet.Range("B" + (rowCounter).ToString(), "O" + (rowCounter).ToString()).Columns.AutoFit()
                            objExWorkSheet.Range("A" + (rowCounter).ToString(), "P" + (rowCounter).ToString()).Borders.LineStyle = True

                            rowCounter += 1

                            Dim drows3() As DataRow = dSet.Tables("CITYDETAILS").Select("CITY_Id=" + dtrow3("CITY_Id").ToString, "POSITION")
                            '@ new Code Added

                            Dim stringArrayCityDetails(drows3.Length - 1, 15) As Object
                            row = 0
                            For Each dtrow4 As DataRow In drows3
                                stringArrayCityDetails(row, 0) = dtrow3("NAME").ToString()
                                stringArrayCityDetails(row, 1) = dtrow4("CRS").ToString()
                                stringArrayCityDetails(row, 2) = dtrow4("FIELD13").ToString()
                                stringArrayCityDetails(row, 3) = dtrow4("FIELD12").ToString()
                                stringArrayCityDetails(row, 4) = dtrow4("FIELD11").ToString()
                                stringArrayCityDetails(row, 5) = dtrow4("FIELD10").ToString()
                                stringArrayCityDetails(row, 6) = dtrow4("FIELD9").ToString()
                                stringArrayCityDetails(row, 7) = dtrow4("FIELD8").ToString()
                                stringArrayCityDetails(row, 8) = dtrow4("FIELD7").ToString()
                                stringArrayCityDetails(row, 9) = dtrow4("FIELD6").ToString()
                                stringArrayCityDetails(row, 10) = dtrow4("FIELD5").ToString()
                                stringArrayCityDetails(row, 11) = dtrow4("FIELD4").ToString()
                                stringArrayCityDetails(row, 12) = dtrow4("FIELD3").ToString()
                                stringArrayCityDetails(row, 13) = dtrow4("FIELD2").ToString()
                                stringArrayCityDetails(row, 14) = dtrow4("FIELD1").ToString()
                                stringArrayCityDetails(row, 15) = dtrow4("SEGMANTTOTAL").ToString()
                                rowCounter += 1
                                row += 1
                            Next
                            usedRange = "A" + (rowCounter - 5).ToString() + ":" + "P" + (rowCounter - 1).ToString()
                            objExWorkSheet.Range(usedRange).Value2 = stringArrayCityDetails
                            objExWorkSheet.Range(usedRange.ToString()).Borders.LineStyle = True
                            '@ new Code Added



                            '@ Prev Code
                            'For Each dtrow4 As DataRow In drows3
                            '    objExWorkSheet.Cells(rowCounter, 1) = dtrow3("NAME").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 2) = dtrow4("CRS").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 3) = dtrow4("FIELD13").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 4) = dtrow4("FIELD12").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 5) = dtrow4("FIELD11").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 6) = dtrow4("FIELD10").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 7) = dtrow4("FIELD9").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 8) = dtrow4("FIELD8").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 9) = dtrow4("FIELD7").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 10) = dtrow4("FIELD6").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 11) = dtrow4("FIELD5").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 12) = dtrow4("FIELD4").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 13) = dtrow4("FIELD3").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 14) = dtrow4("FIELD2").ToString()
                            '    objExWorkSheet.Cells(rowCounter, 15) = dtrow4("FIELD1").ToString()

                            '    objExWorkSheet.Cells(rowCounter, 16) = dtrow4("SEGMANTTOTAL").ToString()
                            '    rowCounter += 1

                            'Next
                            '@ Prev Code


                            objExWorkSheet.Range("B" + (rowCounter - 5).ToString(), "B" + (rowCounter - 1).ToString()).Font.Bold = True
                            '  rowCounter += 1
                            'rowCounter += 4
                            rowCounter += 2
                            ''add a  chart for total comparison
                            MyCharts = objExWorkSheet.ChartObjects
                            'MyCharts1 = MyCharts.Add(15 * 60, (12.75 * rowCounterPosForImage - 7), 300, 100)
                            MyCharts1 = MyCharts.Add(15 * 46, (12.75 * rowCounterPosForImage - 12.75 + 21 * Regionno), 300, 76)
                            oChart = MyCharts1.Chart
                            With oChart
                                Dim chartRange As Excel.Range
                                chartRange = objExWorkSheet.Range("=CITY!$B$" + (rowCounterPosForImage).ToString + ":$O$" + (rowCounterPosForImage + 5).ToString())
                                .SetSourceData(chartRange)
                                .PlotBy = Excel.XlRowCol.xlRows
                                .ChartType = Excel.XlChartType.xlLineMarkers
                                '.ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowValue)
                                .HasLegend = True
                                .HasTitle = True
                                .ChartTitle.Text = dtrow3("NAME").ToString()
                                .ChartTitle.Font.Bold = True
                                .HasDataTable = False
                                oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 2
                                oChart.PlotArea.Height = MyCharts1.Height - 10
                                .ChartArea.Font.Size = 8
                                .PlotArea.Top = 10
                                .PlotArea.Left = 5
                                .Legend.Font.Size = 8
                                .Legend.Border.LineStyle = True
                                .Legend.Border.LineStyle = Excel.XlLineStyle.xlContinuous
                                'oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 2
                                ' oChart.PlotArea.Height = MyCharts1.Height - 15 
                                oChart.PlotArea.Width = MyCharts1.Width - oChart.Legend.Width - 18 ' Change on 19-07-10
                                oChart.PlotArea.Height = MyCharts1.Height - 1 ' Change on 19-07-10
                                oChart.PlotArea.Border.LineStyle = True
                                oChart.PlotArea.Border.LineStyle = Excel.XlLineStyle.xlContinuous
                                oChart.PlotArea.Interior.Color = RGB(192, 192, 192)
                                oChart.Refresh()
                                ' PLOT AREA 

                            End With
                        Next
                        '  rowCounter += 2
                    Next

                    objExWorkSheet.Range("A" + (1).ToString(), "A" + (rowCounter + 10).ToString()).Columns.AutoFit()
                    objExWorkSheet.Range("P" + (1).ToString(), "P" + (rowCounter + 10).ToString()).WrapText = True
                    'objExApplication.ActiveWorkbook.Save()
                    'objExApplication.Workbooks.Close()

                    objTemplatePath = Server.MapPath("~\Template\" + strFileName)
                    strFileName = objTemplatePath
                    objExWorkBook.SaveAs(strFileName)
                    'objExWorkBook.SaveAs(objTemplatePath)
                    objExWorkBook.Close()
                    objExWorkBooks.Close()
                    objExApplication.Quit()

                    Dim filePath As String = objTemplatePath
                    Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                    If targetFile.Exists Then
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                        Response.AddHeader("Content-Length", targetFile.Length.ToString)
                        Response.ContentType = "excel"
                        Response.WriteFile(targetFile.FullName)
                    End If

                ElseIf ReportType = 2 Then

                    Dim objTemplatePath As String
                    'Dim objFile As System.IO.File
                    Dim rowCounter As Integer = 0
                    '************ End of  Declaration Section *******************************
                    If Not Session("LoginSession") Is Nothing Then
                        UserId = Session("LoginSession").ToString().Split("|")(0)
                    Else
                        lblError.Text = "Session Exipred."
                        Exit Sub
                    End If

                    Dim strFileName As String = "MKTCountry_SHARE" + UserId.ToString + ".xls"

                    If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                        File.Delete(Server.MapPath("~\Template\" + strFileName))
                    End If

                    objTemplatePath = Server.MapPath("~\Template\MKTCountry_SHARE.xls")



                    'objExApplication.Workbooks.Open(objTemplatePath)
                    'objExWorkSheet = objExApplication.ActiveSheet
                    'objExApplication.ActiveWorkbook.SaveCopyAs(Server.MapPath("~") + "\Template\" + "MKTCountry_ SHARE" + UserId.ToString + ".xls")

                    'objExApplication.Workbooks.Close()

                    'objTemplatePath = Server.MapPath("~") + "\Template\" + "MKTCountry_ SHARE" + UserId.ToString + ".xls"

                    'objExApplication.Workbooks.Open(objTemplatePath)

                    'objExWorkSheet = objExApplication.ActiveSheet


                    objExWorkBooks = objExApplication.Workbooks
                    objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                    objExWorkSheet = objExApplication.ActiveSheet

                    rowCounter = 2

                    For Each dtrow As DataRow In dSet.Tables("DETAILS").Rows

                        '<COUNTRYBREAKUP COUNTRY="Bangladesh" BUDGET="87276" ACTUAL="55827" DIFFERENCE="31449" CxlationRate="76.51" AVERAGEPERDAY="1993" per_Changeover_LastYear="14.07" 

                        'DAYSLEFT="0" SEGMENTSREQUIREDPERDAY="" PROJECTION="" TILLDATE="28/02/2007" /> 

                        objExWorkSheet.Cells(rowCounter, 6) = dtrow("COUNTRY").ToString()
                        objExWorkSheet.Cells(rowCounter, 7) = dtrow("AMADEUS").ToString()
                        objExWorkSheet.Cells(rowCounter, 8) = dtrow("ABACUS").ToString()
                        objExWorkSheet.Cells(rowCounter, 9) = dtrow("GALILEO").ToString()
                        objExWorkSheet.Cells(rowCounter, 10) = dtrow("WORLDSPAN").ToString()
                        objExWorkSheet.Cells(rowCounter, 11) = dtrow("SABREDOMESTIC").ToString()
                        objExWorkSheet.Cells(rowCounter, 12) = dtrow("TOTAL").ToString()
                        rowCounter += 1
                    Next

                    objExWorkSheet.Cells(rowCounter, 6) = "Total"
                    objExWorkSheet.Cells(rowCounter, 7) = "=SUM(G2:G" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 8) = "=SUM(H2:H" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 9) = "=SUM(I2:I" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 10) = "=SUM(J2:J" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 11) = "=SUM(K2:K" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 12) = "=SUM(L2:L" & (rowCounter - 1) & ")"

                    '   Auto fit the columns
                    objExWorkSheet.Columns.AutoFit()

                    '     Generating the graph
                    'Dim oChart As Excel.Chart
                    'Dim MyCharts As Excel.ChartObjects
                    'Dim MyCharts1 As Excel.ChartObject

                    ' oChart = objExWorkSheet.ChartObjects(0)

                    'add a pie chart for total comparison
                    'MyCharts = objExWorkSheet.ChartObjects
                    'MyCharts1 = MyCharts.Add(12, 15 * 70, 285, 150)
                    'oChart = MyCharts1.Chart
                    'With oChart
                    '    Dim chartRange As Excel.Range
                    '    chartRange = objExWorkSheet.Range("COUNTRY!$F$1:$K$1,COUNTRY!$F$2:$K$2")
                    '    .SetSourceData(chartRange)
                    '    .PlotBy = Excel.XlRowCol.xlRows
                    '    .ChartType = Excel.XlChartType.xl3DPieExploded
                    '    .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowLabelAndPercent)
                    '    .HasLegend = False
                    '    .HasTitle = True
                    '    .ChartTitle.Text = dSet.Tables("DETAILS").Rows(0)(0).ToString
                    '    .ChartTitle.Font.Bold = True
                    '    .HasDataTable = True
                    '    .PlotArea.Width = 280
                    '    .PlotArea.Height = 300


                    'End With

                    'MyCharts = objExWorkSheet.ChartObjects
                    'MyCharts1 = MyCharts.Add(12, 15 * 140, 285, 150)
                    'oChart = MyCharts1.Chart
                    'With oChart
                    '    Dim chartRange As Excel.Range
                    '    chartRange = objExWorkSheet.Range("COUNTRY!$F$1:$K$1,COUNTRY!$F$3:$K$3")
                    '    .SetSourceData(chartRange)
                    '    .PlotBy = Excel.XlRowCol.xlRows

                    '    .ChartType = Excel.XlChartType.xl3DPieExploded
                    '    .ApplyDataLabels(Excel.XlDataLabelsType.xlDataLabelsShowLabelAndPercent)

                    '    .HasLegend = False
                    '    .HasTitle = True
                    '    .ChartTitle.Text = dSet.Tables("DETAILS").Rows(1)(0).ToString
                    '    .ChartTitle.Font.Bold = True
                    '    .HasDataTable = True
                    '    .PlotArea.Width = 280
                    '    .PlotArea.Height = 300

                    'End With


                    'objExApplication.ActiveWorkbook.Save()
                    'objExApplication.Workbooks.Close()
                    objTemplatePath = Server.MapPath("~\Template\" + strFileName)
                    objExWorkBook.SaveAs(objTemplatePath)
                    objExWorkBook.Close()
                    objExWorkBooks.Close()
                    objExApplication.Quit()

                    Dim filePath As String = objTemplatePath ' Server.MapPath(specify the file path on server here) 
                    Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                    If targetFile.Exists Then
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                        Response.AddHeader("Content-Length", targetFile.Length.ToString)
                        Response.ContentType = "excel"
                        Response.WriteFile(targetFile.FullName)
                    End If

                Else
                    Dim objTemplatePath As String
                    ' Dim objFile As System.IO.File
                    Dim rowCounter As Integer = 0
                    '************ End of  Declaration Section *******************************
                    If Not Session("LoginSession") Is Nothing Then
                        UserId = Session("LoginSession").ToString().Split("|")(0)
                    Else
                        lblError.Text = "Session Exipred."
                        Exit Sub
                    End If

                    Dim strFileName As String = "MKT_REGION_SHARE" + UserId.ToString + ".xls"

                    If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                        File.Delete(Server.MapPath("~\Template\" + strFileName))
                    End If
                    objTemplatePath = Server.MapPath("~\Template\MKT_REGION_SHARE.xls")

                    '  objExApplication.Workbooks.Open(objTemplatePath)


                    'objExWorkSheet = objExApplication.ActiveSheet

                    'objExApplication.ActiveWorkbook.SaveCopyAs(Server.MapPath("~") + "\Template\" + "MKT_REGION_ SHARE" + UserId.ToString + ".xls")

                    'objExApplication.Workbooks.Close()

                    'objTemplatePath = Server.MapPath("~") + "\Template\" + "MKT_REGION_ SHARE" + UserId.ToString + ".xls"

                    'objExApplication.Workbooks.Open(objTemplatePath)

                    'objExWorkSheet = objExApplication.ActiveSheet

                    objExWorkBooks = objExApplication.Workbooks
                    objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                    objExWorkSheet = objExApplication.ActiveSheet


                    rowCounter = 2

                    For Each dtrow As DataRow In dSet.Tables("DETAILS").Rows

                        '<COUNTRYBREAKUP COUNTRY="Bangladesh" BUDGET="87276" ACTUAL="55827" DIFFERENCE="31449" CxlationRate="76.51" AVERAGEPERDAY="1993" per_Changeover_LastYear="14.07" 

                        'DAYSLEFT="0" SEGMENTSREQUIREDPERDAY="" PROJECTION="" TILLDATE="28/02/2007" /> 

                        objExWorkSheet.Cells(rowCounter, 6) = dtrow("REGION").ToString()
                        objExWorkSheet.Cells(rowCounter, 7) = dtrow("AMADEUS").ToString()
                        objExWorkSheet.Cells(rowCounter, 8) = dtrow("ABACUS").ToString()
                        objExWorkSheet.Cells(rowCounter, 9) = dtrow("GALILEO").ToString()
                        objExWorkSheet.Cells(rowCounter, 10) = dtrow("WORLDSPAN").ToString()
                        objExWorkSheet.Cells(rowCounter, 11) = dtrow("SABREDOMESTIC").ToString()
                        objExWorkSheet.Cells(rowCounter, 12) = dtrow("TOTAL").ToString()
                        rowCounter += 1
                    Next

                    objExWorkSheet.Cells(rowCounter, 6) = "Total"
                    objExWorkSheet.Cells(rowCounter, 7) = "=SUM(G2:G" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 8) = "=SUM(H2:H" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 9) = "=SUM(I2:I" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 10) = "=SUM(J2:J" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 11) = "=SUM(K2:K" & (rowCounter - 1) & ")"
                    objExWorkSheet.Cells(rowCounter, 12) = "=SUM(L2:L" & (rowCounter - 1) & ")"


                    'objExApplication.ActiveWorkbook.Save()
                    'objExApplication.Workbooks.Close()

                    objTemplatePath = Server.MapPath("~\Template\" + strFileName)
                    objExWorkBook.SaveAs(objTemplatePath)
                    objExWorkBook.Close()
                    objExWorkBooks.Close()
                    objExApplication.Quit()

                    Dim filePath As String = objTemplatePath
                    Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)
                    If targetFile.Exists Then
                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + targetFile.Name)
                        Response.AddHeader("Content-Length", targetFile.Length.ToString)
                        Response.ContentType = "excel"
                        Response.WriteFile(targetFile.FullName)
                    End If
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, ex)
            'objExApplication.Workbooks.Close()
        Finally
            'objInputXml = Nothing
            'objOutputXml = Nothing
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            'objExWorkSheet = Nothing
            'objExApplication = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            Try
                objInputXml = Nothing
                objOutputXml = Nothing
                If objExWorkSheet IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkSheet)
                End If

                If objExWorkBook IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBook)
                End If
                If objExWorkBooks IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkBooks)
                End If
                If objExApplication IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                End If
                'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
                objExWorkSheet = Nothing
                objExWorkBook = Nothing
                objExWorkBooks = Nothing
                objExApplication = Nothing
                GC.Collect()
                'GC.WaitForPendingFinalizers()
            Catch ex As Exception
                bzShared.LogWrite(strClass_NAME, strMETHOD_NAME, ex)
            End Try

        End Try
    End Sub
    Private Sub killAllXls()
        'Try
        '    For Each pr As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("EXCEL")
        '        pr.Kill()
        '    Next
        'Catch ex As System.Runtime.InteropServices.COMException
        '    lblError.Text = ex.Message
        'End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If rdSummOpt.SelectedValue = "1" Then
                MarketShareExportInExcel(1)
            ElseIf rdSummOpt.SelectedValue = "2" Then
                MarketShareExportInExcel(2)
            ElseIf rdSummOpt.SelectedValue = "3" Then
                MarketShareExportInExcel(3)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
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
End Class
