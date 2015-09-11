Imports System.Xml
Imports System.Data
Partial Class TravelAgency_TASR_OnlineStatus
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
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
        Try
            Session("PageName") = Request.Url.ToString() '"TravelAgency_TASR_OnlineStatus"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            ' btnReset.Attributes.Add("onclick", "return OnlineStatusReset();")
            btnSearch.Attributes.Add("onclick", "return OnlineStatusMandatory();")
            btnExport.Attributes.Add("onclick", "return OnlineStatusMandatory();")

            btnNew.Attributes.Add("onclick", "return NewMSUPOnlineStatus();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For                
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        txtStatusCode.Text = Session("Act").ToString().Split("|").GetValue(2)
                        txtOnlineStatus.Text = Session("Act").ToString().Split("|").GetValue(3)
                        SearchOnlineStatus(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If

                '*******************************************************************
                ' Load Data For Editing Mode
            End If
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    OnlineStatusDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If


            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)

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
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchOnlineStatus(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub SearchOnlineStatus(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
            objInputXml.LoadXml("<MS_SEARCHONLINESTATUS_INPUT><OnlineStatus></OnlineStatus><StatusCode></StatusCode><SegExpected></SegExpected><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHONLINESTATUS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = txtStatusCode.Text
            objInputXml.DocumentElement.SelectSingleNode("OnlineStatus").InnerText = txtOnlineStatus.Text
            objInputXml.DocumentElement.SelectSingleNode("SegExpected").InnerText = txtSegExpected.Text
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
                    ViewState("SortName") = "StatusCode"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "StatusCode" '"LOCATION_CODE"
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
            objOutputXml = objbzOnlineStatus.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvOnlineStatus.DataSource = ds.Tables("Status")
                gvOnlineStatus.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("Status").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "StatusCode"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "OnlineStatus"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "SegExpected"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select

                    Case "UnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select

                    Case "NPUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select
                    Case "LKUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select
                    Case "BDUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                    Case "BTUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                    Case "MLUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvOnlineStatus.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                gvOnlineStatus.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select

                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvOnlineStatus.DataSource = Nothing
                gvOnlineStatus.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub OnlineStatusDelete(ByVal strStatusCode As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
            objInputXml.LoadXml("<MS_DELETEONLINESTATUS_INPUT><StatusCode /></MS_DELETEONLINESTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = strStatusCode
            'Here Back end Method Call
            objOutputXml = objbzOnlineStatus.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                Response.Redirect("TASR_OnlineStatus.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOnlineStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOnlineStatus.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdStatusCode As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdStatusCode = e.Row.FindControl("hdStatusCode")
            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Disabled = True
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    linkEdit.Disabled = True
                    'Else
                    '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdStatusCode.Value & "'" & ");")
                    'End If
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objED.Encrypt(hdStatusCode.Value) & "'" & ");")
                End If
            Else
                linkDelete.Disabled = False
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & objED.Encrypt(hdStatusCode.Value) & "'" & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOnlineStatus_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvOnlineStatus.RowCommand
        'Try
        '    'Code for Edit Data
        '    'If e.CommandName = "Editx" Then
        '    '    Response.Redirect("MSUP_ManageAgencyGroup.aspx?Action=U&Chain_Code=" & e.CommandArgument)
        '    'End If
        '    'Code for Delete Date
        '    If e.CommandName = "Deletex" Then
        '        OnlineStatusDelete(e.CommandArgument)
        '        SearchAgencyGroup()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchOnlineStatus(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchOnlineStatus(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchOnlineStatus(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvOnlineStatus_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvOnlineStatus.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOnlineStatus_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvOnlineStatus.Sorting
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
            SearchOnlineStatus(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchOnlineStatus(PageOperation.Export)
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
        'Dim strArray() As String = {"Status Code", "Online Status", "Segment Expected", "Unit Cost"}
        'Dim intArray() As Integer = {1, 0, 2, 3}
        Dim strArray() As String = {"Status Code", "Online Status", "Segment Expected", "Unit Cost (IN)", "Unit Cost(LK)", "Unit Cost(LK)", "Unit Cost(BD)", "Unit Cost(BT)", "Unit Cost(ML)"}
        Dim intArray() As Integer = {1, 0, 2, 3, 4, 5, 6, 7, 8}

        objExport.ExportDetails(objOutputXml, "Status", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportONLINESTATUS.xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString())
    End Sub
End Class
