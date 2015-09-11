
Partial Class TravelAgency_MSUP_AgencyProducts
    Inherits System.Web.UI.Page
#Region "Global Declaration Section"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
#End Region


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
            Session("PageName") = "TravelAgency/MSUP_AgencyProducts.aspx"
            'btnReset.Attributes.Add("onclick", "return OrderStatusReset();")
            ' btnNew.Attributes.Add("onclick", "return NewFunction();")
            btnAdd.Attributes.Add("onclick", "return ChkEmptyProductName();")
            lblError.Text = String.Empty
            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        OrderStatusDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            '***************************************************************************************
            'Code of Security Check
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Dim strLcode As String = Nothing
            DisplayProductList(strLcode)


        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub AddRecordsTemprory()
        Dim objtaInputXml As New XmlDocument
        Dim objtaOutputXml As New XmlDocument
        'Dim objxmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objtaAgencyProducts As New AAMS.bizTravelAgency.bzAgencyProduct
        objtaOutputXml = ViewState("SaveXml")


    End Sub
    Private Sub DisplayProductList(ByVal strLcode As String)
        Try
            Dim objtaInputXml As New XmlDocument
            Dim objtaOutputXml As New XmlDocument
            Dim objxmlReader As XmlNodeReader
            Dim ds As New DataSet
            Dim objtaAgencyProducts As New AAMS.bizTravelAgency.bzAgencyProduct
            ' Dim objtaProductList As New AAMS.bizTravelAgency.bzProduct


            objtaInputXml.LoadXml("<MS_GETAGENCYPRODUCT_INPUT><LCODE /></MS_GETAGENCYPRODUCT_INPUT>")
            objtaInputXml.DocumentElement("LCODE").InnerText = "51" 'strLcode
            objtaOutputXml = objtaAgencyProducts.GetDetails(objtaInputXml)
            'for Saving Recordws
            ViewState("SaveXml") = objtaOutputXml

            If objtaOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objxmlReader = New XmlNodeReader(objtaOutputXml)
                ds.ReadXml(objxmlReader)
                gvAgencyProduct.DataSource = ds.Tables("AGENCYPRODUCT").DefaultView
                gvAgencyProduct.DataBind()
            Else
                gvAgencyProduct.DataSource = Nothing
                gvAgencyProduct.DataBind()
                lblError.Text = objtaOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
