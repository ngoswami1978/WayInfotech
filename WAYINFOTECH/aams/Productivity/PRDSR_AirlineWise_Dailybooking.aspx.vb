Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
Partial Class Productivity_PRDSR_AirlineWise_Dailybooking
    Inherits System.Web.UI.Page
    Dim objMsg As New eAAMSMessage
    Dim objEams As New eAAMS
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim FooterDs As DataSet
    Dim objED As New EncyrptDeCyrpt
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
            objDictionary.Add("Airline_Name", "Airline_Name")
        Catch ex As Exception

        End Try
    End Sub

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        drpAirLineName.Attributes.Add("onkeyup", "return gotop('drpAirLineName')")
        drpCitys.Attributes.Add("onkeyup", "return gotop('drpCitys')")
        drpCountrys.Attributes.Add("onkeyup", "return gotop('drpCountrys')")
        drpOneAOffice.Attributes.Add("onkeyup", "return gotop('drpOneAOffice')")
        drpRegion.Attributes.Add("onkeyup", "return gotop('drpRegion')")
        Page.MaintainScrollPositionOnPostBack = True
        'Code for Paging $ Sorting
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        'Code for Paging $ Sorting

        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        ' This code is used for Expiration of Page From Cache
        objEams.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objEams.CheckSession())
            Exit Sub
        End If
        'btnDisplay.Attributes.Add("onclick", "return ValidateSearchDAILYB();")
        If Session("Security") Is Nothing Then
            lblError.Text = "Re Login"
            Exit Sub
        End If

        If Not Page.IsPostBack Then
            BindAllControls()
            drpMonth.SelectedIndex = DateTime.Now.Month - 1
            drpYear.SelectedValue = DateTime.Now.Year
        End If

        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        If objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText.ToUpper = "TRUE" Then
            drpOneAOffice.SelectedValue = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText.Trim()  'Login time u find Need for discussion
            drpOneAOffice.Enabled = False
        End If

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airlinewise Dailybookings']").Count <> 0 Then
                strBuilder = objEams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airlinewise Dailybookings']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    btnDisplay.Enabled = False
                    btnExport.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If

            End If
        Else
            strBuilder = objEams.SecurityCheck(31)
        End If


    End Sub
    Private Sub BindAllControls()
        Try

            objEams.BindDropDown(drpAirLineName, "AIRLINE", False, 3)
            objEams.BindDropDown(drpCitys, "CITY", False, 3)
            objEams.BindDropDown(drpCountrys, "COUNTRY", False, 3)
            objEams.BindDropDown(drpOneAOffice, "AOFFICE", False, 3)
            objEams.BindDropDown(drpRegion, "REGION", False, 3)
            objEams.BindDropDown(drpLstGroupType, "AGROUP", False, 3)
            objEams.BindDropDown(DlstCompVertical, "CompanyVertical", True, 3)
            'For Binding Year for DropDown
            Dim dtYear As New DateTime
            Dim counter As Integer
            For counter = DateTime.Now.Year To 1990 Step -1
                drpYear.Items.Add(counter.ToString())
            Next
            For counter = 0 To 11
                drpMonth.Items.Add(MonthName(counter + 1))
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisplay.Click
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Private Sub BindData()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
        Try
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISE_DAILYBOOKINGS_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY> <Aoffice></Aoffice><Region></Region><Month></Month><Year></Year><EmployeeID></EmployeeID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_AIRLINEWISE_DAILYBOOKINGS_INPUT>")
            With objInputXml.DocumentElement


                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
                End If
                If drpCountrys.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
                End If
                If drpOneAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
                End If


                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                'AIRLINE_CODE 

                If drpAirLineName.SelectedIndex <> 0 Then
                    .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
                End If

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

                .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
                .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()
                
                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("EmployeeID").InnerText = str(0)


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
                    ViewState("SortName") = "Airline_Name"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Airline_Name" '"LOCATION_CODE"
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
            End With

            
            objOutputXml = objbzDailyBooking.AirLineWiseDailyBookings(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                tlbgrdvAirWithAirBr.Width = 3000
                grdvAirWithAirBr.Width = 3000

                objXmlReader = New XmlNodeReader(objOutputXml)
                'Code for Pagin
                ViewState("PrevSearching") = objInputXml.OuterXml
                ds.ReadXml(objXmlReader)
                'This dataSet contains Footer Value 
                FooterDs = New DataSet()
                FooterDs = ds
                lblError.Text = ""
                'Grid binding as per our conditions


                grdvAirWithAirBr.DataSource = ds.Tables("AIRLINEWISE_DAILYBOOKINGS")
                grdvAirWithAirBr.DataBind()
                'Code for paging
                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndexMukund(grdvAirWithAirBr)
                If ViewState("Desc") = "FALSE" Then
                    grdvAirWithAirBr.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvAirWithAirBr.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
                'End If

            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDs = Nothing
                'txtRecordCount.Text = "0"



                'Code for Paging
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                'Code for Paging

                grdvAirWithAirBr.DataSource = ds.Tables("AIRLINEWISE_DAILYBOOKINGS")
                grdvAirWithAirBr.DataBind()
                'End If


                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

   

    Protected Sub grdvAirWithAirBr_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirWithAirBr.RowCreated
        Dim grvRow As GridViewRow
        grvRow = e.Row
        Dim dtFormat As String
        dtFormat = (drpMonth.SelectedIndex + 1).ToString() & "/" & drpYear.SelectedItem.Text.Trim()
        If grvRow.RowType = DataControlRowType.Header Then

            Dim cellcounter, datecounter As Int16
            cellcounter = 3
            If grdvAirWithAirBr.AllowSorting = True Then
                'CType(grvRow.Cells(16).Controls(0), LinkButton).Text = "Total Air"

                For datecounter = 1 To 31
                    CType(grvRow.Cells(cellcounter).Controls(0), LinkButton).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                Next
            Else
                '  grvRow.Cells(16).Text = "Total Air"
                For datecounter = 1 To 31
                    grvRow.Cells(cellcounter).Text = datecounter.ToString() & "/" & dtFormat
                    cellcounter += 1
                Next
            End If


            Dim intMont As Integer = drpMonth.SelectedIndex + 1

            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    grdvAirWithAirBr.Columns(32).Visible = False
                    grdvAirWithAirBr.Columns(33).Visible = False
                  Else
                    grdvAirWithAirBr.Columns(31).Visible = False
                    grdvAirWithAirBr.Columns(32).Visible = False
                    grdvAirWithAirBr.Columns(33).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvAirWithAirBr.Columns(33).Visible = False
            End If

        End If
    End Sub

    Protected Sub grdvAirWithAirBr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvAirWithAirBr.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.Footer Then
                If FooterDs IsNot Nothing Then

                    e.Row.Cells(0).Text = "Total" 'hdTARGET.Value
                    e.Row.Cells(1).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("AverageBookings").ToString() 'hdTARGETPERDAY.Value
                    e.Row.Cells(2).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("Netbookings").ToString() 'hdAverageBookings.Value
                    Dim columnCounter As Integer

                    For columnCounter = 1 To 31
                        e.Row.Cells(columnCounter + 2).Text = FooterDs.Tables("PAGE_TOTAL").Rows(0)("D" & (columnCounter).ToString).ToString()
                    Next
                End If
            End If

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim intMont As Integer = drpMonth.SelectedIndex + 1

            If intMont = 2 Then
                If Convert.ToInt16(drpYear.SelectedValue) Mod 4 = 0 Then
                    grdvAirWithAirBr.Columns(32).Visible = False
                    grdvAirWithAirBr.Columns(33).Visible = False

                Else
                    grdvAirWithAirBr.Columns(31).Visible = False
                    grdvAirWithAirBr.Columns(32).Visible = False
                    grdvAirWithAirBr.Columns(33).Visible = False
                End If

            End If

            If intMont = 4 Or intMont = 6 Or intMont = 9 Or intMont = 11 Then
                grdvAirWithAirBr.Columns(33).Visible = False
            End If

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

   

    Protected Sub grdvAirWithAirBr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvAirWithAirBr.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    

    

   

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)
        AllNonDescColumnDefault()
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
        BindData()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function GetSortColumnIndexMukund(ByVal grd As GridView) As Int16
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

    Private Sub bindCheckboxList()
        Dim objTaProducts As New AAMS.bizTravelAgency.bzOnlineStatus
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        objOutputXml = New XmlDocument
        objOutputXml = objTaProducts.List()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
        End If
    End Sub

    Protected Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect(Request.Url.OriginalString)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDailyBooking As New AAMS.bizProductivity.bzDailyBookings
        Try
            objInputXml.LoadXml("<PR_SEARCH_AIRLINEWISE_DAILYBOOKINGS_INPUT><GroupTypeID></GroupTypeID><AIRLINE_CODE></AIRLINE_CODE><CITY></CITY><COUNTRY></COUNTRY> <Aoffice></Aoffice><Region></Region><Month></Month><Year></Year><EmployeeID></EmployeeID><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC><COMP_VERTICAL></COMP_VERTICAL></PR_SEARCH_AIRLINEWISE_DAILYBOOKINGS_INPUT>")
            With objInputXml.DocumentElement

                If DlstCompVertical.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("COMP_VERTICAL").InnerText = DlstCompVertical.SelectedValue
                End If

                If drpCitys.SelectedIndex <> 0 Then
                    .SelectSingleNode("CITY").InnerText = drpCitys.SelectedItem.Text.Trim()
                End If
                If drpCountrys.SelectedIndex <> 0 Then
                    .SelectSingleNode("COUNTRY").InnerText = drpCountrys.SelectedItem.Text.Trim()
                End If
                If drpOneAOffice.SelectedIndex <> 0 Then
                    .SelectSingleNode("Aoffice").InnerText = drpOneAOffice.SelectedValue.Trim()
                End If


                If drpRegion.SelectedIndex <> 0 Then
                    .SelectSingleNode("Region").InnerText = drpRegion.SelectedValue.Trim()
                End If
                'AIRLINE_CODE 

                If drpAirLineName.SelectedIndex <> 0 Then
                    .SelectSingleNode("AIRLINE_CODE").InnerText = drpAirLineName.SelectedValue.Trim()
                End If

                .SelectSingleNode("Month").InnerText = drpMonth.SelectedIndex + 1
                .SelectSingleNode("Year").InnerText = drpYear.SelectedItem.Text.Trim()

                Dim str As String()
                str = Session("LoginSession").ToString().Split("|")
                .SelectSingleNode("EmployeeID").InnerText = str(0)

                If drpLstGroupType.SelectedIndex <> 0 Then
                    .SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
                End If

            End With


            objOutputXml = objbzDailyBooking.AirLineWiseDailyBookings(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)


                'Export Start
                grdvAirWithAirBr.AllowSorting = False
                grdvAirWithAirBr.EnableViewState = True
                Dim objOutputXmlExport As New XmlDocument
                Dim objXmlNode, objXmlNodeClone As XmlNode
                objXmlNode = objOutputXml.DocumentElement.SelectSingleNode("AIRLINEWISE_DAILYBOOKINGS")
                objXmlNodeClone = objXmlNode.CloneNode(True)

                For Each XmlAttr As XmlAttribute In objXmlNodeClone.Attributes
                    XmlAttr.Value = ""
                Next

                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("PAGE_TOTAL").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    If objXmlNode.Attributes(strAttribut) IsNot Nothing Then
                        objXmlNodeClone.Attributes(strAttribut).Value = xmlAttrTotal.Value
                    End If
                    If objXmlNode.Attributes("Airline_Name") IsNot Nothing Then
                        objXmlNodeClone.Attributes("Airline_Name").Value = "Total"
                    End If

                Next

                objOutputXml.DocumentElement.AppendChild(objXmlNodeClone)

                Dim dSetBind As New DataTable
                Dim dCol As DataColumn
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("AIRLINEWISE_DAILYBOOKINGS").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dCol = New DataColumn(strAttribut, System.Type.GetType("System.String"))
                    dSetBind.Columns.Add(dCol)
                Next

                Dim dRow As DataRow
                dRow = dSetBind.NewRow()
                For Each xmlAttrTotal As XmlAttribute In objOutputXml.DocumentElement.SelectSingleNode("AIRLINEWISE_DAILYBOOKINGS").Attributes
                    Dim strAttribut As String = xmlAttrTotal.Name
                    dRow(strAttribut) = xmlAttrTotal.Value
                Next

                dSetBind.Rows.Add(dRow)

                grdvAirWithAirBr.DataSource = dSetBind
                grdvAirWithAirBr.DataBind()


                Dim objExport As New ExportExcel
                Dim IntInvisible As Integer = 0
                For intclmn As Integer = 0 To grdvAirWithAirBr.HeaderRow.Cells.Count - 1
                    If grdvAirWithAirBr.Columns(intclmn).Visible = False Then
                        IntInvisible = IntInvisible + 1
                    End If
                Next
                Dim strArray(grdvAirWithAirBr.HeaderRow.Cells.Count - 1 - IntInvisible) As String

                Dim intArray(grdvAirWithAirBr.HeaderRow.Cells.Count - 1 - IntInvisible) As Integer
                Dim intclmnVis As Integer = 0

                For intclmn As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                    If grdvAirWithAirBr.Columns(intclmn).Visible = True Then
                        'grdvDailyBookingsAll.HeaderRow.Cells(38).Text
                        'strArray(intclmnVis) = grdvDailyBookingsAll.Columns(intclmn).HeaderText.ToString().Replace("'", " ") 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name
                        strArray(intclmnVis) = grdvAirWithAirBr.HeaderRow.Cells(intclmn).Text 'objOutputXml.DocumentElement.SelectSingleNode("BIDTMONTHLYBREAKUP").Attributes(intclmn - 1).Name

                        '@ Finding Position From xml Related with Header Text

                        For kk As Integer = 0 To objXmlNodeClone.Attributes.Count - 1

                            If objXmlNodeClone.Attributes(kk).Name.Trim = grdvAirWithAirBr.Columns(intclmn).SortExpression.Trim Then

                                intArray(intclmnVis) = kk

                                intclmnVis = intclmnVis + 1

                                Exit For

                            End If

                        Next kk

                    End If

                Next intclmn



                objExport.ExportDetails(objOutputXml, "AIRLINEWISE_DAILYBOOKINGS", intArray, strArray, ExportExcel.ExportFormat.Excel, "AIRLINEWISEDAILYBOOKINGS.xls")
                'Code for Exporting Data

                'End of Export
            Else
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                FooterDs = Nothing
                'txtRecordCount.Text = "0"



                'Code for Paging
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                'Code for Paging

                grdvAirWithAirBr.DataSource = ds.Tables("AIRLINEWISE_DAILYBOOKINGS")
                grdvAirWithAirBr.DataBind()
                'End If


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
