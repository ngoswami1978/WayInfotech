
Partial Class BirdresHelpDesk_HDUP_WorkOrderType
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
            txtOrderType.Focus()
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl
                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                'btnReset.Attributes.Add("onClick", "return ClearControls();")
                ' Checking Query String for update .
                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("TypeID") Is Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("TypeID").ToString().Trim())
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

        Try
            Dim objInputXml As New XmlDocument
            Dim objOutputXml As New XmlDocument
            Dim objWOType As New AAMS.bizBRHelpDesk.bzWOType
            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATEWOTYPE_INPUT><WO_TYPE WO_TYPE_ID='' WO_TYPE_NAME='' /></HD_UPDATEWOTYPE_INPUT>")
            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_ID").Value = hdID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_NAME").Value = txtOrderType.Text.Trim()

            ' Calling update method for update.
            objOutputXml = objWOType.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click"
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("HDUP_WorkOrderType.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                txtOrderType.Text = ""
                lblError.Text = ""
            Else
                ' Calling View Method.
                'If Not Request.QueryString("TypeID") Is Nothing Then
                ViewDetails()
                'End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "ViewDetails()"
    Private Sub ViewDetails()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objWOType As New AAMS.bizHelpDesk.bzWOType
        Try
            objInputXml.LoadXml("<HD_VIEWWOTYPE_INPUT><WO_TYPE_ID /></HD_VIEWWOTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("WO_TYPE_ID").InnerText = hdID.Value ' Request.QueryString("TypeID").ToString().Trim()
            objOutputXml = objWOType.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtOrderType.Text = objOutputXml.DocumentElement.SelectSingleNode("WO_TYPE").Attributes("WO_TYPE_NAME").Value
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Work Order Type']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("TypeID") IsNot Nothing) Then
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
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
