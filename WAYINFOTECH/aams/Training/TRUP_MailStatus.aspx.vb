
Partial Class Training_TRUP_MailStatus
    Inherits System.Web.UI.Page
#Region "Global Variables Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region
#Region "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtStatus.Focus()
            If Not IsPostBack Then
                Dim strUrl As String = Request.Url.ToString()
                Session("PageName") = strUrl
                btnSave.Attributes.Add("onclick", "return ValidateForm();")
                If Request.QueryString("Action") IsNot Nothing And Request.QueryString("StatusId") IsNot Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("StatusId"))
                    ViewDetails()
                End If
            End If
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Private Sub ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objMailStatus As New AAMS.bizTraining.bzMailStatus
        Try
            objInputXml.LoadXml("<HD_VIEWMAILSTATUS_INPUT><TR_STATUS_ID /></HD_VIEWMAILSTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("TR_STATUS_ID").InnerText = hdID.Value
            objOutputXml = objMailStatus.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_NAME").InnerText
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objMailStatus = Nothing
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Mail Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Mail Status']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("TeamID") IsNot Nothing) Then
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
#Region "Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objMailStatus As New AAMS.bizTraining.bzMailStatus
            objInputXml.LoadXml("<HD_UPDATEMAILSTATUS_INPUT><MAILSTATUS TR_STATUS_ID='' TR_STATUS_NAME='' /></HD_UPDATEMAILSTATUS_INPUT>")

            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_ID").Value = hdID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_NAME").Value = txtStatus.Text.Trim()

            'calling Update Method
            objOutputXml = objMailStatus.Update(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Request.QueryString("Action") IsNot Nothing Then
                    If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                        hdID.Value = objInputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_ID").InnerText.Trim()
                        lblError.Text = objeAAMSMessage.messUpdate ' Records Updated Successfully
                    Else
                        hdID.Value = objInputXml.DocumentElement.SelectSingleNode("MAILSTATUS").Attributes("TR_STATUS_ID").InnerText.Trim()
                        lblError.Text = objeAAMSMessage.messInsert  ' Records Inserted Successfully
                    End If
                End If
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        Finally

        End Try
    End Sub
#End Region
#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                txtStatus.Text = ""
                lblError.Text = ""
            Else
                ' Calling View Method.
                ViewDetails()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("TRUP_MailStatus.aspx?Action=I")
    End Sub
End Class
