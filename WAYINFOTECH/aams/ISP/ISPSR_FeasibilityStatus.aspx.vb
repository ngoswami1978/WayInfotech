Imports System.xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class ISP_ISPSR_FeasibilityStatus
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "ISP/ISPSR_FeasibilityStatus.aspx"
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Attributes("Value").Value)
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
                objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                btnNew.Attributes.Add("onclick", "return NewFunction();")
                '  btnReset.Attributes.Add("onclick", "return FeasibilityReset();")
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                        FeasibilityStatusDelete(Request.QueryString("FeasibleStatusID").ToString())
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Sub FeasibilityStatusDelete(ByVal strFeasibilityID As String)
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtFeasibilityStatus As New AAMS.bizISP.bzISPFeasibleStatus
            objInputXml.LoadXml("<ISP_DELETEFEASIBILESTATUS_INPUT><FeasibleStatusID></FeasibleStatusID></ISP_DELETEFEASIBILESTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("FeasibleStatusID").InnerXml = strFeasibilityID
            'Here Back end Method Call
            objOutputXml = objtFeasibilityStatus.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                FeasibilityStatusSearch(PageOperation.Search)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub FeasibilityStatusSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAgency As New AAMS.bizISP.bzISPFeasibleStatus
        Try
            objInputXml.LoadXml("<ISP_SEARCHFEASIBILESTATUS_INPUT><Name></Name> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></ISP_SEARCHFEASIBILESTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = Trim(txtFeasibilityStatus.Text)
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


            objOutputXml = objbzAgency.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdFeasibilityStatus.DataSource = ds.Tables("FEASIBILE_STATUS")
                grdFeasibilityStatus.DataBind()
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
                hdRecordOnCurrentPage.Value = ds.Tables("FEASIBILE_STATUS").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' 
                ' @ Added Code To Show Image'

                SetImageForSorting(grdFeasibilityStatus)
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "Name"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdFeasibilityStatus.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdFeasibilityStatus.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                'End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                grdFeasibilityStatus.DataSource = String.Empty
                grdFeasibilityStatus.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            lblError.Text = ""
            FeasibilityStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdFeasibilityStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdFeasibilityStatus.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim hdFeasibleId As New HiddenField
        Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor

        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            lnkEdit = e.Row.FindControl("linkEdit")
            lnkDelete = e.Row.FindControl("linkDelete")
            hdFeasibleId = e.Row.FindControl("rowHidden")


            '@ Code Used For Encription/Decription
            Dim EnstrQueryStringForstrFeasibleId As String
            EnstrQueryStringForstrFeasibleId = objED.Encrypt(hdFeasibleId.Value)
            '@ End of Code Used For Encription/Decription


            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspFeasibilityStatus']").Attributes("Value").Value)
                End If

                If strBuilder(3) = "0" Then
                    lnkDelete.Disabled = True
                Else
                    lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdFeasibleId.Value & ");")
                End If

                'If strBuilder(2) = "0" Then
                '    lnkEdit.Disabled = True
                'Else
                '    lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdFeasibleId.Value & ");")
                'End If
                'lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdFeasibleId.Value & ");")
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrFeasibleId & "');")
            Else
                lnkDelete.Disabled = False
                lnkEdit.Disabled = False
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForstrFeasibleId & "');")
                'lnkEdit.Attributes.Add("onclick", "return EditFunction(" & hdFeasibleId.Value & ");")
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdFeasibleId.Value & ");")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("ISPSR_FeasibilityStatus.aspx")
    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            FeasibilityStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            FeasibilityStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            FeasibilityStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdFeasibilityStatus_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFeasibilityStatus.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdFeasibilityStatus_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdFeasibilityStatus.Sorting
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
            FeasibilityStatusSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            FeasibilityStatusSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Status Name"}
        Dim intArray() As Integer = {1}

        objExport.ExportDetails(objOutputXml, "FEASIBILE_STATUS", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPFeasibilityStatus.xls")
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
