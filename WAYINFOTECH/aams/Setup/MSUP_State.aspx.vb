Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
Partial Class Setup_MSUP_State
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
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
        Try

            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            'btnReset.Attributes.Add("onclick", "return StateReset();")
            btnSave.Attributes.Add("onclick", "return StateMandatory();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            'Check Condition for Action Update or Insert
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    StateView()
                End If
                '***************************************************************************************
                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
                    lblError.Text = objeAAMSMessage.messInsert
                    StateView()
                End If

            End If
            '***************************************************************************************

            '***************************************************************************************
            '***************************************************************************************
            'Code of Security Check
            'objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            'objSecurityXml.LoadXml(Session("Security"))
            CheckSecurity()

            'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Count <> 0 Then
            '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='State']").Attributes("Value").Value)
            '    End If
            '    If strBuilder(1) = "0" Then
            '        btnNew.Enabled = False
            '        'btnSave.Enabled = False
            '    End If
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "US" Then
            '        If strBuilder(2) = "0" Then
            '            btnSave.Enabled = False
            '        End If
            '    End If
            'Else
            '    strBuilder = objeAAMS.SecurityCheck(31)
            'End If

        Catch ex As Exception

        End Try
    End Sub
    '*********************************************************************************************************
    '****************************Method for View State *****************************************************
    '*********************************************************************************************************
    Private Sub StateView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCountryState As New AAMS.bizMaster.bzCountryState
        objInputXml.LoadXml("<MS_VIEWCOUNTRYSTATE_INPUT><StateID></StateID></MS_VIEWCOUNTRYSTATE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("StateID").InnerXml = objEn.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1))
        'Here Back end Method Call
        objOutputXml = objbzCountryState.View(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("STATE")
                ViewState("StateID") = .Attributes("StateID").Value()
                txtStateName.Text = .Attributes("State_Name").Value()
                drpCountry.SelectedValue = .Attributes("CountryID").Value()
            End With
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCountryState As New AAMS.bizMaster.bzCountryState

        objInputXml.LoadXml("<MS_UPDATECOUNTRYSTATE_INPUT><STATE Action='' StateID='' State_Name='' CountryID='' /></MS_UPDATECOUNTRYSTATE_INPUT>")
        'objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Request.QueryString("Aoffice")

        With objInputXml.DocumentElement.SelectSingleNode("STATE")
            If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                .Attributes("Action").Value() = "U"
                .Attributes("StateID").Value() = ViewState("StateID")
            Else
                .Attributes("Action").Value() = "I"
            End If
            .Attributes("State_Name").Value() = txtStateName.Text
            If drpCountry.SelectedIndex <> 0 Then
                .Attributes("CountryID").Value() = drpCountry.SelectedValue
            End If
        End With

        'Here Back end Method Call
        objOutputXml = objbzCountryState.Update(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
                lblError.Text = objeAAMSMessage.messUpdate
            Else
                Response.Redirect("MSUP_State.aspx?Action=US|" & objEn.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("STATE").Attributes("StateID").Value.Trim))
            End If
            CheckSecurity()
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
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
                If Request.QueryString("Action").ToString() = "U" Or Request.QueryString("Action").ToString() = "US" Then
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
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "U" Or Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper = "US" Then
            StateView()
        Else
            drpCountry.SelectedIndex = 0
            txtStateName.Text = String.Empty
        End If
    End Sub
End Class
