'Copyright notice: © 2004 by Bird Information Systems All rights reserved.
'********************************************************************************************
' This file contains trade secrets of Bird Information Systems No part
' may be reproduced or transmitted in any form by any means or for any purpose
' without the express written permission of Bird Information Systems
'********************************************************************************************
'$Author: Neeraj $
'Purpose   : This Interface must be implemented by all the Master Classes.
'$Logfile: /AAMS/Components/bizUtility/bzEmail.vb $
'$Workfile: bzEmail.vb $
'$Revision: 4 $
'$Archive: /AAMS/Components/bizUtility/bzEmail.vb $
'$Modtime: 12/02/10 12:29p $
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Net.Mail.SmtpClient
Imports System.IO
Imports AAMS.bizShared

Namespace bizUtility
    Public Class bzEmail
        Const gstrMODULE_NAME = "bzEmail"
        Dim lobjConfig As System.Configuration.ConfigurationSettings

        Public Function SendMail(ByVal objXmlInput As XmlDocument) As Boolean
            '***********************************************************************
            'Purpose:   This function sends Email
            'Input:     <SENDMAILIMMEDIATE_INPUT>
            '	            <MAIL_DETAILS  DESTINATION_TO='' DESTINATION_TO_NAME='' SUBJECT='' SOURCE='' MESSAGE='' DESTINATION_CC=''  DESTINATION_BCC='' ATTACHMENT_FILE='' MAIL_SOURCE= '' SMTP_SERVER_SPECIFIC = ''  SMTP_SERVER_DEFAULT= ''  SMTP_SERVER_PORT='' />
            '           </SENDMAILIMMEDIATE_INPUT>
            'Output:    BOOLEAN
            '***********************************************************************

            Dim lstrMethod_Name As String
            Dim objmail As New System.Net.Mail.MailMessage

            Dim objsmtp As System.Net.Mail.SmtpClient
            Dim strTo, StrCC, strBcc, strSubject, strMessage, strFrom, strSource As String

            lstrMethod_Name = "SendMail"
            Dim objNode As XmlNode
            Dim objListNode As XmlNodeList

            '--FOR FILE ATTACHMENT
            Dim ARStrFiles() As String
            'FOR FILE ATTACHMENT

            Try

                objListNode = objXmlInput.DocumentElement.SelectNodes("MAIL_DETAILS")

                For Each objNode In objListNode
                    objsmtp = New System.Net.Mail.SmtpClient()

                    If objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_SPECIFIC") Is Nothing Then
                        If Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_SPECIFIC").Trim <> "" Then
                            objsmtp.Host = Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_SPECIFIC").Trim
                        Else
                            objsmtp.Host = Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_DEFAULT").Trim
                        End If
                    Else
                        objsmtp.Host = objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_SPECIFIC").InnerText
                    End If


                    If objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_DEFAULT") Is Nothing Then
                        If Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_SPECIFIC").Trim <> "" Then
                            objsmtp.Host = Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_SPECIFIC").Trim
                        Else
                            objsmtp.Host = Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_DEFAULT").Trim
                        End If
                    Else
                        objsmtp.Host = objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_DEFAULT").InnerText
                    End If


                    If objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_PORT") Is Nothing Then
                        If Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_PORT").Trim <> "" Then
                            objsmtp.Port = Configuration.ConfigurationSettings.AppSettings("SMTP_SERVER_PORT").Trim
                        Else
                            objsmtp.Port = 25
                        End If
                    Else
                        objsmtp.Port = objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("SMTP_SERVER_PORT").InnerText
                    End If


                    If objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("MAIL_SOURCE") Is Nothing Then
                        strFrom = Configuration.ConfigurationSettings.AppSettings("MAIL_SOURCE").Trim
                    Else
                        strFrom = objXmlInput.DocumentElement.SelectSingleNode("MAIL_DETAILS").Attributes("MAIL_SOURCE").InnerText
                    End If


                    'Dim cred As New System.Net.NetworkCredential("sauravk@amadeus.co.in", "password")
                    '-----------------FILE ATTACHMENT CODE START
                    If Not objNode.Attributes("ATTACHMENT_FILE") Is Nothing Then
                        If objNode.Attributes("ATTACHMENT_FILE").InnerText.Trim <> "" Then
                            ARStrFiles = objNode.Attributes("ATTACHMENT_FILE").InnerText.Trim.Split(",")
                            For i As Int16 = 0 To UBound(ARStrFiles)
                                Dim Attachment As New System.Net.Mail.Attachment(ARStrFiles(i))
                                objmail.Attachments.Add(Attachment)
                            Next
                        End If
                    End If

                    '-----------------FILE ATTACHMENT CODE END

                    strTo = objNode.Attributes("DESTINATION_TO").InnerText & "".Trim
                    StrCC = objNode.Attributes("DESTINATION_CC").InnerText & "".Trim
                    strBcc = objNode.Attributes("DESTINATION_BCC").InnerText & "".Trim
                    strSource = objNode.Attributes("SOURCE").InnerText & "".Trim
                    strSubject = objNode.Attributes("SUBJECT").InnerText & "".Trim
                    strMessage = objNode.Attributes("MESSAGE").InnerText & "".Trim


                    'Validations
                    If (strTo = "") Then

                        Throw New AAMSException("Mailto field can't be empty")
                    End If
                    If (strSubject = "") Then
                        Throw New AAMSException("Subject field can't be empty")
                    End If
                    If (strMessage = "") Then
                        Throw New AAMSException("Message field can't be empty")
                    End If
                    If Not ValidateEmail(strTo) Then
                        Throw New AAMSException("Please enter valid email adddress.")
                    End If
                    If strSource <> "" And Not ValidateEmail(strSource) Then
                        Throw New AAMSException("Please enter valid email adddress.")
                    End If

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
                    If (strSource = "") Then
                        objmail.From = New System.Net.Mail.MailAddress(strFrom)
                    Else
                        objmail.From = New System.Net.Mail.MailAddress(strSource)
                    End If
                    objmail.IsBodyHtml = True

                    objmail.Body = strMessage
                    objsmtp.UseDefaultCredentials = True
                    'objsmtp.EnableSsl = True
                    'objsmtp.Credentials = cred
                    objsmtp.Send(objmail)
                Next
            Catch exc1 As AAMSException
                bizShared.bzShared.LogWrite(gstrMODULE_NAME, lstrMethod_Name & objXmlInput.OuterXml.ToString, exc1.GetBaseException)
                Return False
            Catch exc As Exception
                bizShared.bzShared.LogWrite(gstrMODULE_NAME, lstrMethod_Name & objXmlInput.OuterXml.ToString, exc.GetBaseException)
                Return False
            End Try
            Return True
        End Function
        Private Function ValidateEmail(ByVal strEmail As String) As Boolean
            'Dim intI, intCnt, intPos, intPos1 As Integer
            'intCnt = 0
            'For intI = 0 To strEmail.Length - 1
            '    If Char.IsLetterOrDigit(strEmail.Substring(intI, 1)) Or strEmail.Substring(intI, 1) = "." Or strEmail.Substring(intI, 1) = "_" Or strEmail.Substring(intI, 1) = "@" Then
            '        If Not Char.IsLetter(Right(strEmail, 1)) Or Not Char.IsLetterOrDigit(Left(strEmail, 1)) Then
            '            Return False
            '            Exit Function
            '        End If
            '        If strEmail.Substring(intI, 1) = "@" Then
            '            intCnt += 1
            '        End If
            '        If intCnt > 1 Then
            '            Return False
            '            Exit Function
            '        End If
            '        intPos = InStr(Trim(strEmail), "@")
            '        If InStr(intPos, Trim(strEmail), ".") = 0 Then
            '            Return False
            '            Exit Function
            '        End If
            '    Else
            '        Return False
            '        Exit Function
            '    End If
            'Next
            Return True
        End Function

    End Class
End Namespace
