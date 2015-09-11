Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class Setup_MSSR_QUALITYORDERSEARCH
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt
#Region "Code for Filter "
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#End Region
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Sub StyleSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objStyle As New WAY.bizMaster.bzStyle

        objInputXml.LoadXml("<MS_SEARCH_QUALITY_ORDER_INPUT><ORDERNUMBER /><LOGGEDBY /><ORDER_DATE_FROM /><ORDER_DATE_TO /><W_StyleId /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCH_QUALITY_ORDER_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText = Trim(txtOrderNo.Text)
        objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = Trim(drpLoggedBy.SelectedValue.ToString)
        objInputXml.DocumentElement.SelectSingleNode("ORDER_DATE_FROM").InnerText = objeAAMS.GetDateFormat(txtOrderDateFrom.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_DATE_TO").InnerText = objeAAMS.GetDateFormat(txtOrderDateTo.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("W_StyleId").InnerText = Trim(drpQuality.SelectedValue.ToString)

        'Code for Paging and Sorting
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
            ViewState("SortName") = "StyleName"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "StyleName"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If
        'End of Paging and Sorting 

        'Here Back end Method Call
        objOutputXml = objStyle.SearchOrderBooking(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            If hdDelete.Value.Trim = "" Then
                lblError.Text = ""
            End If

            ViewState("PrevSearching") = objInputXml.OuterXml

            dbgrdManageStyle.DataSource = ds.Tables("STYLE")
            dbgrdManageStyle.DataBind()

            txtRecordOnCurrentPage.Text = dbgrdManageStyle.Rows.Count.ToString

            PagingCommon(objOutputXml)

            Dim intcol As Integer = GetSortColumnIndex(dbgrdManageStyle)
            If ViewState("Desc") = "FALSE" Then
                dbgrdManageStyle.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                dbgrdManageStyle.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If

        Else
            dbgrdManageStyle.DataSource = Nothing
            dbgrdManageStyle.DataBind()
            txtTotalRecordCount.Text = "0"
            pnlPaging.Visible = False
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub
#Region "Code for paging"
    Private Sub PagingCommon(ByVal objOutputXml As XmlDocument)
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
            lnkNext.Visible = False
            lnkPrev.Visible = False
        Else
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub
#End Region
#Region "Code for Sort Column"
    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function
#End Region

#Region "Code for Sorting"
    Protected Sub dbgrdManageAirLineOffice_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdManageStyle.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Sort Function"
    Sub SortCall(ByVal SortName As String)

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
        StyleSearch()
    End Sub
#End Region

#Region "Code for Next and previous"
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            StyleSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = Request.Url.ToString()
            objeAAMS.ExpirePageCache()

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            'Code of Security Check
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty
            If Not Page.IsPostBack Then
                BindDropDown(drpQuality, "Quality", True, 1)
                BindDropDown(drpLoggedBy, "Employee", True, 1)
            End If

            Dim objSecurityXml As New XmlDocument
            If Session("Security") IsNot Nothing Then
                objSecurityXml.LoadXml(Session("Security"))
            End If

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Attributes("Value").Value)
                End If

                If strBuilder.Length = 0 Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False

                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If

                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            '   Deleting records.
            If (hdDelete.Value <> "") Then

                StyleDelete(hdDelete.Value)
                Dim CurrentPage As Integer = CInt(IIf(ddlPageNumber.SelectedValue = "", "1", ddlPageNumber.SelectedValue))
                If txtRecordOnCurrentPage.Text = "1" Then
                    If CurrentPage - 1 > 0 Then
                        ddlPageNumber.SelectedValue = (CurrentPage - 1).ToString
                    Else
                        ddlPageNumber.SelectedValue = "1"
                    End If
                End If
                StyleSearch()
                hdDelete.Value = ""
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Public Sub BindDropDown(ByVal drpDownList As DropDownList, ByVal strType As String, ByVal bolSelect As Boolean, Optional ByVal SelectOption As Integer = 1)
        Dim objOutputXml As XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim objXml As New XmlDocument
        Dim strInput As String = "<UP_LIST_STYLE_INPUT><TYPE></TYPE></UP_LIST_STYLE_INPUT>"

        '2- STYLENAME /QUALITY , 
        '3- DESIGNNO, 
        '4- SHADENO
        objXml.LoadXml(strInput)

        Dim ds As New DataSet
        Select Case strType
            Case "Quality"
                Dim Objbz As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "2"
                objOutputXml = Objbz.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "Design"
                Dim Objbz As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "3"
                objOutputXml = Objbz.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)

                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "ShadeNo"
                Dim Objbz As New WAY.bizMaster.bzStyle
                objOutputXml = New XmlDocument
                objXml.DocumentElement.SelectSingleNode("TYPE").InnerText = "4"
                objOutputXml = Objbz.List(objXml)
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("STYLE")
                    drpDownList.DataTextField = "VALUE"
                    drpDownList.DataValueField = "ID"
                    drpDownList.DataBind()
                End If

            Case "Employee"
                Dim Objbz As New WAY.bizMaster.bzEmployee
                objOutputXml = New XmlDocument
                objOutputXml = Objbz.List()
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    drpDownList.DataSource = ds.Tables("EMPLOYEE")
                    drpDownList.DataTextField = "EMPLOYEENAME"
                    drpDownList.DataValueField = "EMPLOYEEID"
                    drpDownList.DataBind()
                End If
        End Select
        If SelectOption = 1 Then
            drpDownList.Items.Insert(0, New ListItem("--Select One--", "")) ' In Search/New/Edit Form For Mandatory Field
        End If

    End Sub

    Sub StyleDelete(ByVal strW_StyleId As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzStyle As New WAY.bizMaster.bzStyle
        objInputXml.LoadXml("<INV_DELETE_W_STYLEORDER_INPUT><W_StyleOrderID></W_StyleOrderID></INV_DELETE_W_STYLEORDER_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("W_StyleOrderID").InnerText = strW_StyleId

        objOutputXml = objbzStyle.OrderDELETE(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            lblError.Text = objeAAMSMessage.messDelete
        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

#Region "Code for Grid Row Bound"
    Protected Sub dbgrdManageAirLineOffice_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdManageStyle.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If
        Dim i As Integer
        Dim hdW_orderId As HiddenField

        Dim linkEdit As LinkButton
        Dim linkDelete As LinkButton

        Try
            linkEdit = e.Row.FindControl("lnkEdit")
            linkDelete = e.Row.FindControl("lnkDelete")
            hdW_orderId = e.Row.FindControl("hdW_StyleOrderID")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Order Booking']").Attributes("Value").Value)
                End If
                If strBuilder(3) = "0" Then
                    linkDelete.Enabled = False
                Else
                    If (Request.QueryString("PopUp")) Is Nothing Then
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdW_orderId.Value & "');")
                    Else
                        linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdW_orderId.Value & "');")
                    End If
                End If
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdW_orderId.Value.Trim) & "');")
            Else
                linkDelete.Enabled = True
                linkEdit.Enabled = True
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdW_orderId.Value.Trim) & "');")
                If (Request.QueryString("PopUp")) Is Nothing Then
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdW_orderId.Value & "');")
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction2('" & hdW_orderId.Value & "');")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "Code for Rreset"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect(Request.Url.OriginalString)
    End Sub
