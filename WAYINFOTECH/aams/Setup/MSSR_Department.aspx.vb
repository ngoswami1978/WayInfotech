
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Partial Class Setup_MSSR_Department
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt


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
            Session("PageName") = "Setup/MSSR_DepartmentSearch.aspx"
            'objeAAMS.ExpirePageCache()
            ' btnReset.Attributes.Add("onclick", "return DepartmentReset();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            '***************************************************************************************
            '***************************************************************************************

            If Not Page.IsPostBack Then
                If Not Session("Action") Is Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        txtDepartmentName.Text = Session("Action").ToString().Split("|").GetValue(2)
                        txtManagerName.Text = Session("Action").ToString().Split("|").GetValue(3)
                        DepartmentSearch()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Action") = Nothing
                    End If
                End If
            End If
            '***************************************************************************************

            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    DepartmentDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If

            '***************************************************************************************

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' btnReset.Attributes.Add("onclick", "return DepartmentReset();")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            'If (hdDepID.Value <> "") Then
            '    DepartmentDelete(hdDepID.Value)
            'End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Department.aspx?Action=I")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            DepartmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message()
        End Try
    End Sub
    Private Sub DepartmentSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
        objInputXml.LoadXml("<MS_SEARCHEPARTMENT_INPUT><Department_Name></Department_Name><ManagerName></ManagerName><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEPARTMENT_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Department_Name").InnerText = txtDepartmentName.Text
        objInputXml.DocumentElement.SelectSingleNode("ManagerName").InnerText = txtManagerName.Text
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
            ViewState("SortName") = "Department_Name"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Department_Name" '"LOCATION_CODE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'Here Back end Method Call
        objOutputXml = objbzDepartment.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            pnlPaging.Visible = True
            grdDepartment.DataSource = ds.Tables("DEPARTMENT")
            grdDepartment.DataBind()
            lblError.Text = ""
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
            txtRecordOnCurrentPage.Text = ds.Tables("DEPARTMENT").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            SetImageForSorting(grdDepartment)
            ' @ Added Code To Show Image'
            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "Department_Name"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdDepartment.HeaderRow.Cells(0).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdDepartment.HeaderRow.Cells(0).Controls.Add(imgDown)
            '        End Select
            '    Case "ManagerName"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdDepartment.HeaderRow.Cells(1).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdDepartment.HeaderRow.Cells(1).Controls.Add(imgDown)
            '        End Select
            'End Select

            '' @ Added Code To Show Image'
            ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Else
            grdDepartment.DataSource = Nothing
            grdDepartment.DataBind()
            pnlPaging.Visible = False
            txtTotalRecordCount.Text = "0"
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    
    Private Sub DepartmentDelete(ByVal DeptID As String)

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzADepartment As New AAMS.bizMaster.bzDepartment
        objInputXml.LoadXml("<MS_DELETEDEPARTMENT_INPUT><DepartmentID></DepartmentID></MS_DELETEDEPARTMENT_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = DeptID
        objOutputXml = objbzADepartment.Delete(objInputXml)
        'hdDepID.Value = ""
        Try
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("Action") = Request.QueryString("Action")
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                'Response.Redirect("MSSR_Department.aspx")
                ' Session("pno") = ddlPageNumber.SelectedValue
                DepartmentSearch()
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = "Unable to Delete"
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdDepartment_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdDepartment.RowCommand
        If e.CommandName = "DeleteX" Then
            Session("Action") = "U|" & e.CommandArgument & ""
            ' Response.Redirect("MSSR_Department.aspx")
            DepartmentDelete(e.CommandArgument)
        End If
    End Sub

   
    
    Protected Sub grdDepartment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDepartment.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdDepartmentId As HiddenField

        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        Dim objSecurityXml As New XmlDocument
        ' Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        Dim linkDelete As LinkButton

        linkEdit = e.Row.FindControl("linkEdit")
        linkDelete = e.Row.FindControl("linkDelete")
        hdDepartmentId = e.Row.FindControl("hdDepartmentId")


        objSecurityXml.LoadXml(Session("Security"))

        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Attributes("Value").Value)
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False
                Else
                    'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdDepartmentId.Value & ");")
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction();")
                End If
                'If strBuilder(2) = "0" Then
                'linkEdit.Disabled = False
                'Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdDepartmentId.Value.Trim) & "');")
                'End If
            End If
        Else
            linkDelete.Enabled = True
            linkEdit.Disabled = False
            linkDelete.Attributes.Add("onclick", "return DeleteFunction();")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdDepartmentId.Value.Trim) & "');")
        End If

    End Sub

    
    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            DepartmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            DepartmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            DepartmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdDepartment_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdDepartment.Sorting
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
            DepartmentSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdDepartment.AllowSorting = False
        grdDepartment.HeaderStyle.ForeColor = Drawing.Color.Black
        OfficeIdExport()
    End Sub
    Private Sub OfficeIdExport()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
            objInputXml.LoadXml("<MS_SEARCHEPARTMENT_INPUT><Department_Name></Department_Name><ManagerName></ManagerName><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEPARTMENT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Department_Name").InnerText = txtDepartmentName.Text
            objInputXml.DocumentElement.SelectSingleNode("ManagerName").InnerText = txtManagerName.Text
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
                ViewState("SortName") = "Department_Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Department_Name" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
            'Here Back end Method Call
            objOutputXml = objbzDepartment.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                grdDepartment.DataSource = ds.Tables("DEPARTMENT")
                grdDepartment.DataBind()
                lblError.Text = ""
                Dim objExport As New ExportExcel
                Dim strArray() As String = {"Department Name", "Manager Name"}
                Dim intArray() As Integer = {1, 2}
                objExport.ExportDetails(objOutputXml, "DEPARTMENT", intArray, strArray, ExportExcel.ExportFormat.Excel, "DepartmentExport.xls")
            Else
                grdDepartment.DataSource = String.Empty
                grdDepartment.DataBind()
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_Department.aspx")
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
