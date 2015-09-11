
Partial Class TravelAgency_MSUP_Agency
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
        Session("PageName") = Request.Url.ToString()
        Try
            If Not Page.IsPostBack Then
                BindAllControl()
                If Not Request.QueryString("Action") Is Nothing Then
                    Session("Action") = Request.QueryString("Action")
                    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                        AgencyView()
                    End If
                Else
                    If Session("Action").ToString().Split("|").GetValue(0) = "U" Then
                        AgencyView()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub BindAllControl()
        Try
            objeAAMS.BindDropDown(drpCity, "CITY", True)
            objeAAMS.BindDropDown(drpStatus, "ASTATUS", True)
            objeAAMS.BindDropDown(drpPriority, "PRIORITY", True)
            objeAAMS.BindDropDown(drpType, "ATYPE", True)
            objeAAMS.BindDropDown(drpPrimaryOnlineStatus, "OS", True)
            objeAAMS.BindDropDown(drpBackupOnlineStatus, "OS", True)
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objeAAMS = Nothing
        End Try

    End Sub
    '*********************************************************************************************************
    '****************************Method for View Agency *****************************************************
    '*********************************************************************************************************
    Private Sub AgencyView()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizMaster.bzAgencyGroup
        Try
            objInputXml.LoadXml("<MS_VIEWAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></MS_VIEWAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = Request.QueryString("Action").ToString().Split("|").GetValue(1)
            'Here Back end Method Call

            objOutputXml = objbzAgency.View(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                With objOutputXml.DocumentElement.SelectSingleNode("Agency")
                    txtAgencyGroup.Text = .Attributes("Chain_Name").Value()
                    hdChainId.Value = .Attributes("Chain_Code").Value()
                    txtName.Text = .Attributes("NAME").Value()
                    txtAddress1.Text = .Attributes("ADDRESS").Value()
                    txtAddress2.Text = .Attributes("ADDRESS1").Value()
                    drpCity.SelectedValue = .Attributes("CITY").Value()
                    txtCountry.Text = .Attributes("COUNTRY").Value()
                    txtEmail.Text = .Attributes("EMAIL").Value()
                    txtPhone.Text = .Attributes("PHONE").Value()
                    txtFax.Text = .Attributes("FAX").Value()
                    txtPinCode.Text = .Attributes("PINCODE").Value()
                    txtOfficeName.Text = .Attributes("CityID").Value()
                    drpStatus.SelectedValue = .Attributes("AGENCYSTATUSID").Value()
                    txtIataId.Text = .Attributes("IATA_TID").Value()
                    txtWevSite.Text = .Attributes("WWW_ADDRESS").Value()
                    txtAoffice.Text = .Attributes("Aoffice").Value()
                    drpType.SelectedValue = .Attributes("AGENCYTYPEID").Value()
                    txtDateOnline.Text = .Attributes("DATE_ONLINE").Value()
                    If .Attributes("INCLUDE_IN_CCR").Value() = "" Then
                        rdCCRoster.Checked = True
                    End If
                    txtReason.Text = .Attributes("INCLUDE_IN_CCR_REASON").Value()
                    txtAResponsibility.Text = .Attributes("RESP_1A_NAME").Value()
                    'txtAResponsibility.Text = .Attributes("RESP_1A").Value()
                    drpPriority.SelectedValue = .Attributes("PRIORITYID").Value()
                    txtDateOffline.Text = .Attributes("DATE_OFFLINE").Value()

                    drpFileNumber.SelectedValue = .Attributes("FILENO").Value()
                    drpPrimaryOnlineStatus.SelectedValue = .Attributes("ONLINE_STATUS").Value()
                    drpBackupOnlineStatus.SelectedValue = .Attributes("ONLINE_STATUS_BACKUP").Value()
                    txtPrimaryDate.Text = .Attributes("INSTALL_DATE_PRIMARY").Value()
                    txtBackupDate.Text = .Attributes("INSTALL_DATE_BACKUP").Value()
                    txtPrimaryOrderNumber.Text = .Attributes("ORDERNUMBER_PRIMARY").Value()
                    txtBackupOrderNumber.Text = .Attributes("ORDERNUMBER_BACKUP").Value()
                End With
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveAgency()
    End Sub
    Private Sub SaveAgency()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAgency As New AAMS.bizMaster.bzAOffice
        objInputXml.LoadXml("<MS_UPDATEAGENCY_INPUT><Agency LOCATION_CODE='' Chain_Code='' IATA_TID='' EMAIL='' FAX='' PHONE='' COUNTRY='' PINCODE='' CITY='' ADDRESS1='' ADDRESS='' NAME='' Aoffice='' CONTACT_PERSON_ID='' ORDERNUMBER_BACKUP='' ORDERNUMBER_PRIMARY='' INSTALL_DATE_BACKUP='' INSTALL_DATE_PRIMARY='' ONLINE_STATUS_BACKUP='' AGENCYSTATUSID='' AGENCYTYPEID='' LOCATION_SHORT_NAME='' PRIORITYID='' RESP_1A='' WWW_ADDRESS='' ONLINE_STATUS='' DATE_OFFLINE='' DATE_ONLINE='' FILENO='' INCLUDE_IN_CCR='' INCLUDE_IN_CCR_REASON='' /></MS_UPDATEAGENCY_INPUT>")
        Try
            With objInputXml.DocumentElement.SelectSingleNode("Agency")

                .Attributes("Chain_Name").Value() = txtAgencyGroup.Text
                .Attributes("Chain_Code").Value() = hdChainId.Value
                .Attributes("NAME").Value() = txtName.Text
                .Attributes("ADDRESS").Value() = txtAddress1.Text
                .Attributes("ADDRESS1").Value() = txtAddress2.Text
                .Attributes("CITY").Value() = drpCity.SelectedValue
                .Attributes("COUNTRY").Value() = txtCountry.Text
                .Attributes("EMAIL").Value() = txtEmail.Text
                .Attributes("PHONE").Value() = txtPhone.Text
                .Attributes("FAX").Value() = txtFax.Text
                .Attributes("PINCODE").Value() = txtPinCode.Text
                .Attributes("CityID").Value() = txtOfficeName.Text
                .Attributes("AGENCYSTATUSID").Value() = drpStatus.SelectedValue
                .Attributes("IATA_TID").Value() = txtIataId.Text
                .Attributes("WWW_ADDRESS").Value() = txtWevSite.Text
                .Attributes("Aoffice").Value() = txtAoffice.Text
                .Attributes("AGENCYTYPEID").Value() = drpType.SelectedValue
                .Attributes("DATE_ONLINE").Value() = txtDateOnline.Text
                If .Attributes("INCLUDE_IN_CCR").Value() = "" Then
                    rdCCRoster.Checked = True
                End If
                .Attributes("INCLUDE_IN_CCR_REASON").Value() = txtReason.Text
                .Attributes("RESP_1A_NAME").Value() = txtAResponsibility.Text
                'txtAResponsibility.Text = .Attributes("RESP_1A").Value()
                .Attributes("PRIORITYID").Value() = drpPriority.SelectedValue
                .Attributes("DATE_OFFLINE").Value() = txtDateOffline.Text
                .Attributes("FILENO").Value() = drpFileNumber.SelectedValue
                .Attributes("ONLINE_STATUS").Value() = drpPrimaryOnlineStatus.SelectedValue
                .Attributes("ONLINE_STATUS_BACKUP").Value() = drpBackupOnlineStatus.SelectedValue
                .Attributes("INSTALL_DATE_PRIMARY").Value() = txtPrimaryDate.Text
                .Attributes("INSTALL_DATE_BACKUP").Value() = txtBackupDate.Text
                .Attributes("ORDERNUMBER_PRIMARY").Value() = txtPrimaryOrderNumber.Text
                .Attributes("ORDERNUMBER_BACKUP").Value() = txtBackupOrderNumber.Text

            End With

            'Here Back end Method Call
            objOutputXml = objbzAgency.Update(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "U" Then
                    lblError.Text = objeAAMSMessage.messUpdate
                Else
                    lblError.Text = objeAAMSMessage.messInsert
                End If
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
            objbzAgency = Nothing
        End Try
    End Sub
End Class
