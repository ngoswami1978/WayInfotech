
Partial Class BirdresHelpDesk_HDUP_ChangeStatus
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage

#Region "Code for Filter "
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                If Request.QueryString("HD_RE_ID") IsNot Nothing Then
                    hdHD_RE_ID_Multiple.Value = Request.QueryString("HD_RE_ID").ToString
                End If
                If Request.QueryString("QueryGroup") IsNot Nothing Then
                    If Request.QueryString("QueryGroup").ToString.ToUpper = "FUNCTIONAL" Then
                        objeAAMS.BindDropDown(ddlStatus, "ET_QueryStatusWithClosed", True, 1)
                    Else
                        objeAAMS.BindDropDown(ddlStatus, "ET_TSTATUSWITHCLOSED", True, 1)
                    End If
                End If
            End If

            txtRecordCount.Text = Convert.ToString(Val(hdHD_RE_ID_Multiple.Value.Split(",").Length))

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '        Dim objbzCall As New AAMS.bizETrackerHelpDesk.bzCall
        Dim objbzCall As New AAMS.bizETrackerHelpDesk.bzCall


        Dim objInputXml, objOutputXml As New XmlDocument
        Dim strQueryStatus As String
        Try

            strQueryStatus = ddlStatus.SelectedValue
            If strQueryStatus.Split("|").Length <> 2 Then
                lblError.Text = "Query Status is mandatory"
                Exit Sub
            End If

            If strQueryStatus.Split("|").GetValue(1) = "1" And txtSol.Text = "" Then
                lblError.Text = "Solution is mandatory"
                Exit Sub
            End If

            objInputXml.LoadXml("<HD_UPDATE_CALLREQUESTSTATUS_INPUT><DETAILS HD_STATUS_ID='' SOLUTION='' LTR='' EMPLOYEEID=''></DETAILS></HD_UPDATE_CALLREQUESTSTATUS_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("DETAILS")
                .Attributes("HD_STATUS_ID").Value = strQueryStatus.Split("|").GetValue(0)
                .Attributes("SOLUTION").Value = txtSol.Text
                .Attributes("LTR").Value = hdHD_RE_ID_Multiple.Value
                .Attributes("EMPLOYEEID").Value = objeAAMS.EmployeeID(Session("Security"))
            End With
            objOutputXml = objbzCall.UpdateHDStatus(objInputXml)


            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class

