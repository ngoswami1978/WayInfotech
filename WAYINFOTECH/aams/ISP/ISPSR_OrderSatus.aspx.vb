Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Partial Class ISP_ISPSR_OrderSatus
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim strBuilder As New StringBuilder
    Dim objeAAMS As New eAAMS
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Session("PageName") = "ISP/ISPSR_OrderSatus.aspx"
            objeAAMS.ExpirePageCache()

            btnNew.Attributes.Add("onclick", "return InsertISPOrderStatus();")

            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(1) = "0" Then
                        btnNew.Enabled = False
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If


            If Not Page.IsPostBack Then
                '*****************Delete Functionality
                If Request.QueryString("Action") IsNot Nothing Then
                    If Request.QueryString("Action").Split("|").GetValue(0).ToString.ToUpper = "D" Then
                        DeleteISPOredre_Status(Request.QueryString("Action").Split("|").GetValue(1).ToString.Trim())
                        ISPOrderStatusSearch(PageOperation.Search)
                        'lblError.Text = objeAAMSMessage.messDelete
                    End If
                End If
                '*****************End of Delete 
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'txtOrderStatusName.Text = ""
            'grdvISPordrStatus.DataSource = String.Empty
            'grdvISPordrStatus.DataBind()
            Response.Redirect(Request.Url.ToString())
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ISPOrderStatusSearch(PageOperation.Search)
        Catch ex As Exception

        End Try
       
    End Sub

   

    Sub ISPOrderStatusSearch(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbzISP As New AAMS.bizISP.bzISPOrderStatus
            objInputXml.LoadXml("<ISP_SEARCHORDERSTATUS_INPUT><StatusName/><PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></ISP_SEARCHORDERSTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("StatusName").InnerText = txtOrderStatusName.Text.Trim()

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
                    ViewState("SortName") = "StatusName"
                    objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "StatusName" '"LOCATION_CODE"
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

            'Here Back end Method Call
            objOutputXml = objbzISP.Search(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                ViewState("PrevSearching") = objInputXml.OuterXml
                If Operation = PageOperation.Export Then
                    Export(objOutputXml)
                    Exit Sub
                End If
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                grdvISPordrStatus.DataSource = ds.Tables("OrderStatus").DefaultView
                grdvISPordrStatus.DataBind()
                lblError.Text = ""

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
                hdRecordOnCurrentPage.Value = ds.Tables("OrderStatus").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
                SetImageForSorting(grdvISPordrStatus)
                ' 
                ' @ Added Code To Show Image'
                'Dim imgUp As New Image
                'imgUp.ImageUrl = "~/Images/Sortup.gif"
                'Dim imgDown As New Image
                'imgDown.ImageUrl = "~/Images/Sortdown.gif"

                'Select Case ViewState("SortName")
                '    Case "StatusName"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdvISPordrStatus.HeaderRow.Cells(0).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdvISPordrStatus.HeaderRow.Cells(0).Controls.Add(imgDown)
                '        End Select

                '    Case "ApprovedOrder"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdvISPordrStatus.HeaderRow.Cells(1).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdvISPordrStatus.HeaderRow.Cells(1).Controls.Add(imgDown)
                '        End Select

                '    Case "CancelOrder"
                '        Select Case ViewState("Desc")
                '            Case "FALSE"
                '                grdvISPordrStatus.HeaderRow.Cells(2).Controls.Add(imgUp)
                '            Case "TRUE"
                '                grdvISPordrStatus.HeaderRow.Cells(2).Controls.Add(imgDown)
                '        End Select
                'End Select
                '  Added Code To Show Image'

                ' End of Code Added For Paging And Sorting In case Of Delete The Record
            Else
                grdvISPordrStatus.DataSource = Nothing
                grdvISPordrStatus.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub


    Sub DeleteISPOredre_Status(ByVal ispID As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Try
            Dim objbzIsp As New AAMS.bizISP.bzISPOrderStatus
            objInputXml.LoadXml("<ISP_DELETEORDERSTATUS_INPUT><OrderStatusID/></ISP_DELETEORDERSTATUS_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("OrderStatusID").InnerText = ispID
            'Call a function
            objOutputXml = objbzIsp.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPordrStatus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdvISPordrStatus.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim lblISPStatus As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        linkEdit = e.Row.FindControl("linkEdit")
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        linkDelete = e.Row.FindControl("linkDelete")
        lblISPStatus = e.Row.FindControl("hdField")

        ' Dim chkbox As New CheckBox

        If (CType(e.Row.FindControl("lblCancel"), Label).Text.ToUpper() = "TRUE") Then
            CType(e.Row.FindControl("lblCancel"), Label).Text = "Yes"
        Else
            CType(e.Row.FindControl("lblCancel"), Label).Text = "No"
        End If

        If (CType(e.Row.FindControl("lblApproved"), Label).Text.ToUpper() = "TRUE") Then
            CType(e.Row.FindControl("lblApproved"), Label).Text = "Yes"
        Else
            CType(e.Row.FindControl("lblApproved"), Label).Text = "No"
        End If

        '@ Code Used For Encription/Decription
        Dim EnstrQueryStringForlblISPStatus As String
        EnstrQueryStringForlblISPStatus = objED.Encrypt(lblISPStatus.Value)
        '@ End of Code Used For Encription/Decription

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='IspOrderStatus']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                linkDelete.Disabled = True
            Else
                linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & lblISPStatus.Value & "');")
            End If
            'If strBuilder(2) = "0" Then
            '    linkEdit.Disabled = True
            'Else
            '    linkEdit.Attributes.Add("onclick", "return EditFunction('" & lblISPStatus.Value & "');")
            'End If
            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & lblISPStatus.Value & "');")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForlblISPStatus & "');")
        Else
            'linkEdit.Attributes.Add("onclick", "return EditFunction('" & lblISPStatus.Value & "');")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & EnstrQueryStringForlblISPStatus & "');")
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & lblISPStatus.Value & "');")
        End If
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

    End Sub


#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ISPOrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ISPOrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ISPOrderStatusSearch(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPordrStatus_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdvISPordrStatus.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdvISPordrStatus_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdvISPordrStatus.Sorting
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
            ISPOrderStatusSearch(PageOperation.Search)

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    'Code for Export
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ISPOrderStatusSearch(PageOperation.Export)
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
        Dim strArray() As String = {"Order Status", "Approved Order", "Cancel Order"}
        Dim intArray() As Integer = {1, 3, 2}

        objExport.ExportDetails(objOutputXml, "OrderStatus", intArray, strArray, ExportExcel.ExportFormat.Excel, "ISPOrderStatus.xls")
    End Sub
    'End Code For Export
#Region "Code for Filter "
    'Code Added by Abhishek
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    'Code Added by Abhishek
#End Region

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
