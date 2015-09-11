'##################################################################
'############   Page Name -- TravelAgency_MSSR_OnlineStatus #######
'############   Date 3-December 2007   ############################
'############   Developed By Abhishek  ############################
'##################################################################
Imports System.Xml
Imports System.Data
Partial Class TravelAgency_MSSR_OnlineStatus
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
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
            Session("PageName") = Request.Url.ToString() '"TravelAgency_MSSR_OnlineStatus"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
       
            btnReset.Attributes.Add("onclick", "return OnlineStatusReset();")
            btnSearch.Attributes.Add("onclick", "return OnlineStatusMandatory();")
            btnNew.Attributes.Add("onclick", "return NewMSUPOnlineStatus();")
            If Not Page.IsPostBack Then
              
                '*******************************************************************
                ''Code For                
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        txtStatusCode.Text = Session("Act").ToString().Split("|").GetValue(2)
                        txtOnlineStatus.Text = Session("Act").ToString().Split("|").GetValue(3)
                        SearchOnlineStatus()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If

                '*******************************************************************
                ' Load Data For Editing Mode
            End If
            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    OnlineStatusDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If

           
            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                    End If
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

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchOnlineStatus()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub SearchOnlineStatus()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
            objInputXml.LoadXml("<MS_SEARCHONLINESTATUS_INPUT><OnlineStatus></OnlineStatus><StatusCode></StatusCode></MS_SEARCHONLINESTATUS_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = txtStatusCode.Text
            objInputXml.DocumentElement.SelectSingleNode("OnlineStatus").InnerText = txtOnlineStatus.Text
            'Here Back end Method Call
            objOutputXml = objbzOnlineStatus.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvOnlineStatus.DataSource = ds.Tables("Status")
                gvOnlineStatus.DataBind()
            Else
                gvOnlineStatus.DataSource = Nothing
                gvOnlineStatus.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub OnlineStatusDelete(ByVal strStatusCode As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzOnlineStatus As New AAMS.bizTravelAgency.bzOnlineStatus
            objInputXml.LoadXml("<MS_DELETEONLINESTATUS_INPUT><StatusCode /></MS_DELETEONLINESTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = strStatusCode
            'Here Back end Method Call
            objOutputXml = objbzOnlineStatus.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                'lblError.Text = objeAAMSMessage.messDelete '"Record deleted successfully."
                Session("Act") = Request.QueryString("Action")
                Response.Redirect("MSSR_OnlineStatus.aspx")
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOnlineStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOnlineStatus.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdStatusCode As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdStatusCode = e.Row.FindControl("hdStatusCode")
            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Online Status']").Attributes("Value").Value)
                If strBuilder(3) = "0" Then
                    linkDelete.Disabled = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
                End If
                If strBuilder(2) = "0" Then
                    linkEdit.Disabled = True
                Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdStatusCode.Value & "'" & ");")
                End If
            Else
                linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdStatusCode.Value & "'" & ");")
                linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdStatusCode.Value & "'" & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvOnlineStatus_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvOnlineStatus.RowCommand
        'Try
        '    'Code for Edit Data
        '    'If e.CommandName = "Editx" Then
        '    '    Response.Redirect("MSUP_ManageAgencyGroup.aspx?Action=U&Chain_Code=" & e.CommandArgument)
        '    'End If
        '    'Code for Delete Date
        '    If e.CommandName = "Deletex" Then
        '        OnlineStatusDelete(e.CommandArgument)
        '        SearchAgencyGroup()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = ex.Message
        'End Try
    End Sub
End Class
