
Partial Class TravelAgency_TASR_TravelAgencySalesStaff
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt


#Region "Code for Filter "

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

#End Region

#Region "Search Button Code"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            StaffSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Staff Search Function"

    Private Sub StaffSearch(ByVal Operation As Integer)
        Dim objSecurityXml As New XmlDocument
        Try
            If (txtStaffName.Text = String.Empty) And (txtAgencyName.Text = String.Empty) And (txtOfficeId.Text = String.Empty) And (TxtSignInNum.Text = String.Empty) Then
                lblError.Text = "Please Enter either StaffName or AgencyName or OfficeId or SignIn ID."
                Exit Sub
            End If

            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim dSet As New DataSet
            Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            ' objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></TA_SEARCHSTAFF_INPUT>")

            objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><EmployeeID/><SIGNINID></SIGNINID><LCODE></LCODE></TA_SEARCHSTAFF_INPUT>")

            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If

            Try
                If Request.QueryString("Lcode") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Request.QueryString("Lcode").Trim 'objED.Decrypt()
                End If
            Catch ex As Exception

            End Try

            objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = txtStaffName.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeId.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("SIGNINID").InnerText = TxtSignInNum.Text + TxtSignInChar.Text

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
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
                        '  objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                        If Not Session("LoginSession") Is Nothing Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = Session("LoginSession").ToString().Split("|")(0)
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                End If

                'End If

            End If



            If Operation = PageOperation.Search Then
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
                    ViewState("SortName") = "STAFFNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "STAFFNAME" '"LOCATION_CODE"
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

            End If

            objOutputXml = objbzAgencyStaff.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)

                dSet.ReadXml(objXmlReader)
                grdStaff.DataSource = dSet.Tables("STAFF")
                grdStaff.DataBind()
                lblError.Text = String.Empty


                ' ###################################################################
                '@ Code Added For Paging And Sorting In case Of Delete The Record
                ' ###################################################################
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

                txtRecordOnCurrentPage.Text = dSet.Tables("STAFF").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                '' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                SetImageForSorting(grdStaff)
                '' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName").ToString()

                '    Case "STAFFNAME"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select

                '    Case "DESIGNATION"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select


                '    Case "AgencyName"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select

                '    Case "Address"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(3).Controls.Add(imgDown)

                '        End Select

                '    Case "Address1"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(4).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(4).Controls.Add(imgDown)
                '        End Select

                '    Case "City"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(5).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(5).Controls.Add(imgDown)

                '        End Select

                '    Case "Country"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(6).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(6).Controls.Add(imgDown)

                '        End Select

                '    Case "Phone"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(7).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(7).Controls.Add(imgDown)
                '        End Select

                '    Case "Fax"
                '        Select Case ViewState("Desc").ToString()
                '            Case "FALSE"
                '                grdStaff.HeaderRow.Cells(8).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdStaff.HeaderRow.Cells(8).Controls.Add(imgDown)

                '        End Select
                'End Select


            Else
                txtTotalRecordCount.Text = "0"
                txtRecordOnCurrentPage.Text = "0"
                pnlPaging.Visible = False

                grdStaff.DataSource = String.Empty
                grdStaff.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    '   Private Sub StaffSearch()
    '       Try
    '           If (txtStaffName.Text = String.Empty) And (txtAgencyName.Text = String.Empty) And (txtOfficeId.Text = String.Empty) Then
    '               lblError.Text = "Please Enter either StaffName or AgencyName or OfficeId."
    '               Exit Sub
    '           End If
    '           Dim objInputXml, objOutputXml As New XmlDocument
    '           Dim objXmlReader As XmlNodeReader
    '           Dim dSet As New DataSet
    '           Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzAgencyStaff
    '           objInputXml.LoadXml("<TA_SEARCHSTAFF_INPUT><STAFFNAME></STAFFNAME><AGENCYNAME></AGENCYNAME><OFFICEID/></TA_SEARCHSTAFF_INPUT>")
    '           objInputXml.DocumentElement.SelectSingleNode("STAFFNAME").InnerText = txtStaffName.Text.Trim()
    '           objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text.Trim()
    'objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = txtOfficeId.Text.Trim()
    '           objOutputXml = objbzAgencyStaff.Search(objInputXml)

    '           If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '               objXmlReader = New XmlNodeReader(objOutputXml)
    '               dSet.ReadXml(objXmlReader)
    '               grdStaff.DataSource = dSet.Tables("STAFF")
    '               grdStaff.DataBind()
    '               lblError.Text = String.Empty
    '           Else
    '               grdStaff.DataSource = String.Empty
    '               grdStaff.DataBind()
    '               lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '           End If
    '       Catch ex As Exception
    '           lblError.Text = ex.Message
    '       End Try
    '   End Sub


