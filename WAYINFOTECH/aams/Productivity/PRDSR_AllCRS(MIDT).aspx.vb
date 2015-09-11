Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.io
Imports System.Text

Partial Class Productivity_PRDSR_AllCRS_MIDT_
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim op, op1 As String
    Dim FooterDataSet As DataSet
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            lblError.Text = String.Empty
            txtAgencyName.Attributes.Add("onkeydown", "return AgencyValidation();")

            ''Added by Tapan Nath for LCODE, & Chain Code 18/03/2011
            txtLcode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtChainCode.Attributes.Add("onfocusout", "return EnableDisableGroupProductivity();")
            txtAgencyName.Attributes.Add("onfocusout", "return ActDecLcodeChainCode();")
            ''Added by Tapan Nath for LCODE, & Chain Code

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        btnGraph.Enabled = False
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
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            grdMIDT.Columns(2).HeaderText = "Agency Category"
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onclick", "return CheckValidation();")
                btnExport.Attributes.Add("onclick", "return CheckValidation();")
                btnGraph.Attributes.Add("onclick", "return CheckValidation();")
                drpProductivity.Attributes.Add("onchange", "return validateNumeric();")
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
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", True, 3)
            ' drpAgencyStatus.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
            'drpAgencyType.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpCity1, "CITY", True, 3)
            'drpCity1.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpCountry1, "COUNTRY", True, 3)
            'drpCountry1.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpAOffice, "AOFFICE", True, 3)
            'drpAOffice.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)
            'drpRegion.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpResponsibleStaff, "ResponsbileStaff", True, 3)
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
            objeAAMS.BindDropDown(drpGroupAgencyType, "AGENCYGROUPCLASSTYPE", False, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            'drpResponsibleStaff.Items.Insert(0, New ListItem("All", ""))
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
            MIDTSearch()
            
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Private Sub MIDTSearch()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCHMIDT_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><GROUPID/><AgencyName></AgencyName><WholeGroup></WholeGroup><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><Region></Region>	<MonthFrom></MonthFrom><YearFrom></YearFrom><MonthTo></MonthTo><YearTo></YearTo>	<Symbol/><FValue/><SValue/><CRSCode/><RESPONSIBLESTAFFID />	<TYPEID/><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHMIDT_INPUT>")
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If
                If drpCity1.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
                End If
                If drpCountry1.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text.Trim()
                End If
                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    .SelectSingleNode("LCODE").InnerText = ""
                    .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                Else
                    .SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                    .SelectSingleNode("AgencyName").InnerText = ""
                End If
                '.SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                '.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chbWholeGroup.Checked = True Then
                    .SelectSingleNode("WholeGroup").InnerText = "Y"
                Else
                    .SelectSingleNode("WholeGroup").InnerText = "N"
                End If
                .SelectSingleNode("GROUPID").InnerXml = ""

                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("RESP_1A").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If

                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If

                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If


                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If


                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Symbol").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If
                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CRSCode").InnerText = drpAirlineCode.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("FValue").InnerText = txtNumeric1.Text
                .SelectSingleNode("SValue").InnerText = txtNumeric2.Text
                ' drpMonthFrom.SelectedIndex(+1)

                .SelectSingleNode("MonthFrom").InnerText = drpMonthFrom.SelectedValue
                .SelectSingleNode("YearFrom").InnerText = drpYearFrom.SelectedValue
                .SelectSingleNode("MonthTo").InnerText = drpMonthTo.SelectedValue
                .SelectSingleNode("YearTo").InnerText = drpYearTo.SelectedValue


                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
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
                    Dim objEmpXml As New XmlDocument
                    Dim UserId As String
                    objEmpXml.LoadXml(Session("Security"))
                    UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = UserId
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("LIMITED_TO_OWNAAGENCY").InnerText = 0
                    End If

                End If

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
                    ViewState("SortName") = "LCODE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
                Else
                    If chkShowGroupClassification.Checked = False Then
                        If ViewState("SortName") = "Group_Classification_Name" Then
                            ViewState("SortName") = "LCODE"
                        End If
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                
                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If

                

                '  End Code for paging and sorting

            End With

            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011

            objOutputXml = objbzMIDT.Search(objInputXml)
           
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hd1A.Value = .Attributes("A1").Value.Trim()
                    hd1B.Value = .Attributes("B1").Value.Trim()
                    hd1W.Value = .Attributes("W1").Value.Trim()
                    hd1G.Value = .Attributes("G1").Value.Trim()
                    hd1P.Value = .Attributes("P1").Value.Trim()
                    hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                'FooterDataSet = New DataSet
                'FooterDataSet = ds
                grdMIDT.DataSource = ds.Tables("MIDT")
                grdMIDT.DataBind()
                lblError.Text = ""
                '************************************
                ' Code For Hiding Group Classification.
                If chkShowGroupClassification.Checked = False Then
                    grdMIDT.Columns(2).Visible = False
                End If

                'pnlCount.Visible = True
                'txtRecordCount.Text = ds.Tables("MIDT").Rows.Count.ToString
                '***************************************************************************************
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
                txtRecordOnCurrentPage.Text = ds.Tables("MIDT").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
               
                

                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName")
                    Case "LCODE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "CHAINCODE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "Group_Classification_Name"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "NAME"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(3).Controls.Add(imgDown)

                        End Select
                    Case "ADDRESS"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "SALESEXECUTIVE"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "OFFICEID"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select

                    Case "COMP_VERTICAL"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(7).Controls.Add(imgDown)

                        End Select
                    Case "CITY"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "COUNTRY"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select

                    Case "A"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select

                    Case "B"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select
                    Case "P"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(13).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(13).Controls.Add(imgDown)
                        End Select
                    Case "G"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(12).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(12).Controls.Add(imgDown)
                        End Select
                    Case "W"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(14).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(14).Controls.Add(imgDown)
                        End Select

                    Case "TOTAL"
                        Select Case ViewState("Desc")
                            Case "FALSE"
                                grdMIDT.HeaderRow.Cells(15).Controls.Add(imgUp)
                            Case "TRUE"
                                grdMIDT.HeaderRow.Cells(15).Controls.Add(imgDown)
                        End Select
                End Select
                '' @ Added Code To Show Image'
                ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@





                '*************************************
            Else
                grdMIDT.DataSource = String.Empty
                grdMIDT.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
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
                'drpAirlineCode.SelectedIndex = 0
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
            MIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            MIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'chbWholeGroup.Checked = False
        'lblError.Text = ""
        'txtAgencyName.Text = ""
        'drpCity1.SelectedIndex = 0
        'drpCountry1.SelectedIndex = 0
        'drpResponsibleStaff.SelectedIndex = 0
        'drpAgencyStatus.SelectedIndex = 0
        'drpProductivity.SelectedIndex = 0
        'drpAOffice.SelectedIndex = 0
        'drpAgencyType.SelectedIndex = 0
        'drpRegion.SelectedIndex = 0
        'txtNumeric1.Text = 0
        'txtNumeric2.Text = 0
        'drpAirlineCode.SelectedIndex = 0
        'drpMonthTo.SelectedValue = "12"
        'drpMonthFrom.SelectedValue = "1"
        'drpYearFrom.SelectedValue = DateTime.Now.Year
        'drpYearTo.SelectedValue = DateTime.Now.Year
        'pnlCount.Visible = False
        'txtRecordCount.Text = "0"
        Response.Redirect("PRDSR_AllCRS(MIDT).aspx", False)
    End Sub


    Protected Sub grdMIDT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMIDT.RowDataBound

        'If e.Row.RowType = DataControlRowType.Footer Then
        '    If FooterDataSet IsNot Nothing Then
        '        e.Row.Cells(8).Text = FooterDataSet.Tables("TOTAL").Rows(0)("A").ToString()
        '        e.Row.Cells(9).Text = FooterDataSet.Tables("TOTAL").Rows(0)("B").ToString()
        '        e.Row.Cells(10).Text = FooterDataSet.Tables("TOTAL").Rows(0)("G").ToString()
        '        e.Row.Cells(11).Text = FooterDataSet.Tables("TOTAL").Rows(0)("P").ToString()
        '        e.Row.Cells(12).Text = FooterDataSet.Tables("TOTAL").Rows(0)("W").ToString()
        '        e.Row.Cells(13).Text = FooterDataSet.Tables("TOTAL").Rows(0)("TOTAL").ToString()
        '    End If
        'End If

     


        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(10).Text = hd1A.Value.Trim()
            e.Row.Cells(11).Text = hd1B.Value.Trim()
            e.Row.Cells(12).Text = hd1G.Value.Trim()
            e.Row.Cells(13).Text = hd1P.Value.Trim()
            e.Row.Cells(14).Text = hd1W.Value.Trim()
            e.Row.Cells(15).Text = hdTotal.Value.Trim()
        End If
        Dim objSecurityXml As New XmlDocument
        Dim strBuilder As New StringBuilder
        Dim lnkDetails As System.Web.UI.HtmlControls.HtmlAnchor
        Dim link1AActual As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lblLocationCode As Label
        Dim hdLcode As New HiddenField
        Dim LimAoff, LimReg, LimOwnOff, ResStaffId As String
        Dim strMncNonMnc As String = ""

        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            LimAoff = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            LimAoff = ""
                        End If
                    Else
                        LimAoff = ""
                    End If
                Else
                    LimAoff = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            LimReg = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            LimReg = ""
                        End If
                    Else
                        LimReg = ""
                    End If
                Else
                    LimReg = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        LimOwnOff = "1"
                    Else
                        LimOwnOff = "0"
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    LimOwnOff = "0"
                End If
            End If
            If drpLstGroupType.SelectedIndex <> 0 Then
                strMncNonMnc = drpLstGroupType.SelectedValue
            End If


            lnkDetails = e.Row.FindControl("lnkDetails")
            link1AActual = e.Row.FindControl("link1AActual")
            Dim hdCountry, hdAdd As HiddenField
            hdCountry = CType(e.Row.FindControl("hdCountry"), HiddenField)
            hdAdd = CType(e.Row.FindControl("hdAdd"), HiddenField)
            hdAdd.Value = hdAdd.Value.Replace(vbCrLf, "\n")
            hdAdd.Value = Server.UrlEncode(hdAdd.Value)
            hdAdd.Value = hdAdd.Value.Replace("'", "")
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='All CRS Productivity']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        lnkDetails.Disabled = True
                    Else
                        'lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + objED.Encrypt(drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                        lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "','" + strMncNonMnc.ToString + "');")
                    End If
                Else
                    lnkDetails.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lblLocationCode = CType(e.Row.FindControl("lblLocationCode"), Label)
                'lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + objED.Encrypt(drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                lnkDetails.Attributes.Add("OnClick", "javascript:return DetailFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "','" + strMncNonMnc.ToString + "');")
            End If
            objSecurityXml = New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='1A Productivity Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        link1AActual.Disabled = True
                    Else
                        strBuilder = objeAAMS.SecurityCheck(31)
                        'link1AActual.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + objED.Encrypt(drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                        link1AActual.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                    End If
                Else
                    link1AActual.Disabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                '  link1AActual.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + objED.Encrypt(drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
                link1AActual.Attributes.Add("OnClick", "javascript:return SelectCrsExcAvgFunction('" + drpMonthFrom.SelectedValue + "','" + drpMonthTo.SelectedValue + "','" + drpYearFrom.SelectedValue + "','" + drpYearTo.SelectedValue + "','" + objED.Encrypt(e.Row.Cells(0).Text.ToString.Trim) + "','" + (drpAOffice.SelectedValue.Trim) + "','" + LimAoff + "','" + LimReg + "','" + LimOwnOff + "','" + Server.UrlEncode(e.Row.Cells(2).Text.ToString.Replace("'", "")) + "','" + hdAdd.Value + "','" + e.Row.Cells(6).Text.ToString + "','" + hdCountry.Value + "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'MIDTSearch()
        grdMIDT.AllowSorting = False
        grdMIDT.HeaderStyle.ForeColor = Drawing.Color.Black
        MIDTExport()

        ' grdMIDT.Columns(grdMIDT.Columns.Count - 1).Visible = False
        If grdMIDT.Rows.Count > 0 Then
            ' PrepareGridViewForExport(grdMIDT)
            '   ExportGridView(grdMIDT, "MIDT.xls")
        End If
    End Sub
    Private Sub ExportGridView(ByVal gv2, ByVal FileName)

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
    Private Sub PrepareGridViewForExport(ByVal gv As Control)
        'LinkButton lb = new LinkButton();
        'Dim l As New Literal
        'Dim name As String = ""
        'Dim lb As New LinkButton

        'Dim i As Int32
        'For i = 0 To gv.Controls.Count - 1
        '    If (gv.Controls(i).GetType Is GetType(LinkButton)) Then
        '        l.Text = CType(gv.Controls(i), LinkButton).Text
        '        gv.Controls.Remove(gv.Controls(i))
        '        gv.Controls.AddAt(i, l)

        '    End If

        '    If (gv.Controls(i).HasControls()) Then
        '        PrepareGridViewForExport(gv.Controls(i))
        '    End If

        'Next
        'Dim objXmlNode, objXmlNodeClone As XmlNode
        'objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("MIDT")
        'objXmlNodeClone = objXmlNode.CloneNode(True)
        'For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
        '    XmlAttr.Value = ""
        'Next

        'With objXmlNodeClone
        '    .Attributes("").Value = ""
        '    .Attributes("CRSCODETEXT").Value = "Total"
        '    .Attributes("PASSIVESEGMENTS").Value = hd1A.Value.Trim()
        '    .Attributes("ACTIVESEGMENTS").Value = hd1B.Value.Trim()
        '    .Attributes("TOTALSEGMENTS").Value = hd1G.Value.Trim()
        '    .Attributes("PASSIVE_PER").Value = hd1P.Value.Trim()
        '    .Attributes("PASSIVE_PER").Value = hd1W.Value.Trim()
        'End With
        'objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
        'Dim objExport As New ExportExcel
        'Dim strArray() As String = {rdSummOpt.SelectedItem.Text, "CRS", "Passive                Segments", "Active Segments", "Total Segments", "Passive %"}
        'Dim intArray() As Integer = {0, 1, 2, 3, 4, 5}
        'objExport.ExportDetails(objOutputXml, "AIRLINEPASSIVE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportREport.xls")







    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            MIDTSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdMIDT_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMIDT.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                '  ViewState("Desc") = "FALSE"

                '@ Added Code For Default descending sorting on first time  of following Fields      
                ' @A, B, P, G,W, TOTAL
                If SortName.Trim().ToUpper = "A" Or SortName.Trim().ToUpper = "B" Or SortName.Trim().ToUpper = "G" Or SortName.Trim().ToUpper = "P" Or SortName.Trim().ToUpper = "W" Or SortName.Trim().ToUpper = "TOTAL" Then
                    ViewState("Desc") = "TRUE"
                Else
                    ViewState("Desc") = "FALSE"
                End If
                '@ Added Code For Default descending sorting on first time  of foolowing Fields

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
                    '@ Added Code For Default descending sorting on first time  of following Fields      
                    ' @A, B, P, G,W, TOTAL
                    If SortName.Trim().ToUpper = "A" Or SortName.Trim().ToUpper = "B" Or SortName.Trim().ToUpper = "G" Or SortName.Trim().ToUpper = "P" Or SortName.Trim().ToUpper = "W" Or SortName.Trim().ToUpper = "TOTAL" Then
                        ViewState("Desc") = "TRUE"
                    Else
                        ViewState("Desc") = "FALSE"
                    End If
                    '@ Added Code For Default descending sorting on first time  of foolowing Fields

                End If
            End If
            MIDTSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub MIDTExport()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCHMIDT_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><GROUPID/><AgencyName></AgencyName><WholeGroup></WholeGroup><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><Region></Region>	<MonthFrom></MonthFrom><YearFrom></YearFrom><MonthTo></MonthTo><YearTo></YearTo>	<Symbol/><FValue/><SValue/><CRSCode/><RESPONSIBLESTAFFID />	<LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><CHAIN_CODE></CHAIN_CODE><TYPEID></TYPEID><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHMIDT_INPUT>")
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If
                If drpCity1.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
                End If
                If drpCountry1.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text.Trim()
                End If
                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    .SelectSingleNode("LCODE").InnerText = ""
                    .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                Else
                    .SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                    .SelectSingleNode("AgencyName").InnerText = ""
                End If
                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
                End If

                If txtChainCode.Text.Trim <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                '.SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                '.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chbWholeGroup.Checked = True Then
                    .SelectSingleNode("WholeGroup").InnerText = "Y"
                Else
                    .SelectSingleNode("WholeGroup").InnerText = "N"
                End If
                .SelectSingleNode("GROUPID").InnerXml = ""

                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("RESP_1A").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If

                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If

                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If

                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Symbol").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If
                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CRSCode").InnerText = drpAirlineCode.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("FValue").InnerText = txtNumeric1.Text
                .SelectSingleNode("SValue").InnerText = txtNumeric2.Text
                ' drpMonthFrom.SelectedIndex(+1)

                .SelectSingleNode("MonthFrom").InnerText = drpMonthFrom.SelectedValue
                .SelectSingleNode("YearFrom").InnerText = drpYearFrom.SelectedValue
                .SelectSingleNode("MonthTo").InnerText = drpMonthTo.SelectedValue
                .SelectSingleNode("YearTo").InnerText = drpYearTo.SelectedValue


                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
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


                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If
                'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
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

                '  End Code for paging and sorting

            End With
            objOutputXml = objbzMIDT.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hd1A.Value = .Attributes("A1").Value.Trim()
                    hd1B.Value = .Attributes("B1").Value.Trim()
                    hd1W.Value = .Attributes("W1").Value.Trim()
                    hd1G.Value = .Attributes("G1").Value.Trim()
                    hd1P.Value = .Attributes("P1").Value.Trim()
                    hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                'FooterDataSet = New DataSet
                'FooterDataSet = ds

                '@ New Added Code  
                Dim dtable As New DataTable
                Dim dCol As DataColumn
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("MIDT").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                    dtable.Columns.Add(dCol)
                Next

                Dim dRow As DataRow
                dRow = dtable.NewRow()
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("MIDT").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dRow(strAttribut) = xmlAttrTotal.Value
                Next

                dtable.Rows.Add(dRow)
                grdMIDT.DataSource = dtable


                '@ New Added Code  


                'grdMIDT.DataSource = ds.Tables("MIDT")
                grdMIDT.DataBind()





                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("MIDT")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes("COUNTRY").Value = "Total"
                    .Attributes("A").Value = hd1A.Value.Trim()
                    .Attributes("B").Value = hd1B.Value.Trim()
                    .Attributes("G").Value = hd1G.Value.Trim()
                    .Attributes("P").Value = hd1P.Value.Trim()
                    .Attributes("W").Value = hd1W.Value.Trim()
                    .Attributes("TOTAL").Value = hdTotal.Value.Trim()
                End With
                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel

                If chkShowGroupClassification.Checked = True Then

                    Dim strArray() As String = {"Location Code", "Chain Code", "Agency Category", "Office ID", "Agency Name", "Address", "Sales Executive", "Company Vertical", "City", "Country", "1A", "1B", "1G", "1P", "1W", "Total"}
                    'Dim strArray() As String = {"Location Code", "Chain Code", "Group Classification", "Office ID", "Agency Name", "Address", "Sales Executive", "City", "Country", "1A", "1B", "1G", "1P", "1W", "Total"}
                    Dim intArray() As Integer = {0, 1, 14, 2, 3, 4, 5, 15, 6, 7, 8, 9, 10, 11, 12, 13}
                    objExport.ExportDetails(objOutputXml, "MIDT", intArray, strArray, ExportExcel.ExportFormat.Excel, "MIDTExport.xls")
                Else
                    Dim strArray() As String = {"Location Code", "Chain Code", "Office ID", "Agency Name", "Address", "Sales Executive", "Company Vertical", "City", "Country", "1A", "1B", "1G", "1P", "1W", "Total"}
                    Dim intArray() As Integer = {0, 1, 2, 3, 4, 5, 15, 6, 7, 8, 9, 10, 11, 12, 13}
                    objExport.ExportDetails(objOutputXml, "MIDT", intArray, strArray, ExportExcel.ExportFormat.Excel, "MIDTExport.xls")
                End If

                '*************************************
            Else
                grdMIDT.DataSource = String.Empty
                grdMIDT.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                'pnlPaging.Visible = False
                ' txtTotalRecordCount.Text = "0"
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
                'drpAirlineCode.SelectedIndex = 0
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
    'Code Added on 1st Feb for Graph

    Protected Sub btnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraph.Click

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Try
            objInputXml.LoadXml("<PR_SEARCHMIDT_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><LCODE></LCODE><GROUPID/><AgencyName></AgencyName><WholeGroup></WholeGroup><RESP_1A></RESP_1A><AGENCYTYPEID></AGENCYTYPEID><AGENCYSTATUSID></AGENCYSTATUSID><Aoffice></Aoffice><Region></Region>	<MonthFrom></MonthFrom><YearFrom></YearFrom><MonthTo></MonthTo><YearTo></YearTo>	<Symbol/><FValue/><SValue/><CRSCode/><RESPONSIBLESTAFFID /><TYPEID/><LIMITED_TO_OWNAAGENCY></LIMITED_TO_OWNAAGENCY><LIMITED_TO_AOFFICE></LIMITED_TO_AOFFICE><LIMITED_TO_REGION></LIMITED_TO_REGION><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><CHAIN_CODE></CHAIN_CODE><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHMIDT_INPUT>")
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If
                If drpCity1.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity1.SelectedItem.Text.Trim()
                End If
                If drpCountry1.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry1.SelectedItem.Text.Trim()
                End If
                If (Request.Form("txtAgencyName") <> "" And hdAgencyName.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyName.Value = "") Then
                    .SelectSingleNode("LCODE").InnerText = ""
                    .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                Else
                    .SelectSingleNode("LCODE").InnerText = hdAgencyName.Value.Trim()
                    .SelectSingleNode("AgencyName").InnerText = ""
                End If

                'Added by Tapan Nath 14/03/2011
                If txtLcode.Text.Trim <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerXml = txtLcode.Text.Trim
                End If

                If txtChainCode.Text.Trim <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
                End If
                'Added by Tapan Nath 14/03/2011

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If


                '.SelectSingleNode("LCODE").InnerXml = hdAgencyNameId.Value.Trim()
                '.SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chbWholeGroup.Checked = True Then
                    .SelectSingleNode("WholeGroup").InnerText = "Y"
                Else
                    .SelectSingleNode("WholeGroup").InnerText = "N"
                End If

                .SelectSingleNode("GROUPID").InnerText = ""

                If drpResponsibleStaff.SelectedIndex <> 0 Then
                    .SelectSingleNode("RESP_1A").InnerText = drpResponsibleStaff.SelectedValue.Trim()
                End If

                If drpAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
                End If

                If drpAgencyStatus.SelectedIndex <> 0 Then
                    .SelectSingleNode("AGENCYSTATUSID").InnerText = drpAgencyStatus.SelectedValue.Trim()
                End If

                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If

                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("Symbol").InnerText = Trim(drpProductivity.SelectedItem.Text)
                End If
                If drpProductivity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CRSCode").InnerText = drpAirlineCode.SelectedItem.Text.Trim()
                End If
                .SelectSingleNode("FValue").InnerText = txtNumeric1.Text
                .SelectSingleNode("SValue").InnerText = txtNumeric2.Text
                ' drpMonthFrom.SelectedIndex(+1)

                .SelectSingleNode("MonthFrom").InnerText = drpMonthFrom.SelectedValue
                .SelectSingleNode("YearFrom").InnerText = drpYearFrom.SelectedValue
                .SelectSingleNode("MonthTo").InnerText = drpMonthTo.SelectedValue
                .SelectSingleNode("YearTo").InnerText = drpYearTo.SelectedValue

                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If

                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("RESPONSIBLESTAFFID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
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


                'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
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



                '  End Code for paging and sorting

            End With

            objOutputXml = objbzMIDT.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = ""
                Session("ALLCRSMIDTGRAPH") = objOutputXml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=ALLCRSMIDTGRAPH", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    

End Class
