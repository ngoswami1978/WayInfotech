
Imports System.Xml
Imports System.Data
Partial Class Setup_MSUP_Department
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
            Session("PageName") = "Setup/MSUP_Department.aspx"
            'objeAAMS.ExpirePageCache()

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            CheckSecurity()
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpManagerName, "ManagerName", True, 2)

            End If
            'Check Condition for Action Update or Insert
            If Not Page.IsPostBack Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    DepartmentView()
                End If
                '***************************************************************************************
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    DepartmentView()
                End If
            End If

            btnReset.Attributes.Add("onclick", "return DepartmentReset();")
            btnSave.Attributes.Add("onclick", "return DepartmentMandatory();")

            If Not Page.IsPostBack Then
                ' objeAAMS.BindDropDown(drpManagerName, "ManagerName", True)
                If Request.QueryString("Action").ToUpper = "U" Then
                    DepartmentView()
                End If
            End If

            '***************************************************************************************



            '***************************************************************************************

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Department']").Attributes("Value").Value)
                    'If strBuilder(1) = "0" Then
                    '    btnNew.Enabled = False
                    '    btnSave.Enabled = False
                    'End If
                    'If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                    '    btnSave.Enabled = False
                    'End If
                    'If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                    '    If Request.QueryString("Action").ToString() = "U" Then
                    '        btnSave.Enabled = False
                    '    Else
                    '        btnSave.Enabled = True
                    '    End If
                    'End If
                    'If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                    '    If Request.QueryString("Action").ToString() = "U" Then
                    '        btnSave.Enabled = True
                    '    Else
                    '        btnSave.Enabled = False
                    '    End If
                    'End If

                    'When View rights disabled
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                    End If

                    If strBuilder(0) = "0" Then
                        btnSave.Enabled = False
                    End If
                    'When Add rights disabled
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    'When modify rights disabled and Add rights enabled
                    If strBuilder(2) = "0" And (Request.QueryString("Action").ToString() = "U") Then
                        btnSave.Enabled = False
                    End If
                    'When modify rights Enabled and Add rights disabled
                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        btnSave.Enabled = True
                    End If


                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Sub BindDepartmentData(ByVal objOutputXml As XmlDocument)
        Try
            Dim li As ListItem
            Dim intCount As Integer = 0
            txtDepartment.Text = objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("Department_Name").InnerXml
            If objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").InnerXml <> "0" Then
                li = drpManagerName.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").Value())
                If (li IsNot Nothing) Then
                    drpManagerName.SelectedValue = li.Value
                Else
                    intCount = drpManagerName.Items.Count
                    drpManagerName.Items.Insert(intCount, New ListItem(objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerName").Value(), objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").Value()))
                    drpManagerName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").Value()
                End If
            Else
                drpManagerName.SelectedValue = "" ' objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").InnerXml
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub DepartmentView()
        Try
            Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            objInputXml.LoadXml("<MS_VIEWDEPARTMENT_INPUT><DepartmentID></DepartmentID></MS_VIEWDEPARTMENT_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = Request.QueryString("DepartmentId").ToString()
            objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = objEn.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString.Trim)
            ViewState("DepartmentID") = objEn.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString.Trim)

            'Here Back end Method Call
            objOutputXml = objbzDepartment.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                BindDepartmentData(objOutputXml)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("MSUP_Department.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzDepartment As New AAMS.bizMaster.bzDepartment
        objInputXml.LoadXml("<MS_UPDATEDEPARTMENT_INPUT><DEPARTMENT Action='' DepartmentID='' Department_Name='' ManagerID='' /></MS_UPDATEDEPARTMENT_INPUT>") 'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Request.QueryString("Aoffice")
        Try
            With objInputXml.DocumentElement.SelectSingleNode("DEPARTMENT")
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                    .Attributes("Action").Value() = "U"
                    .Attributes("DepartmentID").Value() = ViewState("DepartmentID")
                Else
                    .Attributes("Action").Value() = "I"
                End If
                .Attributes("Department_Name").Value() = txtDepartment.Text
                ' If drpManagerName.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("ManagerID").Value = drpManagerName.SelectedValue
                ' End If
            End With

            'Here Back end Method Call
            objOutputXml = objbzDepartment.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                    lblError.Text = "Updated successfully."
                Else
                    Response.Redirect("MSUP_Department.aspx?Action=US|" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("DEPARTMENT").Attributes("DepartmentID").Value.Trim))
                End If
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                DepartmentView()
            Else
                txtDepartment.Text = ""
                drpManagerName.SelectedIndex = 0
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
