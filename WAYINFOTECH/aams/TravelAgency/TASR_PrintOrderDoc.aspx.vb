Imports System.Data
Imports System.Xml
Imports System.IO

Partial Class TravelAgency_TASR_PrintOrderDoc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Session("ImagePath") Is Nothing Then
            Dim StrChk As String = Session("ImagePath")

            Dim strImgFile As String = Path.GetFileName(StrChk)


            If StrChk.Split("=").GetValue(0) <> "ID" Then
                imgOrderDocPrnt.ImageUrl = "~/DOC/" + strImgFile ' Session("ImagePath").ToString()
                imgOrderDocPrnt.Width = 670
                imgOrderDocPrnt.Height = 820
            Else
                imgOrderDocPrnt.Visible = False
                SetEmailValues(StrChk.Split("=").GetValue(1))
                pnlEmail.Visible = True
            End If
        End If
    End Sub
    Sub SetEmailValues(ByVal strID As String)
        Dim xInputDoc, xOutPutDoc As New XmlDocument
        Dim bzEmailDetails As New AAMS.bizTravelAgency.bzOrder
        xInputDoc.LoadXml("<UP_GETORDEREMAILDETAILS_INPUT><ID></ID></UP_GETORDEREMAILDETAILS_INPUT>")
        xInputDoc.DocumentElement.SelectSingleNode("ID").InnerText = strID
        xOutPutDoc = bzEmailDetails.GetEmailDetails(xInputDoc)
        lblMailTo.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailTo").Value
        'txtMailFrom.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailFrom").Value
        txtSubject.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailSubject").Value
        'txtMailBody.Text = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailBody").Value
        txtMailBody.InnerHtml = xOutPutDoc.DocumentElement.SelectSingleNode("MailDetail").Attributes("EmailBody").Value
    End Sub
End Class
