'################################################
'######## Developed By Abhishek #################
'################################################
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class ETHelpDesk_HDUP_IRSeverity
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            btnSave.Attributes.Add("onclick", "return validateSeverity();")
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Severity']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Severity']").Attributes("Value").Value)
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
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        ViewIRSeverity()
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
        '        Dim objtIRSeverityId As New AAMS.bizETrackerHelpDesk.bzIRSeverity
        Dim objtIRSeverityId As New AAMS.bizETrackerHelpDesk.bzIRSeverity


        Try
            objInputXml.LoadXml("<HD_UPDATEIR_SEVERITY_INPUT><IR_SEVERITY HD_IR_SEV_ID='' HD_IR_SEV_NAME=''/></HD_UPDATEIR_SEVERITY_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("IR_SEVERITY")

                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                    .Attributes("HD_IR_SEV_NAME").Value() = txtSeverity.Text.Trim()
                    .Attributes("HD_IR_SEV_ID").Value() = objED.Decrypt(Request.QueryString("IRSeverityID"))
                Else
                    .Attributes("HD_IR_SEV_NAME").Value() = txtSeverity.Text.Trim()
                    .Attributes("HD_IR_SEV_ID").Value() = String.Empty
                End If

            End With
            objOutputXml = objtIRSeverityId.Update(objInputXml)
            Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("IR_SEVERITY").Attributes("HD_IR_SEV_ID").Value().Trim()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                    Response.Redirect("HDUP_IRSeverity.aspx?Action=US&IRSeverityID=" + objED.Encrypt(CheckBoxOb))
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
                    ViewIRSeverity()
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
    Private Sub ViewIRSeverity()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            '            Dim objbzIRSeverity As New AAMS.bizETrackerHelpDesk.bzIRSeverity
            Dim objbzIRSeverity As New AAMS.bizETrackerHelpDesk.bzIRSeverity

            objInputXml.LoadXml("<HD_VIEWIR_SEVERITY_INPUT><HD_IR_SEV_ID></HD_IR_SEV_ID></HD_VIEWIR_SEVERITY_INPUT>")
            'If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
            objInputXml.DocumentElement.SelectSingleNode("HD_IR_SEV_ID").InnerText = objED.Decrypt(Request.QueryString("IRSeverityID").ToString().Trim)
            'End If

            objOutputXml = objbzIRSeverity.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtSeverity.Text = objOutputXml.DocumentElement.SelectSingleNode("IR_SEVERITY").Attributes("HD_IR_SEV_NAME").Value.Trim()
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

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_IRSeverity.aspx?Action=I", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
End Class




