
Partial Class Sales_SAUP_PlanDayCalender
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("FinalSesseionXML") Is Nothing Then
            lblError.Text = "Session is Exired."
            BtnSave.Enabled = False
            ClientScript.RegisterStartupScript(Me.GetType(), "close", " window.parent.document.getElementById('BtnRefreshGrid').click(); window.parent.document.getElementById('iframeID').src='';window.parent.document.getElementById('BtnCancel').click();", True)
            Exit Sub
        End If
        Session("UpdateData") = Nothing
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)

        Try
            If Not Me.IsPostBack Then
                ' lblError.Text = "..Loading.."

                If Request.QueryString("Month") IsNot Nothing Then
                    hdMonth.Value = Request.QueryString("Month").ToString
                End If
                If Request.QueryString("Year") IsNot Nothing Then
                    hdYear.Value = Request.QueryString("Year").ToString
                End If
                If Request.QueryString("MaxVisit") IsNot Nothing Then
                    HdMaxVisit.Value = Request.QueryString("MaxVisit").ToString
                End If
                If Request.QueryString("UserDefVisit") IsNot Nothing Then
                    HdUserDefinedVisit.Value = Request.QueryString("UserDefVisit").ToString
                End If
                If Request.QueryString("VT") IsNot Nothing Then
                    hdchk_VisitTarget.Value = Request.QueryString("VT").ToString
                End If

                If Request.QueryString("Lcode") IsNot Nothing Then
                    HdLCode.Value = Request.QueryString("Lcode").ToString
                    AgencyView()
                End If
                Dim tempVisitDays As String = ""
                If Request.QueryString("UserDefVisitDays") IsNot Nothing Then
                    HdUserDefinedVisitDays.Value = Request.QueryString("UserDefVisitDays").ToString


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

                End If
                TxtMonth.Text = MonthName(hdMonth.Value)
                TxtYear.Text = hdYear.Value

                ClnPlanDay.ShowNextPrevMonth = False
                ClnPlanDay.VisibleDate = New DateTime(Int32.Parse(hdYear.Value.Trim), Int32.Parse(hdMonth.Value.Trim), 1)


                ' If ViewState("PlanDays") Is Nothing Then
                Dim dates As New ArrayList
                Dim StrSelectedDateByUser As String = ""
                If HdUserDefinedVisitDays.Value.Trim.Length > 0 Then
                    StrSelectedDateByUser = HdUserDefinedVisitDays.Value.Trim()
                    If StrSelectedDateByUser.Split("|").Length > 0 Then
                        For j As Int16 = 0 To StrSelectedDateByUser.Split("|").Length - 1
                            dates.Add(CInt(StrSelectedDateByUser.Split("|")(j).ToString))
                        Next
                    End If
                End If
                ViewState("PlanDays") = dates
                'End If

                ' lblError.Text = ""
            End If

            'Making Right for Changing the Days

            If Not IsPostBack Then
                '####################################################################
                '@ Start of Codition for Modification of Agency Target             

                '@ Start For User only change or save the data on 1 to 7th days for current Month and also allow on any days of cuurent month for next month in advance.

                Dim BlnForCurrentMonth As Boolean = False
                If Val(Now.Month) = Val(hdMonth.Value) AndAlso Val(Now.Year) = Val(hdYear.Value) Then
                    BlnForCurrentMonth = True
                End If

                If BlnForCurrentMonth = True Then
                    Dim ObjXmlNodeSales_Default As XmlNode
                    Dim objSecurityXml As New XmlDataDocument
                    If Session("Security") IsNot Nothing Then
                        objSecurityXml.LoadXml(Session("Security"))
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                            ObjXmlNodeSales_Default = objSecurityXml.DocumentElement.SelectSingleNode("Sales_Default/SL_Default")
                            If ObjXmlNodeSales_Default IsNot Nothing Then
                                If ObjXmlNodeSales_Default.Attributes("TARGET_CHECK").Value.Trim.ToUpper = "TRUE" Then
                                    If Now.Day <= Val(ObjXmlNodeSales_Default.Attributes("VISIT_TARGET_DAYS").Value) Then
                                        BtnSave.Enabled = True
                                        ClnPlanDay.Enabled = True
                                    Else
                                        BtnSave.Enabled = False
                                        ClnPlanDay.Enabled = False
                                    End If
                                End If
                            End If
                        End If
                    Else
                    End If
                End If
                '@ End For User only change or save the data on 1 to 7th days for current Month and also allow on any days of cuurent month for next month in advance.

                '@ Start For Previous Month User can't change the data
                If Val(Now.Year) >= Val(hdYear.Value) Then
                    If Val(Now.Year) = Val(hdYear.Value) Then
                        If Val(Now.Month) > Val(hdMonth.Value) Then
                            BtnSave.Enabled = False
                            ClnPlanDay.Enabled = False
                        End If
                    Else
                        BtnSave.Enabled = False
                        ClnPlanDay.Enabled = False
                    End If
                End If

                '@ End For Previous Month User can't change the data
                '####################################################################
            End If


            '@  Prev Start of Codition for Modification of Agency Target
            'If Not IsPostBack Then
            '    Dim ObjXmlNodeSales_Default As XmlNode
            '    Dim objSecurityXml As New XmlDataDocument
            '    If Session("Security") IsNot Nothing Then
            '        objSecurityXml.LoadXml(Session("Security"))
            '        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '            ObjXmlNodeSales_Default = objSecurityXml.DocumentElement.SelectSingleNode("Sales_Default/SL_Default")
            '            If ObjXmlNodeSales_Default IsNot Nothing Then
            '                If ObjXmlNodeSales_Default.Attributes("TARGET_CHECK").Value.Trim.ToUpper = "TRUE" Then
            '                    If Now.Day <= Val(ObjXmlNodeSales_Default.Attributes("VISIT_TARGET_DAYS").Value) Or Now.Day = DateTime.DaysInMonth(Now.Year, Now.Month - 1) Then
            '                        BtnSave.Enabled = True
            '                        ClnPlanDay.Enabled = True
            '                    Else
            '                        BtnSave.Enabled = False
            '                        ClnPlanDay.Enabled = False
            '                    End If
            '                End If
            '            End If
            '        End If
            '    Else
            '    End If
            'End If

            '@ Prev End of Codition for Modification of Agency Target






        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

       
    End Sub

    Protected Sub ClnPlanDay_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles ClnPlanDay.DayRender
        Try
            ClnPlanDay.SelectedDates.Clear()
            e.Cell.Attributes.Add("onclick", "return CalenderUpdation('" + e.Day.DayNumberText + "')")

            If e.Day.IsWeekend Then
                ' e.Cell.BackColor = Drawing.Color.Gray
                e.Cell.CssClass = "Weekend"
            End If

            Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)
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


            '@ Working For Saturday and Sunday 
            'e.Day.IsWeekend
            'If e.Day.Date.ToString("ddd").Trim.ToUpper() = "SUN" Or e.Day.Date.ToString("ddd").Trim.ToUpper() = "SAT" Then
            '    '   e.Day.IsSelectable = False
            'End If


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
            Dim dates As New ArrayList
            ClnPlanDay.SelectedDates.Clear()
            Dim StrSelectedDateByUser As String = ""
            If HdUserDefinedVisitDays.Value.Trim.Length > 0 Then

                StrSelectedDateByUser = HdUserDefinedVisitDays.Value.Trim()
                If StrSelectedDateByUser.Split("|").Length > 0 Then
                    For j As Int16 = 0 To StrSelectedDateByUser.Split("|").Length - 1
                        dates.Add(CInt(StrSelectedDateByUser.Split("|")(j).ToString))
                    Next
                End If
            End If
            ViewState("PlanDays") = dates
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim objInputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As DataSet
        Dim ChangedData As Boolean = True
        Try
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
                Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)

                '@ Start of new code added
                If hdchk_VisitTarget.Value.ToUpper = "TRUE" Then ''This upper part of if added by Tapan Nath
                    If dates.Count < HdMaxVisit.Value Then
                        LblCalenderError.Text = "Visit Plan should be equal to or Greater than the objective visit target."
                        Exit Sub
                    End If
                End If
                '@ End of new code added

                Dim objnode As XmlNode = objInputXml.DocumentElement.SelectSingleNode("TARGET[@LCODE='" + HdLCode.Value + "' and @YEAR='" + hdYear.Value + "' and @MONTH='" + hdMonth.Value + "'  ]")
                Dim strPllanedDays As String = ""

                '
                For i As Integer = 0 To dates.Count - 1
                    If strPllanedDays.Trim.Length > 0 Then
                        strPllanedDays = strPllanedDays + "|" + dates(i).ToString()
                    Else
                        strPllanedDays = dates(i).ToString()
                    End If
                    If objnode IsNot Nothing Then
                        Dim objAttributesName As String = "D" + dates(i).ToString
                        If Not (objnode.Attributes(objAttributesName).Value = "2" Or objnode.Attributes(objAttributesName).Value = "4" Or objnode.Attributes(objAttributesName).Value = "5" Or objnode.Attributes(objAttributesName).Value = "7") Then ' Update Calender for Plan Days if on that days  either plan is visited/not visited
                            objnode.Attributes(objAttributesName).Value = "1" ' Setting PLannedDays
                        End If

                    End If
                Next

                If strPllanedDays.ToString.Trim = objnode.Attributes("PLANNED_DAYS").Value.ToString.Trim Then
                    ChangedData = False
                End If

                objnode.Attributes("PLANNED_DAYS").Value = strPllanedDays

                If ChangedData = True Then
                    Dim PrevVisitTarget As Integer = Val(objnode.Attributes("VISITTARGET").Value)
                    objnode.Attributes("VISITTARGET").Value = strPllanedDays.Split("|").Length

                    If objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value = Val(objInputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes("VISITTARGET").Value) + Val(objnode.Attributes("VISITTARGET").Value) - PrevVisitTarget
                    End If

                End If



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

                objXmlReader = New XmlNodeReader(objInputXml)
                ds = New DataSet
                ds.ReadXml(objXmlReader)
                Session("AgencyTargetDataSource") = ds.Tables("TARGET")
                If Not Session("AgencySearchTargetXML") Is Nothing Then
                    Session("AgencySearchTargetXML") = objInputXml.OuterXml
                End If
                Session("FinalSesseionXML") = objInputXml.OuterXml

                'Dim strScript As String = "window.opener.document.forms['form1'].submit(); window.close();"
                'ClientScript.RegisterStartupScript(Me.GetType(), "strScript", strScript)
                Session("UpdateData") = "UpdateData"
                ' ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.opener.document.forms['form1'].submit();window.close();", True)
                '   ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.document.frames.parent.document.forms['form1'].submit();", True)

                If ChangedData = False Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "close", "window.parent.document.getElementById('iframeID').src='';window.parent.document.getElementById('BtnCancel').click();", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "close", " window.parent.document.getElementById('BtnRefreshGrid').click();window.parent.document.getElementById('iframeID').src=''; window.parent.document.getElementById('BtnCancel').click();", True)
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
        ClnPlanDay.SelectedDates.Clear()
        Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)
        Dim Newdates As New ArrayList
        ViewState("PlanDays") = Newdates
        For i As Integer = 0 To dates.Count - 1
            If (visitedPlanDay(dates(i).ToString) Or UnvisitedPlanDay(dates(i).ToString)) Then
                '  Add Those Items Whose already not already is visited/Not Visited in plan Days To Calender
                Newdates.Add(dates(i))
            End If
        Next
        dates.Clear()

        ViewState("PlanDays") = Newdates
    End Sub

    Private Function visitedPlanDay(ByVal Day As String)
        Dim objInputXml As New XmlDocument
        Dim blnVistitedPlanDay As Boolean = False
        Try
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
            If Session("FinalSesseionXML") IsNot Nothing Then
                objInputXml.LoadXml(Session("FinalSesseionXML").ToString)
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
        Try
            '@ Start of Previous Code
            'LblCalenderError.Text = ""
            'Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)

            'If dates.Contains(CInt(Val(HdSelectedDay.Value))) Then
            '    dates.Remove(CInt(Val(HdSelectedDay.Value)))
            'Else
            '    If dates.Count < HdMaxVisit.Value Then
            '        If HdSelectedDay.Value.Trim.Length > 0 Then
            '            dates.Add(CInt(Val(HdSelectedDay.Value)))
            '        End If
            '    Else
            '        If Val(HdMaxVisit.Value) = 0 Then
            '            LblCalenderError.Text = "Can’t plan visit for this category."
            '        Else
            '            LblCalenderError.Text = "Over exceeded from Sales Objective Visit."
            '        End If
            '    End If
            'End If
            'ViewState("PlanDays") = dates
            '@ End of Previous Code


            '@ Start of New Code

            LblCalenderError.Text = ""
            If Val(HdMaxVisit.Value) = 0 Then
                LblCalenderError.Text = "Can’t plan visit for this category."
                Exit Sub
            End If
            Dim dates As ArrayList = TryCast(ViewState("PlanDays"), ArrayList)
            If dates.Contains(CInt(Val(HdSelectedDay.Value))) Then
                dates.Remove(CInt(Val(HdSelectedDay.Value)))
            Else
                If HdSelectedDay.Value.Trim.Length > 0 Then
                    dates.Add(CInt(Val(HdSelectedDay.Value)))
                End If
            End If
            ViewState("PlanDays") = dates
            '@ End of New code

        Catch ex As Exception
            LblCalenderError.Text = ex.Message
        End Try

    End Sub

   
    Protected Sub BtnFake_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFake.Click
        UpdateByClientSideCalender()
    End Sub

  
End Class

