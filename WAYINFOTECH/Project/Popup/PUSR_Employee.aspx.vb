
Partial Class Popup_PUSR_Employee
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Public objeAAMS As New eAAMS
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
        '***************************************************************************************
        '***************************************************************************************
        If Not Page.IsPostBack Then
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
        Dim objXmlReader As XmlNodeReader
        Dim objSecurityXml As New XmlDocument
        Dim ds As New DataSet
        Dim objbzEmployee As New WAY.bizMaster.bzEmployee

        ' objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned></MS_SEARCHEMPLOYEE_INPUT>")
        '@ Added By Neeraj on 06-01-08 ' New Xml Input
        objInputXml.LoadXml("<MS_SEARCHEMPLOYEE_INPUT><Employee_Name></Employee_Name><DepartmentID></DepartmentID><Aoffice></Aoffice> <Designation></Designation><SecurityOptionID></SecurityOptionID><AgreementSigned></AgreementSigned><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><SecurityRegionID></SecurityRegionID></MS_SEARCHEMPLOYEE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Employee_Name").InnerText = Trim(txtEmployeeName.Text)
        
        If drplstPermission.SelectedIndex <> 0 Then
            objInputXml.DocumentElement.SelectSingleNode("SecurityOptionID").InnerText = drplstPermission.SelectedValue.Trim()
        End If

        
        If Session("Security") IsNot Nothing Then
            objSecurityXml.LoadXml(Session("Security"))
        End If

        'Here Back end Method Call
        objOutputXml = objbzEmployee.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            grdEmployee.DataSource = ds.Tables("Employee")
            grdEmployee.DataBind()
            lblError.Text = ""
        Else
            grdEmployee.DataSource = String.Empty
            grdEmployee.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Protected Sub grdEmployee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdEmployee.ItemCommand
        'Code for Edit Data
        If e.CommandName = "SelectX" Then
            Dim strjscript As String = "<script language=""javascript"">"
            strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
            strjscript = strjscript & "</script" & ">"
            Me.litEmployee.Text = strjscript
        End If
    End Sub
End Class
