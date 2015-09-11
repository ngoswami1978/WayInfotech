
Partial Class Sales_SAUP_TargetStatus
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtStatus.Focus()
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                ' Checking Query String for update .
                If Request.QueryString("Action") IsNot Nothing And Request.QueryString("SVT_STATUSID") IsNot Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("SVT_STATUSID").ToString().Trim())
                    ViewDetails()
                End If
            End If
            ' Checking security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objCallStatus As New AAMS.bizSales.bzStrategicVisitTargetStatus
            ' Load Input Xml.
            objInputXml.LoadXml("<SL_UPDATED_STRATEGIC_VISIT_TARGET_STATUS_INPUT><STRATEGIC_VISIT_TARGET SVT_STATUSID='' SVT_STATUS_NAME='' SVT_STATUS_CLOSE='' /></SL_UPDATED_STRATEGIC_VISIT_TARGET_STATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUSID").Value = hdID.Value
            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUS_NAME").Value = txtStatus.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUS_CLOSE").Value = IIf(chkClose.Checked = True, "1", "0")

            ' Calling update method for update.
            objOutputXml = objCallStatus.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUSID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUSID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            CheckSecurity()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("SAUP_TargetStatus.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                txtStatus.Text = ""
            Else
                ' Calling View Method.

                ViewDetails()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objCallStatus As New AAMS.bizSales.bzStrategicVisitTargetStatus
        Try
            objInputXml.LoadXml("<SL_VIEW_STRATEGIC_VISIT_TARGET_STATUS_INPUT><SVR_STATUSID /><SVT_STATUS_CLOSE/></SL_VIEW_STRATEGIC_VISIT_TARGET_STATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("SVR_STATUSID").InnerText = hdID.Value
            objOutputXml = objCallStatus.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUS_NAME").Value
                chkClose.Checked = IIf(objOutputXml.DocumentElement.SelectSingleNode("STRATEGIC_VISIT_TARGET").Attributes("SVT_STATUS_CLOSE").Value.Trim = "1", True, False)
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Target Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Target Status']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("SVT_STATUSID") IsNot Nothing) Then
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
End Class