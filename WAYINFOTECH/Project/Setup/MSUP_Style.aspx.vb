Imports System.Data
Imports System.Xml

Partial Class Setup_MSUP_Style
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objDe As New EncyrptDeCyrpt

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'Session("PageName") = "Setup/MSUP_AirLineOffice.aspx"
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl

            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            btnSave.Attributes.Add("onclick", "return CheckMendatoty();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty

            'Index 0 View
            'Index 1 Add
            'Index 2 Modify
            'Index 3 Delete
            'Index 4 Print
            Dim objSecurityXml As New XmlDocument
            CheckSecurity()
            If Not Page.IsPostBack Then
                'Check Condition for Action Update or Insert
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        ViewAirlineOffice()
                        If (Request.QueryString("Msg") = "A") Then
                            lblError.Text = objeAAMSMessage.messInsert
                        End If
                        If (Request.QueryString("Msg") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                    End If
                End If
            End If
            
            If Not Page.IsPostBack Then
                '// NO Need to Bind any Control here
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewAirlineOffice()
        If (Not Request.QueryString("Action") = Nothing) Then
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzStyle As New WAY.bizMaster.bzStyle
            objInputXml.LoadXml("<MS_VIEWSTYLE_INPUT><W_StyleId></W_StyleId></MS_VIEWSTYLE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("W_StyleId").InnerXml = objDe.Decrypt(Request.QueryString("W_StyleId").ToString().Trim)
            'Here Back end Method Call
            objOutputXml = objbzStyle.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("STYLE") = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").InnerText

                txtBarCode.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("BarcodeNo").InnerText
                txtStyleName.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("StyleName").InnerText
                txtDesignName.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("DesignNo").InnerText
                txtShadeNo.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("ShadeNo").InnerText
                txtMRP.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("MRP").InnerText

            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub
    Sub BindAirlineOfficeData(ByVal objOutputXml As XmlDocument)
        txtBarCode.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("BarCode").InnerText
        txtStyleName.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("StyleName").InnerText
        txtDesignName.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("DesignName").InnerText
        txtShadeNo.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("ShadeNo").InnerText
        txtMRP.Text = objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("MRP").InnerText
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzStyle As New WAY.bizMaster.bzStyle
                objInputXml.LoadXml("<MS_UPDATESTYLE_INPUT><STYLE Action='' W_StyleId='' BarcodeNo='' StyleName='' DesignNo='' ShadeNo='' MRP='' /></MS_UPDATESTYLE_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("STYLE")
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("Action").Value() = "U"
                        .Attributes("W_StyleId").Value() = ViewState("STYLE")
                    Else
                        .Attributes("Action").Value() = "I"
                    End If
                    .Attributes("BarcodeNo").Value = txtBarCode.Text
                    .Attributes("StyleName").Value = txtStyleName.Text
                    .Attributes("DesignNo").Value = txtDesignName.Text
                    .Attributes("ShadeNo").Value = txtShadeNo.Text
                    .Attributes("MRP").Value = txtMRP.Text
                End With

                'Here Back end Method Call
                objOutputXml = objbzStyle.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        Response.Redirect("MSUP_Style.aspx?&Msg=U&Action=U&W_StyleId=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").Value.Trim), False)
                        lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        Response.Redirect("MSUP_Style.aspx?&Msg=A&Action=U&W_StyleId=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STYLE").Attributes("W_StyleId").Value.Trim), False)
                    End If
                    CheckSecurity()
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If

            End If
        End If
    End Sub
    Private Sub CheckSecurity()
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Style']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Style']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = False
                    Else
                        btnSave.Enabled = True
                    End If
                End If
                If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
                    If Request.QueryString("Action").ToString() = "U" Then
                        btnSave.Enabled = True
                    Else
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
            ViewAirlineOffice()
        Else
            txtBarCode.Text = String.Empty
            txtStyleName.Text = String.Empty
            txtDesignName.Text = String.Empty
            txtShadeNo.Text = String.Empty
            txtMRP.Text = String.Empty
        End If
    End Sub
End Class
