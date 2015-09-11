
Partial Class ISP_ISPUP_POP
    Inherits System.Web.UI.Page
#Region "Global Variable Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzProvider As New AAMS.bizISP.bzPOP
                    objInputXml.LoadXml("<IS_UPDATE_PROVIDER_INPUT><ISP PopID= '' PopName='' /></IS_UPDATE_PROVIDER_INPUT>")
                    If (Request.QueryString("ProviderID") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PopID").Value = objED.Decrypt(Request.QueryString("ProviderID"))
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PopName").Value = txtISPProvider.Text


                    'Here Back end Method Call
                    objOutputXml = objbzProvider.Update(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("ISPUP_POP.aspx?&Msg=U&Action=U&ProviderID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PopID").Value), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("ISPUP_POP.aspx?&Msg=A&Action=U&ProviderID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PopID").Value), False)
                        End If
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            btnSave.Attributes.Add("onclick", "return ProviderMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPProvider();")
            If Not Page.IsPostBack Then
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    If Request.QueryString("ProviderID") IsNot Nothing Then
                        ViewIspProvider()
                    End If

                    txtISPProvider.Focus()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If
            End If


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPpop']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Manage IPpop']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False

                    End If
                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#Region " Load Isp Provider  Data For Editing Mode"
    Sub ViewIspProvider()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument

                Dim ds As New DataSet
                Dim objbzProvider As New AAMS.bizISP.bzPOP
                objInputXml.LoadXml("<IS_VIEW_PROVIDER_INPUT><PopID></PopID></IS_VIEW_PROVIDER_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("PopID").InnerText = objED.Decrypt(Request.QueryString("ProviderID").ToString())
                'Here Back end Method Call
                objOutputXml = objbzProvider.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindIspProviderData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindIspProviderData For Binding Data in Controls"
    Sub BindIspProviderData(ByVal objOutputXml As XmlDocument)
        Try

            txtISPProvider.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PopName").InnerXml

            txtISPProvider.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewIspProvider()
                txtISPProvider.Focus()
            Else
                txtISPProvider.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region
End Class
