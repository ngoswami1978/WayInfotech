
Partial Class Popup_PUSR_MonNo
    Inherits System.Web.UI.Page

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
        If Not IsPostBack Then
            bindGrid()
        End If

    End Sub
    Sub bindGrid()
        Try
            lblError.Text = "A Hardware having number " & Request.QueryString("VENDERSERIALNO").ToString & " is already installed at Following"
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            Dim objXmlReader As XmlNodeReader
            Dim ds As New DataSet

            '<INV_RPTMISCHARDWARE_OUTPUT><MISCHARDWARE LCODE='' AGENCYNAME='' OFFICEID='' CITY='' DATEINSTALLED='' EQUIPMENTTYPE='' EQUIPMENTNO='' ONLINE_STATUS='' /><Errors Status=''><Error Code='' Description=''/></Errors></INV_RPTMISCHARDWARE_OUTPUT>
            objInputXml.LoadXml("<INV_RPTPCINSTALL_INPUT><LCODE/> <AGENCYNAME/><CITY/> <COUNTRY/> <AOFFICE/> <REGION/> <WHOLEGROUP/> <ONLINESTATUS/><EQUIPMENTGROUP /> <EQUIPMENTTYPE/><DATEFROM/> <DATETO/> <RESPONSIBLESTAFFID/> <VENDERSERIALNO/><SECREGIONID/><FPURDATE/><TPURDATE/><VENDORSRNOFLTRTYPE/></INV_RPTPCINSTALL_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("VENDERSERIALNO").InnerText = Request.QueryString("VENDERSERIALNO").ToString
            objInputXml.DocumentElement.SelectSingleNode("VENDORSRNOFLTRTYPE").InnerText = "1"
            'objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTTYPE").InnerText = Request.QueryString("EQUIPMENTTYPE").ToString
            ' objInputXml.DocumentElement.SelectSingleNode("STOCKSTATUS").InnerText = "2"
            objOutputXml = objbzChallan.PCInstallationReport(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("PCINSTALL") IsNot Nothing Then
                    gvInstall.DataSource = ds.Tables("PCINSTALL").DefaultView
                    gvInstall.DataBind()
                Else
                    gvInstall.DataSource = Nothing
                    gvInstall.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
