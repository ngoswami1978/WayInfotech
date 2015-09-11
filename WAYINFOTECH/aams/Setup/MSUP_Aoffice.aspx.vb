Imports System.Xml
Imports System.Data
Partial Class Setup_MSUP_Aoffice
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Public strBuilder As New StringBuilder
    Dim objEn As New EncyrptDeCyrpt


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
        Session("PageName") = Request.Url.ToString()
        ' btnReset.Attributes.Add("onclick", "return AofficeReset();")
        btnSave.Attributes.Add("onclick", "return AofficeMandatory();")
        Try
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument

            CheckSecurity()

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITY", True)
                'objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
                objeAAMS.BindDropDown(drpRegion, "REGION", True)
                '' objeAAMS.BindDropDown(drpRegionHQ, "REGIONHQ", True)
                'Check Condition for Action Update or Insert
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                    txtAofficeCode.CssClass = "textboxgrey"
                    txtAofficeCode.ReadOnly = True
                    AofficeView()
                End If

                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    txtAofficeCode.CssClass = "textboxgrey"
                    txtAofficeCode.ReadOnly = True
                    AofficeView()

                    If Session("Security") Is Nothing Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                        Exit Sub
                    End If



                End If

            End If

            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpCity.Attributes.Add("OnChange", "return SendCustomerID('drpCity');")

            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()

            '***************************************************************************************
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            'Dim strBuilder1 As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Aoffice']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Aoffice']").Attributes("Value").Value)
            End If
            If strBuilder(0) = "0" Then
                Response.Redirect("../NoRights.aspx")
            End If

            If strBuilder(0) = "0" Then
                btnSave.Enabled = False
            End If
            'When Add rights disabled
            If strBuilder(1) = "0" Then
                btnNew.Enabled = False
                btnSave.Enabled = False
            End If
            'When modify rights disabled and Add rights enabled
            If strBuilder(2) = "0" And (Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US") Then
                btnSave.Enabled = False
            End If
            'When modify rights Enabled and Add rights disabled
            If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                btnSave.Enabled = True
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim objbzCity As New AAMS.bizMaster.bzCity
        Dim objOutputXml As New XmlDocument
        Dim strArr() As String
        strArr = eventArgument.Split("|")
        objOutputXml = objbzCity.List()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strArr(1) & "']").Count <> 0 Then
                str = objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strArr(1) & "']").Attributes("Country_Name").Value
            End If
        End If
    End Sub

    '*********************************************************************************************************
    '****************************Method for View Aoffice *****************************************************
    '*********************************************************************************************************
    Private Sub AofficeView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
        objInputXml.LoadXml("<MS_VIEWAOFFICE_INPUT><Aoffice></Aoffice></MS_VIEWAOFFICE_INPUT>")
        If Request.QueryString("Action").ToString().Split("|").GetValue(1) IsNot Nothing Or Request.QueryString("Action").ToString().Split("|").GetValue(1) <> "" Then
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = objEn.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1))
        Else
            Exit Sub
        End If
        'Here Back end Method Call
        objOutputXml = objbzAOffice.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("AOFFICE")
                txtAofficeCode.Text = .Attributes("Aoffice").Value()
                drpRegion.SelectedValue = .Attributes("Region").Value()
                txtAofficeAddress.Text = .Attributes("Address").Value()
                drpRegionHQ.SelectedValue = .Attributes("RegionalHQ").Value()
                If .Attributes("BRLimit").Value() = "0" Then
                    txtBDR.Text = ""
                Else
                    txtBDR.Text = .Attributes("BRLimit").Value()
                End If
                txtfax.Text = .Attributes("Fax").Value()
                txtPinCode.Text = .Attributes("Pincode").Value()
                drpCity.SelectedValue = .Attributes("CityID").Value()
                If .Attributes("CityID").Value() <> "" Then
                    txtCountry.Text = FindCountry(.Attributes("CityID").Value())
                End If
                txtPhone.Text = .Attributes("Phone").Value()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Protected Function FindCountry(ByVal strCity As String) As String
        Dim objbzCity As New AAMS.bizMaster.bzCity
        Dim objOutputXml As New XmlDocument
        objOutputXml = objbzCity.List()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If objOutputXml.DocumentElement.SelectNodes("CITY[@CityID='" & strCity & "']").Count <> 0 Then
                Return objOutputXml.DocumentElement.SelectSingleNode("CITY[@CityID='" & strCity & "']").Attributes("Country_Name").Value
            End If
        End If
        Return String.Empty
    End Function
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_Aoffice.aspx?Action=I|")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice

        objInputXml.LoadXml("<MS_UPDATEAOFFICE_INPUT><AOFFICE Action='' Aoffice='' Region='' Address='' RegionalHQ='' BRLimit='' BR_ID='' Fax='' Pincode='' CityID='' Phone='' /></MS_UPDATEAOFFICE_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Request.QueryString("Aoffice")

        With objInputXml.DocumentElement.SelectSingleNode("AOFFICE")

            If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
                .Attributes("Action").Value() = "U"
            Else
                .Attributes("Action").Value() = Request.QueryString("Action").ToString().Split("|").GetValue(0)
            End If
            .Attributes("Aoffice").Value() = txtAofficeCode.Text
            If drpRegion.SelectedIndex <> 0 Then
                .Attributes("Region").Value() = drpRegion.SelectedItem.Text
            End If
            .Attributes("Address").Value() = txtAofficeAddress.Text
            If drpRegionHQ.SelectedIndex <> 0 Then
                .Attributes("RegionalHQ").Value() = drpRegionHQ.SelectedValue
            End If
            If txtBDR.Text.Trim().Length <= 0 Then
                .Attributes("BRLimit").Value() = "0"
            Else
                .Attributes("BRLimit").Value() = txtBDR.Text
            End If
            .Attributes("Fax").Value() = txtfax.Text
            .Attributes("Pincode").Value() = txtPinCode.Text
            If drpCity.SelectedIndex <> 0 Then
                .Attributes("CityID").Value() = drpCity.SelectedValue
            End If
            .Attributes("Phone").Value() = txtPhone.Text
            .Attributes("BR_ID").Value() = "0"

        End With

        'Here Back end Method Call
        objOutputXml = objbzAOffice.Update(objInputXml)
        Dim CheckBoxOb As String = objOutputXml.DocumentElement.SelectSingleNode("AOFFICE").Attributes("Aoffice").Value().Trim()
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                'lblError.Text = objeAAMSMessage.messInsert
                Response.Redirect("MSUP_Aoffice.aspx?Action=US|" + objEn.Encrypt(CheckBoxOb))

            End If
            CheckSecurity()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0) = "US" Then
            AofficeView()
        Else
            txtAofficeCode.Text = String.Empty
            txtAofficeAddress.Text = String.Empty
            drpCity.SelectedIndex = 0
            txtPinCode.Text = String.Empty
            txtCountry.Text = String.Empty
            txtPhone.Text = String.Empty
            txtfax.Text = String.Empty
            drpRegionHQ.SelectedIndex = 0
            drpRegion.SelectedIndex = 0
            txtBDR.Text = String.Empty
            lblError.Text = String.Empty
        End If
    End Sub
End Class
