Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data

Partial Class Setup_MSSR_ManageAirLine
    Inherits System.Web.UI.Page
    Dim dtbtl As DataTable
    Dim dcol As DataColumn
    Dim drowItem As DataRow
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Public strBuilder As New StringBuilder
    Dim imgUp As New Image
    Dim imgDown As New Image
    Dim objEn As New EncyrptDeCyrpt


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
            Session("PageName") = "Setup/MSSR_ManageAirLine.aspx"
            '  btnReset.Attributes.Add("onclick", "return AirlineReset();")
            btnNew.Attributes.Add("onclick", "return NewFunction();")
            lblError.Text = String.Empty

            'Code for Paging $ Sorting
            imgUp.ImageUrl = "~/Images/Sortup.gif"
            imgDown.ImageUrl = "~/Images/Sortdown.gif"

            If Not Page.IsPostBack Then
                'If Not Session("Action") Is Nothing Then
                'If Session("Action").ToString().Split("|").GetValue(0).ToString.ToUpper = "D" Then
                '    txtAirline.Text = Session("Action").ToString().Split("|").GetValue(2)
                '    txtAirlinecode.Text = Session("Action").ToString().Split("|").GetValue(3)
                '    chekOnlineCarrier.Checked = Session("Action").ToString().Split("|").GetValue(4)
                '    AirlineSearch()
                '    lblError.Text = objeAAMSMessage.messDelete
                '    Session("Action") = Nothing
                'End If
                'End If
            End If

            '***************************************************************************************
            'If Not Request.QueryString("Action") Is Nothing Then
            '    If Request.QueryString("Action").ToString().Split("|").GetValue(0) = "D" Then
            '        AirlineDelete(Request.QueryString("Action").ToString().Split("|").GetValue(1))
            '    End If
            'End If
            '***************************************************************************************

            'Code of Security Check
            'objeAAMS.ExpirePageCache()
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Attributes("Value").Value)
                End If

                If strBuilder(0) = "0" Then
                    Response.Redirect("../NoRights.aspx")
                    btnSearch.Enabled = False
                End If
                If strBuilder(1) = "0" Then
                    btnNew.Enabled = False
                End If
                If strBuilder(4) = "0" Then
                    btnExport.Enabled = False
                End If
                'Code End for security
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            '   Deleting records.
            If (hdAirlineID.Value <> "") Then
                AirlineDelete(hdAirlineID.Value)
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            AirlineSearch()
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub
    'Method for search Airlines
    Private Sub AirlineSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAirline As New AAMS.bizMaster.bzAirline

        objInputXml.LoadXml("<MS_SEARCHAIRLINE_INPUT><Airline_Code></Airline_Code><Name></Name><Online_Carrier></Online_Carrier> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAIRLINE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = Trim(txtAirlinecode.Text)
        objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = Trim(txtAirline.Text)
        objInputXml.DocumentElement.SelectSingleNode("Online_Carrier").InnerXml = IIf(chekOnlineCarrier.Checked = False, 0, 1)


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
            ViewState("SortName") = "Airline_Code"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Airline_Code" '"LOCATION_CODE"
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
        objOutputXml = objbzAirline.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            dbgrdManageAirline.Visible = True
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            

            lblError.Text = ""

            ViewState("PrevSearching") = objInputXml.OuterXml

            dbgrdManageAirline.DataSource = ds.Tables("AIRLINE")
            dbgrdManageAirline.DataBind()

            PagingCommon(objOutputXml)
            Dim intcol As Integer = GetSortColumnIndex(dbgrdManageAirline)
            If ViewState("Desc") = "FALSE" Then
                dbgrdManageAirline.HeaderRow.Cells(intcol).Controls.Add(imgUp)
            End If
            If ViewState("Desc") = "TRUE" Then
                dbgrdManageAirline.HeaderRow.Cells(intcol).Controls.Add(imgDown)
            End If


        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            dbgrdManageAirline.Visible = False
            pnlPaging.Visible = False
        End If
    End Sub

    ''Protected Sub dbgrdManageAirline_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dbgrdManageAirline.RowCommand
    ''    'Code for Edit Data
    ''    If e.CommandName = "EditX" Then
    ''        Response.Redirect("MSUP_ManageAirLine.aspx?Action=U&AirlineCode=" & e.CommandArgument)
    ''    End If
    ''    'Code for Delete Date
    ''    If e.CommandName = "DeleteX" Then
    ''        AirlineDelete(e.CommandArgument)
    ''        AirlineSearch()
    ''    End If
    ''End Sub
    Sub AirlineDelete(ByVal strAirline_CODE As String)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objbzAirline As New AAMS.bizMaster.bzAirline
        objInputXml.LoadXml("<MS_DELETEAIRLINE_INPUT><Airline_Code></Airline_Code></MS_DELETEAIRLINE_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = strAirline_CODE
        'Here Back end Method Call
        Try
            hdAirlineID.Value = ""
            objOutputXml = objbzAirline.Delete(objInputXml)
            AirlineSearch()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                '  Session("Action") = Request.QueryString("Action")
                ' Response.Redirect("MSSR_ManageAirLine.aspx", False)
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                If InStr(UCase(objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value), "T_G_AIRLINE_OFFICES", CompareMethod.Text) > 0 Then
                    lblError.Text = "Unable to delete this record , the same has reference to Airlines Office"
                End If
            End If
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Protected Sub dbgrdManageAirline_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dbgrdManageAirline.RowDataBound
        If e.Row.RowIndex < 0 Then
            Exit Sub
        End If

        Dim hdAirlineCode As HiddenField
        Dim linkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        Dim linkDelete As System.Web.UI.HtmlControls.HtmlAnchor

        linkEdit = e.Row.FindControl("linkEdit")
        linkDelete = e.Row.FindControl("linkDelete")
        hdAirlineCode = e.Row.FindControl("hdAirlineCode")
        Dim objSecurityXml As New XmlDocument
        If CType(e.Row.FindControl("lblOnlineCarrier"), Label).Text.Trim() = "1" Then
            CType(e.Row.FindControl("lblOnlineCarrier"), Label).Text = "Yes"
        Else
            CType(e.Row.FindControl("lblOnlineCarrier"), Label).Text = "No"
        End If

        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='Airline']").Attributes("Value").Value)
                If strBuilder(3) = "0" Then
                    linkDelete.Disabled = True
                Else
                    linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
                End If
                'If strBuilder(2) = "0" Then
                'linkEdit.Disabled = False
                'Else
                linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdAirlineCode.Value.Trim) & "');")
                'End If
            End If
        Else
            linkDelete.Attributes.Add("onclick", "return DeleteFunction('" & hdAirlineCode.Value & "');")
            linkEdit.Attributes.Add("onclick", "return EditFunction('" & objEn.Encrypt(hdAirlineCode.Value.Trim) & "');")
        End If
    End Sub

    
    Protected Sub dbgrdManageAirline_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles dbgrdManageAirline.Sorting
        Try
            Dim sortName As String = e.SortExpression
            SortCall(sortName)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
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
        AirlineSearch()
    End Sub
