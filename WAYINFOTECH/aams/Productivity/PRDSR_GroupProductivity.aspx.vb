Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.io
Imports System.Text
Partial Class Productivity_PRDSR_GroupProductivity
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim objDictionary As New HybridDictionary

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Private Sub AllNonDescColumnDefault()
        Try
            objDictionary.Add("Chain_Code", "Chain_Code")
            objDictionary.Add("Chain_Name", "Chain_Name")
            objDictionary.Add("Group_Classification_Name", "Group_Classification_Name")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            'lblError.Text = String.Empty
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Productivity']").Attributes("Value").Value)
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

            grdProductivity.Columns(2).HeaderText = "Agency Group Category"
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            txtAgencyGroup.Attributes.Add("onkeydown", "return AgencyValidation();")
            txtAgencyGroup.Attributes.Add("onfocusout", "return EnableDisableChainCode();")
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onclick", "return CheckValidation();")
                btnExport.Attributes.Add("onclick", "return CheckValidation();")
                drpProductivity.Attributes.Add("onchange", "return validateNumeric();")
                drpProductivity.Attributes.Add("onclick", "return validateNumeric();")
                BindAll()
            End If
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                        drpAOffice.SelectedValue = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        drpAOffice.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindAll()
        Try
            Dim i, j As Integer
            'objeAAMS.BindDropDown(drpCity, "CITY", True)
            'objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
            'objeAAMS.BindDropDown(drpAOffice, "AOFFICE", True)
            'objeAAMS.BindDropDown(drpRegion, "REGION", True)
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            'drpCity.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            'drpCountry.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpAOffice, "AOFFICE", True, 3)
            'drpAOffice.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)
            'drpRegion.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year
            'drpMonthTo.SelectedValue = drpMonthTo.Items(11).Text
            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GroupProductivitySearch()
           
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub GroupProductivitySearch()
        Dim objInputXml, objInputXml1, objOutputXml, objOutputXml2 As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        'Dim i As String
        Try

            objInputXml.LoadXml("<PR_SEARCH_GROUP_PRODUCTIVITY_INPUT><EmployeeID></EmployeeID><Region></Region><CITY></CITY><COUNTRY></COUNTRY><Chain_Code></Chain_Code><Chain_Name></Chain_Name><GROUPTYPEID></GROUPTYPEID><Aoffice></Aoffice><TP_SYMBOL></TP_SYMBOL><TP_FROM></TP_FROM><TP_TO></TP_TO><CRSTYPE></CRSTYPE><FROMMONTH></FROMMONTH><FROMYEAR></FROMYEAR><TOMONTH></TOMONTH><TOYEAR></TOYEAR><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><CHAIN_CODE/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_GROUP_PRODUCTIVITY_INPUT>")
            With objInputXml.DocumentElement

                'Code with EmployeeID is added by Mukund
                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If



                If Not Session("LoginSession") Is Nothing Then
                    .SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                'Code with EmployeeID is added by Mukund

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If
                '.SelectSingleNode("Chain_Code").InnerXml = hdChainNameId.Value.Trim()
                .SelectSingleNode("Chain_Name").InnerText = txtAgencyGroup.Text

                .SelectSingleNode("GROUPTYPEID").InnerText = rbl_CarrierType.SelectedValue
                If (Request.Form("txtAgencyGroup") <> "" And hdChainId.Value = "") Or (Request.Form("txtAgencyGroup") = "" Or hdChainId.Value = "") Then
                    .SelectSingleNode("Chain_Code").InnerXml = ""
                Else
                    .SelectSingleNode("Chain_Code").InnerText = hdChainId.Value.Trim()
                End If
                If txtChaincode.Text.Trim <> "" Then
                    .SelectSingleNode("Chain_Code").InnerText = txtChaincode.Text.Trim
                End If
                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If
                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
                End If

                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("TP_SYMBOL").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If
                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CRSTYPE").InnerText = drpAirlineCode.SelectedItem.Text
                End If
                .SelectSingleNode("TP_FROM").InnerText = txtNumeric1.Text
                .SelectSingleNode("TP_TO").InnerText = txtNumeric2.Text
                .SelectSingleNode("FROMMONTH").InnerText = drpMonthFrom.SelectedValue
                .SelectSingleNode("FROMYEAR").InnerText = drpYearFrom.SelectedValue
                .SelectSingleNode("TOMONTH").InnerText = drpMonthTo.SelectedValue
                .SelectSingleNode("TOYEAR").InnerText = drpYearTo.SelectedValue
                .SelectSingleNode("CHAIN_CODE").InnerText = txtChaincode.Text.Trim

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

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                If chkShowGroupClassification.Checked = False Then
                    If ViewState("SortName") = "Group_Classification_Name" Then
                        ViewState("Desc") = "FALSE"
                    End If
                End If
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If


            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Chain_Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Chain_Name" '"LOCATION_CODE"
            Else
                If chkShowGroupClassification.Checked = False Then
                    If ViewState("SortName") = "Group_Classification_Name" Then
                        ViewState("SortName") = "Chain_Name"
                    End If
                End If
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

           

            '  End Code for paging and sorting


            objOutputXml = objbzMIDT.GroupProductivity(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hd1A.Value = .Attributes("AMADEUS").Value.Trim()
                    hd1B.Value = .Attributes("ABACUS").Value.Trim()
                    hd1G.Value = .Attributes("GALILEO").Value.Trim()
                    hd1W.Value = .Attributes("SABREDOMESTIC").Value.Trim()
                    hd1P.Value = .Attributes("WORLDSPAN").Value.Trim()
                    hdTotal.Value = .Attributes("Total").Value.Trim()
                    hdNoOfPc.Value = .Attributes("No_of_PC").Value.Trim()
                    hdNoOfPrinter.Value = .Attributes("No_of_Printer").Value.Trim()
                    hdNoOfTicket.Value = .Attributes("No_of_Ticket").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdProductivity.DataSource = ds.Tables("GROUP_PRODUCTIVITY")
                grdProductivity.DataBind()
                '************************************
                ' Code For Hiding Group Classification
                If chkShowGroupClassification.Checked = False Then
                    grdProductivity.Columns(2).Visible = False
                End If

                'pnlCount.Visible = True
                'txtRecordCount.Text = ds.Tables("GROUP_PRODUCTIVITY").Rows.Count.ToString
                '************************************
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
                txtRecordOnCurrentPage.Text = ds.Tables("GROUP_PRODUCTIVITY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"


                Select Case ViewState("SortName").ToString
                    Case "Chain_Code"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "Chain_Name"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "Group_Classification_Name"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "AMADEUS"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(3).Controls.Add(imgDown)

                        End Select
                    Case "ABACUS"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "GALILEO"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "WORLDSPAN"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select
                    Case "SABREDOMESTIC"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                    Case "TOTAL"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select

                    Case "No_of_PC"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select

                    Case "No_of_Printer"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select
                    Case "No_of_Ticket"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdProductivity.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                grdProductivity.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select
                End Select
               
                '*************************************


                lblError.Text = ""
                hdChainId.Value = ""
            Else
                grdProductivity.DataSource = String.Empty
                grdProductivity.DataBind()
                'pnlCount.Visible = False
                'txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If drpProductivity.SelectedValue = "" Then
                txtNumeric1.CssClass = "textboxgrey"
                txtNumeric2.CssClass = "textboxgrey"
                txtNumeric1.Enabled = False
                txtNumeric2.Enabled = False
                drpAirlineCode.Enabled = False
            ElseIf drpProductivity.SelectedValue = "7" Then
                txtNumeric1.CssClass = "textbox"
                txtNumeric2.CssClass = "textbox"
                txtNumeric1.Enabled = True
                txtNumeric2.Enabled = True
                drpAirlineCode.Enabled = True
            Else
                txtNumeric1.CssClass = "textbox"
                txtNumeric2.CssClass = "textboxgrey"
                txtNumeric1.Enabled = True
                txtNumeric2.Enabled = False
                txtNumeric2.Text = ""
                drpAirlineCode.Enabled = True
            End If
        End Try
    End Sub
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GroupProductivitySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GroupProductivitySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GroupProductivitySearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ' GroupProductivitySearch()
        grdProductivity.AllowSorting = False
        grdProductivity.HeaderStyle.ForeColor = Drawing.Color.Black
        GroupProductivityExport()
        If grdProductivity.Rows.Count > 0 Then
            'PrepareGridViewForExport(grdProductivity)
            'ExportGridView(grdProductivity, "GProductivity.xls")
        End If
    End Sub
    Private Sub GroupProductivityExport()
        Dim objInputXml, objInputXml1, objOutputXml, objOutputXml2 As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        'Dim i As String
        Try
            objInputXml.LoadXml("<PR_SEARCH_GROUP_PRODUCTIVITY_INPUT><EmployeeID></EmployeeID><Region></Region><CITY></CITY><COUNTRY></COUNTRY><Chain_Code></Chain_Code><Chain_Name></Chain_Name><GROUPTYPEID></GROUPTYPEID><Aoffice></Aoffice><TP_SYMBOL></TP_SYMBOL><TP_FROM></TP_FROM><TP_TO></TP_TO><CRSTYPE></CRSTYPE><FROMMONTH></FROMMONTH><FROMYEAR></FROMYEAR><TOMONTH></TOMONTH><TOYEAR></TOYEAR><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_GROUP_PRODUCTIVITY_INPUT>")
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If


                'Code with EmployeeID is added by Mukund
                If Not Session("LoginSession") Is Nothing Then
                    .SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                'Code with EmployeeID is added by Mukund

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If
                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If
                '.SelectSingleNode("Chain_Code").InnerXml = hdChainNameId.Value.Trim()
                .SelectSingleNode("Chain_Name").InnerText = txtAgencyGroup.Text

                .SelectSingleNode("GROUPTYPEID").InnerText = rbl_CarrierType.SelectedValue
                If (Request.Form("txtAgencyGroup") <> "" And hdChainId.Value = "") Or (Request.Form("txtAgencyGroup") = "" Or hdChainId.Value = "") Then
                    .SelectSingleNode("Chain_Code").InnerXml = ""
                Else
                    .SelectSingleNode("Chain_Code").InnerText = hdChainId.Value.Trim()
                End If

                If txtChaincode.Text.Trim <> "" Then
                    .SelectSingleNode("Chain_Code").InnerText = txtChaincode.Text.Trim
                End If
                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If
                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedItem.Text.Trim()
                End If

                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("TP_SYMBOL").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If
                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CRSTYPE").InnerText = drpAirlineCode.SelectedItem.Text
                End If
                .SelectSingleNode("TP_FROM").InnerText = txtNumeric1.Text
                .SelectSingleNode("TP_TO").InnerText = txtNumeric2.Text
                .SelectSingleNode("FROMMONTH").InnerText = drpMonthFrom.SelectedValue
                .SelectSingleNode("FROMYEAR").InnerText = drpYearFrom.SelectedValue
                .SelectSingleNode("TOMONTH").InnerText = drpMonthTo.SelectedValue
                .SelectSingleNode("TOYEAR").InnerText = drpYearTo.SelectedValue

            End With
       
            ' objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Chain_Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Chain_Name" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            '  End Code for paging and sorting


            objOutputXml = objbzMIDT.GroupProductivity(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hd1A.Value = .Attributes("AMADEUS").Value.Trim()
                    hd1B.Value = .Attributes("ABACUS").Value.Trim()
                    hd1G.Value = .Attributes("GALILEO").Value.Trim()
                    hd1W.Value = .Attributes("SABREDOMESTIC").Value.Trim()
                    hd1P.Value = .Attributes("WORLDSPAN").Value.Trim()
                    hdTotal.Value = .Attributes("Total").Value.Trim()
                    hdNoOfPc.Value = .Attributes("No_of_PC").Value.Trim()
                    hdNoOfPrinter.Value = .Attributes("No_of_Printer").Value.Trim()
                    hdNoOfTicket.Value = .Attributes("No_of_Ticket").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdProductivity.DataSource = ds.Tables("GROUP_PRODUCTIVITY")
                grdProductivity.DataBind()

                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("GROUP_PRODUCTIVITY")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    If chkShowGroupClassification.Checked = True Then
                        .Attributes("Group_Classification_Name").Value = "Total"
                    Else
                        .Attributes("Chain_Name").Value = "Total"
                    End If

                    .Attributes("AMADEUS").Value = hd1A.Value.Trim()
                    .Attributes("ABACUS").Value = hd1B.Value.Trim()
                    .Attributes("GALILEO").Value = hd1G.Value.Trim()
                    .Attributes("SABREDOMESTIC").Value = hd1P.Value.Trim()
                    .Attributes("WORLDSPAN").Value = hd1W.Value.Trim()
                    .Attributes("Total").Value = hdTotal.Value.Trim()
                    .Attributes("No_of_PC").Value = hdNoOfPc.Value.Trim()
                    .Attributes("No_of_Printer").Value = hdNoOfPrinter.Value.Trim()
                    .Attributes("No_of_Ticket").Value = hdNoOfTicket.Value.Trim()
                End With

                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel
                
                If chkShowGroupClassification.Checked = True Then
                    Dim strArray() As String = {"Chain Code", "Chain Name", "Agency Group Category", "1A", "1B", "1G", "1P", "1W", "Total", "No Of PCS", "No Of Printers", "No Of Tickets"}
                    Dim intArray() As Integer = {0, 1, 11, 2, 3, 4, 5, 6, 7, 8, 9, 10}
                    objExport.ExportDetails(objOutputXml, "GROUP_PRODUCTIVITY", intArray, strArray, ExportExcel.ExportFormat.Excel, "GroupProductivityExport.xls")
                Else
                    Dim strArray() As String = {"Chain Code", "Chain Name", "1A", "1B", "1G", "1P", "1W", "Total", "No Of PCS", "No Of Printers", "No Of Tickets"}
                    Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
                    objExport.ExportDetails(objOutputXml, "GROUP_PRODUCTIVITY", intArray, strArray, ExportExcel.ExportFormat.Excel, "GroupProductivityExport.xls")
                End If

            Else
                grdProductivity.DataSource = String.Empty
                grdProductivity.DataBind()
                'pnlCount.Visible = False
                'txtRecordCount.Text = "0"
                ' pnlPaging.Visible = False
                'txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            If drpProductivity.SelectedValue = "" Then
                txtNumeric1.CssClass = "textboxgrey"
                txtNumeric2.CssClass = "textboxgrey"
                txtNumeric1.Enabled = False
                txtNumeric2.Enabled = False
                drpAirlineCode.Enabled = False
            ElseIf drpProductivity.SelectedValue = "7" Then
                txtNumeric1.CssClass = "textbox"
                txtNumeric2.CssClass = "textbox"
                txtNumeric1.Enabled = True
                txtNumeric2.Enabled = True
                drpAirlineCode.Enabled = True
            Else
                txtNumeric1.CssClass = "textbox"
                txtNumeric2.CssClass = "textboxgrey"
                txtNumeric1.Enabled = True
                txtNumeric2.Enabled = False
                txtNumeric2.Text = ""
                drpAirlineCode.Enabled = True
            End If
        End Try
    End Sub
    'Private Sub ExportGridView(ByVal gv2, ByVal FileName)

    '    Dim attachment As String = "attachment; filename=" + FileName
    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", attachment)
    '    Response.ContentType = "application/ms-excel"
    '    Dim sw As New StringWriter
    '    Dim htw As New HtmlTextWriter(sw)
    '    Dim frm As New HtmlForm()
    '    gv2.Parent.Controls.Add(frm)
    '    frm.Attributes("runat") = "server"
    '    frm.Controls.Add(gv2)
    '    frm.RenderControl(htw)
    '    Response.Write(sw.ToString())
    '    Response.End()
    'End Sub
    'Private Sub PrepareGridViewForExport(ByVal gv As Control)
    '    'LinkButton lb = new LinkButton();
    '    Dim l As New Literal
    '    Dim name As String = ""
    '    Dim lb As New LinkButton

    '    Dim i As Int32
    '    For i = 0 To gv.Controls.Count - 1
    '        If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
    '            l.Text = CType(gv.Controls(i), LinkButton).Text
    '            gv.Controls.Remove(gv.Controls(i))
    '            gv.Controls.AddAt(i, l)

    '        End If

    '        If (gv.Controls(i).HasControls()) Then
    '            PrepareGridViewForExport(gv.Controls(i))
    '        End If

    '    Next
    'End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'lblError.Text = ""
        'drpCity.SelectedIndex = 0
        'drpCountry.SelectedIndex = 0
        'hdChainNameId.Value = ""
        'txtChainName.Text = ""
        'drpProductivity.SelectedIndex = 0
        'drpAOffice.SelectedIndex = 0
        'grdProductivity.DataSource = Nothing
        'grdProductivity.DataBind()
        'drpRegion.SelectedIndex = 0
        'txtNumeric1.Text = 0
        'txtNumeric2.Text = 0
        'drpAirlineCode.SelectedIndex = 0
        'drpMonthTo.SelectedValue = "12"
        'drpMonthFrom.SelectedValue = "1"
        'drpYearFrom.SelectedValue = DateTime.Now.Year
        'drpYearTo.SelectedValue = DateTime.Now.Year
        'txtRecordCount.Text = 0
        'pnlCount.Visible = False
        Response.Redirect("PRDSR_GroupProductivity.aspx", False)
    End Sub

    Protected Sub grdProductivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProductivity.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = hd1A.Value.Trim()
            e.Row.Cells(4).Text = hd1B.Value.Trim()
            e.Row.Cells(5).Text = hd1G.Value.Trim()
            e.Row.Cells(6).Text = hd1P.Value.Trim()
            e.Row.Cells(7).Text = hd1W.Value.Trim()
            e.Row.Cells(8).Text = hdTotal.Value.Trim()
            e.Row.Cells(9).Text = hdNoOfPc.Value.Trim()
            e.Row.Cells(10).Text = hdNoOfPrinter.Value.Trim()
            e.Row.Cells(11).Text = hdNoOfTicket.Value.Trim()
            If chkShowGroupClassification.Checked = True Then
                e.Row.Cells(1).Text = ""
            End If
        End If
    End Sub

    Protected Sub grdProductivity_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProductivity.Sorting
        Try
            AllNonDescColumnDefault()
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
                If Not objDictionary.Contains(SortName) Then
                    ViewState("Desc") = "TRUE"
                End If
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                    If Not objDictionary.Contains(SortName) Then
                        ViewState("Desc") = "TRUE"
                    End If
                End If
            End If
            GroupProductivitySearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
