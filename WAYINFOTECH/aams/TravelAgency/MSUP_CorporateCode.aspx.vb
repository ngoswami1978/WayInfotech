'##############################################################
'############   Page Name -- Order_MSUP_CorporateCode #########
'############   Date 29-November 2007  ########################
'############   Developed By Mukund Kumar  ####################
'##############################################################
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Order_MSUP_CorporateCode
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
#Region "Global Declaration Section"
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objMsg As New eAAMSMessage
    Dim objED As New EncyrptDeCyrpt

#End Region


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

#Region "Form Load Code Section"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            btnSave.Attributes.Add("onclick", "return CorporateCodeMandatory();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            If (Not Page.IsPostBack) Then
                If Not Request.QueryString("Action") Is Nothing Then
                    If (Request.QueryString("Action").ToUpper() = "U") Then
                        ViewCorporateCode()
                        txtCorporateQualifier.Focus()
                    End If
                    If (Request.QueryString("Action").ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messInsert
                        ViewCorporateCode()
                    End If
                End If
            End If
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Corporate Codes']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx", False)
                    Exit Sub
                End If
             
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If Request.QueryString("Action").ToString() = "US" Or Request.QueryString("Action").ToString() = "U" Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Save Button Events Definition"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objtaCorporateCode As New AAMS.bizTravelAgency.bzCorporateCodes
        objInputXml.LoadXml("<MS_UPDATEOFFICEIDQUALIFIERS_INPUT><Qualifiers ACTION='' RowID='' Code='' Qualifier='' Description='' /></MS_UPDATEOFFICEIDQUALIFIERS_INPUT>")
        With objInputXml.DocumentElement.SelectSingleNode("Qualifiers")
            If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                .Attributes("ACTION").Value() = "U"
                .Attributes("RowID").Value() = objED.Decrypt(Request.QueryString("CorporateRowID").Trim())
                .Attributes("Code").Value() = txtCorporationCode.Text.Trim()
                .Attributes("Qualifier").Value() = txtCorporateQualifier.Text.Trim()
                .Attributes("Description").Value() = txtCorporateDesc.Text.Trim()
            Else
                .Attributes("ACTION").Value() = "I"
                .Attributes("RowID").Value() = ""
                .Attributes("Code").Value() = txtCorporationCode.Text.Trim()
                .Attributes("Qualifier").Value() = txtCorporateQualifier.Text.Trim()
                .Attributes("Description").Value() = txtCorporateDesc.Text.Trim()
            End If
        End With
        objOutputXml = objtaCorporateCode.Update(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US" Then
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                'lblError.Text = "Record Added successfully."
                Response.Redirect("MSUP_CorporateCode.aspx?Action=US&CorporateRowID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("Qualifiers").Attributes("RowID").Value), False)
            End If
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#End Region

#Region "Reset Buttion Events Definition"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Not Request.QueryString("Action") Is Nothing Then
            If (Request.QueryString("Action").ToUpper() = "U") Or (Request.QueryString("Action").ToUpper() = "US") Then
                ViewCorporateCode()
                txtCorporateQualifier.Focus()
            Else
                txtCorporateDesc.Text = ""
                txtCorporateQualifier.Text = ""
                txtCorporationCode.Text = ""
            End If
        End If
    End Sub
#End Region

#Region "Corporate Code Displaying"
    Private Sub ViewCorporateCode()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtaCorporateCode As New AAMS.bizTravelAgency.bzCorporateCodes
            objInputXml.LoadXml("<MS_VIEWOFFICEIDQUALIFIERS_INPUT><RowID /></MS_VIEWOFFICEIDQUALIFIERS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("RowID").InnerXml = objED.Decrypt(Request.QueryString("CorporateRowID").ToString())
            'Here Back end Method Call
            objOutputXml = objtaCorporateCode.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtCorporationCode.Text = objOutputXml.DocumentElement.SelectSingleNode("Qualifiers").Attributes("Code").Value()
                txtCorporateQualifier.Text = objOutputXml.DocumentElement.SelectSingleNode("Qualifiers").Attributes("Qualifier").Value()
                txtCorporateDesc.Text = objOutputXml.DocumentElement.SelectSingleNode("Qualifiers").Attributes("Description").Value()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class
