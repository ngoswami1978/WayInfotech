Imports System.Data
Imports System.Xml
Partial Class Inventory_INVSR_Equipment
    Inherits System.Web.UI.Page
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
    Dim objED As New EncyrptDeCyrpt
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("PageName") = "Inventory/INVSR_Equipment.aspx"
            objeAAMS.ExpirePageCache()
            ' This code is usedc for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
            End If
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Attributes("Value").Value)
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
                objeAAMS.SecurityCheck(31)
            End If
            If Not Page.IsPostBack Then
                objeAAMS.BindDropDown(drpEquipmentGroup, "EQUIPMENTGROUP", True, 3)
                btnNew.Attributes.Add("onclick", "return NewFunction();")
                '   btnReset.Attributes.Add("onclick", "return Reset();")
                'If Not Request.QueryString("Action") Is Nothing Then
                '    If Request.QueryString("Action").ToString().ToUpper() = "D" Then
                '        EquipmentDelete(Request.QueryString("Productid").ToString())
                '    End If
                'End If
               
            End If
            If (hdID.Value <> "") Then
                DeleteRecords()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub EquipmentSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
        Try
            objInputXml.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><SegExpected></SegExpected><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEQUIPMENT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText = Trim(txtEquipmentType.Text)
            objInputXml.DocumentElement.SelectSingleNode("DESCRIPTION").InnerText = (txtEquipmentDescription.Text)
            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EGROUP_CODE").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("CONFIG").InnerText = Trim(txtEquipmentConfiguration.Text)
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
                ViewState("SortName") = "EQUIPMENT_CODE"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "EQUIPMENT_CODE" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            '  End Code for paging and sorting

            'End With
            objInputXml.DocumentElement.SelectSingleNode("SegExpected").InnerText = txtSegExpected.Text

            objOutputXml = objbzEquipment.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdEquipment.DataSource = ds.Tables("EQUIPMENT")
                grdEquipment.DataBind()

                '***************************************************************************************
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
                txtRecordOnCurrentPage.Text = ds.Tables("EQUIPMENT").Rows.Count.ToString
                txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                ' @ Added Code To Show Image'
                Dim imgUp As New Image
                imgUp.ImageUrl = "~/Images/Sortup.gif"
                Dim imgDown As New Image
                imgDown.ImageUrl = "~/Images/Sortdown.gif"

                Select Case ViewState("SortName").ToString()
                    Case "EQUIPMENT_CODE"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(0).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(0).Controls.Add(imgDown)
                        End Select
                    Case "EGROUP_CODE"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(1).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(1).Controls.Add(imgDown)
                        End Select
                    Case "CONFIG"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(2).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(2).Controls.Add(imgDown)

                        End Select
                    Case "DESCRIPTION"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(3).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(3).Controls.Add(imgDown)
                        End Select
                    Case "SegExpected"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(4).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(4).Controls.Add(imgDown)
                        End Select

                    Case "UnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(5).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(5).Controls.Add(imgDown)
                        End Select

                    Case "NPUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(6).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(6).Controls.Add(imgDown)
                        End Select
                    Case "LKUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(7).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(7).Controls.Add(imgDown)
                        End Select
                    Case "BDUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(8).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(8).Controls.Add(imgDown)
                        End Select
                    Case "BTUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(9).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(9).Controls.Add(imgDown)
                        End Select
                    Case "MLUnitCost"
                        Select Case ViewState("Desc").ToString()
                            Case "FALSE"
                                grdEquipment.HeaderRow.Cells(10).Controls.Add(imgUp)
                            Case "TRUE"
                                grdEquipment.HeaderRow.Cells(10).Controls.Add(imgDown)
                        End Select


                End Select
                '' @ Added Code To Show Image'
                ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



            Else
                grdEquipment.DataSource = String.Empty
                grdEquipment.DataBind()
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
    '  Sub EquipmentDelete(ByVal strEquipID As String)
    Private Sub DeleteRecords()
        Try
            Dim objInputXml, objOutputXml As New XmlDocument
            Dim objtEquipment As New AAMS.bizMaster.bzEquipment
            objInputXml.LoadXml("<MS_DELETEEQUIPMENT_INPUT><Productid></Productid></MS_DELETEEQUIPMENT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("Productid").InnerXml = hdID.Value 'strEquipID.ToString().Split("|").GetValue(0)
            'Here Back end Method Call
            hdID.Value = ""
            objOutputXml = objtEquipment.Delete(objInputXml)
            EquipmentSearch()
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messDelete
                'txtEquipmentType.Text = Request.QueryString("Productid").ToString().Split("|").GetValue(1)
                'txtEquipmentType.Text = strEquipID
                'EquipmentSearch()
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdEquipment_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEquipment.RowDataBound
        Dim objSecurityXml As New XmlDocument
        Dim hdEquipmentId As New HiddenField
        Dim hdEquipmentCode As New HiddenField
        Dim lnkEdit As System.Web.UI.HtmlControls.HtmlAnchor
        'Dim lnkDelete As System.Web.UI.HtmlControls.HtmlAnchor
        Dim lnkDelete As LinkButton
        Dim str As New HiddenField
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If

            lnkEdit = e.Row.FindControl("linkEdit")
            lnkDelete = e.Row.FindControl("linkDelete")
            hdEquipmentId = e.Row.FindControl("hdEquipmentCode")
            hdEquipmentCode = e.Row.FindControl("hidCode")
            ' str = hdEquipmentId + "|" + hdEquipmentCode
            objSecurityXml.LoadXml(Session("Security"))

            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='EquipmentMaster']").Attributes("Value").Value)
                End If

                If strBuilder(3) = "0" Then
                    lnkDelete.Enabled = False
                Else
                    lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdEquipmentId.Value & ");")
                End If

                'If strBuilder(2) = "0" Then
                '    lnkEdit.Disabled = True
                'Else
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdEquipmentId.Value) & "');")
                'End If

            Else
                lnkDelete.Enabled = True
                lnkEdit.Disabled = False
                lnkEdit.Attributes.Add("onclick", "return EditFunction('" & objED.Encrypt(hdEquipmentId.Value) & "');")
                lnkDelete.Attributes.Add("onclick", "return DeleteFunction(" & hdEquipmentId.Value & ");")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            EquipmentSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub grdEquipment_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdEquipment.Sorting
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
            EquipmentSearch()

            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        grdEquipment.AllowSorting = False
        grdEquipment.HeaderStyle.ForeColor = Drawing.Color.Black
        EquipmentSearchExport()

        ' grdMIDT.Columns(grdMIDT.Columns.Count - 1).Visible = False
        If grdEquipment.Rows.Count > 0 Then
            ' PrepareGridViewForExport(grdMIDT)
            '   ExportGridView(grdMIDT, "MIDT.xls")
        End If
    End Sub
    Sub EquipmentSearchExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Dim objbzEquipment As New AAMS.bizMaster.bzEquipment
        Try
            objInputXml.LoadXml("<MS_SEARCHEQUIPMENT_INPUT><EGROUP_CODE /><EQUIPMENT_CODE /><DESCRIPTION /><CONFIG /><SegExpected></SegExpected><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></MS_SEARCHEQUIPMENT_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("EQUIPMENT_CODE").InnerText = Trim(txtEquipmentType.Text)
            objInputXml.DocumentElement.SelectSingleNode("DESCRIPTION").InnerText = Trim(txtEquipmentDescription.Text)
            If (drpEquipmentGroup.SelectedIndex <> 0) Then
                objInputXml.DocumentElement.SelectSingleNode("EGROUP_CODE").InnerText = drpEquipmentGroup.SelectedItem.Text
            End If
            objInputXml.DocumentElement.SelectSingleNode("CONFIG").InnerText = Trim(txtEquipmentConfiguration.Text)
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "NAME"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "NAME" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If

            objOutputXml = objbzEquipment.Search(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                ViewState("PrevSearching") = objInputXml.OuterXml

                grdEquipment.DataSource = ds.Tables("EQUIPMENT")
                grdEquipment.DataBind()
                ' 

                ' <EQUIPMENT 
                'EQUIPMENT_CODE = "A01"                 0
                'EGROUP_CODE = "CPU"                    1
                'CONFIG = "256 MB,2.4 GHZ,40 GB"        2     
                'DESCRIPTION = "A01 ASUS [CMB]"         3
                'MAINTAIN_BALANCE_BY = "0"              4
                'CPU_SPEED = "2"                        5
                'RAM = "256"                            6
                'VRAM = "0"                             7
                'HDD = "40"                             8
                'PRINTER_SPEED = "0"                    9
                'P_SPEED_MEASURE = "0"                  10
                'MONITOR_SIZE = "15"                    11
                'MODEM_SPEED = "0"                      12
                'LAN_CARD_TYPE = "0"                    13
                'LAN_CARD_SPEED = "10"                  14
                'WAN_CARD_TYPE = "0"                    15
                'PCI_SLOTS = "2"                        16
                'ISA_SLOTS = "0"                        17
                'REMARKS = ""                           18
                'MAINTAIN_BALANCE = "1"                 19
                'PRODUCTID = "175"                      20
                'CDR_SPEED = ""                         21
                'SegExpected = "100"                    22
                'UnitCost = "0.00"                      23
                'NPUnitCost = "0.00"                    24
                'LKUnitCost = "2900.00"                 25
                'BDUnitCost = "0.00"                    26
                'BTUnitCost = "0.00"                    27
                'MLUnitCost = "0.00"                    28
                '/> 
                '<PAGE TOTAL_ROWS="1" PAGE_COUNT="" /> 
                'Dim strArray() As String = {"Equipment Code", "Equipment Group", "Configuration", "Description", "Segment Expected", "Unit Cost"}
                'Dim intArray() As Integer = {0, 1, 2, 3, 4, 5}
                Dim strArray() As String = {"Equipment Code", "Equipment Group", "Configuration", "Description", "Segment Expected", "Unit Cost(IN)", "Unit Cost(NP)", "Unit Cost(LK)", "Unit Cost(BD)", "Unit Cost(BT)", "Unit Cost(ML)"}
                Dim intArray() As Integer = {0, 1, 2, 3, 22, 23, 24, 25, 26, 27, 28}
                Dim objExport As New ExportExcel
                objExport.ExportDetails(objOutputXml, "EQUIPMENT", intArray, strArray, ExportExcel.ExportFormat.Excel, "EquipmentExport.xls")
            Else
                grdEquipment.DataSource = String.Empty
                grdEquipment.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
                'pnlPaging.Visible = False
                ' txtTotalRecordCount.Text = "0"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try


    End Sub

    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Response.Redirect("INVSR_Equipment.aspx")
    End Sub
End Class
