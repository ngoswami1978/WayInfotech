'###########################################################################
'############   Page Name --TravelAgency_MSUP_OnlineStatus         #########  
'############   Date 3-December 2007  ######################################
'############   Developed By Abhishek  #####################################
'###########################################################################
Imports System.Data
Imports System.Xml
Partial Class TravelAgency_MSUP_OnlineStatus
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder

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
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights


            btnSave.Attributes.Add("onclick", "return OnlineStatusMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPOnlineStatus();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewOnlineStatus()
                    txtOnlineStatus.Focus()
                End If

            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub ViewOnlineStatus()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
                objInputXml.LoadXml("<MS_VIEWONLINESTATUS_INPUT><StatusCode/></MS_VIEWONLINESTATUS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = Request.QueryString("StatusCode").ToString()
                'Here Back end Method Call
                objOutputXml = objbzOnlineStatus.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindOnlineStatusData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindOnlineStatusData(ByVal objOutputXml As XmlDocument)
        Try
            txtStatusCode.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").InnerXml
            txtOnlineStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("OnlineStatus").InnerXml
            txtStatusCode.ReadOnly = True
            txtStatusCode.CssClass = "textboxgrey"
            txtOnlineStatus.Focus()
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
                    Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
                    objInputXml.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='' OnlineStatus='' StatusCode=''/></MS_UPDATEONLINESTATUS_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("Action").Value = "U"
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value = txtStatusCode.Text ' Request.QueryString("StatusCode")
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("Action").Value = "I"
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value = txtStatusCode.Text
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("OnlineStatus").Value = txtOnlineStatus.Text

                    'Here Back end Method Call
                    objOutputXml = objbzOnlineStatus.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
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
                ViewOnlineStatus()
                txtStatusCode.ReadOnly = True
                txtStatusCode.CssClass = "textboxgrey"
                txtOnlineStatus.Focus()
            Else
                txtStatusCode.Text = ""
                txtOnlineStatus.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
