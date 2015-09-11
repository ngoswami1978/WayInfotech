
Partial Class Training_TRUP_Domain
    Inherits System.Web.UI.Page

#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
	Dim objeAAMSMessage As New eAAMSMessage
	Public strBuilder As New StringBuilder
#End Region
#Region "Page Filter()"
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Try
            ' Checking security.
            CheckSecurity()
            If Not IsPostBack Then
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl

                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                'btnReset.Attributes.Add("onClick", "return ClearControls();")
                ' Checking Query String for update .
                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("DomainId") Is Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("DomainId").ToString().Trim())
                    ViewDetails()
                End If

            End If

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
			Dim objbzDomain As New AAMS.bizTraining.bzDomain
			' Load Input Xml.
			objInputXml.LoadXml("<TR_UPDATE_DOMAIN_INPUT><DOMAIN TR_VALTOPICDOM_ID='' TR_VALTOPICDOM_NAME='' TR_DOMAIN_ORDER='' TR_VALTOPICDOM_PROTECT=''/></TR_UPDATE_DOMAIN_INPUT>")
			If (hdID.Value <> "") Then
				objInputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_ID").Value = hdID.Value
			Else
				objInputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_ID").Value = ""
			End If
            objInputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_DOMAIN_ORDER").Value = txtOrderNo.Text.Trim()
			objInputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_NAME").Value = txtDomainName.Text.Trim()

			' Calling update method for update.
			objOutputXml = objbzDomain.Update(objInputXml)
			'   Checking error status. 
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
				If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_ID").Value.Trim()
					lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
				Else
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_ID").Value.Trim()
					lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.

                End If
                ' Checking security.
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
			Response.Redirect("TRUP_Domain.aspx?Action=I")
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
		Try
			If hdID.Value = "" Then
				txtDomainName.Text = ""
				lblError.Text = ""
			Else
				' Calling View Method.
				' If Not Request.QueryString("ContactID") Is Nothing Then
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
		Dim objbzDomain As New AAMS.bizTraining.bzDomain
		Try
			objInputXml.LoadXml("<TR_VIEW_DOMAIN_INPUT><DOMAIN TR_VALTOPICDOM_ID='' TR_VALTOPICDOM_NAME='' /></TR_VIEW_DOMAIN_INPUT>")
			objInputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_ID").InnerText = hdID.Value 'Request.QueryString("ContactID").ToString().Trim()
			objOutputXml = objbzDomain.View(objInputXml)
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtDomainName.Text = objOutputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_VALTOPICDOM_NAME").Value
                txtOrderNo.Text = objOutputXml.DocumentElement.SelectSingleNode("DOMAIN").Attributes("TR_DOMAIN_ORDER").Value
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Domain']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Feedback Domain']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("DomainId") IsNot Nothing) Then
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


End Class
