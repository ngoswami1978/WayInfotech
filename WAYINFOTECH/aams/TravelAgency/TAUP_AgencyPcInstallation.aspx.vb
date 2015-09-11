'###########################################################################
'############   Page Name -- TravelAgency_TAUP_AgencyPcInstallation  #######
'############   Date 11-December 2007  ######################################
'############   Developed By Abhishek  #####################################
'###########################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region

Partial Class TravelAgency_TAUP_AgencyPcInstallation
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim Lcode As String
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
            lblError.Text = ""
            txt1ATotal.Text = "0"
            txtAgencyTotal.Text = "0"
            Session("PageName") = Request.Url.ToString() '"TravelAgency_TAUP_AgencyPcInstallation"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If (Session("Action") IsNot Nothing) Then
                If (Session("Action").ToString().Split("|").Length >= 2) Then
                    Lcode = Session("Action").ToString().Split("|").GetValue(1)
                Else
                    lblError.Text = "Lcode is not exist."
                    'btnNew.Enabled = False
                    Exit Sub
                End If
            Else
                lblError.Text = "Lcode is not exist."
                ' btnNew.Enabled = False
                Exit Sub
            End If
            btnNewAgency.Attributes.Add("onclick", "return NewAgencyPCInstall();")
            btnNew1APc.Attributes.Add("onclick", "return New1AAgencyPCInstall();")
            'btnNew.Attributes.Add("onclick", "return TAUPAgencyPcInstallation();")
            ' #######################################
            ' ########## This code is     used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        'btnSearch.Enabled = False
                        ' btnDeinstallPC.Disabled = True
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew1APc.Enabled = False
                        btnNewAgency.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnDeinstallPC.Disabled = True
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights

            ' ************************************
            ' Code for Deletion of Pc Installation 
            If hdRowId.Value <> "" Then
                PCInstallationDelete(hdRowId.Value)
            End If
            ' ************************************
            If strBuilder(0) = "1" Then
                GetAgencyPcDetails()
            End If
            If Not Page.IsPostBack Then
                '*******************************************************************

                ''Code For  Updating of Agency Pc Installation    
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        If (Not Request.QueryString("ROWID") = Nothing) Then
                            ' ViewAgencyPCInstallation method  called for binding the controls  
                            ViewAgencyPCInstallation()
                        End If
                    End If
                End If
                '*******************************************************************             

                If strBuilder(0) = "1" Then
                    GetAgencyPcDetails()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewAgencyPCInstallation"
    Sub ViewAgencyPCInstallation()

    End Sub

#End Region
#Region " GetAgencyPcDetails Procedure"
    Private Sub GetAgencyPcDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '  Dim objNode As XmlNode
        Try
            If (Session("Action") IsNot Nothing) Then
                Lcode = Session("Action").ToString().Split("|").GetValue(1)

                Dim objbzAgencyPcInstall As New AAMS.bizTravelAgency.bzAgency
                '<PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
                'objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.LoadXml("<MS_GETPCINSTALLATION_INPUT><LCODE></LCODE><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_GETPCINSTALLATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = Lcode

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
                objOutputXml = objbzAgencyPcInstall.GetInstalledPC(objInputXml)

                'For Each objNode In objOutputXml.DocumentElement.SelectNodes("PCINSTALLATION")
                '    objNode.Attributes("DATE").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("DATE").Value)
                '    objNode.Attributes("LoggedDateTime").Value = objeAAMS.ConvertDateBlank(objNode.Attributes("LoggedDateTime").Value)
                'Next
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                    ViewState("PrevSearching") = objInputXml.OuterXml

                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvPcInstallation.DataSource = ds.Tables("PCINSTALLATION")
                    gvPcInstallation.DataBind()
                    If objOutputXml.DocumentElement.SelectSingleNode("TOTAL") IsNot Nothing Then
                        txt1ATotal.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("A1PC").Value
                        txtAgencyTotal.Text = objOutputXml.DocumentElement.SelectSingleNode("TOTAL").Attributes("AGENCYPC").Value
                    End If


                    ' ##################################################################
                    '@ Code Added For Paging And Sorting In case Of 
                    ' ###################################################################
                    ' BindControlsForNavigation(2)
                    BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                    SetImageForSorting(gvPcInstallation)
                    ' ###################################################################
                    '@ End of Code Added For Paging And Sorting In case 
                    ' ###################################################################

                    pnlPaging.Visible = True

                Else
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvPcInstallation.DataSource = ds.Tables("PCINSTALLATION")
                    gvPcInstallation.DataBind()
                    txt1ATotal.Text = "0"
                    txtAgencyTotal.Text = "0"
                    pnlPaging.Visible = False
                    txtTotalRecordCount.Text = "0"
                    'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            Else
                lblError.Text = "Lcode is not exist."
                pnlPaging.Visible = False
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False
        End Try
    End Sub
