'#####################################################################
'############   Page Name -- TravelAgency_TASR_AgencyPriority  #######
'############   Date 3-December 2007  ################################
'############   Developed By Abhishek  ###############################
'#####################################################################
Imports System.Xml
Imports System.Data
Partial Class TravelAgency_TASR_AgencyPriority
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString() '"TravelAgency_TASR_AgencyPriority"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            ' btnReset.Attributes.Add("onclick", "return AgencyPriorityReset();")
            btnSearch.Attributes.Add("onclick", "return AgencyPriorityMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPAgencyPriority();")

            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Attributes("Value").Value)

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

            If Not Page.IsPostBack Then

                '       '*******************************************************************
                ''Code For                
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        txtPriority.Text = Session("Act").ToString().Split("|").GetValue(2)
                        SearchAgencyPriority(PageOperation.Search)
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If

                '*******************************************************************
                ' Load Data For Editing Mode
            End If

            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    AgencyPriorityDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If


           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchAgencyPriority(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub SearchAgencyPriority(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAgencyPriority As New AAMS.bizTravelAgency.bzAgencyPriority
            objInputXml.LoadXml("<MS_SEARCHAGENCYPRIORITY_INPUT><PRIORITYNAME></PRIORITYNAME><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAGENCYPRIORITY_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("PRIORITYNAME").InnerText = txtPriority.Text

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
                    ViewState("SortName") = "PRIORITYNAME"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "PRIORITYNAME" '"LOCATION_CODE"
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
            objOutputXml = objbzAgencyPriority.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvAgencyPriority.DataSource = ds.Tables("PRIORITY")
                gvAgencyPriority.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("PRIORITY").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "PRIORITYNAME"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvAgencyPriority.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvAgencyPriority.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select

                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvAgencyPriority.DataSource = Nothing
                gvAgencyPriority.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub AgencyPriorityDelete(ByVal strPriorityId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzAgencyPriority As New AAMS.bizTravelAgency.bzAgencyPriority
            objInputXml.LoadXml("<MS_DELETEAGENCYPRIORITY_INPUT><PRIORITYID></PRIORITYID></MS_DELETEAGENCYPRIORITY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = strPriorityId
            'Here Back end Method Call
            objOutputXml = objbzAgencyPriority.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                Response.Redirect("TASR_AgencyPriority.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyPriority_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAgencyPriority.RowCommand
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

    Protected Sub gvAgencyPriority_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyPriority.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrPriorityId As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrPriorityId = e.Row.FindControl("hdPRIORITYID")

            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForstrPriorityId As String
            EnstrQueryStringForstrPriorityId = objED.Encrypt(hdstrPriorityId.Value)
            '@ End of Code Used For Encription/Decription


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        linkDelete.Disabled = True
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    '    linkEdit.Disabled = True
                    '    'Else
                    'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
                    'End If
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & EnstrQueryStringForstrPriorityId & "'" & ");")
                End If
            Else
                linkDelete.Disabled = False
                linkEdit.Disabled = False
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & EnstrQueryStringForstrPriorityId & "'" & ");")
                'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            End If
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
            SearchAgencyPriority(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchAgencyPriority(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchAgencyPriority(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvAgencyPriority_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAgencyPriority.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyPriority_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyPriority.Sorting
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
            SearchAgencyPriority(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchAgencyPriority(PageOperation.Export)
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
        Dim strArray() As String = {"IATA Status Name"}
        Dim intArray() As Integer = {1}
        objExport.ExportDetails(objOutputXml, "PRIORITY", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAgencyIATAStatus .xls")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("TASR_AgencyPriority.aspx")
    End Sub
End Class
