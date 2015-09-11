Imports System.IO
Imports System.Text
Partial Class Market_MTSR_CityProductivityComparision
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim TotalPassivesum As Decimal = 0
    Dim TotalActivesum As Decimal = 0
    Dim TotalSegsum As Decimal = 0
    Dim TotalPercentsum As Decimal = 0
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
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            lblError.Text = ""

            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry');")
            drpAoffice.Attributes.Add("onkeyup", "return gotop('drpAoffice');")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion');")


            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Productivity Comparision']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Productivity Comparision']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        ChkUseOrignalBookData.Checked = False
                        ChkUseOrignalBookData.Visible = False
                        ' ChkListStatus.Items(0).Enabled = False
                        'ChkListStatus.Items(0).Selected = False
                    End If
                End If
            Else
                ChkUseOrignalBookData.Visible = True
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            ChkUseDailyBookData.Attributes.Add("onclick", "return SelectDateRange();")
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            If Not IsPostBack Then

                BindAllControl()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CityProductivityCompareSearch()
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim StartDate1 As String
        Dim StartDate2 As String
        Dim EndDate1 As String
        Dim EndDate2 As String
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT> <CITY></CITY> <COUNTRY></COUNTRY><GroupTypeID />   <Aoffice></Aoffice> <Region></Region> <FFDATE></FFDATE> <FTDATE></FTDATE> <SFDATE></SFDATE> <STDATE></STDATE> <OriginalBookings></OriginalBookings> <DailyBookingsData></DailyBookingsData> <Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT>")

           
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
            End If
            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text
            End If
            If ChkUseDailyBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "True"
          
                If Request("txtDateFrom1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom1.Text)
                End If
                If Request("txtDateTo1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo1.Text)
                End If
                If Request("txtDateFrom2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom2.Text)
                End If
                If Request("txtDateTo2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo2.Text)
                End If


            Else
                StartDate1 = "01" & "/" & drpMonthFrom.SelectedValue & "/" & drpYearFrom.SelectedValue
                EndDate1 = DateTime.DaysInMonth(drpYearTo.SelectedValue, drpMonthTo.SelectedValue) & "/" & drpMonthTo.SelectedValue & "/" & drpYearTo.SelectedValue
                StartDate2 = "01" & "/" & drpMonthFrom2.SelectedValue & "/" & drpYearFrom2.SelectedValue
                EndDate2 = DateTime.DaysInMonth(drpYearTo2.SelectedValue, drpMonthTo2.SelectedValue) & "/" & drpMonthTo2.SelectedValue & "/" & drpYearTo2.SelectedValue

                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "False"
                objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate1)
                objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(EndDate1)
                objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate2)
                objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(EndDate2)

            End If
            If ChkUseOrignalBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "False"
            End If

          

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
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
                ViewState("SortName") = "CITY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CITY" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting
         
            objOutputXml = objbzMIDT.CityProductivityComparision(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                ' lblFound.Visible = True
                grdvCityProductivity.DataSource = ds.Tables("CITYPRODUCTIVITY")
                grdvCityProductivity.DataBind()
                'txtRecordCount.Text = ds.Tables("CITYPRODUCTIVITY").Rows.Count.ToString
                ' txtRecordCount.Visible = True
                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvCityProductivity)
                '@ End of Code Added For Paging And Sorting 
            Else
                grdvCityProductivity.DataSource = Nothing
                grdvCityProductivity.DataBind()
                txtRecordCount.Text = "0"
                'lblFound.Visible = True
                'txtRecordCount.Visible = True
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("txtDateFrom1") IsNot Nothing Then
                txtDateFrom1.Text = Request("txtDateFrom1")
           
            End If
            If Request("txtDateTo1") IsNot Nothing Then
                txtDateTo1.Text = Request("txtDateTo1")
           
            End If
            If Request("txtDateFrom2") IsNot Nothing Then
                txtDateFrom2.Text = Request("txtDateFrom2")
            
            End If
            If Request("txtDateTo2") IsNot Nothing Then
                txtDateTo2.Text = Request("txtDateTo2")
           
            End If

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CityProductivityCompareSearch()
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)

            objeAAMS.BindDropDown(drpCity, "CITY", False, 3)

            objeAAMS.BindDropDown(drpCountry, "COUNTRY", False, 3)

            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", False, 3)

            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)



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

            txtDateFrom1.Text = "01" & "/" & "03" & "/" & DateTime.Now.Year
            txtDateTo1.Text = "31" & "/" & "03" & "/" & DateTime.Now.Year
            txtDateFrom2.Text = "01" & "/" & "04" & "/" & DateTime.Now.Year
            txtDateTo2.Text = "30" & "/" & "04" & "/" & DateTime.Now.Year
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

        grdvCityProductivity.AllowSorting = False
        grdvCityProductivity.HeaderStyle.ForeColor = Drawing.Color.Black
        ' CityProductivityCompareSearch2()
        CityProductivityCompareExport()
        If grdvCityProductivity.Rows.Count > 0 Then
            ' PrepareGridViewForExport(grdvCityProductivity)
            ' ExportGridView(grdvCityProductivity, "CityProductivity.xls")
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

    Protected Sub grdvCityProductivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCityProductivity.RowDataBound
        Try

            'If e.Row.RowType = DataControlRowType.Header Then

            '    If ChkUseDailyBookData.Checked = True Then

            '        Dim st1 As String
            '        Dim st2 As String
            '        Dim Et1 As String
            '        Dim Et2 As String

            '        If Request("txtDateFrom1") IsNot Nothing Then
            '            st1 = Request("txtDateFrom1")
            '        Else
            '            st1 = txtDateFrom1.Text
            '        End If
            '        If Request("txtDateTo1") IsNot Nothing Then
            '            Et1 = Request("txtDateTo1")
            '        Else
            '            Et1 = txtDateTo1.Text
            '        End If
            '        If Request("txtDateFrom2") IsNot Nothing Then
            '            st2 = Request("txtDateFrom2")
            '        Else
            '            st2 = txtDateFrom2.Text
            '        End If
            '        If Request("txtDateTo2") IsNot Nothing Then
            '            Et2 = Request("txtDateTo2")
            '        Else
            '            Et2 = txtDateTo2.Text
            '        End If


            '        e.Row.Cells(1).Text = MonthName(st1.Split("/")(1)) + "'" + st1.Split("/")(2) + " - " + MonthName(Et1.Split("/")(1)) + "'" + Et1.Split("/")(2)
            '        e.Row.Cells(2).Text = MonthName(st2.Split("/")(1)) + "'" + st2.Split("/")(2) + " - " + MonthName(Et2.Split("/")(1)) + "'" + Et2.Split("/")(2)
            '    Else
            '        e.Row.Cells(1).Text = drpMonthFrom.SelectedItem.Text + "'" + drpYearFrom.SelectedValue + " - " + drpMonthTo.SelectedItem.Text + "'" + drpYearTo.SelectedValue
            '        e.Row.Cells(2).Text = drpMonthFrom2.SelectedItem.Text + "'" + drpYearFrom2.SelectedValue + " - " + drpMonthTo2.SelectedItem.Text + "'" + drpYearTo2.SelectedValue
            '    End If

            'End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MTSR_CityProductivityComparision.aspx", False)
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            CityProductivityCompareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            CityProductivityCompareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            CityProductivityCompareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvCityProductivity_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvCityProductivity.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvCityProductivity_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvCityProductivity.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ' ViewState("Desc") = "FALSE"

                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @ CITY
                If SortName.Trim().ToUpper = "CITY" Then
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
                    ' @ CITY
                    If SortName.Trim().ToUpper = "CITY" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            CityProductivityCompareSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvCityProductivity_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCityProductivity.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            If e.Row.RowType = DataControlRowType.Header Then

                If ChkUseDailyBookData.Checked = True Then

                    Dim st1 As String
                    Dim st2 As String
                    Dim Et1 As String
                    Dim Et2 As String

                    If Request("txtDateFrom1") IsNot Nothing Then
                        st1 = Request("txtDateFrom1")
                    Else
                        st1 = txtDateFrom1.Text
                    End If
                    If Request("txtDateTo1") IsNot Nothing Then
                        Et1 = Request("txtDateTo1")
                    Else
                        Et1 = txtDateTo1.Text
                    End If
                    If Request("txtDateFrom2") IsNot Nothing Then
                        st2 = Request("txtDateFrom2")
                    Else
                        st2 = txtDateFrom2.Text
                    End If
                    If Request("txtDateTo2") IsNot Nothing Then
                        Et2 = Request("txtDateTo2")
                    Else
                        Et2 = txtDateTo2.Text
                    End If

                    If grdvCityProductivity.AllowSorting = True Then
                        CType(grvRow.Cells(1).Controls(0), LinkButton).Text = MonthName(st1.Split("/")(1)) + "'" + st1.Split("/")(2) + " - " + MonthName(Et1.Split("/")(1)) + "'" + Et1.Split("/")(2)
                        CType(grvRow.Cells(2).Controls(0), LinkButton).Text = MonthName(st2.Split("/")(1)) + "'" + st2.Split("/")(2) + " - " + MonthName(Et2.Split("/")(1)) + "'" + Et2.Split("/")(2)
                    Else
                        grvRow.Cells(1).Text = MonthName(st1.Split("/")(1)) + "'" + st1.Split("/")(2) + " - " + MonthName(Et1.Split("/")(1)) + "'" + Et1.Split("/")(2)
                        grvRow.Cells(2).Text = MonthName(st2.Split("/")(1)) + "'" + st2.Split("/")(2) + " - " + MonthName(Et2.Split("/")(1)) + "'" + Et2.Split("/")(2)
                    End If
                    ' e.Row.Cells(1).Text = MonthName(st1.Split("/")(1)) + "'" + st1.Split("/")(2) + " - " + MonthName(Et1.Split("/")(1)) + "'" + Et1.Split("/")(2)
                    'e.Row.Cells(2).Text = MonthName(st2.Split("/")(1)) + "'" + st2.Split("/")(2) + " - " + MonthName(Et2.Split("/")(1)) + "'" + Et2.Split("/")(2)
                Else
                    If grdvCityProductivity.AllowSorting = True Then
                        CType(grvRow.Cells(1).Controls(0), LinkButton).Text = drpMonthFrom.SelectedItem.Text + "'" + drpYearFrom.SelectedValue + " - " + drpMonthTo.SelectedItem.Text + "'" + drpYearTo.SelectedValue
                        CType(grvRow.Cells(2).Controls(0), LinkButton).Text = drpMonthFrom2.SelectedItem.Text + "'" + drpYearFrom2.SelectedValue + " - " + drpMonthTo2.SelectedItem.Text + "'" + drpYearTo2.SelectedValue
                    Else
                        grvRow.Cells(1).Text = drpMonthFrom.SelectedItem.Text + "'" + drpYearFrom.SelectedValue + " - " + drpMonthTo.SelectedItem.Text + "'" + drpYearTo.SelectedValue
                        grvRow.Cells(2).Text = drpMonthFrom2.SelectedItem.Text + "'" + drpYearFrom2.SelectedValue + " - " + drpMonthTo2.SelectedItem.Text + "'" + drpYearTo2.SelectedValue
                    End If
                    'e.Row.Cells(1).Text = drpMonthFrom.SelectedItem.Text + "'" + drpYearFrom.SelectedValue + " - " + drpMonthTo.SelectedItem.Text + "'" + drpYearTo.SelectedValue
                    ' e.Row.Cells(2).Text = drpMonthFrom2.SelectedItem.Text + "'" + drpYearFrom2.SelectedValue + " - " + drpMonthTo2.SelectedItem.Text + "'" + drpYearTo2.SelectedValue
                End If

            End If
        Catch ex As Exception

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

