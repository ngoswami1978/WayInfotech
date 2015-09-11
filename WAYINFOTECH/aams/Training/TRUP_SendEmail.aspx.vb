Imports System.Net.Mail

Partial Class Training_TRUP_SendEmail
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        'Dim objMail As New AAMS.bizUtility.bzEmail
        'Dim objMailInputDoc As New XmlDocument
        'Dim blnSuccess As Boolean
        'objMailInputDoc.LoadXml("<OTH_SENDMAILIMMEDIATE_INPUT><MAIL PNR_NO='' EQ_DESTINATION='' EQ_SUBJECT='' EQ_SOURCE='' EQ_MESSAGE='' EQ_DESTINATION_BCC='' EQ_ATTACHMENT_PATH='' /></OTH_SENDMAILIMMEDIATE_INPUT>")
        'objMailInputDoc.DocumentElement.SelectSingleNode("MAIL").Attributes("EQ_DESTINATION").Value = ConfigurationSettings.AppSettings("MAIL_DESTINATION").ToString()
        'objMailInputDoc.DocumentElement.SelectSingleNode("MAIL").Attributes("EQ_SUBJECT").Value = ConfigurationSettings.AppSettings("MAIL_SOURCE_SUBJECT").ToString()
        'objMailInputDoc.DocumentElement.SelectSingleNode("MAIL").Attributes("EQ_SOURCE").Value = ConfigurationSettings.AppSettings("MAIL_SOURCE").ToString()
        'objMailInputDoc.DocumentElement.SelectSingleNode("MAIL").Attributes("EQ_MESSAGE").Value = "Hi,<br/><br/>You received an agency order form attached with this mail.<br/><br/>Regards<br/>Amadeus Online<br/><br/>This message has been automatically generated.Please do not reply."
        'objMailInputDoc.DocumentElement.SelectSingleNode("MAIL").Attributes("EQ_ATTACHMENT_PATH").Value = Server.MapPath("PDFDOC") + "\" + "AgencyOrder.pdf"
        'blnSuccess = objMail.Send(objMailInputDoc)
        ' Code End For Mailing PDF as Attachment.
        Try

       
            Dim objmail As New System.Net.Mail.MailMessage
            Dim objsmtp As System.Net.Mail.SmtpClient
            Dim strTo, StrCC, strBcc, strSubject, strMessage, strFrom As String
            objsmtp = New System.Net.Mail.SmtpClient()
            If ConfigurationManager.AppSettings("SMTP_SERVER_SPECIFIC").Trim <> "" Then
                objsmtp.Host = ConfigurationManager.AppSettings("SMTP_SERVER_SPECIFIC").Trim
            Else
                objsmtp.Host = ConfigurationManager.AppSettings("SMTP_SERVER_DEFAULT").Trim
            End If

            strFrom = ConfigurationManager.AppSettings("MAIL_SOURCE").Trim

            If ConfigurationManager.AppSettings("SMTP_SERVER_PORT").Trim <> "" Then
                objsmtp.Port = ConfigurationManager.AppSettings("SMTP_SERVER_PORT").Trim
            Else
                objsmtp.Port = 25
            End If

            strTo = txtEmailTo.Text
            StrCC = txtCC.Text
            strBcc = txtBcc.Text
            ' strSource = objNode.Attributes("SOURCE").InnerText & "".Trim
            strSubject = txtSub.Text
            strMessage = hdnmsg.Value


            objmail.To.Add(strTo)
            If (StrCC = "") Then
            Else
                objmail.CC.Add(StrCC)
            End If
            If (strBcc = "") Then
            Else
                objmail.Bcc.Add(strBcc)
            End If
            'objmail.From
            objmail.Subject = strSubject
            ' If (strFrom = "") Then
            objmail.From = New System.Net.Mail.MailAddress(strFrom)
            ' Else
            ' objmail.From = New System.Net.Mail.MailAddress(strSource)
            '  End If

            objmail.IsBodyHtml = True
            objmail.Body = strMessage
            objsmtp.UseDefaultCredentials = True
            objsmtp.Send(objmail)
            lblError.Text = "Email Send Successfully"
        Catch ex As Exception
            'lblError.Text=ex.Message
            lblError.Text = "Error in sending Email"
        End Try
    End Sub

    Private Sub CheckSecurity()
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Dim strBuilder As New StringBuilder
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Send Email']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Send Email']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
            End If

            If strBuilder(1) = "0" Then
                btnSendMail.Enabled = False
                btnEmailGroup.Enabled = False
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            CheckSecurity()
            txtEmailTo.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
