'##########################################################
'######   Page Name -- HelpDesk_HDUP_PTRType      #########  
'######   Date 7-November 2007  ###########################    
'############   Developed By Abhishek  ####################
'##########################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class HelpDesk_HDUP_PTRType
    Inherits System.Web.UI.Page
#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
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

            btnSave.Attributes.Add("onclick", "return PtrTypeMandatory();")
            btnNew.Attributes.Add("onclick", "return NewHDUPPtrType();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpCatName, "HDPTRTYPECAT", True)

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewPtrType()
                    txtType.Focus()
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='PTR Type']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
                        btnNew.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                        'btnSave.Enabled = False
                    End If
                    If (Request.QueryString("Action") = "U") Then
                        If strBuilder(2) = "0" Then
                            btnSave.Enabled = False
                        End If
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


#Region " Load Ptr Type Data For Editing Mode"
    Sub ViewPtrType()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzPTRType As New AAMS.bizHelpDesk.bzPTRType
                objInputXml.LoadXml("<HD_VIEWPTR_TYPE_INPUT><HD_PTR_TYPE_ID></HD_PTR_TYPE_ID></HD_VIEWPTR_TYPE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("HD_PTR_TYPE_ID").InnerText = objED.Decrypt(Request.QueryString("HD_PTR_TYPE_ID").ToString().Trim)
                'Here Back end Method Call
                objOutputXml = objbzPTRType.View(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindPTrTypeData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindPTrTypeData For Binding Data in Controls"
    Sub BindPTrTypeData(ByVal objOutputXml As XmlDocument)
        Try
            txtType.Text = objOutputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYPE_NAME").InnerXml
            drpCatName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYCAT_ID").InnerXml
            ' drpCatName.Enabled = False
            ' drpCatName.CssClass = "textboxgrey"
            txtType.Focus()
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
                    Dim objbzPTRType As New AAMS.bizHelpDesk.bzPTRType
                    objInputXml.LoadXml("<HD_UPDATEPTR_TYPE_INPUT><PTR_TYPE HD_PTR_TYPE_ID='' HD_PTR_TYPE_NAME='' HD_PTR_TYCAT_ID='' /></HD_UPDATEPTR_TYPE_INPUT>")
                    If (Request.QueryString("HD_PTR_TYPE_ID") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYPE_ID").Value = objED.Decrypt(Request.QueryString("HD_PTR_TYPE_ID").Trim)
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYPE_NAME").Value = txtType.Text

                    If (drpCatName.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYCAT_ID").Value = drpCatName.SelectedValue
                    End If
                    'Here Back end Method Call
                    objOutputXml = objbzPTRType.Update(objInputXml)

                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("HDUP_PTRType.aspx?&Msg=U&Action=U&HD_PTR_TYPE_ID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYPE_ID").Value.Trim), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("HDUP_PTRType.aspx?&Msg=A&Action=U&HD_PTR_TYPE_ID=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PTR_TYPE").Attributes("HD_PTR_TYPE_ID").Value.Trim), False)
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
#Region "btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewPtrType()
                ' drpCatName.Enabled = False
                ' drpCatName.CssClass = "textboxgrey"
                txtType.Focus()
            Else
                txtType.Text = ""
                drpCatName.SelectedIndex = 0
                ' drpCatName.Enabled = True
                ' drpCatName.CssClass = "dropdownlist"
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
