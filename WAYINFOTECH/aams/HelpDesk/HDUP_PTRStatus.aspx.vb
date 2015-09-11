
Partial Class HelpDesk_HDUP_PTRStatus
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            txtPTRStatus.Focus()
            If Not IsPostBack Then

                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                If Request.QueryString("Action") IsNot Nothing And Request.QueryString("PTRStatusID") IsNot Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("PTRStatusID").ToString().Trim())
                    ViewDetails()
                End If

            End If
            ' Checking security.
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml As New XmlDocument
        Dim objOutputXml As New XmlDocument
        Dim objPTRStatus As New AAMS.bizHelpDesk.bzPTRStatus
        Try
           
            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATEPTR_STATUS_INPUT><PTR_STATUS HD_PTR_STATUS_ID='' HD_PTR_STATUS_NAME='' HD_PTR_STATUS_CLOSE='' /></HD_UPDATEPTR_STATUS_INPUT>")

            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_ID").Value = hdID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_NAME").Value = txtPTRStatus.Text.Trim()
            If chkClose.Checked = True Then
                objInputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_CLOSE").Value = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_CLOSE").Value = "False"
            End If

            ' Calling update method for update.
            objOutputXml = objPTRStatus.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
            CheckSecurity()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_PTRStatus.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try


            If hdID.Value = "" Then
                txtPTRStatus.Text = ""
                chkClose.Checked = False
                lblError.Text = ""
            Else
                ViewDetails()

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objPTRStatus As New AAMS.bizHelpDesk.bzPTRStatus
        Try
            ' <VIEWPTR_STATUS HD_PTR_STATUS_ID="" HD_PTR_STATUS_NAME="" HD_PTR_STATUS_CLOSE="" />
            objInputXml.LoadXml("<HD_VIEWPTR_STATUS_INPUT><HD_PTR_STATUS_ID /></HD_VIEWPTR_STATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_PTR_STATUS_ID").InnerText = hdID.Value 'Request.QueryString("PTRStatusID").ToString().Trim()
            objOutputXml = objPTRStatus.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtPTRStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_NAME").Value

                If objOutputXml.DocumentElement.SelectSingleNode("PTR_STATUS").Attributes("HD_PTR_STATUS_CLOSE").Value.Trim.ToUpper = "TRUE" Then
                    chkClose.Checked = True
                Else
                    chkClose.Checked = False
                End If

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Status']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("PTRStatusID") IsNot Nothing) Then
                        btnSave.Enabled = False
                    End If

                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        btnSave.Enabled = True
                    End If
                Else

                End If
              
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
