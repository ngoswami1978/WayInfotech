
Partial Class BOHelpDesk_Technical_MSUP_QueryCategory
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
            txtCategory.Focus()
            If Not IsPostBack Then

                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                'btnReset.Attributes.Add("onClick", "return ClearControls();")
                ' Checking Query String for update .
                objeAAMS.BindDropDown(drpSubGroup, "BOTQUERYSUBGROUP", True, 1)
                If Not Request.QueryString("Action") = Nothing And Not Request.QueryString("QueryCategoryID") = Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("QueryCategoryID").ToString().Trim())
                    ViewDetails()
                End If
                ' Checking security.

            End If
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
            Dim objCallCategory As New AAMS.bizBOHelpDesk.bzCallCategory
            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATECALLCATEGORY_INPUT><CALL_CATEGORY CALL_CATEGORY_ID='' CALL_CATEGORY_NAME='' CALL_SUB_GROUP_ID='' HD_QUERY_GROUP_ID='2' /></HD_UPDATECALLCATEGORY_INPUT>")

            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_ID").Value = hdID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_NAME").Value = txtCategory.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_SUB_GROUP_ID").Value = drpSubGroup.SelectedValue

            ' Calling update method for update.
            objOutputXml = objCallCategory.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_ID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messInsert ' Record inserted successfully.
                    drpSubGroup.Enabled = False
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
            Response.Redirect("BOTCUP_QueryCategory.aspx?Action=I")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If hdID.Value = "" Then
                txtCategory.Text = ""
                drpSubGroup.SelectedIndex = 0
                lblError.Text = ""
            Else
                ' Calling View Method.
                'If Not Request.QueryString("QueryCategoryID") Is Nothing Then
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
        Dim objCallCategory As New AAMS.bizBOHelpDesk.bzCallCategory
        Try
            objInputXml.LoadXml("<HD_VIEWCALLCATEGORY_INPUT><CALL_CATEGORY_ID /></HD_VIEWCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY_ID").InnerText = hdID.Value 'Request.QueryString("QueryCategoryID").ToString().Trim()
            objOutputXml = objCallCategory.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtCategory.Text = objOutputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_CATEGORY_NAME").Value
                drpSubGroup.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_CATEGORY").Attributes("CALL_SUB_GROUP_ID").Value
                drpSubGroup.Enabled = False
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
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Tech Query Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BO Tech Query Category']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("QueryCategoryID") IsNot Nothing) Then
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
