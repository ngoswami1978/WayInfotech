
Partial Class Popup_PUSR_Agency
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
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
            lblError.Text = String.Empty
            '***************************************************************************************
            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strBuilder As New StringBuilder
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency Details']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("../NoRights.aspx")
                    End If
                  
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                ' hdAdvanceSearch.Value = 1
                hdAdvanceSearch.Value = "0"
                BindAllControl()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True)
            objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUSCODE", True)
            objeAAMS.BindDropDown(drpCRS, "CRS", True)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True)
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", True)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AgencySearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    'Method for search Agency
    Private Sub AgencySearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID></TA_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("NAME").InnerText = Trim(txtAgencyName.Text)
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText = Trim(txtShortName.Text)
            If (drpCity.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = Trim(drpCity.SelectedItem.Text)
            End If
            If (drpCountry.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("Country_Name").InnerText = Trim(drpCountry.SelectedItem.Text)
            End If

            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = Trim(drpOnlineStatus.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Trim(drpAoffice.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = Trim(txtOfficeId.Text)
            objInputXml.DocumentElement.SelectSingleNode("Crs").InnerText = Trim(drpCRS.SelectedValue)


            If Not Session("LoginSession") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
            End If
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
            objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""

            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                'If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                'objSecurityXml.DocumentElement.SelectNodes("
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                        Else
                            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                    End If
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 1
                    Else
                        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                    End If
                    'objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
                Else
                    objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = 0
                End If
                If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then

                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = 0
                End If
                'End If

            End If


            If (hdAdvanceSearch.Value = "1") Then
                objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = Trim(txtAddress.Text)
                objInputXml.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText = Trim(drpAgencyStatus.SelectedValue)
                objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = Trim(drpAgencyType.SelectedValue)
                objInputXml.DocumentElement.SelectSingleNode("EMAIL").InnerText = Trim(txtEmail.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOnline.Text)
                objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOffline.Text)
                objInputXml.DocumentElement.SelectSingleNode("FAX").InnerText = Trim(txtFax.Text)
                objInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = Trim(txtFielNumber.Text)
                objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerText = Trim(txtIATAId.Text)
            End If
            'Here Back end Method Call
            objOutputXml = objbzAgency.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdAgency.DataSource = ds.Tables("AGNECY")
                grdAgency.DataBind()
            Else
                grdAgency.DataSource = String.Empty
                grdAgency.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    'Private Sub AgencySearch()
    '    Dim objInputXml, objOutputXml As New XmlDocument
    '    Dim objXmlReader As XmlNodeReader
    '    Dim ds As New DataSet
    '    Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
    '    Try
    '        objInputXml.LoadXml("<TA_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID></TA_SEARCHAGENCY_INPUT>")
    '        objInputXml.DocumentElement.SelectSingleNode("NAME").InnerText = Trim(txtAgencyName.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerText = Trim(txtShortName.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerText = Trim(drpCity.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("Country_Name").InnerText = Trim(drpCountry.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerText = Trim(drpOnlineStatus.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerText = Trim(drpAoffice.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerText = Trim(txtOfficeId.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("Crs").InnerText = Trim(drpCRS.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = Trim(txtAddress.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("AgencyStatusId").InnerText = Trim(drpAgencyStatus.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerText = Trim(drpAgencyType.SelectedValue)
    '        objInputXml.DocumentElement.SelectSingleNode("EMAIL").InnerText = Trim(txtEmail.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerText = Trim(txtDateOnline.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerText = Trim(txtDateOffline.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("FAX").InnerText = Trim(txtFax.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("FILENO").InnerText = Trim(txtFielNumber.Text)
    '        objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerText = Trim(txtIATAId.Text)

    '        objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText = ""
    '        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText = ""
    '        objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText = ""
    '        objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerText = ""
    '        objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText = ""
    '        'Here Back end Method Call
    '        objOutputXml = objbzAgency.Search(objInputXml)

    '        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
    '            objXmlReader = New XmlNodeReader(objOutputXml)
    '            ds.ReadXml(objXmlReader)
    '            grdAgency.DataSource = ds.Tables("AGNECY")
    '            grdAgency.DataBind()
    '        Else
    '            grdAgency.DataSource = String.Empty
    '            grdAgency.DataBind()
    '            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = ex.Message.ToString
    '    Finally
    '        objInputXml = Nothing
    '        objOutputXml = Nothing
    '    End Try
    'End Sub

    Protected Sub grdAgency_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAgency.ItemCommand
        Try
            Dim strjscript As String = "<script language=""javascript"">"
            strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
            strjscript = strjscript & "</script" & ">"
            Me.litAgency.Text = strjscript
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
End Class
