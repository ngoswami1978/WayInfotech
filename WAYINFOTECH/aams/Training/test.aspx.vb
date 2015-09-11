
Partial Class Training_test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Dim strbody As String = ""

        Try

          
            If (Not IsPostBack) Then
                Dim objInputTempXml, objOutputTempXml As New XmlDocument
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                'Code of Security Check
                Dim objMailTemplate As New AAMS.bizMaster.bzEmailTemplates

                objInputTempXml.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
                objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ISP MAILING" ' Session("Action").ToString().Split("|").GetValue(1).ToString()
                'Here Back end Method Call
                objOutputTempXml = objMailTemplate.View(objInputTempXml)
                If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    strbody = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplate").Value
                  
                    If 1 = 1 Then
                        'hdRequestId.Value = Request.QueryString("RequestId").ToString

                        Dim objtFeasibilityStatusId As New AAMS.bizISP.bzISPFeasibleRequest
                        Dim objInputFeasibilityXml, objOutputFeasibilityXml As New XmlDocument
                        objInputFeasibilityXml.LoadXml("<ISP_VIEWFEASIBILEREQUEST_INPUT><RequestID></RequestID><Name></Name></ISP_VIEWFEASIBILEREQUEST_INPUT>")
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("Name").InnerXml = String.Empty
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = "132"

                        objOutputFeasibilityXml = objtFeasibilityStatusId.View(objInputFeasibilityXml)
                        'Here Back end Method Call

                        If objOutputFeasibilityXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then




                            strbody = strbody.Replace("[[AgencyName]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value)
                            strbody = strbody.Replace("[[Address]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").Value)
                            strbody = strbody.Replace("[[TelNo]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").Value)
                            strbody = strbody.Replace("[[ContactPerson]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("CONCERNED_PERSON").Value)
                            strbody = strbody.Replace("[[FAX]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").Value)
                            'strbody = strbody.Replace("[[OFFICEID]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").Value)
                            strbody = strbody.Replace("[[Remarks]]", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").Value)
                            If Session("Security") IsNot Nothing Then
                                Dim objSecurityXml As New XmlDocument
                                objSecurityXml.LoadXml(Session("Security"))
                                strbody = strbody.Replace("[[AuthorizedSign]]", " " & objSecurityXml.DocumentElement.SelectSingleNode("Login").InnerText)
                            End If


                        End If

                        hdnmsg.Value = strbody
                        divserver.InnerHtml = strbody
                        ' ClientScript.RegisterClientScriptBlock(Me.GetType, "strPopup", "<script language='javascript'>window.setTimeout('TextContent();', 200);</script>")
                    End If
                End If
            End If
        Catch ex As Exception
            ' lblError.Text = ex.Message
        End Try

    End Sub
End Class