#End Region
    Private Sub CityProductivityCompareSearch2()
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim StartDate1 As String
        Dim StartDate2 As String
        Dim EndDate1 As String
        Dim EndDate2 As String
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT> <CITY></CITY> <COUNTRY></COUNTRY>   <Aoffice></Aoffice> <Region></Region> <FFDATE></FFDATE> <FTDATE></FTDATE> <SFDATE></SFDATE> <STDATE></STDATE> <OriginalBookings></OriginalBookings> <DailyBookingsData></DailyBookingsData> <Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT>")
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
            End If
            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text
            End If
            If ChkUseDailyBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "True"

                If Request("txtDateFrom1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom1.Text)
                End If
                If Request("txtDateTo1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo1.Text)
                End If
                If Request("txtDateFrom2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom2.Text)
                End If
                If Request("txtDateTo2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo2.Text)
                End If
            Else
                StartDate1 = "01" & "/" & drpMonthFrom.SelectedValue & "/" & drpYearFrom.SelectedValue
                EndDate1 = DateTime.DaysInMonth(drpYearTo.SelectedValue, drpMonthTo.SelectedValue) & "/" & drpMonthTo.SelectedValue & "/" & drpYearTo.SelectedValue
                StartDate2 = "01" & "/" & drpMonthFrom2.SelectedValue & "/" & drpYearFrom2.SelectedValue
                EndDate2 = DateTime.DaysInMonth(drpYearTo2.SelectedValue, drpMonthTo2.SelectedValue) & "/" & drpMonthTo2.SelectedValue & "/" & drpYearTo2.SelectedValue

                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "False"
                objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate1)
                objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(EndDate1)
                objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate2)
                objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(EndDate2)

            End If
            If ChkUseOrignalBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "False"
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            End If
            'Start CODE for sorting and paging          

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If
            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            objOutputXml = objbzMIDT.CityProductivityComparision(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                ' lblFound.Visible = True
                grdvCityProductivity.DataSource = ds.Tables("CITYPRODUCTIVITY")
                grdvCityProductivity.DataBind()
            Else
                grdvCityProductivity.DataSource = Nothing
                grdvCityProductivity.DataBind()
                txtRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("txtDateFrom1") IsNot Nothing Then
                txtDateFrom1.Text = Request("txtDateFrom1")
            End If
            If Request("txtDateTo1") IsNot Nothing Then
                txtDateTo1.Text = Request("txtDateTo1")
            End If
            If Request("txtDateFrom2") IsNot Nothing Then
                txtDateFrom2.Text = Request("txtDateFrom2")
            End If
            If Request("txtDateTo2") IsNot Nothing Then
                txtDateTo2.Text = Request("txtDateTo2")
            End If
        End Try
    End Sub

    Private Sub CityProductivityCompareExport()
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim StartDate1 As String
        Dim StartDate2 As String
        Dim EndDate1 As String
        Dim EndDate2 As String
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT> <CITY></CITY> <COUNTRY></COUNTRY> <GroupTypeID >  </GroupTypeID >    <Aoffice></Aoffice> <Region></Region> <FFDATE></FFDATE> <FTDATE></FTDATE> <SFDATE></SFDATE> <STDATE></STDATE> <OriginalBookings></OriginalBookings> <DailyBookingsData></DailyBookingsData> <Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCH_COMP_CITYPRODUCTIVITY_INPUT>")
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If
            If drpAoffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
            End If

            If drpRegion.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text
            End If
            If ChkUseDailyBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "True"

                If Request("txtDateFrom1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom1.Text)
                End If
                If Request("txtDateTo1") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo1"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo1.Text)
                End If
                If Request("txtDateFrom2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(txtDateFrom2.Text)
                End If
                If Request("txtDateTo2") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo2"))
                Else
                    objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(txtDateTo2.Text)
                End If
            Else
                StartDate1 = "01" & "/" & drpMonthFrom.SelectedValue & "/" & drpYearFrom.SelectedValue
                EndDate1 = DateTime.DaysInMonth(drpYearTo.SelectedValue, drpMonthTo.SelectedValue) & "/" & drpMonthTo.SelectedValue & "/" & drpYearTo.SelectedValue
                StartDate2 = "01" & "/" & drpMonthFrom2.SelectedValue & "/" & drpYearFrom2.SelectedValue
                EndDate2 = DateTime.DaysInMonth(drpYearTo2.SelectedValue, drpMonthTo2.SelectedValue) & "/" & drpMonthTo2.SelectedValue & "/" & drpYearTo2.SelectedValue

                objInputXml.DocumentElement.SelectSingleNode("DailyBookingsData").InnerText = "False"
                objInputXml.DocumentElement.SelectSingleNode("FFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate1)
                objInputXml.DocumentElement.SelectSingleNode("FTDATE").InnerText = objeAAMS.ConvertTextDate(EndDate1)
                objInputXml.DocumentElement.SelectSingleNode("SFDATE").InnerText = objeAAMS.ConvertTextDate(StartDate2)
                objInputXml.DocumentElement.SelectSingleNode("STDATE").InnerText = objeAAMS.ConvertTextDate(EndDate2)

            End If
            If ChkUseOrignalBookData.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("OriginalBookings").InnerText = "False"
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            End If

            'Start CODE for sorting and paging          

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting

            objOutputXml = objbzMIDT.CityProductivityComparision(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                ' lblFound.Visible = True
                grdvCityProductivity.DataSource = ds.Tables("CITYPRODUCTIVITY")
                grdvCityProductivity.DataBind()

                Dim objOutputXmlExport As New XmlDocument
                'Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                'objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("CITYPRODUCTIVITY")
                'objXmlNodeClone = objXmlNode.CloneNode(True)
                'For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                '    XmlAttr.Value = ""
                'Next
                'With objXmlNodeClone
                '    .Attributes("CITY").Value = ""
                '    .Attributes("COMPDATE1_TOTAL").Value = "Total"
                '    .Attributes("COMPDATE2_TOTAL").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVESEGMENTS").ToString
                '    .Attributes("DIFFERENCE").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("ACTIVESEGMENTS").ToString
                '    .Attributes("DIFFERENCE_PER").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTALSEGMENTS").ToString
                'End With
                'objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                Dim p1 As String = grdvCityProductivity.HeaderRow.Cells(1).Text.Replace("'", "")
                Dim p2 As String = grdvCityProductivity.HeaderRow.Cells(2).Text.Replace("'", "")
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"City", p1, p2, "Difference", "Difference %"}
                Dim intArray() As Integer = {0, 1, 2, 3, 4}
                objExport.ExportDetails(objOutputXmlExport, "CITYPRODUCTIVITY", intArray, strArray, ExportExcel.ExportFormat.Excel, "CityProductivityCompare.xls")
            Else
                grdvCityProductivity.DataSource = Nothing
                grdvCityProductivity.DataBind()
                txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            pnlPaging.Visible = False
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If Request("txtDateFrom1") IsNot Nothing Then
                txtDateFrom1.Text = Request("txtDateFrom1")

            End If
            If Request("txtDateTo1") IsNot Nothing Then
                txtDateTo1.Text = Request("txtDateTo1")

            End If
            If Request("txtDateFrom2") IsNot Nothing Then
                txtDateFrom2.Text = Request("txtDateFrom2")
            End If
            If Request("txtDateTo2") IsNot Nothing Then
                txtDateTo2.Text = Request("txtDateTo2")
            End If
        End Try
    End Sub

End Class
