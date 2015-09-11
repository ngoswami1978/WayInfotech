'###########################################################################
'############   Page Name -- PUUP_AgencyGroup     #########  
'############   Date 15-Feb 2007  #####################################
'############   Developed By Abhishek  #####################################
'###########################################################################
#Region "Required Namespace declaration"
Imports System.Data
Imports System.Xml
#End Region
Partial Class Popup_PUUP_AgencyGroup
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
#Region "Page level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
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


#Region "Page_Load"
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        Response.Redirect("../NoRights.aspx")
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

            btnSave.Attributes.Add("onclick", "return CheckMandatoty();")
            btnNew.Attributes.Add("onclick", "return NewMSUPManageAgencyGroup();")

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True)
                'objeAAMS.BindDropDown(drpLstCity, "CITY", True)

                drpLstCity.Items.Insert(0, "---Select One---")

                objeAAMS.BindDropDown(drpLstPriority, "PRIORITY", True)
                objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True)
                'objeAAMS.BindDropDown(drpLstCountry, "COUNTRY", True)
                '*******************************************************************
                ' Load Data For Editing Mode
                If (Not Request.QueryString("Action") = Nothing) Then
                    If (Request.QueryString("Action").ToUpper = "I") Then
                        txtChainCode.Text = ""
                        If (drpLstPriority.Items.Count > 3) Then
                            drpLstPriority.SelectedValue = 2
                        End If
                    End If
                    If (Request.QueryString("Action").ToUpper = "U") Then
                        ViewAgencyGroup()
                        If (Request.QueryString("Msg") = "A") Then
                            lblError.Text = objeAAMSMessage.messInsert
                        End If
                        If (Request.QueryString("Msg") = "U") Then
                            lblError.Text = objeAAMSMessage.messUpdate
                        End If
                    End If
                End If
            End If
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpLstAoffice.Attributes.Add("OnChange", "return SendAoffice('drpLstAoffice');")
            'drpLstAoffice.Attributes.Add("OnChange", "return CallServer('drpLstAoffice');")

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " GetCallbackResult Procedure is fired internally by ICallbackEventHandler Interface  "
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function
#End Region
#Region " RaiseCallbackEvent Procedure is fired internally by ICallbackEventHandler Interface  "
    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim ds As New DataSet
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
        Dim strArr() As String
        strArr = eventArgument.Split("|")
        objInputXml.LoadXml("<MS_SEARCHAOFFICECITY_INPUT><Aoffice></Aoffice></MS_SEARCHAOFFICECITY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = strArr(1)
        objOutputXml = objbzAOffice.SearchAofficeCity(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            str = objOutputXml.OuterXml
        Else
            str = ""
        End If
    End Sub
#End Region
#Region " ViewAgencyGroup Procedure is called for view the data of agency Group by passing the value of Chain_Code Value. "
    Sub ViewAgencyGroup()
        Try
            If (Not Request.QueryString("Action") = Nothing) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                'Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
                objInputXml.LoadXml("<MS_VIEWGROUP_INPUT><Chain_Code></Chain_Code></MS_VIEWGROUP_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = Request.QueryString("Chain_Code").ToString()
                'Here Back end Method Call
                objOutputXml = objbzAgencyGroup.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindAgencyGroupData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region "btnSave_Click Event is fired for Adding/ Updating the data"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup

                objInputXml.LoadXml("<MS_UPDATEGROUP_INPUT><GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='1' Grouped='0' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='1' CQualifier='' CCode='' MainGroup='' Address ='' /></MS_UPDATEGROUP_INPUT>")
                ' objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerXml = Request.QueryString("Chain_Code")

                With objInputXml.DocumentElement.SelectSingleNode("GROUP")
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("Chain_Code").Value() = Request.QueryString("Chain_Code")
                        .Attributes("ACTION").Value() = "U"
                    Else
                        .Attributes("ACTION").Value() = "I"

                    End If
                    .Attributes("Chain_Name").Value() = txtGroupName.Text

                    If drpLstPriority.SelectedIndex <> 0 Then
                        .Attributes("PriorityID").Value() = drpLstPriority.SelectedValue
                    Else
                        .Attributes("PriorityID").Value() = ""
                    End If
                    If Request("txtCorporateCode") IsNot Nothing Then
                        .Attributes("CCode").Value() = Request("txtCorporateCode")
                    Else
                        .Attributes("CCode").Value() = txtCorporateCode.Text
                    End If

                    If drpLstAoffice.SelectedIndex <> 0 Then
                        .Attributes("Aoffice").Value() = drpLstAoffice.SelectedValue
                    Else
                        .Attributes("Aoffice").Value() = ""
                    End If
                    .Attributes("CQualifier").Value() = txtCorporateQualifier.Text
                    If Request.Form("drpLstCity") IsNot Nothing Then
                        .Attributes("CityID").Value() = Request.Form("drpLstCity")
                    ElseIf drpLstCity.SelectedValue <> "" Then
                        .Attributes("CityID").Value() = drpLstCity.SelectedValue
                    End If
                    If drpLstGroupType.SelectedIndex <> 0 Then
                        .Attributes("GroupTypeID").Value() = drpLstGroupType.SelectedValue
                    Else
                        .Attributes("GroupTypeID").Value() = ""
                    End If
                    If ChkMainGroup.Checked = True Then
                        .Attributes("MainGroup").Value() = True
                    Else
                        .Attributes("MainGroup").Value() = False
                    End If
                    .Attributes("Address").Value() = txtMainGroupAddress.Text
                End With

                'Here Back end Method Call
                objOutputXml = objbzAgencyGroup.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        Response.Redirect("PUUP_AgencyGroup.aspx?&Msg=U&Action=U&Chain_Code=" & objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value, False)
                        lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        ' RebindCity()
                        'drpLstCity.SelectedValue = Request.Form("drpLstCity")
                        Response.Redirect("PUUP_AgencyGroup.aspx?&Msg=A&Action=U&Chain_Code=" & objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value, False)
                    End If
                Else
                    lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            End Try
        End If
    End Sub
#End Region
#Region "BindAgencyGroupData Procedure is called for binding the Agency Group Data to diferent controls"
    Sub BindAgencyGroupData(ByVal objOutputXml As XmlDocument)
        Try
            txtChainCode.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").InnerXml
            'Dim STR As String = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Name").InnerXml
            txtGroupName.Text = Server.HtmlDecode(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Name").InnerXml)
            drpLstAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerXml
            RebindCity()
            drpLstCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CityID").InnerXml
            drpLstPriority.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("PriorityID").InnerXml
            txtCorporateCode.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CCode").InnerXml
            txtCorporateQualifier.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CQualifier").InnerXml
            drpLstGroupType.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("GroupTypeID").InnerXml
            If (objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("MainGroup").InnerXml = "True") Then
                ChkMainGroup.Checked = True
            Else
                ChkMainGroup.Checked = False
            End If
            txtMainGroupAddress.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Address").InnerXml
            drpLstAoffice.Enabled = False
            drpLstAoffice.CssClass = "textboxgrey"
            drpLstCity.Enabled = False
            'drpLstAoffice.BackColor =  Drawing.Color.
            drpLstCity.CssClass = "textboxgrey"
            'drpLstCity.BackColor = Drawing.Color.Black
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
    '    Response.Redirect("MSUP_ManageAgencyGroup.aspx?Action=I")
    'End Sub

#Region "RebindCity Procedure is Called For Rebinding The City Dropdownbox according to Aoffice selected value"
    Private Sub RebindCity()
        Try
            drpLstCity.Items.Clear()
            If (drpLstAoffice.SelectedIndex > 0) Then
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
                objInputXml.LoadXml("<MS_SEARCHAOFFICECITY_INPUT><Aoffice></Aoffice></MS_SEARCHAOFFICECITY_INPUT>")
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpLstAoffice.SelectedValue
                objOutputXml = objbzAOffice.SearchAofficeCity(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpLstCity.DataSource = ds.Tables("AOFFICECITY")
                    drpLstCity.DataTextField = "City_Name"
                    drpLstCity.DataValueField = "CityID"
                    drpLstCity.DataBind()
                    drpLstCity.Items.Insert(0, "---Select One---")
                End If
            End If
            If (drpLstCity.Items.Count = 0) Then
                drpLstCity.Items.Insert(0, "---Select One---")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            If (drpLstCity.Items.Count = 0) Then
                drpLstCity.Items.Insert(0, "---Select One---")
            End If
        End Try
    End Sub
#End Region
#Region " btnReset_Click Event"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            If (Request.QueryString("Action") = "U") Then
                ViewAgencyGroup()
            Else
                txtChainCode.Text = ""
                txtGroupName.Text = ""
                drpLstAoffice.SelectedIndex = 0
                RebindCity()
                drpLstCity.SelectedIndex = 0
                If (drpLstPriority.Items.Count > 3) Then
                    drpLstPriority.SelectedValue = 2
                End If
                txtCorporateCode.Text = ""
                txtCorporateQualifier.Text = ""
                drpLstGroupType.SelectedIndex = 0
                ChkMainGroup.Checked = False
                txtMainGroupAddress.Text = ""
            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
