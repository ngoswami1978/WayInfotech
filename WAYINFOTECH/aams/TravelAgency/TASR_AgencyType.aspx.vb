'#####################################################################
'############   Page Name -- TravelAgency_TASR_AgencyType      #######
'############   Date 4-December 2007  ################################
'############   Developed By Abhishek  ###############################
'#####################################################################
Imports System.Xml
Imports System.Data
Partial Class TravelAgency_TASR_AgencyType
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
            Session("PageName") = Request.Url.ToString() '"TravelAgency_TASR_AgencyType  "
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            'btnReset.Attributes.Add("onclick", "return AgencyTypeReset();")
            'btnSearch.Attributes.Add("onclick", "return AgencyTypeMandatory();")
            'btnNew.Attributes.Add("onclick", "return NewMSUPAgencyType();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For                
                'If Not Session("Act") Is Nothing Then
                '    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                '        txtagencyType.Text = Session("Act").ToString().Split("|").GetValue(2)
                '        SearchAgencyType(PageOperation.Search)
                '        lblError.Text = objeAAMSMessage.messDelete
                '        Session("Act") = Nothing
                '    End If
                'End If

                '*******************************************************************
                ' Load Data For Editing Mode
            End If

            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        AgencyTypeDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If


            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Attributes("Value").Value)
                    If strBuilder(0) <> "0" Then
                        SearchAgencyType(PageOperation.Search)
                    Else
                        Response.Redirect("../NoRights.aspx")
                    End If
                Else
                    SearchAgencyType(PageOperation.Search)
                End If
                    'If strBuilder(0) = "0" Then
                    '    btnSearch.Enabled = False
                    '    Response.Redirect("~/NoRights.aspx", False)
                    '    Exit Sub
                    'End If
                    'If strBuilder(1) = "0" Then
                    '    btnNew.Enabled = False
                    'End If
                    'If strBuilder(4) = "0" Then
                    '    btnExport.Enabled = False
                    'End If


            Else
                SearchAgencyType(PageOperation.Search)
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Try
    '        lblError.Text = ""
    '        SearchAgencyType(PageOperation.Search)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub
    Sub SearchAgencyType(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
            objInputXml.LoadXml("<MS_SEARCHAGENCYTYPE_INPUT><Agency_Type_Name></Agency_Type_Name><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAGENCYTYPE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("Agency_Type_Name").InnerText = "" ' txtAgencyType.Text

            'Start CODE for sorting and paging
            'If Operation = PageOperation.Search Then
            '    If ViewState("PrevSearching") Is Nothing Then
            '        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            '    Else
            '        Dim objTempInputXml As New XmlDocument
            '        Dim objNodeList As XmlNodeList

            '        objTempInputXml.LoadXml(ViewState("PrevSearching"))
            '        objNodeList = objTempInputXml.DocumentElement.ChildNodes
            '        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            '        For Each objNode As XmlNode In objNodeList
            '            If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
            '                If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
            '                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
            '                    ddlPageNumber.SelectedValue = "1"
            '                End If
            '            End If
            '        Next
            '    End If


            'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Agency_Type_Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Agency_Type_Name" '"LOCATION_CODE"
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
            objOutputXml = objbzAgencyType.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    'Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Dim dView As New DataView
                dView = ds.Tables("AgencyType").DefaultView
                Session("AgencyType") = ds.Tables("AgencyType").DefaultView

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "Agency_Type_Name"
                End If
                If ViewState("Direction") Is Nothing Then
                    ViewState("Direction") = "asc"
                End If

                If ViewState("Direction") = "asc" Then
                    dView.Sort = ViewState("SortName").ToString + "    asc "
                Else
                    dView.Sort = ViewState("SortName").ToString + "    desc "
                End If
                gvAgencyType.DataSource = dView
                gvAgencyType.DataBind()
                SetImageForSorting(gvAgencyType)
                'Code Added For Paging And Sorting In case Of Delete The Record

                'pnlPaging.Visible = True
                'Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                'Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
                'If count <> ddlPageNumber.Items.Count Then
                '    ddlPageNumber.Items.Clear()
                '    For i As Integer = 1 To count
                '        ddlPageNumber.Items.Add(i.ToString)
                '    Next
                'End If
                'ddlPageNumber.SelectedValue = selectedValue
                ''Code for hiding prev and next button based on count
                'If count = 1 Then
                '    'pnlPaging.Visible = False
                '    ' ddlPageNumber.Visible = False
                '    lnkNext.Visible = False
                '    lnkPrev.Visible = False
                'Else
                '    'ddlPageNumber.Visible = True
                '    lnkPrev.Visible = True
                '    lnkNext.Visible = True
                'End If

                ''Code for hiding next button when pagenumber is equal to page count
                'If ddlPageNumber.SelectedValue = count.ToString Then
                '    lnkNext.Visible = False
                'Else
                '    lnkNext.Visible = True
                'End If

                ''Code for hiding prev button when pagenumber is 1
                'If ddlPageNumber.SelectedValue = "1" Then
                '    lnkPrev.Visible = False
                'Else
                '    lnkPrev.Visible = True
                'End If
                'hdRecordOnCurrentPage.Value = ds.Tables("AgencyType").Rows.Count.ToString
                'txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                '' 
                '' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "Agency_Type_Name"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                gvAgencyType.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                gvAgencyType.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select
                ''  Added Code To Show Image'

                '' End of Code Added For Paging And Sorting In case Of Delete The Record

            Else
                gvAgencyType.DataSource = Nothing
                gvAgencyType.DataBind()
                'txtTotalRecordCount.Text = "0"
                'hdRecordOnCurrentPage.Value = "0"
                'pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    'Sub AgencyTypeDelete(ByVal strAgencyTypeId As String)
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Try
    '        Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
    '        objInputXml.LoadXml("<MS_DELETEAGENCYTYPE_INPUT><AgencyTypeId/></MS_DELETEAGENCYTYPE_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = strAgencyTypeId
    '        'Here Back end Method Call
    '        objOutputXml = objbzAgencyType.Delete(objInputXml)
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
    '            Session("Act") = Request.QueryString("Action")
    '            Response.Redirect("TASR_AgencyType.aspx")
    '        Else
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub


    Protected Sub gvAgencyType_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAgencyType.RowCommand
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

    'Protected Sub gvAgencyType_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyType.RowDataBound
    '    Try
    '        If e.Row.RowIndex < 0 Then
    '            Exit Sub
    '        End If
    '        Dim hdstrAgencyTypeId As HiddenField
    '        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
    '        linkEdit = e.Row.FindControl("linkEdit")
    '        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
    '        linkDelete = e.Row.FindControl("linkDelete")
    '        hdstrAgencyTypeId = e.Row.FindControl("hdAgencyTypeId")
    '        '@ Code Used For Encription/Decription
    '        Dim EnstrQueryStringForstrAgencyTypeId As String
    '        EnstrQueryStringForstrAgencyTypeId = objED.Encrypt(hdstrAgencyTypeId.Value)
    '        '@ End of Code Used For Encription/Decription


    '        Dim objSecurityXml As New XmlDocument
    '        objSecurityXml.LoadXml(Session("Security"))
    '        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
    '            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Count <> 0 Then
    '                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Attributes("Value").Value)
    '                If strBuilder(3) = "0" Then
    '                    linkDelete.Disabled = True
    '                Else
    '                    linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrAgencyTypeId.Value & "'" & ");")
    '                End If
    '                'If strBuilder(2) = "0" Then
    '                '    linkEdit.Disabled = True
    '                'Else
    '                '    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAgencyTypeId.Value & "'" & ");")
    '                'End If
    '                'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAgencyTypeId.Value & "'" & ");")
    '                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & EnstrQueryStringForstrAgencyTypeId & "'" & ");")
    '            End If
    '        Else
    '            linkEdit.Disabled = False
    '            linkDelete.Disabled = False
    '            linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrAgencyTypeId.Value & "'" & ");")

    '            linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & EnstrQueryStringForstrAgencyTypeId & "'" & ");")
    '            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAgencyTypeId.Value & "'" & ");")
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub



