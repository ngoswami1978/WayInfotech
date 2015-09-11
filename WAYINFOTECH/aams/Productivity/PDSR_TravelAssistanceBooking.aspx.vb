Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.io
Imports System.Text
Partial Class Productivity_PDSR_TravelAssistanceBooking
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
            objDictionary.Add("BAL", "BAL")
            objDictionary.Add("CMS", "CMS")
            objDictionary.Add("ICI", "ICI")
            objDictionary.Add("REL", "REL")
            objDictionary.Add("AIG", "AIG")
            objDictionary.Add("CHR", "CHR")
            objDictionary.Add("Productivity", "Productivity")
        Catch ex As Exception

        End Try
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
            grdTravelAssistance.Columns(3).HeaderText = "Agency Category"

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Travel Assistance Bookings']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Travel Assistance Bookings']").Attributes("Value").Value)
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
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
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
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onclick", "return CheckValidation();")
                BindAll()
            End If

            ' pnlPaging.Visible = True

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
            objeAAMS.BindDropDown(drpCity1, "CITY", True, 3)
            'drpCity1.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpCountry1, "COUNTRY", True, 3)
            'drpCountry1.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpAOffice, "AOFFICE", True, 3)
            'drpAOffice.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(drpRegion, "REGION", True, 3)
            'drpRegion.Items.Insert(0, New ListItem("All", ""))
            objeAAMS.BindDropDown(ddlCrs, "PROVIDERS", True, 3)
            objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
            objeAAMS.BindDropDown(drpGroupAgencyType, "AGENCYGROUPCLASSTYPE", False, 3)
            objeAAMS.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)

            'ddlCrs.Items.Insert(0, New ListItem("All", ""))
            For j = DateTime.Now.Year To 1990 Step -1
                drpYearFrom.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                drpYearTo.Items.Insert(i, New ListItem(j.ToString(), j.ToString()))
                i += 1
            Next
            drpYearFrom.SelectedValue = DateTime.Now.Year
            drpYearTo.SelectedValue = DateTime.Now.Year
            drpMonthTo.SelectedValue = "12"
            drpMonthFrom.SelectedValue = "1"
            'drpMonthTo.SelectedValue = drpMonthTo.Items(11).Text
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            TravelAssistanceSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub TravelAssistanceSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Dim StartDate1 As String
        Dim EndDate1 As String
        Try
            objInputXml.LoadXml("<PR_SEARCH_TRAVELASSISTANCE_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><LCode></LCode><AgencyName></AgencyName><GroupProductivity></GroupProductivity><Provider></Provider><Aoffice></Aoffice><Region></Region><ShowAverage></ShowAverage><FromDate></FromDate><ToDate></ToDate><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><RESP_1A></RESP_1A><TYPEID></TYPEID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><CHAIN_CODE></CHAIN_CODE><AGENCYTYPEID></AGENCYTYPEID><TYPEID/><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_TRAVELASSISTANCE_INPUT>")
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
                '.SelectSingleNode("LCode").InnerXml = hdAgencyNameId.Value.Trim()

                If (Request.Form("txtAgencyName") <> "" And hdAgencyNameId.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyNameId.Value = "") Then
                    .SelectSingleNode("LCode").InnerText = ""
                    .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                Else
                    .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()
                    .SelectSingleNode("AgencyName").InnerText = ""
                End If


                '  .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chbWholeGroup.Checked = True Then
                    .SelectSingleNode("GroupProductivity").InnerText = "True"
                Else
                    .SelectSingleNode("GroupProductivity").InnerText = "False"
                End If

                If chkAverage.Checked = True Then
                    .SelectSingleNode("ShowAverage").InnerText = "True"
                Else
                    .SelectSingleNode("ShowAverage").InnerText = "False"
                End If
                If ddlCrs.SelectedIndex <> 0 Then
                    .SelectSingleNode("Provider").InnerXml = ddlCrs.SelectedValue
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

                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If

                StartDate1 = "01" & "/" & drpMonthFrom.SelectedValue & "/" & drpYearFrom.SelectedValue
                EndDate1 = DateTime.DaysInMonth(drpYearTo.SelectedValue, drpMonthTo.SelectedValue) & "/" & drpMonthTo.SelectedValue & "/" & drpYearTo.SelectedValue

                .SelectSingleNode("FromDate").InnerText = objeAAMS.ConvertTextDate(StartDate1)
                .SelectSingleNode("ToDate").InnerText = objeAAMS.ConvertTextDate(EndDate1)
                'Dim objEmpXml As New XmlDocument
                'objEmpXml.LoadXml(Session("Security"))



                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                '    .SelectSingleNode("Limited_To_Aoffice").InnerText = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText
                'End If

                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.ToUpper = "TRUE" Then
                '    .SelectSingleNode("Limited_To_OwnAagency").InnerXml = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.Trim()
                'End If


                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper = "TRUE" Then
                '    '.SelectSingleNode("Limited_To_Region").InnerXml = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim()
                'End If
                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))


                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                            End If
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If

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
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "True"
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "False"
                        End If
                        'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "False"
                    End If
                    'If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                    '    objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = 0
                    'End If

                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
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
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
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


            'Added by Tapan Nath 14/03/2011
            If txtLcode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCode").InnerXml = txtLcode.Text.Trim
            End If

            If txtChainCode.Text.Trim <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("CHAIN_CODE").InnerXml = txtChainCode.Text.Trim
            End If
            'Added by Tapan Nath 14/03/2011
            If drpAgencyType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
            End If

            objOutputXml = objbzMIDT.TravelAssistance(objInputXml)
           
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdBal.Value = .Attributes("BAL").Value.Trim()
                    hdCms.Value = .Attributes("CMS").Value.Trim()
                    hdIci.Value = .Attributes("ICI").Value.Trim()
                    hdRel.Value = .Attributes("REL").Value.Trim()
                    hdAig.Value = .Attributes("AIG").Value.Trim()
                    hdChr.Value = .Attributes("CHR").Value.Trim()
                    hdProductivity.Value = .Attributes("Productivity").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdTravelAssistance.DataSource = ds.Tables("TRAVELASSISTANCE")
                grdTravelAssistance.DataBind()
                'pnlCount.Visible = True
                'txtRecordCount.Text = ds.Tables("TRAVELASSISTANCE").Rows.Count.ToString

                lblError.Text = ""
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
                txtRecordOnCurrentPage.Text = ds.Tables("TRAVELASSISTANCE").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName")

                    Case "LCode"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(0).Controls.Add(imgDown)

                        End Select

                    Case "chain_code"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(1).Controls.Add(imgDown)

                        End Select




                    Case "AgencyName"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select

                    Case "Group_Classification_Name"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select

                    Case "Address"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "COMP_VERTICAL"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(6).Controls.Add(imgDown)

                        End Select

                    Case "CITY"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                    Case "COUNTRY"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "MONTH"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select
                    Case "BAL"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select

                    Case "CMS"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select
                    Case "ICI"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(12).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(12).Controls.Add(imgDown)
                        End Select
                    Case "REL"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(13).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(13).Controls.Add(imgDown)
                        End Select
                    Case "AIG"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(14).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(14).Controls.Add(imgDown)
                        End Select


                    Case "CHR"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(15).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(15).Controls.Add(imgDown)
                        End Select

                    Case "Productivity"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdTravelAssistance.HeaderRow.Cells(16).Controls.Add(imgUp)
                            Case "TRUE"
                                grdTravelAssistance.HeaderRow.Cells(16).Controls.Add(imgDown)
                        End Select
                End Select
                '' @ Added Code To Show Image'
                ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@





                '*************************************
            Else
                grdTravelAssistance.DataSource = String.Empty
                grdTravelAssistance.DataBind()
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
        End Try
    End Sub



    Protected Sub grdTravelAssistance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTravelAssistance.RowDataBound
        If chkOfficeID.Checked = True Then
            e.Row.Cells(5).Visible = True
        Else
            e.Row.Cells(5).Visible = False
        End If
        If chkAverage.Checked = True Then
            e.Row.Cells(9).Visible = False
        Else
            e.Row.Cells(9).Visible = True
        End If


        If chkGroupClassification.Checked = False Then
            e.Row.Cells(3).Visible = False
        Else
            e.Row.Cells(3).Visible = True
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(10).Text = hdBal.Value.Trim()
            e.Row.Cells(11).Text = hdCms.Value.Trim()
            e.Row.Cells(12).Text = hdIci.Value.Trim()
            e.Row.Cells(13).Text = hdRel.Value.Trim()
            e.Row.Cells(14).Text = hdAig.Value.Trim()
            e.Row.Cells(15).Text = hdChr.Value.Trim()
            e.Row.Cells(16).Text = hdProductivity.Value.Trim()
        End If
    End Sub



    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            TravelAssistanceExport()

            If grdTravelAssistance.Rows.Count > 0 Then
                'PrepareGridViewForExport(grdTravelAssistance)
                'ExportGridView(grdTravelAssistance, "TravelAssistanceBooking.xls")

            End If



        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub TravelAssistanceExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzMIDT As New AAMS.bizProductivity.bzMIDT
        Dim StartDate1 As String
        Dim EndDate1 As String
        Try
            objInputXml.LoadXml("<PR_SEARCH_TRAVELASSISTANCE_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><LCode></LCode><AgencyName></AgencyName><GroupProductivity></GroupProductivity><Provider></Provider><Aoffice></Aoffice><Region></Region><ShowAverage></ShowAverage><FromDate></FromDate><ToDate></ToDate><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><RESP_1A></RESP_1A><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><CHAIN_CODE></CHAIN_CODE><AGENCYTYPEID></AGENCYTYPEID><TYPEID></TYPEID><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_TRAVELASSISTANCE_INPUT>")
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
                '.SelectSingleNode("LCode").InnerXml = hdAgencyNameId.Value.Trim()

                If (Request.Form("txtAgencyName") <> "" And hdAgencyNameId.Value = "") Or (Request.Form("txtAgencyName") = "" Or hdAgencyNameId.Value = "") Then
                    .SelectSingleNode("LCode").InnerText = ""
                    .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                Else
                    .SelectSingleNode("LCode").InnerText = hdAgencyNameId.Value.Trim()
                    .SelectSingleNode("AgencyName").InnerText = ""
                End If


                '  .SelectSingleNode("AgencyName").InnerText = txtAgencyName.Text.Trim()
                If chbWholeGroup.Checked = True Then
                    .SelectSingleNode("GroupProductivity").InnerText = "True"
                Else
                    .SelectSingleNode("GroupProductivity").InnerText = "False"
                End If

                If chkAverage.Checked = True Then
                    .SelectSingleNode("ShowAverage").InnerText = "True"
                Else
                    .SelectSingleNode("ShowAverage").InnerText = "False"
                End If
                If ddlCrs.SelectedIndex <> 0 Then
                    .SelectSingleNode("Provider").InnerXml = ddlCrs.SelectedValue
                End If
                If drpAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpAOffice.SelectedValue.Trim()
                End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                StartDate1 = "01" & "/" & drpMonthFrom.SelectedValue & "/" & drpYearFrom.SelectedValue
                EndDate1 = DateTime.DaysInMonth(drpYearTo.SelectedValue, drpMonthTo.SelectedValue) & "/" & drpMonthTo.SelectedValue & "/" & drpYearTo.SelectedValue

                .SelectSingleNode("FromDate").InnerText = objeAAMS.ConvertTextDate(StartDate1)
                .SelectSingleNode("ToDate").InnerText = objeAAMS.ConvertTextDate(EndDate1)
                'Dim objEmpXml As New XmlDocument
                'objEmpXml.LoadXml(Session("Security"))


                If drpGroupAgencyType.SelectedIndex <> 0 Then
                    .SelectSingleNode("TYPEID").InnerText = drpGroupAgencyType.SelectedValue.Trim()
                End If
                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
                '    .SelectSingleNode("Limited_To_Aoffice").InnerText = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText
                'End If

                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.ToUpper = "TRUE" Then
                '    .SelectSingleNode("Limited_To_OwnAagency").InnerXml = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText.Trim()
                'End If


                'If objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper = "TRUE" Then
                '    '.SelectSingleNode("Limited_To_Region").InnerXml = objEmpXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.Trim()
                'End If
                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))


                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                            Else
                                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                            End If
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If

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
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "True"
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "False"
                        End If
                        'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = "False"
                    End If
                    'If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                    '    objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("SECREGIONID").InnerText = 0
                    'End If

                    If Not Session("LoginSession") Is Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("RESP_1A").InnerText = Session("LoginSession").ToString().Split("|")(0)
                    End If

                End If

               
                ' objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "AgencyName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "AgencyName" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc").ToString
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
            If drpAgencyType.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AGENCYTYPEID").InnerText = drpAgencyType.SelectedValue.Trim()
            End If

            objOutputXml = objbzMIDT.TravelAssistance(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdBal.Value = .Attributes("BAL").Value.Trim()
                    hdCms.Value = .Attributes("CMS").Value.Trim()
                    hdIci.Value = .Attributes("ICI").Value.Trim()
                    hdRel.Value = .Attributes("REL").Value.Trim()
                    hdAig.Value = .Attributes("AIG").Value.Trim()

                    hdChr.Value = .Attributes("CHR").Value.Trim()

                    hdProductivity.Value = .Attributes("Productivity").Value.Trim()
                End With
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdTravelAssistance.DataSource = ds.Tables("TRAVELASSISTANCE")
                grdTravelAssistance.DataBind()


                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("TRAVELASSISTANCE")
                objXmlNodeClone = objXmlNode.CloneNode(True)
                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next
                With objXmlNodeClone
                    .Attributes("MONTH").Value = "Total"
                    .Attributes("BAL").Value = hdBal.Value.Trim()
                    .Attributes("CMS").Value = hdCms.Value.Trim()
                    .Attributes("ICI").Value = hdIci.Value.Trim()
                    .Attributes("REL").Value = hdRel.Value.Trim()
                    .Attributes("AIG").Value = hdAig.Value.Trim()

                    .Attributes("CHR").Value = hdChr.Value

                    .Attributes("Productivity").Value = hdProductivity.Value.Trim()
                End With
                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)
                Dim objExport As New ExportExcel
                'If chkGroupClassification.Checked = True Then
                '    Dim strArray() As String = {"LCode", "Chain Code", "Name", "Type", "Office Id", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                '    Dim intArray() As Integer = {0, 14, 1, 13, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                '    objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                'Else
                '    Dim strArray() As String = {"LCode", "Chain Code", "Name", "Office Id", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                '    Dim intArray() As Integer = {0, 14, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                '    objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                'End If

                If chkGroupClassification.Checked = True Then
                    If chkAverage.Checked = True Then
                        If chkOfficeID.Checked = True Then
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Office Id", "Address", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 13, 2, 3, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Office Id", "Address", "Company Vertical", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 13, 2, 3, 16, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        Else
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Address", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 13, 3, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Address", "Company Vertical", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 13, 3, 16, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        End If
                    Else
                        If chkOfficeID.Checked = True Then
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Office Id", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 13, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Office Id", "Address", "Company Vertical", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 13, 2, 3, 16, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        Else
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 13, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Agency Category", "Address", "Company Vertical", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 13, 3, 16, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        End If
                       
                    End If
                Else
                    If chkAverage.Checked = True Then
                        If chkOfficeID.Checked = True Then
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Office Id", "Address", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Office Id", "Address", "Company Vertical", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 2, 3, 16, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        Else
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Address", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 3, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Address", "Company Vertical", "City", "Country", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 3, 16, 4, 5, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        End If
                    Else
                        If chkOfficeID.Checked = True Then
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Office Id", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Office Id", "Address", "Company Vertical", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 2, 3, 16, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")
                        Else
                            'Dim strArray() As String = {"LCode", "Chain Code", "Name", "Address", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            'Dim intArray() As Integer = {0, 14, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            Dim strArray() As String = {"LCode", "Chain Code", "Name", "Address", "Company Vertical", "City", "Country", "Month", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "Productivity"}
                            Dim intArray() As Integer = {0, 14, 1, 3, 16, 4, 5, 6, 7, 8, 9, 10, 11, 15, 12}
                            objExport.ExportDetails(objOutputXml, "TRAVELASSISTANCE", intArray, strArray, ExportExcel.ExportFormat.Excel, "TravelAssistanceExport.xls")

                        End If
                    End If
                End If
               
                '*************************************
            Else
                grdTravelAssistance.DataSource = String.Empty
                grdTravelAssistance.DataBind()
                'pnlCount.Visible = False
                'txtRecordCount.Text = "0"
                pnlPaging.Visible = False
                'txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Private Sub ExportGridView(ByVal grdvCommon, ByVal FileName)
        'Dim strFileName As String = CType(grdvCommon, GridView).ID
        'strFileName = strFileName.Substring(4)
        'Dim attachment As String = "attachment; filename=" & strFileName & ".xls"
        'Response.ClearContent()
        'Response.AddHeader("content-disposition", attachment)
        'Response.ContentType = "application/ms-excel"
        'Dim sw As New StringWriter
        'Dim htw As New HtmlTextWriter(sw)
        'Dim frm As New HtmlForm()
        'grdvCommon.Parent.Controls.Add(frm)
        'frm.Attributes("runat") = "server"
        'frm.Controls.Add(grdvCommon)
        'frm.RenderControl(htw)
        'Response.Write(sw.ToString())
        'Response.End()
        Dim attachment As String = "attachment; filename=" + FileName
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
       ' chbWholeGroup.Checked = False
       ' chkAverage.Checked = False
        'chkOfficeID.Checked = False
        'lblError.Text = ""
        'txtAgencyName.Text = ""
        'ddlCrs.SelectedIndex = 0
        'hdAgencyName.Value = ""
       ' drpCity1.SelectedIndex = 0
        'drpCountry1.SelectedIndex = 0
        'drpAOffice.SelectedIndex = 0
        'drpRegion.SelectedIndex = 0
        'drpMonthTo.SelectedValue = "12"
        'drpMonthFrom.SelectedValue = "1"
        'drpYearFrom.SelectedValue = DateTime.Now.Year
        'drpYearTo.SelectedValue = DateTime.Now.Year
        'grdTravelAssistance.DataSource = Nothing
        'grdTravelAssistance.DataBind()
        'txtRecordCount.Text = 0
        'pnlCount.Visible = False
        'txtRecordCount.Text = "0"
Response.Redirect("PDSR_TravelAssistanceBooking.aspx", False)
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            TravelAssistanceSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdTravelAssistance_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdTravelAssistance.Sorting
        Try
            AllNonDescColumnDefault()
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
                If objDictionary.Contains(SortName) Then
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
                    If objDictionary.Contains(SortName) Then
                        ViewState("Desc") = "TRUE"
                    End If
                End If
            End If
            TravelAssistanceSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            TravelAssistanceSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            TravelAssistanceSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