#End Region

#Region "Code for Export into Excel"
    Protected Sub btnExport_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objExport As New ExportExcel
        'intArray, strArray,
        Dim intArray(7) As Integer
        Dim strArray(7) As String
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzStyle As New WAY.bizMaster.bzStyle

        objInputXml.LoadXml("<MS_SEARCH_QUALITY_ORDER_INPUT><ORDERNUMBER /><LOGGEDBY /><ORDER_DATE_FROM /><ORDER_DATE_TO /><W_StyleId /><PAGE_NO /><PAGE_SIZE /><SORT_BY /><DESC /></MS_SEARCH_QUALITY_ORDER_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("ORDERNUMBER").InnerText = Trim(txtOrderNo.Text)
        objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = Trim(drpLoggedBy.SelectedValue.ToString)
        objInputXml.DocumentElement.SelectSingleNode("ORDER_DATE_FROM").InnerText = objeAAMS.GetDateFormat(txtOrderDateFrom.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("ORDER_DATE_TO").InnerText = objeAAMS.GetDateFormat(txtOrderDateTo.Text.Trim(), "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("W_StyleId").InnerText = Trim(drpQuality.SelectedValue.ToString)


        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "QUALITY"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "QUALITY"
        Else
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
        End If

        If ViewState("Desc") Is Nothing Then
            ViewState("Desc") = "FALSE"
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
        Else
            objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
        End If

        objOutputXml = objbzStyle.SearchOrderBooking(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)

            lblError.Text = ""
            dbgrdManageStyle.DataSource = ds.Tables("STYLE")
            dbgrdManageStyle.DataBind()
        Else
            dbgrdManageStyle.DataSource = Nothing
            dbgrdManageStyle.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If

        intArray(0) = 2
        strArray(0) = "ORDERNUMBER"

        intArray(1) = 3
        strArray(1) = "ORDERDATE"

        intArray(2) = 5
        strArray(2) = "LOGGERNAME"

        intArray(3) = 6
        strArray(3) = "QUALITY"

        intArray(4) = 7
        strArray(4) = "DESIGN"

        intArray(5) = 8
        strArray(5) = "SHADENO"

        intArray(6) = 9
        strArray(6) = "MRP"

        intArray(7) = 10
        strArray(7) = "QTY"

        objExport.ExportDetails(objOutputXml, "STYLE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportStyleOrder.xls")

    End Sub
#End Region


End Class
