
Partial Class Setup_MSUP_AgencyGroup
    Inherits System.Web.UI.Page
    Implements System.Web.UI.ICallbackEventHandler
#Region "Page level Variables/Objects"
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim str As String
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
#End Region
    Sub Bindata()
        Dim TabText As New ArrayList()
        Try

            TabText.Add("Agency Group")
            TabText.Add("CRS Details")
            TabText.Add("Competition")
            TabText.Add("Staff")
            TabText.Add("PC")
            TabText.Add("Contract")
            TabText.Add("Business Case")

            theTabStrip.DataSource = TabText
            theTabStrip.DataBind()

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub theTabStrip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles theTabStrip.ItemDataBound
        Dim Button1 As Button
        Button1 = e.Item.FindControl("Button1")
        If e.Item.ItemIndex = 0 Then
            Button1.CssClass = "headingtab"
        Else
            If hdChainCode.Value = "" Then
                Button1.Enabled = False
            End If
        End If

        '@ Start of Code Added For Removing Tab On the basis of Right (Added By Abhishek)
        If Session("Security") IsNot Nothing Then
            Dim strButtonText As String
            strButtonText = Button1.Text
            Select Case strButtonText
                Case "Agency Group"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Agency Group") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "CRS Details"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group CRSDetails") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Competition"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Competition") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Staff"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group Staff") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "PC"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Group PC") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If
                Case "Contract"
                    'If objeAAMS.ReturnViewPermission(Session("Security"),"Group Contract") = "0" Then
                    Button1.CssClass = "displayNone"
                    ' End If
                Case "Business Case"
                    If objeAAMS.ReturnViewPermission(Session("Security"), "Business Case") = "0" Then
                        Button1.CssClass = "displayNone"
                    End If

            End Select
        End If
        '@ End of Code Added For Removing Tab On the basis of Right (Added By Abhishek)

        Button1 = e.Item.FindControl("Button1")
        Button1.Attributes.Add("onclick", "return TabMethodAgencyGroup('" & Button1.ClientID.ToString() & "',7);")

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Request.QueryString("OpenFromInc") IsNot Nothing Then
                ' ImgAgroup.Visible = True
            Else
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").ToString.ToUpper = "I" Then
                        ImgAgroup.Visible = False
                    End If
                End If
                ' ImgAgroup.Visible = False
            End If

            If hdChainCodeFromInc.Value <> "" And hdChainCodeFromInc.Value <> "0" Then
                Response.Redirect("MSUP_AgencyGroup.aspx?&Action=U&Chain_Code=" & objED.Encrypt(hdChainCodeFromInc.Value), False)
            End If

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

            If Request.QueryString("ThenOpenFromInc") IsNot Nothing Then
                hdFromIncentive.Value = 1
            Else
                hdFromIncentive.Value = 1 = 0
            End If

            If Not Page.IsPostBack Then

                '*******************************************************************
                ''Code For 

                ' Populating Group Classification Drop Down.
                ' objeAAMS.BindDropDown(drpGroupClassification, "GROUP_CLASSIFICATION", True, 2)

                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True, 1)
                'objeAAMS.BindDropDown(drpLstCity, "CITY", True)

                drpLstCity.Items.Insert(0, "--Select One--")

                objeAAMS.BindDropDown(drpLstPriority, "PRIORITY", True, 1)
                objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True, 1)
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
                Bindata()
            End If
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            drpLstAoffice.Attributes.Add("OnChange", "return SendAoffice('drpLstAoffice');")
            'drpLstAoffice.Attributes.Add("OnChange", "return CallServer('drpLstAoffice');")

            If Not Page.IsPostBack Then
                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
                End If
                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                                Dim li As ListItem
                                li = drpLstAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                                If li IsNot Nothing Then
                                    drpLstAoffice.SelectedValue = li.Value
                                End If
                            End If
                            drpLstAoffice.Enabled = False
                            RebindCity()
                        End If
                    End If
                End If
            End If

            SecurityForGroupOfficeID()

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


