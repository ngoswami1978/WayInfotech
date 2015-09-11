Imports System.Data

Partial Class ISP_MSUP_ISP
    Inherits System.Web.UI.Page
    Dim eaamsObj As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl

        lblError.Text = ""
        btnSave.Attributes.Add("onclick", "return validateISP();")
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", eaamsObj.CheckSession())
            Exit Sub
        End If
        drpCityName.Attributes.Add("onkeyup", "return gotop('drpCityName');")

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Count <> 0 Then
                strBuilder = eaamsObj.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Isp']").Attributes("Value").Value)
                If strBuilder(0) = "0" Then
                    Response.Redirect("~/NoRights.aspx")
                    Exit Sub
                End If


                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    'btnSave.Enabled = False
                End If

                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
                    End If
                End If
            End If
        Else
            strBuilder = eaamsObj.SecurityCheck(31)
        End If

        If Not Page.IsPostBack Then
            eaamsObj.BindDropDown(drpCityName, "CITY", True)
            eaamsObj.BindDropDown(drpIspProvider, "ISPPROVIDER", True)
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    'txtISPName.ReadOnly = True
                    ' txtISPName.CssClass = "textboxgrey"
                    ViewISP()
                End If
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                End If
            End If

        End If
    End Sub
    Sub ViewISP()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument

            Dim objbzIsp As New AAMS.bizISP.bzISP
            objInputXml.LoadXml("<IS_VIEWISP_INPUT><ISPID /></IS_VIEWISP_INPUT>")
            If Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() IsNot Nothing Or Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString() <> " " Then

                '@ Getting  Decrrepted Data 
                Dim DecreptedIspId As String
                DecreptedIspId = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                '@ End of Getting Decrepted Data


                'objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = DecreptedIspId
            Else
                Exit Sub
            End If

            objOutputXml = objbzIsp.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("CityID").Value.Trim() = "" Then
                    drpCityName.SelectedValue = "0" 'objOutputXml.SelectSingleNode("ISP").Attributes("CityID").Value.Trim()
                Else
                    drpCityName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("CityID").Value.Trim()
                End If
                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Address").Value.Trim()
                txtCtcPerson.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("CTCName").Value.Trim()
                txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Email").Value.Trim()
                txtFaxNo.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Fax").Value.Trim()
                txtISPName.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Name").Value.Trim()
                txtPhoneNo.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Phone").Value.Trim()
                txtPinCode.Text = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PinCode").Value.Trim()

                Dim li As ListItem
                li = drpIspProvider.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ProviderID").Value.Trim())
                If li IsNot Nothing Then
                    drpIspProvider.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ProviderID").Value.Trim()
                End If


            Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If (Not Request.QueryString("Action") = Nothing) Then

                Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                Dim objbzProduct As New AAMS.bizISP.bzISP

                'objInputXml.LoadXml("<IS_UPDATEISP_INPUT><ISP ISPID='' Name='' CityID='' Address='' PinCode='' CTCName='' Phone='' Fax='' Email='' /></IS_UPDATEISP_INPUT>")
                objInputXml.LoadXml("<IS_UPDATEISP_INPUT><ISP ISPID='' Name='' CityID='' Address='' PinCode='' CTCName='' Phone='' Fax='' Email='' ProviderID='' /></IS_UPDATEISP_INPUT>")
                If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then

                    '@ Getting  Decrepted Data 
                    Dim DecreptedIspId As String
                    DecreptedIspId = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                    '@ End of Getting Decrepted Data

                    objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ISPID").Value = DecreptedIspId
                    'objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ISPID").Value = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                Else
                    objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ISPID").Value = ""
                End If

                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("CityID").Value = drpCityName.SelectedValue
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Name").InnerText = txtISPName.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Address").InnerText = txtAddress.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("PinCode").InnerText = txtPinCode.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("CTCName").InnerText = txtCtcPerson.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Phone").InnerText = txtPhoneNo.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Fax").InnerText = txtFaxNo.Text.Trim()
                objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("Email").InnerText = txtEmail.Text.Trim()
                '
                If drpIspProvider.SelectedValue <> "" Then
                    objInputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ProviderID").InnerText = drpIspProvider.SelectedValue
                End If

                objOutputXml = objbzProduct.Update(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If (Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US") Then
                        lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                    Else

                        '@ Getting  Encrepted Data 
                        Dim EncreptedIspId As String
                        EncreptedIspId = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ISPID").Value)
                        '@ End of Getting Encrepted Data

                        Response.Redirect("MSUP_ISP.aspx?Action=US|" & EncreptedIspId)

                        'Response.Redirect("MSUP_ISP.aspx?Action=US|" & objOutputXml.DocumentElement.SelectSingleNode("ISP").Attributes("ISPID").Value)
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
        Try
            Response.Redirect("MSUP_ISP.aspx?Action=I|")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            lblError.Text = ""
            If Request.QueryString("Action") IsNot Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    ViewISP()
                Else
                    txtAddress.Text = ""
                    txtCtcPerson.Text = ""
                    txtEmail.Text = ""
                    txtFaxNo.Text = ""
                    txtPhoneNo.Text = ""
                    txtISPName.Text = ""
                    txtPinCode.Text = ""
                    drpCityName.SelectedIndex = 0
                    lblError.Text = ""
                    drpIspProvider.SelectedValue = ""
                End If
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
       
    End Sub

    Protected Sub txtCtcPerson_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCtcPerson.TextChanged

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
