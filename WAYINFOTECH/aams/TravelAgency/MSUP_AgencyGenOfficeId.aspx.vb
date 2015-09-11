Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class TravelAgency_MSGen_OfficeId
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objtaCorporateCodes As New AAMS.bizTravelAgency.bzCorporateCodes

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
        Dim objInputXml As New XmlDocument
        If Not Page.IsPostBack Then
            btnGenerate.Attributes.Add("onclick", "return ChkMandatory();")
            'objeAAMS.BindDropDown(drpCity, "CITY", True)
            objeAAMS.BindDropDown(drpCity, "CITYOFFICEIDGENERATION", True)
            objeAAMS.BindDropDown(ddlOfficeType, "OFFICETYPE", True)
            ddlCorporateCode.Attributes.Add("readonly", "readonly")
            txtCorporateQualifier.Attributes.Add("readonly", "readonly")
        End If
    End Sub
    Protected Sub btn_Reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Reset.Click
        txtOfficeId.Text = String.Empty
        drpCity.SelectedIndex = 0
        ddlCorporateCode.Text = String.Empty
        txtCorporateQualifier.Text = String.Empty
        ddlOfficeType.Text = String.Empty
        lblError.Text = String.Empty
    End Sub


    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim objInputXml As New XmlDataDocument, objOutputXml As New XmlDocument
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim ds As New DataSet
        objInputXml.LoadXml("<TA_GENERATEOFFICEID_INPUT><GENERATEOFFICEID OFFICEID='' CityCode=''   CorporateCode='' CorporateQualifier='' OFFICEID_TYPE_ID='' /></TA_GENERATEOFFICEID_INPUT>")
        With objInputXml.DocumentElement.SelectSingleNode("GENERATEOFFICEID")
            If Request.QueryString("ACTION").ToString().Trim().ToUpper = "I" Then
                .Attributes("OFFICEID").Value() = String.Empty
                '.Attributes("CityID").Value() = drpCity.Text.Trim()
                '.Attributes("CityCode").Value() = drpCity.SelectedValue
                .Attributes("CityCode").Value() = drpCity.SelectedValue.Split("|")(1)
                ' .Attributes("CorporateCode").Value() = ddlCorporateCode.Text.Trim()
                ' .Attributes("CorporateQualifier").Value() = txtCorporateQualifier.Text.Trim()
                .Attributes("CorporateCode").Value() = Request.Form("ddlCorporateCode")
                .Attributes("CorporateQualifier").Value() = Request.Form("txtCorporateQualifier")
                .Attributes("OFFICEID_TYPE_ID").Value() = ddlOfficeType.Text.Trim()
            End If
        End With
        objOutputXml = objOfficeId.GenerateOfficeID(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            With objOutputXml.DocumentElement.SelectSingleNode("GENERATEOFFICEID")
                txtOfficeId.Text = .Attributes("OFFICEID").Value()
            End With
            lblError.Text = objeAAMSMessage.messOfficeId
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
End Class
