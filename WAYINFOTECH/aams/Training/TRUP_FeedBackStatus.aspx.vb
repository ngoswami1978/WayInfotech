
Partial Class Training_TRUP_FeedBackStatus
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
                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("FeedBackStatusId") Is Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("FeedBackStatusId").ToString().Trim())
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
			Dim objbzFeedBackLevel As New AAMS.bizTraining.bzFeedBackLevel
			' Load Input Xml.
            objInputXml.LoadXml("<TR_UPDATE_PARTMOOD_INPUT><PTMOOD TR_PART_MOOD_ID='' TR_PART_MOOD_NAME='' TR_PART_MOOD_ORDER=''/></TR_UPDATE_PARTMOOD_INPUT>")
			If (hdID.Value <> "") Then
				objInputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ID").Value = hdID.Value
			Else
				objInputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ID").Value = ""
			End If

			objInputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_NAME").Value = txtFeedBackStatusName.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ORDER").Value = txtOrderNo.Text.Trim()
			' Calling update method for update.
			objOutputXml = objbzFeedBackLevel.Update(objInputXml)
			'   Checking error status. 
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
				If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ID").Value.Trim()
					lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
				Else
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ID").Value.Trim()
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
			Response.Redirect("TRUP_FeedBackStatus.aspx?Action=I")
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
		Try
			If hdID.Value = "" Then
                txtFeedBackStatusName.Text = ""
                txtOrderNo.Text = ""
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
		Dim objbzFeedBackLevel As New AAMS.bizTraining.bzFeedBackLevel
		Try
			objInputXml.LoadXml("<TR_VIEW_PARTMOOD_INPUT><PTMOOD TR_PART_MOOD_ID=''/></TR_VIEW_PARTMOOD_INPUT>")
			objInputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ID").InnerText = hdID.Value	'Request.QueryString("ContactID").ToString().Trim()
			objOutputXml = objbzFeedBackLevel.View(objInputXml)
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtFeedBackStatusName.Text = objOutputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_NAME").Value
                txtOrderNo.Text = objOutputXml.DocumentElement.SelectSingleNode("PTMOOD").Attributes("TR_PART_MOOD_ORDER").Value
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='FeedBack Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='FeedBack Status']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("FeedBackStatusId") IsNot Nothing) Then
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