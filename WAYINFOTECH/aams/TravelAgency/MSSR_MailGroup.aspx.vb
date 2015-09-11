
Partial Class Order_MSSR_MailGroup
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    ' Const strInput = "<MS_SEARCHEMAILGROUP_INPUT><GROUPDETAIL GroupName="" EmployeeID="" GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice=''/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMAILGROUP_INPUT>"
    Const strInput As String = "<MS_SEARCHEMAILGROUP_INPUT><GROUPDETAIL GroupName='' EmployeeID='' GroupMNC='' GroupISP='' GroupTraining='' GroupAoffice='' DepartmentID='' GroupID='' PAGE_NO='' PAGE_SIZE=''  SORT_BY='' DESC=''  /></MS_SEARCHEMAILGROUP_INPUT>"

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

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_MailGroup.aspx?id=-1")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_MailGroup.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindGrid(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub BindGrid(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        'Dim Xmlnode1 As XmlNode
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmmail As New AAMS.bizMaster.bzEmailGroup
        Dim objSecurityXml As New XmlDocument
        Try
            objInputXml.LoadXml(strInput)
            objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupName").InnerText = txtgrpname.Text.Trim()
            If ddlCrs.SelectedValue = 0 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText = ""
            ElseIf ddlCrs.SelectedValue = 1 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText = 1
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText = ""
            ElseIf ddlCrs.SelectedValue = 2 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText = 1
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText = ""
            ElseIf ddlCrs.SelectedValue = 3 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText = "1"
            ElseIf ddlCrs.SelectedValue = 4 Then
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupMNC").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupISP").InnerText = ""
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupTraining").InnerText = 1
                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("GroupAoffice").InnerText = ""
            End If

            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then

                If ViewState("PrevSearching") Is Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)

                Else
                    Dim objTempInputXml As New XmlDocument
                    ' Dim objNodeList As XmlNodeList
                    Dim objAtt As XmlAttribute
                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objAtt In objTempInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes

                        If objAtt.Name <> "PAGE_NO" And objAtt.Name <> "SORT_BY" And objAtt.Name <> "DESC" And objAtt.Name <> "PAGE_SIZE" Then
                            If objAtt.InnerText <> objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes(objAtt.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                            End If
                        End If
                    Next
                 
                End If

                objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
              

                If ViewState("SortName") Is Nothing Then
                    ViewState("SortName") = "GroupName"
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY").InnerText = "GroupName"

                Else
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("SORT_BY").InnerText = ViewState("SortName")

                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DESC").InnerText = ViewState("Desc")

                Else
                    objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DESC").InnerText = ViewState("Desc")

                End If
            End If
            'End Code for paging and sorting

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If (objSecurityXml.DocumentElement.SelectNodes("DepartmentID").Count <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DepartmentID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("GROUPDETAIL").Attributes("DepartmentID").InnerText = ""
                    End If
                End If
            End If

         


            objOutputXml = objbzEmmail.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'Xmlnode1 = objOutputXml.DocumentElement.SelectSingleNode("Errors")
                ' If Xmlnode1.Attributes("Status").InnerText.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvEmailGroup.DataSource = ds.Tables("GROUP_DETAIL")
                gvEmailGroup.DataBind()
                lblError.Text = ""

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
                hdRecordOnCurrentPage.Value = ds.Tables("GROUP_DETAIL").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString
                    Case "GroupName"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvEmailGroup.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvEmailGroup.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "GroupType"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvEmailGroup.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvEmailGroup.HeaderRow.Cells(2).Controls.Add(imgDown)
                        End Select
                    Case "EmailID"
                        Select Case ViewState("Desc").ToString
                            Case "FALSE"
                                gvEmailGroup.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvEmailGroup.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value ' Xmlnode1.SelectSingleNode("Errors/Error").Attributes("Description").InnerText.ToString()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvEmailGroup.DataSource = Nothing
                gvEmailGroup.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    
    Protected Sub gvEmailGroup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEmailGroup.RowDataBound
        Try

            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim btnDelete, btnEdit As LinkButton
            btnDelete = CType(e.Row.FindControl("btnDelete"), LinkButton)
            btnEdit = CType(e.Row.FindControl("btnEdit"), LinkButton)

            '#############################################################
            ' Code added For Selecting an Items 
            Dim lnkSelect As LinkButton
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "','" + e.Row.RowIndex.ToString + "');")
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Email Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Email Group']").Attributes("Value").Value)

                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False
                    Else
                        btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & e.Row.DataItem("GroupId") & "');")
                    End If

                    'If strBuilder(2) = "0" Then
                    '    btnEdit.Enabled = False
                    'Else
                    '    btnEdit.Attributes.Add("OnClick", "return EditFunction('" & e.Row.DataItem("GroupId") & "');")
                    'End If
                    btnEdit.Attributes.Add("OnClick", "return EditFunction('" & e.Row.DataItem("GroupId") & "');")

                End If
            Else
                btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & e.Row.DataItem("GroupId") & "');")
                btnEdit.Attributes.Add("OnClick", "return EditFunction('" & e.Row.DataItem("GroupId") & "');")
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(3).Text = e.Row.Cells(3).Text.Replace(",", ", ")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString() '"TravelAgency_MSSR_OnlineStatus"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                'If Request.QueryString("Action") IsNot Nothing Then
                '    If Request.QueryString("Action").Trim().ToUpper() = "D" Then
                '        EmailGrp_Delete(Request.QueryString("id").Trim())
                '        BindGrid(PageOperation.Search)
                '    End If
                'End If
            End If
            
            
            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Email Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Email Group']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
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
            ' Code for Delete 
            If (hdMailGroupID.Value <> "") Then
                EmailGrp_Delete(hdMailGroupID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region "Delete Function Definition"
    Sub EmailGrp_Delete(ByVal EmailGID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objEmailGroup As New AAMS.bizMaster.bzEmailGroup
            objInputXml.LoadXml("<MS_DELETEEMAILGROUP_INPUT><GroupID></GroupID></MS_DELETEEMAILGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GroupID").InnerXml = EmailGID
            'Here Back end Method Call
            hdMailGroupID.Value = ""
            objOutputXml = objEmailGroup.Delete(objInputXml)
            BindGrid(PageOperation.Search)
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
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindGrid(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindGrid(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindGrid(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvEmailGroup_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEmailGroup.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvEmailGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvEmailGroup.Sorting
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
            BindGrid(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindGrid(PageOperation.Export)
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
        Dim strArray() As String = {"Group Name", "Email Group Type", "EmailId"}
        Dim intArray() As Integer = {1, 0, 3}
        objExport.ExportDetails(objOutputXml, "GROUP_DETAIL", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportEMAILGROUP.xls")
    End Sub
End Class
