
Partial Class Setup_MSUP_EmployeePermission
    Inherits System.Web.UI.Page
    'Inherits BasePage
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
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
            lblError.Text = ""

            btnPerGrantAll.Attributes.Add("onclick", "return PermissionGrantAll();")
            btnPerRevokeAll.Attributes.Add("onclick", "return PermissionRevokeAll();")
            ' btnApply.Attributes.Add("onclick", "return ValidatApply();")
            '***************************************************************************************
            '*****************************Check Session*********************************************
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            'security check.
            CheckSecurity()
            '***************************************************************************************
            If Not Page.IsPostBack Then
                If Session("Action") IsNot Nothing Then
                    hdEnEmployeeID.Value = objEn.Encrypt(Session("Action").ToString().Split("|").GetValue(1))
                End If
                If Session("Action").ToString().Split("|").GetValue(0) = "I" Then
                    BindPermission()
                Else
                    ViewPermission()
                End If
            End If
            If Not Page.IsPostBack Then
                'Bind Control for Permission
                objeAAMS.BindDropDown(drpDesignation, "DESIGNATION", True)
                objeAAMS.BindDropDown(drpPermissionEmployee, "EMPLOYEE", True)
                Bindata()
            End If
            rdCopyPermissionDesignation.Attributes.Add("onclick", "return optionSelection();")
            rdCopyPermissionEmployee.Attributes.Add("onclick", "return optionSelection();")
        Catch ex As Exception
            lblError.Text = ex.Message
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Permission']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-Permission']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(0) = "0" Then
                    btnPermissionSave.Enabled = False
                End If

                If strBuilder(2) = "0" Then
                    btnPermissionSave.Enabled = False
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
        If e.Item.ItemIndex = 1 Then
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
    Private Sub BindPermission()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim bzPermission As New AAMS.bizMaster.bzPermission
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            'Here Back end Method Call
            objOutputXml = bzPermission.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvPermission.DataSource = ds.Tables("SECURITY_OPTION")
                gvPermission.DataBind()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            ds = Nothing
            bzPermission = Nothing
        End Try
    End Sub
    Private Sub ViewPermission()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("Action").ToString().Split("|").GetValue(1)
        'Here Back end Method Call
        BindPermission()
        objOutputXml = objbzEmployee.GetPermissions(objInputXml)
        Session("ViewEmployeeData") = Session("ViewEmployeeData").ToString().Remove(2, 1).Insert(2, 1)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objOutputXml = EmployeePermission(objOutputXml)

            'Dim permGridItem As DataGridItem
            Dim permGridItem As GridViewRow
            Dim chkView, chkAdd, chkModify, chkDelete, chkPrint As CheckBox
            ' Dim hdSubCategory As HiddenField
            Dim strSubCategory As String
            'For Each permGridItem In grdPermission.Items
            For Each permGridItem In gvPermission.Rows
                strSubCategory = permGridItem.Cells(8).Text
                'hdSubCategory = permGridItem.FindControl("hdSubCategory")
                chkView = permGridItem.FindControl("chkView")
                chkAdd = permGridItem.FindControl("chkAdd")
                chkModify = permGridItem.FindControl("chkModify")
                chkDelete = permGridItem.FindControl("chkDelete")
                chkPrint = permGridItem.FindControl("chkPrint")
                If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowView='1']").Count <> 0 Then
                    chkView.Checked = True
                End If
                If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowAdd='1']").Count <> 0 Then
                    chkAdd.Checked = True
                End If
                If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowModify='1']").Count <> 0 Then
                    chkModify.Checked = True
                End If
                If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowDelete='1']").Count <> 0 Then
                    chkDelete.Checked = True
                End If
                If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowPrint='1']").Count <> 0 Then
                    chkPrint.Checked = True
                End If
            Next
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Function EmployeePermission(ByVal objOutputXml As XmlDocument) As XmlDocument
        Dim strSecurity As String = "<SECURITY_OPTIONS></SECURITY_OPTIONS>"
        Dim strSecurityDetail As String = "<SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' AllowView='' AllowAdd='' AllowModify='' AllowDelete='' AllowPrint='' /></SECURITY_OPTIONS>"
        Dim objIxml As New XmlDocument
        Dim objOxml As New XmlDocument
        Dim objNodeList As XmlNodeList
        Dim objNode As XmlNode
        Dim objxmldocfrag As XmlDocumentFragment
        Try
            objIxml.LoadXml(strSecurityDetail)
            objOxml.LoadXml(strSecurity)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
            For Each objNode In objNodeList
                Dim ViewRight As String = "0"
                Dim AddRight As String = "0"
                Dim ModifyRight As String = "0"
                Dim DeleteRight As String = "0"
                Dim PrintRight As String = "0"
                'Permission Right
                Permission(objNode.Attributes("Value").Value, ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight)

                With objIxml.DocumentElement.SelectSingleNode("SECURITY_OPTION")
                    .Attributes("SecurityOptionID").Value = objNode.Attributes("SecurityOptionID").Value
                    .Attributes("AllowView").Value = ViewRight
                    .Attributes("AllowAdd").Value = AddRight
                    .Attributes("AllowModify").Value = ModifyRight
                    .Attributes("AllowDelete").Value = DeleteRight
                    .Attributes("AllowPrint").Value = PrintRight
                End With
                objxmldocfrag = objOxml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objOxml.DocumentElement.AppendChild(objxmldocfrag)
            Next
            Return objOxml
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            Return objOxml
        End Try
    End Function

    Function DesignationPermission(ByVal objOutputXml As XmlDocument) As XmlDocument
        Dim strSecurity As String = "<SECURITY_OPTIONS></SECURITY_OPTIONS>"
        Dim strSecurityDetail As String = "<SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' AllowView='' AllowAdd='' AllowModify='' AllowDelete='' AllowPrint='' /></SECURITY_OPTIONS>"
        Dim objIxml As New XmlDocument
        Dim objOxml As New XmlDocument
        Dim objNodeList As XmlNodeList
        Dim objNode As XmlNode
        Dim objxmldocfrag As XmlDocumentFragment
        Try
            objIxml.LoadXml(strSecurityDetail)
            objOxml.LoadXml(strSecurity)
            objNodeList = objOutputXml.DocumentElement.SelectNodes("DESIGNATION/SECURITY")
            For Each objNode In objNodeList
                Dim ViewRight As String = "0"
                Dim AddRight As String = "0"
                Dim ModifyRight As String = "0"
                Dim DeleteRight As String = "0"
                Dim PrintRight As String = "0"
                'Permission Right
                Permission(objNode.Attributes("Value").Value, ViewRight, AddRight, ModifyRight, DeleteRight, PrintRight)

                With objIxml.DocumentElement.SelectSingleNode("SECURITY_OPTION")
                    .Attributes("SecurityOptionID").Value = objNode.Attributes("SecurityOptionID").Value
                    .Attributes("AllowView").Value = ViewRight
                    .Attributes("AllowAdd").Value = AddRight
                    .Attributes("AllowModify").Value = ModifyRight
                    .Attributes("AllowDelete").Value = DeleteRight
                    .Attributes("AllowPrint").Value = PrintRight
                End With
                objxmldocfrag = objOxml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objOxml.DocumentElement.AppendChild(objxmldocfrag)
            Next
            Return objOxml
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
            Return objOxml
        End Try
    End Function
    Sub Permission(ByVal intValue As Integer, ByRef ViewRight As String, ByRef AddRight As String, ByRef ModifyRight As String, ByRef DeleteRight As String, ByRef PrintRight As String)
        Try
            Select Case intValue
                Case 1
                    ViewRight = "1"
                Case 2, 3
                    ViewRight = "1"
                    AddRight = "1"
                Case 4, 5
                    ViewRight = "1"
                    ModifyRight = "1"
                Case 6, 7
                    ViewRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 8, 9
                    ViewRight = "1"
                    DeleteRight = "1"
                Case 10, 11
                    ViewRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 12, 13
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 14, 15
                    ViewRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
                Case 16, 17
                    ViewRight = "1"
                    PrintRight = "1"
                Case 18, 19
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                Case 20, 21
                    ViewRight = "1"
                    PrintRight = "1"
                    ModifyRight = "1"
                Case 22, 23
                    ViewRight = "1"
                    PrintRight = "1"
                    AddRight = "1"
                    ModifyRight = "1"
                Case 24, 25
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                Case 26, 27
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    AddRight = "1"
                Case 28, 29
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                Case 30, 31
                    ViewRight = "1"
                    PrintRight = "1"
                    DeleteRight = "1"
                    ModifyRight = "1"
                    AddRight = "1"
            End Select
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
        Try
            ' BindPermission()
            If rdCopyPermissionDesignation.Checked = True Then
                objInputXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>")
                If drpDesignation.SelectedIndex = 0 Then
                    lblError.Text = "Select Designation"
                    Exit Sub
                End If
                objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerXml = drpDesignation.SelectedValue
                'Here Back end Method Call

                objOutputXml = objbzDesignation.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objOutputXml = DesignationPermission(objOutputXml)

                    'Dim permGridItem As DataGridItem
                    Dim permGridItem As GridViewRow
                    Dim chkView, chkAdd, chkModify, chkDelete, chkPrint As CheckBox
                    'Dim hdSubCategory As HiddenField
                    Dim strSubCategory As String
                    '                 For Each permGridItem In grdPermission.Items
                    For Each permGridItem In gvPermission.Rows
                        'hdSubCategory = permGridItem.FindControl("hdSubCategory")
                        strSubCategory = permGridItem.Cells(8).Text
                        chkView = permGridItem.FindControl("chkView")
                        chkAdd = permGridItem.FindControl("chkAdd")
                        chkModify = permGridItem.FindControl("chkModify")
                        chkDelete = permGridItem.FindControl("chkDelete")
                        chkPrint = permGridItem.FindControl("chkPrint")
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowView='1']").Count <> 0 Then
                            chkView.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowAdd='1']").Count <> 0 Then
                            chkAdd.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowModify='1']").Count <> 0 Then
                            chkModify.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowDelete='1']").Count <> 0 Then
                            chkDelete.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowPrint='1']").Count <> 0 Then
                            chkPrint.Checked = True
                        End If
                    Next
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            ElseIf rdCopyPermissionEmployee.Checked = True Then
                objInputXml.LoadXml("<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>")
                If drpPermissionEmployee.SelectedIndex = 0 Then
                    lblError.Text = "Select Employee"
                    Exit Sub
                End If
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = drpPermissionEmployee.SelectedValue
                'Here Back end Method Call
                objOutputXml = objbzEmployee.GetPermissions(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objOutputXml = EmployeePermission(objOutputXml)

                    'Dim permGridItem As DataGridItem
                    Dim permGridItem As GridViewRow
                    Dim chkView, chkAdd, chkModify, chkDelete, chkPrint As CheckBox
                    'Dim hdSubCategory As HiddenField
                    Dim strSubCategory As String
                    For Each permGridItem In gvPermission.Rows
                        'hdSubCategory = permGridItem.FindControl("hdSubCategory")
                        strSubCategory = permGridItem.Cells(8).Text
                        chkView = permGridItem.FindControl("chkView")
                        chkAdd = permGridItem.FindControl("chkAdd")
                        chkModify = permGridItem.FindControl("chkModify")
                        chkDelete = permGridItem.FindControl("chkDelete")
                        chkPrint = permGridItem.FindControl("chkPrint")
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowView='1']").Count <> 0 Then
                            chkView.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowAdd='1']").Count <> 0 Then
                            chkAdd.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowModify='1']").Count <> 0 Then
                            chkModify.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowDelete='1']").Count <> 0 Then
                            chkDelete.Checked = True
                        End If
                        If objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTION[@SecurityOptionID='" & strSubCategory & "'][@AllowPrint='1']").Count <> 0 Then
                            chkPrint.Checked = True
                        End If
                    Next
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    '**************************************************************************************************************
    'Save Permission Information
    '**************************************************************************************************************
    Protected Sub btnPermissionSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPermissionSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objIxml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objIxml.LoadXml("<SECURITY_OPTIONS><SECURITY_OPTION SecurityOptionID='' Value='' /></SECURITY_OPTIONS>")
        objInputXml.LoadXml("<MS_UPDATEEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID><LoginEmployeeID/><SECURITY_OPTIONS></SECURITY_OPTIONS></MS_UPDATEEMPLOYEEPERMISSION_INPUT>")
        Try
            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("Action").ToString().Split("|").GetValue(1)

            objInputXml.DocumentElement.SelectSingleNode("LoginEmployeeID").InnerText = objeAAMS.EmployeeID(Session("Security"))

            'Dim permissionGridItem As DataGridItem
            Dim permissionGridItem As GridViewRow
            Dim chkView, chkAdd, chkModify, chkDelete, chkPrint As CheckBox
            'Dim hdSubCategory As HiddenField
            Dim strSubCategory As String
            Dim intValue As Integer
            Dim strView, strAdd, strModify, strDelete, strPrint As String
            Dim objxmldocfrag As XmlDocumentFragment

            'For Each permissionGridItem In grdPermission.Items
            For Each permissionGridItem In gvPermission.Rows
                strSubCategory = permissionGridItem.Cells(8).Text
                chkView = permissionGridItem.FindControl("chkView")
                chkAdd = permissionGridItem.FindControl("chkAdd")
                chkModify = permissionGridItem.FindControl("chkModify")
                chkDelete = permissionGridItem.FindControl("chkDelete")
                chkPrint = permissionGridItem.FindControl("chkPrint")
                If chkView.Checked = True Then
                    strView = "1"
                Else
                    strView = "0"
                End If
                If chkAdd.Checked = True Then
                    strAdd = "2"
                Else
                    strAdd = "0"
                End If
                If chkModify.Checked = True Then
                    strModify = "4"
                Else
                    strModify = "0"
                End If
                If chkDelete.Checked = True Then
                    strDelete = "8"
                Else
                    strDelete = "0"
                End If
                If chkPrint.Checked = True Then
                    strPrint = "16"
                Else
                    strPrint = "0"
                End If
                intValue = CallculatePermission(strView, strAdd, strModify, strDelete, strPrint)

                'objIxml.DocumentElement.SelectSingleNode("SECURITY_OPTION").Attributes("SecurityOptionID").Value = hdSubCategory.Value
                objIxml.DocumentElement.SelectSingleNode("SECURITY_OPTION").Attributes("SecurityOptionID").Value = strSubCategory
                objIxml.DocumentElement.SelectSingleNode("SECURITY_OPTION").Attributes("Value").Value = intValue

                objxmldocfrag = objInputXml.CreateDocumentFragment()
                objxmldocfrag.InnerXml = objIxml.DocumentElement.InnerXml
                objInputXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS").AppendChild(objxmldocfrag)
            Next
            'Here Back end Method Call
            objOutputXml = objbzEmployee.SavePermissions(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                End If
                'security check.
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Function CallculatePermission(ByVal ViewRight As String, ByVal AddRight As String, ByVal ModifyRight As String, ByVal DeleteRight As String, ByVal PrintRight As String) As Integer
        Dim intValue As Integer
        intValue = CInt(ViewRight) + CInt(AddRight) + CInt(ModifyRight) + CInt(DeleteRight) + CInt(PrintRight)
        Return intValue
    End Function

    'Function CallculatePermission(ByVal ViewRight As String, ByVal AddRight As String, ByVal ModifyRight As String, ByVal DeleteRight As String, ByVal PrintRight As String) As Integer
    '    Dim intValue As Integer
    '    If ViewRight = "1" And PrintRight = "1" And DeleteRight = "1" And ModifyRight = "1" And AddRight = "1" Then
    '        intValue = 31
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "1" And ModifyRight = "1" And AddRight = "0" Then
    '        intValue = 29
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "1" And ModifyRight = "0" And AddRight = "1" Then
    '        intValue = 27
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "1" And ModifyRight = "0" And AddRight = "0" Then
    '        intValue = 25
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "0" And ModifyRight = "1" And AddRight = "1" Then
    '        intValue = 23
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "0" And ModifyRight = "1" And AddRight = "0" Then
    '        intValue = 21
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "0" And ModifyRight = "0" And AddRight = "1" Then
    '        intValue = 19
    '    ElseIf ViewRight = "1" And PrintRight = "1" And DeleteRight = "0" And ModifyRight = "0" And AddRight = "0" Then
    '        intValue = 17
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "1" And ModifyRight = "1" And AddRight = "1" Then
    '        intValue = 15
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "1" And ModifyRight = "1" And AddRight = "0" Then
    '        intValue = 13
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "1" And ModifyRight = "0" And AddRight = "1" Then
    '        intValue = 11
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "1" And ModifyRight = "0" And AddRight = "0" Then
    '        intValue = 9
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "0" And ModifyRight = "1" And AddRight = "1" Then
    '        intValue = 7
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "0" And ModifyRight = "1" And AddRight = "0" Then
    '        intValue = 5
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "0" And ModifyRight = "0" And AddRight = "1" Then
    '        intValue = 3
    '    ElseIf ViewRight = "1" And PrintRight = "0" And DeleteRight = "0" And ModifyRight = "0" And AddRight = "0" Then
    '        intValue = 1
    '    End If
    '    Return intValue
    'End Function
    Protected Sub btnPermissionReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPermissionReset.Click
        ViewPermission()
    End Sub
    'Protected Sub grdPermission_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdPermission.ItemDataBound
    '    Try
    '        'Dim lblCategory As Label
    '        'lblCategory = e.Item.FindControl("lblCategory")
    '        If e.Item.ItemIndex < 0 Then
    '            Exit Sub
    '        End If
    '        Dim strCategory As String
    '        strCategory = DataBinder.Eval(e.Item.DataItem, "Sec_Group")
    '        If (flagPermission <> strCategory) Then
    '            flagPermission = strCategory
    '            Return
    '        Else
    '            e.Item.Cells(0).Text = String.Empty
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub gvPermission_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPermission.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim strCategory As String
        strCategory = DataBinder.Eval(e.Row.DataItem, "Sec_Group")

        Dim img As Image = New Image()
        img = CType(e.Row.FindControl("ImgPermission"), Image)

        'img.Attributes.Add("onmouseover", "javascript:return showenlargeimage(this);")
        'img.Attributes.Add("onmouseout", "javascript:return showenSmallimage(this);")

        If img IsNot Nothing Then
            img.Attributes.Add("onmouseover", "Large(this)")
            img.Attributes.Add("onmouseout", "Out(this)")
            img.Attributes.Add("onmousemove", "Move(this,event)")
        End If

        If (flagPermission <> strCategory) Then
            flagPermission = strCategory
            Return
        Else
            e.Row.Cells(1).Text = String.Empty
        End If
    End Sub
End Class
