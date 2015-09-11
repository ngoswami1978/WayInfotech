
Partial Class Popup_PUUP_HDDescSolution
    Inherits System.Web.UI.Page
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMS As New eAAMS
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            If Request.QueryString("fr") = "ds" Then
                If Request.QueryString("ID") <> "" Then
                    lblHeader.Text = "Description of LTR No:" + objED.Decrypt(Request.QueryString("hd_re_id"))
                    BindDescription()
                End If
            End If
            If Request.QueryString("fr") = "sl" Then
                If Request.QueryString("ID") <> "" Then
                    lblHeader.Text = "Solution of LTR No:" + objED.Decrypt(Request.QueryString("hd_re_id"))
                    BindSolution()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub BindDescription()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTDESCRIPTION_INPUT><HD_RE_ID></HD_RE_ID><ROWID></ROWID></HD_GETREQUESTDESCRIPTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = objED.Decrypt(Request.QueryString("hd_re_id"))
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = objED.Decrypt(Request.QueryString("ID"))
        objOutputXml = objCall.ViewRequestDesc(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If ds.Tables("REQUESTDESCRIPTION").Rows.Count <> 0 Then
                txtDescSolution.Text = ds.Tables("REQUESTDESCRIPTION").Rows(0)("ACTION_TAKEN").ToString
            Else
                txtDescSolution.Text = ""
            End If

        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
            End If
        End If
    End Sub
    Sub BindSolution()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objCall As New AAMS.bizHelpDesk.bzCall
        objInputXml.LoadXml("<HD_GETREQUESTSOLUTION_INPUT><HD_RE_ID></HD_RE_ID><ROWID></ROWID></HD_GETREQUESTSOLUTION_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("HD_RE_ID").InnerText = objED.Decrypt(Request.QueryString("hd_re_id"))
        objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = objED.Decrypt(Request.QueryString("ID"))
        objOutputXml = objCall.ViewRequestSolution(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If ds.Tables("REQUESTSOLUTION").Rows.Count <> 0 Then
                txtDescSolution.Text = ds.Tables("REQUESTSOLUTION").Rows(0)("ACTION_TAKEN").ToString
            Else
                txtDescSolution.Text = ""
            End If

        Else
            If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found!") Then
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            Else
            End If
        End If
    End Sub
End Class
