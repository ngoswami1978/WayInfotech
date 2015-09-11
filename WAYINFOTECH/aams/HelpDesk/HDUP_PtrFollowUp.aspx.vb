Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class HelpDesk_HDUP_PtrFollowUp
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim eaamsObj As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnSave.Attributes.Add("onclick", "return validateFollowup();")
            btnNew.Attributes.Add("onclick", "return InsertPtrFollowup();")
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Followup']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Followup']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                        btnNew.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If

                    If Request.QueryString("Action") IsNot Nothing Then
                        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                            If strBuilder(2) = "0" Then
                                btnSave.Enabled = False
                            End If
                        End If
                    End If
                End If
            Else
                strBuilder = eaamsObj.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        ViewFollowUp()
                    End If
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    Dim objhdFollowup As New AAMS.bizHelpDesk.bzPTRFollowUp

                    objInputXml.LoadXml("<HD_UPDATEPTR_FOLLOWUP_INPUT><PTR_FOLLOWUP HD_PTR_FOLLOWUP_ID='' HD_PTR_FOLLOWUP_NAME='' /></HD_UPDATEPTR_FOLLOWUP_INPUT>")

                    If Request.QueryString("Action").ToString().Split("|").GetValue(0) IsNot Nothing Then
                        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                            objInputXml.DocumentElement.SelectSingleNode("PTR_FOLLOWUP").Attributes("HD_PTR_FOLLOWUP_ID").Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                        End If
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("PTR_FOLLOWUP").Attributes("HD_PTR_FOLLOWUP_NAME").Value = txtFollowUp.Text.Trim()
                    objOutputXml = objhdFollowup.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        Else
                            Response.Redirect("HDUP_PtrFollowUp.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PTR_FOLLOWUP").Attributes("HD_PTR_FOLLOWUP_ID").Value.Trim), False)
                            lblError.Text = objeAAMSMessage.messInsert
                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
                ViewFollowUp()
                lblError.Text = ""
            Else
                txtFollowUp.Text = ""
                End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub ViewFollowUp()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objhFollowup As New AAMS.bizHelpDesk.bzPTRFollowUp
            objInputXml.LoadXml("<HD_VIEWPTR_FOLLOWUP_INPUT><HD_PTR_FOLLOWUP_ID></HD_PTR_FOLLOWUP_ID></HD_VIEWPTR_FOLLOWUP_INPUT>")
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("HD_PTR_FOLLOWUP_ID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString().Trim)
            Else
                Exit Sub
            End If

            objOutputXml = objhFollowup.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtFollowUp.Text = objOutputXml.DocumentElement.SelectSingleNode("PTR_FOLLOWUP").Attributes("HD_PTR_FOLLOWUP_NAME").Value.Trim()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
