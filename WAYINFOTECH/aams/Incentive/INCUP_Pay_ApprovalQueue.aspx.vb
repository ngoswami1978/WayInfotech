
Partial Class Incentive_INCUP_Pay_ApprovalQueue
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                objeAAMS.BindDropDown(ddlIncentiveStatus, "INCENTIVE_STATUS", True, 1)

                ' Checking Query String for update .
                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("BC_ID") Is Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("BC_ID").ToString().Trim())
                    If Not Request.QueryString("PREV_BC_ID") Is Nothing And Request.QueryString("PREV_BC_ID") <> "" Then
                        hdPrevBCID.Value = objED.Decrypt(Request.QueryString("PREV_BC_ID").ToString().Trim())
                    End If
                    ViewDetails()
                End If

            End If

            ' Checking security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
        Try
            '<UP_VIEW_INC_APPROVAL_QUE_OUTPUT>
            '	<DETAILS BC_ID='' PREVIOUS_BC_ID='' BCLEVEL ='' EMPLOYEEID='' EMPLOYEENAME='' APPROVAL_STATUS_ID ='' APPROVAL_STATUS_NAME = '' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</UP_VIEW_INC_APPROVAL_QUE_OUTPUT>
            objInputXml.LoadXml("<UP_VIEW_INC_APPROVAL_QUE_INPUT><BC_ID></BC_ID><PREVIOUS_BC_ID></PREVIOUS_BC_ID></UP_VIEW_INC_APPROVAL_QUE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("BC_ID").InnerText = hdID.Value
            If hdPrevBCID.Value <> "" Then
                objInputXml.DocumentElement.SelectSingleNode("PREVIOUS_BC_ID").InnerText = hdPrevBCID.Value
            End If

            objOutputXml = objApprovalQue.ViewApprovalQue(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DETAILS").Rows.Count <> 0 Then
                    If ds.Tables("DETAILS").Select("EMPLOYEEID='" + objeAAMS.EmployeeID(Session("Security")) + "'").Length > 0 Then
                        hdOldStatusID.Value = ds.Tables("DETAILS").Select("EMPLOYEEID='" + objeAAMS.EmployeeID(Session("Security")) + "'")(0)("APPROVAL_STATUS_ID")
                        If hdOldStatusID.Value.Trim <> "" Then
                            ddlIncentiveStatus.SelectedValue = hdOldStatusID.Value.Trim
                        End If
                    End If
                    gvContactType.DataSource = ds.Tables("DETAILS")
                    gvContactType.DataBind()
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Approval Queue']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                'If strBuilder(1) = "0" Then
                '    btnNew.Enabled = False
                '    btnSave.Enabled = False
                'End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("BC_ID") IsNot Nothing) Then
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objApprovalQue As New AAMS.bizIncetive.bzApprovalQue
            ' Load Input Xml.
            objInputXml.LoadXml("<UP_UPDATE_INC_APPROVAL_QUE_INPUT><DETAILS EMPLOYEEID='' BC_ID='' REC_DATE='' APPROVAL_STATUS_ID='' PREVIOUS_BC_ID='' PREVIOUS_APPROVAL_STATUS_ID=''/></UP_UPDATE_INC_APPROVAL_QUE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("APPROVAL_STATUS_ID").InnerText = ddlIncentiveStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("BC_ID").InnerText = hdID.Value.Trim
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_BC_ID").InnerText = hdPrevBCID.Value.Trim
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("EMPLOYEEID").InnerText = objeAAMS.EmployeeID(Session("Security"))
            objInputXml.DocumentElement.SelectSingleNode("DETAILS").Attributes("PREVIOUS_APPROVAL_STATUS_ID").InnerText = hdOldStatusID.Value
            ' Calling update method for update.
            objOutputXml = objApprovalQue.UpdateApproval_Que(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewDetails()
                If lblError.Text.Trim = "" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                End If
                ' CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                ViewDetails()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            ' Calling View Method.
            ' ViewDetails()
            Response.Redirect(Request.Url.ToString())

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSideLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSideLetter.Click
        '  Session("IssueSideLetter") = "<RP_INC_SIDELETTER_INPUT><CHAIN_CODE>2</CHAIN_CODE></RP_INC_SIDELETTER_INPUT>"
        'Response.Redirect("../RPSR_ReportShow.aspx?Case=IssueSideLetter", False)
    End Sub
End Class
