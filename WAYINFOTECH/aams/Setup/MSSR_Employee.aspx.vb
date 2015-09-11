Imports System.Data
Imports System.Xml
Imports System.Data.SqlClient
Partial Class Setup_MSSR_Employee
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Dim strBuilderRequest As New StringBuilder
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
        Session("PageName") = Request.Url.ToString() '"Setup/MSSR_Employee.aspx"
        Try
            drpAoffice.Attributes.Add("onkeyup", "return gotop('drpAoffice');")
            drpDepartment.Attributes.Add("onkeyup", "return gotop('drpDepartment');")
            drplstDeig.Attributes.Add("onkeyup", "return gotop('drplstDeig');")

            drpAgreementSigned.Attributes.Add("onkeyup", "return gotop('drpAgreementSigned');")
            drplstPermission.Attributes.Add("onkeyup", "return gotop('drplstPermission');")
            drplstSecGroup.Attributes.Add("onkeyup", "return gotop('drplstSecGroup');")

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpDepartment, "DepartmentName", True, 3)
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drplstDeig, "DESIGNATION", True, 3)
                objeAAMS.BindDropDown(drplstPermission, "PERMISSIONS", True, 3)
                objeAAMS.BindDropDown(drplstSecGroup, "SECURITYGROUP", True, 3)

                If Not Request.QueryString("Aoffice") Is Nothing Then
                    'Response.Write(Request.QueryString("Aoffice").Trim())
                    If Request.QueryString("Aoffice").Trim() <> "" Then
                        drpAoffice.SelectedValue = Request.QueryString("Aoffice").Trim()
                        drpAoffice.Enabled = False
                    End If

                End If
            End If
            '***************************************************************************************
            Session("EmployeeIP") = Nothing
            Session("ViewEmployeeData") = Nothing
            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            
            checkSecurity()
            Dim objSecurityXml As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpAoffice.SelectedValue = li.Value
                            End If

                        End If
                        drpAoffice.Enabled = False
                    End If
                End If
            End If

            If Request.QueryString("Dept") IsNot Nothing And Request.QueryString("Popup") IsNot Nothing Then
                If drpDepartment.SelectedValue = "" Then
                    Dim li As ListItem = drpDepartment.Items.FindByText(Request.QueryString("Dept").ToString)
                    If li IsNot Nothing Then
                        drpDepartment.SelectedValue = li.Value
                    End If
                End If
                hdCtrlId.Value = Request.QueryString("ctrlId").ToString
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Sub checkSecurity()
        Dim objSecurityXml As New XmlDocument

        If Session("Security") IsNot Nothing Then
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Attributes("Value").Value)
                    strBuilderRequest = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']").Attributes("Value").Value)
                    If strBuilder(1) = "0" And strBuilderRequest(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(0) = "0" And strBuilderRequest(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    'Code End for security
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        End If
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        EmployeeSearch()
    End Sub
    '*******************************************************************************************************
    'Method for search Employee
    '*******************************************************************************************************
    Private Sub EmployeeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        '@ Start Changed By Abhishek on 05-10-10 ' New Xml Input       
        'objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><Sec_Group_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><Sec_Group_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><INC></INC></MS_SEARCHEMPLOYEE_INPUT>")
        If Request.QueryString("ctrlId") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("INC").InnerText = "T"
        End If
        '@ End of Changed By Abhishek on 05-10-10 ' New Xml Input       
        objInputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(txtEmployeeName.Text)
        If drpDepartment.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = drpDepartment.SelectedValue
        End If
        If drpAoffice.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
        End If
        If drplstDeig.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("Designation").InnerText = drplstDeig.SelectedItem.Text.Trim()
        End If
        If drplstPermission.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText = drplstPermission.SelectedValue.Trim()
        End If
        If drpAgreementSigned.SelectedValue <> "0" Then
            objInputXml.DocumentElement.SelectSingleNode("AgreementSigned").InnerText = drpAgreementSigned.SelectedValue.Trim()
        End If
        If drplstSecGroup.SelectedValue <> "0" Then
            objInputXml.DocumentElement.SelectSingleNode("Sec_Group_ID").InnerText = drplstSecGroup.SelectedValue
        End If


        '@ New 

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
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = 1
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            End If
        End If
        objInputXml.DocumentElement.SelectSingleNode("Request").InnerText = IIf(chkRequest.Checked = True, "0", "")
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
            ViewState("SortName") = "Employee_Name"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Employee_Name" '"LOCATION_CODE"
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
        objOutputXml = objbzEmployee.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml

            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            grdEmployee.DataSource = ds.Tables("Employee")
            grdEmployee.DataBind()
            ' ##################################################################
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
            txtRecordOnCurrentPage.Text = ds.Tables("Employee").Rows.Count.ToString
            txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ' @ Added Code To Show Image'

            SetImageForSorting(grdEmployee)
            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "Employee_Name"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(0).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(0).Controls.Add(imgDown)
            '        End Select
            '    Case "Department_Name"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(1).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(1).Controls.Add(imgDown)
            '        End Select
            '    Case "Aoffice"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(2).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(2).Controls.Add(imgDown)

            '        End Select
            '    Case "DateStart"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(3).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(3).Controls.Add(imgDown)
            '        End Select
            '    Case "DateEnd"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(4).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(4).Controls.Add(imgDown)

            '        End Select
            '    Case "City_Name"
            '        Select Case ViewState("Desc")
            '            Case "FALSE"
            '                grdEmployee.HeaderRow.Cells(5).Controls.Add(imgUp)
            '            Case "TRUE"
            '                grdEmployee.HeaderRow.Cells(5).Controls.Add(imgDown)

            '        End Select
            'End Select
            ' @ Added Code To Show Image'
            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@





            ' ###################################################################
            '@ End of Code Added For Paging And Sorting In case Of Delete The Record
            ' ###################################################################


            lblError.Text = ""
        Else
            grdEmployee.DataSource = Nothing
            grdEmployee.DataBind()
            txtTotalRecordCount.Text = "0"
            txtRecordOnCurrentPage.Text = "0"
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            pnlPaging.Visible = False
        End If
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Session("Action") = "I"
        Response.Redirect("MSUP_Employee.aspx")
    End Sub

    'Protected Sub grdEmployee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdEmployee.ItemCommand
    '    'Code for Edit Data
    '    If e.CommandName = "EditX" Then
    '        Session("Action") = "U|" & e.CommandArgument & ""
    '        Response.Redirect("MSUP_Employee.aspx")
    '    End If
    '    'Code for Delete Date
    '    If e.CommandName = "DeleteX" Then
    '        EmployeeDelete(e.CommandArgument)
    '    End If
    'End Sub
    '*******************************************************************************************************
    'Method for Delete Employee
    '*******************************************************************************************************
    Private Sub EmployeeDelete(ByVal strEmployeeId As String)
        Dim objInputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_DELETEEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_DELETEEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = strEmployeeId
        'Here Back end Method Call

        objInputXml = objbzEmployee.Delete(objInputXml)

        If objInputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

            ' ###################################################################
            '@ Code Added For Paging And Sorting In case Of Delete The Record
            ' ###################################################################
            Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
            If txtRecordOnCurrentPage.Text = "1" Then
                If CurrentPage - 1 > 0 Then
                    ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                Else
                    ddlPageNumber.SelectedValue = "1"
                End If
            End If
            ' ###################################################################
            '@ End of Code Added For Paging And Sorting In case Of Delete The Record
            ' ###################################################################

            EmployeeSearch()
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objInputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    'Protected Sub grdEmployee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdEmployee.ItemDataBound
    '    Try
    '        If e.Item.ItemIndex < 0 Then
    '            Exit Sub
    '        End If
    '        '#############################################################
    '        ' Code added For Selecting an Items 

    '        'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
    '        Dim lnkSelect As LinkButton
    '        'Dim hdSelect As HiddenField
    '        'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
    '        lnkSelect = e.Item.FindControl("lnkSelect")
    '        If (Request.QueryString("PopUp")) Is Nothing Then
    '            lnkSelect.Visible = False
    '        Else
    '            lnkSelect.Visible = True
    '            'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
    '            lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & lnkSelect.CommandArgument & "');")
    '        End If

    '        '#############################################################
    '        Dim lnkDelete, lnkEdit As LinkButton
    '        lnkDelete = CType(e.Item.FindControl("lnkDelete"), LinkButton)
    '        lnkEdit = CType(e.Item.FindControl("lnkEdit"), LinkButton)
    '        Dim objSecurityXml As New XmlDocument
    '        objSecurityXml.LoadXml(Session("Security"))
    '        If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Count <> 0 Then
    '            strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value)

    '            If strBuilder(3) = "0" Then
    '                lnkDelete.Enabled = False
    '            Else
    '                lnkDelete.Attributes.Add("OnClick", "return ConfirmDelete();")
    '            End If
    '            If strBuilder(2) = "0" Then
    '                lnkEdit.Enabled = False
    '            End If
    '        End If

    '        Dim lblDOJ, lblDOL As Label
    '        lblDOJ = e.Item.FindControl("lblDOJ")
    '        lblDOL = e.Item.FindControl("lblDOL")
    '        If lblDOJ.Text <> "" Then
    '            lblDOJ.Text = objeAAMS.ConvertDate(lblDOJ.Text).ToString("dd-MMM-yy")
    '        End If
    '        If lblDOL.Text <> "" Then
    '            lblDOL.Text = objeAAMS.ConvertDate(lblDOL.Text).ToString("dd-MMM-yy")
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'txtEmployeeName.Text = String.Empty
        'drpDepartment.SelectedIndex = 0
        'drpAoffice.SelectedIndex = 0
        'drplstDeig.SelectedIndex = 0
        'drplstPermission.SelectedIndex = 0
        'drpAgreementSigned.SelectedValue = "3"
        'grdEmployee.DataSource = Nothing
        'grdEmployee.DataBind()
        If Request.QueryString("Dept") IsNot Nothing And Request.QueryString("Popup") IsNot Nothing Then
            Response.Redirect("MSSR_Employee.aspx?" + Request.QueryString.ToString, False)
        Else
            Response.Redirect("MSSR_Employee.aspx", False)
        End If


    End Sub

    ' ###################################################################
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            EmployeeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            EmployeeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            EmployeeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdEmployee_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdEmployee.Sorted
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdEmployee_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdEmployee.Sorting
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
            EmployeeSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    ' ###################################################################
    Protected Sub grdEmployee_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEmployee.RowCreated
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdEmployee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEmployee.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            '#############################################################
            ' Code added For Selecting an Items 

            'Dim lnkSelect As System.Web.UI.HtmlControls.HtmlAnchor
            Dim lnkSelect As LinkButton
            'Dim hdSelect As HiddenField
            'hdSelect = (CType(e.Row.FindControl("hdSelect"), HiddenField))
            lnkSelect = e.Row.FindControl("lnkSelect")
            If (Request.QueryString("PopUp")) Is Nothing Then
                lnkSelect.Visible = False
            Else
                lnkSelect.Visible = True
                'lnkSelect.Attributes.Add("OnClick", "return SelectFunction('" & hdSelect.Value & "');")
                lnkSelect.Attributes.Add("OnClick", "return SelectFunctionEmployeeData('" & lnkSelect.CommandArgument & "');")
            End If

            '#############################################################
            Dim lnkDelete, lnkEdit As LinkButton
            lnkDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EmployeeEX']").Attributes("Value").Value)
                'strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Request User']").Attributes("Value").Value)

                If strBuilder(3) = "0" Then
                    lnkDelete.Enabled = False
                Else
                    lnkDelete.Attributes.Add("OnClick", "return ConfirmDeleteEmplyeeData();")
                End If
                If strBuilder(2) = "0" Then
                    lnkEdit.Enabled = True
                End If
            End If

            'Dim lblDOJ, lblDOL As Label
            'lblDOJ = e.Row.FindControl("lblDOJ")
            'lblDOL = e.Row.FindControl("lblDOL")
            'If lblDOJ.Text <> "" Then
            '    lblDOJ.Text = objeAAMS.ConvertDate(lblDOJ.Text).ToString("dd-MMM-yy")
            'End If
            'If lblDOL.Text <> "" Then
            '    lblDOL.Text = objeAAMS.ConvertDate(lblDOL.Text).ToString("dd-MMM-yy")
            'End If

            If e.Row.Cells(3).Text.Trim.Length > 0 Then
                If e.Row.Cells(3).Text.Trim <> "&nbsp;" Then
                    e.Row.Cells(3).Text = objeAAMS.ConvertDate(e.Row.Cells(3).Text).ToString("dd-MMM-yy")
                End If
            End If
            If e.Row.Cells(4).Text.Trim.Length > 0 Then
                If e.Row.Cells(4).Text.Trim <> "&nbsp;" Then
                    e.Row.Cells(4).Text = objeAAMS.ConvertDate(e.Row.Cells(4).Text).ToString("dd-MMM-yy")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdEmployee_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdEmployee.RowCommand
        'Code for Edit Data
        If e.CommandName = "EditX" Then
            Session("Action") = "U|" & e.CommandArgument.ToString.Trim & ""
            Response.Redirect("MSUP_Employee.aspx")
        End If
        'Code for Delete Date
        If e.CommandName = "DeleteX" Then
            EmployeeDelete(e.CommandArgument)
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdEmployee.AllowSorting = False
        grdEmployee.HeaderStyle.ForeColor = Drawing.Color.Black
        EmployeeExport()
    End Sub
    Private Sub EmployeeExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee

        'objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><Sec_Group_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        '@ Start Changed By Abhishek on 05-10-10 ' New Xml Input       
        'objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><Sec_Group_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><Sec_Group_ID/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/><INC></INC></MS_SEARCHEMPLOYEE_INPUT>")
        If Request.QueryString("ctrlId") IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("INC").InnerText = "T"
        End If
        '@ End of Changed By Abhishek on 05-10-10 ' New Xml Input    
        objInputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(txtEmployeeName.Text)
        If drpDepartment.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = drpDepartment.SelectedValue
        End If
        If drpAoffice.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
        End If
        If drplstDeig.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("Designation").InnerText = drplstDeig.SelectedItem.Text.Trim()
        End If
        If drplstPermission.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText = drplstPermission.SelectedValue.Trim()
        End If
        If drpAgreementSigned.SelectedValue <> "0" Then
            objInputXml.DocumentElement.SelectSingleNode("AgreementSigned").InnerText = drpAgreementSigned.SelectedValue.Trim()
        End If
        '    objInputXml.DocumentElement.SelectSingleNode("Request").InnerText = IIf(chkRequest.Checked = True, "1", "0")
        objInputXml.DocumentElement.SelectSingleNode("Request").InnerText = IIf(chkRequest.Checked = True, "1", "")
        If drplstSecGroup.SelectedValue <> "0" Then
            objInputXml.DocumentElement.SelectSingleNode("Sec_Group_ID").InnerText = drplstSecGroup.SelectedValue
        End If


        '@ New 

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
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = 1
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
            Else
                objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            End If
        End If

        'Start CODE for sorting and paging
 



        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "Employee_Name"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Employee_Name" '"LOCATION_CODE"
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
        objOutputXml = objbzEmployee.Search(objInputXml)
        Dim xnode As XmlNode
        Dim nl As XmlNodeList
        nl = objOutputXml.DocumentElement.SelectNodes("Employee")
        For Each xnode In nl
            Dim strdate, dateend As String
            strdate = xnode.Attributes("DateStart").Value
            dateend = xnode.Attributes("DateEnd").Value
            If strdate <> "" Then
                xnode.Attributes("DateStart").Value = objeAAMS.ConvertDate(strdate).ToString("dd-MMM-yy")
            End If
            If dateend <> "" Then
                xnode.Attributes("DateEnd").Value = objeAAMS.ConvertDate(dateend).ToString("dd-MMM-yy")
            End If
        Next
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml

            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            grdEmployee.DataSource = ds.Tables("Employee")
            grdEmployee.DataBind()
            Dim objExport As New ExportExcel
            ' Dim strArray() As String = {"Employee Name", "Aoffice", "Department", "DOJ", "Date of Leaving", "City "}
            'Dim intArray() As Integer = {1, 2, 3, 4, 5, 6}
            Dim strArray() As String = {"Employee Name", "Aoffice", "Department", "Date of Leaving", "City "}
            Dim intArray() As Integer = {1, 2, 3, 5, 6}

            objExport.ExportDetails(objOutputXml, "Employee", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportEmployee.xls")
            lblError.Text = ""
        Else
            grdEmployee.DataSource = Nothing
            grdEmployee.DataBind()
            txtTotalRecordCount.Text = "0"
            txtRecordOnCurrentPage.Text = "0"
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            pnlPaging.Visible = False
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
