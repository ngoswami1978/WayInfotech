Imports System.Data
Imports System.Xml

Partial Class Setup_MSUP_AirLineOffice
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
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(1) = "0" Then
            '        btnNew.Enabled = False
            '        btnSave.Enabled = False
            '    End If
            '    If strBuilder(2) = "0" And strBuilder(1) = "0" Then 'Add =false /Modify= false
            '        btnSave.Enabled = False
            '    End If
            '    If strBuilder(2) = "0" And strBuilder(1) = "1" Then 'Modify =false /Add= True
            '        If Request.QueryString("Action").ToString() = "U" Then
            '            btnSave.Enabled = False
            '        Else
            '            btnSave.Enabled = True
            '        End If
            '    End If
            '    If strBuilder(2) = "1" And strBuilder(1) = "0" Then 'Modify= true/Add =false
            '        If Request.QueryString("Action").ToString() = "U" Then
            '            btnSave.Enabled = True
            '        Else
            '            btnSave.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

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
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                    If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                        cboAoffice.SelectedValue = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        cboAoffice.Enabled = False
                    End If
                End If
            End If
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(cboAirlineName, "AIRLINE", True)
                objeAAMS.BindDropDown(cboAoffice, "AOFFICE", True)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewAirlineOffice()
        If (Not Request.QueryString("Action") = Nothing) Then
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzAirlineOfice As New AAMS.bizMaster.bzAirlineOffice
            objInputXml.LoadXml("<MS_VIEWAIRLINEOFFICE_INPUT><AR_OF_ID></AR_OF_ID></MS_VIEWAIRLINEOFFICE_INPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("AR_OF_ID").InnerXml = Request.QueryString("Action").ToString().Split("|").GetValue(1)
            objInputXml.DocumentElement.SelectSingleNode("AR_OF_ID").InnerXml = objDe.Decrypt(Request.QueryString("AR_OF_ID").ToString().Trim)

            'Here Back end Method Call
            objOutputXml = objbzAirlineOfice.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("AR_OF_ID") = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").InnerText
                cboAirlineName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Airline_Code").InnerText
                cboAirlineName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Airline_Code").InnerText
                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_Address").InnerText
                cboAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Aoffice").InnerText
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        End If
    End Sub
    Sub BindAirlineOfficeData(ByVal objOutputXml As XmlDocument)
        cboAirlineName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Airline_Code").InnerText
        cboAirlineName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Airline_Code").InnerText
        txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_Address").InnerText
        cboAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("Aoffice").InnerText
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzAirlineOffice As New AAMS.bizMaster.bzAirlineOffice
                objInputXml.LoadXml("<MS_UPDATEAIRLINEOFFICE_INPUT><AIRLINE_OFFICE Action='' AR_OF_ID='' Airline_Code='' AR_OF_Address='' Aoffice='' /></MS_UPDATEAIRLINEOFFICE_INPUT>")

                With objInputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE")
                    'If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("Action").Value() = "U"
                        .Attributes("AR_OF_ID").Value() = ViewState("AR_OF_ID")
                    Else
                        .Attributes("Action").Value() = "I"
                    End If
                    .Attributes("Airline_Code").Value = cboAirlineName.SelectedValue.ToString()
                    .Attributes("AR_OF_Address").Value = txtAddress.Text.ToString()
                    .Attributes("Aoffice").Value = IIf(cboAoffice.SelectedIndex = 0, "", cboAoffice.SelectedValue.ToString())
                End With

                'Here Back end Method Call
                objOutputXml = objbzAirlineOffice.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        Response.Redirect("MSUP_AirLineOffice.aspx?&Msg=U&Action=U&AR_OF_ID=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").Value.Trim), False)
                        lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        Response.Redirect("MSUP_AirLineOffice.aspx?&Msg=A&Action=U&AR_OF_ID=" & objDe.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").Value.Trim), False)
                    End If
                    CheckSecurity()
                    'If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                    '    lblError.Text = "Updated successfully."
                    'Else
                    '    Response.Redirect("MSUP_AirLineOffice.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("AIRLINE_OFFICE").Attributes("AR_OF_ID").Value)
                    'End If
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AirLine Office']").Attributes("Value").Value)
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

            txtAddress.Text = String.Empty
            'cboAoffice.SelectedIndex = 0
            cboAirlineName.SelectedIndex = 0
        End If
    End Sub
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_AirLineOffice.aspx?Action=I")
    'End Sub
End Class
