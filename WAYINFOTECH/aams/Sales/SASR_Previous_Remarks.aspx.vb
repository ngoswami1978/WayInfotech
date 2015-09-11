
Partial Class Sales_SASR_Previous_Remarks
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt


#Region "Code for Filter "
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        If Not IsPostBack Then
            If Request.QueryString("VisitDate") IsNot Nothing Then
                'strDSRDate = objeAAMS.GetDateFormat(Request.QueryString("VisitDate").ToString, "dd/MM/yyyy", "yyyyMMdd", "/")
                hdVisitDATE.Value = Request.QueryString("VisitDate").ToString
            End If

            If Request.QueryString("Lcode") IsNot Nothing Then
                hdLCode.Value = Request.QueryString("Lcode").ToString
            End If

        End If

        PreviousRemarks()

    End Sub
#End Region

#Region "PreviousRemarks()"
    Private Sub PreviousRemarks()
        Dim ds As New DataSet
        Dim objXmlReader As XmlNodeReader
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objDSR As New AAMS.bizSales.bzDSR
        Try
            objInputXml.LoadXml("<UP_SL_REMARKS_INPUT><DSR_VISIT_ID/><REMARKS_TYPE/> <PREDATE/><LCODE/></UP_SL_REMARKS_INPUT>")
            If Request.QueryString("DSR_VISIT_ID") IsNot Nothing AndAlso Request.QueryString("DSR_VISIT_ID").ToString.Trim.Length > 0 AndAlso Request.QueryString("REMARKS_TYPE") IsNot Nothing AndAlso Request.QueryString("REMARKS_TYPE").ToString.Trim.Length > 0 Then
                If Val(Request.QueryString("REMARKS_TYPE")) = 1 Then
                    lblHeading.Text = "Previous Remarks" + Space(1) + "Service Call"
                ElseIf Val(Request.QueryString("REMARKS_TYPE")) = 2 Then
                    lblHeading.Text = "Previous Remarks" + Space(1) + "Retention"
                ElseIf Val(Request.QueryString("REMARKS_TYPE")) = 3 Then
                    lblHeading.Text = "Previous Remarks" + Space(1) + "Target"
                ElseIf Val(Request.QueryString("REMARKS_TYPE")) = 4 Then
                    lblHeading.Text = "Previous Remarks" + Space(1) + "Air,NonAir"
                End If
                objInputXml.DocumentElement.SelectSingleNode("DSR_VISIT_ID").InnerText = Request.QueryString("DSR_VISIT_ID").ToString
                objInputXml.DocumentElement.SelectSingleNode("REMARKS_TYPE").InnerText = Request.QueryString("REMARKS_TYPE").ToString

                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdLCode.Value
                objInputXml.DocumentElement.SelectSingleNode("PREDATE").InnerText = hdVisitDATE.Value
                'Here Back end Method Call
                objInputXml.Save("c:\admin\aams\GetDSRRemarkInput.xml")
                objOutputXml = objDSR.GetDSRRemark(objInputXml)
                objOutputXml.Save("c:\admin\aams\GetDSRRemarkOutput.xml")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    gvRemarks.DataSource = ds.Tables("REMARKS")
                    gvRemarks.DataBind()
                Else
                    gvRemarks.DataSource = Nothing
                    gvRemarks.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
End Class
