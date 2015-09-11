Imports System.Xml
Imports System.Data
Partial Class AMS_City_CRS_MS_CRS
    Inherits System.Web.UI.Page
    Dim strBuilder As New StringBuilder
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
        Try
            Session("PageName") = "Setup/MSSR_CRS.aspx"
            objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Crs']").Attributes("Value").Value)
                If strBuilder(0) <> "0" Then
                    AofficeSearch()
                Else
                    Response.Redirect("../NoRights.aspx")
                End If
            Else
                AofficeSearch()
            End If

        Catch ex As Exception

        End Try
        
    End Sub
    'Method for search Aoffice
    Private Sub AofficeSearch()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCRS As New AAMS.bizMaster.bzCRS
        'Here Back end Method Call

        objOutputXml = objbzCRS.List()

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            Dim dView As New DataView
            dView = ds.Tables("CRS").DefaultView
            Session("CRS") = ds.Tables("CRS").DefaultView

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "CRSCode"
            End If
            If ViewState("Direction") Is Nothing Then
                ViewState("Direction") = "asc"
            End If

            If ViewState("Direction") = "asc" Then
                dView.Sort = ViewState("SortName").ToString + "    asc "
            Else
                dView.Sort = ViewState("SortName").ToString + "    desc "
            End If
            gvCRSDetail.DataSource = dView
            gvCRSDetail.DataBind()
            SetImageForSorting(gvCRSDetail)
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
    

    Protected Sub gvCRSDetail_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCRSDetail.Sorting
        Dim dv As DataView
        If Session("CRS") IsNot Nothing Then
            ' Dim dt As DataTable
            dv = CType(Session("CRS"), DataView)
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "CRSCode"
                ViewState("Direction") = "asc"
            Else

                If ViewState("SortName") = SortName Then
                    If ViewState("Direction") = "asc" Then
                        ViewState("Direction") = "desc"
                    Else
                        ViewState("Direction") = "asc"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Direction") = "asc"
                End If
            End If

            dv.Sort = ViewState("SortName").ToString() + " " + ViewState("Direction").ToString()
            gvCRSDetail.DataSource = dv
            gvCRSDetail.DataBind()

          
            SetImageForSorting(gvCRSDetail)
        End If
    End Sub
    Private Sub SetImageForSorting(ByVal grd As GridView)
        Dim imgUp As New Image
        imgUp.ImageUrl = "~/Images/Sortup.gif"
        Dim imgDown As New Image
        imgDown.ImageUrl = "~/Images/Sortdown.gif"
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Dim intcol As Integer = grd.Columns.IndexOf(field)
                If ViewState("Direction") = "asc" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Direction") = "desc" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub
End Class
