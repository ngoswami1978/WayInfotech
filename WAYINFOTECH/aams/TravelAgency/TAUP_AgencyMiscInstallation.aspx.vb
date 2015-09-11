
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_TAUP_AgencyMiscInstallation
    Inherits System.Web.UI.Page

#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
    Dim imgDown As New Image
    Dim imgUp As New Image
#End Region

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

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnNew.Attributes.Add("onclick", "return InstallNewHardwareAgencyMis();")

            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If hdAdd.Value.Trim() = "1" Then
                lblError.Text = objeAAMSMessage.messInsert
                Session("MSG") = Nothing

                ' hdAdd.Value = ""
            End If

            If hdUpReplace.Value.Trim() = "1" Then
                lblError.Text = objeAAMSMessage.messUpdate
                '  hdUpReplace.Value = ""
            End If

            If hdUpReplace.Value.Trim() = "2" Then
                lblError.Text = "Hardware Replaced Successfully"
                ' hdUpReplace.Value = ""
            End If


            If hdDeinstalled.Value = "1" Then
                lblError.Text = "Hardware Deinstalled Successfully"
                ' hdDeinstalled.Value = ""
            End If

            Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyMiscInstallation"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    ' btnNew.Enabled = False
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                'btnNew.Enabled = False
                Exit Sub
            End If
            If hdDel.Value.Trim().Length <> 0 Then
                DeleteMisHardware()
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Misc Hardware']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Misc Hardware']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If
                    If strBuilder(1) = "0" Then
                        'btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnDeinstallMisc.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            If Not Page.IsPostBack Then
                '*******************************************************************
                'If (Request.QueryString("MSG") = "A") Then
                '    lblError.Text = objeAAMSMessage.messInsert
                'ElseIf (Request.QueryString("MSG") = "U") Then
                '    lblError.Text = objeAAMSMessage.messUpdate
                'End If
                ''Code For  Updating of Agency Pc Installation  
                'ViewAgencyMiscInstallation()

                'If (Not Request.QueryString("Action") = Nothing) Then
                '    If (Request.QueryString("Action").ToUpper = "U") Then
                '        If (Not Request.QueryString("ROWID") = Nothing) Then
                '            ' ViewIPPoolGroup method  called for binding the controls  
                '            ' ViewAgencyMiscInstallation()
                '        End If
                '    End If
                ' End If
                '*******************************************************************
                ' Load Data For Editing Mode

                ''Code For  Deletion of Agency Staff    

                'If Not Session("Act") Is Nothing Then
                '    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                '        lblError.Text = objeAAMSMessage.messDelete
                '        Session("Act") = Nothing
                '    End If
                'End If

                If strBuilder(0) = "1" Then
                    GetAgencyMiscInstallationDetails()
                End If

            End If



            Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
            If txtRecordOnCurrReco.Text = "1" Then
                If CurrentPage - 1 > 0 Then
                    ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                Else
                    ddlPageNumber.SelectedValue = "1"
                End If
            End If

            GetAgencyMiscInstallationDetails()

            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        ' code for getting the record for Deleting
            '    End If
            'End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region " GetAgencyMiscInstallationDetails Procedure"
    Private Sub GetAgencyMiscInstallationDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objNode As XmlNode
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyStaff As New AAMS.bizTravelAgency.bzMiscInstallation 

                objInputXml.LoadXml("<MS_GETMISCINSTALLATION_INPUT><LCode></LCode> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_GETMISCINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCode").InnerText = Lcode ' "229"

                'Start CODE for sorting and paging

                If ViewState("PrevSearching") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objTempInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode1 As XmlNode In objNodeList
                        If objNode1.Name <> "PAGE_NO" And objNode1.Name <> "SORT_BY" And objNode1.Name <> "DESC" And objNode1.Name <> "PAGE_SIZE" Then
                            If objNode1.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode1.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                End If


                objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString


                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "DATE"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "DATE" '"LOCATION_CODE"
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

                'Here Back end Method Call
                objOutputXml = objbzAgencyStaff.GetInstalledMiscHW(objInputXml)

                For Each objNode In objOutputXml.DocumentElement.SelectNodes("GETMISCINSTALLATION")
                    objNode.Attributes("DATE").Value = objNode.Attributes("DATE").Value.Trim()
                    objNode.Attributes("LoggedDateTime").Value = objNode.Attributes("LoggedDateTime").Value.Trim()
                Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                   
                    txtRecordOnCurrReco.Text = gvMiscInstallation.Rows.Count


                    ViewState("PrevSearching") = objInputXml.OuterXml

                    If hdDel.Value.Trim().Length <> 0 Then
                        hdDel.Value = ""
                    ElseIf hdUpReplace.Value.Trim() = "1" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                        hdUpReplace.Value = ""
                    ElseIf hdUpReplace.Value.Trim() = "2" Then
                        lblError.Text = "Hardware Replaced Successfully"
                        hdUpReplace.Value = ""
                    ElseIf hdDeinstalled.Value = "1" Then
                        lblError.Text = "Hardware Deinstalled Successfully"
                        hdDeinstalled.Value = ""
                    Else
                        lblError.Text = ""
                    End If

                    'If hdUpReplace.Value.Trim() = "1" Then
                    '    lblError.Text = objeAAMSMessage.messUpdate
                    '    hdUpReplace.Value = ""
                    'End If

                    'If hdUpReplace.Value.Trim() = "2" Then
                    '    lblError.Text = "Hardware Replaced Successfully"
                    '    hdUpReplace.Value = ""
                    'End If

                    gvMiscInstallation.DataSource = ds.Tables("GETMISCINSTALLATION")
                    gvMiscInstallation.DataBind()

                    txtRecordOnCurrReco.Text = gvMiscInstallation.Rows.Count.ToString()
                    PagingCommon(objOutputXml)

                    Dim intcol As Integer = GetSortColumnIndex(gvMiscInstallation)
                    If ViewState("Desc") = "FALSE" Then
                        gvMiscInstallation.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                    End If
                    If ViewState("Desc") = "TRUE" Then
                        gvMiscInstallation.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                    End If


                    txtTotalMis.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("MISCPC").Value.Trim()
                Else
                    'objOutputXml.LoadXml("<UP_GETAGENCYSTAFFDETAILS_OUTPUT><AGENCYSTAFF LCODE='' AGENCYSTAFFID='-1' STAFFNAME='' DESIGNATION='' /><Errors Status='FALSE'><Error Code='' Description='' /></Errors></UP_GETAGENCYSTAFFDETAILS_OUTPUT>")
                    'objXmlReader = New XmlNodeReader(objOutputXml)
                    'ds.ReadXml(objXmlReader)
                    'gvMiscInstallation.DataSource = String.Empty
                    'gvMiscInstallation.DataBind()
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvMiscInstallation.DataSource = ds.Tables("GETMISCINSTALLATION")
                    gvMiscInstallation.DataBind()
                    txtTotalMis.Text = "0"
                    txtTotalRecordCount.Text = "0"
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                lblError.Text = "Lcode is not exist."
            End If

        Catch ex As Exception
            lblError.Text = ex.Message

        End Try
    End Sub
