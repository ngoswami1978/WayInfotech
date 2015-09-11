
Partial Class Setup_MSUP_EmployeeGroup
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt
    Dim objDTable As DataTable


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

            '  btnGrGrantAll.Attributes.Add("onclick", "return GroupGrantAll();")
            'btnGrRevokeAll.Attributes.Add("onclick", "return GroupRevokeAll();")
            Dim objSecurityXml As New XmlDocument
            btnGroupSearch.Attributes.Add("onclick", "return AGroupMandatory();")
            '***************************************************************************************
            '*****************************Check Session*********************************************
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Session("Action") Is Nothing Then
                Response.Redirect("../SupportPages/TimeOutSession.aspx?Logout=True")
            End If
            'security check.
            CheckSecurity()
            '***************************************************************************************
            If Not Page.IsPostBack Then
                'Bind control for Agency Group
                gvEmployeeGroup.PageSize = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
                objeAAMS.BindDropDown(drpGroupAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpGroupAgencyType, "AGROUP", True, 3)
                objeAAMS.BindDropDown(drpGroupCity, "CITY", True, 3)
                objeAAMS.BindDropDown(drpRegionList, "SECURITYREGION", True)
                Bindata()
                SearchAgencyGroupDefault()
                'If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                '    SearchAgencyGroupDefault()
                '    'ViewAgencyGroup()
                'Else
                '    SearchAgencyGroupDefault()
                'End If
            End If

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpGroupAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpGroupAoffice.SelectedValue = li.Value
                            End If
                        End If
                        drpGroupAoffice.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-ChainCode']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-ChainCode']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(0) = "0" Then
                    btnGroupSave.Enabled = False
                End If

                If strBuilder(2) = "0" Then
                    btnGroupSave.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Sub Bindata()
        Dim TabText As New ArrayList()
        TabText.Add("Employee")
        TabText.Add("Permission")
        TabText.Add("Group")
        TabText.Add("IP")
        TabText.Add("Supervisory")
        TabText.Add("HelpDesk")

        theTabStrip.DataSource = TabText
        theTabStrip.DataBind()
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 2 Then
            Button1.CssClass = "headingtab"
        End If
        If Session("Action").ToString().Split("|").GetValue(0) = "I" Then
            Button1.Enabled = False
        End If
        If Button1.Text = "Supervisory" Then
            Button1.Width = 80
        End If

        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Employee"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Permission"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Permission") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-ChainCode") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "IP"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Ip") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

                Case "Supervisory"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-Supervisory") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "HelpDesk"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Employee-HelpDesk") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If
        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return ColorMethod('" & Button1.ClientID.ToString() & "',6);")
    End Sub
  
    Protected Sub btnGroupSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroupSearch.Click
        Try

            SearchGroupPaging()
            'SearchAgencyGroup()
            ' ViewAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Sub BindXml()
        If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then
            Dim grpGridRow As GridViewRow
            Dim chkChainCode As CheckBox
            Dim objTempInputXml As New XmlDocument
            objTempInputXml.LoadXml(Session("Data"))
            For Each grpGridRow In gvEmployeeGroup.Rows
                'lblChainCode = grpGridItem.FindControl("lblChainCode")
                chkChainCode = grpGridRow.FindControl("chkChainCode")
                Dim objNode As XmlNode = objTempInputXml.DocumentElement.SelectSingleNode("GROUP[@Chain_Code='" + grpGridRow.Cells(0).Text + "']")

                If chkChainCode.Checked = True Then
                    objNode.Attributes("ChainSelected").Value = "TRUE"
                Else
                    objNode.Attributes("ChainSelected").Value = "FALSE"
                End If
            Next
            Session("Data") = objTempInputXml.OuterXml
        Else
            Response.Redirect("../SupportPages/TimeOutSession.aspx?Logout=True")
        End If
    End Sub

    'Sub BindData()
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Dim intPageSize As Integer
    '    Dim intRowCount As Integer = 0
    '    intPageSize = CInt(ConfigurationManager.AppSettings("PAGE_SIZE").ToString)
    '    Try

    '        objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")


    '        objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked

    '        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)

    '        objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
    '        objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
    '        If drpGroupCity.SelectedIndex <> 0 Then
    '            objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
    '        End If
    '        If (drpGroupAgencyType.SelectedIndex <> 0) Then
    '            objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
    '        End If

    '        If (drpGroupAoffice.SelectedIndex <> 0) Then
    '            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
    '        End If
    '        'Start CODE for sorting and paging

    '        If ViewState("PrevSearching") Is Nothing Then
    '            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
    '        Else
    '            Dim objTempInputXml As New XmlDocument
    '            Dim objNodeList As XmlNodeList

    '            objTempInputXml.LoadXml(ViewState("PrevSearching"))
    '            objNodeList = objTempInputXml.DocumentElement.ChildNodes
    '            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
    '            For Each objNode As XmlNode In objNodeList
    '                If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
    '                    If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
    '                        objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
    '                        ddlPageNumber.SelectedValue = "1"
    '                        SearchAgencyGroup()
    '                        Exit Sub
    '                    End If
    '                End If
    '            Next
    '        End If


    '        objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = intPageSize


    '        If ViewState("SortName") Is Nothing Then
    '            ViewState("SortName") = "Chain_Code"
    '            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Chain_Code"
    '        Else
    '            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
    '        End If

    '        If ViewState("Desc") Is Nothing Then
    '            ViewState("Desc") = "FALSE"
    '            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
    '        Else
    '            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
    '        End If

    '        'End Code for paging and sorting
    '        If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then

    '            'ViewState("PrevSearching") = objInputXml.OuterXml
    '            objOutputXml.LoadXml(Session("Data"))
    '            objXmlReader = New XmlNodeReader(objOutputXml)
    '            ds.ReadXml(objXmlReader)

    '            'Dim dc As New DataColumn("Chain_Code1", GetType(System.Int32))
    '            'ds.Tables("GROUP").Columns.Add(dc)
    '            'ds.Tables("GROUP").Columns("Chain_Code1").Expression = "Convert(Chain_Code,'System.Int32')"
    '            'ds.Tables("GROUP").Columns("Chain_Code1").ColumnName = "Chain_Code"

    '            Dim objTempOutputXml As New XmlDocument
    '            objTempOutputXml.LoadXml("<Root><GROUP Chain_Code='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected=''/></Root>")
    '            Dim dtTemp As New DataTable
    '            For i As Integer = 0 To objTempOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes.Count - 1
    '                dtTemp.Columns.Add(objTempOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes(i).Name)
    '            Next


    '            lblError.Text = ""
    '            'Code Added For Paging And Sorting In case Of Delete The Record
    '            pnlPaging.Visible = True
    '            intRowCount = ds.Tables("GROUP").Rows.Count
    '            Dim count As Integer = Math.Ceiling(intRowCount / intPageSize)
    '            Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
    '            If count <> ddlPageNumber.Items.Count Then
    '                ddlPageNumber.Items.Clear()
    '                For i As Integer = 1 To count
    '                    ddlPageNumber.Items.Add(i.ToString)
    '                Next
    '            End If
    '            ddlPageNumber.SelectedValue = selectedValue
    '            'Code for hiding prev and next button based on count
    '            If count = 1 Then
    '                'pnlPaging.Visible = False
    '                ' ddlPageNumber.Visible = False
    '                lnkNext.Visible = False
    '                lnkPrev.Visible = False
    '            Else
    '                'ddlPageNumber.Visible = True
    '                lnkPrev.Visible = True
    '                lnkNext.Visible = True
    '            End If

    '            'Code for hiding next button when pagenumber is equal to page count
    '            If ddlPageNumber.SelectedValue = count.ToString Then
    '                lnkNext.Visible = False
    '            Else
    '                lnkNext.Visible = True
    '            End If

    '            'Code for hiding prev button when pagenumber is 1
    '            If ddlPageNumber.SelectedValue = "1" Then
    '                lnkPrev.Visible = False
    '            Else
    '                lnkPrev.Visible = True
    '            End If
    '            hdRecordOnCurrentPage.Value = intRowCount
    '            txtTotalRecordCount.Text = intRowCount

    '            ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    '            ' @ Added Code To Show Image'

    '            Dim dv As DataView
    '            dv = ds.Tables("GROUP").DefaultView
    '            dv.Sort = ViewState("SortName") & " " & IIf(ViewState("Desc") = False, "Asc", "Desc")
    '            Dim PageNo As Integer
    '            PageNo = selectedValue
    '            Dim intStartRecordNo As Integer = 0
    '            Dim intEndRecordNo As Integer = 0
    '            ' Dim intTotalRecord As Integer = CInt(txtTotalRecordCount.Text)
    '            intStartRecordNo = (PageNo - 1) * intPageSize
    '            intEndRecordNo = PageNo * intPageSize - 1
    '            For j As Integer = intStartRecordNo To intEndRecordNo
    '                If intRowCount > j Then


    '                    Dim str() As Object
    '                    str = dv.Item(j).Row.ItemArray
    '                    Dim dr As DataRow
    '                    dr = dtTemp.NewRow
    '                    For k As Integer = 0 To str.Length - 2
    '                        dr(k) = str(k).ToString
    '                    Next
    '                    dtTemp.Rows.Add(dr)
    '                End If
    '            Next
    '            If dtTemp.Rows.Count > 0 Then
    '                gvEmployeeGroup.DataSource = dtTemp
    '                gvEmployeeGroup.DataBind()

    '                SetImageForSorting(gvEmployeeGroup)
    '                'Dim imgUp As New Image
    '                'imgUp.ImageUrl = "~/Images/Sortup.gif"
    '                'Dim imgDown As New Image
    '                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

    '                'Select Case ViewState("SortName")
    '                '    Case "Chain_Code1"
    '                '        Select Case ViewState("Desc")
    '                '            Case "FALSE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(0).Controls.Add(imgUp)
    '                '            Case "TRUE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(0).Controls.Add(imgDown)
    '                '        End Select
    '                '    Case "Aoffice"
    '                '        Select Case ViewState("Desc")
    '                '            Case "FALSE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(1).Controls.Add(imgUp)
    '                '            Case "TRUE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(1).Controls.Add(imgDown)
    '                '        End Select
    '                '    Case "Chain_Name"
    '                '        Select Case ViewState("Desc")
    '                '            Case "FALSE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(2).Controls.Add(imgUp)
    '                '            Case "TRUE"
    '                '                gvEmployeeGroup.HeaderRow.Cells(2).Controls.Add(imgDown)
    '                '        End Select
    '                'End Select


    '            End If
    '            '  Added Code To Show Image'

    '            ' End of Code Added For Paging And Sorting In case Of Delete The Record
    '        Else

    '            txtTotalRecordCount.Text = "0"
    '            hdRecordOnCurrentPage.Value = "0"
    '            pnlPaging.Visible = False
    '            gvEmployeeGroup.DataSource = Nothing
    '            gvEmployeeGroup.DataBind()
    '            ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    Sub BindDataDefault()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objNewRow As DataRow
        Dim intPageSize As Integer
        Dim intRowCount As Integer = 0
        intPageSize = CInt(ConfigurationManager.AppSettings("PAGE_SIZE").ToString)
        Try

            'End Code for paging and sorting
            If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then

                'ViewState("PrevSearching") = objInputXml.OuterXml
                objOutputXml.LoadXml(Session("Data"))
                ' objXmlReader = New XmlNodeReader(objOutputXml)
                '  ds.ReadXml(objXmlReader)

                '*******************************************************

                objDTable = New DataTable("GROUP")
                Dim objCol As DataColumn
                objCol = New DataColumn

                objCol = New DataColumn("Chain_Code")
                objCol.DataType = System.Type.GetType("System.Int32")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Chain_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("City_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("GroupTypeName")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Aoffice")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("ChainSelected")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Main")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("SecurityRegionIds")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("IsSelected")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                For Each objGrpNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")

                    objNewRow = objDTable.NewRow()
                    objNewRow("Chain_Code") = CInt(objGrpNode.Attributes("Chain_Code").InnerText)
                    objNewRow("Chain_Name") = objGrpNode.Attributes("Chain_Name").InnerText
                    objNewRow("City_Name") = objGrpNode.Attributes("City_Name").InnerText
                    objNewRow("GroupTypeName") = objGrpNode.Attributes("GroupTypeName").InnerText
                    objNewRow("Aoffice") = objGrpNode.Attributes("Aoffice").InnerText
                    objNewRow("ChainSelected") = objGrpNode.Attributes("ChainSelected").InnerText
                    objNewRow("Main") = objGrpNode.Attributes("Main").InnerText
                    objNewRow("SecurityRegionIds") = objGrpNode.Attributes("SecurityRegionIds").InnerText

                    ' objNewRow("IsSelected") = "" 'objGrpNode.Attributes("IsSelected").InnerText
                    objDTable.Rows.Add(objNewRow)

                Next
                objDTable.AcceptChanges()
                '*******************************************************


                'Dim dc As New DataColumn("Chain_Code1", GetType(System.Int32))
                'ds.Tables("GROUP").Columns.Add(dc)
                'ds.Tables("GROUP").Columns("Chain_Code1").Expression = "Convert(Chain_Code,'System.Int32')"
                'ds.Tables("GROUP").Columns("Chain_Code1").ColumnName = "Chain_Code"

                Dim objTempOutputXml As New XmlDocument
                objTempOutputXml.LoadXml("<Root><GROUP Chain_Code='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected='' Main='' SecurityRegionIds = ''/></Root>")
                Dim dtTemp As New DataTable
                For i As Integer = 0 To objTempOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes.Count - 1
                    dtTemp.Columns.Add(objTempOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes(i).Name)
                Next
                dtTemp.Columns("Chain_Code").DataType = Type.GetType("System.Int32")


                lblError.Text = ""
                'Code Added For Paging And Sorting In case Of Delete The Record
                pnlPaging.Visible = True
                intRowCount = objDTable.Rows.Count
                Dim count As Integer = Math.Ceiling(intRowCount / intPageSize)
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
                hdRecordOnCurrentPage.Value = intRowCount
                txtTotalRecordCount.Text = intRowCount

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'

                Dim dv As DataView
                dv = objDTable.DefaultView
                dv.Sort = ViewState("SortName") & " " & IIf(ViewState("Desc") = False, "Asc", "Desc")
                Dim PageNo As Integer
                PageNo = selectedValue
                Dim intStartRecordNo As Integer = 0
                Dim intEndRecordNo As Integer = 0
                ' Dim intTotalRecord As Integer = CInt(txtTotalRecordCount.Text)
                intStartRecordNo = (PageNo - 1) * intPageSize
                intEndRecordNo = PageNo * intPageSize - 1
                For j As Integer = intStartRecordNo To intEndRecordNo
                    If intRowCount > j Then


                        Dim str() As Object
                        str = dv.Item(j).Row.ItemArray
                        Dim dr As DataRow
                        dr = dtTemp.NewRow
                        For k As Integer = 0 To str.Length - 2

                            If k = 0 Then
                                dr(k) = CInt(str(k))
                            Else
                                dr(k) = str(k).ToString
                            End If
                        Next
                        dtTemp.Rows.Add(dr)
                    End If
                Next
                If dtTemp.Rows.Count > 0 Then
                    gvEmployeeGroup.DataSource = dtTemp
                    gvEmployeeGroup.DataBind()

                    SetImageForSorting(gvEmployeeGroup)
                    'Dim imgUp As New Image
                    'imgUp.ImageUrl = "~/Images/Sortup.gif"
                    'Dim imgDown As New Image
                    'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    'Select Case ViewState("SortName")
                    '    Case "Chain_Code1"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvEmployeeGroup.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvEmployeeGroup.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Aoffice"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvEmployeeGroup.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvEmployeeGroup.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Chain_Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvEmployeeGroup.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvEmployeeGroup.HeaderRow.Cells(2).Controls.Add(imgDown)
                    '        End Select
                    'End Select


                End If
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else

                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvEmployeeGroup.DataSource = Nothing
                gvEmployeeGroup.DataBind()
                ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
       
        End Try
    End Sub

    Sub BindDataSortPaging(ByVal objdv As DataView)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim intPageSize As Integer
        Dim intRowCount As Integer = 0
        Dim dtTemp As New DataTable
        intPageSize = CInt(ConfigurationManager.AppSettings("PAGE_SIZE").ToString)
        Try

            'End Code for paging and sorting
            If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then

                


                lblError.Text = ""
                'Code Added For Paging And Sorting In case Of Delete The Record
                pnlPaging.Visible = True
                intRowCount = objdv.Count
                Dim count As Integer = Math.Ceiling(intRowCount / intPageSize)
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
                hdRecordOnCurrentPage.Value = intRowCount
                txtTotalRecordCount.Text = intRowCount

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'

               
                Dim PageNo As Integer
                PageNo = selectedValue
                Dim intStartRecordNo As Integer = 0
                Dim intEndRecordNo As Integer = 0
                ' Dim intTotalRecord As Integer = CInt(txtTotalRecordCount.Text)
                intStartRecordNo = (PageNo - 1) * intPageSize
                intEndRecordNo = PageNo * intPageSize - 1
                For i As Integer = 0 To objdv.Table.Columns.Count - 1
                    dtTemp.Columns.Add(objdv.Table.Columns(i).ColumnName)
                Next
                For j As Integer = intStartRecordNo To intEndRecordNo
                    If intRowCount > j Then


                        Dim str() As Object
                        str = objdv.Item(j).Row.ItemArray
                        Dim dr As DataRow
                        dr = dtTemp.NewRow
                        For k As Integer = 0 To str.Length - 2
                            dr(k) = str(k).ToString
                        Next
                        dtTemp.Rows.Add(dr)
                    End If
                Next
                If dtTemp.Rows.Count > 0 Then
                    gvEmployeeGroup.DataSource = dtTemp
                    gvEmployeeGroup.DataBind()

                    SetImageForSorting(gvEmployeeGroup)
                    
                Else
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                    gvEmployeeGroup.DataSource = Nothing
                    gvEmployeeGroup.DataBind()
                    lblError.Text = "No Record found"

                End If
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else

                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                gvEmployeeGroup.DataSource = Nothing
                gvEmployeeGroup.DataBind()
                ' lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            gvEmployeeGroup.DataSource = Nothing
            gvEmployeeGroup.DataBind()
        End Try
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
    Sub SearchGroup(Optional ByVal strParameterSelected As String = "FALSE")
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim dv As DataView
        Dim objNewRow As DataRow
        Dim strMainGroup, strSearchString, strSelected, strSecurityRegionID As String
        Dim arChainCode As New ArrayList
        Try
            If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then
                objOutputXml.LoadXml(Session("Data"))
                ' objXmlReader = New XmlNodeReader(objOutputXml)
                ' ds.ReadXml(objXmlReader)
                '*******************************************************

                objDTable = New DataTable("GROUP")
                Dim objCol As DataColumn
                objCol = New DataColumn

                objCol = New DataColumn("Chain_Code")
                objCol.DataType = System.Type.GetType("System.Int32")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Chain_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("City_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("GroupTypeName")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Aoffice")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("ChainSelected")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Main")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("SecurityRegionIds")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("IsSelected")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                For Each objGrpNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")

                    objNewRow = objDTable.NewRow()
                    objNewRow("Chain_Code") = CInt(objGrpNode.Attributes("Chain_Code").InnerText)
                    objNewRow("Chain_Name") = objGrpNode.Attributes("Chain_Name").InnerText
                    objNewRow("City_Name") = objGrpNode.Attributes("City_Name").InnerText
                    objNewRow("GroupTypeName") = objGrpNode.Attributes("GroupTypeName").InnerText
                    objNewRow("Aoffice") = objGrpNode.Attributes("Aoffice").InnerText
                    objNewRow("ChainSelected") = objGrpNode.Attributes("ChainSelected").InnerText
                    objNewRow("Main") = objGrpNode.Attributes("Main").InnerText
                    objNewRow("SecurityRegionIds") = objGrpNode.Attributes("SecurityRegionIds").InnerText

                    'objNewRow("IsSelected") = "" 'objGrpNode.Attributes("IsSelected").InnerText
                    objDTable.Rows.Add(objNewRow)

                Next
                objDTable.AcceptChanges()

                '*******************************************************
                ' dv = ds.Tables("GROUP").DefaultView
                dv = objDTable.DefaultView
                'Chain_Code1='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected='' Main=''
                strMainGroup = IIf(chkMainGroup.Checked = True, 1, 0)
                If strMainGroup = 1 Then
                    strSearchString = "Main='" + strMainGroup + "'"
                Else
                    strSearchString = "1=1"
                End If

                If txtGroupChainCode.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Code='" + txtGroupChainCode.Text.Trim + "'"
                End If
                '                dv.RowFilter = "LoginName like  '%" + txtUserName.Text.Replace("'", "''") + "%'"
                If txtGroupName.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Name like '%" + txtGroupName.Text.Replace("'", "''").Trim + "%'"
                End If

                If drpGroupCity.SelectedIndex <> 0 Then
                    strSearchString = strSearchString + " and City_Name='" + drpGroupCity.SelectedItem.Text + "'"
                End If

                If (drpGroupAgencyType.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and GroupTypeName='" + drpGroupAgencyType.SelectedItem.Text + "'"
                End If

                If (drpGroupAoffice.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and Aoffice='" + drpGroupAoffice.SelectedValue + "'"
                End If

                If (drpRegionList.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and SecurityRegionIds like '%," + drpRegionList.Text.Trim + ",%'"
                End If

                strSelected = chkSelected.Checked
                If strSelected = "True" Then
                    strSearchString = strSearchString + " and ChainSelected='" + strSelected + "'"
                End If



                dv.RowFilter = strSearchString
                dv.Sort = ViewState("SortName") & " " & IIf(ViewState("Desc") = False, "Asc", "Desc")


                'code added on 12 mar 09
                'Now grant all only updates those records which satisfies searching criteria
                'Previosly it updates aal the records
                If strParameterSelected = "True" Then
                    'For i As Integer = 0 To dv.Count - 1
                    '    For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP[@Chain_Code='" + Convert.ToString(dv.Item(i).Row(0)).Trim + "']")
                    '        objNode.Attributes("ChainSelected").Value = "TRUE"
                    '    Next
                    'Next
                    For i As Integer = 0 To dv.Count - 1
                        'arChainCode.Add(dv.Item(i).Row(0))
                        arChainCode.Add(dv(i).Item("Chain_Code").ToString())
                    Next
                    For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")
                        If arChainCode.Contains(objNode.Attributes("Chain_Code").Value) Then
                            objNode.Attributes("ChainSelected").Value = "TRUE"
                        End If
                    Next

                    'For i As Integer = 0 To arChainCode.Count - 1
                    '    'For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP[@Chain_Code='" + i.ToString + "']")
                    '    '    objNode.Attributes("ChainSelected").Value = "TRUE"
                    '    'Next
                    '    objOutputXml.DocumentElement.SelectSingleNode("GROUP[@Chain_Code='" + arChainCode(i).ToString + "']").Attributes("ChainSelected").Value = "TRUE"

                    'Next
                    Session("Data") = objOutputXml.OuterXml
                End If
                'End Code

                objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><SecurityRegionIds/><IsSelected/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked
                If Session("Action") IsNot Nothing Then
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                End If
                objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
                objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
                If drpGroupCity.SelectedIndex <> 0 Then
                    objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
                End If
                If (drpGroupAgencyType.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
                End If

                If (drpGroupAoffice.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
                End If
                If (drpRegionList.SelectedIndex <> 0) Then
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionIds").InnerText = drpRegionList.SelectedValue
                End If
                objInputXml.DocumentElement.SelectSingleNode("IsSelected").InnerText = chkSelected.Checked
                ViewState("PrevSearching") = objInputXml.OuterXml

                BindDataSortPaging(dv)
            End If



        Catch ex As Exception
            Session("Data") = ""
            lblError.Text = ex.Message


        End Try
    End Sub

    Sub SearchGroupPaging()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim dv As DataView
        Dim objNewRow As DataRow
        Dim strMainGroup, strSearchString, strSelected As String
        Try
            If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then
                If ViewState("PrevSearching") Is Nothing Then
                    ddlPageNumber.SelectedIndex = 0
                Else
                    Dim objTempInputXml As New XmlDocument
                    Dim objNodeList As XmlNodeList
                    objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><IsSelected/><SecurityRegionIds/> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked
                    If Session("Action") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
                    objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
                    If drpGroupCity.SelectedIndex <> 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
                    End If
                    If (drpGroupAgencyType.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
                    End If

                    If (drpGroupAoffice.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
                    End If

                    If (drpRegionList.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionIds").InnerText = drpRegionList.SelectedValue
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("IsSelected").InnerText = chkSelected.Checked

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objTempInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                                SearchGroup()
                                Exit Sub
                            End If
                        End If
                    Next
                End If

                objOutputXml.LoadXml(Session("Data"))
                '*******************************************************

                objDTable = New DataTable("GROUP")
                Dim objCol As DataColumn
                objCol = New DataColumn

                objCol = New DataColumn("Chain_Code")
                objCol.DataType = System.Type.GetType("System.Int32")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Chain_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("City_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("GroupTypeName")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Aoffice")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("ChainSelected")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Main")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("SecurityRegionIds")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("IsSelected")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                For Each objGrpNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")

                    objNewRow = objDTable.NewRow()
                    objNewRow("Chain_Code") = CInt(objGrpNode.Attributes("Chain_Code").InnerText)
                    objNewRow("Chain_Name") = objGrpNode.Attributes("Chain_Name").InnerText
                    objNewRow("City_Name") = objGrpNode.Attributes("City_Name").InnerText
                    objNewRow("GroupTypeName") = objGrpNode.Attributes("GroupTypeName").InnerText
                    objNewRow("Aoffice") = objGrpNode.Attributes("Aoffice").InnerText
                    objNewRow("ChainSelected") = objGrpNode.Attributes("ChainSelected").InnerText
                    objNewRow("Main") = objGrpNode.Attributes("Main").InnerText
                    objNewRow("SecurityRegionIds") = objGrpNode.Attributes("SecurityRegionIds").InnerText
                    ' objNewRow("IsSelected") = "" 'objGrpNode.Attributes("IsSelected").InnerText
                    objDTable.Rows.Add(objNewRow)

                Next
                objDTable.AcceptChanges()
                '*******************************************************
                'objXmlReader = New XmlNodeReader(objOutputXml)
                ' ds.ReadXml(objXmlReader)
                'dv = ds.Tables("GROUP").DefaultView
                'Chain_Code1='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected='' Main=''

                'dv = New DataView(ds.Tables(0), "Price > 19", "Price Desc", DataViewRowState.CurrentRows)

                dv = objDTable.DefaultView

                strMainGroup = IIf(chkMainGroup.Checked = True, 1, 0)
                If strMainGroup = 1 Then
                    strSearchString = "Main='" + strMainGroup + "'"
                Else
                    strSearchString = "1=1"
                End If

                If txtGroupChainCode.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Code='" + txtGroupChainCode.Text.Trim + "'"
                End If
                If txtGroupName.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Name like '%" + txtGroupName.Text.Replace("'", "''").Trim + "%'"

                End If

                If drpGroupCity.SelectedIndex <> 0 Then
                    strSearchString = strSearchString + " and City_Name='" + drpGroupCity.SelectedItem.Text + "'"
                End If

                If (drpGroupAgencyType.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and GroupTypeName='" + drpGroupAgencyType.SelectedItem.Text + "'"
                End If

                If (drpGroupAoffice.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and Aoffice='" + drpGroupAoffice.SelectedValue + "'"
                End If

                If (drpRegionList.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and SecurityRegionIds like '%," + drpRegionList.Text.Trim + ",%'"
                End If

                strSelected = chkSelected.Checked
                If chkSelected.Checked = True Then
                    strSearchString = strSearchString + " and ChainSelected='" + strSelected + "'"
                End If

                dv.RowFilter = strSearchString
                dv.Sort = ViewState("SortName") & " " & IIf(ViewState("Desc") = False, "Asc", "Desc")
                ViewState("PrevSearching") = objInputXml.OuterXml
                BindDataSortPaging(dv)
            End If
        Catch ex As Exception
            Session("Data") = ""
            lblError.Text = ex.Message
            gvEmployeeGroup.DataSource = Nothing
            gvEmployeeGroup.DataBind()
        End Try
    End Sub
    'Sub SearchAgencyGroup()
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    ' Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Try
    '        Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
    '        'objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup></MS_SEARCHGROUP_INPUT>")
    '        If hdInputXml.Value = "" Then
    '            hdInputXml.Value = "<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>"
    '        End If
    '        objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")
    '        ' objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice></MS_SEARCHGROUP_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked

    '        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)

    '        objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
    '        objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
    '        If drpGroupCity.SelectedIndex <> 0 Then
    '            objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
    '        End If
    '        If (drpGroupAgencyType.SelectedIndex <> 0) Then
    '            objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
    '        End If

    '        If (drpGroupAoffice.SelectedIndex <> 0) Then
    '            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
    '        End If
    '        ddlPageNumber.SelectedValue = "1"


    '        ' Here Back end Method Call
    '        'Here Back end Method Call
    '        objOutputXml = objbzAgencyGroup.Search(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            ViewState("PrevSearching") = objInputXml.OuterXml
    '            Session("Data") = objOutputXml.OuterXml
    '            BindData()
    '            'objXmlReader = New XmlNodeReader(objOutputXml)
    '            'ds.ReadXml(objXmlReader)
    '            'Dim dv As DataView
    '            'dv = ds.Tables("GROUP").DefaultView
    '            'If ViewState("SortName") Is Nothing Then
    '            '    ViewState("SortName") = "Chain_Code"
    '            '    dv.Sort = "Chain_Code"
    '            'Else
    '            '    dv.Sort = ViewState("SortName")
    '            'End If

    '            'gvEmployeeGroup.DataSource = dv
    '            'gvEmployeeGroup.DataBind()

    '            ''  grdGroup.DataSource = ds.Tables("GROUP")
    '            ' grdGroup.DataBind()
    '        Else
    '            txtTotalRecordCount.Text = "0"
    '            hdRecordOnCurrentPage.Value = "0"
    '            pnlPaging.Visible = False
    '            Session("Data") = ""
    '            gvEmployeeGroup.DataSource = Nothing
    '            gvEmployeeGroup.DataBind()
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        Session("Data") = ""
    '        lblError.Text = ex.Message
    '    End Try
    'End Sub

    Sub SearchAgencyGroupDefault()
        Dim objInputXml, objOutputXml As New XmlDocument
        ' Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            'objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup></MS_SEARCHGROUP_INPUT>")

            hdInputXml.Value = "<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><SecurityRegionIds/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>"

            objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><IsSelected/><SecurityRegionIds/><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")

            ' objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice></MS_SEARCHGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = "False"
            objInputXml.DocumentElement.SelectSingleNode("IsSelected").InnerText = "False"
            If Session("Action") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
            End If

            ' objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
            ' objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
            'If drpGroupCity.SelectedIndex <> 0 Then
            'objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
            'End If
            ' If (drpGroupAgencyType.SelectedIndex <> 0) Then
            'objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
            'End If

            'If (drpGroupAoffice.SelectedIndex <> 0) Then
            ' objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
            ' End If
            ddlPageNumber.SelectedValue = "1"

            'Here Back end Method Call
            'Here Back end Method Call
            objOutputXml = objbzAgencyGroup.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                ViewState("PageLoadSearchingString") = objInputXml.OuterXml
                Session("Data") = objOutputXml.OuterXml

                ViewState("SortName") = "Chain_Code"
             
                ViewState("Desc") = "FALSE"

                BindDataDefault()

            Else
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                Session("Data") = ""
                gvEmployeeGroup.DataSource = Nothing
                gvEmployeeGroup.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            Session("Data") = ""
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnGroupSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroupSave.Click
        If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objIxml As New XmlDocument
            Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
            objIxml.LoadXml("<Document><Chain_Code></Chain_Code></Document>")
            objInputXml.LoadXml("<MS_UPDATEEMPLOYEEGROUP_INPUT><EmployeeID></EmployeeID><ChangedBy></ChangedBy></MS_UPDATEEMPLOYEEGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
            ''add by ashish
            objInputXml.DocumentElement.SelectSingleNode("ChangedBy").InnerText = Session("LoginSession").ToString().Split("|")(0)  ''login user
            ''end    

            Try
                Dim objTempInputXml As New XmlDocument
                '   Dim grpGridRow As GridViewRow
                '  Dim chkChainCode As CheckBox

                ' Dim lblChainCode As Label
                'Session("Data")
                BindXml()
                Dim objxmldocfrag As XmlDocumentFragment
                objTempInputXml.LoadXml(Session("Data"))
                For Each objNode As XmlNode In objTempInputXml.DocumentElement.SelectNodes("GROUP")
                    'lblChainCode = grpGridItem.FindControl("lblChainCode")
                    If objNode.Attributes("ChainSelected").Value.ToUpper = "TRUE" Then
                        objIxml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = objNode.Attributes("Chain_Code").Value
                        objxmldocfrag = objInputXml.CreateDocumentFragment()
                        objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                        objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                    End If
                Next
                'For Each grpGridRow In gvEmployeeGroup.Rows
                '    'lblChainCode = grpGridItem.FindControl("lblChainCode")
                '    chkChainCode = grpGridRow.FindControl("chkChainCode")
                '    If chkChainCode.Checked = True Then
                '        objIxml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = grpGridRow.Cells(0).Text
                '        objxmldocfrag = objInputXml.CreateDocumentFragment()
                '        objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                '        objInputXml.DocumentElement.AppendChild(objxmldocfrag)
                '    End If
                'Next
                'Here Back end Method Call
                objOutputXml = objbzEmployee.SaveGroupAssigned(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    Else
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    CheckSecurity()
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message.ToString
            End Try
        Else
            Response.Redirect("../SupportPages/TimeOutSession.aspx?Logout=True")
        End If
    End Sub


    Protected Sub btnGroupReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroupReset.Click
        txtGroupName.Text = String.Empty
        txtGroupChainCode.Text = String.Empty
        drpGroupAgencyType.SelectedIndex = 0
        drpGroupCity.SelectedIndex = 0
        drpGroupAoffice.SelectedIndex = 0
        SearchAgencyGroupDefault()
        'ViewAgencyGroup()
    End Sub
    Protected Function AsignValue(ByVal chkselectedvalue As Object) As Boolean
        If chkselectedvalue IsNot Nothing Then
            If chkselectedvalue.ToString() = "True" Then
                Return True
            Else
                Return False
            End If
        End If
        Return False
    End Function

    Protected Sub btnGrGrantAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrGrantAll.Click
        Try
            'Dim objTempInputXml As New XmlDocument
            'objTempInputXml.LoadXml(Session("Data"))
            'For Each objNode As XmlNode In objTempInputXml.DocumentElement.SelectNodes("GROUP")
            '    objNode.Attributes("ChainSelected").Value = "TRUE"
            'Next
            'Session("Data") = objTempInputXml.OuterXml

            GrantAllWithSearchingCriteria()
            Dim grpGridRow As GridViewRow
            For Each grpGridRow In gvEmployeeGroup.Rows
                CType(grpGridRow.FindControl("chkChainCode"), CheckBox).Checked = True
            Next
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub


    Sub GrantAllWithSearchingCriteria()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim dv As DataView
        Dim objNewRow As DataRow
        Dim strMainGroup, strSearchString, strSelected As String
        Dim objTempInputXml As New XmlDocument
        Dim objNodeList As XmlNodeList
        Dim arChainCode As New ArrayList
        Try
            If Session("Data") IsNot Nothing AndAlso Session("Data") <> "" Then
                If ViewState("PrevSearching") Is Nothing Then
                    ddlPageNumber.SelectedIndex = 0
                Else
                    
                    objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup><EmployeeID></EmployeeID><IsSelected/> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHGROUP_INPUT>")

                    objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked
                    If Session("Action") IsNot Nothing Then
                        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtGroupChainCode.Text
                    objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
                    If drpGroupCity.SelectedIndex <> 0 Then
                        objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpGroupCity.SelectedValue
                    End If
                    If (drpGroupAgencyType.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpGroupAgencyType.SelectedValue
                    End If

                    If (drpGroupAoffice.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpGroupAoffice.SelectedValue
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("IsSelected").InnerText = chkSelected.Checked

                    objTempInputXml.LoadXml(ViewState("PrevSearching"))
                    objNodeList = objInputXml.DocumentElement.ChildNodes
                    objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                    For Each objNode As XmlNode In objNodeList
                        If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                            If objNode.InnerText <> objTempInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                                ddlPageNumber.SelectedValue = "1"
                                SearchGroup("True")
                                Exit Sub
                            End If
                        End If
                    Next



                    objOutputXml.LoadXml(Session("Data"))




                    ''''''''''''''''


                    If ViewState("PageLoadSearchingString") IsNot Nothing Then
                        Dim blnCompare As Boolean = False
                        objTempInputXml = New XmlDocument
                        objTempInputXml.LoadXml(ViewState("PageLoadSearchingString"))

                        For Each objNode As XmlNode In objNodeList
                            If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                                If objNode.InnerText <> objTempInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                                    blnCompare = True
                                End If
                            End If
                        Next

                        If blnCompare = False Then
                            For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")
                                objNode.Attributes("ChainSelected").Value = "TRUE"
                            Next
                            Session("Data") = objOutputXml.OuterXml
                            Exit Sub
                        End If
                    End If
                End If
                ''''''''''''''''''''
                '*******************************************************

                objDTable = New DataTable("GROUP")
                Dim objCol As DataColumn
                objCol = New DataColumn

                objCol = New DataColumn("Chain_Code")
                objCol.DataType = System.Type.GetType("System.Int32")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Chain_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("City_Name")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("GroupTypeName")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Aoffice")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("ChainSelected")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                objCol = New DataColumn("Main")
                objCol.DataType = System.Type.GetType("System.String")
                objDTable.Columns.Add(objCol)

                'objCol = New DataColumn("IsSelected")
                'objCol.DataType = System.Type.GetType("System.String")
                'objDTable.Columns.Add(objCol)

                For Each objGrpNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")

                    objNewRow = objDTable.NewRow()
                    objNewRow("Chain_Code") = CInt(objGrpNode.Attributes("Chain_Code").InnerText)
                    objNewRow("Chain_Name") = objGrpNode.Attributes("Chain_Name").InnerText
                    objNewRow("City_Name") = objGrpNode.Attributes("City_Name").InnerText
                    objNewRow("GroupTypeName") = objGrpNode.Attributes("GroupTypeName").InnerText
                    objNewRow("Aoffice") = objGrpNode.Attributes("Aoffice").InnerText
                    objNewRow("ChainSelected") = objGrpNode.Attributes("ChainSelected").InnerText
                    objNewRow("Main") = objGrpNode.Attributes("Main").InnerText
                    ' objNewRow("IsSelected") = "" 'objGrpNode.Attributes("IsSelected").InnerText
                    objDTable.Rows.Add(objNewRow)

                Next
                objDTable.AcceptChanges()
                '*******************************************************
                'objXmlReader = New XmlNodeReader(objOutputXml)
                ' ds.ReadXml(objXmlReader)

                '   dv = ds.Tables("GROUP").DefaultView
                dv = objDTable.DefaultView
                'Chain_Code1='' Chain_Name='' City_Name='' GroupTypeName='' Aoffice='' ChainSelected='' Main=''
                strMainGroup = IIf(chkMainGroup.Checked = True, 1, 0)
                If strMainGroup = 1 Then
                    strSearchString = "Main='" + strMainGroup + "'"
                Else
                    strSearchString = "1=1"
                End If

                If txtGroupChainCode.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Code='" + txtGroupChainCode.Text.Trim + "'"
                End If
                If txtGroupName.Text.Trim <> "" Then
                    strSearchString = strSearchString + " and Chain_Name like '%" + txtGroupName.Text.Replace("'", "''").Trim + "%'"

                End If

                If drpGroupCity.SelectedIndex <> 0 Then
                    strSearchString = strSearchString + " and City_Name='" + drpGroupCity.SelectedItem.Text + "'"
                End If

                If (drpGroupAgencyType.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and GroupTypeName='" + drpGroupAgencyType.SelectedItem.Text + "'"
                End If

                If (drpGroupAoffice.SelectedIndex <> 0) Then
                    strSearchString = strSearchString + " and Aoffice='" + drpGroupAoffice.SelectedValue + "'"
                End If

                strSelected = chkSelected.Checked
                If chkSelected.Checked = True Then
                    strSearchString = strSearchString + " and ChainSelected='" + strSelected + "'"
                End If



                dv.RowFilter = strSearchString
                dv.Sort = ViewState("SortName") & " " & IIf(ViewState("Desc") = False, "Asc", "Desc")

                'code added on 12 mar 09
                'Now grant all only updates those records which satisfies searching criteria
                'Previosly it updates aal the records


                For i As Integer = 0 To dv.Count - 1
                    'arChainCode.Add(dv.Item(i).Row(0))
                    arChainCode.Add(dv(i).Item("Chain_Code").ToString())
                Next

                For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP")
                    If arChainCode.Contains(objNode.Attributes("Chain_Code").Value) Then
                        objNode.Attributes("ChainSelected").Value = "TRUE"
                    End If
                Next
                'For i As Integer = 0 To arChainCode.Count - 1
                '    'For Each objNode As XmlNode In objOutputXml.DocumentElement.SelectNodes("GROUP[@Chain_Code='" + i.ToString + "']")
                '    '    objNode.Attributes("ChainSelected").Value = "TRUE"
                '    'Next
                '    objOutputXml.DocumentElement.SelectSingleNode("GROUP[@Chain_Code='" + arChainCode(i).ToString + "']").Attributes("ChainSelected").Value = "TRUE"
                'Next

                'End Code
                Session("Data") = objOutputXml.OuterXml
                Session("Data") = objOutputXml.OuterXml
                ViewState("PrevSearching") = objInputXml.OuterXml



                BindDataSortPaging(dv)
            End If



        Catch ex As Exception
            Session("Data") = ""
            lblError.Text = ex.Message
            gvEmployeeGroup.DataSource = Nothing
            gvEmployeeGroup.DataBind()
        End Try
    End Sub

    Protected Sub btnGrRevokeAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrRevokeAll.Click
        Try
            Dim objTempInputXml As New XmlDocument
            objTempInputXml.LoadXml(Session("Data"))
            For Each objNode As XmlNode In objTempInputXml.DocumentElement.SelectNodes("GROUP")
                objNode.Attributes("ChainSelected").Value = "FALSE"
            Next
            Session("Data") = objTempInputXml.OuterXml
            Dim grpGridRow As GridViewRow
            For Each grpGridRow In gvEmployeeGroup.Rows
                CType(grpGridRow.FindControl("chkChainCode"), CheckBox).Checked = False
            Next
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If

            'For Each grpGridRow In gvEmployeeGroup.Rows
            '    'lblChainCode = grpGridItem.FindControl("lblChainCode")
            '    chkChainCode = grpGridRow.FindControl("chkChainCode")
            '    If chkChainCode.Checked = True Then
            '        objIxml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = grpGridRow.Cells(0).Text
            '        objxmldocfrag = objInputXml.CreateDocumentFragment()
            '        objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
            '        objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            '    End If
            'Next
            BindXml()
            SearchGroupPaging()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            'For Each grpGridRow In gvEmployeeGroup.Rows
            '    'lblChainCode = grpGridItem.FindControl("lblChainCode")
            '    chkChainCode = grpGridRow.FindControl("chkChainCode")
            '    If chkChainCode.Checked = True Then
            '        objIxml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = grpGridRow.Cells(0).Text
            '        objxmldocfrag = objInputXml.CreateDocumentFragment()
            '        objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
            '        objInputXml.DocumentElement.AppendChild(objxmldocfrag)
            '    End If
            'Next
            BindXml()
            SearchGroupPaging()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindXml()
            SearchGroupPaging()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvEmployeeGroup_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEmployeeGroup.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvEmployeeGroup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvEmployeeGroup.Sorting
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
            SearchGroupPaging()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
