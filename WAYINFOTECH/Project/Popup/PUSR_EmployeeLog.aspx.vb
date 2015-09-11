
Partial Class Popup_PUSR_EmployeeLog
    Inherits System.Web.UI.Page
    Dim objInputXml, objOutputXml As New XmlDocument
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim TmpDT As New DataTable("Filter")

#Region "Code for Filter "
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region


#Region "Code page load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        objeAAMS.ExpirePageCache()
        Call WorkEmployeeHistroy()
    End Sub
#End Region


#Region "Work Employee History"
    Private Sub WorkEmployeeHistroy()
        '1=Employee
        '2=Group
        '3=IP
        '4=Supervisory
        '5=Helpdesk
        pnlEmployeeDetails.Visible = False
        pnlGroup.Visible = False
        pnlIPAddress.Visible = False
        pnlSupervisor.Visible = False
        pnlHelpDesk.Visible = False
        lblError.Visible = False
        TmpDT = Nothing
        Try
            ''Load InputXML
            objInputXml.LoadXml("<MS_EMPLOYEEHISTORY_INPUT><EMPLOYEEID></EMPLOYEEID></MS_EMPLOYEEHISTORY_INPUT>")
            If Not Page.IsPostBack Then
                If Not Session("LoginSession") Is Nothing Then
                    Dim objbzEmployee As New WAY.bizMaster.bzEmployee
                    objInputXml.DocumentElement.SelectSingleNode("EMPLOYEEID").InnerText = Session("Action").ToString().Split("|").GetValue(1)
                    ''call back
                    'objOutputXml = objbzEmployee.GetEmployeeHistory(objInputXml)
                    'objOutputXml.Load("C:\EMP_HIS.XML")
                    ViewState("emp_his") = objOutputXml.OuterXml
                End If
            End If

            If drpSelect.SelectedValue = 1 Then
                Call POPULATE_EmployeeDetails()
            ElseIf drpSelect.SelectedValue = 2 Then
                Call POPULATE_Group()
            ElseIf drpSelect.SelectedValue = 3 Then
                Call POPULATE_IP()
            ElseIf drpSelect.SelectedValue = 4 Then
                Call POPULATE_SUPERVISOR()
            ElseIf drpSelect.SelectedValue = 5 Then
                Call POPULATE_HELPDESK()
            End If
        Catch ex As Exception
            lblError.Visible = True
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


#Region "Filter"
    Private Sub filter_table(ByVal num As Integer)
        Dim ArrDR() As DataRow
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet

        Try
            objOutputXml.LoadXml(ViewState("emp_his"))
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                ArrDR = ds.Tables(2).Select("HISTORYID=" & num & "")
                TmpDT = ds.Tables(2).Clone

                If ArrDR.Length > 0 Then
                    For i As Integer = 0 To ArrDR.Length - 1
                        TmpDT.ImportRow(ArrDR(i))
                    Next
                Else
                    lblError.Visible = True
                    Select Case num
                        Case 1
                            lblError.Text = "No Record Found"
                            pnlEmployeeDetails.Visible = False
                        Case 2
                            lblError.Text = "No Record Found"
                            pnlGroup.Visible = False
                        Case 3
                            lblError.Text = "No Record Found"
                            pnlIPAddress.Visible = False
                        Case 4
                            lblError.Text = "No Record Found"
                            pnlSupervisor.Visible = False
                        Case 5
                            lblError.Text = "No Record Found"
                            pnlHelpDesk.Visible = False
                    End Select
                End If
            Else
                lblError.Visible = True
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

            Select Case num
                Case 1
                    lblEmployeeDetailsError.Text = ex.Message
                Case 2
                    lblGroupError.Text = ex.Message
                Case 3
                    lblIPError.Text = ex.Message
                Case 4
                    lblSupervisorError.Text = ex.Message
                Case 5
                    lblHelpDeskError.Text = ex.Message
            End Select
        End Try

    End Sub
#End Region


#Region "hide and unhide panel"
    Private Sub POPULATE_EmployeeDetails()
        Try

            pnlEmployeeDetails.Visible = True
            Call filter_table(1)
            grdEmployeeDetails.DataSource = TmpDT
            grdEmployeeDetails.DataBind()

            pnlGroup.Visible = False
            pnlIPAddress.Visible = False
            pnlSupervisor.Visible = False
            pnlHelpDesk.Visible = False
        Catch ex As Exception
            lblEmployeeDetailsError.Text = ex.Message
        End Try
    End Sub
    Private Sub POPULATE_Group()
        Try
            pnlGroup.Visible = True
            Call filter_table(2)
            grdEmployeeGroup.DataSource = TmpDT
            grdEmployeeGroup.DataBind()

            pnlEmployeeDetails.Visible = False
            pnlIPAddress.Visible = False
            pnlSupervisor.Visible = False
            pnlHelpDesk.Visible = False
        Catch ex As Exception
            lblGroupError.Text = ex.Message
        End Try
    End Sub
    Private Sub POPULATE_IP()
        Try
            pnlIPAddress.Visible = True
            Call filter_table(3)
            grdIPAddress.DataSource = TmpDT
            grdIPAddress.DataBind()

            pnlEmployeeDetails.Visible = False
            pnlGroup.Visible = False
            pnlSupervisor.Visible = False
            pnlHelpDesk.Visible = False
        Catch ex As Exception
            lblIPError.Text = ex.Message
        End Try

    End Sub
    Private Sub POPULATE_SUPERVISOR()
        Try

            pnlSupervisor.Visible = True
            Call filter_table(4)
            grdSupervisor.DataSource = TmpDT
            grdSupervisor.DataBind()

            pnlEmployeeDetails.Visible = False
            pnlGroup.Visible = False
            pnlIPAddress.Visible = False
            pnlHelpDesk.Visible = False
        Catch ex As Exception
            lblSupervisorError.Text = ex.Message
        End Try

    End Sub
    Private Sub POPULATE_HELPDESK()
        Try
            pnlHelpDesk.Visible = True
            Call filter_table(5)
            grdHelpDesk.DataSource = TmpDT
            grdHelpDesk.DataBind()

            pnlEmployeeDetails.Visible = False
            pnlGroup.Visible = False
            pnlIPAddress.Visible = False
            pnlSupervisor.Visible = False
        Catch ex As Exception
            lblHelpDeskError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class
