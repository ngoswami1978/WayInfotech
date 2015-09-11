
Partial Class TravelAgency_MSUP_AgencyHistory
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS

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
        LoadAgencyHistroy()
        objeAAMS.ExpirePageCache()
    End Sub
    Sub LoadAgencyHistroy()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzAgencyGroup As New AAMS.bizMaster.bzAgencyGroup
            objInputXml.LoadXml("<MS_GETAGENCYHISTORY_INPUT><LOCATION_CODE></LOCATION_CODE></MS_GETAGENCYHISTORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("LOCATION_CODE").InnerText = ""
            'Here Back end Method Call
            objOutputXml = objbzAgencyGroup.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdAgencyHistory.DataSource = ds.Tables("GROUP")
                grdAgencyHistory.DataBind()
            Else
                grdAgencyHistory.DataSource = Nothing
                grdAgencyHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
