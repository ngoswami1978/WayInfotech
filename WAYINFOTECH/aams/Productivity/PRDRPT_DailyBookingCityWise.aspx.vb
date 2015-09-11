Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports System
Imports Excel

Partial Class Productivity_PRDSR_DailyBookingCityWise
    Inherits System.Web.UI.Page
    Implements IDisposable
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Public Overrides Sub Dispose()
        MyBase.Dispose()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url
            objEaams.ExpirePageCache()
            '  btnDisplayReport.Attributes.Add("onclick", "return ValidateReportInput();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Wise Report']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Wise Report']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnDisplayReport.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                End If
            Else
                strBuilder = objEaams.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                BindDropDowns()

                Dim dtMonth As String = Month(DateTime.Now)
                Dim dtYear As String = Year(DateTime.Now)
                drpMonths.SelectedIndex = Convert.ToInt16(dtMonth) - 1
                drpYears.Text = dtYear

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnDisplayReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplayReport.Click

        '************ Declaration Section *******************************
        'KillAllExcels()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objCityReport As New AAMS.bizProductivity.bzDailyBookings
        Dim dSet As New DataSet
        Dim objRdr As XmlNodeReader
        Dim objExApplication As New Excel.Application
        Dim objExWorkBooks As Excel.Workbooks
        Dim objExWorkBook As Excel.Workbook
        Dim objExWorkSheet As Excel.Worksheet
        Dim objTemplatePath As String
        'Dim objFile As System.IO.File
        Dim rowCounter As Integer = 0
        Dim minusRows As Int16 = 0
        '************ End of  Declaration Section *******************************
        ' File.Delete("C:\Mukund_Personal\Template\DailyBookingCountryNew.xls")

        Dim objXdoc As New XmlDocument
        Try
            If Session("Security") IsNot Nothing Then
                objXdoc.LoadXml(Session("Security"))
            Else
                lblError.Text = "Login Again"
            End If

            Dim strFileName As String = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml

            strFileName = "CityReport" + strFileName + ".xls"

            If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                File.Delete(Server.MapPath("~\Template\" + strFileName))
            End If

            ' objTemplatePath = Server.MapPath("~\Template\CityReportTemplatel.xls")


            'Dim objbzFeedback As New AAMS.bizHelpDesk.bzFeedback

            objInputXml.LoadXml("<PR_RPT_DAILYBOOKINGS_CITYWISE_INPUT><Month></Month><Year></Year><SummaryOption></SummaryOption><Employee_ID></Employee_ID></PR_RPT_DAILYBOOKINGS_CITYWISE_INPUT>")
            '  If (drpMonths.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonths.SelectedIndex + 1
            ' End If
            ' If (drpYears.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYears.SelectedValue
            '  End If

            objInputXml.DocumentElement.SelectSingleNode("SummaryOption").InnerText = rdCity.SelectedValue.Trim()
            objInputXml.DocumentElement.SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml
            objOutputXml = objCityReport.DailyBookingsCityWise(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = ""
                If rdCity.SelectedValue = "1" Then



                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        Dim daysCounter As Int16 = 0

                        If objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim() <> "" Then
                            daysCounter = Convert.ToInt16(objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim()) 'DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))
                        End If

                        Dim strArray(daysCounter + 3) As String
                        Dim intArray(daysCounter + 3) As Integer
                        strArray(0) = "CITY CODE"

                        intArray(0) = 0

                        Dim counter As Integer = 0
                        For counter = 1 To daysCounter
                            strArray(counter) = drpYears.SelectedValue.ToString + IIf((drpMonths.SelectedIndex + 1).ToString().Trim.Length < 2, "0" + (drpMonths.SelectedIndex + 1).ToString().Trim, (drpMonths.SelectedIndex + 1).ToString().Trim) + IIf((counter).ToString().Trim.Length < 2, "0" + (counter).ToString(), (counter).ToString())
                            intArray(counter) = counter + 1
                        Next
                        strArray(counter) = "TOTAL"
                        intArray(counter) = 1

                        strArray(counter + 1) = "Projection"
                        intArray(counter + 1) = 33

                        strArray(counter + 2) = MonthName(drpMonths.SelectedIndex + 1).ToString().Substring(0, 3) + "-" + (CType(drpYears.SelectedValue, Int16) - 1).ToString() + " Total "
                        intArray(counter + 2) = 34

                        Dim objExcelExport As New ExportExcel
                        objExcelExport.ExportDetails(objOutputXml, "DAILYBOOKINGS_CITYWISE", intArray, strArray, ExportExcel.ExportFormat.CSV, "City.csv")
                        Exit Sub
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If

                ElseIf rdCity.SelectedValue = "3" Then

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ' Dim daysCounter As Int16 = DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))

                        Dim daysCounter As Int16 = 0

                        If objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim() <> "" Then
                            daysCounter = Convert.ToInt16(objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim()) 'DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))
                        End If

                        Dim strArray(daysCounter + 3) As String
                        Dim intArray(daysCounter + 3) As Integer
                        strArray(0) = "Online Status"
                        strArray(1) = "Name"
                        strArray(2) = "Office ID"
                        strArray(3) = "Total"

                        intArray(0) = 0
                        intArray(1) = 1
                        intArray(2) = 2
                        intArray(3) = 3


                        Dim counter As Integer = 0
                        For counter = 4 To daysCounter + 3
                            strArray(counter) = drpYears.SelectedValue.ToString + IIf((drpMonths.SelectedIndex + 1).ToString().Trim.Length < 2, "0" + (drpMonths.SelectedIndex + 1).ToString().Trim, (drpMonths.SelectedIndex + 1).ToString().Trim) + IIf((counter - 3).ToString().Trim.Length < 2, "0" + (counter - 3).ToString(), (counter - 3).ToString())
                            intArray(counter) = counter
                        Next

                        Dim objExcelExport As New ExportExcel
                        objExcelExport.ExportDetails(objOutputXml, "DAILYBOOKINGS_CITYWISE", intArray, strArray, ExportExcel.ExportFormat.CSV, "OfficeID.csv")
                        Exit Sub
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If


                ElseIf rdCity.SelectedValue = "5" Then
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        ' Dim daysCounter As Int16 = DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))
                        Dim daysCounter As Int16 = 0

                        If objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim() <> "" Then
                            daysCounter = Convert.ToInt16(objOutputXml.DocumentElement.SelectSingleNode("DATATILL").Attributes("NOOFDAYS").Value.Trim()) 'DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))
                        End If

                        Dim strArray(daysCounter + 2) As String
                        Dim intArray(daysCounter + 2) As Integer
                        strArray(0) = "State"
                        strArray(1) = "Region"
                        strArray(2) = "Total"


                        intArray(0) = 0
                        intArray(1) = 1
                        intArray(2) = 2


                        Dim counter As Integer = 0
                        For counter = 3 To daysCounter + 2
                            strArray(counter) = drpYears.SelectedValue.ToString + IIf((drpMonths.SelectedIndex + 1).ToString().Trim.Length < 2, "0" + (drpMonths.SelectedIndex + 1).ToString().Trim, (drpMonths.SelectedIndex + 1).ToString().Trim) + IIf((counter - 2).ToString().Trim.Length < 2, "0" + (counter - 2).ToString(), (counter - 2).ToString())
                            intArray(counter) = counter
                        Next

                        Dim objExcelExport As New ExportExcel
                        objExcelExport.ExportDetails(objOutputXml, "DAILYBOOKINGS_CITYWISE", intArray, strArray, ExportExcel.ExportFormat.CSV, "State.csv")
                        Exit Sub
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                        Exit Sub
                    End If


                ElseIf rdCity.SelectedValue = "4" Then

                    objTemplatePath = Server.MapPath("~\Template\CityRegionTemplate.xls")
                    objExWorkBooks = objExApplication.Workbooks
                    objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                    objExWorkSheet = objExApplication.ActiveSheet
                    'If Not File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                    '    objExApplication.ActiveWorkbook.SaveCopyAs(Server.MapPath("~\Template\" + strFileName))
                    '    objExApplication.Workbooks.Close()
                    'End If
                    'objTemplatePath = Server.MapPath("~\Template\" + strFileName)
                    'objExApplication.Workbooks.Open(objTemplatePath)
                    'objExWorkSheet = objExApplication.ActiveSheet

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objRdr = New XmlNodeReader(objOutputXml)
                        dSet.ReadXml(objRdr)

                        ' System.IO.File.Open(objTemplatePath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Write)
                        'objFile.Open(objTemplatePath, IO.FileMode.Append, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)

                        Dim daysCounter As Int16 = DateTime.DaysInMonth(Convert.ToInt16(drpYears.SelectedValue), Convert.ToInt16(drpMonths.SelectedIndex + 1))

                        'Code Added on 24th January
                        objExWorkSheet.Cells(3, 9) = "Segments " + MonthName(drpMonths.SelectedIndex + 1).ToString().Substring(0, 3) + " " + (Convert.ToInt16(drpYears.SelectedValue.Trim()) - 1).ToString().Substring(2, 2)
                        'Code Added on 24th January


                        'Report showing Region Details
                        rowCounter = 4
                        For Each dtrow As DataRow In dSet.Tables("DAILYBOOKINGS_REGION_WISE").Rows
                            'REGION="North" TARGET="0" SEGMENT="418621" DIFFERENCE="-418621" AVERAGEPERDAY="14951" DAYSLEFT="0" SEGMENT_REQUIRED_PER_DAY="0" PROJECTION="418621" LASTYEARSEGMENT="0"
                            If rowCounter = 4 Then
                                objExWorkSheet.Cells(1, 2) = (daysCounter - Convert.ToInt16(dtrow("DAYSLEFT").ToString().Trim)).ToString() + "-" + MonthName(drpMonths.SelectedIndex + 1).Substring(0, 3) + "-" + drpYears.SelectedValue.Trim.Substring(2, 2)
                            End If
                            objExWorkSheet.Cells(rowCounter, 1) = dtrow("REGION").ToString()
                            objExWorkSheet.Cells(rowCounter, 2) = dtrow("TARGET").ToString()
                            objExWorkSheet.Cells(rowCounter, 3) = dtrow("SEGMENT").ToString()
                            objExWorkSheet.Cells(rowCounter, 4) = dtrow("DIFFERENCE").ToString()
                            objExWorkSheet.Cells(rowCounter, 5) = dtrow("AVERAGEPERDAY").ToString()
                            objExWorkSheet.Cells(rowCounter, 6) = dtrow("DAYSLEFT").ToString()
                            objExWorkSheet.Cells(rowCounter, 7) = dtrow("SEGMENT_REQUIRED_PER_DAY").ToString()
                            objExWorkSheet.Cells(rowCounter, 8) = dtrow("PROJECTION").ToString()
                            objExWorkSheet.Cells(rowCounter, 9) = dtrow("LASTYEARSEGMENT").ToString()
                            rowCounter += 1
                        Next
                        'End of Report showing Region Details


                        'Segment of writting Formul 
                        '=SUM(B4:B7)
                        objExWorkSheet.Cells(rowCounter, 1) = "TTL"
                        Dim formula As String
                        formula = "=SUM(B4:" + "B" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("B" + rowCounter.ToString() + ":B" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(C4:" + "C" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("C" + rowCounter.ToString() + ":C" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(D4:" + "D" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("D" + rowCounter.ToString() + ":D" + rowCounter.ToString()).Formula = formula

                        '=C8/DAY(B1)
                        formula = "=C" + rowCounter.ToString() + "/DAY(B1)"
                        objExWorkSheet.Range("E" + rowCounter.ToString() + ":E" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(F4:" + "F" + (rowCounter - 1).ToString() + ")/" + (rowCounter - 4).ToString()
                        objExWorkSheet.Range("F" + rowCounter.ToString() + ":F" + rowCounter.ToString()).Formula = formula

                        '=ROUND(D8/F8,0)
                        formula = "=ROUND(D" + rowCounter.ToString() + "/F" + rowCounter.ToString() + ",0)"
                        objExWorkSheet.Range("G" + rowCounter.ToString() + ":G" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(H4:" + "H" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("H" + rowCounter.ToString() + ":H" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(I4:" + "I" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("I" + rowCounter.ToString() + ":I" + rowCounter.ToString()).Formula = formula


                        'Formula Writting for Total of Country



                        'For making Bold Font of Total Calculated Rows
                        objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "I" + rowCounter.ToString()).Font.Bold = True  ' = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato)

                        'Report showing Country Details
                        'rowCounter = 10
                        rowCounter += 2
                        Dim CountryRowStart As Integer = rowCounter

                        For Each dtrow As DataRow In dSet.Tables("DAILYBOOKINGS_COUNTRY_WISE").Rows

                            objExWorkSheet.Cells(rowCounter, 1) = dtrow("REGION").ToString()
                            objExWorkSheet.Cells(rowCounter, 2) = dtrow("TARGET").ToString()
                            objExWorkSheet.Cells(rowCounter, 3) = dtrow("SEGMENT").ToString()
                            objExWorkSheet.Cells(rowCounter, 4) = dtrow("DIFFERENCE").ToString()
                            objExWorkSheet.Cells(rowCounter, 5) = dtrow("AVERAGEPERDAY").ToString()
                            objExWorkSheet.Cells(rowCounter, 6) = dtrow("DAYSLEFT").ToString()
                            objExWorkSheet.Cells(rowCounter, 7) = dtrow("SEGMENT_REQUIRED_PER_DAY").ToString()
                            objExWorkSheet.Cells(rowCounter, 8) = dtrow("PROJECTION").ToString()
                            objExWorkSheet.Cells(rowCounter, 9) = dtrow("LASTYEARSEGMENT").ToString()
                            rowCounter += 1
                        Next
                        'End of code for Report showing Country Details
                        'Formula Writting for Total of Country
                        objExWorkSheet.Cells(rowCounter, 1) = "TTL"


                        formula = "=SUM(B" + CountryRowStart.ToString() + ":" + "B" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("B" + rowCounter.ToString() + ":B" + rowCounter.ToString()).Formula = formula

                        formula = "=SUM(C" + CountryRowStart.ToString() + ":" + "C" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("C" + rowCounter.ToString() + ":C" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(D" + CountryRowStart.ToString() + ":D" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("D" + rowCounter.ToString() + ":D" + rowCounter.ToString()).Formula = formula

                        '=C8/DAY(B1)
                        formula = "=C" + rowCounter.ToString() + "/DAY(B1)"
                        objExWorkSheet.Range("E" + rowCounter.ToString() + ":E" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(F" + CountryRowStart.ToString() + ":" + "F" + (rowCounter - 1).ToString() + ")/" + (rowCounter - CountryRowStart).ToString()
                        objExWorkSheet.Range("F" + rowCounter.ToString() + ":F" + rowCounter.ToString()).Formula = formula

                        '=ROUND(D8/F8,0)
                        formula = "=SUM(G" + CountryRowStart.ToString() + ":G" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("G" + rowCounter.ToString() + ":G" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(H" + CountryRowStart.ToString() + ":H" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("H" + rowCounter.ToString() + ":H" + rowCounter.ToString()).Formula = formula


                        formula = "=SUM(I" + CountryRowStart.ToString() + ":I" + (rowCounter - 1).ToString() + ")"
                        objExWorkSheet.Range("I" + rowCounter.ToString() + ":I" + rowCounter.ToString()).Formula = formula

                        'Formula Writting for Total of Country


                        'For making Bold Font of Total Calculated Rows
                        objExWorkSheet.Cells.Range("A" + rowCounter.ToString(), "I" + rowCounter.ToString()).Font.Bold = True  ' = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Tomato)

                        objExWorkSheet.Cells.Range("A" + (rowCounter + 1).ToString(), "I40").ClearFormats()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If

                ElseIf rdCity.SelectedValue = "2" Then
                    objTemplatePath = Server.MapPath("~\Template\CityWiseGraphTemplate.xls")
                    objExWorkBooks = objExApplication.Workbooks
                    objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                    objExWorkSheet = objExApplication.ActiveSheet


                    'If Not File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                    '    objExApplication.ActiveWorkbook.SaveCopyAs(Server.MapPath("~\Template\" + strFileName))
                    '    objExApplication.Workbooks.Close()
                    'End If
                    'objTemplatePath = Server.MapPath("~\Template\" + strFileName)
                    'objExApplication.Workbooks.Open(objTemplatePath)
                    'objExWorkSheet = objExApplication.ActiveSheet

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objRdr = New XmlNodeReader(objOutputXml)
                        dSet.ReadXml(objRdr)



                        If dSet.Tables("DATATILL").Rows.Count > 0 Then
                            objExWorkSheet.Cells(1, 1) = "Data till  " + dSet.Tables("DATATILL").Rows(0)("DATE1").ToString()
                            objExWorkSheet.Cells(1, 10) = "Data till  " + dSet.Tables("DATATILL").Rows(0)("DATE2").ToString()
                            objExWorkSheet.Cells(1, 19) = "Data till  " + dSet.Tables("DATATILL").Rows(0)("DATE3").ToString()
                        End If


                        Dim lstCharFlag As New Hashtable
                        For rowCounter = 1 To 18
                            lstCharFlag.Add(rowCounter, "0")
                        Next


                        rowCounter = 5

                        For Each dtrow As DataRow In dSet.Tables("DAILYBOOKINGS_CITYWISE").Rows

                            If ViewState("TYPE") Is Nothing Then
                                ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                            End If

                            If ViewState("DATATYPE") Is Nothing Then
                                ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "TOP 10 LEASE" Then
                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 5
                                End If

                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 5
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(1) = "1"
                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 3) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(2) = "1"
                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 12) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(3) = "1"
                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 21) = dtrow("NetBookings").ToString()
                                End If
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "LEAST 10 LEASE" Then
                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 18
                                End If

                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 18
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(4) = "1"
                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 3) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(5) = "1"
                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 12) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(6) = "1"
                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 21) = dtrow("NetBookings").ToString()
                                End If
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "TOP 10 VISTA DIALUP" Then
                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 31
                                End If
                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 31
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(7) = "1"

                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 3) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(8) = "1"

                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 12) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(9) = "1"

                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 21) = dtrow("NetBookings").ToString()
                                End If
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "TOP 10 VISTA LEASE LINE" Then
                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 44
                                End If

                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 44
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(10) = "1"

                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 3) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(11) = "1"

                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 12) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(12) = "1"

                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 21) = dtrow("NetBookings").ToString()
                                End If
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "LEAST 10 VISTA LEASE LINE" Then
                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 57
                                End If

                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 57
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(13) = "1"

                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 3) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(14) = "1"

                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 12) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(15) = "1"

                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("OFFICEID").ToString()
                                    objExWorkSheet.Cells(rowCounter, 21) = dtrow("NetBookings").ToString()
                                End If
                            End If

                            If dtrow("TYPE").ToString.Trim.ToUpper = "GROUPS" Then
                                If ViewState("DATATYPE") <> dtrow("DATATYPE").ToString.Trim.ToUpper Then
                                    ViewState("DATATYPE") = dtrow("DATATYPE").ToString.Trim.ToUpper
                                    rowCounter = 70
                                End If

                                If ViewState("TYPE") <> dtrow("TYPE").ToString.Trim.ToUpper Then
                                    ViewState("TYPE") = dtrow("TYPE").ToString.Trim.ToUpper
                                    rowCounter = 70
                                End If

                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "CURRENTMONTH" Then
                                    lstCharFlag.Item(16) = "1"

                                    objExWorkSheet.Cells(rowCounter, 1) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 2) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTYEAR" Then
                                    lstCharFlag.Item(17) = "1"

                                    objExWorkSheet.Cells(rowCounter, 10) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 11) = dtrow("NetBookings").ToString()
                                End If
                                If dtrow("DATATYPE").ToString.Trim.ToUpper = "LASTMONTH" Then
                                    lstCharFlag.Item(18) = "1"

                                    objExWorkSheet.Cells(rowCounter, 19) = dtrow("Name").ToString()
                                    objExWorkSheet.Cells(rowCounter, 20) = dtrow("NetBookings").ToString()
                                End If
                            End If
                            rowCounter += 1
                        Next

                        ''Following Code is written for Clearing Data of Blank Section
                        'If lstCharFlag.Item(1).ToString = "0" Then
                        '    objExWorkSheet.Range("A4", "H15").Clear()
                        'End If
                        'If lstCharFlag.Item(2).ToString = "0" Then
                        '    objExWorkSheet.Range("J4", "Q15").Clear()
                        'End If
                        'If lstCharFlag.Item(3).ToString = "0" Then
                        '    objExWorkSheet.Range("S4", "Z15").Clear()
                        'End If
                        'If lstCharFlag.Item(4).ToString = "0" Then
                        '    objExWorkSheet.Range("A17", "H28").Clear()
                        'End If
                        'If lstCharFlag.Item(5).ToString = "0" Then
                        '    objExWorkSheet.Range("J17", "Q28").Clear()
                        'End If
                        'If lstCharFlag.Item(6).ToString = "0" Then
                        '    objExWorkSheet.Range("S17", "Z28").Clear()
                        'End If
                        'If lstCharFlag.Item(7).ToString = "0" Then
                        '    objExWorkSheet.Range("A30", "H41").Clear()
                        'End If
                        'If lstCharFlag.Item(8).ToString = "0" Then
                        '    objExWorkSheet.Range("J30", "Q41").Clear()
                        'End If
                        'If lstCharFlag.Item(9).ToString = "0" Then
                        '    objExWorkSheet.Range("Z30", "Z41").Clear()
                        'End If
                        'If lstCharFlag.Item(10).ToString = "0" Then
                        '    objExWorkSheet.Range("A43", "H54").Clear()
                        'End If
                        'If lstCharFlag.Item(11).ToString = "0" Then
                        '    objExWorkSheet.Range("J43", "Q54").Clear()
                        'End If

                        'If lstCharFlag.Item(12).ToString = "0" Then
                        '    objExWorkSheet.Range("S43", "Z54").Clear()
                        'End If

                        'If lstCharFlag.Item(13).ToString = "0" Then
                        '    objExWorkSheet.Range("A56", "H67").Clear()
                        'End If
                        'If lstCharFlag.Item(14).ToString = "0" Then
                        '    objExWorkSheet.Range("J56", "Q67").Clear()
                        'End If
                        'If lstCharFlag.Item(15).ToString = "0" Then
                        '    objExWorkSheet.Range("S56", "Z67").Clear()
                        'End If
                        'If lstCharFlag.Item(16).ToString = "0" Then
                        '    objExWorkSheet.Cells(70, 1) = "Test value"
                        '    'Dim a As Excel.Chart
                        '    ' a = objExWorkSheet.ChartObjects(16)
                        '    Dim a As Excel.ChartObject
                        '    objExWorkSheet.Charts.Delete()
                        '    a = objExWorkSheet.ChartObjects(16)
                        '    ' = "" '.Range("A69", "H90").Item(169, 16) = "" '.ClearContents() '.Clear()
                        '    a.Height = 0
                        '    a.Width = 0
                        '    ' a.Delete()
                        '    ' a.Visible = Excel.XlSheetVisibility.xlSheetHidden '.Delete()
                        'End If
                        'If lstCharFlag.Item(17).ToString = "0" Then
                        '    objExWorkSheet.Range("J69", "Q90").Cells.ClearFormats() '.Clear()
                        'End If
                        'If lstCharFlag.Item(18).ToString = "0" Then
                        '    objExWorkSheet.Range("S69", "Z90").ClearNotes() '.ClearOutline() '.Clear()
                        'End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If


                'Clearing Template Range

                '  objExWorkSheet.Range("A1", "H15").Clear()
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
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

            'Code Added on 17th January
            objTemplatePath = Nothing
            objRdr = Nothing
            dSet = Nothing
            GC.Collect()
            'Code Added on 17th January


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '********** Previous Code********************************************************
            ''System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkSheet)
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            'objExWorkSheet = Nothing
            'objExWorkBook = Nothing
            'objExWorkBooks = Nothing
            'objExApplication = Nothing
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            '********** Previous Code********************************************************

            System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            GC.Collect()
            'GC.WaitForPendingFinalizers()



            If objExWorkBooks IsNot Nothing Then
                objExWorkBooks = Nothing
            End If

            If objExWorkBooks IsNot Nothing Then
                objExWorkBook = Nothing
            End If

            If objExWorkBook IsNot Nothing Then
                objExWorkBook = Nothing
            End If

            If objExWorkSheet IsNot Nothing Then
                objExWorkSheet = Nothing
            End If
            If objExApplication IsNot Nothing Then
                objExApplication = Nothing
            End If



            GC.Collect()
            'GC.WaitForPendingFinalizers()

        End Try

    End Sub

    Private Sub BindDropDowns()
        Try
            Dim I As Integer
            Dim lstItem As ListItem
            'drpMonths.Items.Insert(0, "All")
            'drpYears.Items.Insert(0, "All")
            For I = 1 To 12
                lstItem = New ListItem(MonthName(I).ToString(), I.ToString())
                drpMonths.Items.Add(lstItem)
            Next

            'Dim intYear As Int32 = DateTime.Now.Year - 3


            For I = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
                lstItem = New ListItem(I.ToString(), I.ToString())
                drpYears.Items.Add(lstItem)
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DailBookingCityWise()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzFeedback As New AAMS.bizHelpDesk.bzFeedback
        Try
            objInputXml.LoadXml("<PR_RPT_DAILYBOOKINGS_CITYWISE_INPUT><Month></Month><Year></Year><SummaryOption></SummaryOption></PR_RPT_DAILYBOOKINGS_CITYWISE_INPUT>")
            ' If (drpMonths.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Month").InnerText = drpMonths.SelectedIndex + 1
            ' End If
            'If (drpYears.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("Year").InnerText = drpYears.SelectedValue
            ' End If

            objInputXml.DocumentElement.SelectSingleNode("SummaryOption").InnerText = rdCity.SelectedValue.Trim()

            ''Here Back end Method Call
            Session("CityWiseBudget") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=DailyBookingCityWise")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            drpMonths.SelectedIndex = 0
            drpYears.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub KillAllExcels()
        Dim proc As System.Diagnostics.Process
        Try
            For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
                proc.Kill()
            Next
        Catch ex As Exception

        End Try
    End Sub

End Class
