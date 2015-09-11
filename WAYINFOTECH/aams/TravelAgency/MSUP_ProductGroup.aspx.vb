Imports System.Xml
Imports System.Data
Partial Class Order_MSUP_ProductGroup
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K
#End Region

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzProductGroup As New AAMS.bizTravelAgency.bzProductGroup
                objInputXml.LoadXml("<MS_UPDATEPRODUCTGROUP_INPUT><ProductGroup ACTION='' ProductGroupName='' ProductGroupId=''/></MS_UPDATEPRODUCTGROUP_INPUT>")

                objInputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ProductGroupName").Value = txtGroup.Text
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                    objInputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ProductGroupId").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                    objInputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ACTION").InnerText = "U"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ProductGroupId").InnerText = ""
                    objInputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ACTION").InnerText = "I"
                End If
                
                objOutputXml = objbzProductGroup.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                    Else
                        Response.Redirect("MSUP_ProductGroup.aspx?Action=US|" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ProductGroup").Attributes("ProductGroupId").Value))
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_ProductGroup.aspx?Action=I|")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
            lblError.Text = ""
            ViewProductGroup()
        Else
            txtGroup.Text = ""
            lblError.Text = ""
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            'objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "loginScript", objeAAMS.CheckSession())
            End If
           
           
            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    ViewProductGroup()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    ViewProductGroup()
                End If
            End If

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Product Group']").Attributes("Value").Value)
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    'btnSave.Enabled = False
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub ViewProductGroup()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            'Dim objXmlReader As XmlNodeReader
            Dim dt As New DataTable
            Dim objbzProductGroup As New AAMS.bizTravelAgency.bzProductGroup
            objInputXml.LoadXml("<MS_VIEWPRODUCTGROUP_INPUT><ProductGroupId/></MS_VIEWPRODUCTGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ProductGroupId").InnerText = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
            'Here Back end Method Call
            objOutputXml = objbzProductGroup.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtGroup.Text = objOutputXml.SelectSingleNode("MS_VIEWPRODUCTGROUP_OUTPUT/ProductGroup").Attributes("ProductGroupName").InnerText
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
