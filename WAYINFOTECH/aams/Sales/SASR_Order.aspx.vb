Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Sales_SASR_Order
    Inherits System.Web.UI.Page

#Region "Global Variable Declarations."
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objRepDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument
#End Region

#Region "Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim strurl As String = Request.Url.ToString()
            Session("PageName") = strurl
            ' Checking security.
            CheckSecurity()
            If Not Page.IsPostBack Then
                btnSearch.Attributes.Add("onClick", "return ValidateForm();")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Response.Redirect("SASR_Order.aspx")
            ' GeneratePDF("73")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "BindData()"
    Private Sub BindData(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objProfile As New AAMS.bizSales.bzOnlineOrder
        Try
            objInputXml.LoadXml("<UP_ORD_SEARCH_ONLINEORDERAGENCYPROFILE_INPUT><AgencyName/><DateFrom/><DateTo/><PAGE_NO></PAGE_NO><PAGE_SIZE></PAGE_SIZE><SORT_BY></SORT_BY><DESC></DESC></UP_ORD_SEARCH_ONLINEORDERAGENCYPROFILE_INPUT>")

            objInputXml.DocumentElement.SelectSingleNode("AgencyName").InnerText = txtCompany.Text.Trim()
            objInputXml.DocumentElement.SelectSingleNode("DateFrom").InnerText = ConvertTextDate(txtDateFrom.Text.Trim())
            objInputXml.DocumentElement.SelectSingleNode("DateTo").InnerText = ConvertTextDate(txtDateTo.Text.Trim())


            'Start CODE for sorting and paging
            If Operation = PageOperation.Search Then

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
                    ViewState("SortName") = "RequestDate"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "RequestDate" '"LOCATION_CODE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
                End If

                If ViewState("Desc") Is Nothing Then
                    ViewState("Desc") = "FALSE"
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
                Else
                    objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
                End If
            End If
            'End Code for paging and sorting

            objOutputXml = objProfile.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                If ds.Tables("DETAILS").Rows.Count <> 0 Then
                    grdOrder.DataSource = ds.Tables("DETAILS")
                    grdOrder.DataBind()

                    'Code Added For Paging And Sorting In case Of Delete The Record
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
                    hdRecordOnCurrentPage.Value = ds.Tables("DETAILS").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Select Case ViewState("SortName")
                        Case "AgencyName"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    grdOrder.HeaderRow.Cells(0).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdOrder.HeaderRow.Cells(0).Controls.Add(imgDown)
                            End Select
                        Case "RequestDate"
                            Select Case ViewState("Desc")
                                Case "FALSE"
                                    grdOrder.HeaderRow.Cells(1).Controls.Add(imgUp)
                                Case "TRUE"
                                    grdOrder.HeaderRow.Cells(1).Controls.Add(imgDown)
                            End Select

                    End Select
                    '  Added Code To Show Image'

                    ' End of Code Added For Paging And Sorting In case Of Delete The Record
                Else
                    grdOrder.DataSource = Nothing
                    grdOrder.DataBind()
                    txtTotalRecordCount.Text = "0"
                    hdRecordOnCurrentPage.Value = "0"
                    pnlPaging.Visible = False
                End If

            Else
                grdOrder.DataSource = Nothing
                grdOrder.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        Finally
            objInputXml = Nothing
            objOutputXml = Nothing
        End Try
    End Sub
#End Region

#Region "CheckSecurity()::This method is used for security check."
    Private Sub CheckSecurity()
        Try
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyOrder']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='AgencyOrder']").Attributes("Value").Value)
                End If
                If strBuilder(0) = "0" Then
                    btnSearch.Enabled = False
                    Response.Redirect("../NoRights.aspx", False)

                End If

                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If

            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub grdOrder_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrder.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdOrder_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdOrder.Sorting
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
            BindData(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            BindData(PageOperation.Export)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Enum PageOperation
        Search = 1
        Export = 2
    End Enum
    Sub Export(ByVal objOutputXml As XmlDocument)
        Dim objExport As New ExportExcel
        Dim strArray() As String = {"Company", "Request Date"}
        Dim intArray() As Integer = {1, 2}
        objExport.ExportDetails(objOutputXml, "DETAILS", intArray, strArray, ExportExcel.ExportFormat.Excel, "AgencyOrder.xls")
    End Sub
#End Region

#Region "ConvertTextDate(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd"
    Private Function ConvertTextDate(ByVal dt As String) As String '''''''to convert dd/mm/yyyy to yyyymmdd
        Dim lstrarrDate As Array
        Dim lstrdtyear As String
        Dim lstrdtmonth As String
        Dim lstrdtday As String
        Dim lstrDate As String
        If dt <> "" Then
            lstrarrDate = Split(dt, "/", -1, 1)
            lstrdtyear = lstrarrDate(2)
            lstrdtmonth = lstrarrDate(1)
            lstrdtday = lstrarrDate(0)
            If CType(lstrarrDate(1), String).Length = 1 Then
                lstrdtmonth = "0" + lstrarrDate(1)
            Else
                lstrdtmonth = lstrarrDate(1)
            End If
            If CType(lstrarrDate(0), String).Length = 1 Then
                lstrdtday = "0" + lstrarrDate(0)
            Else
                lstrdtday = lstrarrDate(0)
            End If

            lstrDate = lstrdtyear & lstrdtmonth & lstrdtday
            Return lstrDate
        Else
            Return dt
        End If
    End Function
#End Region

#Region "GeneratePDF()"
    Private Sub GeneratePDF(ByVal strID As String)
        Dim objxmlout As New XmlDocument
        Dim objds As New DataSet
        Dim xreader As XmlReader
        Dim objProfile As New AAMS.bizSales.bzOnlineOrder
        Dim objxml As New XmlDocument
        Try

            objRepDocument.Load(Server.MapPath("Reports/Application.rpt"))
            objxml.LoadXml("<UP_ORD_UPDATE_ONLINEORDERAGENCYPROFILE_INPUT><OrderID></OrderID></UP_ORD_UPDATE_ONLINEORDERAGENCYPROFILE_INPUT>")
            objxml.DocumentElement.SelectSingleNode("OrderID").InnerText = strID
            objxmlout = objProfile.View(objxml)
            xreader = New XmlNodeReader(objxmlout)
            objds.ReadXml(xreader)
            objRepDocument.SetDataSource(objds)

            'CRviewer.DisplayGroupTree = False
            'CRviewer.EnableViewState = True
            'CRviewer.ReportSource = objRepDocument
            'CRviewer.DataBind()

            'Code for Exporting in PDF
            'Code for Deletion of Existing File
            If File.Exists(Server.MapPath("Reports") + "/AgencyOrder.pdf") Then
                File.Delete(Server.MapPath("Reports") + "/AgencyOrder.pdf")
            End If
            'End 

            Dim DiskOpts As CrystalDecisions.Shared.DiskFileDestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions
            objRepDocument.ReportOptions.EnableSaveDataWithReport = True
            objRepDocument.SetDataSource(objds)
            objRepDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "")
            'objRepDocument.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            'objRepDocument.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile    '.NoDestination
            'objRepDocument.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Reports") + "/AgencyOrder.pdf")
            ''End of Code for Exporting in PDF
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
#End Region

#Region "grdOrder_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdOrder.RowCommand"
    Protected Sub grdOrder_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdOrder.RowCommand
        Try
            If e.CommandName = "ViewX" Then
                GeneratePDF(e.CommandArgument)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

End Class
