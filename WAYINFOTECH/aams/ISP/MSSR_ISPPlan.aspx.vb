Imports System.Data

Partial Class ISP_MSSR_ISPPlan
    Inherits System.Web.UI.Page
    Dim eaamsObj As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnNew.Attributes.Add("onclick", "return InsertISP();")
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            Dim Lcode As String
            Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
            eaamsObj.ExpirePageCache()
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", eaamsObj.CheckSession())
                Exit Sub
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
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
                eaamsObj.BindDropDown(drpIspProvider, "ISPPROVIDER", True, 3)
            End If
            If (Request.QueryString("IspName") IsNot Nothing) Then
                ' txtISPName.Enabled = True
                ' txtISPName.CssClass = "textboxgrey"
                ' txtISPName.Text = Request.QueryString("IspName").ToString()
            End If
            If Request.QueryString("Lcode") IsNot Nothing Then
                drpCityName.Enabled = False
                drpCityName.CssClass = "textboxgrey"
                Lcode = Request.QueryString("Lcode").ToString
                objInputXmlAgency.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                objInputXmlAgency.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Lcode ' Session("Action").ToString().Split("|").GetValue(1)

                'Here Back end Method Call
                objOutputXmlAgency = objbzAgency.View(objInputXmlAgency)

                If objOutputXmlAgency.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXmlAgency.DocumentElement.SelectSingleNode("Agency")
                        Dim li As New ListItem
                        li = drpCityName.Items.FindByText(.Attributes("CITY").Value())
                        If (li IsNot Nothing) Then
                            If li.Selected = False Then
                                li.Selected = True
                            End If
                        End If
                        'txtCity.Text = .Attributes("CITY").Value()
                    End With
                End If
            End If

            If Not Page.IsPostBack Then
                ' eaamsObj.BindDropDown(drpCityName, "CITY", True)
                '*****************Delete Functionality
                If Request.QueryString("Action") IsNot Nothing Then
                    'If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
                    '    DeleteISP(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
                    '    ISP_Plan_Search(PageOperation.Search)
                    '    lblError.Text = objeAAMSMessage.messDelete
                    'End If
                End If
                '*****************End of Delete 
            End If

            If Request.QueryString("IspProviderID") IsNot Nothing Then
                drpIspProvider.Enabled = False
                drpIspProvider.SelectedValue = Request.QueryString("IspProviderID")
                drpCityName.CssClass = "textboxgrey"
                drpCityName.Enabled = False
                drpCityName.CssClass = "textboxgrey"
                drpCityName.SelectedValue = ""
            End If

            If (Request.QueryString("IspId") IsNot Nothing) Then

                'Get ProviderId On the basis of IspId

                Dim objbzProvider As New AAMS.bizISP.bzProvider
                Dim objInputXmlIsp, objOutputXmlIsp As New XmlDocument
                Dim IspId As Integer = Request.QueryString("IspId").ToString()
                objInputXmlIsp.LoadXml("<IS_LISTISP_INPUT><ISPID /></IS_LISTISP_INPUT>")

                objInputXmlIsp.DocumentElement.SelectSingleNode("ISPID").InnerXml = IspId ' Session("Action").ToString().Split("|").GetValue(1)

                'Here Back end Method Call
                objOutputXmlIsp = objbzProvider.List1(objInputXmlIsp)
                If objOutputXmlIsp.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXmlIsp.DocumentElement.SelectSingleNode("ISP")
                        Dim li As New ListItem
                        li = drpIspProvider.Items.FindByValue(.Attributes("ProviderID").Value())
                        If (li IsNot Nothing) Then
                            drpIspProvider.SelectedValue = .Attributes("ProviderID").Value()
                        End If
                    End With
                End If

            End If


            If hdDeleteISPPlanID.Value <> "" Then
                DeleteISP(hdDeleteISPPlanID.Value)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ISP_Plan_Search(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub DeleteISP(ByVal strPlanID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzIspPlan As New AAMS.bizISP.bzISPPlan
            objInputXml.LoadXml("<IS_DELETEISPPLAN_INPUT><ISPPlanID /></IS_DELETEISPPLAN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ISPPlanID").InnerText = strPlanID

            hdDeleteISPPlanID.Value = ""
            'Call a function
            objOutputXml = objbzIspPlan.Delete(objInputXml)
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
                ISP_Plan_Search(PageOperation.Search)

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub ISP_Plan_Search(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPPlan
            ' objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></IS_SEARCHISPPLAN_INPUT>")
            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><ProviderID/></IS_SEARCHISPPLAN_INPUT>")
            If Request.QueryString("IspId") IsNot Nothing Then
                ' objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = Request.QueryString("IspId").ToString() 'txtISPName.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("NPID").InnerText = txtNpid.Text.Trim()
            Else
                objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = "" ' txtISPName.Text.Trim()
                ' If drpCityName.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCityName.SelectedValue
                ' Else
                ' objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = ""
                ' End If
                objInputXml.DocumentElement.SelectSingleNode("NPID").InnerText = txtNpid.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = ""
             
            End If


            If Request.QueryString("IspProviderID") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerText = Request.QueryString("IspProviderID")
            End If

            If drpIspProvider.SelectedValue <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("ProviderID").InnerText = drpIspProvider.SelectedValue
            End If
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


                '<RP_ISPPLAN_OUTPUT><ISPPLAN Name='
                ' Address='' CityName='' PinCode='' 
                'CTCName='' Phone='' Fax='' ProviderName='' NPID=''
                ' BandWidth='' ContentionRatio='' InstallationCharge=''
                ' MonthlyCharge='' EQPIncluded='' EQPOneTimeCharge=''
                ' EQPMonthlyRental='' DaysRequired='' Remarks=''
                ' TotalSum=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
                '<Errors Status=''><Error Code='' Description='' /></Errors></RP_ISPPLAN_OUTPUT>


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
                grdvISPPlan.DataSource = ds.Tables("ISPPLAN").DefaultView
                grdvISPPlan.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("ISPPLAN").Rows.Count.ToString
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
                '                grdvISPPlan.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdvISPPlan.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select

                Select Case ViewState("SortName").ToString
                    Case "Name"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Address"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "CityName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "CTCName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "Phone"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                End Select


                Select Case ViewState("SortName").ToString
                    Case "NPID"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "BandWidth"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "ContentionRatio"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "InstallationCharge"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "MonthlyCharge"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "EQPIncluded"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "EQPOneTimeCharge"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(11).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(11).Controls.Add(imgDown)
                        End Select
                End Select

                Select Case ViewState("SortName").ToString
                    Case "EQPMonthlyRental"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(12).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(12).Controls.Add(imgDown)
                        End Select
                End Select
                Select Case ViewState("SortName").ToString
                    Case "TotalSum"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(13).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(13).Controls.Add(imgDown)
                        End Select
                End Select
                Select Case ViewState("SortName").ToString
                    Case "DaysRequired"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                grdvISPPlan.HeaderRow.Cells(14).Controls.Add(imgUp)
                            Case "TRUE"
                                grdvISPPlan.HeaderRow.Cells(14).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image  '

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
                '  lblError.Text = ""
            Else
                grdvISPPlan.DataSource = Nothing
                grdvISPPlan.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

   

    Protected Sub grdvISPPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvISPPlan.RowDataBound
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
            Dim hdISPPlanID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim btnDelete As LinkButton
            btnDelete = e.Row.FindControl("btnDelete")
            hdISPPlanID = e.Row.FindControl("ISPPlanID")

            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForISPPlanID As String
            EnstrQueryStringForISPPlanID = objED.Encrypt(hdISPPlanID.Value)
            '@ End of Code Used For Encription/Decription



            If CType(e.Row.FindControl("lblEqpIncluded"), Label).Text = "True" Then
                CType(e.Row.FindControl("lblEqpIncluded"), Label).Text = "Yes"
            Else
                CType(e.Row.FindControl("lblEqpIncluded"), Label).Text = "No"
            End If

         
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    btnDelete.Enabled = False
                Else
                    btnDelete.Enabled = True
                    btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdISPPlanID.Value & "');")
                  

                End If
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForISPPlanID & "');")
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdISPPlanID.Value & "');")
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForISPPlanID & "');")
                'linkEdit.Attributes.Add("onclick", "return EditFunction('" & hdISPPlanID.Value & "');")

                btnDelete.Enabled = True
                btnDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdISPPlanID.Value & "');")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtISPName.Text = ""
            'txtNpid.Text = ""
            'drpCityName.SelectedIndex = 0
            'lblError.Text = ""
            'grdvISPPlan.DataSource = String.Empty
            'grdvISPPlan.DataBind()

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
            ISP_Plan_Search(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ISP_Plan_Search(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ISP_Plan_Search(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPPlan_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvISPPlan.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPPlan_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvISPPlan.Sorting
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
            ISP_Plan_Search(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ISP_Plan_Search(PageOperation.Export)
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
        Dim strArray() As String = {"ISP Name", "Address", "City", "Contact Person", "Contact No", "NPID", "Bandwidth", "Contention Ratio", "Installation Charge", "Monthly Charge", "Equipment Included", "Equipment One Time Charge", "Equipment Monthly Rental", "Total sum", "Delivery Time", "Remarks"}
        Dim intArray() As Integer = {0, 1, 2, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 18, 16, 17}


        '<RP_ISPPLAN_OUTPUT><ISPPLAN Name='
        ' Address='' CityName='' PinCode='' 
        'CTCName='' Phone='' Fax='' ProviderName='' NPID=''
        ' BandWidth='' ContentionRatio='' InstallationCharge=''
        ' MonthlyCharge='' EQPIncluded='' EQPOneTimeCharge=''
        ' EQPMonthlyRental='' DaysRequired='' Remarks=''
        ' TotalSum=''/><PAGE PAGE_COUNT='' TOTAL_ROWS=''/>
        '<Errors Status=''><Error Code='' Description='' /></Errors></RP_ISPPLAN_OUTPUT>


        '<ISPPLAN 
        '        ISPPlanID = "19"
        '        NPID = "INIS-0100"
        '        ISPName = "ASIANET SATELITE COMMUNICATIONS PVT LTD"
        '        BandWidth = "128k"
        '        ContentionRatio = "2:1"
        '        InstallationCharge = "0.00"
        '        MonthlyCharge = "1200.00"
        '        EQPIncluded = "False"
        '        EQPOneTimeCharge = "0.00"
        '        EQPMonthlyRental = "90.00"
        '        VATPercentage = "12.36"
        '        DaysRequired = "12"
        'Remarks="" /> 

        objExport.ExportDetails(objOutputXml, "ISPPLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPPLAN.xls")
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
