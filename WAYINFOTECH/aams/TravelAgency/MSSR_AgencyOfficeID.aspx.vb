Imports System.Data
Imports System.Xml

Partial Class MSSR_AgencyOfficeID
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim x As Boolean
    Dim s As String
    Dim s1 As String
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '''''''''Sent Default Checked CheckBox''''''''
        Dim ccode As String = ""
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim objInputXml1, objOutputXml1 As New XmlDocument
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        If chkunallocatedid.Checked = True Then
            x = True
        End If
        If lblError.Text = "Please Select Agency Name" Then
            Exit Sub
        End If
        ddlCorporateCode.Attributes.Add("readonly", "readonly")
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Try
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITYOFFICEIDGENERATION", True, 3)
                btnGenerate.Attributes.Add("onclick", "return GenerateFunction();")
            End If

            If Request.QueryString("Popup") = "T" Then
                'btnGenerate.Enabled = False
                If Request.QueryString("Lcode") = "" Then
                    lblError.Text = "Please Select Agency Name"
                    Exit Sub
                Else
                    ccode = Request.QueryString("Lcode")
                End If
            End If
            If Request.QueryString("City") IsNot Nothing Then
                s1 = Request.QueryString("City").ToString().Trim()

                If s1 <> "" Then
                    drpCity.Items.FindByText(s1.ToString()).Selected = True
                    drpCity.Enabled = False
                Else
                    drpCity.Enabled = True
                    drpCity.SelectedIndex = -1
                End If
            End If

            If Request.QueryString("Action") IsNot Nothing Then
                s = Request.QueryString("Action").ToString().Trim()
                objInputXml1.LoadXml("<TA_AGENCYCORPORATECODE_INPUT><LCode></LCode></TA_AGENCYCORPORATECODE_INPUT>")
                objInputXml1.DocumentElement.SelectSingleNode("LCode").InnerXml = ccode
                objOutputXml1 = objOfficeId.GetAgencyCorporateCode(objInputXml1)
                ddlCorporateCode.Text = objOutputXml1.DocumentElement.SelectSingleNode("AGENCYCORPORATECODE").Attributes("CCode").Value()
                's = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                If Request.QueryString("City") IsNot Nothing Then
                    s1 = Request.QueryString("City").ToString().Trim()

                    If s1 <> "" Then
                        drpCity.Items.FindByText(s1.ToString()).Selected = True
                        drpCity.Enabled = False
                    Else
                        drpCity.Enabled = True
                        drpCity.SelectedIndex = -1
                    End If
                End If
            End If
            'End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnGenerate.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If lblError.Text = "Please Select Agency Name" Then
                Exit Sub
            End If
            OfficeIdSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub OfficeIdSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Try
            'hdCityText.Value = drpCity.SelectedItem.Text.Split("|").GetValue(1)
            'hdCity.Value = drpCity.SelectedValue
            objInputXml.LoadXml("<TA_SEARCHOFFICEID_INPUT><OFFICEID></OFFICEID><CityCode></CityCode><ALLOCATED></ALLOCATED><CCode></CCode><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TA_SEARCHOFFICEID_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = txtOfficeId.Text.Trim()
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = drpCity.SelectedItem.Value.Split("|").GetValue(1)
            Else
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("CCode").InnerXml = ddlCorporateCode.Text.Trim()
            If chkunallocatedid.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = True
            Else
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = False
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

            objOutputXml = objOfficeId.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                pnlPaging.Visible = True
                grdAgencyOfficeId.DataSource = ds.Tables("OFFICEID").DefaultView
                grdAgencyOfficeId.DataBind()
                lblError.Text = ""
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
                txtRecordOnCurrentPage.Text = ds.Tables("OFFICEID").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdAgencyOfficeId)
                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "OFFICEID"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                '    Case "NAME"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "CID"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(2).Controls.Add(imgDown)

                '        End Select
                '    Case "PROCESSING_DATE"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select
                '    Case "TERMINALID"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select
                '    Case "REMARKS"
                '        Select Case ViewState("Desc").ToString
                '            Case "FALSE"
                '                grdAgencyOfficeId.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdAgencyOfficeId.HeaderRow.Cells(5).Controls.Add(imgDown)

                '        End Select
                'End Select
                '' @ Added Code To Show Image'
                ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Else
                grdAgencyOfficeId.DataSource = String.Empty
                grdAgencyOfficeId.DataBind()
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
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
    Protected Sub grdAgencyOfficeId_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAgencyOfficeId.RowCommand
        'Dim strjscript As String = "<script language=""javascript"">"
        'strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
        'strjscript = strjscript & "</script" & ">"
        'Me.litOfficeId.Text = strjscript
    End Sub

    Protected Sub grdAgencyOfficeId_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAgencyOfficeId.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim lnkSelect As LinkButton
        Dim hdSelect As HiddenField
        hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
        lnkSelect = e.Row.FindControl("lnkSelect")
        If (Request.QueryString("PopUp")) Is Nothing Then
            lnkSelect.Visible = False
        Else
            lnkSelect.Visible = True
            'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
            lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
        End If
        Dim hdofficeid As New HiddenField
        hdofficeid = e.Row.FindControl("HiddenOfficeId")

        Dim lnkEdt As New System.Web.UI.HtmlControls.HtmlAnchor
        lnkEdt = e.Row.FindControl("lnkEdit")
        'If strBuilder(2) = "0" Then
        '    lnkEdt.Disabled = True
        'Else
        '    lnkEdt.Attributes.Add("onclick", "return EditFunction('" & hdofficeid.Value & "');")
        'End If
        lnkEdt.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdofficeid.Value) & "');")
        Dim lblPDate As Label
        lblPDate = e.Row.FindControl("lblPD")

        If lblPDate.Text <> "" Then
            lblPDate.Text = objeAAMS.ConvertDate(lblPDate.Text).ToString("dd-MMM-yy")
        End If

    End Sub



    Protected Sub btnReset_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtOfficeId.Text = String.Empty
        drpCity.SelectedIndex = 0
        ddlCorporateCode.Text = String.Empty
        lblError.Text = String.Empty
        If chkunallocatedid.Checked = False Then
            chkunallocatedid.Checked = True
        End If
        txtTotalRecordCount.Text = "0"
        pnlPaging.Visible = False
        grdAgencyOfficeId.DataSource = Nothing
        grdAgencyOfficeId.DataBind()
    End Sub

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdAgencyOfficeId.AllowSorting = False
        grdAgencyOfficeId.HeaderStyle.ForeColor = Drawing.Color.Black
        OfficeIdExport()
    End Sub
    Private Sub OfficeIdExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Try
            'hdCityText.Value = drpCity.SelectedItem.Text.Split("|").GetValue(1)
            'hdCity.Value = drpCity.SelectedValue
            objInputXml.LoadXml("<TA_SEARCHOFFICEID_INPUT><OFFICEID></OFFICEID><CityCode></CityCode><ALLOCATED></ALLOCATED><CCode></CCode><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TA_SEARCHOFFICEID_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = txtOfficeId.Text.Trim()
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = drpCity.SelectedItem.Value.Split("|").GetValue(1)
            Else
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("CCode").InnerXml = ddlCorporateCode.Text.Trim()
            If chkunallocatedid.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = True
            Else
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = False
            End If


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

            objOutputXml = objOfficeId.Search(objInputXml)
            ' Dim objOutputXml2 As New XmlDocument
            Dim xnode As XmlNode
            Dim nl As XmlNodeList
            nl = objOutputXml.DocumentElement.SelectNodes("OFFICEID")
            For Each xnode In nl
                Dim strdate As String
                strdate = xnode.Attributes("PROCESSING_DATE").Value
                If strdate <> "" Then
                    xnode.Attributes("PROCESSING_DATE").Value = objeAAMS.ConvertDate(strdate).ToString("dd-MMM-yy")
                End If
            Next
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                grdAgencyOfficeId.DataSource = ds.Tables("OFFICEID").DefaultView
                grdAgencyOfficeId.DataBind()
                lblError.Text = ""
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"OFFICEID", "NAME", "CID", "PROCESSING_DATE", "TERMINALID", "REMARKS"}
                Dim intArray() As Integer = {0, 1, 2, 3, 4, 5}
                objExport.ExportDetails(objOutputXml, "OFFICEID", intArray, strArray, ExportExcel.ExportFormat.Excel, "OfficeIdExport.xls")
            Else
                grdAgencyOfficeId.DataSource = String.Empty
                grdAgencyOfficeId.DataBind()
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            OfficeIdSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            OfficeIdSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            OfficeIdSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdAgencyOfficeId_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdAgencyOfficeId.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
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
                End If
            End If
            OfficeIdSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


End Class
