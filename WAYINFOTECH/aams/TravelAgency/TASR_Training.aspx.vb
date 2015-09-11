
Partial Class TravelAgency_TASR_Training
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As StringBuilder

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
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            If Not IsPostBack Then
                objeAAMS.BindDropDown(ddlTrnStatus, "TRAININGSTATUS", True)
                If Not Session("Action") = Nothing Then
                    hdLCode.Value = Session("Action").ToString().Split("|").GetValue(1)
                    ViewDetails()
                End If
                ' Checking security.
            End If

            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objEmployee As New AAMS.bizMaster.bzEmployee
            ' Load Input Xml.
            objInputXml.LoadXml("<MS_UPDATEREGISTRATIONID_INPUT><REGISTRATION ACTION='' LCODE='' USERNAME='' PASSWORD='' TRAININGSTATUS='' /></MS_UPDATEREGISTRATIONID_INPUT>")
            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("ACTION").Value = "U"
            Else
                objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("ACTION").Value = "I"
            End If

            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("LCODE").Value = hdLCode.Value
            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("USERNAME").Value = txtUserName.Text
            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("PASSWORD").Value = txtPassword.Text
            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("TRAININGSTATUS").Value = ddlTrnStatus.SelectedValue.ToString

            ' Calling update method for update.
            objOutputXml = objEmployee.UpdateRegistrationID(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (hdID.Value <> "") Then
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = "1" 'here we are setting the of hdID to "1" to check whether we are insert mode or in update mode
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

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
            If hdLCode.Value = "" Then
                ViewDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim li As ListItem
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objEmployee As New AAMS.bizMaster.bzEmployee
        Try
            objInputXml.LoadXml("<MS_UPDATEREGISTRATIONID_INPUT><REGISTRATION ACTION='' LCODE='' USERNAME='' PASSWORD='' TRAININGSTATUS='' /></MS_UPDATEREGISTRATIONID_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("ACTION").Value = "S"
            objInputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("LCODE").Value = hdLCode.Value
            ' Calling update method for update.
            objOutputXml = objEmployee.UpdateRegistrationID(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtUserName.Text = objOutputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("USERNAME").Value
                txtPassword.Text = objOutputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("PASSWORD").Value
                li = ddlTrnStatus.Items.FindByText(objOutputXml.DocumentElement.SelectSingleNode("REGISTRATION").Attributes("TRAININGSTATUS").Value)
                If li IsNot Nothing Then
                    ddlTrnStatus.SelectedValue = li.Value
                End If
                hdID.Value = "1"
            Else
                If objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record found !" Then
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyTraining']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyTraining']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSave.Enabled = False
                    Response.Redirect("~/NoRights.aspx")
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
End Class
