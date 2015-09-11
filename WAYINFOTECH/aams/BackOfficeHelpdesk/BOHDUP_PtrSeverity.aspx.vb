Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class BOHelpDesk_HDUP_PtrSeverity
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim eaamsObj As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            btnSave.Attributes.Add("onclick", "return validateSeverity();")
            btnNew.Attributes.Add("onclick", "return InsertSeverity();")
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Severity']").Count <> 0 Then
                    strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO PTR Severity']").Attributes("Value").Value)
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
                        ViewPTRSeverity()
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
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtPtrSeverityId As New AAMS.bizBOHelpDesk.bzPTRSeverity

        Try
            objInputXml.LoadXml("<HD_UPDATEPTR_SEVERITY_INPUT><PTR_SEVERITY HD_PTR_SEV_ID='' HD_PTR_SEV_NAME=''/></HD_UPDATEPTR_SEVERITY_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("PTR_SEVERITY")

                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                    .Attributes("HD_PTR_SEV_NAME").Value() = txtSeverity.Text.Trim()
                    .Attributes("HD_PTR_SEV_ID").Value() = objED.Decrypt(Request.QueryString("PTRSeverityID"))
                Else
                    .Attributes("HD_PTR_SEV_NAME").Value() = txtSeverity.Text.Trim()
                    .Attributes("HD_PTR_SEV_ID").Value() = String.Empty
                End If

            End With
            objOutputXml = objtPtrSeverityId.Update(objInputXml)
            Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("PTR_SEVERITY").Attributes("HD_PTR_SEV_ID").Value().Trim()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                    Response.Redirect("BOHDUP_PTRSeverity.aspx?Action=US&PTRSeverityID=" + objED.Encrypt(CheckBoxOb))
                    'Response.Redirect("ISPUP_FeasibilityStatus.aspx?Action=U&FeasibleStatusID=" & CheckBoxOb)

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Not Request.QueryString("Action").ToString().Split("|").GetValue(0) Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    ViewPTRSeverity()
                    txtSeverity.Focus()
                    lblError.Text = ""
                Else
                    txtSeverity.Text = ""
                    lblError.Text = ""
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewPTRSeverity()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzPTRSeverity As New AAMS.bizBOHelpDesk.bzPTRSeverity
            objInputXml.LoadXml("<HD_VIEWPTR_SEVERITY_INPUT><HD_PTR_SEV_ID></HD_PTR_SEV_ID></HD_VIEWPTR_SEVERITY_INPUT>")
            'If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_SEV_ID").InnerText = objED.Decrypt(Request.QueryString("PTRSeverityID").ToString().Trim)
            'End If

            objOutputXml = objbzPTRSeverity.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtSeverity.Text = objOutputXml.DocumentElement.SelectSingleNode("PTR_SEVERITY").Attributes("HD_PTR_SEV_NAME").Value.Trim()
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

   

