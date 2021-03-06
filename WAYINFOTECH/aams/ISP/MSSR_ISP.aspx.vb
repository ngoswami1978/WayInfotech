Imports System.Data

Partial Class ISP_MSSR_ISP
    Inherits System.Web.UI.Page
    Dim eaamsObj As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' Dim Lcode As String
            Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            lblError.Text = ""

            btnNew.Attributes.Add("onclick", "return InsertISP();")
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", eaamsObj.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = eaamsObj.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                eaamsObj.BindDropDown(drpCityName, "CITY", True, 3)
                ' eaamsObj.BindDropDown(drpIspProvider, "ISPPROVIDER", True, 3)

            End If


            If Not Page.IsPostBack Then
                If Request.QueryString("CityNmae") IsNot Nothing Then
                    drpCityName.Enabled = False
                    drpCityName.CssClass = "textboxgrey"
                    Dim li As New ListItem
                    li = drpCityName.Items.FindByText(Request.QueryString("CityNmae"))
                    If (li IsNot Nothing) Then
                        If li.Selected = False Then
                            li.Selected = True
                        End If
                    End If
                End If
            End If
            If Not Page.IsPostBack Then
                '  eaamsObj.BindDropDown(drpCityName, "CITY", True)

                '*****************Delete Functionality
                'If Request.QueryString("Action") IsNot Nothing Then
                '    If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
                '        DeleteISP(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
                '        ISPSearch(PageOperation.Search)
                '    End If
                'End If
                '*****************End of Delete 
            End If
            'If Request.QueryString("CityNmae") IsNot Nothing Then
            '    drpCityName.Enabled = False
            '    drpCityName.CssClass = "textboxgrey"
            'End If
            If hdDeleteISPID.Value <> "" Then
                DeleteISP(hdDeleteISPID.Value)
            End If




        Catch ex As Exception

        End Try
    End Sub

    Sub ISPSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISP
            'objInputXml.LoadXml("<IS_SEARCHISP_INPUT><Name></Name><CityID></CityID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHISP_INPUT>")
            objInputXml.LoadXml("<IS_SEARCHISP_INPUT><Name></Name><CityID></CityID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><ProviderID/></IS_SEARCHISP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = txtISPName.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCityName.SelectedValue
            'If drpIspProvider.SelectedValue <> "" Then
            '    objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerText = drpIspProvider.SelectedValue
            'End If

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then
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
                    ViewState("SortName") = "Name"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Name"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting



            'Here Back end Method Call
            objOutputXml = objbzISP.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvISP.DataSource = ds.Tables("ISP").DefaultView
                grdvISP.DataBind()
                'Code Added For Paging And Sorting In case Of Delete The Record

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
                hdRecordOnCurrentPage.Value = ds.Tables("ISP").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "ProviderName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdvISP.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdvISP.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select


                Select Case ViewState("SortName").ToString
                    Case "Name"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Address"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "CityName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "PinCode"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "CTCName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Phone"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Fax"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Email"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISP.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISP.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
                'lblError.Text = ""
            Else
                grdvISP.DataSource = Nothing
                grdvISP.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub DeleteISP(ByVal ispID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzIsp As New AAMS.bizISP.bzISP
            objInputXml.LoadXml("<IS_DELETEISP_INPUT><ISPID /></IS_DELETEISP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = ispID
            hdDeleteISPID.Value = ""
            'Call a function
            objOutputXml = objbzIsp.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                '@ Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
                ISPSearch(PageOperation.Search)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ISPSearch(PageOperation.Search)
    End Sub

    Protected Sub grdvISP_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvISP.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If


            '#############################################################
            ' Code added For Selecting an Items 

            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
            End If
            '#############################################################

            Dim hdISPName As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            hdISPName = e.Row.FindControl("hdIspID")

            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForstrISPId As String
            EnstrQueryStringForstrISPId = objED.Encrypt(hdISPName.Value)
            '@ End of Code Used For Encription/Decription
          
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            Dim strTest As String = Session("Security")
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                    btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdISPName.Value & "');")
                End If
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdISPName.Value & "');")
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdISPName.Value & "');")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrISPId & "');")
            Else
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdISPName.Value & "');")
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrISPId & "');")
               
                btnDelete.Enabled = True
                btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdISPName.Value & "');")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtISPName.Text = ""
            'If (Request.QueryString("CityNmae")) Is Nothing Then
            '    drpCityName.SelectedIndex = 0
            'End If

            'lblError.Text = ""
            'grdvISP.DataSource = String.Empty
            'grdvISP.DataBind()
            Response.Redirect(Request.Url.ToString())
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ISPSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ISPSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ISPSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISP_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvISP.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISP_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvISP.Sorting
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
            ISPSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ISPSearch(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Provider Name", "ISP Name", "Address", "City", "Pin", "Contact Person", "Phone", "Fax", "Email"}
        Dim intArray() As Integer = {10, 1, 2, 3, 4, 5, 6, 7, 8}

        objExport.ExportDetails(objOutputXml, "ISP", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISP.xls")
    End Sub
    'End Code For Export
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
End Class
