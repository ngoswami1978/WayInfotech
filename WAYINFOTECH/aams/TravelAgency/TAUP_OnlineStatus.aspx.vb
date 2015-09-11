Imports System.Data
Imports System.Xml
Partial Class TravelAgency_TAUP_OnlineStatus
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
            Dim strurl As String = Request.Url.ToString() '"TravelAgency_TAUP_OnlineStatus"
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)
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


            btnSave.Attributes.Add("onclick", "return OnlineStatusMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPOnlineStatus();")
            If Not Page.IsPostBack Then

                bindConnectionCategory()

                '*******************************************************************
                ''Code For 

                '*******************************************************************
                ' Load Data For Editing Mode
                If (Request.QueryString("Action") = "U") Then
                    ViewOnlineStatus()
                    txtOnlineStatus.Focus()
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

    Sub ViewOnlineStatus()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
                objInputXml.LoadXml("<MS_VIEWONLINESTATUS_INPUT><StatusCode/></MS_VIEWONLINESTATUS_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = objED.Decrypt(Request.QueryString("StatusCode").ToString())
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
            txtStatusCode.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").InnerText
            txtOnlineStatus.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("OnlineStatus").InnerText
            txtSegExpected.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("SegExpected").InnerText
            txtUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("UnitCost").InnerText

            Try
                txtNPUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("NPUnitCost").InnerText
                txtLKUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("LKUnitCost").InnerText
                txtBDUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("BDUnitCost").InnerText
                txtBTUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("BTUnitCost").InnerText
                txtMLUnitCost.Text = objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("MLUnitCost").InnerText
            Catch ex As Exception

            End Try






            If objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("BC_ONLINE_CATG_ID") IsNot Nothing Then
                Dim li As ListItem = DlstConnectionCategory.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("BC_ONLINE_CATG_ID").Value())
                If li IsNot Nothing Then
                    DlstConnectionCategory.SelectedValue = li.Value
                End If
            End If


            txtStatusCode.ReadOnly = True
            txtStatusCode.CssClass = "textboxgrey"
            txtOnlineStatus.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            DlstConnectionCategory.Enabled = False

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



                    ' objInputXml.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='' OnlineStatus='' StatusCode=''  SegExpected='' UnitCost='' /></MS_UPDATEONLINESTATUS_INPUT>")

                    objInputXml.LoadXml("<MS_UPDATEONLINESTATUS_INPUT><Status Action='' BC_ONLINE_CATG_ID =''  OnlineStatus='' StatusCode=''  SegExpected='' UnitCost='' /></MS_UPDATEONLINESTATUS_INPUT>")
                    If (Request.QueryString("Action") = "U") Then
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("Action").Value = "U"
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value = txtStatusCode.Text ' Request.QueryString("StatusCode")
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("Action").Value = "I"
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value = txtStatusCode.Text
                    End If
                    objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("OnlineStatus").Value = txtOnlineStatus.Text

                    objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("SegExpected").Value = txtSegExpected.Text

                    objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("UnitCost").Value = txtUnitCost.Text.ToString()


                    If DlstConnectionCategory.SelectedValue <> "" Then
                        objInputXml.DocumentElement.SelectSingleNode("Status").Attributes("BC_ONLINE_CATG_ID").Value = DlstConnectionCategory.SelectedValue
                    End If



                    'Here Back end Method Call
                    objOutputXml = objbzOnlineStatus.Update(objInputXml)
                    If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                        If (Request.QueryString("Action") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate ' "Record updated successfully."
                            Response.Redirect("TAUP_OnlineStatus.aspx?&Msg=U&Action=U&StatusCode=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value), False)
                        Else
                            lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                            Response.Redirect("TAUP_OnlineStatus.aspx?&Msg=A&Action=U&StatusCode=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("Status").Attributes("StatusCode").Value), False)
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
                txtUnitCost.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub bindConnectionCategory()

        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim dsconnection As New DataSet

        Try

            'Connectivity Section 

            Dim objOnlineConn As New AAMS.bizIncetive.bzConnectivity
            objOutputXml = New XmlDocument
            objOutputXml = objOnlineConn.INC_CONNECTIVITY_LIST()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("conData") = objOutputXml.OuterXml
                objXmlReader = New XmlNodeReader(objOutputXml)
                dsconnection.ReadXml(objXmlReader)
                DlstConnectionCategory.DataSource = dsconnection.Tables("CONNECTIVITY")
                DlstConnectionCategory.DataTextField = "BC_ONLINE_CATG_NAME"
                DlstConnectionCategory.DataValueField = "BC_ONLINE_CATG_ID"
                DlstConnectionCategory.DataBind()
            End If
            DlstConnectionCategory.Items.Insert(0, "")

        Catch ex As Exception

        End Try


    End Sub

End Class
