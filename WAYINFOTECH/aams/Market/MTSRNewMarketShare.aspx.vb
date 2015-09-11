
Partial Class Market_MTSRNewMarketShare
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
        Dim strurl As String = Request.Url.ToString()
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        Try
            Session("PageName") = strurl
            lblError.Text = ""
            drpCity.Attributes.Add("onkeyup", "return gotop('drpCity');")
            drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry');")
            drpAoffice.Attributes.Add("onkeyup", "return gotop('drpAoffice');")
            drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion');")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())

                'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Market Share']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(0) = "0" Then
                        btnExport.Enabled = False
                        BtnGraph.Enabled = False
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                        BtnGraph.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            btnSearch.Attributes.Add("onclick", "return CheckValidation();")
            btnExport.Attributes.Add("onclick", "return CheckValidation();")
            BtnGraph.Attributes.Add("onclick", "return CheckValidation();")
            If Not IsPostBack Then
                Session("MarketShareGraph") = Nothing
                BindAllControl()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 3)
            objeAAMS.BindDropDown(drpCity, "CITY", False, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", False, 3)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", False, 3)
            objeAAMS.BindDropDown(drpRegion, "REGION", False, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
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

    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Response.Redirect("MTSRNewMarketShare.aspx", False)
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        MarketShareSearch()
    End Sub

    Private Sub MarketShareSearch()
        txtRecordCount.Text = "0"
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMarketShare As New AAMS.bizProductivity.bzMarketShare
        Try
            ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region></PR_SEARCH_AIRLINEPASSIVE_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_MARKETSHARE_INPUT> <COUNTRY></COUNTRY> <CITY></CITY> <AOFFICE></AOFFICE><REGION></REGION> <MONTHFROM></MONTHFROM> <YEARFROM></YEARFROM> <MONTHTO></MONTHTO> <YEARTO></YEARTO> <GroupTypeID /><SELECTBY/> <LIMITED_TO_REGION></LIMITED_TO_REGION>  <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY> <LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_MARKETSHARE_INPUT>")

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
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

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""


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
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
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
                ViewState("SortName") = "SELECTBY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
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
            'If objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" Then
            '    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = rdSummOpt.SelectedItem.Text
            'End If

            objOutputXml = objbzMarketShare.SearchMarketShare(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvMarketShare.DataSource = ds.Tables("DETAIL")
                grdvMarketShare.DataBind()

                '@ Code Added For Paging And Sorting 
                pnlPaging.Visible = True
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvMarketShare)
                '@ End of Code Added For Paging And Sorting 
            Else
                grdvMarketShare.DataSource = Nothing
                grdvMarketShare.DataBind()
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
        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            MarketShareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            MarketShareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            MarketShareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvMarketShare_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvMarketShare.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvMarketShare_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvMarketShare.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ' ViewState("Desc") = "FALSE"
                '@ Added Code For Default descending sorting order on first time  of following Fields      
                ' @ SELECTBY
                If SortName.Trim().ToUpper = "SELECTBY" Then
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
                    'ViewState("Desc") = "FALSE"
                    '@ Added Code For Default descending sorting order on first time  of following Fields      
                    ' @ SELECTBY
                    If SortName.Trim().ToUpper = "SELECTBY" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                    '@ End of Added Code For Default descending sorting order on first time  of following Fields


                End If
            End If
            MarketShareSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub grdvMarketShare_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMarketShare.RowCreated
        Try
            Dim grvRow As GridViewRow
            grvRow = e.Row
            If e.Row.RowType = DataControlRowType.Header Then
                If grdvMarketShare.AllowSorting = True Then
                    CType(grvRow.Cells(0).Controls(0), LinkButton).Text = rdSummOpt.SelectedItem.Text
                Else
                    e.Row.Cells(0).Text = rdSummOpt.SelectedItem.Text
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdvMarketShare_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvMarketShare.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(0).Text = "Total"
                e.Row.Cells(1).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("A").ToString
                e.Row.Cells(2).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("B").ToString
                e.Row.Cells(3).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("G").ToString
                e.Row.Cells(4).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("P").ToString
                e.Row.Cells(5).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("W").ToString
                e.Row.Cells(6).Text = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString
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
  
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdvMarketShare.AllowSorting = False
        grdvMarketShare.HeaderStyle.ForeColor = Drawing.Color.Black

        ' AirLinePassiveSearch2()
        ExportData()
        If grdvMarketShare.Rows.Count > 0 Then
         
        End If
    End Sub
    Private Sub ExportData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMarketShare As New AAMS.bizProductivity.bzMarketShare
        Try
            ' objInputXml.LoadXml("<PR_SEARCH_AIRLINEPASSIVE_INPUT><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY><Aoffice></Aoffice><Region></Region><SELECTBY></SELECTBY><SMonth></SMonth><SYear></SYear><EMonth></EMonth><EYear></EYear><Limited_To_Region></Limited_To_Region></PR_SEARCH_AIRLINEPASSIVE_INPUT>")
            objInputXml.LoadXml("<PR_SEARCH_MARKETSHARE_INPUT><GroupTypeID /> <COUNTRY></COUNTRY> <CITY></CITY> <AOFFICE></AOFFICE><REGION></REGION> <MONTHFROM></MONTHFROM> <YEARFROM></YEARFROM> <MONTHTO></MONTHTO> <YEARTO></YEARTO> <SELECTBY/> <LIMITED_TO_REGION></LIMITED_TO_REGION>  <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY> <LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_MARKETSHARE_INPUT>")
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
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

            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""


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
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

            End If

            'Start CODE for sorting and paging     
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "SELECTBY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
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


            objOutputXml = objbzMarketShare.SearchMarketShare(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDataset = New DataSet
                FooterDataset = ds
                grdvMarketShare.DataSource = ds.Tables("DETAIL")
                grdvMarketShare.DataBind()


                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objOutputXmlExport.LoadXml(ds.GetXml().ToString)

                objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAIL")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes("SELECTBY").Value = "Total"
                    .Attributes("A").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("A").ToString
                    .Attributes("B").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("B").ToString
                    .Attributes("G").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("G").ToString
                    .Attributes("P").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("P").ToString
                    .Attributes("W").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("W").ToString
                    .Attributes("TOTAL").Value = FooterDataset.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString

                End With
                objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel

                Dim strArray() As String = {rdSummOpt.SelectedItem.Text, "1A", "1B", "1G", "1P", "1W", "Total"}

                Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6}

                objExport.ExportDetails(objOutputXmlExport, "DETAIL", intArray, strArray, ExportExcel.ExportFormat.Excel, "MarketShareReport.xls")


            Else
                grdvMarketShare.DataSource = Nothing
                grdvMarketShare.DataBind()
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

    Protected Sub BtnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGraph.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMarketShare As New AAMS.bizProductivity.bzMarketShare
        Try

            objInputXml.LoadXml("<PR_SEARCH_MARKETSHARE_INPUT><GroupTypeID /> <COUNTRY></COUNTRY> <CITY></CITY> <AOFFICE></AOFFICE><REGION></REGION> <MONTHFROM></MONTHFROM> <YEARFROM></YEARFROM> <MONTHTO></MONTHTO> <YEARTO></YEARTO> <SELECTBY/> <LIMITED_TO_REGION></LIMITED_TO_REGION>  <LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY> <LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_MARKETSHARE_INPUT>")
            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
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
            objInputXml.DocumentElement.SelectSingleNode("SELECTBY").InnerText = rdSummOpt.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("MONTHFROM").InnerText = drpMonthFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARFROM").InnerText = drpYearFrom.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_AOFFICE").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_REGION").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""

            If drpLstGroupType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

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
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                End If

            End If

            'Start CODE for sorting and paging     
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "SELECTBY"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"
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


            objOutputXml = objbzMarketShare.SearchMarketShare(objInputXml)
            '  objOutputXml.LoadXml("<PR_SEARCH_MARKETSHARE_OUTPUT>   <DETAIL SELECTBY='' A='' B='' G='' P='' W='' TOTAL=''/>    <PAGE_TOTAL A='14852168' B='5123909' G='10913854' P='22346' W='20826' TOTAL='30933103'/>    <PAGE PAGE_COUNT='' TOTAL_ROWS='' />    <Errors Status='FALSE'>       <Error Code='' Description=''/>   </Errors></PR_SEARCH_MARKETSHARE_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then


                Session("MarketShareGraph") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=MarketShareGraph", False)

            Else
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
