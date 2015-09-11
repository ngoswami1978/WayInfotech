'###########################################################################
'############   Page Name -- TravelAgency_TAUP_AgencyType     ##############  
'############   Date 4-December 2007  ######################################
'############   Developed By Abhishek  #####################################
'###########################################################################
Imports System.Data
Imports System.Xml
Partial Class TravelAgency_TAUP_AgencyType
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString() '"TravelAgency_TAUP_AgencyType"
            Session("PageName") = strurl
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Type']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If

                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
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


            btnSave.Attributes.Add("onclick", "return AgencyTypeMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPAgencyType();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewAgencyType()
                    txtAgencyType.Focus()
                    If (Request.QueryString("Msg") = "A") Then
                        lblError.Text = objeAAMSMessage.messInsert
                    End If
                    If (Request.QueryString("Msg") = "U") Then
                        lblError.Text = objeAAMSMessage.messUpdate
                    End If
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewAgencyType()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
                objInputXml.LoadXml("<MS_VIEWAGENCYTYPE_INPUT><AgencyTypeId></AgencyTypeId></MS_VIEWAGENCYTYPE_INPUT>")

                '@ Getting  Decrepted Data 
                Dim DecreptedAgencyTypeId As String
                DecreptedAgencyTypeId = objED.Decrypt(Request.QueryString("AgencyTypeId").ToString())
                '@ End of Getting Decrepted Data


                objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = DecreptedAgencyTypeId

                'objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = Request.QueryString("AgencyTypeId").ToString()
                'Here Back end Method Call
                objOutputXml = objbzAgencyType.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindAgencyTypeData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindAgencyTypeData(ByVal objOutputXml As XmlDocument)
        Try
            txtAgencyType.Text = objOutputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("Agency_Type_Name").InnerXml
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If (IsValid) Then
            Try
                If (Not Request.QueryString("Action") = Nothing) Then

                    Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
                    'Dim objXmlReader As XmlNodeReader
                    Dim ds As New DataSet
                    Dim objbzAgencyType As New AAMS.bizTravelAgency.bzAgencyType
                    objInputXml.LoadXml("<MS_UPDATEAGENCYTYPE_INPUT><AgencyType ACTION='' Agency_Type_Name='' AgencyTypeId='' AT_MAX_BDR_FAV='' AT_MAX_CC_CALLS=''></AgencyType></MS_UPDATEAGENCYTYPE_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("ACTION").Value = "U"

                        '@ Getting  Decrepted Data 
                        Dim DecreptedAgencyTypeId As String
                        DecreptedAgencyTypeId = objED.Decrypt(Request.QueryString("AgencyTypeId").ToString())
                        '@ End of Getting Decrepted Data

                        objInputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").Value = DecreptedAgencyTypeId 'Request.QueryString("AgencyTypeId")

                        'objInputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").Value = Request.QueryString("AgencyTypeId")


                    Else
                        objInputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("ACTION").Value = "I"

                    End If
                    objInputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("Agency_Type_Name").Value = txtAgencyType.Text

                    'Here Back end Method Call
                    objOutputXml = objbzAgencyType.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '@ Getting  Encrepted Data 
                        Dim EncreptedAgencyTypeId As String
                        EncreptedAgencyTypeId = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").Value)
                        '@ End of Getting Encrepted Data

                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            'Response.Redirect("TAUP_AgencyType.aspx?&Msg=U&Action=U&AgencyTypeId=" & objOutputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").Value, False)
                            Response.Redirect("TAUP_AgencyType.aspx?&Msg=U&Action=U&AgencyTypeId=" & EncreptedAgencyTypeId, False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            'Response.Redirect("TAUP_AgencyType.aspx?&Msg=A&Action=U&AgencyTypeId=" & objOutputXml.DocumentElement.SelectSingleNode("AgencyType").Attributes("AgencyTypeId").Value, False)
                            Response.Redirect("TAUP_AgencyType.aspx?&Msg=A&Action=U&AgencyTypeId=" & EncreptedAgencyTypeId, False)
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

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewAgencyType()
                txtAgencyType.Focus()
            Else
                txtAgencyType.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class

