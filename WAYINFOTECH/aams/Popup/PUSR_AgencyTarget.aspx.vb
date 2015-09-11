
Partial Class Popup_PUSR_AgencyTarget
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
        ' Dim RowNo As Long
        Dim s1, s2, s3, s4 As String
        s1 = ""
        s2 = ""
        s3 = ""
        s4 = ""

        Dim UserId As String
        Try
            Dim objbzAgencyHistory As New AAMS.bizTravelAgency.bzAgencyTarget
            objInputXml.LoadXml("<TA_HISTORY_AGENCYTARGET_INPUT><TARGET  Action='' LCode=''   Year=''   Month='' SalesPersonId='' Resp_1a='' PAGE_NO='' PAGE_SIZE='' SORT_BY='' DESC='' /></TA_HISTORY_AGENCYTARGET_INPUT>")
            If Request.QueryString("LCode") IsNot Nothing Then
                s1 = Request.QueryString("LCode").ToString
            End If
            If Request.QueryString("Year") IsNot Nothing Then
                s2 = Request.QueryString("Year").ToString
            End If
            If Request.QueryString("Month") IsNot Nothing Then
                s3 = Request.QueryString("Month").ToString
            End If
            If Request.QueryString("SalesPersonId") IsNot Nothing Then
                s4 = Request.QueryString("SalesPersonId").ToString
            End If


            Dim objEmpXml As New XmlDocument
            objEmpXml.LoadXml(Session("Security"))
            UserId = objEmpXml.DocumentElement.SelectSingleNode("EmployeeID").InnerText.Trim()
            With objInputXml.DocumentElement.SelectSingleNode("TARGET")
                .Attributes("Action").Value = ""
                .Attributes("LCode").Value = s1
                .Attributes("Year").Value = s2
                .Attributes("Month").Value = s3
                .Attributes("SalesPersonId").Value = s4
                .Attributes("Resp_1a").Value = UserId

            End With
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)

            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If
            objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Year"
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("SORT_BY").InnerText = "Year" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("TARGET").Attributes("DESC").InnerText = ViewState("Desc")
            End If
            'Here Back end Method Call
            objOutputXml = objbzAgencyHistory.History(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdHistory.DataSource = ds.Tables("TARGET")
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
                txtRecordOnCurrentPage.Text = ds.Tables("TARGET").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'

                SetImageForSorting(grdHistory)

                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "Year"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select
                '    Case "EmpName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select
                '    Case "Date"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                '    Case "ChangedData"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdHistory.HeaderRow.Cells(3).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdHistory.HeaderRow.Cells(3).Controls.Add(imgDown)
                '        End Select

                'End Select



            Else
                grdHistory.DataSource = Nothing
                grdHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
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

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            LoadHistroy()
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
End Class