#End Region
#Region "Page Load Function"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            '  btnNew.Attributes.Add("onclick", "return NewFunction();")
            btnSearch.Attributes.Add("onclick", "return ValidateForm();")

            'If Not Request.QueryString("AgencyName") Is Nothing Then
            '    txtAgencyName.Text = Request.QueryString("AgencyName").ToString
            'End If



            TxtSignInNum.Attributes.Add("onkeyup", "next('TxtSignInNum','4','TxtSignInChar')")

            If Not Page.IsPostBack Then



                If Not Request.QueryString("AgencyName") Is Nothing Then
                    txtAgencyName.Text = Request.QueryString("AgencyName").ToString
                End If
                If Not Request.QueryString("Source") Is Nothing Then
                    hdPageSource.Value = Request.QueryString("Source").ToString
                    If txtAgencyName.Text.Trim.Length > 0 Then
                        StaffSearch(PageOperation.Search)
                    End If

                End If
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtStaffName.Text = Session("Action").ToString().Split("|").GetValue(2)
                        txtAgencyName.Text = Session("Action").ToString().Split("|").GetValue(3)
                        txtOfficeId.Text = Session("Action").ToString.Split("|").GetValue(4)
                        StaffSearch(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If

            'If (Not Request.QueryString("Action") = Nothing) Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        ' ViewIPPoolGroup method  called for binding the controls  
            '        StaffDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))

            '    End If
            'End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '   Deleting records.
            If (hdStaffID.Value <> "") Then
                StaffDelete(hdStaffID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "grdStaff_RowDataBound"
    Protected Sub grdStaff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdStaff.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdStaffID1 As HiddenField
            Dim HdSigninId As HiddenField
            Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            lnkEdit = e.Row.FindControl("linkEdit")
            Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            lnkDelete = e.Row.FindControl("linkDelete")
            hdStaffID1 = e.Row.FindControl("hdStaffID")
            HdSigninId = e.Row.FindControl("HdSigninId")

            Dim FName As String = DataBinder.Eval(e.Row.DataItem, "FIRSTNAME")
            Dim MName As String = DataBinder.Eval(e.Row.DataItem, "MIDDLENAME")
            Dim SurName As String = DataBinder.Eval(e.Row.DataItem, "SURNAME")
            Dim Designation As String = DataBinder.Eval(e.Row.DataItem, "DESIGNATION")



            'Code by Rakesh 29 Feb 08 For pop up
            Dim lnkSelect As LinkButton
            Dim hdSelect As HtmlInputHidden
            hdSelect = (CType(e.Row.FindControl("hdSelect"), HtmlInputHidden))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                Dim LCode As String = DataBinder.Eval(e.Row.DataItem, "LCODE")
                Dim strAgencyName As String = DataBinder.Eval(e.Row.DataItem, "AgencyName")
                strAgencyName = strAgencyName.Replace(vbCrLf, "\n")

                strAgencyName = strAgencyName.Replace("'", "")

                lnkSelect.Attributes.Add("onclick", "return SelectFunction('" & lnkSelect.CommandArgument & "|" & LCode & "|" & strAgencyName & "|" & HdSigninId.Value & "','" + FName + "','" + MName + "','" + SurName + "','" + Designation + "');")
            End If
            'Code end

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Staff']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    lnkDelete.Disabled = True
                Else
                    lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdStaffID1.Value & ");")
                End If
                'If strBuilder(2) = "0" Then
                '    lnkEdit.Disabled = True
                'Else
                '    lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdStaffID1.Value & ");")
                'End If
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdStaffID1.Value) & "');")
            Else
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdStaffID1.Value) & "');")
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdStaffID1.Value & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "StaffDelete"
    Sub StaffDelete(ByVal AgencyStaffID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzStaff As New AAMS.bizTravelAgency.bzAgencyStaff
            objInputXml.LoadXml("<MS_DELETEAGENCYSTAFFDETAILS_INPUT><AGENCYSTAFFID></AGENCYSTAFFID></MS_DELETEAGENCYSTAFFDETAILS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFFID").InnerXml = AgencyStaffID
            'Here Back end Method Call
            hdStaffID.Value = ""
            objOutputXml = objbzStaff.Delete(objInputXml)
            StaffSearch(PageOperation.Search)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = Session("Action")
                ' Session("Action") = Request.QueryString("Action")
                'Response.Redirect("TASR_AgencyStaff.aspx", False)
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Dim strurl As String = "TAUP_TravelAgencySalesStaff.aspx"
            Dim Param As String = "?Action=I"

            If Not Request.QueryString("Source") Is Nothing Then
                hdPageSource.Value = Request.QueryString("Source").ToString
                Param = Param + "&Source=" + hdPageSource.Value
            End If

            If Not Request.QueryString("Lcode") Is Nothing Then
                Param = Param + "&Lcode=" + Request.QueryString("Lcode")
            End If

            If Not Request.QueryString("AgencyName") Is Nothing Then
                Param = Param + "&AgencyName=" + Request.QueryString("AgencyName")
            End If

            strurl = strurl + Param

            Response.Redirect(strurl, False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            StaffSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StaffSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StaffSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdStaff_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdStaff.Sorting
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
            StaffSearch(PageOperation.Search)
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            StaffSearch(PageOperation.Export)
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
        'Dim strArray() As String = {"Name", "Designation", "Agency Name", "Address", "Address1", "City", "Country", "Phone", "Fax"}
        'Dim intArray() As Integer = {1, 10, 3, 4, 5, 6, 7, 8, 9}
        '  <STAFF AGENCYSTAFFID="11480831" LCODE="39465" AGENCYNAME="Freelance Journeys" ADDRESS="H.O.-24, Laxmi Palace" ADDRESS1="Phase-1, Deori Road" CITY="Agra" COUNTRY="India" PHONE="" FAX="" SIGNINID="7777wr" STAFFNAME="Madan Rawat" DESIGNATION="HD Executive" EMAIL="freelancejourneys@gmail.com" MOBILENO="9999999787" MARTIALSTATUS="" FIRSTNAME="Madan" MIDDLENAME="" SURNAME="Rawat" />

        Dim strArray() As String = {"Sign In", "Name", "Designation", "Email", "Mobile No."}
        Dim intArray() As Integer = {9, 10, 11, 12, 13}
        objExport.ExportDetails(objOutputXml, "STAFF", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAgencyStaff.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString())
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
End Class
