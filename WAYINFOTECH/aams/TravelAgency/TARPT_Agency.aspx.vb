
Partial Class TravelAgency_MSSR_Agency
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
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()
            '  btnReset.Attributes.Add("onclick", "return AgencyReset();")
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
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency List']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Agency List']").Attributes("Value").Value)
                    If strBuilder(4) = "0" Then
                        btnPrint.Enabled = False
                    End If
                    If strBuilder(0) = "0" Then
                        Response.Redirect("~/NoRights.aspx")
                    End If

                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                hdAdvanceSearch.Value = "0" '1
                BindAllControl()
            End If
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))

                If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                    If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                        If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                            Dim li As ListItem
                            li = drpAoffice.Items.FindByValue(objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText)
                            If li IsNot Nothing Then
                                drpAoffice.SelectedValue = li.Value
                            End If

                        End If
                        drpAoffice.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
            objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
            objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True, 3)
            objeAAMS.BindDropDown(drpOnlineStatus, "ONLINESTATUSCODE", True, 3)
            objeAAMS.BindDropDown(drpCRS, "CRS", True, 3)
            objeAAMS.BindDropDown(drpAgencyType, "AGENCYTYPE", True, 3)
            objeAAMS.BindDropDown(drpAgencyStatus, "AGENCYSTATUS", True, 3)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    
    'Method for Agency List
    Private Sub AgencyList()
        Dim objInputXml, objOutputXml As New XmlDocument
        '        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objSecurityXml As New XmlDocument

        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgency
        Try
            objInputXml.LoadXml("<RP_AGENCY_INPUT><AGENCY NAME='' LOCATION_SHORT_NAME='' City_Name='' Country_Name='' StatusCode='' Aoffice='' OFFICEID='' Crs='' ADDRESS='' AgencyStatusId='' AgencyTypeId='' EMAIL='' DATE_ONLINE='' DATE_OFFLINE='' FAX='' FILENO='' IATA_TID='' EmployeeID='' Limited_To_Aoffice='' Limited_To_Region='' Limited_To_OwnAagency='' SecurityRegionID='' ></AGENCY></RP_AGENCY_INPUT>")
            With objInputXml.DocumentElement.SelectSingleNode("AGENCY")
                .Attributes("NAME").InnerText = Trim(txtAgencyName.Text)
                .Attributes("LOCATION_SHORT_NAME").InnerText = Trim(txtShortName.Text)
                If (drpCity.SelectedIndex <> 0) Then
                    .Attributes("City_Name").InnerText = Trim(drpCity.SelectedItem.Text)
                End If
                If (drpCountry.SelectedIndex <> 0) Then
                    .Attributes("Country_Name").InnerText = Trim(drpCountry.SelectedItem.Text)
                End If

                .Attributes("StatusCode").InnerText = Trim(drpOnlineStatus.SelectedValue)
                .Attributes("Aoffice").InnerText = Trim(drpAoffice.SelectedValue)
                .Attributes("OFFICEID").InnerText = Trim(txtOfficeId.Text)
                .Attributes("Crs").InnerText = Trim(drpCRS.SelectedValue)


                If Not Session("LoginSession") Is Nothing Then
                    .Attributes("EmployeeID").InnerText = Session("LoginSession").ToString().Split("|")(0)
                End If
                .Attributes("Limited_To_Aoffice").InnerText = ""
                .Attributes("Limited_To_Region").InnerText = ""
                .Attributes("Limited_To_OwnAagency").InnerText = ""
                .Attributes("SecurityRegionID").InnerText = ""

                If Session("Security") IsNot Nothing Then
                    objSecurityXml.LoadXml(Session("Security"))
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Aoffice").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerText) = "True" Then
                            If (objSecurityXml.DocumentElement.SelectNodes("Aoffice").Count <> 0) Then
                                .Attributes("Limited_To_Aoffice").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("Aoffice").InnerText
                            Else
                                .Attributes("Limited_To_Aoffice").InnerText = ""
                            End If
                        Else
                            .Attributes("Limited_To_Aoffice").InnerText = ""
                        End If
                    Else
                        .Attributes("Limited_To_Aoffice").InnerText = ""
                    End If
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_Region").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerText) = "True" Then
                            If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then
                                .Attributes("Limited_To_Region").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                            Else
                                .Attributes("Limited_To_Region").InnerText = ""
                            End If
                        Else
                            .Attributes("Limited_To_Region").InnerText = ""
                        End If
                    Else
                        .Attributes("Limited_To_Region").InnerText = ""
                    End If
                    If (objSecurityXml.DocumentElement.SelectNodes("Limited_To_OwnAgency").Count <> 0) Then
                        If (objSecurityXml.DocumentElement.SelectSingleNode("Limited_To_OwnAgency").InnerText) = "True" Then
                            .Attributes("Limited_To_OwnAagency").InnerText = 1
                        Else
                            .Attributes("Limited_To_OwnAagency").InnerText = 0
                        End If
                        '.Attributes("Limited_To_OwnAagency").InnerText = ""
                    Else
                        .Attributes("Limited_To_OwnAagency").InnerText = 0
                    End If
                    If (objSecurityXml.DocumentElement.SelectNodes("SecurityRegionID").Count <> 0) Then

                        .Attributes("SecurityRegionID").InnerText = objSecurityXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerText
                    Else
                        .Attributes("SecurityRegionID").InnerText = 0
                    End If
                    'End If

                End If
                If (hdAdvanceSearch.Value = "1") Then
                    .Attributes("ADDRESS").InnerText = Trim(txtAddress.Text)
                    .Attributes("AgencyStatusId").InnerText = Trim(drpAgencyStatus.SelectedValue)
                    .Attributes("AgencyTypeId").InnerText = Trim(drpAgencyType.SelectedValue)
                    .Attributes("EMAIL").InnerText = Trim(txtEmail.Text)
                    .Attributes("DATE_ONLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOnline.Text)
                    .Attributes("DATE_OFFLINE").InnerText = objeAAMS.ConvertTextDate(txtDateOffline.Text)
                    .Attributes("FAX").InnerText = Trim(txtFax.Text)
                    .Attributes("FILENO").InnerText = Trim(txtFielNumber.Text)
                    .Attributes("IATA_TID").InnerText = Trim(txtIATAId.Text)
                End If
            End With
            'Here Back end Method Call
            objOutputXml = objbzAgency.AgencyList(objInputXml)
            ' objOutputXml.Load("C:\Inetpub\wwwroot\RP_AGENCY_OUTPUT.xml")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                Session("eAgencyListRpt") = objOutputXml.OuterXml 'objOxml.OuterXml
                Response.Redirect("../RPSR_ReportShow.aspx?Case=AgencyLsit", False)
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
          
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            AgencyList()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.ToString, False)
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub

    Protected Sub txtAgencyName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAgencyName.TextChanged

    End Sub
End Class
