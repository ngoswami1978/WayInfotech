Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports Excel
Imports Microsoft.Office.Core

Partial Class Productivity_PRD_Rpt_CountryWiseDailyBookingBudget
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Const strModuleName As String = "CountryWiseDailyBookingBudget"
    Shared ttlRows As Integer = 1
    Shared ttlStartRows As Integer = 1


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Session("PageName") = "Productivity/PRDRPT_CountryWiseDailyBookingBudget.aspx"
            objEaams.ExpirePageCache()
            btnDisplayReport.Attributes.Add("onclick", "return ValidateReportInput();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Report']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country Wise Report']").Attributes("Value").Value)
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
                drpMonths.SelectedIndex = Convert.ToInt16(dtMonth)
                drpYears.Text = dtYear

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDropDowns()
        Try
            Dim I As Integer
            drpMonths.Items.Insert(0, "---Select One---")
            drpYears.Items.Insert(0, "---Select One---")
            Dim lstItem As ListItem
            For I = 1 To 12
                lstItem = New ListItem(MonthName(I), I.ToString())
                drpMonths.Items.Add(lstItem)
            Next
            ' Dim lstItem As ListItem
            For I = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
                lstItem = New ListItem(I.ToString(), I.ToString())
                drpYears.Items.Add(lstItem)
            Next

            drpYears.SelectedValue = DateTime.Now.Year.ToString()

            drpMonths.SelectedIndex = DateTime.Now.Month

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnDisplayReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplayReport.Click

        'KillAllExcels()

        '************ Declaration Section *******************************
        Dim objLog As New AAMS.bizShared.bzShared

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objWarrantyStatus As New AAMS.bizProductivity.bzDailyBookings
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
        Dim strFormulastring As String = ""
        Dim strFormulastring2 As String = ""
        Dim blnHasData As Boolean = False 'To identify to write data of country 
        '************ End of  Declaration Section *******************************
        ' File.Delete("C:\Mukund_Personal\Template\DailyBookingCountryNew.xls")

        Try

            Dim objXdoc As New XmlDocument
            Dim objex As New Exception

            ''*************Starting Log Write code********************
            'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", "Started Log Write..", 1)
            ''end*************Starting Log Write code********************

            If Session("Security") IsNot Nothing Then
                objXdoc.LoadXml(Session("Security"))
            Else
                lblError.Text = "Login Again"
            End If

            Dim strFileName As String = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml
            strFileName = "CountryRpt" + strFileName + ".xls"
            If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                File.Delete(Server.MapPath("~\Template\" + strFileName))
            End If

            objTemplatePath = Server.MapPath("~\Template\DailyBookingCountryTemplate.xls")

            objInputXml.LoadXml("<PR_RPT_DAILYBOOKINGS_INPUT><CITY></CITY><COUNTRY></COUNTRY><SDATE></SDATE><Employee_ID></Employee_ID></PR_RPT_DAILYBOOKINGS_INPUT>")
            Dim strDate As String
            strDate = Val(drpYears.SelectedValue) & Format(Val(drpMonths.SelectedIndex), "00") & "01"

            objInputXml.DocumentElement.SelectSingleNode("SDATE").InnerText = strDate

            objInputXml.DocumentElement.SelectSingleNode("Employee_ID").InnerText = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml

            objOutputXml = objWarrantyStatus.DailyBookingsCountryWise(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objRdr = New XmlNodeReader(objOutputXml)
                dSet.ReadXml(objRdr)

                objExApplication.Visible = True

                objExWorkBooks = objExApplication.Workbooks
                objExWorkBook = objExWorkBooks.Open(objTemplatePath)
                objExWorkSheet = objExApplication.ActiveSheet

                rowCounter = 5

                Dim AvgPerDay As Integer = 0
                Dim countHasData As Integer = 5

                ' objExWorkSheet.Cells(rowCounter, 1) = "TOTAL" + MonthName(drpMonths.SelectedIndex + 1).ToString() + drpYears.SelectedValue.ToString()

                For Each dtrow As DataRow In dSet.Tables("COUNTRYBREAKUP").Rows
                    '<COUNTRYBREAKUP COUNTRY="Bangladesh" BUDGET="87276" ACTUAL="55827" DIFFERENCE="31449" CxlationRate="76.51" AVERAGEPERDAY="1993" per_Changeover_LastYear="14.07" 
                    'DAYSLEFT="0" SEGMENTSREQUIREDPERDAY="" PROJECTION="" TILLDATE="28/02/2007" HASDATA="Yes" /> 

                    If dtrow("HASDATA").ToString().ToUpper() = "YES" Then
                        objExWorkSheet.Cells(countHasData, 1) = dtrow("COUNTRY").ToString()
                        objExWorkSheet.Cells(countHasData, 2) = dtrow("BUDGET").ToString()
                        objExWorkSheet.Cells(countHasData, 3) = dtrow("ACTUAL").ToString()
                        AvgPerDay += Val(dtrow("ACTUAL").ToString() & "")
                        objExWorkSheet.Cells(countHasData, 4) = dtrow("DIFFERENCE").ToString()
                        objExWorkSheet.Cells(countHasData, 5) = dtrow("CxlationRate").ToString()
                        objExWorkSheet.Cells(countHasData, 6) = dtrow("AVERAGEPERDAY").ToString()
                        objExWorkSheet.Cells(countHasData, 8) = dtrow("per_Changeover_LastYear").ToString()
                        objExWorkSheet.Cells(countHasData, 9) = dtrow("DAYSLEFT").ToString()
                        'Code Added on 6th March
                        objExWorkSheet.Cells(countHasData, 14) = dtrow("TTL2_ACTUAL").ToString()
                        'Code Added on 6th March
                        objExWorkSheet.Cells(countHasData, 11) = dtrow("PROJECTION").ToString()
                        'If rowCounter = 5 Then
                        '    objExWorkSheet.Cells(1, 2) = dtrow("TILLDATE").ToString()
                        'End If
                        rowCounter += 1
                        countHasData += 1

                        ''Log Write code
                        'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtrow("COUNTRY").ToString(), "188")



                        'Dim formulaSegReq As String = "" ' "=ROUND(SUM(H24" + ":H" + (rowCounter + ttlRows - 2).ToString() + ")*100/SUM(L24:L" + (rowCounter + ttlRows - 2).ToString() + "),2)-100"
                        'formulaSegReq = "=ROUND(D" + countHasData.ToString() + "/I" + countHasData.ToString() + ",0)"
                        'objExWorkSheet.Range("J" + countHasData.ToString(), "J" + countHasData.ToString()).Formula = formulaSegReq



                        'Dim formulaProjection As String = "" ' "=ROUND(SUM(H24" + ":H" + (rowCounter + ttlRows - 2).ToString() + ")*100/SUM(L24:L" + (rowCounter + ttlRows - 2).ToString() + "),2)-100"
                        'formulaSegReq = "=ROUND(C" + countHasData.ToString() + "+(F" + countHasData.ToString() + "*I" + countHasData.ToString() + "),0)" '=ROUND(C5+(F5*I5),0)
                        'objExWorkSheet.Range("M" + countHasData.ToString(), "M" + countHasData.ToString()).Formula = formulaProjection


                    Else
                        ' objExWorkSheet.Range("A" + rowCounter.ToString(), "M" + rowCounter.ToString()).ClearContents()
                        ''objExWorkSheet.Cells(rowCounter, 10) = ""
                        ''objExWorkSheet.Cells(rowCounter, 13) = ""

                        rowCounter += 1
                    End If
                Next

                'Code segment for filling Date  in sheet
                Dim countryCounter As Int16 = 1
                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                    If strDt.Trim.Length > 0 Then
                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                        ' objExWorkSheet.Cells(1, 2) = MonthName(intMonth) + "  " + intYear.ToString()
                        objExWorkSheet.Cells(4, 8) = "% Change over " + (Val(drpYears.SelectedValue) - 1).ToString.Substring(2, 2)

                        objExWorkSheet.Cells(4, 14) = "TOTAL " + MonthName(drpMonths.SelectedIndex).ToString.ToUpper().Substring(0, 3) + "  " + (Val(drpYears.SelectedValue) - 1).ToString.Substring(2, 2)

                        minusRows = 31 - DateTime.DaysInMonth(intYear, intMonth)
                    End If
                End If
                If dSet.Tables("COUNTRYBREAKUP").Rows.Count > 0 Then
                   Dim strDt As String = dSet.Tables("COUNTRYBREAKUP").Rows(0)("TILLDATE").ToString().Trim()
                    If strDt.Trim.Length > 0 Then
                        ' Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                        ' Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                        'Dim intDay As Int16 = Day(Convert.ToDateTime(strDt))
                        ' objExWorkSheet.Cells(1, 2) = intDay.ToString + " " + MonthName(intMonth) + " " + intYear.ToString()
                        strDt = strDt.PadLeft(10, "0")
                        'Dim dTime As DateTime = Convert.ToDateTime(strDt)
                        Dim dtFormat As String = ""
                        dtFormat = strDt.Substring(0, 2) & "-" & MonthName(Convert.ToInt16(strDt.Substring(3, 2))).Substring(0, 3) & "-" & strDt.Substring(8, 2)
                        objExWorkSheet.Cells(1, 2) = dtFormat 'strDt 'dTime.ToString("dd-MMM-yy")

                       Else
                        objExWorkSheet.Cells(1, 2) = ""
                    End If

                    'For calculation of Averate Per day for India
                    If strDt.Trim.Length > 0 Then
                        objExWorkSheet.Cells(16, 4) = AvgPerDay / Convert.ToInt16(strDt.Substring(0, 2))
                    End If
                    'end of Code Segment
                End If
                'Code segment for filling Date  in sheet



                Dim TotalTop As Integer = 0
                Dim objHasDataNode As XmlNode

                For Each dtRow As DataRow In dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows

                    If (ViewState("CountryN") Is Nothing) Then

                        objHasDataNode = objOutputXml.DocumentElement.SelectSingleNode("COUNTRYBREAKUP[@COUNTRY='" + dtRow("COUNTRY").ToString() + "']")
                        If objHasDataNode IsNot Nothing Then
                            If objHasDataNode.Attributes("HASDATA").Value.ToString().ToUpper() = "YES" Then

                                blnHasData = True

                                ViewState("CountryN") = dtRow("COUNTRY").ToString()
                                objExWorkSheet.Cells(21, 1) = dtRow("COUNTRY").ToString().ToUpper()
                                objExWorkSheet.Cells(22, 1) = dtRow("DATE1").ToString()
                                rowCounter = 24
                                countryCounter = 1
                                If minusRows <> 0 Then
                                    objExWorkSheet.Range("A" + (rowCounter + 30 - minusRows).ToString(), "O" + (rowCounter + 29).ToString()).EntireRow.Delete()
                                End If
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 4, 2) = returnBudget(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 4 - 3, 13) = ReturnPercentChange(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 4 - 3, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString

                                ''Log Write code
                                'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                                    If strDt.Trim.Length > 0 Then
                                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                                        objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 4 - 3, 1) = "TOTAL " + MonthName(intMonth) + "  " + intYear.ToString()
                                    End If
                                End If


                                'objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 4 - 3, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString
                                strFormulastring = "D" + ((rowCounter + 30 - minusRows) + 4 - 3).ToString
                                strFormulastring2 = "C" + ((rowCounter + 30 - minusRows) + 4 - 3).ToString

                                'objExWorkSheet.Cells(5, 14) = TotalTop
                                'Total Calculation Reset on Country Change
                                TotalTop = 0
                            Else
                                blnHasData = False
                            End If
                        Else
                            blnHasData = False
                        End If
                    End If


                    If ViewState("CountryN") IsNot Nothing Then


                        If (ViewState("CountryN").ToString().Trim().ToUpper() <> dtRow("COUNTRY").ToString().Trim().ToUpper()) Then
                            'If countryCounter = 1 Then
                            '    rowCounter = 24
                            '    'Code Added on 23rd January
                            '    Dim FormulaRow As Integer = (rowCounter + 30 - minusRows) + 4 - 3
                            '    Dim formula As String = "=ROUND(SUM(H24" + ":H" + (rowCounter + ttlRows-2).ToString() + ")*100/SUM(L24:L" + (rowCounter + ttlRows-2).ToString() + "),2)-100"
                            '    objExWorkSheet.Range("M" + FormulaRow.ToString(), "M" + FormulaRow.ToString()).Formula = formula '"=ROUND(SUM(H24:H54)*100/SUM(L24:L54),2)-100"
                            '    'Code Added on 23rd January  End of Code
                            'End If
                            objHasDataNode = objOutputXml.DocumentElement.SelectSingleNode("COUNTRYBREAKUP[@COUNTRY='" + dtRow("COUNTRY").ToString() + "']")

                            If objHasDataNode IsNot Nothing Then
                                If objHasDataNode.Attributes("HASDATA").Value.ToString().ToUpper() = "YES" Then
                                    blnHasData = True
                                    countryCounter += 1
                                    'Calculating Total Number of productivity days
                                    ttlRows = 1

                                    ViewState("CountryN") = dtRow("COUNTRY").ToString()
                                    TotalTop = 0

                                    objExWorkSheet.Cells(23, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                    objExWorkSheet.Cells(23, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)

                                    'Total Calculation Reset on Country Change
                                Else
                                    blnHasData = False
                                End If
                            End If


                            If countryCounter = 1 And blnHasData = True Then
                                rowCounter = 24

                                'Code Added for dynamic Header
                                objExWorkSheet.Cells(rowCounter - 1, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                objExWorkSheet.Cells(rowCounter - 1, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)
                                'Code Added for dynamic Header

                                ''Code Added on 23rd January
                                'Dim FormulaRow As Integer = (rowCounter + 30 - minusRows) + 4 - 3
                                'Dim formula As String = "=ROUND(SUM(H24" + ":H" + (24 + ttlRows).ToString() + ")*100/SUM(L24:L" + (24 + ttlRows).ToString() + "),2)-100"
                                'objExWorkSheet.Range("M" + FormulaRow.ToString(), "M" + FormulaRow.ToString()).Formula = formula '"=ROUND(SUM(H24:H54)*100/SUM(L24:L54),2)-100"
                                ''Code Added on 23rd January  End of Code

                            ElseIf countryCounter = 2 And blnHasData = True Then
                                rowCounter = 65 - (countryCounter - 1) * minusRows
                                objExWorkSheet.Cells(rowCounter - 3, 1) = dtRow("COUNTRY").ToString().ToUpper()
                                objExWorkSheet.Cells(rowCounter - 2, 1) = dtRow("DATE1").ToString()

                                'Code Added for dynamic Header
                                objExWorkSheet.Cells(rowCounter - 1, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                objExWorkSheet.Cells(rowCounter - 1, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)
                                'Code Added for dynamic Header

                                If minusRows <> 0 Then
                                    objExWorkSheet.Range("A" + (rowCounter + 30 - minusRows).ToString(), "O" + (rowCounter + 29).ToString()).EntireRow.Delete()
                                End If
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5, 2) = returnBudget(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 13) = ReturnPercentChange(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString

                                strFormulastring += "+" + "D" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString
                                strFormulastring2 += "+" + "C" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString

                                ''Log Write code
                                'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                                    If strDt.Trim.Length > 0 Then
                                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                                        objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 1) = "TOTAL " + MonthName(intMonth) + "  " + intYear.ToString()
                                    End If
                                End If


                            ElseIf countryCounter = 3 And blnHasData = True Then



                                rowCounter = 107 - (countryCounter - 1) * minusRows
                                objExWorkSheet.Cells(rowCounter - 3, 1) = dtRow("COUNTRY").ToString().ToUpper()
                                objExWorkSheet.Cells(rowCounter - 2, 1) = dtRow("DATE1").ToString()

                                'Code Added for dynamic Header
                                objExWorkSheet.Cells(rowCounter - 1, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                objExWorkSheet.Cells(rowCounter - 1, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)
                                'Code Added for dynamic Header

                                If minusRows <> 0 Then
                                    objExWorkSheet.Range("A" + (rowCounter + 30 - minusRows).ToString(), "O" + (rowCounter + 29).ToString()).EntireRow.Delete()
                                End If
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5, 2) = returnBudget(dtRow("COUNTRY").ToString(), dSet)
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 13) = ReturnPercentChange(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString
                                strFormulastring += "+" + "D" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString
                                strFormulastring2 += "+" + "C" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString

                                ''Log Write code
                                'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                                    If strDt.Trim.Length > 0 Then
                                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                                        objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 1) = "TOTAL " + MonthName(intMonth) + "  " + intYear.ToString()
                                    End If
                                End If

                            ElseIf countryCounter = 4 And blnHasData = True Then



                                rowCounter = 148 - (countryCounter - 1) * minusRows
                                objExWorkSheet.Cells(rowCounter - 3, 1) = dtRow("COUNTRY").ToString().ToUpper()
                                objExWorkSheet.Cells(rowCounter - 2, 1) = dtRow("DATE1").ToString()

                                'Code Added for dynamic Header
                                objExWorkSheet.Cells(rowCounter - 1, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                objExWorkSheet.Cells(rowCounter - 1, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)
                                'Code Added for dynamic Header

                                If minusRows <> 0 Then
                                    objExWorkSheet.Range("A" + (rowCounter + 30 - minusRows).ToString(), "O" + (rowCounter + 29).ToString()).EntireRow.Delete()
                                End If
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5, 2) = returnBudget(dtRow("COUNTRY").ToString(), dSet)
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 13) = ReturnPercentChange(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString

                                strFormulastring += "+" + "D" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString
                                strFormulastring2 += "+" + "C" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString

                                ''Log Write code
                                'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                                    If strDt.Trim.Length > 0 Then
                                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                                        objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 1) = "TOTAL " + MonthName(intMonth) + "  " + intYear.ToString()
                                    End If
                                End If

                            ElseIf countryCounter = 5 And blnHasData = True Then

                                rowCounter = 189 - (countryCounter - 1) * minusRows
                                objExWorkSheet.Cells(rowCounter - 3, 1) = dtRow("COUNTRY").ToString().ToUpper()
                                objExWorkSheet.Cells(rowCounter - 2, 1) = dtRow("DATE1").ToString()
                               
                                'Code Added for dynamic Header
                                objExWorkSheet.Cells(rowCounter - 1, 14) = "Holiday-" + drpYears.SelectedItem.Text.Substring(2, 2)
                                objExWorkSheet.Cells(rowCounter - 1, 15) = "Holiday-" + (Convert.ToInt32(drpYears.SelectedItem.Text) - 1).ToString().Substring(2, 2)
                                'Code Added for dynamic Header


                                If minusRows <> 0 Then
                                    objExWorkSheet.Range("A" + (rowCounter + 30 - minusRows).ToString(), "O" + (rowCounter + 29).ToString()).EntireRow.Delete()
                                End If
                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5, 2) = returnBudget(dtRow("COUNTRY").ToString(), dSet)

                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 13) = ReturnPercentChange(dtRow("COUNTRY").ToString(), dSet)


                                objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 10) = "TOTAL " + (Val(drpYears.SelectedValue) - 1).ToString
                                strFormulastring += "+" + "D" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString
                                strFormulastring2 += "+" + "C" + ((rowCounter + 30 - minusRows) + 5 - 4).ToString

                                ''Log Write code
                                'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                                If dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows.Count > 0 Then
                                    Dim strDt As String = dSet.Tables("DAILYBOOKINGS_COUNTRYWISE").Rows(0)("DATE1").ToString().Trim()
                                    If strDt.Trim.Length > 0 Then
                                        Dim intMonth As Int16 = Month(Convert.ToDateTime(strDt))
                                        Dim intYear As Int16 = Year(Convert.ToDateTime(strDt))
                                        objExWorkSheet.Cells((rowCounter + 30 - minusRows) + 5 - 4, 1) = "TOTAL " + MonthName(intMonth) + "  " + intYear.ToString()
                                    End If
                                End If


                            End If
                        Else
                            'TotalTop = 0
                        End If
                    End If



                    If ViewState("CountryN") IsNot Nothing Then
                        If ViewState("CountryN").ToString().Trim().ToUpper() = dtRow("COUNTRY").ToString().Trim().ToUpper() Then
                            objExWorkSheet.Cells(rowCounter, 1) = dtRow("DATE1").ToString()
                            ' objExWorkSheet.Cells(rowCounter, 2) = dtRow("DATE1").ToString()
                            objExWorkSheet.Cells(rowCounter, 3) = dtRow("AIRBKGS").ToString()
                            objExWorkSheet.Cells(rowCounter, 4) = dtRow("AIRXX").ToString()
                            objExWorkSheet.Cells(rowCounter, 5) = dtRow("AIRNET").ToString()
                            objExWorkSheet.Cells(rowCounter, 6) = dtRow("CARBKGS").ToString()
                            objExWorkSheet.Cells(rowCounter, 7) = dtRow("HOTELBKG").ToString()

                            objExWorkSheet.Cells(rowCounter, 8) = dtRow("TTL1").ToString()
                            'objExWorkSheet.Cells(rowCounter, 9) = dtRow("PROJECTION").ToString()

                            objExWorkSheet.Cells(rowCounter, 10) = dtRow("DATE2").ToString()
                            ' objExWorkSheet.Cells(rowCounter, 11) = dtRow("DATE2").ToString()
                            objExWorkSheet.Cells(rowCounter, 12) = dtRow("TTL2").ToString()


                            'Code Added for Holiday
                            objExWorkSheet.Cells(rowCounter, 14) = dtRow("OCCASION").ToString()
                            objExWorkSheet.Cells(rowCounter, 15) = dtRow("P_OCCASION").ToString()
                            'End of Code Added for Holiday

                            ''Log Write code
                            'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", dtRow("COUNTRY").ToString() + "" + dtRow("DATE1").ToString(), "292")


                            'Total Calculation For Top Summary
                            TotalTop += Val(dtRow("TTL2").ToString() & "")

                            rowCounter += 1

                            'Code for formulating Total available Productivity Modified on 23rd January

                            If dtRow("TTL1").ToString() <> "" Then
                                ttlRows += 1
                            End If

                        End If

                    End If




                    If countryCounter = 1 Then
                        ' objExWorkSheet.Cells(5, 14) = TotalTop
                        'TotalTop = 0
                    ElseIf countryCounter = 2 Then
                        ' objExWorkSheet.Cells(6, 14) = TotalTop
                        ' TotalTop = 0
                    ElseIf countryCounter = 3 Then
                        'objExWorkSheet.Cells(7, 14) = TotalTop
                        'TotalTop = 0
                    ElseIf countryCounter = 4 Then
                        ' objExWorkSheet.Cells(8, 14) = TotalTop
                        ' TotalTop = 0
                    ElseIf countryCounter = 5 Then
                        ' objExWorkSheet.Cells(9, 14) = TotalTop
                        ' TotalTop = 0
                    End If


                    'Code Modified on 23rd August

                Next




                If countryCounter = 1 Then
                    rowCounter = 60 - minusRows
                    objExWorkSheet.Range("A" + rowCounter.ToString(), "O300").Clear()

                ElseIf countryCounter = 2 Then
                    rowCounter = 102 - (countryCounter - 1) * minusRows
                    objExWorkSheet.Range("A" + rowCounter.ToString(), "O300").Clear()
                ElseIf countryCounter = 3 Then
                    rowCounter = 144 - (countryCounter - 1) * minusRows
                    objExWorkSheet.Range("A" + rowCounter.ToString(), "O300").Clear()
                ElseIf countryCounter = 4 Then
                    If drpMonths.SelectedIndex = 2 Then
                        rowCounter = 183 - (countryCounter - 1) * minusRows
                    Else
                        rowCounter = 185 - (countryCounter - 1) * minusRows
                    End If
                    objExWorkSheet.Range("A" + rowCounter.ToString(), "O300").Clear()
                ElseIf countryCounter = 5 Then
                    rowCounter = 226 - (countryCounter - 1) * minusRows
                    objExWorkSheet.Range("A" + rowCounter.ToString(), "O300").Clear()
                End If




                '  objExApplication.ActiveWorkbook.Save()
                ' objExApplication.Workbooks.Close()


                If strFormulastring.Trim.Length > 0 Then
                    objExWorkSheet.Cells(16, 5) = "=(" + strFormulastring + ")/(" + strFormulastring2 + ")*100"
                End If



                'Deleting blank rows in Country Breakup section 




                'Dim rowDelete As Int16 = 5

                'For Each dtrow As DataRow In dSet.Tables("COUNTRYBREAKUP").Rows
                '    If dtrow("HASDATA").ToString().ToUpper() = "NO" Then
                '       objExWorkSheet.Range("A" + (rowDelete).ToString(), "M" + (rowDelete).ToString()).EntireRow.Delete()
                '    End If
                '    rowCounter += 1
                'Next


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

        Catch ex As Exception
            lblError.Text = ex.Message
            AAMS.bizShared.bzShared.LogWrite(strModuleName, "btnDisplayReport_Click", ex)
            'AAMS.bizShared.bzShared.LogWriteText(strModuleName, "btnDisplayReport_Click", ex.Message, "00")

        Finally
            'System.Runtime.InteropServices.Marshal.ReleaseComObject(objExWorkSheet)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objExApplication)
            objExWorkSheet = Nothing
            objExWorkBook = Nothing
            objExWorkBooks = Nothing
            objExApplication = Nothing
            GC.Collect()
            'GC.WaitForPendingFinalizers()
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
    Private Function returnBudget(ByVal strCountry As String, ByVal ds As DataSet) As String
        For Each dtRow As DataRow In ds.Tables("COUNTRYBREAKUP").Rows
            If dtRow("COUNTRY").ToString.Trim.ToUpper() = strCountry.Trim.ToUpper Then
                Return dtRow("BUDGET").ToString()
            End If
        Next
        Return "0"
    End Function

    Private Function ReturnPercentChange(ByVal strCountry As String, ByVal ds As DataSet) As String
        For Each dtRow As DataRow In ds.Tables("COUNTRYBREAKUP").Rows
            If dtRow("COUNTRY").ToString.Trim.ToUpper() = strCountry.Trim.ToUpper Then
                Return dtRow("per_Changeover_LastYear").ToString()
            End If
        Next
        Return "0"
    End Function


    Private Sub FeedbakReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objbzFeedback As New AAMS.bizHelpDesk.bzFeedback
        Try
            objInputXml.LoadXml("<PR_RPT_DAILYBOOKINGS_INPUT><CITY></CITY><COUNTRY></COUNTRY><SDATE></SDATE></PR_RPT_DAILYBOOKINGS_INPUT>")
            'If (drpMonths.SelectedIndex <> 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Format(Val(drpMonths.SelectedIndex), "00")
            'End If
            'If (drpYears.SelectedIndex <> 0) Then
            '    objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Val(drpYears.SelectedValue)
            'End If
            Dim strDate As String
            strDate = Val(drpYears.SelectedValue) & Format(Val(drpMonths.SelectedIndex), "00") & "01"

            objInputXml.DocumentElement.SelectSingleNode("SDATE").InnerText = strDate
            'Here Back end Method Call
            Session("CountryWiseBudget") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=CountryWiseDailyBookingReports")
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
End Class
