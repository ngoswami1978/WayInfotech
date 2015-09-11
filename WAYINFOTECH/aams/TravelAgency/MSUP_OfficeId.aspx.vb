Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class TravelAgency_MSUP_OfficeId
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
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

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If Not Request.QueryString("Action") Is Nothing Then
                If (Request.QueryString("Action").ToUpper() = "U") Then
                    ViewOfficeId()
                Else
                    txtCID.Text = String.Empty
                    txtDOP.Text = String.Empty
                    txtTerminalId.Text = String.Empty
                    txtRemarks.Text = String.Empty
                    txtAgencyName.Focus()
                    lblError.Text = String.Empty
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim ds As New DataSet
        objInputXml.LoadXml("<TA_UPDATEOFFICEID_INPUT><OFFICEID OFFICEID='' NAME='' CID='' PROCESSING_DATE='' TERMINALID='' REMARKS='' /></TA_UPDATEOFFICEID_INPUT>")
        With objInputXml.DocumentElement.SelectSingleNode("OFFICEID")
            If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Then
                .Attributes("OFFICEID").Value() = objED.Decrypt(Request.QueryString("OfficeID").Trim())
                .Attributes("NAME").Value() = txtAgencyName.Text.Trim()
                .Attributes("CID").Value() = txtCID.Text.Trim()
                .Attributes("PROCESSING_DATE").Value() = objeAAMS.ConvertTextDate(txtDOP.Text.Trim())
                .Attributes("TERMINALID").Value() = txtTerminalId.Text.Trim()
                .Attributes("REMARKS").Value() = txtRemarks.Text.Trim()
            Else
                .Attributes("OFFICEID").Value() = txtOfficeId.Text.Trim()
                .Attributes("NAME").Value() = txtAgencyName.Text.Trim()
                .Attributes("CID").Value() = txtCID.Text.Trim()
                .Attributes("PROCESSING_DATE").Value() = objeAAMS.ConvertTextDate(txtDOP.Text.Trim())
                .Attributes("TERMINALID").Value() = txtTerminalId.Text.Trim()
                .Attributes("REMARKS").Value() = txtRemarks.Text.Trim()
            End If
        End With
        objOutputXml = objOfficeId.Update(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Request.QueryString("Action").ToString().ToUpper() = "U" Then
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                lblError.Text = objeAAMSMessage.messInsert
            End If
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
     
        objeAAMS.ExpirePageCache()
        ' This code is usedc for checking session handler according to user login.
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If

        btnSave.Attributes.Add("onclick", "return CheckValidation();")
        If Not Page.IsPostBack Then
            btn_New.Attributes.Add("onclick", "return GenerateFunction();")
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Trim().ToUpper() = "U" Then
                    ViewOfficeId()
                End If
            End If
        End If
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='OfficeId']").Attributes("Value").Value)
            End If

            If strBuilder(0) = "0" Then
                Response.Redirect("~/NoRights.aspx", False)
                Exit Sub
            End If
         
            If strBuilder(1) = "0" Then
                btn_New.Enabled = False
            End If
            If Not Request.QueryString("Action") Is Nothing Then
                If (Request.QueryString("Action").ToUpper() = "U") Then
                    If strBuilder(2) = "0" Then
                        btnSave.Enabled = False
                    End If
                End If
            End If
        Else
            strBuilder = objeAAMS.SecurityCheck(31)
        End If
    End Sub
    Private Sub ViewOfficeId()
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim objInputXml, objOutputXml As New XmlDocument
        objInputXml.LoadXml("<TA_VIEWOFFICEID_INPUT><OFFICEID></OFFICEID></TA_VIEWOFFICEID_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = objED.Decrypt(Request.QueryString("OfficeID").ToString())
        objOutputXml = objOfficeId.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            txtOfficeId.Text = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID").Attributes("OFFICEID").Value()
            txtAgencyName.Text = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID").Attributes("NAME").Value()
            txtCID.Text = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID").Attributes("CID").Value()
            With objOutputXml.DocumentElement.SelectSingleNode("OFFICEID")
                If .Attributes("PROCESSING_DATE").Value() <> "" Then
                    txtDOP.Text = objeAAMS.ConvertDate(.Attributes("PROCESSING_DATE").Value()).ToString("dd/MM/yyyy")
                    txtDOP.Attributes.Add("ReadOnly", "ReadOnly")
                    txtDOP.CssClass = "textboxgrey"

                End If
            End With
            txtTerminalId.Text = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID").Attributes("TERMINALID").Value()
            txtRemarks.Text = objOutputXml.DocumentElement.SelectSingleNode("OFFICEID").Attributes("REMARKS").Value()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btn_New_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_New.Click

    End Sub
End Class

