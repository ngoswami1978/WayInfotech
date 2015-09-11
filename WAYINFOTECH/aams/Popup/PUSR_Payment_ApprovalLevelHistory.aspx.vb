'##############################################################
'############ Page Name -- Popup_PUSR_Payment_ApprovalLevelHistory #
'############   Date 09-Seltember 2009  ########################
'############   Developed By Abhishek  ########################
'##############################################################
Partial Class Popup_PUSR_Payment_ApprovalLevelHistory
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        HttpContext.Current.Response.Cache.SetLastModified(DateTime.Now)
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(False)
        objeAAMS.ExpirePageCache()
        If Not IsPostBack Then
            If (Request.QueryString("Aoffice") IsNot Nothing) Then
                LoadBCApprovalLevelHistroy()
                '  Session("Aoffice") = objED.Decrypt(Request.QueryString("Aoffice"))
            End If
        End If

        'objeAAMS.ExpirePageCache()
    End Sub
    Sub LoadBCApprovalLevelHistroy()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        'Dim RowNo As Long
        Try
            Dim objbzPaymentApprovalLavel As New AAMS.bizIncetive.bzPaymentApprovalLavel

            objInputXml.LoadXml("<INC_PA_APPROVAL_LEVEL_EMP_INPUT><AOFFICE>DEL</AOFFICE><SORT_BY></SORT_BY><DESC></DESC><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE></INC_PA_APPROVAL_LEVEL_EMP_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objED.Decrypt(Request.QueryString("Aoffice")) 'Rakesh Added on 2 Aug 08

            '  <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/>
            '@ Coding For Paging Ansd sorting
            If ViewState("PrevSearching") Is Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
            Else
                Dim objTempInputXml As New XmlDocument
                Dim objNodeList As XmlNodeList

                objTempInputXml.LoadXml(ViewState("PrevSearching"))
                objNodeList = objTempInputXml.DocumentElement.ChildNodes
                objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = IIf(ddlPageNumber.Items.Count = 0, "1", ddlPageNumber.SelectedValue)
                For Each objNode As XmlNode In objNodeList
                    If objNode.Name <> "PAGE_NO" And objNode.Name <> "SORT_BY" And objNode.Name <> "DESC" And objNode.Name <> "PAGE_SIZE" Then
                        If objNode.InnerText <> objInputXml.DocumentElement.SelectSingleNode(objNode.Name.ToString).InnerText Then
                            objInputXml.DocumentElement.SelectSingleNode("PAGE_NO").InnerText = "1"
                            ddlPageNumber.SelectedValue = "1"
                        End If
                    End If
                Next
            End If


            objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "EMPLOYEE_NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EMPLOYEE_NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            'End Code for paging and sorting


            'Here Back end Method Call
            objOutputXml = objbzPaymentApprovalLavel.PaymentAdviceHistorySearch(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                'For RowNo = 0 To ds.Tables("HISTORYDETAIL").Rows.Count - 1
                '    If (ds.Tables("BRDHISTORY").Rows(RowNo)("DateTime") IsNot Nothing) Then
                '        ds.Tables("BRDHISTORY").Rows(RowNo)("DateTime") = objeAAMS.ConvertDateBlank(ds.Tables("HISTORYDETAIL").Rows(RowNo)("DateTime").ToString())
                '    End If
                'Next
                grdPaymentApprovalLevelHistory.DataSource = ds.Tables("HIS")
                grdPaymentApprovalLevelHistory.DataBind()
                ' ##################################################################
                '@ Code Added For Paging And Sorting In case Of 
                ' ###################################################################
                'BindControlsForNavigation(2)
                BindControlsForNavigation(objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("PAGE_COUNT").Value)
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdPaymentApprovalLevelHistory)
                ' ###################################################################
                '@ End of Code Added For Paging And Sorting In case 
                ' ###################################################################

                pnlPaging.Visible = True


            Else
                grdPaymentApprovalLevelHistory.DataSource = Nothing
                grdPaymentApprovalLevelHistory.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                pnlPaging.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            pnlPaging.Visible = False

        End Try
    End Sub
    'Protected Function Geturl() As String
    '    If (Request.QueryString("BDRId") IsNot Nothing) Then
    '        Return "BDRHistoryPrint.aspx?BDRId=" + Request.QueryString("BDRId")
    '    Else
    '        Return "BDRHistoryPrint.aspx"
    '    End If

    'End Function
#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            If (Request.QueryString("Aoffice") IsNot Nothing) Then
                LoadBCApprovalLevelHistroy()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            If (Request.QueryString("Aoffice") IsNot Nothing) Then
                LoadBCApprovalLevelHistroy()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            If (Request.QueryString("Aoffice") IsNot Nothing) Then
                LoadBCApprovalLevelHistroy()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdPaymentApprovalLevelHistory_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPaymentApprovalLevelHistory.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdPaymentApprovalLevelHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPaymentApprovalLevelHistory.Sorting
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
            If (Request.QueryString("Aoffice") IsNot Nothing) Then
                LoadBCApprovalLevelHistroy()
            End If

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub BindControlsForNavigation(ByVal CrrentPageNo As String)
        ' ##################################################################
        '@ Code Added For Paging And Sorting
        ' ###################################################################
        pnlPaging.Visible = True
        '  Dim count As Integer = 0
        Dim count As Integer = CInt(CrrentPageNo)
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

        ' ###################################################################
        '@ End of Code Added For Paging And Sorting 
        ' ###################################################################
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

#End Region

    Protected Sub grdPaymentApprovalLevelHistory_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPaymentApprovalLevelHistory.RowDataBound
        Try


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
End Class
