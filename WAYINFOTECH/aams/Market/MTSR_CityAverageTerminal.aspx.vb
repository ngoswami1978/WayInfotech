Imports System.IO
Imports System.Xml

Partial Class Market_MTSR_CityAverageTerminal
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
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
            ' This code is used for Expiration of Page From Cache
            objEams.ExpirePageCache()
            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity')")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry')")
            drpOneAoffice.Attributes.Add("onkeyup", "return gotop('drpOneAoffice')")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")
            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"
            'Code for Paging $ Sorting
            ' btnPrint.Attributes.Add("onclick", "return CallPrint('grdvCityAvgTerminal')")

            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                LoadAllControls()
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Average Terminal']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City Average Terminal']").Attributes("Value").Value)
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
                strBuilder = objEams.SecurityCheck(31)
            End If
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Count <> 0 Then
                    strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Non Billable Segments']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        chkShowBr.Checked = False
                        chkShowBr.Visible = False
                        ' ChkListStatus.Items(0).Enabled = False
                        'ChkListStatus.Items(0).Selected = False
                    End If
                End If
            Else
                chkShowBr.Visible = True
                strBuilder = objEams.SecurityCheck(31)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try


        'Dim objInputXml, objOutputXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        'Dim ds As New DataSet
        'Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance

        'objInputXml.LoadXml("<PR_SEARCHCITYAVERAGETERMINAL_INPUT><MONTH></MONTH><YEAR></YEAR><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><USEORIGINAL></USEORIGINAL><LIMITED_TO_REGION></LIMITED_TO_REGION></PR_SEARCHCITYAVERAGETERMINAL_INPUT>")

        'With objInputXml.DocumentElement

        '    .SelectSingleNode("MONTH").InnerText = drpMonthF.SelectedIndex + 1

        '    .SelectSingleNode("YEAR").InnerText = drpYearTo.SelectedItem.Text.Trim()

        '    If drpCity.SelectedIndex <> 0 Then
        '        .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
        '    End If
        '    If drpCountry.SelectedIndex <> 0 Then
        '        .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
        '    End If

        '    If drpOneAoffice.SelectedIndex <> 0 Then
        '        .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
        '    End If

        '    If drpRegion.SelectedIndex <> 0 Then
        '        .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
        '    End If



        '    If chkShowBr.Checked = True Then
        '        .SelectSingleNode("USEORIGINAL").InnerText = "1"
        '    Else
        '        .SelectSingleNode("USEORIGINAL").InnerText = "0"
        '    End If
        '    'Following Statement is written for Limited to own Agency
        '    Dim xDoc As New XmlDocument
        '    xDoc.LoadXml(Session("Security"))

        '    '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

        '    If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
        '        .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
        '    End If
        'End With

        'objOutputXml = objbzDailyBooking.SearchCityAverageTerminal(objInputXml)


        'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
        '    objXmlReader = New XmlNodeReader(objOutputXml)
        '    ds.ReadXml(objXmlReader)
        '    lblError.Text = ""
        '    grdvCityAvgTerminal.DataSource = ds.Tables("DETAIL")
        '    grdvCityAvgTerminal.DataBind()
        'Else
        '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        'End If
       
    End Sub
    Private Sub BindGrid()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance

        objInputXml.LoadXml("<PR_SEARCHCITYAVERAGETERMINAL_INPUT><GroupTypeID></GroupTypeID><MONTH></MONTH><YEAR></YEAR><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><USEORIGINAL></USEORIGINAL><LIMITED_TO_REGION></LIMITED_TO_REGION>   <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHCITYAVERAGETERMINAL_INPUT>")

        With objInputXml.DocumentElement

            .SelectSingleNode("MONTH").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("YEAR").InnerText = drpYearTo.SelectedItem.Text.Trim()

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If
            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
            End If



            If chkShowBr.Checked = True Then
                .SelectSingleNode("USEORIGINAL").InnerText = "Y"
            Else
                .SelectSingleNode("USEORIGINAL").InnerText = "N"
            End If
            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If


            If drpLstGroupType.SelectedIndex <> 0 Then
                .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

        End With



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
            If (ViewState("SortName").ToString().Trim() = "PASSIVE" And chkShowBr.Checked = False) Or (ViewState("SortName").ToString().Trim() = "WITHPASSIVE" And chkShowBr.Checked = False) Then
                ViewState("SortName") = "CITY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "CITY"
                ViewState("Desc") = "FALSE"
            End If
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else

            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If



        'If ViewState("SortName") Is Nothing Then
        '    ViewState("SortName") = "AgencyName"
        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
        'Else
        '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")


        '    If ViewState("SortName").ToString().Trim().ToUpper() = "CHAIN_CODE" And chkShowChaniCode.Checked = False Then
        '        ViewState("SortName") = "AgencyName"
        '        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
        '        ViewState("Desc") = Nothing
        '    End If

        '    If ViewState("SortName").ToString().Trim().ToUpper() = "ADDRESS" And chkShowAddress.Checked = False Then
        '        ViewState("SortName") = "AgencyName"
        '        objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
        '        ViewState("Desc") = Nothing
        '    End If

        'End If










        objOutputXml = objbzDailyBooking.SearchCityAverageTerminal(objInputXml)


        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
            ViewState("PrevSearching") = objInputXml.OuterXml

            grdvCityAvgTerminal.DataSource = ds.Tables("DETAIL")
            grdvCityAvgTerminal.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(grdvCityAvgTerminal)
            If ViewState("Desc") = "FALSE" Then
                grdvCityAvgTerminal.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                grdvCityAvgTerminal.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

            ' txtRecordCount.Text = ds.Tables("DETAIL").Rows.Count.ToString
        Else
            'txtRecordCount.Text = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Private Sub LoadAllControls()
        Try
            'objEams.BindDropDown(drpAirLineName, "AIRLINE", False)
            objEams.BindDropDown(drpCity, "CITY", False, 3)
            ' drpCity.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpCountry, "COUNTRY", False, 3)
            ' drpCountry.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpOneAoffice, "AOFFICE", False, 3)
            ' drpOneAoffice.Items.Insert(0, New ListItem("All", ""))
            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
            ' drpRegion.Items.Insert(0, New ListItem("All", ""))

            Dim dtYear As New DateTime
            Dim counter As Integer
            For counter = DateTime.Now.Year To 1990 Step -1

                drpYearTo.Items.Add(counter.ToString())
            Next
            For counter = 0 To 11
                drpMonthF.Items.Add(MonthName(counter + 1))
            Next
            drpMonthF.SelectedIndex = 0
            drpYearTo.SelectedValue = DateTime.Now.Year

            Dim objSecurityXml As New XmlDocument

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpOneAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpOneAoffice.SelectedValue = li.Value

                            End If
                        End If
                        drpOneAoffice.Enabled = False
                    End If
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            '  Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
            Dim objExcelExp As New ExportExcel

            objInputXml.LoadXml("<PR_SEARCHCITYAVERAGETERMINAL_INPUT><GroupTypeID></GroupTypeID><MONTH></MONTH><YEAR></YEAR><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><USEORIGINAL></USEORIGINAL><LIMITED_TO_REGION></LIMITED_TO_REGION>   <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHCITYAVERAGETERMINAL_INPUT>")

            With objInputXml.DocumentElement

                .SelectSingleNode("MONTH").InnerText = drpMonthF.SelectedIndex + 1

                .SelectSingleNode("YEAR").InnerText = drpYearTo.SelectedItem.Text.Trim()

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpOneAoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
                End If



                If chkShowBr.Checked = True Then
                    .SelectSingleNode("USEORIGINAL").InnerText = "Y"
                Else
                    .SelectSingleNode("USEORIGINAL").InnerText = "N"
                End If
                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If
            End With




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

            objOutputXml = objbzDailyBooking.SearchCityAverageTerminal(objInputXml)





            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'Dim objOutputXmlExport As New XmlDocument
                'Dim objXmlNode, objXmlNodeClone As XmlNode
                Dim arlstNo As New ArrayList
                Dim arlstCoName As New ArrayList
                arlstCoName.Insert(0, "City")
                arlstCoName.Insert(1, "Productivity")
                arlstCoName.Insert(2, "NBS")
                arlstCoName.Insert(3, "With NBS")
                arlstCoName.Insert(4, "PC Count")
                arlstCoName.Insert(5, "Average Per PC")
                Dim counter As Integer = 0
                For counter = 0 To 5
                    arlstNo.Insert(counter, counter)
                Next

                If chkShowBr.Checked = False Then
                    arlstCoName.RemoveRange(2, 2)
                    arlstNo.RemoveRange(2, 2)
                End If

                '' Dim objXmlReader As XmlNodeReader

                'objXmlReader = New XmlNodeReader(objOutputXml)
                'ds.ReadXml(objXmlReader)

                'objOutputXmlExport.LoadXml(objOutputXml.OuterXml)
                'objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAIL")
                'objXmlNodeClone = objXmlNode.CloneNode(True)
                'For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                '    XmlAttr.Value = ""
                'Next

                'With objXmlNodeClone
                '    If ds IsNot Nothing Then
                '        .Attributes("CITY").Value = "Total "
                '        .Attributes("PRODUCTIVITY").Value = ds.Tables("PAGE_TOTAL").Rows(0)("PRODUCTIVITY").ToString()
                '        .Attributes("PCCOUNT").Value = ds.Tables("PAGE_TOTAL").Rows(0)("PCCOUNT").ToString() 'hdTARGETPERDAY.Value
                '        .Attributes("TAVERAGE").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TAVERAGE").ToString() 'hdAverageBookings.Value.Trim()
                '    End If
                'End With

                'objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)

                objExcelExp.ExportDetails(objOutputXml, "DETAIL", arlstNo, arlstCoName, ExportExcel.ExportFormat.Excel, "exportREport.xls")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            'Code for Exporting Old way

            'grdvCityAvgTerminal.AllowSorting = False
            'grdvCityAvgTerminal.HeaderStyle.ForeColor = Drawing.Color.Black
            ' BindGridExport()
            '' PrepareGridViewForExport(grdvCityAvgTerminal)
            'ExportGridView(grdvCityAvgTerminal)

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ExportGridView(ByVal grdvCommon)
        Dim strFileName As String = CType(grdvCommon, GridView).ID
        strFileName = strFileName.Substring(4)
        Dim attachment As String = "attachment; filename=" & strFileName & ".xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter
        Dim htw As New HtmlTextWriter(sw)
        Dim frm As New HtmlForm()
        grdvCommon.Parent.Controls.Add(frm)
        frm.Attributes("runat") = "server"
        frm.Controls.Add(grdvCommon)
        frm.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.End()
    End Sub
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        Dim l As New Literal
        Dim name As String = ""
        Dim lb As New System.Web.UI.HtmlControls.HtmlAnchor

        Dim i As Int32
        For i = 0 To gv.Controls.Count - 1
            If (gv.Controls(i).GetType Is GetType(HtmlAnchor)) Then
                l.Text = CType(gv.Controls(i), HtmlAnchor).Name
                gv.Controls.Remove(gv.Controls(i))
                gv.Controls.AddAt(i, l)

            End If

            If (gv.Controls(i).HasControls()) Then
                PrepareGridViewForExport(gv.Controls(i))
            End If

        Next
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("MTSR_CityAverageTerminal.aspx")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

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
                '  ViewState("Desc") = "FALSE"
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
        BindGrid()
    End Sub
