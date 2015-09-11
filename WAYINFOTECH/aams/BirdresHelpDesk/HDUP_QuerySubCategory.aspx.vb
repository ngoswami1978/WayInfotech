
Partial Class BirdresHelpDesk_HDUP_QuerySubCategory
    Inherits System.Web.UI.Page
    Implements ICallbackEventHandler
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim str As String

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
            Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
            objInputXml.LoadXml("<HD_UPDATECALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY CALL_SUB_CATEGORY_ID='' CALL_SUB_CATEGORY_NAME='' CALL_CATEGORY_ID='' HD_STATUS_ID='' CONTACT_TYPE_ID='' HD_SEVERITY_ID='' TEAM_ID='' TITLE='' /></HD_UPDATECALLSUBCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_NAME").Value = txtSubCategoryName.Text

            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").Value = hdCategoryName.Value

            If Not ViewState("hdCategoryName") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").Value = ViewState("hdCategoryName").ToString()
            End If


            If (hdID.Value <> "") Then
                objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_ID").InnerText = hdID.Value

            Else
                objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_ID").InnerText = ""

            End If

            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_STATUS_ID").InnerText = ddlQueryStatus.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CONTACT_TYPE_ID").InnerText = ddlContactType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_SEVERITY_ID").InnerText = ddlQueryPriority.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("TEAM_ID").InnerText = ddlTeam.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("TITLE").InnerText = txtTitle.Text
            ' Calling Back End Method.
            objOutputXml = objbzCallSubCategory.Update(objInputXml)
            fillCategoryName()
            If Not ViewState("hdCategoryName") Is Nothing Then
                ddlCategoryName.SelectedValue = ViewState("hdCategoryName").ToString()
            Else
                ddlCategoryName.SelectedValue = hdCategoryName.Value
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then

                If hdID.Value = "" Then
                    ddlCategoryName.Enabled = False
                    ddlQuerySubGroup.Enabled = False
                    ViewState("hdCategoryName") = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").Value
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_ID").Value
                    lblError.Text = objeAAMSMessage.messInsert ' "Record added successfully."
                Else
                    hdID.Value = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_ID").Value
                    lblError.Text = objeAAMSMessage.messUpdate '"Record updated successfully."
                End If
                CheckSecurity()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("HDUP_QuerySubCategory.aspx?Action=I|")
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        If (hdID.Value <> "") Then
            lblError.Text = ""
            ViewRecords()
        Else
            txtSubCategoryName.Text = ""
            ddlCategoryName.SelectedIndex = 0
            ddlQuerySubGroup.SelectedIndex = 0
            lblError.Text = ""
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '   txtSubCategoryName.Attributes.Add("onkeypress", "return allTextWithSpace();")
            txtSubCategoryName.Focus()
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "loginScript", objeAAMS.CheckSession())
            End If
            Dim m As ClientScriptManager = Me.ClientScript
            str = m.GetCallbackEventReference(Me, "args", "ReceiveServerData", "'this is context from server'")
            Dim strCallback As String = "function CallServer(args,context){" + str + ";}"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CallServer", strCallback, True)
            ddlQuerySubGroup.Attributes.Add("OnChange", "return fillCategoryName('ddlQuerySubGroup');")
            hdCategoryName.Value = Request.Form(ddlCategoryName.UniqueID)

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(ddlQuerySubGroup, "BRQuerySubGroup", True, 1)
                ' objeAAMS.BindDropDown(ddlCategoryName, "TCATEGORYNAME", True)
                ' Load Data For Editing Mode

                ' Code Add On 27-Jan-09 Start.
                objeAAMS.BindDropDown(ddlQueryStatus, "BRQueryStatus", True, 1)
                objeAAMS.BindDropDown(ddlQueryPriority, "BRQUERYPRIORITY", True, 1)
                objeAAMS.BindDropDown(ddlTeam, "BRTEAM", True, 1)
                objeAAMS.BindDropDown(ddlContactType, "BRCONTACTTYPE", True, 1)
                ' Code Add On 27-Jan-09 End.

                If Request.QueryString("Action").ToString().Split("|").GetValue(0).ToString().ToUpper() = "U" Then
                    hdID.Value = objED.Decrypt(Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString())
                    ViewRecords()
                End If
            End If

            CheckSecurity()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Sub ViewRecords()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzCallSubCategory As New AAMS.bizBRHelpDesk.bzCallSubCategory
        'objInputXml.LoadXml("<MS_VIEW_CALLCATEGORY_INPUT><CALL_SUB_CATEGORY_ID/></MS_VIEW_CALLCATEGORY_INPUT>")
        objInputXml.LoadXml("<HD_VIEWCALLSUBCATEGORY_INPUT><CALL_SUB_CATEGORY_ID /></HD_VIEWCALLSUBCATEGORY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY_ID").InnerText = hdID.Value 'Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
        'Here Back end Method Call 
        objOutputXml = objbzCallSubCategory.View(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            txtSubCategoryName.Text = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_CATEGORY_NAME").InnerText
            ddlQuerySubGroup.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_SUB_GROUP_ID").InnerText
            fillCategoryName()
            ddlCategoryName.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").InnerText
            hdCategoryName.Value = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").InnerText
            ViewState("hdCategoryName") = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CALL_CATEGORY_ID").InnerText
            ddlCategoryName.Enabled = False
            ddlQuerySubGroup.Enabled = False

            If objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("TEAM_ID").InnerText.Trim <> "" Then
                ddlTeam.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("TEAM_ID").InnerText.Trim
            End If
            If objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_SEVERITY_ID").InnerText.Trim <> "" Then
                ddlQueryPriority.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_SEVERITY_ID").InnerText.Trim
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CONTACT_TYPE_ID").InnerText.Trim <> "" Then
                ddlContactType.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("CONTACT_TYPE_ID").InnerText.Trim
            End If

            If objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_STATUS_ID").InnerText.Trim <> "" Then
                ddlQueryStatus.SelectedValue = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("HD_STATUS_ID").InnerText.Trim
            End If

            txtTitle.Text = objOutputXml.DocumentElement.SelectSingleNode("CALL_SUB_CATEGORY").Attributes("TITLE").InnerText()

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return str
    End Function



    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        If eventArgument <> "" Then
            Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim id As String = eventArgument

            '  objInputXml.LoadXml("<MS_SEARCH_CALLCATEGORY_INPUT>	<CALL_CATEGORY_NAME/>	<CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></MS_SEARCH_CALLCATEGORY_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = id
            ' objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "2"
            objOutputXml = objbzCallCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                str = objOutputXml.OuterXml
            Else
                str = ""
            End If
        End If
    End Sub

    Public Sub fillCategoryName()
        If ddlQuerySubGroup.SelectedValue <> "" Then
            Dim objbzCallCategory As New AAMS.bizBRHelpDesk.bzCallCategory
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objInputXml, objOutputXml As New XmlDocument
            ' objInputXml.LoadXml("<MS_SEARCH_CALLCATEGORY_INPUT>	<CALL_CATEGORY_NAME/>	<CALL_SUB_GROUP_ID/><HD_QUERY_GROUP_ID/></MS_SEARCH_CALLCATEGORY_INPUT>")
            objInputXml.LoadXml("<HD_SEARCHCALLCATEGORY_INPUT><HD_QUERY_GROUP_ID>1</HD_QUERY_GROUP_ID><CALL_CATEGORY_NAME /><CALL_SUB_GROUP_ID /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></HD_SEARCHCALLCATEGORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("CALL_SUB_GROUP_ID").InnerText = ddlQuerySubGroup.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("HD_QUERY_GROUP_ID").InnerText = "1"
            objOutputXml = objbzCallCategory.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ddlCategoryName.DataSource = ds.Tables("CALL_CATEGORY")
                ddlCategoryName.DataTextField = "CALL_CATEGORY_NAME"
                ddlCategoryName.DataValueField = "CALL_CATEGORY_ID"
                ddlCategoryName.DataBind()
            End If
            ddlCategoryName.Items.Insert(0, New ListItem("--Select One--", ""))
        Else
            ddlCategoryName.Items.Clear()
            ddlCategoryName.Items.Insert(0, New ListItem("--Select One--", ""))
        End If


    End Sub

    Private Sub CheckSecurity()
        Try
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='BR Call Query SubCategory']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                    btnSave.Enabled = False
                End If
                If strBuilder(2) = "0" And (hdID.Value <> "" Or Request.QueryString("Action") IsNot Nothing) Then
                    btnSave.Enabled = False
                End If

                If strBuilder(1) = "0" And strBuilder(2) <> "0" Then
                    btnSave.Enabled = True
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
End Class
