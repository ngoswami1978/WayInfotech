
Partial Class Popup_BDRHistoryPrint
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
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        If (Session("BDRId") IsNot Nothing) Then
            LoadBDRHistroy()
        End If
    End Sub
    Sub LoadBDRHistroy()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        ' Dim RowNo As Long
        Try
            Dim objbzRequestBDR As New AAMS.bizHelpDesk.bzRequestBDR
            objInputXml.LoadXml("<HD_GETHISTORYBDR_INPUT><HD_RE_BDR_ID /></HD_GETHISTORYBDR_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("HD_RE_BDR_ID").InnerText = Session("BDRId") 'Request.QueryString("BDRId")
            'Here Back end Method Call
            objOutputXml = objbzRequestBDR.GetHistory(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                'For RowNo = 0 To ds.Tables("BRDHISTORY").Rows.Count - 1
                '    If (ds.Tables("BRDHISTORY").Rows(RowNo)("DateTime") IsNot Nothing) Then
                '        ds.Tables("BRDHISTORY").Rows(RowNo)("DateTime") = objeAAMS.ConvertDateBlank(ds.Tables("AGENCYHISTORY").Rows(RowNo)("DateTime").ToString())
                '    End If
                'Next
                grdBDRHistory.DataSource = ds.Tables("HISTORYDETAIL")
                grdBDRHistory.DataBind()
            Else
                grdBDRHistory.DataSource = Nothing
                grdBDRHistory.DataBind()
                'lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
End Class
