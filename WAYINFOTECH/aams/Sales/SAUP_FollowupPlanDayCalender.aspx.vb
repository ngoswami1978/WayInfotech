Imports System.Xml
Imports System.Data
Partial Class Sales_SAUP_FollowupPlanDayCalender
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

     

        ' Session("UpdateData") = Nothing
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        Dim objout As New XmlDocument
        Dim objnode As XmlNode
        Try
            If Not Me.IsPostBack Then
                If Request.QueryString("Month") IsNot Nothing Then
                    hdMonth.Value = Request.QueryString("Month").ToString
                End If
                If Request.QueryString("Year") IsNot Nothing Then
                    hdYear.Value = Request.QueryString("Year").ToString
                End If

                If Request.QueryString("VisitDate") IsNot Nothing Then
                    hdMonth.Value = Val(Request.QueryString("VisitDate").ToString.Substring(4, 2))
                    hdYear.Value = Request.QueryString("VisitDate").ToString.Substring(0, 4)
                End If

                'If Request.QueryString("MaxVisit") IsNot Nothing Then
                '    HdMaxVisit.Value = Request.QueryString("MaxVisit").ToString
                'End If

                'If Request.QueryString("UserDefVisit") IsNot Nothing Then
                '    HdUserDefinedVisit.Value = Request.QueryString("UserDefVisit").ToString
                'End If

                If Request.QueryString("Lcode") IsNot Nothing Then
                    HdLCode.Value = Request.QueryString("Lcode").ToString
                    AgencyView()
                End If

                GetPlanDaysByMonthYearAndLcode(hdMonth.Value, hdYear.Value, HdLCode.Value)

                If ViewState("Plandays") IsNot Nothing Then
                    objout.LoadXml(ViewState("Plandays").ToString)
                    objnode = objout.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @MONTH='" + hdMonth.Value + "' and @YEAR='" + hdYear.Value + "']")
                    If objnode IsNot Nothing Then

                        Dim tempVisitDays As String = ""

                        HdUserDefinedVisitDays.Value = objnode.Attributes("PLANNED_DAYS").Value
                        HdMaxVisit.Value = objnode.Attributes("VISITCOUNT").Value

                        '@ Start of New Added Code on 6th Sep 2011
                        If HdUserDefinedVisitDays.Value.Trim.Length > 0 Then
                            If HdUserDefinedVisitDays.Value.Trim.Split("|").Length > 0 Then
                                For j As Int16 = 0 To HdUserDefinedVisitDays.Value.Trim.Split("|").Length - 1
                                    If Val(HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString) <= Val(DateTime.DaysInMonth(CInt(hdYear.Value), CInt(hdMonth.Value))) Then
                                        If tempVisitDays.Trim.Length = 0 Then
                                            tempVisitDays = HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString
                                        Else
                                            tempVisitDays = tempVisitDays + "|" + HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString
                                        End If
                                    End If
                                Next
                            End If
                        End If
                        If HdUserDefinedVisitDays.Value.Trim <> tempVisitDays.Trim Then
                            HdUserDefinedVisitDays.Value = tempVisitDays
                        End If
                        '@ End of New Added Code on 6th Sep 2011
                        TxtMonth.Text = MonthName(hdMonth.Value)
                        TxtYear.Text = hdYear.Value
                        ClnPlanDay.VisibleDate = New DateTime(Int32.Parse(hdYear.Value.Trim), Int32.Parse(hdMonth.Value.Trim), 1)
                        If objnode.Attributes("PLANNED_DAYS").Value.Trim <> HdUserDefinedVisitDays.Value.Trim Then
                            objnode.Attributes("PLANNED_DAYS").Value = HdUserDefinedVisitDays.Value
                            ViewState("Plandays") = objout.OuterXml
                        End If
                    End If
                End If
                allowsavingornot()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ClnPlanDay_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles ClnPlanDay.DayRender
        Dim objout As New XmlDocument
        Dim objnode As XmlNode
        Try
            Dim Lcode As String = ""
            Dim Month As String = ""
            Dim Year As String = ""
            Lcode = HdLCode.Value
            Month = ClnPlanDay.VisibleDate.Month
            Year = ClnPlanDay.VisibleDate.Year

            If ViewState("Plandays") IsNot Nothing Then
                objout.LoadXml(ViewState("Plandays").ToString)
                objnode = objout.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + Lcode + "' and @MONTH='" + Month + "' and @YEAR='" + Year + "']")
                If objnode IsNot Nothing Then
                    '@ Start of New Code
                    ClnPlanDay.SelectedDates.Clear()
                    e.Cell.Attributes.Add("onclick", "return CalenderUpdation('" + e.Day.DayNumberText + "')")

                    If e.Day.IsWeekend Then
                        ' e.Cell.BackColor = Drawing.Color.Gray
                        e.Cell.CssClass = "Weekend"
                    End If

                    Dim dates As New ArrayList
                    Dim StrSelectedDateByUser As String = objnode.Attributes("PLANNED_DAYS").Value
                    If StrSelectedDateByUser.Trim.Length > 0 Then
                        If StrSelectedDateByUser.Split("|").Length > 0 Then
                            For j As Int16 = 0 To StrSelectedDateByUser.Split("|").Length - 1
                                dates.Add(CInt(StrSelectedDateByUser.Split("|")(j).ToString))
                            Next
                        End If
                    End If

                    For i As Integer = 0 To dates.Count - 1
                        Dim dt As DateTime = DirectCast(New DateTime(Int32.Parse(hdYear.Value.Trim), Int32.Parse(hdMonth.Value.Trim), dates(i)), DateTime)
                        ClnPlanDay.SelectedDates.Add(dt)

                        '@ If Selected Days Visitor Already is visited then
                        'Can'T Change it
                        If e.Day.DayNumberText = dates(i).ToString Then
                            If visitedPlanDay(dates(i).ToString) Then
                                e.Day.IsSelectable = False
                                e.Cell.BackColor = Drawing.Color.Green   ' Plan Days with Visited
                                e.Cell.Attributes.Clear()
                            ElseIf UnvisitedPlanDay(dates(i).ToString) Then
                                e.Day.IsSelectable = False
                                e.Cell.BackColor = Drawing.Color.Red   ' Plan Days with UnVisited
                                e.Cell.Attributes.Clear()
                            ElseIf BackDatedDSRLog(dates(i).ToString) Then
                                e.Day.IsSelectable = False
                                e.Cell.BackColor = Drawing.Color.Purple   ' Back Dated DSR Log
                                e.Cell.Attributes.Clear()
                            ElseIf PlanCallByManager(dates(i).ToString) Then
                                e.Day.IsSelectable = False
                                e.Cell.BackColor = Drawing.Color.DarkSalmon   ' Plan Call By Manager
                                e.Cell.Attributes.Clear()
                            ElseIf NotVisitedAfter5Days(dates(i).ToString) Then
                                e.Cell.BackColor = Drawing.Color.Fuchsia   ' PLANNED VISIT NOT LOGGED AFTER 5 DAYS
                            Else
                                e.Cell.BackColor = Drawing.Color.Blue      ' Plan Days
                            End If
                        End If
                    Next
                    If UnPlanDayVisited(e.Day.DayNumberText.ToString) Then
                        e.Day.IsSelectable = False
                        e.Cell.BackColor = Drawing.Color.Yellow   ' UnPlan Days with Visited
                        e.Cell.ForeColor = Drawing.Color.Black
                        e.Cell.Attributes.Clear()
                    End If
                    If e.Day.IsOtherMonth Then
                        e.Cell.Text = ""
                        e.Cell.Attributes.Clear()
                        e.Cell.BackColor = Drawing.Color.White
                    End If

                    '@ Not Allow for Selecting of Previous date/current date

                    Dim Currentdt As DateTime = DirectCast(New DateTime(Int32.Parse(hdYear.Value.Trim), Int32.Parse(hdMonth.Value.Trim), Int32.Parse(e.Day.DayNumberText.Trim)), DateTime)
                    Dim TodayDate As New Date(Now.Year, Now.Month, Now.Day)
                    Dim intdaysDiff As Long = DateDiff(DateInterval.Day, Currentdt, TodayDate)

                    If (intdaysDiff >= 0) Then
                        e.Day.IsSelectable = False
                        ' e.Cell.BackColor = Drawing.Color.Yellow   ' UnPlan Days with Visited
                        'e.Cell.ForeColor = Drawing.Color.Black
                        e.Cell.Attributes.Clear()
                    End If


                End If
            End If
        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        Finally
        End Try
    End Sub

    Protected Sub ClnPlanDay_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClnPlanDay.PreRender

    End Sub
    'Protected Sub ClnPlanDay_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClnPlanDay.SelectionChanged
    '    ' UpdateCalender()
    '    UpdateByClientSideCalender()
    'End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.ToString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'Dim objInputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As DataSet
        'Dim ChangedData As Boolean = True
        Dim objoutputxml As New XmlDocument
        Dim objFinalInputxml As New XmlDocument
        Dim objFinaloutputxml As New XmlDocument
        Dim objnode As XmlNode
        Dim Lcode As String = ""
        Dim Month As String = ""
        Dim Year As String = ""
        Dim ObjInputXmlForPlan As New XmlDocument
        Dim objbzAgencyTarget As New AAMS.bizSales.bzAgencyTarget

        Try
            Lcode = HdLCode.Value
            Month = ClnPlanDay.VisibleDate.Month
            Year = ClnPlanDay.VisibleDate.Year
            If ViewState("Plandays") IsNot Nothing Then
                objoutputxml.LoadXml(ViewState("Plandays").ToString)
                objnode = objoutputxml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + Lcode + "' and @MONTH='" + Month + "' and @YEAR='" + Year + "']")
                If objnode IsNot Nothing Then
                    Dim dates As New ArrayList
                    Dim StrSelectedDateByUser As String = objnode.Attributes("PLANNED_DAYS").Value
                    If StrSelectedDateByUser.Trim.Length > 0 Then
                        If StrSelectedDateByUser.Split("|").Length > 0 Then
                            For j As Int16 = 0 To StrSelectedDateByUser.Split("|").Length - 1
                                dates.Add(CInt(StrSelectedDateByUser.Split("|")(j).ToString))
                            Next
                        End If
                    End If

                    HdMaxVisit.Value = objnode.Attributes("VISITCOUNT").Value

                    '@ Start of new code added
                    If dates.Count < HdMaxVisit.Value Then
                        If Val(Now.Year) = Val(hdYear.Value) Then
                            If Val(Now.Month) = Val(hdMonth.Value) Then
                                LblCalenderError.Text = "Visit Plan should be equal to or Greater than the objective visit target."
                                Exit Sub
                            End If
                        End If
                    End If
                    '@ End of new code added


                    '
                    Dim strPllanedDays As String = ""
                    For i As Integer = 0 To dates.Count - 1
                        If strPllanedDays.Trim.Length > 0 Then
                            strPllanedDays = strPllanedDays + "|" + dates(i).ToString()
                        Else
                            strPllanedDays = dates(i).ToString()
                        End If
                        If objnode IsNot Nothing Then
                            Dim objAttributesName As String = "D" + dates(i).ToString
                            '  If Not (objnode.Attributes(objAttributesName).Value = "2" Or objnode.Attributes(objAttributesName).Value = "4") Then ' Update Calender for Plan Days if on that days  either plan is visited/not visited
                            If Not (objnode.Attributes(objAttributesName).Value = "2" Or objnode.Attributes(objAttributesName).Value = "4" Or objnode.Attributes(objAttributesName).Value = "5" Or objnode.Attributes(objAttributesName).Value = "7") Then ' Update Calender for Plan Days if on that days  either plan is visited/not visited
                                objnode.Attributes(objAttributesName).Value = "1" ' Setting PLannedDays
                            End If
                        End If
                    Next

                    '@Start of code If from Plan Days Day is Unselected then

                    For i As Integer = 1 To 31
                        With objnode
                            Dim objAttributesName As String = "D" + i.ToString
                            If .Attributes(objAttributesName).Value = "1" Or .Attributes(objAttributesName).Value = "6" Then
                                If Not dates.Contains(i) Then
                                    .Attributes(objAttributesName).Value = ""
                                End If
                            End If
                        End With
                    Next
                    '@End of code If from Plan Days Day is Unselected then

                    objnode.Attributes("VISITTARGET").Value = strPllanedDays.Split("|").Length

                    '@ Start of Update the Planning 
                    objFinalInputxml.LoadXml("<TA_AGENCYTARGET_OUTPUT></TA_AGENCYTARGET_OUTPUT>")
                    objFinalInputxml.DocumentElement.AppendChild(objFinalInputxml.ImportNode(objnode, True))

                    objFinaloutputxml = objbzAgencyTarget.Update(objFinalInputxml)

                    If objFinaloutputxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        lblError.Text = "Plan is updated."
                    Else
                        lblError.Text = objFinaloutputxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If

                    '@ End of update the planning

                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub AgencyView()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            objInputXml.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
            If Request.QueryString("Lcode") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = HdLCode.Value
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
            Else
                '  lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub BtnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClear.Click
        Dim objout As New XmlDocument
        Dim objnode As XmlNode
        ClnPlanDay.SelectedDates.Clear()

        Dim Lcode As String = ""
        Dim Month As String = ""
        Dim Year As String = ""
        Lcode = HdLCode.Value
        Month = ClnPlanDay.VisibleDate.Month
        Year = ClnPlanDay.VisibleDate.Year
        If ViewState("Plandays") IsNot Nothing Then
            objout.LoadXml(ViewState("Plandays").ToString)
            objnode = objout.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @MONTH='" + hdMonth.Value + "' and @YEAR='" + hdYear.Value + "']")
            If objnode IsNot Nothing Then
                objnode.Attributes("PLANNED_DAYS").Value = ""
                ViewState("Plandays") = objout.OuterXml
            End If
        End If

    End Sub

    Private Function visitedPlanDay(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            ' If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                'objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)
                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "2" Then   ' If Visited Plan Day
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function
    Private Function UnvisitedPlanDay(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            ' If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                ' objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)

                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "4" Then   ' If Un Visited Plan day
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function
    Private Function UnPlanDayVisited(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            ' If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                ' objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)
                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "3" Then   ' If UnPlanday Visited
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function
    Private Function BackDatedDSRLog(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            ' If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                ' objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)
                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "5" Then   ' If Visited Plan Day
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function
    Private Function NotVisitedAfter5Days(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            ' If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                ' objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)
                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "6" Then   ' If Visited Plan Day
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function

    Private Function PlanCallByManager(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            '  If Session("FinalSesseionXML") IsNot Nothing Then
            If ViewState("Plandays") IsNot Nothing Then
                'objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                objInputXml.LoadXml(ViewState("Plandays").ToString)
                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                If objnode IsNot Nothing Then
                    Dim objAttributesName As String = "D" + Day
                    If objnode.Attributes(objAttributesName).Value = "7" Then   ' If Visited Plan Day
                        blnVistitedPlanDay = True
                    End If
                End If
            End If
            Return blnVistitedPlanDay
        Catch ex As Exception
            Return blnVistitedPlanDay
        End Try
    End Function
    Private Sub UpdateCalender()
        'Try
        '    Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)
        '    If dates.Count < HdMaxVisit.Value Then
        '        For i As Integer = 0 To ClnPlanDay.SelectedDates.Count - 1
        '            If dates.Contains(ClnPlanDay.SelectedDates(i).Day) Then
        '                dates.Remove(ClnPlanDay.SelectedDates(i).Day)
        '            Else
        '                dates.Add(ClnPlanDay.SelectedDates(i).Day)
        '            End If
        '        Next
        '        ViewState("PlanDays") = dates
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try

    End Sub
    Private Sub UpdateByClientSideCalender()

        Dim objout As New XmlDocument
        Dim objnode As XmlNode

        Try
            Dim Lcode As String = ""
            Dim Month As String = ""
            Dim Year As String = ""
            Lcode = HdLCode.Value
            Month = ClnPlanDay.VisibleDate.Month
            Year = ClnPlanDay.VisibleDate.Year

            '@ Start of New Code

            LblCalenderError.Text = ""
            If Val(HdMaxVisit.Value) = 0 Then
                'LblCalenderError.Text = "Can’t plan visit for this category."
                'Exit Sub
            End If

            If ViewState("Plandays") IsNot Nothing Then
                objout.LoadXml(ViewState("Plandays").ToString)
                objnode = objout.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + Lcode + "' and @MONTH='" + Month + "' and @YEAR='" + Year + "']")
                If objnode IsNot Nothing Then

                    Dim dates As New ArrayList
                    Dim StrSelectedDateByUser As String = objnode.Attributes("PLANNED_DAYS").Value
                    If StrSelectedDateByUser.Trim.Length > 0 Then
                        If StrSelectedDateByUser.Split("|").Length > 0 Then
                            For j As Int16 = 0 To StrSelectedDateByUser.Split("|").Length - 1
                                dates.Add(CInt(StrSelectedDateByUser.Split("|")(j).ToString))
                            Next
                        End If
                    End If

                    If dates.Contains(CInt(Val(HdSelectedDay.Value))) Then
                        dates.Remove(CInt(Val(HdSelectedDay.Value)))
                    Else
                        If HdSelectedDay.Value.Trim.Length > 0 Then
                            dates.Add(CInt(Val(HdSelectedDay.Value)))
                        End If
                    End If
                    Dim str As String = ""
                    For i As Integer = 0 To dates.Count - 1
                        If str.Trim.Length = 0 Then
                            str = dates(i).ToString
                        Else
                            str = str + "|" + dates(i).ToString
                        End If
                    Next
                    If objnode.Attributes("PLANNED_DAYS").Value.Trim <> str.Trim Then
                        objnode.Attributes("PLANNED_DAYS").Value = str
                        ViewState("Plandays") = objout.OuterXml

                    End If

                End If
            End If
            '@ End of New code
        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        End Try
    End Sub
    Protected Sub BtnFake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFake.Click
        UpdateByClientSideCalender()
    End Sub
    Private Sub GetPlanDaysByMonthYearAndLcode(ByVal Month As String, ByVal Year As String, ByVal Lcode As String)
        Dim objinputxml As New XmlDocument
        Dim objOutputxml As New XmlDocument
        'dim obj as New 
        Try
            objinputxml.LoadXml("<MakingCalender><TARGET LCODE='' MONTH='' YEAR='' VISITCOUNT=''  PLANNED_DAYS= '' D1=''   D2=''   D3='' D4=''  D5=''  D6=''  D7=''  D8=''  D9='' D10=''   D11=''   D12=''   D13=''  D14=''  D15=''  D16=''  D17='' D18='' D19='' D20='' D21=''   D22=''   D23=''   D24=''  D25=''  D26=''  D27=''  D28='' D29='' D30='' D31=''  ></TARGET></MakingCalender>")
            With objinputxml.DocumentElement.SelectSingleNode("TARGET")
                .Attributes("MONTH").Value = Month
                .Attributes("YEAR").Value = Year
                .Attributes("LCODE").Value = Lcode
            End With

            UpdatePlandays(objinputxml)
        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        End Try
    End Sub
    Private Sub UpdatePlandays(ByVal objinputxml As XmlDocument)
        Dim objoutputxml As New XmlDocument
        Dim objFinaloutputxml As New XmlDocument
        Dim objnode As XmlNode
        Dim Lcode As String = ""
        Dim Month As String = ""
        Dim Year As String = ""
        Dim ObjInputXmlForPlan As New XmlDocument
        Dim objbzAgencyTarget As New AAMS.bizSales.bzAgencyTarget

        Try
            Lcode = objinputxml.DocumentElement.SelectSingleNode("TARGET").Attributes("LCODE").Value
            Month = objinputxml.DocumentElement.SelectSingleNode("TARGET").Attributes("MONTH").Value
            Year = objinputxml.DocumentElement.SelectSingleNode("TARGET").Attributes("YEAR").Value


            ObjInputXmlForPlan.LoadXml("<TA_AGENCY_PLAN_SEARCH_INPUT><LCODE></LCODE> <MONTH></MONTH> <YEAR></YEAR><EMPLOYEEID></EMPLOYEEID><RESP_1A></RESP_1A></TA_AGENCY_PLAN_SEARCH_INPUT>")
            '  ObjInputXmlForPlan.LoadXml("<TA_AGENCYTARGET_INPUT><CITY_ID /> <RESP_1A></RESP_1A> <MONTH></MONTH> <YEAR></YEAR> <VISITDONE></VISITDONE> <LIMITED_TO_AOFFICE /> <LIMITED_TO_REGION></LIMITED_TO_REGION> <LIMITED_TO_OWNAAGENCY /> <EMPLOYEEID></EMPLOYEEID> <PTARGET></PTARGET><PVISIT></PVISIT><SEARCHTYPE></SEARCHTYPE><AOFFICE></AOFFICE><REGION></REGION><CITY></CITY><ACATEGORY></ACATEGORY><AGENCY_NAME></AGENCY_NAME><LCODE></LCODE><OFFICEID></OFFICEID><WHOLEGROUP></WHOLEGROUP><COUNTRY></COUNTRY><VISIT_CATEGORY></VISIT_CATEGORY></TA_AGENCYTARGET_INPUT>")

            Dim objEmpXml As New XmlDocument
            Dim UserId As String
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()

            ObjInputXmlForPlan.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode
            ObjInputXmlForPlan.DocumentElement.SelectSingleNode("MONTH").InnerText = Month
            ObjInputXmlForPlan.DocumentElement.SelectSingleNode("YEAR").InnerText = Year
            ObjInputXmlForPlan.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = UserId
            ObjInputXmlForPlan.DocumentElement.SelectSingleNode("RESP_1A").InnerText = UserId



            If ViewState("Plandays") Is Nothing Then
                ' GetxmlOut for Database 
                objoutputxml = objbzAgencyTarget.GetVisitCalender(ObjInputXmlForPlan)

                If objoutputxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    'objoutputxml.LoadXml(objinputxml.OuterXml)
                    ViewState("Plandays") = objoutputxml.OuterXml
                Else
                    objoutputxml.LoadXml("<TA_AGENCYTARGET_OUTPUT> <TARGET MONTH='' YEAR='' LCODE='' CHAINCODE='' AGENCYNAME='' ADDRESS='' OFFICEID='' CITY='' GROUP_CATG='' RESP1A_ID='' RESP1A_NAME='' EMPLOYEEID='' AVGBIDT='' AVGMIDT='' PASTMOTIVE='' BUSINESSCCOMMIT='' MINIUMSEGS='' VISITCOUNT='' SEGSTARGET='' VISITTARGET='' PVISITDONE='' UVISITDONE='' LOGDATE='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' PLANNED_DAYS='' COLORCODE='' M_CHK_VT='' /></TA_AGENCYTARGET_OUTPUT>")

                    With objoutputxml.DocumentElement.SelectSingleNode("TARGET")
                        .Attributes("LCODE").Value = Lcode
                        .Attributes("YEAR").Value = Year
                        .Attributes("MONTH").Value = Month
                    End With
                    objFinaloutputxml.DocumentElement.AppendChild(objFinaloutputxml.ImportNode(objoutputxml.DocumentElement.SelectSingleNode("TARGET"), True))
                    ViewState("Plandays") = objFinaloutputxml.OuterXml
                    lblError.Text = objoutputxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Else
                objFinaloutputxml.LoadXml(ViewState("Plandays").ToString)
                objnode = objFinaloutputxml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + Lcode + "' and @MONTH='" + Month + "' and @YEAR='" + Year + "']")
                If objnode Is Nothing Then
                    ' GetxmlOut for Database 
                    objoutputxml = objbzAgencyTarget.GetVisitCalender(ObjInputXmlForPlan)
                    If objoutputxml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        objFinaloutputxml.DocumentElement.AppendChild(objFinaloutputxml.ImportNode(objoutputxml.DocumentElement.SelectSingleNode("TARGET"), True))
                        ViewState("Plandays") = objFinaloutputxml.OuterXml
                    Else
                        objoutputxml.LoadXml("<TA_AGENCYTARGET_OUTPUT> <TARGET MONTH='' YEAR='' LCODE='' CHAINCODE='' AGENCYNAME='' ADDRESS='' OFFICEID='' CITY='' GROUP_CATG='' RESP1A_ID='' RESP1A_NAME='' EMPLOYEEID='' AVGBIDT='' AVGMIDT='' PASTMOTIVE='' BUSINESSCCOMMIT='' MINIUMSEGS='' VISITCOUNT='' SEGSTARGET='' VISITTARGET='' PVISITDONE='' UVISITDONE='' LOGDATE='' D1='' D2='' D3='' D4='' D5='' D6='' D7='' D8='' D9='' D10='' D11='' D12='' D13='' D14='' D15='' D16='' D17='' D18='' D19='' D20='' D21='' D22='' D23='' D24='' D25='' D26='' D27='' D28='' D29='' D30='' D31='' PLANNED_DAYS='' COLORCODE='' M_CHK_VT='' /></TA_AGENCYTARGET_OUTPUT>")
                        With objoutputxml.DocumentElement.SelectSingleNode("TARGET")
                            .Attributes("LCODE").Value = Lcode
                            .Attributes("YEAR").Value = Year
                            .Attributes("MONTH").Value = Month
                        End With
                        objFinaloutputxml.DocumentElement.AppendChild(objFinaloutputxml.ImportNode(objoutputxml.DocumentElement.SelectSingleNode("TARGET"), True))
                        ViewState("Plandays") = objFinaloutputxml.OuterXml
                        lblError.Text = objoutputxml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            End If
        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ClnPlanDay_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles ClnPlanDay.VisibleMonthChanged
        Dim objout As New XmlDocument
        Dim objnode As XmlNode
        Try

            GetPlanDaysByMonthYearAndLcode(ClnPlanDay.VisibleDate.Month, ClnPlanDay.VisibleDate.Year, HdLCode.Value)
            hdYear.Value = ClnPlanDay.VisibleDate.Year
            hdMonth.Value = ClnPlanDay.VisibleDate.Month


            AllowSavingornot()


            If ViewState("Plandays") IsNot Nothing Then
                objout.LoadXml(ViewState("Plandays").ToString)
                objnode = objout.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @MONTH='" + hdMonth.Value + "' and @YEAR='" + hdYear.Value + "']")
                If objnode IsNot Nothing Then
                    Dim tempVisitDays As String = ""
                    HdUserDefinedVisitDays.Value = objnode.Attributes("PLANNED_DAYS").Value
                    HdMaxVisit.Value = objnode.Attributes("VISITCOUNT").Value
                    '@ Start of New Added Code on 6th Sep 2011
                    If HdUserDefinedVisitDays.Value.Trim.Length > 0 Then
                        If HdUserDefinedVisitDays.Value.Trim.Split("|").Length > 0 Then
                            For j As Int16 = 0 To HdUserDefinedVisitDays.Value.Trim.Split("|").Length - 1
                                If Val(HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString) <= Val(DateTime.DaysInMonth(CInt(hdYear.Value), CInt(hdMonth.Value))) Then
                                    If tempVisitDays.Trim.Length = 0 Then
                                        tempVisitDays = HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString
                                    Else
                                        tempVisitDays = tempVisitDays + "|" + HdUserDefinedVisitDays.Value.Trim.Split("|")(j).ToString
                                    End If
                                End If
                            Next
                        End If
                    End If
                    If HdUserDefinedVisitDays.Value.Trim <> tempVisitDays.Trim Then
                        HdUserDefinedVisitDays.Value = tempVisitDays
                    End If
                    '@ End of New Added Code on 6th Sep 2011
                    TxtMonth.Text = MonthName(hdMonth.Value)
                    TxtYear.Text = hdYear.Value
                    ClnPlanDay.VisibleDate = New DateTime(Int32.Parse(hdYear.Value.Trim), Int32.Parse(hdMonth.Value.Trim), 1)

                    If objnode.Attributes("PLANNED_DAYS").Value.Trim <> HdUserDefinedVisitDays.Value.Trim Then
                        objnode.Attributes("PLANNED_DAYS").Value = HdUserDefinedVisitDays.Value
                        ViewState("Plandays") = objout.OuterXml
                    End If
                End If
            End If
        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Session("Security") Is Nothing Then
            lblError.Text = "Session is expired."
            Exit Sub
        End If
    End Sub
    Private Sub AllowsavingorNot()
        BtnSave.Enabled = True
        '@ Start For Previous Month User can't change the data
        If Val(Now.Year) >= Val(hdYear.Value) Then
            If Val(Now.Year) = Val(hdYear.Value) Then
                If Val(Now.Month) > Val(hdMonth.Value) Then
                    BtnSave.Enabled = False
                End If
            Else
                BtnSave.Enabled = False
            End If
        End If
        '@ End of For Previous month saving is not allowed 
    End Sub
End Class

