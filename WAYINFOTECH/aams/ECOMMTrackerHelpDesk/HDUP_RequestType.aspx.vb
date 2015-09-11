'##########################################################
'######   Page Name -- HelpDesk_HDUP_RequestType  #########  
'######   Date 7-November 2007  ###########################    
'############   Developed By Abhishek  ####################
'##########################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class ETHelpDesk_HDUP_RequestType
    Inherits System.Web.UI.Page
#Region "Page Level Variable/Objects Declaration"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
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

            btnSave.Attributes.Add("onclick", "return RequestTypeMandatory();")
            btnNew.Attributes.Add("onclick", "return NewHDSRRequestType();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                ' objeAAMS.BindDropDown(drpCatName, "CatName", True)

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewRequestType()
                    drpCatName.Focus()
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='City']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
                        btnSave.Enabled = False
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
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_City.aspx?Action=I")
    'End Sub

#Region " Load City Data For Editing Mode"
    Sub ViewRequestType()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzCity As New AAMS.bizMaster.bzCity
                objInputXml.LoadXml("<HD_VIEWREQUESTTYPE_INPUT><HD_RETYPE_ID /></HD_VIEWREQUESTTYPE_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("HD_RETYPE_ID").InnerText = Request.QueryString("HD_RETYPE_ID").ToString()
                'Here Back end Method Call
                '  objOutputXml = objbzCity.View(objInputXml)
                objOutputXml.LoadXml("<HD_VIEWREQUESTTYPE_OUTPUT><RequestType HD_RETYPE_ID ='1' HD_RETYPE_NAME ='dd' HD_RETYPE_PROTECT ='' HD_RE_TYCAT_ID='1' HD_RETYPE_PRINT='12' /><Errors Status='FALSE'>		<Error Code='' Description='' /></Errors></HD_VIEWREQUESTTYPE_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindRequestTypeData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "Call BindRequestTypeData For Binding Data in Controls"
    Sub BindRequestTypeData(ByVal objOutputXml As XmlDocument)
        Try
            txtType.Text = objOutputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_NAME").InnerXml
            txtTypeToPrint.Text = objOutputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_PRINT").InnerXml
            drpCatName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RE_TYCAT_ID").InnerXml
            drpCatName.Enabled = False
            drpCatName.CssClass = "textboxgrey"
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
                    objInputXml.LoadXml("<HD_UPDATEREQUESTTYPE_INPUT><RequestType HD_RETYPE_ID ='' HD_RETYPE_NAME ='' HD_RETYPE_PROTECT ='' HD_RE_TYCAT_ID='' HD_RETYPE_PRINT='' />	</HD_UPDATEREQUESTTYPE_INPUT>")
                    If (Request.QueryString("HD_RETYPE_ID") IsNot Nothing) Then
                        objInputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_ID").Value = Request.QueryString("HD_RETYPE_ID")
                    End If

                    objInputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_NAME").Value = txtType.Text
                    objInputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_PRINT").Value = txtTypeToPrint.Text
                    If (drpCatName.SelectedIndex <> 0) Then
                        objInputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RE_TYCAT_ID").Value = drpCatName.SelectedValue
                    End If
                    'Here Back end Method Call
                    ' objOutputXml = objbzCity.Update(objInputXml)
                    objOutputXml.LoadXml("<HD_UPDATEREQUESTTYPE_OUTPUT><RequestType HD_RETYPE_ID ='1' HD_RETYPE_NAME ='' HD_RETYPE_PROTECT ='' HD_RE_TYCAT_ID='' HD_RETYPE_PRINT='' /><Errors Status='FALSE'><Error Code='' Description='' /></Errors></HD_UPDATEREQUESTTYPE_OUTPUT>")
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("HDUP_RequestType.aspx?&Msg=U&Action=U&HD_RETYPE_ID=" & objOutputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_ID").Value, False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("HDUP_RequestType.aspx?&Msg=A&Action=U&HD_RETYPE_ID=" & objOutputXml.DocumentElement.SelectSingleNode("RequestType").Attributes("HD_RETYPE_ID").Value, False)
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
                ViewRequestType()
                drpCatName.Enabled = False
                drpCatName.CssClass = "textboxgrey"
                txtType.Focus()
            Else
                txtType.Text = ""
                txtTypeToPrint.Text = ""
                drpCatName.SelectedIndex = 0
                drpCatName.Enabled = True
                drpCatName.CssClass = "dropdownlist"
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
