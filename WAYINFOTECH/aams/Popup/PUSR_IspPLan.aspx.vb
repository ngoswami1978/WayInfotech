Imports System.Data
Partial Class Popup_PUSR_IspPLan
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objeAAMSMessage As New eAAMSMessage


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
            Dim Lcode As String
            Dim objInputXmlAgency, objOutputXmlAgency As New XmlDocument
            Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
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
            If (Request.QueryString("IspName") IsNot Nothing) Then
                txtISPName.Text = Request.QueryString("IspName").ToString()
            End If

            'If Not Page.IsPostBack Then
            ' objeAAMS.BindDropDown(drpCityName, "CITY", True)
            ' End If
            If Request.QueryString("Lcode") IsNot Nothing Then
                Lcode = Request.QueryString("Lcode").ToString
                objInputXmlAgency.LoadXml("<TA_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></TA_VIEWAGENCY_INPUT>")
                objInputXmlAgency.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Lcode ' Session("Action").ToString().Split("|").GetValue(1)

                'Here Back end Method Call
                objOutputXmlAgency = objbzAgency.View(objInputXmlAgency)

                If objOutputXmlAgency.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    With objOutputXmlAgency.DocumentElement.SelectSingleNode("Agency")
                        'Dim li As New ListItem
                        'li = drpCityName.Items.FindByText(.Attributes("CITY").Value())
                        'If (li IsNot Nothing) Then
                        '    li.Selected = True
                        'End If
                        txtCity.Text = .Attributes("CITY").Value()
                    End With
                End If
            End If

            If Not Page.IsPostBack Then
                If strBuilder(0) = "1" Then
                    btnSearch_Click(sender, e)

                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPPlan

            objInputXml.LoadXml("<IS_SEARCHISPPLAN_INPUT><ISPID></ISPID><Name></Name><CityID></CityID><NPID></NPID></IS_SEARCHISPPLAN_INPUT>")
            If Request.QueryString("IspId") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("ISPID").InnerText = Request.QueryString("IspId").ToString() 'txtISPName.Text.Trim()
            End If
            ' objInputXml.DocumentElement.SelectSingleNode("CityID").InnerText = txtCity.SelectedValue
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