#Region "Code for Paging And sorting."
    'Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
    '    Try
    '        If ddlPageNumber.SelectedValue <> "1" Then
    '            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
    '        End If
    '        SearchAgencyType(PageOperation.Search)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
    '    Try
    '        If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
    '            ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
    '        End If
    '        SearchAgencyType(PageOperation.Search)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
    '    Try
    '        SearchAgencyType(PageOperation.Search)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub gvAgencyType_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAgencyType.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvAgencyType_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvAgencyType.Sorting
        Dim dv As DataView
        If Session("AgencyType") IsNot Nothing Then

            dv = CType(Session("AgencyType"), DataView)
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Agency_Type_Name"
                ViewState("Direction") = "asc"
            Else

                If ViewState("SortName") = SortName Then
                    If ViewState("Direction") = "asc" Then
                        ViewState("Direction") = "desc"
                    Else
                        ViewState("Direction") = "asc"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Direction") = "asc"
                End If
            End If

            dv.Sort = ViewState("SortName").ToString() + " " + ViewState("Direction").ToString()
            gvAgencyType.DataSource = dv
            gvAgencyType.DataBind()


            SetImageForSorting(gvAgencyType)
        End If
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
                If ViewState("Direction") = "asc" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Direction") = "desc" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub
#End Region
    'Code for Export
    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Try
    '        SearchAgencyType(PageOperation.Export)
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try

    'End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    'Sub Export(ByVal objOutputXml As XmlDocument)
    '    Dim objExport As New ExportExcel
    '    Dim strArray() As String = {"Agency Type"}
    '    Dim intArray() As Integer = {0}

    '    objExport.ExportDetails(objOutputXml, "AgencyType", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAgencyType.xls")
    'End Sub
    'End Code For Export

    'Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    Response.Redirect("TASR_AgencyType.aspx")
    'End Sub
End Class
