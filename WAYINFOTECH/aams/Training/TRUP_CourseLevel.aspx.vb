
Partial Class Training_TRUP_CourseLevel
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
                If Not Request.QueryString("Action") Is Nothing And Not Request.QueryString("CourseLevelId") Is Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("CourseLevelId").ToString().Trim())
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
			Dim objbzCourselevel As New AAMS.bizTraining.bzCourselevel
			' Load Input Xml.
			objInputXml.LoadXml("<TR_UPDATE_COURSELEVEL_INPUT><LEVEL TR_COURSELEVEL_ID='' TR_COURSELEVEL_NAME=''/></TR_UPDATE_COURSELEVEL_INPUT> ")
			If (hdID.Value <> "") Then
				objInputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_ID").Value = hdID.Value
			Else
				objInputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_ID").Value = ""
			End If

			objInputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_NAME").Value = txtCourseLevelName.Text.Trim()

			' Calling update method for update.
			objOutputXml = objbzCourselevel.Update(objInputXml)
			'   Checking error status. 
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
				If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_ID").Value.Trim()
					lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
				Else
					hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_ID").Value.Trim()
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
			Response.Redirect("TRUP_CourseLevel.aspx?Action=I")
		Catch ex As Exception
			lblError.Text = ex.Message
		End Try
	End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
	Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
		Try
			If hdID.Value = "" Then
				txtCourseLevelName.Text = ""
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
		Dim objbzCourselevel As New AAMS.bizTraining.bzCourselevel
		Try
			objInputXml.LoadXml("<TR_VIEW_COURSELEVEL_INPUT><LEVEL TR_COURSELEVEL_ID='' TR_COURSELEVEL_NAME=''/></TR_VIEW_COURSELEVEL_INPUT>")
			objInputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_ID").InnerText = hdID.Value 'Request.QueryString("ContactID").ToString().Trim()
			objOutputXml = objbzCourselevel.View(objInputXml)
			If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
				txtCourseLevelName.Text = objOutputXml.DocumentElement.SelectSingleNode("LEVEL").Attributes("TR_COURSELEVEL_NAME").Value
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
				If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Level']").Count <> 0 Then
					strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Course Level']").Attributes("Value").Value)
				End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("CourseLevelId") IsNot Nothing) Then
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