#End Region

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            AirlineSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            AirlineSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            AirlineSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
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
        txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value
    End Sub
    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("MSSR_ManageAirLine.aspx")
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzAirline As New AAMS.bizMaster.bzAirline
        Dim objExport As New ExportExcel
        Dim intArray(2) As Integer
        Dim strArray(2) As String

        objInputXml.LoadXml("<MS_SEARCHAIRLINE_INPUT><Airline_Code></Airline_Code><Name></Name><Online_Carrier></Online_Carrier> <PAGE_NO/><PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHAIRLINE_INPUT>")

        objInputXml.DocumentElement.SelectSingleNode("Airline_Code").InnerText = Trim(txtAirlinecode.Text)
        objInputXml.DocumentElement.SelectSingleNode("Name").InnerText = Trim(txtAirline.Text)
        objInputXml.DocumentElement.SelectSingleNode("Online_Carrier").InnerXml = IIf(chekOnlineCarrier.Checked = False, 0, 1)


        'Code for Paging and Sorting
        If ViewState("SortName") Is Nothing Then
            ViewState("SortName") = "Airline_Code"
            objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Airline_Code" '"LOCATION_CODE"
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
        objOutputXml = objbzAirline.Search(objInputXml)

        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            dbgrdManageAirline.Visible = True
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            lblError.Text = ""

            intArray(0) = 0
            strArray(0) = "Airline Code"

            intArray(1) = 1
            strArray(1) = "Airline Name"

            intArray(2) = 2
            strArray(2) = "Online Carrier"

            objExport.ExportDetails(objOutputXml, "AIRLINE", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportAIRLINE.xls")

        Else
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            dbgrdManageAirline.Visible = False
            pnlPaging.Visible = False
        End If
    End Sub
End Class
