Public Class Form3

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj As New bizUtility.bzEmail
        Dim objXml As New Xml.XmlDocument
        'objXml.LoadXml("<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='ngoswami@amadeus.co.in,tnath@amadeus.co.in,ashishs@amadeus.co.in' SUBJECT='Test Mail' MESSAGE='hi !' SOURCE='admin@amadeus.co.in' DESTINATION_CC='' DESTINATION_BCC='' ATTACHMENT_FILE='C:\Inetpub\wwwroot\aams\MailAttachment\PaymenttmpA94.xls'/></SENDMAILIMMEDIATE_INPUT>")
        'objXml.LoadXml("<SENDMAILIMMEDIATE_INPUT><MAIL_DETAILS DESTINATION_TO='kGupta@amadeus.co.in' DESTINATION_TO_NAME='' SUBJECT='dddd' SOURCE='' MESSAGE='amadeus' DESTINATION_CC='' DESTINATION_BCC=''/></SENDMAILIMMEDIATE_INPUT>")
        objXml.Load("c:\input.xml")

        obj.SendMail(objXml)
    End Sub
End Class