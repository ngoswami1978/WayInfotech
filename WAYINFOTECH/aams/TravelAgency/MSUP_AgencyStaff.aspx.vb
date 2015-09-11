'#####################################################################
'############   Page Name -- TravelAgency_MSUP_AgencyStaff     #######
'############   Date 5-December 2007  ################################
'############   Developed By Abhishek  ###############################
'#####################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data
#End Region
Partial Class TravelAgency_MSUP_AgencyStaff
    Inherits System.Web.UI.Page
#Region "Page Level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
#End Region

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

#Region "Page_Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString() '"TravelAgency_MSUP_AgencyStaff"
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            btnAdd.Attributes.Add("onclick", "return AgencyStaffMandatory();")
            If Not Page.IsPostBack Then

                ' Code For Loading All Agency Staff Details according to Current Agency (LCODE)
                ViewAgencyStaff()
                '*******************************************************************
                ''Code For  Deletion of Agency Staff              
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "D" Then
                        'txtPriority.Text = Session("Act").ToString().Split("|").GetValue(2)
                        'SearchAgencyPriority()
                        lblError.Text = objeAAMSMessage.messDelete
                        Session("Act") = Nothing
                    End If
                End If

                ''Code For  Updating of Agency Staff              
                If Not Session("Act") Is Nothing Then
                    If Session("Act").ToString().Split("|").GetValue(0) = "U" Then
                        'txtPriority.Text = Session("Act").ToString().Split("|").GetValue(2)
                        'ViewAgencyStaff(Session("Act").ToString().Split("|").GetValue(2))
                        Session("Act") = Nothing
                    End If
                End If

                '*******************************************************************
                ' Load Data For Editing Mode
            End If

            If Not Request.QueryString("Action") Is Nothing Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
                    'AgencyPriorityDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
                End If
            End If


            ' #######################################
            ' ########## This code is used for enable/disable 
            ' ########## the button according to rights
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyStaff']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyStaff']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        'btnSearch.Enabled = False
                    End If
                    If strBuilder(1) = "0" Then
                        'btnNew.Enabled = False
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
#End Region
#Region " ViewAgencyStaff Procedure"
    Private Sub ViewAgencyStaff()
        Dim objInputXml, objOutputXml As New XmlDocument
        ' Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            'Dim objbzCity As New AAMS.bizMaster.bzCity

            'objInputXml.LoadXml("<MS_SEARCHAGENCYSTAFFINPUT><LCODE></LCODE></MS_SEARCHAGENCYSTAFFINPUT>")
            'objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = "12"
            ''Here Back end Method Call
            'objOutputXml = objbzCity.Search(objInputXml)

            'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            '    objXmlReader = New XmlNodeReader(objOutputXml)
            '    ds.ReadXml(objXmlReader)
            '    gvAgencyStaff.DataSource = ds.Tables("AGENCYSTAFF")
            '    gvAgencyStaff.DataBind()
            'Else
            '    gvAgencyStaff.DataSource = Nothing
            '    gvAgencyStaff.DataBind()
            '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            'End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnReset_Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewAgencyStaff()
                txtName.Focus()
            Else
                txtName.Text = String.Empty
                txtDesig.Text = String.Empty
                txtDob.Text = String.Empty
                txtDow.Text = String.Empty
                txtEmail.Text = String.Empty
                txtFax.Text = String.Empty
                txtPhone.Text = String.Empty
                txtNotes.Text = String.Empty
                chkCor.Checked = False
                chkRes.Checked = False
                If (drplstConPer.Items.Count > 0) Then
                    drplstConPer.SelectedIndex = 0
                End If
                drplstConPer.Items.Clear()
                gvAgencyStaff.DataBind()
                txtName.Focus()
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region " gvAgencyStaff_RowDataBound Event"
    Protected Sub gvAgencyStaff_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAgencyStaff.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim hdstrAGENCYSTAFFID As HiddenField
            Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
            linkEdit = e.Row.FindControl("linkEdit")
            Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
            linkDelete = e.Row.FindControl("linkDelete")
            hdstrAGENCYSTAFFID = e.Row.FindControl("hdAGENCYSTAFFID")
            'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrPriorityId.Value & "'" & ");")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyStaff']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyStaff']").Attributes("Value").Value)
                If strBuilder(3) = "0" Then
                    linkDelete.Disabled = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                End If
                If strBuilder(2) = "0" Then
                    linkEdit.Disabled = True
                Else
                    linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                End If
            Else
                'linkDelete.Disabled = True
                'linkEdit.Disabled = True
                'linkEdit.Attributes.Add("onclick", "return EditFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
                'linkDelete.Attributes.Add("onclick", "return DeleteFunction(" & "'" & hdstrAGENCYSTAFFID.Value & "'" & ");")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnAdd_Click Event"
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim objInputXml As New XmlDataDocument
            Dim objOutputXml As New XmlDocument
            Dim ds As New DataSet
            Dim objChildNode, objChildNodeClone As XmlNode
            Dim objXmlReader As XmlNodeReader
            'Dim objbzIPPool As New AAMS.bizMaster.bzIPPool
            If (IsValid) Then
                If (ViewState("MS_UPDATEAGENCYSTAFF_INPUT") IsNot Nothing) Then
                    objInputXml.LoadXml(ViewState("MS_UPDATEAGENCYSTAFF_INPUT").ToString())
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    objChildNodeClone = objChildNode.CloneNode(True)
                Else
                    objInputXml.LoadXml("<MS_UPDATEAGENCYSTAFF_INPUT><AGENCYSTAFF LCODE='' AGENCYSTAFFID='' STAFFNAME='' DESIGNATION='' DOW='' DOB='' CORRESPONDENCE='' RESPONSIBLE='' FAX='' PHONE='' EMAIL='' NOTES=''></AGENCYSTAFF></MS_UPDATEAGENCYSTAFF_INPUT>")
                    objChildNode = objInputXml.DocumentElement.SelectSingleNode("AGENCYSTAFF")
                    objChildNodeClone = objChildNode.CloneNode(True)
                    objInputXml.DocumentElement.RemoveChild(objChildNode)
                End If

                With objChildNodeClone
                    If Request.QueryString("Action") = "U" Then
                        .Attributes("AGENCYSTAFFID").Value() = Request.QueryString("AGENCYSTAFFID")
                    Else
                        ' .Attributes("AGENCYSTAFFID").Value() = Request.QueryString("AGENCYSTAFFID")
                    End If
                    .Attributes("LCODE").Value() = "12"
                    .Attributes("STAFFNAME").Value() = txtName.Text
                    .Attributes("DESIGNATION").Value() = txtDesig.Text
                    .Attributes("DOB").Value() = txtDob.Text
                    .Attributes("DOW").Value() = txtDow.Text
                    .Attributes("EMAIL").Value() = txtEmail.Text
                    .Attributes("FAX").Value() = txtFax.Text
                    .Attributes("PHONE").Value() = txtPhone.Text
                    .Attributes("CORRESPONDENCE").Value() = chkCor.Checked
                    .Attributes("RESPONSIBLE").Value() = chkRes.Checked
                    .Attributes("NOTES").Value() = txtNotes.Text

                End With
                objInputXml.DocumentElement.AppendChild(objChildNodeClone)

                ViewState("MS_UPDATEAGENCYSTAFF_INPUT") = objInputXml.InnerXml
                objXmlReader = New XmlNodeReader(objInputXml)
                ds.ReadXml(objXmlReader)

                gvAgencyStaff.DataSource = ds.Tables("AGENCYSTAFF")
                gvAgencyStaff.DataBind()

                drplstConPer.Items.Insert(0, New ListItem(txtName.Text, txtName.Text))
                drplstConPer.SelectedIndex = 0

                'Here Back end Method Call

                'objOutputXml = objbzIPPool.Update(objInputXml)
                'If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '    If Request.QueryString("Action").ToUpper = "U" Then
                '        lblError.Text = objeAAMSMessage.messUpdate '"Record Updated successfully."
                '        txtIpAddress.Text = ""
                '    Else
                '        lblError.Text = objeAAMSMessage.messInsert '"Record Added successfully."
                '        'strPoolId = objOutputXml.DocumentElement.SelectSingleNode("IPPOOL").Attributes("PoolID").Value
                '        txtIpAddress.Text = ""
                '        'Response.Redirect("MSUP_IPPool.aspx?Action=U&Msg=S&PoolID=" & strPoolId)
                '        'ReBindIPPoolDetailsGrid(strPoolId)
                '    End If
                'Else
                '    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                'End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
