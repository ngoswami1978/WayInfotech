
Partial Class Popup_PUSR_AgencyGroup
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
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Group']").Attributes("Value").Value)

                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                btnReset.Attributes.Add("onclick", "return AGroupReset();")
                btnSearch.Attributes.Add("onclick", "return AGroupMandatory();")
                '*******************************************************************
                ''Code For 
                objeAAMS.BindDropDown(drpLstAoffice, "AOFFICE", True)
                objeAAMS.BindDropDown(drpLstGroupType, "AGROUP", True)
                objeAAMS.BindDropDown(drpCity, "CITY", True)
                '*******************************************************************
                ' Load Data For Editing Mode
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub SearchAgencyGroup()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            objInputXml.LoadXml("<MS_SEARCHGROUP_INPUT><Chain_Code></Chain_Code><Chain_Name></Chain_Name><City_Name></City_Name><GroupTypeID></GroupTypeID><Aoffice></Aoffice><MainGroup></MainGroup></MS_SEARCHGROUP_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Chain_Code").InnerText = txtChainCode.Text
            objInputXml.DocumentElement.SelectSingleNode("Chain_Name").InnerText = txtGroupName.Text
            'objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpCity.SelectedItem.Text ' txtCity.Text
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = drpCity.SelectedItem.Text ' txtCity.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("MainGroup").InnerText = chkMainGroup.Checked
            If (drpLstGroupType.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("GroupTypeID").InnerText = drpLstGroupType.SelectedValue
            End If

            If (drpLstAoffice.SelectedIndex = 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = drpLstAoffice.SelectedValue
            End If


            'Here Back end Method Call
            objOutputXml = objbzAgencyGroup.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                gvManageAgencyGroup.DataSource = ds.Tables("GROUP")
                gvManageAgencyGroup.DataBind()
            Else
                gvManageAgencyGroup.DataSource = Nothing
                gvManageAgencyGroup.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("PUUP_AgencyGroup.aspx?Action=I")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            SearchAgencyGroup()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvManageAgencyGroup_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvManageAgencyGroup.RowCommand
        Dim strjscript As String = "<script language=""javascript"">"
        strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
        strjscript = strjscript & "</script" & ">"
        Me.litAgencyGroup.Text = strjscript
    End Sub
End Class
