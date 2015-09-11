'##############################################################
'############   Page Name -- nventory_INVUP_Supplier  #########  
'############   Date 19-March 2008    ######################### 
'############   Developed By Abhishek  ######################## 
'############################################################## 
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class Inventory_INVUP_Supplier
    Inherits System.Web.UI.Page

#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
            btnNew.Attributes.Add("onclick", "return NewFunction();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpCity, "CITY", True)
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    hdID.Value = objED.Decrypt(Request.QueryString("SuppId").ToString().Trim())
                    ViewSupplier()
                    txtName.Focus()
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
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE SUPPLIER']").Attributes("Value").Value)
                    'If strBuilder(0) = "0" Then
                    '    btnSave.Enabled = False
                    'End If
                    ''When Add rights disabled
                    'If strBuilder(1) = "0" Then
                    '    btnNew.Enabled = False
                    '    btnSave.Enabled = False
                    'End If
                    ''When modify rights disabled and Add rights enabled
                    'If strBuilder(2) = "0" And (Request.QueryString("Action") IsNot Nothing) Then
                    '    btnSave.Enabled = False
                    'End If
                    ''When modify rights Enabled and Add rights disabled
                    'If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    '    btnSave.Enabled = True
                    'End If
                    'When View rights disabled
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                    End If
                    'When Add rights disabled
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        btnSave.Enabled = False
                    End If
                    'When modify rights disabled and Add rights enabled
                    If strBuilder(2) = "0" And (Request.QueryString("Action") = "U") Then
                        btnSave.Enabled = False
                    End If
                    'When modify rights Enabled and Add rights disabled
                    If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                        btnSave.Enabled = True
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights
            ' btnReset.Attributes.Add("onclick", "return CityReset();")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    

#Region " Load Supplier Data For Editing Mode"
    Sub ViewSupplier()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim ds As New DataSet
                Dim objbzSupplier As New AAMS.bizInventory.bzSupplier
                objInputXml.LoadXml("<INV_VIEWSUPPLIER_INPUT><SUPPLIERID /></INV_VIEWSUPPLIER_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("SUPPLIERID").InnerText = hdID.Value ' Request.QueryString("SuppId").ToString()
                'Here Back end Method Call
                objOutputXml = objbzSupplier.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindSupplierData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindSupplierData For Binding Data in Controls"
    Sub BindSupplierData(ByVal objOutputXml As XmlDocument)
        Try
            '            <INV_VIEWSUPPLIER_OUTPUT>
            '	<SUPPLIER SUPPLIERID='' SUPPLIERNAME='' CONTACTTITLE='' CONTACTNAME='' ADDRESS='' CITYID=''
            ' POSTALCODE=''
            '	 STATE='' COUNTRY='' PHONENUMBER='' FAXNUMBER='' PAYMENTTERMS=''
            ' EMAILADDRESS='' WWW_ADDRESS='' NOTES='' />
            '	<Errors Status=''>
            '		<Error Code='' Description='' />
            '	</Errors>
            '</INV_VIEWSUPPLIER_OUTPUT>

            txtName.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERNAME").InnerText
            txtConTitle.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CONTACTTITLE").InnerText
            txtConName.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CONTACTNAME").InnerText
            txtAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("ADDRESS").InnerText
            drpCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CITYID").InnerText
            txtPostalCode.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("POSTALCODE").InnerText
            drpCountry.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("COUNTRY").InnerText
            txtState.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("STATE").InnerText
            txtPhone.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("PHONENUMBER").InnerText
            txtFax.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("FAXNUMBER").InnerText
            txtEmail.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("EMAILADDRESS").InnerText
            txtWeb.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("WWW_ADDRESS").InnerText
            txtPayTems.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("PAYMENTTERMS").InnerText
            txtNotes.Text = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("NOTES").InnerText
            'txtCtyCode.ReadOnly = True
            'txtCtyCode.CssClass = "textboxgrey"
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
                    Dim objbzSupplier As New AAMS.bizInventory.bzSupplier
                    objInputXml.LoadXml("<INV_UPDATESUPPLIER_INPUT><SUPPLIER SUPPLIERID='' SUPPLIERNAME='' CONTACTTITLE='' CONTACTNAME='' ADDRESS='' CITYID='' POSTALCODE=''	 STATE='' COUNTRY='' PHONENUMBER='' FAXNUMBER='' PAYMENTTERMS='' EMAILADDRESS='' WWW_ADDRESS='' NOTES='' /></INV_UPDATESUPPLIER_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        If (Request.QueryString("SuppId") IsNot Nothing) Then
                            objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERID").Value = hdID.Value 'Request.QueryString("SuppId")
                        End If
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERNAME").Value = txtName.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CONTACTTITLE").Value = txtConTitle.Text
                 
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CONTACTNAME").Value = txtConName.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("ADDRESS").Value = txtAddress.Text

                    If (drpCity.SelectedIndex = 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CITYID").Value = ""
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("CITYID").Value = drpCity.SelectedValue
                    End If
                    If (drpCountry.SelectedIndex = 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("COUNTRY").Value = ""
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("COUNTRY").Value = drpCountry.SelectedValue
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("POSTALCODE").Value = txtPostalCode.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("STATE").Value = txtState.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("PHONENUMBER").Value = txtPhone.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("FAXNUMBER").Value = txtFax.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("PAYMENTTERMS").Value = txtPayTems.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("EMAILADDRESS").Value = txtEmail.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("WWW_ADDRESS").Value = txtWeb.Text
                    objInputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("NOTES").Value = txtNotes.Text

                    'Here Back end Method Call
                    objOutputXml = objbzSupplier.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERID").Value
                            'Response.Redirect("INVUP_Supplier.aspx?&Msg=U&Action=U&SuppId=" & objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERID").Value, False)
                        Else
                            hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERID").Value
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            'Response.Redirect("INVUP_Supplier.aspx?&Msg=A&Action=U&SuppId=" & objOutputXml.DocumentElement.SelectSingleNode("SUPPLIER").Attributes("SUPPLIERID").Value, False)
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
#End Region
#Region "Call clearControls For Clearing the Controls"
    Private Sub clearControls()
        txtName.Text = ""
        txtConTitle.Text = ""
        txtConName.Text = ""
        txtAddress.Text = ""
        drpCity.SelectedIndex = 0
        txtPostalCode.Text = ""
        drpCountry.SelectedIndex = 0
        txtState.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWeb.Text = ""
        txtPayTems.Text = ""
        txtNotes.Text = ""

    End Sub
#End Region
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try

            If (Request.QueryString("Action") = "U") Then
                clearControls()
                ViewSupplier()
            Else
               clearControls
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
