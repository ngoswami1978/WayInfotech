Imports System.Data
Imports System.Xml
Partial Class Popup_PUSR_OfficeId
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim x As Boolean
    Dim s As String
    Dim s1 As String

    Public strBuilder As New StringBuilder

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
        '''''''''Sent Default Checked CheckBox''''''''
        'If chkunallocatedid.Checked = True Then
        '    x = True
        'End If
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim objInputXml1, objOutputXml1 As New XmlDocument
        Dim ccode As String
        If Request.QueryString("Lcode") = "" Then
            lblError.Text = "Please Select Agency Name"
            Exit Sub
        Else
            ccode = Request.QueryString("Lcode")
        End If
        ddlCorporateCode.Attributes.Add("readonly", "readonly")

        Try
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpCity, "CITYOFFICEIDGENERATION", True)
                s = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                objInputXml1.LoadXml("<TA_AGENCYCORPORATECODE_INPUT><LCode></LCode></TA_AGENCYCORPORATECODE_INPUT>")
                objInputXml1.DocumentElement.SelectSingleNode("LCode").InnerXml = ccode
                objOutputXml1 = objOfficeId.GetAgencyCorporateCode(objInputXml1)
                ddlCorporateCode.Text = objOutputXml1.DocumentElement.SelectSingleNode("AGENCYCORPORATECODE").Attributes("CCode").Value()
                s = Request.QueryString("Action").ToString().Split("|").GetValue(1).ToString()
                If s <> "" Then
                    drpCity.Items.FindByText(s.ToString()).Selected = True
                    drpCity.Enabled = False
                Else
                    drpCity.Enabled = True
                    drpCity.SelectedIndex = -1
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objInputXmlCityCode, objOutputXmlCityCode As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objOfficeId As New AAMS.bizTravelAgency.bzOfficeID
        Dim objcitycode As New AAMS.bizMaster.bzCity
        Dim cc As String
        If lblError.Text = "Please Select Agency Name" Then
            Exit Sub
        Else
            lblError.Text = ""
        End If
        Try
            objInputXml.LoadXml("<TA_SEARCHOFFICEID_INPUT><OFFICEID></OFFICEID><CityCode></CityCode><ALLOCATED></ALLOCATED><CCode></CCode></TA_SEARCHOFFICEID_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OFFICEID").InnerXml = txtOfficeId.Text.Trim()

            If (s <> "") Then
                objInputXmlCityCode.LoadXml("<MS_GETCITYCODE_INPUT><City_Name></City_Name></MS_GETCITYCODE_INPUT>")
                objInputXmlCityCode.DocumentElement.SelectSingleNode("City_Name").InnerXml = s
                objOutputXmlCityCode = objcitycode.GetCityDetails(objInputXmlCityCode)
                cc = objOutputXmlCityCode.DocumentElement.SelectSingleNode("CITY_DETAIL").Attributes("CityCode").Value
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = cc
                objInputXml.DocumentElement.SelectSingleNode("CCode").InnerXml = ddlCorporateCode.Text.Trim()
            Else
                objInputXml.DocumentElement.SelectSingleNode("CityCode").InnerXml = drpCity.SelectedItem.Value
            End If
            If chkunallocatedid.Checked Then
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = "True"
            Else
                objInputXml.DocumentElement.SelectSingleNode("ALLOCATED").InnerXml = "False"
            End If
            objOutputXml = objOfficeId.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdAgencyOfficeId.DataSource = ds.Tables("OFFICEID").DefaultView
                grdAgencyOfficeId.DataBind()
            Else
                grdAgencyOfficeId.DataSource = String.Empty
                grdAgencyOfficeId.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtOfficeId.Text = String.Empty
        'drpCity.SelectedIndex = -1
        lblError.Text = String.Empty
        grdAgencyOfficeId.DataSource = String.Empty
        grdAgencyOfficeId.DataBind()
    End Sub

    Protected Sub grdAgencyOfficeId_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAgencyOfficeId.RowCommand
        'If e.CommandName = "SelectX" Then
        Dim strjscript As String = "<script language=""javascript"">"
        strjscript &= "window.returnValue='" + e.CommandArgument.ToString() + "'; window.close();"
        strjscript = strjscript & "</script" & ">"
        Me.litOfficeId.Text = strjscript
        ' End If
    End Sub

    Protected Sub grdAgencyOfficeId_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAgencyOfficeId.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim lblPDate As New Label
        lblPDate = e.Row.FindControl("lblPD")
        If lblPDate.Text <> "" Then
            lblPDate.Text = objeAAMS.ConvertDate(lblPDate.Text).ToString("dd-MMM-yy")
        End If
    End Sub
End Class




