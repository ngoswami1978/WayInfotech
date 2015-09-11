'################################################################
'############   Page Name - Inventory_INVSR_ChallanStatus #######
'############   Date 25-March 2008    ###########################
'############   Developed By Abhishek  ##########################
'################################################################
#Region "Required Namespace Declaration"
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Data
#End Region
Partial Class Inventory_INVSR_ChallanStatus
    Inherits System.Web.UI.Page

#Region "Page level variable/Objects Declaration"
    Dim objeAAMSMessage As New eAAMSMessage
    Dim objeAAMS As New eAAMS
    Dim strBuilder As New StringBuilder
#End Region
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If ConfigurationManager.AppSettings("PAGE_FILTER").ToString = "1" Then
            If Response.ContentType = "text/html" Then
                Response.Filter = New TrimStream(Response.Filter)
            End If
        End If
    End Sub
#Region "Page_Load Event Declaration"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objInputXml As New XmlDocument
            Session("PageName") = Request.Url.ToString()
            ' This code is used for Expiration of Page From Cache
            objeAAMS.ExpirePageCache()
            ' This code is used for checking session handler according to user login.
            If Session("Security") Is Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "loginScript", objeAAMS.CheckSession())
                Exit Sub
            End If

            Page.MaintainScrollPositionOnPostBack = True
            btn_Save.Attributes.Add("onclick", "return validateGrid()")

            ' btnSearch.Attributes.Add("onclick", "return ChallanStatusMandatory();")
            btnReset.Attributes.Add("onclick", "return  ChallanStatusReset();")
            rdHardWareType.Attributes.Add("onclick", "return DisableOrderStatus();")
            rdChallanType.Attributes.Add("onclick", "return ClearItemFromDate();")
            'btnAddScrap.Attributes.Add("onclick", "return NewFunction();")
            txtAgencyName.Attributes.Add("onkeydown", "return ActDeAct();")

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    If strBuilder(0) = "0" Then
                        btnSearch.Enabled = False
                        Response.Redirect("~/NoRights.aspx")
                        Exit Sub
                    End If
                    If strBuilder(4) = "0" Then
                        btnExport.Enabled = False
                    End If
                    If strBuilder(2) = "0" Then
                        btn_Save.Enabled = False
                    End If
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
            End If
            If (Not IsPostBack) Then
                objeAAMS.BindDropDown(drpCity, "CITY", True, 3)
                objeAAMS.BindDropDown(drpCountry, "COUNTRY", True, 3)
                objeAAMS.BindDropDown(drpAOffice, "AOFFICE", True, 3)
                objeAAMS.BindDropDown(drpEquipCode, "EQUIPMENTCODE", True, 3)
                objeAAMS.BindDropDown(drpEquipGroup, "EQUIPMENTGROUP", True, 3)

                objeAAMS.BindDropDown(drpOrderStatus, "ORDERSTATUS", True, 3)
                objeAAMS.BindDropDown(drpLoggedBy, "EMPLOYEE", True, 3)
                objeAAMS.BindDropDown(drpChallanStatus, "CHALLANSTATUS", True, 3)
                BindOrderType("2")
            End If
            ' #######################################
            ' ########## This Code code is used for enable/disable 
            ' ########## the button according to rights
           
            ' #######################################
            ' ########## End of code used for enable/disable 
            ' ########## the button according to rights

          


        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region

#Region "btnSearch_Click"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            lblError.Text = ""
            ChallanStatusSearch()

            'Code for Date on Single Click

            'If rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "PI" Then
            '    If gvChallanStatusPI.Rows.Count > 0 Then
            '        Dim counter As Int16 = 0
            '        Dim imgIdTest As New HtmlImage
            '        Dim txtBox As TextBox
            '        For counter = 0 To gvChallanStatusPI.Rows.Count - 1
            '            imgIdTest = CType(gvChallanStatusPI.Rows(counter).FindControl("Img_ChallanDate"), HtmlImage)
            '            txtBox = CType(gvChallanStatusPI.Rows(counter).FindControl("txtChallanDate"), TextBox)
            '            '[ imgIdTest = CType(gvChallanStatusPI.Rows(counter).FindControl("Img_ChallanDate"), HtmlImage)
            '            imgIdTest.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgIdTest.ClientID.ToString() & "');")

            '        Next
            '    End If
            'End If


            'End of Code for Date

        Catch ex As Exception
            lblError.Text = ex.Message

        Finally
            drpDateSearchOn.Items.Clear()

            If rdChallanType.SelectedValue = "I" Then
                drpDateSearchOn.Items.Insert(0, New ListItem("", ""))
                drpDateSearchOn.Items.Insert(1, New ListItem("Installation Date", "1"))
                drpDateSearchOn.Items.Insert(2, New ListItem("Challan Date", "2"))
                drpDateSearchOn.Items.Insert(3, New ListItem("Login Date", "5"))
            End If

            If rdChallanType.SelectedValue = "D" Then
                drpDateSearchOn.Items.Insert(0, New ListItem("", ""))
                drpDateSearchOn.Items.Insert(1, New ListItem("Installation Date", "1"))
                drpDateSearchOn.Items.Insert(2, New ListItem("Challan Date", "2"))
                drpDateSearchOn.Items.Insert(3, New ListItem("Login Date", "5"))
                drpDateSearchOn.Items.Insert(4, New ListItem("DeInstallation Date", "3"))
            End If

            If rdChallanType.SelectedValue = "R" Then
                drpDateSearchOn.Items.Insert(0, New ListItem("", ""))
                drpDateSearchOn.Items.Insert(1, New ListItem("Installation Date", "1"))
                drpDateSearchOn.Items.Insert(2, New ListItem("Challan Date", "2"))
                drpDateSearchOn.Items.Insert(3, New ListItem("Login Date", "5"))
                drpDateSearchOn.Items.Insert(4, New ListItem("Replacement Date", "4"))
            End If


            If (Request("drpDateSearchOn") IsNot Nothing) Then
                If Request("drpDateSearchOn").ToString.Trim().Length > 0 Then
                    drpDateSearchOn.SelectedValue = Request("drpDateSearchOn")
                End If
            End If
            If (Request("txtDateFrom") IsNot Nothing) Then
                txtDateFrom.Text = Request("txtDateFrom")
            End If
            If (Request("txtDateTo") IsNot Nothing) Then
                txtDateTo.Text = Request("txtDateTo")
            End If

        End Try
    End Sub
