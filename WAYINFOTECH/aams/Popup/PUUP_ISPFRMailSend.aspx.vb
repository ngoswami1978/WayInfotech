
Partial Class Popup_PUUP_ISPFRMailSend
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS


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
        Dim s As String
        s = Request.QueryString("RequestId")

        'Session("PageName") = Request.Url.ToString()
        'objeAAMS.ExpirePageCache()
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        Try
            btnSendMail.Attributes.Add("onclick", "return MailMandatory();")
            If (Not IsPostBack) Then
                Dim objInputTempXml, objOutputTempXml As New XmlDocument
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzEmployee As New AAMS.bizMaster.bzEmployee
                'Code of Security Check
                Dim objMailTemplate As New AAMS.bizMaster.bzEmailTemplates
                If Session("Security") Is Nothing Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                    Exit Sub
                End If
                If Not Session("LoginSession") Is Nothing Then
                    objInputXml.LoadXml("<MS_VIEWEMPLOYEE_INPUT><EmployeeID></EmployeeID></MS_VIEWEMPLOYEE_INPUT>")
                    objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = Session("LoginSession").ToString().Split("|")(0)
                    'Here Back end Method Call
                    objOutputXml = objbzEmployee.View(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        txtEmailFrom.Text = objOutputXml.DocumentElement.SelectSingleNode("Employee").Attributes("Email").Value()
                    End If
                End If
                'Here Back end Method Call
                objInputTempXml.LoadXml("<MS_VIEWEMAILTEMPLATES_INPUT><TEMPLATES MailTemplateName='' /></MS_VIEWEMAILTEMPLATES_INPUT>")
                objInputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplateName").Value() = "ISP MAILING"
                objOutputTempXml = objMailTemplate.View(objInputTempXml)
                If objOutputTempXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    txtbody.Text = objOutputTempXml.DocumentElement.SelectSingleNode("TEMPLATES").Attributes("MailTemplate").Value
                    Dim strbody As String = ""
                    strbody = txtbody.Text
                    If (Request.QueryString("RequestId") IsNot Nothing) Then
                        Dim objtFeasibilityStatusId As New AAMS.bizISP.bzISPFeasibleRequest
                        Dim objInputFeasibilityXml, objOutputFeasibilityXml As New XmlDocument
                        objInputFeasibilityXml.LoadXml("<ISP_VIEWFEASIBILEREQUEST_INPUT><RequestID></RequestID><Name></Name></ISP_VIEWFEASIBILEREQUEST_INPUT>")
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("Name").InnerXml = String.Empty
                        objInputFeasibilityXml.DocumentElement.SelectSingleNode("RequestID").InnerXml = Request.QueryString("RequestId")

                        objOutputFeasibilityXml = objtFeasibilityStatusId.View(objInputFeasibilityXml)
                        'Here Back end Method Call

                        If objOutputFeasibilityXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                            strbody = strbody.Replace("<<AgencyName>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value)
                            strbody = strbody.Replace("<<Address>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ADDRESS").Value)
                            strbody = strbody.Replace("<<TelNo>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("PHONE").Value)
                            strbody = strbody.Replace("<<ContactPerson>>", " ")
                            strbody = strbody.Replace("<<FAX>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("FAX").Value)
                            'strbody = strbody.Replace("<<OFFICEID>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("OFFICEID").Value)
                            strbody = strbody.Replace("<<Remarks>>", " " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Remarks").Value)
                            txtSub.Text = "Agency : " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("Name").Value & " ; Request Id : " & objOutputFeasibilityXml.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").Value
                        End If
                        txtbody.Text = strbody
                    End If
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

  
    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Try
            Dim objbzIspMail As New AAMS.bizISP.bzISPFeasibleRequest
            Dim objISPMailInput, objISPMailOutput As New XmlDocument
            If (Request.QueryString("RequestId") IsNot Nothing) Then
                objISPMailInput.LoadXml("<ISP_UPDATESENDEMAILDATE_INPUT><FEASIBILE_REQUEST RequestID='' ContentType='' EmailFrom='' EmailTo='' EmailSubject ='' EmailBody='' /></ISP_UPDATESENDEMAILDATE_INPUT>")
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("RequestID").InnerText = Request.QueryString("RequestId").ToString()
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("ContentType").Value = "2"
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailFrom").Value = Trim(txtEmailFrom.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailTo").Value = Trim(txtEmailTo.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailSubject").Value = Trim(txtSub.Text)
                objISPMailInput.DocumentElement.SelectSingleNode("FEASIBILE_REQUEST").Attributes("EmailBody").Value = Trim(txtbody.Text)
            End If

            'Here Back end Method Call
            objISPMailOutput = objbzIspMail.SendMail(objISPMailInput)
            If objISPMailOutput.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = "Mail is successfully send."
            Else
                lblError.Text = objISPMailOutput.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