#End Region

    Protected Sub gvPcInstallation_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPcInstallation.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            Dim hdstrRowId, hdPcType, hdCpuType, hdCpuNo, hdMonType, hdMonNo As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As LinkButton
            linkDelete = e.Row.FindControl("linkDelete")
            Dim linkReplace As System.Web.UI.HtmlControls.HtmlAnchor
            linkReplace = e.Row.FindControl("linkReplace")
            Dim linkHistory As System.Web.UI.HtmlControls.HtmlAnchor
            linkHistory = e.Row.FindControl("linkHistory")
            Dim linkDeinstall As System.Web.UI.HtmlControls.HtmlAnchor
            linkDeinstall = e.Row.FindControl("linkDeinstall")


            hdstrRowId = e.Row.FindControl("hdRowId")
            hdPcType = e.Row.FindControl("hdPcType")
            hdCpuType = e.Row.FindControl("hdCpuType")
            hdCpuNo = e.Row.FindControl("hdCpuNo")
            hdMonType = e.Row.FindControl("hdMonType")
            hdMonNo = e.Row.FindControl("hdMonNo")


            If (hdCpuType.Value = "CPP" Or hdCpuType.Value = "CPL") And ((hdCpuNo.Value = "NA" Or hdCpuNo.Value = "N/A") And hdMonNo.Value = "NA") Then
                hdPcType.Value = 0
            Else
                hdPcType.Value = 1
            End If

            'If hdCpuType.Value = "CPP" Then 'And hdMonType.Value = "MMP" Then
            '    hdPcType.Value = 0
            'Else
            '    hdPcType.Value = 1
            'End If

            If (hdstrRowId.Value = "") Then
                e.Row.Cells.Clear()
                Exit Sub
            End If
            ''linkEdit.Attributes.Add("onclick", "return EditFunctionPCInstall(" & "'" & hdstrPriorityId.Value & "'" & ");")
            ''linkDelete.Attributes.Add("onclick", "return DeleteFunctionPCInstall(" & "'" & hdstrPriorityId.Value & "'" & ");")

            '@ For Installation
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Installation']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Enabled = False
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunctionPCInstall(" & "'" & hdstrRowId.Value & "'" & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    '    'linkReplace.Disabled = True
                    '    ' linkHistory.Disabled = True
                    '    'linkDeinstall.Disabled = True
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                    '    'linkReplace.Attributes.Add("onclick", "return ReplaceFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                    '    'linkHistory.Attributes.Add("onclick", "return PCInstallHistoryPCInstall(" & "'" & hdstrRowId.Value & "'" & ");")
                    '    'linkDeinstall.Attributes.Add("onclick", "return PCDeInstallPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")


                    'End If
                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                    End If


                Else

                    If strBuilder(2) = "0" Then
                        linkEdit.Disabled = True
                    Else
                        linkEdit.Attributes.Add("onclick", "return EditFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                    End If
                    '  

                    linkDelete.Enabled = False

                End If
            Else
                linkDelete.Enabled = True
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunctionPCInstall(" & "'" & hdstrRowId.Value & "'" & ");")

                'linkReplace.Attributes.Add("onclick", "return ReplaceFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                ' linkHistory.Attributes.Add("onclick", "return PCInstallHistoryPCInstall(" & "'" & hdstrRowId.Value & "'" & ");")
                ' linkDeinstall.Attributes.Add("onclick", "return PCDeInstallPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")

            End If

            '@ For Deinstallation
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Deinstallation']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        linkDeinstall.Disabled = True
                    Else
                        linkDeinstall.Attributes.Add("onclick", "return PCDeInstallPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")

                    End If
                Else
                    linkDeinstall.Disabled = True
                End If
            Else
                linkDeinstall.Attributes.Add("onclick", "return PCDeInstallPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
            End If

            '@ For Replacement
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Replacement']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PC Replacement']").Attributes("Value").Value)
                    If strBuilder(2) = "0" Then
                        linkReplace.Disabled = True
                    Else
                        linkReplace.Attributes.Add("onclick", "return ReplaceFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
                    End If
                Else
                    linkReplace.Disabled = True
                End If
            Else
                linkReplace.Attributes.Add("onclick", "return ReplaceFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
            End If

            ' ####################################  

            ' @  Apply other Security  

            'linkReplace.Attributes.Add("onclick", "return ReplaceFunctionPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")
            linkHistory.Attributes.Add("onclick", "return PCInstallHistoryPCInstall(" & "'" & hdstrRowId.Value & "'" & ");")
            'linkDeinstall.Attributes.Add("onclick", "return PCDeInstallPCInstall(" & "'" & hdstrRowId.Value & "','" & hdPcType.Value & "'" & ");")



            ' ####################################  




        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region " Sub Procedure Called for deletion of PC Installation"
    Sub PCInstallationDelete(ByVal strRowID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzPCInstallation As New AAMS.bizTravelAgency.bzPCInstallation
            objInputXml.LoadXml("<UP_TA_DELETE_PCINSTALLATION_INPUT><ROWID /></UP_TA_DELETE_PCINSTALLATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = strRowID
            'Here Back end Method Call
            hdRowId.Value = ""
            objOutputXml = objbzPCInstallation.Delete(objInputXml)
            objOutputXml.LoadXml("<UP_TA_DELETE_PCINSTALLATION_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors></UP_TA_DELETE_PCINSTALLATION_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                GetAgencyPcDetails()
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            GetAgencyPcDetails()
            'If Request.QueryString("EditOrderId") IsNot Nothing Then
            '    AgencyOrderEdit(Request.QueryString("ROWID").ToString())
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            GetAgencyPcDetails()
            'If Request.QueryString("EditOrderId") IsNot Nothing Then
            '    AgencyOrderEdit(Request.QueryString("ROWID").ToString())
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            GetAgencyPcDetails()
            'If Request.QueryString("EditOrderId") IsNot Nothing Then
            '    AgencyOrderEdit(Request.QueryString("ROWID").ToString())
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPcInstallation_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPcInstallation.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvPcInstallation_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPcInstallation.Sorting
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
            GetAgencyPcDetails()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
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

#End Region
End Class
