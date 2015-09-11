
Partial Class Popup_PUSR_RegionWiseBudget
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
        LoadHistroy()
    End Sub
    Sub LoadHistroy()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        '  Dim RowNo As Long
        ' Dim s, s1, s2 As String
        Dim s, s2 As String
        Dim UserId As String
        Try
            Dim objbzRegion As New AAMS.bizProductivity.bzRegionBudgetTarget
            objInputXml.LoadXml("<PR_HISTORY_REGIONBUDGETTARGETLOG_INPUT><REGIONBUDGETTARGET Action='' REGIONID='' YEAR='' USERID='' PAGE_NO='' PAGE_SIZE='' SORT_BY='' DESC='' /></PR_HISTORY_REGIONBUDGETTARGETLOG_INPUT>")
            s = Request.QueryString("Str").ToString().Split("|").GetValue(0).ToString()
            s2 = Request.QueryString("Str").ToString().Split("|").GetValue(1).ToString()
            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            With objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET")
                .Attributes("Action").Value = "S"
                .Attributes("REGIONID").Value = s
                .Attributes("YEAR").Value = s2
                .Attributes("USERID").Value = UserId

            End With
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)

            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("PAGE_NO")("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If
            objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "REGIONNAME"
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("SORT_BY").InnerText = "REGIONNAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("REGIONBUDGETTARGET").Attributes("DESC").InnerText = ViewState("Desc")
            End If
            'Here Back end Method Call
            objOutputXml = objbzRegion.History(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdHistory.DataSource = ds.Tables("REGIONBUDGETTARGET")
                grdHistory.DataBind()
                pnlPaging.Visible = True
                Dim count As Integer = CInt(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                Dim selectedValue As String = IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue)
                If count <> ddlPageNumber.Items.Count Then
                    ddlPageNumber.Items.Clear()
                    For i As Integer = 1 To count
                        ddlPageNumber.Items.Add(i.ToString)
                    Next
                End If
                ddlPageNumber.SelectedValue = selectedValue
                'Code for hiding prev and next button based on count
                If count = 1 Then
                    'pnlPaging.Visible = False
                    ' ddlPageNumber.Visible = False
                    lnkNext.Visible = False
                    lnkPrev.Visible = False
                Else
                    'ddlPageNumber.Visible = True
                    lnkPrev.Visible = True
                    lnkNext.Visible = True
                End If

                'Code for hiding next button when pagenumber is equal to page count
                If ddlPageNumber.SelectedValue = count.ToString Then
                    lnkNext.Visible = False
                Else
                    lnkNext.Visible = True
                End If

                'Code for hiding prev button when pagenumber is 1
                If ddlPageNumber.SelectedValue = "1" Then
                    lnkPrev.Visible = False
                Else
                    lnkPrev.Visible = True
                End If
                txtRecordOnCurrentPage.Text = ds.Tables("REGIONBUDGETTARGET").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                SetImageForSorting(grdHistory)
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "REGIONNAME"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                '    Case "DATE"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "USERNAME"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                '    Case "CHANGED"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select

                'End Select
                '' @ Added Code To Show Image'
                ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Else
                grdHistory.DataSource = Nothing
                grdHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
                If ViewState("Desc") = "FALSE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgUp)
                End If
                If ViewState("Desc") = "TRUE" Then
                    grd.HeaderRow.Cells(intcol).Controls.Add(imgDown)
                End If
            End If
        Next
    End Sub
    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            LoadHistroy()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            LoadHistroy()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            LoadHistroy()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdHistory.Sorting
        Try
            Dim SortName As String = e.SortExpression
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = SortName
                ViewState("Desc") = "FALSE"
            Else
                If ViewState("SortName") = SortName Then
                    If ViewState("Desc") = "TRUE" Then
                        ViewState("Desc") = "FALSE"
                    Else
                        ViewState("Desc") = "TRUE"
                    End If
                Else
                    ViewState("SortName") = SortName
                    ViewState("Desc") = "FALSE"
                End If
            End If
            LoadHistroy()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
