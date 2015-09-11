
Partial Class Inventory_INVSR_Challan
    Inherits System.Web.UI.Page
    Dim objeAAMS As New eAAMS
    Dim objeAAMSMessage As New eAAMSMessage
    Public strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Dim strurl As String = Request.Url.ToString()
        Session("PageName") = strurl
        Try
            txtChallanDateFrom.Text = Request.Form("txtChallanDateFrom")
            txtChallanDateTo.Text = Request.Form("txtChallanDateTo")
           

            If hdDeleteId.Value <> "" Then
                DeleteRecords()
            End If
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Attributes("Value").Value)
                End If
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
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If

            If Not Page.IsPostBack Then
                ' objeAAMS.BindDropDown(ddlGodown, "GODOWN", True)
                FillGodown()
                txtChallanDateTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy")
                txtChallanDateFrom.Text = System.DateTime.Now.Subtract(New TimeSpan(7, 0, 0, 0)).ToString("dd/MM/yyyy")
                ' This code is used for checking session handler according to user login.
                

                
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Sub FillGodown()

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))


        ' This code binds godown on the basis of logged user
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            Try
                ddlGodown.Items.Clear()
                Dim objbzGodown As New AAMS.bizInventory.bzGodown
                Dim objInputXml As New XmlDocument
                Dim objOutputXml As New XmlDocument
                Dim objXmlReader As XmlNodeReader
                Dim ds As New DataSet
                objInputXml.LoadXml("<INV_LISTGODOWN_INPUT><AOFFICE/><CITYNAME/><REGION/></INV_LISTGODOWN_INPUT>")
                'If  ChallanRegionWiseGodown value is "1 or 2" then fill Region id else cityname
                If Session("Security") IsNot Nothing Then
                    Dim strRegionId As String = objeAAMS.SecurityRegionID(Session("Security"))
                    objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId

                    '    'Commented on 27 may 08
                    '    'If  ChallanRegionWiseGodown value isnot "1 or 2" then return -100
                    '    If strRegionId = "-100" Then
                    '        'if challan category is customer and agency is selected from popup than city is passed (but in this case we can pass aoffice too)
                    '        'else aoffice will be passed
                    '        If ddlChallanCategory.SelectedValue = "1" And txtCity.Text <> "" Then
                    '            objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                    '        Else
                    '            objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = objeAAMS.AOffice(Session("Security"))
                    '        End If

                    '    Else
                    '        objInputXml.DocumentElement.SelectSingleNode("REGION").InnerText = strRegionId
                    '    End If
                    'Else
                    '    objInputXml.DocumentElement.SelectSingleNode("CITYNAME").InnerText = txtCity.Text
                End If
                'End Comment
                objOutputXml = objbzGodown.ListGodownAoffice(objInputXml)
                'objOutputXml.LoadXml("<INV_AGENCYPENGINDORDER_OUTPUT><Errors Status='FALSE'><Error Code='' Description='' /> </Errors><AGENCYORDER ORDER_NUMBER='2008/2/366' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /><AGENCYORDER ORDER_NUMBER='2008/2/367' ORDER_TYPE_NAME='vista Add Term(1A  P)' ORDER_STATUS_NAME='Pending' APPROVAL_DATE='' RECEIVED_DATE='2/15/2008' APC='2' /></INV_AGENCYPENGINDORDER_OUTPUT>")
                If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                    objXmlReader = New XmlNodeReader(objOutputXml)
                    ds.ReadXml(objXmlReader)
                    ddlGodown.DataSource = ds.Tables("GODOWN").DefaultView
                    ddlGodown.DataTextField = "GODOWNNAME"
                    ddlGodown.DataValueField = "GODOWNID"
                    ddlGodown.DataBind()
                Else
                    If (objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value <> "No Record Found") Then
                        lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                    End If
                End If
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                ddlGodown.Items.Insert(0, New ListItem("--All--", ""))
            End Try
        Else
            objeAAMS.BindDropDown(ddlGodown, "GODOWN", True, 3)
        End If


    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Response.Redirect("INVUP_Challan.aspx?Action=I")
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchRecords(PageOperation.Search)

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_Challan.aspx")
    End Sub
    Sub SearchRecords(ByVal Operation As Integer)
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzChallan As New AAMS.bizInventory.bzChallan
        objInputXml.LoadXml("<INV_SEARCH_CHALLAN_INPUT><GodownID></GodownID><ChallanCategory></ChallanCategory><ChallanType></ChallanType>	<ChallanDateFrom></ChallanDateFrom>	<ChallanDateTo></ChallanDateTo><ChallanNumber /> <PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCH_CHALLAN_INPUT>")
        objInputXml.DocumentElement.SelectSingleNode("GodownID").InnerText = ddlGodown.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("ChallanCategory").InnerText = ddlChallanCategory.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("ChallanType").InnerText = ddlChallanType.SelectedValue
        objInputXml.DocumentElement.SelectSingleNode("ChallanDateFrom").InnerText = objeAAMS.GetDateFormat(txtChallanDateFrom.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("ChallanDateTo").InnerText = objeAAMS.GetDateFormat(txtChallanDateTo.Text, "dd/MM/yyyy", "yyyyMMdd", "/")
        objInputXml.DocumentElement.SelectSingleNode("ChallanNumber").InnerText = txtChallanNo.Text
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
                ViewState("SortName") = "ChallanNumber"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "ChallanNumber" '"LOCATION_CODE"
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

        ' Here Back end Method Call
        objOutputXml = objbzChallan.Search(objInputXml)
        If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
            ViewState("PrevSearching") = objInputXml.OuterXml
            If Operation = PageOperation.Export Then
                Export(objOutputXml)
                Exit Sub
            End If
            objXmlReader = New XmlNodeReader(objOutputXml)
            ds.ReadXml(objXmlReader)
            gvChallan.DataSource = ds.Tables("CHALLAN")
            gvChallan.DataBind()
            lblError.Text = ""
            'Code Added For Paging And Sorting In case Of Delete The Record
            If ds.Tables("CHALLAN").Rows.Count <> 0 Then
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
                hdRecordOnCurrentPage.Value = ds.Tables("CHALLAN").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"
                'PODescription="" OrderDate="29-Jul-03" CreationDate="07-Aug-03 14:34" SupplierName="amadeus old vendor" PRODUCTNAME="6P4C TELE JACK"
                Select Case ViewState("SortName").ToString()
                    Case "ChallanNumber"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "CreationDate"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select

                    Case "ChallanDate"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select
                    Case "ChallanCategory"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select

                    Case "ChallanType"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select

                    Case "SupplierName"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(5).Controls.Add(imgDown)

                        End Select
                    Case "AgencyName"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                    Case "OfficeID"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select

                    Case "GodownName"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "RGodownName"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                gvChallan.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                gvChallan.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select
                End Select
                '  Added Code To Show Image'
            Else
                gvChallan.DataSource = Nothing
                gvChallan.DataBind()
                txtTotalRecordCount.Text = "0"
                hdRecordOnCurrentPage.Value = "0"
                pnlPaging.Visible = False
            End If

        Else
            txtTotalRecordCount.Text = "0"
            hdRecordOnCurrentPage.Value = "0"
            pnlPaging.Visible = False
            gvChallan.DataSource = Nothing
            gvChallan.DataBind()
            lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
        End If
    End Sub

    Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objbzChallan As New AAMS.bizInventory.bzChallan
            objInputXml.LoadXml("<INV_DELETE_CHALLAN_INPUT>	<ChallanID></ChallanID></INV_DELETE_CHALLAN_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ChallanID").InnerText = hdDeleteId.Value
            hdDeleteId.Value = ""
            objOutputXml = objbzChallan.Delete(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                SearchRecords(PageOperation.Search)
                lblError.Text = objeAAMSMessage.messDelete
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallan.RowDataBound
        If e.Row.RowIndex < 0 Then
            Dim str1 As String = ddlChallanCategory.SelectedValue
            Select Case str1
                Case "1"
                    ' e.Row.Cells(5).Visible = False
                    ' e.Row.Cells(9).Visible = False
                Case "2"
                    'e.Row.Cells(6).Visible = False
                    'e.Row.Cells(7).Visible = False
                    ' e.Row.Cells(9).Visible = False
                Case "3"
                    ' e.Row.Cells(6).Visible = False
                    ' e.Row.Cells(7).Visible = False
                    ' e.Row.Cells(9).Visible = False
                Case "4"
                    'e.Row.Cells(5).Visible = False
                    ' e.Row.Cells(6).Visible = False
                    ' e.Row.Cells(7).Visible = False
            End Select
            'under testing this code is used in place of above
            Select Case str1
                Case "1"
                    e.Row.Cells(5).CssClass = "displayNone"
                    e.Row.Cells(9).CssClass = "displayNone"
                Case "2"
                    e.Row.Cells(6).CssClass = "displayNone"
                    e.Row.Cells(7).CssClass = "displayNone"
                    e.Row.Cells(9).CssClass = "displayNone"
                Case "3"
                    e.Row.Cells(6).CssClass = "displayNone"
                    e.Row.Cells(7).CssClass = "displayNone"
                    e.Row.Cells(9).CssClass = "displayNone"
                Case "4"
                    e.Row.Cells(5).CssClass = "displayNone"
                    e.Row.Cells(6).CssClass = "displayNone"
                    e.Row.Cells(7).CssClass = "displayNone"
            End Select

            Exit Sub
        End If

        Dim hdChallanID As HiddenField
        Dim btnEdit As LinkButton
        Dim btnDelete As LinkButton
        btnEdit = CType(e.Row.FindControl("lnkEdit"), LinkButton)
        btnDelete = CType(e.Row.FindControl("lnkDelete"), LinkButton)
        hdChallanID = CType(e.Row.FindControl("hdChallanID"), HiddenField)

        Dim objSecurityXml As New XmlDocument
        objSecurityXml.LoadXml(Session("Security"))
        If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
            If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Count <> 0 Then
                strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='MANAGE CHALLAN']").Attributes("Value").Value)
            End If
            If strBuilder(3) = "0" Then
                btnDelete.Enabled = False
            Else
                btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdChallanID.Value + "');")
            End If
            'If strBuilder(2) = "0" Then
            '    btnEdit.Enabled = False
            'Else
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdChallanID.Value) + "');")
            'End If
        Else
        btnDelete.Attributes.Add("OnClick", "javascript:return DeleteFunction('" + hdChallanID.Value + "');")
            btnEdit.Attributes.Add("OnClick", "javascript:return EditFunction('" + objED.Encrypt(hdChallanID.Value) + "');")
        End If
        '<asp:ListItem Value="1">Customer</asp:ListItem>
        '                                       <asp:ListItem Value="2">Purchase Order</asp:ListItem>
        '                                       <asp:ListItem Value="3">Replacement</asp:ListItem>      
        '                                       <asp:ListItem Value="4">Stock Transfer</asp:ListItem>     



        Dim str As String = ddlChallanCategory.SelectedValue
        Select Case str
            Case "1"
                e.Row.Cells(5).CssClass = "displayNone"
                e.Row.Cells(9).CssClass = "displayNone"
            Case "2"
                e.Row.Cells(6).CssClass = "displayNone"
                e.Row.Cells(7).CssClass = "displayNone"
                e.Row.Cells(9).CssClass = "displayNone"
            Case "3"
                e.Row.Cells(6).CssClass = "displayNone"
                e.Row.Cells(7).CssClass = "displayNone"
                e.Row.Cells(9).CssClass = "displayNone"
            Case "4"
                e.Row.Cells(5).CssClass = "displayNone"
                e.Row.Cells(6).CssClass = "displayNone"
                e.Row.Cells(7).CssClass = "displayNone"
        End Select


    End Sub

