'##########################################################################
'############   Page Name -- Popup_PUSR_ChallanStatusPRHistory      #######
'############   Date 26-March 2008  #######################################
'############   Developed By Abhishek  ####################################
'##########################################################################

Partial Class Popup_PUSR_ChallanStatusPRHistory
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
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        objeAAMS.ExpirePageCache()
        If (Request.QueryString("RowId") IsNot Nothing) Then
            LoadChallanStatusHistroy()
            ' Session("RowId") = Request.QueryString("RowId")
        End If

        'objeAAMS.ExpirePageCache()
    End Sub
    Sub LoadChallanStatusHistroy()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        ' Dim RowNo As Long
        Try
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            objInputXml.LoadXml("<INV_GETPCHISTORY_INPUT><ROWID/></INV_GETPCHISTORY_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ROWID").InnerText = Request.QueryString("RowId")
            'Here Back end Method Call
            objOutputXml = objbzChallan.GetPCReplacementHistory(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdChallanStatusPRHistory.DataSource = ds.Tables("PCHISTORY")
                grdChallanStatusPRHistory.DataBind()
            Else
                grdChallanStatusPRHistory.DataSource = Nothing
                grdChallanStatusPRHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    'Protected Function Geturl() As String
    '    If (Request.QueryString("BDRId") IsNot Nothing) Then
    '        Return "BDRHistoryPrint.aspx?BDRId=" + Request.QueryString("BDRId")
    '    Else
    '        Return "BDRHistoryPrint.aspx"
    '    End If

    'End Function

End Class
