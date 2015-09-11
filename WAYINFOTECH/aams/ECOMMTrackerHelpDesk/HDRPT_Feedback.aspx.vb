Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Imports Excel
Partial Class ETHelpDesk_HDRPT_Feedback
    Inherits System.Web.UI.Page
    Dim objEaams As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objEaams.ExpirePageCache()
            ' btnDisplayReport.Attributes.Add("onclick", "return ValidateReportInput();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEaams.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET FeedBackGraph']").Count <> 0 Then
                    strBuilder = objEaams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET FeedBackGraph']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnDisplayReport.Enabled = False
                        btnExport.Enabled = False
                        Response.Redirect("../NoRights.aspx")
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

                If DateTime.Now.Day <= 15 Then
                    drpRptType.SelectedValue = "F"
                Else
                    drpRptType.SelectedValue = "S"
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindDropDowns()
        Try
            Dim I As Integer
            'drpMonths.Items.Insert(0, "Select One")
            'drpYears.Items.Insert(0, "Select One")
            For I = 1 To 12
                drpMonths.Items.Add(New ListItem(MonthName(I)))
            Next

            For I = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
                drpYears.Items.Add(I)
            Next

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnDisplayReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplayReport.Click
        Try
            FeedbakReport()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub FeedbakReport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        'Dim objbzFeedback As New AAMS.bizETrackerHelpDesk.bzFeedback

        Dim objbzFeedback As New AAMS.bizETrackerHelpDesk.bzFeedback


        Try
            objInputXml.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH></MONTH><YEAR></YEAR><EmployeeID /></HD_RPTFEEDBACK_INPUT>")
            ' If (drpMonths.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Format(Val(drpMonths.SelectedIndex + 1), "00")
            ' End If
            ' If (drpYears.SelectedIndex <> 0) Then
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Val(drpYears.SelectedValue)

            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objEaams.EmployeeID(Session("Security"))
            'End If
            'Here Back end Method Call
            Session("ETFeedback") = objInputXml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=ETFeedback&Action=" + drpRptType.SelectedValue.Trim, False)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            '  drpMonths.SelectedIndex = 0
            'drpYears.SelectedIndex = 0
            Response.Redirect(Request.Url.OriginalString, False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        ' Dim xnode, xnode1 As XmlNode
        Dim objCityReport As New AAMS.bizProductivity.bzDailyBookings
        Dim dSet As New DataSet
        ' Dim objRdr As XmlNodeReader
        Dim objExApplication As New Excel.Application
        Dim objXdoc As New XmlDocument
        Dim objfb As New AAMS.bizETrackerHelpDesk.bzFeedback


        If drpRptType.SelectedValue.Trim() = "F" Then
            CreateChart_Export("FIRSTFOURTH", "F")
        ElseIf drpRptType.SelectedValue.Trim() = "S" Then
            CreateChart_Export("SECONDFOURTH", "S")
        ElseIf drpRptType.SelectedValue.Trim() = "M" Then
            CreateChart_Export("MONTHLY", "M")
        End If





    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub CreateChart_Export(ByVal tableName As String, ByVal chartType As String)

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim xnode As XmlNode
        Dim objCityReport As New AAMS.bizProductivity.bzDailyBookings
        Dim dSet As New DataSet
        ' Dim objRdr As XmlNodeReader
        Dim objExApplication As New Excel.Application
        Dim objExWorkBooks As Excel.Workbooks
        Dim objExWorkBook As Excel.Workbook
        Dim objExWorkSheet As Excel.Worksheet
        Dim objTemplatePath As String
        Dim objXdoc As New XmlDocument
        Dim objfb As New AAMS.bizETrackerHelpDesk.bzFeedback

        Try
            If Session("Security") IsNot Nothing Then
                objXdoc.LoadXml(Session("Security"))
            Else
                lblError.Text = "Login Again"
            End If

            Dim strFileName As String = objXdoc.DocumentElement.SelectSingleNode("EmployeeID").InnerXml

            strFileName = "FeedBackGraph" + strFileName + ".xls"

            If File.Exists(Server.MapPath("~\Template\" + strFileName)) Then
                File.Delete(Server.MapPath("~\Template\" + strFileName))
            End If

            objTemplatePath = Server.MapPath("~\Template\FeedbackGraphTemplate.xls")
            objExWorkBooks = objExApplication.Workbooks
            objExWorkBook = objExWorkBooks.Open(objTemplatePath)
            objExWorkSheet = objExApplication.ActiveSheet

            objInputXml.LoadXml("<HD_RPTFEEDBACK_INPUT><MONTH></MONTH><YEAR></YEAR><EmployeeID /></HD_RPTFEEDBACK_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("MONTH").InnerText = Format(Val(drpMonths.SelectedIndex + 1), "00")
            objInputXml.DocumentElement.SelectSingleNode("YEAR").InnerText = Val(drpYears.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = objEaams.EmployeeID(Session("Security"))

            objOutputXml = objfb.FeedbackReport(objInputXml)


            If chartType = "F" Then
                objExWorkSheet.Cells(1, 1) = "First Fortnight"
                If objOutputXml.DocumentElement.SelectNodes(tableName).Count <= 1 Then
                    lblError.Text = "No Record Found"
                    Return
                End If
            ElseIf chartType = "S" Then
                objExWorkSheet.Cells(1, 1) = "Second Fortnight"
                If objOutputXml.DocumentElement.SelectNodes(tableName).Count <= 1 Then
                    lblError.Text = "No Record Found"
                    Return
                End If
            ElseIf chartType = "M" Then
                objExWorkSheet.Cells(1, 1) = "Monthly"
                If objOutputXml.DocumentElement.SelectNodes(tableName).Count <= 1 Then
                    lblError.Text = "No Record Found"
                    Return
                End If
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                Dim columnCounter As Integer = 2
                Dim RowCounter As Integer = 2
                For Each xnode In objOutputXml.DocumentElement.SelectNodes(tableName)

                    If ViewState("AOFFICE") Is Nothing Then
                        ViewState("AOFFICE") = xnode.Attributes("AOFFICE").Value.Trim()
                        objExWorkSheet.Cells(RowCounter, columnCounter) = xnode.Attributes("QUESTION_TITLE").Value.Trim()
                        columnCounter += 1
                    Else
                        If ViewState("AOFFICE") = xnode.Attributes("AOFFICE").Value.Trim() Then
                            objExWorkSheet.Cells(RowCounter, columnCounter) = xnode.Attributes("QUESTION_TITLE").Value.Trim()
                            columnCounter += 1
                        Else
                            Exit For
                        End If

                    End If
                Next


                RowCounter = 3
                ViewState("AOFFICE") = Nothing
                columnCounter = 2
                ' Dim flag As Boolean = False

                Dim strAoffice As String = objOutputXml.DocumentElement.SelectSingleNode(tableName).Attributes("AOFFICE").Value.Trim()

                objExWorkSheet.Cells(RowCounter, 1) = strAoffice

                For Each xnode In objOutputXml.DocumentElement.SelectNodes(tableName)
                    If strAoffice = xnode.Attributes("AOFFICE").Value.Trim() Then
                        objExWorkSheet.Cells(RowCounter, columnCounter) = xnode.Attributes("STATUS").Value.Trim()
                        columnCounter += 1
                    Else
                        strAoffice = xnode.Attributes("AOFFICE").Value.Trim()
                        columnCounter = 2
                        RowCounter += 1
                        objExWorkSheet.Cells(RowCounter, columnCounter) = xnode.Attributes("STATUS").Value.Trim()
                        columnCounter = 3
                        objExWorkSheet.Cells(RowCounter, 1) = strAoffice
                    End If
                Next


                objExWorkSheet.Range("A3", "A" + (RowCounter).ToString()).Font.Bold = True


                objExWorkSheet.Name = "FeedbackGraph"

                Dim abChar As New Excel.Chart
                Dim MyCharts As Excel.ChartObjects
                Dim MyCharts1 As Excel.ChartObject

                '  abChar.ChartType = XlChartType.xlColumnStacked



                ' objExWorkSheet.ChartObjects()

                MyCharts = objExWorkSheet.ChartObjects
                MyCharts1 = MyCharts.Add(46, RowCounter * 12.75 + 100, 765, 200)

                abChar = MyCharts1.Chart

                With abChar
                    Dim chartRange As Excel.Range
                    chartRange = objExWorkSheet.Range("A2", "K" + (RowCounter).ToString()) '("=FeedbackGraph!$B3$" + ":$K$" + (RowCounter).ToString())
                    ' objExWorkSheet.Range("B2", "K" + (RowCounter).ToString()).Font.Bold = False
                    .SetSourceData(chartRange)
                    .ChartType = Excel.XlChartType.xlColumnClustered
                    .HasTitle = True
                    .ChartTitle.Text = "Feedback Graph"
                    .ChartTitle.Font.Bold = True
                    .PlotBy = XlRowCol.xlRows
                    .PlotArea.Height = 800.0
                    .PlotArea.Width = 700.0
                    .HasDataTable = False
                    .ChartArea.Font.Size = 8
                    .HasLegend = True

                End With

                'Code Written for setting Axis value of chart
                Dim xlAxisCategory, xlAxisValue As Excel.Axes
                xlAxisCategory = CType(abChar.Axes(, _
                                 Excel.XlAxisGroup.xlPrimary), Excel.Axes)
                xlAxisCategory.Item(Excel.XlAxisType.xlCategory).HasTitle = True
                xlAxisCategory.Item(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Question"
                xlAxisValue = CType(abChar.Axes(, _
                              Excel.XlAxisGroup.xlPrimary), Excel.Axes)
                xlAxisValue.Item(Excel.XlAxisType.xlValue).HasTitle = True
                xlAxisValue.Item(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Status"
                'Code Written for setting Axis value of chart




                'Setting Status for Excel sheet
                Dim StatusCounter As Integer = RowCounter + 22
                For Each xnode In objOutputXml.DocumentElement.SelectNodes("FEEDBACK_STATUS")
                    objExWorkSheet.Cells(StatusCounter, 1) = xnode.Attributes("STATUS_ID").Value.Trim()
                    objExWorkSheet.Cells(StatusCounter, 2) = xnode.Attributes("STATUS_NAME").Value.Trim()
                    StatusCounter += 1
                Next

                Dim start As Integer = RowCounter + 22

                objExWorkSheet.Range("A" + start.ToString(), "B" + (StatusCounter).ToString()).Font.Bold = True

                'Setting Status for Excel sheet


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

                'Release All Objects
                releaseObject(objExWorkSheet)
                releaseObject(objExWorkBook)
                releaseObject(objExWorkBooks)
                releaseObject(objExApplication)

                'GC.WaitForPendingFinalizers()

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                Return
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        Finally

        End Try
    End Sub




End Class