#Region "Code for Paging And sorting."
    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub



    Protected Sub gvChallan_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvChallan.Sorted
        Try

        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallan_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallan.Sorting
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
            SearchRecords(PageOperation.Search)
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            SearchRecords(PageOperation.Export)
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
       

        Dim str As String = ddlChallanCategory.SelectedValue
        Select Case str
            Case "1"
                '                 <CHALLAN ChallanID="41582" ChallanNumber="DEL/1/2008/01085" CreationDate="08-May-08 23:20" 
                'ChallanDate="08-May-08" ChallanCategory="Customer" ChallanType="Issue" SupplierName="" '
                'AgencyName="Kiran Travels" OfficeID="DELVS33R9" GodownName="DELHI GODOWN" RGodownName="" 
                'LCODE="27147" /> 

                Dim strArray() As String = {"Challan No.", "Creation Date", "Date", "Category", "Type", "Agency Name", "Office Id", "From Godown"}
                Dim intArray() As Integer = {1, 2, 3, 4, 5, 7, 8, 9}
                objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCHALLAN.xls")
            Case "2"
                Dim strArray() As String = {"Challan No.", "Creation Date", "Date", "Category", "Type", "Supplier Name", "From Godown"}
                Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 9}
                objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCHALLAN.xls")
            Case "3"
                Dim strArray() As String = {"Challan No.", "Creation Date", "Date", "Category", "Type", "Supplier Name", "From Godown"}
                Dim intArray() As Integer = {1, 2, 3, 4, 5, 6, 9}
                objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCHALLAN.xls")
            Case "4"
                Dim strArray() As String = {"Challan No.", "Creation Date", "Date", "Category", "Type", "From Godown", "To Godown"}
                Dim intArray() As Integer = {1, 2, 3, 4, 5, 9, 10}
                objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportCHALLAN.xls")
        End Select


    End Sub
End Class
