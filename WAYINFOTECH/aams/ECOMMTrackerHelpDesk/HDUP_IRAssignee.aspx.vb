'################################################
'######## Developed By Abhishek #################
'################################################
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class ETHelpDesk_HDUP_IRAssignee
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            btnSave.Attributes.Add("onclick", "return validateAssignee();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='ET IR Assignee']").Attributes("Value").Value)

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
                        ViewAssignee()
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
                    '                    Dim objhdAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee
                    Dim objhdAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee


                    objInputXml.LoadXml("<HD_UPDATEIR_ASSIGNEE_INPUT><IR_ASSIGNEE ASSIGNEEID='' ASSIGNEE_NAME=''/></HD_UPDATEIR_ASSIGNEE_INPUT>")

                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        objInputXml.DocumentElement.SelectSingleNode("IR_ASSIGNEE").Attributes("ASSIGNEEID").Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("IR_ASSIGNEE").Attributes("ASSIGNEE_NAME").Value = txtAssigneeName.Text.Trim()
                    objOutputXml = objhdAssignee.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        Else
                            Response.Redirect("HDUP_IRAssignee.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("IR_ASSIGNEE").Attributes("ASSIGNEEID").Value), False)
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
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
            'If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
            '    ViewAssignee()
            '    lblError.Text = ""
            'Else
            '    txtAssigneeName.Text = ""
            'End If
            Response.Redirect(Request.Url.ToString, False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub ViewAssignee()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            '            Dim objhdAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee
            Dim objhdAssignee As New AAMS.bizETrackerHelpDesk.bzIRAssignee

            objInputXml.LoadXml("<HD_VIEWIR_ASSIGNEE_INPUT><ASSIGNEEID></ASSIGNEEID></HD_VIEWIR_ASSIGNEE_INPUT>")
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ASSIGNEEID").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            End If

            objOutputXml = objhdAssignee.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtAssigneeName.Text = objOutputXml.DocumentElement.SelectSingleNode("IR_ASSIGNEE").Attributes("ASSIGNEE_NAME").Value.Trim()
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
            Response.Redirect("HDUP_IRAssignee.aspx?Action=I", False)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
