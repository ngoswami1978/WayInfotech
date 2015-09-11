
Partial Class Setup_MSUP_EmployeeHelpDesk
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Protected strIndex As String
    Protected flagPermission As String
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt


#Region "Code for Filter "

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            btnSave.Attributes.Add("onclick", "return ValidateEmployeeHelpDesk();")
            '***************************************************************************************
            '*****************************Check Session*********************************************
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            CheckSecurity()
            '***************************************************************************************
            If Not Page.IsPostBack Then
                'Bind control for IPPool
                '
                ' objeAAMS.BindDropDown(drpIpPool, "IPPOOL", True)
                objeAAMS.BindDropDown(DlstFuncAssignTo, "TEAM", True, 1)
                objeAAMS.BindDropDown(DlstFuncConType, "CONTACTTYPE", True, 1)
                objeAAMS.BindDropDown(DlstTechAssignTo, "ASSIGNEDTOEMPLOYEE", True, 1)
                objeAAMS.BindDropDown(DlstTechConType, "CONTACTTYPE", True, 1)

                Bindata()
                If Session("Action") IsNot Nothing Then
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        ViewEmployeeHelpDesk()
                    End If
                End If
            End If

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-HelpDesk']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee-HelpDesk']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(2) = "0" Then
                    btnSave.Enabled = False
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
        If e.Item.ItemIndex = 5 Then
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
    Private Sub ViewEmployeeHelpDesk()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet

        Try

            If Session("Action") IsNot Nothing Then

                If Session("Action").ToString().Split("|").GetValue(1) IsNot Nothing Then

                    '<MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>
                    ' <EMPLOYEEID></EMPLOYEEID>
                    '</MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>

                    objInputXml.LoadXml("<MS_VIEWEMPLOYEE_HDDEFAULT_INPUT> <EMPLOYEEID></EMPLOYEEID></MS_VIEWEMPLOYEE_HDDEFAULT_INPUT>")
                    'Here Back end Method Call

                    Dim StrEmpId As String = Session("Action").ToString().Split("|").GetValue(1)
                    objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerXml = StrEmpId

                    objOutputXml = objbzEmployee.ViewHDDefault(objInputXml)


                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '<MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT>
                        ' <EMPLOYEE_HDDEFAULT EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' />
                        ' <Errors Status=''>
                        ' <Error Code='' Description='' />
                        ' </Errors>
                        '</MS_VIEWEMPLOYEE_HDDEFAULT_OUTPUT>

                        With objOutputXml.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT")
                            Dim LiFuncAssingnToItem As New ListItem
                            Dim LiFuncContactTypeItem As New ListItem
                            Dim LiTechAssingnToItem As New ListItem
                            Dim LiTechContactTypeItem As New ListItem
                            LiFuncAssingnToItem = DlstFuncAssignTo.Items.FindByValue(.Attributes("FUN_ASSIGNEDTO").Value.Trim)
                            LiFuncContactTypeItem = DlstFuncConType.Items.FindByValue(.Attributes("CONTACT_TYPE_ID").Value.Trim)
                            LiTechAssingnToItem = DlstTechAssignTo.Items.FindByValue(.Attributes("TEC_ASSIGNEDTO").Value.Trim)
                            LiTechContactTypeItem = DlstTechConType.Items.FindByValue(.Attributes("TEC_CONTACT_TYPE_ID").Value.Trim)

                            If LiFuncAssingnToItem IsNot Nothing Then
                                DlstFuncAssignTo.SelectedValue = LiFuncAssingnToItem.Value
                            End If
                            If LiFuncContactTypeItem IsNot Nothing Then
                                DlstFuncConType.SelectedValue = LiFuncContactTypeItem.Value
                            End If
                            If LiTechAssingnToItem IsNot Nothing Then
                                DlstTechAssignTo.SelectedValue = LiTechAssingnToItem.Value
                            End If

                            If LiTechContactTypeItem IsNot Nothing Then
                                DlstTechConType.SelectedValue = LiTechContactTypeItem.Value
                            End If

                        End With
                    Else
                        If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub
   

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument

        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        Try
            If Session("Action") IsNot Nothing Then

                If Session("Action").ToString().Split("|").GetValue(1) IsNot Nothing Then

                    Dim StrEmpId As String = Session("Action").ToString().Split("|").GetValue(1)

                    '<MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>
                    ' <EMPLOYEE_HDDEFAULT ACTION='' EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' />
                    '</MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>

                    objInputXml.LoadXml("<MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT><EMPLOYEE_HDDEFAULT ACTION='' EMPLOYEEID='' FUN_ASSIGNEDTO='' TEC_ASSIGNEDTO='' CONTACT_TYPE_ID='' TEC_CONTACT_TYPE_ID=''  ChangedBy='' /></MS_UPDATEEMPLOYEE_HDDEFAULT_INPUT>")

                    With objInputXml.DocumentElement.SelectSingleNode("EMPLOYEE_HDDEFAULT")
                        .Attributes("ACTION").Value = "U"
                        .Attributes("EMPLOYEEID").Value = StrEmpId
                        If DlstFuncAssignTo.SelectedValue <> "" Then
                            .Attributes("FUN_ASSIGNEDTO").Value = DlstFuncAssignTo.SelectedValue
                        End If
                        If DlstFuncConType.SelectedValue <> "" Then
                            .Attributes("CONTACT_TYPE_ID").Value = DlstFuncConType.SelectedValue
                        End If
                        If DlstTechAssignTo.SelectedValue <> "" Then
                            .Attributes("TEC_ASSIGNEDTO").Value = DlstTechAssignTo.SelectedValue
                        End If

                        If DlstTechConType.SelectedValue <> "" Then
                            .Attributes("TEC_CONTACT_TYPE_ID").Value = DlstTechConType.SelectedValue
                        End If

                        .Attributes("ChangedBy").Value = Session("LoginSession").ToString().Split("|")(0)  ''login user

                    End With

                    objOutputXml = objbzEmployee.UpdateHDDefault(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                        CheckSecurity()
                    Else

                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value

                    End If
                Else
                    lblError.Text = "Session is Expired."
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub
End Class
