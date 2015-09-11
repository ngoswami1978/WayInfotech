Imports System.Data
Imports System.Xml
Imports System.Data.SqlClient
Partial Class Setup_HDSR_EmployeeR
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("PageName") = "HelpDesk/HDSR_EmployeeR.aspx"
        Try
            '***************************************************************************************
            '***************************************************************************************
            btnSelect.Visible = False
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpDepartment, "DepartmentName", True, 3)
                objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
               
                If Not Request.QueryString("Aoffice") Is Nothing Then
                    'Response.Write(Request.QueryString("Aoffice").Trim())
                    If Request.QueryString("Aoffice").Trim() <> "" Then
                        drpAoffice.SelectedValue = Request.QueryString("Aoffice").Trim()
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
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then

                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Employee']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSearch.Enabled = False
                    End If
                    'Code End for security
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            ex.Message.ToString()
        End Try
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

        ' objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned></MS_SEARCHEMPLOYEE_INPUT>")

        '@ Added By Abhishek on 06-01-08 ' New Xml Input
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID><Request/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(txtEmployeeName.Text)
        If drpDepartment.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("DepartmentID").InnerText = drpDepartment.SelectedValue
        End If
        If drpAoffice.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpAoffice.SelectedValue
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



        'Here Back end Method Call
        objOutputXml = objbzEmployee.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            btnSelect.Visible = True
            gvEmployee.DataSource = ds.Tables("Employee")
            gvEmployee.DataBind()
            lblError.Text = ""
        Else
            gvEmployee.DataSource = Nothing
            gvEmployee.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    '*******************************************************************************************************
    Private Sub EmployeeDelete(ByVal strEmployeeId As String)
        Dim objInputXml As New XmlDocument
        Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
        objInputXml.LoadXml("<MS_DELETEEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_DELETEEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = strEmployeeId
        'Here Back end Method Call

        objInputXml = objbzEmployee.Delete(objInputXml)

        If objInputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            EmployeeSearch()
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objInputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub


    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtEmployeeName.Text = String.Empty
        drpDepartment.SelectedIndex = 0
        drpAoffice.SelectedIndex = 0
        gvEmployee.DataSource = Nothing
        gvEmployee.DataBind()
    End Sub


    Protected Sub gvEmployee_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEmployee.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        'e.Row.Cells(4).Text = objeAAMS.GetDateFormat(DataBinder.Eval(e.Row.DataItem, "DateStart"), "yyyyMMdd", "dd-MMM-yy", "")
        ' e.Row.Cells(5).Text = objeAAMS.GetDateFormat(DataBinder.Eval(e.Row.DataItem, "DateEnd"), "yyyyMMdd", "dd-MMM-yy", "/")
        ' Dim chk As New HtmlInputCheckBox
        ' chk = CType(e.Row.FindControl("chkSelect"), HtmlInputCheckBox)
        '  Dim strId As String
        ' strId = "I|" & DataBinder.Eval(e.Row.DataItem, "EmployeeID")
        ' strId = strId & "|" & DataBinder.Eval(e.Row.DataItem, "Employee_Name")
        'If hdDataEmployee.Value = "" Then
        '    hdDataEmployee.Value = "I|" & strId & "|" & strName
        'Else
        '    hdDataEmployee.Value = hdDataEmployee.Value & "," & "I|" & strId & "|" & strName
        'End If
        'If hdStartId.Value = "" Then
        '    hdStartId.Value = chk.ClientID
        'End If
        'hdEndId.Value = chk.ClientID
        'chk.Attributes.Add("abc", "'" + strId + "'")
        ' Dim hdDataID As New HtmlInputHidden
        'hdDataID = CType(e.Row.FindControl("hdDataID"), HtmlInputHidden)
        'hdDataID.Value = "I|"+
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
