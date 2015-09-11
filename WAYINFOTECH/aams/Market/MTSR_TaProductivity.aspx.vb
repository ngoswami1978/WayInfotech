Imports System.IO
Partial Class Market_MTSR_TaProductivity
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        ' This code is used for Expiration of Page From Cache
        objEams.ExpirePageCache()
        ' btnSearch.Attributes.Add("onclick", "return ValidateSearch();")
        ' btnPrint.Attributes.Add("onclick", "return CallPrint('grdvTaProductivity')")
        '  btnReset.Attributes.Add("onclick", "return ResetControls();")
        ' This code is usedc for checking session handler according to user login.

        'Code for Paging $ Sorting
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        'Code for Paging $ Sorting


        drpCity.Attributes.Add("onkeyup", "return gotop('drpCity')")
        drpCountry.Attributes.Add("onkeyup", "return gotop('drpCountry')")
        drpOneAoffice.Attributes.Add("onkeyup", "return gotop('drpOneAoffice')")
        drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")
        drpProviders.Attributes.Add("onkeyup", "return gotop('drpProviders')")
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
            'ClientScript.RegisterStartupScript(Me.GetType(),"loginScript", objEams.CheckSession())
            Exit Sub
        End If
        If Not Page.IsPostBack Then
            LoadAllControls()
        End If

        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='TA Productivity']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='TA Productivity']").Attributes("Value").Value)
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

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindGrid()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub BindGrid()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
        objInputXml.LoadXml("<PR_SEARCHTAPRODUCTIVITY_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><PROVIDERCODE></PROVIDERCODE><SELECTBY></SELECTBY><SHOWAVG></SHOWAVG><LIMITED_TO_REGION></LIMITED_TO_REGION> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/><COMP_VERTICAL></COMP_VERTICAL> </PR_SEARCHTAPRODUCTIVITY_INPUT>")

        With objInputXml.DocumentElement

            If DlstCompVertical.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
            End If


            If drpOneAoffice.SelectedIndex <> 0 Then
                .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
            End If

            If drpCity.SelectedIndex <> 0 Then
                .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
            End If

            '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

            If drpCountry.SelectedIndex <> 0 Then
                .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
            End If

            If drpRegion.SelectedIndex <> 0 Then
                .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
            End If

            'If chkShowBr.Checked = True Then
            '    .SelectSingleNode("ShowGroup").InnerText = "True"
            'Else
            '    .SelectSingleNode("ShowGroup").InnerText = "False"
            'End If

            ' If drpProviders.SelectedIndex <> 0 Then
            .SelectSingleNode("PROVIDERCODE").InnerText = drpProviders.SelectedValue.Trim()
            ' End If

            .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

            .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

            .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

            .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

            .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

            If chkShowBr.Checked = True Then
                .SelectSingleNode("SHOWAVG").InnerText = "1"
            Else
                .SelectSingleNode("SHOWAVG").InnerText = "0"
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


        If ViewState("PrevSearching") IsNot Nothing Then
            Dim objXml1 As New XmlDocument
            objXml1.LoadXml(ViewState("PrevSearching"))
            Dim objNodes As XmlNodeList = objXml1.DocumentElement.ChildNodes
            If objXml1.OuterXml <> objInputXml.OuterXml Then
                If (objXml1.DocumentElement.SelectSingleNode("SELECTBY").InnerText <> rdSummaryOption.SelectedValue.Trim()) Or (objXml1.DocumentElement.SelectSingleNode("SHOWAVG").InnerText <> IIf((chkShowBr.Checked), "1", "0")) Then
                    ViewState("SortName") = "SELECTBY"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "SELECTBY" '"LOCATION_CODE"

                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                End If
            End If
        End If



        objOutputXml = objbzDailyBooking.SearchTAProductivity(objInputXml)

        '<PR_SEARCHTAP_OUTPUT><DETAIL SELECTBY='' MONTH='' BAL='' CMS='' ICI='' REL='' AIG='' 
        'BOOKING='' TOTAL=''/><TOTAL BAL='' CMS='' ICI='' REL='' AIG='' TOTAL=''/><Errors Status=''>
        '<Error Code='' Description=''/></Errors></PR_SEARCHTAP_OUTPUT>




        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""
            ViewState("PrevSearching") = objInputXml.OuterXml

            With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                hdBal.Value = .Attributes("BAL").Value.Trim()
                hdCms.Value = .Attributes("CMS").Value.Trim()
                hdIci.Value = .Attributes("ICI").Value.Trim()
                hdRel.Value = .Attributes("REL").Value.Trim()
                hdAig.Value = .Attributes("AIG").Value.Trim()
                hdChr.Value = .Attributes("CHR").Value.Trim()
                hdBookings.Value = .Attributes("BOOKING").Value.Trim()
                hdTotal.Value = .Attributes("TOTAL").Value.Trim()
            End With

            grdvTaProductivity.DataSource = ds.Tables("DETAIL")
            grdvTaProductivity.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(grdvTaProductivity)
            If ViewState("Desc") = "FALSE" Then
                grdvTaProductivity.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                grdvTaProductivity.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Private Sub LoadAllControls()
        Try
            Dim objSecurityXml As New XmlDocument
            objEams.BindDropDown(drpProviders, "PROVIDERS", False, 3)

            objEams.BindDropDown(drpCity, "CITY", False, 3)
            objEams.BindDropDown(drpCountry, "COUNTRY", False, 3)
            objEams.BindDropDown(drpOneAoffice, "AOFFICE", False, 3)
            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
            objEams.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            Dim dtYear As New DateTime
            Dim counter As Integer

            drpYearF.Items.Clear()
            drpYearTo.Items.Clear()

            'drpYearF.Items.Insert(0, New ListItem("All", ""))
            'drpYearTo.Items.Insert(0, New ListItem("All", ""))

            For counter = DateTime.Now.Year To 1990 Step -1
                drpYearF.Items.Add(counter.ToString())
                drpYearTo.Items.Add(counter.ToString())
            Next
            For counter = 0 To 11
                drpMonthF.Items.Add(MonthName(counter + 1))
                drpMonthTo.Items.Add(MonthName(counter + 1))
            Next

            drpYearF.Items.FindByText(DateTime.Now.Year.ToString()).Selected = True
            drpYearTo.Items.FindByText(DateTime.Now.Year.ToString()).Selected = True

            drpMonthF.SelectedIndex = 0
            drpMonthTo.SelectedIndex = 11

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
            Dim objExportNew As New ExportExcel
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
            Dim strArray() As String
            Dim intArray() As Integer

            Try
                objInputXml.LoadXml("<PR_SEARCHTAPRODUCTIVITY_INPUT><GroupTypeID></GroupTypeID><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><PROVIDERCODE></PROVIDERCODE><SELECTBY></SELECTBY><SHOWAVG></SHOWAVG><LIMITED_TO_REGION></LIMITED_TO_REGION> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> <COMP_VERTICAL></COMP_VERTICAL></PR_SEARCHTAPRODUCTIVITY_INPUT>")

                With objInputXml.DocumentElement

                    If DlstCompVertical.SelectedValue <> "" Then
                        objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                    End If

                    If drpOneAoffice.SelectedIndex <> 0 Then
                        .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
                    End If

                    If drpCity.SelectedIndex <> 0 Then
                        .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                    End If

                    '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

                    If drpCountry.SelectedIndex <> 0 Then
                        .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                    End If

                    If drpRegion.SelectedIndex <> 0 Then
                        .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
                    End If

                    .SelectSingleNode("PROVIDERCODE").InnerText = drpProviders.SelectedValue.Trim()

                    .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

                    .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

                    .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

                    .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

                    .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

                    If chkShowBr.Checked = True Then
                        .SelectSingleNode("SHOWAVG").InnerText = "1"
                    Else
                        .SelectSingleNode("SHOWAVG").InnerText = "0"
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
                objOutputXml = objbzDailyBooking.SearchTAProductivity(objInputXml)

                '<PR_SEARCHTAP_OUTPUT><DETAIL SELECTBY='' MONTH='' BAL='' CMS='' ICI='' REL='' AIG='' 
                'BOOKING='' TOTAL=''/><TOTAL BAL='' CMS='' ICI='' REL='' AIG='' TOTAL=''/><Errors Status=''>
                '<Error Code='' Description=''/></Errors></PR_SEARCHTAP_OUTPUT>

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    'Dim arlstColNo As New ArrayList
                    'Dim arlstColName As New ArrayList
                    'Dim counter As Int16 = 0
                    'For counter = 0 To 8
                    '    arlstColNo.Insert(counter, counter)
                    'Next

                    'If rdSummaryOption.SelectedValue = "1" Then
                    '    arlstColName.Insert(0, "City")
                    'ElseIf rdSummaryOption.SelectedValue = "2" Then
                    '    arlstColName.Insert(0, "Country")
                    'ElseIf rdSummaryOption.SelectedValue = "3" Then
                    '    arlstColName.Insert(0, "Region")
                    'ElseIf rdSummaryOption.SelectedValue = "4" Then
                    '    arlstColName.Insert(0, "Office")
                    'ElseIf rdSummaryOption.SelectedValue = "5" Then
                    '    arlstColName.Insert(0, "Provider")
                    '    ' arlstColName.Insert(0, "Total")
                    'End If
                    'arlstColName.Insert(1, "MONTH")
                    'arlstColName.Insert(2, "BAL")
                    'arlstColName.Insert(3, "CMS")
                    'arlstColName.Insert(4, "ICI")
                    'arlstColName.Insert(5, "REL")
                    'arlstColName.Insert(6, "AIG")

                    'If rdSummaryOption.SelectedValue = "5" Then
                    '    arlstColName.Insert(7, "TOTAL")
                    'Else
                    '    arlstColName.Insert(7, "BOOKING")
                    'End If

                    ''If rdSummaryOption.SelectedValue = "5" Then
                    ''    arlstColName.Insert(7, "BOOKING")
                    ''    'remove column 7
                    ''Else
                    ''    arlstColName.Insert(7, "TOTAL")
                    ''    'Remove column 8
                    ''End If


                    'arlstColName.Insert(8, "TOTAL")

                    'If rdSummaryOption.SelectedValue = "5" Then
                    '    arlstColName.RemoveRange(2, 5)
                    '    arlstColName.RemoveAt(3)

                    '    arlstColNo.RemoveRange(2, 5)
                    '    arlstColNo.RemoveAt(3)
                    '    'e.Row.Cells(2).Visible = False
                    '    'e.Row.Cells(3).Visible = False
                    '    'e.Row.Cells(4).Visible = False
                    '    'e.Row.Cells(5).Visible = False
                    '    'e.Row.Cells(6).Visible = False

                    '    'If e.Row.RowType = DataControlRowType.Header Then
                    '    '    e.Row.Cells(7).Text = "Total"
                    '    'End If
                    '    'If e.Row.RowType = DataControlRowType.Footer Then
                    '    '    e.Row.Cells(7).Text = hdBookings.Value.Trim()
                    '    'End If

                    '    'e.Row.Cells(8).Visible = False
                    'Else
                    '    arlstColName.RemoveAt(7)
                    '    arlstColNo.RemoveAt(7)
                    'End If

                    'If chkShowBr.Checked = True Then
                    '    arlstColName.RemoveAt(1)
                    '    arlstColNo.RemoveAt(1)
                    'End If

                    Dim objOutputXmlExport As New XmlDocument
                    Dim objXmlNode, objXmlNodeClone As XmlNode
                    objOutputXmlExport.LoadXml(ds.GetXml().ToString)
                    objXmlNode = objOutputXmlExport.DocumentElement.SelectSingleNode("DETAIL")
                    objXmlNodeClone = objXmlNode.CloneNode(True)
                    For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                        XmlAttr.Value = ""
                    Next
                    With objXmlNodeClone
                        If ds IsNot Nothing Then
                            .Attributes("SELECTBY").Value = "Total "
                            .Attributes("BAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BAL").ToString()
                            .Attributes("CMS").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CMS").ToString()
                            .Attributes("ICI").Value = ds.Tables("PAGE_TOTAL").Rows(0)("ICI").ToString()
                            .Attributes("REL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("REL").ToString()
                            .Attributes("AIG").Value = ds.Tables("PAGE_TOTAL").Rows(0)("AIG").ToString()
                            .Attributes("BOOKING").Value = ds.Tables("PAGE_TOTAL").Rows(0)("BOOKING").ToString()
                            .Attributes("TOTAL").Value = ds.Tables("PAGE_TOTAL").Rows(0)("TOTAL").ToString()
                            .Attributes("CHR").Value = ds.Tables("PAGE_TOTAL").Rows(0)("CHR").ToString()
                        End If
                    End With
                    objOutputXmlExport.DocumentElement.AppendChild(objXmlNodeClone)
                    If rdSummaryOption.SelectedValue = "5" Then
                        If chkShowBr.Checked = True Then
                            strArray = New String() {"PROVIDER", "TOTAL"}
                            intArray = New Integer() {0, 7}
                        Else
                            strArray = New String() {"PROVIDER", "MONTH", "TOTAL"}
                            intArray = New Integer() {0, 1, 7}
                        End If
                    Else
                        If chkShowBr.Checked = True Then
                            strArray = New String() {"", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "TOTAL"}
                            intArray = New Integer() {0, 2, 3, 4, 5, 6, 9, 8}

                        Else
                            strArray = New String() {"", "MONTH", "BAL", "CMS", "ICI", "REL", "AIG", "CHR", "TOTAL"}
                            intArray = New Integer() {0, 1, 2, 3, 4, 5, 6, 9, 8}
                        End If
                        If rdSummaryOption.SelectedValue = "1" Then
                            strArray(0) = "CITY"
                        ElseIf rdSummaryOption.SelectedValue = "2" Then
                            strArray(0) = "COUNTRY"
                        ElseIf rdSummaryOption.SelectedValue = "3" Then
                            strArray(0) = "REGION"
                        ElseIf rdSummaryOption.SelectedValue = "4" Then
                            strArray(0) = "OFFICE"
                        End If
                    End If
                    objExportNew.ExportDetails(objOutputXmlExport, "DETAIL", intArray, strArray, ExportExcel.ExportFormat.Excel, "TA_ProductivityReport.xls")
                Else
                    pnlPaging.Visible = False
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try








            'Old Code for Exporting Data
            'grdvTaProductivity.AllowSorting = False
            'grdvTaProductivity.EnableViewState = True
            'grdvTaProductivity.HeaderStyle.ForeColor = Drawing.Color.Black

            ''ExportGridView(grdvTaProductivity)
            ' BindDataExport()
            '' grdvTaProductivity.Columns(grdvTaProductivity.Columns.Count - 1).Visible = False
            'ExportGridView(grdvTaProductivity)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ExportGridView(ByVal grdvCommon As GridView)
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

    Protected Sub grdvTaProductivity_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTaProductivity.RowDataBound
        Try
            

            If e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(0).Text = "Total"
                e.Row.Cells(2).Text = hdBal.Value.Trim()
                e.Row.Cells(3).Text = hdCms.Value.Trim()
                e.Row.Cells(4).Text = hdIci.Value.Trim()
                e.Row.Cells(5).Text = hdRel.Value.Trim()
                e.Row.Cells(6).Text = hdAig.Value.Trim()
                e.Row.Cells(7).Text = hdChr.Value.Trim()
                'e.Row.Cells(7).Text = hdBookings.Value.Trim()
                'e.Row.Cells(8).Text = hdTotal.Value.Trim()

                e.Row.Cells(8).Text = hdBookings.Value.Trim()
                e.Row.Cells(9).Text = hdTotal.Value.Trim()
            End If


            If rdSummaryOption.SelectedValue = "5" Then

                e.Row.Cells(2).Visible = False
                e.Row.Cells(3).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(5).Visible = False
                e.Row.Cells(6).Visible = False
                e.Row.Cells(7).Visible = False
                'If e.Row.RowType = DataControlRowType.Header Then
                '    e.Row.Cells(7).Text = "Total"
                'End If
                If e.Row.RowType = DataControlRowType.Footer Then
                    'e.Row.Cells(7).Text = hdBookings.Value.Trim()
                    e.Row.Cells(8).Text = hdBookings.Value.Trim()
                End If

                'e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = False
            Else
                'e.Row.Cells(7).Visible = False
                e.Row.Cells(8).Visible = False
            End If

            If chkShowBr.Checked = True Then
                e.Row.Cells(1).Visible = False
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
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
#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = SortName
            '  ViewState("Desc") = "FALSE"
            '@ Added Code For Default descending sorting order on first time  of following Fields      
            ' @ SELECTBY, MONTH
            If SortName.Trim().ToUpper = "SELECTBY" Or SortName.Trim().ToUpper = "MONTH" Then
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
                ' @ SELECTBY, MONTH
                If SortName.Trim().ToUpper = "SELECTBY" Or SortName.Trim().ToUpper = "MONTH" Then
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

    Protected Sub grdvTaProductivity_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvTaProductivity.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvTaProductivity_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvTaProductivity.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            If grdvTaProductivity.AllowSorting = True Then
                If rdSummaryOption.SelectedValue = "1" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "5" Then
                    CType(e.Row.Cells(0).Controls(0), LinkButton).Text = "Provider"
                    'CType(e.Row.Cells(7).Controls(0), LinkButton).Text = "Total"
                    CType(e.Row.Cells(8).Controls(0), LinkButton).Text = "Total"
                End If
            Else
                If rdSummaryOption.SelectedValue = "1" Then
                    e.Row.Cells(0).Text = "City"
                ElseIf rdSummaryOption.SelectedValue = "2" Then
                    e.Row.Cells(0).Text = "Country"
                ElseIf rdSummaryOption.SelectedValue = "3" Then
                    e.Row.Cells(0).Text = "Region"
                ElseIf rdSummaryOption.SelectedValue = "4" Then
                    e.Row.Cells(0).Text = "Office"
                ElseIf rdSummaryOption.SelectedValue = "5" Then
                    e.Row.Cells(0).Text = "Provider"
                    'e.Row.Cells(7).Text = "Total"
                    e.Row.Cells(8).Text = "Total"
                End If
            End If
            
        End If
    End Sub
    Private Sub BindDataExport()
        Try

        
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDailyBooking As New AAMS.bizProductivity.bzTravelAssistance
            objInputXml.LoadXml("<PR_SEARCHTAPRODUCTIVITY_INPUT><CITY></CITY><COUNTRY></COUNTRY><AOFFICE></AOFFICE><REGION></REGION><MONTHFROM></MONTHFROM><YEARFROM></YEARFROM><MONTHTO></MONTHTO><YEARTO></YEARTO><PROVIDERCODE></PROVIDERCODE><SELECTBY></SELECTBY><SHOWAVG></SHOWAVG><LIMITED_TO_REGION></LIMITED_TO_REGION> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/> </PR_SEARCHTAPRODUCTIVITY_INPUT>")

            With objInputXml.DocumentElement

                If drpOneAoffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("AOFFICE").InnerText = drpOneAoffice.SelectedValue.Trim()
                End If

                If drpCity.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text.Trim()
                End If

                '.SelectSingleNode("CITY").InnerXml = drpCity.SelectedItem.Text.Trim()

                If drpCountry.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text.Trim()
                End If

                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("REGION").InnerText = drpRegion.SelectedValue.Trim()
                End If

                .SelectSingleNode("PROVIDERCODE").InnerText = drpProviders.SelectedValue.Trim()

                .SelectSingleNode("MONTHFROM").InnerText = drpMonthF.SelectedIndex + 1

                .SelectSingleNode("YEARFROM").InnerText = drpYearF.SelectedItem.Text.Trim()

                .SelectSingleNode("MONTHTO").InnerText = drpMonthTo.SelectedIndex + 1

                .SelectSingleNode("YEARTO").InnerText = drpYearTo.SelectedItem.Text.Trim()

                .SelectSingleNode("SELECTBY").InnerText = rdSummaryOption.SelectedValue.Trim()

                If chkShowBr.Checked = True Then
                    .SelectSingleNode("SHOWAVG").InnerText = "1"
                Else
                    .SelectSingleNode("SHOWAVG").InnerText = "0"
                End If
                'Following Statement is written for Limited to own Agency
                Dim xDoc As New XmlDocument
                xDoc.LoadXml(Session("Security"))

                '.SelectSingleNode("Limited_To_OwnAagency").InnerText = xDoc.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText  'Login time u find Need for discussion

                If xDoc.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText.ToUpper() = "TRUE" Then
                    .SelectSingleNode("Limited_To_Region").InnerText = xDoc.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText   'Login time u find Need for discussion
                End If
            End With

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




            objOutputXml = objbzDailyBooking.SearchTAProductivity(objInputXml)

            '<PR_SEARCHTAP_OUTPUT><DETAIL SELECTBY='' MONTH='' BAL='' CMS='' ICI='' REL='' AIG='' 
            'BOOKING='' TOTAL=''/><TOTAL BAL='' CMS='' ICI='' REL='' AIG='' TOTAL=''/><Errors Status=''>
            '<Error Code='' Description=''/></Errors></PR_SEARCHTAP_OUTPUT>




            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                lblError.Text = ""
                ViewState("PrevSearching") = objInputXml.OuterXml

                With objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL")
                    hdBal.Value = .Attributes("BAL").Value.Trim()
                    hdCms.Value = .Attributes("CMS").Value.Trim()
                    hdIci.Value = .Attributes("ICI").Value.Trim()
                    hdRel.Value = .Attributes("REL").Value.Trim()
                    hdAig.Value = .Attributes("AIG").Value.Trim()
                    hdBookings.Value = .Attributes("BOOKING").Value.Trim()
                    hdTotal.Value = .Attributes("TOTAL").Value.Trim()
                End With

                grdvTaProductivity.DataSource = ds.Tables("DETAIL")
                grdvTaProductivity.DataBind()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
