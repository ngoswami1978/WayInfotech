
Partial Class BirdresHelpDesk_MSUP_CustomerCategory
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
                Dim strurl As String = Request.Url.ToString()
                Session("PageName") = strurl

                btnSave.Attributes.Add("onClick", "return ValidateForm();")
                'btnReset.Attributes.Add("onClick", "return ClearControls();")
                ' Checking Query String for update .
                If Request.QueryString("Action") IsNot Nothing And Request.QueryString("CustCategoryID") IsNot Nothing Then
                    hdID.Value = objED.Decrypt(Request.QueryString("CustCategoryID").ToString().Trim())
                    ViewDetails()
                End If
                ' Checking security.
                CheckSecurity()
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
            Dim objCustomerCategory As New AAMS.bizBRHelpDesk.bzCustomerCategory
            ' Load Input Xml.
            objInputXml.LoadXml("<HD_UPDATECUSTOMERCATEGORY_INPUT><CUSTOMER_CATEGORY CUSTOMER_CATEGORY_ID='' CUSTOMER_CATEGORY_NAME='' COLOR_CODE='' /></HD_UPDATECUSTOMERCATEGORY_INPUT>")

            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_ID").Value = hdID.Value
            Else
                objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_ID").Value = ""
            End If

            objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_NAME").Value = txtCategory.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value = hdColor.Value

            ' Calling update method for update.
            objOutputXml = objCustomerCategory.Update(objInputXml)
            '   Checking error status. 
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or hdID.Value <> "") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_ID").Value.Trim()
                    ' txtColor.Text = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value.Trim()
                    hdColor.Value = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value.Trim()
                    txtColor.Attributes.Add("style", "background-color:" + hdColor.Value)
                    lblError.Text = objeAAMSMessage.messUpdate ' Record updated successfully.
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_ID").Value.Trim()
                    ' txtColor.Text = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value.Trim()
                    hdColor.Value = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value.Trim()
                    txtColor.Attributes.Add("style", "background-color:" + hdColor.Value)
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
            Response.Redirect("HDUP_CustomerCategory.aspx?Action=I")
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
                txtColor.Text = ""
                lblError.Text = ""
            Else
                ' Calling View Method.
                'If Not Request.QueryString("CustCategoryID") Is Nothing Then
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
        Dim objCustomerCategory As New AAMS.bizBRHelpDesk.bzCustomerCategory
        Try
            objInputXml.LoadXml("<HD_VIEWCUSTOMERCATEGORY_INPUT><CUSTOMER_CATEGORY_ID /></HD_VIEWCUSTOMERCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY_ID").InnerText = hdID.Value 'Request.QueryString("CustCategoryID").ToString().Trim()
            objOutputXml = objCustomerCategory.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtCategory.Text = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("CUSTOMER_CATEGORY_NAME").Value
                '    txtColor.Text = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value
                hdColor.Value = objOutputXml.DocumentElement.SelectSingleNode("CUSTOMER_CATEGORY").Attributes("COLOR_CODE").Value
                txtColor.Attributes.Add("style", "background-color:" + hdColor.Value)
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Customer Category']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Customer Category']").Attributes("Value").Value)
                End If
                'When View rights disabled
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("CustCategoryID") IsNot Nothing) Then
                    btnSave.Enabled = False
                End If
                'When modify rights Enabled and Add rights disabled
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
