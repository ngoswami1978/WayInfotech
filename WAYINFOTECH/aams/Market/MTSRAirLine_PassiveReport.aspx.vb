Imports System.IO
Imports System.Text
Partial Class Market_MTSRAirLine_PassiveReport
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Passive Report']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline Passive Report']").Attributes("Value").Value)
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
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            If Not IsPostBack Then
                BindAllControl()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AirLinePassiveSearch()
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region></PR_SEARCH_AIRLINEPASSIVE_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_AIRLINEPASSIVE_INPUT>")

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirline.SelectedValue
            End If
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
            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
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
                '  ViewState("SortName") = "SUMMARYTYPE"
                '  objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SUMMARYTYPE" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ' ViewState("Desc") = "FALSE"
                '  objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If


            ''@ New Code Added
            'If ViewState("PrevSearching") Is Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""
            'Else
            '    Dim objTempInputXml As New XmlDocument
            '    objTempInputXml.LoadXml(ViewState("PrevSearching"))
            '    If objTempInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "" And ViewState("SortName") Is Nothing Then
            '        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            '        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""
            '    End If
            'End If
            ''@ End of New code Added

            'End Code for paging and sorting
            'If objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SUMMARYTYPE" Then
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = rdSummOpt.SelectedItem.Text
            'End If

            objOutputXml = objbzMIDT.AirLinePassiveReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvAirPassive.DataSource = ds.Tables("AIRLINEPASSIVE")
                grdvAirPassive.DataBind()
                ' txtRecordCount.Text = ds.Tables("AIRLINEPASSIVE").Rows.Count.ToString
                ' lblFound.Visible = True
                'txtRecordCount.Visible = True

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                If objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText <> "" Then
                    SetImageForSorting(grdvAirPassive)
                End If


                '@ End of Code Added For Paging And Sorting 



            Else
                grdvAirPassive.DataSource = Nothing
                grdvAirPassive.DataBind()
                txtRecordCount.Text = "0"
                '  lblFound.Visible = True
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
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState("PrevSearching") = Nothing
        ViewState("SortName") = Nothing
        ViewState("Desc") = Nothing

        ddlPageNumber.Items.Clear()

        AirLinePassiveSearch()
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", False, 3)

            objeAAMS.BindDropDown(drpCountry, "COUNTRY", False, 3)

            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", False, 3)

            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)

            objeAAMS.BindDropDown(drpAirline, "AIRLINE", True, 1)

            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", False, 3)

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

        grdvAirPassive.AllowSorting = False
        grdvAirPassive.HeaderStyle.ForeColor = Drawing.Color.Black

        ' AirLinePassiveSearch2()
        AirLinePassiveNewExport()
        If grdvAirPassive.Rows.Count > 0 Then
            'PrepareGridViewForExport(grdvAirPassive)
            ' grdvAirPassive.Columns(grdvAirPassive.Columns.Count - 1).Visible = False
            ' ExportGridView(grdvAirPassive, "AirPassive.xls")
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

    Protected Sub grdvAirPassive_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirPassive.RowDataBound
        Try

            'If e.Row.RowType = DataControlRowType.Header Then
            '    If grdvAirPassive.AllowSorting = True Then
            '        ' e.Row.Cells(0).Text = CType(e.Row.Cells(0).Text, LinkButton)
            '    Else
            '        e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
            '    End If
            'End If

            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(1).Text = "Total"
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "PASSIVESEGMENTS")
                ' TotalPassivesum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotPasSeg As Label
                TotPasSeg = CType(e.Row.FindControl("TotPasSeg"), Label)
                'TotPasSeg.Text = String.Format("{0:d}", TotalPassivesum.ToString)
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        TotPasSeg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVESEGMENTS").ToString
                    End If
                End If

            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "ACTIVESEGMENTS")
                'TotalActivesum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotActSeg As Label
                TotActSeg = CType(e.Row.FindControl("TotActSeg"), Label)
                ' TotActSeg.Text = String.Format("{0:d}", TotalActivesum.ToString)
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        TotActSeg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("ACTIVESEGMENTS").ToString
                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "TOTALSEGMENTS")
                ' TotalSegsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotSeg As Label
                TotSeg = CType(e.Row.FindControl("TotSeg"), Label)
                'TotSeg.Text = String.Format("{0:d}", TotalSegsum.ToString)
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        TotSeg.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTALSEGMENTS").ToString
                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                ' Dim sum As Decimal = DataBinder.Eval(e.Row.DataItem, "PASSIVE_PER")
                ' TotalPercentsum += sum
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                Dim TotPassPercent As Label
                TotPassPercent = CType(e.Row.FindControl("TotPassPercent"), Label)
                ' TotPassPercent.Text = String.Format("{0:d}", TotalPercentsum.ToString)
                If FooterDataset IsNot Nothing Then
                    If FooterDataset.Tables("PAGE_TOTAL") IsNot Nothing Then
                        TotPassPercent.Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_PER").ToString
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MTSRAirLine_PassiveReport.aspx", False)
    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            AirLinePassiveSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            AirLinePassiveSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            AirLinePassiveSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvAirPassive_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvAirPassive.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvAirPassive_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirPassive.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                'ViewState("Desc") = "FALSE"

                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @ PASSIVESEGMENTS, ACTIVESEGMENTS, TOTALSEGMENTS, PASSIVE_PER
                If SortName.Trim().ToUpper = "PASSIVESEGMENTS" Or SortName.Trim().ToUpper = "ACTIVESEGMENTS" Or SortName.Trim().ToUpper = "TOTALSEGMENTS" Or SortName.Trim().ToUpper = "PASSIVE_PER" Then
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
                    'ViewState("Desc") = "FALSE"
                    '@ Added Code For Default descending sorting order on first time  of following Fields      
                    ' @ PASSIVESEGMENTS, ACTIVESEGMENTS, TOTALSEGMENTS, PASSIVE_PER
                    If SortName.Trim().ToUpper = "PASSIVESEGMENTS" Or SortName.Trim().ToUpper = "ACTIVESEGMENTS" Or SortName.Trim().ToUpper = "TOTALSEGMENTS" Or SortName.Trim().ToUpper = "PASSIVE_PER" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            AirLinePassiveSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvAirPassive_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirPassive.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            If e.Row.RowType = DataControlRowType.Header Then
                If grdvAirPassive.AllowSorting = True Then
                    CType(grvRow.Cells(0).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text
                Else
                    e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
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
    Private Sub AirLinePassiveSearch2()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region></PR_SEARCH_AIRLINEPASSIVE_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_AIRLINEPASSIVE_INPUT>")

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirline.SelectedValue
            End If
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
            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
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
                ViewState("SortName") = "SUMMRYTYPE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SUMMRYTYPE" '"LOCATION_CODE"
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


            objOutputXml = objbzMIDT.AirLinePassiveReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvAirPassive.DataSource = ds.Tables("AIRLINEPASSIVE")
                grdvAirPassive.DataBind()
            Else
                grdvAirPassive.DataSource = Nothing
                grdvAirPassive.DataBind()
                txtRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Private Sub AirLinePassiveNewExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region></PR_SEARCH_AIRLINEPASSIVE_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></PR_SEARCH_AIRLINEPASSIVE_INPUT>")

            If drpAirline.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AIRLINE_CODE").InnerText = drpAirline.SelectedValue
            End If
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

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If


            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("SMonth").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EMonth").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("SYear").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("EYear").InnerText = drpYearTo.SelectedValue
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
                ' ViewState("SortName") = "SUMMRYTYPE"
                'objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SUMMRYTYPE" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ' ViewState("Desc") = "FALSE"
                ' objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
            'End Code for paging and sorting

            '@ New Code Added
            'If ViewState("PrevSearching") Is Nothing Then
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            '    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""
            'Else
            '    Dim objTempInputXml As New XmlDocument
            '    objTempInputXml.LoadXml(ViewState("PrevSearching"))
            '    If objTempInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "" Then
            '        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ""
            '        objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ""
            '    End If

            'End If
            '@ End of New code Added

            objOutputXml = objbzMIDT.AirLinePassiveReport(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvAirPassive.DataSource = ds.Tables("AIRLINEPASSIVE")
                grdvAirPassive.DataBind()


                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("AIRLINEPASSIVE")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes("SUMMARYTYPE").Value = ""
                    .Attributes("CRSCODETEXT").Value = "Total"
                    .Attributes("PASSIVESEGMENTS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVESEGMENTS").ToString
                    .Attributes("ACTIVESEGMENTS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("ACTIVESEGMENTS").ToString
                    .Attributes("TOTALSEGMENTS").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTALSEGMENTS").ToString
                    .Attributes("PASSIVE_PER").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("PASSIVE_PER").ToString

                End With
                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel

                Dim strArray() As String = {rdSummOpt.SelectedItem.Text, "CRS", "Passive Segments", "Active Segments", "Total Segments", "Passive %"}

                Dim intArray() As Integer = {0, 1, 2, 3, 4, 5}

                objExport.ExportDetails(objOutputXmlExport, "AIRLINEPASSIVE", intArray, strArray, ExportExcel.ExportFormat.Excel, "AirlinepassiveReport.xls")


            Else
                grdvAirPassive.DataSource = Nothing
                grdvAirPassive.DataBind()
                txtRecordCount.Text = "0"
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
End Class