#End Region
    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function
    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
        pnlPaging.Visible = True
        Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
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
            'pnlPaging.Visible = False
            ' ddlPageNumber.Visible = False
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
            'ddlPageNumber.Visible = True
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub

    Protected Sub grdvCityAvgTerminal_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvCityAvgTerminal.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGridExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance

        objInputXml.LoadXml("<PR_SEARCHCITYAVERAGETERMINAL_INPUT><MONTH></MONTH><YEAR></YEAR><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><USEORIGINAL></USEORIGINAL><LIMITED_TO_REGION></LIMITED_TO_REGION>   <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHCITYAVERAGETERMINAL_INPUT>")

        With objInputXml.DocumentElement

            .SelectSingleNode("MONTH").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("YEAR").InnerText = drpYearTo.SelectedItem.Text.Trim()

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If
            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
            End If



            If chkShowBr.Checked = True Then
                .SelectSingleNode("USEORIGINAL").InnerText = "1"
            Else
                .SelectSingleNode("USEORIGINAL").InnerText = "0"
            End If
            'Following Statement is written for Limited to own Agency
            Dim xDoc As New XmlDocument
            xDoc.LoadXml(Session("Security"))

            '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

            If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                .SelectSingleNode("LIMITED_TO_REGION").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
            End If
        End With




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

        objOutputXml = objbzDailyBooking.SearchCityAverageTerminal(objInputXml)


        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
            ViewState("PrevSearching") = objInputXml.OuterXml

            grdvCityAvgTerminal.DataSource = ds.Tables("DETAIL")
            grdvCityAvgTerminal.DataBind()
        Else
            'txtRecordCount.Text = "0"
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub grdvCityAvgTerminal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvCityAvgTerminal.RowDataBound
        Try
            If chkShowBr.Checked = False Then
                grdvCityAvgTerminal.Columns(2).Visible = False
                grdvCityAvgTerminal.Columns(3).Visible = False
            Else
                grdvCityAvgTerminal.Columns(2).Visible = True
                grdvCityAvgTerminal.Columns(3).Visible = True
            End If
           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
        
    End Sub
End Class
