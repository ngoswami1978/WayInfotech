Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventory_InvUP_Godown
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim str As String
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Inventory/INVUP_Godown.aspx"

            btn_New.Attributes.Add("onclick", "return NewFunction();")
            If Request.QueryString("Action") = "U" Or Request.QueryString("Action") = "US" Then
                drpCity.Enabled = False
            End If

            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    'When Add rights disabled
                    If strBuilder(1) = "0" Then
                        btn_New.Enabled = False
                        btnSave.Enabled = False
                    End If
                    'When modify rights disabled and Add rights enabled
                    If strBuilder(2) = "0" And (Request.QueryString("Action") = "US" Or Request.QueryString("Action") = "U") Then
                        btnSave.Enabled = False
                    End If
                    'When modify rights Enabled and Add rights disabled
                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        btnSave.Enabled = True
                    End If
                End If
            Else
                objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITY", True, 1)
                btnSave.Attributes.Add("onclick", "return ValidateGodown();")

                If Not Request.QueryString("Action") Is Nothing Then
                    If Request.QueryString("Action").ToUpper() = "U" Or Request.QueryString("Action").ToUpper() = "US" Then
                        hdID.Value = objED.Decrypt(Request.QueryString("GODOWNID").ToString().Trim())
                        ViewGodown()
                        txtGodownName.Focus()
                    End If
                    If Request.QueryString("Action").ToUpper() = "US" Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                End If
            End If
            
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpCity.Attributes.Add("OnChange", "return City('drpCity');")
            ' Check Security
            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            Dim objbzCity As New AAMS.bizMaster.bzCity
            Dim objOutputXml As New XmlDocument
            Dim strArr() As String
            str = "|"
            strArr = eventArgument.Split("|")
            objOutputXml = objbzCity.List()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strArr(1) & "']").Count <> 0 Then
                    str = objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strArr(1) & "']").Attributes("Country_Name").Value
                End If
                If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strArr(1) & "']").Count <> 0 Then
                    str = str & "|" & objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strArr(1) & "']").Attributes("Aoffice").Value
                End If
            Else
                str = "|"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    Private Sub ViewGodown()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtGodown As New AAMS.bizInventory.bzGodown
            objInputXml.LoadXml("<IN_VIEWGODOWN_INPUT><GODOWNID/></IN_VIEWGODOWN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("GODOWNID").InnerXml = hdID.Value 'Request.QueryString("GODOWNID").ToString()

            'Here Back end Method Call
            objOutputXml = objtGodown.View(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                txtGodownName.Text = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNNAME").Value()
                txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("ADDRESS").Value()
                txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("PHONE").Value()
                txtPostalCode.Text = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("POSTALCODE").Value()
                drpCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("CITYID").Value()
                txtAoffice.Text = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("AOFFICE").Value()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objInputXml1, objOutputXml1 As New XmlDocument
        Dim objtFeasibilityStatusId As New AAMS.bizInventory.bzGodown
        Dim objbzAgency As New AAMS.bizMaster.bzCity
        Try
            objInputXml1.LoadXml("<MS_GETCITYCODE_INPUT><City_Name></City_Name></MS_GETCITYCODE_INPUT>")
            objInputXml1.DocumentElement.SelectSingleNode("City_Name").InnerXml = drpCity.SelectedItem.ToString()
            objOutputXml1 = objbzAgency.GetCityDetails(objInputXml1)
            objInputXml.LoadXml("<INV_UPDATEGODOWN_INPUT><GODOWN GODOWNID='' GODOWNNAME='' ADDRESS='' PHONE='' POSTALCODE='' AOFFICE='' CITYID='' /></INV_UPDATEGODOWN_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("GODOWN")
                If Request.QueryString("ACTION").ToString().Trim().ToUpper = "U" Or Request.QueryString("ACTION").ToString().Trim().ToUpper = "US" Then
                    .Attributes("GODOWNNAME").Value() = txtGodownName.Text.Trim()
                    .Attributes("ADDRESS").Value() = txtAddress.Text.Trim()
                    .Attributes("PHONE").Value() = txtPhone.Text.Trim()
                    .Attributes("POSTALCODE").Value() = txtPostalCode.Text.Trim()
                    .Attributes("AOFFICE").Value() = objOutputXml1.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").Value()
                    .Attributes("CITYID").Value() = drpCity.SelectedValue
                    .Attributes("GODOWNID").Value() = hdID.Value 'Request.QueryString("GODOWNID")
                Else
                    .Attributes("GODOWNNAME").Value() = txtGodownName.Text.Trim()
                    .Attributes("ADDRESS").Value() = txtAddress.Text.Trim()
                    .Attributes("PHONE").Value() = txtPhone.Text.Trim()
                    .Attributes("POSTALCODE").Value() = txtPostalCode.Text.Trim()
                    .Attributes("AOFFICE").Value() = objOutputXml1.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").Value()
                    .Attributes("CITYID").Value() = drpCity.SelectedValue
                    .Attributes("GODOWNID").Value() = String.Empty
                End If
            End With
            objOutputXml = objtFeasibilityStatusId.Update(objInputXml)
            Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value().Trim()

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If (Request.QueryString("Action").ToString().ToUpper() = "U" Or Request.QueryString("Action").ToString().ToUpper() = "US") Then
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value.Trim()
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("GODOWN").Attributes("GODOWNID").Value().Trim()
                    lblError.Text = objeAAMSMessage.messInsert
                    objED.Encrypt(CheckBoxOb)
                    Response.Redirect("INVUP_Godown.aspx?Action=US&GODOWNID=" + objED.Encrypt(CheckBoxOb))



                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Not Request.QueryString("Action") Is Nothing Then
            If (Request.QueryString("Action").ToUpper() = "U") Then
                ViewGodown()
                txtGodownName.Focus()
            Else
                txtGodownName.Text = ""
                txtAddress.Text = ""
                txtPhone.Text = ""
                txtPostalCode.Text = ""
                drpCity.SelectedIndex = -1
                txtAoffice.Text = ""
                lblError.Text = ""
            End If
        End If
    End Sub

    'Protected Sub drpCity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCity.SelectedIndexChanged
    '    Dim objInputXml1, objOutputXml1 As New XmlDocument
    '    Dim objbzAgency As New AAMS.bizMaster.bzCity
    '    objInputXml1.LoadXml("<MS_GETCITYCODE_INPUT><City_Name></City_Name></MS_GETCITYCODE_INPUT>")
    '    objInputXml1.DocumentElement.SelectSingleNode("City_Name").InnerXml = drpCity.SelectedItem.ToString()
    '    objOutputXml1 = objbzAgency.GetCityDetails(objInputXml1)
    '    txtAoffice.Text = objOutputXml1.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("Aoffice").Value()
    'End Sub

    Private Sub CheckSecurity()
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE GODOWN']").Attributes("Value").Value)
                End If
                'If strBuilder(1) = "0" Then
                '    btn_New.Enabled = False
                'End If

                'When View rights disabled
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx", False)
                    btnSave.Enabled = False
                End If
                'When Add rights disabled
                If strBuilder(1) = "0" Then
                    btn_New.Enabled = False
                    btnSave.Enabled = False
                End If
                'When modify rights disabled and Add rights enabled
                If strBuilder(2) = "0" And (Request.QueryString("Action") = "US") Then
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
End Class
