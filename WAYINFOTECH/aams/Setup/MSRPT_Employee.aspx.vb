
Partial Class Setup_MSRPT_Employee
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder

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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        EmployeeList()
    End Sub
    Private Sub EmployeeList()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objSecurityXml As New XmlDocument
        'Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee

        ' objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned></MS_SEARCHEMPLOYEE_INPUT>")

        '@ Added By Abhishek on 06-01-08 ' New Xml Input
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
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

        objInputXml.DocumentElement.SelectSingleNode("AgreementSigned").InnerText = drpAgreementSigned.SelectedValue


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
        'Here Back end Method Call
        objOutputXml = objbzEmployee.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Session("eEmployeeListRpt") = objOutputXml.OuterXml 'objOxml.OuterXml
            Response.Redirect("../RPSR_ReportShow.aspx?Case=EmployeeList", False)
            lblError.Text = ""
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("PageName") = "Setup/MSRPT_Employee.aspx"
        Try
            '***************************************************************************************
            '***************************************************************************************
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpDepartment, "DepartmentName", True, 3)
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drplstDeig, "DESIGNATION", True, 3)
                objeAAMS.BindDropDown(drplstPermission, "PERMISSIONS", True, 3)
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

            If Session("Security") IsNot Nothing Then

                Dim objSecurityXml As New XmlDocument
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee List']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee List']").Attributes("Value").Value)
                        If strBuilder(1) = "0" Then
                        End If
                        If strBuilder(0) = "0" Then
                            btnSearch.Enabled = False
                            Response.Redirect("../NoRights.aspx")
                        End If
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                ' Code For Binding AOffice
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
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtEmployeeName.Text = String.Empty
        drpDepartment.SelectedIndex = 0
        'drpAoffice.SelectedIndex = 0
        drplstDeig.SelectedIndex = 0
        drplstPermission.SelectedIndex = 0
        drpAgreementSigned.SelectedValue = "3"
    End Sub
End Class
