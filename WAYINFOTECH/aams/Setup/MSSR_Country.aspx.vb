Imports System.Xml
Imports System.Data


Partial Class Setup_MSSR_Country
    Inherits System.Web.UI.Page
    Dim objDT As System.Data.DataTable
    Dim objDR As System.Data.DataRow
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    'Const ADMIN As String = "31"

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
        Session("PageName") = "Setup/MSSR_Country.aspx"
        objeAAMS.ExpirePageCache()
        If Session("Security") Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            Exit Sub
        End If
        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        Try
            objSecurityXml.LoadXml(Session("Security"))
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Country']").Attributes("Value").Value)
                If strBuilder(0) <> "0" Then
                    CountrySearch()
                Else
                    Response.Redirect("../NoRights.aspx")
                End If
            Else
                CountrySearch()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            Exit Sub
        End Try
    End Sub

    Private Sub CountrySearch()
        Dim objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzCountry As New AAMS.bizMaster.bzCountry
        'Here Back end Method Call
        objOutputXml.LoadXml("<MS_LISTCOUNTRY_OUTPUT><COUNTRY CountryID='' Country_Code='' Country_Name=''/><Errors Status=''><Error Code='' Description=''/></Errors></MS_LISTCOUNTRY_OUTPUT>")
        objOutputXml = objbzCountry.List

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            Dim dView As New DataView
            dView = ds.Tables("COUNTRY").DefaultView
            Session("COUNTRY") = ds.Tables("COUNTRY").DefaultView
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Country_Name"
            End If
            If ViewState("Direction") Is Nothing Then
                ViewState("Direction") = "asc"
            End If

            If ViewState("Direction") = "asc" Then
                dView.Sort = ViewState("SortName").ToString + "    asc "
            Else
                dView.Sort = ViewState("SortName").ToString + "    desc "
            End If
            gvCountryDetail.DataSource = dView
            gvCountryDetail.DataBind()
            SetImageForSorting(gvCountryDetail)
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

    End Sub


    Protected Sub gvCountryDetail_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvCountryDetail.Sorting
        Dim dv As DataView
        If Session("COUNTRY") IsNot Nothing Then
            ' Dim dt As DataTable
            dv = CType(Session("COUNTRY"), DataView)
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Country_Name"
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

            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Country_Name"
            End If
            If ViewState("Direction") Is Nothing Then
                ViewState("Direction") = "asc"
            End If

            If ViewState("Direction") = "asc" Then
                dv.Sort = ViewState("SortName").ToString + "    asc "
            Else
                dv.Sort = ViewState("SortName").ToString + "    desc "
            End If


            gvCountryDetail.DataSource = dv
            gvCountryDetail.DataBind()

            'Dim imgUp As New Image
            'imgUp.ImageUrl = "~/Images/Sortup.gif"
            'Dim imgDown As New Image
            'imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Select Case ViewState("SortName")
            '    Case "Country_Name"
            '        Select Case ViewState("Direction")
            '            Case "desc"
            '                gvCountryDetail.HeaderRow.Cells(0).Controls.Add(imgDown)
            '            Case "asc"
            '                gvCountryDetail.HeaderRow.Cells(0).Controls.Add(imgUp)
            '        End Select
            'End Select
            SetImageForSorting(gvCountryDetail)
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
