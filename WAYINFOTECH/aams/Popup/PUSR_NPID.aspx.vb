Imports System.Data
Imports System.Data.SqlClient

Partial Class Popup_PUSR_NPID
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
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
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
            HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
            HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
          
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            btnReset.Attributes.Add("onclick", "return ISPPlanReset();")
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspPlan']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCityName, "CITY", True)
            End If

            If Not Page.IsPostBack Then
                If strBuilder(0) = "1" Then
                    '  btnSearch_Click(sender, e)

                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Try
    '        Dim objbzISP As New AAMS.bizISP.bzISPPlan

    '        objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID></IS_SEARCHISPPLAN_INPUT>")

    '        'objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = txtISPName.Text.Trim()
    '        'If drpCity.SelectedIndex = 0 Then
    '        '    objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = ""
    '        'Else
    '        '    objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCity.SelectedItem.Text.Trim()
    '        'End If
    '        'objInputXml.DocumentElement.SelectSingleNode("NPID").InnerText = txtNpid.Text.Trim()
    '        objOutputXml = objbzISP.Search(objInputXml)
    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            objXmlReader = New XmlNodeReader(objOutputXml)
    '            ds.ReadXml(objXmlReader)
    '            grdvNpid.DataSource = ds.Tables("ISPPLAN")
    '            grdvNpid.DataBind()
    '        Else
    '            'grdvISPPlan.DataSource = Nothing
    '            'grdvISPPlan.DataBind()
    '            'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        ' lblError.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub grdvISPPlan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdvISPPlan.RowCommand
    '    'Dim strjscript As String = "<script language=""javascript"">"
    '    'strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
    '    'strjscript = strjscript & "</script" & ">"
    '    'Me.litIspPlanId.Text = strjscript
    'End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPPlan

            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID></IS_SEARCHISPPLAN_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = txtISPName.Text.Trim()

            ' objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = txtCity.SelectedValue
            If (drpCityName.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = drpCityName.SelectedValue
            End If
            objInputXml.DocumentElement.SelectSingleNode("NPID").InnerText = txtNpid.Text.Trim()
            'objInputXml.DocumentElement.SelectSingleNode("FullMatch").InnerText = "1"

            'Here Back end Method Call
            objOutputXml = objbzISP.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvISPPlan.DataSource = ds.Tables("ISPPLAN").DefaultView
                grdvISPPlan.DataBind()
                lblError.Text = ""
            Else
                grdvISPPlan.DataSource = Nothing
                grdvISPPlan.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPPlan_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdvISPPlan.RowCommand
        Try
            Dim strjscript As String = "<script language=""javascript"">"
            strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
            strjscript = strjscript & "</script" & ">"
            Me.litIspPlanId.Text = strjscript
        Catch ex As Exception
        End Try
    End Sub
End Class
