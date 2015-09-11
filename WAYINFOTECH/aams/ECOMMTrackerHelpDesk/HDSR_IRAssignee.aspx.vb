'################################################
'######## Developed By Abhishek #################
'################################################
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class ETHelpDesk_HDSR_IRAssignee
    Inherits System.Web.UI.Page

    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim imgDown As New Image
    Dim imgUp As New Image

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString
            objeAAMS.ExpirePageCache()
            lblError.Text = String.Empty


            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                Else
                    Response.Redirect("../NoRights.aspx")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '*****************Delete Functionality
            If hdDeleteFlag.Value <> "" Then
                HDIRAssignee_Del(hdDeleteFlag.Value.Trim())
                HDIRAssignee_Search()
            End If

            '*****************End of Delete 

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            HDIRAssignee_Search()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_IRAssignee.aspx?Action=I", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub
    Private Sub HDIRAssignee_Search()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee
            objInputXml.LoadXml("<HD_SEARCHIR_ASSIGNEE_INPUT><ASSIGNEE_NAME></ASSIGNEE_NAME> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHIR_ASSIGNEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEE_NAME").InnerText = txtAssigneeName.Text.Trim()

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
                ViewState("SortName") = "ASSIGNEE_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ASSIGNEE_NAME" '"LOCATION_CODE"
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
            objOutputXml = objbzAssignee.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)


                ViewState("PrevSearching") = objInputXml.OuterXml
                lblError.Text = ""
                grdvIRAssignee.DataSource = ds.Tables("IR_ASSIGNEE").DefaultView
                grdvIRAssignee.DataBind()

                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvIRAssignee)
                If ViewState("Desc") = "FALSE" Then
                    grdvIRAssignee.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvIRAssignee.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

            Else
                grdvIRAssignee.DataSource = Nothing
                grdvIRAssignee.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub HDIRAssignee_Del(ByVal AssingeeID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objhdAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee

            objInputXml.LoadXml("<HD_DELETEIR_ASSIGNEE_INPUT><ASSIGNEEID></ASSIGNEEID></HD_DELETEIR_ASSIGNEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEEID").InnerText = AssingeeID
            hdDeleteFlag.Value = ""
            'Call a function
            objOutputXml = objhdAssignee.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                'Session("Act") = Request.QueryString("Action")
                'Response.Redirect("HDSR_IRAssignee.aspx", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvIRAssignee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvIRAssignee.SelectedIndexChanged

    End Sub

    Protected Sub grdvIRAssignee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvIRAssignee.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
            Dim btnDelete As LinkButton
            Dim hdAssigneeCode As HiddenField

            btnDelete = CType(e.Row.FindControl("linkDelete"), LinkButton)
            btnEdit = CType(e.Row.FindControl("linkEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
            hdAssigneeCode = CType(e.Row.FindControl("hdField"), HiddenField)

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Attributes("Value").Value)
                    If strBuilder(3) = "0" Then
                        btnDelete.Enabled = False '.Disabled = True
                    Else
                        btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & hdAssigneeCode.Value & "');")
                    End If
                    'If strBuilder(2) = "0" Then
                    '    btnEdit.Disabled = True
                    'Else
                    '    btnEdit.Attributes.Add("OnClick", "return EditFunction('" & hdAssigneeCode.Value & "');")
                    'End If
                    btnEdit.Attributes.Add("OnClick", "return EditFunction('" & objED.Encrypt(hdAssigneeCode.Value) & "');")
                End If
            Else
                btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & hdAssigneeCode.Value & "');")
                btnEdit.Attributes.Add("OnClick", "return EditFunction('" & objED.Encrypt(hdAssigneeCode.Value) & "');")
            End If
            'btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & hdAssigneeCode.Value & "');")
            'btnEdit.Attributes.Add("OnClick", "return EditFunction('" & hdAssigneeCode.Value & "');")
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            HDIRAssignee_Search()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            HDIRAssignee_Search()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            HDIRAssignee_Search()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvIRAssignee_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvIRAssignee.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
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
        HDIRAssignee_Search()
    End Sub
#End Region

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

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objExport As New ExportExcel
        Dim intArray(0) As Integer
        Dim strArray(0) As String
        Try

            Dim objbzAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee

            objInputXml.LoadXml("<HD_SEARCHIR_ASSIGNEE_INPUT><ASSIGNEE_NAME></ASSIGNEE_NAME> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHIR_ASSIGNEE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ASSIGNEE_NAME").InnerText = txtAssigneeName.Text.Trim()

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
                ViewState("SortName") = "ASSIGNEE_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ASSIGNEE_NAME" '"LOCATION_CODE"
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
            objOutputXml = objbzAssignee.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)


                ViewState("PrevSearching") = objInputXml.OuterXml
                lblError.Text = ""
                grdvIRAssignee.DataSource = ds.Tables("IR_ASSIGNEE").DefaultView
                grdvIRAssignee.DataBind()

                PagingCommon(objOutputXml)
                Dim intcol As Integer = GetSortColumnIndex(grdvIRAssignee)
                If ViewState("Desc") = "FALSE" Then
                    grdvIRAssignee.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grdvIRAssignee.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If

                intArray(0) = 1
                strArray(0) = "IR Assignee Name"


                objExport.ExportDetails(objOutputXml, "IR_ASSIGNEE", intArray, strArray, ExportExcel.ExportFormat.Excel, "IRAssignee.xls")


            Else
                grdvIRAssignee.DataSource = Nothing
                grdvIRAssignee.DataBind()
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
