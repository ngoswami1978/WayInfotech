
Partial Class TravelAgency_MSSR_Agency
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    'Code Added by M. K
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by M. K



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("PageName") = Request.Url.ToString()
        btnNew.Attributes.Add("onclick", "return NewFunction();")
        btnReset.Attributes.Add("onclick", "return AgencyReset();")
        If Not Page.IsPostBack Then
            hdAdvanceSearch.Value = 1
            BindAllControl()
        End If
    End Sub
    Private Sub BindAllControl()
        objeAAMS.BindDropDown(drpCity, "CITY", True)
        objeAAMS.BindDropDown(drpCountry, "COUNTRY", True)
        objeAAMS.BindDropDown(drpAoffice, "AOFFICE", True)
        objeAAMS.BindDropDown(drpOnlineStatus, "OS", True)
        objeAAMS.BindDropDown(drpCRS, "CRS", True)
        objeAAMS.BindDropDown(drpAgencyType, "ATYPE", True)
        objeAAMS.BindDropDown(drpAgencyStatus, "ASTATUS", True)
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AofficeSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub
    'Method for search Agency
    Private Sub AofficeSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAgency As New AAMS.bizTravelAgency.bzAgencyType
        Try
            objInputXml.LoadXml("<MS_SEARCHAGENCY_INPUT><NAME></NAME><LOCATION_SHORT_NAME></LOCATION_SHORT_NAME><City_Name></City_Name><Country_Name></Country_Name><StatusCode></StatusCode><Aoffice></Aoffice><OFFICEID></OFFICEID><Crs></Crs><ADDRESS></ADDRESS><AgencyStatusId></AgencyStatusId><AgencyTypeId></AgencyTypeId><EMAIL></EMAIL><DATE_ONLINE></DATE_ONLINE><DATE_OFFLINE></DATE_OFFLINE><FAX></FAX><FILENO></FILENO><IATA_TID></IATA_TID><EmployeeID></EmployeeID><Limited_To_Aoffice></Limited_To_Aoffice><Limited_To_Region></Limited_To_Region><Limited_To_OwnAagency></Limited_To_OwnAagency><SecurityRegionID></SecurityRegionID></MS_SEARCHAGENCY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("NAME").InnerXml = Trim(txtAgencyName.Text)
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_SHORT_NAME").InnerXml = Trim(txtShortName.Text)
            objInputXml.DocumentElement.SelectSingleNode("City_Name").InnerXml = Trim(drpCity.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("Country_Name").InnerXml = Trim(drpCountry.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("StatusCode").InnerXml = Trim(drpOnlineStatus.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("Aoffice").InnerXml = Trim(drpAoffice.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = Trim(txtOfficeId.Text)
            objInputXml.DocumentElement.SelectSingleNode("Crs").InnerXml = Trim(drpCRS.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerXml = Trim(txtAddress.Text)
            objInputXml.DocumentElement.SelectSingleNode("AgencyStatusId").InnerXml = Trim(drpAgencyStatus.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("AgencyTypeId").InnerXml = Trim(drpAgencyType.SelectedValue)
            objInputXml.DocumentElement.SelectSingleNode("EMAIL").InnerXml = Trim(txtEmail.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATE_ONLINE").InnerXml = Trim(txtDateOnline.Text)
            objInputXml.DocumentElement.SelectSingleNode("DATE_OFFLINE").InnerXml = Trim(txtDateOffline.Text)
            objInputXml.DocumentElement.SelectSingleNode("FAX").InnerXml = Trim(txtFax.Text)
            objInputXml.DocumentElement.SelectSingleNode("FILENO").InnerXml = Trim(txtFielNumber.Text)
            objInputXml.DocumentElement.SelectSingleNode("IATA_TID").InnerXml = Trim(txtIATAId.Text)

            objInputXml.DocumentElement.SelectSingleNode("EmployeeID").InnerXml = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Aoffice").InnerXml = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_Region").InnerXml = ""
            objInputXml.DocumentElement.SelectSingleNode("Limited_To_OwnAagency").InnerXml = ""
            objInputXml.DocumentElement.SelectSingleNode("SecurityRegionID").InnerXml = ""
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
    Private Sub AofficeDelete(ByVal strAoffice As String)
        Dim objInputXml As New XmlDocument
        Dim objbzAOffice As New AAMS.bizMaster.bzAOffice
        objInputXml.LoadXml("<MS_DELETEAGENCY_INPUT><LOCATION_CODE></LOCATION_CODE></MS_DELETEAGENCY_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerXml = strAoffice
        'Here Back end Method Call

        objInputXml = objbzAOffice.Delete(objInputXml)

        If objInputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            Session("Action") = Request.QueryString("Action")
            Response.Redirect("MSSR_Agency.aspx")
        Else
            lblError.Text = objInputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Protected Sub grdAgency_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAgency.ItemCommand
        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If
        Dim s As String
        Dim strBuilder As New StringBuilder
        Dim btnDelete, btnEdit As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lblLocationCode As Label
        btnDelete = CType(e.Item.FindControl("btnDelete"), System.Web.UI.HtmlControls.HtmlAnchor)
        btnEdit = CType(e.Item.FindControl("btnEdit"), System.Web.UI.HtmlControls.HtmlAnchor)
        lblLocationCode = CType(e.Item.FindControl("lblLocationCode"), Label)
        Dim cityval As New HiddenField
        cityval = e.Item.FindControl("hdCITY")
        If strBuilder(3) = "0" Then
            btnDelete.Disabled = True
        Else
            btnDelete.Attributes.Add("OnClick", "return DeleteFunction('" & lblLocationCode.Text & "');")
        End If
        If strBuilder(2) = "0" Then
            btnEdit.Disabled = True
        Else
            s = lblLocationCode.Text & "|" & cityval.Value
            btnEdit.Attributes.Add("OnClick", "return EditFunction('" & s & "');")
            'btnEdit.Attributes.Add("OnClick", "return EditFunction('" & lblLocationCode.Text & "');")
        End If
    End Sub
End Class
