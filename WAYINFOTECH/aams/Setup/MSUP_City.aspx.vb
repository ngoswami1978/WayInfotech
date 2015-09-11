'#####################################################
'############   Page Name -- MSUP_City.aspx  #########  
'############   Date 7-November 2007  ################
'############   Developed By Abhishek  ###############
'#####################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class AMS_City_CRS_AS_CityUP
    Inherits System.Web.UI.Page
#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt
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
#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Session("PageName") = "Setup/MSUP_City.aspx"
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            btnSave.Attributes.Add("onclick", "return CheckMandatoty();")
            btnNew.Attributes.Add("onclick", "return NewMSUPCity();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAOff, "AOFFICE", True, 1)
                objeAAMS.BindDropDown(drpLstState, "STATE", True, 1)
                objeAAMS.BindDropDown(drpLstCountry, "COUNTRY", True, 1)
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewCity()
                    txtCityName.Focus()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

            End If


            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
            '        If strBuilder(1) = "0" Then
            '            btnNew.Enabled = False
            '            'btnSave.Enabled = False
            '        End If
            '        If (Request.QueryString("Action") = "U") Then
            '            If strBuilder(2) = "0" Then
            '                btnSave.Enabled = False
            '            End If
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            ' btnReset.Attributes.Add("onclick", "return CityReset();")
         

            CheckSecurity()
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpLstAOff.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpLstAOff.SelectedValue = li.Value
                            End If
                        End If
                        drpLstAOff.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_City.aspx?Action=I")
    'End Sub
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            'Dim strBuilder1 As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
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
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " Load City Data For Editing Mode"
    Sub ViewCity()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzCity As New AAMS.bizMaster.bzCity
                objInputXml.LoadXml("<MS_VIEWCITY_INPUT><CityID></CityID></MS_VIEWCITY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = objEn.Decrypt(Request.QueryString("CityID").ToString().Trim)
                'Here Back end Method Call
                objOutputXml = objbzCity.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindCityData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindCityData For Binding Data in Controls"
    Sub BindCityData(ByVal objOutputXml As XmlDocument)
        Try
            txtCtyCode.Text = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityCode").InnerXml
            txtCityName.Text = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("City_Name").InnerXml
            drpLstAOff.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").InnerXml
            drpLstCountry.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CountryID").InnerXml
            drpLstState.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("StateID").InnerXml
            txtCtyCode.ReadOnly = True
            txtCtyCode.CssClass = "textboxgrey"
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSave_Click Event"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (IsValid) Then
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzCity As New AAMS.bizMaster.bzCity
                    objInputXml.LoadXml("<MS_UPDATECITY_INPUT><CITY_DETAIL Action='' CityID='' CityCode='' City_Name='' Aoffice='' CountryID='' StateID='' /></MS_UPDATECITY_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Action").Value = "U"
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityID").Value = objEn.Decrypt(Request.QueryString("CityID").Trim)
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Action").Value = "I"
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityID").Value = ""
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityCode").Value = txtCtyCode.Text
                    objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("City_Name").Value = txtCityName.Text
                    'If (drpLstAOff.SelectedIndex = 0) Then
                    '    objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").Value = ""
                    'Else
                    objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").Value = drpLstAOff.SelectedValue
                    'End If
                    If (drpLstState.SelectedIndex = 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("StateID").Value = ""
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("StateID").Value = drpLstState.SelectedValue
                    End If
                    'If (drpLstCountry.SelectedIndex = 0) Then
                    'objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CountryID").Value = ""
                    'Else
                    objInputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CountryID").Value = drpLstCountry.SelectedValue
                    ' End If
                    'Here Back end Method Call
                    objOutputXml = objbzCity.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("MSUP_City.aspx?&Msg=U&Action=U&CityID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityID").Value.Trim), False)
                        Else

                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("MSUP_City.aspx?&Msg=A&Action=U&CityID=" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityID").Value.Trim), False)
                        End If
                        CheckSecurity()
                    Else
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewCity()
                txtCtyCode.ReadOnly = True
                txtCtyCode.CssClass = "textboxgrey"
                txtCityName.Focus()
            Else
                txtCtyCode.Text = ""
                txtCityName.Text = ""
                'drpLstAOff.SelectedIndex = 0
                drpLstCountry.SelectedIndex = 0
                drpLstState.SelectedIndex = 0

            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
