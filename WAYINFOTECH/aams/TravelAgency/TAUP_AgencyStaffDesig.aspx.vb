
Partial Class TravelAgency_TAUP_AgencyStaffDesig
    Inherits System.Web.UI.Page

    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt

#Region "Code for Filter "

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub

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
                Exit Sub
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff designation']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Staff designation']").Attributes("Value").Value)

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


            btnSave.Attributes.Add("onclick", "return AgencyStaffDesignationMandatory();")
            ' btnNew.Attributes.Add("onclick", "return NewMSUPAgencyStaffDesgId();")
            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewAgencyStaffDesig()
                    TxtDesignation.Focus()
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

    Sub ViewAgencyStaffDesig()
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
                Dim objbzStaffDesignation As New AAMS.bizTravelAgency.bzStaffDesignation
                objInputXml.LoadXml("<MS_VIEWDESIGNATION_INPUT><DesignationID></DesignationID></MS_VIEWDESIGNATION_INPUT>")

                '@ Getting  Decrrepted Data 
                Dim DecreptedDesigId As String
                DecreptedDesigId = objED.Decrypt(Request.QueryString("DesigId").ToString())
                '@ End of Getting Decrepted Data

                objInputXml.DocumentElement.SelectSingleNode("DesignationID").InnerText = DecreptedDesigId
                'objInputXml.DocumentElement.SelectSingleNode("PRIORITYID").InnerText = Request.QueryString("PriorityID").ToString()
                'Here Back end Method Call
                objOutputXml = objbzStaffDesignation.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindAgencyStaffDesig(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub BindAgencyStaffDesig(ByVal objOutputXml As XmlDocument)
        Try
            TxtDesignation.Text = objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").InnerText
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
                    Dim objbzStaffDesignation As New AAMS.bizTravelAgency.bzStaffDesignation
                    objInputXml.LoadXml("<MS_UPDATEDESIGNATION_INPUT><DESIGNATION DesignationID='' Designation='' ACTION='' ></DESIGNATION></MS_UPDATEDESIGNATION_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("ACTION").Value = "U"

                        '@ Getting  Decrepted Data 
                        Dim DecreptedDesigId As String
                        DecreptedDesigId = objED.Decrypt(Request.QueryString("DesigId").ToString)
                        '@ End of Getting Decrepted Data

                        objInputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").Value = DecreptedDesigId

                        'objInputXml.DocumentElement.SelectSingleNode("PRIORITY").Attributes("PRIORITYID").Value = Request.QueryString("PriorityID")
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("ACTION").Value = "I"

                    End If
                    objInputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("Designation").Value = TxtDesignation.Text

                    'Here Back end Method Call
                    objOutputXml = objbzStaffDesignation.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                        '@ Getting  Encrepted Data 
                        Dim EncreptedDESIGID As String
                        EncreptedDESIGID = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("DESIGNATION").Attributes("DesignationID").Value)
                        '@ End of Getting Encrepted Data


                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("TAUP_AgencyStaffDesig.aspx?&Msg=U&Action=U&DESIGID=" & EncreptedDESIGID, False)

                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("TAUP_AgencyStaffDesig.aspx?&Msg=A&Action=U&DESIGID=" & EncreptedDESIGID, False)

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
                ViewAgencyStaffDesig()
                TxtDesignation.Focus()
            Else
                TxtDesignation.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("TAUP_AgencyStaffDesig.aspx?Action=I", False)
        Catch ex As Exception

        End Try
    End Sub
End Class