#End Region
#Region "Method for search ChallanStatus"
    Private Sub ChallanStatusSearch()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbChallan As New AAMS.bizInventory.bzChallan

            objInputXml.LoadXml("<INV_SEARCHCHALLAN_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><EQUIPMENTCODE /><EQUIPMENTGROUP /><CHALLANSTATUS /><ONLINESTATUS /><ADDRESS /><ORDERTYPEID /><ORDERSTATUSID /><DATESEARCHON /><DATEFROM /><DATETO /><CHALLANTYPE /><LOGGEDBY /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHCHALLAN_INPUT>")

            If Request.Form("txtAgencyName") = "" And hdAgencyNameId.Value = "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text '
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAOffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAOffice.SelectedValue
            End If
            If drpEquipCode.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTCODE").InnerText = drpEquipCode.SelectedItem.Text
            End If
            If drpEquipGroup.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipGroup.SelectedItem.Text
            End If



            objInputXml.DocumentElement.SelectSingleNode("CHALLANSTATUS").InnerText = drpChallanStatus.SelectedValue
            ' objInputXml.DocumentElement.SelectSingleNode("ONLINESTATUS").InnerText = drpo
            objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = txtAddress.Text
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerText = drpOrderStatus.SelectedValue
         
            objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = drpLoggedBy.SelectedValue
            ' hdChallanType = objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText
            If (Request("drpDateSearchOn") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("DATESEARCHON").InnerText = Request("drpDateSearchOn")
            End If

            ' drpDateSearchOn.SelectedValue

            If (Request("txtDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateFrom.Text)
            End If
            If (Request("txtDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateTo.Text)
            End If
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
                ViewState("SortName") = "Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Name" '"LOCATION_CODE"
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

           
            'Here Back end Method Call
            objOutputXml = objbChallan.SearchChallanStatus(objInputXml)
            'Dim strdate As String
            'Dim nl3 As XmlNodeList
            'Dim xnode As XmlNode
            'For Each xnode In nl3
            '    If xnode.Attributes("CHALLANDATE").Value <> "" Then
            '        strdate = xnode.Attributes("CHALLANDATE").Value
            '        xnode.Attributes("CHALLANDATE").Value = Convert.ToDateTime(strdate).ToString("yyyyMMdd")
            '    End If
            'Next
            '  objOutputXml.LoadXml("<INV_SEARCHCHALLAN_OUTPUT><CHALLAN LCode='' ROWID='34' Name='' Address='fgf' OFFICEID='fg' DATE='' DATE_Deinst='' DATERepl='' CPUTYPE='' CPUNO='' MONTYPE='' MONNO='' OrderNumber='' ChallanStatus='1' CHALLANDATE='' OrderType='' AmadeusPC='' AgencyPC='' CStatusName='Received' LOGGEDBY='' LOGGEDDATE='' /> <Errors Status='FALSE'><Error Code='' Description=''/> </Errors></INV_SEARCHCHALLAN_OUTPUT>")
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)

                Session("ChallanStatusSearchXML") = objOutputXml.OuterXml


                ViewState("PrevSearching") = objInputXml.OuterXml

                'First Condition

                If objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PI" Then

                    gvChallanStatusPI.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusPI.DataBind()


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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusPI)
                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusPI.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusPI.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If

                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(4).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "MONTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(6).Controls.Add(imgDown)
                    '        End Select
                    '    Case "MonNo"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select

                    '    Case "KBDNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "MSENO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OrderNumber"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(10).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(10).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(11).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(11).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(12).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(12).Controls.Add(imgDown)
                    '        End Select

                    '    Case "OrderType"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(13).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(13).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AmadeusPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(14).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(14).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AgencyPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(15).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(15).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(16).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(16).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPI.HeaderRow.Cells(17).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPI.HeaderRow.Cells(17).Controls.Add(imgDown)
                    '        End Select
                    'End Select
                    '' @ Added Code To Show Image'
                    ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@




                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    gvChallanStatusPD.DataSource = Nothing
                    gvChallanStatusPD.DataBind()

                    gvChallanStatusPR.DataSource = Nothing
                    gvChallanStatusPR.DataBind()

                    gvChallanStatusMI.DataSource = Nothing
                    gvChallanStatusMI.DataBind()

                    gvChallanStatusMD.DataSource = Nothing
                    gvChallanStatusMD.DataBind()

                    gvChallanStatusMR.DataSource = Nothing
                    gvChallanStatusMR.DataBind()

                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PD" Then
                    gvChallanStatusPD.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusPD.DataBind()

                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"

                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusPD)

                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusPD.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusPD.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If


                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select
                    '    Case "DATE_Deinst"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(4).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(6).Controls.Add(imgDown)

                    '        End Select
                    '    Case "MONTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select
                    '    Case "MonNo"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "KBDNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select

                    '    Case "MSENO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(10).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(10).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OrderNumber"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(11).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(11).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(12).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(12).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(13).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(13).Controls.Add(imgDown)
                    '        End Select

                    '    Case "OrderType"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(14).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(14).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AmadeusPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(15).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(15).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AgencyPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(16).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(16).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(17).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(17).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPD.HeaderRow.Cells(18).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPD.HeaderRow.Cells(18).Controls.Add(imgDown)
                    '        End Select
                    'End Select


                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    gvChallanStatusPI.DataSource = Nothing
                    gvChallanStatusPI.DataBind()
                    gvChallanStatusPR.DataSource = Nothing
                    gvChallanStatusPR.DataBind()
                    gvChallanStatusMI.DataSource = Nothing
                    gvChallanStatusMI.DataBind()
                    gvChallanStatusMD.DataSource = Nothing
                    gvChallanStatusMD.DataBind()
                    gvChallanStatusMR.DataSource = Nothing
                    gvChallanStatusMR.DataBind()



                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PR" Then
                    gvChallanStatusPR.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusPR.DataBind()

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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"


                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusPR)

                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusPR.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusPR.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If

                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select
                    '    Case "DATERepl"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(4).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(6).Controls.Add(imgDown)

                    '        End Select
                    '    Case "MONTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select
                    '    Case "MonNo"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "KBDNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select

                    '    Case "MSENO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(10).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(10).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OrderNumber"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(11).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(11).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(12).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(12).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(13).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(13).Controls.Add(imgDown)
                    '        End Select

                    '    Case "OrderType"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(14).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(14).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AmadeusPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(15).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(15).Controls.Add(imgDown)
                    '        End Select
                    '    Case "AgencyPC"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(16).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(16).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(17).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(17).Controls.Add(imgDown)
                    '        End Select
                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusPR.HeaderRow.Cells(18).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusPR.HeaderRow.Cells(18).Controls.Add(imgDown)
                    '        End Select
                    'End Select


                    gvChallanStatusPI.DataSource = Nothing
                    gvChallanStatusPI.DataBind()
                    gvChallanStatusPD.DataSource = Nothing
                    gvChallanStatusPD.DataBind()
                    gvChallanStatusMI.DataSource = Nothing
                    gvChallanStatusMI.DataBind()
                    gvChallanStatusMD.DataSource = Nothing
                    gvChallanStatusMD.DataBind()
                    gvChallanStatusMR.DataSource = Nothing
                    gvChallanStatusMR.DataBind()




                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MI" Then
                    gvChallanStatusMI.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusMI.DataBind()
                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"


                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusMI)

                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusMI.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusMI.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If

                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select

                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(4).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(6).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMI.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMI.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select

                    'End Select


                    gvChallanStatusPI.DataSource = Nothing
                    gvChallanStatusPI.DataBind()

                    gvChallanStatusPD.DataSource = Nothing
                    gvChallanStatusPD.DataBind()

                    gvChallanStatusPR.DataSource = Nothing
                    gvChallanStatusPR.DataBind()

                    gvChallanStatusMD.DataSource = Nothing
                    gvChallanStatusMD.DataBind()

                    gvChallanStatusMR.DataSource = Nothing
                    gvChallanStatusMR.DataBind()



                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MD" Then
                    gvChallanStatusMD.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusMD.DataBind()

                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"


                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusMD)

                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusMD.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusMD.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If

                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATE_Deinst"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select

                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(4).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(6).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMD.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMD.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select

                    'End Select

                    gvChallanStatusPI.DataSource = Nothing
                    gvChallanStatusPI.DataBind()
                    gvChallanStatusPD.DataSource = Nothing
                    gvChallanStatusPD.DataBind()
                    gvChallanStatusPR.DataSource = Nothing
                    gvChallanStatusPR.DataBind()
                    gvChallanStatusMI.DataSource = Nothing
                    gvChallanStatusMI.DataBind()
                    gvChallanStatusMR.DataSource = Nothing
                    gvChallanStatusMR.DataBind()



                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MR" Then
                    gvChallanStatusMR.DataSource = ds.Tables("CHALLAN")
                    gvChallanStatusMR.DataBind()

                    '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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
                    txtRecordOnCurrentPage.Text = ds.Tables("CHALLAN").Rows.Count.ToString
                    txtTotalRecordCount.Text = objOutputXml.DocumentElement.SelectSingleNode("PAGE").Attributes("TOTAL_ROWS").Value

                    ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    ' @ Added Code To Show Image'
                    Dim imgUp As New Image
                    imgUp.ImageUrl = "~/Images/Sortup.gif"
                    Dim imgDown As New Image
                    imgDown.ImageUrl = "~/Images/Sortdown.gif"


                    Dim colIndex As Int16
                    colIndex = GetSortColumnIndex(gvChallanStatusMR)

                    If ViewState("Desc").ToString.ToUpper = "FALSE" Then
                        gvChallanStatusMR.HeaderRow.Cells(colIndex).Controls.Add(imgUp)
                    Else
                        gvChallanStatusMR.HeaderRow.Cells(colIndex).Controls.Add(imgDown)
                    End If


                    'Select Case ViewState("SortName")
                    '    Case "Name"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(0).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(0).Controls.Add(imgDown)
                    '        End Select
                    '    Case "Address"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(1).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(1).Controls.Add(imgDown)
                    '        End Select
                    '    Case "OFFICEID"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(2).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(2).Controls.Add(imgDown)

                    '        End Select
                    '    Case "DATERepl"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(3).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(3).Controls.Add(imgDown)
                    '        End Select

                    '    Case "CPUTYPE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(4).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(4).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CPUNO"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(5).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(5).Controls.Add(imgDown)

                    '        End Select
                    '    Case "CStatusName"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(6).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(6).Controls.Add(imgDown)
                    '        End Select
                    '    Case "CHALLANDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(7).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(7).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDBY"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(8).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(8).Controls.Add(imgDown)
                    '        End Select

                    '    Case "LOGGEDDATE"
                    '        Select Case ViewState("Desc")
                    '            Case "FALSE"
                    '                gvChallanStatusMR.HeaderRow.Cells(9).Controls.Add(imgUp)
                    '            Case "TRUE"
                    '                gvChallanStatusMR.HeaderRow.Cells(9).Controls.Add(imgDown)
                    '        End Select

                    'End Select


                    gvChallanStatusPI.DataSource = Nothing
                    gvChallanStatusPI.DataBind()
                    gvChallanStatusPD.DataSource = Nothing
                    gvChallanStatusPD.DataBind()
                    gvChallanStatusPR.DataSource = Nothing
                    gvChallanStatusPR.DataBind()
                    gvChallanStatusMI.DataSource = Nothing
                    gvChallanStatusMI.DataBind()
                    gvChallanStatusMD.DataSource = Nothing
                    gvChallanStatusMD.DataBind()
                End If
            Else
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                gvChallanStatusPI.DataSource = Nothing
                gvChallanStatusPI.DataBind()
                gvChallanStatusPD.DataSource = Nothing
                gvChallanStatusPD.DataBind()
                gvChallanStatusPR.DataSource = Nothing
                gvChallanStatusPR.DataBind()
                gvChallanStatusMI.DataSource = Nothing
                gvChallanStatusMI.DataBind()
                gvChallanStatusMD.DataSource = Nothing
                gvChallanStatusMD.DataBind()
                gvChallanStatusMR.DataSource = Nothing
                gvChallanStatusMR.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            'txtBdrSentBy.Text = hdEmployeeName.Value
            If Request("txtDateFrom") IsNot Nothing Then
                txtDateFrom.Text = Request("txtDateFrom")
            End If
            If Request("txtDateTo") IsNot Nothing Then
                txtDateTo.Text = Request("txtDateTo")
            End If


        End Try
    End Sub