#End Region

   
#Region "gvMiscInstallation_RowDataBound"
    Protected Sub gvMiscInstallation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMiscInstallation.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdstrRowId As HiddenField

            Dim linkEdit As LinkButton
            linkEdit = e.Row.FindControl("linkEdit")

            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")

            Dim linkHis As LinkButton
            linkHis = e.Row.FindControl("linkHistory")

            Dim linkDeins As LinkButton
            linkDeins = e.Row.FindControl("linkDeinstall")

            Dim linkReplace As LinkButton
            linkReplace = e.Row.FindControl("linkReplace")

            hdstrRowId = e.Row.FindControl("hdRowId")
            If (hdstrRowId.Value = "") Then
                e.Row.Cells.Clear()
                Exit Sub
            End If
            ''linkEdit.Attributes.Add("onclick", "return EditFunctionAgencyMis((" & "'" & hdstrPriorityId.Value & "'" & ");")
            ''linkDelete.Attributes.Add("onclick", "return DeleteFunctionAgencyMis(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))



            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscReplacement']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscReplacement']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        linkReplace.Enabled = False
                    Else
                        linkReplace.Attributes.Add("onclick", "return ReplaceAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkReplace.Enabled = True
                linkReplace.Attributes.Add("onclick", "return ReplaceAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
            End If


            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MiscDeinstallation']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        linkDeins.Enabled = False
                    Else
                        linkDeins.Attributes.Add("onclick", "return DeinstallAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDeins.Enabled = True
                linkDeins.Attributes.Add("onclick", "return DeinstallAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
            End If



            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Misc Hardware']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Misc Hardware']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If

                    If strBuilder(0) = "0" Then
                        linkHis.Enabled = False
                    Else
                        linkHis.Attributes.Add("onclick", "return ShowHistoryAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If



                    If strBuilder(2) = "0" Then
                        linkEdit.Enabled = False
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunctionAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If
                Else
                    ' linkDelete.Disabled = True
                    ' linkEdit.Disabled = True
                    'linkEdit.Attributes.Add("onclick", "return EditFunctionAgencyMis((" & "'" & hdstrRowId.Value & "'" & ");")
                    ' linkDelete.Attributes.Add("onclick", "return DeleteFunctionAgencyMis((" & "'" & hdstrRowId.Value & "'" & ");")

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                linkDelete.Enabled = True
                linkEdit.Enabled = True
                linkReplace.Enabled = True
                linkEdit.Attributes.Add("onclick", "return EditFunctionAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                linkDeins.Attributes.Add("onclick", "return DeinstallAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                linkReplace.Attributes.Add("onclick", "return ReplaceAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
                linkHis.Attributes.Add("onclick", "return ShowHistoryAgencyMis(" & "'" & hdstrRowId.Value & "'" & ");")
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub DeleteMisHardware()
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objhdOrderDel As New AAMS.bizTravelAgency.bzMiscInstallation

            objInputXml.LoadXml("<TA_DELETEMISCINSTALLATION_INPUT><ROWID /></TA_DELETEMISCINSTALLATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = hdDel.Value.Trim()
            'hdDel.Value = ""
            'Call a function
            objOutputXml = objhdOrderDel.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#End Region

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

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
        GetAgencyMiscInstallationDetails()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetAgencyMiscInstallationDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvMiscInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvMiscInstallation.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return -1
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
End Class
