Imports System.Data.SqlClient
Imports System.Data
Imports System.Xml
Partial Class Setup_MSUP_Designation
    Inherits System.Web.UI.Page
#Region "Information"
    'Mukund Kumar
#End Region
#Region "Global Reference Declaration"
    Dim objDT As System.Data.DataTable
    Dim objDR As System.Data.DataRow
    Dim objeAams As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt

#End Region

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



#Region "frmUpdateManageDesignation_Load()"
    Protected Sub frmUpdateManageDesignation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmUpdateManageDesignation.Load
        Try
            Session("PageName") = "Setup/MSUP_Designation.aspx"
            objeAams.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAams.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument

            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Count <> 0 Then
                    strBuilder = objeAams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = False
                    Else
                        btnSave.Enabled = True
                    End If
                End If
                If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = True
                    Else
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAams.SecurityCheck(31)
            End If


            btnSave.Attributes.Add("onclick", "return chkFields();")
            optEmployee.Attributes.Add("onclick", "return optionSelection();")
            optDesignation.Attributes.Add("onclick", "return optionSelection();")
            btnNew.Attributes.Add("onclick", "return NewFunction()")
            ViewState("ColumnVal") = ""

            If Not Page.IsPostBack Then
                objeAams.BindDropDown(cboDesignation, "DESIGNATION", True)
                objeAams.BindDropDown(cboEmployee, "EMPLOYEE", True)
                bindDesigTxtUpdate()
                bindPermission()
                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToString().Trim() = "U" Then
                        ReflectPermission(objEn.Decrypt(Request.QueryString("DesignationId").Trim()))
                    End If
                    If Request.QueryString("Action").ToString().Trim() = "US" Then
                        ReflectPermission(objEn.Decrypt(Request.QueryString("DesignationId").Trim()))
                        lblError.Text = objeAAMSMessage.messInsert

                        '********************************************Security**********
                        objSecurityXml.LoadXml(Session("Security"))
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Count <> 0 Then
                                strBuilder = objeAams.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Designation']").Attributes("Value").Value)
                            End If
                            If strBuilder(2) = "0" Then
                                btnSave.Enabled = False
                            End If
                        Else
                            strBuilder = objeAams.SecurityCheck(31)
                        End If
                        '********************************************Security**********

                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "btnReset_Click()"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().ToString() = "U" Then
                    ReflectPermission(objEn.Decrypt(Request.QueryString("DesignationId").Trim()))
                End If
            Else
                Response.Redirect("MSUP_Designation.aspx")
            End If

            
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "btnSave_Click()"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim count As Integer = 0
        Dim valView As Integer = 1
        Dim valAdd As Integer = 2
        Dim valModi As Integer = 4
        Dim valDel As Integer = 8
        Dim valPrnt As Integer = 16

        Try
            If (Page.IsValid) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzDesignation As New AAMS.bizMaster.bzDesignation
                Dim objParentNode, objChildNode, objChildNodeClone As XmlNode
                objInputXml.LoadXml("<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation=''><SECURITY SecurityOptionID='' Value='' /></DESIGNATION></MS_UPDATEDESIGNATION_INPUT>")
                objParentNode = objInputXml.DocumentElement.SelectSingleNode("DESIGNATION")
                objParentNode.Attributes("DesignationID").Value = objEn.Decrypt(Request.QueryString("DesignationID"))
                objParentNode.Attributes("Designation").Value = txtDesignation.Text.Trim()
                Dim chkSecOptView As New CheckBox
                Dim chkSecOptAdd As New CheckBox
                Dim chkSecOptModi As New CheckBox
                Dim chkSecOptDel As New CheckBox
                Dim chkSecOptPrnt As New CheckBox
                objChildNode = objInputXml.DocumentElement.SelectSingleNode("DESIGNATION/SECURITY")
                objChildNodeClone = objChildNode.CloneNode(True)
                objParentNode.RemoveChild(objChildNode)
                For count = 0 To grdPermission.Rows.Count - 1
                    Dim SecurityValue As Integer = 0

                    chkSecOptView = CType(grdPermission.Rows(count).Cells(2).FindControl("chkView"), CheckBox)
                    chkSecOptAdd = CType(grdPermission.Rows(count).Cells(3).FindControl("chkAdd"), CheckBox)
                    chkSecOptModi = CType(grdPermission.Rows(count).Cells(4).FindControl("chkModify"), CheckBox)
                    chkSecOptDel = CType(grdPermission.Rows(count).Cells(5).FindControl("chkDelete"), CheckBox)
                    chkSecOptPrnt = CType(grdPermission.Rows(count).Cells(6).FindControl("chkPrint"), CheckBox)

                    If chkSecOptView.Checked = True Then
                        SecurityValue = SecurityValue + valView
                    End If

                    If chkSecOptAdd.Checked = True Then
                        SecurityValue = SecurityValue + valAdd
                    End If

                    If chkSecOptModi.Checked = True Then
                        SecurityValue = SecurityValue + valModi
                    End If

                    If chkSecOptDel.Checked = True Then
                        SecurityValue = SecurityValue + valDel
                    End If

                    If chkSecOptPrnt.Checked = True Then
                        SecurityValue = SecurityValue + valPrnt
                    End If

                    If SecurityValue <> 0 Then
                        objChildNodeClone.Attributes("Value").Value = SecurityValue
                        objChildNodeClone.Attributes("SecurityOptionID").Value = grdPermission.Rows(count).Cells(7).Text ' CType(grdPermission.Rows(count).Cells(7).FindControl("SGname"), HiddenField).Value
                        objParentNode.AppendChild(objChildNodeClone)
                        objChildNodeClone = objChildNode.CloneNode(True)
                    End If
                Next
                objOutputXml = objbzDesignation.Update(objInputXml)
                Dim strDeisgID As String = objOutputXml.DocumentElement("DESIGNATION").Attributes("DesignationID").Value.Trim()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Not Request.QueryString("DesignationID") Is Nothing Then
                        If Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US" Then
                            lblError.Text = "Record Updated Successfully."
                            ReflectPermission(objEn.Decrypt(Request.QueryString("DesignationID").Trim()))
                        End If
                    Else
                        Response.Redirect("MSUP_Designation.aspx?Action=US&DesignationId=" + objEn.Encrypt(strDeisgID))
                        lblError.Text = "Record Added Successfully."
                        'objeAams.BindDropDown(cboDesignation, "DESIGNATION", True)
                        'ReflectPermission(strDeisgID)
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "btnNew_Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "bindPermission()"
    Public Sub bindPermission()
        Try
            Dim objbzDesignation As New AAMS.bizMaster.bzPermission
            Dim objInputXml As New XmlDocument
            Dim objOutPutXml As New XmlDocument
            Dim xmlNodeRdr As XmlNodeReader
            Dim dSet As New DataSet
            objOutPutXml = objbzDesignation.List()
            If (objOutPutXml.DocumentElement("Errors").Attributes("Status").Value.ToUpper() = "FALSE") Then
                xmlNodeRdr = New XmlNodeReader(objOutPutXml)
                dSet.ReadXml(xmlNodeRdr)
                grdPermission.DataSource = dSet.Tables("SECURITY_OPTION").DefaultView
                grdPermission.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "grdPermission_RowDataBound()"
    Protected Sub grdPermission_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPermission.RowDataBound
        'Following segment of code is written for 
        Try
            Dim str As String = e.Row.Cells(0).Text.Trim()
            If (ViewState("ColumnVal").ToString() <> str) Then
                ViewState("ColumnVal") = e.Row.Cells(0).Text.Trim()
                Return
            Else
                e.Row.Cells(0).Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "bindDesigTxtUpdate()"
    Public Sub bindDesigTxtUpdate()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objLoadData As New AAMS.bizMaster.bzDesignation
            If Not Request.QueryString("DesignationID") Is Nothing Then

                objInputXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerText = objEn.Decrypt(Request.QueryString("DesignationID").Trim())
                objOutputXml = objLoadData.View(objInputXml)
                txtDesignation.Text = objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").Value.Trim()

            End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "btnApply_Click()"
    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim node As XmlNode
            Dim bzMSPermission As New AAMS.bizMaster.bzDesignation
            Dim bzMSPermissionEmp As New AAMS.bizMaster.bzEmployee
            Dim count As Integer

            If optEmployee.Checked = True Then
                objInputXml.LoadXml("<MS_VIEWEMPLOYEEPERMISSION_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEEPERMISSION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = cboEmployee.SelectedValue.Trim()
                objOutputXml = bzMSPermissionEmp.GetPermissions(objInputXml)
                'For count = 0 To grdPermission.Rows.Count - 1
                '    ' Clear Check Boxes.
                '    CType(grdPermission.Rows(count).Cells(2).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(3).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(4).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(5).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(6).Controls(1), CheckBox).Checked = False
                'Next

                For Each node In objOutputXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION")
                    For count = 0 To grdPermission.Rows.Count - 1
                        Dim strSecOptId As String = grdPermission.Rows(count).Cells(7).Text ' CType(grdPermission.Rows(count).Cells(7).FindControl("SGname"), HiddenField).Value
                        If strSecOptId = node.Attributes("SecurityOptionID").InnerText Then
                            SetValues(Convert.ToInt32(node.Attributes("Value").InnerText), count)
                        End If
                    Next
                Next


            ElseIf (optDesignation.Checked = True) Then
                objInputXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerText = cboDesignation.SelectedValue.Trim()
                objOutputXml = bzMSPermission.View(objInputXml)

                'For count = 0 To grdPermission.Rows.Count - 1
                '    ' Clear Check Boxes.
                '    CType(grdPermission.Rows(count).Cells(2).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(3).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(4).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(5).Controls(1), CheckBox).Checked = False
                '    CType(grdPermission.Rows(count).Cells(6).Controls(1), CheckBox).Checked = False
                'Next

                For Each node In objOutputXml.DocumentElement.SelectNodes("DESIGNATION/SECURITY")
                    For count = 0 To grdPermission.Rows.Count - 1
                        Dim strSecOptId As String = grdPermission.Rows(count).Cells(7).Text ' CType(grdPermission.Rows(count).Cells(7).FindControl("SGname"), HiddenField).Value
                        If strSecOptId = node.Attributes("SecurityOptionID").InnerText Then
                            SetValues(Convert.ToInt32(node.Attributes("Value").InnerText), count)
                        End If
                    Next
                Next

            End If
        Catch ex As Exception

        End Try
        
    End Sub
#End Region

#Region "SetValues()"
    Private Sub SetValues(ByVal SecId As Integer, ByVal RowNo As Integer)
        Select Case SecId
            Case 1
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
            Case 2, 3
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
            Case 4, 5
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
            Case 6, 7
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
            Case 8, 9
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
            Case 10, 11
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
            Case 12, 13
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
            Case 14, 15
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
            Case 16, 17
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 18, 19
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 20, 21
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 22, 23
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 24, 25
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 26, 27
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 28, 29
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True
            Case 30, 31
                CType(grdPermission.Rows(RowNo).Cells(2).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(3).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(4).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(5).Controls(1), CheckBox).Checked = True
                CType(grdPermission.Rows(RowNo).Cells(6).Controls(1), CheckBox).Checked = True

        End Select
    End Sub
#End Region

#Region "ReflectPermission()"
    Private Sub ReflectPermission(ByVal desigID As String)
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim node As XmlNode
            Dim bzMSPermission As New AAMS.bizMaster.bzDesignation
            Dim bzMSPermissionEmp As New AAMS.bizMaster.bzEmployee
            Dim count As Integer
            objInputXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerText = desigID
            objOutputXml = bzMSPermission.View(objInputXml)
            For count = 0 To grdPermission.Rows.Count - 1
                ' Clear Check Boxes.
                CType(grdPermission.Rows(count).Cells(2).Controls(1), CheckBox).Checked = False
                CType(grdPermission.Rows(count).Cells(3).Controls(1), CheckBox).Checked = False
                CType(grdPermission.Rows(count).Cells(4).Controls(1), CheckBox).Checked = False
                CType(grdPermission.Rows(count).Cells(5).Controls(1), CheckBox).Checked = False
                CType(grdPermission.Rows(count).Cells(6).Controls(1), CheckBox).Checked = False
            Next
            For Each node In objOutputXml.DocumentElement.SelectNodes("DESIGNATION/SECURITY")
                For count = 0 To grdPermission.Rows.Count - 1
                    Dim strSecOptId As String = grdPermission.Rows(count).Cells(7).Text ' CType(grdPermission.Rows(count).Cells(7).FindControl("SGname"), HiddenField).Value
                    If strSecOptId = node.Attributes("SecurityOptionID").InnerText Then
                        SetValues(Convert.ToInt32(node.Attributes("Value").InnerText), count)
                    End If
                Next
            Next
            txtDesignation.Text = objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").InnerText
            objeAams.BindDropDown(cboDesignation, "DESIGNATION", True)
            cboDesignation.SelectedValue = desigID
        Catch ex As Exception

        End Try
            End Sub
#End Region

    'Modified on 5-Feb 2008 by Mukund
    Protected Sub btnGrantAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrantAll.Click
        Try

            Dim counter As Integer = 0
            For counter = 0 To grdPermission.Rows.Count - 1
                CType(grdPermission.Rows(counter).FindControl("chkView"), CheckBox).Checked = True
                CType(grdPermission.Rows(counter).FindControl("chkAdd"), CheckBox).Checked = True
                CType(grdPermission.Rows(counter).FindControl("chkModify"), CheckBox).Checked = True
                CType(grdPermission.Rows(counter).FindControl("chkDelete"), CheckBox).Checked = True
                CType(grdPermission.Rows(counter).FindControl("chkPrint"), CheckBox).Checked = True
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Modified on 5-Feb 2008 by Mukund
    Protected Sub btnRevokeAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevokeAll.Click
        Try

            Dim counter As Integer = 0
            For counter = 0 To grdPermission.Rows.Count - 1
                CType(grdPermission.Rows(counter).FindControl("chkView"), CheckBox).Checked = False
                CType(grdPermission.Rows(counter).FindControl("chkAdd"), CheckBox).Checked = False
                CType(grdPermission.Rows(counter).FindControl("chkModify"), CheckBox).Checked = False
                CType(grdPermission.Rows(counter).FindControl("chkDelete"), CheckBox).Checked = False
                CType(grdPermission.Rows(counter).FindControl("chkPrint"), CheckBox).Checked = False
            Next
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