#End Region


#Region " gvChallanStatusPI_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusPI_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusPI.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If


            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)

            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")

            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)
            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")
            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value


            'validateChallanStatus(strDrp,strDate)
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")

            'txtBox.Text = DataBinder.Eval(e.Row.DataItem, "CHALLANDATE")
            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus As LinkButton
            Dim hdLCode, hdChallanDate, hdPCDate, hdChallanType As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)

            '  txtBox.Text = Format(hdChallanDate.Value, "dd/MM/yyyy")
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If

            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If
            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " gvChallanStatusPD_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusPD_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusPD.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)


            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)


            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")

            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")
            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value
            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus As LinkButton
            Dim hdLCode, hdChallanDate, hdChallanType, hdPCDate As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If
            '  hdChallanDate.Value = Format(hdChallanDate.Value, "dd/mm/yyyy")
            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If

            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " gvChallanStatusPR_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusPR_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusPR.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)
            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)
            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")
            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value


            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")


            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus, LinkHistory As LinkButton
            Dim hdLCode, hdChallanDate, hdPCDate, hdChallanType As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If
            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
            LinkHistory = e.Row.FindControl("LinkHistory")
            LinkHistory.Attributes.Add("OnClick", "return HistoryFunctionPR('" + LinkHistory.CommandArgument + "');")


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If
            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")

           
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " gvChallanStatusMI_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusMI_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusMI.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)
            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)
            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")


            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")

            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value
            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus As LinkButton
            Dim hdLCode, hdChallanDate, hdPCDate, hdChallanType As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If
            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If
            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " gvChallanStatusMD_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusMD_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusMD.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)
            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)

            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")


            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")
            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value
            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus As LinkButton
            Dim hdLCode, hdChallanDate, hdPCDate, hdChallanType As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If
            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)

            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If
            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