#Region "Code for Filter "
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
        '    If Response.ContentType = "text/html" Then
        '        Response.Filter = New TrimStream(Response.Filter)
        '    End If
        'End If
    End Sub
    'Code Added by M. K

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
                objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = objED.Decrypt(Request.QueryString("Chain_Code").ToString())
                'objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText Request.QueryString("Chain_Code").ToString()
                'Here Back end Method Call
                objOutputXml = objbzAgencyGroup.View(objInputXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    BindAgencyGroupData(objOutputXml)
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            'ImgAgroup.Visible = False
        End Try
    End Sub
#End Region

#Region "btnSave_Click Event is fired for Adding/ Updating the data"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (IsValid) Then
            Try
                Dim objInputXml, objOutputXml As New XmlDocument
                Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup

                'objInputXml.LoadXml("<MS_UPDATEGROUP_INPUT><GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='1' Grouped='0' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='1' CQualifier='' CCode='' MainGroup='' Address ='' /></MS_UPDATEGROUP_INPUT>")
                objInputXml.LoadXml("<MS_UPDATEGROUP_INPUT><GROUP ACTION='' Chain_Code='' Chain_Name='' ContactPerson='' Affiliation='' Priority_Auto_Update='1' Grouped='0' PK_Bkngs='' PriorityID='' GroupTypeID='' Aoffice='' CityID='' Responsible_Location='1' CQualifier='' CCode='' MainGroup='' Address ='' EmployeeId='' GroupClassificationID='' Group_Classification_Name=''  AccountManager='' PANNO='' GROUP_OFFICEID='' COMP_VERTICAL=''/></MS_UPDATEGROUP_INPUT>")
                ' objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerXml = Request.QueryString("Chain_Code")

                With objInputXml.DocumentElement.SelectSingleNode("GROUP")
                    If Request.QueryString("Action").ToUpper = "U" Then
                        .Attributes("Chain_Code").Value() = objED.Decrypt(Request.QueryString("Chain_Code"))
                        '.Attributes("Chain_Code").Value()=  Request.QueryString("Chain_Code")
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


                    If Not Session("LoginSession") Is Nothing Then
                        .Attributes("EmployeeId").Value() = Session("LoginSession").ToString().Split("|")(0)
                    End If
                    'GroupClassificationID='' AccountManager=''
                    '.Attributes("GroupClassificationID").Value() = drpGroupClassification.SelectedValue
                    .Attributes("AccountManager").Value() = hdAccountsManagerID.Value
                    .Attributes("PANNO").Value() = txtPanNo.Text
                    .Attributes("GROUP_OFFICEID").Value() = TxtGroupOfficeID.Text
                    .Attributes("COMP_VERTICAL").Value() = rdbCompVertical.SelectedValue

                End With

                'Here Back end Method Call
                objOutputXml = objbzAgencyGroup.Update(objInputXml)

                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    If Request.QueryString("Action").ToUpper = "U" Then
                        hdChainCode.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value
                        ' hdEnChainCode.Value = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value)
                        Response.Redirect("MSUP_AgencyGroup.aspx?&Msg=U&Action=U&Chain_Code=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value), False)
                        lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
                    Else
                        lblError.Text = objeAAMSMessage.messInsert '"Record added successfully."
                        ' RebindCity()
                        'drpLstCity.SelectedValue = Request.Form("drpLstCity")
                        Response.Redirect("MSUP_AgencyGroup.aspx?&Msg=A&Action=U&Chain_Code=" & objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value), False)
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
        Dim li, li2 As ListItem
        Try

            txtPanNo.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("PANNO").Value()

            txtChainCode.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").InnerXml
            'Dim STR As String = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Name").InnerXml
            txtGroupName.Text = Server.HtmlDecode(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Name").InnerXml)


            li = drpLstAoffice.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerXml)
            If li IsNot Nothing Then
                drpLstAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerXml
            End If

            ' drpLstAoffice.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerXml
            RebindCity()


            li2 = drpLstCity.Items.FindByValue(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CityID").InnerXml)
            If li2 IsNot Nothing Then
                drpLstCity.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CityID").InnerXml
            End If

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

            drpLstCity.CssClass = "textboxgrey"

            If objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Aoffice").InnerXml.ToString().Trim.Length = 0 Then
                drpLstAoffice.CssClass = "dropdown"
                drpLstAoffice.Enabled = True
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("CityID").InnerXml.ToString().Trim.Length = 0 Then
                drpLstCity.CssClass = "dropdown"
                drpLstCity.Enabled = True
            End If

            'Code Added On 02June
            txtType.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Group_Classification_Name").Value


          

            txtAccountsManager.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("AccountManagerName").Value
            hdAccountsManagerID.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("AccountManager").Value
            txtRegion.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Region").Value
            hdChainCode.Value = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").InnerXml
            hdEnChainCode.Value = objED.Encrypt(objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("Chain_Code").Value)
            TxtGroupOfficeID.Text = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("GROUP_OFFICEID").Value

            If (objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL").Value.ToString() & "") <> "" Then
                rdbCompVertical.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("GROUP").Attributes("COMP_VERTICAL").Value.ToString() & ""
            Else
                rdbCompVertical.SelectedValue = Nothing
            End If

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
                    drpLstCity.Items.Insert(0, "--Select One--")
                    txtRegion.Text = objOutputXml.DocumentElement.SelectSingleNode("AOFFICECITY").Attributes("Region").Value
                End If
            End If
            If (drpLstCity.Items.Count = 0) Then
                drpLstCity.Items.Insert(0, "--Select One--")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            If (drpLstCity.Items.Count = 0) Then
                drpLstCity.Items.Insert(0, "--Select One--")
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
                txtPanNo.Text = ""
                If objeAAMS.Limited_To_Aoffice(Session("Security")) = "" Then
                    drpLstAoffice.SelectedIndex = 0
                End If

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

                'Code Added By Pankaj
                txtAccountsManager.Text = ""
                hdAccountsManagerID.Value = ""
                txtRegion.Text = ""
                hdChainCode.Value = ""
                rdbCompVertical.SelectedValue = Nothing

            End If
            lblError.Text = ""
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            SecurityForGroupOfficeID()
        End Try
    End Sub
#End Region

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("MSUP_AgencyGroup.aspx?Action=I")
    End Sub
    Private Sub SecurityForGroupOfficeID()
        Dim objSecurityXml As New XmlDocument
        Dim strBuilderForGroupOfficeId As New StringBuilder
        Try
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                        strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)
                    Else
                        strBuilder = objeAAMS.SecurityCheck(0)
                    End If
                Else
                    strBuilder = objeAAMS.SecurityCheck(31)
                End If
                If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                    If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Main OfficeID']").Count <> 0 Then
                        strBuilderForGroupOfficeId = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Group Main OfficeID']").Attributes("Value").Value)
                    Else
                        strBuilderForGroupOfficeId = objeAAMS.SecurityCheck(0)
                    End If
                Else
                    strBuilderForGroupOfficeId = objeAAMS.SecurityCheck(31)
                End If
                If Request.QueryString("Action").ToUpper = "U" Then
                    If strBuilder(2) = "1" And strBuilderForGroupOfficeId(2) = "1" Then

                        TxtGroupOfficeID.CssClass = "textbox"
                        TxtGroupOfficeID.ReadOnly = False
                        ImgAgency.Visible = True
                    Else
                        TxtGroupOfficeID.CssClass = "textboxgrey"
                        TxtGroupOfficeID.ReadOnly = True
                        ImgAgency.Visible = False
                    End If
                Else
                    TxtGroupOfficeID.CssClass = "textboxgrey"
                    TxtGroupOfficeID.ReadOnly = True
                    ImgAgency.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
