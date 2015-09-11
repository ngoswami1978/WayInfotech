Imports System.Data
Imports System.Xml
Partial Class TravelAgency_TAUP_AgencyPriority
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
            Dim strurl As String = Request.Url.ToString() '"TravelAgency_TAUP_AgencyPriority"
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IATA Status']").Attributes("Value").Value)

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


            btnSave.Attributes.Add("onclick", "return AgencyPriorityMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPAgencyPriority();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewAgencyPriority()
                    txtPriority.Focus()
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

    Sub ViewAgencyPriority()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                ' #######################################
                '' ########## This Code code is used for enable/disable 
                '' ########## the button according to rights
                'Dim objSecurityXml As New XmlDocument
                'objSecurityXml.LoadXml(Session("Security"))
                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                '    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Priority']").Count <> 0 Then
                '        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Priority']").Attributes("Value").Value)
                '        If (strBuilder(2) = "0") Then
                '            btnSave.Enabled = False
                '        End If
                '    End If
                'End If

                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzAgencyPriority As New AAMS.bizTravelAgency.bzAgencyPriority
                objInputXml.LoadXml("<MS_VIEWAGENCYPRIORITY_INPUT><PRIORITYID></PRIORITYID></MS_VIEWAGENCYPRIORITY_INPUT>")

                '@ Getting  Decrrepted Data 
                Dim DecreptedPRIORITYID As String
                DecreptedPRIORITYID = objED.Decrypt(Request.QueryString("PriorityID").ToString())
                '@ End of Getting Decrepted Data

                objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = DecreptedPRIORITYID
                'objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = Request.QueryString("PriorityID").ToString()
                'Here Back end Method Call
                objOutputXml = objbzAgencyPriority.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindAgencyPriorityData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindAgencyPriorityData(ByVal objOutputXml As XmlDocument)
        Try
            txtPriority.Text = objOutputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYNAME").InnerXml
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
                    Dim objbzAgencyPriority As New AAMS.bizTravelAgency.bzAgencyPriority
                    objInputXml.LoadXml("<MS_UPDATEAGENCYPRIORITY_INPUT><PRIORITY ACTION=''  PRIORITYID='' PRIORITYNAME='' PROTECTED=''/></MS_UPDATEAGENCYPRIORITY_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("ACTION").Value = "U"

                        '@ Getting  Decrepted Data 
                        Dim DecreptedPriorityID As String
                        DecreptedPriorityID = objED.Decrypt(Request.QueryString("PriorityID").ToString)
                        '@ End of Getting Decrepted Data

                        objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value = DecreptedPriorityID

                        'objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value = Request.QueryString("PriorityID")
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("ACTION").Value = "I"

                    End If
                    objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYNAME").Value = txtPriority.Text

                    'Here Back end Method Call
                    objOutputXml = objbzAgencyPriority.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '@ Getting  Encrepted Data 
                        Dim EncreptedPRIORITYID As String
                        EncreptedPRIORITYID = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value)
                        '@ End of Getting Encrepted Data


                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("TAUP_AgencyPriority.aspx?&Msg=U&Action=U&PRIORITYID=" & EncreptedPRIORITYID, False)
                            'Response.Redirect("TAUP_AgencyPriority.aspx?&Msg=U&Action=U&PRIORITYID=" & objOutputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value, False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("TAUP_AgencyPriority.aspx?&Msg=A&Action=U&PRIORITYID=" & EncreptedPRIORITYID, False)
                            'Response.Redirect("TAUP_AgencyPriority.aspx?&Msg=A&Action=U&PRIORITYID=" & objOutputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value, False)
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
                ViewAgencyPriority()
                txtPriority.Focus()
            Else
                txtPriority.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class