#Region " gvChallanStatusMR_RowDataBound Event fired on every row creation in  gridview"
    Protected Sub gvChallanStatusMR_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvChallanStatusMR.RowDataBound
        Try
            If e.Row.RowIndex < 0 Then
                Exit Sub
            End If
            Dim imgId As New HtmlImage
            imgId = CType(e.Row.FindControl("Img_ChallanDate"), HtmlImage)
            Dim ddl As DropDownList
            Dim txtBox, txtChallanDate As New TextBox
            txtBox = CType(e.Row.FindControl("txtChallanDate"), TextBox)
            imgId.Attributes.Add("onmousedown", "SelectDate('" & txtBox.ClientID.ToString() & "','" & imgId.ClientID.ToString() & "');")
            ddl = CType(e.Row.FindControl("drpChallan"), DropDownList)

            txtBox.Attributes.Add("onblur", "return DateValidatin('" + txtBox.ClientID + "')")
            ddl.Attributes.Add("onchange", "return validateChallanStatus('" + ddl.ClientID + "','" + txtBox.ClientID + "')")


            'ddl.SelectedItem.Text = DataBinder.Eval(e.Row.DataItem, "CStatusName")
            ddl.SelectedValue = ddl.Items.FindByText(DataBinder.Eval(e.Row.DataItem, "CStatusName")).Value
            '#############################################################
            ' Code added For Selecting an Items
            Dim lnkModifyChallanStatus, LinkHistory As LinkButton
            Dim hdLCode, hdChallanDate, hdPCDate, hdChallanType As New HiddenField
            hdPCDate = CType(e.Row.FindControl("hdPCDate"), HiddenField)
            hdChallanDate = CType(e.Row.FindControl("hdChallanDate"), HiddenField)
            If hdChallanDate.Value <> "" Then
                txtBox.Text = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
                hdChallanDate.Value = DateTime.Parse(hdChallanDate.Value).ToString("dd/MM/yyyy")
            End If
            If hdPCDate.Value <> "" Then
                hdPCDate.Value = DateTime.Parse(hdPCDate.Value).ToString("dd/MM/yyyy")
            End If
            ' #############################
            ' Cahnage Format of date in integer format
            ' hdChallanDate.Value =
            ' #############################
            hdChallanType = CType(e.Row.FindControl("hdChallanType"), HiddenField)
            hdChallanType.Value = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            lnkModifyChallanStatus = e.Row.FindControl("lnkModifyChallanStatus")
            hdLCode = CType(e.Row.FindControl("hdLCode"), HiddenField)
            'lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            LinkHistory = e.Row.FindControl("LinkHistory")
            LinkHistory.Attributes.Add("OnClick", "return HistoryFunctionMR('" + LinkHistory.CommandArgument + "');")


            Dim objSecurityXml As New XmlDocument
            objSecurityXml.LoadXml(Session("Security"))
            If (objSecurityXml.DocumentElement.SelectSingleNode("Administrator").InnerText = "0") Then
                If objSecurityXml.DocumentElement.SelectNodes("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Count <> 0 Then
                    strBuilder = objeAAMS.SecurityCheck(objSecurityXml.DocumentElement.SelectSingleNode("SECURITY_OPTIONS/SECURITY_OPTION[@SecurityOptionSubName='CHALLAN STATUS']").Attributes("Value").Value)
                    '    If strBuilder(2) = "0" Then
                    '        lnkModifyChallanStatus.Enabled = False
                    '    End If
                    'Else
                    '    lnkModifyChallanStatus.Enabled = True
                    lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
                End If
            Else
                strBuilder = objeAAMS.SecurityCheck(31)
                lnkModifyChallanStatus.Enabled = True
                lnkModifyChallanStatus.Attributes.Add("OnClick", "return EditFunction('" + lnkModifyChallanStatus.CommandArgument + "','" + hdChallanType.Value + "','" + hdChallanDate.Value + "','" + hdPCDate.Value + "');")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
#End Region
    Sub BindOrderType(ByVal str As String)
        Try
            Dim objbzOrderType As New AAMS.bizTravelAgency.bzOrderType
            Dim objOutputXml As New XmlDocument
            Dim objInputXml As New XmlDocument
            objInputXml.LoadXml("<UP_LISTORDERTYPE_INPUT><ORDER_TYPE></ORDER_TYPE></UP_LISTORDERTYPE_INPUT>")
            objInputXml.DocumentElement.SelectSingleNode("ORDER_TYPE").InnerText = str
            Dim objNodeList As XmlNodeList
            objOutputXml = objbzOrderType.List(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objNodeList = objOutputXml.SelectNodes("UP_LISTORDERTYPE_OUTPUT/ORDER_TYPE")
                For Each objXmlNode As XmlNode In objNodeList
                    drpOrderType.Items.Add(New ListItem(objXmlNode.Attributes("ORDER_TYPE_NAME").InnerText, objXmlNode.Attributes("ORDERTYPEID").InnerText))
                Next
            End If
            drpOrderType.Items.Insert(0, New ListItem("--All--", ""))

        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub



   
    Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNext.Click
        Try
            If ddlPageNumber.SelectedValue <> (ddlPageNumber.Items.Count).ToString Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) + 1).ToString
            End If
            ChallanStatusSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPrev.Click
        Try
            If ddlPageNumber.SelectedValue <> "1" Then
                ddlPageNumber.SelectedValue = (CInt(ddlPageNumber.SelectedValue) - 1).ToString
            End If
            ChallanStatusSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Try
            ChallanStatusSearch()
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusPI_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusPI.Sorting
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
             ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusPD_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusPD.Sorting
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
            ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusPR_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusPR.Sorting
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
            ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusMI_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusMI.Sorting
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
            ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusMD_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusMD.Sorting
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
            ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvChallanStatusMR_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvChallanStatusMR.Sorting
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
            ChallanStatusSearch()
            'grdAgency.HeaderRow.Cells(0).Controls.Add("")
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        gvChallanStatusPI.AllowSorting = False
        gvChallanStatusPD.AllowSorting = False
        gvChallanStatusPR.AllowSorting = False
        gvChallanStatusMI.AllowSorting = False
        gvChallanStatusMD.AllowSorting = False
        gvChallanStatusMR.AllowSorting = False
        gvChallanStatusPI.HeaderStyle.ForeColor = Drawing.Color.Black
        gvChallanStatusPD.HeaderStyle.ForeColor = Drawing.Color.Black
        gvChallanStatusPR.HeaderStyle.ForeColor = Drawing.Color.Black
        gvChallanStatusMI.HeaderStyle.ForeColor = Drawing.Color.Black
        gvChallanStatusMD.HeaderStyle.ForeColor = Drawing.Color.Black
        gvChallanStatusMR.HeaderStyle.ForeColor = Drawing.Color.Black
        ChallanStatusExport()
    End Sub
    Private Sub ChallanStatusExport()
        Dim objInputXml, objOutputXml As New XmlDocument
        Dim objXmlReader As XmlNodeReader
        Dim ds As New DataSet
        Try
            Dim objbChallan As New AAMS.bizInventory.bzChallan

            objInputXml.LoadXml("<INV_SEARCHCHALLAN_INPUT><LCODE /><AGENCYNAME /><CITY /><COUNTRY /><AOFFICE /><EQUIPMENTCODE /><EQUIPMENTGROUP /><CHALLANSTATUS /><ONLINESTATUS /><ADDRESS /><ORDERTYPEID /><ORDERSTATUSID /><DATESEARCHON /><DATEFROM /><DATETO /><CHALLANTYPE /><LOGGEDBY /><PAGE_NO/> <PAGE_SIZE/><SORT_BY/><DESC/></INV_SEARCHCHALLAN_INPUT>")

            If Request.Form("txtAgencyName") = "" And hdAgencyNameId.Value = "" Then
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = ""
            Else
                objInputXml.DocumentElement.SelectSingleNode("LCODE").InnerText = hdAgencyNameId.Value.Trim()
            End If

            objInputXml.DocumentElement.SelectSingleNode("AGENCYNAME").InnerText = txtAgencyName.Text
            If drpCity.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("CITY").InnerText = drpCity.SelectedItem.Text '
            End If
            If drpCountry.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("COUNTRY").InnerText = drpCountry.SelectedItem.Text
            End If
            If drpAOffice.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("AOFFICE").InnerText = drpAOffice.SelectedValue
            End If
            If drpEquipCode.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTCODE").InnerText = drpEquipCode.SelectedItem.Text
            End If
            If drpEquipGroup.SelectedIndex <> 0 Then
                objInputXml.DocumentElement.SelectSingleNode("EQUIPMENTGROUP").InnerText = drpEquipGroup.SelectedItem.Text
            End If



            objInputXml.DocumentElement.SelectSingleNode("CHALLANSTATUS").InnerText = drpChallanStatus.SelectedValue
            ' objInputXml.DocumentElement.SelectSingleNode("ONLINESTATUS").InnerText = drpo
            objInputXml.DocumentElement.SelectSingleNode("ADDRESS").InnerText = txtAddress.Text
            objInputXml.DocumentElement.SelectSingleNode("ORDERTYPEID").InnerText = drpOrderType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("ORDERSTATUSID").InnerText = drpOrderStatus.SelectedValue

            objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = rdHardWareType.SelectedValue + rdChallanType.SelectedValue
            objInputXml.DocumentElement.SelectSingleNode("LOGGEDBY").InnerText = drpLoggedBy.SelectedValue
            ' hdChallanType = objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText

            If Request("drpDateSearchOn") IsNot Nothing Then
                objInputXml.DocumentElement.SelectSingleNode("DATESEARCHON").InnerText = Request("drpDateSearchOn")

            End If

            If (Request("txtDateFrom") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(Request("txtDateFrom").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("DATEFROM").InnerText = objeAAMS.ConvertTextDate(txtDateFrom.Text)
            End If
            If (Request("txtDateTo") IsNot Nothing) Then
                objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(Request("txtDateTo").ToString())
            Else
                objInputXml.DocumentElement.SelectSingleNode("DATETO").InnerText = objeAAMS.ConvertTextDate(txtDateTo.Text)
            End If
            'objInputXml.DocumentElement.SelectSingleNode("PAGE_SIZE").InnerText = ConfigurationManager.AppSettings("PAGE_SIZE").ToString
            If ViewState("SortName") Is Nothing Then
                ViewState("SortName") = "Name"
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = "Name" '"LOCATION_CODE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("SORT_BY").InnerText = ViewState("SortName")
            End If

            If ViewState("Desc") Is Nothing Then
                ViewState("Desc") = "FALSE"
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = "FALSE"
            Else
                objInputXml.DocumentElement.SelectSingleNode("DESC").InnerText = ViewState("Desc")
            End If
            objOutputXml = objbChallan.SearchChallanStatus(objInputXml)
            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                objXmlReader = New XmlNodeReader(objOutputXml)
                ds.ReadXml(objXmlReader)
                Dim objExport As New ExportExcel
                If objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PI" Then
                    Dim strArray() As String = {"Name", "Address", "OfiiceId", "Date", "CPU Type", "CPU Number", "Monitor Type", "Monitor Number", "KeyBoard Number", "Mouse Number", "Order Number", "CStatus Name", "Challan Date", "Order Type", "Amadeus PC", "Agency PC", "Logged BY", "Logged DATE"}
                    Dim intArray() As Integer = {2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 20, 16, 17, 18, 19, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusPCInstallation.xls")
                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PD" Then
                    Dim strArray() As String = {"Name", "Address", "OFFICEID", "DATE", "Date DeInstall", "Cpu Type", "CPU NO", "Monitor Type", "Monitor Number", "KeyBoard Number", "Mouse Number", "Order Number", "CStatusName", "Challan Date", "Order Type", "Amadeus PC", "Agency PC", "Logged BY", "Logged DATE"}
                    Dim intArray() As Integer = {2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 20, 16, 17, 18, 19, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusPCDeInstallation.xls")
                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "PR" Then
                    Dim strArray() As String = {"Name", "Address", "OFFICEID", "DATE", "DATERepl", "Cpu Type", "CPU NO", "Monitor Type", "Monitor Number", "KeyBoard Number", "Mouse Number", "OrderNumber", "CStatusName", "Challan Date", "OrderType", "Amadeus PC", "Agency PC", "Logged BY", "Logged DATE"}
                    Dim intArray() As Integer = {2, 3, 4, 5, 7, 8, 9, 10, 11, 12, 13, 14, 20, 16, 17, 18, 19, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusPCReplacement.xls")
                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MI" Then
                    Dim strArray() As String = {"Name", "Address", "OFFICEID", "DATE", "Equip Type", "Equip No", "Challan Status", "Challan Date", "Logged By", "Logged Date"}
                    Dim intArray() As Integer = {2, 3, 4, 5, 8, 9, 15, 16, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusMiscInstallation.xls")
                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MD" Then
                    Dim strArray() As String = {"Name", "Address", "OFFICEID", "Date DeInstall", "Equip Type", "Equip Number", "Challan Status", "Challan Date", "Logged By", "Logged Date"}
                    Dim intArray() As Integer = {2, 3, 4, 6, 8, 9, 15, 16, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusMiscDeInstallation.xls")
                ElseIf objInputXml.DocumentElement.SelectSingleNode("CHALLANTYPE").InnerText = "MR" Then
                    Dim strArray() As String = {"Name", "Address", "OFFICEID", "Date Repl.", "Equip Type", "Equip Number", "Challan Status", "Challan Date", "Logged By", "Logged Date"}
                    Dim intArray() As Integer = {2, 3, 4, 7, 8, 9, 15, 16, 21, 22}
                    objExport.ExportDetails(objOutputXml, "CHALLAN", intArray, strArray, ExportExcel.ExportFormat.Excel, "exportChallanStatusMiscReplacement.xls")
                End If
            Else
                pnlPaging.Visible = False
                txtTotalRecordCount.Text = "0"
                gvChallanStatusPI.DataSource = Nothing
                gvChallanStatusPI.DataBind()
                gvChallanStatusPD.DataSource = Nothing
                gvChallanStatusPD.DataBind()
                gvChallanStatusPR.DataSource = Nothing
                gvChallanStatusPR.DataBind()
                gvChallanStatusMI.DataSource = Nothing
                gvChallanStatusMI.DataBind()
                gvChallanStatusMD.DataSource = Nothing
                gvChallanStatusMD.DataSource = Nothing
                gvChallanStatusMR.DataBind()
                gvChallanStatusMR.DataBind()
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try

    End Sub

    Protected Sub btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Try
            Dim objInputXml, objOutputXml, objTempXmlDoc As New XmlDocument
            Dim objChallanStatus As New AAMS.bizInventory.bzChallan

            objTempXmlDoc.LoadXml(Session("ChallanStatusSearchXML"))

           

            'objInputXml.LoadXml("<INV_UPDATECHALLANSTATUS_INPUT><CHALLAN  ACTION='' ROWID='' CHALLANDATE='' ChallanStatus='' CHALLANTYPE='' PCDATE='' /></INV_UPDATECHALLANSTATUS_INPUT>")
            'objXmlNode = objInputXml.DocumentElement.SelectSingleNode("CHALLAN")
            'objXmlNodeClone = objXmlNode.CloneNode(True)

            If rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "PI" Then

                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusPI, objTempXmlDoc)
                End If

                '    For Rowno = 0 To gvChallanStatusPI.Rows.Count - 1
                '        With objXmlNodeClone
                '            .Attributes("ACTION").Value = ""
                '            .Attributes("ROWID").Value = CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '            'If CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '            '    ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '            '    .Attributes("CHALLANDATE").Value = CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '            'Else

                '            Dim strChallanDate As String = CType(gvChallanStatusPI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim

                '            If (strChallanDate <> "") Then
                '                .Attributes("CHALLANDATE").Value = strChallanDate.Split("/")(2) + strChallanDate.Split("/")(1) + strChallanDate.Split("/")(0)
                '            End If
                '            ' End If
                '            Dim strChallanStatus As String = CType(gvChallanStatusPI.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue.Trim
                '            .Attributes("ChallanStatus").Value = strChallanStatus
                '            .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim

                '            If CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '                .Attributes("PCDATE").Value = CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '            Else
                '                .Attributes("PCDATE").Value = ""
                '            End If
                '        End With
                '        objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '        objXmlNodeClone = objXmlNode.CloneNode(True)
                '    Next
                '    objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))


                '    Dim nl1, nl2 As XmlNodeList
                '    nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                '    nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")



                '    For Each xnode1 In nl2
                '        For Each xnode2 In nl1
                '            Dim finaldate As String = ""
                '            Dim strDate As String = xnode1.Attributes("CHALLANDATE").Value.Trim()
                '            If strDate.Trim.Length <> 0 Then
                '                finaldate = Year(Convert.ToDateTime(strDate).ToShortDateString).ToString + Month(Convert.ToDateTime(strDate).ToShortDateString).ToString("00") + Day(Convert.ToDateTime(strDate).ToShortDateString).ToString("00")
                '            End If

                '            If ((xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value) And (xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value)) Or ((xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value) And (xnode2.Attributes("CHALLANDATE").Value <> finaldate)) Then
                '                xnode2.Attributes("ACTION").InnerText = "U"
                '                Exit For
                '            End If
                '        Next
                '    Next
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ElseIf rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "PD" Then

                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusPD, objTempXmlDoc)
                End If

                'For Rowno = 0 To gvChallanStatusPD.Rows.Count - 1
                '    With objXmlNodeClone
                '        .Attributes("ACTION").Value = ""
                '        .Attributes("ROWID").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '        'If CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '        '    ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '        '    .Attributes("CHALLANDATE").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '        'Else

                '        If (CType(gvChallanStatusPD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text <> "") Then
                '            .Attributes("CHALLANDATE").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(2) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(1) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(0)
                '        Else
                '            .Attributes("CHALLANDATE").Value = ""
                '        End If
                '        ' End If
                '        .Attributes("ChallanStatus").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue
                '        .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim
                '        If CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '            .Attributes("PCDATE").Value = CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            .Attributes("PCDATE").Value = ""
                '        End If
                '        '.Attributes("PCDATE").Value = ""
                '    End With
                '    objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '    objXmlNodeClone = objXmlNode.CloneNode(True)
                'Next
                'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))
                'Dim nl1, nl2 As XmlNodeList
                'nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                'nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")
                'For Each xnode1 In nl2
                '    For Each xnode2 In nl1
                '        If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> xnode1.Attributes("CHALLANDATE").Value) Then
                '            'If (xnode2.Attributes("ChallanStatus").InnerText = "" And xnode2.Attributes("CHALLANDATE").InnerText = "") Then
                '            '    lblError.Text = "Please enter valid Challan Type and ChallanStatus"
                '            '    Exit Sub
                '            'End If
                '            xnode2.Attributes("ACTION").InnerText = "U"
                '            Exit For
                '        End If
                '    Next
                'Next

                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ElseIf rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "PR" Then

                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusPR, objTempXmlDoc)
                End If

                'For Rowno = 0 To gvChallanStatusPR.Rows.Count - 1
                '    With objXmlNodeClone
                '        .Attributes("ACTION").Value = ""
                '        .Attributes("ROWID").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '        If CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '            ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '            .Attributes("CHALLANDATE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '            '.Attributes("CHALLANDATE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim
                '        Else
                '            If (CType(gvChallanStatusPR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text <> "") Then
                '                .Attributes("CHALLANDATE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(2) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(1) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(0)
                '                '.Attributes("CHALLANDATE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim
                '            Else
                '                .Attributes("CHALLANDATE").Value = ""
                '            End If
                '        End If
                '        .Attributes("ChallanStatus").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue
                '        .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim
                '        If CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '            .Attributes("PCDATE").Value = CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusPR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            .Attributes("PCDATE").Value = ""
                '        End If
                '        '.Attributes("PCDATE").Value = ""
                '    End With
                '    objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '    objXmlNodeClone = objXmlNode.CloneNode(True)
                'Next
                'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))
                'Dim nl1, nl2 As XmlNodeList
                'nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                'nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")
                'For Each xnode1 In nl2
                '    For Each xnode2 In nl1
                '        If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> xnode1.Attributes("CHALLANDATE").Value) Then
                '            'If (xnode2.Attributes("ChallanStatus").InnerText = "" And xnode2.Attributes("CHALLANDATE").InnerText = "") Then
                '            '    lblError.Text = "Please enter valid Challan Type and ChallanStatus"
                '            '    Exit Sub
                '            'End If
                '            xnode2.Attributes("ACTION").InnerText = "U"
                '            Exit For
                '        End If
                '    Next
                'Next



            ElseIf rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "MI" Then

                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusMI, objTempXmlDoc)
                End If


                'For Rowno = 0 To gvChallanStatusMI.Rows.Count - 1
                '    With objXmlNodeClone
                '        .Attributes("ACTION").Value = ""
                '        .Attributes("ROWID").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '        If CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '            ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '            .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            If (CType(gvChallanStatusMI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text <> "") Then
                '                .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(2) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(1) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(0)
                '            Else
                '                .Attributes("CHALLANDATE").Value = ""
                '            End If
                '        End If
                '        .Attributes("ChallanStatus").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue
                '        .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim
                '        If CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '            .Attributes("PCDATE").Value = CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMI.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            .Attributes("PCDATE").Value = ""
                '        End If
                '        '.Attributes("PCDATE").Value = ""
                '    End With
                '    objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '    objXmlNodeClone = objXmlNode.CloneNode(True)
                'Next
                'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))
                'Dim nl1, nl2 As XmlNodeList
                'nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                'nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")
                'For Each xnode1 In nl2
                '    For Each xnode2 In nl1
                '        If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> xnode1.Attributes("CHALLANDATE").Value) Then
                '            'If (xnode2.Attributes("ChallanStatus").InnerText = "" And xnode2.Attributes("CHALLANDATE").InnerText = "") Then
                '            '    lblError.Text = "Please enter valid Challan Type and ChallanStatus"
                '            '    Exit Sub
                '            'End If
                '            xnode2.Attributes("ACTION").InnerText = "U"
                '            Exit For
                '        End If
                '    Next
                'Next

            ElseIf rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "MD" Then


                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusMD, objTempXmlDoc)
                End If


                'For Rowno = 0 To gvChallanStatusMD.Rows.Count - 1
                '    With objXmlNodeClone
                '        .Attributes("ACTION").Value = ""
                '        .Attributes("ROWID").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '        If CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '            ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '            .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            If (CType(gvChallanStatusMD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text <> "") Then
                '                .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(2) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(1) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(0)
                '            Else
                '                .Attributes("CHALLANDATE").Value = ""
                '            End If
                '        End If
                '        .Attributes("ChallanStatus").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue
                '        .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim
                '        If CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '            .Attributes("PCDATE").Value = CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMD.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            .Attributes("PCDATE").Value = ""
                '        End If
                '        '.Attributes("PCDATE").Value = ""
                '    End With
                '    objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '    objXmlNodeClone = objXmlNode.CloneNode(True)
                'Next
                'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))
                'Dim nl1, nl2 As XmlNodeList
                'nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                'nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")
                'For Each xnode1 In nl2
                '    For Each xnode2 In nl1
                '        If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> xnode1.Attributes("CHALLANDATE").Value) Then
                '            'If (xnode2.Attributes("ChallanStatus").InnerText = "" And xnode2.Attributes("CHALLANDATE").InnerText = "") Then
                '            '    lblError.Text = "Please enter valid Challan Type and ChallanStatus"
                '            '    Exit Sub
                '            'End If
                '            xnode2.Attributes("ACTION").InnerText = "U"
                '            Exit For
                '        End If
                '    Next
                'Next
                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            ElseIf rdHardWareType.SelectedValue + rdChallanType.SelectedValue = "MR" Then

                If Session("ChallanStatusSearchXML") IsNot Nothing Then
                    objInputXml = ReturnXmlInput(gvChallanStatusMR, objTempXmlDoc)
                End If

                'For Rowno = 0 To gvChallanStatusMR.Rows.Count - 1
                '    With objXmlNodeClone
                '        .Attributes("ACTION").Value = ""
                '        .Attributes("ROWID").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                '        If CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value <> "" Then
                '            ' txtChallanDate.Text = Format(CDate(Request.QueryString("ChDate")), "dd/MM/yyyy")
                '            .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdChallanDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            If (CType(gvChallanStatusPI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text <> "") Then
                '                .Attributes("CHALLANDATE").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(2) + CType(gvChallanStatusPI.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(1) + CType(gvChallanStatusMR.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim.Split("/")(0)
                '            Else
                '                .Attributes("CHALLANDATE").Value = ""
                '            End If
                '        End If
                '        .Attributes("ChallanStatus").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue
                '        .Attributes("CHALLANTYPE").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim
                '        If CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                '            .Attributes("PCDATE").Value = CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(gvChallanStatusMR.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                '        Else
                '            .Attributes("PCDATE").Value = ""
                '        End If
                '        '.Attributes("PCDATE").Value = ""
                '    End With
                '    objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
                '    objXmlNodeClone = objXmlNode.CloneNode(True)
                'Next
                'objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))
                'Dim nl1, nl2 As XmlNodeList
                'nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
                'nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")
                'For Each xnode1 In nl2
                '    For Each xnode2 In nl1
                '        Dim finaldate As String
                '        Dim strDate As String = xnode1.Attributes("CHALLANDATE").Value.Trim()
                '        If strDate IsNot Nothing Or strDate.Trim.Length <> 0 Then
                '            finaldate = Year(Convert.ToDateTime(strDate)) + Month(Convert.ToDateTime(strDate)) + Day(Convert.ToDateTime(strDate))
                '        End If
                '        If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> finaldate) Then
                '            ' If (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value) Or (xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value And xnode2.Attributes("CHALLANDATE").Value <> xnode1.Attributes("CHALLANDATE").Value) Then
                '            'If (xnode2.Attributes("ChallanStatus").InnerText = "" And xnode2.Attributes("CHALLANDATE").InnerText = "") Then
                '            '    lblError.Text = "Please enter valid Challan Type and ChallanStatus"
                '            '    Exit Sub
                '            'End If
                '            xnode2.Attributes("ACTION").InnerText = "U"
                '            Exit For
                '        End If
                '    Next
                'Next
            End If

            objOutputXml = objChallanStatus.UpdateChallanStatus(objInputXml)

            If objOutputXml.DocumentElement.SelectSingleNode("Errors").Attributes("Status").Value.ToUpper = "FALSE" Then
                lblError.Text = objeAAMSMessage.messInsert
            Else
                lblError.Text = objOutputXml.DocumentElement.SelectSingleNode("Errors/Error").Attributes("Description").Value
            End If
        Catch ex As Exception
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

    Private Function ReturnXmlInput(ByVal grdcom As GridView, ByVal objxml As XmlDocument) As XmlDocument

        Dim objInputXml, objOutputXml, objTempXmlDoc As New XmlDocument
        Dim objXmlNode, xnode1, xnode2 As XmlNode
        Dim objXmlNodeClone As XmlNode
        Dim Rowno As Integer
        Dim objChallanStatus As New AAMS.bizInventory.bzChallan

        objTempXmlDoc.LoadXml(Session("ChallanStatusSearchXML"))

        objInputXml.LoadXml("<INV_UPDATECHALLANSTATUS_INPUT><CHALLAN  ACTION='' ROWID='' CHALLANDATE='' ChallanStatus='' CHALLANTYPE='' PCDATE='' /></INV_UPDATECHALLANSTATUS_INPUT>")
        objXmlNode = objInputXml.DocumentElement.SelectSingleNode("CHALLAN")
        objXmlNodeClone = objXmlNode.CloneNode(True)


        For Rowno = 0 To grdcom.Rows.Count - 1
            With objXmlNodeClone
                .Attributes("ACTION").Value = ""
                .Attributes("ROWID").Value = CType(grdcom.Rows(Rowno).FindControl("hdRowId"), HiddenField).Value()

                Dim strChallanDate As String = CType(grdcom.Rows(Rowno).FindControl("txtChallanDate"), TextBox).Text.Trim

                If (strChallanDate <> "") Then
                    .Attributes("CHALLANDATE").Value = strChallanDate.Split("/")(2) + strChallanDate.Split("/")(1) + strChallanDate.Split("/")(0)
                End If
                ' End If
                Dim strChallanStatus As String = CType(grdcom.Rows(Rowno).FindControl("drpChallan"), DropDownList).SelectedValue.Trim
                .Attributes("ChallanStatus").Value = strChallanStatus
                .Attributes("CHALLANTYPE").Value = CType(grdcom.Rows(Rowno).FindControl("hdChallanType"), HiddenField).Value.Trim

                If CType(grdcom.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value <> "" Then
                    .Attributes("PCDATE").Value = CType(grdcom.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(2) + CType(grdcom.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(1) + CType(grdcom.Rows(Rowno).FindControl("hdPCDate"), HiddenField).Value.Trim.Split("/")(0)
                Else
                    .Attributes("PCDATE").Value = ""
                End If
            End With
            objInputXml.DocumentElement.AppendChild(objXmlNodeClone)
            objXmlNodeClone = objXmlNode.CloneNode(True)
        Next
        objInputXml.DocumentElement.RemoveChild(objInputXml.DocumentElement.SelectSingleNode("CHALLAN[@ACTION='']"))


        Dim nl1, nl2 As XmlNodeList
        nl2 = objTempXmlDoc.DocumentElement.SelectNodes("CHALLAN")
        nl1 = objInputXml.DocumentElement.SelectNodes("CHALLAN")



        For Each xnode1 In nl2
            For Each xnode2 In nl1
                Dim finaldate As String = ""
                Dim strDate As String = xnode1.Attributes("CHALLANDATE").Value.Trim()
                If strDate.Trim.Length <> 0 Then
                    finaldate = Year(Convert.ToDateTime(strDate).ToShortDateString).ToString + Month(Convert.ToDateTime(strDate).ToShortDateString).ToString("00") + Day(Convert.ToDateTime(strDate).ToShortDateString).ToString("00")
                End If

                If ((xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value) And (xnode2.Attributes("ChallanStatus").Value <> xnode1.Attributes("ChallanStatus").Value)) Or ((xnode2.Attributes("ROWID").Value = xnode1.Attributes("ROWID").Value) And (xnode2.Attributes("CHALLANDATE").Value <> finaldate)) Then
                    xnode2.Attributes("ACTION").InnerText = "U"
                    Exit For
                End If
            Next
        Next
        Return objInputXml
    End Function

    Private Function GetSortColumnIndex(ByVal grd As GridView) As Int16
        Dim field As DataControlField
        For Each field In grd.Columns
            If field.SortExpression = ViewState("SortName").ToString().Trim() Then
                Return grd.Columns.IndexOf(field)
            End If
        Next
        Return 0
    End Function

End Class
